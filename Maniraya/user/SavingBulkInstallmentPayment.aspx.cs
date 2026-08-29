using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class user_SavingBulkInstallmentPayment : Page
{
    [WebMethod(EnableSession = true)]
    public static bool CheckOnlineTransactionId(string onlineTransactionId)
    {
        if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["userid"] == null)
        {
            return false;
        }

        return SavingProductHelper.IsOnlineTransactionIdUsed(onlineTransactionId);
    }

    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        Page.Form.Enctype = "multipart/form-data";
        try { SavingProductHelper.EnsureBulkInstallmentPaymentSchema(); }
        catch { }
        try { SavingProductHelper.ConfirmDueBulkInstallmentDeliveries(); }
        catch { }

        if (!IsPostBack)
        {
            LoadQrCode();
        }

        LoadProducts();
    }

    void LoadQrCode()
    {
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable("SELECT TOP 1 branchname FROM CompanyAccountDetail");
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            string imageName = Convert.ToString(dt.Rows[0]["branchname"]);
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                lblqrcode.Text = "<img src=\"../ProductImage/" + Server.HtmlEncode(imageName) + "\" alt=\"Payment QR\" />";
            }
        }
    }

    void LoadProducts()
    {
        GridView1.DataSource = GetEligibleProducts();
        GridView1.DataBind();
    }

    DataTable GetEligibleProducts()
    {
        string userId = Convert.ToString(Session["userid"] ?? string.Empty).Trim();
        DataTable result = CreateListingTable();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return result;
        }

        DataTable accounts = RunListingSql(@"
SELECT sd.*, pm.productname
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
WHERE sd.userid = '" + SqlEscape(userId) + "'");

        if (accounts.Columns.Count == 0 || accounts.Rows.Count == 0)
        {
            accounts = RunListingSql(@"
SELECT sd.*, pm.productname
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
WHERE LTRIM(RTRIM(sd.userid)) = '" + SqlEscape(userId) + "'");
        }

        if (accounts.Columns.Count == 0)
        {
            accounts = RunListingSql("SELECT * FROM SavingAccountDetail WITH (NOLOCK) WHERE userid = '" + SqlEscape(userId) + "'");
        }

        DataTable installments = RunListingSql(
            "SELECT * FROM SavingAccountInstallmentDetail WITH (NOLOCK) WHERE userid = '" + SqlEscape(userId) + "'");
        if (installments.Columns.Count == 0 || installments.Rows.Count == 0)
        {
            DataTable instAlt = RunListingSql(
                "SELECT * FROM SavingAccountInstallmentDetail WITH (NOLOCK) WHERE LTRIM(RTRIM(userid)) = '" + SqlEscape(userId) + "'");
            if (instAlt.Columns.Count > 0)
            {
                installments = instAlt;
            }
        }

        DataTable bulkPays = RunListingSql(
            "SELECT * FROM SavingBulkInstallmentPayment WITH (NOLOCK) WHERE userid = '" + SqlEscape(userId) + "'");

        if (accounts.Columns.Count == 0)
        {
            return result;
        }

        string idCol = FindCol(accounts, "id");
        string orderCol = FindCol(accounts, "orderid", "OrderId");
        string couponCol = FindCol(accounts, "couponcode", "CouponCode");
        string statusCol = FindCol(accounts, "status", "Status");
        string productCol = FindCol(accounts, "productname", "ProductName");
        string approveCol = FindCol(accounts, "approvedate", "ApproveDate");
        string planCol = FindCol(accounts, "PlanType", "plantype");
        string userCol = FindCol(accounts, "userid", "UserId");

        string instOrderCol = FindCol(installments, "orderid", "OrderId");
        string instCouponCol = FindCol(installments, "CouponCode", "couponcode");
        string instNoCol = FindCol(installments, "InstNo", "instno");
        string instStatusCol = FindCol(installments, "Status", "status");
        string instAmountCol = FindCol(installments, "Amount", "amount");

        string bpIdCol = FindCol(bulkPays, "Id", "id");
        string bpCouponCol = FindCol(bulkPays, "CouponCode", "couponcode");
        string bpStatusCol = FindCol(bulkPays, "Status", "status");
        string bpRemarkCol = FindCol(bulkPays, "Remark", "remark");
        string bpDateCol = FindCol(bulkPays, "RequestDate", "requestdate");

        foreach (DataRow account in accounts.Rows)
        {
            string status = GetColStr(account, statusCol);
            if (!IsApprovedStatus(status))
            {
                continue;
            }

            if (IsBulk18Plan(GetColStr(account, planCol)))
            {
                continue;
            }

            string coupon = GetColStr(account, couponCol);
            string orderId = GetColStr(account, orderCol);
            if (string.IsNullOrWhiteSpace(coupon) && string.IsNullOrWhiteSpace(orderId))
            {
                continue;
            }

            int pendingCount = 0;
            int processingCount = 0;
            int approvedCount = 0;
            int instCount = 0;
            decimal totalAmount = 0m;
            decimal emiAmount = 0m;

            if (installments.Columns.Count > 0 && instNoCol != null)
            {
                foreach (DataRow inst in installments.Rows)
                {
                    int instNo = ParseInstNo(inst[instNoCol]);
                    if (instNo < 2 || instNo > 18)
                    {
                        continue;
                    }

                    if (!InstallmentMatchesAccount(inst, coupon, orderId, instCouponCol, instOrderCol))
                    {
                        continue;
                    }

                    AddInstallmentCounts(inst, instStatusCol, instAmountCol,
                        ref instCount, ref pendingCount, ref processingCount, ref approvedCount,
                        ref totalAmount, ref emiAmount);
                }

                if (instCount == 0 && !string.IsNullOrWhiteSpace(orderId) && instOrderCol != null)
                {
                    foreach (DataRow inst in installments.Rows)
                    {
                        int instNo = ParseInstNo(inst[instNoCol]);
                        if (instNo < 2 || instNo > 18)
                        {
                            continue;
                        }

                        string instOrder = GetColStr(inst, instOrderCol);
                        if (!instOrder.Equals(orderId, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        AddInstallmentCounts(inst, instStatusCol, instAmountCol,
                            ref instCount, ref pendingCount, ref processingCount, ref approvedCount,
                            ref totalAmount, ref emiAmount);
                    }
                }
            }

            string bulkStatus = string.Empty;
            string bulkRemark = string.Empty;
            object bulkId = DBNull.Value;
            object bulkDate = DBNull.Value;
            if (bulkPays.Columns.Count > 0 && bpCouponCol != null && !string.IsNullOrWhiteSpace(coupon))
            {
                DataRow latest = null;
                int latestId = -1;
                foreach (DataRow bp in bulkPays.Rows)
                {
                    if (!GetColStr(bp, bpCouponCol).Equals(coupon, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    int idVal = bpIdCol != null ? (int)ParseDecimal(bp[bpIdCol]) : 0;
                    if (latest == null || idVal >= latestId)
                    {
                        latest = bp;
                        latestId = idVal;
                    }
                }

                if (latest != null)
                {
                    bulkStatus = bpStatusCol != null ? GetColStr(latest, bpStatusCol) : string.Empty;
                    bulkRemark = bpRemarkCol != null ? GetColStr(latest, bpRemarkCol) : string.Empty;
                    if (bpIdCol != null)
                    {
                        bulkId = latest[bpIdCol];
                    }
                    if (bpDateCol != null)
                    {
                        bulkDate = latest[bpDateCol];
                    }
                }
            }

            string payStatus = "Ready";
            if (processingCount > 0 || bulkStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase))
            {
                payStatus = "Processing";
            }
            else if (pendingCount > 0)
            {
                payStatus = "Ready";
            }
            else if (approvedCount > 0)
            {
                payStatus = "Paid";
            }

            DataRow row = result.NewRow();
            row["id"] = idCol != null ? Convert.ToInt32(ParseDecimal(account[idCol])) : 0;
            row["orderid"] = orderId;
            row["userid"] = userCol != null ? (object)GetColStr(account, userCol) : userId;
            row["couponcode"] = coupon;
            row["approvedate"] = approveCol != null ? account[approveCol] : DBNull.Value;
            row["productname"] = string.IsNullOrWhiteSpace(GetColStr(account, productCol))
                ? "Saving Product"
                : GetColStr(account, productCol);
            row["EmiAmount"] = emiAmount;
            row["TotalAmount"] = totalAmount;
            row["PendingCount"] = pendingCount;
            row["ProcessingCount"] = processingCount;
            row["ApprovedCount"] = approvedCount;
            row["InstCount"] = instCount;
            row["BulkRequestId"] = bulkId;
            row["BulkStatus"] = bulkStatus;
            row["BulkRemark"] = bulkRemark;
            row["BulkRequestDate"] = bulkDate;
            row["PayStatus"] = payStatus;
            result.Rows.Add(row);
        }

        try
        {
            DataView view = result.DefaultView;
            view.Sort = "PendingCount DESC, id DESC";
            return view.ToTable();
        }
        catch
        {
            return result;
        }
    }

    static DataTable CreateListingTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(int));
        dt.Columns.Add("orderid", typeof(string));
        dt.Columns.Add("userid", typeof(string));
        dt.Columns.Add("couponcode", typeof(string));
        dt.Columns.Add("approvedate", typeof(object));
        dt.Columns.Add("productname", typeof(string));
        dt.Columns.Add("EmiAmount", typeof(decimal));
        dt.Columns.Add("TotalAmount", typeof(decimal));
        dt.Columns.Add("PendingCount", typeof(int));
        dt.Columns.Add("ProcessingCount", typeof(int));
        dt.Columns.Add("ApprovedCount", typeof(int));
        dt.Columns.Add("InstCount", typeof(int));
        dt.Columns.Add("BulkRequestId", typeof(object));
        dt.Columns.Add("BulkStatus", typeof(string));
        dt.Columns.Add("BulkRemark", typeof(string));
        dt.Columns.Add("BulkRequestDate", typeof(object));
        dt.Columns.Add("PayStatus", typeof(string));
        return dt;
    }

    static void AddInstallmentCounts(DataRow inst, string instStatusCol, string instAmountCol,
        ref int instCount, ref int pendingCount, ref int processingCount, ref int approvedCount,
        ref decimal totalAmount, ref decimal emiAmount)
    {
        instCount++;
        string instStatus = instStatusCol != null ? GetColStr(inst, instStatusCol) : string.Empty;
        decimal amount = instAmountCol != null ? ParseDecimal(inst[instAmountCol]) : 0m;
        if (emiAmount <= 0m && amount > 0m)
        {
            emiAmount = amount;
        }

        if (IsProcessingStatus(instStatus))
        {
            processingCount++;
        }
        else if (IsPaidStatus(instStatus))
        {
            approvedCount++;
        }
        else
        {
            pendingCount++;
            totalAmount += amount;
            if (amount > 0m)
            {
                emiAmount = amount;
            }
        }
    }

    static bool InstallmentMatchesAccount(DataRow inst, string coupon, string orderId, string instCouponCol, string instOrderCol)
    {
        string instCoupon = instCouponCol != null ? GetColStr(inst, instCouponCol) : string.Empty;
        string instOrder = instOrderCol != null ? GetColStr(inst, instOrderCol) : string.Empty;

        if (!string.IsNullOrWhiteSpace(coupon) && instCoupon.Equals(coupon, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(instCoupon)
            && !string.IsNullOrWhiteSpace(orderId)
            && instOrder.Equals(orderId, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(coupon)
            && !string.IsNullOrWhiteSpace(orderId)
            && instOrder.Equals(orderId, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(orderId) && instOrder.Equals(orderId, StringComparison.OrdinalIgnoreCase)
            && (string.IsNullOrWhiteSpace(instCoupon) || string.IsNullOrWhiteSpace(coupon)
                || instCoupon.Equals(coupon, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return false;
    }

    static bool IsApprovedStatus(string status)
    {
        string value = (status ?? string.Empty).Trim().ToUpperInvariant();
        return value == "APPROVED" || value == "APPROVE" || value == "1" || value == "ACTIVE";
    }

    static bool IsProcessingStatus(string status)
    {
        return (status ?? string.Empty).Trim().Equals("Processing", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsPaidStatus(string status)
    {
        string value = (status ?? string.Empty).Trim().ToUpperInvariant();
        return value == "APPROVED" || value == "APPROVE" || value == "1" || value == "ACTIVE" || value == "PAID";
    }

    static bool IsBulk18Plan(string planType)
    {
        return (planType ?? string.Empty).Trim().Equals("Bulk18", StringComparison.OrdinalIgnoreCase);
    }

    static int ParseInstNo(object value)
    {
        return (int)ParseDecimal(value);
    }

    static decimal ParseDecimal(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }

        decimal amount;
        if (decimal.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out amount)
            || decimal.TryParse(Convert.ToString(value), out amount))
        {
            return amount;
        }

        return 0m;
    }

    static string FindCol(DataTable dt, params string[] names)
    {
        if (dt == null || names == null)
        {
            return null;
        }

        foreach (string name in names)
        {
            foreach (DataColumn col in dt.Columns)
            {
                if (string.Equals(col.ColumnName, name, StringComparison.OrdinalIgnoreCase))
                {
                    return col.ColumnName;
                }
            }
        }

        return null;
    }

    static string GetColStr(DataRow row, string column)
    {
        if (row == null || string.IsNullOrEmpty(column) || !row.Table.Columns.Contains(column) || row[column] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(row[column]).Trim();
    }

    DataTable RunListingSql(string sql)
    {
        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunSelectQuerydatatable(sql);
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        LinkButton btnPay = (LinkButton)e.Row.FindControl("btnPay");
        Label lblremark = (Label)e.Row.FindControl("lblremark");
        Label lblcoupon = (Label)e.Row.FindControl("lblcoupon");
        Label lblproduct = (Label)e.Row.FindControl("lblproduct");
        Label lbltotal = (Label)e.Row.FindControl("lbltotal");
        Label lblemi = (Label)e.Row.FindControl("lblemi");
        if (lblstatus == null)
        {
            return;
        }

        string payStatus = (lblstatus.Text ?? string.Empty).Trim();
        DataRowView drv = e.Row.DataItem as DataRowView;
        int pendingCount = GetRowInt(drv, "PendingCount");
        int processingCount = GetRowInt(drv, "ProcessingCount");
        bool canPay = pendingCount > 0 && processingCount <= 0
            && !payStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase)
            && !payStatus.Equals("Paid", StringComparison.OrdinalIgnoreCase);

        if (btnPay != null)
        {
            btnPay.Visible = canPay;
            if (canPay)
            {
                btnPay.Attributes["data-coupon"] = lblcoupon != null ? lblcoupon.Text.Trim() : string.Empty;
                btnPay.Attributes["data-product"] = lblproduct != null ? lblproduct.Text.Trim() : string.Empty;
                btnPay.Attributes["data-emi"] = lblemi != null ? lblemi.Text.Trim() : string.Empty;
                btnPay.Attributes["data-total"] = lbltotal != null ? lbltotal.Text.Trim() : string.Empty;
                btnPay.Attributes["data-count"] = pendingCount.ToString();
                btnPay.OnClientClick = "return openBulkPayFromRow(this);";
            }
        }

        if (payStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.Text = "Processing";
            lblstatus.CssClass = "dash-saving-status is-processing";
        }
        else if (payStatus.Equals("Paid", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.Text = "Paid";
            lblstatus.CssClass = "dash-saving-status is-paid";
        }
        else if (payStatus.Equals("Ready", StringComparison.OrdinalIgnoreCase) || pendingCount > 0)
        {
            string bulkStatus = drv != null && drv.Row.Table.Columns.Contains("BulkStatus")
                ? Convert.ToString(drv["BulkStatus"])
                : string.Empty;
            if (bulkStatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
            {
                lblstatus.Text = "Rejected";
                lblstatus.CssClass = "dash-saving-status is-unpaid";
            }
            else
            {
                lblstatus.Text = "Unpaid";
                lblstatus.CssClass = "dash-saving-status is-unpaid";
            }
        }
        else
        {
            lblstatus.Text = "Not eligible";
            lblstatus.CssClass = "dash-saving-status is-unpaid";
        }

        if (lblremark != null && string.IsNullOrWhiteSpace(lblremark.Text))
        {
            lblremark.Text = "-";
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "pay")
        {
            return;
        }

        int index;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out index) || index < 0 || index >= GridView1.Rows.Count)
        {
            return;
        }

        GridViewRow row = GridView1.Rows[index];
        Label lblcoupon = (Label)row.FindControl("lblcoupon");
        Label lblproduct = (Label)row.FindControl("lblproduct");
        Label lbltotal = (Label)row.FindControl("lbltotal");
        Label lblemi = (Label)row.FindControl("lblemi");

        hfCouponCode.Value = lblcoupon != null ? lblcoupon.Text.Trim() : string.Empty;
        txtcouponedit.Text = hfCouponCode.Value;
        txtproductedit.Text = lblproduct != null ? lblproduct.Text.Trim() : string.Empty;
        txtamountedit.Text = lbltotal != null ? lbltotal.Text.Trim() : string.Empty;
        txtemiedit.Text = lblemi != null ? lblemi.Text.Trim() : string.Empty;
        txttransactionidedit.Text = string.Empty;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string coupon = (hfCouponCode.Value ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(coupon))
        {
            Message.Show("Please select a product to pay.");
            LoadProducts();
            return;
        }

        if (string.IsNullOrWhiteSpace(txttransactionidedit.Text))
        {
            Message.Show("Please enter UTR No / Transaction ID.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        if (!HasPaymentScreenshot())
        {
            Message.Show("Please upload payment screenshot.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        string utr = txttransactionidedit.Text.Trim();
        if (SavingProductHelper.IsOnlineTransactionIdUsed(utr))
        {
            Message.Show("This UTR No / Transaction ID is already used.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        string imageName = UploadImage();
        if (string.IsNullOrWhiteSpace(imageName))
        {
            Message.Show("Invalid payment screenshot. Please upload JPG, PNG, WEBP or GIF image.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        string userId = Convert.ToString(Session["userid"]).Trim();
        string res = SavingProductHelper.ExecuteScalarProc("sp_add_SavingBulkInstallmentPayment", new[]
        {
            new SqlParameter("@UserId", userId),
            new SqlParameter("@CouponCode", coupon),
            new SqlParameter("@OnlineTransactionId", utr),
            new SqlParameter("@ImageName", imageName),
            new SqlParameter("@EntryBy", userId)
        });

        LoadProducts();

        if (res == "t")
        {
            Message.Show("EMI payment request submitted successfully. Admin will verify and approve.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "Closepopup();", true);
        }
        else if (res == "f")
        {
            Message.Show("A payment request is already in process for this coupon.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (res == "u")
        {
            Message.Show("This UTR No / Transaction ID is already used.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (res == "n")
        {
            Message.Show("This coupon is not eligible for bulk EMI payment right now. Remaining unpaid installments are required.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            Message.Show("Unable to submit payment request. Please try again.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    string UploadImage()
    {
        HttpPostedFile postedFile = GetPostedPaymentFile();
        if (postedFile == null)
        {
            return string.Empty;
        }

        string extension = Path.GetExtension(postedFile.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = ".jpg";
        }

        extension = extension.ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            return string.Empty;
        }

        string uploadFolder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string imageName = DateTime.Now.Ticks + extension;
        postedFile.SaveAs(Path.Combine(uploadFolder, imageName));
        return imageName;
    }

    HttpPostedFile GetPostedPaymentFile()
    {
        if (FileUpload1 != null && FileUpload1.HasFile)
        {
            return FileUpload1.PostedFile;
        }

        if (FileUpload1 != null)
        {
            string key = FileUpload1.UniqueID;
            if (!string.IsNullOrEmpty(key) && Request.Files[key] != null && Request.Files[key].ContentLength > 0)
            {
                return Request.Files[key];
            }
        }

        for (int i = 0; i < Request.Files.Count; i++)
        {
            HttpPostedFile file = Request.Files[i];
            if (file != null && file.ContentLength > 0 && !string.IsNullOrWhiteSpace(file.FileName))
            {
                return file;
            }
        }

        return null;
    }

    bool HasPaymentScreenshot()
    {
        return GetPostedPaymentFile() != null;
    }

    static int GetRowInt(DataRowView drv, string column)
    {
        if (drv == null || drv.Row == null || drv.Row.Table == null || !drv.Row.Table.Columns.Contains(column))
        {
            return 0;
        }

        object value = drv[column];
        if (value == null || value == DBNull.Value)
        {
            return 0;
        }

        decimal amount;
        if (decimal.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out amount)
            || decimal.TryParse(Convert.ToString(value), out amount))
        {
            return (int)amount;
        }

        return 0;
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

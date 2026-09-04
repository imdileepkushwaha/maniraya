using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_SavingProductInstallmentDetail : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    public static bool CheckOnlineTransactionId(string onlineTransactionId, string installmentId)
    {
        if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["userid"] == null)
        {
            return false;
        }

        int excludeId = 0;
        int.TryParse(installmentId, out excludeId);
        return SavingProductHelper.IsOnlineTransactionIdUsed(onlineTransactionId, excludeId);
    }

    Data ObjData = new Data();
    clsProduct objproduct = new clsProduct();
    clsUser objUser = new clsUser();
    string CouponCode
    {
        get { return ViewState["InstallmentCouponCode"] as string ?? string.Empty; }
        set { ViewState["InstallmentCouponCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (lblCashfreeHint != null)
        {
            lblCashfreeHint.Text = !CashfreeHelper.IsConfigured
                ? "Add CashfreeAppId and CashfreeSecretKey in Web.config to enable Pay Now."
                : (CashfreeHelper.IsSandbox
                    ? "Sandbox mode. Use Cashfree test payment methods."
                    : "Secure online payment.");
        }

        if (!IsPostBack)
        {
            loadprevproduct();
            loadqrocde();
        }
        else
        {
            ShowCouponChip();
        }
    }

    string GetRequestOid()
    {
        HttpContext httpContext = HttpContext.Current;
        if (httpContext == null || httpContext.Request == null)
            return string.Empty;

        string oid = httpContext.Request.QueryString["oid"];
        if (string.IsNullOrWhiteSpace(oid))
            oid = httpContext.Request.Form["oid"];

        return string.IsNullOrWhiteSpace(oid) ? string.Empty : oid.Trim();
    }

    public DataTable getPrevProduct()
    {
        if (string.IsNullOrWhiteSpace(CouponCode))
            return new DataTable();

        string userId = Session["userid"] != null ? Convert.ToString(Session["userid"]).Trim() : string.Empty;
        if (string.IsNullOrWhiteSpace(userId))
            return new DataTable();

        SavingProductHelper.EnsureInstallmentProductAssignTable();
        SavingProductHelper.EnsureBulkColumns();
        SavingProductHelper.EnsureBulkInstallmentPaymentSchema();
        SavingProductHelper.EnsureBulkInstallmentsForCoupon(CouponCode);

        bool hasInstCoupon = SavingProductHelper.HasInstallmentCouponCodeColumn();
        string couponSelect = hasInstCoupon
            ? "COALESCE(NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), ''), sd.couponcode) AS couponcode"
            : "sd.couponcode";
        string couponFilter = hasInstCoupon
            ? @" AND (
                    LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))) = '" + SqlEscape(CouponCode) + @"'
                    OR (
                        NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), '') IS NULL
                        AND LTRIM(RTRIM(ISNULL(sd.couponcode, ''))) = '" + SqlEscape(CouponCode) + @"'
                    )
                )"
            : @" AND LTRIM(RTRIM(ISNULL(sd.couponcode, ''))) = '" + SqlEscape(CouponCode) + "'";

        // Same source as: SELECT * FROM SavingAccountInstallmentDetail WHERE UserId=... AND CouponCode=...
        string str_query = @"SELECT
            sa.id,
            sa.userid,
            sa.orderid,
            sa.instno,
            sa.amount,
            sa.installmentdate,
            sa.approvedate,
            sa.requestdate,
            LTRIM(RTRIM(ISNULL(sa.status, ''))) AS status,
            sa.OnlineTransactionId,
            sa.remark,
            sa.productid,
            ISNULL(sa.BulkPaymentId, 0) AS BulkPaymentId,
            ISNULL(sd.PlanType, '') AS PlanType,
            ud.username,
            " + couponSelect + @",
            CASE
                WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
                    THEN LTRIM(RTRIM(assign_pm.ProductName))
                ELSE 'Not assigned'
            END AS productname
            FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
            OUTER APPLY (
                SELECT TOP 1 sd0.couponcode, sd0.PlanType
                FROM SavingAccountDetail sd0 WITH (NOLOCK)
                WHERE sd0.orderid = sa.OrderId
                  AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
                ORDER BY sd0.id DESC
            ) sd
            LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
                ON ISNULL(ipa.Status, 1) = 1
               AND ISNULL(ipa.ProductId, 0) > 0
               AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
            LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
            LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sa.userid
            WHERE LTRIM(RTRIM(sa.UserId)) = '" + SqlEscape(userId) + @"'
            " + couponFilter + @"
            ORDER BY TRY_CONVERT(INT, sa.InstNo) ASC, sa.id ASC";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt ?? new DataTable();
    }


    public void loadqrocde()
    {
   
        string str_query = @"select top 1 branchname from CompanyAccountDetail";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (dt.Rows.Count > 0)
        {
            lblqrcode.Text = @"<img src=""../ProductImage/" + dt.Rows[0]["branchname"].ToString() + @""" alt=""Payment QR"" />";
        }


    }

    public string Insert_SavingInstallment(clsProduct objState,string str_id)
    {
      

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_add_SavingAccountInstallmentDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@id",str_id),
                new SqlParameter("@OnlineTransactionId",objState.TransactionCode),
                new SqlParameter("@ImageName",objState.ProductImage),

                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }

    void loadprevproduct()
    {
        if (string.IsNullOrWhiteSpace(CouponCode))
        {
            string oid = GetRequestOid();
            if (string.IsNullOrWhiteSpace(oid))
            {
                Response.Redirect("SAvingProductPurchaseReport.aspx");
                return;
            }

            CouponCode = oid;
        }

        ShowCouponChip();

        DataTable dt = getPrevProduct();
        ActiveBulkPayStatus = GetActiveBulkInstallmentPayStatus(CouponCode);
        if (GridView1 != null)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    string ActiveBulkPayStatus
    {
        get { return ViewState["ActiveBulkPayStatus"] as string ?? string.Empty; }
        set { ViewState["ActiveBulkPayStatus"] = value; }
    }

    string GetActiveBulkInstallmentPayStatus(string couponCode)
    {
        if (string.IsNullOrWhiteSpace(couponCode))
            return string.Empty;

        string sql = @"SELECT TOP 1 LTRIM(RTRIM(ISNULL(Status, ''))) AS Status
            FROM SavingBulkInstallmentPayment WITH (NOLOCK)
            WHERE LTRIM(RTRIM(ISNULL(CouponCode, ''))) = '" + SqlEscape(couponCode) + @"'
              AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) IN ('PROCESSING', 'APPROVED', 'APPROVE', 'PAID')
            ORDER BY Id DESC";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(sql);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;

        return Convert.ToString(dt.Rows[0]["Status"]).Trim();
    }

    void ShowCouponChip()
    {
        if (lblCouponCode != null && !string.IsNullOrWhiteSpace(CouponCode))
        {
            lblCouponCode.Text = "Coupon: " + CouponCode;
            lblCouponCode.Visible = true;
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblproductname = (Label)e.Row.FindControl("lblproductname");
        if (lblproductname != null)
        {
            string productName = (lblproductname.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(productName)
                || productName.Equals("Not assigned", StringComparison.OrdinalIgnoreCase)
                || productName.Equals("Not assign", StringComparison.OrdinalIgnoreCase))
            {
                lblproductname.Text = "Not assigned";
                lblproductname.CssClass = "dash-saving-product is-unassigned";
            }
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
        if (lbEdit != null)
        {
            lbEdit.Visible = false;
        }

        if (lblstatus == null)
        {
            return;
        }

        DataRowView drv = e.Row.DataItem as DataRowView;
        string planType = GetRowValue(drv, "PlanType");
        int instNo = 0;
        int.TryParse(GetRowValue(drv, "instno"), out instNo);
        int bulkPaymentId = 0;
        int.TryParse(GetRowValue(drv, "BulkPaymentId"), out bulkPaymentId);

        string rawStatus = (lblstatus.Text ?? string.Empty).Trim();
        string statusNorm = rawStatus.ToLowerInvariant();
        bool isBulk18 = planType.Equals("Bulk18", StringComparison.OrdinalIgnoreCase);
        bool isRejected = statusNorm == "rejected" || statusNorm == "2" || statusNorm == "cancelled" || statusNorm == "canceled";
        bool isApproved = statusNorm == "approved" || statusNorm == "approve" || statusNorm == "1" || statusNorm == "active";
        bool isPaid = statusNorm == "paid";
        bool isProcessing = statusNorm == "processing";
        string bulkPayStatus = (ActiveBulkPayStatus ?? string.Empty).Trim();
        bool bulkPayActive = bulkPayStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase)
            || bulkPayStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase)
            || bulkPayStatus.Equals("Approve", StringComparison.OrdinalIgnoreCase)
            || bulkPayStatus.Equals("Paid", StringComparison.OrdinalIgnoreCase);
        bool coveredByBulkPay = !isRejected && instNo != 1 && (bulkPaymentId > 0 || bulkPayActive);
        bool prepaidCovered = !isRejected && (isBulk18 || instNo == 1 || isPaid || coveredByBulkPay);
        bool canPay = false;

        if (isApproved)
        {
            lblstatus.Text = "Approved";
            lblstatus.CssClass = "dash-saving-status is-approved";
        }
        else if (prepaidCovered && (isProcessing || bulkPayStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase)) && !isPaid && !isBulk18 && instNo != 1)
        {
            lblstatus.Text = "Processing";
            lblstatus.CssClass = "dash-saving-status is-pending";
        }
        else if (prepaidCovered)
        {
            lblstatus.Text = "Paid";
            lblstatus.CssClass = "dash-saving-status is-approved";
        }
        else if (isProcessing)
        {
            lblstatus.Text = "Processing";
            lblstatus.CssClass = "dash-saving-status is-pending";
        }
        else if (isRejected)
        {
            lblstatus.Text = "Rejected";
            lblstatus.CssClass = "dash-saving-status is-rejected";
            canPay = true;
        }
        else
        {
            lblstatus.Text = "Pending";
            lblstatus.CssClass = "dash-saving-status is-pending";
            canPay = true;
        }

        if (lbEdit != null)
        {
            lbEdit.Visible = canPay;
        }
    }

    static string GetRowValue(DataRowView drv, string column)
    {
        if (drv == null || drv.Row == null || drv.Row.Table == null || string.IsNullOrWhiteSpace(column))
            return string.Empty;

        foreach (DataColumn col in drv.Row.Table.Columns)
        {
            if (string.Equals(col.ColumnName, column, StringComparison.OrdinalIgnoreCase)
                && drv[col.ColumnName] != DBNull.Value)
            {
                return Convert.ToString(drv[col.ColumnName]).Trim();
            }
        }

        return string.Empty;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    public string UploadImage()
    {
        string Imagename = "";
        if (FileUpload1.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool isOnline = rbOnlinePayment != null && rbOnlinePayment.Checked;

        if (isOnline)
        {
            if (string.IsNullOrWhiteSpace(txttransactionidedit.Text))
            {
                Message.Show("Please enter UTR No / Transaction ID.");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                return;
            }

            if (!FileUpload1.HasFile)
            {
                Message.Show("Please upload payment screenshot.");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                return;
            }

            objproduct.ProductImage = UploadImage();
            objproduct.TransactionCode = txttransactionidedit.Text.Trim();

            int excludeId = 0;
            int.TryParse(lblidedit.Text, out excludeId);
            if (SavingProductHelper.IsOnlineTransactionIdUsed(objproduct.TransactionCode, excludeId))
            {
                Message.Show("This UTR No / Transaction ID is already used.");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                return;
            }
        }
        else
        {
            objproduct.ProductImage = string.Empty;
            objproduct.TransactionCode = "CASH-" + DateTime.Now.Ticks;
        }

        string res = Insert_SavingInstallment(objproduct, lblidedit.Text);
        if (res == "t")
        {
            loadprevproduct();
            Message.Show(isOnline
                ? "Request Submitted Successfully"
                : "Cash installment request sent to admin successfully");
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else if (res == "f")
        {
            Message.Show("Already in process");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (res == "u")
        {
            Message.Show("This UTR No / Transaction ID is already used.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            Message.Show("unknown error occurred");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblamount = (Label)GridView1.Rows[index].FindControl("lblamount");
            Label lblinstallmentdate = (Label)GridView1.Rows[index].FindControl("lblinstallmentdate");

            lblidedit.Text = lblid.Text;
            txtamountedit.Text = lblamount.Text;
            txtinstallmentdateedit.Text = lblinstallmentdate.Text;
            if (rbOnlinePayment != null) rbOnlinePayment.Checked = true;
            if (rbCashPayment != null) rbCashPayment.Checked = false;
            if (txttransactionidedit != null) txttransactionidedit.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    protected void btnPayCashfree_Click(object sender, EventArgs e)
    {
        if (rbOnlinePayment != null) rbOnlinePayment.Checked = true;
        if (rbCashPayment != null) rbCashPayment.Checked = false;

        int installmentId;
        if (!int.TryParse((lblidedit.Text ?? string.Empty).Trim(), out installmentId) || installmentId <= 0)
        {
            Message.Show("Installment is missing. Please click Pay again.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        decimal amount;
        string amountText = (txtamountedit.Text ?? string.Empty).Replace("₹", "").Replace(",", "").Trim();
        if (!decimal.TryParse(amountText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out amount)
            && !decimal.TryParse(amountText, out amount))
        {
            amount = 0m;
        }

        if (amount <= 0)
        {
            Message.Show("Invalid installment amount.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        string userId = Session["userid"].ToString();
        objUser.UserId = userId;
        DataTable userDt = objUser.getUserDetail(objUser);
        string userName = userDt != null && userDt.Rows.Count > 0 ? Convert.ToString(userDt.Rows[0]["UserName"]) : string.Empty;
        string mobile = userDt != null && userDt.Rows.Count > 0 ? Convert.ToString(userDt.Rows[0]["Mobile"]) : string.Empty;
        string email = userDt != null && userDt.Rows.Count > 0 ? Convert.ToString(userDt.Rows[0]["Email"]) : string.Empty;

        CashfreeHelper.CreateOrderResult result = CashfreeHelper.StartSavingInstallment(
            userId,
            userName,
            mobile,
            email,
            amount,
            installmentId,
            Request);

        if (!result.Ok)
        {
            Message.Show(string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? "Unable to start Cashfree payment."
                : result.ErrorMessage);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            return;
        }

        string sessionId = (result.PaymentSessionId ?? string.Empty).Replace("\\", "").Replace("'", "").Replace("\"", "");
        string mode = string.IsNullOrWhiteSpace(result.Mode) ? "production" : result.Mode.Replace("'", "");
        string script = "setTimeout(function(){ Closepopup(); if (typeof window.startCashfreeCheckout === 'function') { window.startCashfreeCheckout('" +
            sessionId + "', '" + mode + "'); } else { alert('Cashfree checkout script is missing. Please refresh.'); } }, 200);";
        ClientScript.RegisterStartupScript(GetType(), "cfCheckout", script, true);
    }
}

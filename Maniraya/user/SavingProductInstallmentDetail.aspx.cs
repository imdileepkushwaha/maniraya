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

        if (!IsPostBack)
        {
            SavingProductHelper.EnsureBulkColumns();
            SavingProductHelper.ProcessBulkSavingSchedule();
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

        SavingProductHelper.EnsureInstallmentProductAssignTable();
        SavingProductHelper.EnsureBulkInstallmentsForCoupon(CouponCode);

        string str_query = @"SELECT sa.*, ud.username, sd.couponcode,
            ISNULL(sd.PlanType, '') AS PlanType,
            sd.ApproveDate AS ParentApproveDate,
            sd.EntryDate AS ParentEntryDate,
            sd.OnlineTransactionId AS ParentOnlineTransactionId,
            sd.Amount AS ParentAmount,
            CASE
                WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
                    THEN LTRIM(RTRIM(assign_pm.ProductName))
                ELSE 'Not assigned'
            END AS productname
            FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
            LEFT JOIN SavingAccountDetail sd WITH (NOLOCK)
                ON sa.OrderId = sd.orderid
               AND LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(sd.UserId))
            LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
                ON ISNULL(ipa.Status, 1) = 1
               AND ISNULL(ipa.ProductId, 0) > 0
               AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
            LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
            LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sd.userid
            WHERE LTRIM(RTRIM(sd.couponcode)) = '" + SqlEscape(CouponCode) + @"'
            ORDER BY sa.instno";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
            SavingProductHelper.AddMissingFirstInstallmentRows(dt, CouponCode);
            SavingProductHelper.ApplyBulkInstallmentDisplayFallbacks(dt);
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

        SavingProductHelper.EnsureBulkInstallmentsForCoupon(CouponCode);
        SavingProductHelper.ProcessBulkSavingSchedule();
        ShowCouponChip();

        DataTable dt = getPrevProduct();
        if (GridView1 != null)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
                return;

            string status = (lblstatus.Text ?? string.Empty).Trim();
            string planType = string.Empty;
            bool isBulkPrepaidFlag = false;
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv != null)
            {
                if (drv.Row.Table.Columns.Contains("PlanType"))
                {
                    planType = Convert.ToString(drv["PlanType"]).Trim();
                }
                if (drv.Row.Table.Columns.Contains("IsBulkPrepaid") && drv["IsBulkPrepaid"] != DBNull.Value)
                {
                    string prepaid = Convert.ToString(drv["IsBulkPrepaid"]).Trim();
                    isBulkPrepaidFlag = prepaid == "1"
                        || prepaid.Equals("True", StringComparison.OrdinalIgnoreCase)
                        || prepaid.Equals("Y", StringComparison.OrdinalIgnoreCase);
                }
            }

            bool isBulkPrepaid = planType.Equals("Bulk18", StringComparison.OrdinalIgnoreCase)
                || isBulkPrepaidFlag;

            int instNo = 0;
            if (drv != null && drv.Row.Table.Columns.Contains("InstNo"))
            {
                int.TryParse(Convert.ToString(drv["InstNo"]), out instNo);
            }

            if (instNo == 1)
            {
                if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    lblstatus.Text = "Paid";
                    lblstatus.CssClass = "dash-saving-status is-approved";
                }
                if (lbEdit != null)
                {
                    lbEdit.Visible = false;
                }
            }
            else if ((isBulkPrepaid && status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                || status.Equals("Paid", StringComparison.OrdinalIgnoreCase))
            {
                lblstatus.Text = "Paid";
                lblstatus.CssClass = "dash-saving-status is-approved";
                if (lbEdit != null)
                {
                    lbEdit.Visible = false;
                }
            }
            else if (lblstatus.Text == "Pending")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "dash-saving-status is-pending";
                if (lbEdit != null)
                {
                    lbEdit.Visible = true;
                }
            }
            else if (lblstatus.Text == "Paid")
            {
                lblstatus.Text = "Paid";
                lblstatus.CssClass = "dash-saving-status is-approved";
            }
            else if (lblstatus.Text == "Approved")
            {
                lblstatus.Text = "Approved";
                lblstatus.CssClass = "dash-saving-status is-approved";
            }
            else if (lblstatus.Text == "Rejected")
            {
                lblstatus.Text = "Rejected";
                lblstatus.CssClass = "dash-saving-status is-rejected";
                if (lbEdit != null)
                {
                    lbEdit.Visible = true;
                }
            }
        }
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
}

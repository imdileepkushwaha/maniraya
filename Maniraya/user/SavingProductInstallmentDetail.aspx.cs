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
            ud.username,
            " + couponSelect + @",
            CASE
                WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
                    THEN LTRIM(RTRIM(assign_pm.ProductName))
                ELSE 'Not assigned'
            END AS productname
            FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
            OUTER APPLY (
                SELECT TOP 1 sd0.couponcode
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

        string rawStatus = (lblstatus.Text ?? string.Empty).Trim();
        string statusNorm = rawStatus.ToLowerInvariant();
        bool canPay = false;

        if (statusNorm == "approved" || statusNorm == "approve" || statusNorm == "1" || statusNorm == "active")
        {
            lblstatus.Text = "Approved";
            lblstatus.CssClass = "dash-saving-status is-approved";
        }
        else if (statusNorm == "processing")
        {
            lblstatus.Text = "Processing";
            lblstatus.CssClass = "dash-saving-status is-pending";
        }
        else if (statusNorm == "rejected" || statusNorm == "2")
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

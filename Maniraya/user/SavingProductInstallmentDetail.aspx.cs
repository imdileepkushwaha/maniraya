using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_SavingProductInstallmentDetail : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objproduct = new clsProduct();
    clsAccount objaccount = new clsAccount();
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

        Page.Form.Enctype = "multipart/form-data";

        if (!IsPostBack)
        {
            loadprevproduct();
            LoadCompanyPaymentQr();
            ApplyPaymentMethodVisibility();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(CouponCode))
            {
                CouponCode = GetRequestOid();
            }

            ShowCouponChip();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ApplyPaymentMethodVisibility();
    }

    bool IsOnlinePayment()
    {
        return rbOnlinePayment == null || rbOnlinePayment.Checked;
    }

    void ApplyPaymentMethodVisibility()
    {
        bool isOnline = IsOnlinePayment();
        if (pnlOnlinePaymentSection != null)
        {
            pnlOnlinePaymentSection.Style["display"] = isOnline ? "" : "none";
        }

        if (pnlCashPaymentInfo != null)
        {
            pnlCashPaymentInfo.Style["display"] = isOnline ? "none" : "";
        }
    }

    void LoadCompanyPaymentQr()
    {
        DataTable dt = objaccount.getCompanyAccountDetail();
        if (dt == null || dt.Rows.Count == 0)
        {
            imgPaymentQr.ImageUrl = ResolveUrl("~/ProductImage/noimage.png");
            return;
        }

        string qrImage = Convert.ToString(dt.Rows[0]["BranchName"]);
        imgPaymentQr.ImageUrl = string.IsNullOrWhiteSpace(qrImage)
            ? ResolveUrl("~/ProductImage/noimage.png")
            : ResolveUrl("~/ProductImage/" + qrImage);
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

        string str_query = @"SELECT sa.*, ud.username, sd.couponcode, pm.productname
            FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
            LEFT JOIN SavingAccountDetail sd WITH (NOLOCK) ON sa.OrderId = sd.orderid
            LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
            LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sd.userid
            WHERE sd.couponcode = '" + SqlEscape(CouponCode) + @"'
            ORDER BY sa.instno";

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


    public string Insert_SavingInstallment(clsProduct objState, string str_id)
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
            lbEdit.Visible = false;


            if (lblstatus == null)
                return;

            if (lblstatus.Text == "Pending")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "dash-saving-status is-pending";
                lbEdit.Visible = true;
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
            }
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    public string UploadImage()
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

        string imagename = DateTime.Now.Ticks + extension;
        postedFile.SaveAs(Path.Combine(uploadFolder, imagename));
        return imagename;
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

    string GetInstallmentId()
    {
        if (!string.IsNullOrWhiteSpace(hfInstallmentId.Value))
        {
            return hfInstallmentId.Value.Trim();
        }

        return (lblidedit.Text ?? string.Empty).Trim();
    }

    void ShowInstallmentPayModal()
    {
        uplMaster.Update();
        ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), "resetInstallmentPayModal(); showModal();", true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string installmentId = GetInstallmentId();
        if (string.IsNullOrWhiteSpace(installmentId))
        {
            Message.Show("Installment record not found. Please open Pay again.");
            ShowInstallmentPayModal();
            return;
        }

        if (IsOnlinePayment())
        {
            if (string.IsNullOrWhiteSpace(txttransactionidedit.Text))
            {
                Message.Show("Enter UTR No / Transaction ID.");
                ShowInstallmentPayModal();
                return;
            }

            if (!HasPaymentScreenshot())
            {
                Message.Show("Please upload payment screenshot.");
                ShowInstallmentPayModal();
                return;
            }

            objproduct.ProductImage = UploadImage();
            if (string.IsNullOrWhiteSpace(objproduct.ProductImage))
            {
                Message.Show("Invalid payment screenshot. Please upload JPG, PNG, WEBP or GIF image.");
                ShowInstallmentPayModal();
                return;
            }

            objproduct.TransactionCode = txttransactionidedit.Text.Trim();
        }
        else
        {
            objproduct.ProductImage = string.Empty;
            objproduct.TransactionCode = "CASH-" + DateTime.Now.Ticks;
        }

        string res = Insert_SavingInstallment(objproduct, installmentId);
        if (res == "t")
        {
            loadprevproduct();
            txttransactionidedit.Text = string.Empty;
            hfInstallmentId.Value = string.Empty;
            lblidedit.Text = string.Empty;
            Message.Show("Request Submitted Successfully");
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), "Closepopup();", true);
        }
        else if (res == "f")
        {
            Message.Show("Already in process");
            ShowInstallmentPayModal();
        }
        else
        {
            Message.Show("Unable to submit request. Please try again.");
            ShowInstallmentPayModal();
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
            hfInstallmentId.Value = lblid.Text;
            txtamountedit.Text = lblamount.Text;
            txtinstallmentdateedit.Text = lblinstallmentdate.Text;
            litPayAmount.Text = lblamount.Text;
            litPayInstallmentDate.Text = lblinstallmentdate.Text;
            txttransactionidedit.Text = string.Empty;
            rbOnlinePayment.Checked = true;
            rbCashPayment.Checked = false;
            ApplyPaymentMethodVisibility();

            ShowInstallmentPayModal();
        }
    }
}

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
            lblqrcode.Text = @"<img src=""../ProductImage/"+dt.Rows[0]["branchname"].ToString()+@""" style=""height:400px;"">";
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
                lbEdit.Visible = true;
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
        objproduct.ProductImage = UploadImage();
        objproduct.TransactionCode = txttransactionidedit.Text;
        string res = Insert_SavingInstallment(objproduct,lblidedit.Text);
        if (res == "t")
        {
            loadprevproduct();
            Message.Show("Request Submitted Successfully");
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else if (res == "t")
        {
            Message.Show("Already in process");
        }
        else
        {
            Message.Show("unknown error occurred");
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}

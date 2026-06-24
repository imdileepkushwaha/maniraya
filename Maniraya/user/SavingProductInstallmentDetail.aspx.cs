using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_SavingProductInstallmentDetail : System.Web.UI.Page
{
    Data ObjData = new Data();

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
            if (lblstatus == null)
                return;

            if (lblstatus.Text == "0")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "dash-saving-status is-pending";
            }
            else if (lblstatus.Text == "1")
            {
                lblstatus.Text = "Approved";
                lblstatus.CssClass = "dash-saving-status is-approved";
            }
            else if (lblstatus.Text == "2")
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
}

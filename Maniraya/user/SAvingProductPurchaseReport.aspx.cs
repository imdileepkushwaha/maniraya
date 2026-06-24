using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_SavingProductPurchaseReport : System.Web.UI.Page
{
    Data ObjData = new Data();

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
    }

    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT sd.*, pm.productname
            FROM SavingAccountDetail sd WITH (NOLOCK)
            LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
            WHERE userid = '" + SqlEscape(Session["userid"].ToString()) + "'";

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
        if (GridView1 != null)
        {
            GridView1.DataSource = getPrevProduct();
            GridView1.DataBind();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static void ApplyStatusBadge(Label lblstatus)
    {
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

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ApplyStatusBadge((Label)e.Row.FindControl("lblstatus"));
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

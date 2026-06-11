using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class VendorPurchaseDetail : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadVendor();
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadVendor()
    {
        DDLstVendor.Items.Clear();
        DataTable dt = new DataTable();
        dt = objV.getVendorList();
        DDLstVendor.DataSource = dt;
        DDLstVendor.DataTextField = "Companyname";
        DDLstVendor.DataValueField = "id";
        DDLstVendor.DataBind();
        ListItem li = new ListItem("Select Vendor", "0");
        DDLstVendor.Items.Insert(0, li);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        objV.VendorId = string.Empty;
        if (txtfromdate.Text != "")
        {
            objV.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objV.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objV.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objV.Todate = DateTime.MinValue;
        }
        if (DDLstVendor.SelectedIndex != 0)
        {
            objV.VendorId = DDLstVendor.SelectedValue;
        }
        DataTable dt = new DataTable();
        dt = objV.getVendorPurchase(objV);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            DataTable dt1 = new DataTable();
            dt1 = objV.getVendorPurchaseProductChild(customerId);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();
        }
    }
}
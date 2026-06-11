using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class UserPurchaseReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
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

        if (txtfromdate.Text != "")
        {
            objP.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objP.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.MinValue;
        }
        objP.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objP.getPurchaseProductQuantity(objP);
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
            dt1 = objP.getPurchaseProductQuantityChild(customerId);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();


        }
    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        Button btndetails = sender as Button;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        string UserID = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lbluserid123")).Text;
        string InvoiceD = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("LblInvoiceStatus")).Text;
        string lbluserid = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lbluserid")).Text;
        
        if (InvoiceD == "0")
        {
            Response.Redirect("UserRepurchaseEntry.aspx?PID=" + lbluserid + "&UserId=" + UserID);
        }
        else
        {
            Message.Show("alredy generate invoice");
        }
    }
}
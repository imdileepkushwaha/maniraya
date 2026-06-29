using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;
using DataTier;
public partial class admin_UserReport : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                loadprevproduct();

            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
   


    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT sd.*,pm.productname FROM SavingAccountDetail sd WITH (nolock) LEFT JOIN savingproductmaster pm WITH (nolock) ON sd.productid=pm.id where userid='"+Session["userid"].ToString()+"'";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    void loadprevproduct()
    {

        DataTable dt = new DataTable();
        dt = getPrevProduct();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            if (lblstatus.Text == "0")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-warning";
            }
            else
                if (lblstatus.Text == "1")
                {
                    lblstatus.Text = "Approved";
                    lblstatus.CssClass = "label label-success";
                }
                else

                    if (lblstatus.Text == "2")
                    {
                        lblstatus.Text = "Rejected";
                        lblstatus.CssClass = "label label-danger";
                    }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
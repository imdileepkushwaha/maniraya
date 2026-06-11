using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class RoyalityincomeReport : System.Web.UI.Page
{
    clsUser objclsUser = new clsUser();
    clsClosing objCl = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loaduser()
    {

        DataTable dt = new DataTable();
        dt = objCl.getRoyaltyClosingDate();
        DDlstFromdate.DataSource = dt;
        DDlstFromdate.DataTextField = "ClosingDate";
        DDlstFromdate.DataValueField = "ClosingDate";
        DDlstFromdate.DataBind();
        ListItem li = new ListItem("Select Date", "0");
        DDlstFromdate.Items.Insert(0, li);
    }
    void loaddata()
    {
        string Fromdate = string.Empty;
        string Todatedate = string.Empty;
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
            Fromdate = str[0].ToString();
            Todatedate = str[1].ToString();
        }

        DataTable Dt = objCl.getroyalityClosingReport(Fromdate, Todatedate, Session["UserId"].ToString());
        GridView1.DataSource = Dt;
        GridView1.DataBind();

    }
    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblUserid = e.Row.FindControl("lblUserid") as Label;
            Label lblDailyid = e.Row.FindControl("lblDailyid") as Label;
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            DataTable dt1 = new DataTable();
            dt1 = objCl.getactivationamount(lblDailyid.Text,lblUserid.Text);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaddata();
    }
}
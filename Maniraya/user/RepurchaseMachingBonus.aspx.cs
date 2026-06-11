using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class RepurchaseMachingBonus : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loaddate();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    void loaddate()
    {

        DataTable dt = new DataTable();
        dt = objCL.getClosingRepurchaseDate();
        DDlstFromdate.DataSource = dt;
        DDlstFromdate.DataTextField = "ClosingDate";
        DDlstFromdate.DataValueField = "ClosingDate";
        DDlstFromdate.DataBind();
        ListItem li = new ListItem("Select Date", "0");
        DDlstFromdate.Items.Insert(0, li);
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
        string Fromdate = "";
        string Todatedate = "";
        string UserId = txtuserid.Text;
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
           Fromdate = str[0].ToString();
           Todatedate = str[1].ToString();

        }
        objaccount.UserId = txtuserid.Text;

        DataTable Dt = objCL.getrepurchaseClosingReport(Fromdate, Todatedate, UserId);
        GridView1.DataSource = Dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
 
   
}
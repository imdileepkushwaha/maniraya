using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLogicTier;
public partial class Subscription_ServiceProviderMaster : System.Web.UI.Page
{
    clsdthsuscription objS = new clsdthsuscription();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadstate();
                loadcountry();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadcountry()
    {
        DDlServiceProvider.Items.Clear();
        ddlserviceprovideredit.Items.Clear();
        DataTable dt = new DataTable();
        dt = objS.getserviceproviderAll(objS);
        DDlServiceProvider.DataSource = dt;
        DDlServiceProvider.DataTextField = "Operator";
        DDlServiceProvider.DataValueField = "id";
        DDlServiceProvider.DataBind();
        ListItem li = new ListItem("Select Service Provider", "0");
        DDlServiceProvider.Items.Insert(0, li);
        ddlserviceprovideredit.DataSource = dt;
        ddlserviceprovideredit.DataTextField = "Operator";
        ddlserviceprovideredit.DataValueField = "id";
        ddlserviceprovideredit.DataBind();
        ListItem li1 = new ListItem("Select Service Provider", "0");
        ddlserviceprovideredit.Items.Insert(0, li1);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       objS.Settopboxid = lblstateid.Text;
       objS.settopboxname = TxtSetTopboxedit.Text;
       objS.ServiceProviderId =ddlserviceprovideredit.SelectedValue;
       objS.Mrp = TxtMrpedit.Text.Trim() == "" ? "0" : TxtMrpedit.Text.Trim();
       objS.Description = TxtDescriptionedit.Text.Trim() == "" ? "" : TxtDescriptionedit.Text.Trim();
       objS.entryuser = Session["useradmin"].ToString();

            DataTable Dt = objS.updateSetTopBox(objS);
       
            string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadstate();
            loadcountry();
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
     
        objS.settopboxname = TxtSetTopbox.Text;
        objS.ServiceProviderId =DDlServiceProvider.SelectedValue;
        objS.Mrp = TxtMrp.Text.Trim() == "" ? "0" : TxtMrp.Text.Trim();
        objS.Description = TxtDescription.Text.Trim() == "" ? "" : TxtDescription.Text.Trim();
        objS.entryuser = Session["useradmin"].ToString();
        DataTable Dt = objS.insertSetTopBox(objS);

        string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        loadstate();
        Clean();
        loadcountry();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView2.Rows[index].FindControl("lblID");
            Label lblsettox = (Label)GridView2.Rows[index].FindControl("lblCountryname");
            Label lblremark = (Label)GridView2.Rows[index].FindControl("lblstatename");
            Label lblmrp = (Label)GridView2.Rows[index].FindControl("lblstatename2");
            Label lblserviceid = (Label)GridView2.Rows[index].FindControl("Label1");

            lblstateid.Text = lblid.Text;
            TxtSetTopboxedit.Text = lblsettox.Text;
            TxtDescriptionedit.Text = lblremark.Text;
            TxtMrpedit.Text = lblmrp.Text;
            ddlserviceprovideredit.SelectedValue = lblserviceid.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    void loadstate()
    {
        DataTable dt = new DataTable();
        dt = objS.getSetTopBox(objS);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
   
    private void Clean()
    {
        TxtSetTopbox.Text = "";
        DDlServiceProvider.SelectedIndex = 0;
        TxtMrp.Text = "";
        TxtDescription.Text = "";
    }
    
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLogicTier;

public partial class admin_Packagemaster : System.Web.UI.Page
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

        ddloperator.Items.Clear();
        ddloperator.Items.Clear();
        DataTable dt = new DataTable();
        dt = objS.getserviceproviderAll(objS);
        ddloperator.DataSource = dt;
        ddloperator.DataTextField = "Operator";
        ddloperator.DataValueField = "id";
        ddloperator.DataBind();
        ListItem li2 = new ListItem("Select Service Provider", "0");
        ddloperator.Items.Insert(0, li2);

        ddlsettopbox.Items.Clear();
        ddlsettopboxedit.Items.Clear();
        //DataTable dt = new DataTable();
        //dt = objS.getSetTopBox(objS);
        //ddlsettopbox.DataSource = dt;
        //ddlsettopbox.DataTextField = "SBoxName";
        //ddlsettopbox.DataTextField = "STBName";
        //ddlsettopbox.DataValueField = "ID";
        //ddlsettopbox.DataBind();
        ListItem li = new ListItem("Select Settop Box", "0");
        ddlsettopbox.Items.Insert(0, li);


        dt = objS.getSetTopBox(objS);
        ddlsettopboxedit.DataSource = dt;
        ddlsettopboxedit.DataTextField = "SBoxName";
        ddlsettopboxedit.DataTextField = "STBName";
        ddlsettopboxedit.DataValueField = "ID";
        ddlsettopboxedit.DataBind();


        ListItem li1 = new ListItem("Select Settop Box", "0");
        ddlsettopboxedit.Items.Insert(0, li1);
    }
    void loadstate()
    {
        DataTable dt = new DataTable();
        dt = objS.getpackage(objS);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objS.packageid = lblstateid.Text;
        objS.packagename =TxtPackageedit.Text;
        objS.Settopboxid = ddlsettopboxedit.SelectedValue;
        objS.validity = TxtValidityedit.Text;
        objS.noofchannel = TxtNoofchannelsEdit.Text;
        objS.Mrp = TxtMrpedit.Text;
        objS.Description = TxtDescriptionedit.Text;
        objS.entryuser = Session["useradmin"].ToString();
        objS.status = ddlstatus.SelectedValue;
        DataTable Dt = objS.updatepackage(objS);

        string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        loadstate();
        loadcountry();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objS.packagename = TxtPackage.Text;
        objS.Settopboxid =ddlsettopbox.SelectedValue;
        objS.validity = TxtValidity.Text;
        objS.noofchannel = TxtNoOfChanel.Text;
        objS.Mrp = TxtMRP.Text;
        objS.Description = TxtDescription.Text;
        objS.entryuser = Session["useradmin"].ToString();
        DataTable Dt = objS.insertpackage(objS);

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
            Label lblpackage = (Label)GridView2.Rows[index].FindControl("lblpackage");
            Label lblvalidity = (Label)GridView2.Rows[index].FindControl("lblvalidity");
            Label lblchannelno = (Label)GridView2.Rows[index].FindControl("lblchannelno");
            Label lblmrp = (Label)GridView2.Rows[index].FindControl("lblmrp");
            Label lblremark = (Label)GridView2.Rows[index].FindControl("lblremark");
            Label lblstbid = (Label)GridView2.Rows[index].FindControl("lblstbid");
            Label lblprice = (Label)GridView2.Rows[index].FindControl("lblprice");
            Label lblcomission = (Label)GridView2.Rows[index].FindControl("lblcomission");

            Label lblstatus = (Label)GridView2.Rows[index].FindControl("lblstatus");

            lblstateid.Text = lblid.Text;
            TxtPackageedit.Text = lblpackage.Text;
            TxtValidityedit.Text = lblvalidity.Text;
            TxtNoofchannelsEdit.Text = lblchannelno.Text;
            TxtMrpedit.Text = lblmrp.Text;
            TxtDescriptionedit.Text = lblmrp.Text;
            TxtDescriptionedit.Text = lblremark.Text;
            ddlsettopboxedit.SelectedValue = lblstbid.Text;
            if (lblstatus.Text == "Active")
                ddlstatus.SelectedValue = "1";
            else
                ddlstatus.SelectedValue = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    private void Clean()
    {
        TxtPackage.Text = "";
        ddlsettopbox.SelectedIndex = 0;
        TxtValidity.Text = "";
        TxtNoOfChanel.Text = "";
        TxtMRP.Text = "";
        TxtDescription.Text = "";
    }
    protected void ddloperator_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsettopbox.Items.Clear();
        if (ddloperator.SelectedIndex > 0)
        {
            objS.opid = ddloperator.SelectedValue;
            DataTable dt = new DataTable();
            dt = objS.getSetTopBoxbyid(objS);
            ddlsettopbox.DataSource = dt;
            ddlsettopbox.DataTextField = "SBoxName";
            ddlsettopbox.DataTextField = "STBName";
            ddlsettopbox.DataValueField = "ID";
            ddlsettopbox.DataBind();
        }
        ListItem li = new ListItem("Select Settop Box", "0");
        ddlsettopbox.Items.Insert(0, li);
       
    }
}
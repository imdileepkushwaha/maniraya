using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MarketAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
                loadMarket();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);
        
        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadCity()
    {
        ddlstcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddlstcity.DataSource = dt;
        ddlstcity.DataTextField = "CityName";
        ddlstcity.DataValueField = "CityID";
        ddlstcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddlstcity.Items.Insert(0, li);
    }
    void loadTehsil()
    {
        ddlsttehsil.Items.Clear();
        DataTable dt = new DataTable();
        objState.CityId = ddlstcity.SelectedValue.ToString();
        dt = objState.getTehsil(objState);

        ddlsttehsil.DataSource = dt;
        ddlsttehsil.DataTextField = "tehsilName";
        ddlsttehsil.DataValueField = "tehsilID";
        ddlsttehsil.DataBind();
        ListItem li = new ListItem("Select Tehsil", "0");
        ddlsttehsil.Items.Insert(0, li);
    }
    void loadMarket()
    {
        DataTable dt = new DataTable();
        dt = objState.getMarketAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objState.MarketName = txtcitynameedit.Text;
        objState.MarketId = lblcityid.Text;
        string res = objState.Update_Market(objState);
        if (res == "t")
        {
            string popupScript = "alert('Market Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadMarket();
        }
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objState.TehsilId = ddlsttehsil.SelectedValue.ToString();
        objState.MarketName = Txtmarket.Text;
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_Market(objState);
        if (res == "t")
        {
            string popupScript = "alert('Market Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            Txtmarket.Text = ""; ddcountry.SelectedValue = "0"; ddstate.SelectedValue = "0";
            loadMarket();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('Market Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblstatename = (Label)GridView1.Rows[index].FindControl("lblMarketName");
            lblcityid.Text = lblid.Text;
            txtcitynameedit.Text = lblstatename.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }
    protected void ddlstcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTehsil();
    }
}
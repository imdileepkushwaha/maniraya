using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_CityAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    Data objData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
                loadcountryedit();
                loadcity();
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

    void loadcountryedit()
    {
        ddcountryedit.Items.Clear();
        DataTable dt = objState.getCountry();
        ddcountryedit.DataSource = dt;
        ddcountryedit.DataTextField = "CountryName";
        ddcountryedit.DataValueField = "CountryID";
        ddcountryedit.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountryedit.Items.Insert(0, li);
    }

    void loadstateedit(string selectedStateId)
    {
        ddstateedit.Items.Clear();
        objState.CountryId = ddcountryedit.SelectedValue;
        DataTable dt = objState.getState(objState);
        ddstateedit.DataSource = dt;
        ddstateedit.DataTextField = "StateName";
        ddstateedit.DataValueField = "StateID";
        ddstateedit.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstateedit.Items.Insert(0, li);

        if (!string.IsNullOrEmpty(selectedStateId))
        {
            ListItem stateItem = ddstateedit.Items.FindByValue(selectedStateId);
            if (stateItem != null)
            {
                ddstateedit.SelectedValue = selectedStateId;
            }
        }
    }
    void loadcity()
    {
        DataTable dt = new DataTable();
        dt = objState.getCityAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objState.CityName = txtcitynameedit.Text;
        objState.CityId = lblcityid.Text;
        objState.StateId = ddstateedit.SelectedValue;
        string res = objState.Update_City(objState);
        if (res == "t")
        {
            string popupScript = "alert('City Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "closeAdminModal('myModal');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadcity();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objState.StateId = ddstate.SelectedValue.ToString();
        objState.CityName = txtcityname.Text;
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_City(objState);
        if (res == "t")
        {
            string popupScript = "alert('City Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtcityname.Text = "";
            ddcountry.SelectedIndex = 0;
            ddstate.SelectedIndex = 0;
            loadcity();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('City Already Exists');";
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
            Label lblcityname = (Label)GridView1.Rows[index].FindControl("lblcityname");
            Label lblstateid = (Label)GridView1.Rows[index].FindControl("lblstateid");
            string stateId = lblstateid != null ? lblstateid.Text : string.Empty;
            string countryId = GetCountryIdByStateId(stateId);

            lblcityid.Text = lblid.Text;
            txtcitynameedit.Text = lblcityname.Text;

            if (!string.IsNullOrEmpty(countryId))
            {
                ListItem countryItem = ddcountryedit.Items.FindByValue(countryId);
                if (countryItem != null)
                {
                    ddcountryedit.SelectedValue = countryId;
                }
            }

            loadstateedit(stateId);

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Pop", "showAdminModal('myModal');", true);
        }
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }

    protected void ddcountryedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstateedit(string.Empty);
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "ReopenEditCityModal", "showAdminModal('myModal');", true);
    }

    string GetCountryIdByStateId(string stateId)
    {
        if (string.IsNullOrEmpty(stateId))
        {
            return "";
        }

        string countryId = "";
        DataTable dt = null;
        objData.StartConnection();
        try
        {
            dt = objData.RunDataTable("select countryid from statemaster where stateid='" + stateId + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row.Table.Columns.Contains("countryid"))
                {
                    countryId = row["countryid"].ToString();
                }
                else if (row.Table.Columns.Contains("CountryID"))
                {
                    countryId = row["CountryID"].ToString();
                }
                else
                {
                    countryId = row[0].ToString();
                }
            }
        }
        catch
        {
            countryId = "";
        }
        objData.EndConnection();
        return countryId;
    }
}
using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_StateAdd : System.Web.UI.Page
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
                loadstate();
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

    void loadstate()
    {
        DataTable dt = new DataTable();
        dt = objState.getStateAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objState.StateName = txtstatenameedit.Text.Trim();
        objState.StateId = lblstateid.Text.Trim();
        objState.CountryId = ddcountryedit.SelectedValue;
        string res = objState.Update_State(objState);
        if (res == "t")
        {
            string popupScript = "alert('State Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "closeAdminModal('myModal');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadstate();
        }
        else
        {
            string popupScript = "alert('Unable to update state. Please try again.'); showAdminModal('myModal');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objState.CountryId = ddcountry.SelectedValue.ToString();
        objState.StateName  = txtstatename.Text;
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_State(objState);
        if (res == "t")
        {
            string popupScript = "alert('State Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtstatename.Text = "";
            ddcountry.SelectedIndex = 0;
            loadstate();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('State Already Exists');";
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
            Label lblstatename = (Label)GridView1.Rows[index].FindControl("lblstatename");
            Label lblcountryid = (Label)GridView1.Rows[index].FindControl("lblcountryid");
            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;

            string countryId = lblcountryid != null ? lblcountryid.Text.Trim() : string.Empty;
            if (string.IsNullOrEmpty(countryId))
            {
                countryId = GetCountryIdByStateId(lblid.Text);
            }

            if (!string.IsNullOrEmpty(countryId))
            {
                ListItem countryItem = ddcountryedit.Items.FindByValue(countryId);
                if (countryItem != null)
                {
                    ddcountryedit.SelectedValue = countryId;
                }
            }

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Pop", "showAdminModal('myModal');", true);
        }
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
using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_CountryAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loaddata();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objState.CountryName = txtcountrynameedit.Text;
        objState.CountryCode = txtcountrycodeedit.Text;
        objState.CountryId = lblcountryid.Text;
        string res = objState.Update_Country(objState);
        if (res == "t")
        {
            string popupScript = "alert('Country Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loaddata();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objState.CountryName = txtcountryname.Text;
        objState.CountryCode = txtcountrycode.Text;
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_Country(objState);
        if (res == "t")
        {
            string popupScript = "alert('Country Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtcountryname.Text = "";
            loaddata();
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
            Label lblCountryname = (Label)GridView1.Rows[index].FindControl("lblCountryname");
            Label lblCountrycode = (Label)GridView1.Rows[index].FindControl("lblCountrycode");
            lblcountryid.Text = lblid.Text;
            txtcountrynameedit.Text = lblCountryname.Text;
            txtcountrycodeedit.Text = lblCountrycode.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}
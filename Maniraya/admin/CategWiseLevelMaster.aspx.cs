using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_CategWiseLevelMaster : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                //loaddata();
                loadCategory();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        string Id = ddcountry.SelectedValue;
        dt = objaccount.getLevelCategoryWise(Id);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    void loadCategory()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCategory();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CategoryName";
        ddcountry.DataValueField = "CategoryId";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Category", "0");
        ddcountry.Items.Insert(0, li);
    }
   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lbllevel");
            TextBox txtcomission = (TextBox)r.FindControl("txtcomission");
            objaccount.LevelNo = lbllevel.Text;
            objaccount.ComissionPercent = Convert.ToDecimal(txtcomission.Text);
            objaccount.UpdateLevelMasterCategoryWise(objaccount,ddcountry.SelectedValue);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

    }
    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaddata();
    }
}
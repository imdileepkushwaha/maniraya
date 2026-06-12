using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubcategoryAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsProduct objProduct = new clsProduct();
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
        DataTable dt = objProduct.getCategory();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "categoryName";
        ddcountry.DataValueField = "categoryID";
        ddcountry.DataBind();
        ddcountry.Items.Insert(0, new ListItem("Select Category", "0"));
    }

    void loadcountryedit()
    {
        ddcountryedit.Items.Clear();
        DataTable dt = objProduct.getCategory();
        ddcountryedit.DataSource = dt;
        ddcountryedit.DataTextField = "categoryName";
        ddcountryedit.DataValueField = "categoryID";
        ddcountryedit.DataBind();
        ddcountryedit.Items.Insert(0, new ListItem("Select Category", "0"));
    }

    void loadstate()
    {
        DataTable dt = objProduct.getSubcategoryAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string subCategoryId = lblstateid.Text.Replace("'", "''");
        string categoryId = ddcountryedit.SelectedValue.Replace("'", "''");
        string subCategoryName = txtstatenameedit.Text.Trim().Replace("'", "''");

        objData.StartConnection();
        try
        {
            string sql = "update subCategoryMaster set subCategoryName='" + subCategoryName + "', categoryid='" + categoryId + "' where subCategoryId='" + subCategoryId + "'";
            objData.RunInsUpDelQuery(sql);

            string popupScript = "alert('Subcategory updated successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadcountryedit();
            loadstate();
        }
        catch
        {
            string popupScript = "alert('Unable to update subcategory');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        finally
        {
            objData.EndConnection();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objProduct.CategoryId = ddcountry.SelectedValue.ToString();
        objProduct.CategoryName = txtstatename.Text;
        objProduct.MentionBy = Session["useradmin"].ToString();
        string res = objProduct.Insert_subCategory(objProduct);
        if (res == "t")
        {
            string popupScript = "alert('Subcategory added successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtstatename.Text = "";
            ddcountry.SelectedValue = "0";
            loadstate();
        }
        else if (res == "f")
        {
            string popupScript = "alert('Subcategory already exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('Unknown error occurred');";
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
            Label lblcategoryid = (Label)GridView1.Rows[index].FindControl("lblcategoryid");

            loadcountryedit();

            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;

            if (ddcountryedit.Items.FindByValue(lblcategoryid.Text) != null)
            {
                ddcountryedit.SelectedValue = lblcategoryid.Text;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}

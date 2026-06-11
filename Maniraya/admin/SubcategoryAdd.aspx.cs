using BusinessLogicTier;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
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
        dt = objProduct.getCategory();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "categoryName";
        ddcountry.DataValueField = "categoryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select category", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        DataTable dt = new DataTable();
        dt = objProduct.getSubcategoryAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objProduct.CategoryName = txtstatenameedit.Text;
        objProduct.CategoryId = lblstateid.Text;
        string res = objProduct.Update_subCategory(objProduct);
        if (res == "t")
        {
            string popupScript = "alert('subCategory Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadstate();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objProduct.CategoryId = ddcountry.SelectedValue.ToString();
        objProduct.CategoryName  = txtstatename.Text;
        objProduct.MentionBy = Session["useradmin"].ToString();
        string res = objProduct.Insert_subCategory(objProduct);
        if (res == "t")
        {
            string popupScript = "alert('subCategory Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtstatename.Text = ""; ddcountry.SelectedValue = "0";
            loadstate();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('subCategory Already Exists');";
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
            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}
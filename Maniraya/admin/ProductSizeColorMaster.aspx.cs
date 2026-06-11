using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ProductSizeColorMaster : System.Web.UI.Page
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
                loadsize();
                loadcolor();
                loaddata();
                loadsizeedit();
                loadcoloredit();
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
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objProduct.getProductcolorSizeMaster();
        gvData.DataSource = dt;
        gvData.DataBind();
    }
    void loadSubCategory()
    {
        ddsubcategory.Items.Clear();
        DataTable dt = new DataTable();
        dt = objProduct.getSubcategoryBycategoryid(ddcountry.SelectedValue);
        ddsubcategory.DataSource = dt;
        ddsubcategory.DataTextField = "SubCategoryName";
        ddsubcategory.DataValueField = "SubCategoryId";
        ddsubcategory.DataBind();
        ListItem li = new ListItem("Select SubCategory", "0");
        ddsubcategory.Items.Insert(0, li);
    }
    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubCategory();
    }
    void loadcolor()
    {
        ddlColor.Items.Clear();
        DataTable dt = new DataTable();
        dt = objProduct.getColorMaster();
        ddlColor.DataSource = dt;
        ddlColor.DataTextField = "colorName";
        ddlColor.DataValueField = "ID";
        ddlColor.DataBind();
        ListItem li = new ListItem("Select color", "0");
        ddlColor.Items.Insert(0, li);
    }
    void loadcoloredit()
    {
        Ddlstcoloredit.Items.Clear();
        DataTable dt = new DataTable();
        dt = objProduct.getColorMaster();
        Ddlstcoloredit.DataSource = dt;
        Ddlstcoloredit.DataTextField = "colorName";
        Ddlstcoloredit.DataValueField = "ID";
        Ddlstcoloredit.DataBind();
        ListItem li = new ListItem("Select color", "0");
        Ddlstcoloredit.Items.Insert(0, li);
    }
    void loadsize()
    {
        ddlSize.Items.Clear();
        DataTable dt = new DataTable();
        dt = objProduct.getSizeMaster();
        ddlSize.DataSource = dt;
        ddlSize.DataTextField = "sizeName";
        ddlSize.DataValueField = "ID";
        ddlSize.DataBind();
        ListItem li = new ListItem("Select size", "0");
        ddlSize.Items.Insert(0, li);
    }
    void loadsizeedit()
    {
        DDlstsizeedit.Items.Clear();
        DataTable dt = new DataTable();
        dt = objProduct.getSizeMaster();
        DDlstsizeedit.DataSource = dt;
        DDlstsizeedit.DataTextField = "sizeName";
        DDlstsizeedit.DataValueField = "ID";
        DDlstsizeedit.DataBind();
        ListItem li = new ListItem("Select size", "0");
        DDlstsizeedit.Items.Insert(0, li);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objProduct.SubCategoryId = ddsubcategory.SelectedValue;
        objProduct.colorId = ddlColor.SelectedValue;
        objProduct.Sizeid = ddlSize.SelectedValue;
        string res = objProduct.Product_ColorSizeSetting(objProduct);
        if (res == "t")
        {
            string popupScript = "alert('Records Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            ddlColor.SelectedIndex = 0;ddlSize.SelectedIndex = 0;
          
        }
        else
            if (res == "f")
        {
            string popupScript = "alert('Records Already Exists');";
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
            Label lblid = (Label)gvData.Rows[index].FindControl("lblid");
            Label lblSubCategoryID = (Label)gvData.Rows[index].FindControl("lblSubCategoryID");
            Label lblcolorid = (Label)gvData.Rows[index].FindControl("lblcolorid");
            Label lblsizeid = (Label)gvData.Rows[index].FindControl("lblsizeid");
            Label lblSubCategoryname = (Label)gvData.Rows[index].FindControl("lblSubCategoryname");



            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}
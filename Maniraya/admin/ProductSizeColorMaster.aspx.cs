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
        cblSize.Items.Clear();
        DataTable dt = objProduct.getSizeMaster();
        if (dt == null || dt.Rows.Count == 0)
        {
            cblSize.Visible = false;
            lblNoSizes.Visible = true;
            return;
        }

        cblSize.Visible = true;
        lblNoSizes.Visible = false;
        cblSize.DataSource = dt;
        cblSize.DataTextField = GetColumnName(dt, "sizeName", "SizeName", "Sizename");
        cblSize.DataValueField = GetColumnName(dt, "ID", "Id");
        cblSize.DataBind();
    }

    string GetColumnName(DataTable dt, params string[] candidates)
    {
        foreach (string candidate in candidates)
        {
            if (dt.Columns.Contains(candidate))
            {
                return candidate;
            }
        }
        return dt.Columns.Count > 0 ? dt.Columns[0].ColumnName : candidates[0];
    }

    void clearSizeSelection()
    {
        foreach (ListItem item in cblSize.Items)
        {
            item.Selected = false;
        }
    }

    bool hasSizeSelection()
    {
        foreach (ListItem item in cblSize.Items)
        {
            if (item.Selected)
            {
                return true;
            }
        }
        return false;
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
        if (ddsubcategory.SelectedValue == "0" || ddlColor.SelectedValue == "0" || !hasSizeSelection())
        {
            string popupScript = "alert('Please select sub-category, color and at least one size.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        objProduct.SubCategoryId = ddsubcategory.SelectedValue;
        objProduct.colorId = ddlColor.SelectedValue;

        int added = 0;
        int duplicate = 0;
        int failed = 0;

        foreach (ListItem item in cblSize.Items)
        {
            if (!item.Selected)
            {
                continue;
            }

            objProduct.Sizeid = item.Value;
            string res = objProduct.Product_ColorSizeSetting(objProduct);
            if (res == "t")
            {
                added++;
            }
            else if (res == "f")
            {
                duplicate++;
            }
            else
            {
                failed++;
            }
        }

        string message;
        if (added > 0 && duplicate == 0 && failed == 0)
        {
            message = added == 1
                ? "Record added successfully."
                : added + " records added successfully.";
        }
        else if (added > 0)
        {
            message = added + " record(s) added.";
            if (duplicate > 0)
            {
                message += " " + duplicate + " already existed.";
            }
            if (failed > 0)
            {
                message += " " + failed + " failed.";
            }
        }
        else if (duplicate > 0 && failed == 0)
        {
            message = "All selected size combinations already exist.";
        }
        else
        {
            message = "Unable to save records. Please try again.";
        }

        string popupScriptResult = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScriptResult, true);

        if (added > 0)
        {
            ddlColor.SelectedIndex = 0;
            clearSizeSelection();
            loaddata();
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
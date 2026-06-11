using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCategory();
            loadProduct(1);
        }
    }

    void loadCategory()
    {
        ddcountry.Items.Clear();
        DataTable dt = CatalogHelper.BuildDisplayCategories();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CategoryName";
        ddcountry.DataValueField = "CategoryId";
        ddcountry.DataBind();
        ddcountry.Items.Insert(0, new ListItem("All Categories", "0"));

        DataTable homeCategories = CatalogHelper.TakeRows(dt, CatalogHelper.HomeCategoryLimit);
        rptcategory.DataSource = homeCategories;
        rptcategory.DataBind();
    }

    void loadProduct(int pageIndex)
    {
        string categoryId = null;
        if (ddcountry.SelectedIndex != 0)
        {
            categoryId = ddcountry.SelectedValue;
        }

        DataTable dt = CatalogHelper.LoadProducts(pageIndex, CatalogHelper.HomeProductLimit, categoryId);
        rptProducts.DataSource = dt;
        rptProducts.DataBind();
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProduct(1);
    }

    protected void rptCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SelectCategory")
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);

            if (categoryId <= 0)
            {
                ddcountry.SelectedIndex = 0;
            }
            else
            {
                ListItem item = ddcountry.Items.FindByValue(categoryId.ToString());
                if (item != null)
                {
                    ddcountry.SelectedValue = categoryId.ToString();
                }
            }

            loadProduct(1);
        }
    }
}

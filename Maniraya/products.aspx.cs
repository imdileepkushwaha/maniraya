using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategoryFilter();
            ApplyCategoryFromQuery();
            LoadProducts();
        }
    }

    void LoadCategoryFilter()
    {
        ddCategory.Items.Clear();
        DataTable dt = CatalogHelper.BuildDisplayCategories();
        ddCategory.DataSource = dt;
        ddCategory.DataTextField = "CategoryName";
        ddCategory.DataValueField = "CategoryId";
        ddCategory.DataBind();
        ddCategory.Items.Insert(0, new ListItem("All Categories", "0"));
    }

    void ApplyCategoryFromQuery()
    {
        string category = Request.QueryString["category"];
        if (string.IsNullOrEmpty(category))
        {
            return;
        }

        ListItem item = ddCategory.Items.FindByValue(category);
        if (item != null)
        {
            ddCategory.SelectedValue = category;
        }
    }

    void LoadProducts()
    {
        string categoryId = null;
        if (ddCategory.SelectedIndex > 0)
        {
            categoryId = ddCategory.SelectedValue;
        }

        DataTable dt = CatalogHelper.LoadProducts(1, CatalogHelper.CatalogProductPageSize, categoryId);
        rptProducts.DataSource = dt;
        rptProducts.DataBind();
        pnlEmpty.Visible = dt == null || dt.Rows.Count == 0;
    }

    protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProducts();
    }
}

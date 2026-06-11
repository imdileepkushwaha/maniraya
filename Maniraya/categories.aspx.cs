using System;
using System.Data;
using System.Web.UI;

public partial class categories : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = CatalogHelper.BuildDisplayCategories();
            rptCategories.DataSource = dt;
            rptCategories.DataBind();
        }
    }
}

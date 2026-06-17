using System;
using System.Data;
using System.Web.UI;

public partial class FAQ : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFAQ();
        }
    }

    private void BindFAQ()
    {
        DataTable dt = FaqHelper.GetActiveFaqs();
        if (dt.Rows.Count > 0)
        {
            rptFAQ.DataSource = dt;
            rptFAQ.DataBind();
        }
    }
}

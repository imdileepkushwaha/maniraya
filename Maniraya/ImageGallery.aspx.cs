using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using DataTier;

public partial class ImageGallery : Page
{
    DBHelper con = new DBHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGallery();
        }
    }

    private void BindGallery()
    {
        try
        {
            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Flag", "S") };
            DataTable dt = con.ExecuteDataSet("Pro_Gallery", arr);

            if (dt != null && dt.Rows.Count > 0)
            {
                rptGallery.DataSource = dt;
                rptGallery.DataBind();
            }
        }
        catch { }
    }
}

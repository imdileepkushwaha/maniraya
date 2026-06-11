using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using DataTier;

public partial class user_Event : System.Web.UI.Page
{
    DBHelper con = new DBHelper();
    public DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Flag", "S") };
                dt = con.ExecuteDataSet("Pro_Slider", arr);
            }
        }
        catch { }
    }
}
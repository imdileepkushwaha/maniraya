using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;

public partial class admin_index : System.Web.UI.Page
{
    clsLogin objlogin = new clsLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        objlogin.username = txtusername.Text;
        objlogin.password = txtpassword.Text;

        DataTable dt = new DataTable();
        dt = objlogin.Chk_AdminLoginDetails(objlogin);
        if (dt.Rows.Count > 0)
        {
            Session["useradmin"] = txtusername.Text;
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            Message.Show("Invalid Login Details...!!!");
        }
    }
}
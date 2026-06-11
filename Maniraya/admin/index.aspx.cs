using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;
using DataTier;

public partial class admin_index : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsLogin objlogin = new clsLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        objlogin.username = txtusername.Text;
        objlogin.password = txtpassword.Text;

        DataTable dt = new DataTable();
        dt = Chk_AdminLoginDetails(objlogin);
        if (dt.Rows.Count > 0)
        {
            Session["useradmin"] = txtusername.Text;
            Session["role"] = dt.Rows[0]["role"].ToString();
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            Message.Show("Invalid Login Details...!!!");
        }
    }
    public DataTable Chk_AdminLoginDetails(clsLogin objlogin)
    {
        string str_query = "";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            str_query = "select * from LoginDetail  where username=@username and password=@password and (role='administrator' or role='Subadmin') and status='1'  ";
            SqlParameter[] parameter = {   
                new SqlParameter("@username", objlogin.username),
                new SqlParameter("@password", objlogin.password)
                };

            dt = ObjData.RunDataTableParam(str_query, parameter);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
}
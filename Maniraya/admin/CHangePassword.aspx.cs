using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class admin_CHangePassword : System.Web.UI.Page
{
    clsLogin objlogin = new clsLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objlogin.username = Session["useradmin"].ToString();
        objlogin.password = txtoldpassword.Text;
        objlogin.newpassword = txtuserpassword.Text;
        int res = objlogin.ChangeAdminPassword(objlogin);
        if (res == 1)
        {
            string popupScript = "alert('Password Updated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtconfirmpassword.Text = txtoldpassword.Text = txtuserpassword.Text = "";
        }
        else
        {
            string popupScript = "alert('Invalid Old Password.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
}
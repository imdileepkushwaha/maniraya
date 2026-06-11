using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class CreateLinkForJoining : System.Web.UI.Page
{
    clsUser objclsUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            TxtLink.Attributes.Add("readonly", "readonly");
            txtuserid.Text = Session["userid"].ToString();
            loadsusername();
            RdBtnLeft.Checked = true;
          
        }
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objclsUser.UserId = txtuserid.Text;
        dt = objclsUser.getUserName(objclsUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();           
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (RdBtnLeft.Checked == true)
        {
            TxtLink.Text = "http://raxtan.com/user/Registration.aspx/?UserId=" + txtuserid.Text + "&Standingposition=" + 1;
        }
        if (RdBtnRight.Checked == true)
        {
            TxtLink.Text = "http://raxtan.com/user/Registration.aspx/?UserId=" + txtuserid.Text + "&Standingposition=" + 2;
        }
    }
}
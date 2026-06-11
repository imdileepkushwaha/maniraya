using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_MasterPage : System.Web.UI.MasterPage
{
    clsUser objuser = new clsUser();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null )
        {
            LblUsernameSideMenu.Text = Session["username"].ToString() + "(" + Session["userid"].ToString() + ")";
            LblMainId.Text = Session["username"].ToString() + "(" + Session["userid"].ToString() + ")";
            //LblFullname.Text = Session["username"].ToString() + "(" + Session["userid"].ToString() + ")";
            String UserImage = Session["UserImage"].ToString();
            if (UserImage.ToString() != "")
            {
                //dvUserImage1.Src = "~/ProductImage/" + UserImage.ToString();
                //dvUserImage2.Src = "~/ProductImage/" + UserImage.ToString();
                dvUserImage3.Src = "~/ProductImage/" + UserImage.ToString();
            }
            else
            {
                //dvUserImage1.Src = "~/ProductImage/636549111447865966default.png";
                //dvUserImage2.Src = "~/ProductImage/636549111447865966default.png";
                dvUserImage3.Src = "~/ProductImage/636549111447865966default.png";
            }

            if (Session["status"].ToString() == "1")
            {
                // JoinPackage.Visible = false;
              limyteam.Visible = true;
               // transfertowallet.Visible = false;
            }
            else
            {
                limyteam.Visible = true;
              //  transfertowallet.Visible = true; 
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnCallbackSubmit_Click(object sender, EventArgs e)
    {
        if (txtcallbackmobile.Text != "")
        {
            objuser.UserId = Session["userid"].ToString();
            objuser.Mobile = txtcallbackmobile.Text;
            objuser.MentionBy = Session["userid"].ToString();
            string res = objuser.InsertCallbackRequest(objuser);
            if (res == "t")
            {
                txtcallbackmobile.Text = "";
                Message.Show("Request Added Successfully...");
            }
            else
            {
                Message.Show("Unknown Error Occurred...");
            }
        }
        else
        {
            Message.Show("Enter Mobile No");
        }
    }
    protected void btnCallbackSubmit_Click1(object sender, EventArgs e)
    {
        if (txtcallbackmobile.Text != "")
        {
            objuser.UserId = Session["userid"].ToString();
            objuser.Mobile = txtcallbackmobile.Text;
            objuser.MentionBy = Session["userid"].ToString();
            string res = objuser.InsertCallbackRequest(objuser);
            if (res == "t")
            {
                txtcallbackmobile.Text = "";
                Message.Show("Request Added Successfully...");
            }
            else
            {
                Message.Show("Unknown Error Occurred...");
            }
        }
        else
        {
            Message.Show("Enter Mobile No");
        }
    }
}

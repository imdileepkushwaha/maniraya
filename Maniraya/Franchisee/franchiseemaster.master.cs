using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class franchiseemaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null )
        {
            LblUsernameSideMenu.Text = Session["username"].ToString() + "(" + Session["fuserid"].ToString() + ")";
            LblMainId.Text = Session["username"].ToString() + "(" + Session["fuserid"].ToString() + ")";
            //LblFullname.Text = Session["username"].ToString() + "(" + Session["fuserid"].ToString() + ")";
            String UserImage = Session["UserImage"].ToString();
            if (UserImage.ToString() != "")
            {
                dvUserImage1.Src = "~/ProductImage/" + UserImage.ToString();
                //dvUserImage2.Src = "~/ProductImage/" + UserImage.ToString();
                dvUserImage3.Src = "~/ProductImage/" + UserImage.ToString();
            }
            else
            {
                dvUserImage1.Src = "~/ProductImage/636549111447865966default.png";
                //dvUserImage2.Src = "~/ProductImage/636549111447865966default.png";
                dvUserImage3.Src = "~/ProductImage/636549111447865966default.png";

            }
            if (Session["status"].ToString()=="1")
            {
                //JoinPackage.Visible=false;
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
}

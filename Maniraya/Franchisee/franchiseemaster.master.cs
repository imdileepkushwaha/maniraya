using System;
using System.Web.UI;

public partial class franchiseemaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        string displayName = Session["username"] + " (" + Session["fuserid"] + ")";
        LblUsernameSideMenu.Text = displayName;
        LblMainId.Text = displayName;
        LblUserMenuName.Text = displayName;
        LblUserRole.Text = "Franchisee";
        LblUserMenuRole.Text = "Franchisee";

        string userImage = Session["UserImage"] != null ? Session["UserImage"].ToString() : string.Empty;
        string imagePath;

        if (!string.IsNullOrWhiteSpace(userImage))
        {
            imagePath = ResolveUrl("~/ProductImage/" + userImage);
        }
        else
        {
            imagePath = ResolveUrl("~/ProductImage/636549111447865966default.png");
        }

        dvUserImage1.Src = imagePath;
        dvUserImage3.Src = imagePath;
        dvUserImageMenu.Src = imagePath;
    }
}

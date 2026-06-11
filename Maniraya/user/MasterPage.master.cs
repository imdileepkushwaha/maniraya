using System;
using System.Data;
using System.Web.UI;
using BusinessLogicTier;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            string profileImage = GetProfileImageUrl();

            dvUserImage3.Src = profileImage;
            imgHeaderUser.Src = profileImage;
            imgHeaderUserMenu.Src = profileImage;

            if (Session["status"] != null && Session["status"].ToString() == "1")
            {
                // JoinPackage.Visible = false;
                // limyteam.Visible = true;
                // transfertowallet.Visible = false;
            }
            else
            {
                // limyteam.Visible = true;
                // transfertowallet.Visible = true;
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    private string GetProfileImageUrl()
    {
        clsUser objUser = new clsUser();
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = objUser.getUserName(objUser);

        if (dt != null && dt.Rows.Count > 0)
        {
            string photo = Convert.ToString(dt.Rows[0]["Photo"]).Trim();
            if (!string.IsNullOrEmpty(photo) && !photo.Equals("img/default.png", StringComparison.OrdinalIgnoreCase))
            {
                SyncUserImageSession(photo);
                return ResolveProfilePhotoPath(photo);
            }
        }

        string userImage = Session["UserImage"] != null ? Session["UserImage"].ToString().Trim() : "";
        if (!string.IsNullOrEmpty(userImage))
            return ResolveUrl("~/ProductImage/" + userImage);

        return ResolveUrl("~/user/img/default.png");
    }

    private void SyncUserImageSession(string photo)
    {
        const string marker = "ProductImage/";
        int markerIndex = photo.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (markerIndex >= 0)
        {
            Session["UserImage"] = photo.Substring(markerIndex + marker.Length);
            return;
        }

        if (!photo.Contains("/"))
            Session["UserImage"] = photo;
    }

    private string ResolveProfilePhotoPath(string photo)
    {
        if (photo.StartsWith("../", StringComparison.Ordinal))
            return ResolveUrl("~" + photo.Substring(2));

        if (photo.StartsWith("~/", StringComparison.Ordinal))
            return ResolveUrl(photo);

        if (photo.StartsWith("/", StringComparison.Ordinal))
            return photo;

        return ResolveUrl("~/" + photo.TrimStart('/'));
    }
}

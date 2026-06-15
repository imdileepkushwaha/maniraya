using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class franchisee_PhotoUpload : System.Web.UI.Page
{
    clsfranchisee objUser = new clsfranchisee();
    clsAccount objaccount = new clsAccount();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] == null)
        {
            Response.Redirect("index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                txtuserid.Text = Session["fuserid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadnotification();
            }
            catch (Exception)
            {
                ImageShow.ImageUrl = "img/default.png";
                ViewState["image"] = "img/default.png";
            }
        }
    }

    public string UploadImage()
    {
        string imageName = string.Empty;
        if (ImageUpload.HasFile)
        {
            string randomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
            imageName = randomNumber + fileName;
            ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + imageName);
        }
        return imageName;
    }

    void loadnotification()
    {
        objUser.UserId = Session["fuserid"].ToString();
        DataTable dt = objUser.getUserDetail(objUser);
        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["activestatus"].ToString() == "0")
        {
            Response.Redirect("Dashboard.aspx");
        }
    }

    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }

    void loadsusername()
    {
        if (string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            return;
        }

        objUser.UserId = txtuserid.Text;
        DataTable dt = objUser.getUserName(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            string photo = dt.Rows[0]["photo"].ToString();
            ImageShow.ImageUrl = string.IsNullOrWhiteSpace(photo) ? "img/default.png" : photo;
            ViewState["image"] = ImageShow.ImageUrl;
        }
        else
        {
            ImageShow.ImageUrl = "img/default.png";
            ViewState["image"] = "img/default.png";
            txtusername.Text = string.Empty;
            Message.Show("Invalid User Id...!!!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            Message.Show("Enter User Id...!!!");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtusername.Text))
        {
            Message.Show("Enter User Name...!!!");
            return;
        }

        if (!ImageUpload.HasFile)
        {
            Message.Show("Please select a photo to upload.");
            return;
        }

        objUser.Photo = UploadImage();
        objUser.MentionBy = Session["fuserid"].ToString();
        objUser.UserId = Session["fuserid"].ToString();
        string rs = objUser.Update_UserPhoto(objUser);
        if (rs == "t")
        {
            Message.Show("Request Submitted Successfully...!!!");
            loadsusername();
        }
        else
        {
            Message.Show("Unknown Error Occurred...!!!");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void ImageShow_Click(object sender, ImageClickEventArgs e)
    {
        string imageUrl = ViewState["image"] != null ? ViewState["image"].ToString() : "img/default.png";
        ImageLarge.ImageUrl = imageUrl;
        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showModal1();", true);
    }
}

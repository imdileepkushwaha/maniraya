using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class franchisee_PanCardImage : System.Web.UI.Page
{
    clsfranchisee objUser = new clsfranchisee();

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
                ViewState["Image"] = "img/default.png";
            }
        }
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
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString();
            string panImage = dt.Rows[0]["PanImage"].ToString();
            ImageShow.ImageUrl = string.IsNullOrWhiteSpace(panImage) ? "img/default.png" : panImage;
            ViewState["Image"] = ImageShow.ImageUrl;
            ApplyStatus(dt.Rows[0]["PanImgStatus"].ToString());
        }
        else
        {
            txtusername.Text = string.Empty;
            ImageShow.ImageUrl = "img/default.png";
            ViewState["Image"] = "img/default.png";
            divStatus.Visible = false;
            Message.Show("Invalid User Id...!!!");
        }
    }

    void ApplyStatus(string status)
    {
        if (status == "0")
        {
            divStatus.Visible = true;
            lblApprovalStatus.Text = "Pending";
            lblApprovalStatus.CssClass = "fr-kyc-status Pending";
        }
        else if (status == "1")
        {
            divStatus.Visible = true;
            lblApprovalStatus.Text = "Approved";
            lblApprovalStatus.CssClass = "fr-kyc-status Approved";
        }
        else if (status == "2")
        {
            divStatus.Visible = true;
            lblApprovalStatus.Text = "Rejected";
            lblApprovalStatus.CssClass = "fr-kyc-status Rejected";
        }
        else
        {
            divStatus.Visible = false;
        }
    }

    public string UploadImage()
    {
        if (!ImageUpload.HasFile)
        {
            return string.Empty;
        }

        string randomNumber = DateTime.Now.Ticks.ToString();
        string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
        string imageName = randomNumber + fileName;
        ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + imageName);
        return imageName;
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
            Message.Show("Please select PAN card image.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPanNumber.Text))
        {
            Message.Show("Please enter PAN number.");
            return;
        }

        objUser.PanImage = UploadImage();
        objUser.PanCardNo = txtPanNumber.Text.Trim().ToUpper();
        objUser.MentionBy = Session["fuserid"].ToString();
        objUser.UserId = Session["fuserid"].ToString();
        string rs = objUser.Update_UserPanForm(objUser);
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
        ImageLarge.ImageUrl = ViewState["Image"] != null ? ViewState["Image"].ToString() : "img/default.png";
        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showModal1();", true);
    }
}

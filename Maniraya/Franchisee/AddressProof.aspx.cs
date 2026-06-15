using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class franchisee_AddressProof : System.Web.UI.Page
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
                ImageShow2.ImageUrl = "img/default.png";
                ViewState["Image1"] = "img/default.png";
                ViewState["Image2"] = "img/default.png";
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
            string front = dt.Rows[0]["AadharImage"].ToString();
            string back = dt.Rows[0]["AadharImageBack"].ToString();
            ImageShow.ImageUrl = string.IsNullOrWhiteSpace(front) ? "img/default.png" : front;
            ImageShow2.ImageUrl = string.IsNullOrWhiteSpace(back) ? "img/default.png" : back;
            ViewState["Image1"] = ImageShow.ImageUrl;
            ViewState["Image2"] = ImageShow2.ImageUrl;

            if (dt.Columns.Contains("AadharNo") && dt.Rows[0]["AadharNo"] != DBNull.Value)
            {
                txtAdharnumber.Text = dt.Rows[0]["AadharNo"].ToString();
            }

            ApplyStatus(dt.Rows[0]["AadharImgStatus"].ToString());
        }
        else
        {
            txtusername.Text = string.Empty;
            ImageShow.ImageUrl = "img/default.png";
            ImageShow2.ImageUrl = "img/default.png";
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

    public string UploadImage(FileUpload fileUpload)
    {
        if (fileUpload == null || !fileUpload.HasFile)
        {
            return string.Empty;
        }

        string randomNumber = DateTime.Now.Ticks.ToString();
        string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
        string imageName = randomNumber + fileName;
        fileUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + imageName);
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

        if (!ImageUpload.HasFile || !ImageUpload2.HasFile)
        {
            Message.Show("Upload Aadhaar Card front and back.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtAdharnumber.Text))
        {
            Message.Show("Please enter Aadhar number.");
            return;
        }

        objUser.Addressproof = UploadImage(ImageUpload);
        objUser.AddressproofBack = UploadImage(ImageUpload2);
        objUser.AdhaarNo = txtAdharnumber.Text.Trim();
        objUser.UserId = Session["fuserid"].ToString();
        string rs = objUser.Update_AddressProof(objUser);
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
        ImageLarge.ImageUrl = ViewState["Image1"] != null ? ViewState["Image1"].ToString() : "img/default.png";
        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showModal1();", true);
    }

    protected void ImageShow2_Click(object sender, ImageClickEventArgs e)
    {
        ImageLarge.ImageUrl = ViewState["Image2"] != null ? ViewState["Image2"].ToString() : "img/default.png";
        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "showModal1();", true);
    }
}

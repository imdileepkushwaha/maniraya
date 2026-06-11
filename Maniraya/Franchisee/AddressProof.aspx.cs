using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class user_AddressProof : System.Web.UI.Page
{

    clsEPin objEPin = new clsEPin();
    clsfranchisee objUser = new clsfranchisee();
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["fuserid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadnotification();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }

    void loadnotification()
    {
        objUser.UserId = Session["fuserid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.getUserDetail(objUser);
        if (dt.Rows[0]["activestatus"].ToString() == "0")
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            objaccount.UserId = txtuserid.Text;
            ImageShow.ImageUrl = dt.Rows[0]["AadharImage"].ToString();
            ImageShow2.ImageUrl = dt.Rows[0]["AadharImageBack"].ToString();
            ViewState["Image1"] = dt.Rows[0]["AadharImage"].ToString();
            ViewState["Image2"] = dt.Rows[0]["AadharImageBack"].ToString();

            if (dt.Rows[0]["AadharImgStatus"].ToString() == "0")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Pending";
                lblApprovalStatus.CssClass = "Pending";
            }
            else if (dt.Rows[0]["AadharImgStatus"].ToString() == "1")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Approved";
                lblApprovalStatus.CssClass = "Approved";
            }
            else if (dt.Rows[0]["AadharImgStatus"].ToString() == "2")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Rejected";
                lblApprovalStatus.CssClass = "Rejected";
            }
            else
                divStatus.Visible = false;
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    public string UploadImage(FileUpload fileUpload)
    {
        string Imagename = "";
        if (ImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            fileUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);
        }
        return Imagename;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (ImageUpload.HasFile == false || ImageUpload2.HasFile == false)
                {
                    Message.Show("Upload Aadhaar Card Back and Front");
                    return;
                }

                objUser.Addressproof = UploadImage(ImageUpload);
                objUser.AddressproofBack = UploadImage(ImageUpload2);
                objUser.AdhaarNo = txtAdharnumber.Text;
                objUser.MentionBy = Session["fuserid"].ToString();
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
            else
            {
                Message.Show("Enter User Name...!!!");
            }
        }
        else
        {
            Message.Show("Enter User Id...!!!");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void ImageShow_Click(object sender, ImageClickEventArgs e)
    {
        ImageLarge.ImageUrl = ViewState["Image1"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    }

    protected void ImageShow2_Click(object sender, ImageClickEventArgs e)
    {
        ImageLarge.ImageUrl = ViewState["Image2"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    }
}
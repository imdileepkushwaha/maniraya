using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;


public partial class UploadKYC : System.Web.UI.Page
{

    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {

                if ((Request.QueryString["Id"] ?? "").ToString() != "")
                {
                    txtuserid.Text = Request.QueryString["Id"].ToString();
                    loadsusername();
                }


            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }

    protected void txtchange(object sender, EventArgs e)
    {
        loadsusername();
    }

   
    string GetStoredImageName(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return string.Empty;

        string value = imagePath.Trim();
        value = value.Replace("../ProductImage/", "").Replace("~/ProductImage/", "").Replace("ProductImage/", "");
        if (string.Equals(value, "img/default.png", StringComparison.OrdinalIgnoreCase))
            return string.Empty;
        return value;
    }

    string GetDisplayImageUrl(string imagePath)
    {
        string fileName = GetStoredImageName(imagePath);
        if (string.IsNullOrWhiteSpace(fileName))
            return "img/default.png";
        return "../ProductImage/" + fileName;
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

            #region SIGNUP
            ImageShow.ImageUrl = GetDisplayImageUrl(dt.Rows[0]["SignUpForm"].ToString());
            ViewState["Image"] = GetStoredImageName(dt.Rows[0]["SignUpForm"].ToString());

            if (dt.Rows[0]["SignUpImgStatus"].ToString() == "0")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Pending";
                lblApprovalStatus.CssClass = "admin-kyc-status Pending";
            }
            else if (dt.Rows[0]["SignUpImgStatus"].ToString() == "1")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Approved";
                lblApprovalStatus.CssClass = "admin-kyc-status Approved";
            }
            else if (dt.Rows[0]["SignUpImgStatus"].ToString() == "2")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Rejected";
                lblApprovalStatus.CssClass = "admin-kyc-status Rejected";
            }
            else
                divStatus.Visible = false;

            #endregion

            #region PAN
            ImageButton1.ImageUrl = GetDisplayImageUrl(dt.Rows[0]["PanImage"].ToString());
            ViewState["PANImage"] = GetStoredImageName(dt.Rows[0]["PanImage"].ToString());
            txtpanno.Text = dt.Rows[0]["PanNumber"].ToString();

            if (dt.Rows[0]["PanImgStatus"].ToString() == "0")
            {
                divpanstatus.Visible = true;
                lblpanstatus.Text = "Pending";
                lblpanstatus.CssClass = "admin-kyc-status Pending";
            }
            else if (dt.Rows[0]["PanImgStatus"].ToString() == "1")
            {
                divpanstatus.Visible = true;
                lblpanstatus.Text = "Approved";
                lblpanstatus.CssClass = "admin-kyc-status Approved";
            }
            else if (dt.Rows[0]["PanImgStatus"].ToString() == "2")
            {
                divpanstatus.Visible = true;
                lblpanstatus.Text = "Rejected";
                lblpanstatus.CssClass = "admin-kyc-status Rejected";
            }
            else
                divpanstatus.Visible = false;

            #endregion

            #region Cheque
            ImageButton2.ImageUrl = GetDisplayImageUrl(dt.Rows[0]["CancelCheque"].ToString());
            ViewState["ChequeImage"] = GetStoredImageName(dt.Rows[0]["CancelCheque"].ToString());

            if (dt.Rows[0]["ChequeImgStatus"].ToString() == "0")
            {
                divchequestatus.Visible = true;
                lblchequestatus.Text = "Pending";
                lblchequestatus.CssClass = "admin-kyc-status Pending";
            }
            else if (dt.Rows[0]["ChequeImgStatus"].ToString() == "1")
            {
                divchequestatus.Visible = true;
                lblchequestatus.Text = "Approved";
                lblchequestatus.CssClass = "admin-kyc-status Approved";
            }
            else if (dt.Rows[0]["ChequeImgStatus"].ToString() == "2")
            {
                divchequestatus.Visible = true;
                lblchequestatus.Text = "Rejected";
                lblchequestatus.CssClass = "admin-kyc-status Rejected";
            }
            else
                divchequestatus.Visible = false;

            #endregion

            #region Aadhar
            ImageButton3.ImageUrl = GetDisplayImageUrl(dt.Rows[0]["AadharImage"].ToString());
            ImageButton4.ImageUrl = GetDisplayImageUrl(dt.Rows[0]["AadharImageBack"].ToString());
            ViewState["AadharImageFront"] = GetStoredImageName(dt.Rows[0]["AadharImage"].ToString());
            ViewState["AadharImageBack"] = GetStoredImageName(dt.Rows[0]["AadharImageBack"].ToString());
            txtaadharno.Text = dt.Rows[0]["adharnumber"].ToString();
            if (dt.Rows[0]["AadharImgStatus"].ToString() == "0")
            {
                divaadharstatus.Visible = true;
                lblaadharstatus.Text = "Pending";
                lblaadharstatus.CssClass = "admin-kyc-status Pending";
            }
            else if (dt.Rows[0]["AadharImgStatus"].ToString() == "1")
            {
                divaadharstatus.Visible = true;
                lblaadharstatus.Text = "Approved";
                lblaadharstatus.CssClass = "admin-kyc-status Approved";
            }
            else if (dt.Rows[0]["AadharImgStatus"].ToString() == "2")
            {
                divaadharstatus.Visible = true;
                lblaadharstatus.Text = "Rejected";
                lblaadharstatus.CssClass = "admin-kyc-status Rejected";
            }
            else
                divaadharstatus.Visible = false;

            #endregion

        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    public string UploadSignUpImage()
    {
        string Imagename = "";
        if (ImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }

    public string UploadPanImage()
    {
        string Imagename = "";
        if (FileUpload1.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }


    public string UploadChequeBookImage()
    {
        string Imagename = "";
        if (FileUpload2.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload2.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }

    public string UploadAadharImageFront()
    {
        string Imagename = "";
        if (FileUpload3.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload3.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }


    public string UploadAadharImageBack()
    {
        string Imagename = "";
        if (FileUpload4.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload4.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload4.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {

                objUser.Signupform = UploadSignUpImage();
                if (ViewState["Image"] != null && objUser.Signupform.Trim() == "")
                {
                    objUser.Signupform = ViewState["Image"].ToString();
                }
                objUser.MentionBy = Session["useradmin"].ToString();
                objUser.UserId = txtuserid.Text;
                string rs = objUser.Update_UserKYC(objUser,"SIGNUP");
                if (rs == "t")
                {
                    Message.Show("SIGN UP Form Uploaded Successfully...!!!");
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

    protected void btnSubmitPan_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (txtpanno.Text != "")
                {

                    objUser.PanImage = UploadPanImage();
                    if (ViewState["PANImage"] != null && objUser.PanImage.Trim() == "")
                    {
                        objUser.PanImage = ViewState["PANImage"].ToString();
                    }
                    objUser.MentionBy = Session["useradmin"].ToString();
                    objUser.UserId = txtuserid.Text;
                    objUser.PanCardNo = txtpanno.Text;
                    string rs = objUser.Update_UserKYC(objUser, "PAN");
                    if (rs == "t")
                    {
                        Message.Show("Pan Card Uploaded Successfully...!!!");
                        loadsusername();
                    }
                    else if (rs == "p")
                    {
                        Message.Show("Pan Card Already Exist For 3 Users...!!!");                       
                    }
                    else
                    {
                        Message.Show("Unknown Error Occurred...!!!");
                    }
                }
                else
                {
                    Message.Show("Enter Pan No...!!!");
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

    protected void btnSubmitCheque_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                
                    objUser.CancelCheck = UploadChequeBookImage();
                    if (ViewState["ChequeImage"] != null && objUser.CancelCheck.Trim() == "")
                    {
                        objUser.CancelCheck = ViewState["ChequeImage"].ToString();
                    }
                    objUser.MentionBy = Session["useradmin"].ToString();
                    objUser.UserId = txtuserid.Text;
                    string rs = objUser.Update_UserKYC(objUser, "CHEQUE");
                    if (rs == "t")
                    {
                        Message.Show("Cancel Cheque/Passbook Uploaded Successfully...!!!");
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

    protected void btnSubmitAadhar_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (txtaadharno.Text != "")
                {
                    objUser.Addressproof = UploadAadharImageFront();
                    objUser.AddressproofBack = UploadAadharImageBack();
                    if (ViewState["AadharImageFront"] != null && objUser.Addressproof.Trim() == "")
                    {
                        objUser.Addressproof = ViewState["AadharImageFront"].ToString();
                    }
                    if (ViewState["AadharImageBack"] != null && objUser.AddressproofBack.Trim() == "")
                    {
                        objUser.AddressproofBack = ViewState["AadharImageBack"].ToString();
                    }
                    objUser.MentionBy = Session["useradmin"].ToString();
                    objUser.UserId = txtuserid.Text;
                    objUser.AdhaarNo = txtaadharno.Text;
                    string rs = objUser.Update_UserKYC(objUser, "ADHAR");
                    if (rs == "t")
                    {
                        Message.Show("Address Proof/Aadhar Uploaded Successfully...!!!");
                        loadsusername();
                    }
                    else if (rs == "a")
                    {
                        Message.Show("Address Proof/Aadhar Already Exist For 3 Users...!!!");                        
                    }
                    else
                    {
                        Message.Show("Unknown Error Occurred...!!!");
                    }

                }
                else
                {
                    Message.Show("Enter Aadhar NO...!!!");
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
   
   
}
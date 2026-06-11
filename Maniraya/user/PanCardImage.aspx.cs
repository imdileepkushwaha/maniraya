using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;
public partial class user_PanCardImage : System.Web.UI.Page
{

    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadnotification();

                if (GetPanEditStatus())
              {
                    div_update.Visible = true;
                    div_noupdate.Visible = false;
              }
             else
             {
                  div_update.Visible = false;
                   div_noupdate.Visible = true;
                   string url = "alert('You cannot upload Pan details.Please contact admin.');";
                 ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
               }
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }

    public bool GetPanEditStatus()
    {
        clsVerfification obj = new clsVerfification();
        DataTable dt = obj.getProfileEditableStatus(Session["userid"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsPanCardEditable"].ToString()))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    

    void loadnotification()
    {
        objUser.UserId = Session["userid"].ToString();
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
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString();
            objaccount.UserId = txtuserid.Text;
            ImageShow.ImageUrl = dt.Rows[0]["PanImage"].ToString();
            ViewState["Image"] = dt.Rows[0]["PanImage"].ToString();
            hdstatus.Value = dt.Rows[0]["status"].ToString(); 

            if (dt.Rows[0]["PanImgStatus"].ToString() == "0")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Pending";
                lblApprovalStatus.CssClass = "Pending";
            }
            else if (dt.Rows[0]["PanImgStatus"].ToString() == "1")
            {
                divStatus.Visible = true;
                lblApprovalStatus.Text = "Approved";
                lblApprovalStatus.CssClass = "Approved";
            }
            else if (dt.Rows[0]["PanImgStatus"].ToString() == "2")
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
    public string UploadImage()
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                objUser.PanImage = UploadImage();
                objUser.PanCardNo = txtPanNumber.Text;
                objUser.MentionBy = Session["userid"].ToString();
                objUser.UserId = Session["userid"].ToString();

                objUser.EditStatus = hdstatus.Value.ToString() == "Active" ? "0" : "1";

                string rs = objUser.Update_UserPanForm(objUser);
                if (rs == "t")
                {
                    Message.Show("Request Submitted Successfully...!!!");
                    loadsusername();
                    //if (GetPanEditStatus())
                    //{
                    //    div_update.Visible = true;
                    //    div_noupdate.Visible = false;
                    //}
                    //else
                    //{
                    //    div_update.Visible = false;
                    //    div_noupdate.Visible = true;
                    //}
                }
                else if (rs == "d")
                {
                    Message.Show("PAN Card Already Exist For 3 Users...!!!");                    
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
        ImageLarge.ImageUrl = ViewState["Image"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    }

}
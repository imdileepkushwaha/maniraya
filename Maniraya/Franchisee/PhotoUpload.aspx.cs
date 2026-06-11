using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class PhotoUpload : System.Web.UI.Page
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
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
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
            ImageShow.ImageUrl = dt.Rows[0]["photo"].ToString();
            ViewState["image"] = dt.Rows[0]["photo"].ToString();
        }
        else
        {
            ImageShow.ImageUrl = "img/default.png";
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {

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
        ImageLarge.ImageUrl = ViewState["image"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    }
}
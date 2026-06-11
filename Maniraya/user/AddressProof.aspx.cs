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
    clsUser objUser = new clsUser();
    clsState objState = new clsState();
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
                loadcountry();
                loaddata();

              if (GetAddressEditStatus())
              {
                    div_update.Visible = true;
                    div_noupdate.Visible = false;
               }
                else
               {
                   div_update.Visible = false;
                   div_noupdate.Visible = true;
                   string url = "alert('You cannot upload Address details.Please contact admin.');";
                  ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
               }

            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }


    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }


    public bool GetAddressEditStatus()
    {
        clsVerfification obj = new clsVerfification();
        DataTable dt = obj.getProfileEditableStatus(Session["userid"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsAddressProofEditable"].ToString()))
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
            objaccount.UserId = txtuserid.Text;
            ImageShow.ImageUrl = dt.Rows[0]["AadharImage"].ToString();
            ImageShow2.ImageUrl = dt.Rows[0]["AadharImageBack"].ToString();
            ViewState["Image1"] = dt.Rows[0]["AadharImage"].ToString();
            ViewState["Image2"] = dt.Rows[0]["AadharImageBack"].ToString();
            txtAdharnumber.Text = dt.Rows[0]["adharnumber"].ToString();
            hdstatus.Value = dt.Rows[0]["status"].ToString(); 

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
                    //Message.Show("Upload Aadhaar Card Back and Front");
                    //return;
                }


                objUser.Addressproof = UploadImage(ImageUpload) == "" ? ViewState["Image1"].ToString().Replace("../ProductImage/", "") : UploadImage(ImageUpload);
                objUser.AddressproofBack = UploadImage(ImageUpload2) == "" ? ViewState["Image2"].ToString().Replace("../ProductImage/", "") : UploadImage(ImageUpload2);
                //objUser.Addressproof = UploadImage(ImageUpload);
                //objUser.AddressproofBack = UploadImage(ImageUpload2);
                objUser.AdhaarNo = txtAdharnumber.Text;
                objUser.Address = txtaddress.Text;
                objUser.CountryId = ddcountry.SelectedValue.ToString();
                objUser.StateId = ddstate.SelectedValue.ToString();
                objUser.CityId = ddcity.SelectedValue.ToString();
                objUser.AreaName = txtareaname.Text;
                objUser.Pincode = txtpincode.Text; ;
                objUser.MentionBy = Session["userid"].ToString();
                objUser.UserId = Session["userid"].ToString();

                objUser.EditStatus = hdstatus.Value.ToString() == "Active" ? "0" : "1";

                string rs = objUser.Update_AddressProof(objUser);
                if (rs == "t")
                {
                    Message.Show("Request Submitted Successfully...!!!");
                    loadsusername();
                    //if (GetAddressEditStatus())
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
                else if (rs == "a")
                {
                    Message.Show("Address Proof/Aadhar Already Exist For 3 Users..!!!");                   
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
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }

    void loaddata()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.getUserDetail(objUser);
        if (dt.Rows.Count > 0)
        {
         
            loadsusername();
         
            txtaddress.Text = dt.Rows[0]["address"].ToString();
            ddcountry.SelectedValue = dt.Rows[0]["countryid"].ToString();
            loadstate();
            ddstate.SelectedValue = dt.Rows[0]["stateid"].ToString();
            loadcity();
            ddcity.SelectedValue = dt.Rows[0]["cityid"].ToString();
            txtareaname.Text = dt.Rows[0]["areaname"].ToString();
            txtpincode.Text = dt.Rows[0]["pincode"].ToString();
           
            hdstatus.Value = dt.Rows[0]["status"].ToString();
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
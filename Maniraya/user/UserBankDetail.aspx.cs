using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using DataTier;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserBankDetail : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsBank objbank = new clsBank();
    Data ObjData = new Data();

    public bool GetChequePassbookEditStatus()
    {
        clsVerfification obj = new clsVerfification();
        DataTable dt = obj.getProfileEditableStatus(Session["userid"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            return Convert.ToBoolean(dt.Rows[0]["IsChequePassbookEditabled"].ToString());
        }

        return false;
    }

    string NormalizePassbookFileName(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return string.Empty;

        string value = imagePath.Trim();
        value = value.Replace("../ProductImage/", "")
                     .Replace("~/ProductImage/", "")
                     .Replace("ProductImage/", "");

        if (string.Equals(value, "img/default.png", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        return value;
    }

    void BindPassbookPreview(string cancelChequeFile)
    {
        string fileName = NormalizePassbookFileName(cancelChequeFile);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            imgPassbook.Visible = false;
            imgPassbook.ImageUrl = string.Empty;
            if (lblPassbookPreviewText != null)
            {
                lblPassbookPreviewText.Text = "No passbook uploaded yet.";
                lblPassbookPreviewText.Visible = true;
            }
            return;
        }

        string ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (ext == ".pdf")
        {
            // Image control cannot render PDF; show text + keep downloadable path in tooltip.
            imgPassbook.Visible = false;
            imgPassbook.ImageUrl = string.Empty;
            if (lblPassbookPreviewText != null)
            {
                lblPassbookPreviewText.Text = "PDF uploaded. Open from admin approval screen.";
                lblPassbookPreviewText.Visible = true;
            }
            return;
        }

        imgPassbook.ImageUrl = "../ProductImage/" + fileName;
        imgPassbook.Visible = true;
        if (lblPassbookPreviewText != null)
            lblPassbookPreviewText.Visible = false;
    }

    void LoadPassbookSection(DataTable userDt)
    {
        try
        {
            // CancelCheque stored as file name in DB; map it to ProductImage/ for preview.
            string cancelChequeFile = userDt.Rows[0]["CancelCheque"] == null ? "" : userDt.Rows[0]["CancelCheque"].ToString();
            string chequeImgStatus = userDt.Rows[0]["ChequeImgStatus"] == null ? "" : userDt.Rows[0]["ChequeImgStatus"].ToString();

            BindPassbookPreview(cancelChequeFile);

            if (chequeImgStatus == "0")
            {
                lblPassbookApprovalStatus.Text = "Pending";
                lblPassbookApprovalStatus.CssClass = "profile-kyc-badge profile-kyc-pending";
            }
            else if (chequeImgStatus == "1")
            {
                lblPassbookApprovalStatus.Text = "Approved";
                lblPassbookApprovalStatus.CssClass = "profile-kyc-badge profile-kyc-approved";
            }
            else if (chequeImgStatus == "2")
            {
                lblPassbookApprovalStatus.Text = "Rejected";
                lblPassbookApprovalStatus.CssClass = "profile-kyc-badge profile-kyc-rejected";
            }
            else
            {
                lblPassbookApprovalStatus.Text = "-";
                lblPassbookApprovalStatus.CssClass = "profile-kyc-badge";
            }

            bool canUpload = GetChequePassbookEditStatus();
            fuPassbook.Visible = canUpload;
            btnPassbookSubmit.Visible = canUpload;
            div_passbook_update.Visible = canUpload;
            div_passbook_noupdate.Visible = !canUpload;
        }
        catch
        {
            lblPassbookApprovalStatus.Text = "-";
            lblPassbookApprovalStatus.CssClass = "profile-kyc-badge";
            if (lblPassbookPreviewText != null) lblPassbookPreviewText.Visible = true;
            imgPassbook.Visible = false;
            div_passbook_update.Visible = false;
            div_passbook_noupdate.Visible = true;
        }
    }

    string UploadPassbookImage()
    {
        if (!fuPassbook.HasFile)
        {
            return string.Empty;
        }

        // Basic safety: only allow common image/document types.
        string ext = Path.GetExtension(fuPassbook.FileName ?? string.Empty).ToLowerInvariant();
        string[] allowed = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
        if (Array.IndexOf(allowed, ext) < 0)
        {
            throw new Exception("Invalid file type. Upload JPG/PNG/PDF only.");
        }

        string randomNumber = DateTime.Now.Ticks.ToString();
        string fileName = Path.GetFileName(fuPassbook.PostedFile.FileName);
        string finalName = randomNumber + fileName;

        // Reuse existing KYC storage folder.
        fuPassbook.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + finalName);
        return finalName;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {

                loadcountry();
                loadbank();
                loaddata();

                if (GetBankEditStatus())
                {
                    div_update.Visible = true;
                    div_noupdate.Visible = false;
                }
                else
                {
                    div_update.Visible = false;
                    div_noupdate.Visible = true;
                    string url = "alert('You cannot update bank details.Please contact admin.');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    public bool GetBankEditStatus()
    {
        clsVerfification obj = new clsVerfification();
        DataTable dt = obj.getProfileEditableStatus(Session["userid"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsBankEditable"].ToString()))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }


    void loaddata()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = getUserDetail(objUser);
        if (dt.Rows.Count > 0)
        {
            txtsponserid.Text = dt.Rows[0]["sponserid"].ToString();
            loadsusername();
            txtname.Text = dt.Rows[0]["username"].ToString();
            txtmobile.Text = dt.Rows[0]["mobile"].ToString();
            txtemail.Text = dt.Rows[0]["email"].ToString();
            ddgender.SelectedValue = dt.Rows[0]["gender"].ToString();
            txtaddress.Text = dt.Rows[0]["address"].ToString();
            ddcountry.SelectedValue = dt.Rows[0]["countryid"].ToString();
            loadstate();
            ddstate.SelectedValue = dt.Rows[0]["stateid"].ToString();
            loadcity();
            ddcity.SelectedValue = dt.Rows[0]["cityid"].ToString();
            txtareaname.Text = dt.Rows[0]["areaname"].ToString();
            txtpincode.Text = dt.Rows[0]["pincode"].ToString();
            try
            {
                txtdateofbirth.Text = Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("dd/MM/yyyy");
            }
            catch { }
            txtnomineename.Text = dt.Rows[0]["nomineename"].ToString(); ;
            txtnomineerelation.Text = dt.Rows[0]["nomineerelation"].ToString(); ;
            txtaccountholdername.Text = dt.Rows[0]["accountholdername"].ToString(); ;
            txtaccountno.Text = dt.Rows[0]["accountno"].ToString(); ;
            txtpan.Text = dt.Rows[0]["pannumber"].ToString(); ;
            txtifsccode.Text = dt.Rows[0]["ifsccode"].ToString(); ;
            txtbranchname.Text = dt.Rows[0]["branchname"].ToString(); ;
            ddbank.SelectedValue = dt.Rows[0]["bankname"].ToString(); ;
            hdstatus.Value = dt.Rows[0]["status"].ToString();

            // Load CancelCheque/Passbook preview + approval status + upload availability.
            LoadPassbookSection(dt);
        }
    }


    public DataTable getUserDetail(clsUser objUser)
    {
        string str_query = "SELECT ud.*,cm.stateid,sm.countryid,sm.statename,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,(select UserName from userdetail where UserId=ud.parentuserid) as parentname,convert(char,ud.activatedate,103) as activationdate,(select planamount from UserTopupTb where userid=ud.userid and type='A') planamount FROM userdetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + objUser.UserId + "' ";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    void loadbank()
    {
        ddbank.Items.Clear();
        DataTable dt = new DataTable();
        dt = objbank.getBank();
        ddbank.DataSource = dt;
        ddbank.DataTextField = "BankName";
        ddbank.DataValueField = "BankID";
        ddbank.DataBind();
        ListItem li = new ListItem("Select Bank", "0");
        ddbank.Items.Insert(0, li);
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objUser.UserId = Session["userid"].ToString();
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.CountryId = ddcountry.SelectedValue.ToString();
        objUser.StateId = ddstate.SelectedValue.ToString();
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirth.Text);
        objUser.MentionBy = Session["userid"].ToString();
        objUser.NomineeName = txtnomineename.Text;
        objUser.NomineeRelation = txtnomineerelation.Text;
        objUser.AccHolderName = txtaccountholdername.Text;
        objUser.AccNo = txtaccountno.Text;
        objUser.IFSCCode = txtifsccode.Text;
        objUser.PanCardNo = txtpan.Text;
        objUser.BankName = ddbank.SelectedValue.ToString();
        objUser.BranchName = txtbranchname.Text;
        objUser.UpdateType = "Bank";

        objUser.EditStatus = hdstatus.Value.ToString() == "0" ? "1" : "0";

        string res = objUser.Update_UserProfile(objUser);
        if (res == "f")
        {
            string popupScript = "alert('User Not Found.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
            if (res == "0")
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                if (GetBankEditStatus())
                {
                    div_update.Visible = true;
                    div_noupdate.Visible = false;
                }
                else
                {
                    div_update.Visible = false;
                    div_noupdate.Visible = true;
                }

                string popupScript = "alert('User Details Updated Successfully.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
    }

    protected void btnPassbookSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!GetChequePassbookEditStatus())
            {
                string url = "alert('You cannot upload passbook. Please contact admin.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
                return;
            }

            if (!fuPassbook.HasFile)
            {
                string url = "alert('Please select passbook file.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
                return;
            }

            string passbookFile = UploadPassbookImage();
            if (string.IsNullOrWhiteSpace(passbookFile))
            {
                string url = "alert('Upload failed. Please try again.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), url, true);
                return;
            }

            objUser.UserId = Session["userid"].ToString();
            objUser.CancelCheck = passbookFile;
            objUser.MentionBy = Session["userid"].ToString();

            // After upload, disable further editing for active users.
            // Existing KYC flow uses IsChequePassbookEditabled = 0/1.
            objUser.EditStatus = (hdstatus.Value != null && (hdstatus.Value == "1" || hdstatus.Value.Equals("Active", StringComparison.OrdinalIgnoreCase)))
                ? "0"
                : "1";

            string rs = objUser.Update_UserCancelCheque(objUser);
            if (rs == "t")
            {
                // Show uploaded file immediately, then reload status/permissions from DB.
                BindPassbookPreview(passbookFile);
                loaddata();
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
                    "alert('Passbook uploaded successfully. Waiting for admin approval.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
                    "alert('Unknown error occurred while uploading passbook.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
                "alert('" + ex.Message.Replace("'", "") + "');", true);
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

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtsponserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtsponsername.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            if (txtsponserid.Text == "0")
            {

                txtsponsername.Text = "Company";
                txtsponserid.Text = "0";
            }
            else
            {
                txtsponsername.Text = "";
                txtsponserid.Text = "";
                string popupScript = "alert('Invalid User Id');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
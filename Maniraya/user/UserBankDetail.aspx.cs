using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserBankDetail : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsBank objbank = new clsBank();
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
        dt = objUser.getUserDetail(objUser);
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
        }
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
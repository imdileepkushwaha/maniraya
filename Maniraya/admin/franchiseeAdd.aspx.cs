using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class franchiseeAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsfranchisee objUser = new clsfranchisee();
    clsEPin objepin = new clsEPin();
    Clsmail objmail = new Clsmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
                loadfranchiseetype();
                FillYears();
                FillMonths();
                FillDays();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadfranchiseetype()
    {
        DDLstFranchiseeType.Items.Clear();
        DataTable dt = new DataTable();
        dt = objUser.getFranchiseetype();
        DDLstFranchiseeType.DataSource = dt;
        DDLstFranchiseeType.DataTextField = "type";
        DDLstFranchiseeType.DataValueField = "ID";
        DDLstFranchiseeType.DataBind();
        ListItem li = new ListItem("Select Type", "0");
        DDLstFranchiseeType.Items.Insert(0, li);
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
        //ListItem li1 = new ListItem("Other", "111111");
        //ddcity.Items.Insert(0, li1);
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    public void FillMonths()
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("ID");
        Dt.Columns.Add("Value");
        DataRow DR;

        for (int i = 1; i <= 12; i++)
        {
            DR = Dt.NewRow();
            DR["ID"] = i;
            DR["Value"] = ChkMonth(i.ToString());
            Dt.Rows.Add(DR);
        }
        ddlMonth.DataSource = Dt;
        ddlMonth.DataTextField = "Value";
        ddlMonth.DataValueField = "ID";
        ddlMonth.DataBind();
        ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString(); // Set current month as selected
    }
    public void FillYears()
    {
        //Fill Years
        for (int i = (DateTime.Now.Year - 50); i <= DateTime.Now.Year; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
        ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected
    }
    public void FillDays()
    {
        //Clear Days
        ddlDay.Items.Clear();
        //getting numbner of days in selected month & year
        int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

        //Fill days
        for (int i = 1; i <= noofdays; i++)
        {
            ddlDay.Items.Add(i.ToString());
        }
        ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
    }
    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfWizardStep.Value = "4";
        if (ddcity.SelectedIndex != 0)
        {
            if (ddcity.SelectedValue == "111111")
            {
                otherPnl.Visible = true;
            }
            else
            {
                otherPnl.Visible = false;
            }
        }
        loadTehsil();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    public string ChkMonth(string Code)
    {
        string Operator = "";
        switch (Code)
        {
            case "1":
                Operator = "Jan";
                break;
            case "2":
                Operator = "Feb";
                break;
            case "3":
                Operator = "Mar";
                break;
            case "4":
                Operator = "Apr";
                break;
            case "5":
                Operator = "May";
                break;
            case "6":
                Operator = "Jun";
                break;
            case "7":
                Operator = "Jul";
                break;
            case "8":
                Operator = "Aug";
                break;
            case "9":
                Operator = "Sep";
                break;
            case "10":
                Operator = "Oct";
                break;
            case "11":
                Operator = "Nov";
                break;
            case "12":
                Operator = "Dec";
                break;
        }

        return Operator;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {     
       
        
       // objUser.RegType = "free";
        objUser.StandingPosition = "1";
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
        objUser.DateOfBirthnew = ddlDay.SelectedValue.ToString() + "/" + ddlMonth.SelectedItem.ToString() + "/" + ddlYear.SelectedItem.ToString();
        objUser.Password = txtuserpassword.Text;
        objUser.MentionBy = Session["useradmin"].ToString();
        objUser.SponserId = "0";
        objUser.EpinNo = "0";
        objUser.OtherCity = TxtOtherCity.Text;



        objUser.outletName = txtOutletName.Text;
        objUser.PanCardNo = txtPANNo.Text;
        objUser.PanImage = Convert.ToString(ViewState["PAN_Img"]);

        objUser.gstNo = txtGSTNo.Text;
        objUser.gstImg = Convert.ToString(ViewState["GST_Img"]);
        objUser.SponserId = txtSponsorId.Text;

        objUser.TehsilId = "26";//ddlsttehsil.SelectedValue.ToString();
        objUser.MarketId ="21"; //ddlstmarket.SelectedValue.ToString();
        objUser.RegType = DDLstFranchiseeType.SelectedValue;
        string res = objUser.Insert_User(objUser);
        if (res == "f")
        {
            string popupScript = "alert('Mobile No Already Exists');";
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
                //objmail.sendmail(txtname.Text, txtuserpassword.Text, res, txtemail.Text);
                string popupScript = "alert('Franchisee Added Successfully, FranchiseeId is " + res + "');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                txtname.Text = txtmobile.Text = txtemail.Text =  txtaddress.Text = txtuserpassword.Text = txtconfirmpassword.Text = txtpincode.Text = txtareaname.Text = "";
               
                ddcountry.SelectedValue = "0";
                loadstate();
                loadcity();
            }
       
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfWizardStep.Value = "4";
        loadstate();
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfWizardStep.Value = "4";
        loadcity();
    }
    
    public string UploadPAN()
    {
        string Imagename = "";
        if (filePAN.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(filePAN.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            filePAN.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }

    public string UploadGST()
    {
        string Imagename = "";
        if (fileGST.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(fileGST.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            fileGST.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }

    protected void btnPANUPload_Click(object sender, EventArgs e)
    {
        hfWizardStep.Value = "3";
        if (!filePAN.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Select PAN File')", true);
            return;
        }

        ViewState["PAN_Img"] = UploadPAN();

        imgPAN.ImageUrl = "~/ProductImage/" + ViewState["PAN_Img"];
    }

    protected void btnGSTUpload_Click(object sender, EventArgs e)
    {
        hfWizardStep.Value = "3";
        if (!fileGST.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Select GST File')", true);
            return;
        }

        ViewState["GST_Img"] = UploadGST();

        imgGST.ImageUrl = "~/ProductImage/" + ViewState["GST_Img"];
    }

    protected void imgPAN_Click(object sender, ImageClickEventArgs e)
    {
        ImagePANLarge.ImageUrl = "~/ProductImage/" + ViewState["PAN_Img"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    }

    protected void imgGST_Click(object sender, ImageClickEventArgs e)
    {
        ImageGSTLarge.ImageUrl = "~/ProductImage/" + ViewState["GST_Img"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
    }
    protected void txtSponsorId_TextChanged(object sender, EventArgs e)
    {
        hfWizardStep.Value = "1";
        loadsusername();
    }

    void loadsusername()
    {
        if (DDLstFranchiseeType.SelectedIndex != 0)
        {
            DataTable dt = new DataTable();
           // clsUser objUser = new clsUser();
            objUser.UserId = txtSponsorId.Text;
            dt = objUser.getuserdetailviaprocedure(objUser);
          //  dt = objUser.getUserName(objUser);
            if (dt.Rows.Count > 0)
            {

                if (Convert.ToInt16(dt.Rows[0]["FranchiseeType"].ToString()) == 1)
                {

                    if (txtSponsorId.Text == "F000001")
                    {
                        txtSponsorName.Text = dt.Rows[0]["username"].ToString();
                        TxtType.Text = dt.Rows[0]["type"].ToString();
                    }
                    else
                    {
                        txtSponsorId.Text = "";
                        txtSponsorName.Text = "";
                        string popupScript = "alert('You are creating Gallary Franchiee, Please entry only F000001');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }
                else
                {
                    if (Convert.ToInt16(dt.Rows[0]["FranchiseeType"].ToString()) <= Convert.ToInt16(DDLstFranchiseeType.SelectedValue))
                    {
                        txtSponsorName.Text = dt.Rows[0]["username"].ToString();
                        TxtType.Text = dt.Rows[0]["type"].ToString();

                    }
                    else
                    {
                        txtSponsorId.Text = "";
                        txtSponsorName.Text = "";
                        string popupScript = "alert('Sponsor Id should be higher');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }            
                   
               
            }
            else
            {
                txtSponsorId.Text = "";
                txtSponsorName.Text = "";
                string popupScript = "alert('Invalid Sponsor Id');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            txtSponsorId.Text = "";
            txtSponsorName.Text = "";
            string popupScript = "alert('Invalid Sponsor Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }

    protected void ddlsttehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmarket();
    }
    protected void ddlstmarket_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void loadTehsil()
    {
        ddlsttehsil.Items.Clear();
        DataTable dt = new DataTable();
        objState.CityId = ddcity.SelectedValue.ToString();
        dt = objState.getTehsil(objState);

        ddlsttehsil.DataSource = dt;
        ddlsttehsil.DataTextField = "tehsilName";
        ddlsttehsil.DataValueField = "tehsilID";
        ddlsttehsil.DataBind();
        ListItem li = new ListItem("Select Tehsil", "0");
        ddlsttehsil.Items.Insert(0, li);
    }
    void loadmarket()
    {
        ddlstmarket.Items.Clear();
        DataTable dt = new DataTable();
        objState.TehsilId = ddlsttehsil.SelectedValue.ToString();
        dt = objState.getMarket(objState);

        ddlstmarket.DataSource = dt;
        ddlstmarket.DataTextField = "Marketname";
        ddlstmarket.DataValueField = "MarketId";
        ddlstmarket.DataBind();
        ListItem li = new ListItem("Select market", "0");
        ddlstmarket.Items.Insert(0, li);
    }
}
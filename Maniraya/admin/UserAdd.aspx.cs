using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_UserAdd : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsEPin objepin = new clsEPin();
    clsnewmail objmail = new clsnewmail();
    clsVerfification objverfiy = new clsVerfification();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Useradmin"] != null)
            {
                // txtsponserid.Text = Session["Userid"].ToString();
                // txtsponserid.Enabled = false;
                // loadsUsername();
                loadcountry();
                loadAmountepin();
                RdBtnLeft.Checked = true;

                #region make epin sign up
                /*
                //make epin sign up
                RdBtnEpin.Checked = true;
                pnlpin.Visible = true;*/

                #endregion

                #region make free sign up

                RdBtnFree.Checked = true;
                pnlpin.Visible = false;

                #endregion

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
        ddlMonth.SelectedValue=System.DateTime.Now.Month.ToString(); // Set current month as selected
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void DDLstPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepin();
    }
    void loadAmountepin()
    {
        DataTable dt = new DataTable();
        objepin.GenerateUserId = txtsponserid.Text;
       
        dt = objepin.getEPinForPinTransfer(objepin);
        DDLstPlan.DataSource = dt;
        DDLstPlan.DataTextField = "Planname";
        DDLstPlan.DataValueField = "Planamount";
        DDLstPlan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        DDLstPlan.Items.Insert(0, li);
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Regex for Mobile No.

        System.Text.RegularExpressions.Regex regexObj = new System.Text.RegularExpressions.Regex(@"^\(?([6-9]{1})\)?([0-9]{9})$");

        if (!regexObj.IsMatch(txtmobile.Text))
        {
            string popupScript = "alert('Enter a Valid Mobile No.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        #endregion
        if (txtmobile.Text.Length != 10)
        {

            string popupScript = "alert('Mobile No should be 10 digit');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (txtsponsername.Text == "")
        {
            string popupScript = "alert('Invalid Sponser Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (RdBtnEpin.Checked == true)
        {
            objUser.RegType = "epin";
            if (ddepin.SelectedIndex == -1 || ddepin.SelectedIndex == 0)
            {
                string popupScript = "alert('Select Epin');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                return;
            }
            if (DDLstPlan.SelectedIndex == 0)
            {
                string popupScript = "alert('Select Plan');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                return;
            }
        }
        else
        {
            objUser.RegType = "free";
        }
        if (RdBtnLeft.Checked == true)
        {
            objUser.StandingPosition = "1";
        }
        if (RdBtnRight.Checked == true)
        {
            objUser.StandingPosition = "2";
        }
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityId = "1";// ddcity.SelectedValue.ToString();
        objUser.CountryId = "1";// ddcountry.SelectedValue.ToString();
        objUser.StateId = "1";// ddstate.SelectedValue.ToString();
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        objUser.DateOfBirthnew = ddlDay.SelectedValue.ToString() + "/" +ddlMonth.SelectedItem.ToString() + "/" + ddlYear.SelectedItem.ToString();

        objUser.Password = txtUserpassword.Text;
        //objUser.Password = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
       // objUser.Password = RandomNumbers();

        objUser.PanImage = UploadImage();
        objUser.PanCardNo = txtPanNumber.Text;


        objUser.MentionBy = Session["Useradmin"].ToString();
        objUser.SponserId = txtsponserid.Text;
        objUser.EpinNo = ddepin.SelectedValue.ToString();
        objUser.OtherCity = TxtOtherCity.Text;


       // Random generator = new Random();
       // String mobotp = generator.Next(0, 999999).ToString("D6");

       //objUser.OTPMobile = mobotp;

       // objUser.OTPMobile = "1234";
        //ViewState["OTPMobile"] = objUser.OTPMobile;

        //String emailotp = generator.Next(0, 999999).ToString("D6");
        //while (emailotp == mobotp)
        //{
        //    emailotp = generator.Next(0, 999999).ToString("D6");
        //}
        //objUser.OTPEmail = emailotp;
        //objUser.OTPEmail = "1234";
        //ViewState["OTPEmail"] = objUser.OTPEmail;

        //string res = objUser.Insert_User(objUser);
       // objmail.MailData("1224", "srivastavadevesh05@gmail.com");
        //string res = objUser.Insert_User_Unverfied(objUser);
        string res = objUser.Insert_User(objUser);
        if (res == "-1")
        {
            string popupScript = "alert('Mobile No Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
            if (res == "-2")
            {
                string popupScript = "alert('Email ID Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
                if (res == "-3")
                {
                    string popupScript = "alert('Sponsor ID Limit Exceeded');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
                else
                    if (res == "-4")
                    {
                        string popupScript = "alert('Sponsor ID Wrong');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
        else

            if (res == "0")
            {
                string popupScript = "alert('Unknown error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                #region Send SMS for Password

                //clsAPI objapi = new clsAPI();

                //DataTable dt = new DataTable();

                //dt = objapi.GetSmsApi(Session["Userid"].ToString());

                //if (dt.Rows.Count > 0)
                //{
                //    string smsBody = "Registration successful.\nUser Id: " + res + " and Password: " + objUser.Password + "\nLogin here http://arsenpay.in/User/index.aspx";

                //    string smsAPI = dt.Rows[0]["Url"].ToString();

                //    smsAPI = smsAPI.Replace("to=mmm", "to=" + txtmobile.Text);

                //    smsAPI = smsAPI.Replace("smstext=ttt", "smstext=" + smsBody);

                //    System.Net.WebRequest request = System.Net.HttpWebRequest.Create(smsAPI);
                //    System.Net.WebResponse response = request.GetResponse();
                //    System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                //    string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
                //    Response.Write(urlText.ToString());

                //    clsSecureService css = new clsSecureService();

                //    css.Insert_SendSMS(txtmobile.Text, urlText, smsBody);

                //    objapi = null;
                //    dt = null;
                //    css = null;
                //}

                #endregion

                //send sms
              
                //clsLogin objlogin = new clsLogin();
                //objlogin.SendotpMobileVerification(mobotp, txtmobile.Text.Trim());

              

                //ViewState["UserId"] = res;
                //Clsmail obm = new Clsmail();
                //obm.sendOTPmailVerfication(txtname.Text, emailotp, res, txtemail.Text);


                ViewState["MobileNo"] = txtmobile.Text.Trim();
                ViewState["EmailId"] = txtemail.Text;
                ViewState["Password"] = objUser.Password ;
                ViewState["Name"] = txtname.Text;

                string popupScript = "alert('User Added Successfully, UserId is " + res + " and password is " + objUser.Password + ".');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                

                //txtname.Text = txtmobile.Text = txtemail.Text = txtaddress.Text = txtUserpassword.Text = txtconfirmpassword.Text = txtpincode.Text = txtareaname.Text = "";
                txtname.Text = txtmobile.Text = txtemail.Text = txtaddress.Text = txtPanNumber.Text = txtpincode.Text = txtareaname.Text = "";               
                loadstate();               
                loadcity();              
                loadepin();
                txtamount.Text = "";
                //FillYears();
                //FillMonths();
                //FillDays(); 
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

    void loadsUsername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtsponserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["status"].ToString().ToUpper() == "ACTIVE")
            {
                txtsponsername.Text = dt.Rows[0]["Username"].ToString();
            }
            else
            {
                txtsponsername.Text = "";
                txtsponserid.Text = "";
                string popupScript = "alert('Sponser is not prime member');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            txtsponsername.Text = "";
            txtsponserid.Text = "";
            string popupScript = "alert('Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void txtsponserid_TextChanged(object sender, EventArgs e)
    {
        loadsUsername();
        loadepin();
    }
    void loadepin()
    {
        ddepin.Items.Clear();
        objepin.GenerateUserId = txtsponserid.Text;
        objepin.Amount = Convert.ToDecimal(DDLstPlan.SelectedValue);
        DataTable dt = new DataTable();
        dt = objepin.getEPinForRegamount(objepin);
        ddepin.DataSource = dt;
        ddepin.DataTextField = "EpinNo";
        ddepin.DataValueField = "EpinNo";
        ddepin.DataBind();
        ListItem li = new ListItem("Select E-Pin", "0");
        ddepin.Items.Insert(0, li);

        //ddepin.Items.Clear();
        //objepin.GenerateUserId = txtsponserid.Text;
        //DataTable dt = new DataTable();
        //dt = objepin.getEPinForReg(objepin);
        //ddepin.DataSource = dt;
        //ddepin.DataTextField = "EpinNo";
        //ddepin.DataValueField = "EpinNo";
        //ddepin.DataBind();
        //ListItem li = new ListItem("Select E-Pin", "0");
        //ddepin.Items.Insert(0, li);
    }
    protected void ddepin_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepinamount();
    }
    void loadepinamount()
    {
        objepin.EPinNo = ddepin.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objepin.getEPinFullDetail(objepin);
        if (dt.Rows.Count > 0)
        {
            txtamount.Text = dt.Rows[0]["amount"].ToString();
        }
        else
        {
            txtamount.Text = "";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
   
    protected void RdBtnFree_CheckedChanged(object sender, EventArgs e)
    {
        pnlpin.Visible = false;
    }
    protected void RdBtnEpin_CheckedChanged(object sender, EventArgs e)
    {
        pnlpin.Visible = true;
    }
    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    }

    public string UploadImage()
    {
        string Imagename = "";
        if (panUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = System.IO.Path.GetFileName(panUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            panUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);
        }
        return Imagename;
    }

    #region Get Random Integers

    private static Random random = new Random((int)DateTime.Now.Ticks);
    private static string RandomNumbers(int length = 6)
    {
        const string pool = "0123456789";
        var chars = Enumerable.Range(0, length)
            .Select(x => pool[random.Next(0, pool.Length)]);
        return new string(chars.ToArray());
    }

    #endregion
    protected void txtmobile_TextChanged(object sender, EventArgs e)
    {
        objUser.Mobile = txtmobile.Text;
        DataTable Dt = objUser.getUserwithmobileno(objUser);
        if (Dt.Rows.Count > 0)
        {
            Message.Show("This Mobile No Already Exists");
            txtmobile.Text = string.Empty;
        }
    }


    public void show(string message)
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }


    protected void btnverifymob_Click(object sender, EventArgs e)
    {
        string UserId = (ViewState["UserId"] ?? "").ToString();
        if (txtmobotp.Text.Trim() == "")
        {
            show("Enter OTP");
        }
        else
            if ((ViewState["OTPMobile"] ?? "").ToString() == txtmobotp.Text)
            {
               
                int r = objverfiy.Verify(UserId, txtmobotp.Text.Trim(), "Mobile");
                if (r == 1)
                {
                    btnverifymob.Attributes.Add("disabled", "disabled");
                    btnresendmobotp.Attributes.Add("disabled", "disabled");
                    btnresendmobotp.Visible = false;
                    txtmobotp.Enabled = false;
                    show("Mobile Verified Successfully");
                    lblmobstatus.Text = "Mobile Verified";
                    getStatus(1);
                }
                else
                    if (r == 2)
                    {
                        show("Invalid OTP");
                    }
                    else
                    {
                        show("Temporary Error");
                    }
            }
            else
            {
                show("Invalid OTP");
            }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "d1", "showFranchiseeModal();", true);

    }
    protected void btnverifyemail_Click(object sender, EventArgs e)
    {
        string UserId = (ViewState["UserId"] ?? "").ToString();

        if (txtemailotp.Text.Trim() == "")
        {
            show("Enter OTP");
        }
        else
            if ((ViewState["OTPEmail"] ?? "").ToString() == txtemailotp.Text)
            {
                int r = objverfiy.Verify(UserId, txtemailotp.Text.Trim(), "Email");
                if (r == 1)
                {
                    btnverifyemail.Attributes.Add("disabled", "disabled");
                    btnresendemailotp.Attributes.Add("disabled", "disabled");
                    btnresendemailotp.Visible = false;
                    txtemailotp.Enabled = false;
                    show("Email Verified Successfully");
                    lblemailstatus.Text = "Email Verified";
                    getStatus(1);
                }
                else
                    if (r == 2)
                    {
                        show("Invalid OTP");
                    }
                    else
                    {
                        show("Temporary Error");
                    }
            }
            else
            {
                show("Invalid OTP");
            }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop4", "showFranchiseeModal();", true);
    }

    public void getStatus(int s)
    {
        string UserId = (ViewState["UserId"] ?? "").ToString();

        try
        {
            DataTable dt = objverfiy.getStatus(UserId);
            if (dt == null)
            {
                objverfiy.SendSmsEmail(ViewState["MobileNo"].ToString(),ViewState["EmailId"].ToString(),ViewState["Name"].ToString(),ViewState["UserId"].ToString(), ViewState["Password"].ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Added Successfully. UserID is :" + ViewState["UserId"].ToString() + "');location.href='Useradd.aspx';", true);
            }
            else
            {
                int mobstatus = Convert.ToBoolean(dt.Rows[0]["MobStatus"].ToString()) ? 1 : 0;
                int emailstatus = Convert.ToBoolean(dt.Rows[0]["EmailStatus"].ToString()) ? 1 : 0;
                if (mobstatus == 1 && emailstatus == 1)
                {
                    btnverifymob.Attributes.Add("disabled", "disabled");
                    btnverifyemail.Attributes.Add("disabled", "disabled");
                    objverfiy.SendSmsEmail(ViewState["MobileNo"].ToString(),ViewState["EmailId"].ToString(),ViewState["Name"].ToString(),ViewState["UserId"].ToString(), ViewState["Password"].ToString());
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Added Successfully. UserID is :"+ViewState["UserId"].ToString()+"');location.href='Useradd.aspx';", true);
                }
                if (s == 0)
                {
                    string mobb = "";
                    if (mobstatus == 0)
                    {
                        Random generator = new Random();
                        String mobotp = generator.Next(0, 999999).ToString("D6");
                        mobb = mobotp;
                        int c = objverfiy.UpdateOtp(UserId, mobotp, "Mobile");
                        if (c == 1)
                        {
                            //send otp
                            clsLogin objlogin = new clsLogin();
                            //objlogin.Sendotp(mobotp, dt.Rows[0]["Mobile"].ToString());
                            objlogin.SendotpMobileVerification(mobotp, dt.Rows[0]["Mobile"].ToString());

                            ViewState["OTPMobile"] = mobotp;
                            divmob.Visible = true;
                        }
                        else
                        {
                            objverfiy.SendSmsEmail(ViewState["MobileNo"].ToString(),ViewState["EmailId"].ToString(),ViewState["Name"].ToString(),ViewState["UserId"].ToString(), ViewState["Password"].ToString());
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Added Successfully. UserID is :" + ViewState["UserId"].ToString() + "');location.href='Useradd.aspx';", true);
                        }
                    }
                    if (emailstatus == 0)
                    {
                        Random generator = new Random();
                        String emailotp =  generator.Next(0, 999999).ToString("D6");
                        //if (mobb != "")
                        //{
                        //    while (emailotp == mobb)
                        //    {
                        //        emailotp = generator.Next(0, 999999).ToString("D6");
                        //    }
                        //}
                        int c = objverfiy.UpdateOtp(UserId, emailotp, "Email");
                        if (c == 1)
                        {
                            //send email
                            Clsmail obkemail = new Clsmail();
                            //obkemail.sendOTPmail(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());
                            obkemail.sendOTPmailVerfication(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());

                            ViewState["OTPEmail"] = emailotp;
                            divemail.Visible = true;
                        }
                        else
                        {
                            objverfiy.SendSmsEmail(ViewState["MobileNo"].ToString(),ViewState["EmailId"].ToString(),ViewState["Name"].ToString(),ViewState["UserId"].ToString(), ViewState["Password"].ToString());
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Added Successfully. UserID is :" + ViewState["UserId"].ToString() + "');location.href='Useradd.aspx';", true);
                        }
                    }
                }
            }

        }
        catch (Exception)
        {
            //Response.Redirect("Logout.aspx");
        }

    }


   

    protected void btnresendmobotp_Click(object sender, EventArgs e)
    {
        string UserId = (ViewState["UserId"] ?? "").ToString();
        try
        {
            DataTable dt = objverfiy.getStatus(UserId);
            clsLogin objlogin = new clsLogin();
            Random generator = new Random();
            String mobotp = generator.Next(0, 999999).ToString("D6");
            //objlogin.Sendotp(mobotp, dt.Rows[0]["Mobile"].ToString());
            objlogin.SendotpMobileVerification(mobotp, dt.Rows[0]["Mobile"].ToString());
            objverfiy.UpdateOtp(UserId, mobotp, "Mobile");
            show(" OTP Send On Mobile");

        }
        catch (Exception)
        {
            show("Cannot Send OTP");
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "d3", "showFranchiseeModal();", true);

    }

    protected void btnresendemailotpClick(object sender, EventArgs e)
    {
        string UserId = (ViewState["UserId"] ?? "").ToString();
        try
        {
            DataTable dt = objverfiy.getStatus(UserId);
            clsLogin objlogin = new clsLogin();
            Random generator = new Random();
            String emailotp = generator.Next(0, 999999).ToString("D6");
            emailotp = "1234";
            //objmail.sendOTPmail(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());
            Clsmail obkemail = new Clsmail();
            obkemail.sendOTPmailVerfication(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());
            objverfiy.UpdateOtp(UserId, emailotp, "Email");
            show(" OTP Send On Email");

        }
        catch (Exception)
        {
            show("Cannot Send OTP");
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "d4", "showFranchiseeModal();", true);

    }




}
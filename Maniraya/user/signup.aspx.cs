using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARA_StringHunt;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

public partial class signup : System.Web.UI.Page
{
    const string CaptchaSessionKey = "SignupCaptcha";
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsEPin objepin = new clsEPin();
    Clsmail objmail = new Clsmail();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateCaptcha();
            ApplyReferralPosition();
            if (Request.QueryString["UserId"] != null)
            {
                loadcountry();
               // txtdob.Text = DateTime.Now.ToString("dd/MM/yyyy");
                RdBtnFree.Checked = true;
                RdBtnLeft.Checked = true;
                txtsponserid.Text = Request.QueryString["UserId"].ToString();
                // txtepin.Text = Request.QueryString["EPinNo"].ToString();
                loadsusername();
                //txtparentid.Text = Request.QueryString["UserId"].ToString();
                //loadParentname();
                //  loadepinamount();
                RdBtnLeft.Checked = true;
                //if (Request.QueryString["EPinNo"] != null)
                //{
                //    if (Request.QueryString["EPinNo"].ToString() == "1")
                //    {
                //        RdBtnLeft.Checked = true;
                //    }
                //    else
                //    {
                //        RdBtnRight.Checked = true;
                //    }

                //}

              //  loadAmountepin();

              //  loadepin();
                txtsponserid.Enabled = false;
                RdBtnFree.Checked = true;

                //if (Request.QueryString["Standingposition"].ToString() == "1")
                //{
                //    RdBtnLeft.Checked = true;
                //}
                //if (Request.QueryString["Standingposition"].ToString() == "2")
                //{
                //    RdBtnRight.Checked = true;
                //}
                FillYears2();
                FillYears();
                FillMonths2();
                FillMonths();
                FillDays();
                FillDays2();

                loadbank();

                if (Request.QueryString["p"] != null)
                {
                    if (Request.QueryString["p"].ToString() == "1")
                    {
                        ddposition.SelectedValue = "Left";
                    }

                    if (Request.QueryString["p"].ToString() == "2")
                    {
                        ddposition.SelectedValue = "Right";
                    }
                }

            }
            else
            {
                RdBtnFree.Checked = true;
                RdBtnLeft.Checked = true;
                loadcountry();

                FillYears2();
                FillYears();
                FillMonths2();
                FillMonths();
                FillDays();
                FillDays2();

                loadbank();
                //Response.Redirect("http://raxtan.com");
            }
        }
    }

    void ApplyReferralPosition()
    {
        string standing = Request.QueryString["standingposition"];
        if (string.IsNullOrEmpty(standing))
        {
            standing = Request.QueryString["Standingposition"];
        }

        if (standing == "2")
        {
            ddposition.SelectedValue = "Right";
        }
        else
        {
            ddposition.SelectedValue = "Left";
        }
    }

    void SyncPositionPickerUi()
    {
        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            UpdatePanel1.GetType(),
            "syncSignupPosition",
            "if (typeof syncSignupPositionPicker === 'function') { syncSignupPositionPicker(true); }",
            true);
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


    public void FillMonths2()
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
        ddlMonth2.DataSource = Dt;
        ddlMonth2.DataTextField = "Value";
        ddlMonth2.DataValueField = "ID";
        ddlMonth2.DataBind();
        ddlMonth2.SelectedValue = System.DateTime.Now.Month.ToString(); // Set current month as selected
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
        for (int i = (DateTime.Now.Year - 100); i <= DateTime.Now.Year; i++)
        {
            ddlYear.Items.Add(i.ToString());

        }
        ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected

    }


    public void FillYears2()
    {
        //Fill Years
        for (int i = (DateTime.Now.Year - 100); i <= DateTime.Now.Year; i++)
        {

            ddlYear2.Items.Add(i.ToString());
        }

        ddlYear2.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected
    }
    public void FillDays()
    {
        int previousDay = 0;
        if (ddlDay.SelectedItem != null)
        {
            int.TryParse(ddlDay.SelectedValue, out previousDay);
        }
        if (previousDay <= 0)
        {
            previousDay = DateTime.Now.Day;
        }

        ddlDay.Items.Clear();

        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int month = Convert.ToInt32(ddlMonth.SelectedValue);
        int noofdays = DateTime.DaysInMonth(year, month);

        for (int i = 1; i <= noofdays; i++)
        {
            ddlDay.Items.Add(i.ToString());
        }

        // Months like Jun/Apr/Sep/Nov have 30 days; Feb has 28/29.
        // Never force today's day (e.g. 31) when it does not exist in the selected month.
        int dayToSelect = Math.Min(previousDay, noofdays);
        ListItem dayItem = ddlDay.Items.FindByValue(dayToSelect.ToString());
        if (dayItem != null)
        {
            dayItem.Selected = true;
        }
        else if (ddlDay.Items.Count > 0)
        {
            ddlDay.SelectedIndex = 0;
        }
    }

    public void FillDays2()
    {
        int previousDay = 0;
        if (ddlDay2.SelectedItem != null)
        {
            int.TryParse(ddlDay2.SelectedValue, out previousDay);
        }
        if (previousDay <= 0)
        {
            previousDay = DateTime.Now.Day;
        }

        ddlDay2.Items.Clear();

        int year = Convert.ToInt32(ddlYear2.SelectedValue);
        int month = Convert.ToInt32(ddlMonth2.SelectedValue);
        int noofdays2 = DateTime.DaysInMonth(year, month);

        for (int i = 1; i <= noofdays2; i++)
        {
            ddlDay2.Items.Add(i.ToString());
        }

        int dayToSelect = Math.Min(previousDay, noofdays2);
        ListItem dayItem = ddlDay2.Items.FindByValue(dayToSelect.ToString());
        if (dayItem != null)
        {
            dayItem.Selected = true;
        }
        else if (ddlDay2.Items.Count > 0)
        {
            ddlDay2.SelectedIndex = 0;
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }

    protected void ddlYear_SelectedIndexChanged2(object sender, EventArgs e)
    {
        FillDays2();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }

    protected void ddlMonth_SelectedIndexChanged2(object sender, EventArgs e)
    {
        FillDays2();
    }
    protected void DDLstPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepin();
    }
    void loadAmountepin()
    {
        DDLstPlan.Items.Clear();
        if (string.IsNullOrWhiteSpace(txtsponserid.Text))
        {
            DDLstPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
            return;
        }

        objepin.GenerateUserId = txtsponserid.Text.Trim();
        DataTable dt = objepin.getEPinForPinTransfer(objepin);
        if (dt == null || dt.Rows.Count == 0)
        {
            DDLstPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
            return;
        }

        DDLstPlan.DataSource = dt;
        DDLstPlan.DataTextField = "planname";
        DDLstPlan.DataValueField = "Planamount";
        DDLstPlan.DataBind();
        DDLstPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        txtuserpassword.Attributes.Add("value", txtuserpassword.Text);
        txtconfirmpassword.Attributes.Add("value", txtconfirmpassword.Text);
        if (CheckBox1.Checked)
        {
            btnSubmit.Enabled = true;
        }

        else
        {
            btnSubmit.Enabled = false;
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

    void loadbank()
    {
        ddbank.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getbank();
        ddbank.DataSource = dt;
        ddbank.DataTextField = "BankName";
        ddbank.DataValueField = "BankID";
        ddbank.DataBind();
        ListItem li = new ListItem("Select Bank", "0");
        ddbank.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        if (dt.Rows.Count > 0)
        {
            //  txtmobile.Text = dt.Rows[0]["countrycode"].ToString();
        }

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
        if (string.IsNullOrWhiteSpace(txtCaptcha.Text))
        {
            ShowCaptchaError("Please enter the security code.");
            return;
        }

        if (!ValidateCaptcha())
        {
            ShowCaptchaError("Invalid security code. Please try again.");
            return;
        }

        //if (txtsponsername.Text == "")
        //{
        //    string popupScript = "alert('Invalid Sponser Id');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    return;
        //}

        // if (txtaadhar.Text.Length < 12)
        // {
        //    string popupScript = "alert('Incorrect Adhar');";
        //   ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //   return;
        // }

        if (txtmobile.Text.Length < 10)
        {
            string popupScript = "alert('Incorrect Mobile Number');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        if (txtmobile.Text =="")
        {
            string popupScript = "alert('Mobile Number is required');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (txtemail.Text == "")
        {
            string popupScript = "alert('Email is required');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (DropDownList1.SelectedIndex == 0)
        {
            string popupScript = "alert('Gender is required');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (txtname.Text == "")
        {
            string popupScript = "alert('Name is required');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        if (RdBtnEpin.Checked == true)
        {
            objUser.RegType = "epin";
            if (ddepin.SelectedIndex == -1 || ddepin.SelectedIndex == 0)
            {
                string popupScript = "alert('Select Epin');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                return;
            }
            if (DDLstPlan.SelectedIndex == 0)
            {
                string popupScript = "alert('Select Plan');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                return;
            }
        }
        else
        {
            objUser.RegType = "free";
        }
        if (ddposition.SelectedValue == "Left")
        {
            objUser.StandingPosition = "1";
        }
        if (ddposition.SelectedValue == "Right")
        {
            objUser.StandingPosition = "2";
        }
        objUser.UserName = txtname.Text;
        objUser.Height = txtheight.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.AdhaarNo = txtaadhar.Text;
        objUser.NomineeName = txtnomineename.Text;
        objUser.NomineeRelation = ddrelation.SelectedValue.ToString();
        objUser.Email = txtemail.Text;
        objUser.PanCardNo = txtPanNumber.Text;
        objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityId = "1";// ddcity.SelectedValue.ToString(); ;
        objUser.CountryId = ddcountry.SelectedValue.ToString();
        objUser.StateId = ddstate.SelectedValue.ToString();
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        /*
        if (txtdob.Text != "")
        {
            objUser.DateOfBirth = Message.GetIndianDate(txtdob.Text);
        }
        else
        {
            objUser.DateOfBirth = DateTime.MinValue;
        }
        if (txtdob.Text != "")
        {
            objUser.DateOfBirth = Message.GetIndianDate(txtdob.Text).AddDays(1);
        }
        else
        {
            objUser.DateOfBirth = DateTime.MinValue;
        */
        objUser.DateOfBirth = new DateTime(
            Convert.ToInt32(ddlYear.SelectedValue),
            Convert.ToInt32(ddlMonth.SelectedValue),
            Convert.ToInt32(ddlDay.SelectedValue));
        objUser.NomDateOfBirth = new DateTime(
            Convert.ToInt32(ddlYear2.SelectedValue),
            Convert.ToInt32(ddlMonth2.SelectedValue),
            Convert.ToInt32(ddlDay2.SelectedValue));
        objUser.Password = txtuserpassword.Text;
        objUser.MentionBy = "Outside";
        if(txtsponserid.Text!="")
        {
            objUser.SponserId = txtsponserid.Text;
        }
       else
        {
            objUser.SponserId = "TW000001";
        }
        objUser.EpinNo = ddepin.SelectedValue.ToString();
        objUser.ParentUserId = txtparentid.Text;
        //  objUser.EpinNo = txtepin.Text;
        string res = objUser.Insert_User(objUser);
        if (res == "f")
        {
            ResetCaptcha();
            string popupScript = "alert('Mobile No Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else

            if (res == "0")
        {
            ResetCaptcha();
            string popupScript = "alert('Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (res == "m")
        {
            ResetCaptcha();
            string popupScript = "alert('this SponserId limit is full');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

        else if (res == "ext")
        {
            ResetCaptcha();
            string popupScript = "alert('this Mobile Number Is Already Exist');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

        else if (res == "e")
        {
            ResetCaptcha();
            string popupScript = "alert('this link is already used');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }


        else if (res == "c")
        {
            ResetCaptcha();
            string popupScript = "alert('User is not 10 years');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

        else if (res == "-2")
        {
            ResetCaptcha();
            string popupScript = "alert('Email Already Exist');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

        else
        {
            DataTable dt = new DataTable();
            objUser.UserId = res.ToString();

            dt = objUser.getConfirmationMessage(objUser);
            if (dt.Rows.Count > 0)
            {
                Session["LoginId1"] = dt.Rows[0]["UserID"].ToString();
                Session["Password1"] = dt.Rows[0]["Password"].ToString();
                Session["SponsorName1"] = dt.Rows[0]["SponsorName"].ToString();
                Session["SponserId1"] = dt.Rows[0]["SponserId"].ToString();
                Session["UserName2"] = dt.Rows[0]["UserName"].ToString();
                Session["TransPassword1"] = dt.Rows[0]["TransPassword"].ToString();

                string username = txtname.Text;
                string userid = res;
                string password = txtuserpassword.Text;
                string useremail = txtemail.Text;
                string number = txtmobile.Text;

                //   userMail(username, userid, password, useremail);

                // smssending(number, username, userid, password);

                Response.Redirect("ConfirmRegistration.aspx");
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('User Added Successfully, UserId is " + dt.Rows[0]["UserID"].ToString() + "'');window.location.href='index.aspx';", true);
          //  string popupScript = "alert('User Added Successfully, UserId is " + res + "');";
          //  ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            // txtmobotp.Text = string.Empty;
            loadstate();
            loadcity();
            loadepin();
            //txtamount.Text = "";
            //FillYears();
            //FillMonths();
            //FillDays();
        }

    }


    public bool userMail(string username, string userid, string password, string useremail)
    {

        MailMessage mail = new MailMessage();
        //
        string Subject2 = "Welcome to Divyayajurveda Wellness";
        string Body2 = "Dear " + username + ",<br><br> Welcome to Divyayajurveda Wellness Thank you for choosing us. <br><br>Your user id :-" + userid + " <br> Your password :-" + password + "<br><br> You can logged in your account from here: <br><br> <button style=' appearance: none; -webkit-appearance: none; font-family: sans-serif; cursor: pointer; padding: 12px; min-width: 100px; border: 0px; -webkit-transition: background-color 100ms linear; -ms-transition: background-color 100ms linear; transition: background-color 100ms linear;  outline: 0; border-radius: 8px;   background: #3498db; color: #ffffff;  background: #2980b9; color: #ffffff;'><a style='color:white' href='#'>Login</a></button><br><br>  For more details please checkout our website www.divyayajurvedawellness.com/ <br><br>If you need any help, you can email us at customer@divyayajurvedawellness.com<br><br>Sincerely,<br>Divyayajurveda Wellness & Team";
        //


        ContentType mimeType = new System.Net.Mime.ContentType("text/html");
        string body = HttpUtility.HtmlDecode(Body2);
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
        mail.AlternateViews.Add(alternate);

        mail.To.Add(useremail);
        mail.From = new MailAddress("divyayajurveda@gmail.com");
        mail.Subject = Subject2;
        mail.Body = Body2;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("divyayajurveda@gmail.com", "wjk6 bcgl urgs a4b4 j32x bo4h qsns wfjw");


        try
        {
            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public string smssending(string number, string username, string userid, string password)
    {
        string txtnumber = number;
        string txtmessage = "Dear " + username + ",Thank you for choosing  SANDHYA INFO SOLUTION. Please use the following data to login. Your User id :- " + userid + "and Your password :- " + password + ". For login please visit 'https://GOOGLE.COM/'";
        //txtmessage = txtmessage.Replace("#", "%23").Replace(":", "%3A").Replace(",", "%2C").Replace(" ", "%20");


        string strurl = "http://chatway.in/api/" + txtnumber + "&message=" + txtmessage + "&";

        //string strurl = "http://chatway.in/api/send-msg?username=SG686869&number=" + txtnumber + "&message=" + txtmessage + "&token=TDlua0RhVlZQOFhMTGlOSFU3bG5GQT09";
        //string strurl = "http://chatway.in/api/send-file?username=SG686869&number=" + txtnumber + "&message=" + txtmessage + "&token=TDlua0RhVlZQOFhMTGlOSFU3bG5GQT09&file_url=&file_name=";

        string result = apicall(strurl);

        return result;
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }

    public string Insert_User(clsUser objUser)
    {
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            s2 = "sp_add_UserDetailAuto";
            SqlParameter[] parameter = {
                    new SqlParameter("@SponserId",objUser.SponserId),
                     new SqlParameter("@UplineId",objUser.ParentUserId),
                    new SqlParameter("@username",objUser.UserName),
                    new SqlParameter("@DateofBirth",objUser.DateOfBirthnew),
                    new SqlParameter("@Gender",objUser.Gender),
                    new SqlParameter("@Email",objUser.Email),
                    new SqlParameter("@Mobile",objUser.Mobile),
                    new SqlParameter("@Address",objUser.Address),
                    new SqlParameter("@CityId",objUser.CityId),
                    new SqlParameter("@AreaName",objUser.AreaName),
                    new SqlParameter("@Pincode",objUser.Pincode),
                    new SqlParameter("@Password",objUser.Password),
                    new SqlParameter("@MentionBy",objUser.MentionBy),
                    new SqlParameter("@EPinNo",objUser.EpinNo),
                     new SqlParameter("@StandingPosition",objUser.StandingPosition),
                      new SqlParameter("@RegType",objUser.RegType),
                      new SqlParameter("@OtherCity",objUser.OtherCity),
                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

            //string url = string.Concat(new string[]
            //{
            //    "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=",
            //    objUser.Mobile,
            //    "&dcs=0&msgtxt=Dear User you are successfully registered on arsenpay.in Your login details are-userid:",
            //    res,
            //    ", password:",
            //    objUser.Password,
            //    "&state=4"
            //});
            string url = objUser.smstemplate(res, objUser.Mobile, objUser.Password, objUser.Password, "jdnsonsgroup", "http://jdnsonsgroup.com", "", "", "", "", "", "", "", "registration");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string Result = url.CallURL();
            objUser.Insert_SendSMS(objUser.Mobile, Result, url);
            tr.Commit();

        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
    protected void lnkRefreshCaptcha_Click(object sender, EventArgs e)
    {
        GenerateCaptcha();
        txtCaptcha.Text = string.Empty;
    }

    void GenerateCaptcha()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var random = new Random(Guid.NewGuid().GetHashCode());
        char[] code = new char[6];

        for (int i = 0; i < code.Length; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        string captcha = new string(code);
        Session[CaptchaSessionKey] = captcha;
        lblCaptchaCode.Text = captcha;
    }

    bool ValidateCaptcha()
    {
        string expected = Convert.ToString(Session[CaptchaSessionKey]);
        string entered = txtCaptcha.Text.Trim();

        if (string.IsNullOrEmpty(expected) || string.IsNullOrEmpty(entered))
        {
            return false;
        }

        return string.Equals(expected, entered, StringComparison.OrdinalIgnoreCase);
    }

    void ResetCaptcha()
    {
        GenerateCaptcha();
        txtCaptcha.Text = string.Empty;
    }

    void ShowCaptchaError(string message)
    {
        ResetCaptcha();
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
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
        if (string.IsNullOrWhiteSpace(txtsponserid.Text))
        {
            txtsponsername.Text = "";
            return;
        }

        objUser.UserId = txtsponserid.Text.Trim();
        DataTable dt = objUser.getUserName(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtsponsername.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            txtsponsername.Text = "";
            txtsponserid.Text = "";
            string popupScript = "alert('Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    void loadParentname()
    {
        if (string.IsNullOrWhiteSpace(txtparentid.Text))
        {
            txtparentname.Text = "";
            return;
        }

        objUser.UserId = txtparentid.Text.Trim();
        DataTable dt = objUser.getUserName(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtparentname.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            txtparentname.Text = "";
            txtparentid.Text = "";
            string popupScript = "alert('Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void txtsponserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
        loadAmountepin();
        SyncPositionPickerUi();
    }
    protected void txtparentid_TextChanged(object sender, EventArgs e)
    {
        loadParentname();

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
        //objepin.EPinNo = ddepin.SelectedValue.ToString();
        objepin.EPinNo = txtepin.Text;
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;


public partial class VerifyRegistration : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsEPin objepin = new clsEPin();
    Clsmail objmail = new Clsmail();
    clsVerfification objverfiy = new clsVerfification();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["UserId"] != null)
            {
                getStatus(0);
            }
            else
            {
                Response.Redirect("Logout.aspx");
            }
        }
    }
   

    public void getStatus(int s)
    {
        try
        {
            byte[] b = Convert.FromBase64String((Request.QueryString["UserId"] ?? "").ToString());
            string UserId = System.Text.Encoding.UTF8.GetString(b);
            DataTable dt = objverfiy.getStatus(UserId);
            if (dt == null)
            {
                Response.Redirect("Logout.aspx");
            }
            else
            {
                int mobstatus = Convert.ToBoolean(dt.Rows[0]["MobStatus"].ToString())?1:0;
                int emailstatus = Convert.ToBoolean(dt.Rows[0]["EmailStatus"].ToString())?1:0;
                if (mobstatus == 1 && emailstatus == 1)
                {
                    btnverifymob.Attributes.Add("disabled", "disabled");
                    btnverifyemail.Attributes.Add("disabled", "disabled");
                    //if (s == 0)
                    //{
                        Response.Redirect("index.aspx");
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "location.href='index.aspx';", true);
                    //}
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
                            Response.Redirect("index.aspx");
                        }
                    }
                    if (emailstatus == 0)
                    {
                        Random generator = new Random();
                        String emailotp = generator.Next(0, 999999).ToString("D6");
                        //if (mobb != "")
                        //{
                        //    while (emailotp == mobb)
                        //    {
                        //        emailotp = generator.Next(0, 999999).ToString("D6");
                        //    }
                        //}
                     emailotp = "1234";
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
                            Response.Redirect("index.aspx");
                        }
                    }
                }
            }

        }
        catch (Exception)
        {
            Response.Redirect("Logout.aspx");
        }
       
    }
   

    
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }


    protected void btnverifymob_Click(object sender, EventArgs e)
    {
        if (txtmobotp.Text.Trim() == "")
        {
            show("Enter OTP");
        }
        else
        if ((ViewState["OTPMobile"] ?? "").ToString() == txtmobotp.Text)
        {
            byte[] b = Convert.FromBase64String((Request.QueryString["UserId"] ?? "").ToString());
            string UserId = System.Text.Encoding.UTF8.GetString(b);
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
    }
    protected void btnverifyemail_Click(object sender, EventArgs e)
    {
        if (txtemailotp.Text.Trim() == "")
        {
            show("Enter OTP");
        }
        else
            if ((ViewState["OTPEmail"] ?? "").ToString() == txtemailotp.Text)
        {
            byte[] b = Convert.FromBase64String((Request.QueryString["UserId"] ?? "").ToString());
            string UserId = System.Text.Encoding.UTF8.GetString(b);
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
    }
    public void show(string message)
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('"+message+"');", true);
    }



    protected void btnresendmobotp_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] b = Convert.FromBase64String((Request.QueryString["UserId"] ?? "").ToString());
            string UserId = System.Text.Encoding.UTF8.GetString(b);
            DataTable dt = objverfiy.getStatus(UserId);
            clsLogin objlogin = new clsLogin();
            Random generator = new Random();
            String mobotp = generator.Next(0, 999999).ToString("D6");
            //objlogin.Sendotp(mobotp, dt.Rows[0]["Mobile"].ToString());

            objlogin.SendotpMobileVerification(mobotp, dt.Rows[0]["Mobile"].ToString());

            objverfiy.UpdateOtp(UserId, mobotp, "Mobile");
            show(" OTP Send On Mobile");

        }
        catch(Exception)
        {
            show("Cannot Send OTP");
        }
    }

    protected void btnresendemailotpClick(object sender, EventArgs e)
    {
        try
        {

            byte[] b = Convert.FromBase64String((Request.QueryString["UserId"] ?? "").ToString());
            string UserId = System.Text.Encoding.UTF8.GetString(b);
            DataTable dt = objverfiy.getStatus(UserId);
            clsLogin objlogin = new clsLogin();
            Random generator = new Random();
            String emailotp = generator.Next(0, 999999).ToString("D6");
            emailotp = "1234";
            //objmail.sendOTPmail(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());

            objmail.sendOTPmailVerfication(dt.Rows[0]["UserName"].ToString(), emailotp, UserId, dt.Rows[0]["Email"].ToString());

            objverfiy.UpdateOtp(UserId, emailotp, "Email");
            show(" OTP Send On Email");

        }
        catch (Exception)
        {
            show("Cannot Send OTP");
        }

    }


}
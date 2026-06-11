using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class CHangeTransactionPassword : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    clsLogin objlogin = new clsLogin();
    clsUser objUser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objUser.UserId = Session["userid"].ToString();

        dt = objUser.getUserDetail(objUser);

        ViewState["userMobile"] = dt.Rows[0]["Mobile"].ToString();

        txtoldpassword.Enabled = false;
        ViewState["oldPwd"] = txtoldpassword.Text;
        txtuserpassword.Enabled = false;
        ViewState["newPwd"] = txtuserpassword.Text;
        txtconfirmpassword.Enabled = false;
        btnSubmit.Enabled = false;
        btnCancel.Enabled = false;

        divOTP.Visible = true;

        string generateOTP = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

        ViewState["otp"] = generateOTP;

        string message = "Your OTP is " + generateOTP + " For change password, Never share your otp or account details with anybody.";

        #region Send Custom SMS


        clsAPI objapi = new clsAPI();

        clsSecureService css = new clsSecureService();

        DataTable dt1 = new DataTable();

        dt1 = objapi.GetSmsApi("admin");

        if (dt1.Rows.Count > 0)
        {
            string smsBody = message;

            string smsAPI = dt1.Rows[0]["Url"].ToString();

            smsAPI = smsAPI.Replace("{mobileno}", ViewState["userMobile"].ToString()).Replace("{msg}", smsBody);

            System.Net.WebRequest request = System.Net.HttpWebRequest.Create(smsAPI);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
            Response.Write(urlText.ToString());

            css.Insert_SendSMS(ViewState["userMobile"].ToString(), urlText, smsBody);

            objapi = null;
            dt1 = null;
        }

        #endregion

        msg.InnerText = "OTP Sent Successfully....";

        //objlogin.username = Session["userid"].ToString();
        //objlogin.password = txtoldpassword.Text;
        //objlogin.newpassword = txtuserpassword.Text;
        //int res = objlogin.ChangeUserPassword(objlogin);
        //if (res == 1)
        //{
        //    string popupScript = "alert('Password Updated Successfully');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    txtconfirmpassword.Text = txtoldpassword.Text = txtuserpassword.Text = "";
        //}
        //else
        //{
        //    string popupScript = "alert('Invalid Old Password.');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Path);
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (txtOTP.Text == ViewState["otp"].ToString())
        {
            objlogin.username = Session["userid"].ToString();
            //objlogin.password = txtoldpassword.Text;
            //objlogin.newpassword = txtuserpassword.Text;
            objlogin.password = ViewState["oldPwd"].ToString();
            objlogin.newpassword = ViewState["newPwd"].ToString();
            int res = objlogin.ChangeUserTransactionPassword(objlogin);
            if (res == 1)
            {
                string popupScript = "alert('Password Updated Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                txtconfirmpassword.Text = txtoldpassword.Text = txtuserpassword.Text = txtOTP.Text = "";
                divOTP.Visible = false;
                txtoldpassword.Enabled = true;
                txtuserpassword.Enabled = true;
                txtconfirmpassword.Enabled = true;
                btnSubmit.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                string popupScript = "alert('Invalid Old Password.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            msg.InnerText = "OTP do not match";
        }
    }
}
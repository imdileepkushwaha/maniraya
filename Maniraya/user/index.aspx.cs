using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using DataTier;
using ARA_StringHunt;

public partial class admin_index : System.Web.UI.Page
{
    clsLogin objlogin = new clsLogin();
    clsUser objuser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        objlogin.username = txtusername.Text;
        objlogin.password = txtpassword.Text;
        ViewState["pwd"] = txtpassword.Text;
        objlogin.ipaddress = GetIp();        
        DataTable dt = new DataTable();
        dt = objlogin.Chk_UserLoginDetails(objlogin);
        if (dt.Rows.Count > 0)
        {
            clsVerfification vr = new clsVerfification();
            DataTable dtverify = vr.getStatus(txtusername.Text);

            DataTable dt2 = objlogin.Chk_userDetails(txtusername.Text);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                //if (dtverify != null && dtverify.Rows[0]["MobStatus"].ToString() == "True" && dtverify.Rows[0]["EmailStatus"].ToString() == "True")
                //{
                Session["userid"] = txtusername.Text;
                Session["username"] = dt.Rows[0]["username2"].ToString();
                Session["UserImage"] = dt.Rows[0]["UserImage"].ToString();
                Session["status"] = dt.Rows[0]["status1"].ToString();
                Session["Mobile"] = dt.Rows[0]["status1"].ToString();
                if (dt.Rows[0]["status123"].ToString() == "1")
                {
                    ViewState["otp"] = dt.Rows[0]["otp"].ToString();
                    Sendotp(dt.Rows[0]["otp"].ToString(), dt.Rows[0]["mobile"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
                //}
                //else
                //{
                //    byte[] byt = System.Text.Encoding.UTF8.GetBytes(txtusername.Text);
                //    Response.Redirect("VerifyRegistration.aspx?UserId=" + Convert.ToBase64String(byt));
                //}
            }
            else
            {
                dt = objlogin.Chk_UserTempLoginDetails(objlogin);
                if (dt.Rows.Count > 0)
                {
                    byte[] byt = System.Text.Encoding.UTF8.GetBytes(txtusername.Text);
                    Response.Redirect("VerifyRegistration.aspx?UserId=" + Convert.ToBase64String(byt));
                }
                else
                {
                    Message.Show("Invalid Login Details...!!!");
                }
            }
        }
        else
        {
                Message.Show("Invalid Login Details...!!!");
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        objuser.UserId= txtuserid.Text;

        string res = objlogin.SendPassword(objuser);
        
        if (res == "0")
        {
            Message.Show("Error Occurred");
        }
        else
            if (res == "f")
            {
                Message.Show("Invalid User Id");
            }
            else
            {
                Message.Show("Password sent to your registered mobile no. & Email");
            }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        //lblmessage.Text = "sgdsgsd";

        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

    }

   
    protected void BtnResend_Click(object sender, EventArgs e)
    {

        //objlogin.Sendotp(ViewState["otp"].ToString(), Session["Mobile"].ToString());
        //string popupScript2 = "Closepopup();";
        //ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);


        objlogin.username = txtusername.Text;
        objlogin.password = ViewState["pwd"].ToString();
        objlogin.ipaddress = GetIp();

        DataTable dt = new DataTable();
        dt = objlogin.Chk_UserLoginDetails(objlogin);
        if (dt.Rows.Count > 0)
        {
            Session["userid"] = txtusername.Text;
            Session["username"] = dt.Rows[0]["username2"].ToString();
            Session["UserImage"] = dt.Rows[0]["UserImage"].ToString();
            Session["status"] = dt.Rows[0]["status1"].ToString();
            if (dt.Rows[0]["status123"].ToString() == "1")
            {
                objlogin.Sendotp(dt.Rows[0]["otp"].ToString(), dt.Rows[0]["mobile"].ToString());
                string popupScript2 = "Closepopup();";
                ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);

            }
            else
            {
                Response.Redirect("Dashboard.aspx");
            }
        }
        else
        {
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            Message.Show("Invalid Login Details...!!!");
        }
    }
    public string GetIp()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        objlogin.username = txtusername.Text;
        objlogin.OTP = TxtOtp.Text;
        objlogin.ipaddress = GetIp();
        string res = objlogin.Confirmotp(objlogin);
        if (res == "1")
        {
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            LblMessages.Visible = true;
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);
        }
    }
    public string Sendotp(string OTP, string MObile)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable dt = new DataTable();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {

            string url = "http://sms.shortmsgservice.com/sms-panel/api/http/index.php?username=sandhyasoftware&apikey=B8F9A-D4004&apirequest=Text&sender=UPPDTE&mobile=" + MObile + "&message=Dear Customer,Your Login OTP For Maniraya Is " + OTP + " Please do not share OTP with anyone.sms&route=TRANS&TemplateID=1607100000000328562&format=JSON";
            string Result = url.CallURL();
            Insert_SendSMS(MObile, Result, url);
            res = MObile;

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
    public string Insert_SendSMS(string str_Mobile, string str_Result, string str_Message)
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
            s2 = "insert into SendSms(CreateDate,Mobile,Result,Message)  values (getdate(),'" + str_Mobile + "','" + str_Result + "','" + str_Message + "') ";
            ObjData.RunInsUpDelQueryTrans(s2, tr);
            res = "t";
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
}
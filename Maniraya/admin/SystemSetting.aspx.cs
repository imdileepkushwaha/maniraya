using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.IO;
using System.Text;
using BusinessLogicTier;

public partial class admin_SystemSetting : System.Web.UI.Page
{
    clsSetting objsetting = new clsSetting();
    public string LoginId = "";
    public string flagstr = "sub";
    public string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }

        if (!IsPostBack)
        {
            fillform();
        }
    }
    protected void ButSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objsetting.Insert_SystemSettings(Convert.ToInt32(txt_InterRech.Text), Convert.ToInt32(txt_InterTrans.Text), Convert.ToInt32(txt_MinFundTransfer.Text), Convert.ToInt32(txt_MaxFundTransfer.Text), Convert.ToInt32(Txt_BackDate.Text), LoginId, txtDMR.Text, txtDDlPushNotification.Text);

            string popupScript = "alert('Data Saved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
          
            fillform();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void fillform()
    {
        DataTable dt = new DataTable();
        dt = objsetting.GetSystemSettings();
        if (dt.Rows.Count > 0)
        {
            txt_InterRech.Text = dt.Rows[0]["IntervalBtRecharge"].ToString();
            txt_InterTrans.Text = dt.Rows[0]["IntervalBtTransfer"].ToString();
            txt_MinFundTransfer.Text = dt.Rows[0]["MinFundTransfer"].ToString();
            txt_MaxFundTransfer.Text = dt.Rows[0]["MaxFundTransfer"].ToString();
            Txt_BackDate.Text = dt.Rows[0]["BackDate"].ToString();
            txtDMR.Text = dt.Rows[0]["DMRVerification"].ToString();
            txtDDlPushNotification.Text = dt.Rows[0]["App_Key"].ToString();
        }
    }

   
    public void PushNotification(string Key, string _DeviceId)
    {

        string postData =
   "{ \"to\": \"" + _DeviceId + "\"," +
    "\"data\": { \"message\": \"\", \"key\" : \"" + Key + "\"  }  }";

        //string response = SendGCMNotification("AIzaSyDuNdGaq69i0WtPb9K2C8OJOz-92DJmdTk", postData);
        string response = SendGCMNotification("AAAArVSedqM:APA91bEfiywuW8jORm5LDU6vBLqEDyOUPQ_zUeOQeQSJNIA6a-BFjSIRsw3C5anMdoE5dTBTDNN5SyOKlL_wYnPPgIPZUp1WzofR4ouOzsekRlodAXH4b-d5u3FnLntYr4n_wY-Z7S7I", postData);

    }
    private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
    {
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  
        //  MESSAGE CONTENT  
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  
        //  CREATE REQUEST  
        //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        Request.Method = "POST";
        //  Request.KeepAlive = false;  

        Request.ContentType = postDataContentType;
        Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
        Request.ContentLength = byteArray.Length;

        Stream dataStream = Request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        //  
        //  SEND MESSAGE  
        try
        {
            WebResponse Response = Request.GetResponse();

            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                var text = "Unauthorized - need new token";
            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                var text = "Response from web service isn't OK";
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadToEnd();
            Reader.Close();

            return responseLine;
        }
        catch (Exception e)
        {
        }
        return "error";
    }
    public static bool ValidateServerCertificate(
                                                object sender,
                                                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                                X509Chain chain,
                                                SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public string updatepush()
    {
        string key = txtDDlPushNotification.Text;
        if (chkDDlPushNotification.Checked == true)
        {
            objsetting.Update_Push(key, "1");            
            return "Without Push Notification";
        }
        else
        {
            DataTable dt = new DataTable();
            dt=objsetting.Update_Push(key, "2");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PushNotification(dt.Rows[i]["App_Key"].ToString(), dt.Rows[i]["DeviceId"].ToString());
                }
            }
            return "With Push Notification";
        }
    }

    protected void btn_key_Click(object sender, EventArgs e)
    {
        txtDDlPushNotification.Text = Guid.NewGuid().ToString();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
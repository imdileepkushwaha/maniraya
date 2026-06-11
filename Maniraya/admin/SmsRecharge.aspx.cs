using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARA_StringHunt;
using System.Data.SqlClient;
using DataTier;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using BusinessLogicTier;
public partial class Api_sms : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        clsRecharge objrecharge = new clsRecharge();
        //if (Request.Browser.Browser != "Unknown")
        //{
        //    return;
        //}
         string MobileNo = Convert.ToString(Request.QueryString["from"]);
        string Sms = Convert.ToString(Request.QueryString["message"]);
        string VMN = Convert.ToString(Request.QueryString["receiver"]);
        if (MobileNo == null)
            MobileNo = "";
        if (Sms == null)
            Sms = "";
        if (VMN == null)
            VMN = "";
       
        string WhiteLabelID = "1";
        if (!IsPostBack)
        {

            try
           {

                string result = "";               
                string RechargeTransactionID = "";
                DataTable dt = ActionFromSms(Sms, MobileNo, VMN);
                if (dt.Rows.Count>0)
                {
                    string UserId = dt.Rows[0]["Userid"].ToString();
                    string ApiName = dt.Rows[0]["ApiName"].ToString();
                    //string WhiteLabelId = dt.Rows[0]["WhiteLabelId"].ToString();
                    string TranId = dt.Rows[0]["TransactionTID"].ToString();
                    string Url = dt.Rows[0]["Url"].ToString();
                    string smsmsg = dt.Rows[0]["msg"].ToString();
                    if (dt.Rows[0][0].ToString() == "-1")
                    {
                       
                   
                    }
                    else if (dt.Rows[0][0].ToString() == "1")
                    {
                        try
                        {

                            string url = objU.smstemplate(dt.Rows[0]["msg"].ToString(), MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", "", "", "", "", "", "", "", "balance");
                            string Result = url.CallURL();
                            objU.Insert_SendSMS(MobileNo, Result, url);

                            //string output = Url.Split('{', '}')[1];
                            //string pass = Decrypt(output);
                            //int start = Url.IndexOf("{");
                            //int end = Url.IndexOf("}");
                            //string result1 = Url.Substring(start + 1, end - start - 1);
                            //Url = Url.Replace("{" + result1 + "}", "- " + pass);
                            //smsmsg = "- " + pass;
                        }
                        catch { }
                    }
                    else if (dt.Rows[0][0].ToString() == "4")
                    {
                        try
                        {

                            string url = objU.smstemplate(dt.Rows[0]["msg"].ToString(), MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", "", "", "", "", "", "", "", "password");
                            string Result = url.CallURL();
                            objU.Insert_SendSMS(MobileNo, Result, url);

                            //string output = Url.Split('{', '}')[1];
                            //string pass=Decrypt(output);
                            //int start = Url.IndexOf("{");
                            //int end = Url.IndexOf("}");
                            //string result1 = Url.Substring(start + 1, end - start - 1);
                            //Url = Url.Replace("{"+result1+"}", "- "+pass);
                            //smsmsg = "- " + pass;
                        }
                        catch { }
                    }
                    else if (dt.Rows[0][0].ToString() == "2")
                    {
                        string ToUserId = dt.Rows[0]["UUserid"].ToString();
                        string command = dt.Rows[0]["Command"].ToString();
                        string FundTransfer = dt.Rows[0]["FundTransfer"].ToString();
                        string FundReceive = dt.Rows[0]["FundReceive"].ToString();
                          string amt=Sms.Split(' ').Last();
                          string[] UserMobile = Sms.Split(' ');
                        //if (command.ToUpper() == "BT")
                        //{
                           
                        //    string mess = Osms.BTTranfer(ToUserId, amt, UserId, FundTransfer, FundReceive);
                        //    if (mess == "0")
                        //    {
                        //        DataSet dtMesg = Osms.ActionFromSms(ToUserId, UserId);
                        //        string SenderUrl = dt.Rows[0]["SenderUrl"].ToString();
                        //        FundReceive = Osms.FundMessage(dtMesg, amt.ToString(), "1", FundReceive, "0");
                        //        SenderUrl = SenderUrl.Replace("ttt", FundReceive);
                        //        SenderUrl = SenderUrl.Replace("mmm", UserMobile[2]);
                        //        objfund.Insert_sendsms(UserMobile[2], SenderUrl.CallURL(), ApiName, FundReceive, "Request Sent", TranId, VMN, TranId.ToInt());
                        //        FundTransfer = Osms.FundMessage(dtMesg, amt.ToString(), "1", FundTransfer, "1");
                        //        Url = Url.Replace("{Message}", FundTransfer);
                        //        smsmsg = FundTransfer;
                        //    }

                        //    else { Url = Url.Replace("{Message}", mess); smsmsg = mess; }
                        //}
                        //if (command.ToUpper() == "UT")
                        //{

                        //    string SenderUrl = dt.Rows[0]["SenderUrl"].ToString();
                        //    string mess = Osms.UTTranfer(ToUserId, amt, UserId, FundTransfer, FundReceive);
                        //    if (mess == "0")
                        //    {
                        //        DataSet dtMesg = Osms.ActionFromSms(ToUserId, UserId);
                        //        FundReceive = Osms.FundMessage(dtMesg, amt.ToString(), "2", FundReceive, "0");
                        //        SenderUrl = SenderUrl.Replace("ttt", FundReceive);
                        //        SenderUrl = SenderUrl.Replace("mmm", UserMobile[2]);
                        //        objfund.Insert_sendsms(UserMobile[2], SenderUrl.CallURL(), ApiName, FundReceive, "Request Sent", TranId, VMN, TranId.ToInt());
                        //        FundTransfer = Osms.FundMessage(dtMesg, amt.ToString(), "2", FundTransfer, "1");
                        //        Url = Url.Replace("{Message}", FundTransfer);
                        //        smsmsg = FundTransfer;
                        //    }
                        //    else { Url = Url.Replace("{Message}", mess);
                        //    smsmsg = mess;
                        //    }
                        //}
                    }
                    else  if (Sms.Any(char.IsDigit) == true)
                    {
                        decimal bal = 0; 
                        string[] smsDetail = Sms.Split(' ');
                        //string OId = dt.Rows[0]["OID"].ToString();
                        if (dt.Rows[0][0].ToString() == "3")
                        {
                            string OId = dt.Rows[0]["OID"].ToString();
                            string Optype = dt.Rows[0]["Optype"].ToString();
                            objrecharge.RechargeMobile = smsDetail[1];
                            objrecharge.RechargeAmount = Convert.ToDecimal(smsDetail[2]);
                            objrecharge.OperatorCode = OId;
                            objrecharge.Option1 = "";
                            objrecharge.Option2 = "";
                            objrecharge.Option3 = "";
                            objrecharge.Option4 = "";
                            objrecharge.DispalyValue1 = "";
                            objrecharge.DispalyValue2 = "";
                            objrecharge.DispalyValue3 = "";
                            objrecharge.DispalyValue4 = "";
                            objrecharge.RechargeType = Optype;
                            objrecharge.UserMobile = UserId;
                            objrecharge.IPAddress = GetIp();
                            objrecharge.EntryBy = UserId;
                            objrecharge.RechargeFrom = "SMS";
                            DataTable dtres = objrecharge.RechargeNew(objrecharge);
                           
                            //if (dtres.Rows[0][0].ToString() == "1")
                            //{
                            //    clsUser objU = new clsUser();
                            //   // Message.Show("Request Sent...!!!");
                            //    //TxtMobileNo.Text = "";
                            //    //TxtAmount.Text = "";
                            //    objU.Insert_SendSMS(MobileNo, smsmsg, Url);

                            //}
                            //else
                            //    if (dtres.Rows[0][0].ToString() == "-1")
                            //    {
                            //       // Message.Show(dtres.Rows[0][1].ToString());
                            //    }
                            //    else
                            //    {
                            //       // Message.Show("Unknown Error Occurred....!!!");
                            //    }
                           // Message.Show(dtres.Rows[0][0].ToString());
                            if (dtres.Rows.Count > 0)
                            {
                                if (dtres.Rows[0][0].ToString() == "-1")
                                {
                                    string url = objU.smstemplate(dt.Rows[0]["msg"].ToString(), MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", "", "", "", "", "", "", "", "error");
                                    string Result = url.CallURL();
                                    objU.Insert_SendSMS(MobileNo, Result, url);
                                }
                                else
                                {
                                    if (objrecharge.Status == "Pending")
                                    {
                                        string url = objU.smstemplate(UserId, MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objrecharge.RechargeMobile, objrecharge.RechargeAmount.ToString(), dtres.Rows[0]["Cuurentbal"].ToString(), "", dtres.Rows[0]["opname"].ToString(), dtres.Rows[0]["referenceid"].ToString(), "", "rechargeaccept");
                                        string Result = url.CallURL();
                                        objU.Insert_SendSMS(MobileNo, Result, url);
                                    }

                                }
                                //ApiName = dtrechage.Rows[0]["api"].ToString();
                                //string msg = dtres.Rows[0]["msg"].ToString();
                                ////   Url = dtrechage.Rows[0]["Url"].ToString();
                                //string msg1 = dtres.Rows[0]["msg"].ToString();
                                //Url = Url.Replace("{Message}", msg1);
                                //smsmsg = msg1;
                            }
                        }
                    }

                    //string Result = "";
                    //if (ApiName != "SRS")
                    //{
                    //    Result = Url.CallURL();
                    //}
                   // Uri myUri = new Uri(Url);
                    //var query = HttpUtility.ParseQueryString(myUri.Query);
                    //var message = query.Get("msg");
                    //string lastPart = HttpUtility.ParseQueryString(myUri.Query).Get("msg");
                   // string lastPart = Url.Split('=').Last();

                   // objfund.Insert_sendsms(MobileNo, Result, ApiName, smsmsg, "Request Sent", RechargeTransactionID, VMN, TranId.ToInt());
                   // objU.Insert_SendSMS(MobileNo, Result, Url);
                }
                   
                }
               
            
            catch
            {
               
            }
            finally
            {
                Response.Write("Final");
            }
        }
     
    }
    clsUser objU = new clsUser();
    public string GetIp()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    public DataTable ActionFromSms(string SMS, string MobileNo, string VMN)
    {
        Data ObjData = new Data();
        DataTable dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SMS", SMS);
            param[1] = new SqlParameter("@MobileNo", MobileNo);
            param[2] = new SqlParameter("@VMN", VMN);

            dt = ObjData.RunDataTableProcedure("SMSRecharge", param);

        }

        catch (Exception ex)
        {

        }
        ObjData.EndConnection();
        return dt;
    }
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
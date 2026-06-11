using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;
using System.Data.SqlClient;
using ARA_StringHunt;
using System.Net.Mime;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web;
namespace BusinessLogicTier
{
    public class clsLogin
    {
        Data ObjData = new Data();

        public string username { get; set; }
        public string password { get; set; }
        public string newpassword { get; set; }
        public string ipaddress { get; set; }
        public string OTP { get; set; }

        public DataTable Chk_AdminLoginDetails(clsLogin objlogin)
        {
            string str_query = "";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                str_query = "select * from LoginDetail  where username=@username and password=@password and role='Administrator' and status='1'  ";
                SqlParameter[] parameter = {   
                new SqlParameter("@username", objlogin.username),
                new SqlParameter("@password", objlogin.password)
                };

                dt = ObjData.RunDataTableParam(str_query, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable Chk_UserLoginDetails(clsLogin objlogin)
        {

            string str_query = "sp_UserLogin";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                SqlParameter[] parameter = {   
                new SqlParameter("@username", objlogin.username),
                new SqlParameter("@password", objlogin.password),
                new SqlParameter("@LoginIP", objlogin.ipaddress)
                };

                dt = ObjData.RunDataTableProcedure(str_query, parameter);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable Chk_UserLoginDetailstranspassword(clsLogin objlogin)
        {

            string str_query = "sp_UserLogintransaction";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                SqlParameter[] parameter = {   
                new SqlParameter("@username", objlogin.username),
                new SqlParameter("@password", objlogin.password),
                new SqlParameter("@LoginIP", objlogin.ipaddress)
                };

                dt = ObjData.RunDataTableProcedure(str_query, parameter);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable Chk_userDetails(string userid)
        {
            string str_query = "";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                str_query = "select * from UserDetail  where userid='" + userid + "' ";
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }



        public DataTable Chk_UserTempLoginDetails(clsLogin objlogin)
        {

            string str_query = "sp_TempUserLogin";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                SqlParameter[] parameter = {   
                new SqlParameter("@username", objlogin.username),
                new SqlParameter("@password", objlogin.password),
                new SqlParameter("@LoginIP", objlogin.ipaddress)
                };

                dt = ObjData.RunDataTableProcedure(str_query, parameter);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }


        public int ChangeAdminPassword(clsLogin objlogin)
        {
            int i = 0;
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                ds = ObjData.RunSelectQueryTrans("select * from LoginDetail where username='" + objlogin.username + "' and password ='" + objlogin.password + "' and  role='Administrator' ", tr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    s2 = " update LoginDetail set password='" + objlogin.newpassword + "' where username='" + objlogin.username + "' and password ='" + objlogin.password + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    i = 1;
                }
                else
                {
                    i = 2;
                }

                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }

        public int ChangeUserPassword(clsLogin objlogin)
        {
            int i = 0;
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                ds = ObjData.RunSelectQueryTrans("select * from LoginDetail where username='" + objlogin.username + "' and password ='" + objlogin.password + "' and  role='User' ", tr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    s2 = " update LoginDetail set password='" + objlogin.newpassword + "' where username='" + objlogin.username + "' and password ='" + objlogin.password + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    i = 1;
                }
                else
                {
                    i = 2;
                }

                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }
        public int ChangeUserTransactionPassword(clsLogin objlogin)
        {
            int i = 0;
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {

                ds = ObjData.RunSelectQueryTrans("select * from UserDetail where userid='" + objlogin.username + "' and transactionpassword ='" + objlogin.password + "' ", tr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    s2 = " update UserDetail set transactionpassword='" + objlogin.newpassword + "' where userid='" + objlogin.username + "' and transactionpassword ='" + objlogin.password + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    i = 1;
                }
                else
                {
                    i = 2;
                }

                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }
        Clsmail objmail = new Clsmail();

        public string SendPassword(clsUser objUser)
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
                s2 = "SELECT ud.userid,ud.mobile,ld.Password,ud.email,ud.UserName,ud.transactionpassword FROM UserDetail ud WITH  (nolock)  LEFT JOIN logindetail ld WITH (nolock) ON ud.UserId=ld.Username where ud.userid='" + objUser.UserId + "' ";
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    objUser.Email = dt.Rows[0]["email"].ToString();
                    string password = dt.Rows[0]["password"].ToString();
                    string email = dt.Rows[0]["email"].ToString();
                   string tpassword = dt.Rows[0]["transactionpassword"].ToString();
                    string Name = dt.Rows[0]["UserName"].ToString();
                    string UserId = dt.Rows[0]["userid"].ToString();
                    objUser.Mobile = dt.Rows[0]["mobile"].ToString();
                   // string url = "http://sms.sandhyasoftware.com/vendorsms/pushsms.aspx?user=sandhya&password=sandhya12&msisdn=" + objUser.Mobile + "&sid=SENDIT&msg=" + "Dear User your password is " + password + " and Transaction Password is " + tpassword + " for Login in http://flearma.com do not share to any one&fl=0&gwid=2";
                    string url = "http://login.fennec-media.com/domestic/sendsms/bulksms_v2.php?apikey=U0RJU1BSTFQ6bUhDenFkcGs=&type=TEXT&sender=SUCCSS&entityId=&templateId=&mobile=" + objUser.Mobile + "&message=" + "Dear User your password is " + password + " and Transaction Password is " + tpassword + " for Login in http://spineworld.io do not share to any one";
                    //string url = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=" + objUser.Mobile + "&dcs=0&msgtxt=" + "Dear User " + Name + " your password is " + password + "&state=4";
                    string Result = url.CallURL();
                   // Insert_SendSMS(objUser.Mobile, Result, url);
                    s2 = "insert into SendSms(CreateDate,Mobile,Result,Message)  values (getdate(),'" + objUser.Mobile + "','" + Result + "','" + url + "') ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);


                    MailMessage mail = new MailMessage();
                    //
                    string Subject2 = "Password";
                    string Body2 = "Dear User, <br> Your User Id:- " + UserId + "<br> Username:- " + Name + " <br> Password:- " + password + "<br> Thank you from SANDHYA INFO SOLUTIONS & team";
                    //


                    ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    string body = HttpUtility.HtmlDecode(Body2);
                    AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                    mail.AlternateViews.Add(alternate);

                    mail.To.Add(email);
                    mail.From = new MailAddress("info@sandhyasoftware.com");
                    mail.Subject = Subject2;
                    mail.Body = Body2;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("info@sandhyasoftware.com", "mwapztpaipcolttw");


                    try
                    {
                        smtp.Send(mail);
                        res = "1";
                    }
                    catch (Exception ex)
                    {
                        res = "0";
                    }


                }
                else
                {
                    res = "f";
                }
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

        public string Sendotp(string OTP,string MObile)
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

                //string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + MObile + "&sender=ETOPUP&smstext=" + "Dear User your OTP is " + OTP + " for Login in dgtalshop.com Please Do not share to  any one &smsformat=TEXT&format=json";
                //string Result = url.CallURL();
                //Insert_SendSMS(MObile, Result, url);
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

        public string SendSms(string message, string MObile)
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

                //string url = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=" + MObile + "&dcs=0&msgtxt=" + message  + "&state=4";
                //    string Result = url.CallURL();
                //    Insert_SendSMS(MObile, Result, url);
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


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string Confirmotp(clsLogin objL)
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

                
                string Query = "select * from rnd_Logins where LoginID='"+objL.username+"'  and LoginIP='"+objL.ipaddress+"' and OTP='"+objL.OTP+"' and IsLogin=0";
                dt = ObjData.RunSelectQueryTTrans(Query, tr);
                if (dt.Rows.Count > 0)
                {

                    Query = "update rnd_Logins set IsLogin=1 where LoginID='" + objL.username + "'   and LoginIP='" + objL.ipaddress + "' and OTP='" + objL.OTP + "' and IsLogin=0";
                   ObjData.RunInsUpDelQueryTrans(Query, tr);
                   tr.Commit();
                   res = "1";

                }
                else
                {
                    res = "0";
                }               

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
        public string SendotpMobileVerification(string OTP, string MObile)
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
                string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + MObile + "&sender=ETOPUP&smstext=" + "Dear User your OTP is " + OTP + " for Mobile Verfication in dgtalshop.com Please Do not share to  any one &smsformat=TEXT&format=json";
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
        public string SendsmsWalletTransfer(string mssg, string MObile)
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
                string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + MObile + "&sender=ETOPUP&smstext=" + mssg + "&smsformat=TEXT&format=json";
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
        public string SendSmsregistrationMssg(string Message, string MObile)
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
                string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + MObile + "&sender=ETOPUP&smstext=" + Message + "&smsformat=TEXT&format=json";
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

    }
}

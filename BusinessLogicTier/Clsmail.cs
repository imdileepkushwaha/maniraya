using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;

using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicTier
{
    public class Clsmail
    {

        public void sendmail(string UserName, string Password, string UserId, string tomail)
        {
            string body = this.createEmailBody(UserName, Password, UserId);

            this.SendHtmlFormattedEmail("Universal Marketing !", body, tomail);
        }
        public void sendRegistrationEmail(string UserName, string password, string UserId, string tomail)
        {
            string body = "Your UserId is " + UserId + " and Password is " + password + ".";
            this.Mail(tomail, body, UserId, UserName);
        }
        private string createEmailBody(string userName, string Password, string UserId)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Sendmail.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{UserName}", userName); //replacing the required things  

            body = body.Replace("{pwd}", Password);

            body = body.Replace("{UserID}", UserId);

            return body;

        }

        public void sendOTPmail(string UserName, string OTP, string UserId, string tomail)
        {
            string body = this.createOTPEmailBody(UserName, OTP, UserId);

            this.SendHtmlFormattedEmail("Universal Marketing !", body, tomail);
        }

        private string createOTPEmailBody(string userName, string otp, string UserId)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/SendOTPmail.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{UserName}", userName); //replacing the required things  

            body = body.Replace("{OTP}", otp);

            body = body.Replace("{UserID}", UserId);

            return body;

        }



        private void SendHtmlFormattedEmail(string subject, string body, string tomail)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
              
                mailMessage.From = new MailAddress("Universal.MCB@gmail.com");

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(tomail));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = Convert.ToBoolean(true);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = "Universal.MCB@gmail.com"; //reading from web.config  

                NetworkCred.Password = "test_123"; //reading from web.config  

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = int.Parse("587"); //reading from web.config  

                smtp.Send(mailMessage);

            }

        }
        public void sendmailContactus(string UserName, string EmailId, string MobileId, string msg)
        {
            string body = this.createEmailBodycontactus(UserName, EmailId, MobileId, msg);

            //this.SendHtmlFormattedEmailcontact("Contact Us !", body);
        }
        private string createEmailBodycontactus(string UserName, string EmailId, string MobileId, string msg)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/ContactUsmail.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{UserName}", UserName); //replacing the required things  

            body = body.Replace("{UserEmailId}", EmailId);

            body = body.Replace("{UserPhoneNumber}", MobileId);

            body = body.Replace("{UserMessage}", msg);

            return body;

        }
        private void SendHtmlFormattedEmailcontact(string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                
                mailMessage.From = new MailAddress("Universal.MCB@gmail.com");

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

               mailMessage.To.Add(new MailAddress("brshaker2@gmail.com"));
               //mailMessage.To.Add(new MailAddress("srivastavadevesh05@gmail.com"));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = Convert.ToBoolean(true);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = "Universal.MCB@gmail.com"; //reading from web.config  

                NetworkCred.Password = "test_123"; //reading from web.config  

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = int.Parse("587"); //reading from web.config  

                smtp.Send(mailMessage);

            }

        }
        public static string HTMLTemplate1(string UserId, string sendmessage, string footerMessage, string website, string Name)
        {
            IHtmlString message = new HtmlString("<div style='width: 580px; margin: 0 auto;border: 3px solid rgba(10, 6, 2, 0.69); color: grey; font-size: 120%'><div  style='background-color:deepskyblue'><br> <img style='width:50%'  src='http://" + website + "/userimages/" + UserId + "/logo.png' /></div><div style='padding: 18px;'><div><br /></div><div style'color:#0094ff;padding:10px'>Dear " + Name + "</div><br><div style='font-size:14px'>" + sendmessage + " </div></div><div><br /><br /></div><div  style='background-color:chocolate;padding:8px;color:white;text-align:center'>" + footerMessage + "</div></div>");
            //  IHtmlString message = new HtmlString("<div style='width: 580px; margin: 0 auto;border: 10px solid #f0f1f2; color: grey; font-size: 120%'><div style='padding: 18px;'><div class='adM'></div><p align='center'><img src='http://" + website + "/userimages/" + UserId + "/logo.png' height='50px' class='CToWUd'/></p>" + sendmessage + " </div><div  style='background-color:darkgray;padding:8px;color:white;text-align:center'>" + footerMessage + "</div></div>");
            string target = message.ToString().Replace("<[^>]*>", "");
            return target;
        }

        public void sendOTPmailVerfication(string UserName, string OTP, string UserId, string tomail)
        {
            string body = "Your Email Verfication Code is " + OTP + " for UserId : " + UserId + ". Please do not share it anyone.";
            this.Mail(tomail, body, UserId, UserName);
        }

        public string Mail(String to_Address, String Body, string LoginId, string Name)
        {
            string Result = "";
            try
            {
                /*string FromEmail = "help@dgtalshop.com";
                string Password = "Digital@2018";
                string HostName = "mail.dgtalshop.com";

                //string FromEmail = "admin@dgtalshop.com";
                //string Password = "Master@147#";
                ////string HostName = "https://mailadmin.zoho.com/";
                //string HostName = "smtp.zoho.com";

                int Port = 25;
                //int Port = 465;
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient(HostName);
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(to_Address);
                mail.Subject = "One Time Password";
                mail.Body = HTMLTemplate1(LoginId, Body, mail.Subject, "DGTALSHOP.COM", Name);
                mail.IsBodyHtml = true;
                SmtpServer.Port = Port;
                //SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
                SmtpServer.EnableSsl = Convert.ToBoolean(false);
                SmtpServer.Send(mail);*/
                Result = "Mail Send";

            }
            catch (Exception ex)
            {
                //Result = ex.Message;

            }
            return Result;
        }
      
    }
}

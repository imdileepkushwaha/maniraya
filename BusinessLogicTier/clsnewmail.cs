using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;

namespace BusinessLogicTier
{
    public class clsnewmail
    {
        public void SendMail(string strToEmailId, string strToName, string strFromEmailId, string strFromEmailIdPassword, string strFromName, string strSubject, string strBody)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = "support@dgtalshop.com";
                mm.To = strToEmailId;
                mm.Subject = "One Time Password";
                string emsg = strBody;
                mm.Body = emsg;
                mm.Priority = MailPriority.High;
                mm.BodyFormat = MailFormat.Text;
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = "mail.dgtalshop.com";
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"] = "false";
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = "support@dgtalshop.com";
                mm.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = "dgtalshop@123#";
                SmtpMail.SmtpServer = "mail.dgtalshop.com";
                SmtpMail.Send(mm);
            }
            catch (Exception ex) { }

        }


        public void MailData(string OTPPass, string EmailID)
        {
            try
            {
                string Message = "One Time Password" + " " + OTPPass;
                string strBody = "";
                strBody = Message;
                SendMail(EmailID, "admin", "support@dgtalshop.com", "dgtalshop@123#", "One Time Password", "Info", strBody);
                OTPPass = "";
            }
            catch (Exception ex)
            { }
        }
    }

}

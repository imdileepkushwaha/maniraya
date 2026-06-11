using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;

namespace BusinessLogicTier
{
    public class clsVerfification
    {


        public DataTable getStatus(string UserId)
        {
            Data dl = new Data();
            DataTable dt;
            try
            {
                dl.StartConnection();
                //string str = " SELECT OTPMobile, OTPEmail, isnull(IsMobileVerified,0) AS MobStatus,isnull(IsEmailVerified,0) AS EmailStatus,Email,Mobile,UserName FROM UserDetail WHERE UserId='" + UserId + "'  ";
                string str = " SELECT OTPMobile, OTPEmail, isnull(IsMobileVerified,0) AS MobStatus,isnull(IsEmailVerified,0) AS EmailStatus,Email,Mobile,UserName FROM TempUserDetail WHERE UserId='" + UserId + "'  ";
                dt = dl.RunDataTable(str);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int mobstatus = Convert.ToBoolean(dt.Rows[0]["MobStatus"].ToString()) ? 1 : 0;
                    int emailstatus = Convert.ToBoolean(dt.Rows[0]["EmailStatus"].ToString()) ? 1 : 0;
                    if (mobstatus == 1 && emailstatus == 1)
                    {
                        str = " select * from userdetail where UserId='" + UserId + "'";
                        DataTable dt2 = dl.RunDataTable(str);
                        if (dt2 == null || dt2.Rows.Count == 0)
                        {
                            str = " Insert into UserDetail SELECT id, UserId, SponserId, UserName, DateofBirth, Gender, Email, Mobile, NomineeName, NomineeRelation, Address, CityId, AreaName, Pincode, AccountHolderName, AccountNo, IFSCCode, BankName, BranchName, PanNumber, RegDate, EPinNo, ParentUserId, StandingPosition, DeleteStatus, ActiveStatus, MentionBy, MentionDate, BalanceAmount, TransactionId, regtype, Status, AadharNo, AadharImage, AadharImgStatus, PanImage, PanImgStatus, CancelCheque, ChequeImgStatus, SignUpFormImage, SignUpImgStatus, PhotoImage, AadharImageBack, SlabID, DirectLeft, DirectRight, cappingamount, OtherCity, InvoiceStatus, UtilityBalance, ActivateDate, epinGenerationStatus, IsGstApplicable, IsTdsApplicable, IsStApplicable, IsGSTDeductedOfUnverified, GSTNUMBER, GSTimage, IsMobileVerified, IsEmailVerified, OTPMobile, OTPEmail, VerifyDate, IsEditable, IsBankEditable, IsPhotoEditable, IsPanCardEditable, IsAddressProofEditable, IsSignUpFomrEditable, IsChequePassbookEditabled,TransactionPassword FROM dbo.TempUserDetail where UserId='" + UserId + "' ";
                            dl.RunInsUpDelQueryNew(str);

                            str = " SELECT * FROM dbo.UserRegistrationSmsEmailStatus where userid='" + UserId + "' ";
                            DataTable dtsms = dl.RunDataTable(str);
                            if (dtsms == null || dtsms.Rows.Count == 0)
                            {
                                DataTable dtteemp = dl.RunDataTable("select * from dbo.TempUserDetail where UserId='" + UserId + "'");
                                if (dtteemp != null && dtteemp.Rows.Count > 0)
                                {
                                    DataTable dtlogin = dl.RunDataTable("select * from dbo.LoginDetail where UserName='" + UserId + "'");

                                    string Password = dtlogin.Rows[0]["Password"].ToString();
                                    string mob = dtteemp.Rows[0]["Mobile"].ToString();
                                    string name = dtteemp.Rows[0]["UserName"].ToString();
                                    string Email = dtteemp.Rows[0]["Email"].ToString();

                                    //send sms
                                   
                                    string smsBody = "You are Registred successfully in dgtalshop .\n Your User Id: " + UserId + " and Password: " + Password + "\nLogin here http://dgtalshop.com";
                                    clsLogin objlogin = new clsLogin();
                                    objlogin.SendSmsregistrationMssg(smsBody, mob);


                                    Clsmail obm = new Clsmail();
                                    obm.sendRegistrationEmail(name, Password, UserId, Email);

                                    str = " INSERT INTO dbo.UserRegistrationSmsEmailStatus (userid,EntryDate) VALUES ('" + UserId + "','" + DateTime.Now + "'); ";
                                    dl.RunInsUpDelQuery(str);
                                }

                            }

                        }
                    }
                }

            }
            catch (Exception er)
            {
                dt = null;
            }
            finally
            {
                dl.EndConnection();
            }
            return dt;
        }


        public int UpdateOtp(string UserId,string Otp,string Type)
        {
            Data dl = new Data();
            int c = 0;
            try
            {
                dl.StartConnection();
                
                if (Type == "Mobile")
                {
                    //string str = " Update UserDetail set OTPMobile='" + Otp + "' where UserId='" + UserId + "' ";
                    string str = " Update TempUserDetail set OTPMobile='" + Otp + "' where UserId='" + UserId + "' ";
                    dl.RunInsUpDelQuery(str);
                    c = 1;
                }
                else
                    if (Type == "Email")
                    {
                        //string str = " Update UserDetail set OTPEmail='" + Otp + "' where UserId='" + UserId + "' ";
                        string str = " Update TempUserDetail set OTPEmail='" + Otp + "' where UserId='" + UserId + "' ";
                        dl.RunInsUpDelQuery(str);
                        c = 1;
                    }
                    else
                    {
                        c = -1;
                    }
                
            }
            catch (Exception er)
            {
                c = -1;
            }
            finally
            {
                dl.EndConnection();
            }
            return c;
        }

        public void SendSmsEmail(string mob,string Email,string name,string UserId,string Password)
        {
            Data dl = new Data();
           
            try
            {
                dl.StartConnection();
                string str = " SELECT * FROM dbo.UserRegistrationSmsEmailStatus where userid='" + UserId + "' ";
                DataTable dt = dl.RunDataTable(str);
                if(dt == null || dt.Rows.Count == 0)
                {
                    
                    //send sms
                    string smsBody = "You are Registred successfully in dgtalshop .\n Your User Id: " + UserId + " and Password: " + Password + "\nLogin here http://dgtalshop.com";
                    clsLogin objlogin = new clsLogin();
                    objlogin.SendSmsregistrationMssg(smsBody, mob);

                   
                    Clsmail obm = new Clsmail();
                    obm.sendRegistrationEmail(name, Password, UserId, Email);

                    str = " INSERT INTO dbo.UserRegistrationSmsEmailStatus (userid,EntryDate) VALUES ('" + UserId + "','"+DateTime.Now+"'); ";
                    dl.RunInsUpDelQuery(str);

                }
                
            }
            catch (Exception er)
            {
               
            }
            finally
            {
                dl.EndConnection();
            }
          
        }

        


        public int Verify(string UserId,string sotp,string type)
        {
            Data dl = new Data();
            int res = 0;
            try
            {
                dl.StartConnection();
                //string str = " SELECT OTPMobile, OTPEmail, isnull(IsMobileVerified,0) AS MobStatus,isnull(IsEmailVerified,0) AS EmailStatus FROM UserDetail WHERE UserId='" + UserId + "'  ";
                string str = " SELECT OTPMobile, OTPEmail, isnull(IsMobileVerified,0) AS MobStatus,isnull(IsEmailVerified,0) AS EmailStatus FROM TempUserDetail WHERE UserId='" + UserId + "'  ";
                DataTable dt = dl.RunDataTable(str);
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (type == "Mobile")
                    {
                        int mobstatus = Convert.ToBoolean(dt.Rows[0]["MobStatus"].ToString()) ? 1 : 0;
                        if (mobstatus == 0)
                        {
                            string otp = dt.Rows[0]["OTPMobile"].ToString();
                            if (sotp == otp)
                            {
                                //str = " Update UserDetail set IsMobileVerified=1,VerifyDate=getdate() where UserId='" + UserId + "' and OTPMobile='" + sotp + "' and IsMobileVerified=0 ";
                                str = " Update TempUserDetail set IsMobileVerified=1,VerifyDate=getdate() where UserId='" + UserId + "' and OTPMobile='" + sotp + "' and IsMobileVerified=0 ";
                                dl.RunInsUpDelQuery(str);
                                res = 1;
                            }
                            else
                            {
                                res = 2;
                            }
                        }
                        else
                        {
                            res = 1;
                        }
                    }
                    else
                        if (type == "Email")
                        {
                            int emailstatus = Convert.ToBoolean(dt.Rows[0]["EmailStatus"].ToString()) ? 1 : 0;
                            if (emailstatus == 0)
                            {
                                string otp = dt.Rows[0]["OTPEmail"].ToString();
                                if (sotp == otp)
                                {
                                    //str = " Update UserDetail set IsEmailVerified=1,VerifyDate=getdate() where UserId='" + UserId + "' and OTPEmail='" + sotp + "' and IsEmailVerified=0 ";
                                    str = " Update TempUserDetail set IsEmailVerified=1,VerifyDate=getdate() where UserId='" + UserId + "' and OTPEmail='" + sotp + "' and IsEmailVerified=0 ";
                                    dl.RunInsUpDelQuery(str);
                                    res = 1;
                                }
                                else
                                {
                                    res = 2;
                                }
                            }
                            else
                            {
                                res = 1;
                            }
                        }
                        else
                        {
                            res = -1;
                        }
                }
                else
                {
                    res = -1;
                }
            }
            catch (Exception er)
            {
                res = -1;
            }
            finally
            {
                dl.EndConnection();
            }
            return res;
        }


        public DataTable getProfileEditableStatus(string UserId)
        {
            Data dl = new Data();
            DataTable dt;
            try
            {
                dl.StartConnection();
                string str = " SELECT isnull(IsEditable,1) as IsEditable,isnull(IsBankEditable,1) as IsBankEditable,isnull(IsPhotoEditable,1) as IsPhotoEditable, isnull(IsPanCardEditable,1) as IsPanCardEditable,isnull(IsAddressProofEditable,1) as IsAddressProofEditable,isnull(IsSignUpFomrEditable,1) as IsSignUpFomrEditable,isnull(IsChequePassbookEditabled,1) as IsChequePassbookEditabled  FROM UserDetail WHERE UserId='" + UserId + "'  ";
                dt = dl.RunDataTable(str);
            }
            catch (Exception er)
            {
                dt = null;
            }
            finally
            {
                dl.EndConnection();
            }
            return dt;
        }

    }
}

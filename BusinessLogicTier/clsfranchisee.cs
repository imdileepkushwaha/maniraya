using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARA_StringHunt;

namespace BusinessLogicTier
{
    public class clsfranchisee
    {
        Data ObjData = new Data();
        public string username { get; set; }
        public string password { get; set; }
        public string newpassword { get; set; }

        public string UserId { get; set; }
        public string SponserId { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthnew { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Password { get; set; }
        public string MentionBy { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string CityId { get; set; }
        public string EpinNo { get; set; }
        public string StandingPosition { get; set; }
        public string AreaName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string TransferUserId { get; set; }
        public int NoOfEpin { get; set; }
        public string ParentUserId { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string AccHolderName { get; set; }
        public string AccNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string PanCardNo { get; set; }
        public decimal pinamount { get; set; }
        public string RegType { get; set; }
        public string Photo { get; set; }
        public string Signupform { get; set; }
        public string PanImage { get; set; }
        public string CancelCheck { get; set; }
        public string Addressproof { get; set; }
        public string AddressproofBack { get; set; }
        public string AdhaarNo { get; set; }
        public string OtherCity { get; set; }
        public string ipaddress { get; set; }
        public string OTP { get; set; }
        public string TehsilId { get; set; }
        public string MarketId { get; set; }
        public string Remark
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public string CallResultStatus
        {
            get;
            set;
        }

        public string PickedBy
        {
            get;
            set;
        }
        public string CallStatus
        {
            get;
            set;
        }


        public string outletName { get; set; }
        public string gstNo { get; set; }
        public string gstImg { get; set; }

        public string queryType { get; set; }
        public int requestId { get; set; }
        public string prevMobileNo { get; set; }
        public string requestStatus { get; set; }

        public DataTable getuserdetailviaprocedure(clsfranchisee objUser)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_getfranchiseedetail";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId), 
                   
                 
                  
                };
                Dt = ObjData.RunDataTableProcedure(s2, parameter);



            }
            catch (Exception ex)
            {

            }
            finally
            {
                ObjData.EndConnection();

            }
            return Dt;
        }

        public string Updatefranchiseecommission(string Id, string Type, string profit)
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
                s2 = "update franchiseeTypeTb set type='" + Type + "',profit='" + profit + "' where Id='" + Id + "'";
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
        public DataTable Chk_UserLoginDetails(clsfranchisee objlogin)
        {

            string str_query = "sp_UserLoginfranchisee";
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
        public string SendPassword(clsfranchisee objUser)
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
                s2 = "SELECT ud.userid,ud.mobile,ld.Password,ud.email,ud.UserName FROM FranchiseeDetail ud WITH  (nolock)  LEFT JOIN logindetail ld WITH (nolock) ON ud.UserId=ld.Username where ud.userid='" + objUser.UserId + "' ";
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    objUser.Email = dt.Rows[0]["email"].ToString();
                    string password = dt.Rows[0]["password"].ToString();
                    string Name = dt.Rows[0]["UserName"].ToString();
                    string UserId = dt.Rows[0]["userid"].ToString();
                    objUser.Mobile = dt.Rows[0]["mobile"].ToString();
                    string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + objUser.Mobile + "&sender=ETOPUP&smstext=" + "Dear User your password is " + password + "&smsformat=TEXT&format=json";
                    //string url = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=" + objUser.Mobile + "&dcs=0&msgtxt=" + "Dear User " + Name + " your password is " + password + "&state=4";
                    string Result = url.CallURL();
                    Insert_SendSMS(objUser.Mobile, Result, url);
                   // objmail.sendmail(Name, password, UserId, objUser.Email);
                    res = objUser.Mobile;
                }
                else
                {
                    res = "f";
                } tr.Commit();
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

                string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + MObile + "&sender=ETOPUP&smstext=" + "Dear User your OTP is " + OTP + " for login Dgtalshop.com &smsformat=TEXT&format=json";
                //string url = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=" + MObile + "&dcs=0&msgtxt=" + "Dear User your one time password is " + OTP + " for login arsenpay.in &state=4";
                string Result = url.CallURL();
                Insert_SendSMS(MObile, Result, url);
                // objmail.sendmail(Name, password, UserId, objUser.Email);
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
        public string Confirmotp(clsfranchisee objL)
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


                string Query = "select * from rnd_Logins where LoginID='" + objL.username + "'  and LoginIP='" + objL.ipaddress + "' and OTP='" + objL.OTP + "' and IsLogin=0";
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

public DataTable getUserReportnew(clsfranchisee objUser)
        {
            string str_query = @"SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,ld.status as activeStatus,ft.type, 
                                (case when SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when SignUpImgStatus=0 then 'Pending' when SignUpImgStatus=1 then 'Approved' when SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss,  
                                (case when PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when PanImgStatus=0 then 'Pending' when PanImgStatus=1 then 'Approved' when PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ChequeImgStatus=0 then 'Pending' when ChequeImgStatus=1 then 'Approved' when ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss,
                                (case when AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when AadharImgStatus=0 then 'Pending' when AadharImgStatus=1 then 'Approved' when AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss, 
                                ud.epinGenerationStatus FROM FranchiseeDetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId  LEFT JOIN franchiseeTypeTb ft ON ud.franchiseetype=ft.id
                                left join Logindetail ld on ud.userid=ld.username where 1=1 ";

            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  >= '" + objUser.FromDate + "'   and ud.MentionDate   <= '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and ud.UserId = '" + objUser.UserId + "' ";
            }
            //if (objUser.Mobile != "")
            if (!string.IsNullOrEmpty(objUser.Mobile))
            {
                str_query += "  and ud.mobile = '" + objUser.Mobile + "' ";
            }
            //if (objUser.Email != "")
            if (!string.IsNullOrEmpty(objUser.Email))
            {
                str_query += "  and ud.email = '" + objUser.Email + "' ";
            }
            if (objUser.CityId != "0" && !string.IsNullOrEmpty(objUser.CityId))
            {
                str_query += "  and ud.cityid = '" + objUser.CityId + "' ";
            }
            if (objUser.TehsilId != "0" && !string.IsNullOrEmpty(objUser.TehsilId))
            {
                str_query += "  and ud.tehsilid = '" + objUser.TehsilId + "' ";
            }

            str_query += " order by ud.MentionDate  desc";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }


public DataTable getFranchiseetype()
        {
            string str_query = "select * from franchiseeTypeTb  order by id";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getUserDetail(clsfranchisee objUser)
        {
            string str_query = "SELECT ud.*,cm.stateid,sm.countryid,lg.Password, CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage FROM FranchiseeDetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid LEFT JOIN logindetail lg ON lg.Username=ud.UserId where ud.UserId= '" + objUser.UserId + "' ";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public int ChangeUserPassword(clsfranchisee objlogin)
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

                ds = ObjData.RunSelectQueryTrans("select * from LoginDetail where username='" + objlogin.username + "' and password ='" + objlogin.password + "' and  role='Franchisee' ", tr);
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
        public string Update_UserProfile(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set tehsilid='" + objUser.TehsilId + "', marketid='" + objUser.MarketId + "',username='" + objUser.UserName + "', email='" + objUser.Email + "',dateofbirth='" + objUser.DateOfBirth.ToString("yyyy/MM/dd") + "',gender='" + objUser.Gender + "' ,mobile='" + objUser.Mobile + "', address='" + objUser.Address + "', cityid='" + objUser.CityId + "',areaName='" + objUser.AreaName + "' ,pincode='" + objUser.Pincode + "',AccountHolderName='" + objUser.AccHolderName + "',AccountNo='" + objUser.AccNo + "',IFSCCode='" + objUser.IFSCCode + "',BankName='" + objUser.BankName + "',BranchName='" + objUser.BranchName + "',PanNumber='" + objUser.PanCardNo + "',NomineeName='" + objUser.NomineeName + "',NomineeRelation='" + objUser.NomineeRelation + "'  where UserId='" + objUser.UserId + "'   ";
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
        public DataTable getUserName(clsfranchisee objUser)
        {
            string str_query = @"SELECT ud.userid,ud.username,ud.mobile,ud.Address,convert(nvarchar(50),ud.DateofBirth,106) as DateofBirth, 
                                convert(nvarchar(50),ud.RegDate,106) as DOJ,case when ActiveStatus=1 then 'Active' else 'Deactive' end as ActiveStatus, 
                                case when Status=1 then 'Active' else 'Deactive' end as Status,ud.PanNumber, 
                                CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS Photo, 
                                CASE WHEN isnull(ud.SignUpFormImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.SignUpFormImage END AS SignUpForm, SignUpImgStatus, 
                                CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImage  , PanImgStatus, 
                                CASE WHEN isnull(ud.CancelCheque,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.CancelCheque END AS CancelCheque, ChequeImgStatus, 
                                CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImage, AadharImgStatus, 
                                CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImageBack,ud.gender   
                                FROM FranchiseeDetail  ud where ud.UserId = '" + objUser.UserId + "' ";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public string Update_UserPhoto(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set PhotoImage='" + objUser.Photo + "'  where UserId='" + objUser.UserId + "'   ";
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
        public string Update_UserSignupForm(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set SignUpFormImage='" + objUser.Signupform + "',SignUpImgStatus=0  where UserId='" + objUser.UserId + "'   ";
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
        public string Update_UserPanForm(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set PanNumber='" + objUser.PanCardNo + "', PanImage='" + objUser.PanImage + "',PanImgStatus=0  where UserId='" + objUser.UserId + "'   ";
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
        public string Update_UserCancelCheque(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set CancelCheque='" + objUser.CancelCheck + "',ChequeImgStatus=0  where UserId='" + objUser.UserId + "'   ";
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
        public string Update_AddressProof(clsfranchisee objUser)
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
                s2 = "update FranchiseeDetail  set AadharImage='" + objUser.Addressproof + "',AadharImageBack='" + objUser.AddressproofBack + "',AadharNo='" + objUser.AdhaarNo + "',AadharImgStatus=0  where UserId='" + objUser.UserId + "'   ";
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
        public string Insert_User(clsfranchisee objUser)
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
                s2 = "sp_add_franchiseeDetail";
                SqlParameter[] parameter = {              
                    new SqlParameter("@SponserId",objUser.SponserId), 
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


                      new SqlParameter("@outletName",objUser.outletName),
                      new SqlParameter("@panNumber",objUser.PanCardNo),
                      new SqlParameter("@panImg",objUser.PanImage),
                      new SqlParameter("@GST_Number",objUser.gstNo),
                      new SqlParameter("@GST_Img",objUser.gstImg),
                       new SqlParameter("@TehsilId",objUser.TehsilId),
                      new SqlParameter("@MarketId",objUser.MarketId)
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                //string url = string.Concat(new string[]
                //{
                //    "http://www.apihub.online/api/Services/transact?token=7fb12b368788c29c84407591b88e036a&skey=SST&to=",
                //    objUser.Mobile,
                //    "&sender=ETOPUP&smstext=Dear User you are successfully registered on raxtan.com Your login details are-userid:",
                //    res,
                //    ", password:",
                //    objUser.Password,
                //    "&smsformat=TEXT&format=json"
                //});
                //string Result = url.CallURL();
                //Insert_SendSMS(objUser.Mobile, Result, url);
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
        public DataTable getUserReport(clsfranchisee objUser)
        {
            string str_query = @"SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,ld.status as activeStatus, 
                                (case when SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when SignUpImgStatus=0 then 'Pending' when SignUpImgStatus=1 then 'Approved' when SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss,  
                                (case when PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when PanImgStatus=0 then 'Pending' when PanImgStatus=1 then 'Approved' when PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ChequeImgStatus=0 then 'Pending' when ChequeImgStatus=1 then 'Approved' when ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss,
                                (case when AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when AadharImgStatus=0 then 'Pending' when AadharImgStatus=1 then 'Approved' when AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss, 
                                ud.epinGenerationStatus FROM FranchiseeDetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId 
                                left join Logindetail ld on ud.userid=ld.username where 1=1 ";
            
            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  >= '" + objUser.FromDate + "'   and ud.MentionDate   <= '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and ud.UserId = '" + objUser.UserId + "' ";
            }
            //if (objUser.Mobile != "")
            if (!string.IsNullOrEmpty(objUser.Mobile))
            {
                str_query += "  and ud.mobile = '" + objUser.Mobile + "' ";
            }
            //if (objUser.Email != "")
            if (!string.IsNullOrEmpty(objUser.Email))
            {
                str_query += "  and ud.email = '" + objUser.Email + "' ";
            }
            if (objUser.CityId != "0" && !string.IsNullOrEmpty(objUser.CityId))
            {
                str_query += "  and ud.cityid = '" + objUser.CityId + "' ";
            }
            if (objUser.TehsilId != "0" && !string.IsNullOrEmpty(objUser.TehsilId))
            {
                str_query += "  and ud.tehsilid = '" + objUser.TehsilId + "' ";
            }

            str_query += " order by ud.MentionDate  desc";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public int changeFranchiseeStatus(clsfranchisee objUser)
        {
            string sql = @"update Logindetail set status=(case status when 0 then 1 when 1 then 0 end) where Username='" + objUser.UserId + "'";

            ObjData.StartConnection();
            try
            {
                return ObjData.RunInsUpDelQueryNew(sql);
            }
            catch
            {
                throw;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }

        public int changeFranchiseeEpinStatus(clsfranchisee objUser)
        {
            string sql = @"update FranchiseeDetail set epinGenerationStatus=(case epinGenerationStatus when 0 then 1 when 1 then 0 end) where UserId='" + objUser.UserId + "'";

            ObjData.StartConnection();
            try
            {
                return ObjData.RunInsUpDelQueryNew(sql);
            }
            catch
            {
                throw;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }

        public DataTable I_U_S_D_MobileChangeRequest(clsfranchisee objUser)
        {
            string sql = "";

            sql = "I_U_S_D_MobileChangeRequest";
            SqlParameter[] parameter = {              
                    new SqlParameter("@queryType",objUser.queryType),
                    new SqlParameter("@request_Id",objUser.requestId),
                    new SqlParameter("@userType",objUser.RegType),
                    new SqlParameter("@user_Id",objUser.UserId),
                    new SqlParameter("@previousNo",objUser.prevMobileNo),
                    new SqlParameter("@newMobileNo",objUser.Mobile),
                    new SqlParameter("@requestStatus",objUser.requestStatus),
                    new SqlParameter("@createdBy",objUser.MentionBy)                  
                };

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTableProcedure(sql, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public int changeKYCStatus(string kycType, string kycStatus, string userId)
        {

            string sql = @" update franchiseedetail ";

            if (kycStatus == "Approve")
            {
                if (kycType == "Sign Up")
                    sql += " set SignUpImgStatus=1 ";

                else if (kycType == "PAN")
                    sql += " set PanImgStatus=1 ";

                else if (kycType == "Cheque")
                    sql += " set ChequeImgStatus=1 ";

                else if (kycType == "Aadhaar")
                    sql += " set AadharImgStatus=1 ";
            }
            else if (kycStatus == "Reject")
            {
                if (kycType == "Sign Up")
                    sql += " set SignUpImgStatus=2 ";

                else if (kycType == "PAN")
                    sql += " set PanImgStatus=2 ";

                else if (kycType == "Cheque")
                    sql += " set ChequeImgStatus=2 ";

                else if (kycType == "Aadhaar")
                    sql += " set AadharImgStatus=2 ";
            }

            sql += " where userid='" + userId + "'";

            ObjData.StartConnection();
            try
            {
                return ObjData.RunInsUpDelQueryNew(sql);
            }
            catch
            {
                throw;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }


    }
}

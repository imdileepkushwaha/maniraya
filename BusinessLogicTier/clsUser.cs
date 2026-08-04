using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;
using System.Data.SqlClient;
using ARA_StringHunt;

namespace BusinessLogicTier
{
    public class clsUser
    {
        clsSetting objsetting = new clsSetting();
        clsAPI objapisms = new clsAPI();
        Data ObjData = new Data();
        public string UserId { get; set; }
        public string Height { get; set; }
        public string SponserId { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime NomDateOfBirth { get; set; }

        public string DateOfBirthnew { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public string AddressSec { get; set; }
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
        public string OTPMobile { get; set; }
        public string OTPEmail { get; set; }

        public string phonepay { get; set; }
        public string UPINo { get; set; }
        public string Bhimno { get; set; }
        public string EditStatus { get; set; } 

        public string UpdateType { get; set; }
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

        public string queryType { get; set; }
        public int requestId { get; set; }
        public string prevMobileNo { get; set; }
        public string requestStatus { get; set; }
        public decimal chargeAmount { get; set; }
        public string plan_Id { get; set; }
        public string PaytmNo { get; set; }
      


        public string Update_paytm(clsUser objUser)
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
                s2 = "select isnull(updatestatus,0) from UserDetail where UserId='" + objUser.UserId + "' ";
                DataTable Dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (Dt.Rows[0][0].ToString() == "0")
                {
                    s2 = "update UserDetail set phonepay='" + objUser.phonepay + "', bhimno='" + objUser.Bhimno + "',  upino='" + objUser.UPINo + "',updatestatus=1 where UserId='" + objUser.UserId + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    res = "t";
                    tr.Commit();
                }
                else
                {
                    res = "rr";
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
        public string Insert_useraddress(clsUser objState)
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
                s2 = "sp_addUserAddess";
                SqlParameter[] parameter = {
                new SqlParameter("@userid",objState.UserId),
                new SqlParameter("@mobileno",objState.Mobile),
                new SqlParameter("@AddressFirst",objState.Address),
                  new SqlParameter("@addressSecond",objState.AddressSec),
                new SqlParameter("@cityid",objState.CityId),
                new SqlParameter("@areaname",objState.AreaName),
                   new SqlParameter("@pincode",objState.Pincode),
                     new SqlParameter("@type",objState.RegType),
                       new SqlParameter("@isdefault",objState.UPINo)
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
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
        public DataTable getUserDownlinePool(clsUser objUser)
        {


            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("Proc_PoolDownline", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable getBusinessSummary(clsUser objUser, string fromDate, string toDate)
        {
            string sql = @"select rr.UserMobile,sum(isnull(rr.rechargeamount,0))sumOfRechargeAmount,sum(isnull(rr.debitamount,0))sumOfDebitAmount from RechargeRequest rr 
                            where rr.Status='Success' ";

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                sql += @" and ( convert(date,rr.EntryDate,103) between convert(date,'" + fromDate + "',103) and convert(date,'" + toDate + "',103) ) ";
            }

            if (!string.IsNullOrEmpty(objUser.UserId) && objUser.UserId != "0")
            {
                sql += @" and rr.UserMobile='" + objUser.UserId + "' ";
            }

            sql += @" group by rr.UserMobile ";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getConfirmationMessage(clsUser objUser)
        {
            string str_query = "SELECT UserDetail.UserId AS USerId,UserDetail.SponserId,Sponsor.UserName AS SponsorName,UserDetail.UserName,L.Password,L.Password AS TransPassword FROM UserDetail  LEFT OUTER  JOIN UserDetail AS Sponsor ON UserDetail.SponserId=Sponsor.UserId  LEFT OUTER JOIN LoginDetail L ON L.Username=UserDetail.UserId WHERE UserDetail.UserId='" + objUser.UserId + "' ";

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
            string USerId = dt.Rows[0]["USerId"].ToString();
            string UserName = dt.Rows[0]["UserName"].ToString();
            string Password = dt.Rows[0]["Password"].ToString();

            string url = string.Concat(new string[]
                {
                   //"http://sms.sandhyasoftware.com/vendorsms/pushsms.aspx?user=OJASSM&password=OJASSM &msisdn=",
                   //objUser.Mobile,
                   //"&sid=OJASSM&msg=CONGRATULATION DEAR "+UserName+" Welcome to http://ojasnetwork.in/user/index.aspx   Your Login id is "+USerId+"  and password "+Password +"   ",
                   //"&fl=0&gwid=2"
                });
            string Result = url.CallURL();
            Insert_SendSMS(objUser.Mobile, Result, url);

            ObjData.EndConnection();
            return dt;
        }
        public DataTable getBusinessSummaryPerUser(clsUser objUser, string fromDate, string toDate)
        {
            string sql = @"select UserMobile,Operator,sum(isnull(RechargeAmount,0))sumOfRechargeAmount,sum(isnull(debitamount,0))sumOfDebitAmount,Name as rechargeAPI 
                            from RechargeRequest rr inner join ApiOpCode aoc on rr.OpCode=aoc.OpCode inner join OperatorCode oc on oc.id=aoc.opid 
                            inner join RechargeApi ra on ra.Id=rr.ApiId where UserMobile='" + objUser.UserId + "' and rr.Status='Success' ";

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                sql += @" and ( convert(date,rr.EntryDate,103) between convert(date,'" + fromDate + "',103) and convert(date,'" + toDate + "',103) ) ";
            }

            sql += @" group by UserMobile,Operator,Name ";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getCallbackReport(clsUser objuser)
        {
            string str_query = "SELECT cd.*,ud.UserName,isnull(cd.callstatus,'Pending') as callstatus2,isnull(cd.CallResultStatus,'Opened') as CallResultStatus2 FROM CallbackRequestDetail cd LEFT JOIN userdetail ud ON cd.UserId=ud.UserId where 1=1 ";
            if (objuser.FromDate != System.DateTime.MinValue && objuser.ToDate != System.DateTime.MinValue)
            {
                object obj = str_query;
                str_query = string.Concat(new object[]
				{
					obj,
					"  and cd.mentiondate  >= '",
					objuser.FromDate,
					"'   and cd.mentiondate   <= '",
					objuser.ToDate,
					"' "
				});
            }
            if (objuser.UserId != "")
            {
                str_query = str_query + "  and cd.UserId = '" + objuser.UserId + "' ";
            }
            if (objuser.CallStatus != "0")
            {
                str_query = str_query + " and isnull(cd.callstatus,'Pending')='" + objuser.CallStatus + "'  ";
            }
            if (objuser.CallResultStatus != "0")
            {
                str_query = str_query + " and isnull(cd.CallResultStatus,'Opened')='" + objuser.CallResultStatus + "'  ";
            }
            str_query += " order by cd.mentiondate  desc";
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                dt = this.ObjData.RunDataTable(str_query);
            }
            catch (System.Exception ex_14B)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public string EditCallbackRequest(clsUser objUser)
        {
            string res = "";
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            SqlConnection cn = this.ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                string s2 = "sp_edit_CallbackRequestDetail";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@id", objUser.Id),
					new SqlParameter("@CallStatus", objUser.CallStatus),
					new SqlParameter("@Remark", objUser.Remark),
					new SqlParameter("@CallResultStatus", objUser.CallResultStatus),
					new SqlParameter("@PickedBy", objUser.PickedBy)
				};
                res = this.ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                tr.Commit();
            }
            catch (System.Exception ex_BF)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                this.ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public DataTable getUserReport(clsUser objUser)
        {
            //string str_query = "SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password,isnull(ud.balanceamount,0) as balanceamount FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join Logindetail ld on ud.userid=ld.username where 1=1 ";
            string str_query = @"SELECT ud.status,ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,isnull(ud.utilityBalance,0) as utilityBalance,ld.status as activeStatus, 
                                (case when ud.SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when ud.SignUpImgStatus=0 then 'Pending' when ud.SignUpImgStatus=1 then 'Approved' when ud.SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss, 
                                (case when ud.PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when ud.PanImgStatus=0 then 'Pending' when ud.PanImgStatus=1 then 'Approved' when ud.PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ud.ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ud.ChequeImgStatus=0 then 'Pending' when ud.ChequeImgStatus=1 then 'Approved' when ud.ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when ud.AadharImgStatus=0 then 'Pending' when ud.AadharImgStatus=1 then 'Approved' when ud.AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss,  
                                epin.planId,plm.PlanName as packageName,ud.SponserId,isnull(ud1.userName,'Company')sponserName,ud.PanNumber,sm.stateName,ud.Pincode,ud.epinGenerationStatus,CASE WHEN isnull(ud.GSTimage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.GSTimage END AS GSTimage,ud.gstnumber,isnull(ud.IsGSTDeductedOfUnverified,0) as IsGSTDeductedOfUnverifie,
(case when ud.IsGstApplicable=0 then 'Pending' when ud.IsGstApplicable=1 then 'Approved' when ud.IsGstApplicable=2 then 'Rejected' end)IsGstApplicable 
                                FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId 
                                left join Logindetail ld on ud.userid=ld.username left join EPinMaster epin on epin.UsedUserId=ud.userID 
                                left join PlanMaster plm on plm.id=epin.planId left join userdetail ud1 on ud.sponserId=ud1.userId 
                                where 1=1 ";

            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
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

        public DataTable getUserReportPageActive(clsUser objUser, string noofrows)
        {
            //string str_query = "SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password,isnull(ud.balanceamount,0) as balanceamount FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join Logindetail ld on ud.userid=ld.username where 1=1 ";
            string str_query = @"SELECT " + noofrows + @" ud.status,ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,isnull(ud.utilityBalance,0) as utilityBalance,ld.status as activeStatus, 
                                (case when ud.SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when ud.SignUpImgStatus=0 then 'Pending' when ud.SignUpImgStatus=1 then 'Approved' when ud.SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss, 
                                (case when ud.PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when ud.PanImgStatus=0 then 'Pending' when ud.PanImgStatus=1 then 'Approved' when ud.PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ud.ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ud.ChequeImgStatus=0 then 'Pending' when ud.ChequeImgStatus=1 then 'Approved' when ud.ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when ud.AadharImgStatus=0 then 'Pending' when ud.AadharImgStatus=1 then 'Approved' when ud.AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss,  
                                epin.planId,plm.PlanName as packageName,ud.SponserId,isnull(ud1.userName,'Company')sponserName,ud.PanNumber,sm.stateName,ud.Pincode,ud.epinGenerationStatus,CASE WHEN isnull(ud.GSTimage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.GSTimage END AS GSTimage,ud.gstnumber,isnull(ud.IsGSTDeductedOfUnverified,0) as IsGSTDeductedOfUnverifie,
(case when ud.IsGstApplicable=0 then 'Pending' when ud.IsGstApplicable=1 then 'Approved' when ud.IsGstApplicable=2 then 'Rejected' end)IsGstApplicable 
                                FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId 
                                left join Logindetail ld on ud.userid=ld.username left join EPinMaster epin on epin.UsedUserId=ud.userID 
                                left join PlanMaster plm on plm.id=epin.planId left join userdetail ud1 on ud.sponserId=ud1.userId 
                                where 1=1 ";

            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }
            str_query += " and ud.status=1";

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
        


        public DataTable getUserReportPage(clsUser objUser,string noofrows)
        {
            //string str_query = "SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password,isnull(ud.balanceamount,0) as balanceamount FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join Logindetail ld on ud.userid=ld.username where 1=1 ";
            string str_query = @"SELECT " + noofrows + @" ud.status,ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,isnull(ud.utilityBalance,0) as utilityBalance,ld.status as activeStatus, 
                                (case when ud.SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when ud.SignUpImgStatus=0 then 'Pending' when ud.SignUpImgStatus=1 then 'Approved' when ud.SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss, 
                                (case when ud.PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when ud.PanImgStatus=0 then 'Pending' when ud.PanImgStatus=1 then 'Approved' when ud.PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ud.ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ud.ChequeImgStatus=0 then 'Pending' when ud.ChequeImgStatus=1 then 'Approved' when ud.ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when ud.AadharImgStatus=0 then 'Pending' when ud.AadharImgStatus=1 then 'Approved' when ud.AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss,  
                                epin.planId,plm.PlanName as packageName,ud.SponserId,isnull(ud1.userName,'Company')sponserName,ud.PanNumber,sm.stateName,ud.Pincode,ud.epinGenerationStatus,CASE WHEN isnull(ud.GSTimage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.GSTimage END AS GSTimage,ud.gstnumber,isnull(ud.IsGSTDeductedOfUnverified,0) as IsGSTDeductedOfUnverifie,
(case when ud.IsGstApplicable=0 then 'Pending' when ud.IsGstApplicable=1 then 'Approved' when ud.IsGstApplicable=2 then 'Rejected' end)IsGstApplicable 
                                FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join statemaster sm on sm.stateId=cm.stateId 
                                left join Logindetail ld on ud.userid=ld.username left join EPinMaster epin on epin.UsedUserId=ud.userID 
                                left join PlanMaster plm on plm.id=epin.planId left join userdetail ud1 on ud.sponserId=ud1.userId 
                                where 1=1 ";

            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objUser.UserName))
            {
                str_query += "  and ud.username LIKE '%" + (objUser.UserName ?? string.Empty).Trim().Replace("'", "''") + "%' ";
            }
            if (!string.IsNullOrWhiteSpace(objUser.UserId))
            {
                str_query += "  and ud.UserId = '" + (objUser.UserId ?? string.Empty).Trim().Replace("'", "''") + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objUser.Mobile))
            {
                str_query += "  and ud.mobile = '" + (objUser.Mobile ?? string.Empty).Trim().Replace("'", "''") + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objUser.Email))
            {
                str_query += "  and ud.email = '" + (objUser.Email ?? string.Empty).Trim().Replace("'", "''") + "' ";
            }
            if (objUser.CityId != "0" && !string.IsNullOrEmpty(objUser.CityId))
            {
                str_query += "  and ud.cityid = '" + objUser.CityId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + (objUser.Pincode ?? string.Empty).Trim().Replace("'", "''") + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + (objUser.PanCardNo ?? string.Empty).Trim().Replace("'", "''") + "' ";
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
        
        public string InsertCallbackRequest(clsUser objUser)
        {
            string res = "";
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            SqlConnection cn = this.ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                string s2 = "sp_add_CallbackRequestDetail";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@UserId", objUser.UserId),
					new SqlParameter("@MobileNo", objUser.Mobile),
					new SqlParameter("@MentionBy", objUser.MentionBy)
				};
                res = this.ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                tr.Commit();
            }
            catch (System.Exception ex_97)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                this.ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public DataTable getUserList()
        {
            string str_query = @"select * from Userdetail ";
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

        public DataTable getUseraddress(string Userid)
        {
            string str_query = "SELECT ID AddressId,Userid,Addressfirst,AddressSecond,C.CityId,c.CityName,areaname,Pincode,mobile,Type,DefaultAdd,CASE WHEN DefaultAdd=1 THEN 'YES' ELSE 'NO' END ISdefault,C.Stateid  FROM useraddress U JOIN citymaster C ON U.Cityid=C.CityId  where 1=1 and userid='" + Userid + "'";
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

        public DataTable getUseraddressById(string userId, string addressId)
        {
            string str_query = "SELECT ID AddressId,Userid,Addressfirst,AddressSecond,C.CityId,c.CityName,areaname,Pincode,mobile,Type,DefaultAdd,CASE WHEN DefaultAdd=1 THEN 'YES' ELSE 'NO' END ISdefault,C.Stateid FROM useraddress U JOIN citymaster C ON U.Cityid=C.CityId WHERE U.ID='" + addressId + "' AND U.userid='" + userId + "'";
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

        public string Update_useraddress(clsUser objState, string addressId)
        {
            string res = "f";
            SqlConnection cn;
            SqlTransaction tr = null;
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                if (objState.UPINo == "1")
                {
                    string resetDefault = "UPDATE useraddress SET DefaultAdd=0 WHERE Userid='" + objState.UserId + "'";
                    ObjData.RunInsUpDelQueryTrans(resetDefault, tr);
                }

                string updateQuery = "UPDATE useraddress SET Addressfirst='" + objState.Address
                    + "',AddressSecond='" + objState.AddressSec
                    + "',Cityid='" + objState.CityId
                    + "',areaname='" + objState.AreaName
                    + "',Pincode='" + objState.Pincode
                    + "',mobile='" + objState.Mobile
                    + "',Type='" + objState.RegType
                    + "',DefaultAdd='" + objState.UPINo
                    + "' WHERE ID='" + addressId + "' AND Userid='" + objState.UserId + "'";

                ObjData.RunInsUpDelQueryTrans(updateQuery, tr);
                tr.Commit();
                res = "t";
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

        public string Delete_useraddress(string userId, string addressId)
        {
            string res = "f";
            ObjData.StartConnection();
            try
            {
                string deleteQuery = "DELETE FROM useraddress WHERE ID='" + addressId + "' AND Userid='" + userId + "'";
                ObjData.RunInsUpDelQuery(deleteQuery);
                res = "t";
            }
            catch (Exception ex)
            {
                res = "0";
            }
            finally
            {
                ObjData.EndConnection();
            }
            return res;
        }

        public DataTable getUsercartaddress(string Userid)
        {
            string str_query = "SELECT UD.UserName,U.ID AddressId,U.Userid,Addressfirst,AddressSecond,C.CityId,c.CityName,U.areaname,U.Pincode,U.mobile,Type,DefaultAdd,CASE WHEN DefaultAdd=1 THEN 'YES' ELSE 'NO' END ISdefault,C.Stateid  FROM useraddress U JOIN citymaster C ON U.Cityid=C.CityId JOIN userdetail UD ON UD.UserId = U.Userid   where 1 = 1 and U.userid = '" + Userid + "' AND U.ID = (SELECT TOP 1  addressid FROM CartItems WHERE UserId = U.Userid)";

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


        public DataTable getUserName(clsUser objUser)
        {
            string str_query = @"SELECT ud.adharnumber,ud.userid,ud.SponserId, (Select Username from UserDetail where UserId=ud.SponserId) as SponserName,ud.username,ud.mobile,ud.Address, 
                                convert(nvarchar(50),ud.DateofBirth,106) as DateofBirth,convert(nvarchar(50),ud.RegDate,106) as DOJ, 
                                case when ActiveStatus=1 then 'Active' else 'Deactive' end as ActiveStatus, case when Status=1 then 'Active' else 'Deactive' end as Status, 
                                ud.PanNumber, CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS Photo, 
                                CASE WHEN isnull(ud.SignUpFormImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.SignUpFormImage END AS SignUpForm, SignUpImgStatus, 
                                CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImage  , PanImgStatus, 
                                CASE WHEN isnull(ud.CancelCheque,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.CancelCheque END AS CancelCheque, ChequeImgStatus, 
                                CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImage, AadharImgStatus, 
                                CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImageBack,CASE WHEN isnull(ud.GSTimage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.GSTimage END AS GSTimage,ud.gstnumber,isnull(ud.IsGSTDeductedOfUnverified,0) as IsGSTDeductedOfUnverified,ud.gender   
                                FROM userdetail  ud where ud.UserId = '" + objUser.UserId + "' ";
            DataTable dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                DataTable result = ObjData.RunDataTable(str_query);
                if (result != null)
                {
                    dt = result;
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable selectUsernameJoiningpackage(clsUser objUser)
        {
            // string str_query = "SELECT U.UserId,U.Username,P.PlanName,P.PlanAmount,regdate  FROM UserDetail U JOIN Planmaster P ON U.SlabID=P.Id WHERE UserId='"+objUser.UserId+"' AND isnull(Status,0)=1 AND isnull(U.InvoiceStatus,0)=0";

            string str_query = "SELECT U.UserId,U.Username,P.PlanName,P.PlanAmount,regdate,pd.Amount as Amount ,pd.ProductName,pd.ProductImage as Image,pd.MRP as BV,cast(1 as float) * cast(pd.Amount as float) as TotalAmount,1 as Quantity,pd.ProductId FROM UserDetail U JOIN Planmaster P ON U.SlabID=P.Id inner join productmaster pd on pd.productid=p.productid WHERE UserId='" + objUser.UserId + "' AND isnull(u.Status,0)=1 AND isnull(U.InvoiceStatus,0)=0";
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
        public DataSet getUserNamenew(clsUser objUser)
        {
            string str_query = "SELECT S.statename as State,cm.CityName as city,ud.UserId,ud.SponserId, (Select Username from UserDetail where UserId=ud.SponserId) as SponserName,ud.UserName,ud.Mobile,ud.Address,convert(nvarchar(50),ud.DateofBirth,106) as DateofBirth,convert(nvarchar(50),ud.RegDate,106) as RegDate,case when ActiveStatus=1 then 'Active' else 'Deactive' end as ActiveStatus, case when Status=1 then 'Active' else 'Deactive' end as Status,ud.PanNumber, CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage, CASE WHEN isnull(ud.SignUpFormImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.SignUpFormImage END AS SignUpForm, CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImage  , CASE WHEN isnull(ud.CancelCheque,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.CancelCheque END AS CancelCheque, CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImage,CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImageBack ,ud.pincode  FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join StateMaster S on S.StateId=cm.stateId where ud.UserId = '" + objUser.UserId + "' ";
            DataSet dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunSelectQuery(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getAssociatesDetail(clsUser objUser)
        {
            string str_query = "select UserId,UserName,convert(nvarchar(50),DateofBirth,106) as DateofBirth,Gender,Email,Mobile,Address,convert(nvarchar(50),RegDate,106) as RegDate,case when StandingPosition='1' then 'Left' else 'Right' end as StandingPosition,case when Status='1' then 'Active' else 'Deactive' end as [Status] from UserDetail where ParentUserId='" + objUser.UserId + "'";
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
        public DataTable getAssociatesDetailNew(clsUser objUser)
        {
            string str_query = @"select ud.sponserId,ud.UserId,ud.UserName,convert(nvarchar(50),ud.DateofBirth,106) as DateofBirth,ud.Gender,ud.Email,ud.Mobile,ud.Address, 
                                convert(nvarchar(50),ud.RegDate,106) as RegDate,case when ud.StandingPosition='1' then 'Left' else 'Right' end as StandingPosition, 
                                case when ud.Status='1' then 'Active' else 'Deactive' end as [Status],ud.ParentUserID,pd.username as parentname, 
                                epin.planId,plm.PlanName as packageName,cm.CityName,sm.stateName
                                from UserDetail ud join UserDetail pd on ud.parentuserId=pd.UserId 
                                LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId 
                                left join EPinMaster epin on epin.UsedUserId=ud.userID left join PlanMaster plm on plm.id=epin.planId 
                                where ud.sponserId='" + objUser.UserId + "'";

            if (objUser.StandingPosition != "")
            {

                str_query += "and ud.standingposition='" + objUser.StandingPosition + "'";

            }
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
        public DataTable getAssociatesDetailNewlatest(clsUser objUser)
        {



            string str_query = @"select " + objUser.Pincode + @" ud.sponserId,ud.UserId,ud.UserName,convert(nvarchar(50),ud.DateofBirth,106) as DateofBirth,ud.Gender,ud.Email,ud.Mobile,ud.Address, 
                                ud.RegDate as RegDate,case when ud.StandingPosition='1' then 'Left' else 'Right' end as StandingPosition, 
                                case when ud.Status='1' then 'Paid' else 'Unpaid' end as [Status],ud.ParentUserID,pd.username as parentname, 
                                ud.slabid planId,plm.PlanName as packageName,cm.CityName,sm.stateName,plm.planamount
                                from UserDetail ud join UserDetail pd on ud.parentuserId=pd.UserId 
                                LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId 
                                left join PlanMaster plm on plm.id=ud.slabid
                                where  ud.sponserId='" + objUser.UserId + "'  ";

            if (objUser.StandingPosition != "0")
            {
                str_query += "and ud.StandingPosition='" + objUser.StandingPosition + "'";
            }


            str_query += "order by ud.regdate desc";
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


        public DataTable getUserleftDirect(clsUser objUser)
        {
            string str_query = "select Count(UD.sponserid) FROM UserBuisnessVolume U JOIN userdetail UD ON U.FromUserId=UD.UserId WHERE U.UserId='" + objUser.UserId + "' AND leftuser='1'  AND U.PurchaseType='J' and UD.SponserId='" + objUser.UserId + "' ";
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
        public DataTable getUserrightDirect(clsUser objUser)
        {
            string str_query = "select Count(UD.sponserid) FROM UserBuisnessVolume U JOIN userdetail UD ON U.FromUserId=UD.UserId WHERE U.UserId='" + objUser.UserId + "' AND rightuser='1'  AND U.PurchaseType='J' and UD.SponserId='" + objUser.UserId + "' ";
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
        public DataTable GetGiftbalanceReport(clsUser objUser)
        {
            string str_query = "SELECT " + objUser.Pincode + " ud.UserId,ud.UserName,P.PlanName,Convert(CHAR,a.Instdate,103) AS Instdate,CASE WHEN a.Status=0 THEN '' ELSE  Convert(CHAR,a.Activationdate,103) END  AS Activationdate,CASE WHEN a.Status=0 THEN 'UNPAID' ELSE 'PAID' END AS Status1,a.Instno,a.amount,'Joining' as Type,a.adminper,a.admincharge,a.paybleamount FROM UserDetail ud JOIN ActivationAmount A ON ud.UserId=A.Userid JOIN planmaster P ON  ud.SlabID=P.Id WHERE 1=1 AND A.Userid='" + objUser.UserId + "' order by a.instno";
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

        public DataTable GetDailyLevelReport(clsUser objUser)
        {
            string str_query = "SELECT " + objUser.Pincode + " ud.UserId,ud.UserName,a.Fromuserid,a.LevelNo,Convert(CHAR,a.EntryDate,103) AS Instdate,CASE WHEN a.Status=0 THEN '' ELSE  Convert(CHAR,a.Entrydate,103) END  AS Entrydate,CASE WHEN a.Status=0 THEN 'UNPAID' ELSE 'PAID' END AS Status1,a.Entrydate,a.income,'Joining' as Type,a.adminper,a.admincharge,a.paybleamount FROM UserDetail ud JOIN ROIDailyLevelIncomeTB A ON ud.UserId=A.Userid  AND A.Userid='" + objUser.UserId + "' order by a.Entrydate";
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

        public DataTable getUserNameWithBalance(clsUser objUser)
        {
            string str_query = "SELECT ud.userid, ud.username,ud.mobile,ud.balanceamount AS balance FROM userdetail ud where ud.UserId = '" + objUser.UserId + "' ";
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
        public DataTable getUserNameWithBalanceutility(clsUser objUser)
        {
            string str_query = "SELECT ud.userid, ud.username,ud.mobile,ud.balanceamount AS balance FROM userdetail ud where ud.UserId = '" + objUser.UserId + "' ";
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
        public DataTable getUserDownline(clsUser objUser)
        {
            int totalCount;
            return getUserDownlinePaged(objUser, 0, 0, out totalCount);
        }

        /// <summary>
        /// Sponsor-based downline. pageSize &lt;= 0 returns all rows (no OFFSET).
        /// </summary>
        public DataTable getUserDownlinePaged(clsUser objUser, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            string userId = (objUser.UserId ?? string.Empty).Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(userId))
            {
                return new DataTable();
            }

            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            string pageClause = string.Empty;
            if (pageSize > 0)
            {
                int offset = pageIndex * pageSize;
                pageClause = " OFFSET " + offset + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }

            string str_query = @";WITH MyCTE AS
(
    SELECT 
        ud.id,
        ud.userid,
        ud.username,
        ud.ParentUserId,
        0 AS userlevel,
        ud.StandingPosition,
        CASE WHEN ISNULL(ud.SavingStatus, 0) = 1 THEN 'Paid' ELSE 'Unpaid' END AS Status,
        ud.SponserId AS sponserid,
        ud.Mobile
    FROM UserDetail ud WITH (NOLOCK)
    WHERE ud.UserId = '" + userId + @"'

    UNION ALL

    SELECT 
        ud.id,
        ud.userid,
        ud.username,
        ud.ParentUserId,
        cte.userlevel + 1 AS userlevel,
        ud.StandingPosition,
        CASE WHEN ISNULL(ud.SavingStatus, 0) = 1 THEN 'Paid' ELSE 'Unpaid' END AS Status,
        ud.SponserId AS sponserid,
        ud.Mobile
    FROM UserDetail ud WITH (NOLOCK)
    INNER JOIN MyCTE cte ON ud.SponserId = cte.userid
    WHERE ud.UserId <> '" + userId + @"'
)
SELECT 
    cte.id,
    cte.userid,
    cte.username,
    cte.ParentUserId,
    cte.userlevel,
    cte.StandingPosition,
    cte.Status,
    cte.sponserid,
    cte.Mobile,
    parent.username AS parentname,
    COUNT(1) OVER() AS TotalRecords
FROM MyCTE cte
LEFT JOIN UserDetail parent WITH (NOLOCK) ON cte.sponserid = parent.UserId
WHERE cte.userlevel > 0
ORDER BY cte.userlevel ASC, cte.userid ASC
" + pageClause + @"
OPTION (MAXRECURSION 0);";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Contains("TotalRecords"))
                {
                    totalCount = Convert.ToInt32(dt.Rows[0]["TotalRecords"]);
                }
                else if (dt != null)
                {
                    totalCount = dt.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt ?? new DataTable();
        }

        public DataTable getUserDownlineChkNew(clsUser objUser, string UserId)
        {
            string str_query = "";

            str_query = @"; WITH MyCTE
AS ( SELECT id,userid,username,   ParentUserId,1 AS userlevel
FROM userdetail
WHERE UserId ='" + objUser.UserId + @"'
UNION ALL
SELECT UserDetail.id,userdetail.userid,userdetail.username,  userdetail.ParentUserId ,MyCTE.userlevel+1 
FROM userdetail
INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
WHERE userdetail.userid !='" + objUser.UserId + @"' )
SELECT MyCTE.*,ud.username as parentname
FROM MyCTE left join userdetail ud on mycte.parentuserid=ud.userid  WHERE MyCTE.UserId='" + UserId + "' option (maxrecursion 0)";



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
        public DataTable getTotalSV(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "get_SV_data";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",UserId),



                };


                DataSet ds = DBHelper.ExecuteQuery(s2, parameter);

                DataTable dt = ds.Tables[0];

                return dt;
                //  Dt = ObjData.RunDataTableProcedure(s2, parameter);



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
        public DataTable getTotalincome(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_Totalincome";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",UserId),



                };


                DataSet ds = DBHelper.ExecuteQuery(s2, parameter);

                DataTable dt = ds.Tables[0];

                return dt;
              //  Dt = ObjData.RunDataTableProcedure(s2, parameter);



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
        public DataSet GetDirect(clsUser objuser)
        {
            SqlParameter[] para = {
                                   new SqlParameter("@UserId",objuser.UserId)
                                   };
            return DBHelper.ExecuteQuery("AllIncome", para);
        }



        public DataTable getUserDirect(clsUser objUser)
        {
            

            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),
                                    
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectStandingWise", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable getUserDirectLeft(clsUser objUser)
        {
            //string str_query = "";
            //            string str_query = @"DECLARE @child NVARCHAR(100)
            //
            //SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='1'
            //; WITH MyCTE
            //AS ( SELECT id,userid,username,ParentUserId,Case when isnull(Status,0)=1 then 'active' else 'deactive' end as Status,1 AS userlevel
            //FROM userdetail
            //WHERE UserId =@child
            //UNION ALL
            //SELECT UserDetail.id,userdetail.userid,userdetail.username,  userdetail.ParentUserId ,Case when isnull(UserDetail.Status,0)=1 then 'active' else 'deactive' end as Status,MyCTE.userlevel+1 
            //FROM userdetail
            //INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
            //WHERE userdetail.userid !=@child )
            //SELECT MyCTE.*,ud.username as parentname
            //FROM MyCTE left join userdetail ud on mycte.parentuserid=ud.userid ";



            //            DataTable dt = null;
            //            ObjData.StartConnection();
            //            try
            //            {
            //                dt = ObjData.RunDataTable(str_query);
            //            }
            //            catch (Exception ex)
            //            {
            //                dt = null;
            //            }
            //            ObjData.EndConnection();
            //            return dt;

            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectStandingWiseLeft", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }
        public DataTable getUserDownlineLeft(clsUser objUser)
        {
            //string str_query = "";
//            string str_query = @"DECLARE @child NVARCHAR(100)
//
//SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='1'
//; WITH MyCTE
//AS ( SELECT id,userid,username,ParentUserId,Case when isnull(Status,0)=1 then 'active' else 'deactive' end as Status,1 AS userlevel
//FROM userdetail
//WHERE UserId =@child
//UNION ALL
//SELECT UserDetail.id,userdetail.userid,userdetail.username,  userdetail.ParentUserId ,Case when isnull(UserDetail.Status,0)=1 then 'active' else 'deactive' end as Status,MyCTE.userlevel+1 
//FROM userdetail
//INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
//WHERE userdetail.userid !=@child )
//SELECT MyCTE.*,ud.username as parentname
//FROM MyCTE left join userdetail ud on mycte.parentuserid=ud.userid ";



//            DataTable dt = null;
//            ObjData.StartConnection();
//            try
//            {
//                dt = ObjData.RunDataTable(str_query);
//            }
//            catch (Exception ex)
//            {
//                dt = null;
//            }
//            ObjData.EndConnection();
//            return dt;
            
            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("LeftUserIDownLine", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable getUserDirectRight(clsUser objUser)
        {
            //string str_query = "";
            //            string str_query = @"DECLARE @child NVARCHAR(100)
            //
            //SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='2'
            //; WITH MyCTE
            //AS ( SELECT id,userid,username,ParentUserId,Case when isnull(Status,0)=1 then 'active' else 'deactive' end as Status,1 AS userlevel
            //FROM userdetail
            //WHERE UserId =@child
            //UNION ALL
            //SELECT UserDetail.id,userdetail.userid,userdetail.username,  userdetail.ParentUserId,Case when isnull(UserDetail.Status,0)=1 then 'active' else 'deactive' end as Status ,MyCTE.userlevel+1 
            //FROM userdetail
            //INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
            //WHERE userdetail.userid !=@child )
            //SELECT MyCTE.*,ud.username as parentname
            //FROM MyCTE left join userdetail ud on mycte.parentuserid=ud.userid ";



            //            DataTable dt = null;
            //            ObjData.StartConnection();
            //            try
            //            {
            //                dt = ObjData.RunDataTable(str_query);
            //            }
            //            catch (Exception ex)
            //            {
            //                dt = null;
            //            }
            //            ObjData.EndConnection();
            //            return dt;

            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectStandingWiseRight", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }
        public DataTable getUserDownlineRight(clsUser objUser)
        {
            //string str_query = "";
//            string str_query = @"DECLARE @child NVARCHAR(100)
//
//SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='2'
//; WITH MyCTE
//AS ( SELECT id,userid,username,ParentUserId,Case when isnull(Status,0)=1 then 'active' else 'deactive' end as Status,1 AS userlevel
//FROM userdetail
//WHERE UserId =@child
//UNION ALL
//SELECT UserDetail.id,userdetail.userid,userdetail.username,  userdetail.ParentUserId,Case when isnull(UserDetail.Status,0)=1 then 'active' else 'deactive' end as Status ,MyCTE.userlevel+1 
//FROM userdetail
//INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
//WHERE userdetail.userid !=@child )
//SELECT MyCTE.*,ud.username as parentname
//FROM MyCTE left join userdetail ud on mycte.parentuserid=ud.userid ";



//            DataTable dt = null;
//            ObjData.StartConnection();
//            try
//            {
//                dt = ObjData.RunDataTable(str_query);
//            }
//            catch (Exception ex)
//            {
//                dt = null;
//            }
//            ObjData.EndConnection();
//            return dt;

            SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("RightUserIDownLine", para);

            DataTable dt = ds.Tables[0];

            return dt;
        }
        public DataTable FillSubNode(clsUser objuser)
        {
            DataTable dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                string qry = "SELECT ad.userid,ad.username,(select count(userid) from userdetail where sponserid=ad.userid) as Subnode FROM userdetail ad where ad.sponserid='" + objuser.UserId + "'   and ad.sponserid !=ad.userid ";
                dt = ObjData.RunDataTable(qry);
            }
            catch (Exception v)
            {
                dt = null;
            }
            finally
            {
                ObjData.EndConnection();
            }
            return dt;
        }
        public DataSet Find_UserDetail2(clsUser objuser)
        {

            string s = "";
            string s1 = "";
            DataSet ds = null;
            ObjData.StartConnection();

            try
            {
                s1 = "SELECT ad.userid,ad.UserName,(select count(userid) from userdetail where sponserid=ad.userid) as Subnode FROM userdetail ad  where ad.userid='" + objuser.UserId + "'";
                ds = ObjData.RunSelectQuery(s1);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }

        public int InsertINRDollor(string Inr,String Dollar)
        {
            string s = "";
            string s1 = "";
            int ds = 0;
            ObjData.StartConnection();

            SqlConnection cn = this.ObjData.StartConnectionInTransaction();

            try
            {
                s1 = "update INRConvertorMaster set INR="+Inr+ ",Dollar="+ Dollar;
                ObjData.RunInsUpDelQuery(s1);
                ds = 1;
            }
            catch (Exception ex)
            {
                ds = 0;
            }
            ObjData.EndConnection();
            return ds;
        }

        public DataSet Find_INRDollor()
        {

            string s = "";
            string s1 = "";
            DataSet ds = null;
            ObjData.StartConnection();

            try
            {
                s1 = "SELECT * from INRConvertorMaster";
                ds = ObjData.RunSelectQuery(s1);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }


        public DataTable getUserDetail(clsUser objUser)
        {
            string str_query = "SELECT ud.*,cm.stateid,sm.countryid,sm.statename,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,(select UserName from userdetail where UserId=ud.parentuserid) as parentname,convert(char,ud.activatedate,103) as activationdate,(select planamount from UserTopupTb where userid=ud.userid and type='A') planamount FROM userdetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + objUser.UserId + "' ";
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
        static string BuildGetUserDetailQuery(string userId)
        {
            return @"SELECT ud.UserId, ud.UserName, ud.Mobile, ud.Email, ud.Gender, ud.Address, ud.CityId, ud.AreaName, ud.Pincode,
                ud.DateOfBirth, ud.RegDate, ud.NomineeName, ud.NomineeRelation, ud.NomineeDateofBirt,
                ud.AccountHolderName, ud.AccountNo, ud.IFSCCode, ud.PanNumber, ud.BranchName, ud.BankName, ud.adharnumber,
                ud.PhotoImage, ud.SponserId, ud.ParentUserId, ud.Status, ud.BalanceAmount, ud.UtilityBalance,
                ISNULL(cm.StateId, 0) AS stateid, ISNULL(sm.CountryId, 0) AS countryid, ISNULL(sm.StateName, '') AS statename,
                (SELECT UserName FROM userdetail WHERE UserId = ud.SponserId) AS Sponsername,
                (SELECT UserName FROM userdetail WHERE UserId = ud.ParentUserId) AS parentname,
                CONVERT(char, ud.ActivateDate, 103) AS activationdate,
                (SELECT TOP 1 planamount FROM UserTopupTb WHERE userid = ud.userid AND type = 'A') AS planamount
                FROM userdetail ud
                LEFT JOIN citymaster cm ON ud.CityId = cm.CityId
                LEFT JOIN statemaster sm ON cm.StateId = sm.StateId
                WHERE ud.UserId = '" + userId + "'";
        }

        DataTable RunGetUserDetailQuery(string str_query)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getUserChild(clsUser objUser)
        {
            string str_query = "select ud.username,ud.userid,SponserId, (Select Username from UserDetail where UserId=ud.SponserId) as SponserName,convert(nvarchar(50),ud.RegDate,106) as DOJ,case when Status=1 then 'Active' else 'Deactive' end as Status,ud.gender  from userdetail ud   where ud.parentuserid='" + objUser.ParentUserId + "'  and ud.StandingPosition='" + objUser.StandingPosition + "' order by ud.RegDate asc, ud.id asc";
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
        public DataTable getRightDataPlanWise2(clsUser objUser)
        {
            string str_query = "";

            str_query = @"DECLARE @child NVARCHAR(100)

SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='2'
; WITH MyCTE
AS ( SELECT id,userid,username,  StandingPosition, ParentUserId,1 AS userlevel,regdate
FROM userdetail
WHERE UserId =@child
UNION ALL
SELECT UserDetail.id,userdetail.userid, UserDetail.username,UserDetail.StandingPosition, userdetail.ParentUserId ,MyCTE.userlevel+1 ,userdetail.regdate
FROM userdetail
INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
WHERE userdetail.userid !=@child )
 
 SELECT count(id) AS totaluser
 
FROM MyCTE  
option (maxrecursion 0)
";

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
        public DataTable getLeftDataPlanWise2(clsUser objUser)
        {

            string str_query = "";
            str_query = @"DECLARE @child NVARCHAR(100)

SELECT @child=userid FROM UserDetail WHERE ParentUserId='" + objUser.UserId + @"' AND StandingPosition='1'
; WITH MyCTE
AS ( SELECT id,userid,username,  StandingPosition, ParentUserId,1 AS userlevel,regdate
FROM userdetail
WHERE UserId =@child
UNION ALL
SELECT UserDetail.id,userdetail.userid, UserDetail.username,UserDetail.StandingPosition, userdetail.ParentUserId ,MyCTE.userlevel+1 ,userdetail.regdate
FROM userdetail
INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.userid
WHERE userdetail.userid !=@child )
 
 SELECT count(id) AS totaluser
 
FROM MyCTE  
option (maxrecursion 0)
";

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
        public DataTable getTeamSumBV(clsUser objUser, string Position, string TYpe)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "TeamSumBv";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@StandingPosition",Position), 
                    new SqlParameter("@TYpe",TYpe), 
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getselfSumBV(clsUser objUser)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "CalculateSelfBV";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId),                  
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public string Insert_User(clsUser objUser)
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
                s2 = "sp_add_UserDetailAuto";
                SqlParameter[] parameter = {              
                    new SqlParameter("@SponserId",objUser.SponserId), 
                    new SqlParameter("@username",objUser.UserName), 
                   new SqlParameter("@DateofBirth",objUser.DateOfBirth), 
                    new SqlParameter("@Gender",objUser.Gender), 
                    new SqlParameter("@Email",objUser.Email), 
                    new SqlParameter("@Mobile",objUser.Mobile),
                      new SqlParameter("@Address",objUser.Address),
                          new SqlParameter("@CityId",objUser.CityId),
                     new SqlParameter("@Nomineename",objUser.NomineeName), 
                    new SqlParameter("@NomineeRelation",objUser.NomineeRelation),
                      new SqlParameter("@NomineeDateofBirth",objUser.NomDateOfBirth),
                      new SqlParameter("@AreaName",objUser.AreaName),
                    new SqlParameter("@Pincode",objUser.Pincode),
                       new SqlParameter("@MentionBy",objUser.MentionBy),                       
                    new SqlParameter("@Password",objUser.Password),                  
                    new SqlParameter("@EPinNo",objUser.EpinNo),
                       new SqlParameter("@StandingPosition",objUser.StandingPosition),
                         new SqlParameter("@RegType",objUser.RegType),
                    new SqlParameter("@Adhar",objUser.AdhaarNo),
                        new SqlParameter("@PanNumber",objUser.PanCardNo),
                      new SqlParameter("@OtherCity",objUser.OtherCity),
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                               
                //string url = string.Concat(new string[]
                //{
                //    "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=",
                //    objUser.Mobile,
                //    "&dcs=0&msgtxt=Dear User you are successfully registered on arsenpay.in Your login details are-userid:",
                //    res,
                //    ", password:",
                //    objUser.Password,
                //    "&state=4"
                //});
               // string url = smstemplate(res, objUser.Mobile, objUser.Password, objUser.Password, "Team World", "https://teammakerindia.com", "", "", "", "", "", "", "", "registration");
               // string Result = url.CallURL();
               // Insert_SendSMS(objUser.Mobile, Result, url);
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
        public string smstemplate(string UserId,string mobileNo, string password, string PinPassword, string Company, string Website,string tousername,string amount,string balanceamount,string fromusername,string opname,string transactionId,string liveId ,string Type)
        {
             
              string smsURl="";
              string SendMessage = "";
              DataTable dt2 = objapisms.GetSmsApi_true("admin");
              smsURl = dt2.Rows[0]["url"].ToString();
              DataTable dtmsg = objsetting.Get_Message_All_Format();
              if (dtmsg != null && dtmsg.Rows.Count > 0)
              {
                  if (Type == "registration")
                  {
                      SendMessage = dtmsg.Rows[0]["Registration"].ToString();
                  }
                  if (Type == "fundcredit")
                  {
                      SendMessage = dtmsg.Rows[0]["FundCredit"].ToString();
                  }
                  if (Type == "funddebit")
                  {
                      SendMessage = dtmsg.Rows[0]["FundDebit"].ToString();
                  }
                  if (Type == "rechargeaccept")
                  {
                      SendMessage = dtmsg.Rows[0]["rechargeaccept"].ToString();
                  }
                  if (Type == "rechargesuccess")
                  {
                      SendMessage = dtmsg.Rows[0]["rechargesuccess"].ToString();
                  }
                  if (Type == "rechargefailed")
                  {
                      SendMessage = dtmsg.Rows[0]["rechargefailed"].ToString();
                  }  
                  SendMessage = SendMessage.Replace("\r\n", "");
                  SendMessage = SendMessage.Replace("{UserID}", UserId);
                  SendMessage = SendMessage.Replace("{MobileNo}", mobileNo);
                  SendMessage = SendMessage.Replace("{Password}", password);
                  SendMessage = SendMessage.Replace("{PinPassword}", PinPassword);
                  SendMessage = SendMessage.Replace("{CompanyName}", Company);
                  SendMessage = SendMessage.Replace("{Website}", Website);
                  SendMessage = SendMessage.Replace("{ToUserName}", tousername);
                  SendMessage = SendMessage.Replace("{Amount}", amount);
                  SendMessage = SendMessage.Replace("{BalanceAmount}", balanceamount);               
                  SendMessage = SendMessage.Replace("{FromUserName}", fromusername);
                  SendMessage = SendMessage.Replace("{OperatorName}", opname);
                  SendMessage = SendMessage.Replace("{TransactionID}", transactionId);
                  SendMessage = SendMessage.Replace("{LiveID}", liveId);
                  SendMessage = SendMessage.Replace("{Mobile}", tousername);
                  SendMessage = SendMessage.Replace("{Reason}", PinPassword);
                  
                  if (Type == "balance")
                  {
                      SendMessage = UserId;
                  }
                  if (Type == "password")
                  {
                      SendMessage = UserId;
                  }
                  if (Type == "error")
                  {
                      SendMessage = UserId;
                  }
              }

              smsURl = smsURl.Replace("{mobileno}", mobileNo).Replace("{msg}", SendMessage);
              return smsURl;
        }

        public DataTable getUserRewardReport(clsUser objUser)
        {
            string str_query = "SELECT st1.*,st2.awardname FROM AwardAchiverUser st1  LEFT JOIN AwardAchiver st2 ON st1.AwardId=st2.id WHERE 1=1";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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


        public DataTable getUserLevelIncomeReport(clsUser objUser)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Level Income' ";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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

        public DataTable getUserDirectIncomeReport(clsUser objUser)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Direct Income' ";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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

        public DataTable getUserAutoPoolIncomeReport(clsUser objUser)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Auto Pool Income' ";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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

        public DataTable getUserDailyIncomeReport(clsUser objUser)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Daily Income' ";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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
        public DataTable getUserDailyLevelIncomeReport(clsUser objUser)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Daily Level Income' ";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            //}


            if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(objUser.UserName))
            {
                str_query += "  and ud.username = '" + objUser.UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(objUser.UserId))
            {
                str_query += "  and UserId = '" + objUser.UserId + "' ";
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
            if (!string.IsNullOrEmpty(objUser.Pincode))
            {
                str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
            }

            if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
            {
                str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
            {
                str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
            {
                str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
            }
            if (!string.IsNullOrEmpty(objUser.PanCardNo))
            {
                str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
            }


            // str_query += " order by ud.MentionDate  desc";

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

        public string activateUserWithEpin(clsUser objUser)
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
                s2 = "sp_activeUserWithEpin";
                SqlParameter[] parameter = {              
                    new SqlParameter("@ActivateUserId",objUser.TransferUserId), 
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@epin",objUser.EpinNo), 
                  
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

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
        public string RetopupUserWithEpin(clsUser objUser)
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
                s2 = "sp_RetopupUserWithEpin";
                SqlParameter[] parameter = {              
                    new SqlParameter("@ActivateUserId",objUser.TransferUserId), 
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@epin",objUser.EpinNo), 
                  
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

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
        public string Transferamountwallet(clsUser objUser)
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
                s2 = "sp_WalletTransferUser";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@TransferUserId",objUser.TransferUserId), 
                    new SqlParameter("@Amount",objUser.chargeAmount), 
                  
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

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
        public string Update_UserPhoto(clsUser objUser)
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
                s2 = "update UserDetail  set IsPhotoEditable=0,PhotoImage='" + objUser.Photo + "'  where UserId='" + objUser.UserId + "'   ";
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


        public string Update_UserSignupForm(clsUser objUser)
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
                s2 = "update UserDetail  set SignUpFormImage='" + objUser.Signupform + "', SignUpImgStatus=0, IsSignUpFomrEditable ='" + objUser.EditStatus + "'  where UserId='" + objUser.UserId + "'   ";
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






        public string Update_UserPanForm(clsUser objUser)
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
                string str_query = "select * from userdetail where PanNumber='" + objUser.PanCardNo + "' ";
                DataTable dt = null;
                dt = ObjData.RunSelectQueryTTrans(str_query, tr);
                if (dt.Rows.Count < 3)
                {
                    s2 = "update UserDetail  set PanNumber='" + objUser.PanCardNo + "', PanImage='" + objUser.PanImage + "', PanImgStatus=0, IsPanCardEditable='" + objUser.EditStatus + "'  where UserId='" + objUser.UserId + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    res = "t";
                    tr.Commit();
                }
                else
                {
                    res = "d";
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

        public string Update_UserGSTForm(clsUser objUser)
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
                s2 = "update UserDetail  set gstNumber='" + objUser.PanCardNo + "', gstImage='" + objUser.PanImage + "',isgstapplicable=0,isstapplicable=0  where UserId='" + objUser.UserId + "'   ";
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

        public string Update_UserCancelCheque(clsUser objUser)
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
                s2 = "update UserDetail  set CancelCheque='" + objUser.CancelCheck + "',ChequeImgStatus=0, IsChequePassbookEditabled='" + objUser.EditStatus + "'  where UserId='" + objUser.UserId + "'   ";
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


        public string Update_AddressProof(clsUser objUser)
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
                if (!string.IsNullOrEmpty(objUser.Addressproof) && !string.IsNullOrEmpty(objUser.AddressproofBack))
                {
                    string str_query = "select * from userdetail where adharnumber='" + objUser.AdhaarNo + "' ";
                    DataTable dt = null;

                    dt = ObjData.RunSelectQueryTTrans(str_query, tr);
                    if (dt.Rows.Count < 3)
                    {

                        s2 = "update UserDetail set AadharImage='" + objUser.Addressproof + "', AadharImageBack='" + objUser.AddressproofBack + "',  CityId='" + objUser.CityId + "', Pincode='" + objUser.Pincode + "', Address='" + objUser.Address + "',AreaName='" + objUser.AreaName + "', adharnumber='" + objUser.AdhaarNo + "', AadharImgStatus=0, IsAddressProofEditable ='" + objUser.EditStatus + "' where UserId='" + objUser.UserId + "'   ";

                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                        res = "t";
                        tr.Commit();
                    }
                    else
                    {
                        res = "a";
                    }
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



        public string User_Activate(clsUser objUser)
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
                s2 = "update mlmlogindetail set userstatus='1' where UserId='" + objUser.UserId + "'";
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
        public string EPinTransfer(clsUser objUser)
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
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans("select * from userdetail where userid='" + objUser.TransferUserId + "'", tr);
                if (dt.Rows.Count > 0)
                {
                    DataTable dtepin = new DataTable();
                    dtepin = ObjData.RunSelectQueryTTrans("SELECT * FROM epinmaster WHERE GenerateUserId='" + objUser.UserId + "'    AND EPinStatus='Active' and amount='" + objUser.pinamount + "'", tr);
                    int totalpins = dtepin.Rows.Count;
                    if (totalpins >= objUser.NoOfEpin)
                    {
                        for (int c = 0; c < objUser.NoOfEpin; c++)
                        {
                            s2 = " declare @id int ; set @id=(select isnull(max(id),0)+1 from EPinTransferHistory) ; insert into EPinTransferHistory ( id, EPinNo  ,UserIdFrom  ,    UserIdTo  ,MentionBy ,mentionDate ) values (@id,'" + dtepin.Rows[c]["EPinNo"].ToString() + "','" + objUser.UserId + "','" + objUser.TransferUserId + "','" + objUser.MentionBy + "',getdate() ) ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);

                            s2 = "update EPinMaster set generateuserid='" + objUser.TransferUserId + "' where EpinNo='" + dtepin.Rows[c]["EPinNo"].ToString() + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        res = "t";
                    }
                    else
                    {
                        res = "n";
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

        public string User_Deactivate(clsUser objUser)
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
                s2 = "update mlmlogindetail set userstatus='0' where UserId='" + objUser.UserId + "'";
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



        public string Update_UserProfile(clsUser objUser)
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
                if (string.IsNullOrEmpty(objUser.UpdateType))
                {
                    s2 = "update UserDetail  set IsEditable='" + objUser.EditStatus + "',username='" + objUser.UserName + "', email='" + objUser.Email + "',dateofbirth='" + objUser.DateOfBirth.ToString("yyyy/MM/dd") + "',gender='" + objUser.Gender + "' ,mobile='" + objUser.Mobile + "', address='" + objUser.Address + "', cityid='" + objUser.CityId + "',areaName='" + objUser.AreaName + "' ,pincode='" + objUser.Pincode + "',AccountHolderName='" + objUser.AccHolderName + "',AccountNo='" + objUser.AccNo + "',IFSCCode='" + objUser.IFSCCode + "',BankName='" + objUser.BankName + "',BranchName='" + objUser.BranchName + "',PanNumber='" + objUser.PanCardNo + "',adharnumber='" + objUser.AdhaarNo + "',NomineeName='" + objUser.NomineeName + "',NomineeRelation='" + objUser.NomineeRelation + "'  where UserId='" + objUser.UserId + "'   ";
                }
                else
                    if (objUser.UpdateType == "Bank")
                    {
                        s2 = "update UserDetail  set IsBankEditable='" + objUser.EditStatus + "',username='" + objUser.UserName + "', email='" + objUser.Email + "',dateofbirth='" + objUser.DateOfBirth.ToString("yyyy/MM/dd") + "',gender='" + objUser.Gender + "' ,mobile='" + objUser.Mobile + "', address='" + objUser.Address + "', cityid='" + objUser.CityId + "',areaName='" + objUser.AreaName + "' ,pincode='" + objUser.Pincode + "',AccountHolderName='" + objUser.AccHolderName + "',AccountNo='" + objUser.AccNo + "',IFSCCode='" + objUser.IFSCCode + "',BankName='" + objUser.BankName + "',BranchName='" + objUser.BranchName + "',PanNumber='" + objUser.PanCardNo + "',adharnumber='" + objUser.AdhaarNo + "',NomineeName='" + objUser.NomineeName + "',NomineeRelation='" + objUser.NomineeRelation + "'  where UserId='" + objUser.UserId + "'   ";
                    }
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
        public DataTable getawardindashboar(string UserId, string ID)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_chkaward";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserID",UserId), 
                    new SqlParameter("@ID",ID), 
                 
                  
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
        public DataTable getvacationindashboar(string UserId, string ID)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_chkvacation";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserID",UserId), 
                    new SqlParameter("@ID",ID), 
                 
                  
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
        public DataTable getTodayPerformance(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_TodayPerformance";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                    new SqlParameter("@Dt",DateTime.Now.ToString("dd/MMM/yyyy")),
                 
                  
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
        public DataTable getweeklyPerformance(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_WeekPerformance";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                    new SqlParameter("@Dt",DateTime.Now.ToString("dd/MMM/yyyy")),
                 
                  
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
        public DataTable getmonthlyPerformance(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_MonthlyPerformance";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                    new SqlParameter("@Dt",DateTime.Now.ToString("dd/MMM/yyyy")),
                 
                  
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
        public DataTable getTotalyPerformance(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_TotalPerformance";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
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
        public DataTable getPV(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_GetPV";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
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
        public DataTable getTotalyPerformancenew(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            try
            {
                s2 = "sp_performance";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
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
        public DataTable getbuissnessPerformancenew(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_TodayBuissness";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
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
        public DataTable getUserReportfortree(clsUser objUser)
        {

            string str_query = "";

            str_query = @"; WITH MyCTE
AS ( SELECT userid as EmployeeId,username as Name,case when isnull(status,0)=0 then 'Deactive' else 'Active' end as Designation,parentuserid as ReportingManager
FROM userdetail
WHERE UserId ='" + objUser.UserId + @"'
UNION ALL
SELECT userdetail.userid as EmployeeId,userdetail.username as Name,case when isnull(userdetail.status,0)=0 then 'Deactive' else 'Active' end as Designation,  userdetail.ParentUserId as ReportingManager 
FROM userdetail
INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.EmployeeId
WHERE userdetail.userid !='" + objUser.UserId + @"' )
SELECT MyCTE.*
FROM MyCTE left join userdetail ud on mycte.ReportingManager=ud.userid ";

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

        public int changeUserStatus(clsUser objUser)
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

        public int changeUserEPinStatus(clsUser objUser)
        {
            string sql = @"update UserDetail set epinGenerationStatus=(case epinGenerationStatus when 0 then 1 when 1 then 0 end) where UserId='" + objUser.UserId + "'";

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

        public DataTable getMobileChangeChargeMaster(string queryType, string charge_Id, decimal amount)
        {
            string sql = "";
            if (queryType == "Select")
                sql = @"select * from mobileChangeChargeMaster where userType='Retailer'";
            else if (queryType == "Update")
                sql = @"update mobileChangeChargeMaster set chargeAmount=" + amount + " where charge_Id=" + charge_Id;

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable I_U_S_D_MobileChangeRequest(clsUser objUser)
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
                    new SqlParameter("@chargeAmount",objUser.chargeAmount),
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

        public DataTable getUsers(string userType,string stateId)
        {
            string sql = "";
            if (userType == "Retailer")
                sql = @"select ud.*,(case when ud.activestatus=1 then 'Active' else 'Deactive' end) as AStatus from userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId where status=1 " + stateId;
            else if (userType == "Franchisee")
                sql = @"select *,(case when activestatus=1 then 'Active' else 'Deactive' end) as AStatus from franchiseedetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId where status=1" + stateId;

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
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

            string sql = @" update userdetail ";

            if (kycStatus == "Approve")
            {
                if (kycType == "Sign Up")
                    sql += " set SignUpImgStatus=1,IsSignUpFomrEditable=0 ";

                else if (kycType == "PAN")
                    sql += " set PanImgStatus=1,IsPanCardEditable=0 ";

                else if (kycType == "Cheque")
                    sql += " set ChequeImgStatus=1,IsChequePassbookEditabled=0 ";

                else if (kycType == "Aadhaar")
                    sql += " set AadharImgStatus=1,IsAddressProofEditable=0 ";

                else if (kycType == "GST")
                    sql += " set IsGstApplicable=1,IsSTApplicable=1 ";
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

                else if (kycType == "GST")
                    sql += " set IsGstApplicable=2,IsSTApplicable=1 ";
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
        public DataTable getUserwithmobileno(clsUser objUser)
        {
            string str_query = "SELECT ud.userid, ud.username,ud.mobile,ud.utilitybalance AS balance FROM userdetail ud where ud.mobile = '" + objUser.Mobile + "' ";
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


        public string Insert_User_Unverfied(clsUser objUser)
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
                s2 = "sp_add_UserDetailAuto_Unverified";
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
                        new SqlParameter("@OTPMobile",objUser.OTPMobile),
                          new SqlParameter("@OTPEMail",objUser.OTPEmail),
                };

                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
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
        public string Update_UserKYC(clsUser objUser, string type)
        {
            string res = "";
            string s2 = "";
            string str_query = "";
            DataTable dt = null;
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                switch (type)
                {
                    case "SIGNUP": s2 = "update UserDetail  set SignUpFormImage='" + objUser.Signupform + "',SignUpImgStatus=1   where UserId='" + objUser.UserId + "'   "; break;
                    case "CHEQUE": s2 = "update UserDetail  set CancelCheque='" + objUser.CancelCheck + "',ChequeImgStatus=1  where UserId='" + objUser.UserId + "'   "; break;
                    case "PAN":
                        {
                            str_query = "select * from userdetail where PanNumber='" + objUser.PanCardNo + "' ";
                            dt = null;

                            dt = ObjData.RunSelectQueryTTrans(str_query, tr);
                            if (dt.Rows.Count < 3)
                            {
                                s2 = "update UserDetail  set PanNumber='" + objUser.PanCardNo + "', PanImage='" + objUser.PanImage + "',PanImgStatus=1  where UserId='" + objUser.UserId + "'   ";
                            }
                            else
                            {
                                res = "p";
                            }
                            break;
                        }
                    case "ADHAR":
                        {
                            str_query = "select * from userdetail where AadharNo='" + objUser.AdhaarNo + "' ";
                            dt = null;

                            dt = ObjData.RunSelectQueryTTrans(str_query, tr);
                            if (dt.Rows.Count < 3)
                            {
                                s2 = "update UserDetail  set AadharImage='" + objUser.Addressproof + "',AadharImageBack='" + objUser.AddressproofBack + "',AadharNo='" + objUser.AdhaarNo + "',AadharImgStatus=1  where UserId='" + objUser.UserId + "'   ";
                            }
                            else
                            {
                                res = "a";
                            }
                            break;
                        }
                }

                if (res != "a" && res != "p")
                {
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    res = "t";
                    tr.Commit();
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
        public DataSet getUserdashboardnew(clsUser objCl)
        {
            //string str_query = "SELECT isnull(sum(Cramount),0) FROM TransactionDetail WHERE UserID='" + objCl.UserId + "'  AND TransactionType='Binary Income';SELECT isnull(sum(Cramount),0) FROM TransactionDetail WHERE UserID='" + objCl.UserId + "'  AND TransactionType='Royality Income';SELECT isnull(sum(LeftUser),0) FROM UserBuisnessVolume WHERE UserID='" + objCl.UserId + "';SELECT isnull(sum(RightUser),0) FROM UserBuisnessVolume WHERE UserID='" + objCl.UserId + "';SELECT isnull(sum(P.BuisnessVolume),0) FROM UserDetail U JOIN Planmaster P ON U.SlabID=P.Id WHERE U.SponserId='" + objCl.UserId + "'  AND U.StandingPosition='1';SELECT isnull(sum(P.BuisnessVolume),0) FROM UserDetail U JOIN Planmaster P ON U.SlabID=P.Id WHERE U.SponserId='" + objCl.UserId + "'  AND U.StandingPosition='2';";

            string str_query = "SELECT isnull(sum(Cramount),0) FROM TransactionDetail WHERE UserID='" + objCl.UserId + "'  AND TransactionType='Binary Income';SELECT isnull(sum(Cramount),0) FROM TransactionDetail WHERE UserID='" + objCl.UserId + "'  AND TransactionType='Level Income';SELECT isnull(sum(LeftUser),0) FROM UserBuisnessVolume WHERE UserID='" + objCl.UserId + "';SELECT isnull(sum(RightUser),0) FROM UserBuisnessVolume WHERE UserID='" + objCl.UserId + "';select * from dbo.sp_ChkRoyality('" + objCl.UserId + "');select isnull(sum(LeftBV),0) LeftBV, isnull(sum(RightBV),0) RightBV from UserBuisnessVolume    where userid='" + objCl.UserId + "';";


            DataSet ds = null;
            ObjData.StartConnection();
            try
            {
                ds = ObjData.RunSelectQuery(str_query);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }

        public string UpgradeUserWithEpin(clsUser objUser)
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
                s2 = "sp_upgradeUserWithEpin";
                SqlParameter[] parameter = {              
                    new SqlParameter("@ActivateUserId",objUser.TransferUserId), 
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@epin",objUser.EpinNo), 
                  
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

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
        public DataTable getUserDownlineDirect(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_getuserdownline";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
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

        public DataTable getUserDasboardproc(string UserId)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "User_Dashboard";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",UserId),



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
    }
}

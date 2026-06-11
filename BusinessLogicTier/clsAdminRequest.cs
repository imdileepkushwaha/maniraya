using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using ARA_StringHunt;
using System.Web;

namespace BusinessLogicTier
{
    public class clsAdminRequest
    {
        Data ObjData = new Data();
        public string MobileNO { get; set; }
        public string Message { get; set; }
        public string TransactionID { get; set; }
        public string Mode { get; set; }
        public int IdNo { get; set; }
        public string Port { get; set; }
        public string OperatorId { get; set; }
        public string MsgStatus = "";
        public DataSet SlectData()
        {
            DataSet dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "SelectDataData";
                SqlParameter[] parameter = {new SqlParameter("@type", ""),
                                           };
                dt = ObjData.RunDataSetProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable StatusChkUrl(int ID)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_getStatusChkUrl";
                SqlParameter[] parameter = { new SqlParameter("@ID", ID),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable Get_DisputeRequest_ByID(int ID)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_get_DisputeRequest_ById";
                SqlParameter[] parameter = { new SqlParameter("@ID", ID),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable Get_DisputeRequest(string ApiId, string OpraterId)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_get_DisputeRequestNew";
                SqlParameter[] parameter = { 
                                                new SqlParameter("@ApiId", ApiId),
                                                new SqlParameter("@OpraterId", OpraterId),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public string ApiHitResponse(int status, string ID, string LiveId, string Msg, string tranId)
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
                s2 = "SPApiResponse";
                SqlParameter[] parameter = {  
              new SqlParameter("@Status", status),
              new SqlParameter("@TID", ID),
              new SqlParameter("@LiveId", LiveId),
              new SqlParameter("@Msg", Msg),
                };
                ObjData.RunInsUpDelQueryTransProc(s2, tr, parameter);
                res = "1";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public String UpdateRecharge_Reject(int ID, string Remark)
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
                s2 = "sp_Recharge_Reject";
                SqlParameter[] parameter = {  
                new SqlParameter("@ID", ID),
                new SqlParameter("@Remark", Remark),
                                           };
                ObjData.RunInsUpDelQueryTransProc(s2, tr, parameter);
                res = "1";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public void InvalidMessage(string UMobileNo, string Message, int IdNo, string PTransactionID, string UserId, string OTPFlag = "")
        {       
            try
            {

                this.MobileNO = UMobileNo;
                this.Message = Message;
                this.IdNo = IdNo;
                this.TransactionID = PTransactionID;

                this.GetUrl(Convert.ToString(UserId), OTPFlag);
            }
            catch (Exception ex)
            {
              
            }
           
           
        }
        public void GetUrl(string userid, string OtpFlag = "")
        {
            try
            {
                if (Mode != null)
                {
                    if (Mode.ToUpper() == "SMS")
                    {
                        MsgStatus = "Request Sent";
                    }
                    else if (Mode.ToUpper() == "APPS")
                    {
                        MsgStatus = "WR";
                    }
                }
                else
                {
                    MsgStatus = "Request Sent";
                }
                String Result = "";
                DataTable dt = new DataTable();
                dt = GetSmsApi_Active(userid, OtpFlag);
                if (IdNo == null || IdNo == 0)
                    IdNo = 0;
                String ApiName = dt.Rows[0]["api"].ToString();
                String Url = dt.Rows[0]["Url"].ToString();
                Url = Url.Replace("mmm", MobileNO);
                Url = Url.Replace("ttt", Message);
                if (ApiName != "SRS")
                {
                    if (Url == "")
                    {
                        Result = "SMS API Not Found";
                    }
                    else
                    {
                        Result = Url.CallURL();
                    }
                }
                else
                {
                    Result = "SMS Switch to SRS";
                }
                if (TransactionID == null) TransactionID = ""; if (IdNo == null) IdNo = 0;
                Insert_sendsms(MobileNO, Result, ApiName, Message, MsgStatus, TransactionID, "", IdNo);
            }
            catch (Exception ex)
            { }

        }
        public DataTable GetSmsApi_Active(string UserId, string OtpFlag = "")
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string websiteId = "";

                if (HttpContext.Current.Session == null || HttpContext.Current.Session["WebsiteId"] == null)
                {
                    websiteId = UserId;
                }
                else { websiteId = (string)HttpContext.Current.Session["WebsiteId"]; }
                string s2 = "sp_GetActive_smsapi";
                SqlParameter[] parameter = {
                                               new SqlParameter("@UserId", websiteId),
                                               new SqlParameter("@OTPFlag", OtpFlag),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public String Insert_sendsms(String MobileNo, String Result, String Api, String Message, String Status, String TransactionID, String Sts, Int32 IdNo)
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
                s2 = "sp_Recharge_Reject";
                SqlParameter[] parameter = {  
                
                   new SqlParameter("@MobileNo", MobileNo),
                   new SqlParameter("@Result", Result),
                   new SqlParameter("@Api", Api),
                   new SqlParameter("@Message", Message),
                   new SqlParameter("@Status", Status),
                   new SqlParameter("@TransactionID", TransactionID),
                   new SqlParameter("@Sts", Sts),
                   new SqlParameter("@IdNo", IdNo),
                                           };
                ObjData.RunInsUpDelQueryTransProc(s2, tr, parameter);
                res = "1";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
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

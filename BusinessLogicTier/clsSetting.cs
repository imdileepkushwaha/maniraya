using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
    public class clsSetting
    {
        Data ObjData = new Data();
        public DataTable GetIP()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_Select_IP";
                SqlParameter[] parameter = {
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
        public DataTable GetIP_ById(Int32 Id)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_IP_byId";
                SqlParameter[] parameter = {
                                             new SqlParameter("@Id", Id),
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
        public DataTable GetEmailSettings()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_Email_Settings";
                SqlParameter[] parameter = { };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable GetEmailSetting_ById(Int32 ID)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_Email_Settings_ByID";
                SqlParameter[] parameter = { new SqlParameter("@ID", ID), };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable Get_Message_All_Format()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetFundTransfer_Formats";
                SqlParameter[] parameter = {  };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public String UpdateStatus(String Status, String ModifyBy, String Id)
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
                s2 = "sp_update_status_IP";
                SqlParameter[] parameter = {              
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@ModifyBY", ModifyBy),
                    new SqlParameter("@Id", Id),
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
        public String Delete_ById(string Id)
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
                s2 = "sp_delete_IP";
                SqlParameter[] parameter = {  
                    new SqlParameter("@Id", Id),
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
        public DataTable GetSystemSettings()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetSystemSettings";
                SqlParameter[] parameter = { };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public String Insert_IP(String IP, String IPType, string UserID, string EntryBy)
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
                s2 = "sp_Insert_IP";
                SqlParameter[] parameter = {              
                    new SqlParameter("@IP", IP),
                    new SqlParameter("@IPType", IPType),
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@EntryBy", EntryBy),
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
        public String Update_IP(String IP, String IPType, string UserID, string ModifyBy, int Id)
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
                s2 = "sp_Update_IP";
                SqlParameter[] parameter = {              
                    new SqlParameter("@IP", IP),
                    new SqlParameter("@IPType", IPType),
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@ModifyBy", ModifyBy),
                    new SqlParameter("@Id", Id),
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
        public String Insert_SystemSettings(int IntervalBtRecharge, int IntervalBtTransfer, int MinFundTransfer, int MaxFundTransfer, int BackDate, string EntryBy, string DMRVerification,string appkey)
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
                s2 = "sp_InsertSystemSettings";
                SqlParameter[] parameter = {              
                    new SqlParameter("@IntervalBtRecharge", IntervalBtRecharge),
                    new SqlParameter("@IntervalBtTransfer", IntervalBtTransfer),
                    new SqlParameter("@MinFundTransfer", MinFundTransfer),
                    new SqlParameter("@MaxFundTransfer", MaxFundTransfer),
                    new SqlParameter("@BackDate", BackDate),
                    new SqlParameter("@EntryBy", EntryBy),
                    new SqlParameter("@DMRVerification", Convert.ToInt32( DMRVerification)),
                      new SqlParameter("@appkey",appkey),

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

        public DataTable Update_Push(string key, string strtype)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_KeyNotification";
                SqlParameter[] parameter = { 
                                             new SqlParameter("@App_Key", key),
                    new SqlParameter("@Type", strtype),};
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public String DeleteEmailSetting_ById(Int32 Id)
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
                s2 = "sp_delete_Email_Settings_ByID";
                SqlParameter[] parameter = {  
                    new SqlParameter("@ID", Id),
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
      
        public String Update_EmailSettings(String FromEmail, String Password, String HostName, Int32 Port, String EmailUserId, string ID)
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
                s2 = "sp_Update_EmailSettings";
                SqlParameter[] parameter = {  
                new SqlParameter("@FromEmail", FromEmail),
                new SqlParameter("@Password", Password),
                new SqlParameter("@HostName", HostName),
                new SqlParameter("@Port", Port),
                new SqlParameter("@EmailUserId", EmailUserId),
                new SqlParameter("@ID", ID),
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
        public String Insert_Message_Formats(String Registration, String FundTransfer, String FundReceive, String FundDebit, String FundCredit, String RechargeAccept, String RechargeSuccess, String RechargeFailed, string EntryBy, string RechAcc, string RechSucc, string RechFail, string OperatorUPMessage, string OperatorDownMessage, string RechRefund, string RechargeRefund)
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
                s2 = "sp_Insert_MessageFormat";
                SqlParameter[] parameter = {  
                new SqlParameter("@Registration", Registration),
                new SqlParameter("@FundTransfer", FundTransfer),
                new SqlParameter("@FundReceive", FundReceive),
                new SqlParameter("@FundDebit", FundDebit),
                new SqlParameter("@FundCredit", FundCredit),
                new SqlParameter("@RechargeAccept", RechargeAccept),
                new SqlParameter("@RechargeSuccess", RechargeSuccess),
                new SqlParameter("@RechargeFailed", RechargeFailed),
                new SqlParameter("@EntryBy", EntryBy),
                new SqlParameter("@RechAcc", RechAcc),
                new SqlParameter("@RechSucc", RechSucc),
                new SqlParameter("@RechFail", RechFail),
                new SqlParameter("@OperatorUPMessage", OperatorUPMessage),
                new SqlParameter("@OperatorDownMessage", OperatorDownMessage),
                new SqlParameter("@RechRefund", RechRefund),
                new SqlParameter("@RechargeRefund", RechargeRefund),
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

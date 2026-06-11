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
    public class clsAPI
    {
        Data ObjData = new Data();
        public DataTable GetSmsApi(string UserId)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_Select_sms_api";
                SqlParameter[] parameter = {
                                             new SqlParameter("@UserId",UserId),
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
        public DataTable GetSmsApi_ById(Int32 Id)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_sms_api_byId";
                SqlParameter[] parameter = {
                                             new SqlParameter("@Id",Id),
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
        public DataTable GetSmsApi_true(string UserId)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_smsapi_true";
                SqlParameter[] parameter = {
                                             new SqlParameter("@UserId",UserId),
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
        public DataTable Get_UserSMS(string UserId)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetUserSMS";
                SqlParameter[] parameter = {};
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
                s2 = "sp_update_status_smsapi";
                SqlParameter[] parameter = {              
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@ModifyBY", ModifyBy),
                    new SqlParameter("@Id", Id),
                };
                ObjData.RunInsUpDelQueryTransProc(s2,tr, parameter);
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
        public String UpdateStatus_final(String Status, String ModifyBy, String Id, string UserId)
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
                s2 = "sp_update_status_smsapi_final";
                SqlParameter[] parameter = {              
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@ModifyBY", ModifyBy),
                    new SqlParameter("@Id", Id),
                    new SqlParameter("@UserId", UserId),
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
        public String InsertSmsApi(String Name, String Url, String EntryBy, string UserId, int DefaultType)
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
                s2 = "sp_Insert_sms_api";
                SqlParameter[] parameter = {  
                new SqlParameter("@Name", Name),
                new SqlParameter("@Url", Url),
                new SqlParameter("@EntryBy", EntryBy),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@DefaultType", DefaultType),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = dtresult.Rows[0][0].ToString();
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "Error: ";
                res += ex.Message;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public String UpdateSmsApi(String Name, String Url, String ModifyBy, String Id, string UserId, int DefaultType)
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
                s2 = "sp_update_sms_api";
                SqlParameter[] parameter = {  
                new SqlParameter("@Name", Name),
                new SqlParameter("@Url", Url),
                new SqlParameter("@ModifyBY", ModifyBy),
                new SqlParameter("@Id", Id),
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@DefaultType", DefaultType),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = dtresult.Rows[0][0].ToString();
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "Error: ";
                res += ex.Message;
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

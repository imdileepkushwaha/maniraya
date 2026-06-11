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
    public class clsRechargeAPI
    {
        Data ObjData = new Data();
        public DataTable GetRechargeApi()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_Select_Recharge_api";
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
        public DataTable Get_Operator()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_get_Operator";
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
        public DataTable Get_Recharge_API()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_getRechargeApi_Active";
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
        public DataTable Get_Operator_API(string OperatorName = "")
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_getOpApi";
                SqlParameter[] parameter = { new SqlParameter("@OperatorName", OperatorName + "%"), };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataSet Get_ApiSwitching(int opid)
        {
            DataSet dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_ApiSwitching";
                SqlParameter[] parameter = { new SqlParameter("@opid", opid), };
                dt = ObjData.RunDataSetProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable GetRechargeApi_ById(Int32 Id)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_select_Recharge_api_byId";
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
        public DataTable Get_Operator_API_Commission_ByOpid(Int32 OperatorId, Int32 Apid)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_Get_Operator_API_Commission_byOpid";
                SqlParameter[] parameter = {
                                             new SqlParameter("@OperatorId", OperatorId),
                                             new SqlParameter("@Apid", Apid),
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
                s2 = "sp_update_status_RechargeApi";
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

        public String InsertRechargeApi(String Name, String Url, String EntryBy, String StatusName, String StatusValue, String ApiOpCode, String VenderId, String Type, String StatusCheckUrl, string BalanceUrl, string DefaultUrl, string Errors, string txtoperatortype, String status)
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
                s2 = "sp_Insert_Recharge_api";
                SqlParameter[] parameter = {               
                new SqlParameter("@Name", Name),
                new SqlParameter("@Url", Url),
                new SqlParameter("@EntryBy", EntryBy),
                new SqlParameter("@StatusName", StatusName),
                new SqlParameter("@StatusValue", StatusValue),
                new SqlParameter("@ApiOpCode", ApiOpCode),
                new SqlParameter("@VenderId", VenderId),
                new SqlParameter("@Type", Type),
                new SqlParameter("@StatusCheckUrl", StatusCheckUrl),
                new SqlParameter("@BalanceUrl", BalanceUrl),
                new SqlParameter("@DefaultUrl", DefaultUrl),
                new SqlParameter("@Errors", Errors),
                new SqlParameter("@operatortype", txtoperatortype),
                 new SqlParameter("@Status", status),
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
        public String UpdateRechargeApi(String Name, String Url, String ModifyBy, String Id, String StatusName, String StatusValue, String ApiOpCode, String VenderId, String Type, String StatusCheckUrl, string BalanceUrl, string DefaultUrl, string Errors, string txtoperatortype, String status)
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
                s2 = "sp_update_recharge_api";
                SqlParameter[] parameter = {  
                new SqlParameter("@Name", Name),
                new SqlParameter("@Url", Url),
                new SqlParameter("@ModifyBY", ModifyBy),
                new SqlParameter("@Id", Id),
                new SqlParameter("@StatusName", StatusName),
                new SqlParameter("@StatusValue", StatusValue),
                new SqlParameter("@ApiOpCode", ApiOpCode),
                new SqlParameter("@VenderId", VenderId),
                new SqlParameter("@Type", Type),
                new SqlParameter("@StatusCheckUrl", StatusCheckUrl),
                new SqlParameter("@BalanceUrl", BalanceUrl),
                new SqlParameter("@DefaultUrl", DefaultUrl),
                new SqlParameter("@Errors", Errors),
                new SqlParameter("@operatortype", txtoperatortype),
                  new SqlParameter("@Status", status),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = "1";
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
        public string Insert_ApiSwitching(DataTable dtsave)
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
                s2 = "sp_Insert_ApiSwitching";
                SqlParameter[] parameter = {  
                new SqlParameter("@dtsave", dtsave),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = "1";
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
        public string Insert_OperatorApiCommission(DataTable dtsave)
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
                s2 = "sp_Insert_OperatorApiCommission";
                SqlParameter[] parameter = {  
                new SqlParameter("@dtsave", dtsave),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = "1";
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
        public DataTable FetchData(string statement)
        {
         
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(statement);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
    }
}

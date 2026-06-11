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
    public class clsOperator
    {
        Data ObjData = new Data();
       

        public DataTable GetAllOperator()
        {
            string str_query = "select oct.Type as TypeName,oc.* from OperatorCode oc inner join OperatorCodeType oct on oc.Type=oct.typeid order by oc.Type,Operator";

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
        public DataTable GetAllOperatorEntryBy(string EntryBy)
        {
            string str_query = "select * from OperatorCode order by Operator desc where EntryBy=" + EntryBy + "";

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
        public DataTable GetOperatorCodeType()
        {
            string str_query = "select * from OperatorCodeType order by Type desc";

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
        public DataTable GetSelectedOperator(string Id)
        {
            string str_query = "select * from OperatorCode where ID=" + Id + "";

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
        public DataTable selectOpertorWithOption()
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "SelectOpertorOption";
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
        public DataTable Get_Operator_API_Code_ByOpid(Int32 OperatorId, Int32 Apid)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_Get_Operator_API_Code_byOpid";
                SqlParameter[] parameter = {              
                    new SqlParameter("@OperatorId",OperatorId),
                    new SqlParameter("@Apid",Apid),
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
        public string GetFromDatatable(DataTable datatable, string column)
        {
            return datatable.Rows[0][column].ToString();
        }
        public String InsertOrEditOperator(String Id, String Operator, String OPID, String Type, String Length, String Minimum, String Maximum, String DisplayName, String DisplayNote, string AccountDisplay, string LocationDisplay, string By, string StartsWith, string DisabledReasion,string status)
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
                string str_paramname = "";
                s2 = "";
                if (Id == "-1")
                {
                    s2 = "AddOperator";
                    str_paramname = "@EntryBy";
                }
                else
                {
                    s2 = "EditOperator";
                    str_paramname = "@ModifyBy";
                }


                SqlParameter[] parameter = {  
                new SqlParameter("@Id", Id),
                new SqlParameter("@Operator", Operator),
                new SqlParameter("@OPID", OPID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Length", Length),
                new SqlParameter("@Minimum", Minimum),
                new SqlParameter("@Maximum", Maximum),
                new SqlParameter("@DisplayName", DisplayName),
                new SqlParameter("@DisplayNote", DisplayNote),
                new SqlParameter("@AccountDisplay", AccountDisplay),
                new SqlParameter("@LocationDisplay", LocationDisplay),
                new SqlParameter(str_paramname, By),
                new SqlParameter("@StartsWith", StartsWith),
                new SqlParameter("@DisabledReasion", DisabledReasion),
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
        public string Insert_OperatorApiCode(DataTable dtsave)
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
                s2 = "sp_Insert_OperatorApiCode";
                SqlParameter[] parameter = {              
                    new SqlParameter("@dtsave", dtsave)
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
        public DataTable OpertorOption(string OperatorCodeId, string replace1, string replace2, string replace3, string replace4, string Note1, string Note2, string Note3, string Note4)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable dtresult = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "insertOperator";
                SqlParameter[] parameter = {            
                new SqlParameter("@OperatorCodeId", OperatorCodeId),
                new SqlParameter("@replace1", replace1),
                new SqlParameter("@replace2", replace2),
                new SqlParameter("@replace3", replace3),
                new SqlParameter("@replace4", replace4),
                new SqlParameter("@Note1", Note1),
                new SqlParameter("@Note2", Note2),
                new SqlParameter("@Note3", Note3),
                new SqlParameter("@Note4", Note4),
                };
               
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
               
                tr.Commit();
            }
            catch (Exception ex)
            {
                dtresult = null;
                res = ex.Message.ToString();
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return dtresult;
        }
    }
}

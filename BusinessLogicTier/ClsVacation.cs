using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLogicTier
{
    public class ClsVacation
    {
        Data ObjData = new Data();
        public string vacationId { get; set; }
        public string vacationName { get; set; }
        public string Fromdate { get; set; }
        public string todate { get; set; }
        public string targetleft { get; set; }
        public string targetright { get; set; }

        public DataTable getvacationDetail()
        {
            string str_query = "select *,Convert(char,FromDate,106) as Fromdate1,Convert(char,toDate,106) as todate1 from vacationMaster order by Id";

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
        public DataTable getvacationDetailfromdashboard()
        {
            string str_query = "select *,Convert(char,FromDate,106) as Fromdate1,Convert(char,toDate,106) as todate1,'' as CurrentLeft,'' as CurrentRight,'' as RequiredLeft,'' as RequiredRight,'' Status from vacationMaster order by Id";

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
        public string Insert_vacation(ClsVacation objBank)
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
                s2 = "sp_add_vacation";
                SqlParameter[] parameter = {  
                new SqlParameter("@vacationName",objBank.vacationName), 
                new SqlParameter("@FromDate",objBank.Fromdate), 
                new SqlParameter("@ToDate",objBank.todate), 
                new SqlParameter("@TargetLeft",objBank.targetleft), 
                new SqlParameter("@TargetRight",objBank.targetright),                
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
        public string Update_vacation(ClsVacation objBank)
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
                s2 = "update vacationmaster set targetleft='" + objBank.targetleft + "',targetright='" + objBank.targetright + "',fromdate='" + Convert.ToDateTime(objBank.Fromdate).ToString("dd/MMM/yyyy") + "',todate='" + Convert.ToDateTime(objBank.todate).ToString("dd/MMM/yyyy") + "',vacationname='" + objBank.vacationName + "' where id='" + objBank.vacationId + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "1";
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
    }
}

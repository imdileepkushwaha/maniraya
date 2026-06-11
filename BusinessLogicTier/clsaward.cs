using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
    public class clsaward
    {
        Data ObjData = new Data();
        public string AwardId {get;set;}
        public string awardName { get; set; }
        public string Fromdate { get; set; }
        public string todate { get; set; }
        public string targetleft { get; set; }
        public string targetright { get; set; }

        public DataTable getawardDetail()
        {
            string str_query = "select *,Convert(char,FromDate,106) as Fromdate1,Convert(char,toDate,106) as todate1 from awardMaster order by Id";

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
        public DataTable getawardDetailfromdashboard()
        {
            string str_query = "select *,Convert(char,FromDate,106) as Fromdate1,Convert(char,toDate,106) as todate1,'' as CurrentLeft,'' as CurrentRight,'' as RequiredLeft,'' as RequiredRight,'' Status from awardMaster order by Id";

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
        public string Insert_Award(clsaward objBank)
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
                s2 = "sp_add_Award";
                SqlParameter[] parameter = {  
                new SqlParameter("@AwardName",objBank.awardName), 
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
        public string Update_Award(clsaward objBank)
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
                s2 = "update awardmaster set targetleft='" + objBank.targetleft + "',targetright='" + objBank.targetright + "',fromdate='" + Convert.ToDateTime(objBank.Fromdate).ToString("dd/MMM/yyyy") + "',todate='" + Convert.ToDateTime(objBank.todate).ToString("dd/MMM/yyyy") + "',awardname='" + objBank.awardName + "' where id='" + objBank.AwardId + "'";
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

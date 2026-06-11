using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataTier;

namespace BusinessLogicTier
{
    public class clsAd
    {
        Data ObjData = new Data();
        public string AdId { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string PANCARd { get; set; }
        public string CItyId { get; set; }
        public decimal AdAmount { get; set; }
        public string MentionBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DataTable getAdBookingRequest(clsAd objad)
        {
            string str_query = "select ab.*,cm.cityname,ud.username from AdBookingRequest ab left join citymaster cm on ab.cityid=cm.cityid left join userdetail ud on ab.userid=ud.userid where 1=1 ";

            if (objad.FromDate != DateTime.MinValue && objad.ToDate != DateTime.MinValue)
            {
                str_query += "  and ab.MentionDate  >= '" + objad.FromDate + "'   and ab.MentionDate   <= '" + objad.ToDate + "' ";
            }
            if (objad.UserId != "")
            {
                str_query += "  and ab.UserId = '" + objad.UserId + "' ";
            }
            str_query += " order by ab.MentionDate  desc";

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
        public string Insert_AdBookingRequest(clsAd objad)
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

                s2 = "sp_add_AdBookingRequest";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objad.UserId), 
                    new SqlParameter("@MobileNo",objad.MobileNo), 
                    new SqlParameter("@AccountNo",objad.AccountNo), 
                    new SqlParameter("@IFSCCode",objad.IFSCCode), 
                    new SqlParameter("@PANCard",objad.PANCARd), 
                    new SqlParameter("@CityId",objad.CItyId), 
                    new SqlParameter("@AdAmount",objad.AdAmount),
                    new SqlParameter("@MentionBy",objad.MentionBy),
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

    }
}

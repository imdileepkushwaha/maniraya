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
    public class clsCoupon
    {
        Data ObjData = new Data();

        public DataTable getCouponDetail(string id)
        {
            //string str_query = " SELECT t.*,h.userid,h.entrydate AS assigndate,h.couponstatus,(CASE WHEN h.couponstatus IS NULL THEN (case when t.status is null then 'UnAssigned' when t.status=0 then 'UnUsed' when t.status=1 then 'Used' else 'Cancelled' end) WHEN h.couponstatus=1 THEN 'Used' WHEN h.couponstatus=2 THEN 'Cancelled' ELSE 'UnUsed' end) AS astatus,h.purchaseId FROM tbl_Coupon t LEFT JOIN tbl_CouponAssignment h ON t.id=h.couponid where t.id=" + (id == null ? "t.id" : id) + " ORDER BY t.id desc ";
            string str_query = " SELECT t.*,t.entrydate AS assigndate,(case when t.status is null or t.status=1 then 'Active' when t.status=0 then 'Deactive' end)  AS astatus FROM tbl_Coupon t where t.id=" + (id == null ? "t.id" : id) + " ORDER BY t.id desc ";

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


        public string Insert_Coupon(decimal amount, DateTime fdate, DateTime tdate, decimal minamount, string EntryUser,int id,int status,string cname,string query)
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
                //string code = GetCouponCode().ToUpper();
                s2 = "sp_CouponAdd";
                SqlParameter[] parameter = {  
                new SqlParameter("@id",(id == 0? null : id.ToString())), 
                new SqlParameter("@amount",amount), 
                new SqlParameter("@fdate",fdate),
                new SqlParameter("@tdate",tdate),
                new SqlParameter("@minamount",minamount),
                new SqlParameter("@EntryUser",EntryUser),
                new SqlParameter("@CouponName",cname),
                new SqlParameter("@status",status),
                new SqlParameter("@query",query),
                };
                int c = ObjData.RunInsUpDelQueryTransProcNew(s2, tr, parameter);
                if (c >= 1)
                {
                    res = "t";
                    tr.Commit();
                }
                else
                {
                    res = "f";
                    tr.Rollback();
                }
            }
            catch (Exception ex)
            {
                res = "f";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

        public string GetCouponCode()
        {
            string res = "";
            res = GenerateRandomAlphanumericCode(6);
            return res;
        }


        public string GenerateRandomAlphanumericCode(int size)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, size).Select(x => input[rand.Next(0, input.Length)]).ToArray());
        }



        public DataTable getCoupon()
        {
            string str_query = " SELECT * from tbl_Coupon order by entrydate desc ";
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





        public DataTable GetUserCouponCode(string userid,decimal amount)
        {
            DataTable dt = new DataTable();
            string res = "";
            try
            {
                ObjData.StartConnection();
                res = "  SELECT (cast(t.Code as varchar(50)) + '_' + cast(h.amount as varchar(50))) as tcouponid,t.couponid, (t.Code + ' ('+ cast(h.amount as varchar(50)) + ')') as tcode,t.Code,h.amount,t.couponstatus FROM  tbl_CouponAssignment t JOIN tbl_Coupon h ON t.couponid=h.id WHERE userid='" + userid + "' AND cast(getdate() AS date)>=CAST(h.fromdate AS DATE) AND cast(getdate() AS date)<=CAST(h.todate AS DATE) AND t.couponstatus=0 and h.minshopamount<=" + amount;
                dt = ObjData.RunDataTable(res);
            }
            catch (Exception er)
            {
                dt = null;
            }
            finally
            {
                ObjData.EndConnection();
            }
            return dt;
        }



        public DataTable I_D_CouponAssignment(int id, int CouponId,string userid, string query)
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
                string code = GetCouponCode().ToUpper();
                s2 = "sp_CouponAssignment";
                SqlParameter[] parameter = {  
                new SqlParameter("@id",(id == 0? null : id.ToString())), 
                new SqlParameter("@CouponId",CouponId), 
                new SqlParameter("@Code",code),
                new SqlParameter("@UserId",userid),
                new SqlParameter("@query",query),
                };
                dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        res = "t";
                        tr.Commit();
                    }
                    else
                        {
                            res = "f";
                            tr.Rollback();
                        }
                }
                else
                {
                    res = "f";
                    tr.Rollback();
                }
            }
            catch (Exception ex)
            {
                res = "f";
                dt = null;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return dt;
        }

        public DataTable getCouponAssignedDetail(string userid)
        {
            string str_query = " DECLARE @purchasedetail AS TABLE(transactionId INT, TRANSACTIONCode VARCHAR(50));";
                  str_query += " INSERT INTO @purchasedetail SELECT transactionId,TRANSACTIONCode FROM PurchaseProductMaster; ";
                  str_query += "SELECT p.TRANSACTIONCode AS purchaseId,a.id,b.Code AS CouponName,b.fromdate,b.todate,b.amount,b.minshopamount,a.userid,a.Code,a.entrydate,a.purchaseId,(CASE WHEN b.todate>getdate() THEN 'Expired' WHEN isnull(a.couponstatus,0)=0 THEN 'Unused' WHEN isnull(a.couponstatus,0)=1 THEN 'Used' end) AS Status,u.UserName FROM tbl_CouponAssignment a LEFT JOIN tbl_coupon b ON a.couponid=b.id LEFT JOIN userdetail u ON a.userid=u.UserId LEFT JOIN @purchasedetail p ON isnull(a.purchaseId,0)=p.transactionId where a.userid=" + (string.IsNullOrEmpty(userid) ? "a.userid" : "'"+userid+"'") + " ORDER BY a.id desc ";

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

    }
}

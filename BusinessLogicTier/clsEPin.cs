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
    public class clsEPin
    {
        Data ObjData = new Data();
        public string EPinNo { get; set; }
        public string GenerateUserId { get; set; }
        public string UsedUserId { get; set; }
        public int NoOfEPins { get; set; }
        public string SMSStatus { get; set; }
        public string EPinStatus { get; set; }
        public string MentionBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string RequestId { get; set; }
        public string plananame { get; set; }
        public decimal Amount { get; set; }
        public string planid { get; set; }

        public DataTable getEPin(clsEPin objEPin)
        {
            string str_query = "select em.* from EPinMaster  em   where 1=1 ";


            if (objEPin.FromDate != DateTime.MinValue && objEPin.ToDate != DateTime.MinValue)
            {
                str_query += "  and em.mentiondate  >= '" + objEPin.FromDate + "'   and em.mentiondate   <= '" + objEPin.ToDate + "' ";
            }
           

            if (objEPin.EPinStatus != "0")
            {
                str_query += "  and em.epinstatus = '" + objEPin.EPinStatus + "' ";
            }

            if (objEPin.GenerateUserId != "")
            {
                str_query += "  and em.GenerateUserId = '" + objEPin.GenerateUserId + "' ";
            }

            if (objEPin.UsedUserId != "")
            {
                str_query += "  and em.UsedUserId = '" + objEPin.UsedUserId + "' ";
            }

            str_query += " order by em.mentiondate  desc";



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


        public DataTable getEPinRequest(clsEPin objEPin)
        {
            string str_query = "select em.*,pm.planname from EPinRequest  em  left join PlanMaster pm on em.PlanId=pm.PlanId where 1=1 ";


            if (objEPin.FromDate != DateTime.MinValue && objEPin.ToDate != DateTime.MinValue)
            {
                str_query += "  and em.mentiondate  >= '" + objEPin.FromDate + "'   and em.mentiondate   <= '" + objEPin.ToDate + "' ";
            }

           

            if (objEPin.EPinStatus != "0")
            {
                str_query += "  and em.status = '" + objEPin.EPinStatus + "' ";
            }

            if (objEPin.GenerateUserId != "")
            {
                str_query += "  and em.UserId = '" + objEPin.GenerateUserId + "' ";
            }


            str_query += " order by em.mentiondate  desc";



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


        public DataTable getEPinForReg(clsEPin objEPin)
        {
            string str_query = "select * from EPinMaster where  GenerateUserId = '" + objEPin.GenerateUserId + "' and EpinStatus='Active'  ";
            str_query += " order by mentiondate  desc";



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
        public DataTable getEPinForRegamount(clsEPin objEPin)
        {
            string str_query = "select * from EPinMaster where  GenerateUserId = '" + objEPin.GenerateUserId + "' and EpinStatus='Active' and amount='"+objEPin.Amount+"'  ";
            str_query += " order by mentiondate  desc";



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
        public DataTable getEPinForRegnew(clsEPin objEPin)
        {
            string str_query = "select DISTINCT amount from EPinMaster where  GenerateUserId = '" + objEPin.GenerateUserId + "' and EpinStatus='Active'  ";
            //str_query += " order by mentiondate  desc";



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
        public DataTable getEPinForGenerateepin(clsEPin objEPin)
        {
            //string str_query = "SELECT (Planname+'('+Rtrim(Convert(CHAR,Planamount))+')')AS [plan],Planamount FROM PlanMaster ";
            //str_query += " order by mentiondate  desc";

            string str_query = "SELECT id, Planname, Planamount FROM PlanMaster where 1=1 ";
           // str_query += " and PlanName Like 'Joining package%' ";

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
        public DataTable getEPinForGenerateepinnew(clsEPin objEPin)
        {
            //string str_query = "SELECT (Planname+'('+Rtrim(Convert(CHAR,Planamount))+')')AS [plan],Planamount FROM PlanMaster ";
            //str_query += " order by mentiondate  desc";

            string str_query = "SELECT id, Planname, Planamount FROM PlanMaster where id<>7 ";
            // str_query += " and PlanName Like 'Joining package%' ";

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
        public DataTable getEPinForPinTransfer(clsEPin objEPin)
        {
            //string str_query = "SELECT (Planname+'('+Rtrim(Convert(CHAR,Planamount))+')')AS [plan],Planamount FROM PlanMaster ";
            //str_query += " order by mentiondate  desc";

            string str_query = "SELECT id, Planname, Planamount FROM PlanMaster  where  1=1 ";
            str_query += " and PlanName Like 'Joining package%' ";
            str_query += " and PlanName != 'Joining package (0)' ";

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
        public DataTable getEPinForPinTransferadmin(clsEPin objEPin)
        {
            //string str_query = "SELECT (Planname+'('+Rtrim(Convert(CHAR,Planamount))+')')AS [plan],Planamount FROM PlanMaster ";
            //str_query += " order by mentiondate  desc";

            string str_query = "SELECT id, Planname, Planamount FROM PlanMaster where 1=1 ";
            str_query += " and PlanName Like 'Joining package%' ";

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
        public DataTable getEPinFullDetail(clsEPin objEPin)
        {
            string str_query = "select * from EPinMaster where  epinno = '" + objEPin.EPinNo + "'  ";
            str_query += " order by mentiondate  desc";
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
        public DataTable getTotalAvailableEPin(clsEPin objEPin)
        {
            string str_query = "select count(*) from EPinMaster where  GenerateUserId = '" + objEPin.GenerateUserId + "' and EpinStatus='Active'  ";



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
        public DataTable getTotalAvailableEPinnew(clsEPin objEPin)
        {
            string str_query = "select count(*) from EPinMaster where  GenerateUserId = '" + objEPin.GenerateUserId + "' and EpinStatus='Active'  and amount='"+objEPin.Amount+"' ";



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

        public string Insert_EPin(clsEPin objEPin)
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
                s2 = "sp_add_EPinMaster";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@NoOfEPin",objEPin.NoOfEPins), 
                new SqlParameter("@GenerateUserId",objEPin.GenerateUserId), 
                new SqlParameter("@Amount",objEPin.Amount),
                 new SqlParameter("@planname",objEPin.plananame),
                new SqlParameter("@MentionBy",objEPin.MentionBy),
                 new SqlParameter("@planid",objEPin.planid)
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
        public string Insert_EPinfranchisee(clsEPin objEPin)
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
                s2 = "sp_add_EPinMasterfranchisee";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@NoOfEPin",objEPin.NoOfEPins), 
                new SqlParameter("@GenerateUserId",objEPin.GenerateUserId), 
                new SqlParameter("@Amount",objEPin.Amount),
                 new SqlParameter("@planname",objEPin.plananame),
                new SqlParameter("@MentionBy",objEPin.MentionBy),
                 new SqlParameter("@planid",objEPin.planid)
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
        public string Insert_EPinUser(clsEPin objEPin)
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
                s2 = "sp_add_EPinMasterUser";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@NoOfEPin",objEPin.NoOfEPins), 
                new SqlParameter("@GenerateUserId",objEPin.GenerateUserId), 
                new SqlParameter("@Amount",objEPin.Amount), 
                new SqlParameter("@MentionBy",objEPin.MentionBy),
                  new SqlParameter("@PlanId",objEPin.planid)
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

        public string Insert_EPinRequest(clsEPin objEPin)
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
                s2 = "sp_addEPinRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objEPin.GenerateUserId), 
                new SqlParameter("@NoOfPin",objEPin.NoOfEPins), 
                new SqlParameter("@Amount",objEPin.Amount), 
                new SqlParameter("@MentionBy",objEPin.MentionBy)
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


        public string Approve_EPinRequest(clsEPin objEPin)
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
                s2 = "sp_Approve_EPinMaster";
                SqlParameter[] parameter = {
                new SqlParameter("@RequestId",objEPin.RequestId),                              
                new SqlParameter("@GenerateUserId",objEPin.GenerateUserId), 
                new SqlParameter("@NoOfEPin",objEPin.NoOfEPins), 
                new SqlParameter("@Amount",objEPin.Amount), 
                new SqlParameter("@MentionBy",objEPin.MentionBy)
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

        public DataTable getEPinToUpgradeUser(clsEPin objEPin)
        {
            //string str_query = "SELECT (Planname+'('+Rtrim(Convert(CHAR,Planamount))+')')AS [plan],Planamount FROM PlanMaster ";
            //str_query += " order by mentiondate  desc";

            string str_query = "SELECT Planname, Planamount FROM PlanMaster where 1=1 ";
            str_query += " and PlanName Like 'Upgrade package%' ";

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
   public  class clsplan
    {
       Data ObjData = new Data();
       public string PlanName { get; set; }
       public string id { get; set; }
       public int PlanAmount { get; set; }
       public int BuisnessVolume { get; set; }
       public string operatorPermission { get; set; }
       public string MonthlyAmount { get; set; }
       public string CountMonthly { get; set; }
       public string MoneyTransfer { get; set; }

       public DataTable getPlanAll()
       {
           string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster ";
           //string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where 1=1 ";
           //str_query += " and PlanName Like 'Joining package%' ";

           DataTable ds = null;
           ObjData.StartConnection();
           try
           {
               ds = ObjData.RunDataTable(str_query);
           }
           catch (Exception ex)
           {
               ds = null;
           }
           ObjData.EndConnection();
           return ds;
       }
        public DataTable getPlanAllnewnew()
        {
            string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where ID<>7 ";
            //string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where 1=1 ";
            //str_query += " and PlanName Like 'Joining package%' ";

            DataTable ds = null;
            ObjData.StartConnection();
            try
            {
                ds = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }
        public DataTable getOperatorType()
       {
           string str_query = "select * from OperatorCodeType ";


           DataTable ds = null;
           ObjData.StartConnection();
           try
           {
               ds = ObjData.RunDataTable(str_query);
           }
           catch (Exception ex)
           {
               ds = null;
           }
           ObjData.EndConnection();
           return ds;
       }
       public DataTable getPlan(clsplan objplan)
       {
           string str_query = "select * from Planmaster where id='"+objplan.id+"' ";


           DataTable ds = null;
           ObjData.StartConnection();
           try
           {
               ds = ObjData.RunDataTable(str_query);
           }
           catch (Exception ex)
           {
               ds = null;
           }
           ObjData.EndConnection();
           return ds;
       }
       public string Insert_Plan(clsplan objPlan)
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
               s2 = "sp_add_Planmaster";
               SqlParameter[] parameter = {  
                new SqlParameter("@Planname",objPlan.PlanName), 
                new SqlParameter("@planAmount",objPlan.PlanAmount), 
                new SqlParameter("@BuisnessVolume",objPlan.BuisnessVolume),
                 new SqlParameter("@operatorPermission",objPlan.operatorPermission),
                  new SqlParameter("@MonthlyAmount",objPlan.MonthlyAmount),
                   new SqlParameter("@MonthlyCount",objPlan.CountMonthly),
                    new SqlParameter("@MoneyTransfer",objPlan.MoneyTransfer)
              
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
       public string Update_Plan(clsplan objPlan)
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
               s2 = "sp_edit_PlanMaster";
               SqlParameter[] parameter = {  
                                               new SqlParameter("@id",objPlan.id), 
                new SqlParameter("@PlanName",objPlan.PlanName), 
                new SqlParameter("@PlanAmount",objPlan.PlanAmount), 
                new SqlParameter("@BuisnessVolume",objPlan.BuisnessVolume),
                  new SqlParameter("@operatorPermission",objPlan.operatorPermission),
                  new SqlParameter("@MonthlyAmount",objPlan.MonthlyAmount),
                   new SqlParameter("@MonthlyCount",objPlan.CountMonthly),
                    new SqlParameter("@MoneyTransfer",objPlan.MoneyTransfer)
              
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
       public DataTable getPlanwithoperatortypeAll(string UserId)
       {
           DataTable Dt1 = new DataTable();
           Dt1 = null;
           ObjData.StartConnection();
           try
           {
               string Query = "SElect Userid from userdetail where UserId='" + UserId + "'";
               DataTable Dt = ObjData.RunDataTable(Query);
               if (Dt.Rows.Count > 0)
               {
                   string str_query = "SELECT *,(SELECT UserId FROM UserDetail WHERE UserId='" + UserId + "') AS UserId,(SELECT Username FROM UserDetail WHERE UserId='" + UserId + "') AS Username FROM OperatorCodeType ORDER BY TypeId";
                   Dt1 = ObjData.RunDataTable(str_query);
               }

           }
           catch (Exception ex)
           {
               Dt1 = null;
           }
           ObjData.EndConnection();
           return Dt1;
       }
       public DataTable getPlanGetUserPermission(string UserId)
       {
           DataTable Dt1 = new DataTable();
           Dt1 = null;
           ObjData.StartConnection();
           try
           {
               string Query = "SElect * from UserPermission where UserId='" + UserId + "'";
               Dt1 = ObjData.RunDataTable(Query);            

           }
           catch (Exception ex)
           {
               Dt1 = null;
           }
           ObjData.EndConnection();
           return Dt1;
       }
       public string Insert_UserPermission(clsplan objP)
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
               s2 = "sp_addUserPermission";
               SqlParameter[] parameter = {  
                new SqlParameter("@UserId",objP.PlanName), 
                new SqlParameter("@OperatorPermission",objP.operatorPermission),               
              
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

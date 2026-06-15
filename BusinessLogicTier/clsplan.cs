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
       public decimal PlanAmount { get; set; }
       public decimal BuisnessVolume { get; set; }
       public decimal CappingAmount { get; set; }
       public DateTime CreateDate { get; set; }
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

       static DateTime NormalizeCreateDate(DateTime createDate)
       {
           if (createDate == default(DateTime) || createDate <= DateTime.MinValue.AddYears(1))
           {
               return DateTime.Today;
           }

           return createDate.Date;
       }

       static string FormatCreateDateSql(DateTime createDate)
       {
           return "'" + NormalizeCreateDate(createDate).ToString("yyyy-MM-dd") + "'";
       }

       public DataTable GetPlanMasterList()
       {
           string[] queries = new string[]
           {
               "SELECT Id, PlanName, ISNULL(Planamount, 0) AS Planamount, ISNULL(BuisnessVolume, 0) AS BuisnessVolume, "
                   + "ISNULL(cappingamount, ISNULL(MonthlyAmount, 0)) AS cappingamount, CreateDate "
                   + "FROM Planmaster ORDER BY Id DESC",
               "SELECT Id, PlanName, ISNULL(Planamount, 0) AS Planamount, ISNULL(BuisnessVolume, 0) AS BuisnessVolume, "
                   + "ISNULL(MonthlyAmount, 0) AS cappingamount, CreateDate "
                   + "FROM Planmaster ORDER BY Id DESC",
               "SELECT Id, PlanName, ISNULL(Planamount, 0) AS Planamount, ISNULL(BuisnessVolume, 0) AS BuisnessVolume, "
                   + "ISNULL(MonthlyAmount, 0) AS cappingamount "
                   + "FROM Planmaster ORDER BY Id DESC"
           };

           DataTable dt = null;
           ObjData.StartConnection();
           try
           {
               for (int i = 0; i < queries.Length; i++)
               {
                   try
                   {
                       dt = ObjData.RunDataTable(queries[i]);
                       if (dt != null)
                       {
                           break;
                       }
                   }
                   catch
                   {
                       dt = null;
                   }
               }
           }
           finally
           {
               ObjData.EndConnection();
           }

           EnrichPlanListDates(dt);
           return dt;
       }

       static void EnrichPlanListDates(DataTable dt)
       {
           if (dt == null)
           {
               return;
           }

           if (!dt.Columns.Contains("CreateDateDisplay"))
           {
               dt.Columns.Add("CreateDateDisplay", typeof(string));
           }

           if (!dt.Columns.Contains("CreateDateValue"))
           {
               dt.Columns.Add("CreateDateValue", typeof(string));
           }

           bool hasCreateDate = dt.Columns.Contains("CreateDate");

           foreach (DataRow row in dt.Rows)
           {
               if (hasCreateDate && row["CreateDate"] != DBNull.Value)
               {
                   DateTime createDate = Convert.ToDateTime(row["CreateDate"]);
                   row["CreateDateDisplay"] = createDate.ToString("dd/MM/yyyy");
                   row["CreateDateValue"] = createDate.ToString("yyyy-MM-dd");
               }
               else
               {
                   DateTime createDate = DateTime.Today;
                   row["CreateDateDisplay"] = createDate.ToString("dd/MM/yyyy");
                   row["CreateDateValue"] = createDate.ToString("yyyy-MM-dd");
               }
           }
       }

       public bool PlanNameExists(string planName, string excludeId)
       {
           string safeName = (planName ?? string.Empty).Replace("'", "''");
           string sql = "SELECT Id FROM Planmaster WHERE PlanName='" + safeName + "'";
           if (!string.IsNullOrEmpty(excludeId))
           {
               sql += " AND Id<>'" + excludeId.Replace("'", "''") + "'";
           }

           DataTable dt = null;
           ObjData.StartConnection();
           try
           {
               dt = ObjData.RunDataTable(sql);
           }
           catch
           {
               return false;
           }
           finally
           {
               ObjData.EndConnection();
           }

           return dt != null && dt.Rows.Count > 0;
       }

       public string Insert_PlanMaster(clsplan objPlan)
       {
           SqlConnection cn = null;
           SqlTransaction tr = null;

           try
           {
               cn = ObjData.StartConnectionInTransaction();
               tr = cn.BeginTransaction(IsolationLevel.Serializable);
               objPlan.CreateDate = NormalizeCreateDate(objPlan.CreateDate);

               if (TryInsertPlanMaster(tr, objPlan, true, false))
               {
                   tr.Commit();
                   return "t";
               }

               if (TryInsertPlanMaster(tr, objPlan, false, true))
               {
                   tr.Commit();
                   return "t";
               }

               if (TryInsertPlanMaster(tr, objPlan, false, false))
               {
                   tr.Commit();
                   return "t";
               }

               tr.Rollback();
               return "0";
           }
           catch
           {
               if (tr != null)
               {
                   tr.Rollback();
               }
               return "0";
           }
           finally
           {
               ObjData.EndConnection();
               if (tr != null)
               {
                   tr.Dispose();
               }
           }
       }

       private bool TryInsertPlanMaster(SqlTransaction tr, clsplan objPlan, bool includeExtendedFields, bool includeCreateDateOnly)
       {
           try
           {
               string safeName = (objPlan.PlanName ?? string.Empty).Replace("'", "''");
               string sql;

               if (includeExtendedFields)
               {
                   sql = "INSERT INTO Planmaster (PlanName, Planamount, BuisnessVolume, CreateDate, cappingamount, operatorpermission, MonthlyAmount, CountMonth, MoneyTransfer) VALUES ('"
                       + safeName + "'," + objPlan.PlanAmount + "," + objPlan.BuisnessVolume + ","
                       + FormatCreateDateSql(objPlan.CreateDate) + "," + objPlan.CappingAmount
                       + ",''," + objPlan.CappingAmount + ",0,0)";
               }
               else if (includeCreateDateOnly)
               {
                   sql = "INSERT INTO Planmaster (PlanName, Planamount, BuisnessVolume, CreateDate, operatorpermission, MonthlyAmount, CountMonth, MoneyTransfer) VALUES ('"
                       + safeName + "'," + objPlan.PlanAmount + "," + objPlan.BuisnessVolume + ","
                       + FormatCreateDateSql(objPlan.CreateDate) + ",'',"
                       + objPlan.CappingAmount + ",0,0)";
               }
               else
               {
                   sql = "INSERT INTO Planmaster (PlanName, Planamount, BuisnessVolume, CreateDate, operatorpermission, MonthlyAmount, CountMonth, MoneyTransfer) VALUES ('"
                       + safeName + "'," + objPlan.PlanAmount + "," + objPlan.BuisnessVolume + ","
                       + FormatCreateDateSql(objPlan.CreateDate) + ",'',"
                       + objPlan.CappingAmount + ",0,0)";
               }

               ObjData.RunInsUpDelQueryTrans(sql, tr);
               return true;
           }
           catch
           {
               return false;
           }
       }

       public string Update_PlanMaster(clsplan objPlan)
       {
           SqlConnection cn = null;
           SqlTransaction tr = null;

           try
           {
               cn = ObjData.StartConnectionInTransaction();
               tr = cn.BeginTransaction(IsolationLevel.Serializable);
               objPlan.CreateDate = NormalizeCreateDate(objPlan.CreateDate);

               if (TryUpdatePlanMaster(tr, objPlan, true, false))
               {
                   tr.Commit();
                   return "t";
               }

               if (TryUpdatePlanMaster(tr, objPlan, false, true))
               {
                   tr.Commit();
                   return "t";
               }

               if (TryUpdatePlanMaster(tr, objPlan, false, false))
               {
                   tr.Commit();
                   return "t";
               }

               tr.Rollback();
               return "0";
           }
           catch
           {
               if (tr != null)
               {
                   tr.Rollback();
               }
               return "0";
           }
           finally
           {
               ObjData.EndConnection();
               if (tr != null)
               {
                   tr.Dispose();
               }
           }
       }

       private bool TryUpdatePlanMaster(SqlTransaction tr, clsplan objPlan, bool includeExtendedFields, bool includeCreateDateOnly)
       {
           try
           {
               string safeName = (objPlan.PlanName ?? string.Empty).Replace("'", "''");
               string safeId = (objPlan.id ?? string.Empty).Replace("'", "''");
               string sql;

               if (includeExtendedFields)
               {
                   sql = "UPDATE Planmaster SET PlanName='" + safeName + "', Planamount=" + objPlan.PlanAmount
                       + ", BuisnessVolume=" + objPlan.BuisnessVolume + ", CreateDate=" + FormatCreateDateSql(objPlan.CreateDate)
                       + ", cappingamount=" + objPlan.CappingAmount + ", MonthlyAmount=" + objPlan.CappingAmount
                       + " WHERE Id='" + safeId + "'";
               }
               else if (includeCreateDateOnly)
               {
                   sql = "UPDATE Planmaster SET PlanName='" + safeName + "', Planamount=" + objPlan.PlanAmount
                       + ", BuisnessVolume=" + objPlan.BuisnessVolume + ", CreateDate=" + FormatCreateDateSql(objPlan.CreateDate)
                       + ", MonthlyAmount=" + objPlan.CappingAmount
                       + " WHERE Id='" + safeId + "'";
               }
               else
               {
                   sql = "UPDATE Planmaster SET PlanName='" + safeName + "', Planamount=" + objPlan.PlanAmount
                       + ", BuisnessVolume=" + objPlan.BuisnessVolume + ", CreateDate=" + FormatCreateDateSql(objPlan.CreateDate)
                       + ", MonthlyAmount=" + objPlan.CappingAmount
                       + " WHERE Id='" + safeId + "'";
               }

               ObjData.RunInsUpDelQueryTrans(sql, tr);
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}

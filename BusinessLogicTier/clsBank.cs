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
    public class clsBank
    {
        Data ObjData = new Data();
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string MentionBy { get; set; }
      
        public string BankAccountId { get; set; }
      
     
        public string AccHolderName { get; set; }
        public string AccNo { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public DataTable getBank()
        {
            string str_query = "select * from BankMaster order by BankName";

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
        public DataTable getBankAccountList()
        {
            string str_query = "select * from CompanyAccountDetail order by id";

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
       
        public string Insert_BankAccount(clsBank objBank)
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
                s2 = "sp_add_CompanyAccountDetail";
                SqlParameter[] parameter = {  
                new SqlParameter("@AccountNo",objBank.AccNo), 
                new SqlParameter("@AccountHolderName",objBank.AccHolderName), 
                new SqlParameter("@BankName",objBank.BankName), 
                new SqlParameter("@IFSCCode",objBank.IFSCCode), 
                new SqlParameter("@BranchName",objBank.BranchName), 
                new SqlParameter("@MentionBy",objBank.MentionBy)
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
        public string Update_BankAccount(clsBank objBank)
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
                s2 = "sp_edit_CompanyAccountDetail";
                SqlParameter[] parameter = {  
                new SqlParameter("@id",objBank.BankAccountId), 
                new SqlParameter("@AccountNo",objBank.AccNo), 
                new SqlParameter("@AccountHolderName",objBank.AccHolderName), 
                new SqlParameter("@BankName",objBank.BankName), 
                new SqlParameter("@IFSCCode",objBank.IFSCCode), 
                new SqlParameter("@BranchName",objBank.BranchName), 
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
        public string Insert_Bank(clsBank objBank)
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
                s2 = "sp_add_BankMaster";
                SqlParameter[] parameter = {  
                new SqlParameter("@BankName",objBank.BankName), 
                new SqlParameter("@MentionBy",objBank.MentionBy)
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
        public string Update_Bank(clsBank objBank)
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
                s2 = "update bankMaster set bankname='" + objBank.BankName + "' where bankid='" + objBank.BankId + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string Delete_Bank(int id)
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
                s2 = " INSERT INTO CompanyAccountDetailHistory SELECT AccountNo, AccountHolderName, BankName, IFSCCode, BranchName, MentionBy, MentionDate FROM CompanyAccountDetail WHERE id='" + id + "' ";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                s2 = " DELETE FROM CompanyAccountDetail WHERE id='" + id + "' ";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                res = "t";
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

        public DataTable getPaymentMode()
        {
            string str_query = "select * from Master_PaymentMode order by id";

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

        public string Insert_PaymentMode(clsBank objBank)
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
                s2 = "INSERT INTO Master_PaymentMode VALUES('" + objBank.BankName + "',1)";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string Update_PaymentMode(clsBank objBank)
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
                s2 = "update Master_PaymentMode set type='" + objBank.BankName + "',status='" + objBank.BankAccountId + "' where id='" + objBank.BankId + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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

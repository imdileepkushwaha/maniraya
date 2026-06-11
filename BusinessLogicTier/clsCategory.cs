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
    public class clsCategory
    {
        Data ObjData = new Data();
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string SubcategorySecondId { get; set; }
        public string SubcategorySecondName { get; set; }
        public string MentionBy { get; set; }

        public DataTable getCategory()
        {
            string str_query = "select * from CategoryMaster order by CategoryName";

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

        public DataTable getSubcategoryAll()
        {
            string str_query = "select sm.*,cm.categoryname from SubcategoryMaster sm left join categoryMaster cm on sm.Categoryid=cm.categoryid  order by Subcategoryname,cm.categoryname";

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
        public DataTable getSubcategory(clsCategory objCategory)
        {
            string str_query = "select sm.*,cm.categoryname from SubcategoryMaster sm left join categoryMaster cm on sm.Categoryid=cm.categoryid   where sm.categoryid=" + objCategory.CategoryId + "  order by Subcategoryname,cm.categoryname";

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

        public DataTable getSubcategorySecondAll()
        {
            string str_query = "select sm2.*,sm.subcategoryname,cm.categoryname from subcategorysecondmaster sm2 left join  SubcategoryMaster sm  on sm2.subcategoryid=sm.subcategoryid  left join categoryMaster cm on sm.Categoryid=cm.categoryid  order by cm.categoryname,sm.Subcategoryname,sm2.subcategorysecondname";

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
        public DataTable getSubcategorySecond(clsCategory objCategory)
        {
            string str_query = "select sm2.*,sm.subcategoryname,cm.categoryname from subcategorysecondmaster sm2 left join  SubcategoryMaster sm  on sm2.subcategoryid=sm.subcategoryid  left join categoryMaster cm on sm.Categoryid=cm.categoryid  where sm2.subcategoryid=" + objCategory.SubcategoryId + "  order by cm.categoryname,sm.Subcategoryname,sm2.subcategorysecondname";

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

        public string Insert_Category(clsCategory objCategory)
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
                s2 = "sp_add_CategoryMaster";
                SqlParameter[] parameter = {  
                new SqlParameter("@CategoryName",objCategory.CategoryName), 
                new SqlParameter("@MentionBy",objCategory.MentionBy)
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
        public string Update_Category(clsCategory objCategory)
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
                s2 = "update CategoryMaster set Categoryname='" + objCategory.CategoryName + "' where Categoryid='" + objCategory.CategoryId + "'";

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

        public string Insert_Subcategory(clsCategory objCategory)
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
                s2 = "sp_add_Subcategory";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@CategoryId",objCategory.CategoryId), 
                new SqlParameter("@SubcategoryName",objCategory.SubcategoryName), 
                new SqlParameter("@MentionBy",objCategory.MentionBy)
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

        public string Update_Subcategory(clsCategory objCategory)
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
                s2 = "update SubcategoryMaster  set SubcategoryName='" + objCategory.SubcategoryName + "' where Subcategoryid='" + objCategory.SubcategoryId + "'";

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
        public string Insert_SubcategorySecond(clsCategory objCategory)
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
                s2 = "sp_add_SubcategorySecond";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@SubcategoryId",objCategory.SubcategoryId), 
                new SqlParameter("@SubcategorySecondName",objCategory.SubcategorySecondName), 
                new SqlParameter("@MentionBy",objCategory.MentionBy)
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

        public string Update_SubcategorySecond(clsCategory objCategory)
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
                s2 = "update SubcategorySecondMaster  set SubcategorysecondName='" + objCategory.SubcategorySecondName + "' where Subcategorysecondid='" + objCategory.SubcategorySecondId + "'";

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

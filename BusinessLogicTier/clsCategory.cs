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
        public string CategoryImage { get; set; }
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
            SqlConnection cn = null;
            SqlTransaction tr = null;

            try
            {
                cn = ObjData.StartConnectionInTransaction();
                tr = cn.BeginTransaction(IsolationLevel.Serializable);

                bool inserted = false;

                if (TryInsertCategoryProc(tr, objCategory, false))
                {
                    inserted = true;
                }
                else if (!string.IsNullOrEmpty(objCategory.CategoryImage) && TryInsertCategoryProc(tr, objCategory, true))
                {
                    inserted = true;
                }
                else if (TryInsertCategoryDirect(tr, objCategory))
                {
                    inserted = true;
                }

                if (!inserted)
                {
                    tr.Rollback();
                    return "0";
                }

                if (!string.IsNullOrEmpty(objCategory.CategoryImage))
                {
                    try
                    {
                        UpdateCategoryImageAfterInsert(tr, objCategory);
                    }
                    catch
                    {
                    }
                }

                tr.Commit();
                return "t";
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

        private bool TryInsertCategoryProc(SqlTransaction tr, clsCategory objCategory, bool includeImage)
        {
            try
            {
                SqlParameter[] parameter;
                if (includeImage)
                {
                    parameter = new SqlParameter[] {
                        new SqlParameter("@CategoryName", objCategory.CategoryName),
                        new SqlParameter("@MentionBy", objCategory.MentionBy),
                        new SqlParameter("@img", objCategory.CategoryImage)
                    };
                }
                else
                {
                    parameter = new SqlParameter[] {
                        new SqlParameter("@CategoryName", objCategory.CategoryName),
                        new SqlParameter("@MentionBy", objCategory.MentionBy)
                    };
                }

                ObjData.RunInsUpDelQueryTransProc("sp_add_CategoryMaster", tr, parameter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryInsertCategoryDirect(SqlTransaction tr, clsCategory objCategory)
        {
            string safeName = (objCategory.CategoryName ?? string.Empty).Replace("'", "''");
            string safeMention = (objCategory.MentionBy ?? string.Empty).Replace("'", "''");

            if (!string.IsNullOrEmpty(objCategory.CategoryImage))
            {
                try
                {
                    string safeImg = objCategory.CategoryImage.Replace("'", "''");
                    string sqlWithImg = "INSERT INTO CategoryMaster (CategoryName, MentionBy, img) VALUES ('"
                        + safeName + "','" + safeMention + "','" + safeImg + "')";
                    ObjData.RunInsUpDelQueryTrans(sqlWithImg, tr);
                    objCategory.CategoryImage = string.Empty;
                    return true;
                }
                catch
                {
                }
            }

            try
            {
                string sql = "INSERT INTO CategoryMaster (CategoryName, MentionBy) VALUES ('" + safeName + "','" + safeMention + "')";
                ObjData.RunInsUpDelQueryTrans(sql, tr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void UpdateCategoryImageAfterInsert(SqlTransaction tr, clsCategory objCategory)
        {
            string safeImg = (objCategory.CategoryImage ?? string.Empty).Replace("'", "''");
            string safeName = (objCategory.CategoryName ?? string.Empty).Replace("'", "''");
            string sql = "UPDATE CategoryMaster SET img='" + safeImg
                + "' WHERE CategoryId = (SELECT TOP 1 CategoryId FROM CategoryMaster WHERE CategoryName='" + safeName + "' ORDER BY CategoryId DESC)";
            ObjData.RunInsUpDelQueryTrans(sql, tr);
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
                s2 = "update CategoryMaster set Categoryname='" + objCategory.CategoryName.Replace("'", "''") + "'"
                    + (string.IsNullOrEmpty(objCategory.CategoryImage) ? "" : ", img='" + objCategory.CategoryImage.Replace("'", "''") + "'")
                    + " where Categoryid='" + objCategory.CategoryId + "'";

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

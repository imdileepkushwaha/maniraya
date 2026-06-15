using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
    public class clsPackagePlan
    {
        private readonly Data ObjData = new Data();

        public string Id { get; set; }
        public string PlanId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public DataTable GetPlansForDropdown()
        {
            return RunSelectFirst(
                "SELECT Id, PlanName FROM Planmaster ORDER BY PlanName",
                "SELECT ID AS Id, PlanName FROM Planmaster ORDER BY PlanName");
        }

        public DataTable GetProductsForDropdown()
        {
            return RunSelectFirst(
                "SELECT ProductId, ProductName FROM ProductMaster WHERE ISNULL(Status, 1) = 1 ORDER BY ProductName",
                "SELECT ProductId, Productname AS ProductName FROM ProductMaster WHERE ISNULL(Status, 1) = 1 ORDER BY ProductName");
        }

        public DataTable GetPlanProductList(string planId)
        {
            if (string.IsNullOrWhiteSpace(planId) || planId == "0")
            {
                return CreateEmptyListTable();
            }

            string safePlanId = planId.Replace("'", "''");
            string planFilter = " WHERE CAST(ppm.planid AS varchar(50))='" + safePlanId + "' ";

            string[] queries = new string[]
            {
                "SELECT ppm.id, ppm.planid, ppm.productid, ISNULL(ppm.quantity, 1) AS quantity, "
                    + "(SELECT TOP 1 pm.PlanName FROM Planmaster pm WHERE CAST(pm.Id AS varchar(50))=CAST(ppm.planid AS varchar(50))) AS PlanName, "
                    + "(SELECT TOP 1 pr.ProductName FROM ProductMaster pr WHERE CAST(pr.ProductId AS varchar(50))=CAST(ppm.productid AS varchar(50))) AS ProductName "
                    + "FROM PlanProductMaster ppm " + planFilter
                    + " AND ppm.productid IS NOT NULL "
                    + " ORDER BY ProductName",

                "SELECT ppm.id, ppm.planid, ppm.productid, ISNULL(ppm.Qnty, 1) AS quantity, "
                    + "(SELECT TOP 1 pm.PlanName FROM Planmaster pm WHERE CAST(pm.Id AS varchar(50))=CAST(ppm.planid AS varchar(50))) AS PlanName, "
                    + "(SELECT TOP 1 pr.ProductName FROM ProductMaster pr WHERE CAST(pr.ProductId AS varchar(50))=CAST(ppm.productid AS varchar(50))) AS ProductName "
                    + "FROM PlanProductMaster ppm " + planFilter
                    + " AND ppm.productid IS NOT NULL "
                    + " ORDER BY ProductName",

                "SELECT ppm.Id AS id, ppm.PlanId AS planid, ppm.ProductId AS productid, ISNULL(ppm.Quantity, 1) AS quantity, "
                    + "(SELECT TOP 1 pm.PlanName FROM Planmaster pm WHERE CAST(pm.Id AS varchar(50))=CAST(ppm.PlanId AS varchar(50))) AS PlanName, "
                    + "(SELECT TOP 1 pr.ProductName FROM ProductMaster pr WHERE CAST(pr.ProductId AS varchar(50))=CAST(ppm.ProductId AS varchar(50))) AS ProductName "
                    + "FROM PlanProductMaster ppm "
                    + " WHERE CAST(ppm.PlanId AS varchar(50))='" + safePlanId + "' "
                    + " AND ppm.ProductId IS NOT NULL "
                    + " ORDER BY ProductName"
            };

            return RunSelectFirst(queries);
        }

        public bool PlanProductExists(string planId, string productId, string excludeId)
        {
            string safePlanId = (planId ?? string.Empty).Replace("'", "''");
            string safeProductId = (productId ?? string.Empty).Replace("'", "''");
            string exclude = string.IsNullOrWhiteSpace(excludeId)
                ? string.Empty
                : " AND id<>'" + excludeId.Replace("'", "''") + "'";

            string[] queries = new string[]
            {
                "SELECT id FROM PlanProductMaster WHERE CAST(planid AS varchar(50))='" + safePlanId + "' AND CAST(productid AS varchar(50))='" + safeProductId + "'" + exclude,
                "SELECT Id AS id FROM PlanProductMaster WHERE CAST(PlanId AS varchar(50))='" + safePlanId + "' AND CAST(ProductId AS varchar(50))='" + safeProductId + "'" + exclude.Replace("id", "Id")
            };

            DataTable dt = RunSelectFirst(queries);
            return dt != null && dt.Rows.Count > 0;
        }

        public string InsertPlanProduct(clsPackagePlan item)
        {
            SqlConnection cn = null;
            SqlTransaction tr = null;

            try
            {
                cn = ObjData.StartConnectionInTransaction();
                tr = cn.BeginTransaction(IsolationLevel.Serializable);

                if (TryInsertPlanProduct(tr, item))
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

        private bool TryInsertPlanProduct(SqlTransaction tr, clsPackagePlan item)
        {
            string safePlanId = (item.PlanId ?? string.Empty).Replace("'", "''");
            string safeProductId = (item.ProductId ?? string.Empty).Replace("'", "''");

            string[] queries = new string[]
            {
                "INSERT INTO PlanProductMaster (planid, productid, quantity) VALUES ('" + safePlanId + "','" + safeProductId + "'," + item.Quantity + ")",
                "INSERT INTO PlanProductMaster (planid, productid, Qnty) VALUES ('" + safePlanId + "','" + safeProductId + "'," + item.Quantity + ")",
                "INSERT INTO PlanProductMaster (PlanId, ProductId, Quantity) VALUES ('" + safePlanId + "','" + safeProductId + "'," + item.Quantity + ")",
                "INSERT INTO PlanProductMaster (PlanId, ProductId, Qnty) VALUES ('" + safePlanId + "','" + safeProductId + "'," + item.Quantity + ")",
                "INSERT INTO PlanProductMaster (planid, productid) VALUES ('" + safePlanId + "','" + safeProductId + "')"
            };

            foreach (string sql in queries)
            {
                try
                {
                    ObjData.RunInsUpDelQueryTrans(sql, tr);
                    return true;
                }
                catch
                {
                }
            }

            return false;
        }

        public string UpdatePlanProduct(clsPackagePlan item)
        {
            SqlConnection cn = null;
            SqlTransaction tr = null;

            try
            {
                cn = ObjData.StartConnectionInTransaction();
                tr = cn.BeginTransaction(IsolationLevel.Serializable);

                if (TryUpdatePlanProduct(tr, item))
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

        private bool TryUpdatePlanProduct(SqlTransaction tr, clsPackagePlan item)
        {
            string safeId = (item.Id ?? string.Empty).Replace("'", "''");
            string safePlanId = (item.PlanId ?? string.Empty).Replace("'", "''");
            string safeProductId = (item.ProductId ?? string.Empty).Replace("'", "''");

            string[] queries = new string[]
            {
                "UPDATE PlanProductMaster SET planid='" + safePlanId + "', productid='" + safeProductId + "', quantity=" + item.Quantity + " WHERE id='" + safeId + "'",
                "UPDATE PlanProductMaster SET planid='" + safePlanId + "', productid='" + safeProductId + "', Qnty=" + item.Quantity + " WHERE id='" + safeId + "'",
                "UPDATE PlanProductMaster SET PlanId='" + safePlanId + "', ProductId='" + safeProductId + "', Quantity=" + item.Quantity + " WHERE Id='" + safeId + "'"
            };

            foreach (string sql in queries)
            {
                try
                {
                    ObjData.RunInsUpDelQueryTrans(sql, tr);
                    return true;
                }
                catch
                {
                }
            }

            return false;
        }

        public string DeletePlanProduct(string id)
        {
            SqlConnection cn = null;
            SqlTransaction tr = null;

            try
            {
                cn = ObjData.StartConnectionInTransaction();
                tr = cn.BeginTransaction(IsolationLevel.Serializable);
                string safeId = (id ?? string.Empty).Replace("'", "''");

                try
                {
                    ObjData.RunInsUpDelQueryTrans("DELETE FROM PlanProductMaster WHERE id='" + safeId + "'", tr);
                }
                catch
                {
                    ObjData.RunInsUpDelQueryTrans("DELETE FROM PlanProductMaster WHERE Id='" + safeId + "'", tr);
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

        private DataTable RunSelectFirst(params string[] queries)
        {
            foreach (string sql in queries)
            {
                DataTable dt = RunSelect(sql);
                if (dt != null)
                {
                    return dt;
                }
            }

            return CreateEmptyListTable();
        }

        private static DataTable CreateEmptyListTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("planid");
            dt.Columns.Add("productid");
            dt.Columns.Add("quantity", typeof(int));
            dt.Columns.Add("PlanName");
            dt.Columns.Add("ProductName");
            return dt;
        }

        private DataTable RunSelect(string sql)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                ObjData.EndConnection();
            }

            return dt;
        }
    }
}

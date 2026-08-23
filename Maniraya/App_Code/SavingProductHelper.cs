using DataTier;
using System;
using System.Data;
using System.Web;

public static class SavingProductHelper
{
    public const string ImageFolder = "~/ProductImage/";

    public static void EnsureStatusColumn()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string sql = @"
                IF COL_LENGTH('SavingProductMaster', 'Status') IS NULL
                BEGIN
                    ALTER TABLE SavingProductMaster ADD Status BIT NOT NULL CONSTRAINT DF_SavingProductMaster_Status DEFAULT 1
                END";
                objData.RunInsUpDelQuery(sql);
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }
    }

    public static void EnsureCatalogColumns()
    {
        EnsureStatusColumn();
        RunNonQuery(@"
            IF COL_LENGTH('SavingProductMaster', 'GST') IS NULL
            BEGIN
                ALTER TABLE SavingProductMaster ADD GST DECIMAL(18,2) NULL;
            END");
        RunNonQuery(@"
            IF COL_LENGTH('SavingProductMaster', 'HSNCODE') IS NULL
            BEGIN
                ALTER TABLE SavingProductMaster ADD HSNCODE NVARCHAR(100) NULL;
            END");
    }

    public static string AddCatalogProduct(string productName, decimal mrp, decimal dp, string imageName, string entryBy, decimal gst, string hsnCode)
    {
        EnsureCatalogColumns();

        productName = (productName ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(productName))
        {
            return "0";
        }

        string safeName = Escape(productName);
        DataTable exists = RunSelect(
            "SELECT TOP 1 id FROM SavingProductMaster WITH (NOLOCK) WHERE ProductName = '" + safeName + "'");
        if (exists != null && exists.Rows.Count > 0)
        {
            return "f";
        }

        System.Globalization.CultureInfo inv = System.Globalization.CultureInfo.InvariantCulture;
        string image = string.IsNullOrWhiteSpace(imageName) ? "noimage.png" : imageName.Trim();

        // Monthly uniqueness uses EntryDate on the 1st. Catalog rows must not occupy that slot.
        string entryDateSql = "CASE WHEN DAY(GETDATE()) = 1 THEN DATEADD(DAY, 1, GETDATE()) ELSE GETDATE() END";

        System.Collections.Generic.List<string> cols = new System.Collections.Generic.List<string>
        {
            "ProductName", "mrp", "dp", "ImageName", "entryby", "entrydate"
        };
        System.Collections.Generic.List<string> vals = new System.Collections.Generic.List<string>
        {
            "'" + safeName + "'",
            mrp.ToString(inv),
            dp.ToString(inv),
            "'" + Escape(image) + "'",
            "'" + Escape(entryBy ?? string.Empty) + "'",
            entryDateSql
        };

        if (HasTableColumn("SavingProductMaster", "Status"))
        {
            cols.Add("status");
            vals.Add("1");
        }
        if (HasTableColumn("SavingProductMaster", "GST"))
        {
            cols.Add("GST");
            vals.Add(gst.ToString(inv));
        }
        if (HasTableColumn("SavingProductMaster", "HSNCODE") || HasTableColumn("SavingProductMaster", "HSNCode"))
        {
            cols.Add("HSNCODE");
            vals.Add("'" + Escape(hsnCode ?? string.Empty) + "'");
        }

        string sql = "INSERT INTO SavingProductMaster (" + string.Join(", ", cols.ToArray()) + ") VALUES (" +
                     string.Join(", ", vals.ToArray()) + ")";
        return RunNonQuery(sql) ? "t" : "0";
    }

    static bool HasTableColumn(string tableName, string columnName)
    {
        DataTable dt = RunSelect(
            "SELECT COL_LENGTH('" + Escape(tableName) + "', '" + Escape(columnName) + "') AS ColLen");
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ColLen"] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0]["ColLen"]) > 0;
    }

    public static void EnsureDeliveryColumns()
    {
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatus') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD DeliveryStatus NVARCHAR(50) NULL;
            END");

        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatusUpdatedOn') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD DeliveryStatusUpdatedOn DATETIME NULL;
            END");

        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatusUpdatedBy') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD DeliveryStatusUpdatedBy NVARCHAR(100) NULL;
            END");

        RunNonQuery(@"
            UPDATE SavingAccountDetail
            SET DeliveryStatus = 'Confirmed'
            WHERE (status = 'Approved' OR LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('approved', 'approve'))
              AND (DeliveryStatus IS NULL OR LTRIM(RTRIM(DeliveryStatus)) = '')");
    }

    public static void EnsurePaymentMethodColumn()
    {
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'PaymentMethod') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD PaymentMethod NVARCHAR(50) NULL;
            END");
    }

    public static void EnsureShippingTypeColumn()
    {
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'ShippingType') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD ShippingType NVARCHAR(50) NULL;
            END");
    }

    public static bool UpdatePurchaseMeta(string orderId, string paymentMethod, string shippingType)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return false;
        }

        EnsurePaymentMethodColumn();
        EnsureShippingTypeColumn();

        string sql = "UPDATE SavingAccountDetail SET "
            + "PaymentMethod = '" + Escape(paymentMethod) + "', "
            + "ShippingType = '" + Escape(shippingType) + "' "
            + "WHERE orderid = '" + Escape(orderId.Trim()) + "'";

        return RunNonQuery(sql);
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    public static bool HasDeliveryStatusColumn()
    {
        DataTable dt = RunSelect("SELECT COL_LENGTH('SavingAccountDetail', 'DeliveryStatus') AS ColLen");
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ColLen"] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0]["ColLen"]) > 0;
    }

    public static void EnsureInstallmentDeliveryColumns()
    {
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatus') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatus NVARCHAR(50) NULL;
            END");

        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatusUpdatedOn') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatusUpdatedOn DATETIME NULL;
            END");

        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatusUpdatedBy') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatusUpdatedBy NVARCHAR(100) NULL;
            END");

        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'ConsignmentNumber') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD ConsignmentNumber NVARCHAR(100) NULL;
            END");

        RunNonQuery(@"
            UPDATE SavingAccountInstallmentDetail
            SET DeliveryStatus = 'Confirmed'
            WHERE (status = 'Approved' OR LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('approved', 'approve'))
              AND (DeliveryStatus IS NULL OR LTRIM(RTRIM(DeliveryStatus)) = '')");
    }

    public static bool HasInstallmentDeliveryStatusColumn()
    {
        DataTable dt = RunSelect("SELECT COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatus') AS ColLen");
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ColLen"] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0]["ColLen"]) > 0;
    }

    public static bool HasInstallmentConsignmentColumn()
    {
        DataTable dt = RunSelect("SELECT COL_LENGTH('SavingAccountInstallmentDetail', 'ConsignmentNumber') AS ColLen");
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ColLen"] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0]["ColLen"]) > 0;
    }

    public static DataTable GetAllProducts()
    {
        EnsureStatusColumn();
        return RunSelect(@"
            SELECT
                id,
                ProductName,
                MRP,
                DP,
                ImageName,
                ISNULL(Status, 1) AS Status,
                CASE WHEN ISNULL(Status, 1) = 1 THEN 'Active' ELSE 'Inactive' END AS StatusText
            FROM SavingProductMaster
            ORDER BY id DESC");
    }

    public static DataTable GetProductById(int id)
    {
        if (id <= 0)
        {
            return new DataTable();
        }

        EnsureStatusColumn();
        return RunSelect("SELECT * FROM SavingProductMaster WHERE id = " + id);
    }

    public static bool UpdateProduct(int id, string productName, decimal mrp, string dp, string imageName, bool status)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(productName))
        {
            return false;
        }

        EnsureStatusColumn();

        string safeName = productName.Trim().Replace("'", "''");
        string safeDp = (dp ?? string.Empty).Trim().Replace("'", "''");
        string safeImage = (imageName ?? string.Empty).Trim().Replace("'", "''");
        string statusBit = status ? "1" : "0";

        string sql = string.Format(
            "UPDATE SavingProductMaster SET ProductName='{0}', MRP={1}, DP='{2}', ImageName='{3}', Status={4} WHERE id={5}",
            safeName,
            mrp.ToString(System.Globalization.CultureInfo.InvariantCulture),
            safeDp,
            safeImage,
            statusBit,
            id);

        return RunNonQuery(sql);
    }

    public static bool SetProductStatus(int id, bool active)
    {
        if (id <= 0)
        {
            return false;
        }

        EnsureStatusColumn();
        return RunNonQuery("UPDATE SavingProductMaster SET Status = " + (active ? "1" : "0") + " WHERE id = " + id);
    }

    public static void EnsureInstallmentProductAssignTable()
    {
        RunNonQuery(@"
            IF OBJECT_ID('dbo.SavingInstallmentProductAssign', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.SavingInstallmentProductAssign
                (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    InstallmentNo INT NOT NULL,
                    ProductId INT NOT NULL,
                    Status BIT NOT NULL CONSTRAINT DF_SavingInstallmentProductAssign_Status DEFAULT (1),
                    EntryBy NVARCHAR(100) NULL,
                    EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingInstallmentProductAssign_EntryDate DEFAULT (GETDATE()),
                    CONSTRAINT UQ_SavingInstallmentProductAssign_Inst UNIQUE (InstallmentNo)
                );
            END");
    }

    public static DataTable GetActiveProductsForAssign()
    {
        EnsureStatusColumn();
        return RunSelect(@"
            SELECT id, ProductName, MRP, DP
            FROM SavingProductMaster WITH (NOLOCK)
            WHERE ISNULL(Status, 1) = 1
            ORDER BY ProductName");
    }

    public static DataTable GetInstallmentAssignmentMap()
    {
        EnsureInstallmentProductAssignTable();
        return RunSelect(@"
            SELECT n.InstallmentNo,
                   a.ProductId,
                   pd.ProductName,
                   pd.MRP,
                   pd.DP,
                   a.EntryBy,
                   a.EntryDate
            FROM (
                SELECT 1 AS InstallmentNo UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4
                UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8
                UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12
                UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15 UNION ALL SELECT 16
                UNION ALL SELECT 17 UNION ALL SELECT 18
            ) n
            LEFT JOIN SavingInstallmentProductAssign a WITH (NOLOCK)
                ON a.InstallmentNo = n.InstallmentNo AND ISNULL(a.Status, 1) = 1
            LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON pd.id = a.ProductId
            ORDER BY n.InstallmentNo");
    }

    public static DataTable GetProductForInstallment(int installmentNo)
    {
        if (installmentNo < 1 || installmentNo > 18)
        {
            return new DataTable();
        }

        EnsureInstallmentProductAssignTable();
        DataTable dt = RunSelect(@"
            SELECT a.ProductId AS productid, pd.ProductName AS productname, pd.ImageName, pd.MRP, pd.DP
            FROM SavingInstallmentProductAssign a WITH (NOLOCK)
            LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON pd.id = a.ProductId
            WHERE ISNULL(a.Status, 1) = 1 AND a.InstallmentNo = " + installmentNo);

        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["productid"] != DBNull.Value
            && Convert.ToInt32(dt.Rows[0]["productid"]) > 0)
        {
            return dt;
        }

        return RunSelect(@"
            SELECT sd.productid, pd.productname, pd.ImageName, pd.MRP, pd.DP
            FROM SavingMonthlyProductDetail sd WITH (NOLOCK)
            LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON sd.productid = pd.id
            WHERE sd.Status = 1");
    }

    public static bool AssignProductToInstallment(int installmentNo, int productId, string entryBy)
    {
        if (installmentNo < 1 || installmentNo > 18 || productId <= 0)
        {
            return false;
        }

        EnsureInstallmentProductAssignTable();
        string safeBy = Escape(entryBy);
        string sql = @"
            IF EXISTS (SELECT 1 FROM SavingInstallmentProductAssign WHERE InstallmentNo = " + installmentNo + @")
            BEGIN
                UPDATE SavingInstallmentProductAssign
                SET ProductId = " + productId + @",
                    Status = 1,
                    EntryBy = '" + safeBy + @"',
                    EntryDate = GETDATE()
                WHERE InstallmentNo = " + installmentNo + @";
            END
            ELSE
            BEGIN
                INSERT INTO SavingInstallmentProductAssign (InstallmentNo, ProductId, Status, EntryBy, EntryDate)
                VALUES (" + installmentNo + ", " + productId + ", 1, '" + safeBy + @"', GETDATE());
            END";

        return RunNonQuery(sql);
    }

    public static string GetImageUrl(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            return VirtualPathUtility.ToAbsolute(ImageFolder + "noimage.png");
        }

        return VirtualPathUtility.ToAbsolute(ImageFolder + imageName.Trim());
    }

    /// <summary>
    /// Returns true if UTR / OnlineTransactionId is already used on a non-rejected
    /// purchase or installment (case-insensitive, trimmed). Optional excludeInstallmentId
    /// lets a rejected installment resubmit with the same UTR.
    /// </summary>
    public static bool IsOnlineTransactionIdUsed(string onlineTransactionId, int excludeInstallmentId = 0)
    {
        string utr = (onlineTransactionId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(utr) || utr.StartsWith("CASH-", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        string safe = Escape(utr);
        string sql = @"
SELECT CASE WHEN EXISTS (
    SELECT 1
    FROM SavingAccountDetail WITH (NOLOCK)
    WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER('" + safe + @"')
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
)
OR EXISTS (
    SELECT 1
    FROM SavingAccountInstallmentDetail WITH (NOLOCK)
    WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER('" + safe + @"')
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
      AND (" + excludeInstallmentId + @" <= 0 OR id <> " + excludeInstallmentId + @")
)
THEN 1 ELSE 0 END";

        DataTable dt = RunSelect(sql);
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0][0]) == 1;
    }

    static DataTable RunSelect(string sql)
    {
        Data objData = new Data();
        DataTable dt = new DataTable();
        try
        {
            objData.StartConnection();
            try
            {
                dt = objData.RunDataTable(sql);
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    static bool RunNonQuery(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery(sql);
                return true;
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }
}

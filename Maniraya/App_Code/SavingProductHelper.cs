using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
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

    public static bool HasInstallmentCouponCodeColumn()
    {
        return HasTableColumn("SavingAccountInstallmentDetail", "CouponCode");
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
        string bulkUtrSql = HasTable("SavingBulkInstallmentPayment")
            ? @"
OR EXISTS (
    SELECT 1
    FROM SavingBulkInstallmentPayment WITH (NOLOCK)
    WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER('" + safe + @"')
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
)"
            : string.Empty;

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
" + bulkUtrSql + @"
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

    public static void EnsureBulkColumns()
    {
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountDetail', 'PlanType') IS NULL
            BEGIN
                ALTER TABLE SavingAccountDetail ADD PlanType NVARCHAR(50) NULL;
            END");
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'IsBulkPrepaid') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD IsBulkPrepaid BIT NULL;
            END");
        RunNonQuery(@"
            IF COL_LENGTH('SavingAccountInstallmentDetail', 'IncomeReleased') IS NULL
            BEGIN
                ALTER TABLE SavingAccountInstallmentDetail ADD IncomeReleased BIT NULL;
            END");
    }

    public static bool SetAccountPlanType(string orderId, string planType)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return false;
        }

        EnsureBulkColumns();
        return RunNonQuery(
            "UPDATE SavingAccountDetail SET PlanType = '" + Escape(planType ?? string.Empty)
            + "' WHERE orderid = '" + Escape(orderId.Trim()) + "'");
    }

    public static string InsertBulkSavingAccount(
        string orderId,
        string userId,
        decimal amount,
        string onlineTransactionId,
        string imageName,
        string entryBy)
    {
        EnsureBulkColumns();

        orderId = (orderId ?? string.Empty).Trim();
        userId = (userId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(userId))
        {
            return "0";
        }

        if (IsOnlineTransactionIdUsed(onlineTransactionId))
        {
            return "u";
        }

        DataTable pending = RunSelect(@"
SELECT TOP 1 id FROM SavingAccountDetail WITH (NOLOCK)
WHERE LTRIM(RTRIM(userid)) = '" + Escape(userId) + @"'
  AND LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('pending', '0')");
        if (pending != null && pending.Rows.Count > 0)
        {
            return "f";
        }

        bool hasExisting = false;
        DataTable existing = RunSelect(@"
SELECT TOP 1 id FROM SavingAccountDetail WITH (NOLOCK)
WHERE LTRIM(RTRIM(userid)) = '" + Escape(userId) + @"'
  AND LOWER(LTRIM(RTRIM(ISNULL(status, '')))) NOT IN ('rejected', '2', 'cancelled', 'canceled')");
        if (existing != null && existing.Rows.Count > 0)
        {
            hasExisting = true;
        }

        string orderType = hasExisting ? "Repurchase" : "FreshPurchase";
        System.Globalization.CultureInfo inv = System.Globalization.CultureInfo.InvariantCulture;
        string amountSql = amount.ToString("0.##", inv);
        string utr = Escape((onlineTransactionId ?? string.Empty).Trim());
        string image = Escape(imageName ?? string.Empty);
        string by = Escape(entryBy ?? string.Empty);

        string planCol = HasTableColumn("SavingAccountDetail", "PlanType") ? ", PlanType" : string.Empty;
        string planVal = HasTableColumn("SavingAccountDetail", "PlanType") ? ", 'Bulk18'" : string.Empty;

        string sql = @"
DECLARE @productid INT = 0;
IF OBJECT_ID('dbo.SavingInstallmentProductAssign', 'U') IS NOT NULL
BEGIN
    SET @productid = ISNULL((
        SELECT TOP 1 a.ProductId
        FROM SavingInstallmentProductAssign a WITH (NOLOCK)
        WHERE ISNULL(a.Status, 1) = 1 AND a.InstallmentNo = 1
    ), 0);
END
IF (@productid = 0)
BEGIN
    SET @productid = ISNULL((
        SELECT TOP 1 sd.productid
        FROM SavingMonthlyProductDetail sd WITH (NOLOCK)
        WHERE sd.Status = 1
    ), 0);
END

DECLARE @gst DECIMAL(18,2) = ISNULL((SELECT gst FROM SavingProductMaster WITH (NOLOCK) WHERE id = @productid), 0);
DECLARE @sgst DECIMAL(18,2), @cgst DECIMAL(18,2), @igst DECIMAL(18,2), @stateid INT;
SET @stateid = ISNULL((
    SELECT cm.stateid
    FROM UserDetail ud WITH (NOLOCK)
    LEFT JOIN CityMaster cm WITH (NOLOCK) ON cm.CityId = ud.CityId
    WHERE ud.userid = '" + Escape(userId) + @"'
), 0);

IF (@stateid = 14)
BEGIN
    SET @sgst = (@gst / 2);
    SET @cgst = (@gst / 2);
    SET @igst = 0;
END
ELSE
BEGIN
    SET @sgst = 0;
    SET @cgst = 0;
    SET @igst = @gst;
END

INSERT INTO SavingAccountDetail (
    OrderId, UserId, Amount, OnlineTransactionId, ImageName, Status, EntryBy, EntryDate,
    productid, couponcode, sgst, cgst, igst, ordertype" + planCol + @"
)
VALUES (
    '" + Escape(orderId) + @"', '" + Escape(userId) + @"', " + amountSql + @",
    '" + utr + @"', '" + image + @"', 'Pending', '" + by + @"', GETDATE(),
    @productid, NULL, @sgst, @cgst, @igst, '" + orderType + @"'" + planVal + @"
);";

        if (!RunNonQuery(sql))
        {
            return "0";
        }

        SetAccountPlanType(orderId, "Bulk18");
        return "t";
    }

    public static void EnsureBulkInstallmentsForCoupon(string couponCode)
    {
        couponCode = (couponCode ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(couponCode))
        {
            return;
        }

        EnsureBulkColumns();
        EnsureInstallmentProductAssignTable();
        EnsureInstallmentDeliveryColumns();

        bool hasPlanType = HasTableColumn("SavingAccountDetail", "PlanType");
        if (!hasPlanType)
        {
            return;
        }

        // Only true Bulk18 prepaid plans — do not treat high EMI amount as bulk.
        string bulkFilter = @" AND LTRIM(RTRIM(ISNULL(sd.PlanType, ''))) = 'Bulk18'";

        DataTable accounts = RunSelect(@"
SELECT sd.id, sd.orderid, sd.userid, sd.amount, sd.status,
       sd.approvedate, sd.entrydate, sd.OnlineTransactionId
FROM SavingAccountDetail sd WITH (NOLOCK)
WHERE LTRIM(RTRIM(sd.couponcode)) = '" + Escape(couponCode) + "'" + bulkFilter);

        if (accounts == null || accounts.Rows.Count == 0)
        {
            return;
        }

        bool hasPrepaid = HasTableColumn("SavingAccountInstallmentDetail", "IsBulkPrepaid");
        bool hasIncome = HasTableColumn("SavingAccountInstallmentDetail", "IncomeReleased");
        bool hasDelivery = HasTableColumn("SavingAccountInstallmentDetail", "DeliveryStatus");

        foreach (DataRow row in accounts.Rows)
        {
            string orderId = GetRowString(row, "orderid");
            string userId = GetRowString(row, "userid");
            if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(userId))
            {
                continue;
            }

            string parentStatus = GetRowString(row, "status").ToLowerInvariant();
            bool parentApproved = parentStatus == "approved" || parentStatus == "approve" || parentStatus == "1" || parentStatus == "active";
            if (!parentApproved)
            {
                continue;
            }

            DateTime joinDate = DateTime.Today;
            DateTime parsedJoin;
            if (row["approvedate"] != DBNull.Value
                && DateTime.TryParse(Convert.ToString(row["approvedate"]), out parsedJoin))
            {
                joinDate = parsedJoin;
            }
            else if (row.Table.Columns.Contains("entrydate") && row["entrydate"] != DBNull.Value
                && DateTime.TryParse(Convert.ToString(row["entrydate"]), out parsedJoin))
            {
                joinDate = parsedJoin;
            }

            string parentUtr = GetRowString(row, "OnlineTransactionId");
            if (string.IsNullOrWhiteSpace(parentUtr))
            {
                parentUtr = "BULK-" + orderId;
            }

            decimal parentAmount = 0m;
            if (row.Table.Columns.Contains("amount") && row["amount"] != DBNull.Value)
            {
                decimal.TryParse(Convert.ToString(row["amount"]), out parentAmount);
            }
            decimal monthlyAmount = parentAmount >= 15000m
                ? Math.Round(parentAmount / 18m, 2)
                : 1000m;
            if (monthlyAmount <= 0m)
            {
                monthlyAmount = 1000m;
            }

            string extra = "";
            if (hasPrepaid)
            {
                extra += ", sa.IsBulkPrepaid = 1";
            }
            if (hasIncome)
            {
                extra += ", sa.IncomeReleased = ISNULL(sa.IncomeReleased, 0)";
            }
            if (hasDelivery)
            {
                extra += @", sa.DeliveryStatus = CASE
                    WHEN NULLIF(LTRIM(RTRIM(ISNULL(sa.DeliveryStatus, ''))), '') IS NULL
                        OR LOWER(LTRIM(RTRIM(sa.DeliveryStatus))) = 'scheduled'
                    THEN 'Scheduled' ELSE sa.DeliveryStatus END
                , sa.DeliveryStatusUpdatedOn = GETDATE()
                , sa.DeliveryStatusUpdatedBy = 'SYSTEM-BULK'";
            }

            string sql = @"
UPDATE sa
SET
    sa.Amount = " + monthlyAmount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + @",
    sa.Status = CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('approved', 'rejected', 'cancelled', 'pending', 'processing')
            THEN sa.Status
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(sa.Status, ''))), '') IS NULL
            THEN 'Paid'
        ELSE sa.Status
    END,
    sa.ApproveDate = CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'approved'
            THEN sa.ApproveDate
        ELSE sa.ApproveDate
    END,
    sa.RequestDate = ISNULL(sa.RequestDate, GETDATE()),
    sa.OnlineTransactionId = CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(sa.OnlineTransactionId, ''))), '') IS NULL
            OR sa.OnlineTransactionId LIKE 'BULK-%'
        THEN '" + Escape(parentUtr) + @"'
        ELSE sa.OnlineTransactionId
    END,
    sa.productid = CASE WHEN ISNULL(ipa.ProductId, 0) > 0 THEN ipa.ProductId ELSE sa.productid END,
    sa.InstallmentDate = CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('approved', 'pending', 'processing', 'rejected')
            THEN sa.InstallmentDate
        ELSE DATEADD(MONTH, ISNULL(TRY_CONVERT(INT, sa.InstNo), 2) - 1, CONVERT(date, '" + joinDate.ToString("yyyy-MM-dd") + @"'))
    END
    " + extra + @"
FROM SavingAccountInstallmentDetail sa
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
WHERE sa.OrderId = '" + Escape(orderId) + @"'
  AND LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM('" + Escape(userId) + @"'))
  AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) > 1
  AND LOWER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) NOT IN ('rejected', 'cancelled');";

            RunNonQuery(sql);
            SetAccountPlanType(orderId, "Bulk18");
        }
    }

    public static void EnsureBulkInstallmentsForUser(string userId)
    {
        userId = (userId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return;
        }

        EnsureBulkColumns();
        bool hasPlanType = HasTableColumn("SavingAccountDetail", "PlanType");
        if (!hasPlanType)
        {
            return;
        }

        string bulkFilter = @" AND LTRIM(RTRIM(ISNULL(sd.PlanType, ''))) = 'Bulk18'";

        DataTable coupons = RunSelect(@"
SELECT DISTINCT LTRIM(RTRIM(sd.couponcode)) AS couponcode
FROM SavingAccountDetail sd WITH (NOLOCK)
WHERE LTRIM(RTRIM(sd.UserId)) = LTRIM(RTRIM('" + Escape(userId) + @"'))
  AND NULLIF(LTRIM(RTRIM(sd.couponcode)), '') IS NOT NULL" + bulkFilter);

        if (coupons == null || coupons.Rows.Count == 0)
        {
            return;
        }

        foreach (DataRow row in coupons.Rows)
        {
            EnsureBulkInstallmentsForCoupon(GetRowString(row, "couponcode"));
        }
    }

    public static string FormatMoney(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "0.00";
        }

        decimal amount;
        if (decimal.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out amount)
            || decimal.TryParse(Convert.ToString(value), out amount))
        {
            return amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

        return "0.00";
    }

    public static string DisplayEmiAmountSql(string parentAlias, string amountAlias)
    {
        parentAlias = string.IsNullOrWhiteSpace(parentAlias) ? "sd" : parentAlias;
        amountAlias = string.IsNullOrWhiteSpace(amountAlias) ? parentAlias : amountAlias;
        string bulkCheck = HasTableColumn("SavingAccountDetail", "PlanType")
            ? "(LTRIM(RTRIM(ISNULL(" + parentAlias + ".PlanType, ''))) = 'Bulk18' OR ISNULL(" + parentAlias + ".Amount, 0) >= 15000)"
            : "(ISNULL(" + parentAlias + ".Amount, 0) >= 15000)";

        return @"CAST(CASE
            WHEN " + bulkCheck + @"
            THEN ROUND(CASE WHEN ISNULL(" + parentAlias + @".Amount, 0) > 0 THEN " + parentAlias + @".Amount ELSE 18000 END / CAST(18 AS DECIMAL(18,2)), 2)
            ELSE ISNULL(" + amountAlias + @".Amount, 0)
        END AS DECIMAL(18,2))";
    }

    public static void AddMissingFirstInstallmentRows(DataTable dt, string couponCode)
    {
        couponCode = (couponCode ?? string.Empty).Trim();
        if (dt == null || dt.Columns.Count == 0 || string.IsNullOrWhiteSpace(couponCode))
        {
            return;
        }

        string instCol = FindColumn(dt, "InstNo");
        if (!string.IsNullOrEmpty(instCol))
        {
            foreach (DataRow row in dt.Rows)
            {
                int instNo;
                if (int.TryParse(Convert.ToString(row[instCol]), out instNo) && instNo == 1)
                {
                    return;
                }
            }
        }

        EnsureInstallmentProductAssignTable();
        EnsureBulkColumns();
        string planSelect = HasTableColumn("SavingAccountDetail", "PlanType")
            ? "ISNULL(sd.PlanType, '') AS PlanType"
            : "CAST('' AS NVARCHAR(50)) AS PlanType";
        DataTable parent = RunSelect(@"
SELECT TOP 1
    sd.id,
    sd.userid,
    sd.orderid,
    sd.amount,
    sd.status,
    sd.approvedate,
    sd.entrydate,
    sd.OnlineTransactionId,
    sd.couponcode,
    sd.remark,
    " + planSelect + @",
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ISNULL(ipa.ProductId, 0) > 0
   AND ipa.InstallmentNo = 1
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
WHERE LTRIM(RTRIM(sd.couponcode)) = '" + Escape(couponCode) + @"'
  AND LOWER(LTRIM(RTRIM(ISNULL(sd.Status, '')))) NOT IN ('rejected', 'cancelled')
ORDER BY sd.id DESC");

        if (parent == null || parent.Rows.Count == 0)
        {
            return;
        }

        DataRow src = parent.Rows[0];
        decimal parentAmount = 0m;
        decimal.TryParse(Convert.ToString(src["amount"]), out parentAmount);
        bool isBulk = GetRowString(src, "PlanType").Equals("Bulk18", StringComparison.OrdinalIgnoreCase)
            || parentAmount >= 15000m;
        decimal monthly = isBulk
            ? Math.Round((parentAmount > 0m ? parentAmount : 18000m) / 18m, 2)
            : parentAmount;

        string parentStatus = GetRowString(src, "status");
        string statusNorm = parentStatus.ToLowerInvariant();
        bool parentApproved = statusNorm == "approved" || statusNorm == "approve" || statusNorm == "1" || statusNorm == "active";
        if (!parentApproved)
        {
            return;
        }

        DataRow dest = dt.NewRow();
        SetDisplayValue(dest, "id", src["id"]);
        SetDisplayValue(dest, "userid", src["userid"]);
        SetDisplayValue(dest, "orderid", src["orderid"]);
        SetDisplayValue(dest, "instno", 1);
        SetDisplayValue(dest, "amount", monthly);
        SetDisplayValue(dest, "installmentdate", src["approvedate"] != DBNull.Value ? src["approvedate"] : src["entrydate"]);
        SetDisplayValue(dest, "approvedate", src["approvedate"]);
        SetDisplayValue(dest, "status", parentApproved ? "Approved" : parentStatus);
        SetDisplayValue(dest, "ParentStatus", parentStatus);
        SetDisplayValue(dest, "couponcode", src["couponcode"]);
        SetDisplayValue(dest, "OnlineTransactionId", src["OnlineTransactionId"]);
        SetDisplayValue(dest, "productname", src["productname"]);
        SetDisplayValue(dest, "PlanType", src["PlanType"]);
        SetDisplayValue(dest, "remark", src["remark"]);
        SetDisplayValue(dest, "ParentAmount", parentAmount);
        SetDisplayValue(dest, "ParentApproveDate", src["approvedate"]);
        SetDisplayValue(dest, "ParentEntryDate", src["entrydate"]);
        SetDisplayValue(dest, "ParentOnlineTransactionId", src["OnlineTransactionId"]);
        dt.Rows.InsertAt(dest, 0);
    }

    static void SetDisplayValue(DataRow row, string columnName, object value)
    {
        string col = FindColumn(row.Table, columnName);
        if (string.IsNullOrEmpty(col) || value == null)
        {
            return;
        }

        try
        {
            row[col] = value;
        }
        catch
        {
        }
    }

    static string GetRowString(DataRow row, string columnName)
    {
        if (row == null || row.Table == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(row[columnName]).Trim();
    }

    public static void ApplyBulkInstallmentDisplayFallbacks(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        string dateCol = FindColumn(dt, "installmentdate");
        string txnCol = FindColumn(dt, "OnlineTransactionId");
        string instCol = FindColumn(dt, "InstNo");
        string amountCol = FindColumn(dt, "amount");
        string parentDateCol = FindColumn(dt, "ParentApproveDate");
        string parentEntryCol = FindColumn(dt, "ParentEntryDate");
        string parentTxnCol = FindColumn(dt, "ParentOnlineTransactionId");
        string parentAmtCol = FindColumn(dt, "ParentAmount");
        string planCol = FindColumn(dt, "PlanType");
        string statusCol = FindColumn(dt, "status");

        foreach (DataRow row in dt.Rows)
        {
            decimal parentAmount = 0m;
            if (!string.IsNullOrEmpty(parentAmtCol) && row[parentAmtCol] != DBNull.Value)
            {
                decimal.TryParse(Convert.ToString(row[parentAmtCol]), out parentAmount);
            }

            string planType = string.IsNullOrEmpty(planCol) ? string.Empty : Convert.ToString(row[planCol]).Trim();
            bool isBulk = planType.Equals("Bulk18", StringComparison.OrdinalIgnoreCase) || parentAmount >= 15000m;
            if (isBulk && !string.IsNullOrEmpty(amountCol))
            {
                decimal monthly = Math.Round((parentAmount > 0m ? parentAmount : 18000m) / 18m, 2);
                try
                {
                    row[amountCol] = monthly;
                }
                catch
                {
                    row[amountCol] = monthly.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            if (!string.IsNullOrEmpty(dateCol)
                && (row[dateCol] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(row[dateCol]))))
            {
                DateTime joinDate;
                bool hasJoin = false;
                if (!string.IsNullOrEmpty(parentDateCol) && row[parentDateCol] != DBNull.Value
                    && DateTime.TryParse(Convert.ToString(row[parentDateCol]), out joinDate))
                {
                    hasJoin = true;
                }
                else if (!string.IsNullOrEmpty(parentEntryCol) && row[parentEntryCol] != DBNull.Value
                    && DateTime.TryParse(Convert.ToString(row[parentEntryCol]), out joinDate))
                {
                    hasJoin = true;
                }
                else
                {
                    joinDate = DateTime.Today;
                }

                int instNo = 2;
                if (!string.IsNullOrEmpty(instCol))
                {
                    int.TryParse(Convert.ToString(row[instCol]), out instNo);
                    if (instNo < 1)
                    {
                        instNo = 2;
                    }
                }

                if (hasJoin || instNo > 0)
                {
                    row[dateCol] = joinDate.Date.AddMonths(instNo - 1);
                }
            }

            if (!string.IsNullOrEmpty(txnCol)
                && string.IsNullOrWhiteSpace(Convert.ToString(row[txnCol]))
                && !string.IsNullOrEmpty(parentTxnCol)
                && !string.IsNullOrWhiteSpace(Convert.ToString(row[parentTxnCol])))
            {
                string rowStatus = string.IsNullOrEmpty(statusCol)
                    ? string.Empty
                    : Convert.ToString(row[statusCol]).Trim().ToLowerInvariant();
                bool isApproved = rowStatus == "approved"
                    || rowStatus == "approve"
                    || rowStatus == "1"
                    || rowStatus == "active";
                bool isBulkPrepaid = planType.Equals("Bulk18", StringComparison.OrdinalIgnoreCase)
                    && rowStatus == "paid";
                if (isApproved || isBulkPrepaid)
                {
                    row[txnCol] = Convert.ToString(row[parentTxnCol]).Trim();
                }
            }
        }
    }

    static string FindColumn(DataTable dt, string name)
    {
        if (dt == null || string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        foreach (DataColumn col in dt.Columns)
        {
            if (string.Equals(col.ColumnName, name, StringComparison.OrdinalIgnoreCase))
            {
                return col.ColumnName;
            }
        }

        return string.Empty;
    }

    public static void ProcessBulkSavingSchedule()
    {
        EnsureBulkColumns();
        ConfirmDueBulkInstallmentDeliveries();
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunDataTableProcedure("sp_processSavingBulkSchedule", new SqlParameter[0]);
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

    const int BulkPaySchemaVersion = 3;
    static int AppliedBulkPaySchemaVersion;

    static bool HasTable(string tableName)
    {
        DataTable dt = RunSelect(
            "SELECT OBJECT_ID('dbo." + Escape(tableName) + "', 'U') AS ObjId");
        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["ObjId"] == DBNull.Value)
        {
            return false;
        }

        return true;
    }

    public static void EnsureBulkInstallmentPaymentSchema()
    {
        if (AppliedBulkPaySchemaVersion == BulkPaySchemaVersion)
        {
            return;
        }

        EnsureBulkColumns();
        RunNonQuery(@"
IF OBJECT_ID('dbo.SavingBulkInstallmentPayment', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingBulkInstallmentPayment
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserId NVARCHAR(100) NOT NULL,
        OrderId NVARCHAR(100) NULL,
        CouponCode NVARCHAR(100) NULL,
        AccountId INT NULL,
        Amount DECIMAL(18,2) NULL,
        InstCount INT NULL,
        OnlineTransactionId NVARCHAR(100) NULL,
        ImageName NVARCHAR(MAX) NULL,
        Status NVARCHAR(50) NOT NULL CONSTRAINT DF_SavingBulkInstPay_Status DEFAULT ('Processing'),
        RequestDate DATETIME NOT NULL CONSTRAINT DF_SavingBulkInstPay_RequestDate DEFAULT (GETDATE()),
        ApproveDate DATETIME NULL,
        ApproveBy NVARCHAR(100) NULL,
        Remark NVARCHAR(MAX) NULL,
        EntryBy NVARCHAR(100) NULL,
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingBulkInstPay_EntryDate DEFAULT (GETDATE())
    );
END");
        RunNonQuery(@"
IF COL_LENGTH('dbo.SavingAccountInstallmentDetail', 'BulkPaymentId') IS NULL
BEGIN
    ALTER TABLE dbo.SavingAccountInstallmentDetail ADD BulkPaymentId INT NULL;
END");
        EnsureInstallmentDeliveryColumns();

        RecreateProcedure("sp_add_SavingBulkInstallmentPayment", GetAddBulkInstallmentPaymentProcSql());
        RecreateProcedure("sp_approveSavingBulkInstallmentPayment", GetApproveBulkInstallmentPaymentProcSql());
        RecreateProcedure("sp_rejectSavingBulkInstallmentPayment", GetRejectBulkInstallmentPaymentProcSql());

        if (HasTable("SavingBulkInstallmentPayment")
            && ProcedureExists("sp_add_SavingBulkInstallmentPayment")
            && ProcedureExists("sp_approveSavingBulkInstallmentPayment")
            && ProcedureExists("sp_rejectSavingBulkInstallmentPayment"))
        {
            AppliedBulkPaySchemaVersion = BulkPaySchemaVersion;
        }
    }

    static bool ProcedureExists(string procName)
    {
        DataTable dt = RunSelect(
            "SELECT OBJECT_ID('dbo." + Escape(procName) + "', 'P') AS ObjId");
        return dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ObjId"] != DBNull.Value;
    }

    static void RecreateProcedure(string procName, string createSql)
    {
        RunNonQuery("IF OBJECT_ID('dbo." + Escape(procName) + "', 'P') IS NOT NULL DROP PROCEDURE dbo." + procName + ";");
        RunNonQuery(createSql == null ? string.Empty : createSql.Trim());
    }

    public static void ConfirmDueBulkInstallmentDeliveries()
    {
        if (!HasTableColumn("SavingAccountInstallmentDetail", "BulkPaymentId")
            || !HasTableColumn("SavingAccountInstallmentDetail", "DeliveryStatus"))
        {
            return;
        }

        RunNonQuery(@"
UPDATE sa
SET
    sa.DeliveryStatus = 'Confirmed',
    sa.DeliveryStatusUpdatedOn = GETDATE(),
    sa.DeliveryStatusUpdatedBy = 'SYSTEM-BULK17'
FROM SavingAccountInstallmentDetail sa
WHERE ISNULL(sa.BulkPaymentId, 0) > 0
  AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('APPROVED', 'APPROVE', '1')
  AND CONVERT(date, ISNULL(sa.ApproveDate, '99991231')) <= CONVERT(date, GETDATE())
  AND (
        UPPER(LTRIM(RTRIM(ISNULL(sa.DeliveryStatus, '')))) IN ('SCHEDULED', '')
        OR sa.DeliveryStatus IS NULL
      )");
    }

    public static string ExecuteScalarProc(string procName, SqlParameter[] parameters)
    {
        Data objData = new Data();
        string res = "0";
        SqlConnection cn = null;
        SqlTransaction tr = null;
        try
        {
            cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            res = objData.RunInsUpDelQueryTransProcScalar(procName, tr, parameters ?? new SqlParameter[0]);
            tr.Commit();
        }
        catch
        {
            res = "0";
            if (tr != null)
            {
                try { tr.Rollback(); }
                catch { }
            }
        }
        finally
        {
            try { objData.EndConnection(); }
            catch { }
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return string.IsNullOrWhiteSpace(res) ? "0" : res.Trim();
    }

    static string GetAddBulkInstallmentPaymentProcSql()
    {
        return @"
CREATE PROC dbo.sp_add_SavingBulkInstallmentPayment
    @UserId NVARCHAR(100),
    @CouponCode NVARCHAR(100),
    @OnlineTransactionId NVARCHAR(100),
    @ImageName NVARCHAR(MAX),
    @EntryBy NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @user NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@UserId, '')));
    DECLARE @coupon NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@CouponCode, '')));
    DECLARE @utr NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OnlineTransactionId, '')));
    DECLARE @image NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@ImageName, '')));
    DECLARE @by NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@EntryBy, @user)));
    IF (@user = '' OR @coupon = '') BEGIN SELECT 'n'; RETURN; END
    IF (
        @utr <> '' AND @utr NOT LIKE 'CASH-%'
        AND (
            EXISTS (SELECT 1 FROM SavingAccountDetail WITH (NOLOCK) WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr) AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED')
            OR EXISTS (SELECT 1 FROM SavingAccountInstallmentDetail WITH (NOLOCK) WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr) AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED')
            OR EXISTS (SELECT 1 FROM SavingBulkInstallmentPayment WITH (NOLOCK) WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr) AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED')
        )
    ) BEGIN SELECT 'u'; RETURN; END
    DECLARE @accountId INT, @orderId NVARCHAR(100), @planType NVARCHAR(50);
    SELECT TOP 1 @accountId = sd.id, @orderId = sd.orderid
    FROM SavingAccountDetail sd WITH (NOLOCK)
    WHERE LTRIM(RTRIM(sd.UserId)) = @user
      AND LTRIM(RTRIM(ISNULL(sd.couponcode, ''))) = @coupon
      AND UPPER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('APPROVED', 'APPROVE', '1', 'ACTIVE')
    ORDER BY sd.id DESC;
    IF (@accountId IS NULL OR ISNULL(@orderId, '') = '') BEGIN SELECT 'n'; RETURN; END
    IF COL_LENGTH('SavingAccountDetail', 'PlanType') IS NOT NULL
    BEGIN
        SELECT @planType = PlanType FROM SavingAccountDetail WITH (NOLOCK) WHERE id = @accountId;
        IF (UPPER(LTRIM(RTRIM(ISNULL(@planType, '')))) = 'BULK18') BEGIN SELECT 'n'; RETURN; END
    END
    IF EXISTS (
        SELECT 1 FROM SavingBulkInstallmentPayment WITH (NOLOCK)
        WHERE LTRIM(RTRIM(UserId)) = @user AND LTRIM(RTRIM(ISNULL(CouponCode, ''))) = @coupon
          AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) = 'PROCESSING'
    ) BEGIN SELECT 'f'; RETURN; END
    DECLARE @pendingCount INT = 0, @blockedCount INT = 0, @totalAmount DECIMAL(18,2) = 0;
    DECLARE @hasInstCoupon BIT = CASE WHEN COL_LENGTH('SavingAccountInstallmentDetail', 'CouponCode') IS NOT NULL THEN 1 ELSE 0 END;
    SELECT
        @pendingCount = SUM(CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID') THEN 1 ELSE 0 END),
        @blockedCount = SUM(CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'PROCESSING' THEN 1 ELSE 0 END),
        @totalAmount = SUM(CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID') THEN ISNULL(sa.Amount, 0) ELSE 0 END)
    FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
    WHERE LTRIM(RTRIM(sa.UserId)) = @user
      AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) BETWEEN 2 AND 18
      AND (
            (@hasInstCoupon = 1 AND (
                LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))) = @coupon
                OR (NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), '') IS NULL AND sa.OrderId = @orderId)
            ))
            OR (@hasInstCoupon = 0 AND sa.OrderId = @orderId)
          );
    IF (ISNULL(@pendingCount, 0) <= 0 OR ISNULL(@blockedCount, 0) <> 0)
    BEGIN SELECT 'n'; RETURN; END
    INSERT INTO SavingBulkInstallmentPayment (UserId, OrderId, CouponCode, AccountId, Amount, InstCount, OnlineTransactionId, ImageName, Status, RequestDate, EntryBy, EntryDate)
    VALUES (@user, @orderId, @coupon, @accountId, ISNULL(@totalAmount, 0), ISNULL(@pendingCount, 0), @utr, @image, 'Processing', GETDATE(), @by, GETDATE());
    DECLARE @bulkId INT = SCOPE_IDENTITY();
    UPDATE SavingAccountInstallmentDetail
    SET Status = 'Processing', OnlineTransactionId = @utr, ImageName = @image, RequestDate = GETDATE(), Remark = NULL, BulkPaymentId = @bulkId
    WHERE LTRIM(RTRIM(UserId)) = @user
      AND ISNULL(TRY_CONVERT(INT, InstNo), 0) BETWEEN 2 AND 18
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID')
      AND (
            (@hasInstCoupon = 1 AND (
                LTRIM(RTRIM(ISNULL(CouponCode, ''))) = @coupon
                OR (NULLIF(LTRIM(RTRIM(ISNULL(CouponCode, ''))), '') IS NULL AND OrderId = @orderId)
            ))
            OR (@hasInstCoupon = 0 AND OrderId = @orderId)
          );
    SELECT 't';
END";
    }

    static string GetApproveBulkInstallmentPaymentProcSql()
    {
        return @"
CREATE PROC dbo.sp_approveSavingBulkInstallmentPayment
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @status NVARCHAR(50), @orderId NVARCHAR(100), @userId NVARCHAR(100), @requestDate DATETIME, @bulkId INT = @id;
    SELECT @status = bp.Status, @orderId = bp.OrderId, @userId = bp.UserId, @requestDate = bp.RequestDate
    FROM SavingBulkInstallmentPayment bp WHERE bp.Id = @bulkId;
    IF (@status IS NULL) BEGIN SELECT '0'; RETURN; END
    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PROCESSING') BEGIN SELECT 'f'; RETURN; END
    DECLARE @baseDate DATE = CONVERT(date, ISNULL(@requestDate, GETDATE()));
    DECLARE @remarkText NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@Remark, '')));
    DECLARE @adminUser NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@Approveby, '')));
    UPDATE SavingBulkInstallmentPayment
    SET Status = 'Approved', ApproveDate = GETDATE(), ApproveBy = @adminUser,
        Remark = CASE WHEN @remarkText = '' THEN Remark ELSE @remarkText END
    WHERE Id = @bulkId;
    UPDATE sa
    SET sa.Status = 'Approved',
        sa.ApproveDate = DATEADD(MONTH, ISNULL(TRY_CONVERT(INT, sa.InstNo), 2) - 1, @baseDate),
        sa.OnlineTransactionId = ISNULL(NULLIF(LTRIM(RTRIM(sa.OnlineTransactionId)), ''), bp.OnlineTransactionId),
        sa.ImageName = ISNULL(NULLIF(LTRIM(RTRIM(sa.ImageName)), ''), bp.ImageName),
        sa.Remark = CASE WHEN @remarkText = '' THEN sa.Remark ELSE @remarkText END,
        sa.BulkPaymentId = @bulkId,
        sa.productid = CASE WHEN ISNULL(ipa.ProductId, 0) > 0 THEN ipa.ProductId ELSE sa.productid END
    FROM SavingAccountInstallmentDetail sa
    INNER JOIN SavingBulkInstallmentPayment bp ON bp.Id = @bulkId
    LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
        ON ISNULL(ipa.Status, 1) = 1 AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
    WHERE (ISNULL(sa.BulkPaymentId, 0) = @bulkId
        OR (LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(@userId))
            AND LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))) = LTRIM(RTRIM(ISNULL(bp.CouponCode, '')))
            AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) BETWEEN 2 AND 18
            AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'PROCESSING'))
      AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('PROCESSING', 'PENDING');
    IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatus') IS NOT NULL
    BEGIN
        UPDATE SavingAccountInstallmentDetail
        SET DeliveryStatus = 'Scheduled', DeliveryStatusUpdatedOn = GETDATE(), DeliveryStatusUpdatedBy = @adminUser
        WHERE ISNULL(BulkPaymentId, 0) = @bulkId
          AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) IN ('APPROVED', 'APPROVE', '1');
    END
    SELECT 't';
END";
    }

    static string GetRejectBulkInstallmentPaymentProcSql()
    {
        return @"
CREATE PROC dbo.sp_rejectSavingBulkInstallmentPayment
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @status NVARCHAR(50), @orderId NVARCHAR(100), @userId NVARCHAR(100), @bulkId INT = @id;
    DECLARE @remarkText NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@Remark, '')));
    DECLARE @rejectBy NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@Approveby, '')));
    IF (@remarkText = '') BEGIN SELECT 'r'; RETURN; END
    SELECT @status = bp.Status, @orderId = bp.OrderId, @userId = bp.UserId
    FROM SavingBulkInstallmentPayment bp WHERE bp.Id = @bulkId;
    IF (@status IS NULL) BEGIN SELECT '0'; RETURN; END
    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PROCESSING') BEGIN SELECT 'f'; RETURN; END
    UPDATE SavingBulkInstallmentPayment
    SET Status = 'Rejected', ApproveDate = GETDATE(), ApproveBy = @rejectBy, Remark = @remarkText
    WHERE Id = @bulkId;
    UPDATE SavingAccountInstallmentDetail
    SET Status = 'Rejected', Remark = @remarkText, BulkPaymentId = @bulkId
    WHERE ISNULL(BulkPaymentId, 0) = @bulkId
       OR (LTRIM(RTRIM(UserId)) = LTRIM(RTRIM(@userId))
           AND LTRIM(RTRIM(ISNULL(CouponCode, ''))) = (
                SELECT LTRIM(RTRIM(ISNULL(CouponCode, ''))) FROM SavingBulkInstallmentPayment WHERE Id = @bulkId)
           AND ISNULL(TRY_CONVERT(INT, InstNo), 0) BETWEEN 2 AND 18
           AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) = 'PROCESSING');
    SELECT 't';
END";
    }
}

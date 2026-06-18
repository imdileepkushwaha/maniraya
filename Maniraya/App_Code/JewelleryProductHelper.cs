using DataTier;
using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public static class JewelleryProductHelper
{
    public static void EnsureTable()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string createSql = @"
                IF OBJECT_ID('tbl_JewelleryProduct', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_JewelleryProduct (
                        JewelleryId INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(300) NOT NULL,
                        ShortDescription NVARCHAR(1000) NULL,
                        Description NVARCHAR(MAX) NULL,
                        Image1 NVARCHAR(500) NULL,
                        Image2 NVARCHAR(500) NULL,
                        Image3 NVARCHAR(500) NULL,
                        Image4 NVARCHAR(500) NULL,
                        GoldWeight DECIMAL(18,3) NOT NULL DEFAULT 0,
                        SilverWeight DECIMAL(18,3) NOT NULL DEFAULT 0,
                        DiamondCarat DECIMAL(18,3) NOT NULL DEFAULT 0,
                        MakingCharges DECIMAL(18,2) NOT NULL DEFAULT 0,
                        GstPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
                        GoldRate DECIMAL(18,2) NOT NULL DEFAULT 0,
                        SilverRate DECIMAL(18,2) NOT NULL DEFAULT 0,
                        DiamondRate DECIMAL(18,2) NOT NULL DEFAULT 0,
                        GoldAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
                        SilverAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
                        DiamondAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
                        Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
                        GstAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
                        Price DECIMAL(18,2) NOT NULL DEFAULT 0,
                        MRP DECIMAL(18,2) NOT NULL DEFAULT 0,
                        BV DECIMAL(18,2) NOT NULL DEFAULT 0,
                        HSNCode NVARCHAR(50) NULL,
                        MetalType NVARCHAR(50) NULL,
                        JewelleryType NVARCHAR(100) NULL,
                        SizeId INT NULL,
                        SizeName NVARCHAR(100) NULL,
                        Status BIT NOT NULL DEFAULT 1,
                        CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
                        CreatedBy NVARCHAR(100) NULL
                    )
                END";
                objData.RunInsUpDelQuery(createSql);
                EnsureColumns(objData);
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

    static void EnsureColumns(Data objData)
    {
        string alterSql = @"
            IF COL_LENGTH('tbl_JewelleryProduct', 'MetalType') IS NULL
                ALTER TABLE tbl_JewelleryProduct ADD MetalType NVARCHAR(50) NULL;
            IF COL_LENGTH('tbl_JewelleryProduct', 'JewelleryType') IS NULL
                ALTER TABLE tbl_JewelleryProduct ADD JewelleryType NVARCHAR(100) NULL;
            IF COL_LENGTH('tbl_JewelleryProduct', 'SizeId') IS NULL
                ALTER TABLE tbl_JewelleryProduct ADD SizeId INT NULL;
            IF COL_LENGTH('tbl_JewelleryProduct', 'SizeName') IS NULL
                ALTER TABLE tbl_JewelleryProduct ADD SizeName NVARCHAR(100) NULL;";

        string sizeTableSql = @"
            IF OBJECT_ID('tbl_JewelleryProductSize', 'U') IS NULL
            BEGIN
                CREATE TABLE tbl_JewelleryProductSize (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    JewelleryId INT NOT NULL,
                    SizeId INT NOT NULL,
                    SizeName NVARCHAR(100) NOT NULL,
                    CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
                );
                CREATE INDEX IX_tbl_JewelleryProductSize_JewelleryId ON tbl_JewelleryProductSize(JewelleryId);
            END";

        objData.RunInsUpDelQuery(alterSql);
        objData.RunInsUpDelQuery(sizeTableSql);
    }

    public static int? AddSize(string sizeName, string mentionBy)
    {
        if (string.IsNullOrWhiteSpace(sizeName))
        {
            return null;
        }

        string trimmedName = sizeName.Trim();
        DataTable sizes = GetActiveSizes();
        if (sizes != null)
        {
            foreach (DataRow row in sizes.Rows)
            {
                string existingName = Convert.ToString(row["SizeName"]) ?? string.Empty;
                if (string.Equals(existingName.Trim(), trimmedName, StringComparison.OrdinalIgnoreCase))
                {
                    int existingId;
                    if (int.TryParse(Convert.ToString(row["Id"]), out existingId))
                    {
                        return existingId;
                    }
                }
            }
        }

        clsProduct product = new clsProduct();
        product.Sizename = trimmedName;
        product.MentionBy = mentionBy ?? "Admin";
        string result = product.Insert_Size(product);

        if (result == "f")
        {
            DataTable refreshed = GetActiveSizes();
            if (refreshed != null)
            {
                foreach (DataRow row in refreshed.Rows)
                {
                    string existingName = Convert.ToString(row["SizeName"]) ?? string.Empty;
                    if (string.Equals(existingName.Trim(), trimmedName, StringComparison.OrdinalIgnoreCase))
                    {
                        int existingId;
                        if (int.TryParse(Convert.ToString(row["Id"]), out existingId))
                        {
                            return existingId;
                        }
                    }
                }
            }

            return null;
        }

        if (result != "t")
        {
            return null;
        }

        DataTable created = GetActiveSizes();
        if (created != null)
        {
            foreach (DataRow row in created.Rows)
            {
                string createdName = Convert.ToString(row["SizeName"]) ?? string.Empty;
                if (string.Equals(createdName.Trim(), trimmedName, StringComparison.OrdinalIgnoreCase))
                {
                    int newId;
                    if (int.TryParse(Convert.ToString(row["Id"]), out newId))
                    {
                        return newId;
                    }
                }
            }
        }

        return null;
    }

    public static int AddSizesFromInput(string input, string mentionBy, List<int> selectedSizeIds)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        int addedCount = 0;
        string[] parts = input.Split(new[] { ',', ';', '|', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            string sizeName = part.Trim();
            if (sizeName.Length == 0)
            {
                continue;
            }

            int? sizeId = AddSize(sizeName, mentionBy);
            if (sizeId.HasValue && selectedSizeIds != null && !selectedSizeIds.Contains(sizeId.Value))
            {
                selectedSizeIds.Add(sizeId.Value);
                addedCount++;
            }
        }

        return addedCount;
    }

    public static DataTable GetActiveSizes()
    {
        Data objData = new Data();
        DataTable dt = new DataTable();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable result = objData.RunDataTable(
                    "SELECT Id, sizeName AS SizeName FROM sizeMaster WHERE ISNULL(Status, '1') = '1' ORDER BY sizeName");
                if (result != null)
                {
                    dt = result;
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }

        return dt;
    }

    public static string InsertJewellery(
        string title,
        string shortDescription,
        string description,
        string image1,
        string image2,
        string image3,
        string image4,
        string metalType,
        string jewelleryType,
        System.Collections.Generic.IList<JewellerySizeItem> sizes,
        decimal goldWeight,
        decimal silverWeight,
        decimal diamondCarat,
        decimal makingCharges,
        decimal gstPercent,
        decimal goldRate,
        decimal silverRate,
        decimal diamondRate,
        JewelleryPriceResult pricing,
        decimal bv,
        string hsnCode,
        string createdBy)
    {
        EnsureTable();

        if (sizes == null || sizes.Count == 0)
        {
            return "0";
        }

        int? firstSizeId = sizes[0].SizeId;
        string allSizeNames = string.Join(", ", sizes.Select(s => s.SizeName));

        Data objData = new Data();
        SqlConnection cn = null;
        SqlTransaction tr = null;

        try
        {
            cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            SqlParameter[] parameters =
            {
                new SqlParameter("@Title", title ?? string.Empty),
                new SqlParameter("@ShortDescription", (object)shortDescription ?? DBNull.Value),
                new SqlParameter("@Description", (object)description ?? DBNull.Value),
                new SqlParameter("@Image1", image1 ?? string.Empty),
                new SqlParameter("@Image2", image2 ?? string.Empty),
                new SqlParameter("@Image3", image3 ?? string.Empty),
                new SqlParameter("@Image4", image4 ?? string.Empty),
                new SqlParameter("@MetalType", metalType ?? string.Empty),
                new SqlParameter("@JewelleryType", jewelleryType ?? string.Empty),
                new SqlParameter("@SizeId", (object)firstSizeId ?? DBNull.Value),
                new SqlParameter("@SizeName", allSizeNames ?? string.Empty),
                new SqlParameter("@GoldWeight", goldWeight),
                new SqlParameter("@SilverWeight", silverWeight),
                new SqlParameter("@DiamondCarat", diamondCarat),
                new SqlParameter("@MakingCharges", makingCharges),
                new SqlParameter("@GstPercent", gstPercent),
                new SqlParameter("@GoldRate", goldRate),
                new SqlParameter("@SilverRate", silverRate),
                new SqlParameter("@DiamondRate", diamondRate),
                new SqlParameter("@GoldAmount", pricing.GoldAmount),
                new SqlParameter("@SilverAmount", pricing.SilverAmount),
                new SqlParameter("@DiamondAmount", pricing.DiamondAmount),
                new SqlParameter("@Subtotal", pricing.Subtotal),
                new SqlParameter("@GstAmount", pricing.GstAmount),
                new SqlParameter("@Price", pricing.Price),
                new SqlParameter("@MRP", pricing.Mrp),
                new SqlParameter("@BV", bv),
                new SqlParameter("@HSNCode", (object)hsnCode ?? DBNull.Value),
                new SqlParameter("@CreatedBy", createdBy ?? "Admin")
            };

            string result = objData.RunInsUpDelQueryTransProcScalar("sp_add_JewelleryProduct", tr, parameters);

            int jewelleryId;
            if (!int.TryParse(result, out jewelleryId) || jewelleryId <= 0)
            {
                tr.Rollback();
                return "0";
            }

            foreach (JewellerySizeItem size in sizes)
            {
                string safeSizeName = (size.SizeName ?? string.Empty).Replace("'", "''");
                string insertSizeSql = string.Format(
                    "INSERT INTO tbl_JewelleryProductSize (JewelleryId, SizeId, SizeName) VALUES ({0}, {1}, '{2}')",
                    jewelleryId,
                    size.SizeId,
                    safeSizeName);
                objData.RunInsUpDelQueryTrans(insertSizeSql, tr);
            }

            tr.Commit();
            return result;
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
            if (tr != null)
            {
                tr.Dispose();
            }

            objData.EndConnection();
        }
    }
}

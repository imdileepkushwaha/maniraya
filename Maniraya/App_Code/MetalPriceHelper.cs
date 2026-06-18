using DataTier;
using System;
using System.Data;

public static class MetalPriceHelper
{
    public const string MetalGold = "Gold";
    public const string MetalSilver = "Silver";
    public const string MetalDiamond = "Diamond";

    static readonly Tuple<string, string, decimal>[] DefaultMetals = new[]
    {
        Tuple.Create(MetalGold, "Per Gram", 0m),
        Tuple.Create(MetalSilver, "Per Gram", 0m),
        Tuple.Create(MetalDiamond, "Per Carat", 0m)
    };

    public static void EnsureTableAndSeedDefaults()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string createSql = @"
                IF OBJECT_ID('tbl_MetalPrice', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_MetalPrice (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        MetalType NVARCHAR(20) NOT NULL,
                        Price DECIMAL(18,2) NOT NULL DEFAULT 0,
                        PriceUnit NVARCHAR(30) NOT NULL DEFAULT 'Per Gram',
                        UpdatedOn DATETIME NOT NULL DEFAULT GETDATE(),
                        UpdatedBy NVARCHAR(100) NULL
                    )
                    CREATE UNIQUE INDEX UX_tbl_MetalPrice_MetalType ON tbl_MetalPrice(MetalType)
                END";
                objData.RunInsUpDelQuery(createSql);

                foreach (Tuple<string, string, decimal> metal in DefaultMetals)
                {
                    string type = metal.Item1.Replace("'", "''");
                    DataTable exists = objData.RunDataTable(
                        "SELECT Id FROM tbl_MetalPrice WHERE MetalType = '" + type + "'");
                    if (exists == null || exists.Rows.Count == 0)
                    {
                        string unit = metal.Item2.Replace("'", "''");
                        string insertSql = string.Format(
                            "INSERT INTO tbl_MetalPrice (MetalType, Price, PriceUnit, UpdatedBy) VALUES ('{0}', {1}, '{2}', 'System')",
                            type,
                            metal.Item3.ToString("0.00"),
                            unit);
                        objData.RunInsUpDelQuery(insertSql);
                    }
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
    }

    public static DataTable GetAllMetalPrices()
    {
        EnsureTableAndSeedDefaults();
        return RunSelect(
            "SELECT Id, MetalType, Price, PriceUnit, UpdatedOn, UpdatedBy FROM tbl_MetalPrice ORDER BY CASE MetalType WHEN 'Gold' THEN 1 WHEN 'Silver' THEN 2 WHEN 'Diamond' THEN 3 ELSE 4 END");
    }

    public static decimal GetPrice(string metalType)
    {
        if (string.IsNullOrWhiteSpace(metalType))
        {
            return 0m;
        }

        EnsureTableAndSeedDefaults();
        string safeType = metalType.Trim().Replace("'", "''");
        DataTable dt = RunSelect(
            "SELECT TOP 1 Price FROM tbl_MetalPrice WHERE MetalType = '" + safeType + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            decimal price;
            if (decimal.TryParse(Convert.ToString(dt.Rows[0]["Price"]), out price))
            {
                return price;
            }
        }

        return 0m;
    }

    public static decimal GetGoldPrice()
    {
        return GetPrice(MetalGold);
    }

    public static decimal GetSilverPrice()
    {
        return GetPrice(MetalSilver);
    }

    public static decimal GetDiamondPrice()
    {
        return GetPrice(MetalDiamond);
    }

    public static string GetPriceUnit(string metalType)
    {
        if (string.IsNullOrWhiteSpace(metalType))
        {
            return string.Empty;
        }

        EnsureTableAndSeedDefaults();
        string safeType = metalType.Trim().Replace("'", "''");
        DataTable dt = RunSelect(
            "SELECT TOP 1 PriceUnit FROM tbl_MetalPrice WHERE MetalType = '" + safeType + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            return Convert.ToString(dt.Rows[0]["PriceUnit"]) ?? string.Empty;
        }

        return string.Empty;
    }

    public static bool SaveMetalPrice(string metalType, decimal price, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(metalType))
        {
            return false;
        }

        EnsureTableAndSeedDefaults();

        string safeType = metalType.Trim().Replace("'", "''");
        string safeUser = (updatedBy ?? "Admin").Replace("'", "''");
        string priceValue = price.ToString("0.00");

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable exists = objData.RunDataTable(
                    "SELECT Id FROM tbl_MetalPrice WHERE MetalType = '" + safeType + "'");

                string sql;
                if (exists != null && exists.Rows.Count > 0)
                {
                    sql = string.Format(
                        "UPDATE tbl_MetalPrice SET Price = {0}, UpdatedOn = GETDATE(), UpdatedBy = '{1}' WHERE MetalType = '{2}'",
                        priceValue,
                        safeUser,
                        safeType);
                }
                else
                {
                    string unit = GetDefaultUnit(metalType).Replace("'", "''");
                    sql = string.Format(
                        "INSERT INTO tbl_MetalPrice (MetalType, Price, PriceUnit, UpdatedOn, UpdatedBy) VALUES ('{0}', {1}, '{2}', GETDATE(), '{3}')",
                        safeType,
                        priceValue,
                        unit,
                        safeUser);
                }

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

    public static bool SaveAllPrices(decimal goldPrice, decimal silverPrice, decimal diamondPrice, string updatedBy)
    {
        bool goldSaved = SaveMetalPrice(MetalGold, goldPrice, updatedBy);
        bool silverSaved = SaveMetalPrice(MetalSilver, silverPrice, updatedBy);
        bool diamondSaved = SaveMetalPrice(MetalDiamond, diamondPrice, updatedBy);
        return goldSaved && silverSaved && diamondSaved;
    }

    static string GetDefaultUnit(string metalType)
    {
        if (string.Equals(metalType, MetalDiamond, StringComparison.OrdinalIgnoreCase))
        {
            return "Per Carat";
        }

        return "Per Gram";
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
                DataTable result = objData.RunDataTable(sql);
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
}

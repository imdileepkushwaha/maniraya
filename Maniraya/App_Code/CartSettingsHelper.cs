using DataTier;
using System;
using System.Data;

public static class CartSettingsHelper
{
    public const string KeyFreeShippingMin = "FreeShippingMinAmount";
    public const string KeyShippingCharge = "ShippingCharge";
    public const string KeyHeroFreeShipping = "HeroFreeShippingText";
    public const string KeyProgressAlmostTitle = "ProgressAlmostTitle";
    public const string KeyProgressUnlockedTitle = "ProgressUnlockedTitle";
    public const string KeyProgressAlmostMessage = "ProgressAlmostMessage";

    static readonly Tuple<string, string>[] DefaultSettings = new[]
    {
        Tuple.Create(KeyFreeShippingMin, "1000"),
        Tuple.Create(KeyShippingCharge, "40"),
        Tuple.Create(KeyHeroFreeShipping, "Free shipping above ₹1000"),
        Tuple.Create(KeyProgressAlmostTitle, "Almost there!"),
        Tuple.Create(KeyProgressUnlockedTitle, "Free shipping unlocked!"),
        Tuple.Create(KeyProgressAlmostMessage, "Add ₹{amount} more for free shipping.")
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
                IF OBJECT_ID('tbl_CartSettings', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_CartSettings (
                        SettingKey NVARCHAR(100) NOT NULL PRIMARY KEY,
                        SettingValue NVARCHAR(500) NOT NULL,
                        UpdatedOn DATETIME NOT NULL DEFAULT GETDATE()
                    )
                END";
                objData.RunInsUpDelQuery(createSql);

                foreach (Tuple<string, string> setting in DefaultSettings)
                {
                    string key = (setting.Item1 ?? string.Empty).Replace("'", "''");
                    string value = (setting.Item2 ?? string.Empty).Replace("'", "''");
                    string insertSql = string.Format(@"
                    IF NOT EXISTS (SELECT 1 FROM tbl_CartSettings WHERE SettingKey = '{0}')
                    BEGIN
                        INSERT INTO tbl_CartSettings (SettingKey, SettingValue) VALUES ('{0}', '{1}')
                    END", key, value);
                    objData.RunInsUpDelQuery(insertSql);
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

    public static decimal GetFreeShippingMinAmount()
    {
        return GetDecimal(KeyFreeShippingMin, 1000m);
    }

    public static decimal GetShippingCharge()
    {
        return GetDecimal(KeyShippingCharge, 40m);
    }

    public static string GetHeroFreeShippingText()
    {
        return GetString(KeyHeroFreeShipping, "Free shipping above ₹1000");
    }

    public static ShippingProgressModel BuildShippingProgress(decimal cartTotal)
    {
        EnsureTableAndSeedDefaults();

        decimal minAmount = GetFreeShippingMinAmount();
        if (minAmount <= 0)
        {
            minAmount = 1000m;
        }

        decimal remaining = Math.Max(0m, minAmount - cartTotal);
        decimal percent = Math.Min(100m, Math.Round((cartTotal / minAmount) * 100m, 0));

        ShippingProgressModel model = new ShippingProgressModel();
        model.MinAmount = minAmount;
        model.CartTotal = cartTotal;
        model.RemainingAmount = remaining;
        model.ProgressPercent = Convert.ToInt32(percent);
        model.IsUnlocked = remaining <= 0m;

        if (model.IsUnlocked)
        {
            model.Title = GetString(KeyProgressUnlockedTitle, "Free shipping unlocked!");
            model.Message = "Your order qualifies for free shipping.";
        }
        else
        {
            model.Title = GetString(KeyProgressAlmostTitle, "Almost there!");
            string template = GetString(KeyProgressAlmostMessage, "Add ₹{amount} more for free shipping.");
            model.Message = template.Replace("{amount}", remaining.ToString("0"));
        }

        return model;
    }

    static decimal GetDecimal(string key, decimal fallback)
    {
        string value = GetString(key, null);
        decimal parsed;
        if (decimal.TryParse(value, out parsed))
        {
            return parsed;
        }

        return fallback;
    }

    static string GetString(string key, string fallback)
    {
        EnsureTableAndSeedDefaults();

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string safeKey = (key ?? string.Empty).Replace("'", "''");
                DataTable dt = objData.RunDataTable(
                    "SELECT SettingValue FROM tbl_CartSettings WHERE SettingKey='" + safeKey + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["SettingValue"]);
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

        return fallback;
    }
}

public class ShippingProgressModel
{
    public decimal MinAmount { get; set; }
    public decimal CartTotal { get; set; }
    public decimal RemainingAmount { get; set; }
    public int ProgressPercent { get; set; }
    public bool IsUnlocked { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}

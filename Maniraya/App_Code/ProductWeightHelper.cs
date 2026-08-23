using DataTier;
using System;
using System.Data;
using System.Globalization;

/// <summary>
/// ProductMaster.Weight is stored in grams.
/// Shipping: Rs 50 per chargeable kg. Any fraction of a kg counts as 1 kg.
/// Amount &lt;= 2500: all kg charged. Amount &gt; 2500: first 3 kg free.
/// </summary>
public static class ProductWeightHelper
{
    public const decimal RatePerKg = 50m;
    public const decimal FreeKgWhenAboveThreshold = 3m;
    public const decimal FreeKgThresholdAmount = 2500m;

    public class ShippingQuote
    {
        public decimal OrderAmount { get; set; }
        public decimal TotalGrams { get; set; }
        public int ChargeableKg { get; set; }
        public int BillableKg { get; set; }
        public decimal ShippingAmount { get; set; }
        public bool HasFreeThreeKg { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public static void EnsureWeightColumn()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery(@"
IF COL_LENGTH('ProductMaster', 'Weight') IS NULL
BEGIN
    ALTER TABLE ProductMaster ADD Weight DECIMAL(18, 2) NULL;
END");
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

    public static decimal ParseGrams(string text)
    {
        decimal weight;
        string trimmed = (text ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(trimmed))
        {
            return 0;
        }

        if ((decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out weight)
            || decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out weight))
            && weight >= 0)
        {
            return weight;
        }

        return 0;
    }

    public static string ToSql(decimal weight)
    {
        return weight.ToString(CultureInfo.InvariantCulture);
    }

    public static void SaveByLatestProduct(string productName, string categoryId, string subCategoryId, decimal weight)
    {
        EnsureWeightColumn();
        string sql = @"
UPDATE pm
SET pm.Weight = " + ToSql(weight) + @"
FROM ProductMaster pm
INNER JOIN (
    SELECT TOP 1 ProductId
    FROM ProductMaster
    WHERE ProductName = '" + Escape(productName) + @"'
      AND CategoryId = '" + Escape(categoryId) + @"'
      AND SubCategoryId = '" + Escape(subCategoryId) + @"'
    ORDER BY ProductId DESC
) latest ON latest.ProductId = pm.ProductId";
        Run(sql);
    }

    public static void SaveByProductId(string productId, decimal weight)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return;
        }

        EnsureWeightColumn();
        string sql = "UPDATE ProductMaster SET Weight = " + ToSql(weight)
            + " WHERE ProductId = '" + Escape(productId) + "'";
        Run(sql);
    }

    static void Run(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
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

    public static int ToChargeableKg(decimal totalGrams)
    {
        if (totalGrams <= 0)
        {
            return 0;
        }

        return (int)Math.Ceiling(totalGrams / 1000m);
    }

    public static ShippingQuote Quote(decimal orderAmount, decimal totalGrams)
    {
        EnsureWeightColumn();

        int chargeableKg = ToChargeableKg(totalGrams);
        bool hasFreeThreeKg = orderAmount > FreeKgThresholdAmount;
        int billableKg = hasFreeThreeKg
            ? Math.Max(0, chargeableKg - Convert.ToInt32(FreeKgWhenAboveThreshold))
            : chargeableKg;

        ShippingQuote quote = new ShippingQuote
        {
            OrderAmount = orderAmount,
            TotalGrams = totalGrams,
            ChargeableKg = chargeableKg,
            BillableKg = billableKg,
            ShippingAmount = billableKg * RatePerKg,
            HasFreeThreeKg = hasFreeThreeKg
        };

        if (hasFreeThreeKg)
        {
            quote.Title = "First 3 kg free";
            quote.Message = quote.BillableKg > 0
                ? "Extra " + quote.BillableKg + " kg × ₹" + RatePerKg.ToString("0") + " shipping."
                : "No shipping on the first 3 kg. Extra kg ₹" + RatePerKg.ToString("0") + " each.";
        }
        else
        {
            quote.Title = "₹" + RatePerKg.ToString("0") + " per kg shipping";
            quote.Message = "Shop above ₹" + FreeKgThresholdAmount.ToString("0") + " to get first 3 kg free.";
        }

        return quote;
    }

    public static ShippingQuote QuoteFromCart(DataTable cart, decimal orderAmount)
    {
        return Quote(orderAmount, SumCartWeightGrams(cart));
    }

    public static decimal SumCartWeightGrams(DataTable cart)
    {
        EnsureWeightColumn();
        if (cart == null || cart.Rows.Count == 0)
        {
            return 0;
        }

        decimal totalGrams = 0;
        foreach (DataRow row in cart.Rows)
        {
            int qty = cart.Columns.Contains("Quantity") ? GetInt(row, "Quantity") : 1;
            if (qty <= 0)
            {
                continue;
            }

            decimal grams = 0;
            if (cart.Columns.Contains("Weight") && row["Weight"] != DBNull.Value
                && Convert.ToString(row["Weight"]).Trim() != string.Empty)
            {
                grams = ParseGrams(Convert.ToString(row["Weight"]));
            }
            else
            {
                grams = GetProductWeightGrams(GetProductId(row));
            }

            totalGrams += grams * qty;
        }

        return totalGrams;
    }

    public static decimal GetProductWeightGrams(string productId)
    {
        string pid = (productId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(pid))
        {
            return 0;
        }

        EnsureWeightColumn();
        DataTable dt = RunSelect(
            "SELECT ISNULL(Weight, 0) AS Weight FROM ProductMaster WITH (NOLOCK) WHERE ProductId = '" + Escape(pid) + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            return ParseGrams(Convert.ToString(dt.Rows[0]["Weight"]));
        }

        return 0;
    }

    public static void SaveOnLatestUserPurchase(string userId, decimal shipping)
    {
        string uid = (userId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(uid))
        {
            return;
        }

        string sql = @"
IF COL_LENGTH('UserFranchiseePurchaseMaster', 'shippingcharges') IS NOT NULL
BEGIN
    UPDATE m
    SET m.shippingcharges = " + ToSql(shipping) + @"
    FROM UserFranchiseePurchaseMaster m
    INNER JOIN (
        SELECT TOP 1 PurchaseId
        FROM UserFranchiseePurchaseMaster
        WHERE UserId = '" + Escape(uid) + @"'
        ORDER BY PurchaseId DESC
    ) latest ON latest.PurchaseId = m.PurchaseId
END";
        Run(sql);
    }

    static string GetProductId(DataRow row)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        string[] names = { "ProductId", "Productid", "productid", "ProductID" };
        foreach (string name in names)
        {
            if (row.Table.Columns.Contains(name) && row[name] != DBNull.Value)
            {
                return Convert.ToString(row[name]).Trim();
            }
        }

        return string.Empty;
    }

    static int GetInt(DataRow row, string columnName)
    {
        if (row == null || row.Table == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
        {
            return 0;
        }

        int value;
        int.TryParse(Convert.ToString(row[columnName]), out value);
        return value;
    }

    static DataTable RunSelect(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                return objData.RunDataTable(sql) ?? new DataTable();
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return new DataTable();
        }
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

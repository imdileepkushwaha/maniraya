using DataTier;
using System;
using System.Globalization;

/// <summary>
/// ProductMaster.Weight is stored in grams.
/// </summary>
public static class ProductWeightHelper
{
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

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

using BusinessLogicTier;
using System;
using System.Data;

public static class CatalogHelper
{
    public const int CatalogProductPageSize = 500;

    public static DataTable BuildDisplayCategories()
    {
        clsProduct objState = new clsProduct();
        DataTable dt = objState.getCategory();
        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("CategoryId", typeof(int));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("Image", typeof(string));
            return dt;
        }

        if (!dt.Columns.Contains("CategoryId") && dt.Columns.Contains("CategoryID"))
        {
            dt.Columns["CategoryID"].ColumnName = "CategoryId";
        }

        if (!dt.Columns.Contains("Image"))
        {
            dt.Columns.Add("Image", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            row["Image"] = ResolveCategoryImageUrl(Convert.ToString(row["Image"]));
        }

        return dt;
    }

    static string ResolveCategoryImageUrl(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return string.Empty;
        }

        string value = imagePath.Trim().Replace("\\", "/");

        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (value.StartsWith("//", StringComparison.Ordinal))
        {
            return "https:" + value;
        }

        if (value.StartsWith("../img/", StringComparison.OrdinalIgnoreCase))
        {
            value = "img/" + value.Substring("../img/".Length);
        }
        else if (!value.StartsWith("img/", StringComparison.OrdinalIgnoreCase) && !value.Contains("/"))
        {
            value = "img/" + value;
        }
        else
        {
            value = value.TrimStart('/');
        }

        return value;
    }

    public static string ResolveProductImageUrl(string imagePath, string productName = null)
    {
        string resolved = NormalizeProductImagePath(imagePath);
        if (!string.IsNullOrWhiteSpace(resolved))
        {
            return resolved;
        }

        return DefaultProductImage;
    }

    static string NormalizeProductImagePath(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return string.Empty;
        }

        string value = imagePath.Trim().Replace("\\", "/");

        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (value.StartsWith("//", StringComparison.Ordinal))
        {
            return "https:" + value;
        }

        if (value.StartsWith("../ProductImage/", StringComparison.OrdinalIgnoreCase))
        {
            value = value.Substring(3);
        }
        else if (value.StartsWith("../img/", StringComparison.OrdinalIgnoreCase))
        {
            value = "ProductImage/" + value.Substring("../img/".Length);
        }
        else if (value.StartsWith("img/", StringComparison.OrdinalIgnoreCase))
        {
            value = "ProductImage/" + value.Substring("img/".Length);
        }
        else if (!value.StartsWith("ProductImage/", StringComparison.OrdinalIgnoreCase) && !value.Contains("/"))
        {
            value = "ProductImage/" + value;
        }

        if (value.StartsWith("ProductImage/", StringComparison.OrdinalIgnoreCase))
        {
            string fileName = value.Substring("ProductImage/".Length);
            if (string.IsNullOrWhiteSpace(fileName) ||
                fileName.Equals("images.png", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return "ProductImage/" + fileName.TrimStart('/');
        }

        if (value.IndexOf("images.png", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return string.Empty;
        }

        if (value.Contains("/"))
        {
            return value.TrimStart('/');
        }

        return "ProductImage/" + value;
    }

    public const string DefaultProductImage = "img/images.png";

    public static DataTable EnrichProductRows(DataTable dt)
    {
        if (dt == null)
        {
            return null;
        }

        if (dt.Columns.Contains("Image"))
        {
            bool hasProductName = dt.Columns.Contains("ProductName");
            foreach (DataRow row in dt.Rows)
            {
                string productName = hasProductName ? Convert.ToString(row["ProductName"]) : null;
                row["Image"] = ResolveProductImageUrl(Convert.ToString(row["Image"]), productName);
            }
        }

        if (!dt.Columns.Contains("discount") && dt.Columns.Contains("Amount") && dt.Columns.Contains("MRP"))
        {
            dt.Columns.Add("discount", typeof(int));
            foreach (DataRow row in dt.Rows)
            {
                decimal amount = Convert.ToDecimal(row["Amount"]);
                decimal mrp = Convert.ToDecimal(row["MRP"]);
                row["discount"] = mrp > amount ? (int)Math.Round((mrp - amount) / mrp * 100m) : 0;
            }
        }

        if (!dt.Columns.Contains("franchiseeid"))
        {
            dt.Columns.Add("franchiseeid", typeof(int));
            foreach (DataRow row in dt.Rows)
            {
                row["franchiseeid"] = 0;
            }
        }

        return dt;
    }

    public static DataTable LoadProducts(int pageIndex, int pageSize, string categoryId)
    {
        clsProduct objState = new clsProduct();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;

        DataTable dt = objState.ProductPageWiseoutside(pageIndex, pageSize, categoryId);
        if (dt == null)
        {
            return new DataTable();
        }

        return EnrichProductRows(dt);
    }
}

using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;

public static class CatalogHelper
{
    public const int HomeCategoryLimit = 8;
    public const int HomeProductLimit = 8;
    public const int CatalogProductPageSize = 500;

    public static readonly Dictionary<string, string> DisplayCategories = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Electronics", "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=600&q=80" },
        { "Fashion", "https://images.unsplash.com/photo-1445205170230-053b83016050?auto=format&fit=crop&w=600&q=80" },
        { "Beauty", "https://images.unsplash.com/photo-1522335789203-aabd1fc54bc9?auto=format&fit=crop&w=600&q=80" },
        { "Home & Living", "https://images.unsplash.com/photo-1484101403633-562f891dc89a?auto=format&fit=crop&w=600&q=80" },
        { "Accessories", "https://images.unsplash.com/photo-1523178663199-48c24d1f9b4a?auto=format&fit=crop&w=600&q=80" },
        { "Sports & Fitness", "https://images.unsplash.com/photo-1571019614242-c5c5dee9f50e?auto=format&fit=crop&w=600&q=80" },
        { "Groceries", "https://images.unsplash.com/photo-1542838132-92c53300491e?auto=format&fit=crop&w=600&q=80" },
        { "Health & Wellness", "https://images.unsplash.com/photo-1505751172879-be763ef4ecc7?auto=format&fit=crop&w=600&q=80" }
    };

    static readonly object[][] StaticProducts = new object[][]
    {
        new object[] { 1001, "Smart Watch Series X3", "Electronics", 16999m, 21999m, "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1002, "Slim 3 Intel Core i5 Laptop", "Electronics", 58999m, 64999m, "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1003, "Wireless Earbuds Pro", "Electronics", 2499m, 3999m, "https://images.unsplash.com/photo-1590658268037-6bf12165a1df?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1004, "Classic Sneakers", "Fashion", 3299m, 4499m, "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1005, "Cotton Hoodie", "Fashion", 1899m, 2499m, "https://images.unsplash.com/photo-1578587018452-892bacefd3f2?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1006, "Leather Handbag", "Accessories", 4599m, 5999m, "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1007, "Skin Care Kit", "Beauty", 1299m, 1899m, "https://images.unsplash.com/photo-1556228720-195a672e8a03?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1008, "Hair Dryer Pro", "Beauty", 2199m, 2999m, "https://images.unsplash.com/photo-1522338140262-f46f5913618a?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1009, "Portable Laptop Table", "Home & Living", 3499m, 4299m, "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1010, "Kitchen Essentials Set", "Home & Living", 2799m, 3499m, "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1011, "Yoga Mat Premium", "Sports & Fitness", 999m, 1499m, "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1012, "Organic Green Tea Pack", "Groceries", 449m, 599m, "https://images.unsplash.com/photo-1556678733-db22be894031?auto=format&fit=crop&w=600&q=80" }
    };

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
            string name = Convert.ToString(row["CategoryName"]);
            string image = Convert.ToString(row["Image"]);
            if (DisplayCategories.ContainsKey(name) &&
                (string.IsNullOrWhiteSpace(image) || image.IndexOf("images.png", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                row["Image"] = DisplayCategories[name];
            }
        }

        foreach (KeyValuePair<string, string> category in DisplayCategories)
        {
            bool exists = false;
            foreach (DataRow row in dt.Rows)
            {
                if (string.Equals(Convert.ToString(row["CategoryName"]), category.Key, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                DataRow row = dt.NewRow();
                row["CategoryId"] = 0;
                row["CategoryName"] = category.Key;
                row["Image"] = category.Value;
                dt.Rows.Add(row);
            }
        }

        return dt;
    }

    public static DataTable TakeRows(DataTable source, int count)
    {
        if (source == null)
        {
            return null;
        }

        if (source.Rows.Count <= count)
        {
            return source;
        }

        DataTable limited = source.Clone();
        for (int i = 0; i < count && i < source.Rows.Count; i++)
        {
            limited.ImportRow(source.Rows[i]);
        }

        return limited;
    }

    public static DataTable GetStaticProductsTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProductID", typeof(int));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("categoryName", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("MRP", typeof(decimal));
        dt.Columns.Add("discount", typeof(int));
        dt.Columns.Add("Image", typeof(string));
        dt.Columns.Add("franchiseeid", typeof(int));

        foreach (object[] item in StaticProducts)
        {
            decimal amount = Convert.ToDecimal(item[3]);
            decimal mrp = Convert.ToDecimal(item[4]);
            int discount = mrp > amount ? (int)Math.Round((mrp - amount) / mrp * 100m) : 0;
            DataRow row = dt.NewRow();
            row["ProductID"] = item[0];
            row["ProductName"] = item[1];
            row["categoryName"] = item[2];
            row["Amount"] = amount;
            row["MRP"] = mrp;
            row["discount"] = discount;
            row["Image"] = item[5];
            row["franchiseeid"] = 0;
            dt.Rows.Add(row);
        }

        return dt;
    }

    public static DataTable EnrichProductImages(DataTable dt)
    {
        if (dt == null || !dt.Columns.Contains("Image") || !dt.Columns.Contains("ProductName"))
        {
            return dt;
        }

        foreach (DataRow row in dt.Rows)
        {
            string image = Convert.ToString(row["Image"]);
            if (!string.IsNullOrWhiteSpace(image) && image.IndexOf("images.png", StringComparison.OrdinalIgnoreCase) < 0)
            {
                continue;
            }

            string name = Convert.ToString(row["ProductName"]);
            foreach (object[] item in StaticProducts)
            {
                if (string.Equals(Convert.ToString(item[1]), name, StringComparison.OrdinalIgnoreCase))
                {
                    row["Image"] = item[5];
                    break;
                }
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
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = GetStaticProductsTable();
        }
        else
        {
            dt = EnrichProductImages(dt);
        }

        return dt;
    }
}

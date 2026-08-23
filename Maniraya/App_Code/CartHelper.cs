using DataTier;
using System;
using System.Data;

public static class CartHelper
{
    public static DataTable GetCartItemsWithImages(string userId)
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return dt;
        }

        string safeUserId = userId.Replace("'", "''");
        ProductWeightHelper.EnsureWeightColumn();
        string sql = @"
            SELECT
                s.SubProductID,
                ct.Id,
                ct.Franchiseeid,
                ct.Userid,
                ct.Productid,
                ct.subproductid,
                ct.Amount,
                ct.MRP,
                ct.PurchaseAmount,
                ct.CGST,
                ct.SGST,
                ct.IGST,
                ct.IGSTper,
                ct.CGSTper,
                ct.SGSTper,
                ct.TotalAmount,
                ct.Quantity,
                cl.ColorName,
                sz.SizeName,
                p.ProductName,
                p.DP,
                p.GST,
                p.BV,
                ISNULL(p.Weight, 0) AS Weight,
                p.BV * ct.Quantity AS TOTALBV,
                p.DP * ct.Quantity AS TOTALDP,
                ISNULL(p.productimage, '') AS ProductImage,
                5 AS stock
            FROM CartItems ct
            INNER JOIN productmaster p ON ct.Productid = p.ProductId
            INNER JOIN subproductmaster s ON ct.SubProductid = s.SubProductID
            INNER JOIN colormaster cl ON s.ColorId = cl.ID
            INNER JOIN SizeMaster sz ON s.SizeID = sz.ID
            WHERE ct.UserId = '" + safeUserId + "'";

        Data objData = new Data();
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

        EnsureImageUrlColumn(dt);
        return dt;
    }

    static void EnsureImageUrlColumn(DataTable dt)
    {
        if (dt == null)
        {
            return;
        }

        if (!dt.Columns.Contains("ImageUrl"))
        {
            dt.Columns.Add("ImageUrl", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            string productImage = dt.Columns.Contains("ProductImage")
                ? Convert.ToString(row["ProductImage"])
                : string.Empty;
            string productName = dt.Columns.Contains("ProductName")
                ? Convert.ToString(row["ProductName"])
                : string.Empty;

            row["ImageUrl"] = CatalogHelper.ResolveProductImageUrl(productImage, productName);
        }
    }
}

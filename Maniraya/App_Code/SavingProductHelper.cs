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

    public static string GetImageUrl(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            return VirtualPathUtility.ToAbsolute(ImageFolder + "noimage.png");
        }

        return VirtualPathUtility.ToAbsolute(ImageFolder + imageName.Trim());
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

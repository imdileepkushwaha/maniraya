using DataTier;
using System;
using System.Data;
using System.Web;

public static class SitePopupHelper
{
    public const string PopupImageFolder = "~/user/popup/";

    public static void EnsureTableAndSeedDefaults()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string createSql = @"
                IF OBJECT_ID('tbl_SitePopup', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_SitePopup (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(200) NULL,
                        PopupContent NVARCHAR(MAX) NULL,
                        PopupImage NVARCHAR(500) NULL,
                        Status BIT NOT NULL DEFAULT 1,
                        CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
                        UpdatedOn DATETIME NULL
                    )
                END";
                objData.RunInsUpDelQuery(createSql);

                string addImageColumnSql = @"
                IF COL_LENGTH('tbl_SitePopup', 'PopupImage') IS NULL
                BEGIN
                    ALTER TABLE tbl_SitePopup ADD PopupImage NVARCHAR(500) NULL
                END";
                objData.RunInsUpDelQuery(addImageColumnSql);

                string allowNullContentSql = @"
                IF EXISTS (
                    SELECT 1
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = 'tbl_SitePopup'
                      AND COLUMN_NAME = 'PopupContent'
                      AND IS_NULLABLE = 'NO'
                )
                BEGIN
                    ALTER TABLE tbl_SitePopup ALTER COLUMN PopupContent NVARCHAR(MAX) NULL
                END";
                objData.RunInsUpDelQuery(allowNullContentSql);
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

    public static DataTable GetAllPopups()
    {
        EnsureTableAndSeedDefaults();
        return RunSelect("SELECT * FROM tbl_SitePopup ORDER BY Id DESC");
    }

    public static DataTable GetActivePopups()
    {
        EnsureTableAndSeedDefaults();
        return RunSelect("SELECT * FROM tbl_SitePopup WHERE Status = 1 ORDER BY Id ASC");
    }

    public static DataTable GetActivePopup()
    {
        return GetActivePopups();
    }

    public static DataTable GetPopupById(int id)
    {
        if (id <= 0)
        {
            return new DataTable();
        }

        return RunSelect("SELECT * FROM tbl_SitePopup WHERE Id=" + id);
    }

    public static bool SavePopup(int? id, string title, string popupContent, string popupImage, bool status)
    {
        bool hasContent = !string.IsNullOrWhiteSpace(popupContent);
        bool hasImage = !string.IsNullOrWhiteSpace(popupImage);

        if (!hasContent && !hasImage)
        {
            return false;
        }

        EnsureTableAndSeedDefaults();

        string safeTitle = (title ?? string.Empty).Trim().Replace("'", "''");
        string safeContent = hasContent ? popupContent.Trim().Replace("'", "''") : string.Empty;
        string safeImage = hasImage ? popupImage.Trim().Replace("'", "''") : string.Empty;
        string statusBit = status ? "1" : "0";

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string sql;
                if (id.HasValue && id.Value > 0)
                {
                    sql = string.Format(
                        "UPDATE tbl_SitePopup SET Title='{0}', PopupContent='{1}', PopupImage='{2}', Status={3}, UpdatedOn=GETDATE() WHERE Id={4}",
                        safeTitle,
                        safeContent,
                        safeImage,
                        statusBit,
                        id.Value);
                }
                else
                {
                    sql = string.Format(
                        "INSERT INTO tbl_SitePopup (Title, PopupContent, PopupImage, Status, CreatedOn) VALUES ('{0}', '{1}', '{2}', {3}, GETDATE())",
                        safeTitle,
                        safeContent,
                        safeImage,
                        statusBit);
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

    public static bool DeletePopup(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery("DELETE FROM tbl_SitePopup WHERE Id=" + id);
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

    public static bool SetPopupStatus(int id, bool status)
    {
        if (id <= 0)
        {
            return false;
        }

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string statusBit = status ? "1" : "0";
                objData.RunInsUpDelQuery(
                    "UPDATE tbl_SitePopup SET Status=" + statusBit + ", UpdatedOn=GETDATE() WHERE Id=" + id);
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

    public static string FormatPopupHtml(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return string.Empty;
        }

        return HttpUtility.HtmlEncode(content).Replace("\r\n", "<br />").Replace("\n", "<br />");
    }

    public static bool HasColumn(DataRow row, string columnName)
    {
        return row != null && row.Table != null && row.Table.Columns.Contains(columnName);
    }

    public static string GetPopupImagePath(DataRow row)
    {
        if (!HasColumn(row, "PopupImage"))
        {
            return string.Empty;
        }

        return Convert.ToString(row["PopupImage"]) ?? string.Empty;
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

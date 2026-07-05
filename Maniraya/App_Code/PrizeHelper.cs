using DataTier;
using System;
using System.Data;
using System.Globalization;

/// <summary>
/// Data access helper for the Prize module (Prize Master + Prize Assignment).
/// Tables are created on demand so no manual migration is required.
/// </summary>
public static class PrizeHelper
{
    public static void EnsureTables()
    {
        RunNonQuery(@"
            IF OBJECT_ID('PrizeMaster', 'U') IS NULL
            BEGIN
                CREATE TABLE PrizeMaster (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    PrizeName NVARCHAR(200) NOT NULL,
                    Description NVARCHAR(500) NULL,
                    Status BIT NOT NULL DEFAULT 1,
                    CreatedBy NVARCHAR(100) NULL,
                    CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
                )
            END");

        RunNonQuery(@"
            IF OBJECT_ID('PrizeAssignment', 'U') IS NULL
            BEGIN
                CREATE TABLE PrizeAssignment (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    PrizeId INT NOT NULL,
                    PrizeName NVARCHAR(200) NOT NULL,
                    UserId NVARCHAR(100) NOT NULL,
                    UserName NVARCHAR(200) NULL,
                    Mobile NVARCHAR(50) NULL,
                    PrizeMonth NVARCHAR(20) NULL,
                    Status BIT NOT NULL DEFAULT 1,
                    CreatedBy NVARCHAR(100) NULL,
                    CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
                )
            END");
    }

    // ---------- Prize Master ----------

    public static DataTable GetAllPrizes()
    {
        EnsureTables();
        return RunSelect(@"
            SELECT
                Id,
                PrizeName,
                ISNULL(Description, '') AS Description,
                ISNULL(Status, 1) AS Status,
                CASE WHEN ISNULL(Status, 1) = 1 THEN 'Active' ELSE 'Inactive' END AS StatusText,
                CreatedOn
            FROM PrizeMaster
            ORDER BY Id DESC");
    }

    public static DataTable GetActivePrizes()
    {
        EnsureTables();
        return RunSelect(@"
            SELECT Id, PrizeName
            FROM PrizeMaster
            WHERE ISNULL(Status, 1) = 1
            ORDER BY PrizeName");
    }

    public static DataTable GetPrizeById(int id)
    {
        if (id <= 0)
        {
            return new DataTable();
        }

        EnsureTables();
        return RunSelect("SELECT * FROM PrizeMaster WHERE Id = " + id);
    }

    public static string AddPrize(string prizeName, string description, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(prizeName))
        {
            return "empty";
        }

        EnsureTables();

        string safeName = Escape(prizeName.Trim());
        DataTable existing = RunSelect(
            "SELECT TOP 1 Id FROM PrizeMaster WHERE LOWER(LTRIM(RTRIM(PrizeName))) = LOWER('" + safeName + "')");
        if (existing != null && existing.Rows.Count > 0)
        {
            return "exists";
        }

        string sql = string.Format(
            "INSERT INTO PrizeMaster (PrizeName, Description, Status, CreatedBy, CreatedOn) VALUES ('{0}', '{1}', 1, '{2}', GETDATE())",
            safeName,
            Escape((description ?? string.Empty).Trim()),
            Escape((createdBy ?? string.Empty).Trim()));

        return RunNonQuery(sql) ? "ok" : "error";
    }

    public static bool UpdatePrize(int id, string prizeName, string description, bool status)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(prizeName))
        {
            return false;
        }

        EnsureTables();

        string sql = string.Format(
            "UPDATE PrizeMaster SET PrizeName='{0}', Description='{1}', Status={2} WHERE Id={3}",
            Escape(prizeName.Trim()),
            Escape((description ?? string.Empty).Trim()),
            status ? "1" : "0",
            id);

        return RunNonQuery(sql);
    }

    public static bool SetPrizeStatus(int id, bool active)
    {
        if (id <= 0)
        {
            return false;
        }

        EnsureTables();
        return RunNonQuery("UPDATE PrizeMaster SET Status = " + (active ? "1" : "0") + " WHERE Id = " + id);
    }

    public static bool DeletePrize(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        EnsureTables();
        return RunNonQuery("DELETE FROM PrizeMaster WHERE Id = " + id);
    }

    // ---------- User lookup ----------

    public static DataTable GetUserByUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new DataTable();
        }

        return RunSelect(@"
            SELECT TOP 1
                ud.userid,
                ISNULL(ud.username, '') AS username,
                ISNULL(ud.mobile, '') AS mobile,
                ISNULL(STUFF((
                    SELECT DISTINCT ', ' + LTRIM(RTRIM(sd.couponcode))
                    FROM SavingAccountDetail sd WITH (NOLOCK)
                    WHERE sd.userid = ud.userid
                      AND ISNULL(LTRIM(RTRIM(sd.couponcode)), '') <> ''
                    FOR XML PATH('')), 1, 2, ''), '') AS couponcodes
            FROM UserDetail ud WITH (NOLOCK)
            WHERE ud.userid = '" + Escape(userId.Trim()) + "'");
    }

    // ---------- Prize Assignment ----------

    public static string AssignPrize(int prizeId, string userId, string prizeMonth, string createdBy)
    {
        if (prizeId <= 0)
        {
            return "noprize";
        }

        if (string.IsNullOrWhiteSpace(userId))
        {
            return "nouser";
        }

        EnsureTables();

        DataTable user = GetUserByUserId(userId);
        if (user == null || user.Rows.Count == 0)
        {
            return "invaliduser";
        }

        DataTable prize = GetPrizeById(prizeId);
        if (prize == null || prize.Rows.Count == 0)
        {
            return "noprize";
        }

        string prizeName = Convert.ToString(prize.Rows[0]["PrizeName"]);
        string userName = Convert.ToString(user.Rows[0]["username"]);
        string mobile = Convert.ToString(user.Rows[0]["mobile"]);
        string month = (prizeMonth ?? string.Empty).Trim();

        DataTable existing = RunSelect(string.Format(
            "SELECT TOP 1 Id FROM PrizeAssignment WHERE PrizeId={0} AND UserId='{1}' AND ISNULL(PrizeMonth,'')='{2}' AND ISNULL(Status,1)=1",
            prizeId,
            Escape(userId.Trim()),
            Escape(month)));
        if (existing != null && existing.Rows.Count > 0)
        {
            return "duplicate";
        }

        string sql = string.Format(
            "INSERT INTO PrizeAssignment (PrizeId, PrizeName, UserId, UserName, Mobile, PrizeMonth, Status, CreatedBy, CreatedOn) " +
            "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', 1, '{6}', GETDATE())",
            prizeId,
            Escape(prizeName),
            Escape(userId.Trim()),
            Escape(userName),
            Escape(mobile),
            Escape(month),
            Escape((createdBy ?? string.Empty).Trim()));

        return RunNonQuery(sql) ? "ok" : "error";
    }

    public static DataTable GetAllAssignments(string userIdFilter, string monthFilter)
    {
        EnsureTables();

        string sql = @"
            SELECT
                Id,
                PrizeId,
                PrizeName,
                UserId,
                ISNULL(UserName, '') AS UserName,
                ISNULL(Mobile, '') AS Mobile,
                ISNULL(PrizeMonth, '') AS PrizeMonth,
                ISNULL(Status, 1) AS Status,
                CASE WHEN ISNULL(Status, 1) = 1 THEN 'Active' ELSE 'Inactive' END AS StatusText,
                CreatedOn
            FROM PrizeAssignment
            WHERE ISNULL(Status, 1) = 1";

        if (!string.IsNullOrWhiteSpace(userIdFilter))
        {
            sql += " AND (UserId LIKE '%" + Escape(userIdFilter.Trim()) + "%' OR UserName LIKE '%" + Escape(userIdFilter.Trim()) + "%')";
        }

        if (!string.IsNullOrWhiteSpace(monthFilter))
        {
            sql += " AND PrizeMonth = '" + Escape(monthFilter.Trim()) + "'";
        }

        sql += " ORDER BY Id DESC";
        return RunSelect(sql);
    }

    public static bool DeleteAssignment(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        EnsureTables();
        return RunNonQuery("UPDATE PrizeAssignment SET Status = 0 WHERE Id = " + id);
    }

    public static DataTable GetAssignmentsForUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new DataTable();
        }

        EnsureTables();
        return RunSelect(@"
            SELECT
                Id,
                PrizeName,
                ISNULL(PrizeMonth, '') AS PrizeMonth,
                CreatedOn
            FROM PrizeAssignment
            WHERE LTRIM(RTRIM(UserId)) = '" + Escape(userId.Trim()) + @"'
              AND ISNULL(Status, 1) = 1
            ORDER BY Id DESC");
    }

    public static DataTable GetAllWinners(int topCount)
    {
        EnsureTables();

        int top = topCount > 0 ? topCount : 200;
        return RunSelect(@"
            SELECT TOP " + top + @"
                Id,
                UserId,
                ISNULL(UserName, '') AS UserName,
                PrizeName,
                ISNULL(PrizeMonth, '') AS PrizeMonth,
                CreatedOn
            FROM PrizeAssignment
            WHERE ISNULL(Status, 1) = 1
            ORDER BY Id DESC");
    }

    public static string FormatPrizeMonth(object value)
    {
        string raw = Convert.ToString(value);
        if (string.IsNullOrWhiteSpace(raw))
        {
            return string.Empty;
        }

        raw = raw.Trim();
        DateTime parsed;
        string[] formats = { "yyyy-MM", "yyyy-MM-dd", "MM/yyyy", "yyyy/MM" };
        if (DateTime.TryParseExact(raw, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed) ||
            DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
        {
            return parsed.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
        }

        return raw;
    }

    // ---------- infrastructure ----------

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
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

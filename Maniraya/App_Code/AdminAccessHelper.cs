using System;
using System.Data;
using System.IO;
using System.Web;
using DataTier;

/// <summary>
/// SubAdmin page-access checks against AdminMenuPermission / Menu.URL.
/// </summary>
public static class AdminAccessHelper
{
    static readonly string[] AlwaysAllowedPages = {
        "dashboard.aspx",
        "logout.aspx",
        "index.aspx",
        "inboxadmin.aspx"
    };

    /// <summary>
    /// Pages only full administrator may open (not SubAdmin, even if somehow assigned).
    /// </summary>
    static readonly string[] AdminOnlyPages = {
        "adminsettings.aspx",
        "subadmin.aspx",
        "subadminreport.aspx",
        "adminmenupermission.aspx"
    };

    public static string GetCurrentPageName()
    {
        HttpContext ctx = HttpContext.Current;
        if (ctx == null || ctx.Request == null)
        {
            return string.Empty;
        }

        string path = ctx.Request.AppRelativeCurrentExecutionFilePath
            ?? ctx.Request.FilePath
            ?? string.Empty;

        return Path.GetFileName(path.Replace("~/", "").Replace("\\", "/")) ?? string.Empty;
    }

    public static bool IsAlwaysAllowed(string pageName)
    {
        if (string.IsNullOrWhiteSpace(pageName))
        {
            return false;
        }

        string name = pageName.Trim().ToLowerInvariant();
        foreach (string allowed in AlwaysAllowedPages)
        {
            if (name == allowed) return true;
        }
        return false;
    }

    public static bool IsAdminOnlyPage(string pageName)
    {
        if (string.IsNullOrWhiteSpace(pageName))
        {
            return false;
        }

        string name = pageName.Trim().ToLowerInvariant();
        foreach (string page in AdminOnlyPages)
        {
            if (name == page) return true;
        }
        return false;
    }

    public static bool IsAdministrator(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return true;
        }

        string r = role.Trim();
        return !string.Equals(r, "Subadmin", StringComparison.OrdinalIgnoreCase);
    }

    public static bool CanAccessPage(string userId, string role, string pageName)
    {
        if (string.IsNullOrWhiteSpace(pageName))
        {
            return false;
        }

        if (IsAlwaysAllowed(pageName))
        {
            return true;
        }

        if (IsAdministrator(role))
        {
            return true;
        }

        // SubAdmin cannot open admin-user management pages
        if (IsAdminOnlyPage(pageName))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(userId))
        {
            return false;
        }

        return HasAssignedMenuUrl(userId, pageName);
    }

    public static bool HasAssignedMenuUrl(string userId, string pageName)
    {
        string safeUser = (userId ?? string.Empty).Replace("'", "''");
        string safePage = (pageName ?? string.Empty).Replace("'", "''");

        string sql = @"
SELECT TOP 1 m.id
FROM Menu m WITH (NOLOCK)
INNER JOIN AdminMenuPermission p WITH (NOLOCK)
    ON p.MenuId = m.id
   AND p.UserId = '" + safeUser + @"'
WHERE ISNULL(m.Status, 1) = 1
  AND LTRIM(RTRIM(m.URL)) <> ''
  AND LOWER(LTRIM(RTRIM(m.URL))) = LOWER(LTRIM(RTRIM('" + safePage + @"')))";

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            DataTable dt = objData.RunDataTable(sql);
            return dt != null && dt.Rows.Count > 0;
        }
        catch
        {
            return false;
        }
        finally
        {
            objData.EndConnection();
        }
    }

    public static void EnforceOrRedirect()
    {
        HttpContext ctx = HttpContext.Current;
        if (ctx == null || ctx.Session == null)
        {
            return;
        }

        if (ctx.Session["useradmin"] == null)
        {
            return;
        }

        string userId = Convert.ToString(ctx.Session["useradmin"]);
        string role = ctx.Session["role"] != null ? Convert.ToString(ctx.Session["role"]) : "Administrator";
        string pageName = GetCurrentPageName();

        if (CanAccessPage(userId, role, pageName))
        {
            return;
        }

        ctx.Response.Redirect("Dashboard.aspx?denied=1", true);
    }
}

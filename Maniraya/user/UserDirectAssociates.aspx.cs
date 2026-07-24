using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_UserDirectAssociates : Page
{
    clsUser objclsUser = new clsUser();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            FillAssociatesDetails();
            filldashboard();
        }
    }

    void filldashboard()
    {
        objclsUser.UserId = Convert.ToString(Session["userid"]);
        DataTable LeftDirectt = getUserleftDirect(objclsUser);
        DataTable RightDirectt = getUserrightDirect(objclsUser);
        LblLeftDirect.Text = (LeftDirectt != null && LeftDirectt.Rows.Count > 0)
            ? Convert.ToString(LeftDirectt.Rows[0][0])
            : "0";
        LblRightDirect.Text = (RightDirectt != null && RightDirectt.Rows.Count > 0)
            ? Convert.ToString(RightDirectt.Rows[0][0])
            : "0";
    }

    public DataTable getUserleftDirect(clsUser objUser)
    {
        string str_query = "SELECT COUNT(1) FROM UserDetail u WITH (NOLOCK) WHERE LTRIM(RTRIM(u.SponserId))='"
            + SqlEscape(objUser.UserId) + "' AND LTRIM(RTRIM(CONVERT(varchar(20), u.StandingPosition)))='1'";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }

    void FillAssociatesDetails()
    {
        string userId = Convert.ToString(Session["userid"]).Trim();
        if (string.IsNullOrWhiteSpace(userId))
        {
            grdBank.DataSource = null;
            grdBank.DataBind();
            return;
        }

        string topClause = GetTopClause();
        string position = DDlstPosition != null ? DDlstPosition.SelectedValue : "0";
        string search = txtSearch != null ? Convert.ToString(txtSearch.Text).Trim() : string.Empty;

        DataTable dt = getUserDirect(userId, position, search, topClause);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }

    string GetTopClause()
    {
        string selected = ddlRecordFilter.SelectedValue;
        if (string.Equals(selected, "All", StringComparison.OrdinalIgnoreCase))
        {
            return string.Empty;
        }

        int top;
        if (int.TryParse(selected, out top) && top > 0)
        {
            return " TOP (" + top + ") ";
        }

        return " TOP (10) ";
    }

    public DataTable getUserDirect(string userId, string position, string search, string topClause)
    {
        // Both legs (was wrongly calling Left-only SP before).
        string sql = @"
SELECT " + topClause + @"
    LTRIM(RTRIM(u.UserId)) AS UserId,
    ISNULL(u.UserName, '') AS UserName,
    ISNULL(NULLIF(LTRIM(RTRIM(pm.PlanName)), ''), '-') AS planname,
    CASE
        WHEN LTRIM(RTRIM(CONVERT(varchar(20), ISNULL(u.StandingPosition, '')))) IN ('1', 'Left', 'L') THEN 'Left'
        WHEN LTRIM(RTRIM(CONVERT(varchar(20), ISNULL(u.StandingPosition, '')))) IN ('2', 'Right', 'R') THEN 'Right'
        ELSE ISNULL(CONVERT(varchar(20), u.StandingPosition), '-')
    END AS StandingPosition,
    ISNULL(u.Mobile, '') AS Mobile,
    CASE
        WHEN u.MentionDate IS NULL THEN ''
        ELSE CONVERT(varchar(11), u.MentionDate, 106)
    END AS mentiondate,
    CASE
        WHEN ISNULL(u.Status, 0) = 1 THEN 'Paid'
        ELSE 'Unpaid'
    END AS Status
FROM UserDetail u WITH (NOLOCK)
LEFT JOIN PlanMaster pm WITH (NOLOCK) ON u.SlabID = pm.Id
WHERE LTRIM(RTRIM(u.SponserId)) = '" + SqlEscape(userId) + @"'
";

        if (position == "1")
        {
            sql += " AND LTRIM(RTRIM(CONVERT(varchar(20), ISNULL(u.StandingPosition, '')))) IN ('1', 'Left', 'L') ";
        }
        else if (position == "2")
        {
            sql += " AND LTRIM(RTRIM(CONVERT(varchar(20), ISNULL(u.StandingPosition, '')))) IN ('2', 'Right', 'R') ";
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            string safe = SqlEscape(search);
            sql += @"
 AND (
        u.UserId LIKE '%" + safe + @"%'
     OR u.UserName LIKE '%" + safe + @"%'
     OR ISNULL(u.Mobile, '') LIKE '%" + safe + @"%'
 ) ";
        }

        sql += " ORDER BY u.MentionDate DESC, u.UserId ASC";

        DataTable dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(sql) ?? new DataTable();
        }
        catch
        {
            dt = new DataTable();
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt;
    }

    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblStatus");
        if (lblstatus != null)
        {
            lblstatus.CssClass = lblstatus.Text == "Unpaid"
                ? "team-status-badge team-status-unpaid"
                : "team-status-badge team-status-paid";
        }
    }

    public DataTable getUserrightDirect(clsUser objUser)
    {
        string str_query = "SELECT COUNT(1) FROM UserDetail u WITH (NOLOCK) WHERE LTRIM(RTRIM(u.SponserId))='"
            + SqlEscape(objUser.UserId) + "' AND LTRIM(RTRIM(CONVERT(varchar(20), u.StandingPosition)))='2'";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAssociatesDetails();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillAssociatesDetails();
        filldashboard();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

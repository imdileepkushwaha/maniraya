using DataTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TopDirectRanking : Page
{
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
            LoadRanking();
        }
    }

    protected void ddTopCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRanking();
    }

    void LoadRanking()
    {
        int topCount = GetTopCount();
        DataTable dt = GetTopDirectRanking(topCount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        lblResultSummary.Text = (dt != null && dt.Rows.Count > 0)
            ? "Showing Top " + dt.Rows.Count + " members by active direct referrals."
            : "No ranking data found.";

        BindMyRankInfo();
    }

    int GetTopCount()
    {
        int topCount;
        if (int.TryParse(ddTopCount.SelectedValue, out topCount) && topCount > 0)
        {
            return topCount;
        }

        return 10;
    }

    DataTable GetTopDirectRanking(int topCount)
    {
        string sql = @"
;WITH Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        ISNULL(s.UserName, '') AS username,
        COUNT(d.UserId) AS TotalDirectCount,
        SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) AS ActiveDirectCount
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId)), s.UserName
)
SELECT TOP (" + topCount + @")
    ROW_NUMBER() OVER (ORDER BY ActiveDirectCount DESC, TotalDirectCount DESC, userid ASC) AS RankNo,
    userid,
    username,
    TotalDirectCount,
    ActiveDirectCount,
    ActiveDirectCount AS DirectCount
FROM Ranked
ORDER BY ActiveDirectCount DESC, TotalDirectCount DESC, userid ASC";

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    void BindMyRankInfo()
    {
        pnlMyRank.Visible = false;
        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return;
        }

        string sql = @"
;WITH Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        COUNT(d.UserId) AS TotalDirectCount,
        SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) AS ActiveDirectCount,
        ROW_NUMBER() OVER (
            ORDER BY SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) DESC,
                     COUNT(d.UserId) DESC,
                     LTRIM(RTRIM(s.UserId)) ASC
        ) AS RankNo
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId))
)
SELECT RankNo, TotalDirectCount, ActiveDirectCount
FROM Ranked
WHERE userid = '" + SqlEscape(currentUserId) + "'";

        try
        {
            ObjData.StartConnection();
            try
            {
                DataTable dt = ObjData.RunDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    pnlMyRank.Visible = true;
                    litMyRank.Text = string.Format(
                        " Your overall rank: <strong>#{0}</strong> | Active Directs: <strong>{1}</strong> | Total Directs: <strong>{2}</strong>.",
                        Convert.ToString(dt.Rows[0]["RankNo"]),
                        Convert.ToString(dt.Rows[0]["ActiveDirectCount"]),
                        Convert.ToString(dt.Rows[0]["TotalDirectCount"]));
                }
                else
                {
                    pnlMyRank.Visible = true;
                    litMyRank.Text = " You are not in the ranking yet (0 active directs).";
                }
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            pnlMyRank.Visible = false;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblRank = (Label)e.Row.FindControl("lblRank");
        if (lblRank != null)
        {
            int rankNo;
            if (int.TryParse(lblRank.Text, out rankNo))
            {
                if (rankNo == 1)
                {
                    lblRank.CssClass = "dash-rank-badge is-gold";
                }
                else if (rankNo == 2)
                {
                    lblRank.CssClass = "dash-rank-badge is-silver";
                }
                else if (rankNo == 3)
                {
                    lblRank.CssClass = "dash-rank-badge is-bronze";
                }
            }
        }

        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        DataRowView row = e.Row.DataItem as DataRowView;
        if (row != null && string.Equals(Convert.ToString(row["userid"]).Trim(), currentUserId, StringComparison.OrdinalIgnoreCase))
        {
            e.Row.CssClass = (e.Row.CssClass + " dash-rank-self-row").Trim();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

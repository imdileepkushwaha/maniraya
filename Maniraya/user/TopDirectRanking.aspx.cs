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
            ? "Showing Top " + dt.Rows.Count + " members by total direct referrals."
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
        COUNT(d.UserId) AS DirectCount
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(d.ActiveStatus, 0) = 1
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId)), s.UserName
)
SELECT TOP (" + topCount + @")
    ROW_NUMBER() OVER (ORDER BY DirectCount DESC, userid ASC) AS RankNo,
    userid,
    username,
    DirectCount
FROM Ranked
ORDER BY DirectCount DESC, userid ASC";

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
        COUNT(d.UserId) AS DirectCount,
        ROW_NUMBER() OVER (ORDER BY COUNT(d.UserId) DESC, LTRIM(RTRIM(s.UserId)) ASC) AS RankNo
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(d.ActiveStatus, 0) = 1
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId))
)
SELECT RankNo, DirectCount
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
                        " Your overall rank: <strong>#{0}</strong> with <strong>{1}</strong> total direct(s).",
                        Convert.ToString(dt.Rows[0]["RankNo"]),
                        Convert.ToString(dt.Rows[0]["DirectCount"]));
                }
                else
                {
                    pnlMyRank.Visible = true;
                    litMyRank.Text = " You are not in the ranking yet (0 directs).";
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

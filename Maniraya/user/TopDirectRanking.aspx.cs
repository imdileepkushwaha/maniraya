using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TopDirectRanking : Page
{
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
        string monthLabel = DirectRankHelper.GetCurrentMonthLabel();
        SetRenewalHeader(monthLabel);

        DataTable dt = DirectRankHelper.GetTopDirectRanking(topCount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        lblResultSummary.Text = (dt != null && dt.Rows.Count > 0)
            ? "Showing Top " + dt.Rows.Count + " members by Active Renewals Direct in " + monthLabel + "."
            : "No Active Renewals Direct found for " + monthLabel + ".";

        BindMyRankInfo(monthLabel);
    }

    void SetRenewalHeader(string monthLabel)
    {
        foreach (DataControlField col in GridView1.Columns)
        {
            if (col.HeaderText != null && col.HeaderText.StartsWith("Active Renewals Direct", StringComparison.OrdinalIgnoreCase))
            {
                col.HeaderText = "Active Renewals Direct (" + monthLabel + ")";
                break;
            }
        }
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

    void BindMyRankInfo(string monthLabel)
    {
        pnlMyRank.Visible = false;
        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return;
        }

        try
        {
            DataTable dt = DirectRankHelper.GetUserMonthlyRank(currentUserId);
            if (dt != null && dt.Rows.Count > 0)
            {
                int activeDirectCount = 0;
                int.TryParse(Convert.ToString(dt.Rows[0]["ActiveDirectCount"]), out activeDirectCount);
                string directRank = DirectRankHelper.GetRank(activeDirectCount);

                pnlMyRank.Visible = true;
                litMyRank.Text = string.Format(
                    " Your {0} rank: <strong>#{1}</strong> | Direct Rank: <strong>{2}</strong> | Active Directs: <strong>{3}</strong> | Active Renewals Direct this month: <strong>{4}</strong> | Total Directs: <strong>{5}</strong>.",
                    monthLabel,
                    Convert.ToString(dt.Rows[0]["RankNo"]),
                    directRank,
                    Convert.ToString(dt.Rows[0]["ActiveDirectCount"]),
                    Convert.ToString(dt.Rows[0]["ActiveRenewalDirectCount"]),
                    Convert.ToString(dt.Rows[0]["TotalDirectCount"]));
            }
            else
            {
                pnlMyRank.Visible = true;
                litMyRank.Text = " You have no Active Renewals Direct in <strong>" + monthLabel + "</strong> yet.";
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

        Label lblDirectRankTitle = (Label)e.Row.FindControl("lblDirectRankTitle");
        if (lblDirectRankTitle != null)
        {
            string rankName = (lblDirectRankTitle.Text ?? string.Empty).Trim();
            string css = "dash-direct-rank-pill";
            if (string.Equals(rankName, DirectRankHelper.RankDiamond, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-diamond";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankGold, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-gold";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankSilver, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-silver";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankBronze, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-bronze";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankDistributor, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-distributor";
            }
            else
            {
                css += " is-member";
            }
            lblDirectRankTitle.CssClass = css;
        }

        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        DataRowView row = e.Row.DataItem as DataRowView;
        if (row != null && string.Equals(Convert.ToString(row["userid"]).Trim(), currentUserId, StringComparison.OrdinalIgnoreCase))
        {
            e.Row.CssClass = (e.Row.CssClass + " dash-rank-self-row").Trim();
        }
    }
}

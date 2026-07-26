using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class user_DirectRankReport : Page
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
            BindReport();
        }
    }

    void BindReport()
    {
        string userId = Convert.ToString(Session["userid"]);
        DirectRankHelper.RankProgress progress = DirectRankHelper.GetProgressForUser(userId);
        IList<DirectRankHelper.LadderRow> ladder = DirectRankHelper.GetLadder(progress.ActiveDirectCount);

        lblCurrentRank.Text = progress.CurrentRank;
        lblActiveDirects.Text = progress.ActiveDirectCount.ToString();
        lblNextRank.Text = progress.IsMaxRank ? "Diamond ✓" : progress.NextRank;
        lblProgressSummary.Text = progress.SummaryText;
        lblRemaining.Text = progress.IsMaxRank ? "0" : progress.RemainingForNext.ToString();
        lblRemainingHero.Text = progress.IsMaxRank ? "0" : progress.RemainingForNext.ToString();
        lblRemainingBig.Text = progress.IsMaxRank ? "0" : progress.RemainingForNext.ToString();
        lblRemainHint.Text = progress.IsMaxRank ? "Highest rank" : "for " + progress.NextRank;
        lblProgressPct.Text = progress.ProgressPercent.ToString();
        divProgressFill.Style["width"] = progress.ProgressPercent + "%";

        ApplyMedalStyle(progress.CurrentRank);

        rptLadder.DataSource = ladder;
        rptLadder.DataBind();
    }

    void ApplyMedalStyle(string rank)
    {
        string tone = GetRankTone(rank);
        divRankMedal.Attributes["class"] = "dash-rank-medal is-" + tone;
        icoRankMedal.Attributes["class"] = "fa " + GetRankIcon(rank);
    }

    static string GetRankTone(string rank)
    {
        if (string.Equals(rank, DirectRankHelper.RankDistributor, StringComparison.OrdinalIgnoreCase))
        {
            return "distributor";
        }
        if (string.Equals(rank, DirectRankHelper.RankBronze, StringComparison.OrdinalIgnoreCase))
        {
            return "bronze";
        }
        if (string.Equals(rank, DirectRankHelper.RankSilver, StringComparison.OrdinalIgnoreCase))
        {
            return "silver";
        }
        if (string.Equals(rank, DirectRankHelper.RankGold, StringComparison.OrdinalIgnoreCase))
        {
            return "gold";
        }
        if (string.Equals(rank, DirectRankHelper.RankDiamond, StringComparison.OrdinalIgnoreCase))
        {
            return "diamond";
        }
        return "member";
    }

    static string GetRankIcon(string rank)
    {
        if (string.Equals(rank, DirectRankHelper.RankDistributor, StringComparison.OrdinalIgnoreCase))
        {
            return "fa-id-badge";
        }
        if (string.Equals(rank, DirectRankHelper.RankBronze, StringComparison.OrdinalIgnoreCase))
        {
            return "fa-medal";
        }
        if (string.Equals(rank, DirectRankHelper.RankSilver, StringComparison.OrdinalIgnoreCase))
        {
            return "fa-award";
        }
        if (string.Equals(rank, DirectRankHelper.RankGold, StringComparison.OrdinalIgnoreCase))
        {
            return "fa-trophy";
        }
        if (string.Equals(rank, DirectRankHelper.RankDiamond, StringComparison.OrdinalIgnoreCase))
        {
            return "fa-gem";
        }
        return "fa-user";
    }

    protected void rptLadder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {
            return;
        }

        DirectRankHelper.LadderRow row = e.Item.DataItem as DirectRankHelper.LadderRow;
        if (row == null)
        {
            return;
        }

        HtmlGenericControl divStep = e.Item.FindControl("divStep") as HtmlGenericControl;
        HtmlGenericControl spIcon = e.Item.FindControl("spIcon") as HtmlGenericControl;
        Label lblStatus = e.Item.FindControl("lblStatus") as Label;

        string tone = GetRankTone(row.RankName);
        string stepClass = "dash-rank-step";
        if (row.IsCurrent)
        {
            stepClass += " is-current";
        }
        else if (row.IsAchieved)
        {
            stepClass += " is-achieved";
        }
        else
        {
            stepClass += " is-locked";
        }

        if (divStep != null)
        {
            divStep.Attributes["class"] = stepClass;
        }
        if (spIcon != null)
        {
            spIcon.Attributes["class"] = "dash-rank-step-icon tone-" + tone;
            spIcon.InnerHtml = "<i class=\"fa " + GetRankIcon(row.RankName) + "\"></i>";
        }

        if (lblStatus == null)
        {
            return;
        }

        if (row.IsCurrent)
        {
            lblStatus.CssClass = "dash-rank-badge is-current";
        }
        else if (row.IsAchieved)
        {
            lblStatus.CssClass = "dash-rank-badge is-achieved";
        }
        else
        {
            lblStatus.CssClass = "dash-rank-badge is-locked";
        }
    }
}

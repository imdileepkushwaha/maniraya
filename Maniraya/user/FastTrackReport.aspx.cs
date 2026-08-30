using System;
using System.Web.UI;

public partial class user_FastTrackReport : Page
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
            BindStatus();
        }
    }

    void BindStatus()
    {
        string userId = Convert.ToString(Session["userid"]);
        System.Data.DataTable sales = FastTrackHelper.LoadQualifyingSales();
        FastTrackHelper.Progress progress = FastTrackHelper.GetProgress(userId, sales);

        lblL1Done.Text = progress.SelfDirects.ToString();
        lblL1Pending.Text = progress.SelfDirectsPending.ToString();
        lblL2Done.Text = progress.CompleteLegs.ToString();
        lblL2Pending.Text = progress.CompleteLegsPending.ToString();
        divL1Fill.Style["width"] = progress.SelfPercent + "%";
        divL2Fill.Style["width"] = progress.LegsPercent + "%";

        lblStatus.Text = progress.StatusLabel;
        lblStatus.CssClass = progress.IsAchieved ? "ft-status is-yes" : "ft-status is-no";

        gvLevel1.DataSource = FastTrackHelper.GetDirectBreakup(userId, sales);
        gvLevel1.DataBind();
    }
}

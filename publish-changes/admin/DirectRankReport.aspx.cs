using DataTier;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DirectRankReport : Page
{
    Data ObjData = new Data();
    DataTable _reportData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        LoadReport();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserId.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtMinActive.Text = string.Empty;
        ddRank.SelectedIndex = 0;
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadReport();
    }

    void LoadReport()
    {
        _reportData = null;
        BindGrid();
    }

    DataTable CurrentReportData
    {
        get
        {
            if (_reportData == null)
            {
                _reportData = GetReportData();
            }
            return _reportData;
        }
    }

    void BindGrid()
    {
        DataTable dt = CurrentReportData;
        if (dt == null || dt.Rows.Count == 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildExternalPager(0);
            lblSummary.Text = "No users found for selected filters.";
            return;
        }

        int totalRecords = dt.Rows.Count;
        int pageSize = GetPageSize();
        bool showAll = pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All";

        if (showAll)
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = Math.Max(totalRecords, 1);
            GridView1.PageIndex = 0;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        int fromRecord = 1;
        int toRecord = totalRecords;
        if (GridView1.AllowPaging && totalRecords > 0)
        {
            fromRecord = (GridView1.PageIndex * GridView1.PageSize) + 1;
            toRecord = Math.Min(totalRecords, (GridView1.PageIndex + 1) * GridView1.PageSize);
        }

        lblSummary.Text = "Showing " + fromRecord + "–" + toRecord + " of " + totalRecords
            + " user(s) · Rank live from Active Directs (SavingStatus = 1)";
        BuildExternalPager(totalRecords);
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }
        return 25;
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!GridView1.AllowPaging)
        {
            return dataItemIndex + 1;
        }
        return (GridView1.PageIndex * GridView1.PageSize) + dataItemIndex + 1;
    }

    void BuildExternalPager(int totalRecords)
    {
        pnlPager.Controls.Clear();

        if (!GridView1.AllowPaging || totalRecords <= 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = GridView1.PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"admin-pager-info\">Page " + (currentPage + 1) + " of " + totalPages
            + " · Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerLink(string text, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-active\">" + text + "</span>"));
            return;
        }
        if (!enabled)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-disabled\">" + text + "</span>"));
            return;
        }

        LinkButton link = new LinkButton();
        link.ID = "pagerBtn_" + pageIndex + "_" + text.Replace(" ", "");
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.Click += ExternalPager_Click;
        link.CausesValidation = false;
        pnlPager.Controls.Add(link);
    }

    protected void ExternalPager_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        int pageIndex;
        if (link == null || !int.TryParse(link.CommandArgument, out pageIndex))
        {
            return;
        }
        GridView1.PageIndex = pageIndex;
        LoadReport();
    }

    DataTable GetReportData()
    {
        StringBuilder sql = new StringBuilder();
        sql.Append(@"
SELECT
    LTRIM(RTRIM(u.UserId)) AS userid,
    ISNULL(u.UserName, '') AS username,
    ISNULL(u.Mobile, '') AS mobile,
    ISNULL(a.ActiveDirectCount, 0) AS activedirects
FROM UserDetail u WITH (NOLOCK)
LEFT JOIN (
    SELECT
        LTRIM(RTRIM(d.SponserId)) AS SponserId,
        COUNT(1) AS ActiveDirectCount
    FROM UserDetail d WITH (NOLOCK)
    WHERE ISNULL(d.SavingStatus, 0) = 1
      AND LTRIM(RTRIM(ISNULL(d.SponserId, ''))) <> ''
    GROUP BY LTRIM(RTRIM(d.SponserId))
) a ON a.SponserId = LTRIM(RTRIM(u.UserId))
WHERE 1 = 1");

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            sql.Append(" AND LTRIM(RTRIM(u.UserId)) LIKE '%")
                .Append(SqlEscape(txtUserId.Text.Trim())).Append("%'");
        }
        if (!string.IsNullOrWhiteSpace(txtUserName.Text))
        {
            sql.Append(" AND u.UserName LIKE '%")
                .Append(SqlEscape(txtUserName.Text.Trim())).Append("%'");
        }

        int minActive;
        if (!string.IsNullOrWhiteSpace(txtMinActive.Text) && int.TryParse(txtMinActive.Text.Trim(), out minActive))
        {
            sql.Append(" AND ISNULL(a.ActiveDirectCount, 0) >= ").Append(minActive);
        }

        sql.Append(" ORDER BY ISNULL(a.ActiveDirectCount, 0) DESC, u.UserId ASC");

        DataTable raw = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                raw = ObjData.RunDataTable(sql.ToString());
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            return new DataTable();
        }

        DataTable result = new DataTable();
        result.Columns.Add("userid", typeof(string));
        result.Columns.Add("username", typeof(string));
        result.Columns.Add("mobile", typeof(string));
        result.Columns.Add("activedirects", typeof(int));
        result.Columns.Add("currentrank", typeof(string));
        result.Columns.Add("nextrank", typeof(string));
        result.Columns.Add("remaining", typeof(string));
        result.Columns.Add("requirement", typeof(string));

        string rankFilter = (ddRank.SelectedValue ?? string.Empty).Trim();

        if (raw != null)
        {
            foreach (DataRow row in raw.Rows)
            {
                int active = 0;
                int.TryParse(Convert.ToString(row["activedirects"]), out active);
                DirectRankHelper.RankProgress progress = DirectRankHelper.GetProgress(active);

                if (!string.IsNullOrWhiteSpace(rankFilter) &&
                    !string.Equals(progress.CurrentRank, rankFilter, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                DataRow outRow = result.NewRow();
                outRow["userid"] = Convert.ToString(row["userid"]);
                outRow["username"] = Convert.ToString(row["username"]);
                outRow["mobile"] = Convert.ToString(row["mobile"]);
                outRow["activedirects"] = progress.ActiveDirectCount;
                outRow["currentrank"] = progress.CurrentRank;
                outRow["nextrank"] = progress.IsMaxRank ? "—" : progress.NextRank;
                outRow["remaining"] = progress.IsMaxRank ? "0" : progress.RemainingForNext.ToString();
                outRow["requirement"] = progress.IsMaxRank
                    ? "Highest rank achieved"
                    : progress.RemainingForNext + " more Active Direct(s) for " + progress.NextRank;
                result.Rows.Add(outRow);
            }
        }

        return result;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = GetReportData();
        if (dt == null || dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "noExcel", "alert('No records available to export.');", true);
            return;
        }

        GridView exportGrid = new GridView();
        exportGrid.AutoGenerateColumns = true;
        exportGrid.DataSource = dt;
        exportGrid.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DirectRankReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                exportGrid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for GridView Excel export
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

using DataTier;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TeamPendingInstallmentReport : Page
{
    Data ObjData = new Data();
    DataTable _reportData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        // Always reload from DB (no ViewState DataTable) so filters/pager work reliably.
        LoadReport();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddLevel.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtInstNo.Text = string.Empty;
        ddStatus.ClearSelection();
        ListItem pending = ddStatus.Items.FindByValue("Pending");
        if (pending != null)
        {
            pending.Selected = true;
        }

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
                _reportData = GetTeamPendingInstallments();
            }
            return _reportData;
        }
    }

    void BindGrid()
    {
        DataTable dt = CurrentReportData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            BuildExternalPager(0);
            lblResultSummary.Text = "No installment records found.";
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 25;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;

            if (dt.Rows.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
        lblResultSummary.Text = dt.Rows.Count > 0
            ? dt.Rows.Count + " installment record(s) found in your team."
            : "No installment records found for the selected filters.";
        BuildExternalPager(dt.Rows.Count);
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

    void BuildExternalPager(int totalRecords)
    {
        pnlPager.Controls.Clear();

        if (!GridView1.AllowPaging || totalRecords == 0)
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
            "<span class=\"saving-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-disabled\">...</span>"));
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-disabled\">...</span>"));
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerLink(string text, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-active\">" + text + "</span>"));
            return;
        }

        if (!enabled)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-disabled\">" + text + "</span>"));
            return;
        }

        LinkButton link = new LinkButton();
        link.Text = text;
        link.CssClass = "saving-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.Click += ExternalPager_Click;
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

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!GridView1.AllowPaging)
        {
            return dataItemIndex + 1;
        }

        return (GridView1.PageIndex * GridView1.PageSize) + dataItemIndex + 1;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblStatus = (Label)e.Row.FindControl("lblStatus");
        if (lblStatus == null)
        {
            return;
        }

        string status = (lblStatus.Text ?? string.Empty).Trim();
        string css = "dash-pending-status";
        if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
        {
            css += " is-pending";
        }
        else if (status.Equals("Processing", StringComparison.OrdinalIgnoreCase))
        {
            css += " is-processing";
        }
        else if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
        {
            css += " is-approved";
        }
        else if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
        {
            css += " is-rejected";
        }
        else
        {
            css += " is-pending";
        }

        lblStatus.CssClass = css;
    }

    DataTable GetTeamPendingInstallments()
    {
        string rootUserId = Convert.ToString(Session["userid"] ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(rootUserId))
        {
            return new DataTable();
        }

        string rootEsc = SqlEscape(rootUserId);
        StringBuilder sql = new StringBuilder();

        // Strict sponsor downline only (Level 1..10). Self and outside-team users never included.
        sql.Append(@";
WITH TeamCTE AS (
    SELECT
        LTRIM(RTRIM(ud.UserId)) AS UserId,
        ud.UserName,
        ud.Mobile,
        ud.Email,
        1 AS userlevel
    FROM UserDetail ud WITH (NOLOCK)
    WHERE LTRIM(RTRIM(ud.SponserId)) = '").Append(rootEsc).Append(@"'
      AND LTRIM(RTRIM(ud.UserId)) <> '").Append(rootEsc).Append(@"'
    UNION ALL
    SELECT
        LTRIM(RTRIM(c.UserId)) AS UserId,
        c.UserName,
        c.Mobile,
        c.Email,
        t.userlevel + 1
    FROM UserDetail c WITH (NOLOCK)
    INNER JOIN TeamCTE t ON LTRIM(RTRIM(c.SponserId)) = t.UserId
    WHERE t.userlevel < 10
      AND LTRIM(RTRIM(c.UserId)) <> '").Append(rootEsc).Append(@"'
)
SELECT
    t.userlevel,
    t.UserId AS userid,
    ISNULL(t.UserName, '') AS username,
    ISNULL(t.Mobile, '') AS mobile,
    ISNULL(t.Email, '') AS email,
    sa.InstallmentDate AS installmentdate,
    sa.InstNo AS instno,
    ISNULL(sa.Amount, 0) AS amount,
    ISNULL(sa.Status, '') AS status
FROM TeamCTE t
INNER JOIN SavingAccountInstallmentDetail sa WITH (NOLOCK)
    ON LTRIM(RTRIM(sa.UserId)) = t.UserId
WHERE t.UserId <> '").Append(rootEsc).Append(@"'");

        if (!string.IsNullOrWhiteSpace(ddLevel.SelectedValue))
        {
            sql.Append(" AND t.userlevel = ").Append(Convert.ToInt32(ddLevel.SelectedValue));
        }

        if (!string.IsNullOrWhiteSpace(ddStatus.SelectedValue))
        {
            sql.Append(" AND LTRIM(RTRIM(sa.Status)) = '").Append(SqlEscape(ddStatus.SelectedValue.Trim())).Append("'");
        }

        if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
        {
            try
            {
                DateTime fromDate = Message.GetIndianDate(txtFromDate.Text.Trim());
                sql.Append(" AND CONVERT(date, sa.InstallmentDate) >= CONVERT(date, '")
                    .Append(fromDate.ToString("yyyy-MM-dd")).Append("')");
            }
            catch
            {
            }
        }

        if (!string.IsNullOrWhiteSpace(txtToDate.Text))
        {
            try
            {
                DateTime toDate = Message.GetIndianDate(txtToDate.Text.Trim());
                sql.Append(" AND CONVERT(date, sa.InstallmentDate) <= CONVERT(date, '")
                    .Append(toDate.ToString("yyyy-MM-dd")).Append("')");
            }
            catch
            {
            }
        }

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            // Exact match only — still constrained to TeamCTE (outside team = 0 rows)
            sql.Append(" AND t.UserId = '").Append(SqlEscape(txtUserId.Text.Trim())).Append("'");
        }

        if (!string.IsNullOrWhiteSpace(txtInstNo.Text))
        {
            string instRaw = txtInstNo.Text.Trim();
            int instNo;
            if (int.TryParse(instRaw, out instNo))
            {
                sql.Append(" AND CONVERT(int, LTRIM(RTRIM(CONVERT(varchar(50), sa.InstNo)))) = ").Append(instNo);
            }
            else
            {
                sql.Append(" AND LTRIM(RTRIM(CONVERT(varchar(50), sa.InstNo))) = '")
                    .Append(SqlEscape(instRaw)).Append("'");
            }
        }

        sql.Append(@"
ORDER BY
    t.userlevel ASC,
    CASE WHEN CONVERT(date, sa.InstallmentDate) >= CONVERT(date, GETDATE()) THEN 0 ELSE 1 END,
    sa.InstallmentDate ASC,
    sa.InstNo ASC,
    sa.id ASC
OPTION (MAXRECURSION 10)");

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql.ToString());
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

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

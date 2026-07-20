using DataTier;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TeamSavingInstallmentReport : Page
{
    Data ObjData = new Data();

    DataTable TeamData
    {
        get { return ViewState["TeamInstallmentData"] as DataTable; }
        set { ViewState["TeamInstallmentData"] = value; }
    }

    int PageIndex
    {
        get { return ViewState["TeamInstallmentPageIndex"] != null ? (int)ViewState["TeamInstallmentPageIndex"] : 0; }
        set { ViewState["TeamInstallmentPageIndex"] = value; }
    }

    bool PagingEnabled
    {
        get { return ViewState["TeamInstallmentPagingEnabled"] != null && (bool)ViewState["TeamInstallmentPagingEnabled"]; }
        set { ViewState["TeamInstallmentPagingEnabled"] = value; }
    }

    int ActivePageSize
    {
        get { return ViewState["TeamInstallmentPageSize"] != null ? (int)ViewState["TeamInstallmentPageSize"] : 10; }
        set { ViewState["TeamInstallmentPageSize"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadTeam();
        }
        else if (TeamData != null)
        {
            BindGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageIndex = 0;
        LoadTeam();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddLevel.SelectedIndex = 0;
        ddSavingStatus.SelectedIndex = 0;
        txtUserId.Text = string.Empty;
        txtUserName.Text = string.Empty;
        PageIndex = 0;
        LoadTeam();
    }

    protected void ddLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = 0;
        LoadTeam();
    }

    protected void ddSavingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = 0;
        LoadTeam();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = 0;
        if (TeamData != null)
        {
            BindGrid();
        }
        else
        {
            LoadTeam();
        }
    }

    void LoadTeam()
    {
        TeamData = GetSponsorTeam();
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = TeamData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            pnlPager.Visible = false;
            lblResultSummary.Text = "No members found for the selected criteria.";
            return;
        }

        int pageSize = GetPageSize();
        PagingEnabled = pageSize > 0 && ddlRecordFilter.SelectedItem.Text != "All";
        ActivePageSize = PagingEnabled ? pageSize : Math.Max(dt.Rows.Count, 1);

        if (PagingEnabled && dt.Rows.Count > 0)
        {
            int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
            if (PageIndex >= totalPages)
            {
                PageIndex = Math.Max(0, totalPages - 1);
            }
        }
        else
        {
            PageIndex = 0;
        }

        GridView1.DataSource = GetPagedRows(dt);
        GridView1.DataBind();
        lblResultSummary.Text = dt.Rows.Count > 0
            ? dt.Rows.Count + " member(s) found in your 10-level sponsor team."
            : "No members found for the selected criteria.";
        BuildExternalPager();
    }

    DataTable GetPagedRows(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return dt == null ? new DataTable() : dt.Clone();
        }

        if (!PagingEnabled)
        {
            return dt;
        }

        DataTable page = dt.Clone();
        int start = PageIndex * ActivePageSize;
        int end = Math.Min(start + ActivePageSize, dt.Rows.Count);
        for (int i = start; i < end; i++)
        {
            page.ImportRow(dt.Rows[i]);
        }

        return page;
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 10;
    }

    void BuildExternalPager()
    {
        pnlPager.Controls.Clear();

        DataTable dt = TeamData;
        if (!PagingEnabled || dt == null || dt.Rows.Count == 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = ActivePageSize;
        int totalRecords = dt.Rows.Count;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"saving-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, Math.Min(currentPage - (windowSize / 2), totalPages - windowSize));
        if (startPage < 0)
        {
            startPage = 0;
        }
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);

        if (startPage > 0)
        {
            AddPagerEllipsis();
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            AddPagerEllipsis();
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerEllipsis()
    {
        pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-ellipsis\">...</span>"));
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

        PageIndex = pageIndex;
        BindGrid();
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!PagingEnabled)
        {
            return dataItemIndex + 1;
        }

        return (PageIndex * ActivePageSize) + dataItemIndex + 1;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblSavingStatus = (Label)e.Row.FindControl("lblSavingStatus");
        if (lblSavingStatus == null)
        {
            return;
        }

        string status = (lblSavingStatus.Text ?? string.Empty).Trim();
        if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
        {
            lblSavingStatus.CssClass = "dash-team-saving-status is-active";
        }
        else
        {
            lblSavingStatus.Text = "Inactive";
            lblSavingStatus.CssClass = "dash-team-saving-status is-inactive";
        }
    }

    DataTable GetSponsorTeam()
    {
        string rootUserId = Convert.ToString(Session["userid"]).Trim();
        StringBuilder sql = new StringBuilder();
        sql.Append(@";
WITH TeamCTE AS (
    SELECT
        ud.UserId,
        ud.UserName,
        ud.Mobile,
        ud.Email,
        ud.SponserId,
        ISNULL(ud.SavingStatus, 0) AS SavingStatus,
        1 AS userlevel
    FROM UserDetail ud WITH (NOLOCK)
    WHERE LTRIM(RTRIM(ud.SponserId)) = '").Append(SqlEscape(rootUserId)).Append(@"'
    UNION ALL
    SELECT
        c.UserId,
        c.UserName,
        c.Mobile,
        c.Email,
        c.SponserId,
        ISNULL(c.SavingStatus, 0) AS SavingStatus,
        t.userlevel + 1
    FROM UserDetail c WITH (NOLOCK)
    INNER JOIN TeamCTE t ON LTRIM(RTRIM(c.SponserId)) = LTRIM(RTRIM(t.UserId))
    WHERE t.userlevel < 10
)
SELECT
    LTRIM(RTRIM(UserId)) AS userid,
    ISNULL(UserName, '') AS username,
    ISNULL(Mobile, '') AS mobile,
    ISNULL(Email, '') AS email,
    LTRIM(RTRIM(ISNULL(SponserId, ''))) AS sponserid,
    ISNULL(SavingStatus, 0) AS savingstatus,
    CASE WHEN ISNULL(SavingStatus, 0) = 1 THEN 'Active' ELSE 'Inactive' END AS StatusDisplay,
    userlevel
FROM TeamCTE
WHERE 1 = 1");

        if (!string.IsNullOrWhiteSpace(ddLevel.SelectedValue))
        {
            sql.Append(" AND userlevel = ").Append(Convert.ToInt32(ddLevel.SelectedValue));
        }

        if (!string.IsNullOrWhiteSpace(ddSavingStatus.SelectedValue))
        {
            sql.Append(" AND ISNULL(SavingStatus, 0) = ").Append(Convert.ToInt32(ddSavingStatus.SelectedValue));
        }

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            sql.Append(" AND LTRIM(RTRIM(UserId)) LIKE '%").Append(SqlEscape(txtUserId.Text.Trim())).Append("%'");
        }

        if (!string.IsNullOrWhiteSpace(txtUserName.Text))
        {
            sql.Append(" AND UserName LIKE '%").Append(SqlEscape(txtUserName.Text.Trim())).Append("%'");
        }

        sql.Append(" ORDER BY userlevel ASC, UserId ASC");

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

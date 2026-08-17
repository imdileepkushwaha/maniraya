using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Bonanza : Page
{
    const string AdminExcludeUserId = "MP000001";
    const int DefaultRequiredQualifyingLegs = 10;
    const int MinActiveUnderLeg = 10;
    const int DefaultRequiredTeamActive = 500;

    Data ObjData = new Data();

    DataTable ReportData
    {
        get { return ViewState["BonanzaData"] as DataTable; }
        set { ViewState["BonanzaData"] = value; }
    }

    int GetRequiredTeamActive()
    {
        int teamActive;
        if (txtTeamActive != null && int.TryParse((txtTeamActive.Text ?? string.Empty).Trim(), out teamActive) && teamActive > 0)
        {
            return teamActive;
        }
        return DefaultRequiredTeamActive;
    }

    int GetRequiredQualifyingLegs()
    {
        int legs;
        if (txtQualifyingLegs != null && int.TryParse((txtQualifyingLegs.Text ?? string.Empty).Trim(), out legs) && legs > 0)
        {
            return legs;
        }
        return DefaultRequiredQualifyingLegs;
    }

    /// <summary>
    /// Optional upper bound. Returns 0 when blank/invalid (no max limit).
    /// </summary>
    int GetMaximumTeamActive()
    {
        int maxTeamActive;
        if (txtMaxTeamActive != null
            && int.TryParse((txtMaxTeamActive.Text ?? string.Empty).Trim(), out maxTeamActive)
            && maxTeamActive > 0)
        {
            return maxTeamActive;
        }
        return 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            lblSummary.Text = "Run search to check Bonanza qualification.";
        }
        else if (ReportData != null)
        {
            // Rebuild pager only — rebinding GridView here breaks RowCommand / View click.
            BuildExternalPager();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        pnlLegs.Visible = false;
        LoadReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserId.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        txtMinDirects.Text = "10";
        txtQualifyingLegs.Text = DefaultRequiredQualifyingLegs.ToString();
        txtTeamActive.Text = DefaultRequiredTeamActive.ToString();
        txtMaxTeamActive.Text = string.Empty;
        GridView1.PageIndex = 0;
        pnlLegs.Visible = false;
        ReportData = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        BuildExternalPager();
        lblSummary.Text = "Run search to check Bonanza qualification.";
        ShowLoadError(null);
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (ReportData != null)
        {
            BindGrid();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
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
        BindGrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "viewlegs", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string userId = Convert.ToString(e.CommandArgument);
        LoadLegs(userId);
    }

    protected void lnkLegs_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        if (link == null)
        {
            return;
        }

        LoadLegs(Convert.ToString(link.CommandArgument));
    }

    void LoadReport()
    {
        string loadError;
        ReportData = GetBonanzaReport(out loadError);
        ShowLoadError(loadError);
        BindGrid();
    }

    void ShowLoadError(string message)
    {
        bool hasError = !string.IsNullOrWhiteSpace(message);
        pnlLoadError.Visible = hasError;
        litLoadError.Text = hasError ? message : string.Empty;
    }

    void BindGrid()
    {
        DataTable dt = ReportData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            BuildExternalPager();
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 10;
            GridView1.PageIndex = 0;
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

        int total = dt.Rows.Count;
        int qualified = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (row["isqualified"] != DBNull.Value && Convert.ToBoolean(row["isqualified"]))
            {
                qualified++;
            }
        }

        lblSummary.Text = total + " user(s) · " + qualified + " Qualified · Need "
            + GetRequiredQualifyingLegs() + " legs (≥" + MinActiveUnderLeg + " under each) · Team Active ≥ "
            + GetRequiredTeamActive()
            + (GetMaximumTeamActive() > 0 ? " and ≤ " + GetMaximumTeamActive() : "")
            + " · Exclude " + AdminExcludeUserId;
        BuildExternalPager();
    }

    void LoadLegs(string userId)
    {
        string loadError;
        DataTable legs = GetLegBreakup(userId, out loadError);
        if (!string.IsNullOrWhiteSpace(loadError))
        {
            ShowLoadError(loadError);
        }
        else
        {
            ShowLoadError(null);
        }

        litLegsUser.Text = Server.HtmlEncode((userId ?? string.Empty).Trim());
        gvLegs.DataSource = legs;
        gvLegs.DataBind();
        pnlLegs.Visible = true;

        // Keep main grid visible after click
        if (ReportData != null)
        {
            BindGrid();
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "bonanzaScrollLegs",
            "setTimeout(function(){var el=document.getElementById('" + pnlLegs.ClientID + "'); if(el){el.scrollIntoView({behavior:'smooth',block:'start'});}},50);",
            true);
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

    void BuildExternalPager()
    {
        pnlPager.Controls.Clear();

        DataTable dt = ReportData;
        if (!GridView1.AllowPaging || dt == null || dt.Rows.Count == 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
        int totalRecords = dt.Rows.Count;
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
            "<span class=\"admin-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", "nav_first", 0, currentPage > 0, false);
        AddPagerLink("Prev", "nav_prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            AddPagerEllipsis("ell_start");
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), "nav_page_" + i, i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            AddPagerEllipsis("ell_end");
        }

        AddPagerLink("Next", "nav_next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", "nav_last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerEllipsis(string controlId)
    {
        Literal ellipsis = new Literal();
        ellipsis.ID = controlId;
        ellipsis.Text = "<span class=\"admin-pager-btn is-ellipsis\">...</span>";
        pnlPager.Controls.Add(ellipsis);
    }

    void AddPagerLink(string text, string controlId, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            Literal active = new Literal();
            active.ID = controlId + "_active";
            active.Text = "<span class=\"admin-pager-btn is-active\">" + text + "</span>";
            pnlPager.Controls.Add(active);
            return;
        }

        if (!enabled)
        {
            Literal disabled = new Literal();
            disabled.ID = controlId + "_disabled";
            disabled.Text = "<span class=\"admin-pager-btn is-disabled\">" + text + "</span>";
            pnlPager.Controls.Add(disabled);
            return;
        }

        LinkButton link = new LinkButton();
        link.ID = controlId;
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandName = "Page";
        link.CommandArgument = pageIndex.ToString();
        link.CausesValidation = false;
        link.Click += ExternalPager_Click;
        pnlPager.Controls.Add(link);
    }

    DataTable GetBonanzaReport(out string loadError)
    {
        loadError = string.Empty;

        int minDirects = 10;
        int parsedMin;
        if (int.TryParse((txtMinDirects.Text ?? string.Empty).Trim(), out parsedMin) && parsedMin >= 0)
        {
            minDirects = parsedMin;
        }

        int requiredTeamActive = GetRequiredTeamActive();
        int maximumTeamActive = GetMaximumTeamActive();
        int requiredQualifyingLegs = GetRequiredQualifyingLegs();
        if (maximumTeamActive > 0 && maximumTeamActive < requiredTeamActive)
        {
            maximumTeamActive = requiredTeamActive;
        }

        string search = (txtUserId.Text ?? string.Empty).Trim();
        string statusFilter = (ddlStatus.SelectedValue ?? string.Empty).Trim();

        BonanzaNetwork network;
        try
        {
            network = LoadNetwork();
        }
        catch (Exception ex)
        {
            loadError = "Unable to load Bonanza data. " + (ex != null ? ex.Message : string.Empty);
            return new DataTable();
        }

        DataTable result = CreateReportTable();
        Dictionary<string, int> teamActiveCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (KeyValuePair<string, BonanzaUser> pair in network.Users)
        {
            BonanzaUser user = pair.Value;
            if (!user.IsActive)
            {
                continue;
            }

            if (string.Equals(user.UserId, AdminExcludeUserId, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(search)
                && user.UserId.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0
                && (user.UserName ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0)
            {
                continue;
            }

            List<string> activeDirects = GetActiveDirects(network, user.UserId);
            int activeDirectCount = activeDirects.Count;
            if (activeDirectCount < minDirects)
            {
                continue;
            }

            int qualifyingLegs = 0;
            foreach (string legId in activeDirects)
            {
                int underActive = GetTeamActiveCount(network, legId, teamActiveCache);
                if (underActive >= MinActiveUnderLeg)
                {
                    qualifyingLegs++;
                }
            }

            int teamActive = GetTeamActiveCount(network, user.UserId, teamActiveCache);
            bool isQualified = qualifyingLegs >= requiredQualifyingLegs
                && teamActive >= requiredTeamActive
                && (maximumTeamActive <= 0 || teamActive <= maximumTeamActive);

            if (string.Equals(statusFilter, "Qualified", StringComparison.OrdinalIgnoreCase) && !isQualified)
            {
                continue;
            }

            if (string.Equals(statusFilter, "NotQualified", StringComparison.OrdinalIgnoreCase) && isQualified)
            {
                continue;
            }

            DataRow row = result.NewRow();
            row["userid"] = user.UserId;
            row["username"] = user.UserName;
            row["mobile"] = user.Mobile;
            row["activedirects"] = activeDirectCount;
            row["qualifyinglegs"] = qualifyingLegs;
            row["teamactive"] = teamActive;
            row["isqualified"] = isQualified;
            row["statuslabel"] = isQualified ? "Qualified" : "Not Qualified";
            result.Rows.Add(row);
        }

        DataView view = result.DefaultView;
        view.Sort = "isqualified DESC, qualifyinglegs DESC, teamactive DESC, userid ASC";
        return view.ToTable();
    }

    DataTable GetLegBreakup(string userId, out string loadError)
    {
        loadError = string.Empty;
        userId = (userId ?? string.Empty).Trim();
        DataTable legs = CreateLegTable();

        if (string.IsNullOrWhiteSpace(userId)
            || string.Equals(userId, AdminExcludeUserId, StringComparison.OrdinalIgnoreCase))
        {
            return legs;
        }

        BonanzaNetwork network;
        try
        {
            network = LoadNetwork();
        }
        catch (Exception ex)
        {
            loadError = "Unable to load leg breakup. " + (ex != null ? ex.Message : string.Empty);
            return legs;
        }

        Dictionary<string, int> teamActiveCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (string legId in GetActiveDirects(network, userId))
        {
            BonanzaUser leg;
            if (!network.Users.TryGetValue(legId, out leg))
            {
                continue;
            }

            int underActive = GetTeamActiveCount(network, legId, teamActiveCache);
            DataRow row = legs.NewRow();
            row["leguserid"] = leg.UserId;
            row["legusername"] = leg.UserName;
            row["underactive"] = underActive;
            row["iscounted"] = underActive >= MinActiveUnderLeg;
            legs.Rows.Add(row);
        }

        DataView view = legs.DefaultView;
        view.Sort = "underactive DESC, leguserid ASC";
        return view.ToTable();
    }

    BonanzaNetwork LoadNetwork()
    {
        string sql = @"
SELECT
    LTRIM(RTRIM(UserId)) AS UserId,
    LTRIM(RTRIM(ISNULL(SponserId, ''))) AS SponserId,
    ISNULL(SavingStatus, 0) AS SavingStatus,
    ISNULL(UserName, '') AS UserName,
    ISNULL(Mobile, '') AS Mobile
FROM UserDetail WITH (NOLOCK)
WHERE NULLIF(LTRIM(RTRIM(UserId)), '') IS NOT NULL";

        DataTable dt;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(sql) ?? new DataTable();
        }
        finally
        {
            ObjData.EndConnection();
        }

        var network = new BonanzaNetwork();
        foreach (DataRow row in dt.Rows)
        {
            string userId = Convert.ToString(row["UserId"]).Trim();
            if (string.IsNullOrWhiteSpace(userId))
            {
                continue;
            }

            if (string.Equals(userId, AdminExcludeUserId, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var user = new BonanzaUser
            {
                UserId = userId,
                SponserId = Convert.ToString(row["SponserId"]).Trim(),
                IsActive = Convert.ToInt32(row["SavingStatus"]) == 1,
                UserName = Convert.ToString(row["UserName"]).Trim(),
                Mobile = Convert.ToString(row["Mobile"]).Trim()
            };

            network.Users[userId] = user;
        }

        foreach (BonanzaUser user in network.Users.Values)
        {
            if (string.IsNullOrWhiteSpace(user.SponserId)
                || string.Equals(user.SponserId, AdminExcludeUserId, StringComparison.OrdinalIgnoreCase)
                || string.Equals(user.SponserId, user.UserId, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!network.Users.ContainsKey(user.SponserId))
            {
                continue;
            }

            List<string> children;
            if (!network.Children.TryGetValue(user.SponserId, out children))
            {
                children = new List<string>();
                network.Children[user.SponserId] = children;
            }

            children.Add(user.UserId);
        }

        return network;
    }

    static List<string> GetActiveDirects(BonanzaNetwork network, string userId)
    {
        List<string> result = new List<string>();
        List<string> children;
        if (!network.Children.TryGetValue(userId, out children))
        {
            return result;
        }

        foreach (string childId in children)
        {
            BonanzaUser child;
            if (network.Users.TryGetValue(childId, out child) && child.IsActive)
            {
                result.Add(childId);
            }
        }

        return result;
    }

    static int GetTeamActiveCount(BonanzaNetwork network, string rootUserId, Dictionary<string, int> cache)
    {
        int cached;
        if (cache.TryGetValue(rootUserId, out cached))
        {
            return cached;
        }

        int count = 0;
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var queue = new Queue<string>();
        queue.Enqueue(rootUserId);
        visited.Add(rootUserId);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            List<string> children;
            if (!network.Children.TryGetValue(current, out children))
            {
                continue;
            }

            foreach (string childId in children)
            {
                if (!visited.Add(childId))
                {
                    continue;
                }

                BonanzaUser child;
                if (!network.Users.TryGetValue(childId, out child))
                {
                    continue;
                }

                if (child.IsActive)
                {
                    count++;
                }

                queue.Enqueue(childId);
            }
        }

        cache[rootUserId] = count;
        return count;
    }

    static DataTable CreateReportTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("userid", typeof(string));
        dt.Columns.Add("username", typeof(string));
        dt.Columns.Add("mobile", typeof(string));
        dt.Columns.Add("activedirects", typeof(int));
        dt.Columns.Add("qualifyinglegs", typeof(int));
        dt.Columns.Add("teamactive", typeof(int));
        dt.Columns.Add("isqualified", typeof(bool));
        dt.Columns.Add("statuslabel", typeof(string));
        return dt;
    }

    static DataTable CreateLegTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("leguserid", typeof(string));
        dt.Columns.Add("legusername", typeof(string));
        dt.Columns.Add("underactive", typeof(int));
        dt.Columns.Add("iscounted", typeof(bool));
        return dt;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (ReportData == null)
        {
            LoadReport();
        }

        DataTable dt = ReportData;
        if (dt == null || dt.Rows.Count == 0)
        {
            ShowLoadError("No data available to export. Please run Search first.");
            return;
        }

        GridView1.AllowPaging = false;
        GridView1.PageIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (GridView1.HeaderRow == null)
        {
            ShowLoadError("Unable to export grid.");
            BindGrid();
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BonanzaReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    cell.CssClass = "textmode";
                }
            }
            GridView1.RenderControl(hw);
            Response.Write(@"<style> .textmode { mso-number-format:\@; } </style>");
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    sealed class BonanzaNetwork
    {
        public Dictionary<string, BonanzaUser> Users = new Dictionary<string, BonanzaUser>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, List<string>> Children = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
    }

    sealed class BonanzaUser
    {
        public string UserId;
        public string SponserId;
        public string UserName;
        public string Mobile;
        public bool IsActive;
    }
}

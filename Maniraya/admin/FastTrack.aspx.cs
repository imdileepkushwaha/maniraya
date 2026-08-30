using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_FastTrack : Page
{
    const string AdminExcludeUserId = "MP000001";

    Data ObjData = new Data();

    DataTable ReportData
    {
        get { return ViewState["FastTrackData"] as DataTable; }
        set { ViewState["FastTrackData"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureBulkColumns();

        if (!IsPostBack)
        {
            LoadReport();
        }
        else if (ReportData != null)
        {
            BuildExternalPager();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        pnlSales.Visible = false;
        LoadReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserId.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        GridView1.PageIndex = 0;
        pnlSales.Visible = false;
        ReportData = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        BuildExternalPager();
            lblSummary.Text = "Run search to check Thailand Trip Bonanza qualification.";
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
        if (!string.Equals(e.CommandName, "viewsales", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        LoadSales(Convert.ToString(e.CommandArgument));
    }

    void LoadReport()
    {
        string loadError;
        ReportData = GetFastTrackReport(out loadError);
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
            + FastTrackHelper.RequiredSelfDirects + " self directs AND "
            + FastTrackHelper.RequiredCompleteLegs + " of those with "
            + FastTrackHelper.RequiredDirectsPerLeg + " directs each · "
            + FastTrackHelper.OfferStartDate.ToString("dd MMM yyyy") + " to "
            + FastTrackHelper.OfferEndDate.ToString("dd MMM yyyy");
        BuildExternalPager();
    }

    void LoadSales(string userId)
    {
        string loadError = string.Empty;
        DataTable directs;
        try
        {
            directs = FastTrackHelper.GetDirectBreakup(userId);
        }
        catch (Exception ex)
        {
            loadError = "Unable to load directs. " + (ex != null ? ex.Message : string.Empty);
            directs = new DataTable();
        }

        if (!string.IsNullOrWhiteSpace(loadError))
        {
            ShowLoadError(loadError);
        }
        else
        {
            ShowLoadError(null);
        }

        litSalesUser.Text = Server.HtmlEncode((userId ?? string.Empty).Trim());
        gvLevel1.DataSource = directs;
        gvLevel1.DataBind();
        pnlSales.Visible = true;

        if (ReportData != null)
        {
            BindGrid();
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "fastTrackScrollSales",
            "setTimeout(function(){var el=document.getElementById('" + pnlSales.ClientID + "'); if(el){el.scrollIntoView({behavior:'smooth',block:'start'});}},50);",
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

    DataTable GetFastTrackReport(out string loadError)
    {
        loadError = string.Empty;
        DataTable result = CreateReportTable();
        string search = (txtUserId.Text ?? string.Empty).Trim();
        string statusFilter = (ddlStatus.SelectedValue ?? string.Empty).Trim();

        DataTable sales;
        try
        {
            sales = FastTrackHelper.LoadQualifyingSales();
        }
        catch (Exception ex)
        {
            loadError = "Unable to load Thailand Trip Bonanza data. " + (ex != null ? ex.Message : string.Empty);
            return result;
        }

        Dictionary<string, HashSet<string>> directMap = FastTrackHelper.BuildDirectMap(sales);
        Dictionary<string, UserInfo> users = string.IsNullOrWhiteSpace(search)
            ? LoadActiveUsers()
            : LoadUsersBySearch(search);

        foreach (KeyValuePair<string, UserInfo> pair in users)
        {
            UserInfo user = pair.Value;

            if (!string.IsNullOrWhiteSpace(search)
                && user.UserId.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0
                && (user.UserName ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0)
            {
                continue;
            }

            FastTrackHelper.Progress progress = FastTrackHelper.GetProgress(user.UserId, directMap);
            bool isQualified = progress.IsAchieved;

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
            row["selfdirects"] = progress.SelfDirects;
            row["completelegs"] = progress.CompleteLegs;
            row["selfpending"] = progress.SelfDirectsPending;
            row["legspending"] = progress.CompleteLegsPending;
            row["isqualified"] = isQualified;
            row["statuslabel"] = isQualified ? "Qualified" : "Not Qualified";
            result.Rows.Add(row);
        }

        DataView view = result.DefaultView;
        view.Sort = "isqualified ASC, selfdirects DESC, completelegs DESC, userid ASC";
        return view.ToTable();
    }

    Dictionary<string, UserInfo> LoadActiveUsers()
    {
        string sql = @"
SELECT LTRIM(RTRIM(UserId)) AS UserId, ISNULL(UserName, '') AS UserName, ISNULL(Mobile, '') AS Mobile
FROM UserDetail WITH (NOLOCK)
WHERE ISNULL(SavingStatus, 0) = 1
  AND LTRIM(RTRIM(UserId)) <> '" + SqlEscape(AdminExcludeUserId) + @"'
  AND NULLIF(LTRIM(RTRIM(UserId)), '') IS NOT NULL";
        return MapUsers(RunUserQuery(sql));
    }

    Dictionary<string, UserInfo> LoadUsersBySearch(string search)
    {
        string safe = SqlEscape(search);
        string sql = @"
SELECT LTRIM(RTRIM(UserId)) AS UserId, ISNULL(UserName, '') AS UserName, ISNULL(Mobile, '') AS Mobile
FROM UserDetail WITH (NOLOCK)
WHERE ISNULL(SavingStatus, 0) = 1
  AND LTRIM(RTRIM(UserId)) <> '" + SqlEscape(AdminExcludeUserId) + @"'
  AND (UserId LIKE '%" + safe + @"%' OR UserName LIKE '%" + safe + @"%')";
        return MapUsers(RunUserQuery(sql));
    }

    DataTable RunUserQuery(string sql)
    {
        ObjData.StartConnection();
        try
        {
            return ObjData.RunDataTable(sql) ?? new DataTable();
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    static Dictionary<string, UserInfo> MapUsers(DataTable dt)
    {
        var map = new Dictionary<string, UserInfo>(StringComparer.OrdinalIgnoreCase);
        if (dt == null)
        {
            return map;
        }

        foreach (DataRow row in dt.Rows)
        {
            string userId = Convert.ToString(row["UserId"]).Trim();
            if (string.IsNullOrWhiteSpace(userId))
            {
                continue;
            }

            map[userId] = new UserInfo
            {
                UserId = userId,
                UserName = Convert.ToString(row["UserName"]).Trim(),
                Mobile = Convert.ToString(row["Mobile"]).Trim()
            };
        }

        return map;
    }

    static bool IsCountableSponsor(string userId)
    {
        return !string.IsNullOrWhiteSpace(userId)
            && !string.Equals(userId, AdminExcludeUserId, StringComparison.OrdinalIgnoreCase)
            && !string.Equals(userId, "0", StringComparison.OrdinalIgnoreCase);
    }

    static void AddCount(Dictionary<string, int> map, string key)
    {
        int current;
        map.TryGetValue(key, out current);
        map[key] = current + 1;
    }

    static int GetCount(Dictionary<string, int> map, string key)
    {
        int current;
        return map.TryGetValue(key, out current) ? current : 0;
    }

    static decimal GetDecimalValue(object value)
    {
        decimal parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }

        return decimal.TryParse(Convert.ToString(value), out parsed) ? parsed : 0m;
    }

    static DateTime GetDateValue(object value)
    {
        DateTime parsed;
        if (value == null || value == DBNull.Value)
        {
            return DateTime.MinValue;
        }

        return DateTime.TryParse(Convert.ToString(value), out parsed) ? parsed : DateTime.MinValue;
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static DataTable CreateReportTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("userid", typeof(string));
        dt.Columns.Add("username", typeof(string));
        dt.Columns.Add("mobile", typeof(string));
        dt.Columns.Add("selfdirects", typeof(int));
        dt.Columns.Add("completelegs", typeof(int));
        dt.Columns.Add("selfpending", typeof(int));
        dt.Columns.Add("legspending", typeof(int));
        dt.Columns.Add("isqualified", typeof(bool));
        dt.Columns.Add("statuslabel", typeof(string));
        return dt;
    }

    static DataTable CreateSaleTable(bool includeDirect)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("buyerid", typeof(string));
        dt.Columns.Add("buyername", typeof(string));
        dt.Columns.Add("orderid", typeof(string));
        dt.Columns.Add("amount", typeof(decimal));
        dt.Columns.Add("saledate", typeof(DateTime));
        if (includeDirect)
        {
            dt.Columns.Add("directid", typeof(string));
        }
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
        Response.AddHeader("content-disposition", "attachment;filename=FastTrackReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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

    sealed class UserInfo
    {
        public string UserId;
        public string UserName;
        public string Mobile;
    }
}

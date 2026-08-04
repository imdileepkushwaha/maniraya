using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class admin_CouponReport : System.Web.UI.Page
{
    Data ObjData = new Data();

    DataTable CouponData
    {
        get { return ViewState["CouponData"] as DataTable; }
        set { ViewState["CouponData"] = value; }
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
            PopulateApproveMonthFilter();
            LoadCouponReport();
        }
        else if (CouponData != null)
        {
            BindGrid();
            BindPrint();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadCouponReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtCouponCode.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        if (ddlApproveMonth.Items.Count > 0)
        {
            ddlApproveMonth.SelectedIndex = 0;
        }
        if (ddlDrawType.Items.Count > 0)
        {
            ddlDrawType.SelectedIndex = 0;
        }
        GridView1.PageIndex = 0;
        LoadCouponReport();
    }

    protected void ddlApproveMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadCouponReport();
    }

    protected void ddlDrawType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadCouponReport();
    }

    void PopulateApproveMonthFilter()
    {
        ddlApproveMonth.Items.Clear();
        ddlApproveMonth.Items.Add(new ListItem("All Months", ""));

        DataTable dt = GetAvailableApproveMonths();
        if (dt == null || dt.Rows.Count == 0)
        {
            DateTime currentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            ddlApproveMonth.Items.Add(new ListItem(
                currentMonth.ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                currentMonth.ToString("yyyy-MM", CultureInfo.InvariantCulture)));
            return;
        }

        foreach (DataRow row in dt.Rows)
        {
            int year;
            int month;
            if (!int.TryParse(Convert.ToString(row["Y"]), out year) || !int.TryParse(Convert.ToString(row["M"]), out month))
            {
                continue;
            }

            DateTime monthDate = new DateTime(year, month, 1);
            ddlApproveMonth.Items.Add(new ListItem(
                monthDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                monthDate.ToString("yyyy-MM", CultureInfo.InvariantCulture)));
        }
    }

    DataTable GetAvailableApproveMonths()
    {
        // Months from both account and installment approvals (coupon-linked only).
        string sql = @"
SELECT Y, M
FROM (
    SELECT DISTINCT
        YEAR(CONVERT(date, sd.approvedate)) AS Y,
        MONTH(CONVERT(date, sd.approvedate)) AS M
    FROM SavingAccountDetail sd WITH (NOLOCK)
    WHERE sd.approvedate IS NOT NULL
      AND ISNULL(LTRIM(RTRIM(sd.couponcode)), '') <> ''
      AND " + GetApprovedStatusFilter("sd") + @"
    UNION
    SELECT DISTINCT
        YEAR(CONVERT(date, sa.approvedate)) AS Y,
        MONTH(CONVERT(date, sa.approvedate)) AS M
    FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
    OUTER APPLY (
        SELECT TOP 1 sd0.couponcode
        FROM SavingAccountDetail sd0 WITH (NOLOCK)
        WHERE sd0.orderid = sa.orderid
          AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
        ORDER BY
            CASE WHEN sd0.productid = sa.productid THEN 0 ELSE 1 END,
            sd0.id ASC
    ) sd
    WHERE sa.approvedate IS NOT NULL
      AND ISNULL(LTRIM(RTRIM(sd.couponcode)), '') <> ''
      AND " + GetApprovedStatusFilter("sa") + @"
) x
ORDER BY Y DESC, M DESC";

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

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (CouponData != null)
        {
            BindGrid();
        }
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

    void LoadCouponReport()
    {
        string loadError;
        CouponData = GetApprovedCoupons(out loadError);
        ShowLoadError(loadError);
        BindGrid();
        BindPrint();
    }

    void ShowLoadError(string message)
    {
        bool hasError = !string.IsNullOrWhiteSpace(message);
        pnlLoadError.Visible = hasError;
        litLoadError.Text = hasError ? message : string.Empty;
    }

    void BindGrid()
    {
        DataTable dt = CouponData;
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
        BuildExternalPager();
    }

    void BindPrint()
    {
        DataTable source = CouponData ?? new DataTable();
        DataTable printTickets = BuildPrintTickets(source);
        rptPrintCoupons.DataSource = printTickets;
        rptPrintCoupons.DataBind();

        pnlPrintArea.Visible = printTickets.Rows.Count > 0;
        btnPrintAll.Visible = printTickets.Rows.Count > 0;
        litCouponCount.Text = printTickets.Rows.Count.ToString();
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

        DataTable dt = CouponData;
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

        // First | Prev | 1 2 3 4 5 | ... | Next | Last
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

    DataTable GetApprovedCoupons(out string loadError)
    {
        loadError = string.Empty;

        string drawType = (ddlDrawType.SelectedValue ?? string.Empty).Trim();
        StringBuilder sql = new StringBuilder();

        if (string.Equals(drawType, "Super", StringComparison.OrdinalIgnoreCase))
        {
            // Super Draw: installment approvals day 1–10; InstNo 1 excluded
            BuildInstallmentCouponSelect(sql, "sa", "sd", excludeInstNoOne: true);
            AppendCommonSearchFilters(sql, "sa", "sd", "ud");
            AppendApproveDateRangeFilter(sql, "sa.approvedate", useSuperDrawWindow: true);
            sql.Append(" ORDER BY sa.id DESC");
        }
        else if (string.Equals(drawType, "Mega", StringComparison.OrdinalIgnoreCase))
        {
            // Mega Draw: installment approvals for full month (day 1–last day)
            BuildInstallmentCouponSelect(sql, "sa", "sd", excludeInstNoOne: false);
            AppendCommonSearchFilters(sql, "sa", "sd", "ud");
            AppendApproveDateRangeFilter(sql, "sa.approvedate", useSuperDrawWindow: false);
            sql.Append(" ORDER BY sa.id DESC");
        }
        else
        {
            // No draw type: approved SavingAccountDetail coupons only
            BuildAccountCouponSelect(sql, includeTrailingWhere: true);
            AppendCommonSearchFilters(sql, "sd", "sd", "ud");
            AppendApproveDateRangeFilter(sql, "sd.approvedate", useSuperDrawWindow: false);
            sql.Append(" ORDER BY sd.id DESC");
        }

        DataTable dt = null;
        bool connectionOpened = false;
        try
        {
            ObjData.StartConnection();
            connectionOpened = true;
            dt = ObjData.RunDataTable(sql.ToString());
        }
        catch (Exception ex)
        {
            dt = null;
            loadError = "Unable to load coupon data. Please check the database connection and try again.";
            if (ex != null && !string.IsNullOrWhiteSpace(ex.Message))
            {
                loadError += " (" + ex.Message + ")";
            }
        }
        finally
        {
            if (connectionOpened)
            {
                try
                {
                    ObjData.EndConnection();
                }
                catch
                {
                }
            }
        }

        return dt ?? new DataTable();
    }

    void BuildAccountCouponSelect(StringBuilder sql, bool includeTrailingWhere)
    {
        sql.Append(@"
SELECT sd.couponcode,
    sd.approvedate,
    sd.amount,
    1 AS quantity,
    sd.status,
    pm.productname,
    ud.username,
    ud.userid,
    ud.mobile
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sd.userid");
        if (includeTrailingWhere)
        {
            sql.Append(@"
WHERE ").Append(GetApprovedStatusFilter("sd")).Append(@"
  AND ISNULL(LTRIM(RTRIM(sd.couponcode)), '') <> ''");
        }
    }

    void BuildInstallmentCouponSelect(StringBuilder sql, string installmentAlias, string detailAlias, bool excludeInstNoOne)
    {
        // Coupon lives on SavingAccountDetail; match installment by order + user.
        sql.Append(@"
SELECT ").Append(detailAlias).Append(@".couponcode,
    ").Append(installmentAlias).Append(@".approvedate,
    ").Append(installmentAlias).Append(@".amount,
    1 AS quantity,
    ").Append(installmentAlias).Append(@".status,
    pm.productname,
    ud.username,
    ud.userid,
    ud.mobile
FROM SavingAccountInstallmentDetail ").Append(installmentAlias).Append(@" WITH (NOLOCK)
OUTER APPLY (
    SELECT TOP 1
        sd0.couponcode,
        sd0.productid,
        sd0.userid
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE sd0.orderid = ").Append(installmentAlias).Append(@".orderid
      AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(").Append(installmentAlias).Append(@".UserId))
    ORDER BY
        CASE WHEN sd0.productid = ").Append(installmentAlias).Append(@".productid THEN 0 ELSE 1 END,
        sd0.id ASC
) ").Append(detailAlias).Append(@"
LEFT JOIN savingproductmaster pm WITH (NOLOCK)
    ON COALESCE(NULLIF(").Append(installmentAlias).Append(@".productid, 0), ").Append(detailAlias).Append(@".productid) = pm.id
LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = ").Append(installmentAlias).Append(@".userid
WHERE ").Append(GetApprovedStatusFilter(installmentAlias)).Append(@"
  AND ISNULL(LTRIM(RTRIM(").Append(detailAlias).Append(@".couponcode)), '') <> ''");

        if (excludeInstNoOne)
        {
            sql.Append(" AND ISNULL(").Append(installmentAlias).Append(".InstNo, 0) <> 1");
        }
    }

    void AppendCommonSearchFilters(StringBuilder sql, string userSourceAlias, string couponAlias, string userDetailAlias)
    {
        if (!string.IsNullOrWhiteSpace(txtCouponCode.Text))
        {
            sql.Append(" AND ").Append(couponAlias).Append(".couponcode LIKE '%")
                .Append(SqlEscape(txtCouponCode.Text.Trim())).Append("%'");
        }

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            string userSearch = SqlEscape(txtUserId.Text.Trim());
            sql.Append(" AND (").Append(userSourceAlias).Append(".userid LIKE '%").Append(userSearch)
                .Append("%' OR ").Append(userDetailAlias).Append(".username LIKE '%").Append(userSearch).Append("%')");
        }

        if (!string.IsNullOrWhiteSpace(txtMobile.Text))
        {
            sql.Append(" AND ").Append(userDetailAlias).Append(".mobile LIKE '%")
                .Append(SqlEscape(txtMobile.Text.Trim())).Append("%'");
        }
    }

    /// <summary>
    /// Applies Approve Month + optional from/to date filters.
    /// Super Draw window = day 1–10 of the selected month (or day 1–10 of any month if month not selected).
    /// Mega / default = full month (day 1–last day) when month is selected.
    /// </summary>
    void AppendApproveDateRangeFilter(StringBuilder sql, string approveDateExpression, bool useSuperDrawWindow)
    {
        DateTime? monthStart;
        DateTime? monthEnd;
        TryGetSelectedApproveMonthRange(out monthStart, out monthEnd);

        if (useSuperDrawWindow)
        {
            if (monthStart.HasValue)
            {
                // Selected month: 1st to 10th
                DateTime superEnd = monthStart.Value.AddDays(9);
                sql.Append(" AND CONVERT(date, ").Append(approveDateExpression).Append(") >= CONVERT(date,'")
                    .Append(monthStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .Append("') AND CONVERT(date, ").Append(approveDateExpression).Append(") <= CONVERT(date,'")
                    .Append(superEnd.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .Append("')");
            }
            else
            {
                // All months: only day 1–10
                sql.Append(" AND DAY(CONVERT(date, ").Append(approveDateExpression).Append(")) BETWEEN 1 AND 10");
            }
        }
        else if (monthStart.HasValue && monthEnd.HasValue)
        {
            // Full month: 1st to last day
            sql.Append(" AND CONVERT(date, ").Append(approveDateExpression).Append(") >= CONVERT(date,'")
                .Append(monthStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Append("') AND CONVERT(date, ").Append(approveDateExpression).Append(") <= CONVERT(date,'")
                .Append(monthEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Append("')");
        }

        if (!string.IsNullOrWhiteSpace(txtFromDate.Text) && !string.IsNullOrWhiteSpace(txtToDate.Text))
        {
            sql.Append(" AND CONVERT(date, ").Append(approveDateExpression).Append(") >= CONVERT(date,'")
                .Append(Message.GetIndianDate(txtFromDate.Text))
                .Append("') AND CONVERT(date, ").Append(approveDateExpression).Append(") <= CONVERT(date,'")
                .Append(Message.GetIndianDate(txtToDate.Text))
                .Append("')");
        }
        else if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
        {
            sql.Append(" AND CONVERT(date, ").Append(approveDateExpression).Append(") >= CONVERT(date,'")
                .Append(Message.GetIndianDate(txtFromDate.Text))
                .Append("')");
        }
        else if (!string.IsNullOrWhiteSpace(txtToDate.Text))
        {
            sql.Append(" AND CONVERT(date, ").Append(approveDateExpression).Append(") <= CONVERT(date,'")
                .Append(Message.GetIndianDate(txtToDate.Text))
                .Append("')");
        }
    }

    bool TryGetSelectedApproveMonthRange(out DateTime? monthStart, out DateTime? monthEnd)
    {
        monthStart = null;
        monthEnd = null;

        string monthValue = (ddlApproveMonth.SelectedValue ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(monthValue))
        {
            return false;
        }

        DateTime start;
        if (!DateTime.TryParseExact(monthValue + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
        {
            return false;
        }

        monthStart = start;
        monthEnd = start.AddMonths(1).AddDays(-1);
        return true;
    }

    static string GetApprovedStatusFilter(string tableAlias)
    {
        return "(" + tableAlias + ".status = 'Approved'"
            + " OR " + tableAlias + ".status = '1'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + tableAlias + ".status, '')))) IN ('approved', 'approve'))";
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static DataTable BuildPrintTickets(DataTable source)
    {
        DataTable tickets = CreateTicketTable();
        if (source == null || source.Rows.Count == 0)
        {
            return tickets;
        }

        int ticketNo = 1;
        foreach (DataRow row in source.Rows)
        {
            int copies = GetQuantity(row);
            for (int i = 0; i < copies; i++)
            {
                DataRow ticket = tickets.NewRow();
                ticket["TicketNo"] = ticketNo++;
                ticket["couponcode"] = GetValue(row, "couponcode");
                ticket["username"] = GetValue(row, "username");
                ticket["userid"] = GetValue(row, "userid");
                ticket["mobile"] = GetValue(row, "mobile");
                ticket["productname"] = GetValue(row, "productname");
                ticket["amount"] = GetValue(row, "amount");
                ticket["approvedate"] = FormatApproveDate(row);
                ticket["copyLabel"] = copies > 1 ? "Copy " + (i + 1) + " of " + copies : string.Empty;
                tickets.Rows.Add(ticket);
            }
        }

        return tickets;
    }

    static DataTable CreateTicketTable()
    {
        DataTable tickets = new DataTable();
        tickets.Columns.Add("TicketNo", typeof(int));
        tickets.Columns.Add("couponcode", typeof(string));
        tickets.Columns.Add("username", typeof(string));
        tickets.Columns.Add("userid", typeof(string));
        tickets.Columns.Add("mobile", typeof(string));
        tickets.Columns.Add("productname", typeof(string));
        tickets.Columns.Add("amount", typeof(string));
        tickets.Columns.Add("approvedate", typeof(string));
        tickets.Columns.Add("copyLabel", typeof(string));
        return tickets;
    }

    static int GetQuantity(DataRow row)
    {
        int quantity = 1;
        if (row != null && row.Table.Columns.Contains("quantity") && row["quantity"] != DBNull.Value)
        {
            int.TryParse(Convert.ToString(row["quantity"]), out quantity);
        }

        return quantity > 0 ? quantity : 1;
    }

    static string FormatApproveDate(DataRow row)
    {
        if (row == null || !row.Table.Columns.Contains("approvedate") || row["approvedate"] == DBNull.Value)
        {
            return "-";
        }

        DateTime approveDate;
        if (DateTime.TryParse(Convert.ToString(row["approvedate"]), out approveDate))
        {
            return approveDate.ToString("dd MMM yyyy");
        }

        return Convert.ToString(row["approvedate"]);
    }

    static string GetValue(DataRow row, string columnName)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (DataColumn column in row.Table.Columns)
        {
            if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase)
                && row[column] != DBNull.Value)
            {
                return Convert.ToString(row[column]).Trim();
            }
        }

        return string.Empty;
    }
}

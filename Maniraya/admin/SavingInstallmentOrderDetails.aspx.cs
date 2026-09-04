using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SavingInstallmentOrderDetails : Page
{
    Data ObjData = new Data();

    DataTable OrderData
    {
        get { return ViewState["OrderData"] as DataTable; }
        set { ViewState["OrderData"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureInstallmentDeliveryColumns();
        SavingProductHelper.EnsureInstallmentProductAssignTable();

        if (!IsPostBack)
        {
            SavingProductHelper.EnsureBulkColumns();
            SavingProductHelper.ProcessBulkSavingSchedule();
            LoadOrders();
        }
        else if (OrderData != null)
        {
            BindGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadOrders();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOrderId.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddDeliveryStatus.SelectedIndex = 0;
        GridView1.PageIndex = 0;
        LoadOrders();
    }

    void LoadOrders()
    {
        OrderData = BuildProductRows(GetConfirmedOrders());
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = OrderData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
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

        DataTable dt = OrderData;
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
        BindGrid();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (OrderData != null)
        {
            BindGrid();
        }
    }

    DataTable GetConfirmedOrders()
    {
        DataTable dt = new DataTable();
        try
        {
            bool hasDeliveryStatus = SavingProductHelper.HasInstallmentDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT
    sa.id,
    sa.orderid,
    sa.userid,
    sa.InstNo,
    ud.username,
    ud.mobile,
    ").Append(AssignedProductNameSql()).Append(@" AS productname,
    sa.amount,
    sa.approvedate,
    sd.couponcode,
    ").Append(deliveryStatusSelect).Append(@",
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.Shippingaddress
        ELSE ud.Address
    END AS ShipAddress,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingAreaName
        ELSE ud.AreaName
    END AS ShipArea,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN CS.CityName
        ELSE C.CityName
    END AS ShipCity,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN SS.StateName
        ELSE S.StateName
    END AS ShipState,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingPincode
        ELSE ud.Pincode
    END AS ShipPincode
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
OUTER APPLY (
    SELECT TOP 1
        sd0.couponcode
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE sd0.orderid = sa.orderid
      AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
    ORDER BY sd0.id ASC
) sd
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
").Append(AssignedProductJoinSql("sa.InstNo")).Append(@"
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE ").Append(GetApprovedStatusFilter("sa"));

            if (!string.IsNullOrWhiteSpace(txtOrderId.Text))
            {
                sql.Append(" AND sa.orderid LIKE '%").Append(SqlEscape(txtOrderId.Text.Trim())).Append("%'");
            }

            if (!string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                string userSearch = SqlEscape(txtUserId.Text.Trim());
                sql.Append(" AND (sa.userid LIKE '%").Append(userSearch)
                    .Append("%' OR ud.username LIKE '%").Append(userSearch).Append("%')");
            }

            if (hasDeliveryStatus && !string.IsNullOrWhiteSpace(ddDeliveryStatus.SelectedValue))
            {
                sql.Append(" AND ISNULL(NULLIF(LTRIM(RTRIM(sa.DeliveryStatus)), ''), 'Confirmed') = '")
                    .Append(SqlEscape(ddDeliveryStatus.SelectedValue)).Append("'");
            }

            if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
            {
                sql.Append(" AND CONVERT(date, sa.requestdate) >= CONVERT(date, '")
                    .Append(Message.GetIndianDate(txtFromDate.Text.Trim()).ToString("yyyy-MM-dd")).Append("')");
            }

            if (!string.IsNullOrWhiteSpace(txtToDate.Text))
            {
                sql.Append(" AND CONVERT(date, sa.requestdate) <= CONVERT(date, '")
                    .Append(Message.GetIndianDate(txtToDate.Text.Trim()).ToString("yyyy-MM-dd")).Append("')");
            }

            sql.Append(" ORDER BY sa.id DESC");

            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql.ToString());
            }
            finally
            {
                ObjData.EndConnection();
            }

            if (dt != null && !dt.Columns.Contains("AddressSummary"))
            {
                dt.Columns.Add("AddressSummary", typeof(string));
            }

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["AddressSummary"] = BuildAddressSummary(row);
                }
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    DataTable BuildProductRows(DataTable source)
    {
        DataTable grouped = new DataTable();
        grouped.Columns.Add("id", typeof(int));
        grouped.Columns.Add("orderid", typeof(string));
        grouped.Columns.Add("userid", typeof(string));
        grouped.Columns.Add("username", typeof(string));
        grouped.Columns.Add("mobile", typeof(string));
        grouped.Columns.Add("productsummary", typeof(string));
        grouped.Columns.Add("productcount", typeof(int));
        grouped.Columns.Add("amount", typeof(decimal));
        grouped.Columns.Add("approvedate", typeof(DateTime));
        grouped.Columns.Add("DeliveryStatus", typeof(string));
        grouped.Columns.Add("AddressSummary", typeof(string));
        grouped.Columns.Add("ShipAddress", typeof(string));
        grouped.Columns.Add("ShipArea", typeof(string));
        grouped.Columns.Add("ShipCity", typeof(string));
        grouped.Columns.Add("ShipState", typeof(string));
        grouped.Columns.Add("ShipPincode", typeof(string));

        if (source == null || source.Rows.Count == 0)
        {
            return grouped;
        }

        var orderGroups = source.AsEnumerable()
            .GroupBy(row => Convert.ToString(row["orderid"]).Trim(), StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Max(row => Convert.ToInt32(row["id"])));

        foreach (var orderGroup in orderGroups)
        {
            if (string.IsNullOrWhiteSpace(orderGroup.Key))
            {
                continue;
            }

            foreach (DataRow row in orderGroup
                .OrderBy(r => GetIntValue(r, "InstNo", "instno"))
                .ThenBy(r => Convert.ToInt32(r["id"])))
            {
                DataRow groupedRow = grouped.NewRow();
                groupedRow["id"] = Convert.ToInt32(row["id"]);
                groupedRow["orderid"] = orderGroup.Key;
                groupedRow["userid"] = row["userid"];
                groupedRow["username"] = row["username"];
                groupedRow["mobile"] = row["mobile"];
                groupedRow["productsummary"] = BuildSingleProductHtml(row);
                groupedRow["productcount"] = 1;
                groupedRow["amount"] = GetDecimalValue(row["amount"]);
                groupedRow["approvedate"] = GetDateValue(row["approvedate"]);
                groupedRow["DeliveryStatus"] = string.IsNullOrWhiteSpace(Convert.ToString(row["DeliveryStatus"]))
                    ? "Confirmed"
                    : Convert.ToString(row["DeliveryStatus"]).Trim();
                groupedRow["AddressSummary"] = Convert.ToString(row["AddressSummary"]);
                groupedRow["ShipAddress"] = row["ShipAddress"];
                groupedRow["ShipArea"] = row["ShipArea"];
                groupedRow["ShipCity"] = row["ShipCity"];
                groupedRow["ShipState"] = row["ShipState"];
                groupedRow["ShipPincode"] = row["ShipPincode"];
                grouped.Rows.Add(groupedRow);
            }
        }

        return grouped;
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

    static decimal GetDecimalValue(object value)
    {
        decimal parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }

        return decimal.TryParse(Convert.ToString(value), out parsed) ? parsed : 0m;
    }

    static int GetIntValue(DataRow row, params string[] columnNames)
    {
        if (row == null || columnNames == null)
        {
            return 0;
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName) || !row.Table.Columns.Contains(columnName))
            {
                continue;
            }

            object value = row[columnName];
            int parsed;
            if (value != null && value != DBNull.Value && int.TryParse(Convert.ToString(value), out parsed))
            {
                return parsed;
            }
        }

        return 0;
    }

    static string GetRowDeliveryStatus(DataRow row)
    {
        if (row == null)
        {
            return "Confirmed";
        }

        string status = (Convert.ToString(row["DeliveryStatus"]) ?? "Confirmed").Trim();
        return string.IsNullOrWhiteSpace(status) ? "Confirmed" : status;
    }

    static string AssignedProductNameSql()
    {
        return @"CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END";
    }

    static string AssignedProductJoinSql(string instNoExpr)
    {
        return @"
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ISNULL(ipa.ProductId, 0) > 0
   AND ipa.InstallmentNo = TRY_CONVERT(INT, " + instNoExpr + @")
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId";
    }

    static string GetAssignedProductName(object value)
    {
        string productName = Convert.ToString(value);
        return string.IsNullOrWhiteSpace(productName) ? "Not assigned" : productName.Trim();
    }

    static bool IsUnassignedProduct(string productName)
    {
        return string.IsNullOrWhiteSpace(productName)
            || productName.Equals("Not assigned", StringComparison.OrdinalIgnoreCase)
            || productName.Equals("Not assign", StringComparison.OrdinalIgnoreCase);
    }

    static string BuildSingleProductHtml(DataRow row)
    {
        StringBuilder sb = new StringBuilder();
        string productName = GetAssignedProductName(row["productname"]);
        bool unassigned = IsUnassignedProduct(productName);
        int instNo = GetIntValue(row, "InstNo", "instno");
        if (instNo > 0)
        {
            productName = productName + " · Inst #" + instNo;
        }

        sb.Append("<div class=\"saving-order-products\">");
        sb.Append("<div class=\"saving-order-product-item")
            .Append(unassigned ? " is-unassigned" : string.Empty)
            .Append("\"><span>")
            .Append(HttpUtility.HtmlEncode(productName))
            .Append("</span><strong>")
            .Append(HttpUtility.HtmlEncode(Convert.ToString(row["amount"])))
            .Append("</strong></div>");
        sb.Append("</div>");
        return sb.ToString();
    }

    static string BuildModalProductsHtml(DataRow[] rows)
    {
        if (rows == null || rows.Length == 0)
        {
            return "<p class=\"text-muted\">No installments found for this order.</p>";
        }

        StringBuilder sb = new StringBuilder("<ul class=\"saving-order-modal-products\">");
        foreach (DataRow row in rows.OrderBy(r => GetAssignedProductName(r["productname"])))
        {
            string productName = GetAssignedProductName(row["productname"]);
            sb.Append("<li")
                .Append(IsUnassignedProduct(productName) ? " class=\"is-unassigned\"" : string.Empty)
                .Append("><span>")
                .Append(HttpUtility.HtmlEncode(productName))
                .Append("</span><strong>")
                .Append(HttpUtility.HtmlEncode(Convert.ToString(row["amount"])))
                .Append("</strong></li>");
        }

        sb.Append("</ul>");
        return sb.ToString();
    }

    static string GetApprovedStatusFilter(string tableAlias)
    {
        return "(" + tableAlias + ".status = 'Approved'"
            + " OR " + tableAlias + ".status = '1'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + tableAlias + ".status, '')))) IN ('approved', 'approve'))";
    }

    DataRow GetInstallmentRow(string installmentId)
    {
        DataTable dt = GetInstallmentById(installmentId);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }

        return null;
    }

    DataRow GetOrderRow(string orderId)
    {
        DataTable dt = GetOrdersByOrderId(orderId);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }

        return null;
    }

    DataRow[] GetOrderRows(string orderId)
    {
        DataTable dt = GetOrdersByOrderId(orderId);
        if (dt == null || dt.Rows.Count == 0)
        {
            return new DataRow[0];
        }

        return dt.AsEnumerable().ToArray();
    }

    DataTable GetInstallmentById(string installmentId)
    {
        DataTable dt = new DataTable();
        int id;
        if (!int.TryParse(installmentId, out id) || id <= 0)
        {
            return dt;
        }

        try
        {
            bool hasDeliveryStatus = SavingProductHelper.HasInstallmentDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";
            string consignmentSelect = SavingProductHelper.HasInstallmentConsignmentColumn()
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.ConsignmentNumber)), ''), '') AS ConsignmentNumber"
                : "'' AS ConsignmentNumber";

            string sql = @"
SELECT
    sa.id,
    sa.orderid,
    sa.userid,
    ud.username,
    ud.mobile,
    " + AssignedProductNameSql() + @" AS productname,
    sa.amount,
    sa.approvedate,
    sd.couponcode,
    " + deliveryStatusSelect + @",
    " + consignmentSelect + @"
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
OUTER APPLY (
    SELECT TOP 1
        sd0.couponcode
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE sd0.orderid = sa.orderid
      AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
    ORDER BY sd0.id ASC
) sd
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
" + AssignedProductJoinSql("sa.InstNo") + @"
WHERE sa.id = " + id + @"
  AND " + GetApprovedStatusFilter("sa");

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

    DataTable GetOrdersByOrderId(string orderId)
    {
        DataTable dt = new DataTable();
        try
        {
            bool hasDeliveryStatus = SavingProductHelper.HasInstallmentDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";

            string sql = @"
SELECT
    sa.id,
    sa.orderid,
    sa.userid,
    ud.username,
    ud.mobile,
    " + AssignedProductNameSql() + @" AS productname,
    sa.amount,
    sa.approvedate,
    sd.couponcode,
    " + deliveryStatusSelect + @",
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.Shippingaddress
        ELSE ud.Address
    END AS ShipAddress,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingAreaName
        ELSE ud.AreaName
    END AS ShipArea,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN CS.CityName
        ELSE C.CityName
    END AS ShipCity,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN SS.StateName
        ELSE S.StateName
    END AS ShipState,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingPincode
        ELSE ud.Pincode
    END AS ShipPincode
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
OUTER APPLY (
    SELECT TOP 1
        sd0.couponcode
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE sd0.orderid = sa.orderid
      AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
    ORDER BY sd0.id ASC
) sd
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
" + AssignedProductJoinSql("sa.InstNo") + @"
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE sa.orderid = '" + SqlEscape(orderId) + @"'
  AND " + GetApprovedStatusFilter("sa") + @"
ORDER BY sa.id";

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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string argument = Convert.ToString(e.CommandArgument).Trim();
        if (string.IsNullOrWhiteSpace(argument))
        {
            return;
        }

        if (e.CommandName == "printaddress")
        {
            BindPrintModal(argument);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openPrintModal", "showAdminModal('addressPrintModal');", true);
            return;
        }

        if (e.CommandName == "updatestatus")
        {
            BindStatusModal(argument);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openStatusModal", "showAdminModal('statusUpdateModal'); setTimeout(function(){ if (window.toggleConsignmentField) { window.toggleConsignmentField(); } }, 0);", true);
        }
    }

    void BindPrintModal(string orderId)
    {
        DataRow row = GetOrderRow(orderId);
        DataRow[] rows = GetOrderRows(orderId);
        if (row == null)
        {
            litPrintAddress.Text = "<p>No address found for this order.</p>";
            return;
        }

        string userName = Convert.ToString(row["username"]);
        string userId = Convert.ToString(row["userid"]);
        string mobile = Convert.ToString(row["mobile"]);
        string addressBlock = Server.HtmlEncode(FormatAddressBlock(row));
        string productsBlock = BuildModalProductsHtml(rows);

        litPrintAddress.Text =
            "<div class=\"saving-address-print-brand\">" +
            "<h4>Maniraya Shipping Label</h4>" +
            "<div class=\"saving-address-print-meta\">Order: " + Server.HtmlEncode(orderId) + "<br/>Date: " + DateTime.Now.ToString("dd MMM yyyy") + "</div>" +
            "</div>" +
            "<div class=\"saving-address-print-box\">" +
            "<p class=\"saving-address-print-label\">Deliver To</p>" +
            "<p class=\"saving-address-print-name\">" + Server.HtmlEncode(userName) + "</p>" +
            "<p class=\"saving-address-print-lines\">" + addressBlock + "</p>" +
            "</div>" +
            "<div class=\"saving-address-print-footer\">" +
            "User ID: " + Server.HtmlEncode(userId) +
            (string.IsNullOrWhiteSpace(mobile) ? string.Empty : " | Mobile: " + Server.HtmlEncode(mobile)) +
            "</div>" +
            "<div class=\"saving-address-print-footer\">Installments:" + productsBlock + "</div>";
    }

    void BindStatusModal(string installmentId)
    {
        DataRow row = GetInstallmentRow(installmentId);
        if (row == null)
        {
            return;
        }

        hfInstallmentId.Value = Convert.ToString(row["id"]);
        hfOrderId.Value = Convert.ToString(row["orderid"]);
        txtModalOrderId.Text = Convert.ToString(row["orderid"]);
        txtModalUserName.Text = Convert.ToString(row["username"]);
        litModalProducts.Text = BuildModalProductsHtml(new[] { row });

        string currentStatus = GetRowDeliveryStatus(row);
        ListItem statusItem = ddModalDeliveryStatus.Items.FindByValue(currentStatus);
        if (statusItem != null)
        {
            ddModalDeliveryStatus.ClearSelection();
            statusItem.Selected = true;
        }

        txtConsignmentNumber.Text = row.Table.Columns.Contains("ConsignmentNumber")
            ? Convert.ToString(row["ConsignmentNumber"])
            : string.Empty;

        SetConsignmentPanelVisibility(currentStatus);
    }

    void SetConsignmentPanelVisibility(string status)
    {
        if (IsOutForDeliveryStatus(status))
        {
            pnlConsignment.CssClass = "form-group saving-consignment-group is-open";
            pnlConsignment.Style["display"] = "block";
        }
        else
        {
            pnlConsignment.CssClass = "form-group saving-consignment-group";
            pnlConsignment.Style["display"] = "none";
        }
    }

    protected void btnSaveDeliveryStatus_Click(object sender, EventArgs e)
    {
        string installmentId = (hfInstallmentId.Value ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(installmentId))
        {
            Message.Show("Invalid product selected.");
            return;
        }

        string newStatus = ddModalDeliveryStatus.SelectedValue;
        if (string.IsNullOrWhiteSpace(newStatus))
        {
            Message.Show("Select delivery status.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModal", "showAdminModal('statusUpdateModal'); setTimeout(function(){ if (window.toggleConsignmentField) { window.toggleConsignmentField(); } }, 0);", true);
            return;
        }

        string consignmentNumber = (txtConsignmentNumber.Text ?? string.Empty).Trim();
        if (IsOutForDeliveryStatus(newStatus) && string.IsNullOrWhiteSpace(consignmentNumber))
        {
            Message.Show("Enter consignment number for out for delivery order.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModalConsignment", "showAdminModal('statusUpdateModal'); setTimeout(function(){ if (window.toggleConsignmentField) { window.toggleConsignmentField(); } }, 0);", true);
            return;
        }

        if (UpdateDeliveryStatusByInstallmentId(installmentId, newStatus, consignmentNumber, Session["useradmin"].ToString()))
        {
            Message.Show("Delivery status updated for this product.");
            LoadOrders();
        }
        else
        {
            Message.Show("Unable to update delivery status.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModalFail", "showAdminModal('statusUpdateModal'); setTimeout(function(){ if (window.toggleConsignmentField) { window.toggleConsignmentField(); } }, 0);", true);
        }
    }

    bool UpdateDeliveryStatusByInstallmentId(string installmentId, string deliveryStatus, string consignmentNumber, string updatedBy)
    {
        int id;
        if (!int.TryParse(installmentId, out id) || id <= 0)
        {
            return false;
        }

        if (!SavingProductHelper.HasInstallmentDeliveryStatusColumn())
        {
            SavingProductHelper.EnsureInstallmentDeliveryColumns();
        }

        try
        {
            ObjData.StartConnection();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE SavingAccountInstallmentDetail SET DeliveryStatus='")
                    .Append(SqlEscape(deliveryStatus))
                    .Append("', DeliveryStatusUpdatedOn=GETDATE(), DeliveryStatusUpdatedBy='")
                    .Append(SqlEscape(updatedBy))
                    .Append("'");

                if (SavingProductHelper.HasInstallmentConsignmentColumn() && IsOutForDeliveryStatus(deliveryStatus))
                {
                    sql.Append(", ConsignmentNumber='").Append(SqlEscape(consignmentNumber)).Append("'");
                }

                sql.Append(" WHERE id=").Append(id)
                    .Append(" AND ").Append(GetApprovedStatusFilter("SavingAccountInstallmentDetail"));
                ObjData.RunInsUpDelQuery(sql.ToString());
                return true;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    static bool IsOutForDeliveryStatus(string status)
    {
        string value = (status ?? string.Empty).Trim();
        return string.Equals(value, "Out for Delivery", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "Out of Delivery", StringComparison.OrdinalIgnoreCase);
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblDeliveryStatus = (Label)e.Row.FindControl("lblDeliveryStatus");
        Literal litProducts = (Literal)e.Row.FindControl("litProducts");
        if (litProducts != null)
        {
            litProducts.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "productsummary"));
        }

        if (lblDeliveryStatus == null)
        {
            return;
        }

        string status = lblDeliveryStatus.Text.Trim();
        lblDeliveryStatus.CssClass = "admin-delivery-badge " + GetDeliveryBadgeClass(status);

        bool isDelivered = string.Equals(status, "Delivered", StringComparison.OrdinalIgnoreCase);

        LinkButton btnUpdateStatus = (LinkButton)e.Row.FindControl("btnUpdateStatus");
        if (btnUpdateStatus != null)
        {
            btnUpdateStatus.Visible = !isDelivered;
        }

        LinkButton btnPrintAddress = (LinkButton)e.Row.FindControl("btnPrintAddress");
        if (btnPrintAddress != null)
        {
            btnPrintAddress.Visible = !isDelivered;
        }
    }

    static string GetDeliveryBadgeClass(string status)
    {
        switch ((status ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "shipped":
                return "delivery-badge-shipped";
            case "out for delivery":
                return "delivery-badge-out";
            case "delivered":
                return "delivery-badge-delivered";
            default:
                return "delivery-badge-confirmed";
        }
    }

    static string BuildAddressSummary(DataRow row)
    {
        string summary = FormatAddressBlock(row);
        if (string.IsNullOrWhiteSpace(summary))
        {
            return "Address not available";
        }

        if (summary.Length <= 90)
        {
            return summary.Replace("\n", ", ");
        }

        return summary.Replace("\n", ", ").Substring(0, 87) + "...";
    }

    static string FormatAddressBlock(DataRow row)
    {
        if (row == null)
        {
            return string.Empty;
        }

        StringBuilder sb = new StringBuilder();
        AppendLine(sb, Convert.ToString(row["ShipAddress"]));
        AppendLine(sb, Convert.ToString(row["ShipArea"]));

        string city = Convert.ToString(row["ShipCity"]);
        string state = Convert.ToString(row["ShipState"]);
        string pincode = Convert.ToString(row["ShipPincode"]);
        string location = JoinParts(city, state);
        if (!string.IsNullOrWhiteSpace(pincode))
        {
            location = string.IsNullOrWhiteSpace(location) ? pincode : location + " - " + pincode;
        }

        AppendLine(sb, location);
        return sb.ToString().Trim();
    }

    static void AppendLine(StringBuilder sb, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (sb.Length > 0)
        {
            sb.AppendLine();
        }

        sb.Append(value.Trim());
    }

    static string JoinParts(string first, string second)
    {
        if (string.IsNullOrWhiteSpace(first))
        {
            return second ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(second))
        {
            return first;
        }

        return first.Trim() + ", " + second.Trim();
    }
}

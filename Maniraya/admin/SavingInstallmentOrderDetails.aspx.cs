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

        if (!IsPostBack)
        {
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
        OrderData = GroupOrdersByOrderId(GetConfirmedOrders());
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

        for (int i = 0; i < totalPages; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

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
        link.Text = text;
        link.CssClass = "admin-pager-btn";
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
    ud.username,
    ud.mobile,
    pm.productname,
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
LEFT JOIN SavingAccountDetail sd WITH (NOLOCK) ON sa.OrderId = sd.orderid
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE ").Append(GetApprovedStatusFilter("sa")).Append(" AND sa.InstNo > 1");

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

    DataTable GroupOrdersByOrderId(DataTable source)
    {
        DataTable grouped = new DataTable();
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

        foreach (var group in orderGroups)
        {
            if (string.IsNullOrWhiteSpace(group.Key))
            {
                continue;
            }

            DataRow first = group
                .OrderByDescending(row => Convert.ToInt32(row["id"]))
                .First();

            DataRow groupedRow = grouped.NewRow();
            groupedRow["orderid"] = group.Key;
            groupedRow["userid"] = first["userid"];
            groupedRow["username"] = first["username"];
            groupedRow["mobile"] = first["mobile"];
            groupedRow["productsummary"] = BuildProductSummaryHtml(group);
            groupedRow["productcount"] = group.Count();
            groupedRow["amount"] = group.Sum(row => GetDecimalValue(row["amount"]));
            groupedRow["approvedate"] = group.Max(row => GetDateValue(row["approvedate"]));
            groupedRow["DeliveryStatus"] = GetGroupDeliveryStatus(group);
            groupedRow["AddressSummary"] = Convert.ToString(first["AddressSummary"]);
            groupedRow["ShipAddress"] = first["ShipAddress"];
            groupedRow["ShipArea"] = first["ShipArea"];
            groupedRow["ShipCity"] = first["ShipCity"];
            groupedRow["ShipState"] = first["ShipState"];
            groupedRow["ShipPincode"] = first["ShipPincode"];
            grouped.Rows.Add(groupedRow);
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

    static string GetGroupDeliveryStatus(IGrouping<string, DataRow> group)
    {
        var statuses = group
            .Select(row => (Convert.ToString(row["DeliveryStatus"]) ?? "Confirmed").Trim())
            .Where(status => !string.IsNullOrWhiteSpace(status))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (statuses.Count == 0)
        {
            return "Confirmed";
        }

        return statuses[0];
    }

    static string BuildProductSummaryHtml(IGrouping<string, DataRow> group)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"saving-order-products\">");

        foreach (DataRow row in group.OrderBy(r => Convert.ToString(r["productname"])))
        {
            sb.Append("<div class=\"saving-order-product-item\"><span>")
                .Append(HttpUtility.HtmlEncode(Convert.ToString(row["productname"])))
                .Append("</span><strong>")
                .Append(HttpUtility.HtmlEncode(Convert.ToString(row["amount"])))
                .Append("</strong></div>");
        }

        if (group.Count() > 1)
        {
            sb.Append("<span class=\"saving-order-product-count\"><i class=\"fa fa-cubes\"></i> ")
                .Append(group.Count())
                .Append(" installments</span>");
        }

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
        foreach (DataRow row in rows.OrderBy(r => Convert.ToString(r["productname"])))
        {
            sb.Append("<li><span>")
                .Append(HttpUtility.HtmlEncode(Convert.ToString(row["productname"])))
                .Append("</span><strong>")
                .Append(HttpUtility.HtmlEncode(Convert.ToString(row["amount"])))
                .Append("</strong></li>");
        }

        sb.Append("</ul>");
        return sb.ToString();
    }

    static string GetGroupDeliveryStatus(DataRow[] rows)
    {
        if (rows == null || rows.Length == 0)
        {
            return "Confirmed";
        }

        var statuses = rows
            .Select(row => (Convert.ToString(row["DeliveryStatus"]) ?? "Confirmed").Trim())
            .Where(status => !string.IsNullOrWhiteSpace(status))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return statuses.Count == 0 ? "Confirmed" : statuses[0];
    }

    static string GetApprovedStatusFilter(string tableAlias)
    {
        return "(" + tableAlias + ".status = 'Approved'"
            + " OR " + tableAlias + ".status = '1'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + tableAlias + ".status, '')))) IN ('approved', 'approve'))";
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
    pm.productname,
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
LEFT JOIN SavingAccountDetail sd WITH (NOLOCK) ON sa.OrderId = sd.orderid
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE sa.orderid = '" + SqlEscape(orderId) + @"'
  AND " + GetApprovedStatusFilter("sa") + @"
  AND sa.InstNo > 1
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
        string orderId = Convert.ToString(e.CommandArgument).Trim();
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return;
        }

        if (e.CommandName == "printaddress")
        {
            BindPrintModal(orderId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openPrintModal", "showAdminModal('addressPrintModal');", true);
            return;
        }

        if (e.CommandName == "updatestatus")
        {
            BindStatusModal(orderId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openStatusModal", "showAdminModal('statusUpdateModal');", true);
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

    void BindStatusModal(string orderId)
    {
        DataRow row = GetOrderRow(orderId);
        DataRow[] rows = GetOrderRows(orderId);
        if (row == null)
        {
            return;
        }

        hfOrderId.Value = orderId;
        txtModalOrderId.Text = orderId;
        txtModalUserName.Text = Convert.ToString(row["username"]);
        litModalProducts.Text = BuildModalProductsHtml(rows);

        string currentStatus = GetGroupDeliveryStatus(rows);
        ListItem statusItem = ddModalDeliveryStatus.Items.FindByValue(currentStatus);
        if (statusItem != null)
        {
            ddModalDeliveryStatus.ClearSelection();
            statusItem.Selected = true;
        }
    }

    protected void btnSaveDeliveryStatus_Click(object sender, EventArgs e)
    {
        string orderId = (hfOrderId.Value ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(orderId))
        {
            Message.Show("Invalid order selected.");
            return;
        }

        string newStatus = ddModalDeliveryStatus.SelectedValue;
        if (string.IsNullOrWhiteSpace(newStatus))
        {
            Message.Show("Select delivery status.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModal", "showAdminModal('statusUpdateModal');", true);
            return;
        }

        if (UpdateDeliveryStatusByOrderId(orderId, newStatus, Session["useradmin"].ToString()))
        {
            Message.Show("Delivery status updated for all installments in this order.");
            LoadOrders();
        }
        else
        {
            Message.Show("Unable to update delivery status.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModalFail", "showAdminModal('statusUpdateModal');", true);
        }
    }

    bool UpdateDeliveryStatusByOrderId(string orderId, string deliveryStatus, string updatedBy)
    {
        if (!SavingProductHelper.HasInstallmentDeliveryStatusColumn())
        {
            SavingProductHelper.EnsureInstallmentDeliveryColumns();
        }

        try
        {
            ObjData.StartConnection();
            try
            {
                string sql = "UPDATE SavingAccountInstallmentDetail SET DeliveryStatus='" + SqlEscape(deliveryStatus)
                    + "', DeliveryStatusUpdatedOn=GETDATE(), DeliveryStatusUpdatedBy='" + SqlEscape(updatedBy)
                    + "' WHERE orderid='" + SqlEscape(orderId) + "' AND " + GetApprovedStatusFilter("SavingAccountInstallmentDetail")
                    + " AND InstNo > 1";
                ObjData.RunInsUpDelQuery(sql);
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

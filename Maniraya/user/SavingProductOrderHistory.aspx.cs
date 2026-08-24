using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingProductOrderHistory : Page
{
    Data ObjData = new Data();

    DataTable OrderData
    {
        get { return ViewState["OrderData"] as DataTable; }
        set { ViewState["OrderData"] = value; }
    }

    int PageIndex
    {
        get { return ViewState["SOHPageIndex"] != null ? (int)ViewState["SOHPageIndex"] : 0; }
        set { ViewState["SOHPageIndex"] = value; }
    }

    bool PagingEnabled
    {
        get { return ViewState["SOHPagingEnabled"] != null && (bool)ViewState["SOHPagingEnabled"]; }
        set { ViewState["SOHPagingEnabled"] = value; }
    }

    int ActivePageSize
    {
        get { return ViewState["SOHPageSize"] != null ? (int)ViewState["SOHPageSize"] : 10; }
        set { ViewState["SOHPageSize"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureDeliveryColumns();
        SavingProductHelper.EnsureInstallmentDeliveryColumns();
        SavingProductHelper.ProcessBulkSavingSchedule();

        if (!IsPostBack)
        {
            LoadOrders();
        }
        else if (OrderData != null)
        {
            BindGrid();
        }
    }

    void LoadOrders()
    {
        // One card per approved installment (not grouped by OrderId).
        OrderData = BuildApprovedInstallmentOrders(GetApprovedInstallmentHistory(Session["userid"].ToString()));
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = OrderData;
        if (dt == null)
        {
            rptOrders.DataSource = null;
            rptOrders.DataBind();
            pnlPager.Visible = false;
            pnlEmpty.Visible = true;
            UpdateOrderCount(0);
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

        pnlEmpty.Visible = dt.Rows.Count == 0;
        rptOrders.DataSource = GetPagedRows(dt);
        rptOrders.DataBind();
        UpdateOrderCount(dt.Rows.Count);
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

    void UpdateOrderCount(int count)
    {
        if (lblOrderCount == null)
        {
            return;
        }

        lblOrderCount.Text = count == 0
            ? "No approved installments found"
            : count + (count == 1 ? " approved installment" : " approved installments");
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

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = 0;
        if (OrderData != null)
        {
            BindGrid();
        }
    }


    DataTable GetApprovedInstallmentHistory(string userId)
    {
        DataTable dt = new DataTable();
        try
        {
            bool hasDeliveryStatus = SavingProductHelper.HasInstallmentDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";
            string deliveryDateSelect = hasDeliveryStatus
                ? @"CASE
                    WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.DeliveryStatus, '')))) = 'delivered'
                    THEN sa.DeliveryStatusUpdatedOn
                    ELSE NULL
                END AS DeliveryDate"
                : "CAST(NULL AS DATETIME) AS DeliveryDate";
            string consignmentSelect = SavingProductHelper.HasInstallmentConsignmentColumn()
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sa.ConsignmentNumber)), ''), '') AS ConsignmentNumber"
                : "'' AS ConsignmentNumber";

            string sql = @"
SELECT
    sa.id,
    sa.orderid,
    sa.instno,
    sa.amount,
    sa.status AS OrderStatus,
    ISNULL(sa.approvedate, sa.installmentdate) AS OrderDate,
    ISNULL(NULLIF(LTRIM(RTRIM(sd.couponcode)), ''), '-') AS couponcode,
    ISNULL(NULLIF(LTRIM(RTRIM(pm.productname)), ''), 'Saving Product') AS productname,
    COALESCE(NULLIF(sa.productid, 0), sd.productid) AS productid,
    ud.username,
    " + deliveryStatusSelect + @",
    " + deliveryDateSelect + @",
    " + consignmentSelect + @",
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
        sd0.couponcode,
        sd0.productid
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE sd0.orderid = sa.orderid
      AND LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
    ORDER BY
        CASE WHEN sd0.productid = sa.productid THEN 0 ELSE 1 END,
        sd0.id ASC
) sd
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK)
    ON COALESCE(NULLIF(sa.productid, 0), sd.productid) = pm.id
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE LTRIM(RTRIM(sa.UserId)) = '" + SqlEscape(userId) + @"'
  AND LOWER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'approved'
ORDER BY ISNULL(sa.approvedate, sa.installmentdate) DESC, sa.InstNo DESC, sa.id DESC";

            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            finally
            {
                ObjData.EndConnection();
            }

            if (dt == null)
            {
                return new DataTable();
            }

            AddDisplayColumns(dt);
            foreach (DataRow row in dt.Rows)
            {
                row["AddressSummary"] = BuildAddressSummary(row);
                row["OrderStatusDisplay"] = FormatOrderStatus(Convert.ToString(row["OrderStatus"]));
                row["DeliveryStatusDisplay"] = FormatDeliveryStatus(row);
                row["OrderDateDisplay"] = FormatDateTime(row["OrderDate"]);
                row["DeliveryDateDisplay"] = FormatDateTime(row["DeliveryDate"]);
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt;
    }

    static DataTable BuildApprovedInstallmentOrders(DataTable installmentRows)
    {
        DataTable cards = CreateGroupedOrderTable();
        if (installmentRows == null || installmentRows.Rows.Count == 0)
        {
            return cards;
        }

        foreach (DataRow row in installmentRows.Rows)
        {
            DataRow summary = cards.NewRow();
            string product = Convert.ToString(row["productname"]).Trim();
            string coupon = Convert.ToString(row["couponcode"]).Trim();
            string instNo = Convert.ToString(row["instno"]).Trim();
            if (string.IsNullOrWhiteSpace(product))
            {
                product = "Saving Product";
            }

            string productLine = product;
            if (!string.IsNullOrWhiteSpace(coupon) && coupon != "-")
            {
                productLine += " (" + coupon + ")";
            }
            if (!string.IsNullOrWhiteSpace(instNo))
            {
                productLine += " · Inst #" + instNo;
            }

            summary["orderid"] = row["orderid"];
            summary["InstallmentId"] = row["id"];
            summary["username"] = row["username"];
            summary["AddressSummary"] = row["AddressSummary"];
            summary["ProductsSummary"] = productLine;
            summary["ItemCount"] = 1;
            summary["ProductsHtml"] = BuildProductsHtml(productLine);
            summary["OrderStatusDisplay"] = Convert.ToString(row["OrderStatusDisplay"]);
            summary["DeliveryStatusDisplay"] = Convert.ToString(row["DeliveryStatusDisplay"]);
            summary["OrderDateDisplay"] = Convert.ToString(row["OrderDateDisplay"]);
            summary["DeliveryDateDisplay"] = Convert.ToString(row["DeliveryDateDisplay"]);
            summary["ConsignmentNumber"] = row.Table.Columns.Contains("ConsignmentNumber")
                ? Convert.ToString(row["ConsignmentNumber"])
                : string.Empty;
            summary["CanInvoice"] = IsApprovedOrderStatus(Convert.ToString(row["OrderStatus"]));

            cards.Rows.Add(summary);
        }

        return cards;
    }

    static DataTable CreateGroupedOrderTable()
    {
        DataTable grouped = new DataTable();
        grouped.Columns.Add("orderid", typeof(string));
        grouped.Columns.Add("InstallmentId", typeof(object));
        grouped.Columns.Add("username", typeof(string));
        grouped.Columns.Add("AddressSummary", typeof(string));
        grouped.Columns.Add("ProductsSummary", typeof(string));
        grouped.Columns.Add("ItemCount", typeof(int));
        grouped.Columns.Add("ProductsHtml", typeof(string));
        grouped.Columns.Add("OrderStatusDisplay", typeof(string));
        grouped.Columns.Add("DeliveryStatusDisplay", typeof(string));
        grouped.Columns.Add("OrderDateDisplay", typeof(string));
        grouped.Columns.Add("DeliveryDateDisplay", typeof(string));
        grouped.Columns.Add("ConsignmentNumber", typeof(string));
        grouped.Columns.Add("CanInvoice", typeof(bool));
        return grouped;
    }

    static string BuildProductsSummary(IEnumerable<DataRow> rows)
    {
        List<string> parts = new List<string>();
        foreach (DataRow row in rows)
        {
            string product = Convert.ToString(row["productname"]).Trim();
            string coupon = Convert.ToString(row["couponcode"]).Trim();
            if (string.IsNullOrWhiteSpace(product))
            {
                product = "Saving Product";
            }

            parts.Add(string.IsNullOrWhiteSpace(coupon) ? product : product + " (" + coupon + ")");
        }

        return string.Join(Environment.NewLine, parts);
    }

    static string BuildProductsHtml(string productsSummary)
    {
        if (string.IsNullOrWhiteSpace(productsSummary))
        {
            return "<span class=\"soh-product-empty\">No products listed</span>";
        }

        string[] lines = productsSummary.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        sb.Append("<ul class=\"soh-product-lines\">");

        foreach (string line in lines)
        {
            string value = line.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            sb.Append("<li>").Append(HttpUtility.HtmlEncode(value)).Append("</li>");
        }

        sb.Append("</ul>");
        return sb.ToString();
    }

    static string AggregateOrderStatus(IEnumerable<DataRow> rows)
    {
        List<string> statuses = rows
            .Select(row => Normalize(Convert.ToString(row["OrderStatus"])))
            .Distinct()
            .ToList();

        bool anyPending = statuses.Any(status => status == "pending" || status == "0");
        bool anyApproved = statuses.Any(status => status == "approved" || status == "1" || status == "active" || status == "approve");
        bool anyRejected = statuses.Any(status => status == "rejected" || status == "2" || status == "cancelled" || status == "canceled");

        if (anyPending)
        {
            return "Pending";
        }

        if (anyApproved && anyRejected)
        {
            return "Partial";
        }

        if (anyApproved)
        {
            return "Approved";
        }

        if (anyRejected)
        {
            return "Rejected";
        }

        return FormatOrderStatus(Convert.ToString(rows.First()["OrderStatus"]));
    }

    static string AggregateDeliveryStatus(IEnumerable<DataRow> rows)
    {
        List<string> deliveryStatuses = rows
            .Select(FormatDeliveryStatus)
            .Where(status => status != "-" && status != "Not Applicable")
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (deliveryStatuses.Count == 0)
        {
            return rows.Any(row =>
            {
                string status = Normalize(Convert.ToString(row["OrderStatus"]));
                return status == "rejected" || status == "2" || status == "cancelled" || status == "canceled";
            })
                ? "Not Applicable"
                : "-";
        }

        if (deliveryStatuses.Count == 1)
        {
            return deliveryStatuses[0];
        }

        return string.Join(", ", deliveryStatuses);
    }

    static string AggregateDeliveryDateDisplay(IEnumerable<DataRow> rows)
    {
        List<DateTime> deliveredDates = rows
            .Select(row => GetDateValue(row["DeliveryDate"]))
            .Where(date => date.Year > 1900)
            .ToList();

        if (deliveredDates.Count == 0)
        {
            return "-";
        }

        return FormatDateTime(deliveredDates.Max());
    }

    static DateTime GetDateValue(object value)
    {
        DateTime parsedDate;
        if (value == null || value == DBNull.Value)
        {
            return DateTime.MinValue;
        }

        if (DateTime.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsedDate))
        {
            return parsedDate;
        }

        return DateTime.MinValue;
    }

    static void AddDisplayColumns(DataTable dt)
    {
        AddColumnIfMissing(dt, "AddressSummary", typeof(string));
        AddColumnIfMissing(dt, "OrderStatusDisplay", typeof(string));
        AddColumnIfMissing(dt, "DeliveryStatusDisplay", typeof(string));
        AddColumnIfMissing(dt, "OrderDateDisplay", typeof(string));
        AddColumnIfMissing(dt, "DeliveryDateDisplay", typeof(string));
    }

    static void AddColumnIfMissing(DataTable dt, string columnName, Type type)
    {
        if (!dt.Columns.Contains(columnName))
        {
            dt.Columns.Add(columnName, type);
        }
    }

    protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {
            return;
        }

        Label lblOrderStatus = (Label)e.Item.FindControl("lblOrderStatus");
        if (lblOrderStatus != null)
        {
            lblOrderStatus.CssClass = "dash-order-status-badge " + GetOrderStatusBadgeClass(lblOrderStatus.Text);
        }

        Label lblDeliveryStatus = (Label)e.Item.FindControl("lblDeliveryStatus");
        if (lblDeliveryStatus != null)
        {
            lblDeliveryStatus.CssClass = "dash-order-status-badge " + GetDeliveryStatusClass(lblDeliveryStatus.Text);
        }

        Panel pnlConsignment = (Panel)e.Item.FindControl("pnlConsignment");
        Literal litConsignment = (Literal)e.Item.FindControl("litConsignment");
        if (pnlConsignment != null && litConsignment != null)
        {
            string consignment = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ConsignmentNumber")).Trim();
            string deliveryStatus = Normalize(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DeliveryStatusDisplay")));
            bool showConsignment = !string.IsNullOrWhiteSpace(consignment)
                && (deliveryStatus == "shipped" || deliveryStatus == "out for delivery" || deliveryStatus == "delivered");
            pnlConsignment.Visible = showConsignment;
            litConsignment.Text = HttpUtility.HtmlEncode(consignment);
        }

        HyperLink lnkInvoice = (HyperLink)e.Item.FindControl("lnkInvoice");
        Label lblInvoiceUnavailable = (Label)e.Item.FindControl("lblInvoiceUnavailable");
        if (lnkInvoice != null && lblInvoiceUnavailable != null)
        {
            DataRowView rowView = e.Item.DataItem as DataRowView;
            bool showInvoice = rowView != null && Convert.ToBoolean(rowView["CanInvoice"]);
            lnkInvoice.Visible = showInvoice;
            lblInvoiceUnavailable.Visible = !showInvoice;
        }
    }

    static bool IsApprovedOrderStatus(string status)
    {
        switch (Normalize(status))
        {
            case "approved":
            case "1":
            case "active":
            case "approve":
                return true;
            default:
                return false;
        }
    }

    static string FormatOrderStatus(string status)
    {
        switch (Normalize(status))
        {
            case "approved":
            case "1":
            case "active":
                return "Approved";
            case "rejected":
            case "2":
            case "cancelled":
            case "canceled":
                return "Rejected";
            case "pending":
            case "0":
                return "Pending";
            default:
                return string.IsNullOrWhiteSpace(status) ? "-" : status;
        }
    }

    static string FormatDeliveryStatus(DataRow row)
    {
        string orderStatus = Normalize(Convert.ToString(row["OrderStatus"]));
        if (orderStatus == "pending" || orderStatus == "0")
        {
            return "-";
        }

        if (orderStatus == "rejected" || orderStatus == "2")
        {
            return "Not Applicable";
        }

        if (orderStatus != "approved" && orderStatus != "1" && orderStatus != "active")
        {
            return "-";
        }

        string deliveryStatus = Convert.ToString(row["DeliveryStatus"]);
        return string.IsNullOrWhiteSpace(deliveryStatus) || deliveryStatus == "-"
            ? "Confirmed"
            : deliveryStatus;
    }

    static string FormatDateTime(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "-";
        }

        DateTime parsedDate;
        if (DateTime.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsedDate))
        {
            if (parsedDate.Year <= 1900)
            {
                return "-";
            }

            return parsedDate.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        return "-";
    }

    static string BuildAddressSummary(DataRow row)
    {
        StringBuilder sb = new StringBuilder();
        AppendPart(sb, Convert.ToString(row["ShipAddress"]));
        AppendPart(sb, Convert.ToString(row["ShipArea"]));

        string city = Convert.ToString(row["ShipCity"]);
        string state = Convert.ToString(row["ShipState"]);
        string pincode = Convert.ToString(row["ShipPincode"]);
        string location = JoinParts(city, state);
        if (!string.IsNullOrWhiteSpace(pincode))
        {
            location = string.IsNullOrWhiteSpace(location) ? pincode : location + " - " + pincode;
        }

        AppendPart(sb, location);
        string address = sb.ToString().Trim();
        return string.IsNullOrWhiteSpace(address) ? "Address not available" : address;
    }

    static void AppendPart(StringBuilder sb, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (sb.Length > 0)
        {
            sb.Append(", ");
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

    static string GetOrderStatusBadgeClass(string status)
    {
        switch (Normalize(status))
        {
            case "approved":
                return "is-approved";
            case "rejected":
                return "is-rejected";
            case "pending":
                return "is-pending";
            case "partial":
                return "is-out";
            default:
                return "is-inactive";
        }
    }

    static string GetDeliveryStatusClass(string status)
    {
        switch (Normalize(status))
        {
            case "shipped":
                return "is-shipped";
            case "out for delivery":
                return "is-out";
            case "delivered":
                return "is-delivered";
            case "not applicable":
                return "is-inactive";
            case "-":
                return "is-inactive";
            default:
                return "is-confirmed";
        }
    }

    static string Normalize(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

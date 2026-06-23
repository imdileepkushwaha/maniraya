using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SavingProductOrderDetails : Page
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

        SavingProductHelper.EnsureDeliveryColumns();

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
        ddDeliveryStatus.SelectedIndex = 0;
        GridView1.PageIndex = 0;
        LoadOrders();
    }

    void LoadOrders()
    {
        OrderData = GetConfirmedOrders();
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
            bool hasDeliveryStatus = SavingProductHelper.HasDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sd.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT
    sd.id,
    sd.orderid,
    sd.userid,
    ud.username,
    ud.mobile,
    pm.productname,
    sd.amount,
    sd.approvedate,
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
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sd.UserId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE ").Append(GetApprovedStatusFilter("sd"));

            if (!string.IsNullOrWhiteSpace(txtOrderId.Text))
            {
                sql.Append(" AND sd.orderid LIKE '%").Append(SqlEscape(txtOrderId.Text.Trim())).Append("%'");
            }

            if (!string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                string userSearch = SqlEscape(txtUserId.Text.Trim());
                sql.Append(" AND (sd.userid LIKE '%").Append(userSearch)
                    .Append("%' OR ud.username LIKE '%").Append(userSearch).Append("%')");
            }

            if (hasDeliveryStatus && !string.IsNullOrWhiteSpace(ddDeliveryStatus.SelectedValue))
            {
                sql.Append(" AND ISNULL(NULLIF(LTRIM(RTRIM(sd.DeliveryStatus)), ''), 'Confirmed') = '")
                    .Append(SqlEscape(ddDeliveryStatus.SelectedValue)).Append("'");
            }

            sql.Append(" ORDER BY sd.approvedate DESC, sd.id DESC");

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

    static string GetApprovedStatusFilter(string tableAlias)
    {
        return "(" + tableAlias + ".status = 'Approved'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + tableAlias + ".status, '')))) IN ('approved', 'approve'))";
    }

    DataRow GetOrderRow(int recordId)
    {
        DataTable dt = GetOrderById(recordId);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }

        return null;
    }

    DataTable GetOrderById(int recordId)
    {
        DataTable dt = new DataTable();
        try
        {
            bool hasDeliveryStatus = SavingProductHelper.HasDeliveryStatusColumn();
            string deliveryStatusSelect = hasDeliveryStatus
                ? "ISNULL(NULLIF(LTRIM(RTRIM(sd.DeliveryStatus)), ''), 'Confirmed') AS DeliveryStatus"
                : "'Confirmed' AS DeliveryStatus";

            string sql = @"
SELECT
    sd.id,
    sd.orderid,
    sd.userid,
    ud.username,
    ud.mobile,
    pm.productname,
    sd.amount,
    sd.approvedate,
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
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sd.UserId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE sd.id = " + recordId + @"
  AND " + GetApprovedStatusFilter("sd");

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
        int recordId;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out recordId))
        {
            return;
        }

        if (e.CommandName == "printaddress")
        {
            BindPrintModal(recordId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openPrintModal", "showAdminModal('addressPrintModal');", true);
            return;
        }

        if (e.CommandName == "updatestatus")
        {
            BindStatusModal(recordId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openStatusModal", "showAdminModal('statusUpdateModal');", true);
        }
    }

    void BindPrintModal(int recordId)
    {
        DataRow row = GetOrderRow(recordId);
        if (row == null)
        {
            litPrintAddress.Text = "<p>No address found for this order.</p>";
            return;
        }

        string orderId = Convert.ToString(row["orderid"]);
        string userName = Convert.ToString(row["username"]);
        string userId = Convert.ToString(row["userid"]);
        string mobile = Convert.ToString(row["mobile"]);
        string productName = Convert.ToString(row["productname"]);
        string addressBlock = Server.HtmlEncode(FormatAddressBlock(row));

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
            (string.IsNullOrWhiteSpace(productName) ? string.Empty : " | Product: " + Server.HtmlEncode(productName)) +
            "</div>";
    }

    void BindStatusModal(int recordId)
    {
        DataRow row = GetOrderRow(recordId);
        if (row == null)
        {
            return;
        }

        hfOrderRecordId.Value = recordId.ToString();
        txtModalOrderId.Text = Convert.ToString(row["orderid"]);
        txtModalUserName.Text = Convert.ToString(row["username"]);

        string currentStatus = Convert.ToString(row["DeliveryStatus"]);
        ListItem statusItem = ddModalDeliveryStatus.Items.FindByValue(currentStatus);
        if (statusItem != null)
        {
            ddModalDeliveryStatus.ClearSelection();
            statusItem.Selected = true;
        }
    }

    protected void btnSaveDeliveryStatus_Click(object sender, EventArgs e)
    {
        int recordId;
        if (!int.TryParse(hfOrderRecordId.Value, out recordId) || recordId <= 0)
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

        if (UpdateDeliveryStatus(recordId, newStatus, Session["useradmin"].ToString()))
        {
            Message.Show("Delivery status updated successfully.");
            LoadOrders();
        }
        else
        {
            Message.Show("Unable to update delivery status.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "reopenStatusModalFail", "showAdminModal('statusUpdateModal');", true);
        }
    }

    bool UpdateDeliveryStatus(int recordId, string deliveryStatus, string updatedBy)
    {
        if (!SavingProductHelper.HasDeliveryStatusColumn())
        {
            SavingProductHelper.EnsureDeliveryColumns();
        }

        try
        {
            ObjData.StartConnection();
            try
            {
                string sql = "UPDATE SavingAccountDetail SET DeliveryStatus='" + SqlEscape(deliveryStatus)
                    + "', DeliveryStatusUpdatedOn=GETDATE(), DeliveryStatusUpdatedBy='" + SqlEscape(updatedBy)
                    + "' WHERE id=" + recordId;
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

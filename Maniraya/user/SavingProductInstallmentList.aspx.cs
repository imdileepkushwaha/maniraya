using DataTier;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingProductInstallmentList : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            SavingProductHelper.EnsureBulkColumns();
            SavingProductHelper.ProcessBulkSavingSchedule();
            LoadCoupons();
            ApplyCouponQueryString();
            LoadInstallments();
        }
    }

    void ApplyCouponQueryString()
    {
        string coupon = (Convert.ToString(Request.QueryString["coupon"]) ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(coupon) || ddCouponCode == null || ddCouponCode.Items.Count == 0)
        {
            return;
        }

        ListItem item = ddCouponCode.Items.FindByValue(coupon);
        if (item == null)
        {
            item = ddCouponCode.Items.FindByText(coupon);
        }

        if (item != null)
        {
            ddCouponCode.ClearSelection();
            item.Selected = true;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadInstallments();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddStatus.SelectedIndex = 0;
        if (ddCouponCode.Items.Count > 0)
        {
            ddCouponCode.SelectedIndex = 0;
        }
        txtProduct.Text = string.Empty;
        LoadInstallments();
    }

    protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadInstallments();
    }

    protected void ddCouponCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadInstallments();
    }

    void LoadCoupons()
    {
        ddCouponCode.Items.Clear();
        ddCouponCode.Items.Add(new ListItem("All Coupons", ""));

        string userId = Convert.ToString(Session["userid"]).Trim();
        string sql = @"
SELECT DISTINCT LTRIM(RTRIM(sd.couponcode)) AS couponcode
FROM SavingAccountDetail sd WITH (NOLOCK)
WHERE sd.UserId = '" + SqlEscape(userId) + @"'
  AND NULLIF(LTRIM(RTRIM(sd.couponcode)), '') IS NOT NULL
  AND EXISTS (
        SELECT 1
        FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
        WHERE sa.OrderId = sd.orderid
          AND sa.UserId = sd.UserId
  )
ORDER BY couponcode";

        try
        {
            ObjData.StartConnection();
            DataTable dt;
            try
            {
                dt = ObjData.RunDataTable(sql);
            }
            finally
            {
                ObjData.EndConnection();
            }

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string coupon = Convert.ToString(row["couponcode"]).Trim();
                    if (!string.IsNullOrWhiteSpace(coupon))
                    {
                        ddCouponCode.Items.Add(new ListItem(coupon, coupon));
                    }
                }
            }
        }
        catch
        {
        }
    }

    void LoadInstallments()
    {
        string coupon = (ddCouponCode != null ? ddCouponCode.SelectedValue : string.Empty) ?? string.Empty;
        coupon = coupon.Trim();
        if (!string.IsNullOrWhiteSpace(coupon))
        {
            SavingProductHelper.EnsureBulkInstallmentsForCoupon(coupon);
        }
        else
        {
            SavingProductHelper.EnsureBulkInstallmentsForUser(Convert.ToString(Session["userid"]));
        }

        SavingProductHelper.ProcessBulkSavingSchedule();

        DataTable dt = GetInstallments();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    DataTable GetInstallments()
    {
        SavingProductHelper.EnsureInstallmentProductAssignTable();

        string userId = Convert.ToString(Session["userid"]).Trim();
        string amountSql = SavingProductHelper.DisplayEmiAmountSql("sd", "sa");
        string firstAmountSql = SavingProductHelper.DisplayEmiAmountSql("sd", "sd");
        StringBuilder sql = new StringBuilder();
        sql.Append(@"
SELECT
    x.id,
    x.userid,
    x.orderid,
    x.instno,
    x.amount,
    x.installmentdate,
    x.status,
    x.couponcode,
    x.productname,
    x.StatusDisplay
FROM (
SELECT
    sa.id,
    sa.userid,
    sa.orderid,
    sa.instno,
    ").Append(amountSql).Append(@" AS amount,
    sa.installmentdate,
    sa.status,
    sd.couponcode,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname,
    CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('approved', 'paid') THEN 'Paid'
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) = 'processing' THEN 'Processing'
        ELSE 'Unpaid'
    END AS StatusDisplay
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
LEFT JOIN SavingAccountDetail sd WITH (NOLOCK)
    ON sa.OrderId = sd.orderid
   AND LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(sd.UserId))
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ISNULL(ipa.ProductId, 0) > 0
   AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
WHERE sa.UserId = '").Append(SqlEscape(userId)).Append(@"'
UNION ALL
SELECT
    sd.id,
    sd.userid,
    sd.orderid,
    1 AS instno,
    ").Append(firstAmountSql).Append(@" AS amount,
    CONVERT(datetime, CONVERT(date, ISNULL(sd.ApproveDate, sd.EntryDate))) AS installmentdate,
    CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', 'approve', '1', 'active') THEN 'Approved'
        ELSE sd.status
    END AS status,
    sd.couponcode,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname,
    CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', 'approve', '1', 'active', 'paid') THEN 'Paid'
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) = 'processing' THEN 'Processing'
        ELSE 'Unpaid'
    END AS StatusDisplay
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ISNULL(ipa.ProductId, 0) > 0
   AND ipa.InstallmentNo = 1
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
WHERE LTRIM(RTRIM(sd.UserId)) = '").Append(SqlEscape(userId)).Append(@"'
  AND NULLIF(LTRIM(RTRIM(sd.couponcode)), '') IS NOT NULL
  AND LOWER(LTRIM(RTRIM(ISNULL(sd.Status, '')))) NOT IN ('rejected', 'cancelled')
  AND NOT EXISTS (
        SELECT 1
        FROM SavingAccountInstallmentDetail sa1 WITH (NOLOCK)
        WHERE sa1.OrderId = sd.orderid
          AND LTRIM(RTRIM(sa1.UserId)) = LTRIM(RTRIM(sd.UserId))
          AND ISNULL(TRY_CONVERT(INT, sa1.InstNo), 0) = 1
  )
) x
WHERE 1 = 1");

        string statusFilter = (ddStatus.SelectedValue ?? string.Empty).Trim();
        if (statusFilter == "Paid")
        {
            sql.Append(" AND x.StatusDisplay = 'Paid'");
        }
        else if (statusFilter == "Processing")
        {
            sql.Append(" AND x.StatusDisplay = 'Processing'");
        }
        else if (statusFilter == "Unpaid")
        {
            sql.Append(" AND x.StatusDisplay = 'Unpaid'");
        }

        if (!string.IsNullOrWhiteSpace(ddCouponCode.SelectedValue))
        {
            sql.Append(" AND LTRIM(RTRIM(x.couponcode)) = '").Append(SqlEscape(ddCouponCode.SelectedValue.Trim())).Append("'");
        }

        if (!string.IsNullOrWhiteSpace(txtProduct.Text))
        {
            string productSearch = SqlEscape(txtProduct.Text.Trim());
            sql.Append(" AND ISNULL(x.productname, 'Not assigned') LIKE '%")
                .Append(productSearch)
                .Append("%'");
        }

        sql.Append(" ORDER BY x.InstNo ASC, x.InstallmentDate ASC, x.id ASC");

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblproductname = (Label)e.Row.FindControl("lblproductname");
        if (lblproductname != null)
        {
            string productName = (lblproductname.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(productName)
                || productName.Equals("Not assigned", StringComparison.OrdinalIgnoreCase))
            {
                lblproductname.Text = "Not assigned";
                lblproductname.CssClass = "dash-saving-product is-unassigned";
            }
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        if (lblstatus == null)
        {
            return;
        }

        string displayStatus = (lblstatus.Text ?? string.Empty).Trim();
        if (displayStatus.Equals("Paid", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "dash-saving-status is-paid";
        }
        else if (displayStatus.Equals("Processing", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "dash-saving-status is-processing";
        }
        else
        {
            lblstatus.Text = "Unpaid";
            lblstatus.CssClass = "dash-saving-status is-unpaid";
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

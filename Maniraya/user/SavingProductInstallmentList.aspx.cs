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
        DataTable dt = GetInstallments();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    DataTable GetInstallments()
    {
        SavingProductHelper.EnsureInstallmentProductAssignTable();

        string userId = Convert.ToString(Session["userid"]).Trim();
        StringBuilder sql = new StringBuilder();
        sql.Append(@"
SELECT
    sa.id,
    sa.userid,
    sa.orderid,
    sa.instno,
    sa.amount,
    sa.installmentdate,
    sa.status,
    sd.couponcode,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname,
    CASE
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) = 'approved' THEN 'Paid'
        WHEN LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) = 'processing' THEN 'Processing'
        ELSE 'Unpaid'
    END AS StatusDisplay
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
LEFT JOIN SavingAccountDetail sd WITH (NOLOCK) ON sa.OrderId = sd.orderid
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
WHERE sa.UserId = '").Append(SqlEscape(userId)).Append("'");

        string statusFilter = (ddStatus.SelectedValue ?? string.Empty).Trim();
        if (statusFilter == "Paid")
        {
            sql.Append(" AND LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) = 'approved'");
        }
        else if (statusFilter == "Processing")
        {
            sql.Append(" AND LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) = 'processing'");
        }
        else if (statusFilter == "Unpaid")
        {
            sql.Append(" AND LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) NOT IN ('approved', 'processing')");
        }

        if (!string.IsNullOrWhiteSpace(ddCouponCode.SelectedValue))
        {
            sql.Append(" AND LTRIM(RTRIM(sd.couponcode)) = '").Append(SqlEscape(ddCouponCode.SelectedValue.Trim())).Append("'");
        }

        if (!string.IsNullOrWhiteSpace(txtProduct.Text))
        {
            string productSearch = SqlEscape(txtProduct.Text.Trim());
            sql.Append(" AND ISNULL(NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), ''), 'Not assigned') LIKE '%")
                .Append(productSearch)
                .Append("%'");
        }

        sql.Append(" ORDER BY sa.InstNo ASC, sa.InstallmentDate ASC, sa.id ASC");

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

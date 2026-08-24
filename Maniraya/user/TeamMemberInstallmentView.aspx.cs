using DataTier;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TeamMemberInstallmentView : Page
{
    Data ObjData = new Data();

    string MemberUserId
    {
        get { return ViewState["TeamMemberUserId"] as string ?? string.Empty; }
        set { ViewState["TeamMemberUserId"] = value; }
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
            string uid = (Request.QueryString["uid"] ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(uid) || !IsInSponsorTeam(uid))
            {
                pnlAccessDenied.Visible = true;
                pnlContent.Visible = false;
                lblMemberInfo.Text = string.Empty;
                return;
            }

            MemberUserId = uid;
            pnlAccessDenied.Visible = false;
            pnlContent.Visible = true;
            SavingProductHelper.EnsureBulkColumns();
            SavingProductHelper.EnsureBulkInstallmentsForUser(uid);
            SavingProductHelper.ProcessBulkSavingSchedule();
            BindMemberInfo();
            LoadCoupons();
            LoadInstallments();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!EnsureAccess())
        {
            return;
        }
        LoadInstallments();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (!EnsureAccess())
        {
            return;
        }

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
        if (!EnsureAccess())
        {
            return;
        }
        LoadInstallments();
    }

    protected void ddCouponCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!EnsureAccess())
        {
            return;
        }
        LoadInstallments();
    }

    bool EnsureAccess()
    {
        if (string.IsNullOrWhiteSpace(MemberUserId) || !IsInSponsorTeam(MemberUserId))
        {
            pnlAccessDenied.Visible = true;
            pnlContent.Visible = false;
            return false;
        }

        pnlAccessDenied.Visible = false;
        pnlContent.Visible = true;
        return true;
    }

    void BindMemberInfo()
    {
        DataTable dt = null;
        string sql = @"
SELECT TOP 1
    LTRIM(RTRIM(UserId)) AS userid,
    ISNULL(UserName, '') AS username,
    ISNULL(Mobile, '') AS mobile,
    ISNULL(Email, '') AS email
FROM UserDetail WITH (NOLOCK)
WHERE LTRIM(RTRIM(UserId)) = '" + SqlEscape(MemberUserId) + "'";

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
            dt = null;
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            lblMemberInfo.Text = string.Format("{0} ({1}) | {2} | {3}",
                Convert.ToString(dt.Rows[0]["username"]),
                Convert.ToString(dt.Rows[0]["userid"]),
                Convert.ToString(dt.Rows[0]["mobile"]),
                Convert.ToString(dt.Rows[0]["email"]));
        }
        else
        {
            lblMemberInfo.Text = "User: " + MemberUserId;
        }
    }

    bool IsInSponsorTeam(string memberUserId)
    {
        string rootUserId = Convert.ToString(Session["userid"]).Trim();
        if (string.IsNullOrWhiteSpace(memberUserId) || string.IsNullOrWhiteSpace(rootUserId))
        {
            return false;
        }

        if (string.Equals(memberUserId, rootUserId, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        string sql = @";
WITH TeamCTE AS (
    SELECT ud.UserId, ISNULL(ud.SavingStatus, 0) AS SavingStatus, 1 AS userlevel
    FROM UserDetail ud WITH (NOLOCK)
    WHERE LTRIM(RTRIM(ud.SponserId)) = '" + SqlEscape(rootUserId) + @"'
    UNION ALL
    SELECT c.UserId, ISNULL(c.SavingStatus, 0) AS SavingStatus, t.userlevel + 1
    FROM UserDetail c WITH (NOLOCK)
    INNER JOIN TeamCTE t ON LTRIM(RTRIM(c.SponserId)) = LTRIM(RTRIM(t.UserId))
    WHERE t.userlevel < 10
)
SELECT TOP 1 UserId
FROM TeamCTE
WHERE LTRIM(RTRIM(UserId)) = '" + SqlEscape(memberUserId) + @"'
  AND ISNULL(SavingStatus, 0) = 1";

        try
        {
            ObjData.StartConnection();
            try
            {
                DataTable dt = ObjData.RunDataTable(sql);
                return dt != null && dt.Rows.Count > 0;
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

    void LoadCoupons()
    {
        ddCouponCode.Items.Clear();
        ddCouponCode.Items.Add(new ListItem("All Coupons", ""));

        string sql = @"
SELECT DISTINCT LTRIM(RTRIM(sd.couponcode)) AS couponcode
FROM SavingAccountDetail sd WITH (NOLOCK)
WHERE sd.UserId = '" + SqlEscape(MemberUserId) + @"'
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
    ISNULL(NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), ''), 'Not assigned') AS productname,
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
WHERE sa.UserId = '").Append(SqlEscape(MemberUserId)).Append(@"'
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
    ISNULL(NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), ''), 'Not assigned') AS productname,
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
WHERE LTRIM(RTRIM(sd.UserId)) = '").Append(SqlEscape(MemberUserId)).Append(@"'
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
            sql.Append(" AND ISNULL(x.productname, '') LIKE '%").Append(SqlEscape(txtProduct.Text.Trim())).Append("%'");
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

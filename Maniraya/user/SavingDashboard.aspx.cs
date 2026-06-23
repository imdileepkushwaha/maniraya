using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingDashboard : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            string userId = Session["userid"].ToString();
            LoadCouponDropdown(userId);
            BindSavingDashboard(userId);
        }
    }

    protected void ddlCouponCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSavingDashboard(Session["userid"].ToString());
    }

    void LoadCouponDropdown(string userId)
    {
        ddlCouponCode.Items.Clear();
        DataTable accounts = GetSavingAccounts(userId);

        if (accounts == null || accounts.Rows.Count == 0)
        {
            ddlCouponCode.Items.Add(new ListItem("No coupon found", string.Empty));
            ddlCouponCode.Enabled = false;
            return;
        }

        foreach (DataRow row in accounts.Rows)
        {
            string couponCode = ResolveCouponCode(row);
            if (string.IsNullOrWhiteSpace(couponCode))
            {
                continue;
            }

            if (ddlCouponCode.Items.FindByValue(couponCode) != null)
            {
                continue;
            }

            string orderId = GetRowValue(row, "orderid", "OrderId", "OrderID");
            if (!string.IsNullOrWhiteSpace(orderId)
                && ddlCouponCode.Items.FindByValue(orderId) != null
                && !string.Equals(couponCode, orderId, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string productName = GetRowValue(row, "productname", "ProductName");
            string displayText = string.IsNullOrWhiteSpace(productName)
                ? couponCode
                : couponCode + " - " + productName;

            ddlCouponCode.Items.Add(new ListItem(displayText, couponCode));
        }

        if (ddlCouponCode.Items.Count == 0)
        {
            ddlCouponCode.Items.Add(new ListItem("No coupon found", string.Empty));
            ddlCouponCode.Enabled = false;
            return;
        }

        ddlCouponCode.Enabled = true;
        ddlCouponCode.SelectedIndex = 0;
    }

    void BindSavingDashboard(string userId)
    {
        string couponCode = ddlCouponCode.SelectedValue;
        lblcardno.Text = string.IsNullOrWhiteSpace(couponCode) ? "-" : couponCode;

        DataRow accountRow = GetAccountRow(userId, couponCode);
        BindAccountStatus(accountRow, userId);

        DataRow dashboardRow = GetSavingDashboardRow(userId, couponCode);
        if (dashboardRow != null)
        {
            BindMetricsFromDashboard(dashboardRow);
            return;
        }

        if (accountRow != null)
        {
            BindMetricsFromAccount(accountRow);
            return;
        }

        ResetMetrics();
    }

    void BindMetricsFromDashboard(DataRow dashboardRow)
    {
        lbllevelincome.Text = GetRowValueOrDefault(dashboardRow, "0", "levelincome", "LevelIncome");
        lbltotalemi.Text = GetRowValueOrDefault(dashboardRow, "0.00", "totalemi", "TotalEmi");
        lblpendingemi.Text = GetRowValueOrDefault(dashboardRow, "0.00", "pendingemi", "PendingEmi");
        lblpaidemi.Text = GetRowValueOrDefault(dashboardRow, "0.00", "paidemi", "PaidEmi");
        lblactivationdate.Text = FormatDate(GetRowValueOrDefault(dashboardRow, "-", "approvedate", "ApproveDate", "InvestmentDate"));
        lblmaturitydate.Text = FormatDate(GetRowValueOrDefault(dashboardRow, "-", "maturitydate", "MaturityDate"));
        lblmaturityamount.Text = GetRowValueOrDefault(dashboardRow, "0.00", "maturityamount", "MaturityAmount");
    }

    void BindMetricsFromAccount(DataRow accountRow)
    {
        lbllevelincome.Text = "0";
        lbltotalemi.Text = GetRowValueOrDefault(accountRow, "0.00", "amount", "Amount");
        lblpaidemi.Text = "0.00";
        lblpendingemi.Text = "0.00";
        lblactivationdate.Text = FormatDate(GetRowValueOrDefault(accountRow, "-", "approvedate", "ApproveDate", "entrydate", "EntryDate"));
        lblmaturitydate.Text = "-";
        lblmaturityamount.Text = GetRowValueOrDefault(accountRow, "0.00", "amount", "Amount");
    }

    void BindAccountStatus(DataRow accountRow, string userId)
    {
        if (accountRow != null)
        {
            string status = NormalizeStatus(GetRowValue(accountRow, "status", "Status", "SavingStatus"));
            string approveDate = GetRowValue(accountRow, "approvedate", "ApproveDate");

            if (IsApprovedStatus(status) || HasApprovedDate(approveDate, status))
            {
                lblsavingstatus.Text = "Active";
                lblsavingstatus.CssClass = "dash-status-badge is-active";
                return;
            }

            if (IsPendingStatus(status))
            {
                lblsavingstatus.Text = "Pending";
                lblsavingstatus.CssClass = "dash-status-badge is-inactive";
                return;
            }

            if (IsRejectedStatus(status))
            {
                lblsavingstatus.Text = "Rejected";
                lblsavingstatus.CssClass = "dash-status-badge is-inactive";
                return;
            }
        }

        BindUserSavingStatus(userId);
    }

    void BindUserSavingStatus(string userId)
    {
        DataTable dt = GetTotalIncomeData(userId);
        if (dt == null || dt.Rows.Count == 0 || IsErrorResult(dt) || !dt.Columns.Contains("SavingStatus"))
        {
            lblsavingstatus.Text = "-";
            lblsavingstatus.CssClass = "dash-status-badge is-inactive";
            return;
        }

        bool isActive = NormalizeStatus(GetRowValue(dt.Rows[0], "SavingStatus")) == "1";
        lblsavingstatus.Text = isActive ? "Active" : "Inactive";
        lblsavingstatus.CssClass = isActive ? "dash-status-badge is-active" : "dash-status-badge is-inactive";
    }

    static string NormalizeStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return string.Empty;
        }

        return status.Trim().ToLowerInvariant();
    }

    static bool IsApprovedStatus(string status)
    {
        return status == "1"
            || status == "approved"
            || status == "active"
            || status == "true";
    }

    static bool IsPendingStatus(string status)
    {
        return status == "0"
            || status == "pending";
    }

    static bool IsRejectedStatus(string status)
    {
        return status == "2"
            || status == "rejected"
            || status == "cancelled"
            || status == "canceled"
            || status == "deactive"
            || status == "inactive";
    }

    static bool HasApprovedDate(string approveDate, string status)
    {
        if (string.IsNullOrWhiteSpace(approveDate)
            || approveDate == "-"
            || approveDate == "0"
            || approveDate.StartsWith("01/01/0001", StringComparison.OrdinalIgnoreCase)
            || approveDate.StartsWith("0001-01-01", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return !IsPendingStatus(status) && !IsRejectedStatus(status);
    }

    DataTable GetTotalIncomeData(string userId)
    {
        try
        {
            SqlParameter[] parameter = {
                new SqlParameter("@UserId", userId)
            };
            DataSet ds = DBHelper.ExecuteQuery("sp_Totalincome", parameter);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
        }
        catch
        {
        }

        return null;
    }

    void ResetMetrics()
    {
        lbllevelincome.Text = "0";
        lbltotalemi.Text = "0.00";
        lblpendingemi.Text = "0.00";
        lblpaidemi.Text = "0.00";
        lblactivationdate.Text = "-";
        lblmaturitydate.Text = "-";
        lblmaturityamount.Text = "0.00";
    }

    DataRow GetSavingDashboardRow(string userId, string couponCode)
    {
        DataTable dt = GetSavingDashboardData(userId, couponCode);
        if (dt == null || dt.Rows.Count == 0 || IsErrorResult(dt))
        {
            return null;
        }

        DataRow matchedRow = FindRowByCoupon(dt, couponCode);
        if (matchedRow != null)
        {
            return matchedRow;
        }

        if (string.IsNullOrWhiteSpace(couponCode))
        {
            return dt.Rows[0];
        }

        return null;
    }

    DataRow GetAccountRow(string userId, string couponCode)
    {
        DataTable accounts = GetSavingAccounts(userId);
        if (accounts == null || accounts.Rows.Count == 0)
        {
            return null;
        }

        return FindRowByCoupon(accounts, couponCode);
    }

    DataTable GetSavingAccounts(string userId)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = @"SELECT sd.*, pm.productname
                FROM SavingAccountDetail sd WITH (NOLOCK)
                LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
                WHERE sd.userid = @UserId
                ORDER BY sd.entrydate DESC, sd.id DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId)
            };

            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTableParam(sql, parameters);
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

        return dt;
    }

    DataTable GetSavingDashboardData(string userId, string couponCode)
    {
        if (!string.IsNullOrWhiteSpace(couponCode))
        {
            DataTable withCoupon = ExecuteSavingDashboardSp(userId, couponCode, "CouponCode");
            if (withCoupon != null && withCoupon.Rows.Count > 0 && !IsErrorResult(withCoupon))
            {
                return withCoupon;
            }

            DataTable withOrderId = ExecuteSavingDashboardSp(userId, couponCode, "OrderId");
            if (withOrderId != null && withOrderId.Rows.Count > 0 && !IsErrorResult(withOrderId))
            {
                return withOrderId;
            }
        }

        return ExecuteSavingDashboardSp(userId, null, null);
    }

    DataTable ExecuteSavingDashboardSp(string userId, string couponCode, string couponParameterName)
    {
        try
        {
            SqlParameter[] parameters;
            if (string.IsNullOrWhiteSpace(couponCode) || string.IsNullOrWhiteSpace(couponParameterName))
            {
                parameters = new SqlParameter[] {
                    new SqlParameter("@UserId", userId)
                };
            }
            else
            {
                parameters = new SqlParameter[] {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@" + couponParameterName, couponCode)
                };
            }

            DataSet ds = DBHelper.ExecuteQuery("sp_GetSAvingDashboard", parameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
        }
        catch
        {
        }

        return null;
    }

    static DataRow FindRowByCoupon(DataTable dt, string couponCode)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(couponCode))
        {
            return dt.Rows[0];
        }

        foreach (DataRow row in dt.Rows)
        {
            if (RowMatchesCoupon(row, couponCode))
            {
                return row;
            }
        }

        return null;
    }

    static bool RowMatchesCoupon(DataRow row, string couponCode)
    {
        if (row == null || string.IsNullOrWhiteSpace(couponCode))
        {
            return false;
        }

        string coupon = GetRowValue(row, "couponcode", "CouponCode", "couponCode", "Couponcode");
        string orderId = GetRowValue(row, "orderid", "OrderId", "OrderID");
        string resolved = ResolveCouponCode(row);

        return string.Equals(coupon, couponCode, StringComparison.OrdinalIgnoreCase)
            || string.Equals(orderId, couponCode, StringComparison.OrdinalIgnoreCase)
            || string.Equals(resolved, couponCode, StringComparison.OrdinalIgnoreCase);
    }

    static string ResolveCouponCode(DataRow row)
    {
        if (row == null)
        {
            return string.Empty;
        }

        string couponCode = GetRowValue(row, "couponcode", "CouponCode", "couponCode", "Couponcode");
        if (!string.IsNullOrWhiteSpace(couponCode))
        {
            return couponCode.Trim();
        }

        return GetRowValue(row, "orderid", "OrderId", "OrderID");
    }

    static string GetRowValue(DataRow row, params string[] columnNames)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (string columnName in columnNames)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    string value = Convert.ToString(row[column], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return value.Trim();
                    }
                }
            }
        }

        return string.Empty;
    }

    static string GetRowValueOrDefault(DataRow row, string defaultValue, params string[] columnNames)
    {
        string value = GetRowValue(row, columnNames);
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    static string FormatDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value == "-")
        {
            return "-";
        }

        DateTime parsedDate;
        if (DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate)
            || DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            return parsedDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
        }

        return value;
    }

    static bool IsErrorResult(DataTable dt)
    {
        return dt != null
            && dt.Columns.Contains("Code")
            && dt.Columns.Contains("Remark")
            && dt.Rows.Count == 1
            && Convert.ToString(dt.Rows[0]["Code"]) == "0";
    }
}

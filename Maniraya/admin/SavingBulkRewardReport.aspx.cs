using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class admin_SavingBulkRewardReport : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureBulkInstallmentPaymentSchema();

        if (!IsPostBack)
        {
            LoadReport();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadReport();
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

        string status = (lblstatus.Text ?? string.Empty).Trim();
        lblstatus.CssClass = (status.Equals("Redeemable", StringComparison.OrdinalIgnoreCase)
            || status.Equals("Available", StringComparison.OrdinalIgnoreCase)
            || status.Equals("Redeemed", StringComparison.OrdinalIgnoreCase))
            ? "label label-success"
            : "label label-warning";
    }

    void LoadReport()
    {
        DataTable dt = GetReport();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        lblSummary.Text = dt.Rows.Count == 0
            ? "No bulk rewards found."
            : dt.Rows.Count + " reward(s) found.";
    }

    DataTable GetReport()
    {
        string extra = string.Empty;
        string fromSql = TryGetSqlDate(txtfromdate.Text);
        string toSql = TryGetSqlDate(txttodate.Text);
        if (!string.IsNullOrEmpty(fromSql))
        {
            extra += " AND CONVERT(date, x.EntryDate) >= CONVERT(date, '" + fromSql + "')";
        }
        if (!string.IsNullOrEmpty(toSql))
        {
            extra += " AND CONVERT(date, x.EntryDate) <= CONVERT(date, '" + toSql + "')";
        }
        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            extra += " AND LTRIM(RTRIM(x.UserId)) = '" + SqlEscape(txtuserid.Text.Trim()) + "'";
        }
        if (!string.IsNullOrWhiteSpace(txtcoupon.Text))
        {
            extra += " AND LTRIM(RTRIM(ISNULL(x.DisplayCouponCode, ''))) LIKE '%" + SqlEscape(txtcoupon.Text.Trim()) + "%'";
        }

        string rewardType = (ddRewardType.SelectedValue ?? string.Empty).Trim();
        if (!string.IsNullOrWhiteSpace(rewardType))
        {
            extra += " AND x.RewardType = '" + SqlEscape(rewardType) + "'";
        }

        string status = (ddstatus.SelectedValue ?? string.Empty).Trim();
        if (!string.IsNullOrWhiteSpace(status))
        {
            extra += " AND x.RewardStatus = '" + SqlEscape(status) + "'";
        }

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(SavingProductHelper.GetBulkRewardLinesSql(extra));
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

    static string TryGetSqlDate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        try
        {
            DateTime dt = Message.GetIndianDate(raw.Trim());
            if (dt.Year <= 1900)
            {
                return null;
            }
            return dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

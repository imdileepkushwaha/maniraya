using DataTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingBulkRewardReport : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureBulkInstallmentPaymentSchema();

        if (!IsPostBack)
        {
            txtuserid.Text = Session["userid"].ToString();
            LoadReport();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadReport();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!GridView1.AllowPaging)
        {
            return dataItemIndex + 1;
        }
        return (GridView1.PageIndex * GridView1.PageSize) + dataItemIndex + 1;
    }

    void ApplyPaging(DataTable dt)
    {
        int pageSize;
        if (ddlRecordFilter.SelectedItem.Text == "All" || !int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize) || pageSize <= 0)
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt != null && dt.Rows.Count > 0 ? dt.Rows.Count : 25;
            return;
        }

        GridView1.AllowPaging = true;
        GridView1.PageSize = pageSize;
        if (dt != null && dt.Rows.Count > 0)
        {
            int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }
    }

    void LoadReport()
    {
        string userId = Convert.ToString(Session["userid"]).Trim();
        txtuserid.Text = userId;

        string extra = " AND LTRIM(RTRIM(x.UserId)) = '" + SqlEscape(userId) + "'";
        if (!string.IsNullOrWhiteSpace(txtfromdate.Text))
        {
            DateTime fromDate = Message.GetIndianDate(txtfromdate.Text);
            if (fromDate.Year > 1900)
            {
                extra += " AND CONVERT(date, x.EntryDate) >= CONVERT(date, '" + fromDate.ToString("yyyy-MM-dd") + "')";
            }
        }
        if (!string.IsNullOrWhiteSpace(txttodate.Text))
        {
            DateTime toDate = Message.GetIndianDate(txttodate.Text);
            if (toDate.Year > 1900)
            {
                extra += " AND CONVERT(date, x.EntryDate) <= CONVERT(date, '" + toDate.ToString("yyyy-MM-dd") + "')";
            }
        }

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try { dt = ObjData.RunDataTable(SavingProductHelper.GetBulkRewardLinesSql(extra)) ?? new DataTable(); }
            finally { ObjData.EndConnection(); }
        }
        catch
        {
            dt = new DataTable();
        }

        ApplyPaging(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "RedeemCoupon")
        {
            return;
        }

        int rewardId;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out rewardId))
        {
            return;
        }

        string userId = Convert.ToString(Session["userid"]).Trim();
        SavingProductHelper.BulkCouponInfo coupon = SavingProductHelper.GetPendingBulkCoupon(userId, rewardId);
        if (coupon == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
                "alert('This coupon is not available to redeem.');", true);
            LoadReport();
            return;
        }

        SavingProductHelper.BeginCouponRedeem(Session, coupon.Id);
        Response.Redirect("FranchiseeSearchNew.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        LinkButton btnRedeem = (LinkButton)e.Row.FindControl("btnRedeem");
        HyperLink lnkPurchase = (HyperLink)e.Row.FindControl("lnkPurchase");
        Label lblLocked = (Label)e.Row.FindControl("lblLocked");

        string status = lblstatus != null ? (lblstatus.Text ?? string.Empty).Trim() : string.Empty;
        if (lblstatus != null)
        {
            if (status.Equals("Redeemed", StringComparison.OrdinalIgnoreCase) || status.Equals("Available", StringComparison.OrdinalIgnoreCase) || status.Equals("Redeemable", StringComparison.OrdinalIgnoreCase))
            {
                lblstatus.CssClass = "label label-success";
            }
            else
            {
                lblstatus.CssClass = "label label-warning";
            }
        }

        DataRowView row = e.Row.DataItem as DataRowView;
        bool canRedeem = GetBool(row, "CanRedeem");
        bool canPurchase = GetBool(row, "CanPurchase");
        if (btnRedeem != null) btnRedeem.Visible = canRedeem;
        if (lnkPurchase != null) lnkPurchase.Visible = canPurchase;
        if (lblLocked != null) lblLocked.Visible = !canRedeem && !canPurchase;
    }

    static bool GetBool(DataRowView row, string column)
    {
        if (row == null || !row.Row.Table.Columns.Contains(column) || row[column] == DBNull.Value)
        {
            return false;
        }

        int n;
        if (int.TryParse(Convert.ToString(row[column]), out n))
        {
            return n == 1;
        }

        bool b;
        return bool.TryParse(Convert.ToString(row[column]), out b) && b;
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

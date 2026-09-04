using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class admin_SavingBulkInstallmentPaymentReport : Page
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
        SavingProductHelper.ConfirmDueBulkInstallmentDeliveries();

        if (!IsPostBack)
        {
            if (ddstatus.Items.FindByText("Processing") != null)
            {
                ddstatus.ClearSelection();
                ddstatus.Items.FindByText("Processing").Selected = true;
            }
            LoadRequests(true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadRequests(true);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    void LoadRequests()
    {
        LoadRequests(false);
    }

    void LoadRequests(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

        DataTable dt = GetRequests();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        lblSummary.Text = dt.Rows.Count == 0
            ? "No bulk EMI payment requests found."
            : dt.Rows.Count + " request(s) found.";
    }

    DataTable GetRequests()
    {
        string sql = @"
SELECT
    bp.Id,
    bp.UserId,
    ud.username,
    bp.OrderId,
    bp.CouponCode,
    bp.Amount,
    bp.InstCount,
    bp.OnlineTransactionId,
    bp.ImageName,
    bp.Status,
    bp.RequestDate,
    bp.ApproveDate,
    bp.ApproveBy,
    bp.Remark,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(pm.ProductName, ''))), '') IS NOT NULL THEN LTRIM(RTRIM(pm.ProductName))
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname
FROM SavingBulkInstallmentPayment bp WITH (NOLOCK)
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = bp.UserId
LEFT JOIN SavingAccountDetail sd WITH (NOLOCK) ON sd.id = bp.AccountId
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON pm.id = sd.productid
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1 AND ISNULL(ipa.ProductId, 0) > 0 AND ipa.InstallmentNo = 1
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
WHERE 1 = 1";

        string fromSql = TryGetSqlDate(txtfromdate.Text);
        string toSql = TryGetSqlDate(txttodate.Text);
        if (!string.IsNullOrEmpty(fromSql))
        {
            sql += " AND CONVERT(date, bp.RequestDate) >= CONVERT(date, '" + fromSql + "')";
        }
        if (!string.IsNullOrEmpty(toSql))
        {
            sql += " AND CONVERT(date, bp.RequestDate) <= CONVERT(date, '" + toSql + "')";
        }
        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            sql += " AND LTRIM(RTRIM(bp.UserId)) = '" + SqlEscape(txtuserid.Text.Trim()) + "'";
        }
        if (!string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            sql += " AND LTRIM(RTRIM(ISNULL(bp.OnlineTransactionId, ''))) LIKE '%" + SqlEscape(txttransactionid.Text.Trim()) + "%'";
        }

        string selectedStatus = (ddstatus.SelectedValue ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(selectedStatus))
        {
            selectedStatus = "Processing";
        }
        sql += " AND UPPER(LTRIM(RTRIM(ISNULL(bp.Status, '')))) = '" + SqlEscape(selectedStatus.ToUpperInvariant()) + "'";
        sql += " ORDER BY bp.Id DESC";

        DataTable dt = new DataTable();
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

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        Label lblremark = (Label)e.Row.FindControl("lblremark");
        TextBox txtremark = (TextBox)e.Row.FindControl("txtremark");
        LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
        LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
        if (lblstatus == null)
        {
            return;
        }

        string status = (lblstatus.Text ?? string.Empty).Trim();
        bool isProcessing = status.Equals("Processing", StringComparison.OrdinalIgnoreCase);
        if (btnApprove != null) btnApprove.Visible = isProcessing;
        if (btnReject != null) btnReject.Visible = isProcessing;
        if (txtremark != null) txtremark.Visible = isProcessing;
        if (lblremark != null) lblremark.Visible = !isProcessing;

        if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "label label-success";
        }
        else if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "label label-danger";
        }
        else
        {
            lblstatus.CssClass = "label label-warning";
        }
    }

    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = ((Control)sender).NamingContainer as GridViewRow;
        if (gvRow == null)
        {
            return;
        }
        Label lblId = (Label)gvRow.FindControl("lblId");
        if (lblId == null)
        {
            return;
        }
        int requestId;
        if (!int.TryParse(lblId.Text, out requestId))
        {
            return;
        }
        TextBox txtremark = (TextBox)gvRow.FindControl("txtremark");
        string remark = txtremark != null ? txtremark.Text.Trim() : string.Empty;
        string res = SavingProductHelper.ExecuteScalarProc("sp_approveSavingBulkInstallmentPayment", new[]
        {
            new SqlParameter("@id", requestId),
            new SqlParameter("@Approveby", Convert.ToString(Session["useradmin"])),
            new SqlParameter("@Remark", remark)
        });

        ShowResult(res, "Bulk EMI payment approved. User received 20000 shopping point (redeem after 18 months) and 2000 coupon.");
    }

    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = ((Control)sender).NamingContainer as GridViewRow;
        if (gvRow == null)
        {
            return;
        }
        Label lblId = (Label)gvRow.FindControl("lblId");
        if (lblId == null)
        {
            return;
        }
        int requestId;
        if (!int.TryParse(lblId.Text, out requestId))
        {
            return;
        }
        TextBox txtremark = (TextBox)gvRow.FindControl("txtremark");
        string remark = txtremark != null ? txtremark.Text.Trim() : string.Empty;
        if (string.IsNullOrWhiteSpace(remark))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
                "alert('Please enter rejection reason. User will see this remark.');", true);
            return;
        }

        string res = SavingProductHelper.ExecuteScalarProc("sp_rejectSavingBulkInstallmentPayment", new[]
        {
            new SqlParameter("@id", requestId),
            new SqlParameter("@Approveby", Convert.ToString(Session["useradmin"])),
            new SqlParameter("@Remark", remark)
        });

        ShowResult(res, "Bulk EMI payment rejected. Reason will be visible to the user.");
    }

    void ShowResult(string res, string successMessage)
    {
        string alert;
        if (res == "t")
        {
            alert = successMessage;
        }
        else if (res == "f")
        {
            alert = "This request is already processed.";
        }
        else if (res == "r")
        {
            alert = "Please enter rejection reason.";
        }
        else
        {
            alert = "Unable to process request. Please try again.";
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
            "alert('" + alert.Replace("'", "\\'") + "');", true);
        LoadRequests();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "photolarge")
        {
            return;
        }

        int index;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out index) || index < 0 || index >= GridView1.Rows.Count)
        {
            return;
        }

        Label lblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
        string imageName = lblImage != null ? Convert.ToString(lblImage.Text).Trim() : string.Empty;
        ImageLarge.ImageUrl = string.IsNullOrWhiteSpace(imageName)
            ? "../ProductImage/noimage.png"
            : "../ProductImage/" + imageName;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAdminModal('DivPhotolarge');", true);
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

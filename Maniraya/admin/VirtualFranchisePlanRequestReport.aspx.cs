using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class admin_VirtualFranchisePlanRequestReport : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        try { VirtualFranchiseHelper.EnsureSchema(); }
        catch { }
        try { VirtualFranchiseHelper.ProcessDueRoi(); }
        catch { }

        if (!IsPostBack)
        {
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
            ? "No virtual franchise plan requests found."
            : dt.Rows.Count + " request(s) found.";
    }

    DataTable GetRequests()
    {
        string sql = @"
SELECT
    r.id,
    r.userid,
    ud.username,
    r.planname,
    r.planamount,
    r.monthlyroi,
    r.totalcashback,
    r.paymentmethod,
    r.onlinetransactionid,
    r.imagename,
    r.status,
    r.remark,
    r.entrydate,
    r.approvedate
FROM Virtual_Franchise_Request r WITH (NOLOCK)
LEFT JOIN UserDetail ud WITH (NOLOCK) ON LTRIM(RTRIM(ud.UserId)) = LTRIM(RTRIM(r.userid))
WHERE 1 = 1 ";

        if (!string.IsNullOrWhiteSpace(txtfromdate.Text) && !string.IsNullOrWhiteSpace(txttodate.Text))
        {
            DateTime fromDate = Message.GetIndianDate(txtfromdate.Text);
            DateTime toDate = Message.GetIndianDate(txttodate.Text);
            sql += " AND CONVERT(date, r.entrydate) >= CONVERT(date, '" + fromDate.ToString("yyyy-MM-dd") + "')"
                + " AND CONVERT(date, r.entrydate) <= CONVERT(date, '" + toDate.ToString("yyyy-MM-dd") + "')";
        }

        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            sql += " AND LTRIM(RTRIM(r.userid)) = '" + SqlEscape(txtuserid.Text.Trim()) + "'";
        }

        string status = ddstatus.SelectedValue;
        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            sql += " AND UPPER(LTRIM(RTRIM(ISNULL(r.status, '')))) = '" + SqlEscape(status.ToUpperInvariant()) + "'";
        }

        if (!string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            sql += " AND r.onlinetransactionid LIKE '%" + SqlEscape(txttransactionid.Text.Trim()) + "%'";
        }

        sql += " ORDER BY r.entrydate DESC";

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try { dt = ObjData.RunDataTable(sql) ?? new DataTable(); }
            finally { ObjData.EndConnection(); }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt;
    }

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
        LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
        TextBox txtremark = (TextBox)e.Row.FindControl("txtremark");
        Label lblremark = (Label)e.Row.FindControl("lblremark");

        if (lblstatus == null)
        {
            return;
        }

        string status = (lblstatus.Text ?? string.Empty).Trim();
        bool isPending = status.Equals("Pending", StringComparison.OrdinalIgnoreCase);
        if (btnApprove != null) btnApprove.Visible = isPending;
        if (btnReject != null) btnReject.Visible = isPending;
        if (txtremark != null) txtremark.Visible = isPending;
        if (lblremark != null) lblremark.Visible = !isPending;

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
        string res = VirtualFranchiseHelper.ExecuteScalarProc("sp_approve_VirtualFranchiseRequest", new[]
        {
            new SqlParameter("@id", requestId),
            new SqlParameter("@Approveby", Convert.ToString(Session["useradmin"])),
            new SqlParameter("@Remark", remark)
        });

        ShowResult(res, "Plan request approved. 40-month ROI generated and Level 1 income credited after admin and TDS charges.");
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

        string res = VirtualFranchiseHelper.ExecuteScalarProc("sp_reject_VirtualFranchiseRequest", new[]
        {
            new SqlParameter("@id", requestId),
            new SqlParameter("@Approveby", Convert.ToString(Session["useradmin"])),
            new SqlParameter("@Remark", remark)
        });

        ShowResult(res, "Plan request rejected. Reason will be visible to the user.");
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

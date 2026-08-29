using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;
using DataTier;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                if (ddstatus.Items.FindByText("Processing") != null)
                {
                    ddstatus.ClearSelection();
                    ddstatus.Items.FindByText("Processing").Selected = true;
                }
                ViewState["HasSearched"] = true;
                loadprevproduct(true);
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ViewState["HasSearched"] = true;
        loadprevproduct(true);
    }

    public DataTable getPrevProduct()
    {
        bool hasInstCoupon = SavingProductHelper.HasInstallmentCouponCodeColumn();
        string couponSelect = hasInstCoupon
            ? "COALESCE(NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), ''), sd.couponcode) AS couponcodedisplay"
            : "sd.couponcode AS couponcodedisplay";
        string parentMatch = hasInstCoupon
            ? @" AND (
                    (
                        NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), '') IS NOT NULL
                        AND LTRIM(RTRIM(ISNULL(sd0.couponcode, ''))) = LTRIM(RTRIM(sa.CouponCode))
                    )
                    OR (
                        NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), '') IS NULL
                        AND sa.OrderId = sd0.orderid
                    )
                )"
            : " AND sa.OrderId = sd0.orderid";

        SavingProductHelper.EnsureInstallmentProductAssignTable();

        string str_query = @"SELECT sa.*, ud.username, " + couponSelect + @",
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ISNULL(assign_pm.ProductName, ''))), '') IS NOT NULL
            THEN LTRIM(RTRIM(assign_pm.ProductName))
        ELSE 'Not assigned'
    END AS productname
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
OUTER APPLY (
    SELECT TOP 1 sd0.couponcode
    FROM SavingAccountDetail sd0 WITH (NOLOCK)
    WHERE LTRIM(RTRIM(sd0.UserId)) = LTRIM(RTRIM(sa.UserId))
    " + parentMatch + @"
    ORDER BY sd0.id DESC
) sd
LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
    ON ISNULL(ipa.Status, 1) = 1
   AND ISNULL(ipa.ProductId, 0) > 0
   AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
LEFT JOIN SavingProductMaster assign_pm WITH (NOLOCK) ON assign_pm.id = ipa.ProductId
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
WHERE LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('processing', 'approved', 'rejected', '1', 'active', '2', 'cancelled', 'canceled') ";

        // Date filter: use request / installment / entry date (whichever is available)
        string dateExpr = "CONVERT(date, COALESCE(sa.requestdate, sa.installmentdate, sa.entrydate))";
        string fromSql = TryGetSqlDate(txtfromdate.Text);
        string toSql = TryGetSqlDate(txttodate.Text);
        if (!string.IsNullOrEmpty(fromSql))
        {
            str_query += " AND " + dateExpr + " >= CONVERT(date, '" + fromSql + "') ";
        }
        if (!string.IsNullOrEmpty(toSql))
        {
            str_query += " AND " + dateExpr + " <= CONVERT(date, '" + toSql + "') ";
        }

        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            str_query += " AND LTRIM(RTRIM(sa.UserId)) = '" + SqlEscape(txtuserid.Text.Trim()) + "' ";
        }

        if (!string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            str_query += " AND LTRIM(RTRIM(ISNULL(sa.OnlineTransactionId, ''))) LIKE '%" + SqlEscape(txttransactionid.Text.Trim()) + "%' ";
        }

        string selectedStatus = ddstatus.SelectedValue;
        if (string.IsNullOrWhiteSpace(selectedStatus))
        {
            selectedStatus = "Processing";
        }
        str_query += " AND " + BuildInstallmentStatusFilter(selectedStatus) + " ";

        str_query += " ORDER BY ISNULL(sa.entrydate, sa.requestdate) DESC, sa.id DESC";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }
        return dt;
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
            return dt.ToString("yyyy-MM-dd");
        }
        catch
        {
            return null;
        }
    }

    static string BuildInstallmentStatusFilter(string selectedStatus)
    {
        string status = (selectedStatus ?? string.Empty).Trim();

        if (string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            return @"LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('approved', '1', 'active')";
        }

        if (string.Equals(status, "Rejected", StringComparison.OrdinalIgnoreCase))
        {
            return @"LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('rejected', '2', 'cancelled', 'canceled')";
        }

        // Default / Processing
        return @"LOWER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('processing')";
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    void loadprevproduct()
    {
        loadprevproduct(false);
    }

    void loadprevproduct(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

        DataTable dt = getPrevProduct();
        BindGrid(dt);
    }

    void BindGrid(DataTable dt)
    {
        if (dt == null)
        {
            dt = new DataTable();
        }

        int totalRecords = dt.Rows.Count;
        int pageSize = GetPageSize();
        bool showAll = pageSize <= 0 || string.Equals(ddlRecordFilter.SelectedItem.Text, "All", StringComparison.OrdinalIgnoreCase);

        if (showAll || totalRecords == 0)
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = Math.Max(totalRecords, 1);
            if (showAll)
            {
                GridView1.PageIndex = 0;
            }
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (totalRecords == 0)
        {
            lblSummary.Text = "No installment requests found for selected filters.";
        }
        else
        {
            int fromRecord = 1;
            int toRecord = totalRecords;
            if (GridView1.AllowPaging)
            {
                fromRecord = (GridView1.PageIndex * GridView1.PageSize) + 1;
                toRecord = Math.Min(totalRecords, (GridView1.PageIndex + 1) * GridView1.PageSize);
            }
            lblSummary.Text = "Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + " record(s)";
        }

        BuildExternalPager(totalRecords);
    }

    int GetPageSize()
    {
        int pageSize;
        if (ddlRecordFilter != null && int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }
        return 25;
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loadprevproduct(true);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loadprevproduct(false);
    }

    void BuildExternalPager(int totalRecords)
    {
        pnlPager.Controls.Clear();

        if (!GridView1.AllowPaging || totalRecords <= 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
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
            "<span class=\"admin-pager-info\">Page " + (currentPage + 1) + " of " + totalPages
            + " · Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
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
        link.ID = "pagerBtn_" + pageIndex + "_" + text.Replace(" ", "");
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.Click += ExternalPager_Click;
        link.CausesValidation = false;
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
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loadprevproduct(false);
    }
    public DataTable getWithdrawlRequest(clsAccount objaccount)
    {
        string s1 = "select isnull(CashWalletPercent,0) as CashWalletPercent from tbl_Deduction";
        ObjData.StartConnection();
        DataTable dt1 = ObjData.RunDataTable(s1);
        ObjData.EndConnection();
        decimal deductionPercent = Convert.ToDecimal(dt1.Rows[0]["CashWalletPercent"].ToString());

        string str_query = "select wr.*,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Cash Wallet' when requesttype='U' then 'ProfitShare wallet' else 'Wallet' end as RequestType1,ud.mobile, bm.BankName, ud.AccountNo, ud.IFSCCode, ud.phonepay, ud.bhimno, ud.upino from withdrawlrequest wr LEFT JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId Left Join BankMaster bm on ud.BankName=bm.BankId where 1=1  ";


        if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
        {
            str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
        }



        if (objaccount.WithdrawlRequestStatus != "0")
        {
            str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
        }

        if (objaccount.UserId != "")
        {
            str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
        }


        str_query += " order by wr.mentiondate  desc";



        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProductName = (Label)e.Row.FindControl("lblamount");
            if (lblProductName != null)
            {
                string productName = (lblProductName.Text ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(productName)
                    || productName.Equals("Not assigned", StringComparison.OrdinalIgnoreCase)
                    || productName.Equals("Not assign", StringComparison.OrdinalIgnoreCase))
                {
                    lblProductName.Text = "Not assigned";
                }
            }

            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            Label lblremark = (Label)e.Row.FindControl("lblremark");
            TextBox txtremark = (TextBox)e.Row.FindControl("txtremark");
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            lblremark.Visible = false;
            txtremark.Visible = false;

            if (lblstatus.Text == "Pending")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-warning";
                btnApprove.Visible = false;
                btnReject.Visible = false;
                lblremark.Visible = true;
            }
            else if (lblstatus.Text == "Processing")
            {
                lblstatus.Text = "Processing";
                lblstatus.CssClass = "label label-info";
                btnApprove.Visible = true;
                btnReject.Visible = true;
                txtremark.Visible = true;
            }
            else
                if (lblstatus.Text == "Approved")
            {
                lblstatus.Text = "Approved";
                lblstatus.CssClass = "label label-success";
                btnApprove.Visible = false;
                btnReject.Visible = false;
                lblremark.Visible = true;
            }
            else

                    if (lblstatus.Text == "Rejected")
            {
                lblstatus.Text = "Cancelled";
                lblstatus.CssClass = "label label-danger";
                btnApprove.Visible = false;
                btnReject.Visible = false;
                lblremark.Visible = true;
            }

        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");


        TextBox txtremark = (TextBox)gvRow.FindControl("txtremark");



        string res = Approve_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString(), txtremark.Text);
        if (res == "t")
        {
            string waStatus;
            TrySendWhatsAppInvoice(gvRow, ChatwayWhatsAppHelper.InvoiceMessageType.Reinstallment, out waStatus);
            string popupScript = "alert('Purchase Approved Successfully" + FormatWhatsAppAlertSuffix(waStatus) + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }
        else if (res == "f")
        {

            string popupScript = "alert('Purchase Already Processed');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }
        else
        {
            string popupScript = "alert('Something wrong ');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }
        loadprevproduct();
    }

    public string Approve_ProductPurchase(string str_id, string str_approveby, string str_remark)
    {


        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_approveSavingInstallmentDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@id",str_id),
                new SqlParameter("@Approveby",str_approveby),
                new SqlParameter("@Remark",str_remark),


                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
    public string Reject_ProductPurchase(string str_id, string str_approveby, string str_remark)
    {


        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_RejectSavingInstallmentDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@id",str_id),
                new SqlParameter("@Approveby",str_approveby),
                new SqlParameter("@Remark",str_remark),


                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
    Data ObjData = new Data();

    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");

        TextBox txtremark = (TextBox)gvRow.FindControl("txtremark");
        string res = Reject_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString(), txtremark.Text);
        if (res == "t")
        {

            string popupScript = "alert('Purchase Rejected Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }
        else if (res == "f")
        {

            string popupScript = "alert('Purchase Already Processed');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }
        else
        {
            string popupScript = "alert('Something wrong ');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

        }

        loadprevproduct();

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "photolarge")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            ImageLarge.ImageUrl = "../ProductImage/" + LblImage.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAdminModal('DivPhotolarge');", true);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {

    }
    protected void chckchanged(object sender, EventArgs e)

    {

        CheckBox chckheader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox1");

        foreach (GridViewRow row in GridView1.Rows)

        {

            CheckBox chckrw = (CheckBox)row.FindControl("CheckBox2");

            if (chckheader.Checked == true)

            {
                chckrw.Checked = true;
            }
            else

            {
                chckrw.Checked = false;
            }

        }

    }
    protected void btnPayAll_Click(object sender, EventArgs e)
    {
        int whatsAppSent = 0;
        int whatsAppFailed = 0;

        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox CheckBox2 = (CheckBox)r.FindControl("CheckBox2");
            if (CheckBox2.Checked == true)
            {
                Label lblgalleryid = (Label)r.FindControl("lblId");
                TextBox txtremark = (TextBox)r.FindControl("txtremark");

                string res = Approve_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString(), txtremark.Text);
                if (res == "t")
                {
                    string waStatus;
                    if (TrySendWhatsAppInvoice(r, ChatwayWhatsAppHelper.InvoiceMessageType.Reinstallment, out waStatus))
                    {
                        whatsAppSent++;
                    }
                    else if (ChatwayWhatsAppHelper.IsEnabled)
                    {
                        whatsAppFailed++;
                    }
                }
            }
        }

        string alertText = "Purchase Approved Successfully";
        if (ChatwayWhatsAppHelper.IsEnabled)
        {
            alertText += ". WhatsApp queued: " + whatsAppSent;
            if (whatsAppFailed > 0)
            {
                alertText += ", failed: " + whatsAppFailed;
            }
        }

        string popupScript = "alert('" + alertText.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        loadprevproduct();
    }

    bool TrySendWhatsAppInvoice(GridViewRow row, ChatwayWhatsAppHelper.InvoiceMessageType messageType, out string statusMessage)
    {
        statusMessage = string.Empty;
        if (row == null)
        {
            statusMessage = "Row missing.";
            return false;
        }

        Label lblInstallmentId = (Label)row.FindControl("lblId");
        Label lblUserId = (Label)row.FindControl("lblurrorderid");
        Label lblOrderId = (Label)row.FindControl("lblorderid");
        string userId = lblUserId != null ? Convert.ToString(lblUserId.Text).Trim() : string.Empty;
        string orderId = lblOrderId != null ? Convert.ToString(lblOrderId.Text).Trim() : string.Empty;
        int installmentId = 0;
        if (lblInstallmentId != null)
        {
            int.TryParse(Convert.ToString(lblInstallmentId.Text).Trim(), out installmentId);
        }

        // Installment-scoped invoice uses SavingAccountInstallmentDetail.productid.
        return ChatwayWhatsAppHelper.TrySendInvoiceAfterApprove(userId, orderId, installmentId, messageType, out statusMessage);
    }

    static string FormatWhatsAppAlertSuffix(string waStatus)
    {
        if (string.IsNullOrWhiteSpace(waStatus) || !ChatwayWhatsAppHelper.IsEnabled)
        {
            return string.Empty;
        }

        return ". " + waStatus.Replace("'", "");
    }
}
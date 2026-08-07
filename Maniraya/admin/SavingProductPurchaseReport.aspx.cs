using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
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
        string str_query = @"SELECT ud.username, sd.*,pm.productname FROM SavingAccountDetail sd WITH (nolock) LEFT JOIN savingproductmaster pm WITH (nolock) ON sd.productid=pm.id left join userdetail ud with(nolock) on ud.userid=sd.userid where 1=1 ";
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            str_query += "  and convert(date, sd.entrydate)  >= convert(date,'" + Message.GetIndianDate(txtfromdate.Text) + "' )  and convert(date,sd.entrydate  ) <= convert(date,'" + Message.GetIndianDate(txttodate.Text) + "') ";
        }



        if (ddstatus.SelectedValue.ToString() != "0")
        {
            str_query += "  and " + BuildSavingStatusFilter(ddstatus.SelectedValue.ToString()) + " ";
        }

        if (txtuserid.Text != "")
        {
            str_query += "  and sd.UserId = '" + SqlEscape(txtuserid.Text.Trim()) + "' ";
        }

        if (!string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            str_query += "  and sd.OnlineTransactionId LIKE '%" + SqlEscape(txttransactionid.Text.Trim()) + "%' ";
        }

        str_query += " order by sd.entrydate  desc";
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
            lblSummary.Text = "No purchase requests found for selected filters.";
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
    static string BuildSavingStatusFilter(string selectedStatus)
    {
        string status = (selectedStatus ?? string.Empty).Trim();
        if (string.Equals(status, "Pending", StringComparison.OrdinalIgnoreCase))
        {
            return @"LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('pending', '0')
                OR sd.status IS NULL OR LTRIM(RTRIM(ISNULL(sd.status, ''))) = ''";
        }

        if (string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            return @"LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', '1', 'active')";
        }

        if (string.Equals(status, "Rejected", StringComparison.OrdinalIgnoreCase))
        {
            return @"LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('rejected', '2', 'cancelled', 'canceled')";
        }

        return "sd.status = '" + SqlEscape(status) + "'";
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static void ApplySavingStatusBadge(Label lblstatus, LinkButton btnApprove, LinkButton btnReject, Label lblremark, TextBox txtremark)
    {
        if (lblstatus == null)
        {
            return;
        }

        string status = (lblstatus.Text ?? string.Empty).Trim();
        string normalized = status.ToLowerInvariant();

        bool isPending = normalized == "pending" || normalized == "0" || normalized == string.Empty;
        bool isApproved = normalized == "approved" || normalized == "1" || normalized == "active";
        bool isRejected = normalized == "rejected" || normalized == "2" || normalized == "cancelled" || normalized == "canceled";

        if (isPending)
        {
            lblstatus.Text = "Pending";
            lblstatus.CssClass = "label label-warning";
            if (btnApprove != null) btnApprove.Visible = true;
            if (btnReject != null) btnReject.Visible = true;
            if (txtremark != null) txtremark.Visible = true;
            if (lblremark != null) lblremark.Visible = false;
            return;
        }

        if (isApproved)
        {
            lblstatus.Text = "Approved";
            lblstatus.CssClass = "label label-success";
            if (btnApprove != null) btnApprove.Visible = false;
            if (btnReject != null) btnReject.Visible = false;
            if (txtremark != null) txtremark.Visible = false;
            if (lblremark != null) lblremark.Visible = true;
            return;
        }

        if (isRejected)
        {
            lblstatus.Text = "Cancelled";
            lblstatus.CssClass = "label label-danger";
            if (btnApprove != null) btnApprove.Visible = false;
            if (btnReject != null) btnReject.Visible = false;
            if (txtremark != null) txtremark.Visible = false;
            if (lblremark != null) lblremark.Visible = true;
        }
    }

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            Label lblremark = (Label)e.Row.FindControl("lblremark");
            TextBox txtremark = (TextBox)e.Row.FindControl("txtremark");
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");

            ApplySavingStatusBadge(lblstatus, btnApprove, btnReject, lblremark, txtremark);
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
            TrySendWhatsAppInvoice(gvRow, ChatwayWhatsAppHelper.InvoiceMessageType.FirstPurchase, out waStatus);
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
            s2 = "sp_approveSavingAccountDetail";
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
            s2 = "sp_rejectSavingAccountDetail";
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
                    if (TrySendWhatsAppInvoice(r, ChatwayWhatsAppHelper.InvoiceMessageType.FirstPurchase, out waStatus))
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

        Label lblUserId = (Label)row.FindControl("lblurrorderid");
        Label lblOrderId = (Label)row.FindControl("lblorderid");
        string userId = lblUserId != null ? Convert.ToString(lblUserId.Text).Trim() : string.Empty;
        string orderId = lblOrderId != null ? Convert.ToString(lblOrderId.Text).Trim() : string.Empty;

        return ChatwayWhatsAppHelper.TrySendInvoiceAfterApprove(userId, orderId, messageType, out statusMessage);
    }

    static string FormatWhatsAppAlertSuffix(string waStatus)
    {
        if (string.IsNullOrWhiteSpace(waStatus) || !ChatwayWhatsAppHelper.IsEnabled)
        {
            return string.Empty;
        }

        return ". " + waStatus.Replace("'", "");
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = getPrevProduct();
        if (dt == null || dt.Rows.Count == 0)
        {
            Message.Show("No records available to export.");
            return;
        }

        DataTable exportDt = BuildPurchaseExportTable(dt);
        GridView exportGrid = new GridView();
        exportGrid.AutoGenerateColumns = true;
        exportGrid.DataSource = exportDt;
        exportGrid.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=SavingProductPurchaseReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            exportGrid.RenderControl(hw);
            Response.Write("<style> td { mso-number-format:\\@; } </style>");
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    static DataTable BuildPurchaseExportTable(DataTable source)
    {
        DataTable export = new DataTable();
        export.Columns.Add("Order Id", typeof(string));
        export.Columns.Add("User Id", typeof(string));
        export.Columns.Add("Name", typeof(string));
        export.Columns.Add("Date of Request", typeof(string));
        export.Columns.Add("Approve Date", typeof(string));
        export.Columns.Add("Amount", typeof(string));
        export.Columns.Add("Product", typeof(string));
        export.Columns.Add("Transaction Id", typeof(string));
        export.Columns.Add("Status", typeof(string));
        export.Columns.Add("Remark", typeof(string));

        foreach (DataRow row in source.Rows)
        {
            DataRow outRow = export.NewRow();
            outRow["Order Id"] = Convert.ToString(row["orderid"]);
            outRow["User Id"] = Convert.ToString(row["userid"]);
            outRow["Name"] = Convert.ToString(row["username"]);
            outRow["Date of Request"] = FormatExportDate(row["entrydate"]);
            outRow["Approve Date"] = FormatExportDate(row["approvedate"]);
            outRow["Amount"] = Convert.ToString(row["amount"]);
            outRow["Product"] = Convert.ToString(row["productname"]);
            outRow["Transaction Id"] = Convert.ToString(row["OnlineTransactionId"]);
            outRow["Status"] = Convert.ToString(row["status"]);
            outRow["Remark"] = Convert.ToString(row["remark"]);
            export.Rows.Add(outRow);
        }

        return export;
    }

    static string FormatExportDate(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return string.Empty;
        }

        DateTime dt;
        if (DateTime.TryParse(Convert.ToString(value), out dt))
        {
            return dt.ToString("dd/MM/yyyy hh:mm tt");
        }

        return Convert.ToString(value);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
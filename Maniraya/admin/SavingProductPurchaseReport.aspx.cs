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


            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadprevproduct();
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

        DataTable dt = new DataTable();
        dt = getPrevProduct();
        GridView1.DataSource = dt;
        GridView1.DataBind();
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
}
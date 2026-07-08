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
        string str_query = @"SELECT sa.*, ud.username, sd.couponcode,pm.productname FROM SavingAccountInstallmentDetail sa WITH (nolock) LEFT JOIN  SavingAccountDetail sd WITH (nolock) ON sa.OrderId=sd.orderid LEFT JOIN savingproductmaster pm WITH (nolock) ON sd.productid=pm.id left join userdetail ud with(nolock) on ud.userid=sd.userid where 1=1 and sa.status = 'Processing' ";
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            str_query += "  and convert(date, sa.requestdate)  >= convert(date,'" + Message.GetIndianDate(txtfromdate.Text) + "' )  and convert(date,sa.requestdate  ) <= convert(date,'" + Message.GetIndianDate(txttodate.Text) + "') ";
        }
        if (txtuserid.Text != "")
        {
            str_query += "  and sa.UserId = '" + txtuserid.Text.Trim().Replace("'", "''") + "' ";
        }

        if (!string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            str_query += "  and sa.OnlineTransactionId LIKE '%" + txttransactionid.Text.Trim().Replace("'", "''") + "%' ";
        }

        str_query += " order by sa.entrydate  desc";
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
    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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

            string popupScript = "alert('Purchase Approved Successfully');";
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
        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox CheckBox2 = (CheckBox)r.FindControl("CheckBox2");
            if (CheckBox2.Checked == true)
            {

                Label lblgalleryid = (Label)r.FindControl("lblId");


                TextBox txtremark = (TextBox)r.FindControl("txtremark");



                string res = Approve_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString(), txtremark.Text);
                //if (res == "t")
                //{

                //    string popupScript = "alert('Purchase Approved Successfully');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //else if (res == "f")
                //{

                //    string popupScript = "alert('Purchase Already Processed');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //else
                //{
                //    string popupScript = "alert('Something wrong ');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //loadprevproduct();
            }
        }
       
            string popupScript = "alert('Purchase Approved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadprevproduct();

       
    }
}
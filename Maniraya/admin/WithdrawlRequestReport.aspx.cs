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
        loadgethelp();
    }
    void loadgethelp()
    {
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            objaccount.FromDate = Message.GetIndianDate(txtfromdate.Text);
            objaccount.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objaccount.FromDate = DateTime.MinValue;
            objaccount.ToDate = DateTime.MinValue;
        }
        objaccount.WithdrawlRequestStatus = ddstatus.SelectedValue.ToString();
        DataTable dt = new DataTable();
        objaccount.UserId = txtuserid.Text;
        dt = getWithdrawlRequest(objaccount);
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
            Label lbltransactionid = (Label)e.Row.FindControl("lbltransactionid");
            TextBox txttransactionid = (TextBox)e.Row.FindControl("txttransactionid");

            DropDownList ddmode = (DropDownList)e.Row.FindControl("ddmode");
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            if (lblstatus.Text == "Pending")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-warning";
                btnApprove.Visible = true;
                btnReject.Visible = true;
                ddmode.Visible = true;
                txttransactionid.Visible = true;
                lbltransactionid.Visible = false;
            }
            else
                if (lblstatus.Text == "Approved")
                {
                    lblstatus.Text = "Approved";
                    lblstatus.CssClass = "label label-success";
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    ddmode.Visible = false;
                    txttransactionid.Visible = false;
                    lbltransactionid.Visible = true;
                }
                else

                    if (lblstatus.Text == "Rejected")
                    {
                        lblstatus.Text = "Cancelled";
                        lblstatus.CssClass = "label label-danger";
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        ddmode.Visible = false;
                        txttransactionid.Visible = false;
                        lbltransactionid.Visible = false;
                    }

        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        Label lblamount = (Label)gvRow.FindControl("lblamount");
        Label lbluserid = (Label)gvRow.FindControl("lbluserid");
        Label lblmobile = (Label)gvRow.FindControl("lblmobile");
        Label lblRequesttype = (Label)gvRow.FindControl("LblRtype");
        TextBox txttransactionid = (TextBox)gvRow.FindControl("txttransactionid");
        DropDownList ddpaymentmode = (DropDownList)gvRow.FindControl("ddmode");

        objaccount.UserId = lbluserid.Text;
        objaccount.WithdrawlAmount = Convert.ToDecimal(lblamount.Text);

        objaccount.PaymentMode = ddpaymentmode.SelectedValue.ToString();
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        objaccount.OnlineTransactionId = txttransactionid.Text;
        objaccount.DepositrequestRequestType = lblRequesttype.Text;
        objaccount.WithdrawlAccountType = "A";
        objaccount.UserMObile = lblmobile.Text;
        if (ddpaymentmode.SelectedIndex == 0)
        {
            string popupScript = "alert('Please Select Payment Mode');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (txttransactionid.Text == "")
        {
            string popupScript = "alert('Please Enter Transaction ID');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        DataTable Dt = Approve_WithdrawlRequestNew(objaccount);
        if (Dt != null)
        {

            string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }
        else
        {
            string popupScript = "alert('Something wrong ');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }
        loadgethelp();
    }
    Data ObjData = new Data();
    public DataTable Approve_WithdrawlRequestNew(clsAccount objaccount)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_addWithdrawlRequestApprove";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount),                
                new SqlParameter("@PaymentMode",objaccount.PaymentMode),
                 new SqlParameter("@WithdrawlRequestId",objaccount.WithdrawlRequestId),
                  new SqlParameter("@MentionBy",objaccount.MentionBy),
                     new SqlParameter("@OnlineTransactionId",objaccount.OnlineTransactionId),
                new SqlParameter("@RequestType",objaccount.DepositrequestRequestType),
                   new SqlParameter("@status",objaccount.WithdrawlAccountType),
              
                     
                };
            Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

            if (Dt != null && Dt.Rows[0][0].ToString() == "1")
            {
                tr.Commit();
                //s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                //DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                //if (Dtmsms.Rows.Count > 0)
                //{
                //    string MobileNo = "";
                //    string BalanceAmount = "";
                //    MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                //    if (objaccount.DepositrequestRequestType == "R")
                //    {
                //        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                //    }
                //    if (objaccount.DepositrequestRequestType == "U")
                //    {
                //        BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                //    }
                //    string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                //    string Result = url.CallURL();
                //    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                //}
            }


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
        return Dt;
    }
    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        Label lblamount = (Label)gvRow.FindControl("lblswamount");
        Label lbluserid = (Label)gvRow.FindControl("lbluserid");
        Label lblmobile = (Label)gvRow.FindControl("lblmobile");
        Label lblRequesttype = (Label)gvRow.FindControl("LblRtype");
        TextBox txttransactionid = (TextBox)gvRow.FindControl("txttransactionid");
        DropDownList ddpaymentmode = (DropDownList)gvRow.FindControl("ddmode");

        objaccount.UserId = lbluserid.Text;
        objaccount.WithdrawlAmount = Convert.ToDecimal(lblamount.Text);

        objaccount.PaymentMode = "Other";
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        objaccount.OnlineTransactionId = "123";
        objaccount.DepositrequestRequestType = lblRequesttype.Text;
        objaccount.WithdrawlAccountType = "R";
        objaccount.UserMObile = lblmobile.Text;
        //if (ddpaymentmode.SelectedIndex == 0)
        //{
        //    string popupScript = "alert('Please Select Payment Mode');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    return;
        //}
        //if (txttransactionid.Text == "")
        //{
        //    string popupScript = "alert('Please Enter Transaction ID');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    return;
        //}

        DataTable Dt = Approve_WithdrawlRequestNew(objaccount);
        if (Dt != null)
        {

            string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }
        else
        {
            string popupScript = "alert('Something wrong ');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }

        loadgethelp();

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
            ImageLarge.ImageUrl = LblImage.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }
    }
}
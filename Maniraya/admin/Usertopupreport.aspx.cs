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
public partial class Usertopupreport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {

                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
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
        //if (txtFromDate.Text != "" && txtFromDate.Text != "")
        //{
        //    objaccount.FromDate = Message.GetIndianDate(txtFromDate.Text);
        //    objaccount.ToDate = Message.GetIndianDate(txttodate.Text);
        //}
        //else
        //{
        //    objaccount.FromDate = DateTime.MinValue;
        //    objaccount.ToDate = DateTime.MinValue;
        //}
        objaccount.WithdrawlRequestStatus = ddstatus.SelectedValue.ToString();
        DataTable dt = new DataTable();
        objaccount.UserId = txtuserid.Text;
        dt = getSalesReport(objaccount,txtFromDate.Text,txtToDate.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }


    public DataTable getSalesReport(clsAccount objaccount,string fromdate,string todate)
    {

        string str_query = "SELECT U.Userid,U.entrydate,U.PlanAmount,P.username FROM UserTOPUPtb U JOIN userdetail P ON U.userid=P.userid WHERE 1=1  ";


        if (fromdate != string.Empty && todate != string.Empty)
        {
            str_query += "and   cast(U.entrydate as date)  >= cast('" + fromdate + "' as date)   and cast(U.entrydate as date)   <= cast('" + todate + "' as date) ";
        }
       
     
        if (objaccount.UserId != "")
        {
            str_query += "  and U.UserId = '" + objaccount.UserId + "' ";
        }   


        str_query += " order by U.ID DESC ";



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

          
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            

        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        TextBox txttransactionid = (TextBox)gvRow.FindControl("txttransactionid");
        objaccount.WithdrawlAccountType = "A";
        objaccount.OnlineTransactionId = txttransactionid.Text;
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
       
        if (txttransactionid.Text == "")
        {
            string popupScript = "alert('Please Enter Comment');";
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
            s2 = "sp_OrderProductApprove";
            SqlParameter[] parameter = {                                              
               
               
               
                 new SqlParameter("@WithdrawlRequestId",objaccount.WithdrawlRequestId),
                
                     new SqlParameter("@OnlineTransactionId",objaccount.OnlineTransactionId),
                
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
        TextBox txttransactionid = (TextBox)gvRow.FindControl("txttransactionid");
        objaccount.WithdrawlAccountType = "R";
        objaccount.OnlineTransactionId = txttransactionid.Text;
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
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
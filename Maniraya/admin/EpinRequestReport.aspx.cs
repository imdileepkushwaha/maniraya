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

public partial class EpinRequestReport : System.Web.UI.Page
{
    Data ObjData = new Data();
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
        objaccount.DepositrequestTo = Session["useradmin"].ToString();
        dt = getTopupRequestnew(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    public DataTable getTopupRequestnew(clsAccount objaccount)
    {
        string str_query = "select wr.*,CA.AccountNo,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo,ud.mobile  from epinRequest wr inner JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


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
        str_query += " and wr.requestto = '" + objaccount.DepositrequestTo + "'";

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
            Label lbltransactionid=(Label)e.Row.FindControl("lbltransactionid");
            Label requestType = (Label)e.Row.FindControl("LblRequestType");
            //TextBox txttransactionid = (TextBox)e.Row.FindControl("txttransactionid");
            //DropDownList ddmode = (DropDownList)e.Row.FindControl("ddmode");
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            if (lblstatus.Text == "Pending")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-warning";
                btnApprove.Visible = true;
                btnReject.Visible = true;
               // ddmode.Visible = true;
                //txttransactionid.Visible = true;
                //lbltransactionid.Visible = false;
            }
            else
                if (lblstatus.Text == "Approved")
                {
                    lblstatus.Text = "Approved";
                    lblstatus.CssClass = "label label-success";
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                   // ddmode.Visible = false;
                   // txttransactionid.Visible = false;
                   // lbltransactionid.Visible = true;
                }
                else

                    if (lblstatus.Text == "Rejected")
                    {
                        lblstatus.Text = "Rejected";
                        lblstatus.CssClass = "label label-danger";
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        //ddmode.Visible = false;
                        //txttransactionid.Visible = false;
                        //lbltransactionid.Visible = false;
                    }

        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        Label lblamount = (Label)gvRow.FindControl("lblamount");
        Label lblmainamount = (Label)gvRow.FindControl("lblmainamount");
        Label lbldownid = (Label)gvRow.FindControl("lbldownid");
        Label lbluserid = (Label)gvRow.FindControl("lbluserid");
        Label lblmobile = (Label)gvRow.FindControl("lblmobile");
        Label Remark = (Label)gvRow.FindControl("lbltransactionid123");
        Label LblRequestType = (Label)gvRow.FindControl("LblRequestType");     
  Label LblRequestTo = (Label)gvRow.FindControl("LblRequestTo");    
     
        objaccount.UserId = lbluserid.Text;
        objaccount.WithdrawlAmount = Convert.ToDecimal( lblamount.Text);
        //objaccount.Mainamount = Convert.ToDecimal(lblmainamount.Text);
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = lbldownid.Text;
   
        objaccount.Remark = Remark.Text;
        objaccount.DepositrequestRequestType = LblRequestType.Text;
        objaccount.DepositrequestTo = LblRequestTo.Text;
        objaccount.UserMObile = lblmobile.Text;
        DataTable Dt =Approve_topupRequest(objaccount);
        if (Dt != null)
        {

            string popupScript = "alert('"+Dt.Rows[0][1].ToString()+"');";
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
    public DataTable Approve_topupRequest(clsAccount objaccount)
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
            s2 = "sp_approve_Epinrequest";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@GenerateUserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@ID",objaccount.WithdrawlRequestId),
              
                     
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
    public string Reject_DepositRequest(clsAccount objaccount)
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
            s2 = "select * from EPinRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
            DataTable dt = new DataTable();
            dt = ObjData.RunSelectQueryTTrans(s2, tr);
            if (dt.Rows.Count > 0)
            {

                s2 = "update EPinRequest set status='Rejected' , approvedate=getdate() where id=" + objaccount.WithdrawlRequestId + "  ";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                res = "t";
            }
            else
            {
                res = "f";
            }


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
    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        string res = Reject_DepositRequest(objaccount);
        if (res == "t")
        {
            string popupScript = "alert('Request Rejected Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }
        else
        {
            string popupScript = "alert('Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
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
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
public partial class CashRequestReport : System.Web.UI.Page
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
        objaccount.DepositrequestRequestType = "PRIME MEMBER";
        DataTable dt = new DataTable();
        objaccount.UserId = txtuserid.Text;
        dt = objaccount.getPrimeMmeberRequest(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
           
          

         
            LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            if (lblstatus.Text.ToUpper() == "PENDING")
            {
                lblstatus.Text = "PENDING";
                lblstatus.CssClass = "label label-warning";
                btnApprove.Visible = true;
                btnReject.Visible = true;
              
            }
            else
                if (lblstatus.Text.ToUpper() == "APPROVED")
                {
                    lblstatus.Text = "Approved";
                    lblstatus.CssClass = "label label-success";
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                  
                }
                else

                    if (lblstatus.Text.ToUpper() == "REJECTED")
                    {
                        lblstatus.Text = "Cancelled";
                        lblstatus.CssClass = "label label-danger";
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                       
                    }

        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
      
        Label lbluserid = (Label)gvRow.FindControl("lbluserid");
        Label lblmobile = (Label)gvRow.FindControl("lblmobile");
        Label lblamount = (Label)gvRow.FindControl("lblamount");     
        objaccount.UserId = lbluserid.Text;      
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        objaccount.Amount = Convert.ToDecimal(lblamount.Text);
        objaccount.UserMObile = lblmobile.Text;
        string res = objaccount.Approve_PrimeMemberRequest(objaccount);
        if (res == "t")
        {
            string popupScript = "alert('Request Approved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadgethelp();

        }
        else
            if (res == "f")
            {
                string popupScript = "alert('request is not pending');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        //loadgethelp();
    }
    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        string res = objaccount.Rejected_PrimeMemberRequest(objaccount);
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
       
    }
}
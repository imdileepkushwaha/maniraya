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
public partial class admin_PinRequestReport : System.Web.UI.Page
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
        objaccount.DepositrequestTo = Session["useradmin"].ToString();
        dt = objaccount.getPinRequest(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

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
        Label lblNoOfepin = (Label)gvRow.FindControl("lblNoOfepin");
        Label lbluserid = (Label)gvRow.FindControl("lbluserid");
        Label lblmobile = (Label)gvRow.FindControl("lblmobile");
        Label lblPlanid = (Label)gvRow.FindControl("lblPlanid");
        Label Remark = (Label)gvRow.FindControl("lbltransactionid123");
        Label LblRequestType = (Label)gvRow.FindControl("LblRequestType");     
  Label LblRequestTo = (Label)gvRow.FindControl("LblRequestTo");    
     
        objaccount.UserId = lbluserid.Text;
        objaccount.WithdrawlAmount = Convert.ToDecimal( lblamount.Text);
     
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        objaccount.NoOfEpin = lblNoOfepin.Text;
        objaccount.planid = lblPlanid.Text;
        objaccount.Remark = Remark.Text;
        objaccount.DepositrequestRequestType = LblRequestType.Text;
        objaccount.DepositrequestTo = LblRequestTo.Text;
        objaccount.UserMObile = lblmobile.Text;
        DataTable Dt = objaccount.Approve_PinRequest(objaccount);
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
    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");
        objaccount.WithdrawlRequestId = lblgalleryid.Text;
        objaccount.MentionBy = Session["useradmin"].ToString();
        string res = objaccount.Reject_DepositRequest(objaccount);
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
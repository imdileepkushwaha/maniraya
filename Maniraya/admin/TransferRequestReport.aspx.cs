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
public partial class TransferRequestReport : System.Web.UI.Page
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
        objaccount.WithdrawlRequestStatus = "0";
        objaccount.DepositrequestRequestType = "CASH";
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
            //Label lblstatus = (Label)e.Row.FindControl("lblstatus");
           
          

         
            //LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            //LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            //if (lblstatus.Text.ToUpper() == "PENDING")
            //{
            //    lblstatus.Text = "PENDING";
            //    lblstatus.CssClass = "label label-warning";
            //    btnApprove.Visible = true;
            //    btnReject.Visible = true;
              
            //}
            //else
            //    if (lblstatus.Text.ToUpper() == "APPROVED")
            //    {
            //        lblstatus.Text = "Approved";
            //        lblstatus.CssClass = "label label-success";
            //        btnApprove.Visible = false;
            //        btnReject.Visible = false;
                  
            //    }
            //    else

            //        if (lblstatus.Text.ToUpper() == "REJECTED")
            //        {
            //            lblstatus.Text = "Cancelled";
            //            lblstatus.CssClass = "label label-danger";
            //            btnApprove.Visible = false;
            //            btnReject.Visible = false;
                       
            //        }

        }
    }
   
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_CoreCommitteePayout : System.Web.UI.Page
{
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            loaduser();          
        }
    }

    void loaduser()
    {
        objCL.paystatus = "0";
        DataTable Dt = objCL.getCoreCommitteeClosingReport(objCL);
        gvusers.DataSource = Dt;
        gvusers.DataBind();
    }

    protected void gvusers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pay")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)gvusers.Rows[index].FindControl("lbluserid");
            objCL.GenerateUserId = lbluser.Text.Trim();

            string res = objCL.InsertCoreCommitteePayout(objCL);
            if (res == "t")
            {
                string popupScript = "alert('Paid Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                loaduser();
            }
            else if (res == "f")
            {
                string popupScript = "alert('Already Paid !!!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Failed !!!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }

    protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkPay = (LinkButton)e.Row.FindControl("lbpay");
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");

            if (lblstatus.Text.Trim() == "DUE")
            {
                lnkPay.Enabled = true;
            }
            else
            {
                lnkPay.Enabled = false;
            }
        }
    }
}
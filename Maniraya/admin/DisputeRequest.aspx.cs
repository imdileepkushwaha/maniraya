using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using ARA_StringHunt;

public partial class admin_DisputeRequest : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    protected void Page_Load(object sender, EventArgs e)
    {
        string LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {

            loaddata();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lbltransactionidgrid = (Label)GridView1.Rows[index].FindControl("lbltransactionid");
            lblidedit.Text = lblid.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaddata();
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        objrecharge.UserMobile = txtuserno.Text;
        objrecharge.RechargeMobile = txtrechargeno.Text;
        objrecharge.ReferenceId = txttransactionid.Text;
        objrecharge.Status = ddstatus.SelectedValue.ToString();
        if (txtfromdate.Text != "")
        {
            objrecharge.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objrecharge.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objrecharge.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objrecharge.ToDate = DateTime.MinValue;
        }
        string Rowno = "";
        if (ddlRecordFilter.SelectedValue == "10")
        {
            Rowno = " top 10 ";
        }
        if (ddlRecordFilter.SelectedValue == "25")
        {
            Rowno = " top 25 ";
        }
        if (ddlRecordFilter.SelectedValue == "50")
        {
            Rowno = " top 50 ";
        }
        if (ddlRecordFilter.SelectedValue == "100")
        {
            Rowno = " top 100 ";
        }
        if (ddlRecordFilter.SelectedValue == "500")
        {
            Rowno = " top 500 ";
        }
        dt = objrecharge.getDIsputeReportnew(objrecharge);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            Label lblstatus2222 = (Label)e.Row.FindControl("lblstatus2222");
            
            LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");

            if (lblstatus.Text == "Pending")
            {
                lblstatus.CssClass = "label label-info";
                lbEdit.Visible = true;
            }
            else
                if (lblstatus.Text == "Approved")
                {
                    lblstatus.CssClass = "label label-success";
                    lbEdit.Visible = false;
                }
                else
                    if (lblstatus.Text == "Rejected")
                    {
                        lblstatus.CssClass = "label label-danger";
                        lbEdit.Visible = false;
                    }

            if (lblstatus2222.Text == "Success")
            {
                lblstatus2222.CssClass = "label label-success";        
            }
            if (lblstatus2222.Text == "Failed")
            {
                lblstatus2222.CssClass = "label label-danger";
            }
            if (lblstatus2222.Text == "Refund")
            {
                lblstatus2222.CssClass = "label label-info";
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = objrecharge.UpdateDisputeStatus(lblidedit.Text, txtliveid.Text.Trim(), txtremark.Text, ddstatusedit.SelectedValue.ToString(), Session["useradmin"].ToString());
        if (dt.Rows[0][0].ToString() == "t")
        {
            string popupScript = "alert('Request status changed successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
             loaddata();
             lblidedit.Text = ""; txtliveid.Text = txtremark.Text = "";
             ddstatusedit.SelectedValue = "0";
        }
        else
            if (dt.Rows[0][0].ToString() == "f")
            {
                string popupScript = "alert('Error Occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);           
            }
            else
            {
                string popupScript = "alert('Error Occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);           
            }
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            
    }
    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaddata();
    }
}
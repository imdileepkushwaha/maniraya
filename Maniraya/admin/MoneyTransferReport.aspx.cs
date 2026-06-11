using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsMoneyTransfer objmoneytransfer = new clsMoneyTransfer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                txtfromdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lbltransaction");
            Label lbluserid = (Label)GridView1.Rows[index].FindControl("lbluserId");           
             lblrefrenceid.Text = lblid.Text;
             lblUserid.Text = lbluserid.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {

        if (txtfromdate.Text != "")
        {
            objmoneytransfer.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objmoneytransfer.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objmoneytransfer.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objmoneytransfer.ToDate = DateTime.MinValue;
        }
        objmoneytransfer.UserId = txtuserid.Text;
        objmoneytransfer.AmountTransferTo = txtaccountno.Text;
        DataTable dt = new DataTable();
        dt = objmoneytransfer.getMoneyTransferReport(objmoneytransfer);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");

            if (lblstatus.Text.ToLower() == "success")
            {
                lblstatus.CssClass = "label label-success";
                lbEdit.Visible = false;

            }
            else
                if (lblstatus.Text.ToLower() == "dispute")
                {
                    lblstatus.CssClass = "label badge-info";
                    lbEdit.Visible = false;

                }
                else
                    if (lblstatus.Text.ToLower() == "failed")
                    {
                        lblstatus.CssClass = "label label-danger";
                        lbEdit.Visible = false;

                    }
                    else
            if (lblstatus.Text.ToLower() == "pending")
            {
                lblstatus.CssClass = "label label-info";
                lbEdit.Visible = true;

            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        clsMoneyTransfer objmoneytransfer = new clsMoneyTransfer();
        string UserId = lblUserid.Text;
        string beneficiaryid = "";
        string accountno = "";
        string PreviousTransId = lblrefrenceid.Text;
        string Response = "";
        string Type = "";
        string Oprater_Id = TxtLiveId.Text;
        string vendorid = TxtVEndorId.Text;
        string mentionid = "admin";
        objmoneytransfer.UpdateMRTransferResponse(UserId, "", "",PreviousTransId, Response, ddlststatus.SelectedValue, Oprater_Id, vendorid, mentionid);
        string popupScript = "alert('Status Update Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        loaduser();
    }
}
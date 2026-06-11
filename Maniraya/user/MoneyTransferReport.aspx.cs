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
            if (Session["userid"] != null)
            {
                txtfromdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtuserid.Text = Session["userid"].ToString();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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
        objmoneytransfer.UserId = Session["userid"].ToString();
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


            if (lblstatus.Text.ToLower() == "success")
            {
                lblstatus.CssClass = "label label-success";

            }
            else
                if (lblstatus.Text.ToLower() == "dispute")
                {
                    lblstatus.CssClass = "label badge-info";

                }
                else
                    if (lblstatus.Text.ToLower() == "failed")
                    {
                        lblstatus.CssClass = "label label-danger";

                    }
                    else
            if (lblstatus.Text.ToLower() == "pending")
            {
                lblstatus.CssClass = "label label-info";

            }
        }
    }
}
using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ApprovePurchaseProduct : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    decimal amount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fuserid"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
                
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            amount = 0;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lnl = (Label)e.Row.FindControl("lblemail");
            amount = amount + Convert.ToDecimal(lnl.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total (Rs) :";
            e.Row.Cells[9].Text = amount.ToString();
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
            objP.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objP.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.MinValue;
        }
        if (txtransactionId.Text != "")
        {
            objP.TransactionCode = txtransactionId.Text.Trim();
        }
        else
        {
            objP.TransactionCode = string.Empty;
        }
        if (txtpaymentcode.Text != "")
        {
            objP.PaymentCode = txtpaymentcode.Text;
        }
        else
        {
            objP.PaymentCode = string.Empty;
        }
        if (txtuserid.Text != "")
        {
            objP.UserId = txtuserid.Text;
        }
        else
        {
            objP.UserId = string.Empty;
        }
        if (ddlpstatus.SelectedValue != "")
        {
            objP.Status = ddlpstatus.SelectedValue;
        }
        else
        {
            objP.Status = string.Empty;
        }
        objP.FranchiseeID = new clsvendor().GetFranchiseeId(Session["fuserid"].ToString()).ToString();
        
        objP.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objP.getPurchaseProductForApproval(objP);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

   
    protected void lnk_click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        DataTable dt = new DataTable();
        objP.UserId = string.Empty;
        objP.FranchiseeID = string.Empty;
        objP.TransactionCode = lnk.CommandArgument.ToString();

        dt = objP.getPurchaseProduct(objP);
        GridView2.DataSource = dt;
        GridView2.DataBind();

        if (dt != null && dt.Rows.Count > 0)
        {
            lbltransactionid.Text = dt.Rows[0]["TransactionCode"].ToString();
            lbltuserid.Text = dt.Rows[0]["UserID"].ToString();
            lblpaymentcode.Text = dt.Rows[0]["PaymentCode"].ToString();
            lblpaymentstatus.Text = dt.Rows[0]["PStatus"].ToString();
            lblproductstatus.Text = dt.Rows[0]["RStatus"].ToString();
            lblpurchasedate.Text = dt.Rows[0]["PurchaseDate"].ToString();
            lblfranchiseeid.Text = dt.Rows[0]["FranchiseeId"].ToString();
            hdId.Value = objP.TransactionCode;
            if (lblproductstatus.Text.ToLower() == "notdelivered")
            {
                div_pay.Visible = true;
                div_succcess.Visible = false;
                lblorderstatus.Text = "";
            }
            else
            {
                div_pay.Visible = false;
                div_succcess.Visible = true;
                lblorderstatus.Text = lblproductstatus.Text.ToLower() == "rejected" ? "Order Rejected" : "Order Delivered";
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showFranchiseeModal();", true);
    }


    protected void btnapprove_click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdId.Value))
        {
            objP.FranchiseeID = lblfranchiseeid.Text;
            objP.UserId = lbltuserid.Text;
            objP.TransactionCode = lbltransactionid.Text;
            objP.Status = ddlstatus.SelectedValue;
            string res = objP.AprovePurchase(objP);

            if (res == "1")
            {
                if (ddlstatus.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alt", "alert('Order Successfully Approved');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alt", "alert('Order Successfully Rejected');", true);
                }
                div_pay.Visible = false;
                div_succcess.Visible = true;
                lblorderstatus.Text = ddlstatus.SelectedValue == "2" ? "Order Rejected Successully" : "Order Successully Delivered";

                loaduser();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alt", "alert('Temporary Error Occurred');", true);
            }

        }
        else
        {

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "ClosesFranchiseepopup();showFranchiseeModal();", true);
    }

    
  
}
using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FranchiseePvReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    decimal amount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
                loadFranchisee();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    void loadFranchisee()
    {
        clsvendor objbank = new clsvendor();
        DDLstFranchisee.Items.Clear();
        DataTable dt = new DataTable();
        dt = objbank.getFranchiseeList();
        DDLstFranchisee.DataSource = dt;
        DDLstFranchisee.DataTextField = "Username";
        DDLstFranchisee.DataValueField = "Userid";
        DDLstFranchisee.DataBind();
        ListItem li = new ListItem("Select Franchisee", "0");
        DDLstFranchisee.Items.Insert(0, li);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GV1_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            amount = 0;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amount = amount + Convert.ToDecimal(((Label)e.Row.FindControl("lblbv")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total : ";
            e.Row.Cells[3].Text = amount.ToString();
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
            objP.Fromdate = DateTime.Parse("01/01/1900");
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.Now.AddDays(1);
        }
        if (DDLstFranchisee.SelectedIndex > 0)
        {
            objP.FranchiseeID = DDLstFranchisee.SelectedValue;
        }
        else
        {
            objP.FranchiseeID = null;
        }
        DataTable dt = new DataTable();
        dt = objP.getFranchiseeBV(objP);
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

            div_succcess.Visible = true;
            lblorderstatus.Text = lblproductstatus.Text.ToLower() == "rejected" ? "Order Rejected" : "Order Delivered";

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showFranchiseeModal();", true);
    }




}
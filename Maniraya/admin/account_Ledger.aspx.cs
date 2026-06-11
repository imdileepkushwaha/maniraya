using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;
using System.Data;
using System.IO;

public partial class User_account_Ledger : System.Web.UI.Page
{
    DataTable dt;
    clsAccount objAccount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");


                RDBtnRechargeWallet.Checked = true;
                getAccountLedgerReport();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }

      
    }

    protected void getAccountLedgerReport()
    {
        dt = new DataTable();
        objAccount = new clsAccount();
        string noOfRows = "", fromDate = "", toDate = "", walletType = "";

        try
        {
            objAccount.UserId = txtuserid.Text;

            fromDate = txtFromDate.Text;

            toDate = txtToDate.Text;


            if ( RDBtnRechargeWallet.Checked == true)
            {
                walletType = "1";
            }
            else
            {
                walletType = "2";
            }

            if (ddlRecordFilter.SelectedItem.Text == "All")
                noOfRows = "";

            //else if (ddlRecordFilter.SelectedItem.Text == "5")
            //    noOfRows = "top 5";

            else if (ddlRecordFilter.SelectedItem.Text == "25")
                noOfRows = "top 25";

            else if (ddlRecordFilter.SelectedItem.Text == "50")
                noOfRows = "top 50";

            else if (ddlRecordFilter.SelectedItem.Text == "100")
                noOfRows = "top 100";

            else if (ddlRecordFilter.SelectedItem.Text == "500")
                noOfRows = "top 500";

            dt = objAccount.getAccountLedgerReport(objAccount, noOfRows, fromDate, toDate, walletType);

            if (dt.Rows.Count > 0)
                grdAccountsList.DataSource = dt;

            else
                grdAccountsList.EmptyDataText = "No record(s) found";

            grdAccountsList.DataBind();
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally 
        {
            dt = null;
            objAccount = null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtFromDate.Text) && string.IsNullOrEmpty(txtToDate.Text))
        {
            Message.Show("Select From Date and To Date");
            return;
        }
        getAccountLedgerReport();
    }
    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    decimal sumOfDebit = 0, sumOfCredit = 0, sumOfCurrentBal = 0;

    protected void grdAccountsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDebit = e.Row.FindControl("lblDebit") as Label;
            Label lblCredit = e.Row.FindControl("lblCredit") as Label;
            Label lblCurrentBalance = e.Row.FindControl("lblCurrentBalance") as Label;

            //sumOfDebit += Convert.ToDouble(lblDebit.Text);
            //sumOfCredit += Convert.ToDouble(lblCredit.Text);
            //sumOfCurrentBal += Convert.ToDouble(lblCurrentBalance.Text);

            sumOfDebit += Convert.ToDecimal(lblDebit.Text);
            sumOfCredit += Convert.ToDecimal(lblCredit.Text);
            sumOfCurrentBal += Convert.ToDecimal(lblCurrentBalance.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblDebitTotal = e.Row.FindControl("lblDebitTotal") as Label;
            Label lblCreditTotal = e.Row.FindControl("lblCreditTotal") as Label;
            Label lblCurrentBalanceTotal = e.Row.FindControl("lblCurrentBalanceTotal") as Label;

            //lblDebitTotal.Text = Convert.ToString(Math.Round(sumOfDebit, 2));
            //lblCreditTotal.Text = Convert.ToString(Math.Round(sumOfCredit, 2));
            //lblCurrentBalanceTotal.Text = Convert.ToString(Math.Round(sumOfCurrentBal, 2));

            lblDebitTotal.Text = Convert.ToString(decimal.Round(sumOfDebit, 2, MidpointRounding.AwayFromZero));
            lblCreditTotal.Text = Convert.ToString(decimal.Round(sumOfCredit, 2, MidpointRounding.AwayFromZero));
            lblCurrentBalanceTotal.Text = Convert.ToString(decimal.Round(sumOfCurrentBal, 2, MidpointRounding.AwayFromZero));
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }

    protected void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Account_Ledger_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdAccountsList.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
        grdAccountsList.GridLines = GridLines.Both;
        grdAccountsList.HeaderStyle.Font.Bold = true;
        grdAccountsList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportGridToExcel();
    }
    protected void RDBtnRechargeWallet_CheckedChanged(object sender, EventArgs e)
    {
        getAccountLedgerReport();
    }
    protected void RdBtnUtilityWallet_CheckedChanged(object sender, EventArgs e)
    {
        getAccountLedgerReport();
    }
}
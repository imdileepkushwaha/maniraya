using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class franchisee_account_Ledger : System.Web.UI.Page
{
    clsAccount objAccount;
    decimal sumOfDebit = 0, sumOfCredit = 0, sumOfCurrentBal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] == null)
        {
            Response.Redirect("index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "true");
            txtToDate.Attributes.Add("readonly", "true");
            getAccountLedgerReport();
        }
    }

    protected void getAccountLedgerReport()
    {
        sumOfDebit = 0;
        sumOfCredit = 0;
        sumOfCurrentBal = 0;

        DataTable dt = null;
        objAccount = new clsAccount();

        try
        {
            objAccount.UserId = Convert.ToString(Session["fuserid"]);
            string fromDate = txtFromDate.Text;
            string toDate = txtToDate.Text;
            string walletType = "3";
            string noOfRows = GetRecordLimit();

            dt = objAccount.getAccountLedgerReport(objAccount, noOfRows, fromDate, toDate, walletType);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdAccountsList.DataSource = dt;
                UpdateSummaryCards(dt);
            }
            else
            {
                grdAccountsList.DataSource = null;
                ResetSummaryCards();
            }

            grdAccountsList.DataBind();
        }
        catch (Exception ex)
        {
            ResetSummaryCards();
            Message.Show(ex.Message);
        }
    }

    string GetRecordLimit()
    {
        switch (ddlRecordFilter.SelectedItem.Text)
        {
            case "25": return "top 25";
            case "50": return "top 50";
            case "100": return "top 100";
            case "500": return "top 500";
            default: return "";
        }
    }

    void UpdateSummaryCards(DataTable dt)
    {
        decimal totalDebit = 0;
        decimal totalCredit = 0;
        decimal latestBalance = 0;

        foreach (DataRow row in dt.Rows)
        {
            totalDebit += ToDecimal(row["DrAmount"]);
            totalCredit += ToDecimal(row["CrAmount"]);
            latestBalance = ToDecimal(row["CurrentBalance"]);
        }

        lblSummaryDebit.Text = FormatAmount(totalDebit);
        lblSummaryCredit.Text = FormatAmount(totalCredit);
        lblSummaryBalance.Text = FormatAmount(latestBalance);
    }

    void ResetSummaryCards()
    {
        lblSummaryDebit.Text = "0.00";
        lblSummaryCredit.Text = "0.00";
        lblSummaryBalance.Text = "0.00";
    }

    decimal ToDecimal(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return 0;
        }

        decimal result;
        return decimal.TryParse(value.ToString(), out result) ? result : 0;
    }

    string FormatAmount(decimal amount)
    {
        return decimal.Round(amount, 2, MidpointRounding.AwayFromZero).ToString("N2");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
        {
            Message.Show("Select From Date and To Date");
            return;
        }

        getAccountLedgerReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        getAccountLedgerReport();
    }

    protected void grdAccountsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDebit = e.Row.FindControl("lblDebit") as Label;
            Label lblCredit = e.Row.FindControl("lblCredit") as Label;
            Label lblCurrentBalance = e.Row.FindControl("lblCurrentBalance") as Label;

            decimal debit = ToDecimal(lblDebit != null ? lblDebit.Text : "0");
            decimal credit = ToDecimal(lblCredit != null ? lblCredit.Text : "0");

            if (lblDebit != null && debit > 0)
            {
                lblDebit.CssClass = "fr-amount-debit";
            }

            if (lblCredit != null && credit > 0)
            {
                lblCredit.CssClass = "fr-amount-credit";
            }

            if (lblCurrentBalance != null)
            {
                lblCurrentBalance.CssClass = "fr-amount-balance";
            }

            sumOfDebit += debit;
            sumOfCredit += credit;
            sumOfCurrentBal += ToDecimal(lblCurrentBalance != null ? lblCurrentBalance.Text : "0");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblDebitTotal = e.Row.FindControl("lblDebitTotal") as Label;
            Label lblCreditTotal = e.Row.FindControl("lblCreditTotal") as Label;
            Label lblCurrentBalanceTotal = e.Row.FindControl("lblCurrentBalanceTotal") as Label;

            if (lblDebitTotal != null)
            {
                lblDebitTotal.Text = FormatAmount(sumOfDebit);
                lblDebitTotal.CssClass = "fr-amount-debit";
            }

            if (lblCreditTotal != null)
            {
                lblCreditTotal.Text = FormatAmount(sumOfCredit);
                lblCreditTotal.CssClass = "fr-amount-credit";
            }

            if (lblCurrentBalanceTotal != null)
            {
                lblCurrentBalanceTotal.Text = FormatAmount(sumOfCurrentBal);
                lblCurrentBalanceTotal.CssClass = "fr-amount-balance";
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string fileName = "Account_Ledger_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        grdAccountsList.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
        grdAccountsList.GridLines = GridLines.Both;
        grdAccountsList.HeaderStyle.Font.Bold = true;
        grdAccountsList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    protected void imgExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
}

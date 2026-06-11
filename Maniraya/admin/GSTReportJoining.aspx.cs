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

public partial class GSTReportJoining : System.Web.UI.Page
{
    DataTable dt;
    clsAccount objAccount;
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
                txtFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

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

            dt = getUserGSTReport(objAccount, noOfRows, fromDate, toDate, walletType);

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
    public DataTable getUserGSTReport(clsAccount objaccount, string noOfRows, string fromDate, string toDate, string walletType)
        {
            DataSet ds = new DataSet();

            try
            {
                string sql = "";
                //string sql = @"select " + noOfRows + " MentionDate,userID,TransactionId, (case when TransactionType='Deposit' then Remark else TransactionType end) as [Description],DrAmount, " + 
                //                " CrAmount,(CrAmount-DrAmount) as [Current_Balance],Remark from transactiondetail where userId='" + objaccount.UserId + "'";
                if (walletType == "1")
                {
                    sql = "SELECT " + noOfRows + " U.UserId,P.Purchasedate,P.OrderNo,D.ProductName,PD.Quantity,CASE WHEN isnull(P.Isdistributer,0)=0 THEN PD.Amount WHEN isnull(P.Isdistributer,0)=1  THEN PD.DP END AS Amount,CASE WHEN isnull(P.Isdistributer,0)=0 THEN PD.TotalAmount WHEN isnull(P.Isdistributer,0)=1  THEN PD.TotalDP END AS TotalAmount,PD.PurchaseAmount,PD.GSTPER,PD.CGST,PD.SGST,PD.IGST FROM UserJoiningPurchaseMaster PD JOIN UserJoiningPurchaseProductMaster P ON PD.PurchaseId=P.PurchaseId JOIN Productmaster D ON PD.ProductID=D.ProductId JOIN userdetail U ON U.UserId=P.UserId WHERE 1=1 ";//P.Cstatus=1 ";
                }
                else
                {
                    sql = "SELECT " + noOfRows + " U.UserId,P.Purchasedate,P.OrderNo,D.ProductName,PD.Quantity,CASE WHEN isnull(P.Isdistributer,0)=0 THEN PD.Amount WHEN isnull(P.Isdistributer,0)=1  THEN PD.DP END AS Amount,CASE WHEN isnull(P.Isdistributer,0)=0 THEN PD.TotalAmount WHEN isnull(P.Isdistributer,0)=1  THEN PD.TotalDP END AS TotalAmount,PD.PurchaseAmount,PD.GSTPER,PD.CGST,PD.SGST,PD.IGST FROM FranchiseePurchaseProductMaster PD JOIN FranchiseePurchaseMaster P ON PD.PurchaseId=P.PurchaseId JOIN Productmaster D ON PD.ProductID=D.ProductId JOIN franchiseedetail U ON U.UserId=P.franchiseeid WHERE P.status=1 and P.mentionby='admin' ";
                }
              

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    sql += " and (convert(date,P.purchasedate) between convert(date,'" + fromDate + "') and convert(date,'" + toDate + "') ) ";

               
                sql += " order by P.ID desc";
                ObjData.StartConnection();
                ds = ObjData.RunSelectQuery(sql);

                return ds.Tables[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                ds = null;
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
        getAccountLedgerReport();
    }

    decimal sumOfDebit = 0, sumOfCredit = 0, sumOfCurrentBal = 0, sumOftotalamount;

    protected void grdAccountsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDebit = e.Row.FindControl("lbCGST") as Label;
            Label lblCredit = e.Row.FindControl("lblSGST") as Label;
            Label lblCurrentBalance = e.Row.FindControl("lblIGST") as Label;

            Label lblTotalAmount = e.Row.FindControl("lblTotalAmount") as Label;
        

            //sumOfDebit += Convert.ToDouble(lblDebit.Text);
            //sumOfCredit += Convert.ToDouble(lblCredit.Text);
            //sumOfCurrentBal += Convert.ToDouble(lblCurrentBalance.Text);

            sumOfDebit += Convert.ToDecimal(lblDebit.Text);
            sumOfCredit += Convert.ToDecimal(lblCredit.Text);
            sumOfCurrentBal += Convert.ToDecimal(lblCurrentBalance.Text);
            sumOftotalamount += Convert.ToDecimal(lblTotalAmount.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalCGST = e.Row.FindControl("lblTotalCGST") as Label;
            Label lblTotalSGST = e.Row.FindControl("lblTotalSGST") as Label;
            Label lblTotalIGST = e.Row.FindControl("lblTotalIGST") as Label;
            Label lblTotalTotalAmount = e.Row.FindControl("lblTotalTotalAmount") as Label;

            //lblDebitTotal.Text = Convert.ToString(Math.Round(sumOfDebit, 2));
            //lblCreditTotal.Text = Convert.ToString(Math.Round(sumOfCredit, 2));
            //lblCurrentBalanceTotal.Text = Convert.ToString(Math.Round(sumOfCurrentBal, 2));

            lblTotalCGST.Text = Convert.ToString(decimal.Round(sumOfDebit, 2, MidpointRounding.AwayFromZero));
            lblTotalSGST.Text = Convert.ToString(decimal.Round(sumOfCredit, 2, MidpointRounding.AwayFromZero));
            lblTotalIGST.Text = Convert.ToString(decimal.Round(sumOfCurrentBal, 2, MidpointRounding.AwayFromZero));
            lblTotalTotalAmount.Text = Convert.ToString(decimal.Round(sumOftotalamount, 2, MidpointRounding.AwayFromZero));
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
        string FileName = "GSTREPORT_" + DateTime.Now + ".xls";
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
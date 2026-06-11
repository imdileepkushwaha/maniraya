using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class franchisee_fundTransferReceiveReport : System.Web.UI.Page
{
    DataTable dt;
    clsAccount objAccount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            txtUserId.Text = Session["fuserid"].ToString();
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        objAccount = new clsAccount();
        string noOfRows = "", walletType = "", fromDate = "", toDate = "";

        try
        {
            objAccount.UserId = Session["fuserid"].ToString();

            if (ddlWalletType.SelectedIndex > 0)
                walletType = ddlWalletType.SelectedValue;

            fromDate = txtFromDate.Text;
            toDate = txtToDate.Text;
            
            dt = objAccount.getFundTransferReceiveReport(objAccount, noOfRows, fromDate, toDate, walletType);

            if (dt.Rows.Count > 0)
                GridView1.DataSource = dt;
            else
                GridView1.EmptyDataText = "No Record(s) Found...";

            GridView1.DataBind();
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Path);
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
        string FileName = "FundTransferReceiveReport_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportGridToExcel();
    }
}
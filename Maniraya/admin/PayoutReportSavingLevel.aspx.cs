using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;
using DataTier;
public partial class PayoutReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {


            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadgethelp();
    }
    void loadgethelp()
    {
        //if (txtfromdate.Text != "" && txttodate.Text != "")
        //{
        //    objaccount.FromDate = Message.GetIndianDate(txtfromdate.Text);
        //    objaccount.ToDate = Message.GetIndianDate(txttodate.Text);
        //}
        //else
        //{
        //    objaccount.FromDate = DateTime.MinValue;
        //    objaccount.ToDate = DateTime.MinValue;
        //}

        //objaccount.WithdrawlRequestStatus = ddstatus.SelectedValue.ToString();
        DataTable dt = new DataTable();
        objaccount.UserId = txtuserid.Text;
        dt = getWithdrawlRequest(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    public DataTable getWithdrawlRequest(clsAccount objaccount)
    {
        // string s1 = "select isnull(CashWalletPercent,0) as CashWalletPercent from tbl_Deduction";
        // ObjData.StartConnection();
        // DataTable dt1 = ObjData.RunDataTable(s1);
        // ObjData.EndConnection();
        // decimal deductionPercent = Convert.ToDecimal(dt1.Rows[0]["CashWalletPercent"].ToString());

        string str_query = "SELECT sum(t.cramount)-sum(t.dramount) AS amount, t.UserID,U.PanNumber, U.UserName,U.AccountNo,'Saving' AS Accounttype,C.BankName,U.BranchName,U.IFSCCode,U.Mobile FROM TransactionDetail t WITH (nolock) LEFT JOIN userdetail U ON t.UserId=U.UserId LEFT JOIN BankMaster C ON C.BankId=U.BankName  where t.TransactionType='Saving Level Income' ";


        //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
        //{
        //    str_query += "  and convert(date, W.MentionDate ) >= convert(date, '" + objaccount.FromDate + "')   and convert(date, W.MentionDate)   <= convert(date, '" + objaccount.ToDate + "') ";
        //}



        //   if (objaccount.WithdrawlRequestStatus != "0")
        //   {
        //       str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
        //   }

        if (objaccount.UserId != "")
        {
            str_query += "  and t.UserId = '" + objaccount.UserId + "' ";
        }


        str_query += @" GROUP BY t.UserID,U.PanNumber, U.UserName,U.AccountNo,C.BankName,U.BranchName,U.IFSCCode,U.Mobile  HAVING sum(t.cramount)-sum(t.dramount)>50
ORDER BY u.username";



        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    Data ObjData = new Data();

    protected void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "PayoutReport_" + DateTime.Now + ".xls";
        System.IO.StringWriter strwritter = new System.IO.StringWriter();
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportGridToExcel();
    }
    public string Release_SavingLevelIncome(string userid, decimal amount)
    {


        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_releaseSavingLevelIncome";
            SqlParameter[] parameter = {
                new SqlParameter("@userid",userid),
                new SqlParameter("@amount",amount),


                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }

    protected void chckchanged(object sender, EventArgs e)

    {

        CheckBox chckheader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox1");

        foreach (GridViewRow row in GridView1.Rows)

        {

            CheckBox chckrw = (CheckBox)row.FindControl("CheckBox2");

            if (chckheader.Checked == true)

            {
                chckrw.Checked = true;
            }
            else

            {
                chckrw.Checked = false;
            }

        }

    }
    protected void btnPayAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox CheckBox2 = (CheckBox)r.FindControl("CheckBox2");
            if (CheckBox2.Checked == true)
            {

                Label lbluserid = (Label)r.FindControl("lbluserid");
                Label lblamount = (Label)r.FindControl("lblamount");
                string res = Release_SavingLevelIncome(lbluserid.Text, Convert.ToDecimal(lblamount.Text));
                //if (res == "t")
                //{

                //    string popupScript = "alert('Purchase Approved Successfully');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //else if (res == "f")
                //{

                //    string popupScript = "alert('Purchase Already Processed');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //else
                //{
                //    string popupScript = "alert('Something wrong ');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //    loadprevproduct();

                //}
                //loadprevproduct();
            }
        }

        string popupScript = "alert('Payout Released Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        loadgethelp();


    }
}
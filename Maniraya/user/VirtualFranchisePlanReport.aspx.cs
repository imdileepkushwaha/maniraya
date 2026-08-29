using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class user_VirtualFranchisePlanReport : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        try { VirtualFranchiseHelper.EnsureSchema(); }
        catch { }
        try { VirtualFranchiseHelper.ProcessDueRoi(); }
        catch { }

        if (!IsPostBack)
        {
            LoadReport();
        }
    }

    void LoadReport()
    {
        string userId = Convert.ToString(Session["userid"]).Trim().Replace("'", "''");
        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            dt = ObjData.RunDataTable(@"
SELECT id, planname, planamount, monthlyroi, totalcashback, paymentmethod, onlinetransactionid,
       entrydate, approvedate, status, remark
FROM Virtual_Franchise_Request WITH (NOLOCK)
WHERE LTRIM(RTRIM(userid)) = '" + userId + @"'
ORDER BY entrydate DESC") ?? new DataTable();
        }
        catch
        {
            dt = new DataTable();
        }
        finally
        {
            try { ObjData.EndConnection(); }
            catch { }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        if (lblstatus == null)
        {
            return;
        }

        string status = (lblstatus.Text ?? string.Empty).Trim();
        if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "dash-saving-status is-approved";
        }
        else if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
        {
            lblstatus.CssClass = "dash-saving-status is-rejected";
        }
        else
        {
            lblstatus.CssClass = "dash-saving-status is-pending";
        }
    }
}

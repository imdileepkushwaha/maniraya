using DataTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_VirtualFranchiseLevelIncomeReport : Page
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

        if (!IsPostBack)
        {
            txtuserid.Text = Session["userid"].ToString();
            LoadReport();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadReport();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!GridView1.AllowPaging)
        {
            return dataItemIndex + 1;
        }
        return (GridView1.PageIndex * GridView1.PageSize) + dataItemIndex + 1;
    }

    void ApplyPaging(DataTable dt)
    {
        int pageSize;
        if (ddlRecordFilter.SelectedItem.Text == "All" || !int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize) || pageSize <= 0)
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt != null && dt.Rows.Count > 0 ? dt.Rows.Count : 25;
            return;
        }

        GridView1.AllowPaging = true;
        GridView1.PageSize = pageSize;
        if (dt != null && dt.Rows.Count > 0)
        {
            int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }
    }

    void LoadReport()
    {
        string userId = Convert.ToString(Session["userid"]).Trim();
        txtuserid.Text = userId;

        DateTime fromDate = DateTime.MinValue;
        DateTime toDate = DateTime.MinValue;
        if (!string.IsNullOrWhiteSpace(txtfromdate.Text))
        {
            fromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        if (!string.IsNullOrWhiteSpace(txttodate.Text))
        {
            toDate = Message.GetIndianDate(txttodate.Text);
        }

        string sql = @"
SELECT mentiondate, fromuserid, planname, levelno, incomeper, income, admincharge, tdscharge, payableamount, transactionid
FROM Virtual_Level_Income_Detail WITH (NOLOCK)
WHERE LTRIM(RTRIM(userid)) = '" + SqlEscape(userId) + "'";

        if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
        {
            sql += " AND CONVERT(date, mentiondate) >= CONVERT(date, '" + fromDate.ToString("yyyy-MM-dd") + "')"
                + " AND CONVERT(date, mentiondate) <= CONVERT(date, '" + toDate.ToString("yyyy-MM-dd") + "')";
        }

        sql += " ORDER BY mentiondate DESC";

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try { dt = ObjData.RunDataTable(sql) ?? new DataTable(); }
            finally { ObjData.EndConnection(); }
        }
        catch
        {
            dt = new DataTable();
        }

        ApplyPaging(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

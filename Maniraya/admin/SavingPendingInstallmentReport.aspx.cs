using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SavingPendingInstallmentReport : Page
{
    Data ObjData = new Data();
    DataTable _reportData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadReport();
        }
        else
        {
            // Rebuild pager links so First/Prev/Next/Last postbacks work (no ViewState DataTable).
            LoadReport();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtInstNo.Text = string.Empty;
        ddStatus.ClearSelection();
        ListItem pending = ddStatus.Items.FindByValue("Pending");
        if (pending != null)
        {
            pending.Selected = true;
        }
        GridView1.PageIndex = 0;
        LoadReport();
    }

    void LoadReport()
    {
        _reportData = null;
        BindGrid();
    }

    DataTable CurrentReportData
    {
        get
        {
            if (_reportData == null)
            {
                _reportData = GetReportData();
            }
            return _reportData;
        }
    }

    void BindGrid()
    {
        DataTable dt = CurrentReportData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            BuildExternalPager(0);
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 25;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;

            if (dt.Rows.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
        BuildExternalPager(dt.Rows.Count);
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 25;
    }

    void BuildExternalPager(int totalRecords)
    {
        pnlPager.Controls.Clear();

        if (!GridView1.AllowPaging || totalRecords == 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = GridView1.PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"admin-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-disabled\">...</span>"));
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-disabled\">...</span>"));
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerLink(string text, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-active\">" + text + "</span>"));
            return;
        }

        if (!enabled)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-disabled\">" + text + "</span>"));
            return;
        }

        LinkButton link = new LinkButton();
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.Click += ExternalPager_Click;
        pnlPager.Controls.Add(link);
    }

    protected void ExternalPager_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        int pageIndex;
        if (link == null || !int.TryParse(link.CommandArgument, out pageIndex))
        {
            return;
        }

        GridView1.PageIndex = pageIndex;
        LoadReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport();
    }

    DataTable GetReportData()
    {
        StringBuilder sql = new StringBuilder();
        sql.Append(@"
SELECT
    sa.userid,
    ud.username,
    ud.mobile,
    ud.email,
    sa.installmentdate,
    sa.instno,
    sa.amount,
    sa.status
FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sa.UserId
WHERE 1 = 1");

        if (!string.IsNullOrWhiteSpace(ddStatus.SelectedValue))
        {
            sql.Append(" AND sa.status = '").Append(SqlEscape(ddStatus.SelectedValue)).Append("'");
        }

        if (!string.IsNullOrWhiteSpace(txtFromDate.Text))
        {
            sql.Append(" AND CONVERT(date, sa.InstallmentDate) >= CONVERT(date, '")
                .Append(Message.GetIndianDate(txtFromDate.Text.Trim()).ToString("yyyy-MM-dd")).Append("')");
        }

        if (!string.IsNullOrWhiteSpace(txtToDate.Text))
        {
            sql.Append(" AND CONVERT(date, sa.InstallmentDate) <= CONVERT(date, '")
                .Append(Message.GetIndianDate(txtToDate.Text.Trim()).ToString("yyyy-MM-dd")).Append("')");
        }

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            sql.Append(" AND sa.UserId = '").Append(SqlEscape(txtUserId.Text.Trim())).Append("'");
        }

        int instNo;
        if (!string.IsNullOrWhiteSpace(txtInstNo.Text) && int.TryParse(txtInstNo.Text.Trim(), out instNo))
        {
            sql.Append(" AND sa.InstNo = ").Append(instNo);
        }

        sql.Append(@" ORDER BY
    CASE WHEN CONVERT(date, sa.InstallmentDate) >= CONVERT(date, GETDATE()) THEN 0 ELSE 1 END,
    sa.InstallmentDate ASC,
    sa.InstNo ASC,
    sa.id ASC");

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql.ToString());
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = GetReportData();
        if (dt == null || dt.Rows.Count == 0)
        {
            Message.Show("No records available to export.");
            return;
        }

        GridView1.AllowPaging = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PendingInstallmentReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);
            Response.Write("<style> .textmode { mso-number-format:\\@; } </style>");
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

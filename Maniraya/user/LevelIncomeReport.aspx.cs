using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LevelIncomeReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    Data ObjData = new Data();

    DataTable IncomeData
    {
        get { return ViewState["LevelIncomeData"] as DataTable; }
        set { ViewState["LevelIncomeData"] = value; }
    }

    int PageIndex
    {
        get { return ViewState["LevelIncomePageIndex"] != null ? (int)ViewState["LevelIncomePageIndex"] : 0; }
        set { ViewState["LevelIncomePageIndex"] = value; }
    }

    bool PagingEnabled
    {
        get { return ViewState["LevelIncomePagingEnabled"] != null && (bool)ViewState["LevelIncomePagingEnabled"]; }
        set { ViewState["LevelIncomePagingEnabled"] = value; }
    }

    int ActivePageSize
    {
        get { return ViewState["LevelIncomePageSize"] != null ? (int)ViewState["LevelIncomePageSize"] : 10; }
        set { ViewState["LevelIncomePageSize"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            string userId = Session["userid"].ToString();
            txtuserid.Text = userId;
            hdnUserId.Value = userId;
            txtuserid.ReadOnly = true;
            loaduser();
        }
        else if (IncomeData != null)
        {
            BindGrid();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PageIndex = 0;
        loaduser();
    }

    void loaduser()
    {
        pnlLoadError.Visible = false;

        if (txtfromdate.Text != "")
        {
            objaccount.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objaccount.FromDate = DateTime.MinValue;
        }

        if (txttodate.Text != "")
        {
            objaccount.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objaccount.ToDate = DateTime.MinValue;
        }

        string userId = GetCurrentUserId();
        objaccount.UserId = userId;
        txtuserid.Text = userId;

        DataTable dt = GetLevelIncomeData(objaccount);
        if (dt == null)
        {
            dt = new DataTable();
            pnlLoadError.Visible = true;
        }

        IncomeData = dt;
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = IncomeData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            pnlPager.Visible = false;
            UpdateResultSummary(new DataTable());
            return;
        }

        int pageSize = GetPageSize();
        PagingEnabled = pageSize > 0 && ddlRecordFilter.SelectedItem.Text != "All";
        ActivePageSize = PagingEnabled ? pageSize : Math.Max(dt.Rows.Count, 1);

        if (PagingEnabled && dt.Rows.Count > 0)
        {
            int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
            if (PageIndex >= totalPages)
            {
                PageIndex = Math.Max(0, totalPages - 1);
            }
        }
        else
        {
            PageIndex = 0;
        }

        GridView1.DataSource = GetPagedRows(dt);
        GridView1.DataBind();
        UpdateResultSummary(dt);
        BuildExternalPager();
    }

    DataTable GetPagedRows(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return dt == null ? new DataTable() : dt.Clone();
        }

        if (!PagingEnabled)
        {
            return dt;
        }

        DataTable page = dt.Clone();
        int start = PageIndex * ActivePageSize;
        int end = Math.Min(start + ActivePageSize, dt.Rows.Count);
        for (int i = start; i < end; i++)
        {
            page.ImportRow(dt.Rows[i]);
        }

        return page;
    }

    string GetCurrentUserId()
    {
        if (Session["userid"] != null)
        {
            return Session["userid"].ToString();
        }

        if (!string.IsNullOrWhiteSpace(hdnUserId.Value))
        {
            return hdnUserId.Value.Trim();
        }

        return txtuserid.Text.Trim();
    }

    public DataTable GetLevelIncomeData(clsAccount objaccount)
    {
        string str_query = @"SELECT sd.UserId,
                ud.UserName AS username,
                sd.JuniorUserId,
                sd.LevelNo,
                sd.Amount,
                sd.transactionid,
                CONVERT(VARCHAR(50), sd.MentionDate, 103) AS EntryDate,
                sd.MentionDate
            FROM SavingLevelIncomeDetail sd WITH (NOLOCK)
            LEFT JOIN userdetail ud WITH (NOLOCK) ON sd.UserId = ud.UserId
            WHERE 1 = 1";

        if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
        {
            str_query += " AND CONVERT(date, sd.MentionDate) >= CONVERT(date, '" + SqlEscape(objaccount.FromDate.ToString("yyyy-MM-dd")) + "')";
            str_query += " AND CONVERT(date, sd.MentionDate) <= CONVERT(date, '" + SqlEscape(objaccount.ToDate.ToString("yyyy-MM-dd")) + "')";
        }

        if (!string.IsNullOrWhiteSpace(objaccount.UserId))
        {
            str_query += " AND sd.UserId = '" + SqlEscape(objaccount.UserId) + "'";
        }

        str_query += " ORDER BY sd.MentionDate DESC";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt;
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    void UpdateResultSummary(DataTable dt)
    {
        int count = dt != null ? dt.Rows.Count : 0;
        decimal totalAmount = 0m;

        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                decimal amount;
                if (decimal.TryParse(Convert.ToString(row["Amount"]), out amount))
                {
                    totalAmount += amount;
                }
            }
        }

        litRecordCount.Text = count.ToString();
        litTotalPayable.Text = totalAmount.ToString("N2");
        pnlSummary.Visible = count > 0;
        lblResultSummary.Text = count > 0
            ? count + " record(s) found for the selected criteria."
            : "No level income records found for the selected criteria.";
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 10;
    }

    void BuildExternalPager()
    {
        pnlPager.Controls.Clear();

        DataTable dt = IncomeData;
        if (!PagingEnabled || dt == null || dt.Rows.Count == 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = ActivePageSize;
        int totalRecords = dt.Rows.Count;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"saving-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);

        for (int i = 0; i < totalPages; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerLink(string text, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-active\">" + text + "</span>"));
            return;
        }

        if (!enabled)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"saving-pager-btn is-disabled\">" + text + "</span>"));
            return;
        }

        LinkButton link = new LinkButton();
        link.Text = text;
        link.CssClass = "saving-pager-btn";
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

        PageIndex = pageIndex;
        BindGrid();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = 0;
        if (IncomeData != null)
        {
            BindGrid();
        }
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!PagingEnabled)
        {
            return dataItemIndex + 1;
        }

        return (PageIndex * ActivePageSize) + dataItemIndex + 1;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        FormatAmountLabel((Label)e.Row.FindControl("lblAmount"));
    }

    void FormatAmountLabel(Label label)
    {
        if (label == null)
        {
            return;
        }

        decimal amount;
        if (decimal.TryParse(label.Text, out amount))
        {
            label.Text = amount.ToString("N2");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

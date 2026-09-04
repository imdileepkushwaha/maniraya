using DataTier;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_RepurchaseLevelIncome : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadReport(true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport(true);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadReport(true);
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

    void LoadReport(bool allowPaging)
    {
        DataTable dt = GetIncomeData();
        BindSummary(dt);
        GridView1.AllowPaging = allowPaging;
        GridView1.PageSize = 25;
        if (allowPaging && dt != null && dt.Rows.Count > 0)
        {
            int totalPages = (int)Math.Ceiling(dt.Rows.Count / 25.0);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    DataTable GetIncomeData()
    {
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
SELECT
    CONVERT(CHAR, I.FromDate, 103) AS FromDate,
    CONVERT(CHAR, I.ToDate, 103) AS ToDate,
    CONVERT(CHAR, I.Entrydate, 103) AS EntryDate,
    I.Userid,
    ISNULL(U.UserName, '') AS UserName,
    I.Fromuserid,
    ISNULL(F.UserName, '') AS FromUserName,
    ISNULL(I.OrderNO, '') AS OrderNO,
    CASE WHEN ISNULL(I.LevelNo, 0) = 0 THEN 'Self Income' ELSE 'Level Income' END AS IncomeType,
    CASE WHEN ISNULL(I.LevelNo, 0) = 0 THEN 'Self' ELSE CONVERT(VARCHAR(10), I.LevelNo) END AS LevelNo,
    ISNULL(I.FROMuserCommission, 0) AS BV,
    ISNULL(I.IncomePer, 0) AS IncomePer,
    ISNULL(I.income, 0) AS Income,
    ISNULL(I.Admincharge, 0) AS AdminCharge,
    ISNULL(I.TDS, 0) AS TDS,
    ISNULL(I.Paybleamount, 0) AS PaybleAmount,
    CASE WHEN ISNULL(I.Status, 0) = 0 THEN 'DUE' ELSE 'PAID' END AS Status1
FROM ROIDailyLevelIncomeTB I WITH (NOLOCK)
LEFT JOIN UserDetail U WITH (NOLOCK) ON LTRIM(RTRIM(U.UserId)) = LTRIM(RTRIM(I.Userid))
LEFT JOIN UserDetail F WITH (NOLOCK) ON LTRIM(RTRIM(F.UserId)) = LTRIM(RTRIM(I.Fromuserid))
WHERE 1 = 1";

        if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue
            && fromDate.Year > 1900 && toDate.Year > 1900)
        {
            sql += " AND CONVERT(date, ISNULL(I.FromDate, I.Entrydate)) >= CONVERT(date, '" + fromDate.ToString("yyyy-MM-dd") + "')"
                + " AND CONVERT(date, ISNULL(I.ToDate, I.Entrydate)) <= CONVERT(date, '" + toDate.ToString("yyyy-MM-dd") + "')";
        }

        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            sql += " AND LTRIM(RTRIM(I.Userid)) = '" + SqlEscape(txtuserid.Text.Trim()) + "'";
        }

        if (ddlIncomeType.SelectedValue == "Self")
        {
            sql += " AND ISNULL(I.LevelNo, 0) = 0";
        }
        else if (ddlIncomeType.SelectedValue == "Level")
        {
            sql += " AND ISNULL(I.LevelNo, 0) > 0";
        }

        sql += " ORDER BY I.Entrydate DESC, I.ID DESC";

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql) ?? new DataTable();
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
        return dt;
    }

    void BindSummary(DataTable dt)
    {
        int count = dt == null ? 0 : dt.Rows.Count;
        decimal cashback = 0;
        decimal level = 0;
        decimal total = 0;
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                decimal income = GetDecimal(row["Income"]);
                total += income;
                if (string.Equals(Convert.ToString(row["IncomeType"]), "Self Income", StringComparison.OrdinalIgnoreCase))
                {
                    cashback += income;
                }
                else
                {
                    level += income;
                }
            }
        }

        lblSummary.Text = "Records " + count
            + " | Self Income " + cashback.ToString("N2")
            + " | Level " + level.ToString("N2")
            + " | Total " + total.ToString("N2");
    }

    static decimal GetDecimal(object value)
    {
        decimal parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }
        return decimal.TryParse(Convert.ToString(value), out parsed) ? parsed : 0m;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void ExportToExcel(object sender, ImageClickEventArgs e)
    {
        GridView1.PageIndex = 0;
        LoadReport(false);

        if (GridView1.HeaderRow == null)
        {
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RepurchaseLevelIncome.xls");
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
                    cell.CssClass = "textmode";
                }
            }
            GridView1.RenderControl(hw);
            Response.Write(@"<style> .textmode { } </style>");
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

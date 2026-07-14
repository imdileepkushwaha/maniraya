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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtUserId.Text = string.Empty;
        ddStatus.ClearSelection();
        ListItem pending = ddStatus.Items.FindByValue("Pending");
        if (pending != null)
        {
            pending.Selected = true;
        }
        LoadReport();
    }

    void LoadReport()
    {
        DataTable dt = GetReportData();
        GridView1.DataSource = dt;
        GridView1.DataBind();
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

        sql.Append(" ORDER BY sa.InstallmentDate DESC, sa.instno ASC, sa.id DESC");

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
        if (GridView1.Rows.Count == 0)
        {
            LoadReport();
        }

        if (GridView1.Rows.Count == 0)
        {
            Message.Show("No records available to export.");
            return;
        }

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

using DataTier;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingLevelIncomeReport : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            txtuserid.Text = Session["userid"].ToString();
            loadReport();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        loadReport();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        loadReport();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        loadReport();
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

    void loadReport()
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

        string sql = @"SELECT sd.*, ud.username
FROM SavingLevelIncomeDetail sd WITH (NOLOCK)
LEFT JOIN userdetail ud WITH (NOLOCK) ON sd.UserId = ud.userid
WHERE sd.LevelNo > 1
  AND sd.UserId = '" + SqlEscape(userId) + "'";

        if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
        {
            sql += " AND CONVERT(date, sd.mentiondate) >= CONVERT(date, '" + fromDate.ToString("yyyy-MM-dd") + "')"
                + " AND CONVERT(date, sd.mentiondate) <= CONVERT(date, '" + toDate.ToString("yyyy-MM-dd") + "')";
        }

        sql += " ORDER BY sd.mentiondate DESC";

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

        ApplyPaging(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        ddlRecordFilter.ClearSelection();
        ListItem allItem = ddlRecordFilter.Items.FindByText("All");
        if (allItem != null)
        {
            allItem.Selected = true;
        }
        GridView1.PageIndex = 0;
        GridView1.AllowPaging = false;
        loadReport();

        if (GridView1.HeaderRow == null)
        {
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=SavingLevelIncomeReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.HeaderRow.BackColor = Color.White;
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

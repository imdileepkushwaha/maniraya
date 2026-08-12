using BusinessLogicTier;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_BVReport : System.Web.UI.Page
{
    clsUser objuser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            loadReport();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadReport();
    }

    void loadReport()
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

        string noOfRows = "";
        if (ddlRecordFilter.SelectedItem.Text == "25")
            noOfRows = "top 25";
        else if (ddlRecordFilter.SelectedItem.Text == "50")
            noOfRows = "top 50";
        else if (ddlRecordFilter.SelectedItem.Text == "100")
            noOfRows = "top 100";
        else if (ddlRecordFilter.SelectedItem.Text == "500")
            noOfRows = "top 500";

        DataTable dt = objuser.getBVReport(
            Session["userid"].ToString(),
            fromDate,
            toDate,
            ddlType.SelectedValue,
            txtUseId.Text.Trim(),
            noOfRows);

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

    protected void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "BVReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
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

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        loadReport();
        ExportGridToExcel();
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadReport();
    }
}

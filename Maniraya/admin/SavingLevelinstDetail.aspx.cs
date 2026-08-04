using BusinessLogicTier;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class admin_SavingLevelinstDetail : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] == null)
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }

    void loaduser()
    {
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

        objaccount.UserId = txtuserid.Text;
        DataTable dt = getInstallmentIncome(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    public DataTable getInstallmentIncome(clsAccount objaccount)
    {
        // Only income linked to installment number greater than 1
        string str_query = @"SELECT sd.*, ud.username
            FROM SavingLevelIncomeDetail sd WITH (NOLOCK)
            LEFT JOIN userdetail ud WITH (NOLOCK) ON sd.UserId = ud.userid
            WHERE ISNULL(sd.InstNo, 0) > 1 ";

        if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
        {
            str_query += " AND CONVERT(date, sd.mentiondate) >= CONVERT(date, '" + SqlEscape(objaccount.FromDate.ToString("yyyy-MM-dd")) + "')";
            str_query += " AND CONVERT(date, sd.mentiondate) <= CONVERT(date, '" + SqlEscape(objaccount.ToDate.ToString("yyyy-MM-dd")) + "')";
        }

        if (!string.IsNullOrWhiteSpace(objaccount.UserId))
        {
            str_query += " AND sd.UserId = '" + SqlEscape(objaccount.UserId.Trim()) + "'";
        }

        int instNo;
        if (!string.IsNullOrWhiteSpace(txtInstNo.Text) && int.TryParse(txtInstNo.Text.Trim(), out instNo) && instNo > 1)
        {
            str_query += " AND sd.InstNo = " + instNo;
        }

        str_query += " ORDER BY sd.mentiondate DESC, sd.InstNo ASC";

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (GridView1.HeaderRow == null)
        {
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=SavingLevelInstDetail.xls");
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

            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}

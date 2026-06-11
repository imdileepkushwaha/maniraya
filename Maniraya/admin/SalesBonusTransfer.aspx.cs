using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesBonusTransfer : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loaduser()
    {
      
        DataTable dt = new DataTable();
        dt = objCL.getClosingDateSalesnew();
        DDlstFromdate.DataSource = dt;
        DDlstFromdate.DataTextField = "ClosingDate";
        DDlstFromdate.DataValueField = "ClosingDate";
        DDlstFromdate.DataBind();
        ListItem li = new ListItem("Select Date", "0");
        DDlstFromdate.Items.Insert(0, li);
    }
    void loaddata()
    {
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
            string Fromdate = str[0].ToString();
            string Todatedate = str[1].ToString();

            DataTable Dt = objCL.getClosingReportSales(Fromdate, Todatedate, "");
            GridView1.DataSource = Dt;
            GridView1.DataBind();
            if (Dt.Rows.Count > 0)
            {
                Button1.Visible = true;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaddata();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }   
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DownlineReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
           

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

            //style to format numbers to string
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
            string Fromdate = str[0].ToString();
            string Todatedate = str[1].ToString();
            string res = objCL.UpdateTransferSales(Fromdate, Todatedate);
            if (res == "1")
            {
                string popupScript = "alert('transfer successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                loaduser();
                GridView1.DataSource = null;
                GridView1.DataBind();
                Button1.Visible = false;

            }
            else
            {
                string popupScript = "alert('error occured');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }
}
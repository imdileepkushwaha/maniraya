using BusinessLogicTier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JoiningClosingReportMonthly : System.Web.UI.Page
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            CheckBox chk = (CheckBox)e.Row.FindControl("chk");
            if (lblstatus.Text == "PAID")
            {
                chk.Checked = false;
                chk.Enabled = false;

            }
        }
    }
    protected void btnpay_Click(object sender, EventArgs e)
    {
        int chkcount = 0;
        ArrayList IdList = new ArrayList();
        ArrayList UserList = new ArrayList();
        ArrayList AmountList = new ArrayList();
        ArrayList MobileList = new ArrayList();
        ArrayList TList = new ArrayList();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            Label lblPaybleAmount = (Label)GridView1.Rows[i].FindControl("lblmathingBv");
            Label lblId = (Label)GridView1.Rows[i].FindControl("lblId");
            Label lbluserid = (Label)GridView1.Rows[i].FindControl("lbluserid");
            Label LabMobile = (Label)GridView1.Rows[i].FindControl("LabMobile");
            TextBox TxtTransaction = (TextBox)GridView1.Rows[i].FindControl("TxtTransaction");
            

            if (chk.Checked == true)
            {
                IdList.Add(lblId.Text);
                UserList.Add(lbluserid.Text);
                AmountList.Add(lblPaybleAmount.Text);
                MobileList.Add(LabMobile.Text);
                TList.Add(TxtTransaction.Text);
                chkcount = chkcount + 1;
            }

        }

        if (chkcount == 0)
        {
            Message.Show("Please select any row");
            return;
        }
        else
        {
            int c = objCL.TransferPayoutmonthly(IdList, UserList, AmountList, MobileList, TList);
            if (c == 1)
            {
                loaddata();
                Message.Show("Payout Transferred Successfully");
            }
            else
            {
                Message.Show("Some Error Occurred");
            }
        }

    }
    void loaduser()
    {
      
        DataTable dt = new DataTable();
        dt = objCL.getMonthJoiningClosingDate();
        DDlstFromdate.DataSource = dt;
        DDlstFromdate.DataTextField = "ClosingDate";
        DDlstFromdate.DataValueField = "ClosingDate";
        DDlstFromdate.DataBind();
        ListItem li = new ListItem("Select Date", "0");
        DDlstFromdate.Items.Insert(0, li);
    }
  
    void loaddata()
    {
        string Fromdate = "";
        string Todatedate = "";
        string UserId = "";
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
           Fromdate = str[0].ToString();
           Todatedate = str[1].ToString();
        }
        DataTable Dt = objCL.getMonthleyJoiningClosingReportDue(Fromdate, Todatedate, TxtUserId.Text);
        GridView1.DataSource = Dt;
        GridView1.DataBind();
        if (Dt != null && Dt.Rows.Count > 0)
        {
            btnpay.Visible = true;
        }
        else
        {
            btnpay.Visible = false;
        }
        
    }
    protected void chckchanged(object sender, EventArgs e)
    {

        CheckBox chckheader = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox1");

        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox chckrw = (CheckBox)row.FindControl("chk");

            if (chckheader.Checked == true)
            {
                chckrw.Checked = true;
            }
            else
            {
                chckrw.Checked = false;
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
        Response.AddHeader("content-disposition", "attachment;filename=PayoutReport.xls");
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

       
    protected void BtnMessage_Click(object sender, EventArgs e)
    {
       int chkcount = 0;
        ArrayList IdList = new ArrayList();
        ArrayList UserList = new ArrayList();
        ArrayList AmountList = new ArrayList();
        ArrayList MobileList = new ArrayList();
        ArrayList TList = new ArrayList();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            Label lblPaybleAmount = (Label)GridView1.Rows[i].FindControl("lblPaybleAmount");
            Label lblId = (Label)GridView1.Rows[i].FindControl("lblId");
            Label lbluserid = (Label)GridView1.Rows[i].FindControl("lblusername");
            Label LabMobile = (Label)GridView1.Rows[i].FindControl("LabMobile");
            TextBox TxtTransaction = (TextBox)GridView1.Rows[i].FindControl("TxtTransaction");
            

            if (chk.Checked == true)
            {
                IdList.Add(lblId.Text);
                UserList.Add(lbluserid.Text);
                AmountList.Add(lblPaybleAmount.Text);
                MobileList.Add(LabMobile.Text);
                TList.Add(TxtTransaction.Text);
                chkcount = chkcount + 1;
            }

        }

        if (chkcount == 0)
        {
            Message.Show("Please select any row");
            return;
        }
        else
        {
            int c = objCL.TransferPayoutMessgesend(IdList, UserList, AmountList, MobileList, TList);
            if (c == 1)
            {
                loaddata();
                Message.Show("Message send Successfully");
            }
            else
            {
                Message.Show("Some Error Occurred");
            }
        }

    }
    
}
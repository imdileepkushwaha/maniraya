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

public partial class TransferReferalRepurchase : System.Web.UI.Page
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
        dt = objCL.getClosingDate("REFERALREPURCHASE");
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
        string UserId = TxtUserId.Text;
        if (DDlstFromdate.SelectedIndex != 0)
        {
            string[] str = DDlstFromdate.SelectedValue.Split('=');
            Fromdate = str[0].ToString();
            Todatedate = str[1].ToString();

        }
       // objaccount.UserId = txtuserid.Text;

        DataTable Dt = objCL.getTransferReport(Fromdate, Todatedate, UserId, "REFERALREPURCHASE");
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
    protected void btnpay_Click(object sender, EventArgs e)
    {
        int chkcount = 0;
        ArrayList IdList = new ArrayList();
        ArrayList UserList = new ArrayList();
        ArrayList AmountList = new ArrayList();
        ArrayList MobileList = new ArrayList();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            Label lblPaybleAmount = (Label)GridView1.Rows[i].FindControl("lblPaybleAmount");
            Label lblId = (Label)GridView1.Rows[i].FindControl("lblId");
            Label lbluserid = (Label)GridView1.Rows[i].FindControl("lbluserid");
            Label LblMobile = (Label)GridView1.Rows[i].FindControl("LblMobile");
            
            if (chk.Checked == true)
            {
                IdList.Add(lblId.Text);
                UserList.Add(lbluserid.Text);
                AmountList.Add(lblPaybleAmount.Text);
                MobileList.Add(LblMobile.Text);
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
            int c = objCL.TransferPayoutReferal(IdList, UserList, AmountList, MobileList);
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
}
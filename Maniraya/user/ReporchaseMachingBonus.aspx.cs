using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ReporchaseMachingBonus : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
            }
            else
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
        DataTable dt = new DataTable();
        dt = objaccount.getrepurchaseMachingReport(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    Decimal LeftBv = 0;
    Decimal RightBv = 0;
    Decimal MachingBv = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblleft = e.Row.FindControl("lblmobile") as Label;
            Label lblright = e.Row.FindControl("lblmobile1") as Label;
            LeftBv += Convert.ToDecimal(lblleft.Text);
            RightBv += Convert.ToDecimal(lblright.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (LeftBv >= RightBv)
            {
                MachingBv = RightBv;
            }
            if (RightBv > LeftBv)
            {
                MachingBv = LeftBv;
            }
            e.Row.Cells[3].Text = LeftBv.ToString();
            e.Row.Cells[4].Text = RightBv.ToString();
            e.Row.Cells[5].Text = "Maching : "+MachingBv.ToString();
            LeftBv = 0;
            RightBv = 0;
            MachingBv = 0;
        }
    }
}
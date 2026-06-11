using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DownlineReport : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                filldashboard();
                loadLeftuser();
                loadRightuser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadLeftuser()
    {
        objUser.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objUser.getUserDownlineLeft(objUser);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    void loadRightuser()
    {
        objUser.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objUser.getUserDownlineRight(objUser);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }


    void filldashboard()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable LeftDt = objUser.getUserDownlineLeft(objUser);
        DataTable RightDt = objUser.getUserDownlineRight(objUser);
        LblTotalLeft.Text = LeftDt.Rows.Count.ToString();
        LblTotalright.Text = RightDt.Rows.Count.ToString();
        //DataRow[] Sactiveusers = LeftDt.Select("Status='active'");
        //DataRow[] Sdeactiveusers = RightDt.Select("Status='active'");
        //DataRow[] SLdeactiveusers = LeftDt.Select("Status='deactive'");
        //DataRow[] SRdeactiveusers = RightDt.Select("Status='deactive'");
        //Lblactiveleft.Text = Sactiveusers.Length.ToString();
        //LblActiveRight.Text = Sdeactiveusers.Length.ToString();
        //LblInactiveleft.Text = SLdeactiveusers.Length.ToString();
        //LblInActiveRight.Text = SRdeactiveusers.Length.ToString();
        //DataTable LeftDirectt = objUser.getUserleftDirect(objUser);
        //DataTable RightDirectt = objUser.getUserrightDirect(objUser);
        //LblLeftDirect.Text = LeftDirectt.Rows[0][0].ToString();
        //LblRightDirect.Text = RightDirectt.Rows[0][0].ToString();
        //string Fromdate = string.Empty;
        //string Todatedate = string.Empty;

      //  DataTable Dt = objCL.getdailyClosingReport(Fromdate, Todatedate, Session["UserId"].ToString());
        //lblleftbv.Text = Dt.Rows[0]["leftbv"].ToString();
        //lblrightbv.Text = Dt.Rows[0]["rightbv"].ToString();


        //  DataSet Ds = objuser.getTotalamount(objuser);
        //  LblBinaryIncome.Text = Ds.Tables[0].Rows[0][0].ToString();
        // LblDirectIncome.Text = Ds.Tables[1].Rows[0][0].ToString();
        //  LblSponserIncome.Text = Ds.Tables[2].Rows[0][0].ToString();
        // LblRoinIncome.Text = Ds.Tables[3].Rows[0][0].ToString();
        //lblTotalincome.Text = Convert.ToString(Convert.ToDecimal(LblBinaryIncome.Text)

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadLeftuser();
        loadRightuser();
        LblTotalLeft.Text = GridView1.Rows.Count.ToString();
        LblTotalright.Text = GridView2.Rows.Count.ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            if (lblstatus != null)
            {
                lblstatus.CssClass = lblstatus.Text == "Unpaid"
                    ? "team-status-badge team-status-unpaid"
                    : "team-status-badge team-status-paid";
            }
        }
    }
}
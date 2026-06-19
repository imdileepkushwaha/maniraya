using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using DataTier;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class user_UserDirectAssociates : System.Web.UI.Page
{
    clsUser objclsUser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            FillAssociatesDetails();
            filldashboard();

        }
    }


    void filldashboard()
    {
        objclsUser.UserId = Session["userid"].ToString();
        //DataTable LeftDt = objUser.getUserDownlineLeft(objUser);
        //DataTable RightDt = objUser.getUserDownlineRight(objUser);
        //LblTotalLeft.Text = LeftDt.Rows.Count.ToString();
        //LblTotalright.Text = RightDt.Rows.Count.ToString();
        //DataRow[] Sactiveusers = LeftDt.Select("Status='active'");
        //DataRow[] Sdeactiveusers = RightDt.Select("Status='active'");
        //DataRow[] SLdeactiveusers = LeftDt.Select("Status='deactive'");
        //DataRow[] SRdeactiveusers = RightDt.Select("Status='deactive'");
        //Lblactiveleft.Text = Sactiveusers.Length.ToString();
        //LblActiveRight.Text = Sdeactiveusers.Length.ToString();
        //LblInactiveleft.Text = SLdeactiveusers.Length.ToString();
        //LblInActiveRight.Text = SRdeactiveusers.Length.ToString();
        DataTable LeftDirectt = getUserleftDirect(objclsUser);
        DataTable RightDirectt = getUserrightDirect(objclsUser);
        LblLeftDirect.Text = LeftDirectt.Rows[0][0].ToString();
        LblRightDirect.Text = RightDirectt.Rows[0][0].ToString();
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

    public DataTable getUserleftDirect(clsUser objUser)
    {
        string str_query = "SELECT count(u.sponserid)  FROM UserDetail u WHERE u.SponserId='" + objUser.UserId + "' AND u.StandingPosition='1'"; 
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    void FillAssociatesDetails()
    {
        objclsUser.UserId = Session["userid"].ToString();
        
       

        DataTable dt = new DataTable();
        string Rowno = "";
        if (ddlRecordFilter.SelectedValue == "10")
        {
            Rowno = " top 10 ";
        }
        if (ddlRecordFilter.SelectedValue == "25")
        {
            Rowno = " top 25 ";
        }
        if (ddlRecordFilter.SelectedValue == "50")
        {
            Rowno = " top 50 ";
        }
        if (ddlRecordFilter.SelectedValue == "100")
        {
            Rowno = " top 100 ";
        }
        if (ddlRecordFilter.SelectedValue == "500")
        {
            Rowno = " top 500 ";
        }


        objclsUser.Pincode = Rowno;
        dt = getUserDirect(objclsUser);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }


    public DataTable getUserDirect(clsUser objUser)
    {


        SqlParameter[] para ={
                                     new SqlParameter ("@UserId",objUser.UserId),
                                    
                                     };
        DataSet ds = DBHelper.ExecuteQuery("GetDirectStandingWiseLeft", para);

        DataTable dt = ds.Tables[0];

        return dt;
    }

    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblStatus");
            if (lblstatus != null)
            {
                lblstatus.CssClass = lblstatus.Text == "Unpaid"
                    ? "team-status-badge team-status-unpaid"
                    : "team-status-badge team-status-paid";
            }
        }
    }


    public DataTable getUserrightDirect(clsUser objUser)
    {
        string str_query = "SELECT count(u.sponserid)  FROM UserDetail u WHERE u.SponserId='" + objUser.UserId + "' AND u.StandingPosition='2'";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAssociatesDetails();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillAssociatesDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
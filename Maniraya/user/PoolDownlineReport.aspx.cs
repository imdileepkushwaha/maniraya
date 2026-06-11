using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PoolDownlineReport : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsClosing objaccount = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                TxtUserId.Text = Session["userid"].ToString();
                TxtUserId.Enabled = false;
                // loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void FillPoolID(string userid)
    {
        
        DataTable dt = new DataTable();
        dt = getPoolID(TxtUserId.Text,"1");
        GrdPoolId.DataSource = dt;
        GrdPoolId.DataBind();
    }
    public DataTable getPoolID(string userid, string PoolNo)
    {
        string str_query = "";
        if (PoolNo == "1")
        {
            str_query = "SELECT poolid,Entrytype FROM userpoolOnetb where 1=1 ";
        }
        if (PoolNo == "2")
        {
            str_query = "SELECT poolid,Entrytype FROM userpoolTwotb where 1=1 ";
        }
        if (PoolNo == "3")
        {
            str_query = "SELECT poolid,Entrytype FROM userpoolthreetb where 1=1 ";
        }

        if (PoolNo == "4")
        {
            str_query = "SELECT poolid,Entrytype FROM userpoolFourtb where 1=1 ";
        }
        if (PoolNo == "5")
        {
            str_query = "SELECT poolid,Entrytype FROM userpoolFivetb where 1=1 ";
        }


        str_query += " and userid='" + userid + "'";

        str_query += " order by id";
        DataTable ds = null;
        ObjData.StartConnection();
        try
        {
            ds = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            ds = null;
        }
        ObjData.EndConnection();
        return ds;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillPoolID(TxtUserId.Text);
    }
    void FillPoolReport(string Poolid)
    {

        DataTable dt = new DataTable();
        dt = getUserPoolReport(Poolid, DDlstpoolNo.SelectedValue);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getUserPoolReport(string PoolId, String poolNO)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "GetPoolReportLevelwise";
            SqlParameter[] parameter = {
                    new SqlParameter("@PoolID",PoolId),
                      new SqlParameter("@LevelNO",poolNO),



                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblGroupid = (Label)GrdPoolId.Rows[index].FindControl("lblGroupid");
            FillPoolReport(lblGroupid.Text);


        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
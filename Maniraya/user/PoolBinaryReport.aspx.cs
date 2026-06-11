using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;

public partial class PoolBinaryReport : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsClosing objaccount = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            txtuserid.Text = Session["userid"].ToString();
           // txtuserid.Enabled = false;
            //f1.Src = "pooltest.aspx?SuperId=" + txtuserid.Text + "";
            FillPoolID(txtuserid.Text);
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void FillPoolID(string userid)
    {

        DataTable dt = new DataTable();
        dt = getPoolID(txtuserid.Text, "1");
        GrdPoolId.DataSource = dt;
        GrdPoolId.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblGroupid = (Label)GrdPoolId.Rows[index].FindControl("lblGroupid");
            f1.Src = "pooltest.aspx?SuperId=" + lblGroupid.Text + "";


        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            objuser.UserId = Session["userid"].ToString();
            //DataTable Dt = objuser.getUserDownlineChkNew(objuser, txtuserid.Text);
            //if (Dt.Rows.Count > 0)
            //{
                //ltiframe.Text = @"<iframe src=""test.aspx?SuperId=" + txtuserid.Text + @""" style=""width:100%;height:700px;""  />";
               // f1.Src = "pooltest.aspx?SuperId=" + txtuserid.Text + "";
            FillPoolID(txtuserid.Text);
            //}
        }
        else
        {
            Message.Show("Enter User Id...!!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    Data ObjData = new Data();
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
}
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

public partial class CareerMyDirect : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsClosing objCL = new clsClosing();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                TxtUserId.Text = Session["userid"].ToString();
                TxtUserId.Enabled = false;
              //  loadplanlist();
              //  DDlstpoolNo.Enabled = false;
               // loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadplanlist()
    {

        DataTable dt = new DataTable();
     //   dt = objUser.getPlanList(Session["userid"].ToString(), "4");
        foreach (DataRow Dr in dt.Rows)
        {
            if (Dr["activatedate"].ToString() != "")
            {
                DDlstpoolNo.SelectedValue = Dr["planid"].ToString();
            }
        }
    }
    void loaddata()
    {
        string Fromdate = "";
        string Todatedate = "";
        string UserId = "";
        objCL.GenerateUserId = TxtUserId.Text;
        if (txtfromdate.Text != "")
        {
            objCL.Todate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objCL.Todate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objCL.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objCL.Todate = DateTime.MinValue;
        }
      //  objCL.Planname = DDlstpoolNo.SelectedValue;
        //if (DDlstFromdate.SelectedIndex != 0)
        //{
        //    string[] str = DDlstFromdate.SelectedValue.Split('=');
        //   Fromdate = str[0].ToString();
        //   Todatedate = str[1].ToString();
        //}
        DataTable Dt = MyNetworkCareerReport(TxtUserId.Text, DDlstpoolNo.SelectedValue, txtfromdate.Text, txttodate.Text,DDlstPosition.SelectedValue);
        GridView1.DataSource = Dt;
        GridView1.DataBind();

    }
    public DataTable MyNetworkCareerReport(string UserId, string Planid, string Fromdate, string todate, String Position)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "getCareerMyDirect";
            SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                      new SqlParameter("@PlanId",Planid), 
                        new SqlParameter("@Fromdate",Fromdate), 
                          new SqlParameter("@Todate",todate), 
                           new SqlParameter("@Position",Position), 
                  
                 
                  
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaddata();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Leadershipedt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lbluserid");

            fillleadershiprankdata(lblid.Text);
             ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }
        if (e.CommandName == "rankedt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lbluserid");

            fillrankdata(lblid.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    private void fillrankdata(string Userid)
    {

        DataTable Dt = FillRankdataPopup(Userid);
        GridView2.DataSource = Dt;
        GridView2.DataBind();
    }
    private void fillleadershiprankdata(string Userid)
    {

        DataTable Dt = FillLeadershipRankdataPopup(Userid);
        GridView3.DataSource = Dt;
        GridView3.DataBind();
    }
    public DataTable FillRankdataPopup(string UserId)
    {
        string str_query = "SELECT A.RankName,CASE WHEN isnull((SELECT 1 FROM RankCareerBonus WHERE type=A.Id AND Userid='" + UserId + "'),'')='' THEN '' ELSE 'Achieve' END Status,(SELECT TOP 1 Todate FROM RankCareerBonus WHERE type=A.Id AND Userid='" + UserId + "') entrydate FROM RankCareerCommissionmaster A ";



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
    public DataTable FillLeadershipRankdataPopup(string UserId)
    {
        string str_query = "SELECT A.RankName,CASE WHEN isnull((SELECT 1 FROM LeadershipCareerBonus WHERE type=A.Id AND Userid='" + UserId + "'),'')='' THEN '' ELSE 'Achieve' END Status,(SELECT TOP 1 Todate FROM LeadershipCareerBonus WHERE type=A.Id AND Userid='" + UserId + "') entrydate FROM LeadershipCommissionmaster A ";



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
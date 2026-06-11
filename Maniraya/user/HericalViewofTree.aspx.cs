using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Web.Services;
using System.Data.SqlClient;
using DataTier;
public partial class user_HericalViewofTree : System.Web.UI.Page
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                if (Request.QueryString["SuperId"] != null)
                {
                    txtuserid.Text = Request.QueryString["SuperId"].ToString();
                txtuserid.Enabled = false;
                }
                else
                {
                    txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                }
                
            }
        }
    }
    [WebMethod]
    public static List<object> GetChartData(string UserId)
    {

        List<object> chartData = new List<object>();
        clsUser objU = new clsUser();
       // objU.UserId = UserId;
        objU.UserId = UserId;
        DataTable Dt = getUserReportfortree(objU);
        if (Dt.Rows.Count > 0)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                //DataTable dtpopup = getUnityTreePopupDetail(Dt.Rows[i]["EmployeeId"].ToString().ToUpper(),"1");
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<table style='width: 100%;'>");
                //sb.Append("<tr>");
                //sb.Append("<td>User Id:" + Dt.Rows[i]["EmployeeId"].ToString().ToUpper() + "</td>");
                //sb.Append("<td>User Name:" + dtpopup.Rows[0]["username"].ToString().ToUpper() + "</td>");
                //sb.Append("</tr>");
                //sb.Append("</table>");
                //Dt.Rows[i]["tooltip"] = sb;
                chartData.Add(new object[]
                    {
                         Dt.Rows[i]["EmployeeId"].ToString().ToUpper(), Dt.Rows[i]["Name"].ToString().ToUpper(), Dt.Rows[i]["Designation"].ToString().ToUpper() , Dt.Rows[i]["ReportingManager"].ToString().ToUpper()
                    });

            }
          
        }



        return chartData;
    }
    [WebMethod]
    public static List<object> Getpopup(string UserId)
    {
        List<object> chartData = new List<object>();
        return chartData;

    }
    public static DataTable getUserReportfortree(clsUser objUser)
    {

        string str_query = "";
        Data ObjData = new Data();
        str_query = @"; WITH MyCTE
AS ( SELECT userid as EmployeeId,username as Name,case when isnull(status,0)=0 then 'Deactive' else 'Active' end as Designation,parentuserid as ReportingManager,0 AS Lvl,'' as ToolTIP
FROM userdetail
WHERE UserId ='" + objUser.UserId + @"'
UNION ALL
SELECT userdetail.userid as EmployeeId,userdetail.username as Name,case when isnull(userdetail.status,0)=0 then 'Deactive' else 'Active' end as Designation,  userdetail.ParentUserId as ReportingManager,Lvl+1 AS Lvl,'' as ToolTIP 
FROM userdetail
INNER JOIN MyCTE ON userdetail.parentuserid = MyCTE.EmployeeId AND (MyCTE.Lvl+1)<=5 
WHERE userdetail.userid !='" + objUser.UserId + @"' )
SELECT MyCTE.*
FROM MyCTE left join userdetail ud on mycte.ReportingManager=ud.userid ORDER BY MyCTE.Lvl ";

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
    //[WebMethod]
    //public static List<object> GetPopup(string UserId)
    //{
    //    string s2;
    //    Data ObjData = new Data();        
    //    DataTable Dt = new DataTable();
    //    ObjData.StartConnection();
    //    try
    //    {
    //        s2 = "GetUnityTreeuserDetail";
    //        SqlParameter[] parameter = {              
    //                new SqlParameter("@Userid",UserId),                       
    //                        new SqlParameter("@PlanId","5"), 
                   
                 
                  
    //            };
    //        Dt = ObjData.RunDataTableProcedure(s2, parameter);



    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        ObjData.EndConnection();

    //    }
    //    if (Dt.Rows.Count > 0)
    //    {
    //        LblUserID.Text = Userid;
    //        LblUserName.Text = dt.Rows[0]["UserName"].ToString();
    //        LblSponserId.Text = dt.Rows[0]["Sponserid"].ToString();
    //        LblSponserName.Text = dt.Rows[0]["Sponsername"].ToString();
    //        LblDOB.Text = dt.Rows[0]["Regdate"].ToString();
    //        LblMobileno.Text = dt.Rows[0]["ActDate"].ToString();
    //        if (dt.Rows[0]["ActDate"].ToString() != "")
    //        {
    //            LblStatus.Text = "ACTIVE";
    //        }
    //        else
    //        {
    //            LblStatus.Text = "INACTIVE";
    //        }
    //        LblTodayREgLeft.Text = dt.Rows[0]["TodayRegLeft"].ToString();
    //        LblTodayREgRight.Text = dt.Rows[0]["TodayRegRight"].ToString();
    //        LblTodayActLeft.Text = dt.Rows[0]["TodayActLeft"].ToString();
    //        LblTodayActRight.Text = dt.Rows[0]["TodayActRight"].ToString();
    //        LblTotalRegLeft.Text = dt.Rows[0]["TotalRegLeft"].ToString();
    //        LblTotalRegRight.Text = dt.Rows[0]["TotalRegRight"].ToString();
    //        LblTotalACtLeft.Text = dt.Rows[0]["TotalActLeft"].ToString();
    //        LblTotalActRight.Text = dt.Rows[0]["TotalActRight"].ToString();
    //        LblRank.Text = dt.Rows[0]["Rank"].ToString();
    //        LblLbv.Text = dt.Rows[0]["leftbv"].ToString();
    //        LblRBv.Text = dt.Rows[0]["leftbv"].ToString();
    //        LblLeftsale.Text = dt.Rows[0]["LeftPurchase"].ToString();
    //        LblRightSale.Text = dt.Rows[0]["RightPurchase"].ToString();
    //        LblOwnpurchase.Text = dt.Rows[0]["OwnPurchase"].ToString();
    //        Lbldateofbirth.Text = dt.Rows[0]["DOB"].ToString();
    //    }

    //}
    public static DataTable getUnityTreePopupDetail(string UserId, string planid)
    {
        Data ObjData = new Data();
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "GetUnityTreeuserDetail";
            SqlParameter[] parameter = {              
                    new SqlParameter("@Userid",UserId),                       
                            new SqlParameter("@PlanId",planid), 
                   
                 
                  
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

   
}
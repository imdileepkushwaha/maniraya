using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;

public partial class AwardUserReport : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsUser objclsUser = new clsUser();
    clsClosing objCl = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            FillAssociatesDetails();
        }
    }

    public void FillAssociatesDetails()
    {
        DataTable dt = new DataTable();
        objCl.GenerateUserId = Session["userid"].ToString();
        dt = getUserRewardReport(objCl);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }
    public DataTable getUserRewardReport(clsClosing objCl)
    {
        //string str_query = "SELECT A.LeftId,AU.point,'0' AS balance,A.AwardName,'Yes' AS achieved,Convert(CHAR,AU.achievedate,103) AS Achievedate FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "' UNION ALL  SELECT TOP 1 A.LeftId,(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS point,A.LeftId-(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS balance,Awardname,'No' AS achieved,'Not Achieved' AS Achievedate FROM  AwardAchiver A  WHERE A.Id NOT IN (SELECT A.Id FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "')";

        string str_query = "SELECT A.LeftId,A.RightId,AU.LeftID leftpoint,AU.RightID Rightpoint,0 AS Leftbalance,0 AS Rightbalance,A.AwardName,'Yes' AS achieved,A.Amount,A.Award,A.EDUCATIONINCOME,A.CHILDEDUCATIONINCOME,Convert(CHAR,AU.achievedate,103) AS Achievedate FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "' UNION ALL SELECT  A.LeftId,A.RightId,(SELECT isnull(sum(leftuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "') AS leftpoint,(SELECT isnull(sum(rightuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "') AS Rightpoint,iif(A.LeftId-(SELECT isnull(sum(leftuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "')<0,0,A.LeftId-(SELECT isnull(sum(leftuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "')) AS Leftbalance,iif(A.RightId-(SELECT isnull(sum(rightuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "')<0,0,A.RightId-(SELECT isnull(sum(rightuser),0) FROM UserBuisnessVolumeRepurchase WHERE UserID='" + objCl.GenerateUserId + "')) AS Rightbalance,Awardname,'No' AS achieved,A.Amount,A.Award,A.EDUCATIONINCOME,A.CHILDEDUCATIONINCOME,'Not Achieved' AS Achievedate FROM  AwardAchiver A  WHERE A.Id NOT IN (SELECT A.Id FROM AwardAchiverUser AU JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN  Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "')";


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
    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.Cells[11].Text.ToUpper()=="YES")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
               // e.Row.ForeColor = System.Drawing.Color.White;
            }
            //else
            //{
            //    e.Row.BackColor = System.Drawing.Color.Green;
            //}
        }
    }
}
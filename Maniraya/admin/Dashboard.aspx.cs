using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Globalization;

public partial class admin_Dashboard : System.Web.UI.Page
{
    clsdashboard objA = new clsdashboard();
    clsAccount objaccount = new clsAccount();

   
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                Filldashboard();
                BindPurchaseChart();
                BindUserChart();
                GetDashboard();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    private void Filldashboard()
    {
        DataSet Ds = objA.Getdashboardnew();
        if (Ds.Tables[0].Rows.Count > 0)
        {
            LblUserCount.Text = Ds.Tables[0].Rows[0][0].ToString();
            LblProductCount.Text = Ds.Tables[1].Rows[0][0].ToString();
            //LblPurchaseAmount.Text = Ds.Tables[2].Rows[0][0].ToString();
            LblActiveEpin.Text = Ds.Tables[3].Rows[0][0].ToString();
            LblDepositlTotal.Text = Ds.Tables[4].Rows[0][0].ToString();
            LblDepositPending.Text = Ds.Tables[5].Rows[0][0].ToString();
            LblWithdrawlTotal.Text = Ds.Tables[6].Rows[0][0].ToString();
            LblWithdrawlPending.Text = Ds.Tables[7].Rows[0][0].ToString();
            LblNewsCount.Text = Ds.Tables[8].Rows[0][0].ToString();
            LblPurchaseProductCount.Text = Ds.Tables[9].Rows[0][0].ToString();
            LblPurchaseAmount.Text = Ds.Tables[10].Rows[0][0].ToString();
        }
    }
    private void BindPurchaseChart()
    {
        DataTable dsChartData = new DataTable();
        StringBuilder strScript = new StringBuilder();

        try
        {
            string m = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture);;
            dsChartData = objA.GetBindChartrechrge();

            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date','Recharge'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript.Append("['" + row["Date"] + "'," +
                    row["Recharge"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = { title : 'Recharge Amount weekwise', vAxis: {title: 'Amount'},  hAxis: {title: '"+m+"'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");

            ltScripts.Text = strScript.ToString();
        }
        catch
        {
        }
        finally
        {
            dsChartData.Dispose();
            strScript.Clear();
        }
    }

      
   

    private void GetDashboard()
    {
        DataTable dt = new DataTable();
        objaccount.UserId = Session["useradmin"].ToString();
        dt = objaccount.getAdminDashboard(objaccount);
        if (dt.Rows.Count > 0)
        {
            Lbltotalteamactive.Text = dt.Rows[0]["ActiveDirect"].ToString();
            Lbltodayteamactive.Text = dt.Rows[0]["TodayActiveDirect"].ToString();
            Lbltotakbusiness.Text = dt.Rows[0]["TotalBV"].ToString();
            Lbltotakbusinesstoday.Text = dt.Rows[0]["TodayTotalBV"].ToString();
           // lblpendingwithdraw.Text = dt.Rows[0]["pendingwithdraw"].ToString();

            //lbltotalbonus.Text = dt.Rows[0]["bonus"].ToString();
            Lblwithdrawal.Text = dt.Rows[0]["withdrawal"].ToString();
            Lblwithdrawaltoday.Text = dt.Rows[0]["Todaywithdrawal"].ToString();
            Lbldeposit.Text = dt.Rows[0]["deposit"].ToString();
            Lbldeposittoday.Text = dt.Rows[0]["Todaydeposit"].ToString();
            lbltotalpayout.Text = dt.Rows[0]["Todaypayout"].ToString();
            lbltotalpayouttoday.Text = dt.Rows[0]["Todaypayouttoday"].ToString();
        }
    }

    private void BindUserChart()
    {
        DataTable dsChartData1 = new DataTable();
        StringBuilder strScript1 = new StringBuilder();

        try
        {
            string m = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture); ;
            dsChartData1 = objA.GetBindChartuser();

            strScript1.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date','JoinUser'],");

            foreach (DataRow row in dsChartData1.Rows)
            {
                strScript1.Append("['" + row["Date"] + "'," +
                    row["JoinUser"] + "],");
            }
            strScript1.Remove(strScript1.Length - 1, 1);
            strScript1.Append("]);");

            strScript1.Append("var options1 = { title : 'User join weekwise', vAxis: {title: 'Number'},  hAxis: {title: '" + m + "'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            strScript1.Append(" var chart1 = new google.visualization.ComboChart(document.getElementById('Div1'));  chart1.draw(data, options1); } google.setOnLoadCallback(drawVisualization);");
            strScript1.Append(" </script>");

            Literal1.Text = strScript1.ToString();
        }
        catch
        {
        }
        finally
        {
            dsChartData1.Dispose();
            strScript1.Clear();
        }
    }  
}
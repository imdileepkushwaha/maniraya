using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using BusinessLogicTier;
using DataTier;

public partial class admin_Dashboard : Page
{
    clsdashboard objA = new clsdashboard();
    clsAccount objaccount = new clsAccount();
    Data ObjData = new Data();

    const string ApprovedStatusSql = @"(
        status = 'Approved'
        OR status = '1'
        OR LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('approved', 'approve')
    )";

    const string PendingStatusSql = @"(
        LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('pending', 'processing')
    )";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            FillAllDashboardStats();
            BindPurchaseChart();
            BindUserChart();
        }
    }

    void FillAllDashboardStats()
    {
        SetZeroDefaults();

        // Info boxes (deposit / withdraw / news / purchase pending) — simple counts.
        try
        {
            DataSet ds = objA.Getdashboardnew();
            if (ds != null && ds.Tables.Count >= 11)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LblUserCount.Text = FormatNumber(ds.Tables[0].Rows[0][0]);
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    LblProductCount.Text = FormatNumber(ds.Tables[1].Rows[0][0]);
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    LblActiveEpin.Text = FormatNumber(ds.Tables[3].Rows[0][0]); // ProductMaster count
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    LblDepositlTotal.Text = FormatNumber(ds.Tables[4].Rows[0][0]);
                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    LblDepositPending.Text = FormatNumber(ds.Tables[5].Rows[0][0]);
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    LblWithdrawlTotal.Text = FormatNumber(ds.Tables[6].Rows[0][0]);
                }

                if (ds.Tables[7].Rows.Count > 0)
                {
                    LblWithdrawlPending.Text = FormatNumber(ds.Tables[7].Rows[0][0]);
                }

                if (ds.Tables[8].Rows.Count > 0)
                {
                    LblNewsCount.Text = FormatNumber(ds.Tables[8].Rows[0][0]);
                }

                if (ds.Tables[9].Rows.Count > 0)
                {
                    LblPurchaseProductCount.Text = FormatNumber(ds.Tables[9].Rows[0][0]);
                }

                if (ds.Tables[10].Rows.Count > 0)
                {
                    LblPurchaseAmount.Text = FormatNumber(ds.Tables[10].Rows[0][0]);
                }
            }
        }
        catch
        {
        }

        // Main small-boxes from live tables (do not depend on Admin_Dashboard SP).
        string sql = @"
SELECT
    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK)) AS TotalUsers,

    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK)
     WHERE ISNULL(ActiveStatus, 0) = 1) AS TotalActiveUsers,

    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK)
     WHERE ISNULL(ActiveStatus, 0) = 1
       AND (
            CONVERT(date, ISNULL(ActivateDate, MentionDate)) = CONVERT(date, GETDATE())
            OR CONVERT(date, MentionDate) = CONVERT(date, GETDATE())
       )) AS TodayActiveUsers,

    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @")
    +
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalBusiness,

    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND CONVERT(date, COALESCE(approvedate, entrydate, mentiondate)) = CONVERT(date, GETDATE()))
    +
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND CONVERT(date, COALESCE(approvedate, entrydate, mentiondate)) = CONVERT(date, GETDATE())) AS TodayBusiness,

    (SELECT ISNULL(SUM(ISNULL(CrAmount, 0)), 0) FROM TransactionDetail WITH (NOLOCK)
     WHERE ISNULL(CrAmount, 0) > 0
       AND (
            TransactionType LIKE '%Income%'
            OR TransactionType LIKE '%Bonus%'
            OR TransactionType LIKE '%Commission%'
            OR TransactionType LIKE '%Reward%'
       )) AS TotalBonus,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalWithdrawal,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND CONVERT(date, COALESCE(approvedate, mentiondate, entrydate)) = CONVERT(date, GETDATE())) AS TodayWithdrawal,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + PendingStatusSql + @") AS PendingWithdrawal,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM DepositRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalDeposit,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM DepositRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND CONVERT(date, COALESCE(approvedate, mentiondate, entrydate)) = CONVERT(date, GETDATE())) AS TodayDeposit,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalPayout,

    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND CONVERT(date, COALESCE(approvedate, mentiondate, entrydate)) = CONVERT(date, GETDATE())) AS TodayPayout,

    (SELECT COUNT(1) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalFirstPurchase,

    (SELECT COUNT(1) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND YEAR(COALESCE(approvedate, entrydate, mentiondate)) = YEAR(GETDATE())
       AND MONTH(COALESCE(approvedate, entrydate, mentiondate)) = MONTH(GETDATE())) AS MonthFirstPurchase,

    (SELECT COUNT(1) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalInstallmentPaid,

    (SELECT COUNT(1) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND YEAR(COALESCE(approvedate, entrydate, mentiondate)) = YEAR(GETDATE())
       AND MONTH(COALESCE(approvedate, entrydate, mentiondate)) = MONTH(GETDATE())) AS MonthInstallmentPaid,

    (SELECT COUNT(1) FROM AwardAchiverUser WITH (NOLOCK)) AS AwardRewardCount,

    (SELECT COUNT(1) FROM ProductMaster WITH (NOLOCK)) AS ProductCount
";

        if (!TryBindMainStats(sql))
        {
            // Fallback without approvedate/mentiondate on some tables.
            string fallbackSql = @"
SELECT
    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK)) AS TotalUsers,
    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK) WHERE ISNULL(ActiveStatus, 0) = 1) AS TotalActiveUsers,
    (SELECT COUNT(1) FROM UserDetail WITH (NOLOCK)
     WHERE ISNULL(ActiveStatus, 0) = 1
       AND CONVERT(date, MentionDate) = CONVERT(date, GETDATE())) AS TodayActiveUsers,
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @")
    +
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalBusiness,
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @" AND CONVERT(date, entrydate) = CONVERT(date, GETDATE()))
    +
    (SELECT ISNULL(SUM(ISNULL(amount, 0)), 0) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @" AND CONVERT(date, entrydate) = CONVERT(date, GETDATE())) AS TodayBusiness,
    (SELECT ISNULL(SUM(ISNULL(CrAmount, 0)), 0) FROM TransactionDetail WITH (NOLOCK)
     WHERE ISNULL(CrAmount, 0) > 0
       AND (TransactionType LIKE '%Income%' OR TransactionType LIKE '%Bonus%' OR TransactionType LIKE '%Commission%')) AS TotalBonus,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalWithdrawal,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @" AND CONVERT(date, mentiondate) = CONVERT(date, GETDATE())) AS TodayWithdrawal,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + PendingStatusSql + @") AS PendingWithdrawal,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM DepositRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalDeposit,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM DepositRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @" AND CONVERT(date, mentiondate) = CONVERT(date, GETDATE())) AS TodayDeposit,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalPayout,
    (SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) FROM WithdrawlRequest WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @" AND CONVERT(date, mentiondate) = CONVERT(date, GETDATE())) AS TodayPayout,
    (SELECT COUNT(1) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalFirstPurchase,
    (SELECT COUNT(1) FROM SavingAccountDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND YEAR(entrydate) = YEAR(GETDATE()) AND MONTH(entrydate) = MONTH(GETDATE())) AS MonthFirstPurchase,
    (SELECT COUNT(1) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @") AS TotalInstallmentPaid,
    (SELECT COUNT(1) FROM SavingAccountInstallmentDetail WITH (NOLOCK)
     WHERE " + ApprovedStatusSql + @"
       AND YEAR(entrydate) = YEAR(GETDATE()) AND MONTH(entrydate) = MONTH(GETDATE())) AS MonthInstallmentPaid,
    (SELECT COUNT(1) FROM AwardAchiverUser WITH (NOLOCK)) AS AwardRewardCount,
    (SELECT COUNT(1) FROM ProductMaster WITH (NOLOCK)) AS ProductCount
";
            TryBindMainStats(fallbackSql);
        }
    }

    bool TryBindMainStats(string sql)
    {
        try
        {
            ObjData.StartConnection();
            try
            {
                DataTable dt = ObjData.RunDataTable(sql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                DataRow row = dt.Rows[0];
                LblUserCount.Text = FormatNumber(row["TotalUsers"]);
                Lbltotalteamactive.Text = FormatNumber(row["TotalActiveUsers"]);
                Lbltodayteamactive.Text = FormatNumber(row["TodayActiveUsers"]);
                Lbltotakbusiness.Text = FormatAmount(row["TotalBusiness"]);
                Lbltotakbusinesstoday.Text = FormatAmount(row["TodayBusiness"]);
                lbltotalbonus.Text = FormatAmount(row["TotalBonus"]);
                Lblwithdrawal.Text = FormatAmount(row["TotalWithdrawal"]);
                Lblwithdrawaltoday.Text = FormatAmount(row["TodayWithdrawal"]);
                lblpendingwithdraw.Text = FormatAmount(row["PendingWithdrawal"]);
                Lbldeposit.Text = FormatAmount(row["TotalDeposit"]);
                Lbldeposittoday.Text = FormatAmount(row["TodayDeposit"]);
                lbltotalpayout.Text = FormatAmount(row["TotalPayout"]);
                lbltotalpayouttoday.Text = FormatAmount(row["TodayPayout"]);
                LblTotalFirstPurchase.Text = FormatNumber(row["TotalFirstPurchase"]);
                LblMonthFirstPurchase.Text = FormatNumber(row["MonthFirstPurchase"]);
                LblTotalInstallmentPaid.Text = FormatNumber(row["TotalInstallmentPaid"]);
                LblMonthInstallmentPaid.Text = FormatNumber(row["MonthInstallmentPaid"]);
                lable1.Text = FormatNumber(row["AwardRewardCount"]);
                LblActiveEpin.Text = FormatNumber(row["ProductCount"]);
                return true;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    void SetZeroDefaults()
    {
        LblUserCount.Text = "0";
        Lbltotalteamactive.Text = "0";
        Lbltodayteamactive.Text = "0";
        Lbltotakbusiness.Text = "0";
        Lbltotakbusinesstoday.Text = "0";
        lbltotalbonus.Text = "0";
        Lblwithdrawal.Text = "0";
        Lblwithdrawaltoday.Text = "0";
        lblpendingwithdraw.Text = "0";
        Lbldeposit.Text = "0";
        Lbldeposittoday.Text = "0";
        lbltotalpayout.Text = "0";
        lbltotalpayouttoday.Text = "0";
        LblTotalFirstPurchase.Text = "0";
        LblMonthFirstPurchase.Text = "0";
        LblTotalInstallmentPaid.Text = "0";
        LblMonthInstallmentPaid.Text = "0";
        lable1.Text = "0";
        LblActiveEpin.Text = "0";
        LblDepositlTotal.Text = "0";
        LblDepositPending.Text = "0";
        LblWithdrawlTotal.Text = "0";
        LblWithdrawlPending.Text = "0";
        LblNewsCount.Text = "0";
        LblPurchaseProductCount.Text = "0";
        LblProductCount.Text = "0";
        LblPurchaseAmount.Text = "0";
    }

    static string FormatNumber(object value)
    {
        decimal n;
        if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out n)
            || decimal.TryParse(Convert.ToString(value), out n))
        {
            return Math.Round(n, 0, MidpointRounding.AwayFromZero).ToString("N0", CultureInfo.InvariantCulture);
        }

        return "0";
    }

    static string FormatAmount(object value)
    {
        decimal n;
        if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out n)
            || decimal.TryParse(Convert.ToString(value), out n))
        {
            if (n == Math.Truncate(n))
            {
                return n.ToString("N0", CultureInfo.InvariantCulture);
            }

            return n.ToString("N2", CultureInfo.InvariantCulture);
        }

        return "0";
    }

    private void BindPurchaseChart()
    {
        DataTable dsChartData = new DataTable();
        StringBuilder strScript = new StringBuilder();

        try
        {
            string m = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture);
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

            strScript.Append("var options = { title : 'Recharge Amount weekwise', vAxis: {title: 'Amount'},  hAxis: {title: '" + m + "'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
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

    private void BindUserChart()
    {
        DataTable dsChartData1 = new DataTable();
        StringBuilder strScript1 = new StringBuilder();

        try
        {
            dsChartData1 = objA.GetBindChartuser();

            int totalJoins = 0;
            foreach (DataRow row in dsChartData1.Rows)
            {
                if (row["JoinUser"] != DBNull.Value)
                {
                    totalJoins += Convert.ToInt32(row["JoinUser"]);
                }
            }

            strScript1.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date','Joins','Trend'],");

            foreach (DataRow row in dsChartData1.Rows)
            {
                int val = 0;
                if (row["JoinUser"] != DBNull.Value)
                {
                    val = Convert.ToInt32(row["JoinUser"]);
                }
                strScript1.Append("['" + row["Date"] + "'," + val + "," + val + "],");
            }
            strScript1.Remove(strScript1.Length - 1, 1);
            strScript1.Append("]);");

            strScript1.Append(@"
                var options1 = {
                    legend: { position: 'none' },
                    chartArea: { width: '92%', height: '80%', left: '6%', right: '2%', top: '6%', bottom: '14%' },
                    bar: { groupWidth: '50%' },
                    seriesType: 'bars',
                    series: {
                        0: { type: 'bars', color: '#e52d27' },
                        1: { type: 'area', color: '#f1f5f9', areaOpacity: 0.7, lineWidth: 0, visibleInLegend: false }
                    },
                    vAxis: {
                        gridlines: { color: '#f1f5f9', count: 6 },
                        textStyle: { color: '#94a3b8', fontName: 'Segoe UI', fontSize: 11 },
                        baselineColor: '#f1f5f9'
                    },
                    hAxis: {
                        gridlines: { color: 'transparent' },
                        textStyle: { color: '#94a3b8', fontName: 'Segoe UI', fontSize: 11 },
                        baselineColor: '#e2e8f0'
                    }
                };
                var chart1 = new google.visualization.ComboChart(document.getElementById('Div1'));
                chart1.draw(data, options1);
                var badge = document.getElementById('lblTotalJoinsThisWeek');
                if (badge) { badge.innerText = '" + totalJoins + @"'; }
            }
            google.setOnLoadCallback(drawVisualization);");
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

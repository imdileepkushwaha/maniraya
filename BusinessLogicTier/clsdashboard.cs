using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;

namespace BusinessLogicTier
{
    public class clsdashboard
    {
        Data ObjData = new Data();
        public DataSet Getdashboard()
        {
            ObjData.StartConnection();
            DataSet Ds = new DataSet();
            string Query = "SElect Count(*) from UserDetail;SElect Count(*) from ProductMaster;SElect isnull(sum(Dramount),0) from TransactionDetail where transactiontype='Purchase';SElect Count(*) from EPinMaster where epinstatus='active';SELECT Count(*) FROM DepositRequest;SELECT Count(*) FROM DepositRequest where status='pending';SELECT Count(*) FROM WithdrawlRequest;SELECT Count(*) FROM WithdrawlRequest where status='pending';SELECT Count(*) FROM NewsMaster;SELECT Sum(Quantity) FROM PurchaseProductMaster;";
            Ds = ObjData.RunSelectQuery(Query);
            ObjData.EndConnection();
            return Ds;
        }
        public DataSet Getdashboardnew()
        {
            ObjData.StartConnection();
            DataSet Ds = new DataSet();
            string Query = "SElect Count(*) from UserDetail;SElect isnull(sum(totalamount),0) from PurchaseProductMaster   where Rstatus=1;SElect isnull(sum(rechargeamount),0) from rechargerequest where status='SUCCESS';SElect Count(*) from ProductMaster;SELECT Count(*) FROM DepositRequest;SELECT Count(*) FROM DepositRequest where status='pending';SELECT Count(*) FROM WithdrawlRequest;SELECT Count(*) FROM WithdrawlRequest where status='pending';SELECT Count(*) FROM NewsMaster;SElect isnull(sum(totalamount),0) from PurchaseProductMaster   where Rstatus=0;SElect Count(*) from FranchiseeDetail;";
            Ds = ObjData.RunSelectQuery(Query);
            ObjData.EndConnection();
            return Ds;
        }
        public DataTable GetBindChart()
        {
            ObjData.StartConnection();
            DataTable Ds = new DataTable();
            DateTime Dt = DateTime.Now;
            int lastDayOfMonth = DateTime.DaysInMonth(Dt.Year, Dt.Month);
            int year = Dt.Year;
            int month = Dt.Month;
            DateTime startdate=Convert.ToDateTime(month.ToString()+"/"+"01/"+year.ToString());
            string Sqluser = "";
            string Sqlpurchase = "";
            string Sqlepin = "";
            string Dateshow = "";
            int rem = lastDayOfMonth % 4;
            for (int i = 1; i <= 4; i++)
            {
                 Dateshow = ((i * 7)-6).ToString()+" to "+(i * 7).ToString();


                 Sqluser += "SELECT isnull(sum(Dramount),0) as purchase,'" + Dateshow + "' as Date FROM TransactionDetail WHERE MentionDate>'" + startdate + "' AND  MentionDate<'" + startdate.AddDays(7) + "' and transactiontype='Purchase'";
                startdate = startdate.AddDays(7);
                if (i != 4)
                {
                    Sqluser += " union all ";
                }
            }
            if (rem > 0)
            {
                Sqluser += " union all ";
                Dateshow = ((4 * 7)).ToString() + " to " + ((4 * 7) + rem).ToString();
                startdate = startdate.AddDays(-1);
                Sqluser += "SELECT isnull(sum(Dramount),0) as purchase,'" + Dateshow + "' as Date FROM TransactionDetail WHERE MentionDate>'" + startdate + "' AND  MentionDate<'" + startdate.AddDays(rem + 1) + "' and transactiontype='Purchase'";
                startdate = startdate.AddDays(rem + 1);               
            }
            Ds = ObjData.RunDataTable(Sqluser);
            ObjData.EndConnection();
            return Ds;
        }
        public DataTable GetBindChartrechrge()
        {
            ObjData.StartConnection();
            DataTable Ds = new DataTable();
            DateTime Dt = DateTime.Now;
            int lastDayOfMonth = DateTime.DaysInMonth(Dt.Year, Dt.Month);
            int year = Dt.Year;
            int month = Dt.Month;
            DateTime startdate = Convert.ToDateTime(month.ToString() + "/" + "01/" + year.ToString());
            string Sqluser = "";
            string Sqlpurchase = "";
            string Sqlepin = "";
            string Dateshow = "";
            int rem = lastDayOfMonth % 4;
            for (int i = 1; i <= 4; i++)
            {
                Dateshow = ((i * 7) - 6).ToString() + " to " + (i * 7).ToString();


                Sqluser += "SELECT isnull(sum(rechargeamount),0) as Recharge,'" + Dateshow + "' as Date FROM RechargeRequest WHERE entrydate>'" + startdate + "' AND  entrydate<'" + startdate.AddDays(7) + "' and status='success'";
                startdate = startdate.AddDays(7);
                if (i != 4)
                {
                    Sqluser += " union all ";
                }
            }
            if (rem > 0)
            {
                Sqluser += " union all ";
                Dateshow = ((4 * 7)).ToString() + " to " + ((4 * 7) + rem).ToString();
                startdate = startdate.AddDays(-1);
                Sqluser += "SELECT isnull(sum(rechargeamount),0) as Recharge,'" + Dateshow + "' as Date FROM RechargeRequest WHERE entrydate>'" + startdate + "' AND  entrydate<'" + startdate.AddDays(rem + 1) + "' and status='success'";
                startdate = startdate.AddDays(rem + 1);
            }
            Ds = ObjData.RunDataTable(Sqluser);
            ObjData.EndConnection();
            return Ds;
        }
        public DataTable GetBindChartuser()
        {
            ObjData.StartConnection();
            DataTable Ds = new DataTable();
            DateTime Dt = DateTime.Now;
            int lastDayOfMonth = DateTime.DaysInMonth(Dt.Year, Dt.Month);
            int year = Dt.Year;
            int month = Dt.Month;
            DateTime startdate = Convert.ToDateTime(month.ToString() + "/" + "01/" + year.ToString());
            string Sqluser = "";
            string Sqlpurchase = "";
            string Sqlepin = "";
            string Dateshow = "";
            int rem = lastDayOfMonth % 4;
            for (int i = 1; i <= 4; i++)
            {
                Dateshow = ((i * 7) - 6).ToString() + " to " + (i * 7).ToString();


                Sqluser += "SELECT Count(UserId) as JoinUser,'" + Dateshow + "' as Date FROM UserDetail WHERE MentionDate>'" + startdate + "' AND  MentionDate<'" + startdate.AddDays(7) + "' ";
                startdate = startdate.AddDays(7);
                if (i != 4)
                {
                    Sqluser += " union all ";
                }
            }
            if (rem > 0)
            {
                Sqluser += " union all ";
                Dateshow = ((4 * 7)).ToString() + " to " + ((4 * 7) + rem).ToString();
                startdate = startdate.AddDays(-1);
                Sqluser += "SELECT Count(UserId) as JoinUser,'" + Dateshow + "' as Date FROM UserDetail WHERE MentionDate>'" + startdate + "' AND  MentionDate<'" + startdate.AddDays(rem + 1) + "' ";
                startdate = startdate.AddDays(rem + 1);
            }
            Ds = ObjData.RunDataTable(Sqluser);
            ObjData.EndConnection();
            return Ds;
        }
    }
   
   
}

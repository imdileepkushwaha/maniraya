using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Collections;
using ARA_StringHunt;

namespace BusinessLogicTier
{
   
    public class clsClosing
    {
         Data ObjData = new Data();
         public string GenerateUserId { get; set; }
         public DateTime Fromdate { get; set; }
         public DateTime Todate { get; set; }
         public Decimal Amount { get; set; }
         public string paystatus { get; set; }
         public Decimal Percentage { get; set; }



         public DataTable getMonthJoiningClosingDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from WeeklyClosingMaster";

             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {

                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }

                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
          public int TransferPayoutmonthly(ArrayList arrId, ArrayList arrUser, ArrayList arrAmount, ArrayList arrmobile, ArrayList TList)
         {
             int c = 0;
             int arrcount = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {
                 string str = "Proc_TransferPayout_Binary";

                 for (int i = 0; i < arrId.Count; i++)
                 {

                     SqlParameter[] sqm = new SqlParameter[]
                {
                 new SqlParameter("@Amount", arrAmount[i].ToString()),
                 new SqlParameter("@UserId", arrUser[i].ToString()),
                 new SqlParameter("@id", arrId[i].ToString()),
                   new SqlParameter("@paymentTransactionId", TList[i].ToString())

                };
                     DataTable dt = ObjData.RunDataTableProcedureTRans(str, tr, sqm);

                     if (dt.Rows[0][0].ToString() == "-1")
                     {
                         c = 0;
                         tr.Rollback();
                         break;
                     }

                     //    string url = string.Concat(new string[]
                     //{
                     //    "http://sms.sandhyasoftware.com/vendorsms/pushsms.aspx?user=mdsm&password=mdsm12&msisdn=",
                     //    arrmobile[i].ToString(),
                     //    "&sid=MANAVD&msg=Congratulation ! You have achieved Payout Rs. " + arrAmount[i].ToString() + " successfully from mdsm.in",
                     //    "&fl=0&gwid=2"
                     //});
                     //    string Result = url.CallURL();

                     //    //string smsBody = "Congratulation ! You have achieved Payout Rs. " + arrAmount[i].ToString() + " successfully from mdsm.in";
                     //    //string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + arrmobile[i].ToString() + "&sender=ETOPUP&smstext=" + smsBody + "&smsformat=TEXT&format=json";
                     //    //string Result = url.CallURL();
                     //    string s2 = "insert into SendSms(CreateDate,Mobile,Result,Message)  values (getdate(),'" + arrmobile[i].ToString() + "','" + Result + "','" + url + "') ";
                     //    ObjData.RunInsUpDelQueryTrans(s2, tr);
                     arrcount = arrcount + 1;
                 }

                 if (arrcount == arrId.Count)
                 {
                     c = 1;
                     tr.Commit();
                 }
                 else
                 {
                     c = 0;
                     tr.Rollback();
                 }

             }
             catch (Exception ex)
             {
                 c = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return c;
         }
   public DataTable getMonthleyJoiningClosingReportDue(string FromDate, string Todate, string UserId)
         {
             {
                 string str_query = "";
                 str_query = "  SELECT w.directincome,W.AwardIncome,w.SelfPurchaseIncomee,w.matchingincomee, W.directorincomee,w.leadershipincomee, W.golddirectorincomee, W.crowndirectorIncomee, W.platinumdirectorIncomee, W.diamonddirectorIncomee, w.totalincome,U.PhonePay,U.UPINo,U.BhimNo,W.TotalIncome,W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,W.tds,W.tdsper,W.admincharge,W.paybleamount,W.Weekno,U.Mobile,U.accountno,U.ifsccode,U.AccountHolderName FROM WeeklyClosing W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1 and W.Status=0";


                 if (FromDate != string.Empty)
                 {
                     str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
                 }
                 if (Todate != string.Empty)
                 {
                     str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
                 }
                 if (UserId != string.Empty)
                 {
                     str_query += " and W.userid='" + UserId + "'";
                 }
                 str_query += " order by W.Weekno";
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
    public int TransferPayoutMessgesend(ArrayList arrId, ArrayList arrUser, ArrayList arrAmount, ArrayList arrmobile, ArrayList TList)
         {
             int c = 0;
             int arrcount = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {


                 for (int i = 0; i < arrId.Count; i++)
                 {

                     SqlParameter[] sqm = new SqlParameter[]
                {
                 new SqlParameter("@Amount", arrAmount[i].ToString()),
                 new SqlParameter("@UserId", arrUser[i].ToString()),
                 new SqlParameter("@id", arrId[i].ToString()),
                   new SqlParameter("@paymentTransactionId", TList[i].ToString())

                };


                     string url = string.Concat(new string[]
               {
                   "http://sms.sandhyasoftware.com/vendorsms/pushsms.aspx?user=OJASSM&password=OJASSM &msisdn=",
                   arrmobile[i].ToString(),
                   "&sid=OJASSM &msg=CONGRATULATION DEAR "+arrUser[i].ToString()+" !!! You are best !!! Today you have earn "+arrAmount[i].ToString()+"Rs. amount from OJASNETWORK...Do you best now. And be very successful person in OJASNETWORK..All the best . Regards OJASNETWORK",
                   "&fl=0&gwid=2"
               });
                     string Result = url.CallURL();


                     arrcount = arrcount + 1;
                 }

                 if (arrcount == arrId.Count)
                 {
                     c = 1;
                     tr.Commit();
                 }
                 else
                 {
                     c = 0;
                     tr.Rollback();
                 }

             }
             catch (Exception ex)
             {
                 c = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return c;
         }


    public DataTable getJoiningClosingReportMonth(string FromDate, string Todate, string UserId)
    {
        {
            string str_query = "";
            str_query = "   SELECT isnull(W.directincome,0) as DirectIncome, isnull(W.AwardIncome,0) as AwardIncome, isnull(W.totalincome,0) as totalincome,isnull(tdsper,0) as tdsper,isnull(Adminper,0) as Adminper,W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.admincharge,W.Status, CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,W.tds,W.paybleamount,W.Weekno,U.Mobile FROM WeeklyClosing W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1";
            if (FromDate != string.Empty)
            {
                str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
            }
            if (Todate != string.Empty)
            {
                str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
            }
            if (UserId != string.Empty)
            {
                str_query += " and W.userid='" + UserId + "'";
            }
            str_query += " order by W.Weekno";
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
         public int ClaculateClosing(clsClosing objCl,string fromdate,string todate)
         {

             int h = 0;
             string res = "";
             Decimal PreviousBv = 0;
             Decimal MatchinBv = 0;
             Decimal LeftBv = 0;
             Decimal RightBv = 0;
             int LeftUser = 0;
             int RightUser = 0;
             int weekno = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 res = "select * from WeeklyClosingMaster where Cast(Todate as date)>='" + fromdate + "'";
                 DataSet Ds = ObjData.RunSelectQueryTrans(res, tr);
                 if (Ds.Tables[0].Rows.Count == 0)
                 {

                     res = "select isnull(max(weekno),0)+1 as Id from WeeklyClosingMaster";
                     DataTable Dtw = ObjData.RunSelectQueryTTrans(res, tr);
                     weekno = Convert.ToInt32(Dtw.Rows[0][0].ToString());


                     res = "SELECT UserId,isnull(Sum(LeftUser),0) AS LeftUser,isnull(Sum(RightUser),0) AS RightUser,isnull(Sum(Leftbv),0) AS LeftBv,isnull(Sum(Rightbv),0) AS Rightbv,isnull(Sum(Selfbv),0) AS Selfbv FROM UserBuisnessVolume WHERE purchaseType='J' and Cast(Createdate as date)<='" + todate + "'  GROUP BY UserId HAVING isnull(Sum(LeftUser),0)>0 AND  isnull(Sum(RightUser),0)>0 ";
                     DataSet Ds1 = ObjData.RunSelectQueryTrans(res, tr);

                     foreach (DataRow Dr in Ds1.Tables[0].Rows)
                     {
                         PreviousBv = 0;
                         MatchinBv = 0;
                         LeftBv = 0;
                         RightBv = 0;
                         LeftUser = 0;
                         RightUser = 0;

                         res = "SElect isnull(sum(MatchingBv),0) as PBV,isnull(sum(TotalLeftId),0) as leftId,isnull(sum(TotalRightId),0) as RightId from WeeklyClosing where UserId='" + Dr["UserId"].ToString() + "'";
                         DataSet Ds2 = ObjData.RunSelectQueryTrans(res, tr);
                         PreviousBv = Convert.ToDecimal(Ds2.Tables[0].Rows[0]["PBV"].ToString());

                         LeftBv = Convert.ToDecimal(Dr["LeftBv"].ToString());
                         RightBv = Convert.ToDecimal(Dr["RightBv"].ToString());

                         LeftUser = Convert.ToInt32(Dr["LeftUser"].ToString()) - Convert.ToInt32(Ds2.Tables[0].Rows[0]["leftId"].ToString());
                         RightUser = Convert.ToInt32(Dr["RightUser"].ToString()) - Convert.ToInt32(Ds2.Tables[0].Rows[0]["RightId"].ToString());

                         LeftBv = LeftBv - PreviousBv;
                         RightBv = RightBv - PreviousBv;

                         if (LeftBv >= RightBv)
                         {
                             MatchinBv = RightBv;
                         }
                         else
                         {
                             MatchinBv = LeftBv;
                         }
                         int CurrentBonus = 0;
                         decimal NewBonus = 0;

                         res = "SElect isnull(U.slabid,0) as BonusID,(SElect count(*) from userdetail where parentuserid=U.userid and Standingposition='1') as DirectLeft,(SElect count(*) from userdetail where parentuserid=U.userid and Standingposition='2') as DirectRight from UserDetail U where U.UserId='" + Dr["UserId"].ToString() + "' and isnull(status,0)=1";
                         DataSet DsBonusid = ObjData.RunSelectQueryTrans(res, tr);
                         CurrentBonus = Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["BonusID"].ToString());

                         if (Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["DirectLeft"].ToString()) > 0 && Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["DirectRight"].ToString()) > 0)
                         {


                             res = "SElect isnull(Bonus,0) as Bonus,Capping from JoiningBonusMaster where planId='" + CurrentBonus + "' AND  tobv>=" + MatchinBv + " AND frombv<=" + MatchinBv + "";
                             DataSet Ds3 = ObjData.RunSelectQueryTrans(res, tr);
                            
                             Decimal Capping = Convert.ToDecimal(Ds3.Tables[0].Rows[0]["Capping"].ToString());
                             if (MatchinBv >= Capping)
                             {
                                 NewBonus = Capping;
                             }
                             else
                             {
                                 NewBonus = MatchinBv;
                             }
                             Decimal CommissionPer = Convert.ToDecimal(Ds3.Tables[0].Rows[0]["Bonus"].ToString());

                             decimal Amount = (NewBonus * CommissionPer) * 1 / 100;
                             decimal Tds = 0;
                             if (Amount > 0)
                             {
                                 Tds = Math.Round(Amount * 5 / 100,2);

                                 res = "insert into WeeklyClosing(Fromdate,ToDate,UserID,TotalLeftID,TotalRightID,TotalLeftBV,TotalRightBV,MatchingBV,CommissionPer,Commission,Status,PreviousSlab,CurrentSlab,GenerateDate,TDS,CFLeftbv,CFRightbv,PaybleAmount,weekno)values('" + fromdate + "','" + todate + "','" + Dr["UserId"].ToString() + "','" + LeftUser + "','" + RightUser + "','" + LeftBv + "','" + RightBv + "','" + NewBonus + "','" + CommissionPer + "','" + Amount + "','" + 0 + "','" + CurrentBonus + "','" + CurrentBonus + "',getdate(),'" + Tds + "','" + (LeftBv - MatchinBv) + "','" + (RightBv - MatchinBv) + "','" + (Amount - Tds) + "','" + weekno + "')";
                                 ObjData.RunInsUpDelQueryTrans(res, tr);
                             }
                         }
                         h = 1;

                     }
                     res = "insert into WeeklyClosingMaster(Fromdate,ToDate,CreateDate,weekno)values('" + fromdate + "','" + todate + "','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','" + weekno + "')";
                     ObjData.RunInsUpDelQueryTrans(res, tr);

                 }
                 else
                 {
                     h = 2;
                 }
                 if (h == 1)
                 {
                    //tr.Commit();

                 }

               
              
              
              

             }
             catch (Exception ex)
             {
                 h = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return h;
         }
         public DataTable getPreviousDate()
         {
             string str_query = "select Max(Todate) from WeeklyClosingMaster ";


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
         public DataTable getPreviousDateSales()
         {
             string str_query = "select Max(Todate) from SalesClosingMaster ";


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
         public DataTable getrepurchasePreviousDate()
         {
             string str_query = "select Max(Todate) from WeeklyRepurchaseClosingMaster ";


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
         public DataTable getClosingReport(string FromDate,string Todate,string UserId)
         {
             string str_query = "SELECT W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno FROM WeeklyClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.userid='" + UserId + "'";
             }
             str_query += " order by W.Weekno";
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
        public DataTable getUserPoolReport(string PoolId,String poolNO)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "GetPoolReport";
                SqlParameter[] parameter = {
                    new SqlParameter("@PoolID",PoolId),
                      new SqlParameter("@PoolNO",poolNO),



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
        public DataTable getPoolID(string userid,string PoolNo)
        {
            string str_query = "";
            if (PoolNo=="1")
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
        public DataTable getPoolClosingReport(clsClosing objCL)
         {
             string str_query = "SELECT P.id,Convert(CHAR,P.Fromdate,103) AS Fromdate,Convert(CHAR,P.ToDate,103) AS Todate,P.UserID,P.PoolNo,P.LevelNo,P.commission,P.adminper,P.admincharge,P.tdsper,P.tds,P.paybleamount,P.Status,CASE WHEN P.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName FROM PoolClosing P INNER JOIN Userdetail U ON P.UserID=U.UserId where 1=1 ";

             if (objCL.Fromdate != DateTime.MinValue && objCL.Fromdate != DateTime.MinValue)
             {
                 str_query += "  and cast( W.Fromdate as date)  >= cast('" + objCL.Fromdate + "' as date)   and cast( W.Todate as date)    <= cast('" + objCL.Todate + "' as date)  ";
             }

             if (objCL.GenerateUserId != null && objCL.GenerateUserId != string.Empty)
             {
                 str_query += " and P.userid='" + objCL.GenerateUserId + "'";
             }
             str_query += " order by P.id";
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
         public DataTable getGroupClosingReport(clsClosing objCL)
         {
             string str_query = "SELECT W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.groupID,W.Commissionper,W.commission,W.adminper,W.admincharge,W.tdsper,W.tds,15+Convert(decimal(18,2),W.paybleamount) as paybleamount ,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName FROM GroupClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (objCL.Fromdate != DateTime.MinValue && objCL.Fromdate != DateTime.MinValue)
             {
                 str_query += "  and cast( W.Fromdate as date)  >= cast('" + objCL.Fromdate + "' as date)   and cast( W.Todate as date)    <= cast('" + objCL.Todate + "' as date)  ";
             }

             if (objCL.GenerateUserId !=null && objCL.GenerateUserId != string.Empty)
             {
                 str_query += " and W.userid='" + objCL.GenerateUserId + "'";
             }
             str_query += " order by W.id";
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

     
        public DataTable getGroupReport(clsClosing objCL)
         {
             string str_query = "SELECT UT.Userid,U.UserName,UT.entrydate,UT.GROUPID FROM UserTopupTb UT JOIN userdetail U ON UT.Userid=U.UserId WHERE GROUPID IS NOT NULL AND Type='A' and 1=1 ";

             if (objCL.Fromdate != DateTime.MinValue && objCL.Fromdate != DateTime.MinValue)
             {
                 str_query += "  and cast( UT.entrydate as date)  >= cast('" + objCL.Fromdate + "' as date)   and cast( UT.entrydate as date)    <= cast('" + objCL.Todate + "' as date)  ";
             }

             if (objCL.GenerateUserId != null && objCL.GenerateUserId != string.Empty)
             {
                 str_query += " and UT.userid='" + objCL.GenerateUserId + "'";
             }
             if (objCL.paystatus != null && objCL.paystatus != string.Empty)
             {
                 str_query += " and UT.GroupId='" + objCL.paystatus + "'";
             }
            
             str_query += " order by UT.id";
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
        
         public DataTable getClosingReportSales(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,W.RechargeAmount,W.DTHConnection,W.RechargeBV,W.DTHBV FROM SalesClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.userid='" + UserId + "'";
             }
             str_query += " order by W.Weekno";
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
         public DataTable getrepurchaseClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.TotalLeftBV,W.TotalRightBV,W.TotalselfBV,W.MatchingBV,W.calculateBV,W.CommissionPer,W.Commission,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,cfleftbv,cfrightbv,W.fortnightno FROM WeeklyRepurchaseClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.userid='" + UserId + "'";
             }
             str_query += " order by W.fortnightno";


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
         public DataTable getpayoutREportReport(string UserId)
         {
             string str_query = "SELECT U.UserId,U.UserName,(SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId) AS GroupmatchingBonus,(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId) AS RepurchaseBonus,0 AS LifeStyleBonus,0 AS TravelBonus,0 AS carBonus,(SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId) AS TotalAmount,Round(((SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId))*.05,2) AS Tds,Round(((SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId))*.05,2) AS admincharge,((SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId))-((SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId))*.05-((SELECT isnull(sum(commission),0) FROM WeeklyClosing WHERE UserID=U.UserId)+(SELECT isnull(sum(commission),0) FROM WeeklyRepurchaseClosing WHERE UserID=U.UserId))*.05 AS finalamount FROM UserDetail U WHERE 1=1";

             //if (FromDate != string.Empty)
             //{
             //    str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             //}
             //if (Todate != string.Empty)
             //{
             //    str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             //}
             if (UserId != string.Empty)
             {
                 str_query += " and U.userid='" + UserId + "'";
             }
             //str_query += " order by W.fortnightno";


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
         public DataTable getClosingDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from DailyClosingMaster";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getClosingDateSales()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from SalesClosingMaster";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getClosingRepurchaseDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from WeeklyRepurchaseClosingMaster";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getrepurchaseClosingDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from WeeklyRepurchaseClosingMaster";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public int ClaculateRepurchaseClosing(clsClosing objCl, string fromdate, string todate)
         {

             int h = 0;
             string res = "";
             Decimal PreviousBv = 0;
             Decimal MatchinBv = 0;
             Decimal LeftBv = 0;
             Decimal RightBv = 0;
             Decimal Leftnode = 0;
             Decimal Rightnode = 0;
             Decimal SelfBv = 0;
             Decimal CalculateBv = 0;
             SqlConnection cn;
             int FortNightNo = 0;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 res = "select * from WeeklyRepurchaseClosingMaster where Cast(Todate as date)>='" + fromdate + "'";
                 DataSet Ds = ObjData.RunSelectQueryTrans(res, tr);
                 if (Ds.Tables[0].Rows.Count == 0)
                 {

                     res = "select isnull(max(FortNightNo),0)+1 as Id from WeeklyRepurchaseClosingMaster";
                     DataTable Dtw = ObjData.RunSelectQueryTTrans(res, tr);
                     FortNightNo = Convert.ToInt32(Dtw.Rows[0][0].ToString());

                     res = "SELECT UserId,isnull(Sum(LeftUser),0) AS LeftUser,isnull(Sum(RightUser),0) AS RightUser,isnull(Sum(Leftbv),0) AS LeftBv,isnull(Sum(Rightbv),0) AS Rightbv,isnull(Sum(Selfbv),0) AS Selfbv FROM UserBuisnessVolume WHERE purchaseType='R' and Cast(Createdate as date)<='" + todate + "' GROUP BY UserId ";
                     DataSet Ds1 = ObjData.RunSelectQueryTrans(res, tr);

                     foreach (DataRow Dr in Ds1.Tables[0].Rows)
                     {
                           PreviousBv = 0;
                           MatchinBv = 0;
                           LeftBv = 0;
                           RightBv = 0;
                           SelfBv = 0;
                           CalculateBv = 0;
                           Leftnode = 0;
                           Rightnode = 0;

                         res = "SElect isnull(sum(MatchingBv),0) as PBV,isnull(sum(TotalselfBv),0) as selfBV from WeeklyRepurchaseClosing where UserId='" + Dr["UserId"].ToString() + "'";
                         DataSet Ds2 = ObjData.RunSelectQueryTrans(res, tr);
                         PreviousBv = Convert.ToDecimal(Ds2.Tables[0].Rows[0]["PBV"].ToString());
                         Decimal selfBv1 = Convert.ToDecimal(Ds2.Tables[0].Rows[0]["selfBV"].ToString());

                         LeftBv = Convert.ToDecimal(Dr["LeftBv"].ToString());
                         RightBv = Convert.ToDecimal(Dr["RightBv"].ToString());
                         SelfBv = Convert.ToDecimal(Dr["Selfbv"].ToString()) - selfBv1;

                         LeftBv = LeftBv - PreviousBv;
                         RightBv = RightBv - PreviousBv;

                         if (LeftBv > RightBv)
                         {

                             RightBv = RightBv + SelfBv;
                            // MatchinBv = RightBv;
                            // Rightnode = RightBv + SelfBv;                           
                         }
                         else 
                         {
                             //MatchinBv = LeftBv;
                             //Leftnode = LeftBv + SelfBv;
                             LeftBv = LeftBv + SelfBv;
                         }
                         if (LeftBv >= RightBv)
                         {
                             MatchinBv = RightBv;
                         }
                         else
                         {
                             MatchinBv = LeftBv;
                         }

                         //if (Leftnode >= Rightnode)
                         //{
                         //    CalculateBv = Rightnode;
                         //}
                         //else
                         //{
                         //    CalculateBv = Leftnode;
                         //}
                         int CurrentBonus = 0;
                         decimal NewBonus = 0;

                         res = "SElect isnull(U.SlabId,0) as BonusID,(SElect count(*) from userdetail where parentuserid=U.userid and Standingposition='1') as DirectLeft,(SElect count(*) from userdetail where parentuserid=U.userid and Standingposition='2') as DirectRight from userdetail U where U.UserId='" + Dr["UserId"].ToString() + "' and isnull(U.Status,0)='1'";
                         DataSet DsBonusid = ObjData.RunSelectQueryTrans(res, tr);
                         CurrentBonus = Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["BonusID"].ToString());

                         //if (Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["DirectLeft"].ToString()) > 0 && Convert.ToInt32(DsBonusid.Tables[0].Rows[0]["DirectRight"].ToString()) > 0)
                         if (DsBonusid.Tables[0].Rows.Count>0)
                         {


                             res = "SElect isnull(Bonus,0) as Bonus,Capping from RepurchaseBonusMaster where  tobv>=" + MatchinBv + " AND frombv<=" + MatchinBv + "";
                             DataSet Ds3 = ObjData.RunSelectQueryTrans(res, tr);
                             Decimal Capping = Convert.ToDecimal(Ds3.Tables[0].Rows[0]["Capping"].ToString());
                             //if (MatchinBv >= Capping)
                             //{
                             //    NewBonus = Capping;
                             //}
                             //else
                             //{
                             //    NewBonus = MatchinBv;
                             //}
                            // NewBonus = MatchinBv;

                             Decimal CommissionPer = Convert.ToDecimal(Ds3.Tables[0].Rows[0]["Bonus"].ToString());

                             decimal Amount = (MatchinBv * CommissionPer) * 1 / 100;

                             if (Amount > 0)
                             {

                                 res = "insert into WeeklyRepurchaseClosing(Fromdate,ToDate,UserID,TotalLeftBV,TotalRightBV,TotalSelfBV,MatchingBV,calculatebv,CommissionPer,Commission,Status,GenerateDate,CFLeftBv,CFRightBv,FortnightNo)values('" + fromdate + "','" + todate + "','" + Dr["UserId"].ToString() + "','" + LeftBv + "','" + RightBv + "','" + SelfBv + "','" + MatchinBv + "','" + CalculateBv + "','" + CommissionPer + "','" + Amount + "','" + 0 + "',getdate(),'" + (LeftBv - MatchinBv) + "','" + (RightBv - MatchinBv) + "','" + FortNightNo + "')";
                                 ObjData.RunInsUpDelQueryTrans(res, tr);
                             }
                         }
                        

                     }
                     res = "insert into WeeklyRepurchaseClosingMaster(Fromdate,ToDate,CreateDate,FortnightNo)values('" + fromdate + "','" + todate + "','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','" + FortNightNo + "')";
                     ObjData.RunInsUpDelQueryTrans(res, tr);
                     h = 1;
                 }
                 else
                 {
                     h = 2;
                 }
                 if (h == 1)
                 {
                     tr.Commit();

                 }






             }
             catch (Exception ex)
             {
                 h = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return h;
         }
         public int ClaculateClosingJoining(clsClosing objCl, string fromdate, string todate)
         {

             int h = 0;
             string res = "";
             Decimal PreviousBv = 0;
             Decimal MatchinBv = 0;
             Decimal LeftBv = 0;
             Decimal RightBv = 0;
             int LeftUser = 0;
             int RightUser = 0;
             int weekno = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 res = "select * from WeeklyClosingMaster where Cast(Todate as date)>='" + fromdate + "'";
                 DataSet Ds = ObjData.RunSelectQueryTrans(res, tr);
                 if (Ds.Tables[0].Rows.Count == 0)
                 {

                     string s2 = "ActivationIncentive";
                     SqlParameter[] parameter = {              
                    new SqlParameter("@Todate",todate),                   
                };
                     res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                 }
                 if (res == "t")
                 {
                     tr.Commit();
                     h = 1;
                 }






             }
             catch (Exception ex)
             {
                 h = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return h;
         }
         public int ClaculateClosingSales(clsClosing objCl, string fromdate, string todate)
         {

             int h = 0;
             string res = "";
             Decimal PreviousBv = 0;
             Decimal MatchinBv = 0;
             Decimal LeftBv = 0;
             Decimal RightBv = 0;
             int LeftUser = 0;
             int RightUser = 0;
             int weekno = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 res = "select * from SalesClosingMaster where Cast(Todate as date)>='" + fromdate + "'";
                 DataSet Ds = ObjData.RunSelectQueryTrans(res, tr);
                 if (Ds.Tables[0].Rows.Count == 0)
                 {

                     string s2 = "SalesIncentive";
                     SqlParameter[] parameter = {              
                    new SqlParameter("@Todate",todate),                   
                };
                     res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                 }
                 if (res == "t")
                 {
                     tr.Commit();
                     h = 1;
                 }






             }
             catch (Exception ex)
             {
                 h = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return h;
         }
         public DataTable getClosingDatenewnew()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from WeeklyClosingMaster where status='0'";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getClosingDateSalesnew()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from SalesClosingMaster where status='0'";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public string UpdateTransfer(string FromDate, string Todate)
         {

             string res = "";
             string s2 = "";
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 string str_query = "SELECT W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno FROM WeeklyClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1  ";

                 if (FromDate != string.Empty)
                 {
                     str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
                 }
                 if (Todate != string.Empty)
                 {
                     str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
                 }
                 DataTable DT = ObjData.RunSelectQueryTTrans(str_query, tr);

                 int strtransactionid = 0;
                 s2 = "select isnull(max(transactionid),0) from  TransactionDetail";
                 DataTable dttran = new DataTable();
                 dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                 strtransactionid = Convert.ToInt32(dttran.Rows[0][0].ToString());



                 foreach (DataRow Dr in DT.Rows)
                 {

                     string Amount = Dr["paybleamount"].ToString();
                     strtransactionid = strtransactionid + 1;

                     s2 = " SElect from isnull(Utilitybalance,0) UserDetail where userid='" + Dr["UserID"].ToString() + "'";                   
                     DataTable Dtbalance = ObjData.RunSelectQueryTTrans(s2, tr);

                     Decimal Oldbalance = Convert.ToDecimal(Dtbalance.Rows[0][0].ToString());
                     Decimal Newbalance = Oldbalance + Convert.ToDecimal(Amount);


                     s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,oldBalance, currentBalance,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + Amount + ",0,'" + Dr["UserID"].ToString() + "'," + Oldbalance + "," + Newbalance + "'Activation Incentive','" + "Transfer Activation Incentive" + "','" + "admin" + "',getdate(),'2')";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                     s2 = "update userdetail set balanceamount=isnull(Utilitybalance,0)+" + Amount + "  where userid='" + Dr["UserID"].ToString() + "'  ";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                     s2 = "update WeeklyClosing set status='1',transactionid='" + strtransactionid + "',paymentdate=getdate() where Userid='" + Dr["UserID"].ToString() + "' and Cast(Fromdate as date)='" + FromDate + "'";

                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                 }

                 s2 = "update WeeklyClosingMaster set status='1' where  Cast(Fromdate as date)='" + FromDate + "'";
                 ObjData.RunInsUpDelQueryTrans(s2, tr);

                 tr.Commit();
                 res = "1";

             }
             catch (Exception ex)
             {
                 res = "0";
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return res;

         }
         public string UpdateTransferSales(string FromDate, string Todate)
         {

             string res = "";
             string s2 = "";
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {

                 string str_query = "SELECT W.id,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,W.RechargeAmount,W.DTHConnection,W.RechargeBV,W.DTHBV FROM SalesClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

                 if (FromDate != string.Empty)
                 {
                     str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
                 }
                 if (Todate != string.Empty)
                 {
                     str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
                 }
                 DataTable DT = ObjData.RunSelectQueryTTrans(str_query, tr);

                 int strtransactionid = 0;
                 s2 = "select isnull(max(transactionid),0) from  TransactionDetail";
                 DataTable dttran = new DataTable();
                 dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                 strtransactionid = Convert.ToInt32(dttran.Rows[0][0].ToString());



                 foreach (DataRow Dr in DT.Rows)
                 {

                     string Amount = Dr["paybleamount"].ToString();
                     strtransactionid = strtransactionid + 1;

                     s2 = " SElect from isnull(Utilitybalance,0) UserDetail where userid='" + Dr["UserID"].ToString() + "'";
                     DataTable Dtbalance = ObjData.RunSelectQueryTTrans(s2, tr);

                     Decimal Oldbalance = Convert.ToDecimal(Dtbalance.Rows[0][0].ToString());
                     Decimal Newbalance = Oldbalance + Convert.ToDecimal(Amount);


                     s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,oldBalance, currentBalance,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + Amount + ",0,'" + Dr["UserID"].ToString() + "'," + Oldbalance + "," + Newbalance + "'Sales Incentive','" + "Transfer Sales Incentive" + "','" + "admin" + "',getdate(),'2')";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                     s2 = "update userdetail set balanceamount=isnull(Utilitybalance,0)+" + Amount + "  where userid='" + Dr["UserID"].ToString() + "'  ";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                     s2 = "update WeeklyClosing set status='1',transactionid='" + strtransactionid + "',paymentdate=getdate() where Userid='" + Dr["UserID"].ToString() + "' and Cast(Fromdate as date)='" + FromDate + "'";

                     ObjData.RunInsUpDelQueryTrans(s2, tr);

                 }

                 s2 = "update WeeklyClosingMaster set status='1' where  Cast(Fromdate as date)='" + FromDate + "'";
                 ObjData.RunInsUpDelQueryTrans(s2, tr);

                 tr.Commit();
                 res = "1";

             }
             catch (Exception ex)
             {
                 res = "0";
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return res;

         }


         public DataTable getClosingDate(string Type)
         {
             string table = "";
             switch (Type)
             {
                 case "SELFPURCHASE": table = "WeeklyClosingMaster"; break;
                 case "REFERALBONUS": table = "RefrealClosingMaster"; break;
                 case "REPURCHASE": table = "RepurchaseClosingMaster"; break;
                 case "AWARD": table = "DailyClosingMaster"; break;
                 case "REFERALREPURCHASE": table = "ReferealRepurchaseClosingMaster"; break;
             }
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from " + table;

             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     if (Type == "AWARD")
                     {
                         foreach (DataRow Dr in ds.Rows)
                         {
                             Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy");
                         }
                     }
                     else
                     {
                         foreach (DataRow Dr in ds.Rows)
                         {
                             Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }

         public DateTime GetClosingDate(string Type)
         {

             int h = 0;
             DateTime res = DateTime.Parse("03/01/2018");
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             try
             {
                 string table = "";
                 switch (Type)
                 {
                     case "SELFPURCHASE": table = "WeeklyClosingMaster"; break;
                     case "REFERALBONUS": table = "RefrealClosingMaster"; break;
                     case "REPURCHASE": table = "RepurchaseClosingMaster"; break;
                     case "REFERALREPURCHASE": table = "ReferealRepurchaseClosingMaster"; break;
                 }

                 ObjData.StartConnection();
                 string qry = " SELECT count(*) FROM " + table;
                 DataTable Dt = ObjData.RunDataTable(qry);
                 if (Dt != null && Dt.Rows.Count > 0)
                 {

                     if (Convert.ToInt16(Dt.Rows[0][0].ToString()) == 0)
                     {
                         res = DateTime.Parse("03/01/2018");
                     }
                     else
                     {
                         qry = " SELECT max(todate) FROM " + table;
                         DataTable dttime = ObjData.RunDataTable(qry);
                         res = DateTime.Parse(dttime.Rows[0][0].ToString()).AddDays(1);
                     }

                 }
                 else
                 {
                     res = DateTime.Parse("03/01/2018");
                 }

             }
             catch (Exception ex)
             {
                 res = DateTime.Parse("03/01/2018");
             }
             finally
             {
                 ObjData.EndConnection();
             }
             return res;
         }
         public int CalculateClosingNew(string todate, string Type)
         {

             int h = 0;
             string res = "";
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {
                 string proc = "";
                 switch (Type)
                 {
                     case "SELFPURCHASE": proc = "Closing_SelfPurchaseBonus"; break;
                     case "REFERALBONUS": proc = "Closing_RefrealBonus"; break;
                     case "REPURCHASE": proc = "Closing_RepurchaseBonus"; break;
                     case "REFERALREPURCHASE": proc = "Closing_ReferalRepurchaseBonus"; break;
                 }

                 res = proc;
                 SqlParameter[] sqm = new SqlParameter[]
                 {
                 new SqlParameter("@Todate", todate),
                 };
                 ObjData.RunInsUpDelQueryTransProc(res, tr, sqm);
                 h = 1;
                 tr.Commit();
             }
             catch (Exception ex)
             {
                 h = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return h;
         }
         public DataTable getClosingReport(string FromDate, string Todate, string UserId, string Type)
         {
             {
                 string table = "";
                 switch (Type)
                 {
                     case "SELFPURCHASE": table = "WeeklyClosing"; break;
                     case "REFERALBONUS": table = "RefrealClosing"; break;
                     case "REPURCHASE": table = "RepurchaseClosing"; break;
                 }

                 string str_query = "";
                 if (Type == "REFERALREPURCHASE")
                 {
                     str_query = " SELECT w.ReferalIncome,w.RepurchaseIncome,(w.ReferalIncome+w.RepurchaseIncome) as TotalIncentive,w.cappingamount,w.DifferenceAmount,W.id,w.Commission,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,U.Mobile FROM ReferealRepurchaseClosing W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1 ";
                 }
                 else
                 {
                     str_query = " SELECT W.id,w.Commission,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,U.Mobile FROM " + table + " W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1 ";
                 }

                 if (FromDate != string.Empty)
                 {
                     str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
                 }
                 if (Todate != string.Empty)
                 {
                     str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
                 }
                 if (UserId != string.Empty)
                 {
                     str_query += " and W.userid='" + UserId + "'";
                 }
                 str_query += " order by W.Weekno";
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
         public DataTable getAwardClosingReport(string dte, string UserId)
         {


             string str_query = " SELECT a.*,u.UserName,iif(a.status is null or a.status=0,'Due','Paid') as PStatus FROM AwardAcheiver a JOIN userdetail u ON a.userid=u.UserId WHERE 1=1 ";

             if (dte != string.Empty)
             {
                 str_query += " and Cast(a.entrydate as date)='" + dte + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and a.userid='" + UserId + "'";
             }
             str_query += " order by a.id";
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

         public DataTable getAwardList(string UserId)
         {
             string str_query = "GetUserAwardList";
             SqlParameter[] param = new SqlParameter[]
             {
                 new SqlParameter("@userid",UserId)
             };


             DataTable dt = null;
             ObjData.StartConnection();
             try
             {
                 dt = ObjData.RunDataTableProcedure(str_query, param);
             }
             catch (Exception ex)
             {
                 dt = null;
             }
             ObjData.EndConnection();
             return dt;
         }

         public DataTable getAllAwardWinners()
         {
             string str_query = "GetAllAwardWinners";
             SqlParameter[] param = new SqlParameter[]
             {
             };
             DataTable dt = null;
             ObjData.StartConnection();
             try
             {
                 dt = ObjData.RunDataTableProcedure(str_query, param);
             }
             catch (Exception ex)
             {
                 dt = null;
             }
             ObjData.EndConnection();
             return dt;
         }
         public DataTable getTransferReport(string FromDate, string Todate, string UserId, string Type)
         {
             {
                 string table = "";
                 switch (Type)
                 {
                     case "SELFPURCHASE": table = "WeeklyClosing"; break;
                     case "REFERALBONUS": table = "RefrealClosing"; break;
                     case "REPURCHASE": table = "RepurchaseClosing"; break;
                 }

                 string str_query = "";
                 if (Type == "REFERALREPURCHASE")
                 {
                     str_query = " SELECT w.ReferalIncome,w.RepurchaseIncome,(w.ReferalIncome+w.RepurchaseIncome) as TotalIncentive,w.cappingamount,w.DifferenceAmount,W.id,w.Commission,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,U.Mobile FROM ReferealRepurchaseClosing W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1 and W.Status=0 ";
                 }
                 else
                 {
                     str_query = " SELECT W.id,w.Commission,Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.MatchingBV,W.adminper,W.admincharge,W.Status,CASE WHEN W.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status1,U.UserName,W.TransactionID,Convert(CHAR,GenerateDate,103) AS GenerateDate,Convert(CHAR,PaymentDate,103) AS PaymentDate,tds,tdsper,paybleamount,W.Weekno,U.Mobile FROM " + table + " W with(nolock) INNER JOIN Userdetail U with(nolock) ON W.UserID=U.UserId where 1=1 and W.Status=0  ";
                 }

                 if (FromDate != string.Empty)
                 {
                     str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
                 }
                 if (Todate != string.Empty)
                 {
                     str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
                 }
                 if (UserId != string.Empty)
                 {
                     str_query += " and W.userid='" + UserId + "'";
                 }
                 str_query += " order by W.Weekno";
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


         public int TransferPayoutReferal(ArrayList arrId, ArrayList arrUser, ArrayList arrAmount, ArrayList arrMobile)
         {
             int c = 0;
             int arrcount = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {
                 string str = "Proc_TransferPayout_ReferalRepurchase";

                 for (int i = 0; i < arrId.Count; i++)
                 {

                     SqlParameter[] sqm = new SqlParameter[]
                 {
                 new SqlParameter("@Amount", arrAmount[i].ToString()),
                 new SqlParameter("@UserId", arrUser[i].ToString()),
                 new SqlParameter("@id", arrId[i].ToString())
                 };
                     DataTable dt = ObjData.RunDataTableProcedureTRans(str, tr, sqm);

                     if (dt.Rows[0][0].ToString() == "-1")
                     {
                         c = 0;
                         tr.Rollback();
                         break;
                     }
                     string smsBody = "Congratulation ! You have achieved referal-repurchase bonus Rs. " + arrAmount[i].ToString() + " successfully from dgtalshop in your wallet";
                     string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + arrMobile[i].ToString() + "&sender=ETOPUP&smstext=" + smsBody + "&smsformat=TEXT&format=json";
                     string Result = url.CallURL();
                     string s2 = "insert into SendSms(CreateDate,Mobile,Result,Message)  values (getdate(),'" + arrMobile[i].ToString() + "','" + Result + "','" + smsBody + "') ";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);
                     arrcount = arrcount + 1;
                 }

                 if (arrcount == arrId.Count)
                 {
                     c = 1;
                     tr.Commit();
                 }
                 else
                 {
                     c = 0;
                     tr.Rollback();
                 }

             }
             catch (Exception ex)
             {
                 c = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return c;
         }

         public int TransferPayoutSelfPurchase(ArrayList arrId, ArrayList arrUser, ArrayList arrAmount, ArrayList arrmobile)
         {
             int c = 0;
             int arrcount = 0;
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);
             try
             {
                 string str = "Proc_TransferPayout_SelfPurchase";

                 for (int i = 0; i < arrId.Count; i++)
                 {

                     SqlParameter[] sqm = new SqlParameter[]
                 {
                 new SqlParameter("@Amount", arrAmount[i].ToString()),
                 new SqlParameter("@UserId", arrUser[i].ToString()),
                 new SqlParameter("@id", arrId[i].ToString())
                 };
                     DataTable dt = ObjData.RunDataTableProcedureTRans(str, tr, sqm);

                     if (dt.Rows[0][0].ToString() == "-1")
                     {
                         c = 0;
                         tr.Rollback();
                         break;
                     }

                     string smsBody = "Congratulation ! You have achieved self purchase bonus Rs. " + arrAmount[i].ToString() + " successfully from dgtalshop in your wallet";
                     string url = "http://www.apihub.online/api/Services/transact?token=99a9eea2b24341ad760c2ac9e4c45031&skey=SST&to=" + arrmobile[i].ToString() + "&sender=ETOPUP&smstext=" + smsBody + "&smsformat=TEXT&format=json";
                     string Result = url.CallURL();
                     string s2 = "insert into SendSms(CreateDate,Mobile,Result,Message)  values (getdate(),'" + arrmobile[i].ToString() + "','" + Result + "','" + smsBody + "') ";
                     ObjData.RunInsUpDelQueryTrans(s2, tr);
                     arrcount = arrcount + 1;
                 }

                 if (arrcount == arrId.Count)
                 {
                     c = 1;
                     tr.Commit();
                 }
                 else
                 {
                     c = 0;
                     tr.Rollback();
                 }

             }
             catch (Exception ex)
             {
                 c = 0;
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return c;
         }

         public DataSet GetSettlementReport(DateTime fdate, DateTime tdate)
         {
             ObjData.StartConnection();
             DataSet Ds = new DataSet();
             string Query = "Proc_AdminSettlementreport";
             SqlParameter[] sqm = new SqlParameter[]{
             new SqlParameter("@fdate",fdate),
             new SqlParameter("@tdate",tdate)
             };
             Ds = ObjData.RunDataSetProcedure(Query, sqm);
             ObjData.EndConnection();
             return Ds;
         }

         public DataTable getdirectorClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,w.GtotalLeft, W.GtotalRight, W.GtotalSelf,   w.Binaryincome,W.Pair,W.TotalLeft,W.TotalRight,W.LeftcarryPv,W.RightCarryPv,W.dailyId,U.UserName,W.dailyId,W.adminper,w.admincharge,W.tdsper,W.tdscharge,W.paybleamount,W.status,w.Rank, W.cto AS Totalbv FROM directorIncome W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.UserId='" + UserId + "'";
             }


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

         public DataTable getGoldClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,w.GtotalLeft, W.GtotalRight, W.GtotalSelf,   w.Binaryincome,W.Pair,W.TotalLeft,W.TotalRight,W.LeftcarryPv,W.RightCarryPv,W.dailyId,U.UserName,W.dailyId,W.adminper,w.admincharge,W.tdsper,W.tdscharge,W.paybleamount,W.status,w.Rank, W.cto AS Totalbv FROM golddirectorIncome W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.UserId='" + UserId + "'";
             }


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

         public DataTable getleaderClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,w.GtotalLeft, W.GtotalRight, W.GtotalSelf,   w.Binaryincome,W.Pair,W.TotalLeft,W.TotalRight,W.LeftcarryPv,W.RightCarryPv,W.dailyId,U.UserName,W.dailyId,W.adminper,w.admincharge,W.tdsper,W.tdscharge,W.paybleamount,W.status,w.Rank, W.cto AS Totalbv FROM leadershipIncome W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.UserId='" + UserId + "'";
             }


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
         public DataTable getdailyClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,w.Binaryincome,W.Pair,W.TotalLeft,W.TotalRight,W.LeftcarryPv,W.RightCarryPv,W.dailyId,U.UserName,W.dailyId,W.adminper,w.admincharge,W.tdsper,W.tdscharge,W.paybleamount,W.status FROM DailyClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.UserId='" + UserId + "'";
             }


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
         public DataTable getdailyClosingDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from dailyClosingMaster";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getUserRewardReport(clsClosing objCl)
         {
             //string str_query = "SELECT A.LeftId,AU.point,'0' AS balance,A.AwardName,'Yes' AS achieved,Convert(CHAR,AU.achievedate,103) AS Achievedate FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "' UNION ALL  SELECT TOP 1 A.LeftId,(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS point,A.LeftId-(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS balance,Awardname,'No' AS achieved,'Not Achieved' AS Achievedate FROM  AwardAchiver A  WHERE A.Id NOT IN (SELECT A.Id FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "')";

             string str_query = "SELECT A.LeftId,AU.point,0 AS balance,A.AwardName,'Yes' AS achieved,AU.adminper,AU.admincharge,AU.tds,AU.tdscharge,AU.paybleamount,AU.status ,Convert(CHAR,AU.achievedate,103) AS Achievedate FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "' UNION ALL SELECT TOP 1 A.LeftId,(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS point, A.LeftId-(SELECT isnull(sum(pair),0) FROM DailyClosing WHERE UserID='" + objCl.GenerateUserId + "') AS balance, Awardname,'No' AS achieved,0 AS adminper,0 AS admincharge,0 AS tds,0 AS tdscharge,0 AS paybleamount,'' AS status,'Not Achieved' AS Achievedate FROM  AwardAchiver A  WHERE A.Id NOT IN (SELECT A.Id FROM AwardAchiverUser AU  JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN  Userdetail U ON AU.UserId=U.UserId WHERE AU.UserId='" + objCl.GenerateUserId + "')";


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
         public DataTable getroyalityClosingReport(string FromDate, string Todate, string UserId)
         {
             string str_query = "SELECT Convert(CHAR,W.Fromdate,103) AS Fromdate,Convert(CHAR,W.ToDate,103) AS Todate,W.UserID,W.income,W.Pair,W.TotalLeft,W.TotalRight,W.LeftcarryPv,W.RightCarryPv,W.dailyId,U.UserName,W.dailyId FROM RoyalityClosing W INNER JOIN Userdetail U ON W.UserID=U.UserId where 1=1 ";

             if (FromDate != string.Empty)
             {
                 str_query += " and Cast(W.Fromdate as date)='" + FromDate + "'";
             }
             if (Todate != string.Empty)
             {
                 str_query += " and Cast(W.ToDate as date)='" + Todate + "'";
             }
             if (UserId != string.Empty)
             {
                 str_query += " and W.UserId='" + UserId + "'";
             }


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
         public DataTable getRoyaltyClosingDate()
         {
             string str_query = "select Distinct FromDate,Todate,'' AS ClosingDate from RoyalityClosing";


             DataTable ds = null;
             ObjData.StartConnection();
             try
             {

                 ds = ObjData.RunDataTable(str_query);
                 if (ds.Rows.Count > 0)
                 {
                     foreach (DataRow Dr in ds.Rows)
                     {
                         Dr["ClosingDate"] = Convert.ToDateTime(Dr["FromDate"].ToString()).ToString("dd/MMM/yyyy") + "=" + Convert.ToDateTime(Dr["Todate"].ToString()).ToString("dd/MMM/yyyy");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ds = null;
             }
             ObjData.EndConnection();
             return ds;
         }
         public DataTable getactivationamount(string dailyid,string UserId)
         {
             string str_query = "SELECT *,case when status=0 then 'DUE' else 'PAID' end as Status1 FROM ActivationAmount  where 1=1 ";

             if (dailyid != string.Empty)
             {
                 str_query += " and PlanID='" + dailyid + "'";
             }

             if (UserId != string.Empty)
             {
                 str_query += " and UserId='" + UserId + "'";
             }


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
         public DataTable getUserRewardReportadmin(string Userid)
         {          

             string str_query = "SELECT U.UserId,U.UserName,A.AwardName,AU.achievedate,AU.Point,AU.Status,AU.adminper,AU.admincharge,AU.TDS,Au.TDScharge,Au.paybleamount,A.leftid FROM AwardAchiverUser AU JOIN AwardAchiver A ON AU.AwardId=A.Id JOIN UserDetail U ON U.UserId=AU.UserId where 1=1 ";

             if (Userid != string.Empty)
             {
                 str_query += " and AU.UserId='"+Userid+"'";
             }

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
         public DataTable getUsersForCoreCommitteeClosing()
         {
             string str_query = "select * from dbo.sp_ChkCoreCommitteeIncome()";

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

         public string Create_CoreCommitteeClosing(clsClosing objaccount)
         {
             string res = "";
             string s2 = "";
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);

             try
             {
                 s2 = "sp_CreateCoreCommitteeClosing";

                 SqlParameter[] parameter = {                                              
                new SqlParameter("@Amount",objaccount.Amount),
                new SqlParameter("@Percentage",objaccount.Percentage),   
                };
                 res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                 tr.Commit();
             }
             catch (Exception ex)
             {
                 res = "0";
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return res;
         }

         public DataTable getCoreCommitteeClosingReport(clsClosing objaccount)
         {
             string str_query = "select c.userid, ud.username, c.leftuser, c.rightuser, c.amount, case when c.status=1 then 'PAID' else 'DUE' end as status, c.tdsPer, c.tdsCharge, c.adminPer, c.adminCharge, c.payableAmount from tblCoreCommittee c inner join userdetail ud on ud.userid=c.userid where 1=1 ";

             if (objaccount.paystatus != "all")
             {
                 str_query += " and c.status=" + objaccount.paystatus;
             }
             if (objaccount.GenerateUserId != null && objaccount.GenerateUserId != "")
             {
                 str_query += " and c.userid='" + objaccount.GenerateUserId + "' ";
             }
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

         public string InsertCoreCommitteePayout(clsClosing objaccount)
         {
             string res = "";
             string s2 = "";
             SqlConnection cn;
             SqlTransaction tr = null;
             DataSet ds = new DataSet();
             cn = ObjData.StartConnectionInTransaction();
             tr = cn.BeginTransaction(IsolationLevel.Serializable);

             try
             {
                 s2 = "sp_CreateCoreCommitteePayout";

                 SqlParameter[] parameter = {                                              
                new SqlParameter("@userid",objaccount.GenerateUserId),  
                };
                 res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                 tr.Commit();
             }
             catch (Exception ex)
             {
                 res = "0";
                 tr.Rollback();
             }
             finally
             {
                 ObjData.EndConnection();
                 tr.Dispose();
             }
             return res;
         }

    }
}

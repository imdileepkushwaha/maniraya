using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using ARA_StringHunt;

namespace BusinessLogicTier
{
    public class clsAccount
    {
        Data ObjData = new Data();
        clsSetting objsetting = new clsSetting();
        clsUser objUser = new clsUser();
        clsAPI objapisms = new clsAPI();
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserMObile { get; set; }
        public string WithdrawlRequestId { get; set; }
        public string img { get; set; }
        public string WithdrawlAccountType { get; set; }
        public decimal WithdrawlAmount { get; set; }
        public string WithdrawlRequestStatus { get; set; }
        public string TransactionId { get; set; }
        public string MentionBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ROIDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public DataTable DtLinks { get; set; }
        public string CommitmentRequestId { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public decimal CommitmentAmount { get; set; }
        public string CommitmentUserId { get; set; }
        public string AdminCharge { get; set; }
        public string TDSCharge { get; set; }
        public string NetAmount { get; set; }
        public decimal CurrencyInRs { get; set; }
        public string CurrencyName { get; set; }
        public string TransferUserId { get; set; }
        public decimal ComissionPercent { get; set; }
        public string LevelNo { get; set; }
        public string PaymentMode { get; set; }
        public string OnlineTransactionId { get; set; }
        public string txtifsccode { get; set; }
        public string Id { get; set; }
        public string BankAccountId { get; set; }
        public string plananame { get; set; }
        public string DepositrequestTo { get; set; }
        public string DepositrequestRequestType { get; set; }
        public string Depositrequestfranchiseeuserid { get; set; }

        public string userType { get; set; }
        public string NoOfRow { get; set; }

        public DataTable getCurrencyAll()
        {
            string str_query = "select * from currencymaster ";


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




        public DataTable getLevelCategoryWise(string Id)
        {
            string str_query = "select * from CategoryLevelMaster where CategoryId='" + Id + "'";


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
        public string UpdateLevelMasterCategoryWise(clsAccount objAccount, string CategoryId)
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
                s2 = "update CategoryLevelMaster set CommissionPercent='" + objAccount.ComissionPercent + "' where [level]='" + objAccount.LevelNo + "' and CategoryId='" + CategoryId + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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

        public DataTable getRankStatus(clsAccount objaccount)
        {
            string str_query = "SELECT st1.*,st2.awardname FROM AwardAchiverUser st1  LEFT JOIN AwardAchiver st2 ON st1.AwardId=st2.id WHERE 1=1";


            //if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            //{
            //    str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            //}


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            //str_query += " order by mentiondate  desc";



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

        public DataTable getCompanyAccountDetailById(clsAccount objaccount)
        {
            string str_query = "select *,AccountNo+'('+bankname+')' as accno2 from  CompanyAccountDetail where id='" + objaccount.Id + "' ";


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
        public DataTable getCompanyAccountDetail()
        {
            string str_query = "select *,AccountNo+'('+bankname+')' as accno2 from CompanyAccountDetail ";


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
        public DataTable getLevel()
        {
            string str_query = "select * from LevelMaster ";


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
        public DataTable getselfpurchaseLevel()
        {
            string str_query = "select * from SelfPurchaseBonusMaster ";


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
        public DataTable getsalesLevel()
        {
            string str_query = "select * from SalesLevelMaster ";


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
        public DataSet getClosingDate()
        {
            string str_query = "SELECT dateadd(dd, 1,max(closingday ) )from ClosingDate; select min(mentiondate) from CommitmentDetail    ";


            DataSet ds = null;
            ObjData.StartConnection();
            try
            {
                ds = ObjData.RunSelectQuery(str_query);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }

        public DataSet getCommitmentDate()
        {
            string str_query = "SELECT dateadd(dd, 1,max(CommitmentDate) )from CommitmentDateDetail; SELECT min(growthdate) FROM commitmentdetail WHERE GrowthStatus=1   ";


            DataSet ds = null;
            ObjData.StartConnection();
            try
            {
                ds = ObjData.RunSelectQuery(str_query);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            ObjData.EndConnection();
            return ds;
        }
        public DataTable getGrowthComission()
        {
            string str_query = "SELECT TOP 1 growthpercent FROM SystemSetting  ORDER BY id desc  ";


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

        public DataTable getCommissionStatement(clsAccount objaccount)
        {
            string str_query = "SELECT cl.*,cd.Userid as CommitmentUserid, rm.RankName FROM closingledger cl LEFT JOIN commitmentdetail cd ON cl.CommitmentId=cd.id LEFT JOIN rankmaster rm ON cl.RankId=rm.id   where cl.start_date='" + objaccount.FromDate + "' and  cl.end_date='" + objaccount.ToDate + "' ";

            if (objaccount.UserId != "")
            {
                str_query += "  and cl.userid='" + objaccount.UserId + "'  ";
            }

            str_query += "  order by  cl.userid ";

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

        public DataTable getAccountBalance(clsAccount objaccount)
        {
            string str_query = "SELECT isnull(Sum(cramount),0.00)- isnull(sum(dramount),0.00) FROM TransactionDetail  where userid='" + objaccount.UserId + "'    ";


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

        public DataTable getAccountBalanceForGetHelp(clsAccount objaccount)
        {
            string str_query = "SELECT isnull( isnull(Sum(td.cramount),0.00)- isnull(sum(td.dramount),0.00)-( SELECT isnull( sum(wr.Amount),0) FROM  WithdrawlRequest wr WHERE wr.UserId=td.UserID AND wr.Status='Pending'),0) FROM TransactionDetail td   where td.userid='" + objaccount.UserId + "' GROUP BY td.UserID    ";

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

        public DataTable getUserWalletBalance(clsAccount objaccount)
        {
            string str_query = "select isnull( sum(cramount),0)-isnull( sum(dramount),0) from transactiondetail td where 1=1 ";

            if (objaccount.UserId != "")
            {
                str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
            }

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
        public DataTable getUserWallet(clsAccount objaccount)
        {
            string str_query = "select td.*,ud.username from transactiondetail td left join userdetail ud on td.userid=ud.userid where 1=1 ";

            if (objaccount.UserId != "")
            {
                str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by td.mentiondate  desc";



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



        public DataTable getGroupIncome(clsAccount objaccount)
        {
            string str_query = "select * from transactiondetail where   transactiontype = 'Group Income' ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by mentiondate  desc";



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
        public DataTable getDirectIncome(clsAccount objaccount)
        {
            string str_query = "SELECT Convert(CHAR,Fromdate,103) AS Fromdate,Convert(CHAR,ToDate,103) AS Todate, userid, useself AS SelfBV, Income FROM SelfPurchaseIncome where 1=1";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by paymentdate  desc";



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



        public DataTable getAdminTds(clsAccount objaccount)
        {
            string str_query = "SELECT admincharge, tdswithoutpan FROM tbl_Deduction  ";

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


        public DataTable GetGiftbalanceReport(clsAccount objUser)
        {
            string str_query = "SELECT  ud.UserId,ud.UserName,P.PlanName,Convert(CHAR,a.Instdate,103) AS Instdate,A.TDS,A.TDScharge,A.adminper,A.admincharge,A.paybleamount,CASE WHEN a.Status=0 THEN '' ELSE  Convert(CHAR,a.Activationdate,103) END  AS Activationdate,CASE WHEN a.Status=0 THEN 'UNPAID' ELSE 'PAID' END AS Status1,a.Instno,a.amount,'Joining' as Type FROM UserDetail ud JOIN ActivationAmount A ON ud.UserId=A.Userid JOIN planmaster P ON  ud.SlabID=P.Id WHERE 1=1 order by a.instno";
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

        public DataTable getAutoPoolIncome(clsAccount objaccount)
        {
            string str_query = "select * from transactiondetail where   transactiontype = 'AutoPool Income' ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by mentiondate  desc";



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
        public DataTable getLevelRoiIncome(clsAccount objaccount)
        {
            string str_query = "SELECT I.Userid,U.UserName,I.Fromuserid,I.FromuserCommission,I.IncomePer,I.levelNo,I.income,I.AdminPer,I.Admincharge,I.Paybleamount,Convert(VARCHAR(50),I.Entrydate,103) Entrydate,CASE when I.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status FROM ROILevelIncomeTB I JOIN UserDetail U ON I.Userid=U.UserId where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and cast(I.Entrydate as date)  >= cast('" + objaccount.FromDate + "' as date)   and cast(I.Entrydate as date)   <= cast('" + objaccount.ToDate + "' as date) ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and I.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by Entrydate  desc";



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



        public string Insert_PinRequest(clsAccount objaccount)
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
                s2 = "sp_addPinRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                         new SqlParameter("@NoOfEpin",objaccount.NoOfEpin),
                     new SqlParameter("@EpinAmount",objaccount.Epinamount),
                        new SqlParameter("@Planname",objaccount.plananame),

                          new SqlParameter("@Planid", objaccount.planid),
                       
                     
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




        public DataTable Approve_PinRequest(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_approve_PinRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@GenerateUserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@ID",objaccount.WithdrawlRequestId),
                  new SqlParameter("@NoOfEpin",objaccount.NoOfEpin),
                     new SqlParameter("@Planid",objaccount.planid),
              
              
                     
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                if (Dt != null && Dt.Rows[0][0].ToString() == "1")
                {
                    tr.Commit();
                    //s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                    //DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    //if (Dtmsms.Rows.Count > 0)
                    //{
                    //    string MobileNo = "";
                    //    string BalanceAmount = "";
                    //    MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                    //    if (objaccount.DepositrequestRequestType == "R")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                    //    }
                    //    if (objaccount.DepositrequestRequestType == "U")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                    //    }
                    //    string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                    //    string Result = url.CallURL();
                    //    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    //}
                }


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
            return Dt;
        }

        public DataTable getPinRequest(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo,ud.mobile  from DepositRequest wr inner JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }
            // str_query += " and wr.requestto = '" + objaccount.DepositrequestTo + "'";

            str_query += " order by wr.mentiondate  desc";



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

        public DataTable getBonusIncome(clsAccount objaccount)
        {
            string str_query = "SELECT a.AwardName, CASE WHEN (a.Award='NULL') THEN 'No Award' ELSE a.Award END AS Award, a.Amount, a.EDUCATIONINCOME, a.CHILDEDUCATIONINCOME,au.UserId,au.TDS,au.TDScharge,au.admincharge,au.paybleamount, au.LeftId,au.RightID, au.achievedate, ud.UserName   FROM AwardAchiver a RIGHT  JOIN AwardAchiverUser au ON au.AwardId=a.Id INNER JOIN UserDetail ud ON ud.UserId=au.UserId where 1=1";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and cast(au.achievedate as date)  >= cast('" + objaccount.FromDate + "' as date)   and cast(au.achievedate as date)   <= cast('" + objaccount.ToDate + "' as date) ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and au.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by au.achievedate  desc";



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




        public DataTable getLevelIncome(clsAccount objaccount)
        {
            string str_query = "SELECT I.Userid,U.UserName,I.Fromuserid,I.FromuserCommission,I.IncomePer,I.levelNo,I.income,I.AdminPer,I.Admincharge,I.Paybleamount,Convert(VARCHAR(50),I.Pdate,103) ExpectedDate,CASE when I.Status=0 THEN 'DUE' ELSE 'PAID' END AS Status FROM IncomeTB I JOIN UserDetail U ON I.Userid=U.UserId where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and cast(I.Entrydate as date)  >= cast('" + objaccount.FromDate + "' as date)   and cast(I.Entrydate as date)   <= cast('" + objaccount.ToDate + "' as date) ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and I.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by Pdate  desc";



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

        public DataTable getBinaryIncome(clsAccount objaccount)
        {
            string str_query = "select * from TransactionDetail WHERE TransactionType='Binary Income' ";


            if (objaccount.UserId != "")
            {
                str_query += "  AND UserId = '" + objaccount.UserId + "' ";
            }

            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }





            str_query += " order by mentiondate  desc";



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
        public DataTable getPurchaseIncome(clsAccount objaccount)
        {
            string str_query = "select * from transactiondetail where   transactiontype = 'Purchase' and cramount>0 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by mentiondate  desc";



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
        public DataTable getTransactionReport(clsAccount objaccount)
        {
            string str_query = "select " + objaccount.NoOfRow + " * from transactiondetail where   1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and cast(mentiondate as date)  >= '" + objaccount.FromDate + "'   and cast(mentiondate as date)   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by mentiondate  desc";



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
        public DataTable getTransactionReportnew(clsAccount objaccount)
        {
            string str_query = "select " + objaccount.NoOfRow + " * from transactiondetail where   1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by mentiondate  desc";



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
        public DataTable getGroupMachingReport(clsAccount objaccount)
        {
            string str_query = "select * from UserBuisnessVolume   where   1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and Cast(CreateDate as date)  >= Convert(datetime," + objaccount.FromDate.ToString("dd/MMM/yyyy") + ",101)   and Cast(CreateDate as date)   <= Convert(datetime," + objaccount.ToDate.ToString("dd/MMM/yyyy") + ",101) ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " and PurchaseType='J' order by ID  desc";



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
        public DataTable getrepurchaseMachingReport(clsAccount objaccount)
        {
            string str_query = "select * from UserBuisnessVolume   where   1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and Cast(CreateDate as date)  >= Convert(datetime," + objaccount.FromDate.ToString("dd/MMM/yyyy") + ",101)   and Cast(CreateDate as date)   <= Convert(datetime," + objaccount.ToDate.ToString("dd/MMM/yyyy") + ",101) ";
            }


            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " and PurchaseType='R' order by ID  desc";



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
        public DataTable getGrowthIncome(clsAccount objaccount)
        {
            string str_query = "select * from transactiondetail where   transactiontype = 'Growth Income' ";
            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and mentiondate  >= '" + objaccount.FromDate + "'   and mentiondate   <= '" + objaccount.ToDate + "' ";
            }
            if (objaccount.UserId != "")
            {
                str_query += "  and UserId = '" + objaccount.UserId + "' ";
            }
            str_query += " order by mentiondate  desc";
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

        public DataTable getWithdrawlRequest(clsAccount objaccount)
        {
            string s1 = "select isnull(CashWalletPercent,0) as CashWalletPercent from tbl_Deduction";
            ObjData.StartConnection();
            DataTable dt1 = ObjData.RunDataTable(s1);
            ObjData.EndConnection();
            decimal deductionPercent = Convert.ToDecimal(dt1.Rows[0]["CashWalletPercent"].ToString());

            string str_query = "select wr.*,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,ud.mobile, bm.BankName, ud.AccountNo, ud.IFSCCode, ud.phonepay, ud.bhimno, ud.upino, cast((select isnull((wr.Amount*" + deductionPercent + ")/100, 0)) as decimal(18,2))  as shoppingWallet, cast((select isnull((wr.Amount*(100-" + deductionPercent + ")/100), 0))  as decimal(18,2)) as MainWallet from withdrawlrequest wr LEFT JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId Left Join BankMaster bm on ud.BankName=bm.BankId where 1=1  ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getPrimeMmeberRequest(clsAccount objaccount)
        {
            string str_query = "select wr.*,ud.UserName,ud.mobile from CashRequest wr LEFT JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }
            if (objaccount.DepositrequestRequestType != "")
            {
                str_query += "  and wr.type = '" + objaccount.DepositrequestRequestType + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getWithdrawlRequestfranchisee(clsAccount objaccount)
        {
            string str_query = "select wr.*,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,ud.mobile from withdrawlrequest wr inner JOIN FranchiseeDetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getDepositRequest(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Main Wallet' when requesttype='U' then 'Cash wallet' else 'Wallet' end as RequestType1,RequestTo  from DepositRequest wr inner JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getfranchiseeDepositRequest(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo  from DepositRequest wr inner JOIN FranchiseeDetail ud ON wr.UserId=ud.UserId LEFT JOIN FranchiseeDetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getTopupRequestnew(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo,ud.mobile  from TopupRequest wr inner JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }
            str_query += " and wr.requestto = '" + objaccount.DepositrequestTo + "'";

            str_query += " order by wr.mentiondate  desc";



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

        public DataTable getDepositRequestnew(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo,ud.mobile  from AmountRequest wr inner JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }
            str_query += " and wr.requestto = '" + objaccount.DepositrequestTo + "'";

            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getfranchiseeDepositRequestnew(clsAccount objaccount)
        {
            string str_query = "select wr.*,CA.AccountNo+'('+CA.bankname+')' as accno2,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as Image,case when requesttype='R' then 'Recharge Wallet' when requesttype='U' then 'Utility wallet' else 'Wallet' end as RequestType1,RequestTo ,ud.mobile from DepositRequest wr inner JOIN FranchiseeDetail ud ON wr.UserId=ud.UserId LEFT JOIN FranchiseeDetail ud2 ON ud2.UserId=ud.SponserId left join CompanyAccountDetail CA on wr.DepositBankID=CA.id where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }



            if (objaccount.WithdrawlRequestStatus != "0")
            {
                str_query += "  and wr.status = '" + objaccount.WithdrawlRequestStatus + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }
            str_query += " and wr.requestto = '" + objaccount.DepositrequestTo + "'";

            str_query += " order by wr.mentiondate  desc";



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
        public DataTable getWithdrawlRequestForLink(clsAccount objaccount)
        {
            string str_query = "select wr.*,ud.UserName,ud.SponserId,ud2.UserName AS Sponsername from withdrawlrequest wr LEFT JOIN userdetail ud ON wr.UserId=ud.UserId LEFT JOIN userdetail ud2 ON ud2.UserId=ud.SponserId where 1=1 and wr.status = 'Pending'  and wr.userid !='" + objaccount.CommitmentUserId + "' ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and wr.mentiondate  >= '" + objaccount.FromDate + "'   and wr.mentiondate   <= '" + objaccount.ToDate + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and wr.UserId = '" + objaccount.UserId + "' ";
            }


            str_query += " order by wr.mentiondate  desc";



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



        public DataTable getROIDetail(clsAccount objaccount)
        {
            string str_query = "select rd.*,ud.username,pd.planname from roidetail rd left join userdetail ud on rd.userid=ud.userid  left join planmaster pd on ud.planid=pd.planid  where 1=1 and detail = 'Direct ROI' ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and rd.roidate  >= '" + objaccount.FromDate + "'   and rd.roidate   <= '" + objaccount.ToDate + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and ud.UserId = '" + objaccount.UserId + "' ";
            }

            if (objaccount.UserName != "")
            {
                str_query += "  and ud.Username like '%" + objaccount.UserName + "%' ";
            }


            str_query += " order by ud.mentiondate  desc";



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


        public DataTable getClosingStatementDetail(clsAccount objaccount)
        {
            string str_query = "select rd.*,ud.username from closingdetail rd left join userdetail ud on rd.userid=ud.userid    where 1=1 and rd.publishstatus='0'  ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and rd.closingdate  >= '" + objaccount.FromDate + "'   and rd.closingdate   <= '" + objaccount.ToDate + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and ud.UserId = '" + objaccount.UserId + "' ";
            }

            if (objaccount.UserName != "")
            {
                str_query += "  and ud.Username like '%" + objaccount.UserName + "%' ";
            }


            str_query += " order by rd.closingdate  desc";



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

        public DataTable getClosingDetail(clsAccount objaccount)
        {
            string str_query = "select rd.*,ud.username from closingdetail rd left join userdetail ud on rd.userid=ud.userid    where 1=1  and rd.publishstatus='1'  ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and rd.closingdate  >= '" + objaccount.FromDate + "'   and rd.closingdate   <= '" + objaccount.ToDate + "' ";
            }

            if (objaccount.UserId != "")
            {
                str_query += "  and ud.UserId = '" + objaccount.UserId + "' ";
            }

            if (objaccount.UserName != "")
            {
                str_query += "  and ud.Username like '%" + objaccount.UserName + "%' ";
            }


            str_query += " order by rd.closingdate  desc";



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


        public DataTable getROIDetailSummary(clsAccount objaccount)
        {
            string str_query = "SELECT  sum(roiamount ) AS roiamount,roidate,max(mentiondate) AS mentiondate FROM ROIDetail  where 1=1 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and roidate  >= '" + objaccount.FromDate + "'   and roidate   <= '" + objaccount.ToDate + "' ";
            }


            str_query += "     GROUP BY ROIDate    order by roidate  desc";



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


        public DataTable getHelpLink(clsAccount objaccount)
        {
            string str_query = "SELECT hd.*,ud.UserName AS ReceiverName FROM HelpLinkDetail hd LEFT JOIN userdetail ud ON ud.UserId=hd.ReceiverId WHERE hd.DonarId='" + objaccount.UserId + "' order by hd.mentiondate";

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


        public DataTable getFundCreditReport(clsAccount objaccount)
        {
            string str_query = "SELECT td.*,ud.UserName FROM TransactionDetail td LEFT JOIN userdetAil ud ON td.UserID=ud.UserId WHERE td.cramount>0 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and td.mentiondate  >= '" + objaccount.FromDate + "'   and td.mentiondate   <= '" + objaccount.ToDate + "' ";
            }
            if (objaccount.UserId != "")
            {
                str_query += "  and td.userid  = '" + objaccount.UserId + "'  ";
            }


            str_query += " order by td.mentiondate ";



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

        public DataTable getFundDebitReport(clsAccount objaccount)
        {
            string str_query = "SELECT td.*,ud.UserName FROM TransactionDetail td LEFT JOIN userdetAil ud ON td.UserID=ud.UserId WHERE td.dramount>0 ";


            if (objaccount.FromDate != DateTime.MinValue && objaccount.ToDate != DateTime.MinValue)
            {
                str_query += "  and td.mentiondate  >= '" + objaccount.FromDate + "'   and td.mentiondate   <= '" + objaccount.ToDate + "' ";
            }
            if (objaccount.UserId != "")
            {
                str_query += "  and td.userid  = '" + objaccount.UserId + "'  ";
            }


            str_query += " order by td.mentiondate ";



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

        public DataTable getWalletReport(clsAccount objaccount)
        {
            string str_query = "SELECT isnull((SELECT isnull(sum(td.cramount),0)-isnull(sum(td.dramount),0)  FROM TransactionDetail td WHERE td.UserID=ud.UserId ),0) AS BalanceAmount, ud.SponserId, ud2.UserName AS sponsername , ud.Email,ud.username,ud.userid,pm.planamount  from userdetail ud   left join planmaster pm on ud.planid=pm.planid  LEFT JOIN userdetail ud2 ON ud.SponserId=ud2.UserId  where 1=1 ";
            if (objaccount.UserId != "")
            {
                str_query += " and  ud.userid='" + objaccount.UserId + "'";
            }
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


        public string FundCredit(clsAccount objAccount)
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
                DataTable dt = new DataTable();

                s2 = "SELECT * FROM userdetail WHERE userid='" + objAccount.UserId + "' ";
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    s2 = "declare @id int; set @id=(select isnull(max(transactionid),0)+1 from  TransactionDetail);  insert into TransactionDetail (transactionid,cramount ,dramount ,userid ,transactiontype ,remark, mentionby,mentiondate)  values (@id," + objAccount.Amount + ",0,'" + objAccount.UserId + "','Admin Credit','" + objAccount.Remark + "','" + objAccount.MentionBy + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    res = "t";
                }
                else
                {
                    res = "f";
                }

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

        public string FundDebit(clsAccount objAccount)
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
                DataTable dt = new DataTable();

                s2 = "SELECT * FROM userdetail WHERE userid='" + objAccount.UserId + "' ";
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    s2 = "declare @id int; set @id=(select isnull(max(transactionid),0)+1 from  TransactionDetail);  insert into TransactionDetail (transactionid,dramount ,cramount ,userid ,transactiontype ,remark, mentionby,mentiondate)  values (@id," + objAccount.Amount + ",0,'" + objAccount.UserId + "','Admin Debit','" + objAccount.Remark + "','" + objAccount.MentionBy + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    res = "t";
                }
                else
                {
                    res = "f";
                }

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


        public string UpdateLevelMaster(clsAccount objAccount)
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
                s2 = "update levelmaster set comissionpercent='" + objAccount.ComissionPercent + "' where [level]='" + objAccount.LevelNo + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string UpdateselfbonusMaster(clsAccount objAccount)
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
                s2 = "update SelfPurchaseBonusMaster set Bonus='" + objAccount.ComissionPercent + "' where fromid='" + objAccount.LevelNo + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string UpdatesalesLevelMaster(clsAccount objAccount)
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
                s2 = "update SalesLevelMaster set RechargeCommssion='" + objAccount.ComissionPercent + "',DTHCommission='" + objAccount.CommitmentAmount + "' where [level]='" + objAccount.LevelNo + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string UpdateFranchseeCommission(clsAccount objAccount)
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
                s2 = "update FranchiseeCommission set Commission='" + objAccount.ComissionPercent + "' where Id='" + objAccount.LevelNo + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string Generate_Growth(clsAccount objaccount)
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
                s2 = "sp_GenerateGrowth";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@ClosingDate",objaccount.ClosingDate),
                new SqlParameter("@MentionBy",objaccount.MentionBy)
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

        public string Growth_Comission_Set(clsAccount objAccount)
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

                s2 = "insert into SystemSetting(GrowthPercent ,MentionDate ) values (" + objAccount.ComissionPercent + ",getdate())";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                res = "t";


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

        public string Insert_WithdrawlRequest(clsAccount objaccount)
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
                s2 = "sp_addWithdrawlRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@RequestType",objaccount.DepositrequestRequestType),
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

        public DataTable Insert_WithdrawlRequestDt(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_addWithdrawlRequest";
                SqlParameter[] parameter = {
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount),
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@RequestType",objaccount.DepositrequestRequestType),
                };
                dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
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
            return dt;
        }

        public string Insert_WithdrawlRequestdirectbyadmin(clsAccount objaccount)
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
                s2 = "sp_addWithdrawlRequestdirect";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@RequestType",objaccount.DepositrequestRequestType),
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                if (res == "t")
                {
                    s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                    DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    if (Dtmsms.Rows.Count > 0)
                    {
                        string MobileNo = "";
                        string BalanceAmount = "";
                        MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                        if (objaccount.DepositrequestRequestType == "R")
                        {
                            BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                        }
                        if (objaccount.DepositrequestRequestType == "U")
                        {
                            BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                        }
                        string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "funddebit");
                        string Result = url.CallURL();
                        objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    }
                }

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

        public string Insert_DirectWithdrawlFranchiseeByAdmin(clsAccount objaccount)
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
                s2 = "sp_addDirectWithdrawlFranchisee";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@RequestType",objaccount.DepositrequestRequestType),
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                if (res == "t")
                {
                    s2 = "select mobile,isnull(balanceamount,0) as balanceamount from FranchiseeDetail where userid='" + objaccount.UserId + "'";
                    DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    if (Dtmsms.Rows.Count > 0)
                    {
                        string MobileNo = "";
                        string BalanceAmount = "";
                        MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                        string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "funddebit");
                        string Result = url.CallURL();
                        objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    }
                }


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
        public string Insert_DepositRequestold(clsAccount objaccount)
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
                s2 = "sp_addDepositRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType)
                     
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

        public string Insert_DepositRequest(clsAccount objaccount)
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
                s2 = "sp_addDepositRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType)
                     
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


        public string Insert_TopupRequest(clsAccount objaccount)
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
                s2 = "sp_addTopupRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType),
                               new SqlParameter("@PlanId",objaccount.TransactionId)
                     
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
        public string Direct_DepositRequest(clsAccount objaccount)
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
                s2 = "sp_addretailerDepositRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType)
                     
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                if (res == "t")
                {
                    //s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                    //DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    //if (Dtmsms.Rows.Count > 0)
                    //{
                    //    string MobileNo = "";
                    //    string BalanceAmount = "";
                    //    MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                    //    if (objaccount.DepositrequestRequestType == "R")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                    //    }
                    //    if (objaccount.DepositrequestRequestType == "U")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                    //    }
                    //    string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy,"","","", "fundcredit");
                    //    string Result = url.CallURL();
                    //    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    //}
                }

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
        public string Direct_franciseeDepositRequest(clsAccount objaccount)
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
                s2 = "sp_addfranchiseeDepositRequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType)
                     
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                if (res == "t")
                {
                    s2 = "select mobile,isnull(balanceamount,0) as balanceamount from FranchiseeDetail where userid='" + objaccount.UserId + "'";
                    DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    if (Dtmsms.Rows.Count > 0)
                    {
                        string MobileNo = "";
                        string BalanceAmount = "";
                        MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                        string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                        string Result = url.CallURL();
                        objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    }
                }

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



        public string Approve_PrimeMemberRequest(clsAccount objaccount)
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


                s2 = "sp_activeUserBV";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@RequestId",objaccount.WithdrawlRequestId),
                new SqlParameter("@UserId",objaccount.UserId), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@BV",objaccount.Amount),
               
                     
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
        public string Rejected_PrimeMemberRequest(clsAccount objaccount)
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


                s2 = "select * from CashRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {


                    s2 = "update CashRequest set status='REJECTED',ApproveBy='" + objaccount.MentionBy + "' , approvedate=getdate(),transactionid=" + "0" + " where   id=" + objaccount.WithdrawlRequestId + " ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);


                    res = "t";
                }
                else
                {
                    res = "f";
                }


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
        public string Approve_WithdrawlRequest(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            string s3 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "select * from withdrawlrequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    string Newbalance = "";
                    decimal deductionPercent = 0;
                    if (objaccount.WithdrawlAccountType.ToUpper() == "R")
                    {
                        s2 = "select isnull(balanceamount,0) as balanceamount,isnull(Utilitybalance,0) as Utilitybalance from userdetail  where userid='" + objaccount.UserId + "'   ";
                        s3 = "select isnull(CashWalletPercent,0) as CashWalletPercent from tbl_Deduction";
                        DataTable dt2 = new DataTable();
                        dt2 = ObjData.RunSelectQueryTTrans(s3, tr);
                        deductionPercent = Convert.ToDecimal(dt2.Rows[0]["CashWalletPercent"].ToString());
                    }
                    if (objaccount.WithdrawlAccountType.ToUpper() == "U")
                    {
                        s2 = "select isnull(Utilitybalance,0) as Utilitybalance, isnull(balanceamount,0) as balanceamount from userdetail  where userid='" + objaccount.UserId + "'   ";
                    }
                    DataTable dt1 = new DataTable();
                    dt1 = ObjData.RunSelectQueryTTrans(s2, tr);
                    decimal balanceamount = Convert.ToDecimal(dt1.Rows[0]["balanceamount"].ToString());
                    decimal Utilitybalance = Convert.ToDecimal(dt1.Rows[0]["Utilitybalance"].ToString());
                    Decimal shoppingwallet = (objaccount.WithdrawlAmount * deductionPercent) / 100;
                    if (balanceamount >= objaccount.WithdrawlAmount)
                    {

                        Newbalance = Convert.ToString(Convert.ToDecimal(balanceamount) - objaccount.WithdrawlAmount);
                        objaccount.Remark = " withdrawl request from userid" + objaccount.UserId + " Amount:" + objaccount.WithdrawlAmount.ToString();
                        string strtransactionid = "0";
                        s2 = "select isnull(max(transactionid),0)+1 as transactionid from  TransactionDetail";
                        DataTable dttran = new DataTable();
                        dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                        strtransactionid = dttran.Rows[0][0].ToString();
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + objaccount.UserId + "','Withdrawl','"+objaccount.Remark+"','" + objaccount.MentionBy + "',getdate())";
                        if (objaccount.WithdrawlAccountType.ToUpper() == "R")
                        {
                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + balanceamount + "','" + Newbalance + "','" + objaccount.UserId + "','Withdrawl','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),1)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);



                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + shoppingwallet + ",0,'" + Utilitybalance + "','" + (Utilitybalance + shoppingwallet) + "','" + objaccount.UserId + "','Withdrawl Transfer','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),2)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        if (objaccount.WithdrawlAccountType.ToUpper() == "U")
                        {
                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + balanceamount + "','" + Newbalance + "','" + objaccount.UserId + "','Withdrawl','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),2)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);

                        }



                        s2 = "update withdrawlrequest set status='Approved', ApproveBy='" + objaccount.MentionBy + "' , approvedate=getdate(),transactionid=" + strtransactionid + ",Paymentmode='" + objaccount.PaymentMode + "', OnlineTransactionId='" + OnlineTransactionId + "' where id=" + objaccount.WithdrawlRequestId + "  ";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);

                        if (objaccount.WithdrawlAccountType.ToUpper() == "R")
                        {
                            s2 = "update userdetail set balanceamount=isnull(balanceamount,0)-" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);

                            s2 = "update userdetail set UtilityBalance=isnull(UtilityBalance,0)+" + shoppingwallet + "  where userid='" + objaccount.UserId + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        if (objaccount.WithdrawlAccountType.ToUpper() == "U")
                        {
                            s2 = "update userdetail set UtilityBalance=isnull(UtilityBalance,0)-" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }

                        string url = objUser.smstemplate("", objaccount.UserMObile, "", "", "RIDHIKA MARKETING", "www.ridhikamarketing.com", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), Newbalance, objaccount.MentionBy, "", "", "", "funddebit");
                        string Result = url.CallURL();
                        objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);

                        res = "t";
                    }
                    else
                    {
                        res = "f";
                    }
                }
                else
                {
                    res = "f";
                }


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
        public DataTable Approve_depositRequestNew(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_approve_Depositrequest";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@GenerateUserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@ID",objaccount.WithdrawlRequestId),
              
                     
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                if (Dt != null && Dt.Rows[0][0].ToString() == "1")
                {
                    tr.Commit();
                    //s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                    //DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    //if (Dtmsms.Rows.Count > 0)
                    //{
                    //    string MobileNo = "";
                    //    string BalanceAmount = "";
                    //    MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                    //    if (objaccount.DepositrequestRequestType == "R")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                    //    }
                    //    if (objaccount.DepositrequestRequestType == "U")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                    //    }
                    //    string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                    //    string Result = url.CallURL();
                    //    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    //}
                }


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
            return Dt;
        }


        public DataTable getAdminDashboard(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "Admin_Dashboard";
                SqlParameter[] parameter = {
                new SqlParameter("@UserId",objaccount.UserId),
               
                };
                dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
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
            return dt;
        }

        public DataTable Approve_topupRequest(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_approve_Topup";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@GenerateUserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@ID",objaccount.WithdrawlRequestId),
              
                     
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                if (Dt != null && Dt.Rows[0][0].ToString() == "1")
                {
                    tr.Commit();
                    //s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                    //DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                    //if (Dtmsms.Rows.Count > 0)
                    //{
                    //    string MobileNo = "";
                    //    string BalanceAmount = "";
                    //    MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                    //    if (objaccount.DepositrequestRequestType == "R")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                    //    }
                    //    if (objaccount.DepositrequestRequestType == "U")
                    //    {
                    //        BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                    //    }
                    //    string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                    //    string Result = url.CallURL();
                    //    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                    //}
                }


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
            return Dt;
        }

        public string Approve_depositRequest(clsAccount objaccount)
        {
            string cuurentretailerbalanse = "";
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "select * from DepositRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    if (objaccount.DepositrequestTo.ToUpper() != "ADMIN")
                    {


                        string str_query = "select isnull( sum(cramount),0)-isnull( sum(dramount),0) from transactiondetail td where 1=1  and td.UserId = '" + objaccount.DepositrequestTo + "'  ";
                        DataTable dtchk = ObjData.RunSelectQueryTTrans(str_query, tr);
                        Decimal bal = Convert.ToDecimal(dtchk.Rows[0][0].ToString());
                        if (bal < objaccount.WithdrawlAmount)
                        {
                            res = "nn";
                            goto TheEnd;
                        }

                    }


                    s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0; ";

                    if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                    {
                        s2 += @"select @oldBalance=isnull(BalanceAmount,0) from userdetail where userid='" + objaccount.UserId + @"'; ";
                    }
                    else if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                    {
                        s2 += @"select @oldBalance=isnull(Utilitybalance,0) from userdetail where userid='" + objaccount.UserId + @"'; ";
                    }

                    s2 += @"set @newBalance=@oldBalance+" + objaccount.WithdrawlAmount + @"; 
                                select @oldBalance as oldBalance,@newBalance as currentBalance";

                    DataTable dt11 = new DataTable();
                    dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                    string oldBal = dt11.Rows[0]["oldBalance"].ToString();
                    string newBal = dt11.Rows[0]["currentBalance"].ToString();

                    objaccount.Remark = "Deposit Request from Userid:" + objaccount.UserId + " amount:" + objaccount.WithdrawlAmount.ToString();

                    string strtransactionid = "0";
                    s2 = "select isnull(max(transactionid),0)+1 from  TransactionDetail";
                    DataTable dttran = new DataTable();
                    dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                    strtransactionid = dttran.Rows[0][0].ToString();
                    if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                    {
                        s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + objaccount.WithdrawlAmount + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Deposit','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),1)";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                    }
                    if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                    {
                        s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + objaccount.WithdrawlAmount + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Deposit','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),2)";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);

                        s2 = "sp_activeUserFromDeposit";
                        SqlParameter[] parameter = {                                              
                new SqlParameter("@RequestId",objaccount.WithdrawlRequestId),
                new SqlParameter("@UserId",objaccount.UserId), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@BV","1000"),
               
                     
                };
                        res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

                    }

                    cuurentretailerbalanse = newBal;
                    //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.TDS + ",'" + objaccount.UserId + "','TDS','TDS For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                    //ObjData.RunInsUpDelQueryTrans(s2, tr);
                    //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.AdminCharge + ",'" + objaccount.UserId + "','Admin Charge','Admin Charge For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                    //ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "update DepositRequest set status='Approved', ApproveBy='" + objaccount.MentionBy + "' , approvedate=getdate(),transactionid=" + strtransactionid + " where id=" + objaccount.WithdrawlRequestId + "  ";

                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                    {
                        s2 = "update userdetail set balanceamount=isnull(balanceamount,0)+" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                    }
                    if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                    {

                        s2 = "update userdetail set Utilitybalance=isnull(Utilitybalance,0)+" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                    }
                    if (objaccount.DepositrequestTo.ToUpper() != "ADMIN")
                    {
                        if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                        {
                            s2 = "select isnull(commission,0) from  FranchiseeCommission where id='1'";
                        }
                        if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                        {
                            s2 = "select isnull(commission,0) from  FranchiseeCommission where id='2'";
                        }
                        DataTable dttran123 = new DataTable();
                        dttran123 = ObjData.RunSelectQueryTTrans(s2, tr);
                        Decimal Commission = Convert.ToDecimal(dttran123.Rows[0][0].ToString());
                        Decimal Amount = Convert.ToDecimal(objaccount.WithdrawlAmount);
                        Decimal Totalamount = Amount * Commission / 100;


                        s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                                    select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.DepositrequestTo + @"';
                                    set @newBalance=@oldBalance+" + Totalamount + @"; 
                                    select @oldBalance as oldBalance,@newBalance as currentBalance";
                        dt11 = new DataTable();
                        dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                        oldBal = dt11.Rows[0]["oldBalance"].ToString();
                        newBal = dt11.Rows[0]["currentBalance"].ToString();


                        s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + Math.Round(Totalamount, 2) + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.DepositrequestTo + "','Fund Request Commission','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);

                        s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)+" + Totalamount + "  where userid='" + objaccount.DepositrequestTo + "'";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);




                        s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                                    select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.DepositrequestTo + @"';
                                    set @newBalance=@oldBalance-" + objaccount.WithdrawlAmount + @"; 
                                    select @oldBalance as oldBalance,@newBalance as currentBalance";
                        dt11 = new DataTable();
                        dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                        oldBal = dt11.Rows[0]["oldBalance"].ToString();
                        newBal = dt11.Rows[0]["currentBalance"].ToString();


                        s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + oldBal + "','" + newBal + "','" + objaccount.DepositrequestTo + "','Fund Request Approved','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);

                        s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)-" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.DepositrequestTo + "'";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);


                    }

                    string url = objUser.smstemplate("", objaccount.UserMObile, "", "", "DGTALSHOP", "www.dgtalshop.com", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), cuurentretailerbalanse, objaccount.MentionBy, "", "", "", "fundcredit");
                    string Result = url.CallURL();
                    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);

                    res = "t";

                }
                else
                {
                    res = "f";
                }


                tr.Commit();
            TheEnd: ;
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
        public string Approve_franchiseedepositRequest(clsAccount objaccount)
        {
            string cuurentretailerbalanse = "";
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "select * from DepositRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    if (objaccount.DepositrequestTo.ToUpper() != "ADMIN")
                    {


                        string str_query = "select isnull( sum(cramount),0)-isnull( sum(dramount),0) from transactiondetail td where 1=1  and td.UserId = '" + objaccount.DepositrequestTo + "'  ";
                        DataTable dtchk = ObjData.RunSelectQueryTTrans(str_query, tr);
                        Decimal bal = Convert.ToDecimal(dtchk.Rows[0][0].ToString());
                        if (bal < objaccount.WithdrawlAmount)
                        {
                            res = "nn";
                            goto TheEnd;
                        }

                    }

                    s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                            select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.UserId + @"';
                            set @newBalance=@oldBalance+" + objaccount.WithdrawlAmount + @"; 
                            select @oldBalance as oldBalance,@newBalance as currentBalance";
                    DataTable dt11 = new DataTable();
                    dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                    string oldBal = dt11.Rows[0]["oldBalance"].ToString();
                    string newBal = dt11.Rows[0]["currentBalance"].ToString();

                    objaccount.Remark = "Deposit Request from Userid:" + objaccount.UserId + " amount:" + objaccount.WithdrawlAmount.ToString();

                    string strtransactionid = "0";
                    s2 = "select isnull(max(transactionid),0)+1 from  TransactionDetail";
                    DataTable dttran = new DataTable();
                    dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                    strtransactionid = dttran.Rows[0][0].ToString();

                    s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + objaccount.WithdrawlAmount + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Deposit','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    cuurentretailerbalanse = newBal;

                    //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.TDS + ",'" + objaccount.UserId + "','TDS','TDS For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                    //ObjData.RunInsUpDelQueryTrans(s2, tr);
                    //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.AdminCharge + ",'" + objaccount.UserId + "','Admin Charge','Admin Charge For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                    //ObjData.RunInsUpDelQueryTrans(s2, tr);
                    s2 = "update DepositRequest set status='Approved', ApproveBy='" + objaccount.MentionBy + "',approvedate=getdate(),transactionid=" + strtransactionid + " where id=" + objaccount.WithdrawlRequestId + "  ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)+" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);


                    string url = objUser.smstemplate("", objaccount.UserMObile, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), cuurentretailerbalanse, objaccount.MentionBy, "", "", "", "fundcredit");
                    string Result = url.CallURL();
                    objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);

                    res = "t";

                }
                else
                {
                    res = "f";
                }


                tr.Commit();
            TheEnd: ;
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
        public string Reject_WithdrawlRequest(clsAccount objaccount)
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
                s2 = "select * from withdrawlrequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {

                    s2 = "update withdrawlrequest set status='Rejected' , approvedate=getdate() where id=" + objaccount.WithdrawlRequestId + "  ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    res = "t";
                }
                else
                {
                    res = "f";
                }


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
        public string Reject_DepositRequest(clsAccount objaccount)
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
                s2 = "select * from topupRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {

                    s2 = "update topupRequest set status='Rejected' , approvedate=getdate() where id=" + objaccount.WithdrawlRequestId + "  ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    res = "t";
                }
                else
                {
                    res = "f";
                }


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

        public string Reject_amountRequest(clsAccount objaccount)
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
                s2 = "select * from amountRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {

                    s2 = "update amountRequest set status='Rejected' , approvedate=getdate() where id=" + objaccount.WithdrawlRequestId + "  ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    res = "t";
                }
                else
                {
                    res = "f";
                }


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
        public DataTable getUserWalletBalanceReport(clsAccount objaccount)
        {
            string str_query = "select isnull( sum(cramount),0) as sumCr,isnull( sum(dramount),0) as sumdr,isnull( sum(cramount),0)-isnull( sum(dramount),0) as bal from transactiondetail td where 1=1 ";

            if (objaccount.UserId != "")
            {
                str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
            }

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
        public DataTable getUserWalletBalanceReportrechargewallet(clsAccount objaccount)
        {
            string str_query = "select isnull( sum(cramount),0) as sumCr,isnull( sum(dramount),0) as sumdr,isnull( sum(cramount),0)-isnull( sum(dramount),0) as bal from transactiondetail td where 1=1";

            if (objaccount.UserId != "")
            {
                str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
            }

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
        public DataTable getUserWalletBalanceReportrechargeuntility(clsAccount objaccount)
        {
            string str_query = "select isnull( sum(cramount),0) as sumCr,isnull( sum(dramount),0) as sumdr,isnull( sum(cramount),0)-isnull( sum(dramount),0) as bal from transactiondetail td where 1=1 and type='2'";

            if (objaccount.UserId != "")
            {
                str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
            }

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
        public string UserOwnActivation(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable ds = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_activeOwnWithEpin";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@ActivateUserId",objaccount.UserId),
                new SqlParameter("@NoOfEPin",1),
                  new SqlParameter("@Amount",objaccount.Amount),
                   new SqlParameter("@PlanName",objaccount.plananame)
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

        public string UserCashrequest(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable ds = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_Requestwithoutprimemember";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),             
                   new SqlParameter("@RequestType",objaccount.plananame),
                     new SqlParameter("@UBV",objaccount.Amount)

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

        public string Direct_DepositRequestByUserFromFranchisee(clsAccount objaccount)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_addretailerDepositRequestFromFranchisee";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType)
                     
                };

                dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                if (dt.Rows[0][0].ToString() == "t")
                {
                    objaccount.WithdrawlRequestId = dt.Rows[0][1].ToString();

                    //objaccount.userType = "Franchisee";     //      property created above...

                    //res = Approve_depositRequest(objaccount);

                    //if (res == "t")
                    //{
                    //====================================================================================

                    s2 = "select * from DepositRequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                    DataTable dt1 = new DataTable();
                    dt1 = ObjData.RunSelectQueryTTrans(s2, tr);
                    if (dt1.Rows.Count > 0)
                    {
                        if (objaccount.DepositrequestTo.ToUpper() != "ADMIN")
                        {


                            string str_query = "select isnull( sum(cramount),0)-isnull( sum(dramount),0) from transactiondetail td where 1=1  and td.UserId = '" + objaccount.DepositrequestTo + "'  ";
                            DataTable dtchk = ObjData.RunSelectQueryTTrans(str_query, tr);
                            Decimal bal = Convert.ToDecimal(dtchk.Rows[0][0].ToString());
                            if (bal < objaccount.WithdrawlAmount)
                            {
                                res = "nn";
                                tr.Rollback();
                                goto TheEnd;
                            }

                        }


                        s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0; ";

                        if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                        {
                            s2 += @"select @oldBalance=isnull(BalanceAmount,0) from userdetail where userid='" + objaccount.UserId + @"'; ";
                        }
                        else if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                        {
                            s2 += @"select @oldBalance=isnull(Utilitybalance,0) from userdetail where userid='" + objaccount.UserId + @"'; ";
                        }

                        s2 += @"set @newBalance=@oldBalance+" + objaccount.WithdrawlAmount + @"; 
                                select @oldBalance as oldBalance,@newBalance as currentBalance";

                        DataTable dt11 = new DataTable();
                        dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                        string oldBal = dt.Rows[0]["oldBalance"].ToString();
                        string newBal = dt.Rows[0]["currentBalance"].ToString();



                        string strtransactionid = "0";
                        s2 = "select isnull(max(transactionid),0)+1 from  TransactionDetail";
                        DataTable dttran = new DataTable();
                        dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                        strtransactionid = dttran.Rows[0][0].ToString();
                        if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                        {
                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + objaccount.WithdrawlAmount + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Deposit','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),1)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                        {
                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + objaccount.WithdrawlAmount + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Deposit','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),2)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.TDS + ",'" + objaccount.UserId + "','TDS','TDS For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                        //ObjData.RunInsUpDelQueryTrans(s2, tr);
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.AdminCharge + ",'" + objaccount.UserId + "','Admin Charge','Admin Charge For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                        //ObjData.RunInsUpDelQueryTrans(s2, tr);

                        s2 = "update DepositRequest set status='Approved', ApproveBy='" + objaccount.MentionBy + "' , approvedate=getdate(),transactionid=" + strtransactionid + " where id=" + objaccount.WithdrawlRequestId + "  ";

                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                        if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                        {
                            s2 = "update userdetail set balanceamount=isnull(balanceamount,0)+" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                        {

                            s2 = "update userdetail set Utilitybalance=isnull(Utilitybalance,0)+" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);
                        }
                        if (objaccount.DepositrequestTo.ToUpper() != "ADMIN")
                        {
                            if (objaccount.DepositrequestRequestType.ToUpper() == "R")
                            {
                                s2 = "select isnull(commission,0) from  FranchiseeCommission where id='1'";
                            }
                            if (objaccount.DepositrequestRequestType.ToUpper() == "U")
                            {
                                s2 = "select isnull(commission,0) from  FranchiseeCommission where id='2'";
                            }
                            DataTable dttran123 = new DataTable();
                            dttran123 = ObjData.RunSelectQueryTTrans(s2, tr);
                            Decimal Commission = Convert.ToDecimal(dttran123.Rows[0][0].ToString());
                            Decimal Amount = Convert.ToDecimal(objaccount.WithdrawlAmount);
                            Decimal Totalamount = Amount * Commission / 100;



                            s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                                    select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.DepositrequestTo + @"';
                                    set @newBalance=@oldBalance+" + Totalamount + @"; 
                                    select @oldBalance as oldBalance,@newBalance as currentBalance";
                            dt11 = new DataTable();
                            dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                            oldBal = dt.Rows[0]["oldBalance"].ToString();
                            newBal = dt.Rows[0]["currentBalance"].ToString();


                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + "," + Math.Round(Totalamount, 2) + ",0,'" + oldBal + "','" + newBal + "','" + objaccount.DepositrequestTo + "','Fund Request Commission','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);

                            s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)+" + Totalamount + "  where userid='" + objaccount.DepositrequestTo + "'";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);




                            s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                                    select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.DepositrequestTo + @"';
                                    set @newBalance=@oldBalance-" + objaccount.WithdrawlAmount + @"; 
                                    select @oldBalance as oldBalance,@newBalance as currentBalance";
                            dt11 = new DataTable();
                            dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                            oldBal = dt.Rows[0]["oldBalance"].ToString();
                            newBal = dt.Rows[0]["currentBalance"].ToString();


                            s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + oldBal + "','" + newBal + "','" + objaccount.DepositrequestTo + "','Fund Request Approved','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);


                            s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)-" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.DepositrequestTo + "'";
                            ObjData.RunInsUpDelQueryTrans(s2, tr);


                            s2 = "select mobile,isnull(balanceamount,0) as balanceamount,isnull(utilitybalance,0) as utilitybalance from userdetail where userid='" + objaccount.UserId + "'";
                            DataTable Dtmsms = ObjData.RunSelectQueryTTrans(s2, tr);
                            if (Dtmsms.Rows.Count > 0)
                            {
                                string MobileNo = "";
                                string BalanceAmount = "";
                                MobileNo = Dtmsms.Rows[0]["mobile"].ToString();
                                if (objaccount.DepositrequestRequestType == "R")
                                {
                                    BalanceAmount = Dtmsms.Rows[0]["BalanceAmount"].ToString();
                                }
                                if (objaccount.DepositrequestRequestType == "U")
                                {
                                    BalanceAmount = Dtmsms.Rows[0]["utilityBalance"].ToString();
                                }
                                string url = objUser.smstemplate("", MobileNo, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), BalanceAmount, objaccount.MentionBy, "", "", "", "fundcredit");
                                string Result = url.CallURL();
                                objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);
                            }



                        }

                        res = "t";
                        tr.Commit();
                    }
                    else
                    {
                        res = "f";
                        tr.Rollback();
                    }


                        //tr.Commit();
                //TheEnd: ;

                        //====================================================================================


                        //tr.Commit();
                TheEnd: ;
                    //}
                    //else
                    //{
                    //    tr.Rollback();
                    //}
                }
                else
                {
                    res = "0";
                    tr.Rollback();
                }
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



        public DataTable getAccountLedgerReport(clsAccount objaccount, string noOfRows, string fromDate, string toDate, string walletType)
        {
            DataSet ds = new DataSet();

            try
            {
                //string sql = @"select " + noOfRows + " MentionDate,userID,TransactionId, (case when TransactionType='Deposit' then Remark else TransactionType end) as [Description],DrAmount, " + 
                //                " CrAmount,(CrAmount-DrAmount) as [Current_Balance],Remark from transactiondetail where userId='" + objaccount.UserId + "'";

                string sql = @"select " + noOfRows + " tr.MentionDate,tr.userID,tr.TransactionId,  tr.Remark  as [Description],tr.DrAmount, " +
                                " tr.CrAmount,tr.oldbalance, tr.CurrentBalance,(case when upper(dr.MentionBy)='ADMIN' then tr.Remark else tr.Remark + ', Online TransactionId : '+dr.OnlineTransactionId end) as Remark, " +
                                " (case when tr.Type=1 then 'Wallet' when tr.Type=2 then 'Cash Wallet' when tr.Type=3 then 'Franchisee Wallet' else '' end)walletType " +
                                " from transactiondetail tr left join DepositRequest dr on dr.TransactionId=tr.TransactionId where tr.userId='" + objaccount.UserId + "'";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    sql += " and (convert(date,tr.MentionDate) between convert(date,'" + fromDate + "') and convert(date,'" + toDate + "') ) ";

                if (!string.IsNullOrEmpty(walletType))
                    sql += " and tr.Type='" + walletType + "' ";
                sql += " order by tr.MentionDate desc";
                ObjData.StartConnection();
                ds = ObjData.RunSelectQuery(sql);

                return ds.Tables[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                ds = null;
            }
        }

        public DataTable getFundTransferReceiveReport(clsAccount objaccount, string noOfRows, string fromDate, string toDate, string walletType)
        {
            DataSet ds = new DataSet();

            try
            {
                string sql = @"select " + noOfRows + @" td.transactionId,td.mentiondate,td.userid,ld.[role] as userType,(td.transactiontype+', '+td.remark) as [description],dramount,cramount, 
                            (case when [Type]=1 then 'Recharge Wallet' when [Type]=2 then 'Utility Wallet' when [Type]=3 then 'Franchisee Wallet' end) as walletType, 
                            (SElect isnull(sum(cramount),0)-isnull(sum(dramount),0) from transactiondetail where userid=td.userid and id<td.id and type=td.type)+td.cramount-td.dramount as Currentbalance, 
                            (SElect isnull(sum(cramount),0)-isnull(sum(dramount),0) from transactiondetail where userid=td.userid and id<td.id and type=td.type) as oldbalance, 
                            td.mentionby as approvedBy from transactiondetail td inner join logindetail ld on td.userId=ld.username 
                            where td.[Type] in (1,2,3) and (td.transactiontype like '%fund request%' or td.transactiontype='Deposit') ";

                if (!string.IsNullOrEmpty(objaccount.UserId) && objaccount.UserId != "0")
                {
                    sql += "  and td.UserId='" + objaccount.UserId + "' ";
                }

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    sql += " and (convert(date,td.MentionDate) between convert(date,'" + fromDate + "',103) and convert(date,'" + toDate + "',103) ) ";

                if (!string.IsNullOrEmpty(walletType))
                    sql += " and td.Type='" + walletType + "' ";

                sql += " order by td.id desc ";

                ObjData.StartConnection();
                ds = ObjData.RunSelectQuery(sql);

                return ds.Tables[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                ds = null;
            }
        }
        public string getPrimeMemberStatus(string userid)
        {
            string res = "";
            string str_query = " SELECT * FROM UserDetail WHERE UserId='" + userid + "' AND isnull(status,0)=0 ";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    str_query = "sp_GetPV";
                    SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",UserId), 
                   
                 
                  
                };
                    dt = ObjData.RunDataTableProcedure(str_query, parameter);
                    decimal bv = Convert.ToDecimal(dt.Rows[0][2].ToString());
                    if (bv >= 1000)
                    {
                        str_query = " SELECT * FROM CashRequest WHERE UserId='" + userid + "' AND Type='PRIME MEMBER' AND Status in ('PENDING','APPROVED') ";
                        dt = ObjData.RunDataTable(str_query);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            res = "0";
                        }
                        else
                        {
                            res = "1";
                        }
                    }
                    else
                    {
                        res = "0";
                    }
                }
                else
                {
                    res = "0";
                }
            }
            catch (Exception ex)
            {
                res = "0";
                dt = null;
            }
            ObjData.EndConnection();
            return res;
        }
        public int BecomePrimeMember(string userid)
        {
            int res = 0;
            string str_query = " UPDATE UserDetail SET status=1, Activatedate=getdate() WHERE UserId='" + userid + "' ";
            ObjData.StartConnection();
            try
            {
                ObjData.RunInsUpDelQuery(str_query);
                res = 1;
            }
            catch (Exception ex)
            {
                res = 0;
            }
            ObjData.EndConnection();
            return res;
        }
        public string Updatedeductioncommission(string Id, string admincharge, string tdswithpan, string tdswithoutpan, string cashwallet, string walletpercent, string cappingamount, string minamt, string maxamt)
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
                s2 = "update tbl_Deduction set admincharge='" + admincharge + "',tdswithpan='" + tdswithpan + "',tdswithoutpan='" + tdswithoutpan + "',CashWallet='" + cashwallet + "',CashWalletPercent='" + walletpercent + "',CappingAmount='" + cappingamount + "',MinDepositAmount='" + minamt + "',MaxDepositAmount='" + maxamt + "' where Id='" + Id + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string Updatewithdrawlsetting(string Id, string MinCashWithdrawl, string MaxCashWithdrawl, string DMRStatus)
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
                s2 = "update tbl_Deduction set MinCashWithdrawl='" + MinCashWithdrawl + "',MaxCashWithdrawl='" + MaxCashWithdrawl + "',DMRStatus='" + DMRStatus + "' where Id='" + Id + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public DataTable getAwardLevel()
        {
            string str_query = "select * from AwardMaster ";


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

        public string UpdateAwardMaster(clsAccount objAccount)
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
                s2 = "update Awardmaster set amount='" + objAccount.ComissionPercent + "',target='" + objAccount.CommitmentAmount + "' where [ulevel]='" + objAccount.LevelNo + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                res = "t";
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
        public string Approve_WithdrawlRequestfranchisee(clsAccount objaccount)
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
                s2 = "select * from withdrawlrequest where id=" + objaccount.WithdrawlRequestId + "   and status='Pending' ";
                DataTable dt = new DataTable();
                dt = ObjData.RunSelectQueryTTrans(s2, tr);
                if (dt.Rows.Count > 0)
                {
                    s2 = "select isnull(balanceamount,0) from FranchiseeDetail  where userid='" + objaccount.UserId + "'   ";
                    DataTable dt1 = new DataTable();
                    dt1 = ObjData.RunSelectQueryTTrans(s2, tr);
                    decimal balanceamount = Convert.ToDecimal(dt1.Rows[0][0].ToString());
                    if (balanceamount >= objaccount.WithdrawlAmount)
                    {
                        s2 = @"declare @oldBalance decimal(18,2)=0,@newBalance decimal(18,2)=0;
                            select @oldBalance=isnull(BalanceAmount,0) from FranchiseeDetail where userid='" + objaccount.UserId + @"';
                            set @newBalance=@oldBalance-" + objaccount.WithdrawlAmount + @"; 
                            select @oldBalance as oldBalance,@newBalance as currentBalance";
                        DataTable dt11 = new DataTable();
                        dt11 = ObjData.RunSelectQueryTTrans(s2, tr);

                        string oldBal = dt.Rows[0]["oldBalance"].ToString();
                        string newBal = dt.Rows[0]["currentBalance"].ToString();

                        objaccount.Remark = " withdrawl request from userid" + objaccount.UserId + " Amount:" + objaccount.WithdrawlAmount.ToString();

                        string strtransactionid = "0";
                        s2 = "select isnull(max(transactionid),0)+1 from  TransactionDetail";
                        DataTable dttran = new DataTable();
                        dttran = ObjData.RunSelectQueryTTrans(s2, tr);
                        strtransactionid = dttran.Rows[0][0].ToString();
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + objaccount.UserId + "','Withdrawl','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                        s2 = " insert into TransactionDetail (transactionid,cramount,dramount, oldBalance, currentBalance, userid,transactiontype,remark,mentionby,mentiondate,type)  values (" + strtransactionid + ",0," + objaccount.WithdrawlAmount + ",'" + oldBal + "','" + newBal + "','" + objaccount.UserId + "','Withdrawl','" + objaccount.Remark + "','" + objaccount.MentionBy + "',getdate(),3)";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.TDS + ",'" + objaccount.UserId + "','TDS','TDS For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                        //ObjData.RunInsUpDelQueryTrans(s2, tr);
                        //s2 = " insert into TransactionDetail (transactionid,cramount,dramount,userid,transactiontype,remark,mentionby,mentiondate)  values (" + strtransactionid + ",0," + objaccount.AdminCharge + ",'" + objaccount.UserId + "','Admin Charge','Admin Charge For User Id : " + objaccount.UserId + "','" + objaccount.MentionBy + "',getdate())";
                        //ObjData.RunInsUpDelQueryTrans(s2, tr);
                        s2 = "update withdrawlrequest set status='Approved', ApproveBy='" + objaccount.MentionBy + "' , approvedate=getdate(),transactionid=" + strtransactionid + ",Paymentmode='" + objaccount.PaymentMode + "', OnlineTransactionId='" + OnlineTransactionId + "' where id=" + objaccount.WithdrawlRequestId + "  ";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);
                        s2 = "update FranchiseeDetail set balanceamount=isnull(balanceamount,0)-" + objaccount.WithdrawlAmount + "  where userid='" + objaccount.UserId + "'  ";
                        ObjData.RunInsUpDelQueryTrans(s2, tr);

                        string url = objUser.smstemplate("", objaccount.UserMObile, "", "", "ARSENPAY", "www.arsenpay.in", objaccount.UserId, objaccount.WithdrawlAmount.ToString(), newBal, objaccount.MentionBy, "", "", "", "funddebit");
                        string Result = url.CallURL();
                        objUser.Insert_SendSMS(objaccount.UserMObile, Result, url);

                        res = "t";
                    }
                    else
                    {
                        res = "f";
                    }
                }
                else
                {
                    res = "f";
                }


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


        public string Epinamount { get; set; }

        public string NoOfEpin { get; set; }

        public string planid { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLogicTier
{
    public class clsvendor
    {
        Data ObjData = new Data();
        public string VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Ownername { get; set; }
        public string WithdrawlRequestStatus { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string GSTNo { get; set; }
        public string MentionBy { get; set; }


        public Decimal PurchaseAmount{get;set;}
            public Decimal CGST {get;set;}
             public Decimal SGST {get;set;}
             public Decimal IGST { get; set; }
             public Decimal CGSTAmt { get; set; }
             public Decimal SGSTAmt { get; set; }
             public Decimal IGSTAmt { get; set; }
             public Decimal PaybleAmountAmount{get;set;}
           public string Purchasedate{get;set;}

           public string PurchaseID { get; set; }
           public string UserId { get; set; }
           public DateTime Fromdate { get; set; }
           public DateTime Todate { get; set; }
           public string ProductId { get; set; }
           public string Purchasetype { get; set; }

           public string UpdateID { get; set; }

           public Decimal CashAmount { get; set; }
           public Decimal RestAmount { get; set; }

        public string Insert_Vendor(clsvendor objBank)
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
                s2 = "sp_add_VendorDetail";
                SqlParameter[] parameter = {  
                new SqlParameter("@CompanyName",objBank.CompanyName), 
                new SqlParameter("@OwnerName",objBank.Ownername), 
                new SqlParameter("@ContactNo",objBank.ContactNo), 
                new SqlParameter("@Address",objBank.Address), 
                new SqlParameter("@GSTNO",objBank.GSTNo), 
                new SqlParameter("@MentionBy",objBank.MentionBy)
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
        public DataTable getuserFranchiseePurchase(clsvendor objstate)
        {
            string str_query = " SELECT P.PurchaseID,P.FranchiseeID,V.username as FranchiseeName,P.userid,U.username,P.PurchaseAmount,U.Shippingaddress,CM.CityName,SM.StateName,U.ShippingAreaName,U.ShippingPincode, P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,P.orderNo FROM UserFranchiseePurchaseMaster P INNER JOIN FranchiseeDetail V ON P.FranchiseeID=V.userid  INNER JOIN userDetail U ON P.userid=U.userid INNER JOIN CityMaster CM ON CM.CityId=U.ShippingCityId INNER JOIN  StateMaster SM ON CM.StateId = SM.StateId ";

            if (objstate.Fromdate != DateTime.MinValue && objstate.Todate != DateTime.MinValue)
            {
                str_query += "  and cast(P.Entrydate as date)  >= cast('" + objstate.Fromdate + "' as date)   and cast(P.Entrydate as date)   <= cast('" + objstate.Todate + "' as date) ";
            }


            

            

            if (objstate.WithdrawlRequestStatus != "0")
            {
                str_query += "  and P.Cstatus = '" + objstate.WithdrawlRequestStatus + "' ";
            }

            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.FranchiseeID='" + objstate.VendorId + "'";
            }
            str_query += " order BY P.PurchaseId";
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
        public DataTable getUserFranchiseePurchaseProductChild(string ID)
        {
            string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.MRP,PR.TotalAmount,PR.ProductID,CGST,IGST,SGST,PurchaseAmount FROM UserFranchiseePurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID  where 1=1 ";


            if (ID != string.Empty)
            {
                str_query += " and PR.PurchaseID='" + ID + "'";
            }
            str_query += " order by PR.PurchaseID";
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

        public DataTable AddPurchase123(clsvendor objP, DataTable Dt)
        {
            int i = 0;

            string res = "";
            string s2 = "";
            string ChkStock = "1";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable ds = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {


                s2 = "sp_add_Purchaseoffline";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",objP.UserId),
                    new SqlParameter("@PurchaseAmount",objP.PurchaseAmount),
                    new SqlParameter("@CGSTAmount",objP.CGSTAmt),
                      new SqlParameter("@SGSTAmount",objP.SGSTAmt),
                    new SqlParameter("@IGSTAmount",objP.IGSTAmt),
                      new SqlParameter("@CGSTPer",objP.CGST),
                    new SqlParameter("@SGSTPer",objP.SGST),
                      new SqlParameter("@IGSTPer",objP.IGST),
                    new SqlParameter("@Paybleamount",objP.PaybleAmountAmount),
                     new SqlParameter("@FranchiseeId",objP.VendorId),
                      new SqlParameter("@PurchaseProduct",Dt),
                         new SqlParameter("@Cashamount",objP.CashAmount),
                            new SqlParameter("@RestAmount",objP.RestAmount),

                };
                ds = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                if (ds.Rows[0][1].ToString() == "1")
                {
                    tr.Commit();
                    //clsAPI objapi = new clsAPI();
                    //clsLogin objL = new clsLogin();
                    //DataTable dt1 = new DataTable();
                    //dt1 = objapi.GetSmsApi("admin");
                    //string message = "Dear User " + objP.UserId + " Your OrderNo " + ds.Rows[0]["OrderNo"].ToString() + " OrderAmount " + objP.PaybleAmountAmount.ToString() + " TotalBV " + ds.Rows[0]["BV"].ToString() + " and Commission " + ds.Rows[0]["commission"].ToString() + " !..Thanks http://freefireincome.com/";
                    //message = HttpUtility.UrlEncode(message, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    string smsBody = message;
                    //    string smsAPI = dt1.Rows[0]["Url"].ToString();
                    //    smsAPI = smsAPI.Replace("{mobileno}", ds.Rows[0]["Mobile"].ToString()).Replace("{msg}", smsBody);
                    //    string Result = smsAPI.CallURL();
                    //    objL.Insert_SendSMS(ds.Rows[0]["Mobile"].ToString(), Result, smsAPI);
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
            return ds;

        }

 public DataTable FranchiseePurchaseByAdmin(clsvendor objP, DataTable Dt)
        {
            int i = 0;

            string res = "";
            string s2 = "";
            string ChkStock = "1";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            DataTable Dtresult = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {

                //foreach (DataRow Dr in Dt.Rows)
                //{
                //    string Q = "select isnull(Sum(CrQuantity),0) -isnull(Sum(DrQuantity),0) FROM StockMaster where ProductId='" + Dr["ProductId"].ToString() + "'";
                //    DataSet Ds = ObjData.RunSelectQueryTrans(Q, tr);
                //    if (Convert.ToInt32(Dr["Quantity"].ToString()) > Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString()))
                //    {
                //        ChkStock = "0";
                //    }
                //}
                if (ChkStock == "1")
                {
                    s2 = "sp_add_Franchisee_Purchasebyadmin";
                    SqlParameter[] parameter = {
                    new SqlParameter("@UserId",objP.UserId),
                       new SqlParameter("@PurchaseAmount",objP.PurchaseAmount),
                          new SqlParameter("@CGSTAmount",objP.CGSTAmt),
                             new SqlParameter("@SGSTAmount",objP.SGSTAmt),
                                 new SqlParameter("@IGSTAmount",objP.IGSTAmt),
                                   new SqlParameter("@CGSTPer","0"),
                             new SqlParameter("@SGSTPer","0"),
                                 new SqlParameter("@IGSTPer","0"),
                                    new SqlParameter("@Paybleamount",objP.PaybleAmountAmount),
                    new SqlParameter("@FranchiseeId",objP.VendorId),
                       new SqlParameter("@PurchaseProduct",Dt),
                       new SqlParameter("@Cashamount",objP.CashAmount),
                       new SqlParameter("@RestAmount",objP.RestAmount),
                         new SqlParameter("@Paymentmode",objP.Purchasetype),
                       new SqlParameter("@ChequeNo",objP.UpdateID),
                         new SqlParameter("@Mentionby",objP.MentionBy),
                 

                };
                    Dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                    res = Dtresult.Rows[0][1].ToString();

                    tr.Commit();
                }
                else
                {
                    res = "3";
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
            return Dtresult;

        }




        public DataTable getUserRepurchasePackage(clsvendor objstate)
        {
            string str_query = "SELECT (P.CGST+P.SGST+P.IGST) GST, (CGSTPER+SGSTPER+IGSTPER)GSTPER,  P.PurchaseID,P.userId,V.username,P.PurchaseAmount,P.TotalAmount,(SELECT SUM(Quantity*BV) FROM PurchaseProductMaster pp INNER JOIN  UserPurchaseMaster UM  ON (UM.PurchaseId=pp.PurchaseId) WHERE PP.PurchaseId=P.PurchaseId  GROUP BY PRODUCTID ) RBV  FROM UserPurchaseMaster P INNER JOIN UserDetail V ON P.userID=V.userID ";

            str_query += "and P.PurchaseType='R'";
            if (objstate.Fromdate != DateTime.MinValue)
            {

                str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
            }
            if (objstate.Todate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
            }
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.userId='" + objstate.VendorId + "'";
            }
            if (objstate.Purchasetype != string.Empty)
            {
                str_query += " and P.Purchasetype='" + objstate.Purchasetype + "'";
            }
            str_query += " order BY P.PurchaseId";
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
        public DataTable getUserJoiningPackagePurchase(clsvendor objstate)
        {
            string str_query = "SELECT (P.CGST+P.SGST+P.IGST) GST, (CGSTPER+SGSTPER+IGSTPER)GSTPER,  P.PurchaseID,P.userId,V.username,P.PurchaseAmount,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,PL.BuisnessVolume as JBV FROM UserPurchaseMaster P INNER JOIN UserDetail V ON P.userID=V.userID inner join planmaster PL on(Pl.id=V.slabid)";

            str_query += "and P.PurchaseType='J'";
            if (objstate.Fromdate != DateTime.MinValue)
            {

                str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
            }
            if (objstate.Todate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
            }
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.userId='" + objstate.VendorId + "'";
            }
            if (objstate.Purchasetype != string.Empty)
            {
                str_query += " and P.Purchasetype='" + objstate.Purchasetype + "'";
            }
            str_query += " order BY P.PurchaseId";
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
        public string Update_BankAccount(clsvendor objBank)
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
                s2 = "sp_edit_VendorDetail";
                SqlParameter[] parameter = {  
                new SqlParameter("@id",objBank.VendorId), 
                 new SqlParameter("@CompanyName",objBank.CompanyName), 
                new SqlParameter("@OwnerName",objBank.Ownername), 
                new SqlParameter("@ContactNo",objBank.ContactNo), 
                new SqlParameter("@Address",objBank.Address), 
                new SqlParameter("@GSTNO",objBank.GSTNo),               
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
        public DataTable getVendorList()
        {
            string str_query = "select * from VenderDetail order by id";

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

        public DataTable getFranchiseeList()
        {
            string str_query = "select * from FranchiseeDetail order by id";

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

        public DataTable getFranchiseeListCityWise(string CityId)
        {
            string str_query = "select * from FranchiseeDetail where cityid='" + CityId + "' order by id";

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

        public string VendorPurchase(clsvendor objV,DataTable Dt)
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
                s2="Select isnull(max(Purchaseid),0)+ 1 as Id from VendorPurchaseMaster";
                DataSet Ds=ObjData.RunSelectQueryTrans(s2,tr);
                string Id=Ds.Tables[0].Rows[0][0].ToString();

                s2 = "insert into VendorPurchaseMaster(PurchaseId, VendorID, PurchaseAmount, CGST,SGST,IGST,CGSTPER,SGSTPER,IGSTPER,TotalAmount,Purchasedate,Entrydate)values('" + Id + "','" + objV.VendorId + "','" + objV.PurchaseAmount + "','" + objV.CGSTAmt + "','" + objV.SGSTAmt + "','" + objV.IGSTAmt + "','" + objV.CGST + "','" + objV.SGST + "','" + objV.IGST + "','" + objV.PaybleAmountAmount + "','" + objV.Purchasedate + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2,tr);

                foreach(DataRow Dr in Dt.Rows)
                {
                  //  ObjData.RunInsUpDelQueryTrans(s2,tr);

                    s2 = "insert into StockMaster(VendorPurchaseID,ProductId, Transactiondate, CrQuantity,DrQuantity,Entrydate,BatchNumber)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + objV.Purchasedate + "','" + Dr["Quantity"].ToString() + "','0',getdate(),'" + Dr["BatchNumber"].ToString() + "')";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "f";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
      
    }

        public string FranchiseePurchase(clsvendor objV, DataTable Dt)
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
                s2 = "Select isnull(max(Purchaseid),0)+ 1 as Id from FranchiseePurchaseMaster";
                DataSet Ds = ObjData.RunSelectQueryTrans(s2, tr);
                string Id = Ds.Tables[0].Rows[0][0].ToString();

                s2 = "insert into FranchiseePurchaseMaster(PurchaseId, FranchiseeID, PurchaseAmount, CGST,SGST,IGST,CGSTPER,SGSTPER,IGSTPER,TotalAmount,Purchasedate,Entrydate)values('" + Id + "','" + objV.VendorId + "','" + objV.PurchaseAmount + "','" + objV.CGSTAmt + "','" + objV.SGSTAmt + "','" + objV.IGSTAmt + "','" + objV.CGST + "','" + objV.SGST + "','" + objV.IGST + "','" + objV.PaybleAmountAmount + "','" + objV.Purchasedate + "',getdate())";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                foreach (DataRow Dr in Dt.Rows)
                {
                    s2 = "insert into FranchiseePurchaseProductMaster(PurchaseId, ProductId, Quantity, Amount,MRP,TotalAmount,Purchasedate,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + Dr["Quantity"].ToString() + "','" + Dr["Amount"].ToString() + "','" + Dr["BV"].ToString() + "','" + Dr["TotalAmount"].ToString() + "','" + objV.Purchasedate + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "insert into StockMaster(VendorPurchaseID,ProductId, Transactiondate, CrQuantity,DrQuantity,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + objV.Purchasedate + "',0,'" + Dr["Quantity"].ToString() + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "insert into FranchiseeStockMaster(VendorPurchaseID,ProductId, Transactiondate, CrQuantity,DrQuantity,Entrydate,FranchiseeId)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + objV.Purchasedate + "','" + Dr["Quantity"].ToString() + "','0',getdate(),'" + objV.VendorId + "')";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "f";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;

        }



        public string UserJoiningPurchase(clsvendor objV, DataTable Dt)
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
                s2 = "Select isnull(max(Purchaseid),0)+ 1 as Id from UserPurchaseMaster";
                DataSet Ds = ObjData.RunSelectQueryTrans(s2, tr);
                string Id = Ds.Tables[0].Rows[0][0].ToString();

                s2 = "insert into UserPurchaseMaster(PurchaseId, UserID, PurchaseAmount, CGST,SGST,IGST,CGSTPER,SGSTPER,IGSTPER,TotalAmount,Purchasedate,Entrydate,PurchaseType)values('" + Id + "','" + objV.VendorId + "','" + objV.PurchaseAmount + "','" + objV.CGSTAmt + "','" + objV.SGSTAmt + "','" + objV.IGSTAmt + "','" + objV.CGST + "','" + objV.SGST + "','" + objV.IGST + "','" + objV.PaybleAmountAmount + "','" + objV.Purchasedate + "',getdate(),'J')";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                s2 = "Update UserDetail set InvoiceStatus='1'  WHERE UserId='" + objV.VendorId + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                foreach (DataRow Dr in Dt.Rows)
                {
                    s2 = "insert into UserPurchaseProductMaster(PurchaseId, ProductId, Quantity, Amount,MRP,TotalAmount,Purchasedate,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + Dr["Quantity"].ToString() + "','" + Dr["Amount"].ToString() + "','" + Dr["BV"].ToString() + "','" + Dr["TotalAmount"].ToString() + "','" + objV.Purchasedate + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "insert into StockMaster(PurchaseID,ProductId, Transactiondate, CrQuantity,DrQuantity,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + objV.Purchasedate + "','0','" + Dr["Quantity"].ToString() + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "f";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;

        }
        public string UserRepurchasePurchase(clsvendor objV, DataTable Dt)
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
                s2 = "Select isnull(max(Purchaseid),0)+ 1 as Id from UserPurchaseMaster";
                DataSet Ds = ObjData.RunSelectQueryTrans(s2, tr);
                string Id = Ds.Tables[0].Rows[0][0].ToString();

                s2 = "insert into UserPurchaseMaster(PurchaseId, UserID, PurchaseAmount, CGST,SGST,IGST,CGSTPER,SGSTPER,IGSTPER,TotalAmount,Purchasedate,Entrydate,PurchaseType)values('" + Id + "','" + objV.VendorId + "','" + objV.PurchaseAmount + "','" + objV.CGSTAmt + "','" + objV.SGSTAmt + "','" + objV.IGSTAmt + "','" + objV.CGST + "','" + objV.SGST + "','" + objV.IGST + "','" + objV.PaybleAmountAmount + "','" + objV.Purchasedate + "',getdate(),'R')";
                ObjData.RunInsUpDelQueryTrans(s2, tr);


                s2 = "Update PurchaseProductMaster set invoicestatus='1'  WHERE PurchaseId='" + objV.UpdateID + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);

                foreach (DataRow Dr in Dt.Rows)
                {
                    s2 = "insert into UserPurchaseProductMaster(PurchaseId, ProductId, Quantity, Amount,MRP,TotalAmount,Purchasedate,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + Dr["Quantity"].ToString() + "','" + Dr["Amount"].ToString() + "','" + Dr["BV"].ToString() + "','" + Dr["TotalAmount"].ToString() + "','" + objV.Purchasedate + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);

                    s2 = "insert into StockMaster(PurchaseID,ProductId, Transactiondate, CrQuantity,DrQuantity,Entrydate)values('" + Id + "','" + Dr["ProductId"].ToString() + "','" + objV.Purchasedate + "','0','" + Dr["Quantity"].ToString() + "',getdate())";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "f";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;

        }
        public DataTable getVendorPurchase(clsvendor objstate)
        {
            string str_query = "SELECT P.PurchaseID,P.VendorId,V.Companyname,P.PurchaseAmount,P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate FROM VendorPurchaseMaster P INNER JOIN VenderDetail V ON P.VendorID=V.id ";

            if (objstate.Fromdate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
            }
            if (objstate.Todate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
            }
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.VendorId='" + objstate.VendorId + "'";
            }
            str_query += " order BY P.PurchaseId";
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


        public DataTable getFranchiseePurchase(clsvendor objstate)
        {
            string str_query = "SELECT P.PurchaseID,P.FranchiseeID,V.username,P.PurchaseAmount,P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate FROM FranchiseePurchaseMaster P INNER JOIN FranchiseeDetail V ON P.FranchiseeID=V.userid ";

            if (objstate.Fromdate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
            }
            if (objstate.Todate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
            }
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.FranchiseeID='" + objstate.VendorId + "'";
            }
            str_query += " order BY P.PurchaseId";
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



        public int GetFranchiseeId(string userid)
        {
            int c = 0;


            string str_query = "  SELECT Id FROM FranchiseeDetail WITH(nolock) WHERE UserId='" + userid + "' ";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
                c = Convert.ToInt16(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                dt = null;
                c = 0;
            }
            ObjData.EndConnection();
            return c;

        }



        public DataTable getFranchiseeStock(clsvendor objstate)
        {
            string str_query = "  SELECT  fd.userid,fd.Username,pm.ProductId,pm.ProductName,pm.BV AS BV,pm.DP AS DP,isnull(sum(fm.CRQuantity),0) Recieve,isnull(sum(fm.DrQuantity),0) Sales,(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) AS stockleft, (BV * (isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0))) AS BVLEFT, (DP *(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) ) AS DPLEFT FROM FranchiseeStockMaster fm JOIN ProductMaster pm ON fm.ProductID=pm.ProductId JOIN franchiseedetail fd ON fm.FranchiseeID=fd.userid where 1=1";
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and fd.userid='" + objstate.VendorId + "'";
            }
            if (objstate.ProductId != string.Empty)
            {
                str_query += " and pm.ProductId='" + objstate.ProductId + "'";
            }
            str_query += " GROUP BY fd.userid,fd.Username,pm.ProductId,pm.ProductName,pm.BV,pm.DP  order BY pm.ProductId";
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



        
        public DataTable getUserPurchasePurchase(clsvendor objstate)
        {
            string str_query = "SELECT P.PurchaseID,P.userId,V.username,P.PurchaseAmount,P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,case when P.PurchaseType='J' then 'Joining' else 'Repurchase' end as PurchaseType FROM UserPurchaseMaster P INNER JOIN UserDetail V ON P.userID=V.userID ";

            if (objstate.Fromdate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
            }
            if (objstate.Todate != DateTime.MinValue)
            {
                str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
            }
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.userId='" + objstate.VendorId + "'";
            }
            if (objstate.Purchasetype != string.Empty)
            {
                str_query += " and P.Purchasetype='" + objstate.Purchasetype + "'";
            }
            str_query += " order BY P.PurchaseId";
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
        public DataTable getUserPurchasePurchaseforinvoice(clsvendor objstate)
        {
            string str_query = "SELECT P.PurchaseID,P.userId,V.username,(SELECT Mobile FROM UserDetail WHERE UserId=P.userId) AS ContactNo,(SELECT Address+' '+C.CityName+' '+S.StateName+' '+U.Pincode FROM UserDetail U INNER JOIN CityMaster C ON U.CityId=C.CityId INNER JOIN StateMaster S on S.Stateid=C.StateId   WHERE UserId=P.userId) AS address,P.PurchaseAmount,P.CGST,P.SGST,P.IGST,P.CGSTper,P.SGSTper,P.IGSTper,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,case when P.PurchaseType='J' then 'Joining' else 'Repurchase' end as PurchaseType,V.email FROM UserPurchaseMaster P INNER JOIN UserDetail V ON P.userID=V.userID ";

           
            if (objstate.VendorId != string.Empty)
            {
                str_query += " and P.userId='" + objstate.VendorId + "'";
            }
            if (objstate.PurchaseID != string.Empty)
            {
                str_query += " and P.PurchaseId='" + objstate.PurchaseID + "'";
            }
            str_query += " order BY P.PurchaseId";
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
        public DataTable getUserPurchaseProductChild(string ID)
        {
            string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.MRP,PR.TotalAmount,PR.ProductID FROM UserPurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID  where 1=1 ";


            if (ID != string.Empty)
            {
                str_query += " and PR.PurchaseID='" + ID + "'";
            }
            str_query += " order by PR.PurchaseID";
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
        public DataTable getVendorPurchaseProductChild(string ID)
        {
            string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.MRP,PR.TotalAmount,PR.ProductID FROM VendorPurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID  where 1=1 ";


            if (ID != string.Empty)
            {
                str_query += " and PR.PurchaseID='" + ID + "'";
            }
            str_query += " order by PR.PurchaseID";
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

        public DataTable getFranchiseePurchaseProductChild(string ID)
        {
            string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.MRP,PR.TotalAmount,PR.ProductID FROM FranchiseePurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID  where 1=1 ";


            if (ID != string.Empty)
            {
                str_query += " and PR.PurchaseID='" + ID + "'";
            }
            str_query += " order by PR.PurchaseID";
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



        public DataTable getStock(clsvendor objV)
        {
            string str_query = "SELECT S.ProductID,P.ProductName,isnull(Sum(S.CrQuantity),0) as Cr,isnull(Sum(S.DrQuantity),0) as Dr,isnull(Sum(S.CrQuantity),0) -isnull(Sum(S.DrQuantity),0) as Stock FROM StockMaster S inner JOIN Productmaster P ON S.ProductID=P.ProductId where 1=1 ";
            if (objV.ProductId != string.Empty)
            {
                str_query += " and S.ProductID='" + objV.ProductId + "'";
            }
            str_query += " GROUP BY S.ProductID,P.ProductName";
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
}
}
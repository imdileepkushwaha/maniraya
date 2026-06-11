using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using System.Collections;

namespace BusinessLogicTier
{
    public class clsdthsuscription
    {
        Data ObjData = new Data();
        public string ServiceProviderId { get; set; }
        public string settopboxname { get; set; }
        public string Settopboxid { get; set; }
        public string Description { get; set; }
        public string Mrp { get; set; }
        public string entryuser { get; set; }

        public string validity { get; set; }
        public string noofchannel { get; set; }
        public string packageid { get; set; }
        public string packagename { get; set; }
        public string opid { get; set; }

        public string name { get; set; }
        public string address { get; set; }
        public string mobileno { get; set; }
        public string pincode { get; set; }
        public string ipaddress { get; set; }

        public string state { get; set; }
        public string City { get; set; }

        public string FRomdate { get; set; }
        public string Todate { get; set; }
        public string noofrow { get; set; }
        public string status { get; set; }
        public string retailerPrice { get; set; }
        public string Commission { get; set; }
        public string District { get; set; }
        public string LandMark { get; set; }

        public DataTable insertSetTopBox(clsdthsuscription objS)
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
                s2 = "proc_AddSetTopBox";
                SqlParameter[] parameter = {              
                    new SqlParameter("@SPID",objS.ServiceProviderId), 
                    new SqlParameter("@SBoxName",objS.settopboxname), 
                    new SqlParameter("@Remark",objS.Description), 
                     new SqlParameter("@MRP",objS.Mrp), 
                        new SqlParameter("@UserID",objS.entryuser), 
                 
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getSetTopBox(clsdthsuscription objS)
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
                s2 = "select oc.Operator,stb.ID,stb.SPID,stb.SBoxName STBName,stb.MRP,stb.Remark,oc.id as Operatorid  from tblDishSetTopBox stb inner join OperatorCode oc on oc.Id=stb.SPID order by stb.SBoxName";
               
                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getSetTopBoxbyid(clsdthsuscription objS)
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
                s2 = "select oc.Operator,stb.ID,stb.SPID,stb.SBoxName STBName,stb.MRP,stb.Remark,oc.id as Operatorid  from tblDishSetTopBox stb inner join OperatorCode oc on oc.Id=stb.SPID  where 1=1 and stb.spid='" + objS.opid + "' order by stb.SBoxName";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable updateSetTopBox(clsdthsuscription objS)
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
                s2 = "proc_UpdateSetTopBox";
                SqlParameter[] parameter = {    
                                                 new SqlParameter("@ID",objS.Settopboxid), 
                    new SqlParameter("@SPID",objS.ServiceProviderId), 
                    new SqlParameter("@SBoxName",objS.settopboxname), 
                    new SqlParameter("@Remark",objS.Description), 
                     new SqlParameter("@MRP",objS.Mrp), 
                        new SqlParameter("@UserID",objS.entryuser), 
                 
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getserviceprovider(clsdthsuscription objS)
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
                s2 = "SELECT * FROM OperatorCode WHERE Type=2 and isnull(ActiveStatus,1)=1 order by id";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getpackage(clsdthsuscription objS)
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
                s2 = "select (case when isnull(dp.ActiveStatus,1)=1 then 'Active' else 'Deactive' end) as ActiveStatus,oc.Operator ServiceProvider,oc.CustomerDisplay SPHint,oc.AccountDisplay SPAmountText,ds.SPID,ds.SBoxName,dp.commission,dp.retailerprice,dp.* from tblDishPackage dp inner join tblDishSetTopBox ds on ds.ID=dp.STBID inner join OperatorCode oc on oc.Id=ds.SPID order by dp.PackageName";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable insertpackage(clsdthsuscription objS)
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
                s2 = "proc_addSTBPackage";
                SqlParameter[] parameter = {              
                    new SqlParameter("@STBID",objS.Settopboxid), 
                    new SqlParameter("@PackageName",objS.packagename), 
                    new SqlParameter("@ValidityInDays",objS.validity), 
                     new SqlParameter("@ChannelsNo",objS.noofchannel), 
                       new SqlParameter("@PMRP",objS.Mrp), 
                         new SqlParameter("@Remark",objS.Description), 
                        new SqlParameter("@UserID",objS.entryuser)
           
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable updatepackage(clsdthsuscription objS)
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
                s2 = "proc_UpdateSTBPackage";
                SqlParameter[] parameter = {       
                                                new SqlParameter("@ID",objS.packageid),
                    new SqlParameter("@STBID",objS.Settopboxid), 
                    new SqlParameter("@PackageName",objS.packagename), 
                    new SqlParameter("@ValidityInDays",objS.validity), 
                     new SqlParameter("@ChannelsNo",objS.noofchannel), 
                       new SqlParameter("@PMRP",objS.Mrp), 
                         new SqlParameter("@Remark",objS.Description), 
                        new SqlParameter("@UserID",objS.entryuser), 
                 new SqlParameter("@Status",objS.status)
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getCommission(clsdthsuscription objS)
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
                s2 = "SELECT C.ID,C.PackageId,C.ComType,C.AmtType,C.AtRate,P.PackageName,O.Id as Operatorid,O.Operator as Operatorname FROM DTH_CommissionModule1 C INNER JOIN tblDishPackage P ON C.PackageId=P.ID INNER JOIN tblDishSetTopBox S ON P.STBID=S.ID INNER JOIN OperatorCode O ON S.SPID=O.Id where 1=1";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public int insertCommission(string Query)
        {

            string res = "";
            int s2 = 0;
            SqlConnection cn;
            SqlTransaction tr = null;
            DataTable Dt = new DataTable();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {

                ObjData.RunInsUpDelQueryTrans(Query, tr);
                s2 = 1;
                tr.Commit();

            }
            catch (Exception ex)
            {
                s2 = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return s2;
        }
        public DataTable getpackagebyid(clsdthsuscription objS)
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
                s2 = "select oc.Operator ServiceProvider,oc.CustomerDisplay SPHint,oc.AccountDisplay SPAmountText,ds.SPID,ds.SBoxName,dp.* from tblDishPackage dp inner join tblDishSetTopBox ds on ds.ID=dp.STBID inner join OperatorCode oc on oc.Id=ds.SPID  where 1=1 and stbid='" + objS.Settopboxid + "' order by dp.PackageName";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getpackagebyidown(clsdthsuscription objS)
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
                s2 = "select oc.Operator ServiceProvider,oc.CustomerDisplay SPHint,oc.AccountDisplay SPAmountText,ds.SPID,ds.SBoxName,dp.* from tblDishPackage dp inner join tblDishSetTopBox ds on ds.ID=dp.STBID inner join OperatorCode oc on oc.Id=ds.SPID  where 1=1 and stbid='" + objS.Settopboxid + "' and dp.id='"+objS.packageid+"' order by dp.PackageName";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getCommissionDTH(clsdthsuscription objS)
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
                s2 = "select atrate,case when ComType='C' then 0 else 1 end as ComType,case when AmtType='V' then 0 else 1 end  as AmtType from DTH_CommissionModule1 with(nolock) where PackageID='"+objS.packageid+"'";
              
                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        	
        public DataTable insertDTHBooking(clsdthsuscription objS)
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
                s2 = "proc_DTHConnectionService";
                SqlParameter[] parameter = {              
                    new SqlParameter("@UserID",objS.entryuser), 
                    new SqlParameter("@PackageID",objS.packageid), 
                    new SqlParameter("@FullName",objS.name), 
                     new SqlParameter("@MobileNo",objS.mobileno),
                          new SqlParameter("@InstallationAddress",objS.address), 
                               new SqlParameter("@PinCode",objS.pincode), 
                        new SqlParameter("@RequestMode","PANEL"), 
                                 new SqlParameter("@IP",objS.ipaddress), 
                        new SqlParameter("@APIRequestID",objS.entryuser), 
                            new SqlParameter("@PID",""), 
                             new SqlParameter("@State",objS.state),
                              new SqlParameter("@City",objS.City),
                 new SqlParameter("@District",objS.District),
                              new SqlParameter("@LandMark",objS.LandMark),
                  
                };
                Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable getDTHBookingdetail(clsdthsuscription objS)
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
                s2 = "SELECT " + objS.noofrow + " TD.dispute,TD.TransactionID,TD.Entrydate,TD.Userid,TD.Amount,TD.AmountTransferto CustomerMobile,TD.RequestedAmount AS PackRate,TD.LiveID,TD.Remark,TD.Type AS status, dp.PackageName,stb.SBoxName,oc.Operator,TD.Optional2 CustomerName,TD.Optional1 AS Address ,TD.Optional3 PinCode,";
                s2+= " (SELECT TOP 1 st.statename FROM Log_proc_DTHConnectionService ttt INNER JOIN statemaster st ON ttt.State=st.StateId WHERE PackageID=dp.ID AND InstallationAddress=TD.Optional1 AND Pincode=TD.Optional3 AND FullName=TD.Optional2) AS State,";
                s2+= " (SELECT TOP 1 st.cityname FROM Log_proc_DTHConnectionService ttt INNER JOIN CityMaster st ON ttt.City=st.cityid WHERE PackageID=dp.ID AND InstallationAddress=TD.Optional1 AND Pincode=TD.Optional3 AND FullName=TD.Optional2) AS City,";
                s2+= " (SELECT TOP 1 District FROM Log_proc_DTHConnectionService ttt WHERE PackageID=dp.ID AND InstallationAddress=TD.Optional1 AND Pincode=TD.Optional3 AND FullName=TD.Optional2) AS District, ";
                s2+= " (SELECT TOP 1 LandMark FROM Log_proc_DTHConnectionService ttt WHERE PackageID=dp.ID AND InstallationAddress=TD.Optional1 AND Pincode=TD.Optional3 AND FullName=TD.Optional2) AS LandMark   FROM tbl_SubscriptionTransactions TD  inner join tblDishPackage dp on dp.ID=TD.OptionalInt1 inner join tblDishSetTopBox stb on stb.ID=dp.STBID	 inner join OperatorCode oc on oc.Id = stb.SPID";

                if (objS.FRomdate != string.Empty)
                {
                    s2 += " and Cast(TD.entrydate as date)>=Cast('" + objS.FRomdate + "' as date)";
                }
                if (objS.Todate != string.Empty)
                {
                    s2 += " and Cast(TD.entrydate as date)<=Cast('" + objS.Todate + "' as date)";
                }
                if (objS.entryuser != string.Empty)
                {
                    s2 += " and TD.Userid='" + objS.entryuser + "'";
                }
                if (objS.ServiceProviderId != string.Empty)
                {
                    s2 += " and oc.Id='" + objS.ServiceProviderId + "'";
                }
                if (objS.status != string.Empty)
                {
                    s2 += " and TD.type='" + objS.status + "'";
                }
                if (objS.mobileno != string.Empty)
                {
                    s2 += " and TD.AmountTransferto='" + objS.mobileno + "'";
                }


                s2 += " order by TD.Entrydate desc";
                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }
        public DataTable UpdateRechargeStatusManual(string refId, string status, string entryby,string LiveId)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateStatusManualDTH";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ReferenceId", refId),
					new SqlParameter("@status", status),
					new SqlParameter("@EntryBy", entryby),
                    new SqlParameter("@Liveid", LiveId)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_5F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable getserviceproviderAll(clsdthsuscription objS)
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
                s2 = "SELECT * FROM OperatorCode WHERE Type=2 order by id";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }

        public int updateserviceproviderstatus(ArrayList list,int status)
        {

            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            int c = 0;
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                int index = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    s2 = "Update OperatorCode Set ActiveStatus='" + status + "' WHERE Type=2 and id='" + list[i].ToString() + "' ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    index = index + 1;
                }

                if (index == list.Count)
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

        public DataTable getActivepackagebyid(clsdthsuscription objS)
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
                s2 = "select oc.Operator ServiceProvider,oc.CustomerDisplay SPHint,oc.AccountDisplay SPAmountText,ds.SPID,ds.SBoxName,dp.* from tblDishPackage dp inner join tblDishSetTopBox ds on ds.ID=dp.STBID inner join OperatorCode oc on oc.Id=ds.SPID  where 1=1 and stbid='" + objS.Settopboxid + "' and isnull(dp.ActiveStatus,1)=1 order by dp.PackageName";

                Dt = ObjData.RunSelectQueryTTrans(s2, tr);

                tr.Commit();

            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return Dt;
        }


    }
}

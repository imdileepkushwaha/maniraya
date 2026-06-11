using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;

namespace BusinessLogicTier
{
    public class clsState
    {
        Data ObjData = new Data();
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string CountryCode { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string MentionBy { get; set; }
        public string TehsilId { get; set; }
        public string TehsilName { get; set; }
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public DataTable getCountry()
        {
            string str_query = "select * from CountryMaster order by CountryName";

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

        public DataTable getbank()
        {
            string str_query = "select * from bankmaster order by BankName";

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

        public DataTable getNumberList()
        {
            string str_query = "select * from NumberList order by Operator";

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
        public DataTable getCircle()
        {
            string str_query = "SELECT Circle,Ltrim(Rtrim(IReffCircle)) as IReffCircle FROM CIRCLEMASTER";

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
        public DataTable getStateAll()
        {
            string str_query = "select sm.*,cm.countryname,cm.countrycode from StateMaster sm left join countrymaster cm on sm.countryid=cm.countryid  order by StateName,cm.countryname";

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
        public DataTable getState(clsState objstate)
        {
            string str_query = "select sm.*,cm.countryname,cm.countrycode from StateMaster sm left join countrymaster cm on sm.countryid=cm.countryid where sm.countryid=" + objstate.CountryId + " order by StateName,cm.countryname";

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

         public DataTable getPaymentMode()
        {
            string str_query = "select * from Master_PaymentMode where status=1 order by id";

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

        

        public DataTable getCityAll()
        {
            string str_query = "select cm.*, sm.statename,cm2.countryname,cm2.countrycode from citymaster cm  left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid   order by sm.statename,cm.cityname,cm2.countryname";

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
        public DataTable getTehsilAll()
        {
            string str_query = "select tt.*, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from tehsilmaster tt left join  citymaster cm on tt.cityid=cm.cityid  left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid   order by sm.statename,cm.cityname,cm2.countryname";

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
        public DataTable getMarketAll()
        {
            string str_query = "select mm.*,tt.Tehsilname, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from  marketmaster mm left join  tehsilmaster tt   on mm.tehsilid=tt.tehsilid left join  citymaster cm on tt.cityid=cm.cityid  left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid   order by sm.statename,cm.cityname,cm2.countryname";

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
        public DataTable getMarket(clsState objstate)
        {
            string str_query = "select mm.*,tt.Tehsilname, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from  marketmaster mm left join  tehsilmaster tt   on mm.tehsilid=tt.tehsilid left join  citymaster cm on tt.cityid=cm.cityid  left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid  where mm.tehsilid= '" + objstate.TehsilId + "'  order by sm.statename,cm.cityname,cm2.countryname";

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
        public DataTable getCity(clsState objstate)
        {
            string str_query = "select cm.*, sm.statename from citymaster cm  left join statemaster sm on cm.stateid=sm.stateid where cm.StateId= '" + objstate.StateId + "'  order by sm.statename,cm.cityname";

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
        public DataTable getTehsil(clsState objstate)
        {
            string str_query = "select tt.*, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from tehsilmaster tt left join  citymaster cm on tt.cityid=cm.cityid  left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid where tt.cityid= '" + objstate.CityId + "'  order by sm.statename,cm.cityname";

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
        public DataTable getAreaAll()
        {
            string str_query = "select am.*, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from areamaster am left join  citymaster cm  on cm.cityid=am.cityid left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid   order by cm2.countryname,sm.statename,cm.cityname,am.areaname";

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
        public DataTable getArea(clsState objstate)
        {
            string str_query = "select am.*, cm.cityname, sm.statename,cm2.countryname,cm2.countrycode from areamaster am left join  citymaster cm  on cm.cityid=am.cityid left join statemaster sm on cm.stateid=sm.stateid  left join countrymaster cm2 on sm.countryid=cm2.countryid    where am.cityid= '" + objstate.CityId + "'  order by sm.statename,cm.cityname,am.areaname";

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
        public string Insert_Country(clsState objState)
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
                s2 = "sp_add_CountryMaster";
                SqlParameter[] parameter = {  
                new SqlParameter("@CountryName",objState.CountryName), 
                new SqlParameter("@CountryCode",objState.CountryCode),
                new SqlParameter("@MentionBy",objState.MentionBy)
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
        public string Update_Country(clsState objState)
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
                s2 = "update CountryMaster set countryname='" + objState.CountryName + "',countrycode='" + objState.CountryCode + "' where countryid='" + objState.CountryId + "'";

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
        public string Insert_State(clsState objState)
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
                s2 = "sp_add_State";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@CountryId",objState.CountryId), 
                new SqlParameter("@StateName",objState.StateName), 
                new SqlParameter("@MentionBy",objState.MentionBy)
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

        public string Update_State(clsState objState)
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
                s2 = "update statemaster set statename='"+objState.StateName+"' where stateid='"+objState.StateId+"'";
              
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


        public string Insert_City(clsState objState)
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
                s2 = "sp_add_City";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@StateId",objState.StateId), 
                new SqlParameter("@CityName",objState.CityName), 
                new SqlParameter("@MentionBy",objState.MentionBy)
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
        public string Insert_Tehsil(clsState objState)
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
                s2 = "sp_add_Tehsil";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@CityId",objState.CityId), 
                new SqlParameter("@TehsilName",objState.TehsilName), 
                new SqlParameter("@MentionBy",objState.MentionBy)
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
        public string Insert_Market(clsState objState)
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
                s2 = "sp_add_Market";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@TehsilId",objState.TehsilId), 
                new SqlParameter("@MarketName",objState.MarketName), 
                new SqlParameter("@MentionBy",objState.MentionBy)
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
        public string Update_City(clsState objState)
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
                s2 = "update citymaster set cityname='" + objState.CityName + "' where cityid='" + objState.CityId + "'";

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
        public string Update_Tehsil(clsState objState)
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
                s2 = "update TehsilMaster set TehsilName='" + objState.TehsilName + "' where TehsilId='" + objState.TehsilId + "'";

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
        public string Update_Market(clsState objState)
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
                s2 = "update MarketMaster set Marketname='" + objState.MarketName + "' where marketId='" + objState.MarketId + "'";

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
        public string Insert_Area(clsState objState)
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
                s2 = "sp_add_AreaMaster";
                SqlParameter[] parameter = {                                              
                new SqlParameter("@CityId",objState.CityId), 
                new SqlParameter("@AreaName",objState.AreaName), 
                new SqlParameter("@Pincode",objState.Pincode), 
                new SqlParameter("@MentionBy",objState.MentionBy)
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
        public string Update_Area(clsState objState)
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
                s2 = "update areamaster set areaname='" + objState.AreaName + "',pincode='"+objState.Pincode+"' where areaid='" + objState.AreaId + "'";
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
        public string Insert_NumberList(string Number,string Operator,string operatorid,string circle,string circlecode,string id,string type)
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
                s2 = "sp_add_NumberList";
                SqlParameter[] parameter = { 
                                                  new SqlParameter("@Number",Number), 
                new SqlParameter("@Operator",Operator), 
                new SqlParameter("@Circle",circle),
                new SqlParameter("@CorcleCode",circlecode),
                new SqlParameter("@Operatorid",operatorid),
                new SqlParameter("@id",id),
                 new SqlParameter("@type",type)
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

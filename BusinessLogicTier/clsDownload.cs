using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataTier;

namespace BusinessLogicTier
{
    public class clsDownload
    {
        Data ObjData = new Data();

        public string ToId { get; set; }
        public string DownloadContent { get; set; }
        public string EntryBy { get; set; }

        public DataTable getUserDetail(clsDownload objclsDownload)
        {
            string str_query = "select * from Userdetail where UserId='" + objclsDownload.ToId+"'";
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

        public int InsertDownload(clsDownload objclsDownload)
        {
            int i = 0;
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "INSERT INTO tbl_Download(ToID,DownloadContent,EntryBy,EntryDate)VALUES('" + objclsDownload.ToId.ToString() + "','" + objclsDownload.DownloadContent.ToString() + "','" + objclsDownload.EntryBy.ToString() + "',getdate())";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                i = 1;
                tr.Commit();
            }
            catch (Exception ex)
            {
                i = 0;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return i;
        }

        public DataTable getDownloadDetail(clsDownload objclsDownload)
        {
            string str_query = "select DownloadContent,EntryBy,convert(nvarchar(50),EntryDate,106) as EntryDate from tbl_download where ToID='" + objclsDownload.ToId + "'";
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
        public DataTable franchseecommission()
        {
            string str_query = "select * from FranchiseeCommission";
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
        public DataTable Deductioncommission()
        {
            string str_query = "select * from tbl_Deduction";
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
        public DataTable Dollarcharge()
        {
            string str_query = "select * from INRConvertorMaster";
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

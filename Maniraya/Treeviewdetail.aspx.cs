using BusinessLogicTier;
using DataTier;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Treeviewdetail : System.Web.UI.Page
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string parentId = "MP000001";

            if (Request.QueryString["SuperId"] != null)
            {
                parentId = Request.QueryString["SuperId"].ToString();
            }

            // Pass to JS or use directly
            hiddenParentId.Value = parentId.ToString();
        }
    }

    public class User
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string parentid { get; set; }
        public string status { get; set; }
        public string photoimage { get; set; }

        public string topupamount { get; set; }
    }
    [WebMethod]
    public static List<User> GetChildren(string parentId)
    {
        List<User> list = new List<User>();
        string connStr = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT userid, username, parentuserid AS parentid, CASE WHEN ISNULL(SavingStatus,0)=1 THEN 'Active' ELSE 'Inactive' END AS status, CASE WHEN ISNULL(Savingstatus,0)=1 THEN 'user/img/green-boy.png' ELSE 'user/img/red-boy.png' END AS photoimage, (SELECT ISNULL(SUM(amount),0) FROM SavingAccountDetail WHERE Userid=u.userid and Status='Approved') AS topupamount FROM UserDetail u WHERE u.sponserid=@parentid;";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@parentid", parentId);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new User
                {
                    userid = dr["userid"].ToString(),
                    username = dr["username"].ToString(),
                    parentid = dr["parentid"].ToString(),
                    status = dr["status"].ToString(),
                    photoimage = dr["photoimage"].ToString(),
                    topupamount = dr["topupamount"].ToString()
                });
            }
        }
        return list;
    }

}
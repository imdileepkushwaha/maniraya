using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AdminMenuPermission : System.Web.UI.Page
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
               
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }


    public class Menu
    {
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public List<ListMenu> MenuId { get; set; }
        public bool Checked { get; set; }
    }

    public class ListMenu
    {
        public int Menu { get; set; }
        public string MenuName { get; set; }
        public bool Checked { get; set; }
    }
    public class Foo
    {
        Data ObjData = new Data();
        public DataSet GetMenuPermission(string Userid)
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
                s2 = "GetAdminPermissionMenu";
                SqlParameter[] parameter = {              
                    new SqlParameter("@userid",Userid)
                };
                ds = ObjData.RunDataSetProcedureTRans(s2, tr, parameter);

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
            return ds;
        }
        public DataTable UpdateMenuPermission(string Userid, DataTable MainMenu, DataTable SubMenu)
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
                s2 = "UpdateAdminPermissionMenu";
                SqlParameter[] parameter = {              
                    new SqlParameter("@userid",Userid),
                     new SqlParameter("@tmainmenu",MainMenu),
                      new SqlParameter("@tmenu",SubMenu)
                };
                ds = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

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
            return ds;
        }
    }
    [WebMethod]
    public static List<Menu> GetUserMenu(string user)
    {

        Foo objUser = new Foo();
        DataSet ds = objUser.GetMenuPermission(user);

        if (ds != null)
        {
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            List<Menu> list = new List<Menu>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Menu item = new Menu();
                item.MainMenuId = Convert.ToInt32(dt1.Rows[i]["id"].ToString());
                item.Checked = (Convert.ToInt32(dt1.Rows[i]["Checked"].ToString()) == 1 ? true : false);
                item.MainMenuName = dt1.Rows[i]["MainMenuName"].ToString();
                DataRow[] dr = dt2.Select(" MainMenuId=" + item.MainMenuId + " ");
                 List<ListMenu> slist = new List<ListMenu>();
                 if (dr != null && dr.Count() > 0)
                 {

                     DataTable dtsub = dr.CopyToDataTable();


                     for (int j = 0; j < dtsub.Rows.Count; j++)
                     {
                         ListMenu subitem = new ListMenu();
                         subitem.Menu = Convert.ToInt32(dtsub.Rows[j]["id"].ToString());
                         subitem.Checked = (Convert.ToInt32(dtsub.Rows[j]["Checked"].ToString()) == 1 ? true : false);
                         subitem.MenuName = dtsub.Rows[j]["MenuName"].ToString();
                         slist.Add(subitem);
                     }

                 }
                item.MenuId = slist;

                list.Add(item);
            }

            return list;

        }
        else
        {
            return null;
        }

    }


    public class SubMenu
    {
        public int M {get;set;}
        public int S {get;set;}
    }

    public class DT
    {
        public string user {get;set;} 
        public List<int> MainMenu{get;set;}
        public List<SubMenu> SubMenu { get; set; }
    }

  
    [WebMethod]
    public static int UpdateUserMenu(DT Data)
    {
        clsUser objUser = new clsUser();

        DataTable dtmain = new DataTable();
        dtmain.Columns.Add("MainMenuId");

        foreach (var item in Data.MainMenu)
        {
            DataRow dr = dtmain.NewRow();
            dr["MainMenuId"] = item;
            dtmain.Rows.Add(dr);
        }


        DataTable dtsub = new DataTable();
        dtsub.Columns.Add("MainMenuId");
        dtsub.Columns.Add("MenuId");

        foreach (var item in Data.SubMenu)
        {
            DataRow dr = dtsub.NewRow();
            dr["MainMenuId"] = item.M;
            dr["MenuId"] = item.S;
            dtsub.Rows.Add(dr);
        }

        Foo objUserr = new Foo();
        DataTable dt = objUserr.UpdateMenuPermission(Data.user, dtmain, dtsub);
        if (dt != null && dt.Rows.Count > 0)
        {
            return Convert.ToInt16(dt.Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
        
    }
    


}

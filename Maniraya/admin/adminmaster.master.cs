using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_MasterPage : System.Web.UI.MasterPage
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["useradmin"] != null)
            //{
            //    LblUsernameSideMenu.Text = Session["useradmin"].ToString();
            //    LblMainId.Text = Session["useradmin"].ToString();
            //    //LblFullname.Text = Session["useradmin"].ToString();
            //}
            if (Session["useradmin"] != null)
            {
                //LblUsernameSideMenu.Text = Session["useradmin"].ToString();
                LblMainId.Text = Session["useradmin"].ToString();
                //LblFullname.Text = Session["useradmin"].ToString();
                if (Session["role"].ToString() == "Subadmin")
                {
                    BindSubMenu(Session["useradmin"].ToString());
                }
                else
                {
                    paneladmin2.Visible = true;
                    panelsubadmin2.Visible = false;
                }
            }
        }
    }
    public void BindSubMenu(string userid)
    {
       
        DataSet ds = GetMenuPermission(userid);

        string html = "";

        html += "<ul class='nav navbar-nav'>";

        html += "<li class='active'><a href='Dashboard.aspx'>Dashboard</a></li>";

        if (ds != null)
        {
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            List<Menu> list = new List<Menu>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                int ch = Convert.ToInt32(dt1.Rows[i]["Checked"].ToString());
                if (ch == 1)
                {

                    html += "<li class='dropdown dropdown-large'>";
                    html += "<a href='" + dt1.Rows[i]["Url"].ToString() + "' class='dropdown-toggle' data-toggle='dropdown'>" + dt1.Rows[i]["MainMenuName"].ToString() + " <b class='caret'></b></a>";


                    DataRow[] dr = dt2.Select(" MainMenuId=" + Convert.ToInt32(dt1.Rows[i]["id"].ToString()) + " ");


                    if (dr != null && dr.Count() > 0)
                    {
                        DataTable dtsub = dr.CopyToDataTable();



                        html += "<ul class='dropdown-menu dropdown-menu-large row'>";
                        html += "<li class='col-sm-12'><ul>";
                        for (int j = 0; j < dtsub.Rows.Count; j++)
                        {
                            ch = Convert.ToInt32(dtsub.Rows[j]["Checked"].ToString());

                            if (ch == 1)
                            {
                                html += " <li><a href='" + dtsub.Rows[j]["Url"].ToString() + "'>" + dtsub.Rows[j]["MenuName"].ToString() + "</a></li>";
                            }

                        }
                        html += "</ul></li>";
                        html += "</ul>";
                    }


                    html += "</li>";

                }
            }
            html += "</ul>";


            Literal1.Text = html;
            paneladmin2.Visible = false;
            panelsubadmin2.Visible = true;



        }






    }
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
}

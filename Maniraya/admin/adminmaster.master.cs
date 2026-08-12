using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_MasterPage : System.Web.UI.MasterPage
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            return;
        }

        string userId = Session["useradmin"].ToString();
        string role = Session["role"] != null ? Session["role"].ToString() : "Administrator";

        LblUsernameSideMenu.Text = userId;
        LblMainId.Text = userId;
        LblTopbarGreet.Text = userId;
        LblUserMenuName.Text = userId;
        LblUserRole.Text = role;
        LblUserMenuRole.Text = role;

        // SubAdmin: block direct URL access to unassigned pages (every request)
        AdminAccessHelper.EnforceOrRedirect();

        if (!IsPostBack)
        {
            BindNotifications(userId);

            if (string.Equals(role, "Subadmin", StringComparison.OrdinalIgnoreCase))
            {
                BindSubMenu(userId);
            }
            else
            {
                paneladmin2.Visible = true;
                panelsubadmin2.Visible = false;
            }
        }
        else if (string.Equals(role, "Subadmin", StringComparison.OrdinalIgnoreCase))
        {
            paneladmin2.Visible = false;
            panelsubadmin2.Visible = true;
        }
    }
    public void BindNotifications(string userId)
    {
        clsSupport support = new clsSupport();
        support.ToId = userId;
        DataTable dt = support.getInbox(support);

        int count = dt != null ? dt.Rows.Count : 0;
        pnlNotifyBadge.Visible = count > 0;
        LitNotifyCount.Text = count > 99 ? "99+" : count.ToString();

        StringBuilder html = new StringBuilder();
        if (dt != null && dt.Rows.Count > 0)
        {
            int max = Math.Min(5, dt.Rows.Count);
            for (int i = 0; i < max; i++)
            {
                string title = HttpUtility.HtmlEncode(dt.Rows[i]["MessageTitle"].ToString());
                string fromId = HttpUtility.HtmlEncode(dt.Rows[i]["FromId"].ToString());
                string dateText = "";

                if (dt.Rows[i]["mentiondate"] != DBNull.Value)
                {
                    dateText = Convert.ToDateTime(dt.Rows[i]["mentiondate"]).ToString("dd MMM, hh:mm tt");
                }

                html.Append("<a href='InboxAdmin.aspx' class='admin-notify-item'>");
                html.Append("<span class='admin-notify-item-icon'><i class='fa fa-envelope-o'></i></span>");
                html.Append("<span class='admin-notify-item-copy'>");
                html.Append("<strong>" + title + "</strong>");
                html.Append("<small>From " + fromId + (string.IsNullOrEmpty(dateText) ? "" : " · " + dateText) + "</small>");
                html.Append("</span>");
                html.Append("</a>");
            }
        }
        else
        {
            html.Append("<div class='admin-notify-empty'>");
            html.Append("<i class='fa fa-bell-slash-o'></i>");
            html.Append("<p>No new messages</p>");
            html.Append("</div>");
        }

        LitNotifyList.Text = html.ToString();
    }

    public void BindSubMenu(string userid)
    {
        DataSet ds = GetMenuPermission(userid);

        StringBuilder html = new StringBuilder();
        html.Append("<ul class='admin-side-menu'>");
        html.Append("<li class='admin-side-section'>Main</li>");
        html.Append("<li><a href='Dashboard.aspx' class='admin-side-link'><i class='fa fa-tachometer admin-side-icon'></i> Dashboard</a></li>");
        html.Append("<li class='admin-side-section'>Modules</li>");

        if (ds != null && ds.Tables.Count > 1)
        {
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                int ch = Convert.ToInt32(dt1.Rows[i]["Checked"].ToString());
                if (ch != 1) continue;

                DataRow[] dr = dt2.Select("MainMenuId=" + Convert.ToInt32(dt1.Rows[i]["id"].ToString()));
                if (dr == null || dr.Length == 0) continue;

                StringBuilder subHtml = new StringBuilder();
                for (int j = 0; j < dr.Length; j++)
                {
                    if (Convert.ToInt32(dr[j]["Checked"].ToString()) != 1) continue;

                    string url = Convert.ToString(dr[j]["Url"]);
                    string urlLower = (url ?? string.Empty).Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(urlLower)) continue;
                    if (urlLower == "subadmin.aspx" || urlLower == "subadminreport.aspx" || urlLower == "adminmenupermission.aspx")
                    {
                        continue;
                    }

                    subHtml.Append("<li><a href='")
                        .Append(HttpUtility.HtmlAttributeEncode(url))
                        .Append("'>")
                        .Append(HttpUtility.HtmlEncode(Convert.ToString(dr[j]["MenuName"])))
                        .Append("</a></li>");
                }

                if (subHtml.Length == 0) continue;

                string mainMenuName = Convert.ToString(dt1.Rows[i]["MainMenuName"]);
                html.Append("<li class='admin-side-group'>");
                html.Append("<button type='button' class='admin-side-toggle'><i class='fa fa-folder-o admin-side-icon'></i> ")
                    .Append(HttpUtility.HtmlEncode(mainMenuName))
                    .Append(" <i class='fa fa-chevron-down admin-side-chevron'></i></button>");
                html.Append("<ul class='admin-side-submenu'>");
                html.Append(subHtml.ToString());
                html.Append("</ul></li>");
            }
        }

        html.Append("</ul>");
        Literal1.Text = html.ToString();
        paneladmin2.Visible = false;
        panelsubadmin2.Visible = true;
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

using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class admin_MemberLoginPanel : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindMembers();
        }
    }

    void BindMembers()
    {
        DataTable dt = GetMemberLoginPanelData();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    DataTable GetMemberLoginPanelData()
    {
        string sql = @"SELECT
                ud.UserId,
                ud.UserName,
                ud.Mobile,
                CASE WHEN ISNULL(ud.Status, 0) = 1 THEN 'Active' ELSE 'Deactive' END AS status,
                ISNULL(ud.PhotoImage, '') AS UserImage
            FROM UserDetail ud WITH (NOLOCK)
            WHERE 1 = 1";

        if (!string.IsNullOrWhiteSpace(txtsearch.Text))
        {
            sql += " AND ud.UserId LIKE '%" + SqlEscape(txtsearch.Text.Trim()) + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtusername.Text))
        {
            sql += " AND ud.UserName LIKE '%" + SqlEscape(txtusername.Text.Trim()) + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtmobile.Text))
        {
            sql += " AND ud.Mobile LIKE '%" + SqlEscape(txtmobile.Text.Trim()) + "%'";
        }

        if (ddlststatus.SelectedValue != "-1")
        {
            sql += " AND ISNULL(ud.Status, 0) = " + ddlststatus.SelectedValue;
        }

        sql += " ORDER BY ud.UserName, ud.UserId";

        DataTable dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            DataTable result = ObjData.RunDataTable(sql);
            if (result != null)
            {
                dt = result;
            }
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblStatus = (Label)e.Row.FindControl("lblStatus");
        if (lblStatus == null)
        {
            return;
        }

        if (string.Equals(lblStatus.Text, "Active", StringComparison.OrdinalIgnoreCase))
        {
            lblStatus.CssClass = "label label-success";
        }
        else if (string.Equals(lblStatus.Text, "Deactive", StringComparison.OrdinalIgnoreCase))
        {
            lblStatus.CssClass = "label label-danger";
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)GridView1.Rows[index].FindControl("lblUserId");
            Label lblusernm = (Label)GridView1.Rows[index].FindControl("lblUserName");
            Label lbluserimg = (Label)GridView1.Rows[index].FindControl("LblUserImage");
            Label lblStats = (Label)GridView1.Rows[index].FindControl("lblStatus");
            Session["userid"] = lbluser.Text;
            Session["username"] = lblusernm.Text;
            Session["UserImage"] = lbluserimg.Text;
            Session["status"] = lblStats.Text;
            Response.Redirect("../user/Dashboard.aspx");
        }
    }

    protected void btnfet_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        BindMembers();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindMembers();
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

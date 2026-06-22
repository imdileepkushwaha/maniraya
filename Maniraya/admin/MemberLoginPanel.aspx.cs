using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataTier;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class admin_MemberLoginPanel : System.Web.UI.Page
{
    Data ObjData = new Data();
     clsUser objuser = new clsUser();
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
               
                loadcity();
             
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
  
    void loadcity()
    {
        DataSet dt = new DataSet();
        dt = getMemberLoginPanel();
        GridView1.DataSource = dt;
        GridView1.DataBind();


    }




    public DataSet getMemberLoginPanel()
        {
            return DBHelper.ExecuteQuery("getMemberLoginPanel");
        }

        public DataSet FetchInGdvwByUserId(clsUser fetch)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@UserId", string.IsNullOrWhiteSpace(fetch.UserId) ? (object)DBNull.Value : fetch.UserId),
                                  new SqlParameter("@UserName", string.IsNullOrWhiteSpace(fetch.UserName) ? (object)DBNull.Value : fetch.UserName)
                                  };
            return DBHelper.ExecuteQuery("getMemberLoginPanelFetch", para);
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
            Session["userid"] = lbluser.Text.ToString();
            Session["username"] = lblusernm.Text.ToString();
            Session["UserImage"] = lbluserimg.Text.ToString();
            Session["status"] = lblStats.Text.ToString();
            Response.Redirect("../user/Dashboard.aspx");
        }
    }
    public void FetchInGdVw()
    {
        objuser.UserId = !string.IsNullOrWhiteSpace(txtsearch.Text) ? txtsearch.Text.Trim() : null;
        objuser.UserName = !string.IsNullOrWhiteSpace(txtusername.Text) ? txtusername.Text.Trim() : null;

        GridView1.DataSource = FetchInGdvwByUserId(objuser);
        GridView1.DataBind();
    }
    protected void btnfet_Click(object sender, EventArgs e)
    {
        FetchInGdVw();
        //Response.Redirect("MemberLoginPanel.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FetchInGdVw();
    }
}
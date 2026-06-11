using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_businessSummary : System.Web.UI.Page
{
    DataTable dt;
    clsUser objUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getBusinessSummary();
        }
    }

    protected void getBusinessSummary()
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            objUser.UserId = txtUserId.Text;

            dt = objUser.getBusinessSummary(objUser, txtFromDate.Text, txtToDate.Text);

            if (dt.Rows.Count > 0)
                GridView1.DataSource = dt;
            else
                GridView1.EmptyDataText = "No Record(s) Found...";

            GridView1.DataBind();
        }
        catch(Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblUserMobile = e.Row.FindControl("lblUserMobile") as Label;
                GridView GridView2 = e.Row.FindControl("GridView2") as GridView;

                objUser.UserId = lblUserMobile.Text;

                dt = objUser.getBusinessSummaryPerUser(objUser, txtFromDate.Text, txtToDate.Text);

                if (dt.Rows.Count > 0)
                    GridView2.DataSource = dt;
                else
                    GridView2.EmptyDataText = "No Record(s) Found...";

                GridView2.DataBind();
            }
        }
        catch(Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getBusinessSummary();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Path);
    }
}
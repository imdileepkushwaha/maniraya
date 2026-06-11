using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_PoolReport : System.Web.UI.Page
{
    clsClosing objaccount = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                TxtUserId.Text = Session["userid"].ToString();
                TxtUserId.Enabled = false;
                // loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void FillPoolID(string userid)
    {
        
        DataTable dt = new DataTable();
        dt = objaccount.getPoolID(TxtUserId.Text,DDlstpoolNo.SelectedValue);
        GrdPoolId.DataSource = dt;
        GrdPoolId.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillPoolID(TxtUserId.Text);
    }
    void FillPoolReport(string Poolid)
    {

        DataTable dt = new DataTable();
        dt = objaccount.getUserPoolReport(Poolid, DDlstpoolNo.SelectedValue);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblGroupid = (Label)GrdPoolId.Rows[index].FindControl("lblGroupid");
            FillPoolReport(lblGroupid.Text);


        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
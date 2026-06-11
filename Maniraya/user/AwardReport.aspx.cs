using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class AwardReport : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsaward objAward = new clsaward();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            loadaward();
        }
    }

    void loadaward()
    {
        DataTable dt = new DataTable();
        dt = objAward.getawardDetailfromdashboard();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label Id = e.Row.FindControl("lblid") as Label;
            Label lbltargetleft = e.Row.FindControl("lbltargetleft") as Label;
            Label lbltargetright = e.Row.FindControl("lbltargetright") as Label;
            Label lblCurrentLeftBv = e.Row.FindControl("lblCurrentLeftBv") as Label;
            Label lblCurrentRightBv = e.Row.FindControl("lblCurrentRightBv") as Label;
            Label lblrequiredLeftBv = e.Row.FindControl("lblrequiredLeftBv") as Label;
            Label lblrequiredRightBv = e.Row.FindControl("lblrequiredRightBv") as Label;
            Label lblstatus = e.Row.FindControl("lblstatus") as Label;
            string UserId = Session["userid"].ToString();
            DataTable Dt = objuser.getawardindashboar(UserId, Id.Text);
            if (Dt.Rows.Count > 0)
            {
                lblCurrentLeftBv.Text = Dt.Rows[0]["leftbv"].ToString();
                lblCurrentRightBv.Text = Dt.Rows[0]["RightBv"].ToString();
                Decimal I = Convert.ToDecimal(lbltargetleft.Text) - Convert.ToDecimal(lblCurrentLeftBv.Text);
                Decimal K = Convert.ToDecimal(lbltargetright.Text) - Convert.ToDecimal(lblCurrentRightBv.Text);
                if (I > 0)
                {
                    lblrequiredLeftBv.Text = I.ToString();
                }
                else
                {
                    lblrequiredLeftBv.Text = "0";
                }
                if (K > 0)
                {
                    lblrequiredRightBv.Text = K.ToString();
                }
                else
                {
                    lblrequiredRightBv.Text = "0";
                }
                if (I <= 0 && K <= 0)
                {
                    lblstatus.Text = "Achieved";
                }
                else
                {
                    lblstatus.Text = "Due";
                }
            }
        }
    }
}
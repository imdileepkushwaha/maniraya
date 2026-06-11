using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class UserReferalPvReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();
    decimal amount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;

            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }




    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GV1_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            amount = 0;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amount = amount + Convert.ToDecimal(((Label)e.Row.FindControl("lblbv")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total : ";
            e.Row.Cells[2].Text = amount.ToString();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {

        if (txtfromdate.Text != "")
        {
            objP.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objP.Fromdate = DateTime.Parse("01/01/1900");
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.Now.AddDays(1);
        }
        if (txtuserid.Text.Trim() != "")
        {
            objP.FranchiseeID = txtuserid.Text;
        }
        else
        {
            objP.FranchiseeID = null;
        }
        if (TxtLevel.Text != string.Empty)
        {
            objP.Level = TxtLevel.Text;
        }
        objP.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();

        dt = objP.Get_UserReferalBV(objP);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }


 




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class DailyLevelReport : System.Web.UI.Page
{
    clsUser objclsUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            FillAssociatesDetails();
        }
    }

    public void FillAssociatesDetails()
    {
        DataTable dt = new DataTable();
        objclsUser.UserId = Session["userid"].ToString();
        string Rowno = "";
        if (ddlRecordFilter.SelectedValue == "10")
        {
            Rowno = " top 10 ";
        }
        if (ddlRecordFilter.SelectedValue == "25")
        {
            Rowno = " top 25 ";
        }
        if (ddlRecordFilter.SelectedValue == "50")
        {
            Rowno = " top 50 ";
        }
        if (ddlRecordFilter.SelectedValue == "100")
        {
            Rowno = " top 100 ";
        }
        if (ddlRecordFilter.SelectedValue == "500")
        {
            Rowno = " top 500 ";
        }
        objclsUser.Pincode = Rowno;
        dt = objclsUser.GetDailyLevelReport(objclsUser);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }
    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAssociatesDetails();
    }
}
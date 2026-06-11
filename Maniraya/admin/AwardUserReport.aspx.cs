using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class AwardUserReport : System.Web.UI.Page
{
    clsUser objclsUser = new clsUser();
    clsClosing objCl = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
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
        //objCl.GenerateUserId = Session["userid"].ToString();
        string Userid=txtuserid.Text;
        dt = objCl.getUserRewardReportadmin(Userid);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }
    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView drv = e.Row.DataItem as DataRowView;
        //    if (e.Row.Cells[5].Text.ToUpper()=="YES")
        //    {
        //        e.Row.BackColor = System.Drawing.Color.LightGreen;
        //       // e.Row.ForeColor = System.Drawing.Color.White;
        //    }
        //    //else
        //    //{
        //    //    e.Row.BackColor = System.Drawing.Color.Green;
        //    //}
        //}
    }
}
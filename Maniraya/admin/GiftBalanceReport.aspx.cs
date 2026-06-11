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

public partial class GiftBalanceReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
              //  loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

  

    //public void FillAssociatesDetails()
    //{
    //    DataTable dt = new DataTable();
    //    objclsUser.UserId = Session["useradmin"].ToString();
    //    string Rowno = "";
    //    if (ddlRecordFilter.SelectedValue == "10")
    //    {
    //        Rowno = " top 10 ";
    //    }
    //    if (ddlRecordFilter.SelectedValue == "25")
    //    {
    //        Rowno = " top 25 ";
    //    }
    //    if (ddlRecordFilter.SelectedValue == "50")
    //    {
    //        Rowno = " top 50 ";
    //    }
    //    if (ddlRecordFilter.SelectedValue == "100")
    //    {
    //        Rowno = " top 100 ";
    //    }
    //    if (ddlRecordFilter.SelectedValue == "500")
    //    {
    //        Rowno = " top 500 ";
    //    }
    //    objclsUser.Pincode = Rowno;
    //    dt = objclsUser.GetGiftbalanceReport(objclsUser);
    //    grdBank.DataSource = dt;
    //    grdBank.DataBind();
    //}
    //protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillAssociatesDetails();
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {

        if (txtfromdate.Text != "")
        {
            objaccount.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objaccount.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objaccount.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objaccount.ToDate = DateTime.MinValue;
        }
        objaccount.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objaccount.GetGiftbalanceReport(objaccount);
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }

   
}
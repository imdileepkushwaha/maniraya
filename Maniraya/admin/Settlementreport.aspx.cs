using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Globalization;

public partial class Settlementreport : System.Web.UI.Page
{
    clsClosing objA = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                txtfdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Filldashboard();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Filldashboard();
    }

    private void Filldashboard()
    {
        DateTime fdate = DateTime.MinValue;
        DateTime tdate = DateTime.MinValue;
        if (txtfdate.Text != "")
        {
            fdate = Message.GetIndianDate(txtfdate.Text);
        }
        else
        {
            fdate = DateTime.Now;
        }
        if (txttdate.Text != "")
        {
            tdate = Message.GetIndianDate(txttdate.Text);
        }
        else
        {
            tdate = DateTime.Now;
        }
        DataSet Ds = objA.GetSettlementReport(fdate, tdate);
        if (Ds != null)
        {
            //opening balance
            DataTable dt = Ds.Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //closing balance
            DataTable dt2 = Ds.Tables[5];
            GridView2.DataSource = dt2;
            GridView2.DataBind();

            //recharge mainwallet
            DataTable dt3 = Ds.Tables[1];
            GridView3.DataSource = dt3;
            GridView3.DataBind();

            //recharge cashwallet
            DataTable dt4 = Ds.Tables[2];
            GridView4.DataSource = dt4;
            GridView4.DataBind();


            //deposit mainwallet
            DataTable dt5 = Ds.Tables[3];
            GridView5.DataSource = dt5;
            GridView5.DataBind();

            //deposit cashwallet
            DataTable dt6 = Ds.Tables[4];
            GridView6.DataSource = dt6;
            GridView6.DataBind();

            //payout 
            DataTable dt7 = Ds.Tables[6];
            GridView7.DataSource = dt7;
            GridView7.DataBind();

            divdata.Visible = true;
        }
        else
        {
            divdata.Visible = false;
        }
    }
   
   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class UserWallet : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsUser objclsUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            loaduser();
        }
    }
    void loaduser()
    {

        objaccount.UserId = Session["userId"].ToString();
        objaccount.userType = "1";
        DataTable dt = new DataTable();
        dt = objaccount.getUserWalletBalanceReportrechargewallet(objaccount);
        if (dt.Rows.Count > 0)
        {
            LblCredited.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet.Text = dt.Rows[0]["bal"].ToString();
        }

        objaccount.userType = "2";
        dt = new DataTable();
        dt = objaccount.getUserWalletBalanceReportrechargeuntility(objaccount);
        if (dt.Rows.Count > 0)
        {
            LblCredited2.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited2.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet2.Text = dt.Rows[0]["bal"].ToString();
        }

    }

   
}
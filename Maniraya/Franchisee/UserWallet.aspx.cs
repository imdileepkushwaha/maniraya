using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;

public partial class UserWallet : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    clsUser objclsUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserId"] == null)
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

        objaccount.UserId = Session["fuserId"].ToString();
        DataTable dt = new DataTable();
        dt = getUserWalletBalanceReportrechargeuntility(objaccount);
        if (dt.Rows.Count > 0)
        {
            LblCredited.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet.Text = dt.Rows[0]["bal"].ToString();
        }
    }
    public DataTable getUserWalletBalanceReportrechargeuntility(clsAccount objaccount)
    {
        string str_query = "select isnull( sum(cramount),0) as sumCr,isnull( sum(dramount),0) as sumdr,isnull( sum(cramount),0)-isnull( sum(dramount),0) as bal from TransactionDetailfranchisee td where 1=1 and type='2'";

        if (objaccount.UserId != "")
        {
            str_query += "  and td.UserId = '" + objaccount.UserId + "' ";
        }

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }

   
}
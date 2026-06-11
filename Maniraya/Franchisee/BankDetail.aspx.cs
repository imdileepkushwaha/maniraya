using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class user_BankDetail : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            FillBankDetails();
        }
    }

    public void FillBankDetails()
    {
        DataTable dt = new DataTable();
        dt = objbank.getBankAccountList();
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }
}
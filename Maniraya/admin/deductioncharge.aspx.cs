using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class deductioncharge : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsDownload objD = new clsDownload();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objD.Deductioncommission();
        if (dt.Rows.Count == 0)
        {
            return;
        }

        DataRow row = dt.Rows[0];
        hfId.Value = row["id"].ToString();
        TxtAdminCharge.Text = row["admincharge"].ToString();
        TxtTdswithpam.Text = row["tdswithpan"].ToString();
        TxtTdswithoutpan.Text = row["tdswithoutpan"].ToString();
        TxtcashWallet.Text = row["CashWallet"].ToString();
        TxtcashWalletPercentage.Text = row["CashWalletPercent"].ToString();
        TxtCappingAmount.Text = row["CappingAmount"].ToString();
        TxtMinAmt.Text = row["MinDepositAmount"].ToString();
        TxtMaxAmt.Text = row["MaxDepositAmount"].ToString();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objaccount.Updatedeductioncommission(
            hfId.Value,
            TxtAdminCharge.Text,
            TxtTdswithpam.Text,
            TxtTdswithoutpan.Text,
            TxtcashWallet.Text,
            TxtcashWalletPercentage.Text,
            TxtCappingAmount.Text,
            TxtMinAmt.Text,
            TxtMaxAmt.Text);

        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

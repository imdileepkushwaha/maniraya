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
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            TextBox TxtAdminCharge = (TextBox)r.FindControl("TxtAdminCharge");
            TextBox TxtTdswithpam = (TextBox)r.FindControl("TxtTdswithpam");
            TextBox TxtTdswithoutpan = (TextBox)r.FindControl("TxtTdswithoutpan");
            TextBox TxtcashWallet = (TextBox)r.FindControl("TxtcashWallet");
            TextBox TxtcashWalletPercent = (TextBox)r.FindControl("TxtcashWalletPercentage");
            TextBox TxtCappingAmount = (TextBox)r.FindControl("TxtCappingAmount");
            TextBox TxtMinAmt = (TextBox)r.FindControl("TxtMinAmt");
            TextBox TxtMaxAmt = (TextBox)r.FindControl("TxtMaxAmt");

            objaccount.Updatedeductioncommission(lbllevel.Text, TxtAdminCharge.Text, TxtTdswithpam.Text, TxtTdswithoutpan.Text, TxtcashWallet.Text, TxtcashWalletPercent.Text, TxtCappingAmount.Text, TxtMinAmt.Text, TxtMaxAmt.Text);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class WithdrawlSettings : System.Web.UI.Page
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddlCategories = e.Row.FindControl("DDlstStatus") as DropDownList;
            Label LblStatus = e.Row.FindControl("Label1") as Label;
            if (ddlCategories != null)
            {
                //Get the data from DB and bind the dropdownlist
                ddlCategories.SelectedValue = LblStatus.Text;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            TextBox MinCashWithdrawl = (TextBox)r.FindControl("TxtAdminCharge");
            TextBox MaxCashWithdrawl = (TextBox)r.FindControl("TxtTdswithpam");
            DropDownList DDlstStatus = (DropDownList)r.FindControl("DDlstStatus");

            objaccount.Updatewithdrawlsetting(lbllevel.Text, MinCashWithdrawl.Text, MaxCashWithdrawl.Text, DDlstStatus.SelectedValue);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           
    }
}
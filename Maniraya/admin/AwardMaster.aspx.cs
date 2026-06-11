using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class AwardMaster : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
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
        dt = objaccount.getAwardLevel();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lbllevel");
            TextBox txtamount = (TextBox)r.FindControl("txtamount");
            TextBox txttarget = (TextBox)r.FindControl("txttarget");
            objaccount.LevelNo = lbllevel.Text;
            objaccount.ComissionPercent = Convert.ToDecimal(txtamount.Text);
            objaccount.CommitmentAmount = Convert.ToDecimal(txttarget.Text);
            objaccount.UpdateAwardMaster(objaccount);
        }
        
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated Successfully');", true);
           
    }
}
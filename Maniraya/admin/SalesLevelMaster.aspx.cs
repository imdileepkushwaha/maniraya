using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class SalesLevelMaster : System.Web.UI.Page
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
        dt = objaccount.getsalesLevel();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lbllevel");
            TextBox txtrcomission = (TextBox)r.FindControl("txtrcomission");
            TextBox txtdcomission = (TextBox)r.FindControl("txtdcomission");
            objaccount.LevelNo = lbllevel.Text;
            objaccount.ComissionPercent = Convert.ToDecimal(txtrcomission.Text);
            objaccount.CommitmentAmount = Convert.ToDecimal(txtdcomission.Text);
            objaccount.UpdatesalesLevelMaster(objaccount);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           
    }
}
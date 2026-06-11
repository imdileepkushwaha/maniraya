using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class FranchiseeCommission : System.Web.UI.Page
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
        dt = objD.franchseecommission();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            TextBox txtcomission = (TextBox)r.FindControl("txtcomission");
            objaccount.LevelNo = lbllevel.Text;
            objaccount.ComissionPercent = Convert.ToDecimal( txtcomission.Text);
            objaccount.UpdateFranchseeCommission(objaccount);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           
    }
}
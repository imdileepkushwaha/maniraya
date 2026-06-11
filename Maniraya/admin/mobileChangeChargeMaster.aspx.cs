using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class mobileChangeChargeMaster : System.Web.UI.Page
{
    clsUser objUser;
    DataTable dt;

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
        dt = new DataTable();
        objUser = new clsUser();
        dt = objUser.getMobileChangeChargeMaster("Select", "", 0);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        dt = null;
        objUser = null;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();

        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lblCharge_Id = (Label)r.FindControl("lblCharge_Id");
            TextBox txtCharge = (TextBox)r.FindControl("txtCharge");

            try
            {
                Convert.ToDecimal(txtCharge.Text);
            }
            catch
            {
                Message.Show("Enter proper amount...");
                return;
            }

            objUser.getMobileChangeChargeMaster("Update", lblCharge_Id.Text, Convert.ToDecimal(txtCharge.Text));
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

        dt = null;
        objUser = null;
    }
}
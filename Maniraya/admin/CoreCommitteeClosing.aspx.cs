using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_CoreCommitteeClosing : System.Web.UI.Page
{
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            ClearFields();
        }
    }

    public void ClearFields()
    {
        txtamount.Text = "";
        txtshare.Text = "";
        txtpercentage.Text = "10";
        loaduser();
    }

    void loaduser()
    {
        DataTable Dt = objCL.getUsersForCoreCommitteeClosing();
        if (Dt.Rows.Count > 0)
        {
            gvusers.DataSource = Dt;
            gvusers.DataBind();

            lblcount.Text = Dt.Rows.Count.ToString();
        }
        else
        {
            gvusers.DataSource = null;
            gvusers.DataBind();

            lblcount.Text = "0";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtamount.Text.Trim() != "" && txtpercentage.Text.Trim() != "" && lblcount.Text.Trim() != "0")
        {
            objCL.Amount = Convert.ToDecimal(txtamount.Text);
            objCL.Percentage = Convert.ToDecimal(txtpercentage.Text);

            string res = objCL.Create_CoreCommitteeClosing(objCL);
            if (res == "f")
            {
                string popupScript = "alert('Failed !!!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (res == "t")
            {
                string popupScript = "alert('Created Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                ClearFields();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        claculateShare();
    }

    protected void txtpercentage_TextChanged(object sender, EventArgs e)
    {
        claculateShare();
    }

    public void claculateShare()
    {
        if (txtamount.Text.Trim() != "" && txtpercentage.Text.Trim() != "" && lblcount.Text.Trim() != "0")
        {
            try
            {
                decimal eachShare = (Convert.ToDecimal(txtamount.Text) * Convert.ToDecimal(txtpercentage.Text)) / (100 * Convert.ToInt32(lblcount.Text.Trim()));
                txtshare.Text = eachShare.ToString("0.00");
            }
            catch (Exception ex)
            {
                txtshare.Text = "";
            }
        }
        else
        {
            txtshare.Text = "";
        }
    }
}
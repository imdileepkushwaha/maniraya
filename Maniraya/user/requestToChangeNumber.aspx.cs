using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class user_requestToChangeNumber : System.Web.UI.Page
{
    clsUser objUser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                loadsusername();
                loaddata();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }

    void loaddata()
    {
        DataTable dt = new DataTable();
        objUser = new clsUser();
        dt = objUser.getMobileChangeChargeMaster("Select", "", 0);
        lblMobileChangeCharge.Text = dt.Rows[0]["chargeAmount"].ToString() + " INR";

        dt = null;
        objUser = null;
    }

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            txtPrevMobileNo.Text = dt.Rows[0]["mobile"].ToString();
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Regex for Mobile No.

        System.Text.RegularExpressions.Regex regexObj = new System.Text.RegularExpressions.Regex(@"^\(?([6-9]{1})\)?([0-9]{9})$");

        if (!regexObj.IsMatch(txtNewMobileNo.Text))
        {
            string popupScript = "alert('Enter a Valid Mobile No.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }

        #endregion

        objUser.queryType = "Insert";
        objUser.RegType = "Retailer";
        objUser.UserId = txtuserid.Text;
        objUser.prevMobileNo = txtPrevMobileNo.Text;
        objUser.Mobile = txtNewMobileNo.Text;
        objUser.MentionBy = txtuserid.Text;

        DataTable dt = new DataTable();

        dt = objUser.I_U_S_D_MobileChangeRequest(objUser);

        if (dt.Rows[0][0].ToString() == "Request Under Process")
        {
            Message.Show("Change request is already under process");
        }
        else if (dt.Rows[0][0].ToString() == "Success")
        {
            Message.Show("Request submitted successfully");
            txtNewMobileNo.Text = string.Empty;
        }
        else
        {
            Message.Show("Please try again later");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Path);
    }
}
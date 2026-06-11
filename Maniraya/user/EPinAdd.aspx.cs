using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;

public partial class admin_EPinAdd : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsplan objplan = new clsplan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadplan();
                checkEPinStatus();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    void loadplan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = objEPin.getEPinForGenerateepinnew(objEPin);
        ddplan.DataSource = dt;
        ddplan.DataTextField = "Planname";
        ddplan.DataValueField = "id";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }

    protected void checkEPinStatus()
    {
        DataTable dt = new DataTable();
        objUser.UserId = Session["userid"].ToString();
        dt = objUser.getUserReport(objUser);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["epinGenerationStatus"].ToString() == "0")
            {
                //divFooter.Visible = false;
                //Message.Show("Not allowed to generate new E-Pin");
            }
        }
    }

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserNameWithBalanceutility(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            txtbalance.Text = dt.Rows[0]["balance"].ToString();
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            string popupScript = "toastr.error('Error', 'Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objEPin.GenerateUserId = txtuserid.Text;
        objEPin.Amount = Convert.ToDecimal(txtamount.Text);
        objEPin.NoOfEPins = Convert.ToInt32(txtnoofepin.Text);
        objEPin.MentionBy = Session["userid"].ToString();
        objEPin.plananame = ddplan.SelectedItem.Text;
        objEPin.planid = ddplan.SelectedValue;
        string res = objEPin.Insert_EPinUser(objEPin);
        if (res == "t")
        {
            string popupScript = "alert('E-Pin Generated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadsusername();
            txtnoofepin.Text = "";
            txttotalamount.Text = "";
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('You do not have sufficient balance');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (res == "mmm")
            {
                string popupScript = "alert('You generate pin only one time per day');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (res == "mm")
            {
                string popupScript = "alert('you can geterate 10 pin per day');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    void loadamount()
    {
        DataTable dt = new DataTable();
        objplan.id = ddplan.SelectedValue;
        dt = objplan.getPlan(objplan);
        if (dt.Rows.Count > 0)
        {
            txtamount.Text = dt.Rows[0]["planamount"].ToString();
        }
        else
        {
            txtamount.Text = "";

        }
    }
    protected void ddplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadamount();
    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        int h = 0;
        if (Int32.TryParse(txtamount.Text, out h))
        {
            Int32 amt = Convert.ToInt32(txtamount.Text);
            Int32 min = Convert.ToInt32(ViewState["min"].ToString());
            Int32 max = Convert.ToInt32(ViewState["max"].ToString());
            if (amt < min)
            {
                Message.Show("minimum amount " + ViewState["min"].ToString());
                txtamount.Text = ViewState["min"].ToString();
            }
            if (amt > max)
            {
                Message.Show("minimum amount " + ViewState["max"].ToString());
                txtamount.Text = ViewState["min"].ToString();
            }

        }
        else
        {
            txtamount.Text = ViewState["min"].ToString();
            Message.Show("input only number");
        }


    }
}
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
    clsfranchisee objFran = new clsfranchisee();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            if (!IsPostBack)
            {
                loadplan();
                checkEPinStatus();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }

    protected void checkEPinStatus()
    {
        DataTable dt = new DataTable();
        objFran.UserId = Session["fuserid"].ToString();
        dt = objFran.getUserReport(objFran);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["epinGenerationStatus"].ToString() == "0")
            {
                divFooter.Visible = false;
                Message.Show("Not allowed to generate new E-Pin");
            }
        }

    }

    void loadplan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = objplan.getPlanAll();
        ddplan.DataSource = dt;
        ddplan.DataTextField = "planname";
        ddplan.DataValueField = "id";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            string popupScript = "alert('Invalid User Id');";
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
        objEPin.MentionBy = Session["fuserid"].ToString();
        objEPin.plananame = ddplan.SelectedItem.Text;
        objEPin.planid = ddplan.SelectedValue;
        string res = objEPin.Insert_EPinfranchisee(objEPin);
        if (res == "t")
        {
            string popupScript = "alert('E-Pin Generated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtuserid.Text = "";
            txtusername.Text = "";
            txtnoofepin.Text = "";
        }
        else if (res == "f")
        {
            string popupScript = "alert('Balance is low');";
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
    protected void txtnoofepin_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int TotalAmount = Convert.ToInt32(txtnoofepin.Text) * Convert.ToInt32(txtamount.Text);
            TxtTotalAmount.Text = TotalAmount.ToString();
        }
        catch
        {
           
            TxtTotalAmount.Text = txtamount.Text;
            txtnoofepin.Text = "0";
        }
    }
    protected void ddplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadamount();
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataTier;
using System.Configuration;
using BusinessLogicTier;

public partial class EPinAddFranhcisee : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsfranchisee objUserf = new clsfranchisee();
    clsplan objplan = new clsplan();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loadplan();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadplan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = getPlanAll();
        ddplan.DataSource = dt;
        ddplan.DataTextField = "planname";
        ddplan.DataValueField = "id";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }
    public DataTable getPlanAll()
    {
        string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where MoneyTransfer=1 ";
        //string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where 1=1 ";
        //str_query += " and PlanName Like 'Joining package%' ";

        DataTable ds = null;
        ObjData.StartConnection();
        try
        {
            ds = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            ds = null;
        }
        ObjData.EndConnection();
        return ds;
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUserf.UserId = txtuserid.Text;
        dt = objUserf.getUserName(objUserf);
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
        objEPin.MentionBy = Session["useradmin"].ToString();
        objEPin.plananame = ddplan.SelectedItem.Text;
        objEPin.planid = ddplan.SelectedValue;
        string res = objEPin.Insert_EPin(objEPin);
        if (res == "t")
        {
            string popupScript = "alert('E-Pin Generated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtuserid.Text = "";
            txtusername.Text = "";
            txtnoofepin.Text = "";
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
            ViewState["min"] = dt.Rows[0]["planamount"].ToString();
            ViewState["max"] = dt.Rows[0]["productid"].ToString();
        }
        else
        {
            txtamount.Text = "";          
          
        }
    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        int h=0;
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
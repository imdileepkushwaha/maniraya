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

public partial class admin_EPinReport : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
               
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            objEPin.FromDate = Message.GetIndianDate(txtfromdate.Text);
            objEPin.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objEPin.FromDate = DateTime.MinValue;
            objEPin.ToDate = DateTime.MinValue;
        }
        objEPin.EPinStatus = ddstatus.SelectedValue.ToString();
        objEPin.GenerateUserId = txtgenerateuserid.Text;
        objEPin.UsedUserId = txtuseduserid.Text;

        DataTable dt = new DataTable();
        dt = objEPin.getEPin(objEPin);
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        epincount();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    private void epincount()
    {
        DataTable dt = new DataTable();
        dt = getcount(txtgenerateuserid.Text);
        if (dt.Rows.Count > 0)
        {

            LblCount.Text = dt.Rows[0][0].ToString();
        }
    }
    public DataTable getcount(string userid)
    {
        string str_query = "SELECT count(1)  FROM EPinMaster WHERE  EPinStatus='active'";

        if (userid != string.Empty)
        {
            str_query += " and GenerateUserId='" + userid + "' ";
        }
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
}
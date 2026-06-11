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
using DataTier;

public partial class admin_EPinReport : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
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
        dt = getEPin(objEPin);
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

    }
    public DataTable getEPin(clsEPin objEPin)
    {
        string str_query = "select em.* from EPinMasterFranchisee  em   where 1=1 ";


        if (objEPin.FromDate != DateTime.MinValue && objEPin.ToDate != DateTime.MinValue)
        {
            str_query += "  and em.mentiondate  >= '" + objEPin.FromDate + "'   and em.mentiondate   <= '" + objEPin.ToDate + "' ";
        }


        if (objEPin.EPinStatus != "0")
        {
            str_query += "  and em.epinstatus = '" + objEPin.EPinStatus + "' ";
        }

        if (objEPin.GenerateUserId != "")
        {
            str_query += "  and em.GenerateUserId = '" + objEPin.GenerateUserId + "' ";
        }

        if (objEPin.UsedUserId != "")
        {
            str_query += "  and em.UsedUserId = '" + objEPin.UsedUserId + "' ";
        }

        str_query += " order by em.mentiondate  desc";



        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
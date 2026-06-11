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

public partial class admin_EPinReport : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtgenerateuserid.Text = Session["userid"].ToString();
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
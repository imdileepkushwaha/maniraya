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
            Response.Redirect("index.aspx");
        }
    }
    void loadusername()
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
            txtavailablepins.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    void loadavailableepin()
    {
        DataTable dt = new DataTable();
        objEPin.Amount = Convert.ToDecimal(ddplan.SelectedValue);
        objEPin.GenerateUserId = txtuserid.Text;
        dt = objEPin.getTotalAvailableEPinnew(objEPin);
        txtavailablepins.Text = dt.Rows[0][0].ToString();
    }
    void loadAmountepin()
    {
        DataTable dt = new DataTable();
        objEPin.GenerateUserId = txtuserid.Text;
        dt = objEPin.getEPinForRegnew(objEPin);
        ddplan.DataSource = dt;
        ddplan.DataTextField = "amount";
        ddplan.DataValueField = "amount";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Amount", "0");
        ddplan.Items.Insert(0, li);
    }
    void loadtransferusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txttransferuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txttransferusername.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            txttransferusername.Text = "";
            txttransferuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txttransferuserid.Text != "")
            {

                if (txtnoofepin.Text != "")
                {
                    objUser.UserId = txtuserid.Text;
                    objUser.TransferUserId = txttransferuserid.Text;
                    objUser.pinamount = Convert.ToDecimal(ddplan.SelectedValue);
                    objUser.NoOfEpin = Convert.ToInt32(txtnoofepin.Text);
                    objUser.MentionBy = Session["useradmin"].ToString();
                    string rs = objUser.EPinTransfer(objUser);
                    if (rs == "t")
                    {
                        Message.Show("E-Pin Transferred Successfully...!!!");
                        txttransferuserid.Text = "";
                        txttransferusername.Text = "";
                        txtnoofepin.Text = "";
                        txtavailablepins.Text = "";
                        txtuserid.Text = "";
                        txtusername.Text = "";
                    }
                    else
                        if (rs == "f")
                        {
                            Message.Show("Invalid Transfer User Id...!!!");
                        }
                        else
                            if (rs == "n")
                            {
                                Message.Show("User do not have sufficient E-Pins...!!!");
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                }
                else
                {
                    Message.Show("Enter No of Pin...!!!");
                }

            }
            else
            {
                Message.Show("Enter transfer user id...!!!");
            }
        }
        else
        {
            Message.Show("Enter user id...!!!");
        }
    }


    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadusername();
        loadAmountepin();
    }
    protected void txttransferuserid_TextChanged(object sender, EventArgs e)
    {
        loadtransferusername();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void ddplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadavailableepin();
    }
}
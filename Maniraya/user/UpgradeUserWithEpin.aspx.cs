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

public partial class UpgradeUserWithEpin : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();

                loadusername();
                loadAmountepin();
                loadepin();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadAmountepin()
    {
        DataTable dt = new DataTable();
        objEPin.GenerateUserId = txtuserid.Text;
        dt = objEPin.getEPinToUpgradeUser(objEPin);
        DDLstPlan.DataSource = dt;
        DDLstPlan.DataTextField = "planname";
        DDLstPlan.DataValueField = "Planamount";
        DDLstPlan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        DDLstPlan.Items.Insert(0, li);
    }

    void loadepin()
    {
        ddepin.Items.Clear();
        objEPin.GenerateUserId = txtuserid.Text;
        objEPin.Amount = Convert.ToDecimal(DDLstPlan.SelectedValue);
        DataTable dt = new DataTable();
        dt = objEPin.getEPinForRegamount(objEPin);
        ddepin.DataSource = dt;
        ddepin.DataTextField = "EpinNo";
        ddepin.DataValueField = "EpinNo";
        ddepin.DataBind();
        ListItem li = new ListItem("Select E-Pin", "0");
        ddepin.Items.Insert(0, li);
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

            Message.Show("Invalid User Id...!!!");
        }
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
                if (ddepin.SelectedIndex != 0)
                {
                    objUser.UserId = txtuserid.Text;
                    objUser.TransferUserId = txttransferuserid.Text;
                    objUser.EpinNo = ddepin.SelectedValue;
                    objUser.MentionBy = Session["userid"].ToString();
                    string rs = objUser.UpgradeUserWithEpin(objUser);
                    if (rs == "t")
                    {
                        Message.Show("User Upgraded Successfully...!!!");
                        txttransferuserid.Text = "";
                        txttransferusername.Text = "";
                        //txtuserid.Text = "";
                        // txtusername.Text = "";
                        loadepin();
                    }
                    else
                        if (rs == "f")
                        {
                            Message.Show("this User Id is not active...!!!");
                        }
                        else
                            if (rs == "n")
                            {
                                Message.Show("invalid E-Pins...!!!");
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                }
                else
                {
                    Message.Show("select Pin...!!!");
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

    void loadepinamount()
    {
        objEPin.EPinNo = ddepin.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objEPin.getEPinFullDetail(objEPin);
        if (dt.Rows.Count > 0)
        {
            txtamount.Text = dt.Rows[0]["amount"].ToString();
        }
        else
        {
            txtamount.Text = "";
        }
    }
    protected void ddepin_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepinamount();
    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadusername();
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
    protected void DDLstPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepin();
    }
}
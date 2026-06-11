using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class CashRequstAdd : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["chktrans"] != null)
                {


                    txtuserid.Text = Session["userid"].ToString();
                    txtuserid.Enabled = false;
                    loadsusername();
                    loadPV();
                    loadnotification();
                    loadgethelp();
                    Session.Remove("chktrans");
                }


                else
                {
                    Session["pg"] = "cashrequest";
                    Response.Redirect("TransactionPass.aspx");
                }
            }

        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    
    void loadnotification()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.getUserDetail(objUser);        
        if (dt.Rows[0]["activestatus"].ToString() == "0")
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
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
            Message.Show("Invalid User Id...!!!");
        }
    }

    void loadPV()
    {
        DataTable dt = new DataTable();
        dt = objUser.getPV(txtuserid.Text);
        if (dt.Rows.Count > 0)
        {
           
          txtbalance.Text = dt.Rows[0][2].ToString();
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (txtamount.Text != "")
                {
                    if (Convert.ToDecimal(txtbalance.Text) >= Convert.ToDecimal(txtamount.Text))
                    {
                        if (Convert.ToDecimal(txtamount.Text) >= 1000.00M)
                        {

                            objaccount.UserId = Session["userId"].ToString();
                            objaccount.plananame = "T";
                            objaccount.Amount = Convert.ToDecimal(txtamount.Text);
                            string res = objaccount.UserCashrequest(objaccount);

                            if (res == "t")
                            {
                                Message.Show("Transfer to wallet Submitted Successfully...!!!");
                                txtamount.Text = "";
                                loadsusername();
                                loadPV();
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                        }
                        else
                        {
                            Message.Show("Transfer PV Must Be Greater Than or equal 1000...!!!");
                        }
                    }
                    else
                    {
                        Message.Show("Transfer PV Must Be Lesss Than Or Equal Than PV Balance...!!!");
                    }
                }
                else
                {
                    Message.Show("Enter PV...!!!");
                }
            }
            else
            {
                Message.Show("Enter User Name...!!!");
            }
        }
        else
        {
            Message.Show("Enter User Id...!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    void loadgethelp()
    {
       
            objaccount.FromDate = DateTime.MinValue;
            objaccount.ToDate = DateTime.MinValue;
      
        objaccount.WithdrawlRequestStatus = "0";
        objaccount.DepositrequestRequestType = "CASH";
        DataTable dt = new DataTable();
        objaccount.UserId = txtuserid.Text;
        dt = objaccount.getPrimeMmeberRequest(objaccount);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
   
}
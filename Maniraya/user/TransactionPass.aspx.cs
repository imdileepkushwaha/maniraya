using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_TransactionPass : System.Web.UI.Page
{
    clsLogin objlogin = new clsLogin();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {

            txtuserid.Text = Session["userid"].ToString();
            txtuserid.Enabled = false;
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    public string GetIp()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["pg"] != null)
        {
            objlogin.username = txtuserid.Text;
            objlogin.password = Txtapssword.Text;
            ViewState["pwd"] = Txtapssword.Text;
            objlogin.ipaddress = GetIp();
            DataTable dt = new DataTable();
            dt = objlogin.Chk_UserLoginDetailstranspassword(objlogin);
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["status123"].ToString() == "1")
                {
                    ViewState["otp"] = dt.Rows[0]["otp"].ToString();
                    objlogin.Sendotp(dt.Rows[0]["otp"].ToString(), dt.Rows[0]["mobile"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);

                }
                else
                {
                    Session["chktrans"] = "0";
                    redirecttopage();
                }

            }
            else
            {
                Message.Show("Invalid Login Details...!!!");
            }
        }
        else
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    void redirecttopage()
    {
         if (Session["pg"] == "purchase")
         {
             Response.Redirect("FranchiseeSearchnew.aspx");
         }
         if (Session["pg"] == "purchasereport")
         {
             Response.Redirect("PurchaseReport.aspx");
         }
         if (Session["pg"] == "coupanassignment")
         {
             Response.Redirect("CouponAssignmentMaster.aspx");
         }
         if (Session["pg"] == "recharge")
         {
             Response.Redirect("Recharge.aspx");
         }
         if (Session["pg"] == "rechargereport")
         {
             Response.Redirect("RechargeReport.aspx");
         }
         if (Session["pg"] == "disputereport")
         {
             Response.Redirect("DisputeRequest.aspx");
         }
         if (Session["pg"] == "wallettransfer")
         {
             Response.Redirect("WalletTransfer.aspx");
         }
         if (Session["pg"] == "cashrequest")
         {
             Response.Redirect("CashRequstAdd.aspx");
         }
        
        
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        objlogin.username = txtuserid.Text;
        objlogin.OTP = TxtOtp.Text;
        objlogin.ipaddress = GetIp();
        string res = objlogin.Confirmotp(objlogin);
        if (res == "1")
        {
            redirecttopage();
        }
        else
        {
            LblMessages.Visible = true;
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);
        }
    }
    protected void BtnResend_Click(object sender, EventArgs e)
    {


        objlogin.username =txtuserid.Text;
        objlogin.password = ViewState["pwd"].ToString();
        objlogin.ipaddress = GetIp();

        DataTable dt = new DataTable();
        dt = objlogin.Chk_UserLoginDetails(objlogin);
        if (dt.Rows.Count > 0)
        {
                objlogin.Sendotp(dt.Rows[0]["otp"].ToString(), dt.Rows[0]["mobile"].ToString());
                string popupScript2 = "Closepopup();";
                ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12();", true);
       
        }
        else
        {
            Message.Show("Invalid Login Details...!!!");
        }
    }
}
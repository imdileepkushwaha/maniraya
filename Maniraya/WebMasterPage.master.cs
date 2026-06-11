using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebMasterPage : System.Web.UI.MasterPage
{
    clsProduct objState = new clsProduct();
    clsUser objusr = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        loadaddcartitem();
        UpdateBal();
        UpdateAuthHeader();
    }

    void UpdateAuthHeader()
    {
        bool isLoggedIn = Session["userid"] != null;
        string displayName = Session["username"] != null
            ? Session["username"].ToString()
            : "Account";

        if (liMenuLogin != null) liMenuLogin.Visible = !isLoggedIn;
        if (liMenuSignup != null) liMenuSignup.Visible = !isLoggedIn;
        if (liMenuLogout != null) liMenuLogout.Visible = isLoggedIn;
        if (liMenuDashboard != null) liMenuDashboard.Visible = isLoggedIn;

        if (headerAccountLink != null)
        {
            headerAccountLink.HRef = isLoggedIn
                ? ResolveUrl("~/user/dashboard.aspx")
                : ResolveUrl("~/Login.aspx");
            headerAccountLink.Attributes["aria-label"] = isLoggedIn
                ? "Open account dashboard"
                : "Sign in to your account";
        }

        if (headerAccountLabel != null)
        {
            headerAccountLabel.Text = isLoggedIn ? displayName : "Account";
        }

        if (BtnMyaccount != null)
        {
            BtnMyaccount.Text = isLoggedIn ? "Dashboard" : "My Account";
        }

        if (lblwallet != null)
        {
            lblwallet.Visible = isLoggedIn;
        }
    }

    void loadaddcartitem()
    {
        if (Session["userid"] != null)
        {
            objState.UserId = Session["userid"].ToString();
            DataTable Dt = objState.getCartItems(objState);
            if (Dt != null && Dt.Rows.Count > 0)
            {


                cartCount.Text = Dt.Rows.Count.ToString();

               
            }
        }     


    }
    public void UpdateBal()
    {
        DataTable dt = new DataTable();
        if (Session["userid"] != null)
        {
            objusr.UserId = Session["userid"].ToString();
            dt = objusr.getUserDetail(objusr);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblwallet.Text = "Wallet: " + Math.Round(Convert.ToDecimal(dt.Rows[0]["balanceamount"].ToString()), 2).ToString();
            }
        }
       
       
    }
    protected void BtnMyaccount_Click(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            Response.Redirect("user/dashboard.aspx");
        }
        else
        {
            Session["returnUrl"] = "user/dashboard.aspx";
            Response.Redirect("login.aspx");

        }
    }
}

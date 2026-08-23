using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebMasterPage : System.Web.UI.MasterPage
{
    clsProduct objState = new clsProduct();
    clsUser objusr = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategories();
            BindSiteContacts();
        }
        loadaddcartitem();
        UpdateBal();
        UpdateAuthHeader();
    }

    private void BindCategories()
    {
        try
        {
            DataTable dt = CatalogHelper.BuildDisplayCategories();
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            if (!dt.Columns.Contains("CategoryId") && dt.Columns.Contains("CategoryID"))
            {
                dt.Columns["CategoryID"].ColumnName = "CategoryId";
            }
            else if (!dt.Columns.Contains("CategoryId") && dt.Columns.Contains("Categoryid"))
            {
                dt.Columns["Categoryid"].ColumnName = "CategoryId";
            }

            rptShopCategories.DataSource = dt;
            rptShopCategories.DataBind();

            // if (rptFooterCategories != null)
            // {
            //     rptFooterCategories.DataSource = dt;
            //     rptFooterCategories.DataBind();
            // }

            if (ddlHeaderCategory != null)
            {
                ddlHeaderCategory.Items.Clear();
                ddlHeaderCategory.DataSource = dt;
                ddlHeaderCategory.DataTextField = "CategoryName";
                ddlHeaderCategory.DataValueField = "CategoryId";
                ddlHeaderCategory.DataBind();
                ddlHeaderCategory.Items.Insert(0, new ListItem("All categories", "0"));
            }
        }
        catch
        {
        }
    }

    private void BindSiteContacts()
    {
        try
        {
            string phone = SiteContactHelper.GetPrimaryPhone();
            string email = SiteContactHelper.GetPrimaryEmail();

            if (lnkHeaderPhone != null)
            {
                lnkHeaderPhone.HRef = SiteContactHelper.BuildTelHref(phone);
            }

            if (lblHeaderPhone != null)
            {
                lblHeaderPhone.InnerText = phone;
            }

            if (lnkFooterPhone != null)
            {
                lnkFooterPhone.HRef = SiteContactHelper.BuildTelHref(phone);
                lnkFooterPhone.InnerText = phone;
            }

            if (lnkFooterEmail != null)
            {
                lnkFooterEmail.HRef = SiteContactHelper.BuildMailtoHref(email);
                lnkFooterEmail.InnerText = email;
            }
        }
        catch
        {
        }
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
        CatalogCartHelper.TryCompletePendingAdd();
        if (Session["userid"] != null)
        {
            objState.UserId = Session["userid"].ToString();
            DataTable Dt = objState.getCartItems(objState);
            cartCount.Text = (Dt != null ? Dt.Rows.Count : 0).ToString();
        }
    }

    public void UpdateBal()
    {
        if (Session["userid"] == null || lblwallet == null)
        {
            return;
        }

        objusr.UserId = Session["userid"].ToString();
        DataTable dt = objusr.getUserDetail(objusr);
        if (dt == null || dt.Rows.Count == 0)
        {
            lblwallet.Text = "Wallet: 0.00";
            return;
        }

        lblwallet.Text = "Wallet: " + GetWalletBalanceText(dt.Rows[0]);
    }

    static string GetWalletBalanceText(DataRow row)
    {
        decimal balance = 0m;
        if (row != null && row.Table != null && row.Table.Columns.Contains("balanceamount") && row["balanceamount"] != DBNull.Value)
        {
            string raw = Convert.ToString(row["balanceamount"]).Trim();
            if (!decimal.TryParse(raw, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out balance))
            {
                decimal.TryParse(raw, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out balance);
            }
        }

        return Math.Round(balance, 2).ToString("0.00");
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

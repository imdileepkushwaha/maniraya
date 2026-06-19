using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addtocart : Page
{
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            CartSettingsHelper.EnsureTableAndSeedDefaults();
            BindHeroMeta();
            UpdateBal();
            loadaddcartitem(Session["userid"].ToString());
        }
    }

    void BindHeroMeta()
    {
        litHeroFreeShipping.Text = CartSettingsHelper.GetHeroFreeShippingText();
    }

    public void UpdateBal()
    {
        if (Session["userid"] == null)
        {
            return;
        }

        objuser.UserId = Session["userid"].ToString();
        DataTable dt = objuser.getUserDetail(objuser);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfwalllet.Value = Math.Round(Convert.ToDecimal(dt.Rows[0]["balanceamount"].ToString()), 2).ToString();
        }
    }

    void loadaddcartitem(string id)
    {
        rptCart.DataSource = null;
        rptCart.DataBind();

        objState.UserId = id;
        DataTable dt = CartHelper.GetCartItemsWithImages(id);
        if (dt == null || dt.Rows.Count == 0)
        {
            pnlCartItems.Visible = false;
            pnlEmptyCart.Visible = true;
            pnlShippingProgress.Visible = false;
            LinkButton1.Visible = false;
            LblProductcount.Text = "0 Products";
            ResetSummary();
            return;
        }

        pnlCartItems.Visible = true;
        pnlEmptyCart.Visible = false;
        pnlShippingProgress.Visible = true;
        LinkButton1.Visible = true;

        rptCart.DataSource = dt;
        rptCart.DataBind();
        LblProductcount.Text = dt.Rows.Count + " Products";
        CalculateSummary(dt);
    }

    void CalculateSummary(DataTable dt)
    {
        decimal purchaseamounttotal = 0;
        decimal gstTotal = 0;
        decimal orderTotal = 0;
        decimal totalMrp = 0;

        foreach (DataRow row in dt.Rows)
        {
            purchaseamounttotal += Convert.ToDecimal(row["PurchaseAmount"]);
            gstTotal += Convert.ToDecimal(row["CGST"]) + Convert.ToDecimal(row["SGST"]) + Convert.ToDecimal(row["IGST"]);
            orderTotal += Convert.ToDecimal(row["TotalAmount"]);
            totalMrp += Convert.ToDecimal(row["MRP"]) * Convert.ToInt32(row["Quantity"]);
        }

        decimal walletDeduction = Math.Round(orderTotal * 6m / 100m, 2);
        decimal walletBalance = 0m;
        decimal.TryParse(hfwalllet.Value, out walletBalance);

        if (walletBalance >= walletDeduction)
        {
            Lblwalletdeduction.Text = "₹ " + walletDeduction.ToString("0.00");
        }
        else
        {
            walletDeduction = 0m;
            Lblwalletdeduction.Text = "₹ 0.00";
        }

        decimal discount = totalMrp - orderTotal;
        decimal shipping = 0m;
        decimal freeShippingMin = CartSettingsHelper.GetFreeShippingMinAmount();

        if (orderTotal < freeShippingMin)
        {
            shipping = CartSettingsHelper.GetShippingCharge();
            lblShipping.Text = "₹ " + shipping.ToString("0.00");
            lblShipping.CssClass = "summary-value";
        }
        else
        {
            lblShipping.Text = "FREE";
            lblShipping.CssClass = "summary-value text-success";
        }

        decimal payable = orderTotal - walletDeduction + shipping;

        lblSubtotal.Text = "₹ " + purchaseamounttotal.ToString("0.00");
        lblDiscount.Text = "₹ " + discount.ToString("0.00");
        lblTax.Text = "₹ " + gstTotal.ToString("0.00");
        lblTotal.Text = "₹ " + payable.ToString("0.00");

        BindShippingProgress(orderTotal);
    }

    void BindShippingProgress(decimal cartTotal)
    {
        ShippingProgressModel progress = CartSettingsHelper.BuildShippingProgress(cartTotal);
        litProgressTitle.Text = progress.Title;
        litProgressMessage.Text = progress.Message;
        shippingProgressFill.Style["width"] = progress.ProgressPercent + "%";
        pnlShippingProgress.CssClass = progress.IsUnlocked
            ? "cart-delivery-progress is-unlocked"
            : "cart-delivery-progress";
    }

    void ResetSummary()
    {
        lblSubtotal.Text = "₹ 0.00";
        lblDiscount.Text = "₹ 0.00";
        lblTax.Text = "₹ 0.00";
        lblShipping.Text = "₹ 0.00";
        lblShipping.CssClass = "summary-value";
        Lblwalletdeduction.Text = "₹ 0.00";
        lblTotal.Text = "₹ 0.00";
    }

    protected string GetCartImageUrl(object imageUrl, object productName)
    {
        string imagePath = Convert.ToString(imageUrl);
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            imagePath = CatalogHelper.ResolveProductImageUrl(null, Convert.ToString(productName));
        }

        return ResolveUrl("~/" + imagePath.TrimStart('/'));
    }

    protected void IncreaseQty(object sender, CommandEventArgs e)
    {
        objState.CategoryId = e.CommandArgument.ToString();
        objState.BATCHNO = "P";
        objState.updatecartitem(objState);
        loadaddcartitem(Session["userid"].ToString());
    }

    protected void DecreaseQty(object sender, CommandEventArgs e)
    {
        objState.CategoryId = e.CommandArgument.ToString();
        objState.BATCHNO = "M";
        objState.updatecartitem(objState);
        loadaddcartitem(Session["userid"].ToString());
    }

    protected void RemoveQty(object sender, CommandEventArgs e)
    {
        objState.ProductId = e.CommandArgument.ToString();
        objState.DeleteCartItems(objState);
        loadaddcartitem(Session["userid"].ToString());
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        objState.UserId = Session["userid"].ToString();
        objState.DeleteAllCartItems(objState);
        loadaddcartitem(Session["userid"].ToString());
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        Response.Redirect("addaddress.aspx");
    }
}

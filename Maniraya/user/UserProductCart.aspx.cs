using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_UserProductCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindCart();
        }
    }

    void BindCart()
    {
        DataTable cart = UserPanelCartHelper.GetCart(Session);
        UserPanelCartHelper.CartMeta meta = UserPanelCartHelper.GetMeta(Session);
        string catalogUrl = UserPanelCartHelper.GetCatalogUrl(meta);
        lnkContinueTop.NavigateUrl = catalogUrl;
        lnkContinue.NavigateUrl = catalogUrl;
        lnkEmptyShop.NavigateUrl = catalogUrl;

        bool hasItems = cart != null && cart.Rows.Count > 0;
        pnlCart.Visible = hasItems;
        pnlEmpty.Visible = !hasItems;

        UserPanelCartHelper.CartTotals totals = UserPanelCartHelper.GetTotals(Session);
        litCartTitleCount.Text = hasItems ? "(" + totals.ItemCount + " items)" : string.Empty;
        litItemCount.Text = totals.ItemCount + (totals.ItemCount == 1 ? " item" : " items");
        litSubtotal.Text = totals.Subtotal.ToString("0.00");
        litShipping.Text = totals.Shipping.ToString("0.00");
        litPayable.Text = totals.Payable.ToString("0.00");
        litShipNote.Text = totals.Quote == null ? string.Empty : totals.Quote.Message;

        rptCart.DataSource = cart;
        rptCart.DataBind();
        chkSelectAll.Checked = false;
    }

    protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string productId = Convert.ToString(e.CommandArgument);
        string message;
        if (e.CommandName == "Remove")
        {
            UserPanelCartHelper.Remove(Session, productId);
        }
        else if (e.CommandName == "Plus" || e.CommandName == "Minus")
        {
            DataRow row = FindRow(productId);
            int qty = row == null ? 1 : UserPanelCartHelper.ToInt(row["Quantity"]);
            qty = e.CommandName == "Plus" ? qty + 1 : qty - 1;
            if (!UserPanelCartHelper.UpdateQuantity(Session, productId, qty, out message))
            {
                Alert(message);
            }
        }

        BindCart();
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rptCart.Items)
        {
            CheckBox chk = item.FindControl("chkSelect") as CheckBox;
            if (chk != null)
            {
                chk.Checked = chkSelectAll.Checked;
            }
        }
    }

    protected void btnRemoveSelected_Click(object sender, EventArgs e)
    {
        bool any = false;
        foreach (RepeaterItem item in rptCart.Items)
        {
            CheckBox chk = item.FindControl("chkSelect") as CheckBox;
            HiddenField hf = item.FindControl("hfProductId") as HiddenField;
            if (chk != null && chk.Checked && hf != null)
            {
                UserPanelCartHelper.Remove(Session, hf.Value);
                any = true;
            }
        }

        if (!any)
        {
            Alert("Select at least one item to remove.");
        }

        BindCart();
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        if (UserPanelCartHelper.GetLineCount(Session) == 0)
        {
            Alert("Your cart is empty.");
            BindCart();
            return;
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "goCheckout", "window.location='UserProductCheckout.aspx';", true);
    }

    DataRow FindRow(string productId)
    {
        DataTable cart = UserPanelCartHelper.GetCart(Session);
        foreach (DataRow row in cart.Rows)
        {
            if (string.Equals(Convert.ToString(row["ProductId"]), productId, StringComparison.OrdinalIgnoreCase))
            {
                return row;
            }
        }

        return null;
    }

    void Alert(string message)
    {
        string safe = (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" + safe + "');", true);
    }
}

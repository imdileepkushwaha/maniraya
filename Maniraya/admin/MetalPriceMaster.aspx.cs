using System;
using System.Data;
using System.Globalization;
using System.Web.UI;

public partial class MetalPriceMaster : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            MetalPriceHelper.EnsureTableAndSeedDefaults();
            LoadPrices();
            BindGrid();
        }
    }

    private void LoadPrices()
    {
        txtGoldPrice.Text = FormatPriceInput(MetalPriceHelper.GetGoldPrice());
        txtSilverPrice.Text = FormatPriceInput(MetalPriceHelper.GetSilverPrice());
        txtDiamondPrice.Text = FormatPriceInput(MetalPriceHelper.GetDiamondPrice());
    }

    private void BindGrid()
    {
        GridView1.DataSource = MetalPriceHelper.GetAllMetalPrices();
        GridView1.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal goldPrice;
        decimal silverPrice;
        decimal diamondPrice;

        if (!TryParsePrice(txtGoldPrice.Text, out goldPrice) ||
            !TryParsePrice(txtSilverPrice.Text, out silverPrice) ||
            !TryParsePrice(txtDiamondPrice.Text, out diamondPrice))
        {
            ShowAlert("Please enter valid prices for Gold, Silver, and Diamond.");
            return;
        }

        if (goldPrice < 0 || silverPrice < 0 || diamondPrice < 0)
        {
            ShowAlert("Price cannot be negative.");
            return;
        }

        string updatedBy = Session["useradmin"] != null
            ? Session["useradmin"].ToString()
            : "Admin";

        if (!MetalPriceHelper.SaveAllPrices(goldPrice, silverPrice, diamondPrice, updatedBy))
        {
            ShowAlert("Unable to save metal prices. Please try again.");
            return;
        }

        ShowAlert("Metal prices saved successfully.");
        LoadPrices();
        BindGrid();
    }

    private static bool TryParsePrice(string input, out decimal price)
    {
        price = 0m;
        if (string.IsNullOrWhiteSpace(input))
        {
            return true;
        }

        return decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out price)
            || decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out price);
    }

    private static string FormatPriceInput(decimal price)
    {
        return price.ToString("0.##", CultureInfo.InvariantCulture);
    }

    private void ShowAlert(string message)
    {
        string safeMessage = (message ?? string.Empty).Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(this, GetType(), "metalPriceAlert", "alert('" + safeMessage + "');", true);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddJewellery : Page
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
            JewelleryProductHelper.EnsureTable();
            LoadMetalRates();
            BindSizeCheckboxes();
            LoadMetalTypes();
            LoadJewelleryTypes();
            hfMrpMarkup.Value = JewelleryPriceHelper.MrpMarkupPercent.ToString(CultureInfo.InvariantCulture);
            litPriceHint.Text = string.Format(
                CultureInfo.InvariantCulture,
                "Price = Metal value + Making + GST. MRP = Price + {0}% markup (rounded to nearest ₹10).",
                JewelleryPriceHelper.MrpMarkupPercent);
        }
    }

    private void LoadMetalTypes()
    {
        ddMetalType.Items.Clear();
        ddMetalType.Items.Add(new ListItem("Select Metal Type", "0"));
        ddMetalType.Items.Add(new ListItem("Gold", JewelleryMetalTypes.Gold));
        ddMetalType.Items.Add(new ListItem("Silver", JewelleryMetalTypes.Silver));
        ddMetalType.Items.Add(new ListItem("Diamond", JewelleryMetalTypes.Diamond));
        ddMetalType.Items.Add(new ListItem("Gold + Diamond", JewelleryMetalTypes.GoldDiamond));
        ddMetalType.Items.Add(new ListItem("Gold + Silver", JewelleryMetalTypes.GoldSilver));
        ddMetalType.Items.Add(new ListItem("Silver + Diamond", JewelleryMetalTypes.SilverDiamond));
        ddMetalType.Items.Add(new ListItem("Gold + Silver + Diamond", JewelleryMetalTypes.GoldSilverDiamond));
    }

    private void LoadJewelleryTypes()
    {
        ddJewelleryType.Items.Clear();
        ddJewelleryType.Items.Add(new ListItem("Select Jewellery Type", "0"));
        ddJewelleryType.Items.Add(new ListItem("Ring", "Ring"));
        ddJewelleryType.Items.Add(new ListItem("Necklace", "Necklace"));
        ddJewelleryType.Items.Add(new ListItem("Earring", "Earring"));
        ddJewelleryType.Items.Add(new ListItem("Bangle", "Bangle"));
        ddJewelleryType.Items.Add(new ListItem("Bracelet", "Bracelet"));
        ddJewelleryType.Items.Add(new ListItem("Pendant", "Pendant"));
        ddJewelleryType.Items.Add(new ListItem("Chain", "Chain"));
        ddJewelleryType.Items.Add(new ListItem("Nose Pin", "Nose Pin"));
        ddJewelleryType.Items.Add(new ListItem("Other", "Other"));
    }

    private void BindSizeCheckboxes(List<int> selectedSizeIds = null)
    {
        cblSizes.Items.Clear();
        cblSizes.DataSource = JewelleryProductHelper.GetActiveSizes();
        cblSizes.DataTextField = "SizeName";
        cblSizes.DataValueField = "Id";
        cblSizes.DataBind();

        if (selectedSizeIds == null || selectedSizeIds.Count == 0)
        {
            return;
        }

        foreach (ListItem item in cblSizes.Items)
        {
            int sizeId;
            if (int.TryParse(item.Value, out sizeId) && selectedSizeIds.Contains(sizeId))
            {
                item.Selected = true;
            }
        }
    }

    private List<int> GetSelectedSizeIds()
    {
        List<int> selected = new List<int>();
        foreach (ListItem item in cblSizes.Items)
        {
            if (!item.Selected)
            {
                continue;
            }

            int sizeId;
            if (int.TryParse(item.Value, out sizeId))
            {
                selected.Add(sizeId);
            }
        }

        return selected;
    }

    private List<JewellerySizeItem> GetSelectedSizes()
    {
        List<JewellerySizeItem> sizes = new List<JewellerySizeItem>();
        foreach (ListItem item in cblSizes.Items)
        {
            if (!item.Selected)
            {
                continue;
            }

            int sizeId;
            if (int.TryParse(item.Value, out sizeId))
            {
                sizes.Add(new JewellerySizeItem
                {
                    SizeId = sizeId,
                    SizeName = item.Text
                });
            }
        }

        return sizes;
    }

    protected void btnAddSize_Click(object sender, EventArgs e)
    {
        string input = txtNewSize.Text.Trim();
        if (string.IsNullOrEmpty(input))
        {
            ShowAlert("Enter size name (e.g. 12 or 12, 14, 16).");
            return;
        }

        List<int> selectedSizeIds = GetSelectedSizeIds();
        string createdBy = Session["useradmin"] != null
            ? Session["useradmin"].ToString()
            : "Admin";

        int addedCount = JewelleryProductHelper.AddSizesFromInput(input, createdBy, selectedSizeIds);
        BindSizeCheckboxes(selectedSizeIds);
        txtNewSize.Text = string.Empty;

        if (addedCount > 0)
        {
            ShowAlert(addedCount == 1
                ? "Size added and selected."
                : addedCount + " sizes added and selected.");
        }
        else
        {
            ShowAlert("Size already exists and has been selected.");
        }
    }

    private void LoadMetalRates()
    {
        decimal goldRate = MetalPriceHelper.GetGoldPrice();
        decimal silverRate = MetalPriceHelper.GetSilverPrice();
        decimal diamondRate = MetalPriceHelper.GetDiamondPrice();

        hfGoldRate.Value = goldRate.ToString(CultureInfo.InvariantCulture);
        hfSilverRate.Value = silverRate.ToString(CultureInfo.InvariantCulture);
        hfDiamondRate.Value = diamondRate.ToString(CultureInfo.InvariantCulture);

        litGoldRate.Text = goldRate.ToString("N2", CultureInfo.InvariantCulture);
        litSilverRate.Text = silverRate.ToString("N2", CultureInfo.InvariantCulture);
        litDiamondRate.Text = diamondRate.ToString("N2", CultureInfo.InvariantCulture);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text.Trim();
        if (string.IsNullOrEmpty(title))
        {
            ShowAlert("Enter jewellery title.");
            return;
        }

        if (ddMetalType.SelectedValue == "0")
        {
            ShowAlert("Select metal type.");
            return;
        }

        if (ddJewelleryType.SelectedValue == "0")
        {
            ShowAlert("Select jewellery type (Ring, Necklace, etc.).");
            return;
        }

        List<JewellerySizeItem> selectedSizes = GetSelectedSizes();
        if (selectedSizes.Count == 0)
        {
            ShowAlert("Select at least one size, or add new sizes on this page.");
            return;
        }

        decimal bv;
        if (!TryParseDecimal(txtBV.Text, out bv) || bv < 0)
        {
            ShowAlert("Enter a valid Business Volume (BV).");
            return;
        }

        string metalType = ddMetalType.SelectedValue;
        decimal goldWeight;
        decimal silverWeight;
        decimal diamondCarat;
        decimal makingCharges;
        decimal gstPercent;

        if (!TryParseDecimal(txtGoldWeight.Text, out goldWeight) ||
            !TryParseDecimal(txtSilverWeight.Text, out silverWeight) ||
            !TryParseDecimal(txtDiamondCarat.Text, out diamondCarat) ||
            !TryParseDecimal(txtMakingCharges.Text, out makingCharges) ||
            !TryParseDecimal(txtGstPercent.Text, out gstPercent))
        {
            ShowAlert("Please enter valid numbers for composition and charges.");
            return;
        }

        if (goldWeight < 0 || silverWeight < 0 || diamondCarat < 0 || makingCharges < 0 || gstPercent < 0)
        {
            ShowAlert("Weights and charges cannot be negative.");
            return;
        }

        JewelleryMetalTypes.NormalizeWeights(metalType, ref goldWeight, ref silverWeight, ref diamondCarat);

        if (!ValidateComposition(metalType, goldWeight, silverWeight, diamondCarat))
        {
            return;
        }

        string image1 = UploadImage(fuImage1);
        string image2 = UploadImage(fuImage2);
        string image3 = UploadImage(fuImage3);
        string image4 = UploadImage(fuImage4);

        if (string.IsNullOrEmpty(image1))
        {
            ShowAlert("Upload primary image (Image 1).");
            return;
        }

        if (string.IsNullOrEmpty(image2) || string.IsNullOrEmpty(image3) || string.IsNullOrEmpty(image4))
        {
            ShowAlert("Upload all 4 images.");
            return;
        }

        decimal goldRate = MetalPriceHelper.GetGoldPrice();
        decimal silverRate = MetalPriceHelper.GetSilverPrice();
        decimal diamondRate = MetalPriceHelper.GetDiamondPrice();

        JewelleryPriceResult pricing = JewelleryPriceHelper.Calculate(
            goldWeight,
            silverWeight,
            diamondCarat,
            makingCharges,
            gstPercent,
            goldRate,
            silverRate,
            diamondRate);

        txtPrice.Text = pricing.Price.ToString("0.00", CultureInfo.InvariantCulture);
        txtMRP.Text = pricing.Mrp.ToString("0.00", CultureInfo.InvariantCulture);

        string createdBy = Session["useradmin"] != null
            ? Session["useradmin"].ToString()
            : "Admin";

        string result = JewelleryProductHelper.InsertJewellery(
            title,
            txtShortDescription.Text.Trim(),
            txtDescription.Text.Trim(),
            image1,
            image2,
            image3,
            image4,
            metalType,
            ddJewelleryType.SelectedValue,
            selectedSizes,
            goldWeight,
            silverWeight,
            diamondCarat,
            makingCharges,
            gstPercent,
            goldRate,
            silverRate,
            diamondRate,
            pricing,
            bv,
            txtHSN.Text.Trim(),
            createdBy);

        if (result == "0" || string.IsNullOrEmpty(result))
        {
            ShowAlert("Unable to save jewellery. Run database scripts and try again.");
            return;
        }

        ShowAlert("Jewellery saved successfully with " + selectedSizes.Count + " size(s).");
        ClearForm();
    }

    private bool ValidateComposition(string metalType, decimal goldWeight, decimal silverWeight, decimal diamondCarat)
    {
        if (JewelleryMetalTypes.RequiresGold(metalType) && goldWeight <= 0)
        {
            ShowAlert("Enter gold weight in grams.");
            return false;
        }

        if (JewelleryMetalTypes.RequiresSilver(metalType) && silverWeight <= 0)
        {
            ShowAlert("Enter silver weight in grams.");
            return false;
        }

        if (JewelleryMetalTypes.RequiresDiamond(metalType) && diamondCarat <= 0)
        {
            ShowAlert("Enter diamond weight in carat.");
            return false;
        }

        return true;
    }

    private void ClearForm()
    {
        txtTitle.Text = string.Empty;
        txtHSN.Text = string.Empty;
        txtShortDescription.Text = string.Empty;
        txtDescription.Text = string.Empty;
        ddMetalType.SelectedValue = "0";
        ddJewelleryType.SelectedValue = "0";
        txtNewSize.Text = string.Empty;
        txtGoldWeight.Text = "0";
        txtSilverWeight.Text = "0";
        txtDiamondCarat.Text = "0";
        txtMakingCharges.Text = "0";
        txtGstPercent.Text = "3";
        txtBV.Text = string.Empty;
        txtPrice.Text = string.Empty;
        txtMRP.Text = string.Empty;
        BindSizeCheckboxes();
    }

    private static string UploadImage(FileUpload upload)
    {
        if (upload == null || !upload.HasFile)
        {
            return string.Empty;
        }

        string fileName = Path.GetFileName(upload.PostedFile.FileName);
        string imageName = DateTime.Now.Ticks + fileName;
        string folder = upload.Page.Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        upload.PostedFile.SaveAs(Path.Combine(folder, imageName));
        return imageName;
    }

    private static bool TryParseDecimal(string input, out decimal value)
    {
        value = 0m;
        if (string.IsNullOrWhiteSpace(input))
        {
            return true;
        }

        return decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out value)
            || decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out value);
    }

    private void ShowAlert(string message)
    {
        string safeMessage = (message ?? string.Empty).Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(this, GetType(), "jewelleryAlert", "alert('" + safeMessage + "');", true);
    }
}

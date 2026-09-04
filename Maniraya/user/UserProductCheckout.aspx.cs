using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_UserProductCheckout : System.Web.UI.Page
{
    clsState objCState = new clsState();
    clsfranchisee objF = new clsfranchisee();
    clsAccount objaccount = new clsAccount();
    clsBank objbank = new clsBank();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        Page.Form.Enctype = "multipart/form-data";

        if (UserPanelCartHelper.GetLineCount(Session) == 0)
        {
            Response.Redirect("UserProductCart.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadStates();
            LoadAddress();
            SavingProductHelper.BulkCouponInfo coupon = SavingProductHelper.GetActiveCoupon(Session, Convert.ToString(Session["userid"]));
            if (coupon != null && chkApplyCoupon != null)
            {
                UserPanelCartHelper.CartTotals firstTotals = UserPanelCartHelper.GetTotals(Session);
                chkApplyCoupon.Checked = firstTotals.Subtotal >= coupon.Amount;
            }
            BindSummary();
            ApplyStep();
        }

        LoadCompanyAccounts();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        BindSummary();
        ApplyStep();
        RegisterAddressModalScript();
    }

    void BindSummary()
    {
        UserPanelCartHelper.CartTotals totals = UserPanelCartHelper.GetTotals(Session);
        decimal shipping = totals.Shipping;
        decimal payableProducts = totals.Subtotal;
        decimal discount = 0;
        SavingProductHelper.BulkCouponInfo coupon = SavingProductHelper.GetActiveCoupon(Session, Convert.ToString(Session["userid"]));
        bool hasCoupon = coupon != null;
        if (pnlBulkCoupon != null)
        {
            pnlBulkCoupon.Visible = hasCoupon;
        }

        if (hasCoupon)
        {
            hfCouponRewardId.Value = coupon.Id.ToString();
            hfCouponAmount.Value = coupon.Amount.ToString("0.00");
            hfCouponCode.Value = coupon.CouponCode;
            litCouponCode.Text = coupon.CouponCode;
            litCouponAmount.Text = coupon.Amount.ToString("0.00");
            litCouponMinDp.Text = coupon.Amount.ToString("0.00");
            litCouponPayCode.Text = coupon.CouponCode;
            if (lblCouponMsg != null)
            {
                lblCouponMsg.Text = string.Empty;
            }

            if (chkApplyCoupon != null && chkApplyCoupon.Checked)
            {
                if (totals.Subtotal < coupon.Amount)
                {
                    if (lblCouponMsg != null)
                    {
                        lblCouponMsg.Text = SavingProductHelper.GetCouponMinPurchaseMessage(coupon.Amount);
                    }
                }
                else
                {
                    discount = coupon.Amount;
                    payableProducts = Math.Max(0m, totals.Subtotal - coupon.Amount);
                }
            }
        }

        decimal payable = payableProducts + shipping;
        bool coversFull = hasCoupon && chkApplyCoupon != null && chkApplyCoupon.Checked && discount > 0 && payable <= 0.009m;
        hfCouponCoversFull.Value = coversFull ? "1" : "0";
        if (pnlCouponSummary != null)
        {
            pnlCouponSummary.Visible = discount > 0;
        }
        litCouponDiscount.Text = discount.ToString("0.00");

        litSubtotal.Text = totals.Subtotal.ToString("0.00");
        litShipping.Text = totals.Shipping.ToString("0.00");
        litPayable.Text = payable.ToString("0.00");
        litShipNote.Text = totals.Quote == null ? string.Empty : totals.Quote.Message;
        if (lblQrAmount != null)
        {
            lblQrAmount.Text = "₹" + payable.ToString("0.00");
        }
        ViewState["CheckoutPayableProducts"] = payableProducts;
        ViewState["CheckoutCouponDiscount"] = discount;
        ViewState["CheckoutCartDp"] = totals.Subtotal;
    }

    bool CouponCoversFull()
    {
        return hfCouponCoversFull != null && hfCouponCoversFull.Value == "1";
    }

    bool ShouldApplyCoupon()
    {
        return pnlBulkCoupon != null && pnlBulkCoupon.Visible && chkApplyCoupon != null && chkApplyCoupon.Checked;
    }

    bool ValidateCouponMinDp(bool showAlert)
    {
        if (!ShouldApplyCoupon())
        {
            return true;
        }

        SavingProductHelper.BulkCouponInfo coupon = SavingProductHelper.GetActiveCoupon(Session, Convert.ToString(Session["userid"]));
        if (coupon == null)
        {
            if (showAlert)
            {
                Alert("Coupon is not available.");
            }
            return false;
        }

        decimal couponAmt = coupon.Amount;
        decimal parsedAmt;
        if (decimal.TryParse(hfCouponAmount.Value, out parsedAmt) && parsedAmt > 0)
        {
            couponAmt = parsedAmt;
        }

        decimal cartDp = UserPanelCartHelper.GetTotals(Session).Subtotal;
        if (cartDp < couponAmt)
        {
            if (showAlert)
            {
                Alert(SavingProductHelper.GetCouponMinPurchaseMessage(couponAmt));
            }
            return false;
        }

        return true;
    }

    void ApplyCouponPaymentPlaceholder()
    {
        if (string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            txttransactionid.Text = hfCouponCode.Value;
        }
        if (string.IsNullOrWhiteSpace(HDFilename.Value))
        {
            HDFilename.Value = "bulk-coupon.png";
        }
        EnsureBankSelected();
    }

    protected void chkApplyCoupon_CheckedChanged(object sender, EventArgs e)
    {
        if (chkApplyCoupon != null && chkApplyCoupon.Checked && !ValidateCouponMinDp(true))
        {
            chkApplyCoupon.Checked = false;
        }

        BindSummary();
        ApplyStep();
    }

    void ApplyStep()
    {
        int step = GetStep();
        pnlStepAddress.Visible = step == 1;
        pnlStepPayment.Visible = step == 2;
        pnlStepReview.Visible = step == 3;
        SetStepClass(step1Box, step, 1);
        SetStepClass(step2Box, step, 2);
        SetStepClass(step3Box, step, 3);
        bool coversFull = CouponCoversFull();
        if (pnlCouponPaySkip != null)
        {
            pnlCouponPaySkip.Visible = step == 2 && coversFull;
        }
        if (pnlPaymentDetails != null)
        {
            pnlPaymentDetails.Visible = step != 2 || !coversFull;
        }
        ApplyAddressMode();
    }

    static void SetStepClass(System.Web.UI.HtmlControls.HtmlGenericControl box, int current, int index)
    {
        box.Attributes["class"] = current == index ? "upc-step is-active" : (current > index ? "upc-step is-done" : "upc-step");
    }

    int GetStep()
    {
        int step;
        return int.TryParse(hfStep.Value, out step) ? step : 1;
    }

    protected void AddressMode_Changed(object sender, EventArgs e)
    {
        if (rbProfileAddress.Checked)
        {
            hfShowAddressModal.Value = "0";
            hfHasNewAddress.Value = "0";
        }

        ApplyAddressMode();
    }

    void ApplyAddressMode()
    {
        bool useNew = rbNewAddress.Checked;
        bool hasNew = hfHasNewAddress.Value == "1";
        pnlNewAddress.Visible = true;
        pnlProfileView.Visible = !useNew || !hasNew;
        pnlNewAddressPreview.Visible = useNew && hasNew;
    }

    void RegisterAddressModalScript()
    {
        if (GetStep() != 1)
        {
            hfShowAddressModal.Value = "0";
        }

        string script = hfShowAddressModal.Value == "1" ? "showUpcAddressModal();" : "hideUpcAddressModal();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "upcAddressModal", script, true);
    }

    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCities();
        if (GetStep() == 1 && rbNewAddress.Checked)
        {
            hfShowAddressModal.Value = "1";
        }
    }

    protected void btnOpenAddressModal_Click(object sender, EventArgs e)
    {
        OpenNewAddressModal(true);
    }

    protected void btnEditNewAddress_Click(object sender, EventArgs e)
    {
        OpenNewAddressModal(false);
    }

    protected void btnSaveNewAddress_Click(object sender, EventArgs e)
    {
        hfShowAddressModal.Value = "1";
        rbNewAddress.Checked = true;
        rbProfileAddress.Checked = false;

        if (!ValidateAddress())
        {
            ApplyAddressMode();
            return;
        }

        SaveNewAddressToViewState();
        hfHasNewAddress.Value = "1";
        hfShowAddressModal.Value = "0";
        litNewAddress.Text = Server.HtmlEncode(FormatCurrentAddress());
        ApplyAddressMode();
    }

    protected void btnCancelNewAddress_Click(object sender, EventArgs e)
    {
        hfShowAddressModal.Value = "0";

        if (hfHasNewAddress.Value == "1")
        {
            RestoreNewAddressFields();
            litNewAddress.Text = Server.HtmlEncode(FormatCurrentAddress());
            rbNewAddress.Checked = true;
            rbProfileAddress.Checked = false;
        }
        else if (HasSavedProfile())
        {
            rbProfileAddress.Checked = true;
            rbNewAddress.Checked = false;
            ClearNewAddressForm();
        }
        else
        {
            rbNewAddress.Checked = true;
            rbProfileAddress.Checked = false;
            ClearNewAddressForm();
        }

        ApplyAddressMode();
    }

    void OpenNewAddressModal(bool clearForm)
    {
        rbNewAddress.Checked = true;
        rbProfileAddress.Checked = false;

        if (clearForm)
        {
            ClearNewAddressForm();
            hfHasNewAddress.Value = "0";
        }
        else
        {
            RestoreNewAddressFields();
        }

        hfShowAddressModal.Value = "1";
        ApplyAddressMode();
    }

    protected void btnToPayment_Click(object sender, EventArgs e)
    {
        if (!PrepareSelectedAddress())
        {
            return;
        }

        if (!ValidateAddress())
        {
            return;
        }

        if (!ValidateCouponMinDp(true))
        {
            return;
        }

        hfStep.Value = "2";
        ApplyStep();
    }

    protected void btnBackAddress_Click(object sender, EventArgs e)
    {
        hfStep.Value = "1";
        ApplyStep();
    }

    protected void btnToReview_Click(object sender, EventArgs e)
    {
        if (!ValidateCouponMinDp(true))
        {
            return;
        }

        if (CouponCoversFull())
        {
            ApplyCouponPaymentPlaceholder();
        }
        else if (!ValidatePaymentAndUpload())
        {
            return;
        }

        BindReview();
        hfStep.Value = "3";
        ApplyStep();
    }

    protected void btnBackPayment_Click(object sender, EventArgs e)
    {
        hfStep.Value = "2";
        ApplyStep();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!PrepareSelectedAddress())
        {
            hfStep.Value = "1";
            ApplyStep();
            return;
        }

        if (!ValidateAddress())
        {
            hfStep.Value = "1";
            ApplyStep();
            return;
        }

        if (CouponCoversFull())
        {
            ApplyCouponPaymentPlaceholder();
        }
        else if (string.IsNullOrWhiteSpace(HDFilename.Value) || string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            Alert("Enter transaction ID and upload payment screenshot.");
            hfStep.Value = "2";
            ApplyStep();
            return;
        }

        DataTable cart = UserPanelCartHelper.GetCart(Session);
        UserPanelCartHelper.CartMeta meta = UserPanelCartHelper.GetMeta(Session);
        if (cart == null || cart.Rows.Count == 0 || meta == null)
        {
            Alert("Your cart is empty.");
            Response.Redirect("UserProductCart.aspx");
            return;
        }

        UserPanelCartHelper.CartTotals totals = UserPanelCartHelper.GetTotals(Session);
        if (totals.Subtotal <= 0)
        {
            Alert("Buy any product.");
            return;
        }

        decimal couponAmt = 0;
        decimal.TryParse(hfCouponAmount.Value, out couponAmt);
        bool applyCoupon = ShouldApplyCoupon();
        if (applyCoupon)
        {
            if (!ValidateCouponMinDp(true))
            {
                return;
            }
        }

        decimal payableProducts = totals.Subtotal;
        if (applyCoupon)
        {
            payableProducts = Math.Max(0m, totals.Subtotal - couponAmt);
        }

        string bankId = GetSelectedBankId();
        if (string.IsNullOrWhiteSpace(bankId))
        {
            EnsureBankSelected();
            bankId = GetSelectedBankId();
        }

        if (string.IsNullOrWhiteSpace(bankId))
        {
            Alert("Company bank account is not available. Please contact support.");
            hfStep.Value = "2";
            ApplyStep();
            return;
        }

        UserPanelCartHelper.UpdateUserShipping(
            Session["userid"].ToString(),
            txtaddress.Text.Trim(),
            ddcity.SelectedValue,
            txtareaname.Text.Trim(),
            txtpincode.Text.Trim());

        DataTable purchaseForSp = UserPanelCartHelper.BuildPurchaseProductForSp(cart, meta.FranchiseeId);
        string result = UserPanelCartHelper.AddPurchase(
            Session["userid"].ToString(),
            totals.PurchaseAmount,
            totals.Cgst,
            totals.Sgst,
            totals.Igst,
            payableProducts,
            meta.FranchiseeId,
            meta.PlanType,
            purchaseForSp,
            bankId,
            txttransactionid.Text.Trim(),
            GetPaymentMode(),
            HDFilename.Value);

        HandlePurchaseResult(result, totals.Shipping, meta, applyCoupon, couponAmt, totals.Subtotal, payableProducts);
    }

    void HandlePurchaseResult(string result, decimal shipping, UserPanelCartHelper.CartMeta meta, bool applyCoupon, decimal couponAmt, decimal cartDp, decimal payableProducts)
    {
        if (result == "1")
        {
            ProductWeightHelper.SaveOnLatestUserPurchase(Session["userid"].ToString(), shipping);
            string okMessage = "Purchase Successful";
            if (applyCoupon)
            {
                int rewardId;
                int.TryParse(hfCouponRewardId.Value, out rewardId);
                string userId = Convert.ToString(Session["userid"]);
                string remark = "Purchase DP " + cartDp.ToString("0.00") + " | Discount " + couponAmt.ToString("0.00")
                    + " | Payable " + payableProducts.ToString("0.00");
                string orderNo = SavingProductHelper.GetLatestRepurchaseOrderNo(userId);
                string redeemRes = SavingProductHelper.RedeemBulkCoupon(rewardId, userId, remark, orderNo);
                SavingProductHelper.ClearCouponRedeem(Session);
                if (redeemRes == "t")
                {
                    okMessage = "Purchase successful. Coupon discount Rs. " + couponAmt.ToString("0.00") + " applied on billing.";
                }
                else
                {
                    okMessage = "Purchase successful. Coupon could not be marked redeemed. Please contact admin.";
                }
            }
            UserPanelCartHelper.Clear(Session);
            string catalog = UserPanelCartHelper.GetCatalogUrl(meta);
            ScriptManager.RegisterStartupScript(this, GetType(), "ok", "alert('" + okMessage.Replace("'", "\\'") + "'); window.location='" + catalog.Replace("'", "") + "';", true);
            return;
        }

        if (result == "2")
        {
            Alert("You have insufficient balance.");
        }
        else if (result == "3")
        {
            Alert("Insufficient stock.");
        }
        else if (result == "4")
        {
            Alert("You already topup with this plan.");
        }
        else if (result == "5")
        {
            Alert("Already one request is pending. Please approve/reject first.");
        }
        else if (result == "6")
        {
            Alert("You can purchase only 1000/2000/3000/4000 for Freedom Plan.");
        }
        else if (result == "7")
        {
            Alert("You can purchase only 600/1200/1800/2400 for Unity Plan.");
        }
        else if (result == "8")
        {
            Alert("You can purchase only 2000 for Global Plan.");
        }
        else if (result == "9")
        {
            Alert("First purchase minimum should be 26 Point.");
        }
        else if (!string.IsNullOrWhiteSpace(result) && result.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
        {
            Alert(result);
        }
        else
        {
            Alert("Unknown error. Please try again.");
        }
    }

    bool ValidateAddress()
    {
        if (string.IsNullOrWhiteSpace(txtaddress.Text))
        {
            Alert("Enter address.");
            return false;
        }

        if (ddstate.SelectedValue == "0" || string.IsNullOrWhiteSpace(ddstate.SelectedValue))
        {
            Alert("Select state.");
            return false;
        }

        if (ddcity.SelectedValue == "0" || string.IsNullOrWhiteSpace(ddcity.SelectedValue))
        {
            Alert("Select city.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtpincode.Text))
        {
            Alert("Enter pincode.");
            return false;
        }

        return true;
    }

    bool ValidatePaymentAndUpload()
    {
        EnsureBankSelected();
        string bankId = GetSelectedBankId();
        if (GetSelectedPaymentMethod() == "qr" && FilterQrAccounts(GetBankAccounts()).Rows.Count == 0)
        {
            Alert("QR code is not available. Please use online bank transfer or contact support.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(bankId))
        {
            Alert("Company bank account is not available. Please contact support.");
            return false;
        }

        hfSelectedBankId.Value = bankId;
        ViewState["CheckoutBankId"] = bankId;
        Session["CheckoutBankId"] = bankId;

        if (string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            Alert("Please enter transaction ID.");
            return false;
        }

        string uploaded = UploadImage();
        if (!string.IsNullOrWhiteSpace(uploaded))
        {
            HDFilename.Value = uploaded;
        }

        if (string.IsNullOrWhiteSpace(HDFilename.Value))
        {
            Alert("Upload payment screenshot.");
            return false;
        }

        return true;
    }

    void BindReview()
    {
        litReviewAddress.Text = Server.HtmlEncode(FormatCurrentAddress());
        litReviewMode.Text = GetPaymentMode();
        litReviewTxn.Text = txttransactionid.Text.Trim();
        rptReview.DataSource = UserPanelCartHelper.GetCart(Session);
        rptReview.DataBind();
        decimal discount = 0;
        object discountObj = ViewState["CheckoutCouponDiscount"];
        if (discountObj != null)
        {
            decimal.TryParse(Convert.ToString(discountObj), out discount);
        }
        if (pnlReviewCoupon != null)
        {
            pnlReviewCoupon.Visible = discount > 0;
            litReviewCouponDiscount.Text = discount.ToString("0.00");
        }
        if (!string.IsNullOrWhiteSpace(HDFilename.Value) && !string.Equals(HDFilename.Value, "bulk-coupon.png", StringComparison.OrdinalIgnoreCase))
        {
            imgReviewReceipt.Visible = true;
            imgReviewReceipt.ImageUrl = ResolveUrl("~/ProductImage/" + HDFilename.Value);
        }
        else
        {
            imgReviewReceipt.Visible = false;
        }
    }

    string GetPaymentMode()
    {
        if (CouponCoversFull())
        {
            return "Bulk Coupon";
        }
        return GetSelectedPaymentMethod() == "qr" ? "QR" : "Online";
    }

    string GetSelectedPaymentMethod()
    {
        string method = hfPaymentMethod != null ? (hfPaymentMethod.Value ?? string.Empty).Trim().ToLowerInvariant() : "online";
        return method == "qr" ? "qr" : "online";
    }

    string GetSelectedBankId()
    {
        string bankId = NormalizeBankId(hfSelectedBankId != null ? hfSelectedBankId.Value : null);
        if (!string.IsNullOrWhiteSpace(bankId))
        {
            return bankId;
        }

        bankId = NormalizeBankId(ViewState["CheckoutBankId"]);
        if (!string.IsNullOrWhiteSpace(bankId))
        {
            return bankId;
        }

        bankId = NormalizeBankId(Session["CheckoutBankId"]);
        if (!string.IsNullOrWhiteSpace(bankId))
        {
            return bankId;
        }

        bankId = GetCheckedRepeaterBankId();
        if (!string.IsNullOrWhiteSpace(bankId))
        {
            return bankId;
        }

        bankId = GetRepeaterBankId(0);
        if (!string.IsNullOrWhiteSpace(bankId))
        {
            return bankId;
        }

        DataTable dt = GetBankAccounts();
        if (GetSelectedPaymentMethod() == "qr")
        {
            bankId = GetFirstAccountId(FilterQrAccounts(dt));
            if (!string.IsNullOrWhiteSpace(bankId))
            {
                return bankId;
            }
        }

        return GetFirstAccountId(dt);
    }

    void EnsureBankSelected()
    {
        string bankId = GetSelectedBankId();
        if (string.IsNullOrWhiteSpace(bankId))
        {
            bankId = GetFirstAccountId(GetBankAccounts());
        }

        if (string.IsNullOrWhiteSpace(bankId))
        {
            return;
        }

        hfSelectedBankId.Value = bankId;
        ViewState["CheckoutBankId"] = bankId;
        Session["CheckoutBankId"] = bankId;
    }

    DataTable GetBankAccounts()
    {
        DataTable dt = null;
        try
        {
            dt = objbank.getBankAccountList();
        }
        catch
        {
            dt = null;
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }

        try
        {
            dt = objaccount.getCompanyAccountDetail();
        }
        catch
        {
            dt = null;
        }

        return dt ?? new DataTable();
    }

    static string NormalizeBankId(object value)
    {
        string bankId = Convert.ToString(value);
        if (string.IsNullOrWhiteSpace(bankId) || bankId == "0")
        {
            return string.Empty;
        }

        return bankId.Trim();
    }

    string GetCheckedRepeaterBankId()
    {
        if (rptBankAccounts == null)
        {
            return string.Empty;
        }

        foreach (RepeaterItem item in rptBankAccounts.Items)
        {
            RadioButton rbBank = item.FindControl("rbBank") as RadioButton;
            HiddenField hfBankId = item.FindControl("hfBankId") as HiddenField;
            if (rbBank != null && rbBank.Checked && hfBankId != null)
            {
                return NormalizeBankId(hfBankId.Value);
            }
        }

        return string.Empty;
    }

    string GetRepeaterBankId(int itemIndex)
    {
        if (rptBankAccounts == null || rptBankAccounts.Items.Count <= itemIndex)
        {
            return string.Empty;
        }

        HiddenField hfBankId = rptBankAccounts.Items[itemIndex].FindControl("hfBankId") as HiddenField;
        return hfBankId == null ? string.Empty : NormalizeBankId(hfBankId.Value);
    }

    static string GetFirstAccountId(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return string.Empty;
        }

        DataRow row = dt.Rows[0];
        string id = GetValue(row, "id", "Id", "ID", "BankAccountId", "AccountId");
        if (!string.IsNullOrWhiteSpace(id))
        {
            return NormalizeBankId(id);
        }

        return NormalizeBankId(row[0]);
    }

    string FormatCurrentAddress()
    {
        string city = ddcity.SelectedItem == null ? string.Empty : ddcity.SelectedItem.Text;
        string state = ddstate.SelectedItem == null ? string.Empty : ddstate.SelectedItem.Text;
        if (city == "Select City") city = string.Empty;
        if (state == "Select State") state = string.Empty;
        return FormatAddress(txtaddress.Text, txtareaname.Text, city, state, txtpincode.Text);
    }

    bool PrepareSelectedAddress()
    {
        if (rbNewAddress.Checked)
        {
            if (hfHasNewAddress.Value != "1")
            {
                Alert("Please add and save a delivery address.");
                hfShowAddressModal.Value = "1";
                ApplyAddressMode();
                return false;
            }

            RestoreNewAddressFields();
            return true;
        }

        if (!HasSavedProfile())
        {
            Alert("Please add and save a delivery address.");
            OpenNewAddressModal(true);
            return false;
        }

        RestoreProfileFields();
        return true;
    }

    bool HasSavedProfile()
    {
        return HasDisplayableProfile() && IsValidId(Convert.ToString(ViewState["ProfileCityId"]));
    }

    bool HasDisplayableProfile()
    {
        return !string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ProfileAddress"]))
            || !string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ProfileArea"]))
            || !string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ProfilePincode"]));
    }

    static bool IsValidId(string value)
    {
        string id = NormalizeId(value);
        return !string.IsNullOrWhiteSpace(id) && id != "0";
    }

    static string NormalizeId(string value)
    {
        string text = (value ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        int id;
        if (int.TryParse(text, out id))
        {
            return id.ToString();
        }

        decimal dec;
        if (decimal.TryParse(text, out dec))
        {
            return ((int)dec).ToString();
        }

        return text;
    }

    void SaveNewAddressToViewState()
    {
        ViewState["NewAddress"] = txtaddress.Text.Trim();
        ViewState["NewArea"] = txtareaname.Text.Trim();
        ViewState["NewPincode"] = txtpincode.Text.Trim();
        ViewState["NewStateId"] = ddstate.SelectedValue;
        ViewState["NewCityId"] = ddcity.SelectedValue;
    }

    void RestoreNewAddressFields()
    {
        FillFields(
            Convert.ToString(ViewState["NewAddress"]),
            Convert.ToString(ViewState["NewArea"]),
            Convert.ToString(ViewState["NewPincode"]),
            Convert.ToString(ViewState["NewStateId"]),
            Convert.ToString(ViewState["NewCityId"]));
    }

    void ClearNewAddressForm()
    {
        txtaddress.Text = string.Empty;
        txtareaname.Text = string.Empty;
        txtpincode.Text = string.Empty;
        if (ddstate.Items.Count > 0)
        {
            ddstate.ClearSelection();
            ddstate.SelectedIndex = 0;
        }
        LoadCities();
    }

    void LoadAddress()
    {
        ClearNewAddressForm();
        hfHasNewAddress.Value = "0";
        hfShowAddressModal.Value = "0";
        rbProfileAddress.Checked = true;
        rbNewAddress.Checked = false;

        DataRow row = GetProfileAddressRow();
        if (row == null)
        {
            litProfileAddress.Text = "No profile address found. Please add a new delivery address.";
            ApplyAddressMode();
            return;
        }

        BindProfileRow(row);
        if (HasDisplayableProfile())
        {
            litProfileAddress.Text = Server.HtmlEncode(FormatStoredAddress(
                Convert.ToString(ViewState["ProfileAddress"]),
                Convert.ToString(ViewState["ProfileArea"]),
                Convert.ToString(ViewState["ProfilePincode"]),
                Convert.ToString(ViewState["ProfileStateId"]),
                Convert.ToString(ViewState["ProfileCityId"])));
        }
        else
        {
            litProfileAddress.Text = "No profile address found. Please add a new delivery address.";
        }

        ApplyAddressMode();
    }

    DataRow GetProfileAddressRow()
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (RowHasAddress(dt))
        {
            return dt.Rows[0];
        }

        clsUser objUser = new clsUser();
        objUser.UserId = Session["userid"].ToString();
        dt = objUser.getUserDetail(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }

        return null;
    }

    bool RowHasAddress(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return false;
        }

        DataRow row = dt.Rows[0];
        return !string.IsNullOrWhiteSpace(GetAddressValue(row))
            || !string.IsNullOrWhiteSpace(GetValue(row, "AreaName", "areaname"))
            || !string.IsNullOrWhiteSpace(GetValue(row, "Pincode", "pincode"));
    }

    void BindProfileRow(DataRow row)
    {
        ViewState["ProfileAddress"] = GetAddressValue(row);
        ViewState["ProfileArea"] = GetValue(row, "AreaName", "areaname", "Area");
        ViewState["ProfilePincode"] = GetValue(row, "Pincode", "pincode");
        ViewState["ProfileStateId"] = NormalizeId(GetValue(row, "stateid", "StateId", "StateID"));
        ViewState["ProfileCityId"] = NormalizeId(GetValue(row, "cityid", "CityId", "CityID"));
        ViewState["ProfileCityName"] = GetValue(row, "Cityname", "CityName", "cityname");
        ViewState["ProfileStateName"] = GetValue(row, "Statename", "StateName", "statename");
    }

    static string GetAddressValue(DataRow row)
    {
        return GetValue(row, "address", "Address", "Addressfirst", "AddressFirst", "Address1");
    }

    string FormatStoredAddress(string address, string area, string pincode, string stateId, string cityId)
    {
        string stateName = Convert.ToString(ViewState["ProfileStateName"]);
        string cityName = Convert.ToString(ViewState["ProfileCityName"]);
        ListItem stateItem = !IsValidId(stateId) ? null : ddstate.Items.FindByValue(NormalizeId(stateId));
        if (stateItem != null && stateItem.Text != "Select State")
        {
            stateName = stateItem.Text;
        }

        if (IsValidId(stateId))
        {
            objCState.StateId = NormalizeId(stateId);
            DataTable cities = objCState.getCity(objCState);
            if (cities != null)
            {
                string normalizedCityId = NormalizeId(cityId);
                foreach (DataRow cityRow in cities.Rows)
                {
                    if (string.Equals(NormalizeId(GetValue(cityRow, "CityID", "CityId")), normalizedCityId, StringComparison.OrdinalIgnoreCase))
                    {
                        cityName = GetValue(cityRow, "CityName");
                        break;
                    }
                }
            }
        }

        return FormatAddress(address, area, cityName, stateName, pincode);
    }

    void RestoreProfileFields()
    {
        FillFields(
            Convert.ToString(ViewState["ProfileAddress"]),
            Convert.ToString(ViewState["ProfileArea"]),
            Convert.ToString(ViewState["ProfilePincode"]),
            Convert.ToString(ViewState["ProfileStateId"]),
            Convert.ToString(ViewState["ProfileCityId"]));
        litProfileAddress.Text = Server.HtmlEncode(FormatCurrentAddress());
    }

    void FillFields(string address, string area, string pincode, string stateId, string cityId)
    {
        txtaddress.Text = address ?? string.Empty;
        txtareaname.Text = area ?? string.Empty;
        txtpincode.Text = pincode ?? string.Empty;
        string stateIdNorm = NormalizeId(stateId);
        string cityIdNorm = NormalizeId(cityId);
        if (IsValidId(stateIdNorm) && ddstate.Items.FindByValue(stateIdNorm) != null)
        {
            ddstate.ClearSelection();
            ddstate.SelectedValue = stateIdNorm;
            LoadCities();
            if (IsValidId(cityIdNorm) && ddcity.Items.FindByValue(cityIdNorm) != null)
            {
                ddcity.ClearSelection();
                ddcity.SelectedValue = cityIdNorm;
            }
        }
        else
        {
            if (ddstate.Items.Count > 0)
            {
                ddstate.ClearSelection();
                ddstate.SelectedIndex = 0;
            }
            LoadCities();
        }
    }

    void LoadStates()
    {
        ddstate.Items.Clear();
        objCState.CountryId = "1";
        DataTable dt = objCState.getState(objCState);
        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ddstate.Items.Insert(0, new ListItem("Select State", "0"));
        LoadCities();
    }

    void LoadCities()
    {
        ddcity.Items.Clear();
        objCState.StateId = ddstate.SelectedValue;
        DataTable dt = objCState.getCity(objCState);
        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ddcity.Items.Insert(0, new ListItem("Select City", "0"));
    }

    DataTable GetUserAddressDetail(string userId)
    {
        try
        {
            ObjData.StartConnection();
            SqlParameter[] parameter = { new SqlParameter("@UserId", userId) };
            return ObjData.RunDataTableProcedure("sp_getuseraddressdetail", parameter);
        }
        catch
        {
            return new DataTable();
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    void LoadCompanyAccounts()
    {
        DataTable dt = GetBankAccounts();
        bool hasBanks = dt != null && dt.Rows.Count > 0;
        DataTable qrDt = FilterQrAccounts(dt);
        bool hasQr = qrDt != null && qrDt.Rows.Count > 0;

        pnlNoCompanyAccount.Visible = !hasBanks;
        pnlCompanyAccount.Visible = hasBanks;
        pnlQrPayment.Visible = hasQr;
        pnlNoQr.Visible = !hasQr;

        if (hasBanks)
        {
            rptBankAccounts.DataSource = dt;
            rptBankAccounts.DataBind();
        }
        else
        {
            rptBankAccounts.DataSource = null;
            rptBankAccounts.DataBind();
        }

        if (hasQr)
        {
            rptQrAccounts.DataSource = qrDt;
            rptQrAccounts.DataBind();
        }
        else
        {
            rptQrAccounts.DataSource = null;
            rptQrAccounts.DataBind();
        }

        EnsureBankSelected();
    }

    DataTable FilterQrAccounts(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return new DataTable();
        }

        DataTable qrDt = dt.Clone();
        foreach (DataRow row in dt.Rows)
        {
            if (!string.IsNullOrWhiteSpace(GetValue(row, "BranchName", "branchname")))
            {
                qrDt.ImportRow(row);
            }
        }

        return qrDt;
    }

    protected void rptBankAccounts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {
            return;
        }

        RadioButton rbBank = e.Item.FindControl("rbBank") as RadioButton;
        HiddenField hfBankId = e.Item.FindControl("hfBankId") as HiddenField;
        if (rbBank == null || hfBankId == null)
        {
            return;
        }

        string bankId = hfBankId.Value;
        rbBank.Checked = bankId == hfSelectedBankId.Value;
        rbBank.Attributes["onclick"] = string.Format(
            "document.getElementById('{0}').value='{1}';",
            hfSelectedBankId.ClientID,
            bankId.Replace("'", "\\'"));
    }

    protected string GetBankField(object dataItem, params string[] columnNames)
    {
        DataRowView row = dataItem as DataRowView;
        if (row == null)
        {
            return string.Empty;
        }

        foreach (string columnName in columnNames)
        {
            if (row.Row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                return Convert.ToString(row[columnName]).Trim();
            }
        }

        return string.Empty;
    }

    protected string GetQrImageUrl(object dataItem)
    {
        string fileName = GetBankField(dataItem, "BranchName", "branchname");
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return ResolveUrl("~/ProductImage/noimage.png");
        }

        return ResolveUrl("~/ProductImage/" + fileName);
    }

    protected string MaskAccountNo(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
        {
            return string.Empty;
        }

        string trimmed = accountNo.Trim();
        if (trimmed.Length <= 4)
        {
            return trimmed;
        }

        return trimmed.Substring(trimmed.Length - 4);
    }

    string UploadImage()
    {
        HttpPostedFile postedFile = GetPostedPaymentFile();
        if (postedFile == null)
        {
            return string.Empty;
        }

        string extension = Path.GetExtension(postedFile.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = ".jpg";
        }

        extension = extension.ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            return string.Empty;
        }

        string uploadFolder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string imagename = DateTime.Now.Ticks + extension;
        postedFile.SaveAs(Path.Combine(uploadFolder, imagename));
        return imagename;
    }

    HttpPostedFile GetPostedPaymentFile()
    {
        if (ImageUpload != null && ImageUpload.HasFile)
        {
            return ImageUpload.PostedFile;
        }

        if (ImageUpload != null)
        {
            string key = ImageUpload.UniqueID;
            if (!string.IsNullOrEmpty(key) && Request.Files[key] != null && Request.Files[key].ContentLength > 0)
            {
                return Request.Files[key];
            }
        }

        return null;
    }

    static string GetValue(DataRow row, params string[] names)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (string name in names)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                if (string.Equals(column.ColumnName, name, StringComparison.OrdinalIgnoreCase)
                    && row[column] != DBNull.Value)
                {
                    return Convert.ToString(row[column]).Trim();
                }
            }
        }

        return string.Empty;
    }

    static string FormatAddress(string address, string area, string city, string state, string pincode)
    {
        StringBuilder sb = new StringBuilder();
        AppendLine(sb, address);
        AppendLine(sb, area);
        string location = Join(city, state);
        if (!string.IsNullOrWhiteSpace(pincode))
        {
            location = string.IsNullOrWhiteSpace(location) ? pincode : location + " - " + pincode;
        }

        AppendLine(sb, location);
        return sb.ToString().Trim();
    }

    static void AppendLine(StringBuilder sb, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(value.Trim());
        }
    }

    static string Join(string a, string b)
    {
        if (string.IsNullOrWhiteSpace(a)) return b ?? string.Empty;
        if (string.IsNullOrWhiteSpace(b)) return a;
        return a + ", " + b;
    }

    void Alert(string message)
    {
        string safe = (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" + safe + "');", true);
    }
}

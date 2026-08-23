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
            LoadCompanyAccounts();
            BindSummary();
            ApplyStep();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        BindSummary();
        ApplyStep();
    }

    void BindSummary()
    {
        UserPanelCartHelper.CartTotals totals = UserPanelCartHelper.GetTotals(Session);
        litSubtotal.Text = totals.Subtotal.ToString("0.00");
        litShipping.Text = totals.Shipping.ToString("0.00");
        litPayable.Text = totals.Payable.ToString("0.00");
        litShipNote.Text = totals.Quote == null ? string.Empty : totals.Quote.Message;
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
            RestoreProfileFields();
        }

        ApplyAddressMode();
    }

    void ApplyAddressMode()
    {
        bool useNew = rbNewAddress.Checked;
        pnlNewAddress.Visible = useNew;
        pnlProfileView.Visible = !useNew;
    }

    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCities();
    }

    protected void ddbankaccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCompanyAccountDetail();
    }

    protected void btnToPayment_Click(object sender, EventArgs e)
    {
        if (rbProfileAddress.Checked)
        {
            RestoreProfileFields();
        }

        if (!ValidateAddress())
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
        if (!ValidatePaymentAndUpload())
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
        if (rbProfileAddress.Checked)
        {
            RestoreProfileFields();
        }

        if (!ValidateAddress())
        {
            hfStep.Value = "1";
            ApplyStep();
            return;
        }

        if (string.IsNullOrWhiteSpace(HDFilename.Value) || string.IsNullOrWhiteSpace(txttransactionid.Text))
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

        string bankId = ddbankaccount.SelectedValue;
        if (string.IsNullOrWhiteSpace(bankId) || bankId == "0")
        {
            Alert("Select a company bank account.");
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
            totals.Subtotal,
            meta.FranchiseeId,
            meta.PlanType,
            purchaseForSp,
            bankId,
            txttransactionid.Text.Trim(),
            GetPaymentMode(),
            HDFilename.Value);

        HandlePurchaseResult(result, totals.Shipping, meta);
    }

    void HandlePurchaseResult(string result, decimal shipping, UserPanelCartHelper.CartMeta meta)
    {
        if (result == "1")
        {
            ProductWeightHelper.SaveOnLatestUserPurchase(Session["userid"].ToString(), shipping);
            UserPanelCartHelper.Clear(Session);
            string catalog = UserPanelCartHelper.GetCatalogUrl(meta);
            ScriptManager.RegisterStartupScript(this, GetType(), "ok", "alert('Purchase Successful'); window.location='" + catalog.Replace("'", "") + "';", true);
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
        if (pnlBankSelectWrap.Visible && (ddbankaccount.SelectedValue == "0" || string.IsNullOrWhiteSpace(ddbankaccount.SelectedValue)))
        {
            Alert("Select a company bank account.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            Alert("Enter UTR / Transaction ID.");
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
        if (!string.IsNullOrWhiteSpace(HDFilename.Value))
        {
            imgReviewReceipt.Visible = true;
            imgReviewReceipt.ImageUrl = ResolveUrl("~/ProductImage/" + HDFilename.Value);
        }
    }

    string GetPaymentMode()
    {
        if (rbRtgs.Checked) return "RTGS";
        if (rbNeft.Checked) return "NEFT";
        if (rbImps.Checked) return "IMPS";
        return "UPI";
    }

    string FormatCurrentAddress()
    {
        string city = ddcity.SelectedItem == null ? string.Empty : ddcity.SelectedItem.Text;
        string state = ddstate.SelectedItem == null ? string.Empty : ddstate.SelectedItem.Text;
        if (city == "Select City") city = string.Empty;
        if (state == "Select State") state = string.Empty;
        return FormatAddress(txtaddress.Text, txtareaname.Text, city, state, txtpincode.Text);
    }

    void LoadAddress()
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            litProfileAddress.Text = "No profile address found. Please add a new delivery address.";
            rbNewAddress.Checked = true;
            rbProfileAddress.Checked = false;
            ApplyAddressMode();
            return;
        }

        DataRow row = dt.Rows[0];
        ViewState["ProfileAddress"] = GetValue(row, "address", "Address");
        ViewState["ProfileArea"] = GetValue(row, "AreaName");
        ViewState["ProfilePincode"] = GetValue(row, "Pincode");
        ViewState["ProfileStateId"] = GetValue(row, "stateid", "StateId");
        ViewState["ProfileCityId"] = GetValue(row, "cityid", "CityId");

        string shipAddress = GetValue(row, "Shippingaddress", "ShippingAddress");
        bool hasProfile = !string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ProfileAddress"]))
            && !string.IsNullOrWhiteSpace(Convert.ToString(ViewState["ProfileCityId"]));
        bool hasShipping = !string.IsNullOrWhiteSpace(shipAddress);

        if (hasProfile)
        {
            RestoreProfileFields();
            litProfileAddress.Text = Server.HtmlEncode(FormatCurrentAddress());
        }
        else
        {
            litProfileAddress.Text = "No complete profile address found.";
            rbNewAddress.Checked = true;
            rbProfileAddress.Checked = false;
        }

        if (!hasProfile && hasShipping)
        {
            FillFields(
                shipAddress,
                GetValue(row, "ShippingAreaName"),
                GetValue(row, "ShippingPincode"),
                GetValue(row, "Shippingstateid", "ShippingStateId"),
                GetValue(row, "ShippingCityId"));
        }

        ApplyAddressMode();
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
        if (!string.IsNullOrWhiteSpace(stateId) && ddstate.Items.FindByValue(stateId) != null)
        {
            ddstate.SelectedValue = stateId;
            LoadCities();
            if (!string.IsNullOrWhiteSpace(cityId) && ddcity.Items.FindByValue(cityId) != null)
            {
                ddcity.SelectedValue = cityId;
            }
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
        ddbankaccount.Items.Clear();
        DataTable dt = objaccount.getCompanyAccountDetail();
        if (dt == null || dt.Rows.Count == 0)
        {
            pnlNoCompanyAccount.Visible = true;
            pnlCompanyAccount.Visible = false;
            pnlBankSelectWrap.Visible = false;
            return;
        }

        pnlNoCompanyAccount.Visible = false;
        pnlCompanyAccount.Visible = true;
        pnlBankSelectWrap.Visible = dt.Rows.Count > 1;
        ddbankaccount.DataSource = dt;
        ddbankaccount.DataTextField = "accno2";
        ddbankaccount.DataValueField = "id";
        ddbankaccount.DataBind();
        if (dt.Rows.Count > 1)
        {
            ddbankaccount.Items.Insert(0, new ListItem("Select Account", "0"));
        }

        LoadCompanyAccountDetail();
    }

    void LoadCompanyAccountDetail()
    {
        if (ddbankaccount.Items.Count == 0)
        {
            pnlNoCompanyAccount.Visible = true;
            pnlCompanyAccount.Visible = false;
            return;
        }

        if (pnlBankSelectWrap.Visible && ddbankaccount.SelectedValue == "0")
        {
            litAccountHolder.Text = "-";
            litAccountNo.Text = "-";
            litBankName.Text = "-";
            litIfscCode.Text = "-";
            imgPaymentQr.ImageUrl = ResolveUrl("~/ProductImage/noimage.png");
            return;
        }

        string accountId = ddbankaccount.SelectedValue;
        if (string.IsNullOrWhiteSpace(accountId) || accountId == "0")
        {
            accountId = ddbankaccount.Items[0].Value;
        }

        objaccount.Id = accountId;
        DataTable dt = objaccount.getCompanyAccountDetailById(objaccount);
        if (dt == null || dt.Rows.Count == 0)
        {
            pnlNoCompanyAccount.Visible = true;
            pnlCompanyAccount.Visible = false;
            return;
        }

        DataRow row = dt.Rows[0];
        litAccountHolder.Text = GetValue(row, "AccountHolderName");
        litAccountNo.Text = GetValue(row, "AccountNo", "accountno");
        litBankName.Text = GetValue(row, "BankName");
        litIfscCode.Text = GetValue(row, "IFSCCode");
        string qrImage = GetValue(row, "BranchName");
        imgPaymentQr.ImageUrl = string.IsNullOrWhiteSpace(qrImage)
            ? ResolveUrl("~/ProductImage/noimage.png")
            : ResolveUrl("~/ProductImage/" + qrImage);
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

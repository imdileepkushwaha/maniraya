using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class user_VirtualFranchisePlanPurchase : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    public static bool CheckOnlineTransactionId(string onlineTransactionId)
    {
        if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["userid"] == null)
        {
            return false;
        }

        return VirtualFranchiseHelper.IsOnlineTransactionIdUsed(onlineTransactionId);
    }

    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        Page.Form.Enctype = "multipart/form-data";
        try { VirtualFranchiseHelper.EnsureSchema(); }
        catch { }
        try { VirtualFranchiseHelper.ProcessDueRoi(); }
        catch { }

        if (!IsPostBack)
        {
            txtuserid.Text = Session["userid"].ToString();
            LoadUserName();
            LoadPlans();
            LoadCompanyAccounts();
            ApplyPaymentMethodVisibility();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ApplyPaymentMethodVisibility();
    }

    bool IsOnlinePayment()
    {
        return rbOnlinePayment.Checked;
    }

    string GetPaymentMethod()
    {
        return rbCashPayment.Checked ? "Cash" : "Online";
    }

    void ApplyPaymentMethodVisibility()
    {
        bool isOnline = IsOnlinePayment();
        pnlOnlinePaymentSection.Style["display"] = isOnline ? "" : "none";
        pnlCashPaymentInfo.Style["display"] = isOnline ? "none" : "";
    }

    void LoadUserName()
    {
        objUser.UserId = txtuserid.Text;
        DataTable dt = objUser.getUserName(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtusername.Text = Convert.ToString(dt.Rows[0]["username"]);
        }
        else
        {
            txtusername.Text = "";
        }
    }

    void LoadPlans()
    {
        ddPlan.Items.Clear();
        DataTable dt = VirtualFranchiseHelper.GetActivePlans();
        ddPlan.Items.Add(new ListItem("Select Plan", "0"));
        if (dt == null)
        {
            return;
        }

        foreach (DataRow row in dt.Rows)
        {
            string id = Convert.ToString(row["id"]);
            string name = Convert.ToString(row["plan_planname"]);
            decimal amount = 0m;
            decimal.TryParse(Convert.ToString(row["plan_amount"]), out amount);
            ddPlan.Items.Add(new ListItem(name + " - Rs. " + amount.ToString("N0"), id));
        }

        BindSelectedPlan();
    }

    protected void ddPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSelectedPlan();
    }

    void BindSelectedPlan()
    {
        DataTable dt = VirtualFranchiseHelper.GetActivePlans();
        DataRow selected = null;
        if (dt != null && ddPlan.SelectedValue != "0")
        {
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(row["id"]) == ddPlan.SelectedValue)
                {
                    selected = row;
                    break;
                }
            }
        }

        if (selected == null)
        {
            litPlanName.Text = "Select a plan";
            litAmount.Text = "0.00";
            litRoi.Text = "0";
            litMonthly.Text = "0.00";
            litTotal.Text = "0.00";
            txtamount.Text = "";
            txtmonthlyroi.Text = "";
            txttotalcashback.Text = "";
            return;
        }

        string planName = Convert.ToString(selected["plan_planname"]);
        decimal amount = 0m;
        decimal roi = 0m;
        int roiTime = 40;
        decimal.TryParse(Convert.ToString(selected["plan_amount"]), out amount);
        decimal.TryParse(Convert.ToString(selected["roi"]), out roi);
        int.TryParse(Convert.ToString(selected["roitime"]), out roiTime);
        if (roiTime <= 0) roiTime = 40;

        decimal monthly = Math.Round(amount * roi / 100m, 2);
        decimal total = Math.Round(monthly * roiTime, 2);

        litPlanName.Text = planName;
        litAmount.Text = amount.ToString("N2");
        litRoi.Text = roi.ToString("0.##");
        litMonthly.Text = monthly.ToString("N2");
        litTotal.Text = total.ToString("N2");
        txtamount.Text = amount.ToString("0.##");
        txtmonthlyroi.Text = monthly.ToString("0.##");
        txttotalcashback.Text = total.ToString("0.##");
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
        litAccountHolder.Text = Convert.ToString(row["AccountHolderName"]);
        litAccountNo.Text = Convert.ToString(row["AccountNo"]);
        litBankName.Text = Convert.ToString(row["BankName"]);
        litIfscCode.Text = Convert.ToString(row["IFSCCode"]);

        string qrImage = Convert.ToString(row["BranchName"]);
        imgPaymentQr.ImageUrl = string.IsNullOrWhiteSpace(qrImage)
            ? ResolveUrl("~/ProductImage/noimage.png")
            : ResolveUrl("~/ProductImage/" + qrImage);
    }

    protected void ddbankaccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCompanyAccountDetail();
    }

    public string UploadImage()
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

        for (int i = 0; i < Request.Files.Count; i++)
        {
            HttpPostedFile file = Request.Files[i];
            if (file != null && file.ContentLength > 0 && !string.IsNullOrWhiteSpace(file.FileName))
            {
                return file;
            }
        }

        return null;
    }

    bool HasPaymentScreenshot()
    {
        return GetPostedPaymentFile() != null;
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddPlan.SelectedValue == "0")
        {
            ShowAlert("Select Plan");
            return;
        }

        string imageName = string.Empty;
        string utr = string.Empty;

        if (IsOnlinePayment())
        {
            if (string.IsNullOrWhiteSpace(txttransactionid.Text))
            {
                ShowAlert("Enter UTR No / Transaction ID.");
                return;
            }

            if (!HasPaymentScreenshot())
            {
                ShowAlert("Please upload payment screenshot.");
                return;
            }

            imageName = UploadImage();
            if (string.IsNullOrWhiteSpace(imageName))
            {
                ShowAlert("Invalid payment screenshot. Please upload JPG, PNG, WEBP or GIF image.");
                return;
            }

            utr = txttransactionid.Text.Trim();
            if (VirtualFranchiseHelper.IsOnlineTransactionIdUsed(utr))
            {
                ShowAlert("This Transaction Id already used");
                return;
            }
        }
        else
        {
            utr = "CASH-" + DateTime.Now.Ticks;
        }

        int planId;
        if (!int.TryParse(ddPlan.SelectedValue, out planId) || planId <= 0)
        {
            ShowAlert("Select Plan");
            return;
        }

        string res = VirtualFranchiseHelper.ExecuteScalarProc("sp_add_VirtualFranchiseRequest", new[]
        {
            new SqlParameter("@UserId", txtuserid.Text.Trim()),
            new SqlParameter("@PlanId", planId),
            new SqlParameter("@PaymentMethod", GetPaymentMethod()),
            new SqlParameter("@OnlineTransactionId", utr),
            new SqlParameter("@ImageName", imageName),
            new SqlParameter("@EntryBy", Session["userid"].ToString())
        });

        if (res == "t")
        {
            ShowAlert(IsOnlinePayment()
                ? "Virtual Franchise plan request submitted successfully."
                : "Cash plan request sent to admin successfully.");
            txttransactionid.Text = "";
            ddPlan.SelectedIndex = 0;
            BindSelectedPlan();
        }
        else if (res == "f")
        {
            ShowAlert("Another request is already pending.");
        }
        else if (res == "u")
        {
            ShowAlert("This Transaction Id already used");
        }
        else if (res == "n")
        {
            ShowAlert("Invalid plan or user. Please try again.");
        }
        else
        {
            ShowAlert("Unable to submit plan request. Please try again.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

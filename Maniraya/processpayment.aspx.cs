using BusinessLogicTier;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class processpayment : System.Web.UI.Page
{
    DataTable PurchaseDt;
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    clsBank objbank = new clsBank();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            loadaddcartitem(Session["userid"].ToString());
            loadaddress(Session["userid"].ToString());
        }

        LoadBankAccounts();
    }

    void loadaddcartitem(string id)
    {
        objState.UserId = id;
        DataTable Dt = objState.getCartItems(objState);
        if (Dt.Rows.Count > 0)
        {
            CalculateSummary(Dt);
        }
    }

    void loadaddress(string id)
    {
        DataTable Dt = objuser.getUsercartaddress(id);
        if (Dt.Rows.Count > 0)
        {
            Lblusername.Text = Dt.Rows[0]["UserName"].ToString();
            Lbladdress.Text = Dt.Rows[0]["Addressfirst"].ToString() + " " + Dt.Rows[0]["AddressSecond"].ToString();
            Lblcity.Text = Dt.Rows[0]["CityName"].ToString() + " ,";
            LblPincode.Text = Dt.Rows[0]["Pincode"].ToString();
            Lblmobile.Text = Dt.Rows[0]["mobile"].ToString();
        }
    }

    void LoadBankAccounts()
    {
        DataTable dt = objbank.getBankAccountList();
        bool hasBanks = dt != null && dt.Rows.Count > 0;
        DataTable qrDt = FilterQrAccounts(dt);
        bool hasQr = qrDt != null && qrDt.Rows.Count > 0;

        pnlBankAccounts.Visible = hasBanks;
        pnlNoBank.Visible = !hasBanks;
        pnlQrPayment.Visible = hasQr;
        pnlNoQr.Visible = false;
        pnlFallbackQr.Visible = !hasQr;
        Btnpayment.Enabled = hasBanks || hasQr || !hasQr;

        if (hasBanks)
        {
            rptBankAccounts.DataSource = dt;
            rptBankAccounts.DataBind();

            if (string.IsNullOrWhiteSpace(hfSelectedBankId.Value))
            {
                hfSelectedBankId.Value = dt.Rows[0]["id"].ToString();
            }
        }

        if (hasQr)
        {
            rptQrAccounts.DataSource = qrDt;
            rptQrAccounts.DataBind();

            if (string.IsNullOrWhiteSpace(hfSelectedBankId.Value))
            {
                hfSelectedBankId.Value = qrDt.Rows[0]["id"].ToString();
            }
        }
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
            string qrFile = GetRowValue(row, "BranchName", "branchname");
            if (!string.IsNullOrWhiteSpace(qrFile))
            {
                qrDt.ImportRow(row);
            }
        }

        return qrDt;
    }

    string GetRowValue(DataRow row, params string[] columnNames)
    {
        foreach (string columnName in columnNames)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                return row[columnName].ToString();
            }
        }

        return string.Empty;
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

    public void CreateDatatable()
    {
        PurchaseDt = new DataTable();
        PurchaseDt.Columns.Add("Franchiseeid");
        PurchaseDt.Columns.Add("ProductId");
        PurchaseDt.Columns.Add("SubProductId");
        PurchaseDt.Columns.Add("ProductName");
        PurchaseDt.Columns.Add("Image");
        PurchaseDt.Columns.Add("Amount");
        PurchaseDt.Columns.Add("MRP");
        PurchaseDt.Columns.Add("BV");
        PurchaseDt.Columns.Add("DP");
        PurchaseDt.Columns.Add("STOCK");
        PurchaseDt.Columns.Add("TOTALBV");
        PurchaseDt.Columns.Add("TOTALDP");
        PurchaseDt.Columns.Add("Quantity");
        PurchaseDt.Columns.Add("TotalAmount");
        PurchaseDt.Columns.Add("CGST");
        PurchaseDt.Columns.Add("SGST");
        PurchaseDt.Columns.Add("IGST");
        PurchaseDt.Columns.Add("PurchaseAmount");
        PurchaseDt.Columns.Add("GSTPER");
    }

    void CalculateSummary(DataTable dt)
    {
        decimal purchaseamounttotal = 0;
        decimal GSTtotal = 0;
        decimal Totaltotal = 0;
        decimal TotalMRP = 0;

        foreach (DataRow row in dt.Rows)
        {
            purchaseamounttotal += Convert.ToDecimal(row["PurchaseAmount"]);
            GSTtotal += Convert.ToDecimal(row["CGST"]) + Convert.ToDecimal(row["SGST"]) + Convert.ToDecimal(row["IGST"]);
            Totaltotal += Convert.ToDecimal(row["TotalAmount"]);
            TotalMRP += Convert.ToDecimal(row["MRP"]) * Convert.ToInt32(row["Quantity"]);
        }

        decimal discount = TotalMRP - Totaltotal;
        decimal tax = GSTtotal;

        lblSubtotal.Text = "₹" + purchaseamounttotal.ToString("0.00");
        lblDiscount.Text = "₹" + discount.ToString("0.00");
        lblTax.Text = "₹" + tax.ToString("0.00");
        lblTotal.Text = "₹" + Totaltotal.ToString("0.00");
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
                return row[columnName].ToString();
            }
        }

        return string.Empty;
    }

    protected bool HasQrCode(object dataItem)
    {
        return !string.IsNullOrWhiteSpace(GetBankField(dataItem, "BranchName", "branchname"));
    }

    protected string GetQrImageUrl(object dataItem)
    {
        string fileName = GetBankField(dataItem, "BranchName", "branchname");
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
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

    string GetSelectedBankId()
    {
        return hfSelectedBankId.Value;
    }

    string UploadReceipt()
    {
        if (!fuReceipt.HasFile)
        {
            return string.Empty;
        }

        string extension = Path.GetExtension(fuReceipt.FileName).ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            return string.Empty;
        }

        string fileName = DateTime.Now.Ticks + Path.GetFileName(fuReceipt.FileName);
        string folder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        fuReceipt.SaveAs(Path.Combine(folder, fileName));
        return fileName;
    }

    protected void Btnpayment_Click(object sender, EventArgs e)
    {
        string paymentMethod = string.IsNullOrWhiteSpace(hfPaymentMethod.Value)
            ? "online"
            : hfPaymentMethod.Value.Trim().ToLowerInvariant();

        string bankId = GetSelectedBankId();
        if (paymentMethod == "online")
        {
            if (string.IsNullOrWhiteSpace(bankId))
            {
                ShowAlert("Please select a bank account.");
                return;
            }
        }
        else if (paymentMethod == "qr")
        {
            DataTable qrDt = FilterQrAccounts(objbank.getBankAccountList());
            if (qrDt.Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(bankId))
                {
                    bankId = qrDt.Rows[0]["id"].ToString();
                }
            }
            else if (string.IsNullOrWhiteSpace(bankId))
            {
                DataTable banks = objbank.getBankAccountList();
                if (banks != null && banks.Rows.Count > 0)
                {
                    bankId = banks.Rows[0]["id"].ToString();
                }
            }
        }

        if (string.IsNullOrWhiteSpace(txtransactionid.Text.Trim()))
        {
            ShowAlert("Please enter transaction ID.");
            return;
        }

        string receiptFile = UploadReceipt();
        if (string.IsNullOrWhiteSpace(receiptFile))
        {
            ShowAlert("Please upload payment receipt.");
            return;
        }

        Decimal CGST = 0;
        Decimal SGST = 0;
        Decimal IGST = 0;
        Decimal Subtotal = 0;
        Decimal total = 0;

        if (PurchaseDt == null)
        {
            CreateDatatable();
        }

        objState.UserId = Session["userid"].ToString();
        DataTable Dt = objState.getCartItems(objState);
        if (Dt.Rows.Count == 0)
        {
            ShowAlert("Your cart is empty.");
            return;
        }

        DataRow DR;
        foreach (DataRow row in Dt.Rows)
        {
            DR = PurchaseDt.NewRow();

            DR["Franchiseeid"] = row["Franchiseeid"].ToString();
            DR["ProductId"] = row["Productid"].ToString();
            DR["SubProductId"] = row["SubProductID"].ToString();
            DR["ProductName"] = row["ProductName"].ToString();
            DR["Image"] = "";
            DR["Amount"] = row["Amount"].ToString();
            DR["MRP"] = row["MRP"].ToString();
            DR["BV"] = row["BV"].ToString();
            DR["DP"] = row["DP"].ToString();
            DR["STOCK"] = "0";
            DR["TOTALBV"] = row["TOTALBV"].ToString();
            DR["TOTALDP"] = row["TOTALDP"].ToString();
            DR["Quantity"] = row["Quantity"].ToString();
            DR["TotalAmount"] = row["TotalAmount"].ToString();
            DR["PurchaseAmount"] = row["PurchaseAmount"].ToString();
            DR["CGST"] = row["CGST"].ToString();
            DR["SGST"] = row["SGST"].ToString();
            DR["IGST"] = row["IGST"].ToString();
            CGST += Convert.ToDecimal(row["CGST"].ToString());
            SGST += Convert.ToDecimal(row["SGST"].ToString());
            IGST += Convert.ToDecimal(row["IGST"].ToString());
            Subtotal += Convert.ToDecimal(row["PurchaseAmount"].ToString());
            total += Convert.ToDecimal(row["TotalAmount"].ToString());
            DR["GSTPER"] = row["GST"].ToString();

            PurchaseDt.Rows.Add(DR);
        }

        objState.PurchaseAmount = Convert.ToDecimal(Subtotal);
        objState.CGST = Convert.ToDecimal(CGST);
        objState.SGST = Convert.ToDecimal(SGST);
        objState.IGST = Convert.ToDecimal(IGST);
        objState.TotalAmount = Convert.ToDecimal(total);
        objState.FranchiseeID = "F000001";
        objState.UserId = Session["userid"].ToString();
        objState.ProductId = "1";
        objState.TransactionCode = txtransactionid.Text.Trim();
        objState.ProductImage = receiptFile;
        objState.tehsilid = bankId;
        objState.PaymentMode = paymentMethod == "qr" ? "2" : "1";
        Session["CartItem"] = PurchaseDt;

        string i = objState.AddPurchaseOutside(objState, PurchaseDt, Convert.ToDecimal(0));
        if (i == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Purchase Successfull');window.location.href='index.aspx';", true);
        }
        else if (i == "2")
        {
            ShowAlert("you have insufficient balance");
        }
        else if (i == "3")
        {
            ShowAlert("insufficient stock");
        }
        else if (i == "4")
        {
            ShowAlert("Your already topup with this plan !");
        }
        else if (i == "5")
        {
            ShowAlert("Already one request is pending please approve/Reject first !");
        }
        else if (i == "6")
        {
            ShowAlert("you can purchase only 1000/2000/3000/4000 for freedom Plan !");
        }
        else if (i == "7")
        {
            ShowAlert("you can purchase only 600/1200/1800/2400 for Unity Plan !");
        }
        else if (i == "8")
        {
            ShowAlert("you can purchase only 2000 for Global Plan !");
        }
        else if (i == "9")
        {
            ShowAlert("First purchase minimum should be 26 Point!");
        }
        else
        {
            ShowAlert("unknown error");
        }
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_SavingProductInvoice : Page
{
    const int CompanyStateCode = 29;

    readonly Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool hasPublicAccess = HasValidInvoiceAccessKey();
        bool isAdmin = Session["useradmin"] != null;
        string sessionUserId = Session["userid"] != null ? Session["userid"].ToString() : null;

        if (string.IsNullOrEmpty(sessionUserId) && !isAdmin && !hasPublicAccess)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        string fallbackUrl = isAdmin ? "../admin/SavingProductGSTReport.aspx" : "SavingProductOrderHistory.aspx";
        if (hasPublicAccess && string.IsNullOrEmpty(sessionUserId) && !isAdmin)
        {
            fallbackUrl = "logout.aspx";
        }

        SiteContactHelper.BindInvoiceCompanyInfo(litCompanyContact);
        SiteContactHelper.BindInvoiceCompanyInfo(litCompanyFooter);
        SiteContactHelper.BindInvoiceSign(imgInvoiceSign, "../");
        SiteContactHelper.BindSupportContactLine(litSupportContact);

        string companyGst = SiteContactHelper.GetPrimaryGst();
        litCompanyGst.Text = string.IsNullOrWhiteSpace(companyGst) ? "-" : companyGst.Trim();
        litCompanyGstFooter.Text = litCompanyGst.Text;
        lblCompanyStateCode.Text = GetGstStateCode(companyGst, CompanyStateCode);

        // For admins / WhatsApp public link the invoice can belong to any member, so resolve
        // the buyer's user id from the query string (or the order itself).
        string effectiveUserId = sessionUserId;
        if ((isAdmin || hasPublicAccess) && string.IsNullOrEmpty(sessionUserId))
        {
            effectiveUserId = QueryStringValue(Request, "userId");
        }

        string orderId = QueryStringValue(Request, "orderId");
        if (string.IsNullOrWhiteSpace(orderId))
        {
            int recordId;
            if (int.TryParse(QueryStringValue(Request, "id"), out recordId) && recordId > 0)
            {
                orderId = GetOrderIdByRecordId(recordId, effectiveUserId);
            }
        }

        if (string.IsNullOrWhiteSpace(orderId))
        {
            Response.Redirect(fallbackUrl);
            return;
        }

        if ((isAdmin || hasPublicAccess) && string.IsNullOrWhiteSpace(effectiveUserId))
        {
            effectiveUserId = GetUserIdByOrderId(orderId);
        }

        if (string.IsNullOrWhiteSpace(effectiveUserId))
        {
            Response.Redirect(fallbackUrl);
            return;
        }

        DataTable items = GetInvoiceItems(orderId, effectiveUserId);
        if (items == null || items.Rows.Count == 0)
        {
            Response.Redirect(fallbackUrl);
            return;
        }

        BindInvoice(orderId, items);
    }

    static string QueryStringValue(System.Web.HttpRequest request, string key)
    {
        if (request == null || request.QueryString == null)
        {
            return string.Empty;
        }

        return (request.QueryString[key] ?? string.Empty).Trim();
    }

    bool HasValidInvoiceAccessKey()
    {
        string configuredKey = ChatwayWhatsAppHelper.InvoiceAccessKey;
        if (string.IsNullOrWhiteSpace(configuredKey))
        {
            return false;
        }

        string requestKey = QueryStringValue(Request, "accessKey");
        return !string.IsNullOrWhiteSpace(requestKey)
           && string.Equals(requestKey, configuredKey, StringComparison.Ordinal);
    }

    string GetUserIdByOrderId(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return string.Empty;
        }

        string sql = "SELECT TOP 1 UserId FROM SavingAccountDetail WITH (NOLOCK) WHERE orderid = '"
            + SqlEscape(orderId) + "'";

        ObjData.StartConnection();
        try
        {
            DataTable dt = ObjData.RunDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["UserId"]).Trim();
            }

            return string.Empty;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    string GetOrderIdByRecordId(int recordId, string userId)
    {
        string sql = "SELECT TOP 1 orderid FROM SavingAccountDetail WITH (NOLOCK) WHERE id = "
            + recordId + " AND UserId = '" + SqlEscape(userId) + "'";

        ObjData.StartConnection();
        try
        {
            DataTable dt = ObjData.RunDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["orderid"]).Trim();
            }

            return string.Empty;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    DataTable GetInvoiceItems(string orderId, string userId)
    {
        string sql = @"
SELECT
    sd.id,
    sd.orderid,
    sd.userid,
    sd.amount,
    sd.status,
    sd.entrydate,
    sd.approvedate,
    sd.productid,
    1 AS quantity,
    ISNULL(sd.CGST, 0) AS CGSTPER,
    ISNULL(sd.SGST, 0) AS SGSTPER,
    ISNULL(sd.IGST, 0) AS IGSTPER,
    ISNULL(NULLIF(LTRIM(RTRIM(pm.HSNCode)), ''), '-') AS hsncode,
    ISNULL(NULLIF(LTRIM(RTRIM(sd.couponcode)), ''), '-') AS couponcode,
    ISNULL(NULLIF(LTRIM(RTRIM(pm.productname)), ''), 'Saving Product') AS productname,
    pm.MRP,
    pm.DP,
    ud.username,
    ud.mobile,
    ISNULL(NULLIF(LTRIM(RTRIM(ud.Email)), ''), '') AS email,
    ISNULL(NULLIF(LTRIM(RTRIM(ud.PanNumber)), ''), '') AS pannumber,
    ISNULL(NULLIF(LTRIM(RTRIM(ud.Address)), ''), '') AS BillAddress,
    ISNULL(NULLIF(LTRIM(RTRIM(ud.AreaName)), ''), '') AS BillArea,
    ISNULL(C.CityName, '') AS BillCity,
    ISNULL(S.StateName, '') AS BillState,
    ISNULL(S.StateId, 0) AS BillStateId,
    ISNULL(NULLIF(LTRIM(RTRIM(ud.Pincode)), ''), '') AS BillPincode,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.Shippingaddress
        ELSE ud.Address
    END AS ShipAddress,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingAreaName
        ELSE ud.AreaName
    END AS ShipArea,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN CS.CityName
        ELSE C.CityName
    END AS ShipCity,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN SS.StateName
        ELSE S.StateName
    END AS ShipState,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN SS.StateId
        ELSE S.StateId
    END AS ShipStateId,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingPincode
        ELSE ud.Pincode
    END AS ShipPincode
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sd.UserId
LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId
LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId
LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId
LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId
WHERE sd.orderid = '" + SqlEscape(orderId) + @"'
  AND sd.UserId = '" + SqlEscape(userId) + @"'
  AND (sd.status = 'Approved'
       OR LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', 'approve', '1', 'active'))
ORDER BY sd.couponcode, sd.id";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(sql);
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (dt == null)
        {
            return new DataTable();
        }

        EnrichInvoiceItems(dt);
        return dt;
    }

    void EnrichInvoiceItems(DataTable dt)
    {
        AddColumnIfMissing(dt, "qty", typeof(int));
        AddColumnIfMissing(dt, "unitprice", typeof(decimal));
        AddColumnIfMissing(dt, "unitpricedisplay", typeof(string));
        AddColumnIfMissing(dt, "pv", typeof(decimal));
        AddColumnIfMissing(dt, "totalpv", typeof(decimal));
        AddColumnIfMissing(dt, "pvdisplay", typeof(string));
        AddColumnIfMissing(dt, "totalpvdisplay", typeof(string));
        AddColumnIfMissing(dt, "amountdisplay", typeof(string));
        AddColumnIfMissing(dt, "taxableamount", typeof(decimal));
        AddColumnIfMissing(dt, "cgst", typeof(decimal));
        AddColumnIfMissing(dt, "sgst", typeof(decimal));
        AddColumnIfMissing(dt, "igst", typeof(decimal));
        AddColumnIfMissing(dt, "totalgst", typeof(decimal));
        AddColumnIfMissing(dt, "gstper", typeof(decimal));
        AddColumnIfMissing(dt, "cgstper", typeof(decimal));
        AddColumnIfMissing(dt, "sgstper", typeof(decimal));
        AddColumnIfMissing(dt, "igstper", typeof(decimal));

        if (!dt.Columns.Contains("hsncode"))
        {
            dt.Columns.Add("hsncode", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            int qty = ParseInt(GetColumnValue(row, "quantity", "qty"));
            if (qty <= 0)
            {
                qty = 1;
            }

            decimal amount = ParseDecimal(row["amount"]);
            decimal cgstPer = ParseDecimal(GetColumnValue(row, "CGSTPER", "CGSTPer", "cgstper", "CGST", "cgst"));
            decimal sgstPer = ParseDecimal(GetColumnValue(row, "SGSTPER", "SGSTPer", "sgstper", "SGST", "sgst"));
            decimal igstPer = ParseDecimal(GetColumnValue(row, "IGSTPER", "IGSTPer", "igstper", "IGST", "igst"));
            decimal totalGstRate = cgstPer + sgstPer + igstPer;

            decimal taxable = amount;
            if (totalGstRate > 0m)
            {
                taxable = RoundMoney((amount * 100m) / (100m + totalGstRate));
            }

            decimal cgst = RoundMoney(taxable * cgstPer / 100m);
            decimal sgst = RoundMoney(taxable * sgstPer / 100m);
            decimal igst = RoundMoney(taxable * igstPer / 100m);
            decimal totalGst = RoundMoney(cgst + sgst + igst);

            decimal unitPrice = qty > 0 ? RoundMoney(taxable / qty) : taxable;

            string hsn = Convert.ToString(GetColumnValue(row, "hsncode", "HSNCode", "HSNCODE")).Trim();
            if (string.IsNullOrWhiteSpace(hsn))
            {
                hsn = "-";
            }

            row["qty"] = qty;
            row["hsncode"] = hsn;
            row["unitprice"] = unitPrice;
            row["unitpricedisplay"] = FormatMoney(unitPrice);
            row["pv"] = 0m;
            row["totalpv"] = 0m;
            row["pvdisplay"] = "0.000";
            row["totalpvdisplay"] = "0.000";
            row["amountdisplay"] = FormatMoney(amount);
            row["taxableamount"] = taxable;
            row["cgst"] = cgst;
            row["sgst"] = sgst;
            row["igst"] = igst;
            row["totalgst"] = totalGst;
            row["gstper"] = totalGstRate;
            row["cgstper"] = cgstPer;
            row["sgstper"] = sgstPer;
            row["igstper"] = igstPer;
        }
    }

    static object GetColumnValue(DataRow row, params string[] columnNames)
    {
        if (row == null || columnNames == null)
        {
            return null;
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName) || !row.Table.Columns.Contains(columnName))
            {
                continue;
            }

            return row[columnName];
        }

        return null;
    }

    void BindInvoice(string orderId, DataTable items)
    {
        DataRow header = items.Rows[0];
        DateTime invoiceDate = GetInvoiceDate(items);

        lblBillingName.Text = Convert.ToString(header["username"]).Trim();
        lblUserId.Text = Convert.ToString(header["userid"]).Trim();
        lblBillingAddress.Text = Convert.ToString(header["BillAddress"]).Trim();
        lblBillingArea.Text = Convert.ToString(header["BillArea"]).Trim();
        lblBillingCity.Text = Convert.ToString(header["BillCity"]).Trim();
        lblBillingState.Text = Convert.ToString(header["BillState"]).Trim();
        lblBillingPincode.Text = Convert.ToString(header["BillPincode"]).Trim();
        lblBillingMobile.Text = Convert.ToString(header["mobile"]).Trim();
        lblBillingEmail.Text = string.IsNullOrWhiteSpace(Convert.ToString(header["email"]))
            ? string.Empty
            : Convert.ToString(header["email"]).Trim();
        lblBillingGstin.Text = string.IsNullOrWhiteSpace(Convert.ToString(header["pannumber"]))
            ? "0"
            : Convert.ToString(header["pannumber"]).Trim();
        lblBillingStateCode.Text = GetCustomerStateCode(header);

        lblShippingAddress.Text = Convert.ToString(header["ShipAddress"]).Trim();
        lblShippingArea.Text = Convert.ToString(header["ShipArea"]).Trim();
        lblShippingCity.Text = Convert.ToString(header["ShipCity"]).Trim();
        lblShippingState.Text = Convert.ToString(header["ShipState"]).Trim();
        lblShippingPincode.Text = Convert.ToString(header["ShipPincode"]).Trim();

        lblInvoiceDate.Text = invoiceDate.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture);
        lblDueDate.Text = invoiceDate.AddDays(15).ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture);
        lblInvoiceNumber.Text = "#INV/SP/" + invoiceDate.ToString("yy", CultureInfo.InvariantCulture) + "/" + orderId;
        lblOrderId.Text = orderId;

        gvItems.DataSource = items;
        gvItems.DataBind();

        decimal subTotal = items.AsEnumerable().Sum(row => ParseDecimal(row["taxableamount"]));
        decimal cgstTotal = items.AsEnumerable().Sum(row => ParseDecimal(row["cgst"]));
        decimal sgstTotal = items.AsEnumerable().Sum(row => ParseDecimal(row["sgst"]));
        decimal igstTotal = items.AsEnumerable().Sum(row => ParseDecimal(row["igst"]));
        decimal grandTotal = items.AsEnumerable().Sum(row => ParseDecimal(row["amount"]));
        int totalQty = items.AsEnumerable().Sum(row => ParseInt(row["qty"]));

        lblSubTotal.Text = FormatMoney(subTotal);
        lblCgstAmount.Text = FormatMoney(cgstTotal);
        lblSgstAmount.Text = FormatMoney(sgstTotal);
        lblIgstAmount.Text = FormatMoney(igstTotal);
        lblPayableAmount.Text = FormatMoney(grandTotal);
        lblGrandTotal.Text = FormatMoney(grandTotal);
        lblCouponDiscount.Text = "0.00";
        lblTotalQty.Text = totalQty.ToString(CultureInfo.InvariantCulture);

        rowCgst.Visible = true;
        rowSgst.Visible = true;
        rowIgst.Visible = true;

        lblAmountWords.Text = ConvertAmountToWords(grandTotal);
        lblTaxAmountWords.Text = ConvertAmountToWords(subTotal);

        DataTable hsnSummary = BuildHsnSummary(items);
        gvHsn.DataSource = hsnSummary;
        gvHsn.DataBind();
    }

    protected void gvHsn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        DataRowView rowView = e.Row.DataItem as DataRowView;
        if (rowView == null)
        {
            return;
        }

        object isFooter = rowView["isfooter"];
        if (isFooter != null && isFooter != DBNull.Value && Convert.ToBoolean(isFooter))
        {
            e.Row.CssClass = "inv-hsn-footer";
        }
    }

    DataTable BuildHsnSummary(DataTable items)
    {
        DataTable summary = new DataTable();
        summary.Columns.Add("hsncode", typeof(string));
        summary.Columns.Add("taxableamount", typeof(decimal));
        summary.Columns.Add("taxabledisplay", typeof(string));
        summary.Columns.Add("cgstperdisplay", typeof(string));
        summary.Columns.Add("cgstamount", typeof(decimal));
        summary.Columns.Add("cgstdisplay", typeof(string));
        summary.Columns.Add("sgstperdisplay", typeof(string));
        summary.Columns.Add("sgstamount", typeof(decimal));
        summary.Columns.Add("sgstdisplay", typeof(string));
        summary.Columns.Add("igstperdisplay", typeof(string));
        summary.Columns.Add("igstamount", typeof(decimal));
        summary.Columns.Add("igstdisplay", typeof(string));
        summary.Columns.Add("totalgstamount", typeof(decimal));
        summary.Columns.Add("totalgstdisplay", typeof(string));
        summary.Columns.Add("totalamount", typeof(decimal));
        summary.Columns.Add("totalamountdisplay", typeof(string));
        summary.Columns.Add("isfooter", typeof(bool));

        var groups = items.AsEnumerable()
            .GroupBy(row => new
            {
                Hsn = Convert.ToString(row["hsncode"]).Trim(),
                CgstPer = ParseDecimal(row["cgstper"]),
                SgstPer = ParseDecimal(row["sgstper"]),
                IgstPer = ParseDecimal(row["igstper"])
            });

        decimal totalTaxable = 0m;
        decimal totalCgst = 0m;
        decimal totalSgst = 0m;
        decimal totalIgst = 0m;
        decimal totalGst = 0m;
        decimal totalAmount = 0m;

        foreach (var group in groups)
        {
            decimal taxable = group.Sum(item => ParseDecimal(item["taxableamount"]));
            decimal cgst = group.Sum(item => ParseDecimal(item["cgst"]));
            decimal sgst = group.Sum(item => ParseDecimal(item["sgst"]));
            decimal igst = group.Sum(item => ParseDecimal(item["igst"]));
            decimal gst = group.Sum(item => ParseDecimal(item["totalgst"]));
            decimal amount = group.Sum(item => ParseDecimal(item["amount"]));

            DataRow summaryRow = summary.NewRow();
            summaryRow["hsncode"] = group.Key.Hsn;
            summaryRow["taxableamount"] = taxable;
            summaryRow["taxabledisplay"] = FormatMoney(taxable);
            summaryRow["cgstperdisplay"] = group.Key.CgstPer > 0m ? FormatPercent(group.Key.CgstPer) : "0%";
            summaryRow["cgstamount"] = cgst;
            summaryRow["cgstdisplay"] = FormatMoney(cgst);
            summaryRow["sgstperdisplay"] = group.Key.SgstPer > 0m ? FormatPercent(group.Key.SgstPer) : "0%";
            summaryRow["sgstamount"] = sgst;
            summaryRow["sgstdisplay"] = FormatMoney(sgst);
            summaryRow["igstperdisplay"] = group.Key.IgstPer > 0m ? FormatPercent(group.Key.IgstPer) : "0%";
            summaryRow["igstamount"] = igst;
            summaryRow["igstdisplay"] = FormatMoney(igst);
            summaryRow["totalgstamount"] = gst;
            summaryRow["totalgstdisplay"] = FormatMoney(gst);
            summaryRow["totalamount"] = amount;
            summaryRow["totalamountdisplay"] = FormatMoney(amount);
            summaryRow["isfooter"] = false;
            summary.Rows.Add(summaryRow);

            totalTaxable += taxable;
            totalCgst += cgst;
            totalSgst += sgst;
            totalIgst += igst;
            totalGst += gst;
            totalAmount += amount;
        }

        DataRow footer = summary.NewRow();
        footer["hsncode"] = "Total:";
        footer["taxableamount"] = totalTaxable;
        footer["taxabledisplay"] = FormatMoney(totalTaxable);
        footer["cgstperdisplay"] = string.Empty;
        footer["cgstamount"] = totalCgst;
        footer["cgstdisplay"] = FormatMoney(totalCgst);
        footer["sgstperdisplay"] = string.Empty;
        footer["sgstamount"] = totalSgst;
        footer["sgstdisplay"] = FormatMoney(totalSgst);
        footer["igstperdisplay"] = string.Empty;
        footer["igstamount"] = totalIgst;
        footer["igstdisplay"] = FormatMoney(totalIgst);
        footer["totalgstamount"] = totalGst;
        footer["totalgstdisplay"] = FormatMoney(totalGst);
        footer["totalamount"] = totalAmount;
        footer["totalamountdisplay"] = FormatMoney(totalAmount);
        footer["isfooter"] = true;
        summary.Rows.Add(footer);

        return summary;
    }

    static string GetCustomerStateCode(DataRow header)
    {
        int stateId = ParseInt(header["BillStateId"]);
        if (stateId > 0)
        {
            return stateId.ToString(CultureInfo.InvariantCulture);
        }

        string stateName = Convert.ToString(header["BillState"]).Trim();
        int mappedCode;
        if (TryGetStateCodeByName(stateName, out mappedCode))
        {
            return mappedCode.ToString(CultureInfo.InvariantCulture);
        }

        return "0";
    }

    static string GetGstStateCode(string gstin, int fallback)
    {
        if (string.IsNullOrWhiteSpace(gstin) || gstin.Trim().Length < 2)
        {
            return fallback.ToString(CultureInfo.InvariantCulture);
        }

        int code;
        if (int.TryParse(gstin.Trim().Substring(0, 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out code))
        {
            return code.ToString(CultureInfo.InvariantCulture);
        }

        return fallback.ToString(CultureInfo.InvariantCulture);
    }

    static bool TryGetStateCodeByName(string stateName, out int stateCode)
    {
        stateCode = 0;
        if (string.IsNullOrWhiteSpace(stateName))
        {
            return false;
        }

        Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Jammu and Kashmir", 1 },
            { "Himachal Pradesh", 2 },
            { "Punjab", 3 },
            { "Chandigarh", 4 },
            { "Uttarakhand", 5 },
            { "Haryana", 6 },
            { "Delhi", 7 },
            { "Rajasthan", 8 },
            { "Uttar Pradesh", 9 },
            { "Bihar", 10 },
            { "Sikkim", 11 },
            { "Arunachal Pradesh", 12 },
            { "Nagaland", 13 },
            { "Manipur", 14 },
            { "Mizoram", 15 },
            { "Tripura", 16 },
            { "Meghalaya", 17 },
            { "Assam", 18 },
            { "West Bengal", 19 },
            { "Jharkhand", 20 },
            { "Odisha", 21 },
            { "Chhattisgarh", 22 },
            { "Madhya Pradesh", 23 },
            { "Gujarat", 24 },
            { "Daman and Diu", 25 },
            { "Dadra and Nagar Haveli", 26 },
            { "Maharashtra", 27 },
            { "Andhra Pradesh", 28 },
            { "Karnataka", 29 },
            { "Goa", 30 },
            { "Lakshadweep", 31 },
            { "Kerala", 32 },
            { "Tamil Nadu", 33 },
            { "Puducherry", 34 },
            { "Andaman and Nicobar Islands", 35 },
            { "Telangana", 36 },
            { "Andhra Pradesh (New)", 37 },
            { "Ladakh", 38 }
        };

        return map.TryGetValue(stateName.Trim(), out stateCode);
    }

    static DateTime GetInvoiceDate(DataTable items)
    {
        DateTime latestDate = DateTime.MinValue;
        foreach (DataRow row in items.Rows)
        {
            DateTime approvedDate = GetDateValue(row["approvedate"]);
            if (approvedDate.Year > 1900 && approvedDate > latestDate)
            {
                latestDate = approvedDate;
            }

            DateTime entryDate = GetDateValue(row["entrydate"]);
            if (entryDate.Year > 1900 && entryDate > latestDate)
            {
                latestDate = entryDate;
            }
        }

        return latestDate.Year > 1900 ? latestDate : DateTime.Now;
    }

    static DateTime GetDateValue(object value)
    {
        DateTime parsedDate;
        if (value == null || value == DBNull.Value)
        {
            return DateTime.MinValue;
        }

        if (DateTime.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsedDate))
        {
            return parsedDate;
        }

        return DateTime.MinValue;
    }

    static decimal ParseDecimal(object value)
    {
        decimal parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }

        if (decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsed))
        {
            return parsed;
        }

        return 0m;
    }

    static int ParseInt(object value)
    {
        int parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0;
        }

        if (int.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsed))
        {
            return parsed;
        }

        return 0;
    }

    static decimal RoundMoney(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    static string FormatMoney(decimal value)
    {
        return value.ToString("N2", CultureInfo.InvariantCulture);
    }

    static string FormatPercent(decimal value)
    {
        return value.ToString("0.#", CultureInfo.InvariantCulture) + "%";
    }

    static void AddColumnIfMissing(DataTable table, string columnName, Type columnType)
    {
        if (!table.Columns.Contains(columnName))
        {
            table.Columns.Add(columnName, columnType);
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static string ConvertAmountToWords(decimal amount)
    {
        if (amount <= 0m)
        {
            return "Zero";
        }

        long rupees = (long)Math.Floor(amount);
        int paise = (int)Math.Round((amount - rupees) * 100m, 0, MidpointRounding.AwayFromZero);

        StringBuilder words = new StringBuilder();
        if (rupees > 0)
        {
            words.Append(ConvertWholeNumber(rupees.ToString(CultureInfo.InvariantCulture)));
        }

        if (paise > 0)
        {
            if (words.Length > 0)
            {
                words.Append(" And ");
            }

            words.Append(ConvertWholeNumber(paise.ToString(CultureInfo.InvariantCulture)));
            words.Append(" Paise");
        }

        return ToTitleCaseWords(words.ToString());
    }

    static string ToTitleCaseWords(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        string[] parts = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];
            if (part.Length == 0)
            {
                continue;
            }

            parts[i] = char.ToUpperInvariant(part[0]) + part.Substring(1).ToLowerInvariant();
        }

        return string.Join(" ", parts);
    }

    static string ConvertWholeNumber(string number)
    {
        try
        {
            double amount = Convert.ToDouble(number, CultureInfo.InvariantCulture);
            if (amount <= 0)
            {
                return string.Empty;
            }

            int whole = (int)Math.Round(amount);
            return ConvertWholeNumberCore(whole).Trim();
        }
        catch
        {
            return string.Empty;
        }
    }

    static string ConvertWholeNumberCore(int number)
    {
        if (number == 0)
        {
            return "Zero";
        }

        if (number < 0)
        {
            return "Minus " + ConvertWholeNumberCore(Math.Abs(number));
        }

        string words = string.Empty;
        if ((number / 10000000) > 0)
        {
            words += ConvertWholeNumberCore(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            words += ConvertWholeNumberCore(number / 100000) + " Lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += ConvertWholeNumberCore(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += ConvertWholeNumberCore(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != string.Empty)
            {
                words += "and ";
            }

            string[] unitsMap = {
                "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };
            string[] tensMap = {
                "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
            };

            if (number < 20)
            {
                words += unitsMap[number];
            }
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                {
                    words += " " + unitsMap[number % 10];
                }
            }
        }

        return words;
    }
}

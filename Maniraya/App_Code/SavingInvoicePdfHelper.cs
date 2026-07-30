using DataTier;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

public static class SavingInvoicePdfHelper
{
    const int CompanyStateCode = 29;

    // Brand green matching SavingProductInvoice.aspx
    static readonly float[] Green = { 0.090f, 0.647f, 0.294f };      // #17a54b
    static readonly float[] GreenSoft = { 0.941f, 0.992f, 0.953f };  // #f0fdf4
    static readonly float[] Border = { 0.820f, 0.835f, 0.859f };     // #d1d5db
    static readonly float[] Muted = { 0.420f, 0.447f, 0.502f };      // #6b7280
    static readonly float[] TextDark = { 0.122f, 0.161f, 0.216f };   // #1f2937
    static readonly float[] HeaderGray = { 0.953f, 0.957f, 0.965f }; // #f3f4f6
    static readonly float[] White = { 1f, 1f, 1f };
    static readonly float[] RowAlt = { 0.980f, 0.980f, 0.980f };

    public static string InvoiceFolderVirtual
    {
        get { return "~/InvoiceFiles/"; }
    }

    public static byte[] BuildInvoicePdf(string orderId, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            userId = ResolveUserId(orderId);
        }

        DataTable items = GetInvoiceItems(orderId, userId);
        if (items == null || items.Rows.Count == 0)
        {
            return null;
        }

        EnrichInvoiceItems(items);
        return BuildPdfBytes(orderId, items);
    }

    /// <summary>
    /// Saves invoice PDF under ~/InvoiceFiles and returns the public https URL for Chatway.
    /// </summary>
    public static string SaveInvoicePdfAndGetPublicUrl(string orderId, string userId, out string fileName, out string error)
    {
        fileName = string.Empty;
        error = string.Empty;

        byte[] pdf = BuildInvoicePdf(orderId, userId);
        if (pdf == null || pdf.Length < 10)
        {
            error = "Invoice PDF could not be generated (no approved items).";
            return string.Empty;
        }

        string header = Encoding.ASCII.GetString(pdf, 0, Math.Min(5, pdf.Length));
        if (!header.StartsWith("%PDF", StringComparison.Ordinal))
        {
            error = "Generated file is not a valid PDF.";
            return string.Empty;
        }

        fileName = "Invoice_" + Sanitize(orderId) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + ".pdf";

        string folder = HostingEnvironment.MapPath(InvoiceFolderVirtual);
        if (string.IsNullOrWhiteSpace(folder))
        {
            error = "Invoice folder path unavailable.";
            return string.Empty;
        }

        try
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllBytes(Path.Combine(folder, fileName), pdf);
        }
        catch (Exception ex)
        {
            error = "Unable to save invoice PDF: " + ex.Message;
            return string.Empty;
        }

        string publicUrl = BuildPublicFileUrl(fileName);
        if (string.IsNullOrWhiteSpace(publicUrl))
        {
            error = "Public invoice file URL is not configured.";
            return string.Empty;
        }

        return publicUrl;
    }

    public static string BuildPublicFileUrl(string fileName)
    {
        string baseUrl = GetSetting("ChatwayPublicFileBaseUrl", "https://mpremium.in/InvoiceFiles/");
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            return string.Empty;
        }

        if (!baseUrl.EndsWith("/"))
        {
            baseUrl += "/";
        }

        return baseUrl + HttpUtility.UrlPathEncode(fileName);
    }

    static byte[] BuildPdfBytes(string orderId, DataTable items)
    {
        DataRow header = items.Rows[0];
        DateTime invoiceDate = GetInvoiceDate(items);

        decimal subTotal = items.AsEnumerable().Sum(r => ParseDecimal(r["taxableamount"]));
        decimal cgstTotal = items.AsEnumerable().Sum(r => ParseDecimal(r["cgst"]));
        decimal sgstTotal = items.AsEnumerable().Sum(r => ParseDecimal(r["sgst"]));
        decimal igstTotal = items.AsEnumerable().Sum(r => ParseDecimal(r["igst"]));
        decimal grandTotal = items.AsEnumerable().Sum(r => ParseDecimal(r["amount"]));
        int totalQty = items.AsEnumerable().Sum(r => ParseInt(r["qty"]));

        string invoiceNo = "#INV/SP/" + invoiceDate.ToString("yy", CultureInfo.InvariantCulture) + "/" + orderId;
        string companyGst = Safe(SiteContactHelper.GetPrimaryGst());
        string companyAddress = Safe(SiteContactHelper.GetPrimaryAddress());
        string companyPhone = Safe(SiteContactHelper.GetPrimaryPhone());
        string companyEmail = Safe(SiteContactHelper.GetPrimaryEmail());
        string companyWebsite = Safe(SiteContactHelper.GetPrimaryWebsite());
        string companyStateCode = GetGstStateCode(companyGst, CompanyStateCode);

        string userName = Safe(header["username"]);
        string userId = Safe(header["userid"]);
        string mobile = Safe(header["mobile"]);
        string email = Safe(GetCol(header, "email"));
        string billAddress = Safe(header["BillAddress"]);
        string billArea = Safe(header["BillArea"]);
        string billCity = Safe(header["BillCity"]);
        string billState = Safe(header["BillState"]);
        string billPin = Safe(header["BillPincode"]);
        string shipAddress = Safe(GetCol(header, "ShipAddress", "BillAddress"));
        string shipArea = Safe(GetCol(header, "ShipArea", "BillArea"));
        string shipCity = Safe(GetCol(header, "ShipCity", "BillCity"));
        string shipState = Safe(GetCol(header, "ShipState", "BillState"));
        string shipPin = Safe(GetCol(header, "ShipPincode", "BillPincode"));
        string panOrGst = Safe(GetCol(header, "pannumber"));
        if (string.IsNullOrWhiteSpace(panOrGst))
        {
            panOrGst = "0";
        }

        string billLine = JoinNonEmpty(billPin, billCity, billArea, billState);
        string shipLine = JoinNonEmpty(shipPin, shipCity, shipArea, shipState);
        string custStateCode = GetCustomerStateCode(header);

        var pdf = new SimplePdfBuilder(26f, 26f);
        float left = pdf.ContentLeft;
        float right = pdf.ContentRight;
        float width = pdf.ContentWidth;
        float y = SimplePdfBuilder.PageHeight - 26f;

        // ── Outer sheet border ──
        pdf.StrokeRect(left - 2f, 26f, width + 4f, SimplePdfBuilder.PageHeight - 52f, Border[0], Border[1], Border[2], 0.8f);

        // ── Green header bar (taller so Order Id / address do not clip or overlap) ──
        float headerH = 112f;
        float headerBottom = y - headerH;
        pdf.FillRect(left, headerBottom, width, headerH, Green[0], Green[1], Green[2]);

        // Logo mark
        pdf.FillStrokeRect(left + 12f, y - 48f, 32f, 32f, 0.20f, 0.72f, 0.40f, 1f, 1f, 1f, 1.2f);
        pdf.TextCenter("M", left + 28f, y - 38f, 15f, true, 1f, 1f, 1f);

        float leftColX = left + 54f;
        float leftColMaxW = Math.Max(180f, width - 200f);
        pdf.Text("Maniraya Enterprises", leftColX, y - 26f, 13f, true, 1f, 1f, 1f);

        float companyInfoY = y - 40f;
        companyInfoY = DrawWrappedWhite(pdf, companyAddress, leftColX, companyInfoY, leftColMaxW, 7f, 9f) - 3f;
        string contactBits = JoinNonEmpty(
            string.IsNullOrWhiteSpace(companyPhone) ? null : "Ph: " + companyPhone,
            string.IsNullOrWhiteSpace(companyEmail) ? null : companyEmail,
            string.IsNullOrWhiteSpace(companyWebsite) ? null : companyWebsite);
        if (!string.IsNullOrWhiteSpace(contactBits))
        {
            companyInfoY = DrawWrappedWhite(pdf, contactBits, leftColX, companyInfoY, leftColMaxW, 7f, 9f) - 3f;
        }

        pdf.Text("GSTIN/UIN: " + (string.IsNullOrWhiteSpace(companyGst) ? "-" : companyGst)
            + "  |  State: Karnataka, Code: " + companyStateCode,
            leftColX, companyInfoY, 7f, false, 0.95f, 0.98f, 0.95f);

        // Right side: INVOICE + meta (no Due Date; Invoice Date → Paid Date)
        pdf.TextRight("INVOICE", right - 14f, y - 26f, 20f, true, 1f, 1f, 1f);
        pdf.FillStrokeRect(right - 128f, y - 46f, 114f, 14f, 0.20f, 0.72f, 0.40f, 1f, 1f, 1f, 0.7f);
        pdf.TextCenter("ORIGINAL FOR RECIPIENT", right - 71f, y - 42f, 6.5f, true, 1f, 1f, 1f);

        float metaY = y - 62f;
        pdf.TextRight("Invoice No: " + invoiceNo, right - 14f, metaY, 8f, false, 1f, 1f, 1f);
        metaY -= 12f;
        pdf.TextRight("Paid Date: " + invoiceDate.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture), right - 14f, metaY, 8f, false, 1f, 1f, 1f);
        metaY -= 12f;
        pdf.TextRight("Order Id: " + orderId, right - 14f, metaY, 8f, false, 1f, 1f, 1f);

        y = headerBottom - 12f;

        // ── Address cards ──
        float cardGap = 10f;
        float cardW = (width - cardGap) / 2f;
        float cardH = 88f;
        float cardTop = y;
        float cardBottom = y - cardH;

        DrawAddressCard(pdf, left, cardBottom, cardW, cardH, "INVOICE TO:",
            userName + " (" + userId + ")",
            new[]
            {
                billAddress,
                billLine,
                "State: (Code: " + custStateCode + ")",
                "GSTIN/UIN: " + panOrGst,
                "Mobile: " + mobile,
                string.IsNullOrWhiteSpace(email) ? null : "Email: " + email
            });

        DrawAddressCard(pdf, left + cardW + cardGap, cardBottom, cardW, cardH, "SHIPPING ADDRESS:",
            null,
            new[]
            {
                shipAddress,
                shipLine
            });

        y = cardBottom - 12f;

        // ── Items table ──
        // Columns: # | Product | HSN | Qty | Price | PV | Total PV | Total
        float[] colW = { 22f, 200f, 58f, 32f, 58f, 48f, 58f, 67f };
        // Ensure columns fill content width exactly
        float colSum = 0f;
        for (int i = 0; i < colW.Length; i++)
        {
            colSum += colW[i];
        }

        colW[colW.Length - 1] += (width - colSum);

        string[] colHeads = { "#", "PRODUCT NAME", "HSN CODE", "QTY", "PRICE", "PV", "TOTAL PV", "TOTAL" };
        float tableX = left;
        float rowH = 22f;
        float headerRowH = 18f;

        // Header row
        pdf.FillRect(tableX, y - headerRowH, width, headerRowH, Green[0], Green[1], Green[2]);
        float cx = tableX;
        for (int i = 0; i < colHeads.Length; i++)
        {
            pdf.TextCenter(colHeads[i], cx + (colW[i] / 2f), y - 13f, 6.5f, true, 1f, 1f, 1f);
            cx += colW[i];
        }

        y -= headerRowH;
        pdf.StrokeRect(tableX, y - (items.Rows.Count * rowH), width, headerRowH + (items.Rows.Count * rowH), Border[0], Border[1], Border[2], 0.6f);

        int sno = 1;
        foreach (DataRow row in items.Rows)
        {
            if (sno % 2 == 0)
            {
                pdf.FillRect(tableX, y - rowH, width, rowH, RowAlt[0], RowAlt[1], RowAlt[2]);
            }

            string product = Safe(row["productname"]);
            string coupon = Safe(row["couponcode"]);
            string hsn = Safe(row["hsncode"]);
            string qty = Convert.ToString(row["qty"]);
            string price = "Rs " + FormatMoney(row["unitprice"]);
            string pv = "0.000";
            string totalPv = "0.000";
            string total = "Rs " + FormatMoney(row["amount"]);

            float midY = y - 9f;
            float descY = y - 18f;
            cx = tableX;
            pdf.TextCenter(sno.ToString(CultureInfo.InvariantCulture), cx + (colW[0] / 2f), midY, 8f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[0];
            pdf.Text(Truncate(product, 38), cx + 3f, midY, 8f, true, TextDark[0], TextDark[1], TextDark[2]);
            pdf.Text(Truncate("Description : Coupon " + coupon, 38), cx + 3f, descY, 6.5f, false, Muted[0], Muted[1], Muted[2]);
            cx += colW[1];
            pdf.TextCenter(Truncate(hsn, 10), cx + (colW[2] / 2f), midY, 7.5f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[2];
            pdf.TextCenter(qty, cx + (colW[3] / 2f), midY, 8f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[3];
            pdf.TextRight(price, cx + colW[4] - 3f, midY, 7.5f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[4];
            pdf.TextRight(pv, cx + colW[5] - 3f, midY, 7.5f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[5];
            pdf.TextRight(totalPv, cx + colW[6] - 3f, midY, 7.5f, false, TextDark[0], TextDark[1], TextDark[2]);
            cx += colW[6];
            pdf.TextRight(total, cx + colW[7] - 3f, midY, 7.5f, true, TextDark[0], TextDark[1], TextDark[2]);

            // Column separators
            float sepX = tableX;
            for (int i = 0; i < colW.Length - 1; i++)
            {
                sepX += colW[i];
                pdf.Line(sepX, y, sepX, y - rowH, 0.95f, 0.95f, 0.96f, 0.4f);
            }

            pdf.Line(tableX, y - rowH, tableX + width, y - rowH, 0.90f, 0.91f, 0.92f, 0.4f);
            y -= rowH;
            sno++;
        }

        y -= 12f;

        // ── Bottom: terms + totals ──
        float bottomLeftW = width * 0.55f;
        float bottomRightW = width - bottomLeftW - 10f;
        float termsH = 118f;
        float termsBottom = y - termsH;

        pdf.StrokeRect(left, termsBottom, bottomLeftW, termsH, Border[0], Border[1], Border[2], 0.6f);
        pdf.Text("ADDITIONAL INFORMATION:", left + 8f, y - 12f, 7.5f, true, Muted[0], Muted[1], Muted[2]);
        float ty = y - 26f;
        ty = pdf.TextWrapped(
            "Thank you for your business. We appreciate your trust in our services.",
            left + 8f, ty, bottomLeftW - 16f, 7.5f, 10f, false, TextDark[0], TextDark[1], TextDark[2]) - 4f;
        pdf.Text("Terms & Conditions:", left + 8f, ty, 7.5f, true, TextDark[0], TextDark[1], TextDark[2]);
        ty -= 12f;
        string[] terms =
        {
            "Subject to Karnataka Jurisdiction.",
            "GST is Not refundable.",
            "Return your product within 30 days from the date of Activation.",
            "Payment must be made at the time of purchase."
        };
        foreach (string term in terms)
        {
            pdf.Text("- " + term, left + 8f, ty, 7f, false, TextDark[0], TextDark[1], TextDark[2]);
            ty -= 11f;
        }

        string support = Safe(SiteContactHelper.BuildSupportContactLine());
        if (!string.IsNullOrWhiteSpace(support))
        {
            pdf.Text(Truncate(support, 70), left + 8f, termsBottom + 8f, 7f, false, Muted[0], Muted[1], Muted[2]);
        }

        // Totals box
        float totX = left + bottomLeftW + 10f;
        float totY = y;
        float totRowH = 16f;
        var totRows = new List<KeyValuePair<string, string>>();
        totRows.Add(new KeyValuePair<string, string>("Sub Total:", "Rs " + FormatMoney(subTotal)));
        totRows.Add(new KeyValuePair<string, string>("CGST Amount:", "Rs " + FormatMoney(cgstTotal)));
        totRows.Add(new KeyValuePair<string, string>("SGST Amount:", "Rs " + FormatMoney(sgstTotal)));
        totRows.Add(new KeyValuePair<string, string>("IGST Amount:", "Rs " + FormatMoney(igstTotal)));
        totRows.Add(new KeyValuePair<string, string>("Amount Payable:", "Rs " + FormatMoney(grandTotal)));
        totRows.Add(new KeyValuePair<string, string>("Coupon Discount:", "Rs 0.00"));

        float totBoxH = totRows.Count * totRowH;
        pdf.StrokeRect(totX, totY - totBoxH, bottomRightW, totBoxH, Border[0], Border[1], Border[2], 0.6f);
        for (int i = 0; i < totRows.Count; i++)
        {
            float ry = totY - (i * totRowH);
            if (i > 0)
            {
                pdf.Line(totX, ry, totX + bottomRightW, ry, 0.90f, 0.91f, 0.92f, 0.4f);
            }

            pdf.TextRight(totRows[i].Key, totX + bottomRightW - 70f, ry - 11f, 8f, true, Muted[0], Muted[1], Muted[2]);
            pdf.TextRight(totRows[i].Value, totX + bottomRightW - 6f, ry - 11f, 8f, true, TextDark[0], TextDark[1], TextDark[2]);
        }

        totY -= totBoxH + 6f;
        float grandH = 22f;
        pdf.FillRect(totX, totY - grandH, bottomRightW, grandH, Green[0], Green[1], Green[2]);
        pdf.Text("Grand Total:", totX + 10f, totY - 15f, 10f, true, 1f, 1f, 1f);
        pdf.TextRight("Rs " + FormatMoney(grandTotal), totX + bottomRightW - 10f, totY - 15f, 11f, true, 1f, 1f, 1f);

        y = Math.Min(termsBottom, totY - grandH) - 12f;

        // ── HSN summary (compact) ──
        DataTable hsnSummary = BuildHsnSummary(items);
        if (hsnSummary != null && hsnSummary.Rows.Count > 0)
        {
            float[] hCol = { 70f, 70f, 42f, 58f, 42f, 58f, 42f, 58f, 63f };
            // Adjust last to fill
            float hSum = 0f;
            for (int i = 0; i < hCol.Length; i++)
            {
                hSum += hCol[i];
            }

            hCol[hCol.Length - 1] += (width - hSum);
            string[] hHeads =
            {
                "HSN CODE", "TAXABLE", "CGST %", "CGST Amt", "SGST %", "SGST Amt", "IGST %", "IGST Amt", "TOTAL"
            };

            float hHeaderH = 14f;
            float hRowH = 13f;
            float hsnTableTop = y;
            pdf.FillRect(left, y - hHeaderH, width, hHeaderH, Green[0], Green[1], Green[2]);
            cx = left;
            for (int i = 0; i < hHeads.Length; i++)
            {
                pdf.TextCenter(hHeads[i], cx + (hCol[i] / 2f), y - 10f, 5.5f, true, 1f, 1f, 1f);
                cx += hCol[i];
            }

            y -= hHeaderH;
            int hsnIndex = 0;
            foreach (DataRow hr in hsnSummary.Rows)
            {
                bool isFooter = hr.Table.Columns.Contains("isfooter") && hr["isfooter"] != DBNull.Value && Convert.ToBoolean(hr["isfooter"]);
                if (isFooter)
                {
                    pdf.FillRect(left, y - hRowH, width, hRowH, GreenSoft[0], GreenSoft[1], GreenSoft[2]);
                }
                else if (hsnIndex % 2 == 1)
                {
                    pdf.FillRect(left, y - hRowH, width, hRowH, RowAlt[0], RowAlt[1], RowAlt[2]);
                }

                string[] vals =
                {
                    Safe(hr["hsncode"]),
                    "Rs " + FormatMoney(hr["taxableamount"]),
                    Safe(GetCol(hr, "cgstperdisplay")),
                    "Rs " + FormatMoney(GetCol(hr, "cgstamount")),
                    Safe(GetCol(hr, "sgstperdisplay")),
                    "Rs " + FormatMoney(GetCol(hr, "sgstamount")),
                    Safe(GetCol(hr, "igstperdisplay")),
                    "Rs " + FormatMoney(GetCol(hr, "igstamount")),
                    "Rs " + FormatMoney(GetCol(hr, "totalamount"))
                };

                cx = left;
                float basel = y - 9f;
                for (int i = 0; i < vals.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 4 || i == 6)
                    {
                        pdf.TextCenter(Truncate(vals[i], 12), cx + (hCol[i] / 2f), basel, 6f, isFooter, TextDark[0], TextDark[1], TextDark[2]);
                    }
                    else
                    {
                        pdf.TextRight(Truncate(vals[i], 12), cx + hCol[i] - 2f, basel, 6f, isFooter, TextDark[0], TextDark[1], TextDark[2]);
                    }

                    cx += hCol[i];
                }

                pdf.Line(left, y - hRowH, left + width, y - hRowH, 0.90f, 0.91f, 0.92f, 0.35f);
                y -= hRowH;
                hsnIndex++;
            }

            pdf.StrokeRect(left, y, width, hsnTableTop - y, Border[0], Border[1], Border[2], 0.5f);
            y -= 10f;
        }

        // ── Amount in words ──
        float wordsH = 28f;
        float wordsGap = 6f;
        float wordsW1 = width * 0.42f;
        float wordsW3 = width * 0.42f;
        float wordsW2 = width - wordsW1 - wordsW3 - (2f * wordsGap);

        pdf.FillStrokeRect(left, y - wordsH, wordsW1, wordsH, GreenSoft[0], GreenSoft[1], GreenSoft[2], Border[0], Border[1], Border[2], 0.5f);
        pdf.Text("AMOUNT IN WORDS:", left + 6f, y - 10f, 6.5f, true, Green[0], Green[1], Green[2]);
        pdf.Text(Truncate(ConvertAmountToWords(grandTotal) + " Only.", 48), left + 6f, y - 21f, 7f, false, TextDark[0], TextDark[1], TextDark[2]);

        pdf.FillStrokeRect(left + wordsW1 + wordsGap, y - wordsH, wordsW2, wordsH, GreenSoft[0], GreenSoft[1], GreenSoft[2], Border[0], Border[1], Border[2], 0.5f);
        pdf.Text("TOTAL QTY:", left + wordsW1 + wordsGap + 6f, y - 10f, 6.5f, true, Green[0], Green[1], Green[2]);
        pdf.Text(totalQty.ToString(CultureInfo.InvariantCulture), left + wordsW1 + wordsGap + 6f, y - 21f, 9f, true, TextDark[0], TextDark[1], TextDark[2]);

        pdf.FillStrokeRect(left + wordsW1 + wordsGap + wordsW2 + wordsGap, y - wordsH, wordsW3, wordsH, GreenSoft[0], GreenSoft[1], GreenSoft[2], Border[0], Border[1], Border[2], 0.5f);
        pdf.Text("TAXABLE AMOUNT IN WORDS:", left + wordsW1 + wordsGap + wordsW2 + wordsGap + 6f, y - 10f, 6.5f, true, Green[0], Green[1], Green[2]);
        pdf.Text(Truncate(ConvertAmountToWords(subTotal) + " Only.", 48), left + wordsW1 + wordsGap + wordsW2 + wordsGap + 6f, y - 21f, 7f, false, TextDark[0], TextDark[1], TextDark[2]);

        y -= wordsH + 10f;

        // ── Footer ──
        float footH = 48f;
        float footRightW = 130f;
        float footLeftW = width - footRightW;
        pdf.FillRect(left, y - footH, footLeftW, footH, 0.980f, 0.980f, 0.980f);
        pdf.StrokeRect(left, y - footH, width, footH, Border[0], Border[1], Border[2], 0.6f);
        pdf.Line(left + footLeftW, y, left + footLeftW, y - footH, Border[0], Border[1], Border[2], 0.6f);

        pdf.Text("Maniraya Enterprises", left + 8f, y - 14f, 9f, true, 0.08f, 0.33f, 0.18f);
        float fy = y - 26f;
        fy = DrawMutedWrapped(pdf, companyAddress, left + 8f, fy, footLeftW - 16f, 6.5f, 8.5f) - 2f;
        pdf.Text("GSTIN/UIN: " + (string.IsNullOrWhiteSpace(companyGst) ? "-" : companyGst), left + 8f, fy, 7f, true, TextDark[0], TextDark[1], TextDark[2]);

        pdf.TextCenter("Authorized Signatory", left + footLeftW + (footRightW / 2f), y - footH + 10f, 8f, true, 0.08f, 0.33f, 0.18f);

        y -= footH + 10f;
        pdf.TextCenter("THIS IS A COMPUTER GENERATED INVOICE", left + (width / 2f), y, 8f, true, Muted[0], Muted[1], Muted[2]);

        return pdf.Build();
    }

    static void DrawAddressCard(SimplePdfBuilder pdf, float x, float bottom, float w, float h, string title, string name, string[] lines)
    {
        float top = bottom + h;
        pdf.StrokeRect(x, bottom, w, h, Border[0], Border[1], Border[2], 0.6f);
        pdf.FillRect(x, top - 16f, w, 16f, HeaderGray[0], HeaderGray[1], HeaderGray[2]);
        pdf.Line(x, top - 16f, x + w, top - 16f, Border[0], Border[1], Border[2], 0.5f);
        pdf.Text(title, x + 8f, top - 11f, 7.5f, true, Muted[0], Muted[1], Muted[2]);

        float ty = top - 28f;
        if (!string.IsNullOrWhiteSpace(name))
        {
            pdf.Text(Truncate(name, 42), x + 8f, ty, 9f, true, Green[0], Green[1], Green[2]);
            ty -= 12f;
        }

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            IList<string> wrapped = pdf.Wrap(line, w - 16f, 7.5f, false);
            foreach (string wl in wrapped)
            {
                if (ty < bottom + 6f)
                {
                    return;
                }

                pdf.Text(wl, x + 8f, ty, 7.5f, false, TextDark[0], TextDark[1], TextDark[2]);
                ty -= 10f;
            }
        }
    }

    static float DrawWrappedWhite(SimplePdfBuilder pdf, string text, float x, float y, float maxWidth, float size, float lineHeight)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return y;
        }

        foreach (string line in pdf.Wrap(text, maxWidth, size, false))
        {
            pdf.Text(line, x, y, size, false, 0.95f, 0.98f, 0.95f);
            y -= lineHeight;
        }

        return y;
    }

    static float DrawMutedWrapped(SimplePdfBuilder pdf, string text, float x, float y, float maxWidth, float size, float lineHeight)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return y;
        }

        foreach (string line in pdf.Wrap(text, maxWidth, size, false))
        {
            pdf.Text(line, x, y, size, false, Muted[0], Muted[1], Muted[2]);
            y -= lineHeight;
        }

        return y;
    }

    static DateTime GetInvoiceDate(DataTable items)
    {
        DateTime invoiceDate = DateTime.Today;
        foreach (DataRow row in items.Rows)
        {
            DateTime d;
            if (row.Table.Columns.Contains("approvedate") && row["approvedate"] != DBNull.Value
                && DateTime.TryParse(Convert.ToString(row["approvedate"]), out d))
            {
                return d;
            }

            if (row.Table.Columns.Contains("entrydate") && row["entrydate"] != DBNull.Value
                && DateTime.TryParse(Convert.ToString(row["entrydate"]), out d))
            {
                invoiceDate = d;
            }
        }

        return invoiceDate;
    }

    static void EnrichInvoiceItems(DataTable dt)
    {
        AddColumnIfMissing(dt, "qty", typeof(int));
        AddColumnIfMissing(dt, "unitprice", typeof(decimal));
        AddColumnIfMissing(dt, "taxableamount", typeof(decimal));
        AddColumnIfMissing(dt, "cgst", typeof(decimal));
        AddColumnIfMissing(dt, "sgst", typeof(decimal));
        AddColumnIfMissing(dt, "igst", typeof(decimal));
        AddColumnIfMissing(dt, "totalgst", typeof(decimal));
        AddColumnIfMissing(dt, "cgstper", typeof(decimal));
        AddColumnIfMissing(dt, "sgstper", typeof(decimal));
        AddColumnIfMissing(dt, "igstper", typeof(decimal));
        if (!dt.Columns.Contains("hsncode"))
        {
            dt.Columns.Add("hsncode", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            int qty = ParseInt(GetCol(row, "quantity", "qty"));
            if (qty <= 0)
            {
                qty = 1;
            }

            decimal amount = ParseDecimal(row["amount"]);
            decimal cgstPer = ParseDecimal(GetCol(row, "CGSTPER", "cgstper", "CGST"));
            decimal sgstPer = ParseDecimal(GetCol(row, "SGSTPER", "sgstper", "SGST"));
            decimal igstPer = ParseDecimal(GetCol(row, "IGSTPER", "igstper", "IGST"));
            decimal totalGstRate = cgstPer + sgstPer + igstPer;

            decimal taxable = amount;
            if (totalGstRate > 0m)
            {
                taxable = RoundMoney((amount * 100m) / (100m + totalGstRate));
            }

            decimal cgst = RoundMoney(taxable * cgstPer / 100m);
            decimal sgst = RoundMoney(taxable * sgstPer / 100m);
            decimal igst = RoundMoney(taxable * igstPer / 100m);

            string hsn = Safe(GetCol(row, "hsncode", "HSNCode"));
            if (string.IsNullOrWhiteSpace(hsn))
            {
                hsn = "-";
            }

            row["qty"] = qty;
            row["hsncode"] = hsn;
            row["unitprice"] = qty > 0 ? RoundMoney(taxable / qty) : taxable;
            row["taxableamount"] = taxable;
            row["cgst"] = cgst;
            row["sgst"] = sgst;
            row["igst"] = igst;
            row["totalgst"] = RoundMoney(cgst + sgst + igst);
            row["cgstper"] = cgstPer;
            row["sgstper"] = sgstPer;
            row["igstper"] = igstPer;
        }
    }

    static DataTable BuildHsnSummary(DataTable items)
    {
        var summary = new DataTable();
        summary.Columns.Add("hsncode", typeof(string));
        summary.Columns.Add("taxableamount", typeof(decimal));
        summary.Columns.Add("cgstperdisplay", typeof(string));
        summary.Columns.Add("cgstamount", typeof(decimal));
        summary.Columns.Add("sgstperdisplay", typeof(string));
        summary.Columns.Add("sgstamount", typeof(decimal));
        summary.Columns.Add("igstperdisplay", typeof(string));
        summary.Columns.Add("igstamount", typeof(decimal));
        summary.Columns.Add("totalgstamount", typeof(decimal));
        summary.Columns.Add("totalamount", typeof(decimal));
        summary.Columns.Add("isfooter", typeof(bool));

        decimal totalTaxable = 0m, totalCgst = 0m, totalSgst = 0m, totalIgst = 0m, totalGst = 0m, totalAmount = 0m;

        var groups = items.AsEnumerable()
            .GroupBy(r => Safe(r["hsncode"]) + "|" + FormatMoney(r["cgstper"]) + "|" + FormatMoney(r["sgstper"]) + "|" + FormatMoney(r["igstper"]));

        foreach (var group in groups)
        {
            decimal taxable = group.Sum(r => ParseDecimal(r["taxableamount"]));
            decimal cgst = group.Sum(r => ParseDecimal(r["cgst"]));
            decimal sgst = group.Sum(r => ParseDecimal(r["sgst"]));
            decimal igst = group.Sum(r => ParseDecimal(r["igst"]));
            decimal amount = group.Sum(r => ParseDecimal(r["amount"]));
            DataRow first = group.First();

            DataRow row = summary.NewRow();
            row["hsncode"] = Safe(first["hsncode"]);
            row["taxableamount"] = taxable;
            row["cgstperdisplay"] = FormatMoney(first["cgstper"]);
            row["cgstamount"] = cgst;
            row["sgstperdisplay"] = FormatMoney(first["sgstper"]);
            row["sgstamount"] = sgst;
            row["igstperdisplay"] = FormatMoney(first["igstper"]);
            row["igstamount"] = igst;
            row["totalgstamount"] = RoundMoney(cgst + sgst + igst);
            row["totalamount"] = amount;
            row["isfooter"] = false;
            summary.Rows.Add(row);

            totalTaxable += taxable;
            totalCgst += cgst;
            totalSgst += sgst;
            totalIgst += igst;
            totalGst += RoundMoney(cgst + sgst + igst);
            totalAmount += amount;
        }

        DataRow footer = summary.NewRow();
        footer["hsncode"] = "Total";
        footer["taxableamount"] = totalTaxable;
        footer["cgstperdisplay"] = string.Empty;
        footer["cgstamount"] = totalCgst;
        footer["sgstperdisplay"] = string.Empty;
        footer["sgstamount"] = totalSgst;
        footer["igstperdisplay"] = string.Empty;
        footer["igstamount"] = totalIgst;
        footer["totalgstamount"] = totalGst;
        footer["totalamount"] = totalAmount;
        footer["isfooter"] = true;
        summary.Rows.Add(footer);

        return summary;
    }

    static string ResolveUserId(string orderId)
    {
        string sql = "SELECT TOP 1 UserId FROM SavingAccountDetail WITH (NOLOCK) WHERE orderid = '"
            + SqlEscape(orderId) + "'";
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable dt = objData.RunDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["UserId"]).Trim();
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }

        return string.Empty;
    }

    static DataTable GetInvoiceItems(string orderId, string userId)
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

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                return objData.RunDataTable(sql) ?? new DataTable();
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return new DataTable();
        }
    }

    static string GetCustomerStateCode(DataRow header)
    {
        int stateId = ParseInt(GetCol(header, "BillStateId"));
        if (stateId > 0)
        {
            return stateId.ToString(CultureInfo.InvariantCulture);
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

    static string ConvertAmountToWords(decimal amount)
    {
        if (amount <= 0m)
        {
            return "Zero";
        }

        long rupees = (long)Math.Floor(amount);
        int paise = (int)Math.Round((amount - rupees) * 100m, 0, MidpointRounding.AwayFromZero);
        var words = new StringBuilder();
        if (rupees > 0)
        {
            words.Append(NumberToWords(rupees));
        }

        if (paise > 0)
        {
            if (words.Length > 0)
            {
                words.Append(" And ");
            }

            words.Append(NumberToWords(paise));
            words.Append(" Paise");
        }

        return words.ToString();
    }

    static string NumberToWords(long number)
    {
        if (number == 0)
        {
            return "Zero";
        }

        if (number < 0)
        {
            return "Minus " + NumberToWords(Math.Abs(number));
        }

        var sb = new StringBuilder();
        if ((number / 10000000) > 0)
        {
            sb.Append(NumberToWords(number / 10000000)).Append(" Crore ");
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            sb.Append(NumberToWords(number / 100000)).Append(" Lakh ");
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            sb.Append(NumberToWords(number / 1000)).Append(" Thousand ");
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            sb.Append(NumberToWords(number / 100)).Append(" Hundred ");
            number %= 100;
        }

        if (number > 0)
        {
            string[] unitsMap =
            {
                "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };
            string[] tensMap =
            {
                "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
            };

            if (number < 20)
            {
                sb.Append(unitsMap[number]);
            }
            else
            {
                sb.Append(tensMap[number / 10]);
                if ((number % 10) > 0)
                {
                    sb.Append(" ").Append(unitsMap[number % 10]);
                }
            }
        }

        return sb.ToString().Trim();
    }

    static void AddColumnIfMissing(DataTable dt, string name, Type type)
    {
        if (!dt.Columns.Contains(name))
        {
            dt.Columns.Add(name, type);
        }
    }

    static object GetCol(DataRow row, params string[] names)
    {
        foreach (string name in names)
        {
            if (row != null && row.Table.Columns.Contains(name))
            {
                return row[name];
            }
        }

        return null;
    }

    static string FormatMoney(object value)
    {
        return ParseDecimal(value).ToString("0.00", CultureInfo.InvariantCulture);
    }

    static decimal ParseDecimal(object value)
    {
        decimal amount;
        if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out amount)
            || decimal.TryParse(Convert.ToString(value), out amount))
        {
            return amount;
        }

        return 0m;
    }

    static int ParseInt(object value)
    {
        int n;
        if (int.TryParse(Convert.ToString(value), NumberStyles.Integer, CultureInfo.InvariantCulture, out n)
            || int.TryParse(Convert.ToString(value), out n))
        {
            return n;
        }

        return 0;
    }

    static decimal RoundMoney(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    static string Safe(object value)
    {
        return Convert.ToString(value ?? string.Empty).Trim();
    }

    static string Truncate(string value, int maxChars)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxChars)
        {
            return value ?? string.Empty;
        }

        return value.Substring(0, Math.Max(0, maxChars - 3)) + "...";
    }

    static string JoinNonEmpty(params string[] parts)
    {
        var sb = new StringBuilder();
        foreach (string part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                continue;
            }

            if (sb.Length > 0)
            {
                sb.Append(", ");
            }

            sb.Append(part.Trim());
        }

        return sb.ToString();
    }

    static string Sanitize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Invoice";
        }

        var sb = new StringBuilder();
        foreach (char ch in value.Trim())
        {
            if (char.IsLetterOrDigit(ch) || ch == '-' || ch == '_')
            {
                sb.Append(ch);
            }
            else
            {
                sb.Append('_');
            }
        }

        return sb.Length == 0 ? "Invoice" : sb.ToString();
    }

    static string GetSetting(string key, string defaultValue)
    {
        try
        {
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
        }
        catch
        {
        }

        return defaultValue ?? string.Empty;
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

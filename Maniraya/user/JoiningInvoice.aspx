<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JoiningInvoice.aspx.cs" Inherits="MehndilinkInvoice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Purchase Invoice</title>
    <style>
        :root {
            --inv-green-dark: #14532d;
            --inv-green: #17a54b;
            --inv-green-mid: #15803d;
            --inv-green-light: #dcfce7;
            --inv-green-soft: #f0fdf4;
            --inv-border: #d1d5db;
            --inv-text: #1f2937;
            --inv-muted: #6b7280;
        }

        * { box-sizing: border-box; }

        body {
            margin: 0;
            font-family: "Segoe UI", Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: var(--inv-text);
            background: #eef2f7;
            line-height: 1.5;
        }

        .invoice-actions {
            max-width: 920px;
            margin: 14px auto 0;
            padding: 0 12px;
            display: flex;
            justify-content: flex-end;
        }

        .invoice-btn {
            padding: 10px 18px;
            border: none;
            border-radius: 8px;
            background: #1e1e1e;
            color: #fff;
            font-size: 13px;
            font-weight: 700;
            cursor: pointer;
        }

        .invoice-btn:hover { background: #1e1e1e; }

        .invoice-sheet {
            max-width: 920px;
            margin: 12px auto 28px;
            background: #fff;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            overflow: hidden;
            box-shadow: 0 10px 30px rgba(15, 23, 42, 0.08);
        }

        .inv-header-bar {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            gap: 16px;
            background: linear-gradient(135deg, #17a54b 0%, #17a54b 100%);
            color: #fff;
            padding: 18px 22px 16px;
        }

        .inv-header-left { flex: 1; min-width: 0; }

        .inv-logo-row {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 8px;
        }

        .inv-logo-mark {
            width: 44px;
            height: 44px;
            border: 2px solid rgba(255, 255, 255, 0.85);
            border-radius: 8px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 22px;
            font-weight: 800;
            color: #fff;
            flex-shrink: 0;
            background: rgba(255, 255, 255, 0.12);
        }

        .inv-brand-title {
            font-size: 17px;
            font-weight: 800;
            letter-spacing: 0.03em;
            line-height: 1.2;
        }

        .inv-header-left .inv-company-detail {
            font-size: 10px;
            line-height: 1.55;
            opacity: 0.92;
        }

        .inv-header-right { text-align: right; flex-shrink: 0; }

        .inv-header-right h2 {
            margin: 0;
            font-size: 30px;
            font-weight: 800;
            letter-spacing: 0.08em;
            line-height: 1;
        }

        .inv-copy-badge {
            display: inline-block;
            margin: 6px 0 12px;
            padding: 3px 10px;
            border: 1px solid rgba(255, 255, 255, 0.45);
            border-radius: 20px;
            font-size: 9px;
            font-weight: 700;
            letter-spacing: 0.06em;
            background: rgba(255, 255, 255, 0.1);
        }

        .inv-meta div {
            font-size: 10px;
            margin-bottom: 3px;
            opacity: 0.95;
        }

        .inv-body { padding: 14px 18px 18px; }

        .inv-address-row {
            display: flex;
            gap: 12px;
            margin-bottom: 12px;
        }

        .inv-address-card {
            flex: 1;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            overflow: hidden;
        }

        .inv-address-card-head {
            background: #f3f4f6;
            padding: 6px 10px;
            font-size: 10px;
            font-weight: 700;
            color: var(--inv-muted);
            letter-spacing: 0.04em;
            border-bottom: 1px solid var(--inv-border);
        }

        .inv-address-card-body {
            padding: 8px 10px;
            font-size: 11px;
            min-height: 90px;
        }

        .inv-customer-name {
            font-weight: 700;
            color: var(--inv-green);
            font-size: 12px;
            margin-bottom: 4px;
        }

        .inv-table {
            width: 100%;
            border-collapse: collapse;
        }

        .inv-items-table,
        .inv-hsn-table {
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            overflow: hidden;
        }

        .inv-items-table th,
        .inv-hsn-table th {
            background: var(--inv-green) !important;
            color: #fff !important;
            font-weight: 700;
            font-size: 9px;
            padding: 8px 6px;
            border: none !important;
            border-right: 1px solid rgba(255, 255, 255, 0.15) !important;
            text-align: center;
            white-space: nowrap;
        }

        .inv-items-table th:last-child,
        .inv-hsn-table th:last-child { border-right: none !important; }

        .inv-items-table td,
        .inv-hsn-table td {
            padding: 7px 6px;
            border: none !important;
            border-bottom: 1px solid #e5e7eb !important;
            border-right: 1px solid #f3f4f6 !important;
            font-size: 11px;
            vertical-align: top;
        }

        .inv-items-table tr:last-child td,
        .inv-hsn-table tr:last-child td { border-bottom: none !important; }

        .inv-items-table tr:nth-child(even) td,
        .inv-hsn-table tr:nth-child(even) td { background: #fafafa; }

        .inv-product-name { font-weight: 700; color: var(--inv-text); }

        .inv-num {
            text-align: right !important;
            white-space: nowrap;
        }

        .inv-bottom-row {
            display: flex;
            gap: 12px;
            margin-top: 12px;
            align-items: flex-start;
        }

        .inv-bottom-left {
            flex: 1;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            padding: 10px 12px;
        }

        .inv-section-head {
            font-weight: 700;
            font-size: 10px;
            color: var(--inv-muted);
            letter-spacing: 0.04em;
            margin-bottom: 6px;
        }

        .inv-additional { font-size: 10px; color: var(--inv-text); }

        .inv-additional ul {
            margin: 6px 0 0;
            padding-left: 16px;
        }

        .inv-additional li { margin-bottom: 3px; }

        .inv-bottom-right {
            width: 280px;
            flex-shrink: 0;
        }

        .inv-totals {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            overflow: hidden;
        }

        .inv-totals td {
            padding: 6px 10px;
            font-size: 11px;
            border-bottom: 1px solid #e5e7eb;
        }

        .inv-totals .inv-total-label {
            text-align: right;
            color: var(--inv-muted);
            font-weight: 600;
        }

        .inv-totals .inv-total-value {
            text-align: right;
            width: 110px;
            font-weight: 700;
            color: var(--inv-text);
        }

        .inv-grand-bar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background: linear-gradient(135deg, #17a54b 0%, #17a54b 100%);
            color: #fff;
            padding: 10px 14px;
            border-radius: 4px;
            margin-top: 8px;
            font-size: 13px;
            font-weight: 800;
        }

        .inv-grand-bar .inv-grand-amount { font-size: 15px; }

        .inv-words-row {
            display: flex;
            gap: 10px;
            margin-top: 12px;
        }

        .inv-words-box {
            flex: 1;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            padding: 8px 10px;
            background: var(--inv-green-soft);
            font-size: 10px;
        }

        .inv-words-box strong {
            display: block;
            font-size: 9px;
            color: var(--inv-green);
            letter-spacing: 0.04em;
            margin-bottom: 3px;
        }

        .inv-footer {
            display: flex;
            gap: 12px;
            margin-top: 12px;
            border: 1px solid var(--inv-border);
            border-radius: 4px;
            overflow: hidden;
        }

        .inv-footer-left {
            flex: 1;
            padding: 12px 14px;
            background: #fafafa;
            font-size: 10px;
        }

        .inv-footer-left strong {
            color: var(--inv-green-dark);
            font-size: 11px;
        }

        .inv-footer-right {
            width: 220px;
            padding: 12px;
            text-align: center;
            border-left: 1px solid var(--inv-border);
            display: flex;
            flex-direction: column;
            justify-content: flex-end;
        }

        .inv-sign-box {
            min-height: 60px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: flex-end;
        }

        .inv-sign-image {
            max-height: 55px;
            max-width: 160px;
            object-fit: contain;
            display: block;
            margin-bottom: 6px;
        }

        .inv-sign-label {
            font-size: 10px;
            font-weight: 700;
            color: var(--inv-green-dark);
        }

        .inv-footer-note {
            text-align: center;
            margin: 14px 0 4px;
            font-size: 11px;
            font-weight: 700;
            color: var(--inv-muted);
            letter-spacing: 0.04em;
        }

        .inv-hidden { display: none; }

        @media print {
            body { background: #fff; }
            .no-print { display: none !important; }
            .invoice-sheet {
                margin: 0;
                border: none;
                box-shadow: none;
                max-width: none;
            }
            .inv-header-bar,
            .inv-items-table th,
            .inv-hsn-table th,
            .inv-grand-bar {
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-actions no-print">
            <button type="button" class="invoice-btn" onclick="window.print(); return false;">Download / Print Invoice</button>
        </div>

        <div id="invoicePrintArea" class="invoice-sheet">
            <div class="inv-header-bar">
                <div class="inv-header-left">
                    <div class="inv-logo-row">
                        <div class="inv-logo-mark">M</div>
                        <div class="inv-brand-title">Maniraya Enterprises</div>
                    </div>
                    <div class="inv-company-detail">
                        <strong>Maniraya Enterprises</strong><br />
                        <asp:Literal ID="litCompanyContact" runat="server" /><br />
                        <strong>GSTIN/UIN:</strong> <asp:Literal ID="litCompanyGst" runat="server" />
                        &nbsp;|&nbsp; <strong>State:</strong> Karnataka, <strong>Code:</strong>
                        <asp:Label ID="lblCompanyStateCode" runat="server" Text="29" />
                    </div>
                </div>
                <div class="inv-header-right">
                    <h2>INVOICE</h2>
                    <div class="inv-copy-badge">ORIGINAL FOR RECIPIENT</div>
                    <div class="inv-meta">
                        <div><strong>Invoice No:</strong> <asp:Label ID="LblInvoiceNumber" runat="server" /></div>
                        <div><strong>Invoice Date:</strong> <asp:Label ID="LblInvoicedate" runat="server" /></div>
                        <div><strong>Time:</strong> <asp:Label ID="LblInvoiceTime" runat="server" /></div>
                        <div><strong>Order Id:</strong> <asp:Label ID="LblOrderId" runat="server" /></div>
                    </div>
                </div>
            </div>

            <div class="inv-body">
                <div class="inv-address-row">
                    <div class="inv-address-card">
                        <div class="inv-address-card-head">INVOICE TO:</div>
                        <div class="inv-address-card-body">
                            <div class="inv-customer-name">
                                <asp:Label ID="LblBillingname" runat="server" />(<asp:Label ID="lbluserid" runat="server" />)
                            </div>
                            <asp:Label ID="LblBillingAddress" runat="server" /><br />
                            <asp:Label ID="LblBillingPincode" runat="server" />,
                            <asp:Label ID="LblBillingCity" runat="server" />,
                            <asp:Label ID="LblBillingPost" runat="server" />,
                            <asp:Label ID="LblBillingState" runat="server" /><br />
                            <strong>Mobile:</strong> <asp:Label ID="LblBillingMobile" runat="server" /><br />
                            <strong>Email:</strong> <asp:Label ID="LblBillingEmail" runat="server" />
                        </div>
                    </div>
                    <div class="inv-address-card">
                        <div class="inv-address-card-head">SHIPPING ADDRESS:</div>
                        <div class="inv-address-card-body">
                            <asp:Label ID="LblShippingAddress" runat="server" /><br />
                            <asp:Label ID="LblShippingPincode" runat="server" />,
                            <asp:Label ID="LblShippingCity" runat="server" />,
                            <asp:Label ID="LblShippingPost" runat="server" />,
                            <asp:Label ID="LblShippingState" runat="server" />
                        </div>
                    </div>
                </div>

                <asp:GridView ID="GridView1" runat="server" CssClass="inv-table inv-items-table" Width="100%"
                    AutoGenerateColumns="False" ShowHeader="true" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemStyle Width="28px" HorizontalAlign="Center" />
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRODUCT NAME">
                            <ItemTemplate>
                                <span class="inv-product-name"><asp:Label ID="lblproduct" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTY">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HSN CODE">
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblHSNCODE" runat="server" Text='<%# Eval("HSNCODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CP/RATE" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDP" runat="server" Text='<%# Eval("DP") %>'></asp:Label>
                                <asp:Label ID="lblttdp" runat="server" Visible="false" Text='<%# Eval("TotalDP") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRICE">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                ₹ <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BV">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                <asp:Label ID="lblBV" runat="server" Text='<%# Eval("BV") %>'></asp:Label>
                                <asp:Label ID="lblttbv" runat="server" Visible="false" Text='<%# Eval("Totalbv") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TAXABLE">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                ₹ <asp:Label ID="lblPurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGST">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                <asp:Label ID="lblTGST" runat="server" Text='<%# Eval("GST") %>' Visible="false"></asp:Label>
                                ₹ <asp:Label ID="lblGST" runat="server" Text='<%# Eval("CGST") %>'></asp:Label>
                                <div>(<asp:Label ID="lblGSTper" runat="server" Text='<%# Eval("CGSTper") %>'></asp:Label>%)</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SGST">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                ₹ <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST") %>'></asp:Label>
                                <div>(<asp:Label ID="lblSGSTper" runat="server" Text='<%# Eval("SGSTper") %>'></asp:Label>%)</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IGST">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                ₹ <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST") %>'></asp:Label>
                                <div>(<asp:Label ID="lblIGSTper" runat="server" Text='<%# Eval("IGSTper") %>'></asp:Label>%)</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TOTAL">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                <asp:Label ID="lblTotalDP" runat="server" Text='<%# Eval("TotalDP") %>' Visible="false"></asp:Label>
                                ₹ <asp:Label ID="LblTotalAmt" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="inv-bottom-row">
                    <div class="inv-bottom-left">
                        <div class="inv-section-head">ADDITIONAL INFORMATION:</div>
                        <div class="inv-additional">
                            Thank you for your business. We appreciate your trust in our services.
                            <div style="margin-top: 8px;"><strong>Terms &amp; Conditions:</strong></div>
                            <ul>
                                <li>Full payment must be made at the time of purchase.</li>
                                <li>Products will be delivered within 15 working days.</li>
                                <li>Return requests must be made within 7 days from invoice date.</li>
                                <li>GST is not refundable. Subject to Bangalore, Karnataka jurisdiction.</li>
                            </ul>
                            <p style="margin: 6px 0 0;"><asp:Literal ID="litSupportContact" runat="server" /></p>
                        </div>
                    </div>
                    <div class="inv-bottom-right">
                        <table class="inv-totals">
                            <tr>
                                <td class="inv-total-label">Sub Total:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="LblSubTotal" runat="server" Text="0.00" /></td>
                            </tr>
                            <tr id="rowCgst" runat="server">
                                <td class="inv-total-label">CGST Amount:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="LblCgstAmount" runat="server" Text="0.00" /></td>
                            </tr>
                            <tr id="rowSgst" runat="server">
                                <td class="inv-total-label">SGST Amount:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="LblSgstAmount" runat="server" Text="0.00" /></td>
                            </tr>
                            <tr id="rowIgst" runat="server">
                                <td class="inv-total-label">IGST Amount:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="LblIgstAmount" runat="server" Text="0.00" /></td>
                            </tr>
                            <tr>
                                <td class="inv-total-label">Shipping Charges:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="LblShipping" runat="server" Text="0" /></td>
                            </tr>
                            <tr>
                                <td class="inv-total-label">Amount Payable:</td>
                                <td class="inv-total-value">₹ <asp:Label ID="lblTotalAmount" runat="server" Text="0.00" /></td>
                            </tr>
                        </table>
                        <div class="inv-grand-bar">
                            <span>Grand Total:</span>
                            <span class="inv-grand-amount">₹ <asp:Label ID="LblPaybleamount" runat="server" Text="0.00" /></span>
                        </div>
                    </div>
                </div>

                <asp:GridView ID="GridViewgst" runat="server" CssClass="inv-table inv-hsn-table" Width="100%"
                    AutoGenerateColumns="False" ShowHeader="true" GridLines="None" Style="margin-top: 12px;">
                    <Columns>
                        <asp:TemplateField HeaderText="GST TYPE">
                            <ItemTemplate>
                                <asp:Label ID="lblgsttype" runat="server" Text='<%# Eval("gsttype") %>'></asp:Label>
                                (<asp:Label ID="lblgstper" runat="server" Text='<%# Eval("gstper") %>'></asp:Label>%)
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GST VALUE">
                            <ItemStyle CssClass="inv-num" />
                            <ItemTemplate>
                                ₹ <asp:Label ID="lblgst" runat="server" Text='<%# Eval("gst") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="inv-words-row">
                    <div class="inv-words-box">
                        <strong>AMOUNT IN WORDS:</strong>
                        <asp:Label ID="LblPaybleamountwords" runat="server" /> Only.
                    </div>
                    <div class="inv-words-box" style="flex: 0 0 140px;">
                        <strong>TOTAL ITEMS / QTY:</strong>
                        <asp:Label ID="LblTotalqnty" runat="server" Text="0" />
                    </div>
                    <div class="inv-words-box">
                        <strong>TOTAL BV / GST:</strong>
                        BV <asp:Label ID="lblTotalBV" runat="server" Text="0" />
                        &nbsp;|&nbsp; GST ₹ <asp:Label ID="LblTotalgst" runat="server" Text="0" />
                    </div>
                </div>

                <div class="inv-footer">
                    <div class="inv-footer-left">
                        <strong>Maniraya Enterprises</strong><br />
                        <asp:Literal ID="litCompanyFooter" runat="server" /><br />
                        <strong>GSTIN/UIN:</strong> <asp:Literal ID="litCompanyGstFooter" runat="server" />
                    </div>
                    <div class="inv-footer-right">
                        <div class="inv-sign-box">
                            <asp:Image ID="imgInvoiceSign" runat="server" CssClass="inv-sign-image" AlternateText="Authorized Signatory" />
                            <span class="inv-sign-label">Authorized Signatory</span>
                        </div>
                    </div>
                </div>

                <div class="inv-footer-note">THIS IS A COMPUTER GENERATED INVOICE</div>
            </div>
        </div>

        <%-- Keep legacy fields for code-behind compatibility --%>
        <div class="inv-hidden">
            <asp:Label ID="LblDistributername" runat="server" />
            <asp:Label ID="LbDistributerlUserid" runat="server" />
            <asp:Label ID="LblDistributerSponserid" runat="server" />
            <asp:Label ID="LblDistributerSponsername" runat="server" />
            <asp:Label ID="LblDistributerRegdate" runat="server" />
            <asp:Label ID="LblDistributerEmail" runat="server" />
            <asp:Label ID="LblDistributerMobile" runat="server" />
            <asp:Label ID="lblTotaldp" runat="server" />
        </div>
    </form>
</body>
</html>

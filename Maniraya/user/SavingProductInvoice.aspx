<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SavingProductInvoice.aspx.cs" Inherits="user_SavingProductInvoice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Saving Product Invoice</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, Helvetica, sans-serif;
            color: #0f172a;
            background: #f8fafc;
        }

        .invoice-actions {
            max-width: 960px;
            margin: 16px auto 0;
            padding: 0 16px;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }

        .invoice-btn {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 10px 18px;
            border: none;
            border-radius: 8px;
            background: linear-gradient(135deg, #e52d27 0%, #c41e17 100%);
            color: #fff;
            font-size: 14px;
            font-weight: 700;
            cursor: pointer;
        }

        .invoice-sheet {
            max-width: 960px;
            margin: 16px auto 24px;
            padding: 24px;
            background: #fff;
            border: 1px solid #e2e8f0;
            box-shadow: 0 8px 24px rgba(15, 23, 42, 0.08);
        }

        .invoice-title {
            text-align: center;
            font-size: 24px;
            font-weight: 700;
            margin-bottom: 18px;
        }

        .invoice-brand-table,
        .invoice-bill-table,
        .invoice-items-table,
        .invoice-terms-table {
            width: 100%;
            border-collapse: collapse;
        }

        .invoice-brand-table td,
        .invoice-bill-table td,
        .invoice-items-table th,
        .invoice-items-table td,
        .invoice-terms-table td {
            border: 1px solid #000;
            padding: 8px;
            vertical-align: top;
        }

        .invoice-items-table th {
            font-weight: 700;
            background: #f1f5f9;
        }

        .invoice-section-title {
            background: #4800ff;
            color: #fff;
            padding: 6px 8px;
            font-weight: 700;
            margin-top: 12px;
        }

        .invoice-total-bar {
            margin-top: 12px;
            background: #4800ff;
            color: #fff;
            text-align: right;
            padding: 12px 16px;
            font-size: 16px;
            font-weight: 700;
        }

        .invoice-gst-note {
            margin: 8px 0 0;
            text-align: right;
            font-size: 13px;
            font-weight: 600;
            color: #475569;
        }

        .invoice-footer-note {
            text-align: center;
            margin-top: 18px;
            font-size: 18px;
            font-weight: 700;
        }

        .invoice-muted {
            color: #64748b;
            font-size: 12px;
        }

        .invoice-sign-image {
            max-height: 80px;
            max-width: 200px;
            object-fit: contain;
            display: block;
            margin: 0 auto 8px;
        }

        @media print {
            body {
                background: #fff;
            }

            .no-print {
                display: none !important;
            }

            .invoice-sheet {
                margin: 0;
                box-shadow: none;
                border: none;
                max-width: none;
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
            <div class="invoice-title">TAX INVOICE</div>

            <table class="invoice-brand-table">
                <tr>
                    <td style="width: 50%;">
                        <span style="font-size: 1.75rem; font-weight: 800; letter-spacing: 0.03em; color: #1e293b;">MPremium</span>
                    </td>
                    <td style="width: 50%;">
                        <h4 style="margin: 0 0 8px;">M Premium</h4>
                        <h6 style="margin: 0 0 8px;"><asp:Literal ID="litCompanyContact" runat="server" /></h6>
                        <strong><asp:Literal ID="litCompanyGst" runat="server" /></strong>
                    </td>
                </tr>
            </table>

            <div class="invoice-section-title">Bill To</div>
            <table class="invoice-bill-table">
                <tr>
                    <td style="width: 60%;">
                        <asp:Label ID="lblBillingName" runat="server" /><br />
                        User ID: <asp:Label ID="lblUserId" runat="server" /><br />
                        Address: <asp:Label ID="lblBillingAddress" runat="server" /><br />
                        Area: <asp:Label ID="lblBillingArea" runat="server" />,
                        City: <asp:Label ID="lblBillingCity" runat="server" />,
                        State: <asp:Label ID="lblBillingState" runat="server" />,
                        Pin: <asp:Label ID="lblBillingPincode" runat="server" /><br />
                        Mobile: <asp:Label ID="lblBillingMobile" runat="server" />
                    </td>
                    <td style="width: 40%; text-align: right;">
                        Date: <asp:Label ID="lblInvoiceDate" runat="server" /><br />
                        Time: <asp:Label ID="lblInvoiceTime" runat="server" /><br />
                        Invoice No: <asp:Label ID="lblInvoiceNumber" runat="server" /><br />
                        Order Id: <asp:Label ID="lblOrderId" runat="server" />
                    </td>
                </tr>
            </table>

            <asp:GridView ID="gvItems" runat="server" CssClass="invoice-items-table" Width="100%"
                AutoGenerateColumns="False" ShowHeader="true" GridLines="None" Style="margin-top: 12px;">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate><%# Eval("productname") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Coupon Code">
                        <ItemTemplate><%# Eval("couponcode") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>1</ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRP">
                        <ItemTemplate><%# Eval("mrpdisplay") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><%# Eval("amountdisplay") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="invoice-total-bar">
                Total Payable: ₹ <asp:Label ID="lblPayableAmount" runat="server" />
            </div>
            <p class="invoice-gst-note">18% GST including</p>

            <p style="margin-top: 12px;">
                <strong>Amount in words:</strong>
                <asp:Label ID="lblAmountWords" runat="server" /> ONLY
            </p>

            <table class="invoice-terms-table" style="margin-top: 16px;">
                <tr>
                    <td>
                        <strong>Invoice Terms &amp; Conditions</strong>
                        <p class="invoice-muted" style="margin: 8px 0 0;">
                            Payment must be made at the time of purchase. Products will be delivered within 15 working days.
                            Return requests must be made within 7 days from invoice date. Warranty applies to manufacturing defects only.
                            Subject to Bangalore, Karnataka jurisdiction.
                        </p>
                        <p class="invoice-muted"><asp:Literal ID="litSupportContact" runat="server" /></p>
                    </td>
                    <td style="width: 220px; text-align: center;">
                        <strong>Authorised Signatory</strong>
                        <div style="min-height: 90px;">
                            <asp:Image ID="imgInvoiceSign" runat="server" CssClass="invoice-sign-image" AlternateText="Authorised Signatory" />
                        </div>
                        <strong>M Premium</strong>
                    </td>
                </tr>
            </table>

            <div class="invoice-footer-note">THIS IS A COMPUTER GENERATED INVOICE</div>
        </div>
    </form>
</body>
</html>

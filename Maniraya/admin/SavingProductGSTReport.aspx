<%@ Page Title="Saving Product GST Reports" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductGSTReport.aspx.cs" Inherits="admin_SavingProductGSTReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .gst-filter-bar {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            align-items: center;
            padding: 14px 16px;
            margin-bottom: 16px;
            background: #f7fafc;
            border: 1px solid #e6edf2;
            border-radius: 12px;
        }
        .gst-field {
            display: flex;
            align-items: center;
            flex: 1 1 220px;
            min-width: 180px;
            background: #fff;
            border: 1px solid #dbe3ea;
            border-radius: 10px;
            overflow: hidden;
            transition: border-color 0.15s ease, box-shadow 0.15s ease;
        }
        .gst-field:focus-within {
            border-color: #2fa15c;
            box-shadow: 0 0 0 3px rgba(47, 161, 92, 0.12);
        }
        .gst-field > i {
            flex-shrink: 0;
            width: 40px;
            text-align: center;
            color: #8a95a1;
            font-size: 14px;
            pointer-events: none;
        }
        .gst-field .form-control {
            flex: 1 1 auto;
            min-width: 0;
            width: auto;
            height: 42px;
            margin: 0 !important;
            padding: 8px 12px 8px 4px !important;
            border: none !important;
            border-radius: 0 !important;
            background: transparent !important;
            box-shadow: none !important;
            font-size: 14px;
        }
        .gst-btn {
            height: 42px;
            padding: 0 22px;
            border: none;
            border-radius: 10px;
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            color: #fff !important;
            font-weight: 700;
            font-size: 14px;
            cursor: pointer;
            box-shadow: 0 8px 18px -10px rgba(31, 122, 69, 0.7);
        }
        .gst-btn-reset {
            height: 42px;
            padding: 0 18px;
            background: #fff;
            border: 1px solid #dbe3ea;
            border-radius: 10px;
            color: #52616b !important;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
        }
        .gst-export-btn {
            display: inline-flex;
            align-items: center;
            gap: 7px;
            height: 34px;
            padding: 0 16px;
            border: none;
            border-radius: 8px;
            background: linear-gradient(135deg, #1d6f3f, #2fa15c);
            color: #fff !important;
            font-weight: 700;
            font-size: 13px;
            cursor: pointer;
            box-shadow: 0 6px 14px -8px rgba(31, 122, 69, 0.7);
        }
        .gst-export-btn:hover,
        .gst-export-btn:focus {
            color: #fff !important;
            box-shadow: 0 8px 18px -8px rgba(31, 122, 69, 0.85);
        }
        .gst-summary {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            margin-bottom: 16px;
        }
        .gst-summary-card {
            flex: 1 1 160px;
            padding: 14px 16px;
            border-radius: 12px;
            background: #fff;
            border: 1px solid #e6edf2;
            border-left: 4px solid #2fa15c;
            box-shadow: 0 10px 24px -20px rgba(0, 0, 0, 0.5);
        }
        .gst-summary-card span {
            display: block;
            font-size: 12px;
            color: #7c8a83;
            text-transform: uppercase;
            letter-spacing: 0.4px;
        }
        .gst-summary-card strong {
            display: block;
            margin-top: 4px;
            font-size: 18px;
            font-weight: 800;
            color: #16341f;
        }
        .gst-invoice-btn {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 6px 14px;
            border-radius: 8px;
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            color: #fff !important;
            font-size: 12.5px;
            font-weight: 700;
            text-decoration: none;
            white-space: nowrap;
        }
        .gst-invoice-btn:hover {
            color: #fff !important;
            box-shadow: 0 6px 14px -6px rgba(31, 122, 69, 0.7);
        }
        .gst-order-id {
            font-weight: 700;
            color: #1f6f43;
        }
        .gst-member-name {
            font-weight: 600;
            color: #16341f;
        }
        .gst-member-id {
            font-size: 12px;
            color: #8a95a1;
        }
        .gst-field--date {
            flex: 0 1 200px;
            min-width: 170px;
        }
        .gst-date-label {
            flex-shrink: 0;
            padding: 0 8px 0 12px;
            font-size: 12px;
            font-weight: 700;
            color: #6b8577;
            white-space: nowrap;
        }
        .gst-field--date .form-control {
            padding-left: 4px !important;
        }
        .gst-pager td {
            padding: 10px 0 !important;
        }
        .gst-pager table {
            margin: 0 auto;
        }
        .gst-pager a,
        .gst-pager span {
            display: inline-block;
            min-width: 32px;
            padding: 6px 10px;
            margin: 0 3px;
            border-radius: 8px;
            border: 1px solid #dbe3ea;
            background: #fff;
            color: #1f6f43 !important;
            font-weight: 600;
            text-decoration: none;
            text-align: center;
        }
        .gst-pager span {
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            border-color: #1f7a45;
            color: #fff !important;
        }
        .gst-pager a:hover {
            border-color: #2fa15c;
            background: #eafaf0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Saving Product GST Reports</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
            <li class="active">Saving Product GST Reports</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-file-text-o"></i> Saving Product GST Report</h3>
                            <div class="box-tools pull-right">
                                <asp:Button ID="btnExport" runat="server" CssClass="gst-export-btn" Text="Export to Excel" OnClick="btnExport_Click" />
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="gst-summary">
                                <div class="gst-summary-card">
                                    <span>Total Orders</span>
                                    <strong><asp:Label ID="lblTotalOrders" runat="server" Text="0" /></strong>
                                </div>
                                <div class="gst-summary-card">
                                    <span>Total Price</span>
                                    <strong>&#8377; <asp:Label ID="lblTotalPrice" runat="server" Text="0.00" /></strong>
                                </div>
                                <div class="gst-summary-card">
                                    <span>Total CGST</span>
                                    <strong>&#8377; <asp:Label ID="lblTotalCgst" runat="server" Text="0.00" /></strong>
                                </div>
                                <div class="gst-summary-card">
                                    <span>Total SGST</span>
                                    <strong>&#8377; <asp:Label ID="lblTotalSgst" runat="server" Text="0.00" /></strong>
                                </div>
                                <div class="gst-summary-card">
                                    <span>Total IGST</span>
                                    <strong>&#8377; <asp:Label ID="lblTotalIgst" runat="server" Text="0.00" /></strong>
                                </div>
                            </div>

                            <div class="gst-filter-bar">
                                <div class="gst-field">
                                    <i class="fa fa-hashtag"></i>
                                    <asp:TextBox ID="txtOrderId" CssClass="form-control" runat="server" placeholder="Order ID" />
                                </div>
                                <div class="gst-field">
                                    <i class="fa fa-user"></i>
                                    <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" placeholder="User ID / Member Name" />
                                </div>
                                <div class="gst-field gst-field--date" title="From Date">
                                    <span class="gst-date-label">From</span>
                                    <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server" TextMode="Date" />
                                </div>
                                <div class="gst-field gst-field--date" title="To Date">
                                    <span class="gst-date-label">To</span>
                                    <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" TextMode="Date" />
                                </div>
                                <asp:Button ID="btnFilter" CssClass="gst-btn" runat="server" Text="Search" OnClick="btnFilter_Click" />
                                <asp:Button ID="btnReset" CssClass="gst-btn-reset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="gvGst" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvGst_PageIndexChanging">
                                    <PagerStyle CssClass="gst-pager" HorizontalAlign="Center" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="&laquo;" LastPageText="&raquo;" PageButtonCount="10" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemStyle Width="44px" HorizontalAlign="Center" />
                                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order ID">
                                            <ItemTemplate><span class="gst-order-id"><%# Server.HtmlEncode(Convert.ToString(Eval("orderid"))) %></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate><%# GetDateDisplay(Eval("orderdate")) %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member">
                                            <ItemTemplate>
                                                <span class="gst-member-name"><%# Server.HtmlEncode(Convert.ToString(Eval("username"))) %></span><br />
                                                <span class="gst-member-id"><%# Server.HtmlEncode(Convert.ToString(Eval("userid"))) %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="productname" HeaderText="Product(s)" />
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>&#8377; <%# Eval("price", "{0:N2}") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>&#8377; <%# Eval("cgst", "{0:N2}") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>&#8377; <%# Eval("sgst", "{0:N2}") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>&#8377; <%# Eval("igst", "{0:N2}") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                                            <ItemTemplate>
                                                <a class="gst-invoice-btn" target="_blank"
                                                   href='<%# "../user/SavingProductInvoice.aspx?orderId=" + Server.UrlEncode(Convert.ToString(Eval("orderid"))) + "&userId=" + Server.UrlEncode(Convert.ToString(Eval("userid"))) %>'>
                                                    <i class="fa fa-file-text-o"></i> Invoice
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="text-center" style="padding:18px;color:#888;">No GST records found.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

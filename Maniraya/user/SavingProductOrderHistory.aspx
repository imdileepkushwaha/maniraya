<%@ Page Title="Saving Order History" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingProductOrderHistory.aspx.cs" Inherits="user_SavingProductOrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=28" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Saving Order History</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Purchase</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Order History</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage saving-order-history-page">
                <div class="profile-hero dash-subpage-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-history"></i></div>
                    <div class="profile-hero-info">
                        <h2>Order History</h2>
                        <p class="profile-hero-meta">Track your approved saving installments, delivery status, and download invoices.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SavingProductPurchase.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-shopping-cart"></i> Buy Product</a>
                        <a href="SavingProductBulkPurchase.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-layer-group"></i> Bulk Purchase</a>
                        <a href="SavingDashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-university"></i> Saving Dashboard</a>
                    </div>
                </div>

                <div class="dash-subpage-panel soh-orders-panel">
                    <div class="dash-subpage-panel-head soh-panel-head">
                        <span class="dash-subpage-panel-icon tone-blue" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                        <div class="soh-panel-head-text">
                            <h3>Approved Installments</h3>
                            <asp:Label ID="lblOrderCount" runat="server" CssClass="soh-panel-subtitle" Text="Loading orders..." />
                        </div>
                        <div class="soh-panel-filter">
                            <label for="<%= ddlRecordFilter.ClientID %>">Show</label>
                            <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control soh-records-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="dash-subpage-panel-body soh-panel-body">
                        <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="soh-empty-state">
                            <span class="soh-empty-icon" aria-hidden="true"><i class="fa fa-inbox"></i></span>
                            <h4>No orders yet</h4>
                            <p>Your saving product orders will appear here after purchase.</p>
                            <a href="SavingProductPurchase.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-shopping-cart"></i> Buy Product</a>
                        </asp:Panel>

                        <div class="soh-order-list">
                            <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound">
                                <ItemTemplate>
                                    <article class="soh-order-card">
                                        <div class="soh-order-card-head">
                                            <div class="soh-order-card-main">
                                                <span class="soh-order-card-label">Order ID</span>
                                                <strong class="soh-order-card-id"><%# Eval("orderid") %></strong>
                                                <span class="soh-order-card-meta">
                                                    Approved Date: <%# Eval("OrderDateDisplay") %>
                                                </span>
                                            </div>
                                            <div class="soh-order-card-statuses">
                                                <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("OrderStatusDisplay") %>' CssClass="dash-order-status-badge" />
                                                <asp:Label ID="lblDeliveryStatus" runat="server" Text='<%# Eval("DeliveryStatusDisplay") %>' CssClass="dash-order-status-badge" />
                                            </div>
                                        </div>

                                        <div class="soh-order-card-body">
                                            <div class="soh-order-block">
                                                <span class="soh-order-block-label">Products</span>
                                                <asp:Literal runat="server" Mode="PassThrough" Text='<%# Eval("ProductsHtml") %>' />
                                            </div>
                                            <div class="soh-order-block">
                                                <span class="soh-order-block-label">Shipping Address</span>
                                                <p class="soh-order-address"><%# Eval("AddressSummary") %></p>
                                            </div>
                                        </div>

                                        <div class="soh-order-card-foot">
                                            <div class="soh-order-delivery">
                                                <span class="soh-order-block-label">Delivery Date</span>
                                                <span class="soh-order-delivery-value<%# Eval("DeliveryDateDisplay").ToString() == "-" ? " is-muted" : "" %>"><%# Eval("DeliveryDateDisplay") %></span>
                                            </div>
                                            <asp:Panel ID="pnlConsignment" runat="server" CssClass="soh-order-consignment" Visible="false">
                                                <span class="soh-order-block-label">Consignment Number</span>
                                                <span class="soh-order-consignment-value"><asp:Literal ID="litConsignment" runat="server" /></span>
                                            </asp:Panel>
                                            <div class="soh-order-actions">
                                                <asp:HyperLink ID="lnkInvoice" runat="server" CssClass="soh-invoice-btn" Target="_blank"
                                                    NavigateUrl='<%# "SavingProductInvoice.aspx?orderId=" + HttpUtility.UrlEncode(Convert.ToString(Eval("orderid"))) + "&installmentId=" + HttpUtility.UrlEncode(Convert.ToString(Eval("InstallmentId"))) %>'>
                                                    <i class="fa fa-file-alt"></i> Download Invoice
                                                </asp:HyperLink>
                                                <asp:Label ID="lblInvoiceUnavailable" runat="server" Text="Invoice after approval" CssClass="soh-invoice-pending" />
                                            </div>
                                        </div>
                                    </article>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <asp:Panel ID="pnlPager" runat="server" CssClass="soh-pager-bar"></asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

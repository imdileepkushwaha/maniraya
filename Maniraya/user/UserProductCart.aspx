<%@ Page Title="Your Cart" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="UserProductCart.aspx.cs" Inherits="user_UserProductCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <link href="assets/css/user-product-cart.css?v=1" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Your Cart</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="FranchiseeSearchNew.aspx">My Repurchase</a></li>
            <li class="active">Your Cart</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page upc-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-shopping-cart"></i></div>
                    <div class="profile-hero-info">
                        <h2>Your Cart <asp:Literal ID="litCartTitleCount" runat="server" /></h2>
                        <p class="profile-hero-meta">Review items, update quantity, then proceed to checkout.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <asp:HyperLink ID="lnkContinueTop" runat="server" CssClass="profile-btn profile-btn-outline">
                            <i class="fa fa-arrow-left"></i> Continue Shopping
                        </asp:HyperLink>
                    </div>
                </div>

                <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="upc-card upc-empty">
                    <i class="fa fa-shopping-basket" aria-hidden="true"></i>
                    <h3>Your cart is empty</h3>
                    <p>Add products from the franchisee catalog to continue.</p>
                    <asp:HyperLink ID="lnkEmptyShop" runat="server" CssClass="upc-btn upc-btn-primary" Style="max-width: 260px; margin: 0 auto;">
                        Continue Shopping
                    </asp:HyperLink>
                </asp:Panel>

                <asp:Panel ID="pnlCart" runat="server" CssClass="upc-layout">
                    <div class="upc-card upc-card-pad">
                        <div class="upc-toolbar">
                            <div class="upc-toolbar-left">
                                <label class="upc-check">
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    Select All
                                </label>
                                <asp:LinkButton ID="btnRemoveSelected" runat="server" CssClass="upc-link-btn" OnClick="btnRemoveSelected_Click" CausesValidation="false">
                                    Remove Selected
                                </asp:LinkButton>
                            </div>
                            <span class="upc-count"><asp:Literal ID="litItemCount" runat="server" /></span>
                        </div>

                        <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                            <ItemTemplate>
                                <div class="upc-item">
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                    <asp:HiddenField ID="hfProductId" runat="server" Value='<%# Eval("ProductId") %>' />
                                    <div class="upc-item-img">
                                        <img src='<%# Eval("Image") %>' alt="product" />
                                    </div>
                                    <div>
                                        <h3 class="upc-item-name"><%# Eval("ProductName") %></h3>
                                        <p class="upc-item-meta">Product ID <%# Eval("ProductId") %></p>
                                        <div class="upc-price-row">
                                            <span class="upc-price">&#8377;<%# UserPanelCartHelper.FormatMoney(Eval("Amount")) %></span>
                                            <span class="upc-mrp" style='<%# UserPanelCartHelper.HasDiscount(Eval("MRP"), Eval("Amount")) ? "" : "display:none;" %>'>
                                                MRP &#8377;<%# UserPanelCartHelper.FormatMoney(Eval("MRP")) %>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="upc-item-side">
                                        <div class="upc-qty">
                                            <asp:LinkButton ID="btnMinus" runat="server" CommandName="Minus" CommandArgument='<%# Eval("ProductId") %>' CausesValidation="false">-</asp:LinkButton>
                                            <span><%# Eval("Quantity") %></span>
                                            <asp:LinkButton ID="btnPlus" runat="server" CommandName="Plus" CommandArgument='<%# Eval("ProductId") %>' CausesValidation="false">+</asp:LinkButton>
                                        </div>
                                        <span class="upc-line-total">&#8377;<%# UserPanelCartHelper.FormatMoney(Eval("TotalAmount")) %></span>
                                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="upc-remove" CommandName="Remove" CommandArgument='<%# Eval("ProductId") %>' CausesValidation="false">
                                            <i class="fa fa-trash"></i> Remove
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <aside class="upc-card upc-card-pad upc-summary">
                        <h3>Order Summary</h3>
                        <div class="upc-summary-row">
                            <span>Subtotal</span>
                            <strong>&#8377;<asp:Literal ID="litSubtotal" runat="server" /></strong>
                        </div>
                        <div class="upc-summary-row">
                            <span>Shipping</span>
                            <strong>&#8377;<asp:Literal ID="litShipping" runat="server" /></strong>
                        </div>
                        <p class="upc-ship-note"><asp:Literal ID="litShipNote" runat="server" /></p>
                        <div class="upc-summary-total">
                            <span>Total</span>
                            <span>&#8377;<asp:Literal ID="litPayable" runat="server" /></span>
                        </div>
                        <div class="upc-actions">
                            <asp:Button ID="btnCheckout" runat="server" CssClass="upc-btn upc-btn-primary" Text="Proceed to Checkout" OnClick="btnCheckout_Click" />
                            <asp:HyperLink ID="lnkContinue" runat="server" CssClass="upc-btn upc-btn-outline">Continue Shopping</asp:HyperLink>
                        </div>
                    </aside>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

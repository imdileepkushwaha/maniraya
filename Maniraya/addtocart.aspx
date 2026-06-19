<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="addtocart.aspx.cs" Inherits="addtocart" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="myassets/assets/css/add-to-cart.css?v=3" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:HiddenField ID="hfwalllet" runat="server" />



    <main class="cart-page-main">

        <section class="cart-page-hero">

            <div class="container">

                <!-- <p class="cart-page-kicker">Your Shopping Cart</p> -->

                <h1>Review your cart</h1>

                <p>Check products, apply coupon, and continue to secure checkout.</p>

                <div class="cart-hero-meta">

                    <span><asp:Literal ID="litHeroFreeShipping" runat="server" /></span>

                    <span>Easy 14-day return</span>

                    <span>Secure checkout</span>

                </div>

            </div>

        </section>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <section class="cart-page-content">

                    <div class="container cart-page-grid">

                        <div class="cart-items-card">

                            <div class="cart-card-head">
                                <div class="cart-card-head-main">
                                    <div class="cart-section-icon cart-section-icon--items" aria-hidden="true">
                                        <svg viewBox="0 0 24 24" fill="none">
                                            <path d="M6 6h15l-1.5 9h-12L6 6Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                            <path d="M6 6 5 3H2" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>
                                            <circle cx="9" cy="19" r="1.5" fill="currentColor"/>
                                            <circle cx="17" cy="19" r="1.5" fill="currentColor"/>
                                        </svg>
                                    </div>
                                    <div class="cart-section-copy">
                                        <!-- <span class="cart-section-kicker">Your Bag</span> -->
                                        <h2>Cart Items</h2>
                                        <p class="cart-card-subtitle">Manage quantity and remove items before checkout.</p>
                                    </div>
                                </div>
                                <div class="cart-card-head-right">
                                    <span class="cart-count-pill"><asp:Label ID="LblProductcount" runat="server" Text="0 Products"></asp:Label></span>
                                    <asp:Button runat="server" Text="Clear Cart" OnClick="Unnamed_Click" CssClass="cart-clear-btn" />
                                </div>
                            </div>



                            <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false" CssClass="cart-empty-state">

                                <div class="cart-empty-icon" aria-hidden="true">🛒</div>

                                <h3>Your cart is empty</h3>

                                <p>Add products from the store to see them here.</p>

                                <a href="index.aspx" class="secondary-btn">Continue Shopping</a>

                            </asp:Panel>



                            <asp:Panel ID="pnlCartItems" runat="server" CssClass="cart-items-list">

                                <asp:Repeater ID="rptCart" runat="server">

                                    <ItemTemplate>

                                        <article class="cart-line-item">

                                            <div class="cart-thumb">

                                                <img src='<%# GetCartImageUrl(Eval("ImageUrl"), Eval("ProductName")) %>'

                                                    alt='<%# Eval("ProductName") %>'

                                                    class="cart-thumb-img" />

                                            </div>



                                            <div class="cart-item-info">

                                                <h3><%# Eval("ProductName") %></h3>

                                                <p>Color: <%# Eval("ColorName") %> | Size: <%# Eval("SizeName") %></p>

                                                <div class="cart-item-meta">

                                                    <span class="cart-item-badge">In stock</span>

                                                </div>

                                            </div>



                                            <div class="cart-line-actions">

                                                <div class="cart-qty-box" aria-label="Quantity">

                                                    <asp:Button runat="server" BorderStyle="None" Text="-" CommandArgument='<%# Eval("Id") %>' OnCommand="DecreaseQty" CssClass="cart-qty-btn" />

                                                    <span><%# Eval("Quantity") %></span>

                                                    <asp:Button runat="server" BorderStyle="None" Text="+" CommandArgument='<%# Eval("Id") %>' OnCommand="IncreaseQty" CssClass="cart-qty-btn" />

                                                </div>



                                                <div class="cart-line-price">

                                                    <strong>₹<%# Eval("TotalAmount", "{0:0.##}") %></strong>

                                                    <span>₹<%# Eval("MRP") %> each</span>

                                                </div>



                                                <asp:Button runat="server" Text="&#10005;" CommandArgument='<%# Eval("Id") %>' OnCommand="RemoveQty" CssClass="cart-remove-btn" ToolTip="Remove item" />

                                            </div>

                                        </article>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </asp:Panel>



                            <asp:Panel ID="pnlShippingProgress" runat="server" CssClass="cart-delivery-progress" aria-label="Free shipping progress">

                                <div class="cart-progress-copy">

                                    <strong><asp:Literal ID="litProgressTitle" runat="server" /></strong>

                                    <span><asp:Literal ID="litProgressMessage" runat="server" /></span>

                                </div>

                                <div class="cart-progress-track">

                                    <span id="shippingProgressFill" runat="server" class="cart-progress-fill"></span>

                                </div>

                            </asp:Panel>



                            <div class="cart-bottom-actions">

                                <a href="index.aspx" class="secondary-btn">Continue Shopping</a>

                                <asp:Button ID="btnUpdateCart" runat="server" Text="Refresh Cart" CssClass="ghost-btn" />

                            </div>

                        </div>



                        <aside class="order-summary-card">

                            <div class="order-summary-head">
                                <div class="order-summary-head-main">
                                    <div class="cart-section-icon cart-section-icon--summary" aria-hidden="true">
                                        <svg viewBox="0 0 24 24" fill="none">
                                            <path d="M7 3h10a2 2 0 0 1 2 2v14l-3-2-3 2-3-2-3 2V5a2 2 0 0 1 2-2Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                            <path d="M9 8h6M9 12h6" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                        </svg>
                                    </div>
                                    <div class="cart-section-copy">
                                        <!-- <span class="cart-section-kicker cart-section-kicker--gold">Checkout</span> -->
                                        <h2>Order Summary</h2>
                                        <p class="summary-subtitle">Review totals and proceed to secure checkout.</p>
                                    </div>
                                </div>
                                <span class="order-summary-secure-badge">Secure Payment</span>
                            </div>



                            <div class="coupon-row">

                                <asp:TextBox ID="txtCoupon" runat="server" placeholder="Enter coupon code" aria-label="Coupon code" CssClass="coupon-input" />

                                <asp:Button ID="btnApplyCoupon" runat="server" Text="Apply" CssClass="coupon-apply-btn" />

                            </div>



                            <div class="summary-list">

                                <div class="summary-row">

                                    <span>Subtotal</span>

                                    <asp:Label ID="lblSubtotal" runat="server" Text="&#8377; 0.00" CssClass="summary-value"></asp:Label>

                                </div>

                                <div class="summary-row">

                                    <span>Discount</span>

                                    <asp:Label ID="lblDiscount" runat="server" Text="&#8377; 0.00" CssClass="summary-value text-success"></asp:Label>

                                </div>

                                <div class="summary-row">

                                    <span>Shipping</span>

                                    <asp:Label ID="lblShipping" runat="server" Text="&#8377; 0.00" CssClass="summary-value"></asp:Label>

                                </div>

                                <div class="summary-row">

                                    <span>Tax</span>

                                    <asp:Label ID="lblTax" runat="server" Text="&#8377; 0.00" CssClass="summary-value"></asp:Label>

                                </div>

                                <div class="summary-row">

                                    <span>Wallet Deduction</span>

                                    <asp:Label ID="Lblwalletdeduction" runat="server" Text="&#8377; 0.00" CssClass="summary-value text-success"></asp:Label>

                                </div>

                            </div>



                            <div class="summary-total">

                                <span>Total Payable</span>

                                <asp:Label ID="lblTotal" runat="server" Text="&#8377; 0.00" CssClass="summary-total-value"></asp:Label>

                            </div>



                            <asp:LinkButton ID="LinkButton1" CssClass="checkout-btn-static" runat="server" PostBackUrl="~/addaddress.aspx">Proceed to Checkout</asp:LinkButton>

                            <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="checkout-btn-static" Visible="false" OnClick="btnCheckout_Click" />



                            <div class="summary-payments" aria-label="Accepted payments">

                                <img src="myassets/assets/images/payment/1.svg" alt="Payment method" />

                                <img src="myassets/assets/images/payment/2.svg" alt="Payment method" />

                                <img src="myassets/assets/images/payment/3.svg" alt="Payment method" />

                                <img src="myassets/assets/images/payment/4.svg" alt="Payment method" />

                            </div>

                            <p class="secure-note">100% secure payment with trusted gateways.</p>

                            <!-- <ul class="summary-benefits">

                                <li>No-cost EMI options available</li>

                                <li>Trusted seller warranty support</li>

                                <li>Safe delivery with order tracking</li>

                            </ul> -->

                        </aside>

                    </div>

                </section>

            </ContentTemplate>

        </asp:UpdatePanel>

    </main>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentScript" Runat="Server">

</asp:Content>



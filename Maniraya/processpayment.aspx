<%@ Page Title="Process Payment" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="processpayment.aspx.cs" Inherits="processpayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function selectPaymentMethod(method) {
            var tabs = document.querySelectorAll(".pay-method-tab");
            var panels = {
                card: document.getElementById("cardFields"),
                upi: document.getElementById("upiFields"),
                cod: document.getElementById("codFields")
            };

            tabs.forEach(function (tab) {
                tab.classList.toggle("is-active", tab.getAttribute("data-method") === method);
            });

            Object.keys(panels).forEach(function (key) {
                if (panels[key]) {
                    panels[key].hidden = key !== method;
                }
            });
        }

        document.addEventListener("DOMContentLoaded", function () {
            selectPaymentMethod("card");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main class="pay-page-main">
                <section class="pay-section">
                    <div class="container">
                        <nav class="addr-steps" aria-label="Checkout progress">
                            <span class="addr-step is-done">Cart</span>
                            <span class="addr-step-line" aria-hidden="true"></span>
                            <span class="addr-step is-done">Address</span>
                            <span class="addr-step-line" aria-hidden="true"></span>
                            <span class="addr-step is-active">Payment</span>
                        </nav>

                        <div class="pay-shell">
                            <header class="pay-page-head">
                                <div class="pay-page-head-copy">
                                    <span class="addr-eyebrow">Secure checkout</span>
                                    <h1>Complete your payment</h1>
                                    <p>Review your order and choose a payment method to place your order.</p>
                                </div>
                                <div class="pay-secure-badge" aria-label="Secure payment">
                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                        <path d="M12 3L4 7v6c0 4.4 3.4 8.5 8 9.5 4.6-1 8-5.1 8-9.5V7l-8-4Z" stroke="currentColor" stroke-width="1.8"/>
                                        <path d="m9.5 12 1.8 1.8L15 10.1" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>
                                    </svg>
                                    <span>SSL Secured</span>
                                </div>
                            </header>

                            <div class="pay-grid">
                                <div class="pay-panel pay-panel-methods">
                                    <div class="pay-panel-head">
                                        <span class="pay-panel-icon" aria-hidden="true">
                                            <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                                <path d="M4 7H20V17H4V7ZM4 9L12 14L20 9" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>
                                            </svg>
                                        </span>
                                        <div>
                                            <h2>Payment method</h2>
                                            <p>Select how you would like to pay for this order.</p>
                                        </div>
                                    </div>

                                    <div class="pay-method-tabs" role="tablist" aria-label="Payment methods">
                                        <button type="button" class="pay-method-tab is-active" data-method="card" onclick="selectPaymentMethod('card')" id="btnCard" role="tab" aria-selected="true">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <rect x="3" y="6" width="18" height="12" rx="2" stroke="currentColor" stroke-width="1.8"/>
                                                <path d="M3 10H21" stroke="currentColor" stroke-width="1.8"/>
                                            </svg>
                                            <span>Card</span>
                                        </button>
                                        <button type="button" class="pay-method-tab" data-method="upi" onclick="selectPaymentMethod('upi')" id="btnUPI" role="tab" aria-selected="false">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <path d="M7 8H17M7 12H14M7 16H12" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                                <rect x="4" y="4" width="16" height="16" rx="3" stroke="currentColor" stroke-width="1.8"/>
                                            </svg>
                                            <span>UPI</span>
                                        </button>
                                        <button type="button" class="pay-method-tab" data-method="cod" onclick="selectPaymentMethod('cod')" id="btnCOD" role="tab" aria-selected="false">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                                <path d="M9 11v6M15 11v6" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                            </svg>
                                            <span>COD</span>
                                        </button>
                                    </div>

                                    <div class="pay-fields-panel" id="cardFields" role="tabpanel">
                                        <div class="pay-form-grid">
                                            <div class="auth-field pay-form-span-2">
                                                <label for="txtCardNumber">Card number</label>
                                                <div class="auth-input-wrap">
                                                    <span class="auth-input-icon" aria-hidden="true">
                                                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><rect x="3" y="6" width="18" height="12" rx="2" stroke="currentColor" stroke-width="1.8"/><path d="M3 10H21" stroke="currentColor" stroke-width="1.8"/></svg>
                                                    </span>
                                                    <input type="text" id="txtCardNumber" class="form-control auth-input" maxlength="19" placeholder="1234 5678 9012 3456" autocomplete="cc-number" />
                                                </div>
                                            </div>
                                            <div class="auth-field">
                                                <label for="txtCardExpiry">Expiry</label>
                                                <input type="text" id="txtCardExpiry" class="form-control auth-input" maxlength="5" placeholder="MM/YY" autocomplete="cc-exp" />
                                            </div>
                                            <div class="auth-field">
                                                <label for="txtCardCVV">CVV</label>
                                                <input type="password" id="txtCardCVV" class="form-control auth-input" maxlength="4" placeholder="•••" autocomplete="cc-csc" />
                                            </div>
                                            <div class="auth-field pay-form-span-2">
                                                <label for="txtCardName">Name on card</label>
                                                <input type="text" id="txtCardName" class="form-control auth-input" maxlength="50" placeholder="Cardholder name" autocomplete="cc-name" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="pay-fields-panel" id="upiFields" role="tabpanel" hidden>
                                        <div class="pay-upi-wrap">
                                            <div class="auth-field">
                                                <label for="txtUPI">UPI ID</label>
                                                <input type="text" id="txtUPI" class="form-control auth-input" maxlength="50" placeholder="yourname@bank" />
                                            </div>
                                            <div class="pay-qr-card">
                                                <p class="pay-qr-label">Scan to pay</p>
                                                <img src="myassets/assets/images/QRCODE.jpeg" alt="UPI QR code" class="pay-qr-image" />
                                            </div>
                                            <div class="auth-field">
                                                <label for="<%= txtransactionid.ClientID %>">Transaction ID</label>
                                                <div class="auth-input-wrap">
                                                    <span class="auth-input-icon" aria-hidden="true">
                                                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M8 8H16M8 12H13M8 16H11M6 4H18C19.1 4 20 4.9 20 6V18C20 19.1 19.1 20 18 20H6C4.9 20 4 19.1 4 18V6C4 4.9 4.9 4 6 4Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                                    </span>
                                                    <asp:TextBox ID="txtransactionid" runat="server" CssClass="form-control auth-input" placeholder="Enter UPI transaction ID" />
                                                </div>
                                                <p class="pay-field-hint">Enter the transaction reference after completing UPI payment.</p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="pay-fields-panel" id="codFields" role="tabpanel" hidden>
                                        <div class="pay-cod-card">
                                            <span class="pay-cod-icon" aria-hidden="true">
                                                <svg width="28" height="28" viewBox="0 0 24 24" fill="none">
                                                    <path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                                    <path d="M9 11v6M15 11v6M8 7V5a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                                </svg>
                                            </span>
                                            <div>
                                                <h3>Cash on delivery</h3>
                                                <p>Pay with cash when your order arrives at your doorstep.</p>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Button ID="Btnpayment" runat="server" CssClass="pay-place-btn" Text="Place Order" OnClick="Btnpayment_Click" />

                                    <div class="pay-trust-row">
                                        <span>
                                            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M12 3L4 7v6c0 4.4 3.4 8.5 8 9.5 4.6-1 8-5.1 8-9.5V7l-8-4Z" stroke="currentColor" stroke-width="1.8"/></svg>
                                            Encrypted checkout
                                        </span>
                                        <span>
                                            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M12 8V12M12 16H12.01M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                            Buyer protection
                                        </span>
                                    </div>
                                </div>

                                <aside class="pay-panel pay-panel-summary">
                                    <div class="pay-panel-head">
                                        <span class="pay-panel-icon pay-panel-icon-gold" aria-hidden="true">
                                            <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                                <path d="M9 5H5C3.9 5 3 5.9 3 7V19C3 20.1 3.9 21 5 21H19C20.1 21 21 20.1 21 19V7C21 6.9 20.1 6 19 6H15M9 5C9 3.9 9.9 3 11 3H13C14.1 3 15 3.9 15 5M9 5C9 6.1 9.9 7 11 7H13C14.1 7 15 6.1 15 5" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                            </svg>
                                        </span>
                                        <div>
                                            <h2>Order summary</h2>
                                            <p>Review totals before placing your order.</p>
                                        </div>
                                    </div>

                                    <div class="pay-delivery-card">
                                        <div class="pay-delivery-head">
                                            <span class="pay-delivery-label">Deliver to</span>
                                            <a href="addaddress.aspx" class="pay-edit-link">Change</a>
                                        </div>
                                        <p class="pay-delivery-name"><asp:Label ID="Lblusername" runat="server" /></p>
                                        <p class="pay-delivery-line"><asp:Label ID="Lbladdress" runat="server" /></p>
                                        <p class="pay-delivery-line pay-delivery-muted">
                                            <asp:Label ID="Lblcity" runat="server" />
                                            <asp:Label ID="LblPincode" runat="server" />
                                        </p>
                                        <p class="pay-delivery-phone">
                                            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <path d="M6.6 10.8C8.2 14 10 15.8 13.2 17.4L15.4 15.2C15.7 14.9 16.1 14.8 16.5 15C17.6 15.4 18.8 15.6 20 15.6C20.6 15.6 21 16 21 16.6V20C21 20.6 20.6 21 20 21C10.6 21 3 13.4 3 4C3 3.4 3.4 3 4 3H7.4C8 3 8.4 3.4 8.4 4C8.4 5.2 8.6 6.4 9 7.5C9.1 7.9 9 8.3 8.7 8.6L6.6 10.8Z" fill="currentColor"/>
                                            </svg>
                                            <asp:Label ID="Lblmobile" runat="server" />
                                        </p>
                                    </div>

                                    <ul class="pay-summary-list">
                                        <li><span>Subtotal</span><asp:Label ID="lblSubtotal" runat="server" CssClass="pay-summary-value" /></li>
                                        <li class="pay-summary-discount"><span>Discount</span><asp:Label ID="lblDiscount" runat="server" CssClass="pay-summary-value" /></li>
                                        <li><span>Shipping</span><span class="pay-summary-free">Free</span></li>
                                        <li><span>Tax</span><asp:Label ID="lblTax" runat="server" CssClass="pay-summary-value" /></li>
                                    </ul>

                                    <div class="pay-total-card">
                                        <span>Total payable</span>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="pay-total-amount" />
                                    </div>
                                </aside>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

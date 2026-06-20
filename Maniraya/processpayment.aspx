<%@ Page Title="Process Payment" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="processpayment.aspx.cs" Inherits="processpayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function selectPaymentMethod(method) {
            var tabs = document.querySelectorAll(".pay-method-tab");
            var panels = {
                online: document.getElementById("onlineFields"),
                qr: document.getElementById("qrFields")
            };
            var methodField = document.getElementById("<%= hfPaymentMethod.ClientID %>");

            tabs.forEach(function (tab) {
                tab.classList.toggle("is-active", tab.getAttribute("data-method") === method);
            });

            Object.keys(panels).forEach(function (key) {
                if (panels[key]) {
                    panels[key].hidden = key !== method;
                }
            });

            if (methodField) {
                methodField.value = method;
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            var savedMethod = document.getElementById("<%= hfPaymentMethod.ClientID %>");
            selectPaymentMethod(savedMethod && savedMethod.value ? savedMethod.value : "online");

            var uploadInput = document.getElementById("<%= fuReceipt.ClientID %>");
            var uploadZone = document.getElementById("payReceiptUploadZone");
            var uploadSelection = document.getElementById("payReceiptUploadSelection");
            var uploadPreview = document.getElementById("payReceiptUploadPreview");
            var uploadFilechip = document.getElementById("payReceiptUploadFilechip");
            var uploadClear = document.getElementById("payReceiptUploadClear");

            if (!uploadInput || !uploadZone) {
                return;
            }

            function showReceiptPreview(file) {
                if (!file || !file.type.match(/^image\//)) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (event) {
                    uploadPreview.src = event.target.result;
                    uploadFilechip.textContent = file.name;
                    uploadZone.hidden = true;
                    uploadSelection.hidden = false;
                };
                reader.readAsDataURL(file);
            }

            uploadInput.addEventListener("change", function () {
                if (uploadInput.files && uploadInput.files[0]) {
                    showReceiptPreview(uploadInput.files[0]);
                }
            });

            if (uploadClear) {
                uploadClear.addEventListener("click", function () {
                    uploadInput.value = "";
                    uploadPreview.src = "";
                    uploadFilechip.textContent = "";
                    uploadSelection.hidden = true;
                    uploadZone.hidden = false;
                });
            }
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
                                    <p>Choose online bank transfer or scan QR, then submit your transaction ID and payment receipt.</p>
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
                                                <path d="M3 10h18M5 6h14a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                                <path d="M8 14h4" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                            </svg>
                                        </span>
                                        <div>
                                            <h2>Payment method</h2>
                                            <p>Pay online via bank transfer or scan the QR code.</p>
                                        </div>
                                    </div>

                                    <asp:HiddenField ID="hfPaymentMethod" runat="server" Value="online" />

                                    <div class="pay-method-tabs pay-method-tabs--two" role="tablist" aria-label="Payment methods">
                                        <button type="button" class="pay-method-tab is-active" data-method="online" onclick="selectPaymentMethod('online')" role="tab" aria-selected="true">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <path d="M3 10h18M5 6h14a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                                <path d="M8 14h4" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                            </svg>
                                            <span>Online</span>
                                        </button>
                                        <button type="button" class="pay-method-tab" data-method="qr" onclick="selectPaymentMethod('qr')" role="tab" aria-selected="false">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                <path d="M4 8V4H8M16 4h4v4M20 16v4h-4M8 20H4v-4" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                                                <path d="M9 9h2v2H9zM13 9h2v2h-2zM9 13h2v2H9zM13 13h3v3h-3z" fill="currentColor"/>
                                            </svg>
                                            <span>QR</span>
                                        </button>
                                    </div>

                                    <div class="pay-fields-panel" id="onlineFields" role="tabpanel">
                                        <div class="pay-method-panel-intro">
                                            <h3>Online bank transfer</h3>
                                            <p>Transfer the order amount to the company bank account below.</p>
                                        </div>

                                    <asp:Panel ID="pnlNoBank" runat="server" Visible="false" CssClass="pay-bank-empty">
                                        <p>Bank details are not available right now. Please contact support or try again later.</p>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlBankAccounts" runat="server" CssClass="pay-bank-list">
                                        <asp:HiddenField ID="hfSelectedBankId" runat="server" />
                                        <asp:Repeater ID="rptBankAccounts" runat="server" OnItemDataBound="rptBankAccounts_ItemDataBound">
                                            <ItemTemplate>
                                                <article class="pay-bank-card">
                                                    <div class="pay-bank-card-top">
                                                        <label class="pay-bank-select">
                                                            <asp:RadioButton ID="rbBank" runat="server" GroupName="SelectedBank" Checked='<%# Container.ItemIndex == 0 %>' />
                                                            <span class="pay-bank-select-copy">
                                                                <strong><%# GetBankField(Container.DataItem, "BankName", "bankname") %></strong>
                                                                <span>Account ending <%# MaskAccountNo(GetBankField(Container.DataItem, "AccountNo", "accountno")) %></span>
                                                            </span>
                                                        </label>
                                                        <asp:HiddenField ID="hfBankId" runat="server" Value='<%# GetBankField(Container.DataItem, "id", "Id") %>' />
                                                    </div>

                                                    <div class="pay-bank-card-body pay-bank-card-body--online">
                                                        <dl class="pay-bank-details">
                                                            <div>
                                                                <dt>Account holder</dt>
                                                                <dd><%# GetBankField(Container.DataItem, "AccountHolderName", "accountholdername") %></dd>
                                                            </div>
                                                            <div>
                                                                <dt>Account number</dt>
                                                                <dd><%# GetBankField(Container.DataItem, "AccountNo", "accountno") %></dd>
                                                            </div>
                                                            <div>
                                                                <dt>IFSC code</dt>
                                                                <dd><%# GetBankField(Container.DataItem, "IFSCCode", "ifsccode") %></dd>
                                                            </div>
                                                        </dl>
                                                    </div>
                                                </article>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                    </div>

                                    <div class="pay-fields-panel" id="qrFields" role="tabpanel" hidden>
                                        <div class="pay-method-panel-intro">
                                            <h3>Scan &amp; pay</h3>
                                            <p>Scan the QR code using any UPI app and pay the exact order amount.</p>
                                        </div>

                                        <div class="pay-qr-amount-banner">
                                            <span class="pay-qr-amount-label">Amount to pay</span>
                                            <asp:Label ID="lblQrAmount" runat="server" CssClass="pay-qr-amount-value" Text="₹0.00" />
                                        </div>

                                        <asp:Panel ID="pnlNoQr" runat="server" Visible="false" CssClass="pay-bank-empty">
                                            <p>QR code is not available right now. Please use online bank transfer or contact support.</p>
                                        </asp:Panel>

                                        <div class="pay-qr-scan-shell">
                                            <ol class="pay-qr-steps" aria-label="QR payment steps">
                                                <li><span>1</span> Scan QR</li>
                                                <li><span>2</span> Pay amount</li>
                                                <li><span>3</span> Upload proof</li>
                                            </ol>

                                            <asp:Panel ID="pnlQrPayment" runat="server" CssClass="pay-qr-list">
                                                <asp:Repeater ID="rptQrAccounts" runat="server">
                                                    <ItemTemplate>
                                                        <article class="pay-qr-card pay-qr-card--panel">
                                                            <div class="pay-qr-card-visual">
                                                                <div class="pay-qr-frame" aria-hidden="true">
                                                                    <span class="pay-qr-corner pay-qr-corner--tl"></span>
                                                                    <span class="pay-qr-corner pay-qr-corner--tr"></span>
                                                                    <span class="pay-qr-corner pay-qr-corner--bl"></span>
                                                                    <span class="pay-qr-corner pay-qr-corner--br"></span>
                                                                    <img src='<%# GetQrImageUrl(Container.DataItem) %>' alt='<%# "Payment QR code for " + GetBankField(Container.DataItem, "BankName", "bankname") %>' class="pay-qr-image" />
                                                                </div>
                                                                <p class="pay-qr-scan-hint">Open Google Pay, PhonePe, Paytm or any UPI app</p>
                                                            </div>
                                                            <div class="pay-qr-card-meta">
                                                                <p class="pay-qr-bank-name"><%# GetBankField(Container.DataItem, "BankName", "bankname") %></p>
                                                                <p class="pay-qr-account">Account ending <%# MaskAccountNo(GetBankField(Container.DataItem, "AccountNo", "accountno")) %></p>
                                                                <div class="pay-qr-upi-tags" aria-hidden="true">
                                                                    <span>GPay</span>
                                                                    <span>PhonePe</span>
                                                                    <span>Paytm</span>
                                                                    <span>BHIM</span>
                                                                </div>
                                                            </div>
                                                        </article>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>

                                            <div class="pay-qr-fallback">
                                                <asp:Panel ID="pnlFallbackQr" runat="server">
                                                    <article class="pay-qr-card pay-qr-card--panel pay-qr-card--fallback">
                                                        <div class="pay-qr-card-visual">
                                                            <div class="pay-qr-frame" aria-hidden="true">
                                                                <span class="pay-qr-corner pay-qr-corner--tl"></span>
                                                                <span class="pay-qr-corner pay-qr-corner--tr"></span>
                                                                <span class="pay-qr-corner pay-qr-corner--bl"></span>
                                                                <span class="pay-qr-corner pay-qr-corner--br"></span>
                                                                <asp:Image ID="imgFallbackQr" runat="server" ImageUrl="~/myassets/assets/images/QRCODE.jpeg" CssClass="pay-qr-image" AlternateText="Payment QR code" />
                                                            </div>
                                                            <p class="pay-qr-scan-hint">Open Google Pay, PhonePe, Paytm or any UPI app</p>
                                                        </div>
                                                        <div class="pay-qr-card-meta">
                                                            <p class="pay-qr-bank-name">Company payment QR</p>
                                                            <p class="pay-qr-account">Scan and pay the exact order total</p>
                                                        </div>
                                                    </article>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="pay-proof-section">
                                        <div class="pay-proof-head">
                                            <h3>Payment proof</h3>
                                            <p>After transferring the amount, enter your transaction reference and upload the payment receipt.</p>
                                        </div>

                                        <div class="auth-field pay-proof-field">
                                            <label for="<%= txtransactionid.ClientID %>">Transaction ID</label>
                                            <div class="pay-proof-input-wrap">
                                                <span class="pay-proof-input-icon" aria-hidden="true">
                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M8 8H16M8 12H13M8 16H11M6 4H18C19.1 4 20 4.9 20 6V18C20 19.1 19.1 20 18 20H6C4.9 20 4 19.1 4 18V6C4 4.9 4.9 4 6 4Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                                </span>
                                                <asp:TextBox ID="txtransactionid" runat="server" CssClass="pay-proof-input" placeholder="Enter UTR / transaction reference" />
                                            </div>
                                            <p class="pay-field-hint">Use the transaction ID from your bank or UPI app.</p>
                                        </div>

                                        <div class="auth-field pay-receipt-field">
                                            <label for="<%= fuReceipt.ClientID %>">Payment receipt</label>
                                            <div class="pay-receipt-upload">
                                                <div class="pay-receipt-dropzone" id="payReceiptUploadZone">
                                                    <div class="pay-receipt-dropzone-inner">
                                                        <span class="pay-receipt-dropzone-icon" aria-hidden="true">
                                                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none"><path d="M12 16V8M12 8l-3 3M12 8l3 3" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/><path d="M4 16v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2v-2" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/><path d="M16 10l-2.2-2.2a2 2 0 0 0-2.8 0L8 10" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <p class="pay-receipt-dropzone-title">Upload payment screenshot</p>
                                                        <p class="pay-receipt-dropzone-text">Drag &amp; drop your receipt here, or <span>browse file</span></p>
                                                        <p class="pay-receipt-dropzone-meta">JPG, PNG, WEBP · UTR / amount clearly visible</p>
                                                    </div>
                                                    <asp:FileUpload ID="fuReceipt" runat="server" CssClass="pay-receipt-file-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                                </div>

                                                <div class="pay-receipt-preview-card" id="payReceiptUploadSelection" hidden>
                                                    <div class="pay-receipt-preview-thumb">
                                                        <img id="payReceiptUploadPreview" src="" alt="Payment receipt preview" />
                                                    </div>
                                                    <div class="pay-receipt-preview-copy">
                                                        <span class="pay-receipt-preview-label">Receipt attached</span>
                                                        <span class="pay-receipt-preview-name" id="payReceiptUploadFilechip"></span>
                                                        <button type="button" class="pay-receipt-preview-remove" id="payReceiptUploadClear">
                                                            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M6 6l12 12M18 6 6 18" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                                            Remove file
                                                        </button>
                                                    </div>
                                                </div>
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
                                            Order verified after payment proof
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
        <Triggers>
            <asp:PostBackTrigger ControlID="Btnpayment" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

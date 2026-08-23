<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="UserProductCheckout.aspx.cs" Inherits="user_UserProductCheckout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <link href="assets/css/user-product-cart.css?v=6" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Checkout</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="UserProductCart.aspx">Your Cart</a></li>
            <li class="active">Checkout</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page upc-page saving-purchase-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-credit-card"></i></div>
                    <div class="profile-hero-info">
                        <h2>Checkout</h2>
                        <p class="profile-hero-meta">Confirm address, complete payment, then review and submit.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProductCart.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back to Cart</a>
                    </div>
                </div>

                <asp:HiddenField ID="hfStep" runat="server" Value="1" />
                <asp:HiddenField ID="HDFilename" runat="server" />
                <asp:HiddenField ID="hfPaymentMethod" runat="server" Value="online" />
                <asp:HiddenField ID="hfSelectedBankId" runat="server" />

                <div class="upc-steps">
                    <div class="upc-step" id="step1Box" runat="server">
                        <span class="upc-step-num">1</span> Address
                    </div>
                    <div class="upc-step" id="step2Box" runat="server">
                        <span class="upc-step-num">2</span> Payment
                    </div>
                    <div class="upc-step" id="step3Box" runat="server">
                        <span class="upc-step-num">3</span> Review
                    </div>
                </div>

                <div class="upc-layout">
                    <div class="upc-card upc-card-pad">
                        <asp:Panel ID="pnlStepAddress" runat="server">
                            <h3 class="upc-section-title"><i class="fa fa-map-marker-alt"></i> Shipping Address</h3>
                            <div class="upc-address-options">
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbProfileAddress" runat="server" GroupName="AddrMode" AutoPostBack="true" Checked="true" OnCheckedChanged="AddressMode_Changed" />
                                    <label for="<%= rbProfileAddress.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-user"></i></span>
                                        <span class="upc-choice-text">
                                            <strong>Use Profile Address</strong>
                                            <span>Deliver to the address saved on your profile</span>
                                        </span>
                                    </label>
                                </div>
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbNewAddress" runat="server" GroupName="AddrMode" />
                                    <label for="<%= rbNewAddress.ClientID %>" onclick="return upcOpenNewAddress(event);">
                                        <span class="upc-choice-icon"><i class="fa fa-plus"></i></span>
                                        <span class="upc-choice-text">
                                            <strong>Add New Address</strong>
                                            <span>Enter a different delivery address</span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <asp:HiddenField ID="hfShowAddressModal" runat="server" Value="0" />
                            <asp:HiddenField ID="hfHasNewAddress" runat="server" Value="0" />
                            <asp:Button ID="btnOpenAddressModal" runat="server" CssClass="upc-addr-hidden-btn" Text="Open address" OnClick="btnOpenAddressModal_Click" CausesValidation="false" TabIndex="-1" aria-hidden="true" />

                            <asp:Panel ID="pnlProfileView" runat="server" CssClass="upc-review-block">
                                <p class="upc-review-label">Profile address</p>
                                <p class="upc-address-preview"><asp:Literal ID="litProfileAddress" runat="server" /></p>
                            </asp:Panel>

                            <asp:Panel ID="pnlNewAddressPreview" runat="server" Visible="false" CssClass="upc-review-block">
                                <p class="upc-review-label">New delivery address</p>
                                <p class="upc-address-preview"><asp:Literal ID="litNewAddress" runat="server" /></p>
                                <asp:Button ID="btnEditNewAddress" runat="server" CssClass="upc-addr-edit" Text="Change address" OnClick="btnEditNewAddress_Click" CausesValidation="false" />
                            </asp:Panel>

                            <div id="upcAddressModal" class="upc-addr-modal" aria-hidden="true">
                                <div class="upc-addr-modal-card" role="dialog" aria-modal="true" aria-labelledby="upcAddressModalTitle">
                                    <button type="button" class="upc-addr-modal-close" onclick="document.getElementById('<%= btnCancelNewAddress.ClientID %>').click();" aria-label="Close">
                                        <i class="fa fa-times" aria-hidden="true"></i>
                                    </button>
                                    <div class="upc-addr-modal-head">
                                        <h4 id="upcAddressModalTitle">Add new address</h4>
                                        <p>Enter a different delivery address. This will not change your profile address.</p>
                                    </div>
                                    <asp:Panel ID="pnlNewAddress" runat="server">
                                        <div class="form-group">
                                            <label for="<%= txtaddress.ClientID %>"><i class="fa fa-home"></i> Address</label>
                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="House no., street, landmark" />
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="<%= ddstate.ClientID %>"><i class="fa fa-map"></i> State</label>
                                                    <asp:DropDownList ID="ddstate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddstate_SelectedIndexChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="<%= ddcity.ClientID %>"><i class="fa fa-building"></i> City</label>
                                                    <asp:DropDownList ID="ddcity" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="<%= txtareaname.ClientID %>"><i class="fa fa-location-arrow"></i> Area / Locality</label>
                                                    <asp:TextBox ID="txtareaname" runat="server" CssClass="form-control" placeholder="Area or locality" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="<%= txtpincode.ClientID %>"><i class="fa fa-thumb-tack"></i> Pincode</label>
                                                    <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control" MaxLength="6" placeholder="Pincode" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="upc-addr-modal-actions">
                                            <asp:Button ID="btnCancelNewAddress" runat="server" CssClass="upc-btn upc-btn-outline" Text="Cancel" OnClick="btnCancelNewAddress_Click" CausesValidation="false" />
                                            <asp:Button ID="btnSaveNewAddress" runat="server" CssClass="upc-btn upc-btn-primary" Text="Save address" OnClick="btnSaveNewAddress_Click" CausesValidation="false" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="upc-nav">
                                <asp:Button ID="btnToPayment" runat="server" CssClass="upc-btn upc-btn-primary" Text="Continue to Payment" OnClick="btnToPayment_Click" CausesValidation="false" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlStepPayment" runat="server" Visible="false">
                            <h3 class="upc-section-title"><i class="fa fa-credit-card"></i> Payment Method</h3>
                            <p class="upc-pay-intro">Choose online bank transfer or scan QR, then submit your transaction ID and payment receipt.</p>

                            <div class="upc-pay-tabs" role="tablist" aria-label="Payment methods">
                                <button type="button" class="upc-pay-tab is-active" data-method="online" onclick="selectCheckoutPayMethod('online')" role="tab" aria-selected="true">
                                    <i class="fa fa-university" aria-hidden="true"></i>
                                    <span>Online</span>
                                </button>
                                <button type="button" class="upc-pay-tab" data-method="qr" onclick="selectCheckoutPayMethod('qr')" role="tab" aria-selected="false">
                                    <i class="fa fa-qrcode" aria-hidden="true"></i>
                                    <span>QR</span>
                                </button>
                            </div>

                            <div class="upc-pay-panel" id="upcOnlineFields" role="tabpanel">
                                <div class="upc-pay-panel-intro">
                                    <h4>Online bank transfer</h4>
                                    <p>Transfer the order amount to the company bank account below.</p>
                                </div>
                                <asp:Panel ID="pnlNoCompanyAccount" runat="server" Visible="false">
                                    <p class="upc-ship-note">Bank details are not available right now. Please contact support.</p>
                                </asp:Panel>
                                <asp:Panel ID="pnlCompanyAccount" runat="server" CssClass="upc-bank-list">
                                    <asp:Repeater ID="rptBankAccounts" runat="server" OnItemDataBound="rptBankAccounts_ItemDataBound">
                                        <ItemTemplate>
                                            <article class="upc-bank-card">
                                                <label class="upc-bank-select">
                                                    <asp:RadioButton ID="rbBank" runat="server" GroupName="SelectedBank" />
                                                    <span>
                                                        <strong><%# GetBankField(Container.DataItem, "BankName", "bankname") %></strong>
                                                        <em>Account ending <%# MaskAccountNo(GetBankField(Container.DataItem, "AccountNo", "accountno")) %></em>
                                                    </span>
                                                </label>
                                                <asp:HiddenField ID="hfBankId" runat="server" Value='<%# GetBankField(Container.DataItem, "id", "Id") %>' />
                                                <dl class="upc-bank-details">
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
                                            </article>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>

                            <div class="upc-pay-panel" id="upcQrFields" role="tabpanel" hidden>
                                <div class="upc-pay-panel-intro">
                                    <h4>Scan &amp; pay</h4>
                                    <p>Scan the QR code using any UPI app and pay the exact order amount.</p>
                                </div>
                                <div class="upc-qr-amount">
                                    <span>Amount to pay</span>
                                    <asp:Label ID="lblQrAmount" runat="server" Text="₹0.00" />
                                </div>
                                <ol class="upc-qr-steps" aria-label="QR payment steps">
                                    <li><span>1</span> Scan QR</li>
                                    <li><span>2</span> Pay amount</li>
                                    <li><span>3</span> Upload proof</li>
                                </ol>
                                <asp:Panel ID="pnlQrPayment" runat="server" CssClass="upc-qr-list">
                                    <asp:Repeater ID="rptQrAccounts" runat="server">
                                        <ItemTemplate>
                                            <article class="upc-qr-card">
                                                <div class="upc-qr-frame">
                                                    <img src='<%# GetQrImageUrl(Container.DataItem) %>' alt="Payment QR code" />
                                                </div>
                                                <p class="upc-qr-hint">Open Google Pay, PhonePe, Paytm or any UPI app</p>
                                                <p class="upc-qr-bank"><%# GetBankField(Container.DataItem, "BankName", "bankname") %></p>
                                                <p class="upc-qr-acc">Account ending <%# MaskAccountNo(GetBankField(Container.DataItem, "AccountNo", "accountno")) %></p>
                                            </article>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                <asp:Panel ID="pnlNoQr" runat="server" Visible="false">
                                    <p class="upc-ship-note">QR code is not available right now. Please use online bank transfer or contact support.</p>
                                </asp:Panel>
                            </div>

                            <div class="upc-pay-proof">
                                <h4>Payment proof</h4>
                                <p>After transferring the amount, enter your transaction reference and upload the payment receipt.</p>
                                <div class="form-group">
                                    <label for="<%= txttransactionid.ClientID %>"><i class="fa fa-exchange-alt"></i> Transaction ID</label>
                                    <asp:TextBox ID="txttransactionid" runat="server" CssClass="form-control" placeholder="Enter UTR / transaction reference" />
                                </div>
                                <div class="form-group profile-upload-field topup-payment-upload">
                                    <label><i class="fa fa-camera"></i> Payment receipt</label>
                                    <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="savingPaymentUploadZone">
                                        <div class="profile-upload-zone-inner">
                                            <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                            <p class="profile-upload-title">Drop payment screenshot here</p>
                                            <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                            <p class="profile-upload-meta">JPG, PNG, WEBP — receipt clearly visible</p>
                                        </div>
                                        <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                    </div>
                                    <div class="profile-upload-selection profile-upload-selection-attach" id="savingPaymentUploadSelection" hidden>
                                        <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                            <img id="savingPaymentUploadPreview" src="" alt="Payment screenshot preview" />
                                        </div>
                                        <div class="profile-upload-selection-info">
                                            <span class="profile-upload-filechip" id="savingPaymentUploadFilechip"></span>
                                            <button type="button" class="profile-upload-clear" id="savingPaymentUploadClear">
                                                <i class="fa fa-times"></i> Remove
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="saving-purchase-info-note">
                                <i class="fa fa-info-circle" aria-hidden="true"></i>
                                <span>After making the payment, enter your UTR/transaction ID and upload a clear screenshot. Your request will be verified by the admin team.</span>
                            </div>
                            <div class="upc-nav">
                                <asp:Button ID="btnBackAddress" runat="server" CssClass="upc-btn upc-btn-outline" Text="Back" OnClick="btnBackAddress_Click" CausesValidation="false" />
                                <asp:Button ID="btnToReview" runat="server" CssClass="upc-btn upc-btn-primary" Text="Continue to Review" OnClick="btnToReview_Click" CausesValidation="false" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlStepReview" runat="server" Visible="false">
                            <h3 class="upc-section-title"><i class="fa fa-clipboard-check"></i> Review Order</h3>
                            <div class="upc-review-block">
                                <p class="upc-review-label">Delivery address</p>
                                <p class="upc-address-preview"><asp:Literal ID="litReviewAddress" runat="server" /></p>
                            </div>
                            <div class="upc-review-block">
                                <p class="upc-review-label">Payment</p>
                                <p class="upc-address-preview">
                                    Mode: <strong><asp:Literal ID="litReviewMode" runat="server" /></strong><br />
                                    UTR / Transaction ID: <strong><asp:Literal ID="litReviewTxn" runat="server" /></strong>
                                </p>
                                <asp:Image ID="imgReviewReceipt" runat="server" CssClass="upc-receipt" Visible="false" />
                            </div>
                            <div class="upc-review-block">
                                <p class="upc-review-label">Items</p>
                                <asp:Repeater ID="rptReview" runat="server">
                                    <ItemTemplate>
                                        <div class="upc-mini-item">
                                            <img src='<%# Eval("Image") %>' alt="product" />
                                            <div>
                                                <strong><%# Eval("ProductName") %></strong>
                                                <span class="upc-item-meta">Qty <%# Eval("Quantity") %> × &#8377;<%# UserPanelCartHelper.FormatMoney(Eval("Amount")) %></span>
                                            </div>
                                            <strong>&#8377;<%# UserPanelCartHelper.FormatMoney(Eval("TotalAmount")) %></strong>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="upc-nav">
                                <asp:Button ID="btnBackPayment" runat="server" CssClass="upc-btn upc-btn-outline" Text="Back" OnClick="btnBackPayment_Click" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="upc-btn upc-btn-primary" Text="Submit Request" OnClick="btnSubmit_Click" />
                            </div>
                        </asp:Panel>
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
                    </aside>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnToReview" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function showUpcAddressModal() {
            var modal = document.getElementById("upcAddressModal");
            if (!modal) return;
            modal.classList.add("is-open");
            modal.setAttribute("aria-hidden", "false");
            document.body.classList.add("upc-addr-modal-open");
        }

        function hideUpcAddressModal() {
            var modal = document.getElementById("upcAddressModal");
            if (!modal) return;
            modal.classList.remove("is-open");
            modal.setAttribute("aria-hidden", "true");
            document.body.classList.remove("upc-addr-modal-open");
        }

        function syncUpcAddressModal() {
            var field = document.getElementById("<%= hfShowAddressModal.ClientID %>");
            if (field && field.value === "1") {
                showUpcAddressModal();
            } else {
                hideUpcAddressModal();
            }
        }

        function upcOpenNewAddress(e) {
            if (e) {
                e.preventDefault();
                e.stopPropagation();
            }
            var openBtn = document.getElementById("<%= btnOpenAddressModal.ClientID %>");
            if (openBtn) openBtn.click();
            return false;
        }

        function selectCheckoutPayMethod(method) {
            var tabs = document.querySelectorAll(".upc-pay-tab");
            var onlinePanel = document.getElementById("upcOnlineFields");
            var qrPanel = document.getElementById("upcQrFields");
            var methodField = document.getElementById("<%= hfPaymentMethod.ClientID %>");

            tabs.forEach(function (tab) {
                var active = tab.getAttribute("data-method") === method;
                tab.classList.toggle("is-active", active);
                tab.setAttribute("aria-selected", active ? "true" : "false");
            });

            if (onlinePanel) onlinePanel.hidden = method !== "online";
            if (qrPanel) qrPanel.hidden = method !== "qr";
            if (methodField) methodField.value = method || "online";
        }

        (function () {
            var previewUrl = null;

            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }

            function clearPaymentUpload(input, zone, selection, preview, filechip) {
                input.value = '';
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                    previewUrl = null;
                }
                preview.removeAttribute('src');
                filechip.textContent = '';
                selection.hidden = true;
                zone.classList.remove('is-hidden');
            }

            function showPaymentUpload(file, zone, selection, preview, filechip) {
                if (!file || !file.type.match(/^image\//i)) {
                    alert('Please choose a valid image file (JPG, PNG, WEBP).');
                    return false;
                }
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                }
                previewUrl = URL.createObjectURL(file);
                preview.src = previewUrl;
                filechip.textContent = file.name + ' — ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initSavingPaymentUpload() {
                var input = document.getElementById('<%= ImageUpload.ClientID %>');
                var zone = document.getElementById('savingPaymentUploadZone');
                var selection = document.getElementById('savingPaymentUploadSelection');
                var preview = document.getElementById('savingPaymentUploadPreview');
                var filechip = document.getElementById('savingPaymentUploadFilechip');
                var clearBtn = document.getElementById('savingPaymentUploadClear');
                if (!input || !zone || input.getAttribute('data-bound') === '1') {
                    return;
                }
                input.setAttribute('data-bound', '1');
                input.addEventListener('change', function () {
                    var file = input.files && input.files[0];
                    if (file) {
                        showPaymentUpload(file, zone, selection, preview, filechip);
                    } else {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    }
                });
                zone.addEventListener('click', function () { input.click(); });
                zone.addEventListener('dragover', function (e) {
                    e.preventDefault();
                    zone.classList.add('is-dragover');
                });
                zone.addEventListener('dragleave', function () {
                    zone.classList.remove('is-dragover');
                });
                zone.addEventListener('drop', function (e) {
                    e.preventDefault();
                    zone.classList.remove('is-dragover');
                    var file = e.dataTransfer && e.dataTransfer.files && e.dataTransfer.files[0];
                    if (!file) return;
                    try {
                        var dt = new DataTransfer();
                        dt.items.add(file);
                        input.files = dt.files;
                    } catch (ex) { }
                    showPaymentUpload(file, zone, selection, preview, filechip);
                });
                if (clearBtn) {
                    clearBtn.addEventListener('click', function () {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    });
                }
            }

            if (document.readyState === 'complete') {
                initSavingPaymentUpload();
                selectCheckoutPayMethod(document.getElementById('<%= hfPaymentMethod.ClientID %>').value || 'online');
                syncUpcAddressModal();
            } else {
                window.addEventListener('load', function () {
                    initSavingPaymentUpload();
                    selectCheckoutPayMethod(document.getElementById('<%= hfPaymentMethod.ClientID %>').value || 'online');
                    syncUpcAddressModal();
                });
            }
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    var input = document.getElementById('<%= ImageUpload.ClientID %>');
                    if (input) input.removeAttribute('data-bound');
                    initSavingPaymentUpload();
                    selectCheckoutPayMethod(document.getElementById('<%= hfPaymentMethod.ClientID %>').value || 'online');
                    syncUpcAddressModal();
                });
            }
        })();
    </script>
</asp:Content>

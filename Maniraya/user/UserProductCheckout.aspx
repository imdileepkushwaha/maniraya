<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="UserProductCheckout.aspx.cs" Inherits="user_UserProductCheckout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <link href="assets/css/user-product-cart.css?v=1" rel="stylesheet" />
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
                                    <asp:RadioButton ID="rbNewAddress" runat="server" GroupName="AddrMode" AutoPostBack="true" OnCheckedChanged="AddressMode_Changed" />
                                    <label for="<%= rbNewAddress.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-plus"></i></span>
                                        <span class="upc-choice-text">
                                            <strong>Add New Address</strong>
                                            <span>Enter a different delivery address</span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <asp:Panel ID="pnlProfileView" runat="server" CssClass="upc-review-block">
                                <p class="upc-review-label">Profile address</p>
                                <p class="upc-address-preview"><asp:Literal ID="litProfileAddress" runat="server" /></p>
                            </asp:Panel>

                            <asp:Panel ID="pnlNewAddress" runat="server" Visible="false">
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
                            </asp:Panel>

                            <div class="upc-nav">
                                <asp:Button ID="btnToPayment" runat="server" CssClass="upc-btn upc-btn-primary" Text="Continue to Payment" OnClick="btnToPayment_Click" CausesValidation="false" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlStepPayment" runat="server" Visible="false">
                            <h3 class="upc-section-title"><i class="fa fa-credit-card"></i> Payment Method</h3>
                            <div class="upc-pay-options">
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbUpi" runat="server" GroupName="PayMode" Checked="true" />
                                    <label for="<%= rbUpi.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-qrcode"></i></span>
                                        <span class="upc-choice-text"><strong>UPI</strong><span>Scan QR and pay</span></span>
                                    </label>
                                </div>
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbImps" runat="server" GroupName="PayMode" />
                                    <label for="<%= rbImps.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-bolt"></i></span>
                                        <span class="upc-choice-text"><strong>IMPS</strong><span>Instant bank transfer</span></span>
                                    </label>
                                </div>
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbNeft" runat="server" GroupName="PayMode" />
                                    <label for="<%= rbNeft.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-university"></i></span>
                                        <span class="upc-choice-text"><strong>NEFT</strong><span>Bank NEFT transfer</span></span>
                                    </label>
                                </div>
                                <div class="upc-choice">
                                    <asp:RadioButton ID="rbRtgs" runat="server" GroupName="PayMode" />
                                    <label for="<%= rbRtgs.ClientID %>">
                                        <span class="upc-choice-icon"><i class="fa fa-exchange-alt"></i></span>
                                        <span class="upc-choice-text"><strong>RTGS</strong><span>Same-day bank transfer</span></span>
                                    </label>
                                </div>
                            </div>

                            <div class="row saving-online-payment-row">
                                <div class="col-md-5">
                                    <p class="upc-section-title"><i class="fa fa-qrcode"></i> Scan &amp; Pay</p>
                                    <asp:Panel ID="pnlNoCompanyAccount" runat="server" Visible="false">
                                        <p class="upc-ship-note">Company payment account is not available right now. Please contact support.</p>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlCompanyAccount" runat="server">
                                        <asp:Panel ID="pnlBankSelectWrap" runat="server" Visible="false" CssClass="form-group">
                                            <label for="<%= ddbankaccount.ClientID %>"><i class="fa fa-university"></i> Select Account</label>
                                            <asp:DropDownList ID="ddbankaccount" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddbankaccount_SelectedIndexChanged" />
                                        </asp:Panel>
                                        <div class="topup-qr-card">
                                            <p class="topup-qr-label"><i class="fa fa-qrcode"></i> Scan to Pay</p>
                                            <asp:Image ID="imgPaymentQr" runat="server" CssClass="topup-qr-image" AlternateText="Payment QR code" />
                                        </div>
                                        <ul class="saving-bank-detail-list">
                                            <li><span>Account Holder</span><strong><asp:Literal ID="litAccountHolder" runat="server" /></strong></li>
                                            <li><span>Account No</span><strong><asp:Literal ID="litAccountNo" runat="server" /></strong></li>
                                            <li><span>Bank</span><strong><asp:Literal ID="litBankName" runat="server" /></strong></li>
                                            <li><span>IFSC</span><strong><asp:Literal ID="litIfscCode" runat="server" /></strong></li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-7">
                                    <p class="upc-section-title"><i class="fa fa-check-square"></i> Payment Proof</p>
                                    <div class="form-group">
                                        <label for="<%= txttransactionid.ClientID %>"><i class="fa fa-exchange-alt"></i> UTR No / Transaction ID</label>
                                        <asp:TextBox ID="txttransactionid" runat="server" CssClass="form-control" placeholder="Enter UTR or transaction reference" />
                                    </div>
                                    <div class="form-group profile-upload-field topup-payment-upload">
                                        <label><i class="fa fa-camera"></i> Payment Screenshot</label>
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
            } else {
                window.addEventListener('load', initSavingPaymentUpload);
            }
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    var input = document.getElementById('<%= ImageUpload.ClientID %>');
                    if (input) input.removeAttribute('data-bound');
                    initSavingPaymentUpload();
                });
            }
        })();
    </script>
</asp:Content>

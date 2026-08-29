<%@ Page Title="Virtual Franchise Plan" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="VirtualFranchisePlanPurchase.aspx.cs" Inherits="user_VirtualFranchisePlanPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <style>
        .saving-purchase-page .saving-product-showcase {
            margin-bottom: 24px;
            border-radius: 16px;
            overflow: hidden;
            border: 1px solid rgba(229, 169, 6, 0.24);
            background: linear-gradient(135deg, #0f1729 0%, #1a2540 52%, #243352 100%);
            box-shadow: 0 14px 34px rgba(15, 23, 42, 0.16);
            color: #fff;
        }
        .saving-product-showcase-inner { padding: 22px 24px; }
        .saving-product-showcase-eyebrow {
            margin: 0 0 6px; font-size: 11px; font-weight: 700;
            letter-spacing: 0.08em; text-transform: uppercase; color: rgba(246, 207, 99, 0.92);
        }
        .saving-product-showcase-title { margin: 0 0 10px; font-size: 1.55rem; font-weight: 700; color: #fff; }
        .saving-product-showcase-desc { margin: 0; font-size: 0.9rem; line-height: 1.55; color: rgba(255, 255, 255, 0.72); }
        .saving-product-price-grid {
            display: grid; grid-template-columns: repeat(4, minmax(0, 1fr)); gap: 12px; margin-top: 18px;
        }
        .saving-product-price-item {
            padding: 12px 14px; border-radius: 12px;
            background: rgba(255, 255, 255, 0.06); border: 1px solid rgba(255, 255, 255, 0.1);
        }
        .saving-product-price-item span {
            display: block; margin-bottom: 4px; font-size: 11px; font-weight: 600;
            letter-spacing: 0.05em; text-transform: uppercase; color: rgba(255, 255, 255, 0.55);
        }
        .saving-product-price-item strong { display: block; font-size: 1.12rem; font-weight: 700; color: #fff; }
        .saving-product-price-item.is-highlight {
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.2) 0%, rgba(229, 169, 6, 0.08) 100%);
            border-color: rgba(246, 207, 99, 0.42);
        }
        .saving-product-price-item.is-highlight span { color: rgba(246, 207, 99, 0.9); }
        .saving-product-price-item.is-highlight strong { color: #f6cf63; font-size: 1.28rem; }
        .saving-purchase-summary { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); gap: 14px; }
        .saving-purchase-info-note {
            display: flex; align-items: flex-start; gap: 10px; margin-top: 8px; padding: 12px 14px;
            border-radius: 10px; background: #fffbeb; border: 1px solid rgba(229, 169, 6, 0.35);
            color: #78350f; font-size: 13px; line-height: 1.5;
        }
        .saving-purchase-info-note i { margin-top: 2px; flex-shrink: 0; color: #b45309; }
        .saving-payment-type-options { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); gap: 12px; margin-bottom: 16px; }
        .saving-payment-type-item { position: relative; }
        .saving-payment-type-item input[type="radio"] { position: absolute; opacity: 0; pointer-events: none; }
        .saving-payment-type-item label {
            display: flex !important; align-items: center; gap: 12px; margin: 0; padding: 14px 16px;
            border-radius: 12px; border: 1px solid rgba(148, 163, 184, 0.35); background: #fff; cursor: pointer;
        }
        .saving-payment-type-item input[type="radio"]:checked + label {
            border-color: rgba(229, 169, 6, 0.65);
            background: linear-gradient(135deg, rgba(246, 207, 99, 0.16) 0%, rgba(255, 251, 235, 0.9) 100%);
        }
        .saving-shipping-type-icon {
            width: 42px; height: 42px; border-radius: 10px; display: inline-flex; align-items: center;
            justify-content: center; font-size: 18px; color: #92400e; background: rgba(246, 207, 99, 0.22); flex-shrink: 0;
        }
        .saving-shipping-type-text strong { display: block; font-size: 0.95rem; color: #0f172a; margin-bottom: 2px; }
        .saving-shipping-type-text span { display: block; font-size: 0.82rem; color: #64748b; }
        .saving-bank-detail-list { margin: 12px 0 0; padding: 0; list-style: none; }
        .saving-bank-detail-list li {
            display: flex; justify-content: space-between; gap: 10px; padding: 7px 0;
            border-bottom: 1px dashed rgba(148, 163, 184, 0.35); font-size: 0.88rem;
        }
        .saving-bank-detail-list span { color: #64748b; }
        .saving-bank-detail-list strong { color: #0f172a; text-align: right; }
        .saving-shipping-card { padding: 16px 18px; border-radius: 12px; margin-bottom: 15px; border: 1px solid rgba(148, 163, 184, 0.28); }
        .saving-shipping-card.is-empty { border-style: dashed; background: rgba(248, 250, 252, 0.9); }
        .saving-shipping-empty-text { margin: 0; font-size: 0.9rem; color: #64748b; }
        @media (max-width: 767px) {
            .saving-product-price-grid, .saving-purchase-summary, .saving-payment-type-options { grid-template-columns: 1fr; }
        }
    </style>
    <script type="text/javascript">
        var __vfUtrUsed = false;
        var __vfUtrLastChecked = '';
        var __vfUtrCheckToken = 0;

        function validate() {
            var plan = document.getElementById("<%= ddPlan.ClientID %>");
            if (!plan || plan.value === "0") {
                alert('Select Plan');
                if (plan) plan.focus();
                return false;
            }
            var onlinePayment = document.getElementById("<%= rbOnlinePayment.ClientID %>");
            if (onlinePayment && onlinePayment.checked) {
                var txn = document.getElementById("<%= txttransactionid.ClientID %>");
                if (!txn || txn.value.trim() === "") {
                    alert('Enter Transaction ID / UTR number');
                    if (txn) txn.focus();
                    return false;
                }
                if (__vfUtrUsed && __vfUtrLastChecked === txn.value.trim()) {
                    alert('This Transaction Id already used');
                    txn.focus();
                    return false;
                }
            }
            return true;
        }

        function setVfUtrMessage(isUsed) {
            var msg = document.getElementById('vfUtrCheckMsg');
            if (!msg) return;
            msg.style.display = isUsed ? 'block' : 'none';
            msg.innerHTML = isUsed ? 'This Transaction Id already used' : '';
        }

        function resetVfUtrCheckState() {
            __vfUtrUsed = false;
            __vfUtrLastChecked = '';
            setVfUtrMessage(false);
        }

        function checkVfUtrUsed() {
            var txn = document.getElementById("<%= txttransactionid.ClientID %>");
            if (!txn) return;
            var utr = (txn.value || '').trim();
            var onlinePayment = document.getElementById("<%= rbOnlinePayment.ClientID %>");
            if (onlinePayment && !onlinePayment.checked) { resetVfUtrCheckState(); return; }
            if (!utr) { resetVfUtrCheckState(); return; }
            if (__vfUtrLastChecked === utr) { setVfUtrMessage(__vfUtrUsed); return; }
            var token = ++__vfUtrCheckToken;
            $.ajax({
                type: 'POST',
                url: 'VirtualFranchisePlanPurchase.aspx/CheckOnlineTransactionId',
                data: JSON.stringify({ onlineTransactionId: utr }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    if (token !== __vfUtrCheckToken) return;
                    var current = (txn.value || '').trim();
                    if (current !== utr) return;
                    __vfUtrLastChecked = utr;
                    __vfUtrUsed = !!(res && res.d === true);
                    setVfUtrMessage(__vfUtrUsed);
                }
            });
        }

        (function () {
            function bindVfUtrCheck() {
                var txn = document.getElementById("<%= txttransactionid.ClientID %>");
                if (!txn || txn.getAttribute('data-utr-bound') === '1') return;
                txn.setAttribute('data-utr-bound', '1');
                txn.addEventListener('input', function () { resetVfUtrCheckState(); });
                txn.addEventListener('blur', function () { checkVfUtrUsed(); });
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', bindVfUtrCheck);
            } else {
                bindVfUtrCheck();
            }
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindVfUtrCheck);
            }
        })();
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Virtual Franchise Plan</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Virtual Franchise</a></li>
            <li class="active">Purchase Plan</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="Loading" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page saving-purchase-page topup-request-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-gem"></i></div>
                    <div class="profile-hero-info">
                        <h2>Virtual Franchise Plan</h2>
                        <p class="profile-hero-meta">Select a plan, complete payment, and submit the request for admin approval. 40-month ROI starts after approval.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="VirtualFranchisePlanReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list"></i> My Requests</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-4">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-credit-card"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Plan Request</h3>
                                <p class="box-subtitle">Choose a plan and share your payment details</p>
                            </div>
                        </div>
                    </div>

                    <div class="box-body profile-form-grid">
                        <div class="saving-product-showcase">
                            <div class="saving-product-showcase-inner">
                                <p class="saving-product-showcase-eyebrow">Selected Plan</p>
                                <h3 class="saving-product-showcase-title"><asp:Literal ID="litPlanName" runat="server" Text="Select a plan" /></h3>
                                <p class="saving-product-showcase-desc">Monthly ROI is credited for 40 months after admin approval. Level 1 income is paid instantly to your sponsor.</p>
                                <div class="saving-product-price-grid">
                                    <div class="saving-product-price-item">
                                        <span>Plan Amount</span>
                                        <strong>Rs. <asp:Literal ID="litAmount" runat="server" Text="0.00" /></strong>
                                    </div>
                                    <div class="saving-product-price-item">
                                        <span>ROI / Month</span>
                                        <strong><asp:Literal ID="litRoi" runat="server" Text="0" />%</strong>
                                    </div>
                                    <div class="saving-product-price-item">
                                        <span>Monthly Cashback</span>
                                        <strong>Rs. <asp:Literal ID="litMonthly" runat="server" Text="0.00" /></strong>
                                    </div>
                                    <div class="saving-product-price-item is-highlight">
                                        <span>Total Cashback (40 x Monthly)</span>
                                        <strong>Rs. <asp:Literal ID="litTotal" runat="server" Text="0.00" /></strong>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-user"></i> Your Details</p>
                        <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= txtuserid.ClientID %>"><i class="fa fa-id-badge"></i> User ID</label>
                                <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control" Enabled="false" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtusername.ClientID %>"><i class="fa fa-user"></i> User Name</label>
                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-layer-group"></i> Plan Summary</p>
                        <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= ddPlan.ClientID %>"><i class="fa fa-gem"></i> Select Plan</label>
                                <asp:DropDownList ID="ddPlan" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddPlan_SelectedIndexChanged" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtamount.ClientID %>"><i class="fa fa-rupee-sign"></i> Plan Amount</label>
                                <asp:TextBox ID="txtamount" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= txtmonthlyroi.ClientID %>"><i class="fa fa-calendar"></i> Monthly ROI</label>
                                <asp:TextBox ID="txtmonthlyroi" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txttotalcashback.ClientID %>"><i class="fa fa-coins"></i> Total Cashback (40 months)</label>
                                <asp:TextBox ID="txttotalcashback" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-credit-card"></i> Payment Method</p>
                        <div class="saving-payment-type-options">
                            <div class="saving-payment-type-item">
                                <asp:RadioButton ID="rbCashPayment" runat="server" GroupName="PaymentMethod" />
                                <label for="<%= rbCashPayment.ClientID %>">
                                    <span class="saving-shipping-type-icon"><i class="fa fa-money-bill-alt"></i></span>
                                    <span class="saving-shipping-type-text">
                                        <strong>Cash</strong>
                                        <span>Submit request directly to admin</span>
                                    </span>
                                </label>
                            </div>
                            <div class="saving-payment-type-item">
                                <asp:RadioButton ID="rbOnlinePayment" runat="server" GroupName="PaymentMethod" Checked="true" />
                                <label for="<%= rbOnlinePayment.ClientID %>">
                                    <span class="saving-shipping-type-icon"><i class="fa fa-qrcode"></i></span>
                                    <span class="saving-shipping-type-text">
                                        <strong>Online</strong>
                                        <span>Pay via QR and upload payment proof</span>
                                    </span>
                                </label>
                            </div>
                        </div>

                        <asp:Panel ID="pnlCashPaymentInfo" runat="server" CssClass="saving-purchase-info-note" Style="display:none;">
                            <i class="fa fa-info-circle" aria-hidden="true"></i>
                            <span>Cash payment selected. Click <strong>Submit Request</strong> and your plan request will be sent to admin for approval.</span>
                        </asp:Panel>

                        <asp:Panel ID="pnlOnlinePaymentSection" runat="server">
                            <div class="row saving-online-payment-row">
                                <div class="col-md-5">
                                    <p class="profile-subsection-title"><i class="fa fa-qrcode"></i> Scan &amp; Pay</p>
                                    <asp:Panel ID="pnlNoCompanyAccount" runat="server" Visible="false" CssClass="saving-shipping-card is-empty">
                                        <p class="saving-shipping-empty-text">Company payment account is not available right now. Please contact support.</p>
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
                                    <p class="profile-subsection-title"><i class="fa fa-check-square"></i> Payment Proof</p>
                                    <div class="form-group">
                                        <label for="<%= txttransactionid.ClientID %>"><i class="fa fa-exchange-alt"></i> UTR No / Transaction ID</label>
                                        <asp:TextBox ID="txttransactionid" runat="server" CssClass="form-control" placeholder="Enter UTR or transaction reference" />
                                        <span id="vfUtrCheckMsg" style="display:none;color:#c0392b;font-size:12px;margin-top:6px;font-weight:600;"></span>
                                    </div>
                                    <div class="form-group profile-upload-field topup-payment-upload">
                                        <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                        <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="vfPaymentUploadZone">
                                            <div class="profile-upload-zone-inner">
                                                <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                <p class="profile-upload-title">Drop payment screenshot here</p>
                                                <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                                <p class="profile-upload-meta">JPG, PNG, WEBP - receipt clearly visible</p>
                                            </div>
                                            <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                        </div>
                                        <div class="profile-upload-selection profile-upload-selection-attach" id="vfPaymentUploadSelection" hidden>
                                            <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                <img id="vfPaymentUploadPreview" src="" alt="Payment screenshot preview" />
                                            </div>
                                            <div class="profile-upload-selection-info">
                                                <span class="profile-upload-filechip" id="vfPaymentUploadFilechip"></span>
                                                <button type="button" class="profile-upload-clear" id="vfPaymentUploadClear">
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
                        </asp:Panel>
                    </div>

                    <div class="box-footer profile-password-actions">
                        <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary profile-btn-primary-action" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-default profile-btn-secondary-action" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
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
                if (previewUrl) { URL.revokeObjectURL(previewUrl); previewUrl = null; }
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
                if (previewUrl) URL.revokeObjectURL(previewUrl);
                previewUrl = URL.createObjectURL(file);
                preview.src = previewUrl;
                filechip.textContent = file.name + ' - ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initVfPaymentUpload() {
                var input = document.getElementById('<%= ImageUpload.ClientID %>');
                var zone = document.getElementById('vfPaymentUploadZone');
                var selection = document.getElementById('vfPaymentUploadSelection');
                var preview = document.getElementById('vfPaymentUploadPreview');
                var filechip = document.getElementById('vfPaymentUploadFilechip');
                var clearBtn = document.getElementById('vfPaymentUploadClear');
                if (!input || !zone || input.getAttribute('data-bound') === '1') return;
                input.setAttribute('data-bound', '1');
                input.addEventListener('change', function () {
                    var file = input.files && input.files[0];
                    if (file) showPaymentUpload(file, zone, selection, preview, filechip);
                    else clearPaymentUpload(input, zone, selection, preview, filechip);
                });
                if (clearBtn) {
                    clearBtn.addEventListener('click', function () {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    });
                }
                ['dragenter', 'dragover'].forEach(function (name) {
                    zone.addEventListener(name, function (e) { e.preventDefault(); zone.classList.add('is-dragover'); });
                });
                ['dragleave', 'drop'].forEach(function (name) {
                    zone.addEventListener(name, function (e) { e.preventDefault(); zone.classList.remove('is-dragover'); });
                });
                zone.addEventListener('drop', function (e) {
                    var file = e.dataTransfer && e.dataTransfer.files ? e.dataTransfer.files[0] : null;
                    if (!file) return;
                    try {
                        var dt = new DataTransfer();
                        dt.items.add(file);
                        input.files = dt.files;
                    } catch (ex) { return; }
                    showPaymentUpload(file, zone, selection, preview, filechip);
                });
            }

            function togglePaymentSections() {
                var onlineSection = document.getElementById('<%= pnlOnlinePaymentSection.ClientID %>');
                var cashInfo = document.getElementById('<%= pnlCashPaymentInfo.ClientID %>');
                var onlinePayment = document.getElementById('<%= rbOnlinePayment.ClientID %>');
                if (!onlinePayment) return;
                var isOnline = onlinePayment.checked;
                if (onlineSection) onlineSection.style.display = isOnline ? '' : 'none';
                if (cashInfo) cashInfo.style.display = isOnline ? 'none' : '';
            }

            function initPaymentMethodToggle() {
                var cashPayment = document.getElementById('<%= rbCashPayment.ClientID %>');
                var onlinePayment = document.getElementById('<%= rbOnlinePayment.ClientID %>');
                if (!cashPayment || !onlinePayment) return;
                if (cashPayment.getAttribute('data-bound') === '1') { togglePaymentSections(); return; }
                cashPayment.setAttribute('data-bound', '1');
                onlinePayment.setAttribute('data-bound', '1');
                cashPayment.addEventListener('change', togglePaymentSections);
                onlinePayment.addEventListener('change', togglePaymentSections);
                togglePaymentSections();
            }

            if (window.Sys && Sys.Application) {
                Sys.Application.add_load(function () { initVfPaymentUpload(); initPaymentMethodToggle(); });
            } else {
                document.addEventListener('DOMContentLoaded', function () { initVfPaymentUpload(); initPaymentMethodToggle(); });
            }
        })();
    </script>
</asp:Content>

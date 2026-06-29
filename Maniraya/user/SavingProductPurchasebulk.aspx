<%@ Page Title="Purchase Saving Product" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="SavingProductPurchasebulk.aspx.cs" Inherits="user_SavingProductPurchasebulk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
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

        .saving-product-showcase-inner {
            display: grid;
            grid-template-columns: minmax(190px, 250px) minmax(0, 1fr);
            gap: 24px;
            padding: 22px 24px;
            align-items: center;
        }

        .saving-product-showcase-visual {
            position: relative;
        }

        .saving-product-showcase-frame {
            width: 100%;
            aspect-ratio: 1 / 1;
            border-radius: 14px;
            overflow: hidden;
            background: rgba(255, 255, 255, 0.96);
            border: 3px solid rgba(246, 207, 99, 0.45);
            box-shadow: 0 10px 28px rgba(0, 0, 0, 0.28);
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .saving-product-showcase-img,
        .saving-product-showcase-frame img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            display: block;
        }

        .saving-product-showcase-badge {
            position: absolute;
            top: 12px;
            left: 12px;
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 5px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.03em;
            text-transform: uppercase;
            color: #1a1d21;
            background: linear-gradient(135deg, #f6cf63 0%, #e5a906 100%);
            box-shadow: 0 4px 14px rgba(229, 169, 6, 0.35);
        }

        .saving-product-showcase-eyebrow {
            margin: 0 0 6px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: rgba(246, 207, 99, 0.92);
        }

        .saving-product-showcase-title {
            margin: 0 0 10px;
            font-size: 1.55rem;
            font-weight: 700;
            line-height: 1.25;
            color: #fff;
        }

        .saving-product-showcase-desc {
            margin: 0;
            font-size: 0.9rem;
            line-height: 1.55;
            color: rgba(255, 255, 255, 0.72);
            max-width: 56ch;
        }

        .saving-product-price-grid {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr));
            gap: 12px;
            margin-top: 18px;
        }

        .saving-product-price-item {
            padding: 12px 14px;
            border-radius: 12px;
            background: rgba(255, 255, 255, 0.06);
            border: 1px solid rgba(255, 255, 255, 0.1);
        }

        .saving-product-price-item span {
            display: block;
            margin-bottom: 4px;
            font-size: 11px;
            font-weight: 600;
            letter-spacing: 0.05em;
            text-transform: uppercase;
            color: rgba(255, 255, 255, 0.55);
        }

        .saving-product-price-item strong {
            display: block;
            font-size: 1.12rem;
            font-weight: 700;
            color: #fff;
            line-height: 1.2;
        }

        .saving-product-price-item.is-highlight {
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.2) 0%, rgba(229, 169, 6, 0.08) 100%);
            border-color: rgba(246, 207, 99, 0.42);
        }

        .saving-product-price-item.is-highlight span {
            color: rgba(246, 207, 99, 0.9);
        }

        .saving-product-price-item.is-highlight strong {
            color: #f6cf63;
            font-size: 1.28rem;
        }

        .saving-purchase-summary {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 14px;
        }

        .saving-purchase-summary .form-group label {
            display: flex;
            align-items: center;
            gap: 6px;
        }

        .saving-purchase-info-note {
            display: flex;
            align-items: flex-start;
            gap: 10px;
            margin-top: 8px;
            padding: 12px 14px;
            border-radius: 10px;
            background: rgba(59, 130, 246, 0.12);
            border: 1px solid rgba(59, 130, 246, 0.25);
            color: #bfdbfe;
            font-size: 13px;
            line-height: 1.5;
        }

        .saving-purchase-info-note i {
            margin-top: 2px;
            color: #93c5fd;
        }

        @media (max-width: 991px) {
            .saving-product-showcase-inner {
                grid-template-columns: minmax(160px, 200px) minmax(0, 1fr);
                gap: 18px;
                padding: 18px;
            }

            .saving-product-showcase-title {
                font-size: 1.3rem;
            }
        }

        @media (max-width: 767px) {
            .saving-product-showcase-inner {
                grid-template-columns: 1fr;
                text-align: center;
            }

            .saving-product-showcase-visual {
                max-width: 240px;
                margin: 0 auto;
            }

            .saving-product-showcase-desc {
                margin-left: auto;
                margin-right: auto;
            }

            .saving-product-price-grid {
                grid-template-columns: 1fr;
            }

            .saving-purchase-summary {
                grid-template-columns: 1fr;
            }
        }

        .saving-shipping-card {
            padding: 16px 18px;
            border-radius: 12px;
            margin-bottom: 15px;
            border: 1px solid rgba(148, 163, 184, 0.28);
            background: linear-gradient(135deg, rgba(15, 23, 42, 0.04) 0%, rgba(30, 41, 59, 0.06) 100%);
        }

        .saving-shipping-card.is-empty {
            border-style: dashed;
            background: rgba(248, 250, 252, 0.9);
        }

        .saving-shipping-card-head {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            margin-bottom: 10px;
        }

        .saving-shipping-card-title {
            margin: 0;
            font-size: 0.95rem;
            font-weight: 700;
            color: #0f172a;
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .saving-shipping-source {
            display: inline-flex;
            align-items: center;
            padding: 3px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.03em;
            text-transform: uppercase;
            color: #0369a1;
            background: rgba(14, 165, 233, 0.14);
            border: 1px solid rgba(14, 165, 233, 0.28);
        }

        .saving-shipping-address-text {
            margin: 0;
            font-size: 0.92rem;
            line-height: 1.65;
            color: #334155;
            white-space: pre-line;
        }

        .saving-shipping-empty-text {
            margin: 0 0 12px;
            font-size: 0.9rem;
            line-height: 1.55;
            color: #64748b;
        }

        .saving-shipping-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-top: 14px;
        }

        .saving-shipping-edit-panel {
            padding: 16px 18px;
            border-radius: 12px;
            border: 1px solid rgba(229, 169, 6, 0.28);
            background: rgba(255, 251, 235, 0.55);
        }
    </style>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txttransactionid.ClientID%>").value.trim() === "") {
                alert('Enter Transaction ID / UTR number');
                document.getElementById("<%=txttransactionid.ClientID%>").focus();
                return false;
            }
               if (document.getElementById("<%=txtquantity.ClientID%>").value.trim() === "") {
                alert('Enter Quantity');
                document.getElementById("<%=txtquantity.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Purchase Saving Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving</a></li>
            <li class="active">Purchase Saving Product</li>
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
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-shopping-bag"></i></div>
                    <div class="profile-hero-info">
                        <h2>Purchase Saving Product</h2>
                        <p class="profile-hero-meta">Complete your payment and submit the transaction details with payment proof for verification.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-4">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-credit-card"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Purchase Request</h3>
                                <p class="box-subtitle">Review the product and share your payment details</p>
                            </div>
                        </div>
                    </div>

                    <div class="box-body profile-form-grid">
                        <div class="saving-product-showcase">
                            <div class="saving-product-showcase-inner">
                                <div class="saving-product-showcase-visual">
                                    <span class="saving-product-showcase-badge"><i class="fa fa-tag"></i> Monthly Saving</span>
                                    <div class="saving-product-showcase-frame">
                                        <asp:Image ID="imgProduct" runat="server" CssClass="saving-product-showcase-img" AlternateText="Saving product" />
                                    </div>
                                </div>
                                <div class="saving-product-showcase-content">
                                    <p class="saving-product-showcase-eyebrow">Selected Product</p>
                                    <h3 class="saving-product-showcase-title"><asp:Literal ID="litProductName" runat="server" /></h3>
                                    <p class="saving-product-showcase-desc">Complete payment for this saving product and submit your UTR with payment screenshot for admin verification.</p>
                                    <div class="saving-product-price-grid">
                                        <div class="saving-product-price-item">
                                            <span>MRP</span>
                                            <strong>₹ <asp:Literal ID="litMrp" runat="server" /></strong>
                                        </div>
                                        <div class="saving-product-price-item">
                                            <span>DP</span>
                                            <strong>₹ <asp:Literal ID="litDp" runat="server" /></strong>
                                        </div>
                                        <div class="saving-product-price-item is-highlight">
                                            <span>Pay Amount</span>
                                            <strong><asp:Literal ID="litAmount" runat="server" /></strong>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-user"></i> Your Details</p>
                        <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= txtuserid.ClientID %>"><i class="fa fa-id-badge"></i> User ID</label>
                                <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtusername.ClientID %>"><i class="fa fa-user"></i> User Name</label>
                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-cube"></i> Product Summary</p>
                        <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= txtproductname.ClientID %>"><i class="fa fa-cube"></i> Product</label>
                                <asp:TextBox ID="txtproductname" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtamount.ClientID %>"><i class="fa fa-inr"></i> Amount</label>
                                <asp:TextBox ID="txtamount" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                            </div>
                        </div>
                           <div class="saving-purchase-summary">
                            <div class="form-group">
                                <label for="<%= txtquantity.ClientID %>"><i class="fa fa-cube"></i> Quantity</label>
                                <asp:TextBox ID="txtquantity" AutoPostBack="true" Text="1" OnTextChanged="txtquantity_TextChanged" CssClass="form-control" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txttotalamount.ClientID %>"><i class="fa fa-inr"></i> Total Amount</label>
                                <asp:TextBox ID="txttotalamount" Enabled="false" Text="1000" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-map-marker"></i> Shipping Address</p>
                        <asp:HiddenField ID="hfShippingMode" runat="server" Value="view" />

                        <asp:Panel ID="pnlShippingView" runat="server" CssClass="saving-shipping-card">
                            <div class="saving-shipping-card-head">
                                <h4 class="saving-shipping-card-title"><i class="fa fa-truck"></i> Delivery Address</h4>
                                <asp:Label ID="lblShippingSource" runat="server" CssClass="saving-shipping-source" Visible="false" />
                            </div>
                            <asp:Literal ID="litShippingAddress" runat="server" Mode="PassThrough" />
                            <div class="saving-shipping-actions">
                                <asp:Button ID="btnChangeShipping" runat="server" CssClass="btn btn-default profile-btn-secondary-action"
                                    Text="Change Address" CausesValidation="false" OnClick="btnChangeShipping_Click" />
                                <asp:Button ID="btnUseProfileShipping" runat="server" CssClass="btn btn-primary profile-btn-primary-action"
                                    Text="Use Profile Address" CausesValidation="false" OnClick="btnUseProfileShipping_Click" Visible="false" />
                                <a href="AddressProof.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-id-card"></i> Update Profile Address</a>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlShippingEmpty" runat="server" CssClass="saving-shipping-card is-empty" Visible="false">
                            <p class="saving-shipping-empty-text">
                                <i class="fa fa-info-circle"></i>
                                No shipping address found. Please add your delivery address before submitting the purchase request.
                            </p>
                            <div class="saving-shipping-actions">
                                <asp:Button ID="btnAddShipping" runat="server" CssClass="btn btn-primary profile-btn-primary-action"
                                    Text="Add Shipping Address" CausesValidation="false" OnClick="btnAddShipping_Click" />
                                <asp:Button ID="btnUseProfileFromEmpty" runat="server" CssClass="btn btn-default profile-btn-secondary-action"
                                    Text="Use Profile Address" CausesValidation="false" OnClick="btnUseProfileShipping_Click" Visible="false" />
                                <a href="AddressProof.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-id-card"></i> Add Profile Address</a>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlShippingEdit" runat="server" CssClass="saving-shipping-edit-panel" Visible="false">
                            <div class="form-group">
                                <label for="<%= txtShipAddress.ClientID %>"><i class="fa fa-home"></i> Address</label>
                                <asp:TextBox ID="txtShipAddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                    placeholder="House no., street, landmark" />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= ddShipState.ClientID %>"><i class="fa fa-map"></i> State</label>
                                        <asp:DropDownList ID="ddShipState" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddShipState_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= ddShipCity.ClientID %>"><i class="fa fa-building"></i> City</label>
                                        <asp:DropDownList ID="ddShipCity" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtShipArea.ClientID %>"><i class="fa fa-location-arrow"></i> Area / Locality</label>
                                        <asp:TextBox ID="txtShipArea" runat="server" CssClass="form-control" placeholder="Area or locality" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtShipPincode.ClientID %>"><i class="fa fa-thumb-tack"></i> Pincode</label>
                                        <asp:TextBox ID="txtShipPincode" runat="server" CssClass="form-control" placeholder="Pincode"
                                            onkeypress="return isNumberKey(event);" MaxLength="6" />
                                    </div>
                                </div>
                            </div>
                            <div class="saving-shipping-actions">
                                <asp:Button ID="btnSaveShipping" runat="server" CssClass="btn btn-primary profile-btn-primary-action"
                                    Text="Save Shipping Address" CausesValidation="false" OnClick="btnSaveShipping_Click" />
                                <asp:Button ID="btnCancelShipping" runat="server" CssClass="btn btn-default profile-btn-secondary-action"
                                    Text="Cancel" CausesValidation="false" OnClick="btnCancelShipping_Click" />
                            </div>
                        </asp:Panel>

                        <p class="profile-subsection-title"><i class="fa fa-check-square-o"></i> Payment Proof</p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txttransactionid.ClientID %>"><i class="fa fa-exchange"></i> UTR No / Transaction ID</label>
                                    <asp:TextBox ID="txttransactionid" runat="server" CssClass="form-control" placeholder="Enter UTR or transaction reference" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group profile-upload-field topup-payment-upload">
                                    <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                    <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="savingPaymentUploadZone">
                                        <div class="profile-upload-zone-inner">
                                            <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload"></i></span>
                                            <p class="profile-upload-title">Drop payment screenshot here</p>
                                            <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                            <p class="profile-upload-meta">JPG, PNG, WEBP · receipt clearly visible</p>
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
                filechip.textContent = file.name + ' · ' + formatFileSize(file.size);
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

                if (clearBtn) {
                    clearBtn.addEventListener('click', function () {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    });
                }

                ['dragenter', 'dragover'].forEach(function (name) {
                    zone.addEventListener(name, function (e) {
                        e.preventDefault();
                        zone.classList.add('is-dragover');
                    });
                });

                ['dragleave', 'drop'].forEach(function (name) {
                    zone.addEventListener(name, function (e) {
                        e.preventDefault();
                        zone.classList.remove('is-dragover');
                    });
                });

                zone.addEventListener('drop', function (e) {
                    var file = e.dataTransfer && e.dataTransfer.files ? e.dataTransfer.files[0] : null;
                    if (!file) {
                        return;
                    }
                    try {
                        var dt = new DataTransfer();
                        dt.items.add(file);
                        input.files = dt.files;
                    } catch (ex) {
                        return;
                    }
                    showPaymentUpload(file, zone, selection, preview, filechip);
                });
            }

            function scheduleInit() {
                initSavingPaymentUpload();
            }

            if (window.Sys && Sys.Application) {
                Sys.Application.add_load(scheduleInit);
            } else {
                document.addEventListener('DOMContentLoaded', scheduleInit);
            }
        })();
    </script>
</asp:Content>

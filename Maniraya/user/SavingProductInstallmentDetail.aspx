<%@ Page Title="My Savings Installment" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingProductInstallmentDetail.aspx.cs" Inherits="user_SavingProductInstallmentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=28" rel="stylesheet" />
    <style>
        .installment-pay-modal {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .installment-pay-modal .modal-dialog {
            width: 92%;
            max-width: 900px;
            margin: 1.75rem auto;
        }

        .installment-pay-modal.show .modal-dialog,
        .installment-pay-modal .modal-dialog.modal-dialog-centered {
            display: flex;
            align-items: center;
            min-height: calc(100% - 3.5rem);
        }

        .installment-pay-modal .modal-content {
            width: 100%;
            border: none;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 24px 60px rgba(15, 23, 42, 0.22);
        }

        .installment-pay-modal .modal-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            padding: 18px 22px;
            border-bottom: 1px solid #e8ecf1;
            background: linear-gradient(135deg, #0f1729 0%, #1a2540 100%);
            color: #fff;
        }

        .installment-pay-modal .modal-title {
            margin: 0;
            font-size: 1.15rem;
            font-weight: 700;
            display: flex;
            align-items: center;
            gap: 10px;
            color: #fff;
        }

        .installment-pay-modal .modal-title i {
            color: #f6cf63;
        }

        .installment-pay-modal .modal-header .close {
            color: #fff;
            opacity: 0.85;
            text-shadow: none;
            font-size: 1.6rem;
        }

        .installment-pay-modal .modal-body {
            padding: 22px;
            background: #f8fafc;
        }

        .installment-pay-modal .modal-footer {
            padding: 16px 22px;
            border-top: 1px solid #e8ecf1;
            background: #fff;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }

        .installment-pay-summary {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 12px;
            margin-bottom: 18px;
        }

        .installment-pay-summary-item {
            padding: 14px 16px;
            border-radius: 12px;
            background: #fff;
            border: 1px solid #e8ecf1;
        }

        .installment-pay-summary-item span {
            display: block;
            margin-bottom: 4px;
            font-size: 0.75rem;
            font-weight: 700;
            letter-spacing: 0.05em;
            text-transform: uppercase;
            color: #94a3b8;
        }

        .installment-pay-summary-item strong {
            display: block;
            font-size: 1.05rem;
            color: #0f172a;
        }

        .installment-pay-summary-item.is-amount strong {
            color: #b45309;
        }

        .installment-pay-modal .profile-subsection-title {
            margin-top: 0;
        }

        .installment-pay-modal .saving-payment-type-options {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 12px;
            margin-bottom: 14px;
        }

        .installment-pay-modal .saving-payment-type-item {
            position: relative;
        }

        .installment-pay-modal .saving-payment-type-item input[type="radio"] {
            position: absolute;
            opacity: 0;
            pointer-events: none;
        }

        .installment-pay-modal .saving-payment-type-item label {
            display: flex !important;
            align-items: center;
            gap: 12px;
            margin: 0;
            padding: 14px 16px;
            border-radius: 12px;
            border: 1px solid rgba(148, 163, 184, 0.35);
            background: #fff;
            cursor: pointer;
            transition: border-color 0.15s ease, box-shadow 0.15s ease, background 0.15s ease;
        }

        .installment-pay-modal .saving-payment-type-item input[type="radio"]:checked + label {
            border-color: rgba(229, 169, 6, 0.65);
            background: linear-gradient(135deg, rgba(246, 207, 99, 0.16) 0%, rgba(255, 251, 235, 0.9) 100%);
            box-shadow: 0 8px 20px -14px rgba(229, 169, 6, 0.55);
        }

        .installment-pay-modal .saving-shipping-type-icon {
            width: 42px;
            height: 42px;
            border-radius: 10px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 18px;
            color: #92400e;
            background: rgba(246, 207, 99, 0.22);
            flex-shrink: 0;
        }

        .installment-pay-modal .saving-shipping-type-text strong {
            display: block;
            font-size: 0.95rem;
            color: #0f172a;
            margin-bottom: 2px;
        }

        .installment-pay-modal .saving-shipping-type-text span {
            display: block;
            font-size: 0.82rem;
            color: #64748b;
            line-height: 1.4;
        }

        .installment-pay-cash-note {
            display: flex;
            align-items: flex-start;
            gap: 10px;
            margin-bottom: 14px;
            padding: 12px 14px;
            border-radius: 10px;
            background: #fffbeb;
            border: 1px solid rgba(229, 169, 6, 0.35);
            color: #78350f;
            font-size: 13px;
            line-height: 1.5;
        }

        .installment-pay-online-grid {
            display: grid;
            grid-template-columns: minmax(180px, 240px) minmax(0, 1fr);
            gap: 18px;
            align-items: start;
        }

        .installment-pay-online-grid .form-group label {
            display: flex;
            align-items: center;
            gap: 6px;
            font-size: 0.82rem;
            font-weight: 600;
            color: #64748b;
            margin-bottom: 7px;
        }

        .installment-pay-modal .profile-btn-primary-action {
            min-width: 130px;
        }

        @media (max-width: 767px) {
            .installment-pay-summary,
            .installment-pay-modal .saving-payment-type-options,
            .installment-pay-online-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>My Savings Installment</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="SAvingProductPurchaseReport.aspx">My Savings</a></li>
            <li class="active">Installments</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-calendar-check-o"></i></div>
                    <div class="profile-hero-info">
                        <h2>Installment Details</h2>
                        <p class="profile-hero-meta">Track monthly installment dates, payment status, and transaction references for your saving plan.</p>
                        <asp:Label ID="lblCouponCode" runat="server" CssClass="dash-saving-coupon-chip" Visible="false" />
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SAvingProductPurchaseReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i>Back to Savings</a>
                        <a href="SavingDashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-bank"></i>Dashboard</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-money"></i></span>
                        <div>
                            <h3>Monthly Installments</h3>
                            <p>Payment schedule and approval status for this coupon</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Pending installments can be paid from the action column when payment is enabled.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdGetHelp_RowDataBound" GridLines="None" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Installment Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date">
                                                <asp:Label ID="lblinstallmentdate" runat="server" Text='<%# Eval("installmentdate", "{0:dd MMM yyyy}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date">
                                                <asp:Label ID="lblreleasedate" runat="server" Text='<%# Eval("approvedate", "{0:dd MMM yyyy, hh:mm tt}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-inr"></i>
                                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product">
                                        <ItemTemplate>
                                            <span class="dash-saving-product">
                                                <asp:Label ID="lblproductname" runat="server" Text='<%# Eval("productname") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>' CssClass="dash-saving-status"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <span class="dash-saving-txn">
                                                <asp:Label ID="lbltransactionid" runat="server" Text='<%# Eval("OnlineTransactionId") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" CssClass="dash-saving-action-btn is-pay"><i class="fa fa-credit-card"></i> Pay</asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-calendar-times-o"></i>
                                        <h4>No installments found</h4>
                                        <p>Installment records for this coupon will appear here once they are generated.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfInstallmentId" runat="server" />
            <div id="myModal" class="modal fade installment-pay-modal" tabindex="-1" role="dialog" aria-labelledby="installmentPayModalTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="installmentPayModalTitle"><i class="fa fa-credit-card"></i> Pay Installment</h4>
                            <button type="button" class="close" onclick="Closepopup();" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label runat="server" ID="lblidedit" Visible="false"></asp:Label>

                            <div class="installment-pay-summary">
                                <div class="installment-pay-summary-item is-amount">
                                    <span>Amount</span>
                                    <strong><i class="fa fa-inr"></i> <asp:Literal ID="litPayAmount" runat="server" /></strong>
                                    <asp:TextBox ID="txtamountedit" Enabled="false" CssClass="form-control" runat="server" Style="display:none;"></asp:TextBox>
                                </div>
                                <div class="installment-pay-summary-item">
                                    <span>Installment Date</span>
                                    <strong><asp:Literal ID="litPayInstallmentDate" runat="server" /></strong>
                                    <asp:TextBox ID="txtinstallmentdateedit" Enabled="false" CssClass="form-control" runat="server" Style="display:none;"></asp:TextBox>
                                </div>
                            </div>

                            <p class="profile-subsection-title"><i class="fa fa-credit-card"></i> Payment Method</p>
                            <div class="saving-payment-type-options">
                                <div class="saving-payment-type-item">
                                    <asp:RadioButton ID="rbCashPayment" runat="server" GroupName="InstallmentPaymentMethod" onclick="toggleInstallmentPaymentMethod();" />
                                    <label for="<%= rbCashPayment.ClientID %>">
                                        <span class="saving-shipping-type-icon"><i class="fa fa-money"></i></span>
                                        <span class="saving-shipping-type-text">
                                            <strong>Cash</strong>
                                            <span>Submit directly to admin</span>
                                        </span>
                                    </label>
                                </div>
                                <div class="saving-payment-type-item">
                                    <asp:RadioButton ID="rbOnlinePayment" runat="server" GroupName="InstallmentPaymentMethod" Checked="true" onclick="toggleInstallmentPaymentMethod();" />
                                    <label for="<%= rbOnlinePayment.ClientID %>">
                                        <span class="saving-shipping-type-icon"><i class="fa fa-qrcode"></i></span>
                                        <span class="saving-shipping-type-text">
                                            <strong>Online</strong>
                                            <span>Pay via QR and upload proof</span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <asp:Panel ID="pnlCashPaymentInfo" runat="server" CssClass="installment-pay-cash-note" Style="display:none;">
                                <i class="fa fa-info-circle" aria-hidden="true"></i>
                                <span>Cash payment selected. Click <strong>Submit Payment</strong> and your installment request will be sent to admin.</span>
                            </asp:Panel>

                            <asp:Panel ID="pnlOnlinePaymentSection" runat="server">
                                <div class="installment-pay-online-grid">
                                    <div>
                                        <div class="topup-qr-card">
                                            <p class="topup-qr-label"><i class="fa fa-qrcode"></i> Scan to Pay</p>
                                            <asp:Image ID="imgPaymentQr" runat="server" CssClass="topup-qr-image" AlternateText="Payment QR code" />
                                        </div>
                                    </div>
                                    <div>
                                        <div class="form-group">
                                            <label for="<%= txttransactionidedit.ClientID %>"><i class="fa fa-exchange"></i> UTR No / Transaction ID</label>
                                            <asp:TextBox ID="txttransactionidedit" CssClass="form-control" runat="server" placeholder="Enter UTR or transaction reference" />
                                        </div>
                                        <div class="form-group profile-upload-field topup-payment-upload">
                                            <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                            <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="installmentPaymentUploadZone">
                                                <div class="profile-upload-zone-inner">
                                                    <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload"></i></span>
                                                    <p class="profile-upload-title">Drop payment screenshot here</p>
                                                    <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                                    <p class="profile-upload-meta">JPG, PNG, WEBP · receipt clearly visible</p>
                                                </div>
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                            </div>
                                            <div class="profile-upload-selection profile-upload-selection-attach" id="installmentPaymentUploadSelection" hidden>
                                                <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                    <img id="installmentPaymentUploadPreview" src="" alt="Payment screenshot preview" />
                                                </div>
                                                <div class="profile-upload-selection-info">
                                                    <span class="profile-upload-filechip" id="installmentPaymentUploadFilechip"></span>
                                                    <button type="button" class="profile-upload-clear" id="installmentPaymentUploadClear">
                                                        <i class="fa fa-times"></i> Remove
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Submit Payment" OnClientClick="return validate2();" CssClass="btn btn-primary profile-btn-primary-action" OnClick="btnUpdate_Click" UseSubmitBehavior="true" />
                            <button type="button" class="btn btn-default profile-btn-secondary-action" onclick="Closepopup();">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false, show: true });
            toggleInstallmentPaymentMethod();
        }

        function Closepopup() {
            var $modal = $('#myModal');
            $modal.modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '');
            $('.modal-backdrop').remove();
        }

        function toggleInstallmentPaymentMethod() {
            var online = document.getElementById('<%= rbOnlinePayment.ClientID %>');
            var onlineSection = document.getElementById('<%= pnlOnlinePaymentSection.ClientID %>');
            var cashInfo = document.getElementById('<%= pnlCashPaymentInfo.ClientID %>');
            var isOnline = online && online.checked;

            if (onlineSection) {
                onlineSection.style.display = isOnline ? '' : 'none';
            }
            if (cashInfo) {
                cashInfo.style.display = isOnline ? 'none' : '';
            }
        }

        function resetInstallmentPayModal() {
            var online = document.getElementById('<%= rbOnlinePayment.ClientID %>');
            var cash = document.getElementById('<%= rbCashPayment.ClientID %>');
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (online) online.checked = true;
            if (cash) cash.checked = false;
            if (txn) txn.value = '';
            clearInstallmentPaymentUpload();
            toggleInstallmentPaymentMethod();
        }

        function validate2() {
            var onlinePayment = document.getElementById('<%= rbOnlinePayment.ClientID %>');
            if (onlinePayment && onlinePayment.checked) {
                if (document.getElementById('<%= txttransactionidedit.ClientID %>').value.trim() === '') {
                    alert('Enter Transaction ID / UTR number');
                    document.getElementById('<%= txttransactionidedit.ClientID %>').focus();
                    return false;
                }

                var fileInput = document.getElementById('<%= FileUpload1.ClientID %>');
                if (!fileInput || !fileInput.files || fileInput.files.length === 0) {
                    alert('Please upload payment screenshot');
                    return false;
                }
            }

            return true;
        }

        (function () {
            var previewUrl = null;

            function clearInstallmentPaymentUpload() {
                var input = document.getElementById('<%= FileUpload1.ClientID %>');
                var zone = document.getElementById('installmentPaymentUploadZone');
                var selection = document.getElementById('installmentPaymentUploadSelection');
                var preview = document.getElementById('installmentPaymentUploadPreview');
                var filechip = document.getElementById('installmentPaymentUploadFilechip');
                if (!input || !zone || !selection || !preview || !filechip) return;

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

            window.clearInstallmentPaymentUpload = clearInstallmentPaymentUpload;

            function initInstallmentPaymentUpload() {
                var input = document.getElementById('<%= FileUpload1.ClientID %>');
                var zone = document.getElementById('installmentPaymentUploadZone');
                var selection = document.getElementById('installmentPaymentUploadSelection');
                var preview = document.getElementById('installmentPaymentUploadPreview');
                var filechip = document.getElementById('installmentPaymentUploadFilechip');
                var clearBtn = document.getElementById('installmentPaymentUploadClear');
                if (!input || !zone || input.getAttribute('data-bound') === '1') return;

                input.setAttribute('data-bound', '1');

                zone.addEventListener('click', function () { input.click(); });
                zone.addEventListener('dragover', function (e) { e.preventDefault(); zone.classList.add('is-dragover'); });
                zone.addEventListener('dragleave', function () { zone.classList.remove('is-dragover'); });
                zone.addEventListener('drop', function (e) {
                    e.preventDefault();
                    zone.classList.remove('is-dragover');
                    if (e.dataTransfer.files && e.dataTransfer.files[0]) {
                        input.files = e.dataTransfer.files;
                        input.dispatchEvent(new Event('change'));
                    }
                });

                input.addEventListener('change', function () {
                    var file = input.files && input.files[0];
                    if (!file || !file.type.match(/^image\//i)) {
                        alert('Please choose a valid image file (JPG, PNG, WEBP).');
                        clearInstallmentPaymentUpload();
                        return;
                    }
                    if (previewUrl) URL.revokeObjectURL(previewUrl);
                    previewUrl = URL.createObjectURL(file);
                    preview.src = previewUrl;
                    filechip.textContent = file.name;
                    selection.hidden = false;
                    zone.classList.add('is-hidden');
                });

                if (clearBtn) {
                    clearBtn.addEventListener('click', function (e) {
                        e.preventDefault();
                        clearInstallmentPaymentUpload();
                    });
                }
            }

            $(function () { initInstallmentPaymentUpload(); });
            Sys.Application.add_load(function () { initInstallmentPaymentUpload(); });
        })();
    </script>
</asp:Content>

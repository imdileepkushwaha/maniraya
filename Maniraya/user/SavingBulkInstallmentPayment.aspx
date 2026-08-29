<%@ Page Title="Bulk EMI Payment" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingBulkInstallmentPayment.aspx.cs" Inherits="user_SavingBulkInstallmentPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <style>
        .dash-saving-status.is-unpaid { background: #fff7ed; color: #c2410c; border: 1px solid #fed7aa; }
        .dash-saving-status.is-paid { background: #ecfdf5; color: #047857; border: 1px solid #a7f3d0; }
        .dash-saving-status.is-processing { background: #eff6ff; color: #1d4ed8; border: 1px solid #bfdbfe; }
        .dash-pay-installment-modal .modal-dialog { max-width: 920px; margin: 1.5rem auto; }
        .dash-pay-modal-content { border: 0; border-radius: 18px; overflow: hidden; box-shadow: 0 24px 60px rgba(15, 23, 42, 0.22); }
        .dash-pay-modal-header {
            display: flex; align-items: flex-start; justify-content: space-between; gap: 16px; border: 0;
            padding: 22px 24px 16px;
            background: radial-gradient(circle at top right, rgba(245, 158, 11, 0.18), transparent 42%),
                linear-gradient(135deg, #0f172a 0%, #1e293b 55%, #334155 100%);
            color: #f8fafc;
        }
        .dash-pay-modal-kicker { display: inline-flex; align-items: center; gap: 8px; font-size: 11px; font-weight: 700; letter-spacing: 0.06em; text-transform: uppercase; color: #fde68a; margin-bottom: 6px; }
        .dash-pay-modal-header .modal-title { margin: 0; font-size: 1.35rem; font-weight: 800; color: #fff; }
        .dash-pay-modal-close { width: 36px; height: 36px; border: 0; border-radius: 10px; background: rgba(255,255,255,0.1); color: #fff; flex: 0 0 auto; }
        .dash-pay-modal-body { padding: 20px 22px 8px; background: linear-gradient(180deg, #f8fafc 0%, #ffffff 40%); }
        .dash-pay-modal-grid { display: grid; grid-template-columns: minmax(220px, 0.9fr) minmax(280px, 1.2fr); gap: 18px; align-items: start; }
        .dash-pay-qr-card, .dash-pay-form-card { background: #fff; border: 1px solid #e2e8f0; border-radius: 14px; padding: 16px; box-shadow: 0 8px 24px rgba(15, 23, 42, 0.04); }
        .dash-pay-section-label { display: flex; align-items: center; gap: 8px; margin-bottom: 12px; font-size: 13px; font-weight: 800; color: #0f172a; }
        .dash-pay-section-label i { color: #d97706; }
        .dash-pay-qr-frame { display: grid; place-items: center; min-height: 220px; padding: 14px; border-radius: 12px; background: linear-gradient(180deg, #fffbeb 0%, #ffffff 100%); border: 1px dashed rgba(217, 119, 6, 0.35); }
        .dash-pay-qr-image img, .dash-pay-qr-frame img { max-width: 100%; height: auto !important; max-height: 260px; border-radius: 10px; display: block; }
        .dash-pay-qr-hint { margin: 12px 0 0; font-size: 12px; line-height: 1.45; color: #64748b; text-align: center; }
        .dash-pay-summary-row { display: grid; grid-template-columns: 1fr 1fr; gap: 12px; }
        .dash-pay-summary-label { display: block; margin-bottom: 6px; font-size: 12px; font-weight: 700; color: #64748b; }
        .dash-pay-input { border-radius: 10px !important; border-color: #dbe3ee !important; box-shadow: none !important; min-height: 42px; }
        .dash-pay-field label { display: block; margin-bottom: 6px; font-size: 12px; font-weight: 700; color: #475569; }
        .dash-pay-info-note { display: flex; gap: 10px; align-items: flex-start; margin-top: 8px; padding: 12px 14px; border-radius: 12px; background: #fffbeb; border: 1px solid #fde68a; color: #92400e; font-size: 12.5px; line-height: 1.45; }
        .dash-pay-info-note i { margin-top: 2px; color: #d97706; }
        .dash-pay-modal-footer { border-top: 1px solid #e2e8f0; background: #fff; padding: 14px 22px 18px; display: flex; justify-content: flex-end; gap: 10px; }
        .dash-pay-btn { display: inline-flex; align-items: center; justify-content: center; min-height: 42px; padding: 0 18px; border-radius: 10px; border: 1px solid transparent; font-size: 13px; font-weight: 700; cursor: pointer; text-decoration: none; }
        .dash-pay-btn.is-ghost { background: #f8fafc; border-color: #e2e8f0; color: #475569; }
        .dash-pay-btn.is-primary { background: linear-gradient(135deg, #f59e0b 0%, #d97706 100%); color: #fff !important; border-color: #d97706; box-shadow: 0 8px 18px rgba(217, 119, 6, 0.28); }
        @media (max-width: 767px) {
            .dash-pay-modal-grid, .dash-pay-summary-row { grid-template-columns: 1fr; }
            .dash-pay-modal-footer { flex-direction: column-reverse; }
            .dash-pay-btn { width: 100%; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Bulk EMI Payment</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Bulk EMI Payment</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-layer-group"></i></div>
                    <div class="profile-hero-info">
                        <h2>Bulk EMI Payment</h2>
                        <p class="profile-hero-meta">Pay remaining 17 installments together with one UTR and one payment screenshot. Each approved product / coupon is listed separately.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SavingProductInstallmentList.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-calendar"></i> Installment List</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-money-bill-alt"></i></span>
                        <div>
                            <h3>Approved Products</h3>
                            <p>Inst 1 is already approved. Pay remaining Inst 2–18 together with one UTR and one screenshot.</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Use <strong>Pay Remaining EMI</strong> for unpaid coupons. One UTR and one payment screenshot covers all remaining EMIs of that coupon. After admin approval, installment approve dates are set one month apart from your pay date (Inst 2 = next month, Inst 3 = month after, and so on).</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None"
                                OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
                                EmptyDataText="">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coupon Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcoupon" runat="server" Font-Bold="true" Text='<%# Eval("couponcode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproduct" runat="server" CssClass="dash-saving-product" Text='<%# Eval("productname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMI Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i>
                                                <asp:Label ID="lblemi" runat="server" Text='<%# SavingProductHelper.FormatMoney(Eval("EmiAmount")) %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining EMI">
                                        <ItemTemplate><%# Eval("PendingCount") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# SavingProductHelper.FormatMoney(Eval("TotalAmount")) %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" CssClass="dash-saving-status" Text='<%# Eval("PayStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Admin Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server" Text='<%# Eval("BulkRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPay" runat="server" CommandName="pay" CausesValidation="false"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CssClass="dash-saving-action-btn is-pay"><i class="fa fa-credit-card"></i> Pay Remaining EMI</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-calendar-times-o"></i>
                                        <h4>No pending EMI found</h4>
                                        <p>Approved first purchase with unpaid Inst 2–18 will appear here. If your purchase is still pending admin approval, wait for approval first.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hfCouponCode" runat="server" />
            <div id="myModal" class="modal fade dash-pay-installment-modal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content dash-pay-modal-content">
                        <div class="modal-header dash-pay-modal-header">
                            <div>
                                <span class="dash-pay-modal-kicker"><i class="fa fa-credit-card"></i> Bulk Payment</span>
                                <h4 class="modal-title">Pay Remaining EMI Together</h4>
                            </div>
                            <button type="button" class="dash-pay-modal-close" onclick="Closepopup();" aria-label="Close">
                                <i class="fa fa-times"></i>
                            </button>
                        </div>
                        <div class="modal-body dash-pay-modal-body">
                            <div class="dash-pay-modal-grid">
                                <div class="dash-pay-qr-card">
                                    <div class="dash-pay-section-label"><i class="fa fa-qrcode"></i> Scan &amp; Pay</div>
                                    <div class="dash-pay-qr-frame">
                                        <asp:Label ID="lblqrcode" runat="server" CssClass="dash-pay-qr-image" Text=""></asp:Label>
                                    </div>
                                    <p class="dash-pay-qr-hint">Pay the remaining EMI total using this QR, then submit one UTR and one screenshot.</p>
                                </div>
                                <div class="dash-pay-form-card">
                                    <div class="dash-pay-section-label"><i class="fa fa-info-circle"></i> Payment Summary</div>
                                    <div class="dash-pay-summary-row">
                                        <div>
                                            <span class="dash-pay-summary-label">Coupon</span>
                                            <asp:TextBox ID="txtcouponedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <span class="dash-pay-summary-label">Product</span>
                                            <asp:TextBox ID="txtproductedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <span class="dash-pay-summary-label">One EMI</span>
                                            <asp:TextBox ID="txtemiedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <span class="dash-pay-summary-label">Pay Amount</span>
                                            <asp:TextBox ID="txtamountedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="dash-pay-section-label" style="margin-top:18px;"><i class="fa fa-check-square"></i> Payment Proof</div>
                                    <div class="form-group dash-pay-field">
                                        <label for="<%= txttransactionidedit.ClientID %>"><i class="fa fa-exchange-alt"></i> UTR No / Transaction ID</label>
                                        <asp:TextBox ID="txttransactionidedit" CssClass="form-control dash-pay-input" runat="server" placeholder="Enter UTR or transaction reference"></asp:TextBox>
                                        <span id="bulkUtrCheckMsg" style="display:none;color:#c0392b;font-size:12px;margin-top:6px;font-weight:600;"></span>
                                    </div>
                                    <div class="form-group profile-upload-field topup-payment-upload dash-pay-upload">
                                        <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                        <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="bulkPaymentUploadZone">
                                            <div class="profile-upload-zone-inner">
                                                <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                <p class="profile-upload-title">Drop payment screenshot here</p>
                                                <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                                <p class="profile-upload-meta">JPG, PNG, WEBP - receipt clearly visible</p>
                                            </div>
                                            <asp:FileUpload ID="FileUpload1" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" runat="server" />
                                        </div>
                                        <div class="profile-upload-selection profile-upload-selection-attach" id="bulkPaymentUploadSelection" hidden>
                                            <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                <img id="bulkPaymentUploadPreview" src="" alt="Payment screenshot preview" />
                                            </div>
                                            <div class="profile-upload-selection-info">
                                                <span class="profile-upload-filechip" id="bulkPaymentUploadFilechip"></span>
                                                <button type="button" class="profile-upload-clear" id="bulkPaymentUploadClear">
                                                    <i class="fa fa-times"></i> Remove
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dash-pay-info-note">
                                        <i class="fa fa-info-circle" aria-hidden="true"></i>
                                        <span>One UTR and one screenshot will be used for all remaining EMIs of this coupon. Admin can approve or reject with a reason.</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer dash-pay-modal-footer">
                            <button type="button" class="dash-pay-btn is-ghost" onclick="Closepopup();">Close</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Submit Payment" OnClientClick="return validateBulkPay();" CssClass="dash-pay-btn is-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            setTimeout(function () {
                initBulkPaymentUpload();
                bindBulkUtrCheck();
            }, 50);
        }
        function openBulkPayFromRow(btn) {
            if (!btn) return false;
            var coupon = btn.getAttribute('data-coupon') || '';
            var product = btn.getAttribute('data-product') || '';
            var emi = btn.getAttribute('data-emi') || '';
            var total = btn.getAttribute('data-total') || '';
            var hf = document.getElementById('<%= hfCouponCode.ClientID %>');
            var couponBox = document.getElementById('<%= txtcouponedit.ClientID %>');
            var productBox = document.getElementById('<%= txtproductedit.ClientID %>');
            var emiBox = document.getElementById('<%= txtemiedit.ClientID %>');
            var amountBox = document.getElementById('<%= txtamountedit.ClientID %>');
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (hf) hf.value = coupon;
            if (couponBox) couponBox.value = coupon;
            if (productBox) productBox.value = product;
            if (emiBox) emiBox.value = emi;
            if (amountBox) amountBox.value = total;
            if (txn) txn.value = '';
            resetBulkUtrCheckState();
            showModal();
            return false;
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }

        var __bulkUtrUsed = false;
        var __bulkUtrLastChecked = '';
        var __bulkUtrCheckToken = 0;

        function setBulkUtrMessage(isUsed) {
            var msg = document.getElementById('bulkUtrCheckMsg');
            if (!msg) return;
            msg.style.display = isUsed ? 'block' : 'none';
            msg.innerHTML = isUsed ? 'This UTR No / Transaction ID is already used.' : '';
        }
        function resetBulkUtrCheckState() {
            __bulkUtrUsed = false;
            __bulkUtrLastChecked = '';
            setBulkUtrMessage(false);
        }
        function checkBulkUtrUsed() {
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (!txn) return;
            var utr = (txn.value || '').trim();
            if (!utr) { resetBulkUtrCheckState(); return; }
            if (__bulkUtrLastChecked === utr) { setBulkUtrMessage(__bulkUtrUsed); return; }
            var token = ++__bulkUtrCheckToken;
            $.ajax({
                type: 'POST',
                url: 'SavingBulkInstallmentPayment.aspx/CheckOnlineTransactionId',
                data: JSON.stringify({ onlineTransactionId: utr }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    if (token !== __bulkUtrCheckToken) return;
                    if ((txn.value || '').trim() !== utr) return;
                    __bulkUtrLastChecked = utr;
                    __bulkUtrUsed = !!(res && res.d === true);
                    setBulkUtrMessage(__bulkUtrUsed);
                }
            });
        }
        function bindBulkUtrCheck() {
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (!txn || txn.getAttribute('data-utr-bound') === '1') return;
            txn.setAttribute('data-utr-bound', '1');
            txn.addEventListener('input', resetBulkUtrCheckState);
            txn.addEventListener('blur', checkBulkUtrUsed);
        }
        function validateBulkPay() {
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            var file = document.getElementById('<%= FileUpload1.ClientID %>');
            if (!txn || !txn.value || !txn.value.trim()) {
                alert('Please enter UTR No / Transaction ID.');
                if (txn) txn.focus();
                return false;
            }
            if (__bulkUtrUsed && __bulkUtrLastChecked === txn.value.trim()) {
                alert('This UTR No / Transaction ID is already used.');
                txn.focus();
                return false;
            }
            if (!file || !file.files || file.files.length === 0) {
                alert('Please upload payment screenshot.');
                return false;
            }
            return true;
        }

        (function () {
            var previewUrl = null;
            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }
            window.initBulkPaymentUpload = function () {
                var input = document.getElementById('<%= FileUpload1.ClientID %>');
                var zone = document.getElementById('bulkPaymentUploadZone');
                var selection = document.getElementById('bulkPaymentUploadSelection');
                var preview = document.getElementById('bulkPaymentUploadPreview');
                var filechip = document.getElementById('bulkPaymentUploadFilechip');
                var clearBtn = document.getElementById('bulkPaymentUploadClear');
                if (!input || !zone || input.getAttribute('data-bound') === '1') return;
                input.setAttribute('data-bound', '1');
                input.addEventListener('change', function () {
                    var file = input.files && input.files[0];
                    if (!file || !file.type.match(/^image\//i)) {
                        alert('Please choose a valid image file (JPG, PNG, WEBP).');
                        return;
                    }
                    if (previewUrl) URL.revokeObjectURL(previewUrl);
                    previewUrl = URL.createObjectURL(file);
                    preview.src = previewUrl;
                    filechip.textContent = file.name + ' — ' + formatFileSize(file.size);
                    selection.hidden = false;
                    zone.classList.add('is-hidden');
                });
                zone.addEventListener('click', function (e) {
                    if (e.target === input) return;
                    input.click();
                });
                if (clearBtn) {
                    clearBtn.addEventListener('click', function (e) {
                        e.preventDefault();
                        input.value = '';
                        if (previewUrl) { URL.revokeObjectURL(previewUrl); previewUrl = null; }
                        preview.removeAttribute('src');
                        filechip.textContent = '';
                        selection.hidden = true;
                        zone.classList.remove('is-hidden');
                    });
                }
            };
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initBulkPaymentUpload);
            } else {
                initBulkPaymentUpload();
            }
        })();
    </script>
</asp:Content>

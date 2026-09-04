<%@ Page Title="My Savings Installment" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingProductInstallmentDetail.aspx.cs" Inherits="user_SavingProductInstallmentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://sdk.cashfree.com/js/v3/cashfree.js"></script>
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=26" rel="stylesheet" />
    <style>
        .dash-saving-product.is-unassigned {
            color: #94a3b8;
            font-style: italic;
            font-weight: 600;
        }
        .dash-pay-installment-modal .modal-dialog {
            max-width: 920px;
            margin: 1.5rem auto;
        }
        .dash-pay-modal-content {
            border: 0;
            border-radius: 18px;
            overflow: hidden;
            box-shadow: 0 24px 60px rgba(15, 23, 42, 0.22);
        }
        .dash-pay-modal-header {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            gap: 16px;
            border: 0;
            padding: 22px 24px 16px;
            background:
                radial-gradient(circle at top right, rgba(245, 158, 11, 0.18), transparent 42%),
                linear-gradient(135deg, #0f172a 0%, #1e293b 55%, #334155 100%);
            color: #f8fafc;
        }
        .dash-pay-modal-kicker {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #fde68a;
            margin-bottom: 6px;
        }
        .dash-pay-modal-header .modal-title {
            margin: 0;
            font-size: 1.35rem;
            font-weight: 800;
            color: #fff;
        }
        .dash-pay-modal-sub {
            margin: 6px 0 0;
            font-size: 13px;
            color: rgba(248, 250, 252, 0.78);
            max-width: 46ch;
        }
        .dash-pay-modal-close {
            width: 36px;
            height: 36px;
            border: 0;
            border-radius: 10px;
            background: rgba(255,255,255,0.1);
            color: #fff;
            flex: 0 0 auto;
        }
        .dash-pay-modal-close:hover { background: rgba(255,255,255,0.18); }
        .dash-pay-modal-body {
            padding: 20px 22px 8px;
            background: linear-gradient(180deg, #f8fafc 0%, #ffffff 40%);
        }
        .dash-pay-modal-grid {
            display: grid;
            grid-template-columns: minmax(220px, 0.9fr) minmax(280px, 1.2fr);
            gap: 18px;
            align-items: start;
        }
        .dash-pay-qr-card,
        .dash-pay-form-card {
            background: #fff;
            border: 1px solid #e2e8f0;
            border-radius: 14px;
            padding: 16px;
            box-shadow: 0 8px 24px rgba(15, 23, 42, 0.04);
        }
        .dash-pay-section-label {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-bottom: 12px;
            font-size: 13px;
            font-weight: 800;
            color: #0f172a;
        }
        .dash-pay-section-label i { color: #d97706; }
        .dash-pay-qr-frame {
            display: grid;
            place-items: center;
            min-height: 220px;
            padding: 14px;
            border-radius: 12px;
            background:
                linear-gradient(180deg, #fffbeb 0%, #ffffff 100%);
            border: 1px dashed rgba(217, 119, 6, 0.35);
        }
        .dash-pay-qr-image img,
        .dash-pay-qr-frame img {
            max-width: 100%;
            height: auto !important;
            max-height: 260px;
            border-radius: 10px;
            display: block;
        }
        .dash-pay-qr-hint {
            margin: 12px 0 0;
            font-size: 12px;
            line-height: 1.45;
            color: #64748b;
            text-align: center;
        }
        .dash-pay-summary-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 12px;
        }
        .dash-pay-summary-label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 700;
            color: #64748b;
        }
        .dash-pay-amount-field {
            position: relative;
        }
        .dash-pay-amount-field > i {
            position: absolute;
            left: 12px;
            top: 50%;
            transform: translateY(-50%);
            color: #b45309;
            z-index: 1;
        }
        .dash-pay-amount-field .dash-pay-input {
            padding-left: 30px;
            font-weight: 800;
            color: #92400e;
        }
        .dash-pay-input {
            border-radius: 10px !important;
            border-color: #dbe3ee !important;
            box-shadow: none !important;
            min-height: 42px;
        }
        .dash-pay-field label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 700;
            color: #475569;
        }
        .dash-pay-upload {
            margin-bottom: 10px;
        }
        .dash-pay-info-note {
            display: flex;
            gap: 10px;
            align-items: flex-start;
            margin-top: 8px;
            padding: 12px 14px;
            border-radius: 12px;
            background: #fffbeb;
            border: 1px solid #fde68a;
            color: #92400e;
            font-size: 12.5px;
            line-height: 1.45;
        }
        .dash-pay-info-note i {
            margin-top: 2px;
            color: #d97706;
        }
        .dash-pay-modal-footer {
            border-top: 1px solid #e2e8f0;
            background: #fff;
            padding: 14px 22px 18px;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }
        .dash-pay-btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-height: 42px;
            padding: 0 18px;
            border-radius: 10px;
            border: 1px solid transparent;
            font-size: 13px;
            font-weight: 700;
            cursor: pointer;
            text-decoration: none;
        }
        .dash-pay-btn.is-ghost {
            background: #f8fafc;
            border-color: #e2e8f0;
            color: #475569;
        }
        .dash-pay-btn.is-ghost:hover {
            background: #f1f5f9;
            color: #0f172a;
        }
        .dash-pay-btn.is-primary {
            background: linear-gradient(135deg, #f59e0b 0%, #d97706 100%);
            color: #fff !important;
            border-color: #d97706;
            box-shadow: 0 8px 18px rgba(217, 119, 6, 0.28);
        }
        .dash-pay-btn.is-primary:hover {
            filter: brightness(1.03);
            color: #fff !important;
        }
        .dash-pay-method-options {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 10px;
        }
        .dash-pay-method-item {
            position: relative;
        }
        .dash-pay-method-item input[type="radio"] {
            position: absolute;
            opacity: 0;
            pointer-events: none;
        }
        .dash-pay-method-item label {
            display: flex;
            align-items: center;
            gap: 10px;
            min-height: 72px;
            margin: 0;
            padding: 12px;
            border-radius: 12px;
            border: 1px solid #e2e8f0;
            background: #f8fafc;
            cursor: pointer;
            transition: border-color 0.2s ease, background 0.2s ease, box-shadow 0.2s ease;
        }
        .dash-pay-method-item label:hover {
            border-color: #fcd34d;
            background: #fffbeb;
        }
        .dash-pay-method-item input[type="radio"]:checked + label {
            border-color: #d97706;
            background: linear-gradient(180deg, #fffbeb 0%, #ffffff 100%);
            box-shadow: 0 8px 18px rgba(217, 119, 6, 0.12);
        }
        .dash-pay-method-icon {
            position: relative;
            width: 40px;
            height: 40px;
            border-radius: 10px;
            display: grid;
            place-items: center;
            flex: 0 0 auto;
            color: #b45309;
            background: rgba(245, 158, 11, 0.14);
        }
        .dash-pay-method-icon > i {
            position: absolute !important;
            width: 1px !important;
            height: 1px !important;
            overflow: hidden !important;
            clip: rect(0, 0, 0, 0) !important;
            border: 0 !important;
        }
        .dash-pay-method-icon::before {
            content: "";
            display: block;
            width: 1.25em;
            height: 1.25em;
            background-color: currentColor;
            -webkit-mask-repeat: no-repeat;
            mask-repeat: no-repeat;
            -webkit-mask-position: center;
            mask-position: center;
            -webkit-mask-size: contain;
            mask-size: contain;
        }
        .dash-pay-method-icon.is-online::before {
            -webkit-mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='black' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3E%3Crect x='3' y='3' width='7' height='7' rx='1'/%3E%3Crect x='14' y='3' width='7' height='7' rx='1'/%3E%3Crect x='3' y='14' width='7' height='7' rx='1'/%3E%3Cpath d='M14 14h3v3h-3zM20 14h1v1h-1zM17 17h1v1h-1zM20 20h1v1h-1zM14 20h1v1h-1z'/%3E%3C/svg%3E");
            mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='black' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3E%3Crect x='3' y='3' width='7' height='7' rx='1'/%3E%3Crect x='14' y='3' width='7' height='7' rx='1'/%3E%3Crect x='3' y='14' width='7' height='7' rx='1'/%3E%3Cpath d='M14 14h3v3h-3zM20 14h1v1h-1zM17 17h1v1h-1zM20 20h1v1h-1zM14 20h1v1h-1z'/%3E%3C/svg%3E");
        }
        .dash-pay-method-icon.is-cash::before {
            -webkit-mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='black' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3E%3Crect x='2' y='6' width='20' height='12' rx='2'/%3E%3Ccircle cx='12' cy='12' r='3'/%3E%3Cpath d='M6 12h.01M18 12h.01'/%3E%3C/svg%3E");
            mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='black' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3E%3Crect x='2' y='6' width='20' height='12' rx='2'/%3E%3Ccircle cx='12' cy='12' r='3'/%3E%3Cpath d='M6 12h.01M18 12h.01'/%3E%3C/svg%3E");
        }
        .dash-pay-method-text {
            display: flex;
            flex-direction: column;
            gap: 2px;
            min-width: 0;
        }
        .dash-pay-method-text strong {
            font-size: 13px;
            color: #0f172a;
        }
        .dash-pay-method-text span {
            font-size: 11.5px;
            color: #64748b;
            line-height: 1.35;
        }
        .dash-pay-modal-grid.is-cash .dash-pay-qr-card {
            display: none;
        }
        .dash-pay-modal-grid.is-cash {
            grid-template-columns: 1fr;
        }
        .saving-cashfree-box {
            margin: 14px 0 8px;
            padding: 14px 16px;
            border-radius: 14px;
            border: 1px solid rgba(37, 99, 235, 0.22);
            background: linear-gradient(135deg, #eff6ff 0%, #f8fafc 100%);
        }
        .saving-cashfree-box h4 {
            margin: 0 0 6px;
            font-size: 0.95rem;
            font-weight: 800;
            color: #1e3a8a;
        }
        .saving-cashfree-box p {
            margin: 0 0 12px;
            font-size: 12.5px;
            color: #334155;
            line-height: 1.45;
        }
        .saving-cashfree-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            align-items: center;
        }
        .btn-cashfree {
            background: #2563eb !important;
            border-color: #2563eb !important;
            color: #fff !important;
        }
        .btn-cashfree:hover {
            background: #1d4ed8 !important;
            border-color: #1d4ed8 !important;
            color: #fff !important;
        }
        .saving-manual-divider {
            margin: 12px 0 8px;
            display: flex;
            align-items: center;
            gap: 12px;
            color: #94a3b8;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.06em;
            text-transform: uppercase;
        }
        .saving-manual-divider::before,
        .saving-manual-divider::after {
            content: "";
            flex: 1;
            height: 1px;
            background: #e2e8f0;
        }
        @media (max-width: 767px) {
            .dash-pay-modal-grid,
            .dash-pay-summary-row,
            .dash-pay-method-options {
                grid-template-columns: 1fr;
            }
            .dash-pay-modal-header,
            .dash-pay-modal-body,
            .dash-pay-modal-footer {
                padding-left: 16px;
                padding-right: 16px;
            }
            .dash-pay-modal-footer {
                flex-direction: column-reverse;
            }
            .dash-pay-btn { width: 100%; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>My Savings Installment</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home</a></li>
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
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-calendar-check"></i></div>
                    <div class="profile-hero-info">
                        <h2>Installment Details</h2>
                        <p class="profile-hero-meta">Track monthly installment dates, payment status, and transaction references for your saving plan.</p>
                        <asp:Label ID="lblCouponCode" runat="server" CssClass="dash-saving-coupon-chip" Visible="false" />
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SAvingProductPurchaseReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i>Back to Savings</a>
                        <a href="SavingDashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-university"></i>Dashboard</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-money-bill-alt"></i></span>
                        <div>
                            <h3>Monthly Installments</h3>
                            <p>Payment schedule and approval status for this coupon</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Status comes from SavingAccountInstallmentDetail: Approved, Pending, Processing, or Rejected. Pending / Rejected installments can be paid from the action column.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdGetHelp_RowDataBound" GridLines="None" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inst No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinstno" runat="server" Text='<%# Eval("instno") %>'></asp:Label>
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
                                            <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i>
                                                <asp:Label ID="lblamount" runat="server" Text='<%# SavingProductHelper.FormatMoney(Eval("amount")) %>'></asp:Label></span>
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
                                      <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <span class="dash-saving-txn">
                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label></span>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">

    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModal" class="modal fade dash-pay-installment-modal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content dash-pay-modal-content">
                        <div class="modal-header dash-pay-modal-header">
                            <div class="dash-pay-modal-header-copy">
                                <span class="dash-pay-modal-kicker"><i class="fa fa-credit-card"></i> Payment</span>
                                <h4 class="modal-title">Pay Installment</h4>
                                <!-- <p class="dash-pay-modal-sub">Scan QR / transfer amount, then submit UTR with payment screenshot.</p> -->
                            </div>
                            <button type="button" class="dash-pay-modal-close" onclick="Closepopup();" aria-label="Close">
                                <i class="fa fa-times"></i>
                            </button>
                        </div>
                        <div class="modal-body dash-pay-modal-body">
                            <asp:Label runat="server" ID="lblidedit" style="display:none;" aria-hidden="true"></asp:Label>

                            <div class="dash-pay-modal-grid">
                                <div class="dash-pay-qr-card" id="dashPayQrCard">
                                    <div class="dash-pay-section-label"><i class="fa fa-qrcode"></i> Scan &amp; Pay</div>
                                    <div class="dash-pay-qr-frame">
                                        <asp:Label ID="lblqrcode" runat="server" CssClass="dash-pay-qr-image" Text=""></asp:Label>
                                    </div>
                                    <p class="dash-pay-qr-hint">Use UPI / bank QR shown above to pay the installment amount.</p>
                                </div>

                                <div class="dash-pay-form-card">
                                    <div class="dash-pay-section-label"><i class="fa fa-info-circle"></i> Installment Summary</div>
                                    <div class="dash-pay-summary-row">
                                        <div class="dash-pay-summary-item">
                                            <span class="dash-pay-summary-label">Amount</span>
                                            <div class="dash-pay-amount-field">
                                                <i class="fa fa-rupee-sign"></i>
                                                <asp:TextBox ID="txtamountedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="dash-pay-summary-item">
                                            <span class="dash-pay-summary-label">Installment Date</span>
                                            <asp:TextBox ID="txtinstallmentdateedit" Enabled="false" CssClass="form-control dash-pay-input" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="dash-pay-section-label" style="margin-top:18px;"><i class="fa fa-credit-card"></i> Payment Method</div>
                                    <div class="dash-pay-method-options">
                                        <div class="dash-pay-method-item">
                                            <asp:RadioButton ID="rbOnlinePayment" runat="server" GroupName="InstallmentPaymentMethod" Checked="true" />
                                            <label for="<%= rbOnlinePayment.ClientID %>">
                                                <span class="dash-pay-method-icon is-online"><i class="fa fa-qrcode"></i></span>
                                                <span class="dash-pay-method-text">
                                                    <strong>Online</strong>
                                                    <span>Pay with Cashfree, or submit UTR</span>
                                                </span>
                                            </label>
                                        </div>
                                        <div class="dash-pay-method-item">
                                            <asp:RadioButton ID="rbCashPayment" runat="server" GroupName="InstallmentPaymentMethod" />
                                            <label for="<%= rbCashPayment.ClientID %>">
                                                <span class="dash-pay-method-icon is-cash"><i class="fa fa-money-bill-alt"></i></span>
                                                <span class="dash-pay-method-text">
                                                    <strong>Cash</strong>
                                                    <span>Submit directly to admin</span>
                                                </span>
                                            </label>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlCashPaymentInfo" runat="server" CssClass="dash-pay-info-note" Style="display:none; margin-top:14px;">
                                        <i class="fa fa-info-circle" aria-hidden="true"></i>
                                        <span>Cash payment selected. Click <strong>Submit Payment</strong> and your installment request will go to admin for approval. UTR and screenshot are not required.</span>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlOnlinePaymentSection" runat="server">
                                        <div class="saving-cashfree-box">
                                            <h4><i class="fa fa-shield-alt"></i> Pay Online with Cashfree</h4>
                                            <p>UPI, card or net banking. After payment the installment request goes to admin for approval, same as UTR or cash.</p>
                                            <div class="saving-cashfree-actions">
                                                <asp:Button ID="btnPayCashfree" runat="server" CssClass="btn btn-primary btn-cashfree dash-pay-btn is-primary"
                                                    Text="Pay with Cashfree" CausesValidation="false" OnClick="btnPayCashfree_Click" />
                                                <asp:Label ID="lblCashfreeHint" runat="server" CssClass="saving-shipping-empty-text" />
                                            </div>
                                        </div>

                                        <div class="saving-manual-divider">Or submit UTR payment proof</div>

                                        <div class="dash-pay-section-label" style="margin-top:10px;"><i class="fa fa-check-square"></i> Payment Proof</div>
                                        <div class="form-group dash-pay-field">
                                            <label for="<%= txttransactionidedit.ClientID %>"><i class="fa fa-exchange-alt"></i> UTR No / Transaction ID</label>
                                            <asp:TextBox ID="txttransactionidedit" CssClass="form-control dash-pay-input" runat="server" placeholder="Enter UTR or transaction reference"></asp:TextBox>
                                            <span id="installmentUtrCheckMsg" style="display:none;color:#c0392b;font-size:12px;margin-top:6px;font-weight:600;"></span>
                                        </div>

                                        <div class="form-group profile-upload-field topup-payment-upload dash-pay-upload">
                                            <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                            <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="installmentPaymentUploadZone">
                                                <div class="profile-upload-zone-inner">
                                                    <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                    <p class="profile-upload-title">Drop payment screenshot here</p>
                                                    <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                                    <p class="profile-upload-meta">JPG, PNG, WEBP - receipt clearly visible</p>
                                                </div>
                                                <asp:FileUpload ID="FileUpload1" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" runat="server" />
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

                                        <div class="dash-pay-info-note">
                                            <i class="fa fa-info-circle" aria-hidden="true"></i>
                                            <span>After payment, enter UTR and upload a clear screenshot. Admin will verify and approve your installment.</span>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer dash-pay-modal-footer">
                            <button type="button" class="dash-pay-btn is-ghost" onclick="Closepopup();">Close</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Submit Payment" OnClientClick="return validate2();" CssClass="dash-pay-btn is-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="btnPayCashfree" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            setTimeout(function () {
                resetInstallmentPaymentMethod();
                initInstallmentPaymentUpload();
                syncInstallmentPaymentMethodUI();
                bindInstallmentUtrCheck();
            }, 50);
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function isInstallmentOnlinePayment() {
            var online = document.getElementById('<%= rbOnlinePayment.ClientID %>');
            return !online || online.checked;
        }
        function resetInstallmentPaymentMethod() {
            var online = document.getElementById('<%= rbOnlinePayment.ClientID %>');
            var cash = document.getElementById('<%= rbCashPayment.ClientID %>');
            if (online) online.checked = true;
            if (cash) cash.checked = false;
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (txn) txn.value = '';
            resetInstallmentUtrCheckState();
        }
        function syncInstallmentPaymentMethodUI() {
            var isOnline = isInstallmentOnlinePayment();
            var onlineSection = document.getElementById('<%= pnlOnlinePaymentSection.ClientID %>');
            var cashInfo = document.getElementById('<%= pnlCashPaymentInfo.ClientID %>');
            var grid = document.querySelector('#myModal .dash-pay-modal-grid');
            if (onlineSection) onlineSection.style.display = isOnline ? '' : 'none';
            if (cashInfo) cashInfo.style.display = isOnline ? 'none' : '';
            if (grid) {
                if (isOnline) grid.classList.remove('is-cash');
                else grid.classList.add('is-cash');
            }
            if (!isOnline) {
                resetInstallmentUtrCheckState();
            }
        }

        var __installmentUtrUsed = false;
        var __installmentUtrLastChecked = '';
        var __installmentUtrCheckToken = 0;

        function setInstallmentUtrMessage(isUsed) {
            var msg = document.getElementById('installmentUtrCheckMsg');
            if (!msg) return;
            if (isUsed) {
                msg.style.display = 'block';
                msg.innerHTML = 'This UTR No / Transaction ID is already used.';
            } else {
                msg.style.display = 'none';
                msg.innerHTML = '';
            }
        }

        function resetInstallmentUtrCheckState() {
            __installmentUtrUsed = false;
            __installmentUtrLastChecked = '';
            setInstallmentUtrMessage(false);
        }

        function checkInstallmentUtrUsed() {
            if (!isInstallmentOnlinePayment()) {
                resetInstallmentUtrCheckState();
                return;
            }
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (!txn) return;
            var utr = (txn.value || '').trim();
            if (!utr) {
                resetInstallmentUtrCheckState();
                return;
            }
            if (__installmentUtrLastChecked === utr) {
                setInstallmentUtrMessage(__installmentUtrUsed);
                return;
            }
            var idEl = document.getElementById('<%= lblidedit.ClientID %>');
            var installmentId = idEl ? (idEl.innerText || idEl.textContent || '') : '';
            var token = ++__installmentUtrCheckToken;

            $.ajax({
                type: 'POST',
                url: 'SavingProductInstallmentDetail.aspx/CheckOnlineTransactionId',
                data: JSON.stringify({ onlineTransactionId: utr, installmentId: installmentId }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    if (token !== __installmentUtrCheckToken) return;
                    var current = (txn.value || '').trim();
                    if (current !== utr) return;
                    __installmentUtrLastChecked = utr;
                    __installmentUtrUsed = !!(res && res.d === true);
                    setInstallmentUtrMessage(__installmentUtrUsed);
                }
            });
        }

        function bindInstallmentUtrCheck() {
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            if (!txn) return;
            if (txn.getAttribute('data-utr-bound') === '1') return;
            txn.setAttribute('data-utr-bound', '1');
            txn.addEventListener('input', function () {
                resetInstallmentUtrCheckState();
            });
            txn.addEventListener('blur', function () { checkInstallmentUtrUsed(); });
        }

        function validate2() {
            if (!isInstallmentOnlinePayment()) {
                return true;
            }
            var txn = document.getElementById('<%= txttransactionidedit.ClientID %>');
            var file = document.getElementById('<%= FileUpload1.ClientID %>');
            if (!txn || !txn.value || !txn.value.trim()) {
                alert('Please enter UTR No / Transaction ID.');
                if (txn) txn.focus();
                return false;
            }
            if (__installmentUtrUsed && __installmentUtrLastChecked === txn.value.trim()) {
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
            function bindPaymentMethodToggle() {
                var online = document.getElementById('<%= rbOnlinePayment.ClientID %>');
                var cash = document.getElementById('<%= rbCashPayment.ClientID %>');
                if (online && online.getAttribute('data-bound') !== '1') {
                    online.setAttribute('data-bound', '1');
                    online.addEventListener('change', syncInstallmentPaymentMethodUI);
                }
                if (cash && cash.getAttribute('data-bound') !== '1') {
                    cash.setAttribute('data-bound', '1');
                    cash.addEventListener('change', syncInstallmentPaymentMethodUI);
                }
                syncInstallmentPaymentMethodUI();
                bindInstallmentUtrCheck();
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', bindPaymentMethodToggle);
            } else {
                bindPaymentMethodToggle();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    bindPaymentMethodToggle();
                });
            }
        })();

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
                filechip.textContent = file.name + ' � ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            window.initInstallmentPaymentUpload = function () {
                var input = document.getElementById('<%= FileUpload1.ClientID %>');
                var zone = document.getElementById('installmentPaymentUploadZone');
                var selection = document.getElementById('installmentPaymentUploadSelection');
                var preview = document.getElementById('installmentPaymentUploadPreview');
                var filechip = document.getElementById('installmentPaymentUploadFilechip');
                var clearBtn = document.getElementById('installmentPaymentUploadClear');

                if (!input || !zone || input.getAttribute('data-bound') === '1') {
                    return;
                }

                input.setAttribute('data-bound', '1');

                input.addEventListener('change', function () {
                    var file = input.files && input.files[0];
                    if (file) {
                        showPaymentUpload(file, zone, selection, preview, filechip);
                    }
                });

                zone.addEventListener('click', function (e) {
                    if (e.target === input) return;
                    input.click();
                });

                ['dragenter', 'dragover'].forEach(function (evtName) {
                    zone.addEventListener(evtName, function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.add('is-dragover');
                    });
                });

                ['dragleave', 'drop'].forEach(function (evtName) {
                    zone.addEventListener(evtName, function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.remove('is-dragover');
                    });
                });

                zone.addEventListener('drop', function (e) {
                    var files = e.dataTransfer && e.dataTransfer.files;
                    if (!files || !files.length) return;
                    try {
                        var dt = new DataTransfer();
                        dt.items.add(files[0]);
                        input.files = dt.files;
                    } catch (err) {
                        // Fallback: some browsers block programmatic FileList assign
                    }
                    showPaymentUpload(files[0], zone, selection, preview, filechip);
                });

                if (clearBtn) {
                    clearBtn.addEventListener('click', function (e) {
                        e.preventDefault();
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    });
                }
            };

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initInstallmentPaymentUpload);
            } else {
                initInstallmentPaymentUpload();
            }
        })();

        window.startCashfreeCheckout = function (sessionId, mode) {
            if (!sessionId) {
                alert('Cashfree session is missing. Please try again.');
                return;
            }

            function openCheckout() {
                if (typeof Cashfree !== 'function') {
                    alert('Cashfree checkout could not load. Please refresh and try again.');
                    return;
                }

                try {
                    var cashfree = Cashfree({ mode: mode || 'production' });
                    var result = cashfree.checkout({
                        paymentSessionId: sessionId,
                        redirectTarget: '_self'
                    });
                    if (result && typeof result.then === 'function') {
                        result.then(function (res) {
                            if (res && res.error) {
                                alert(res.error.message || 'Unable to open Cashfree checkout.');
                            }
                        }).catch(function () {
                            alert('Unable to open Cashfree checkout. Please try again.');
                        });
                    }
                } catch (ex) {
                    alert((ex && ex.message) ? ex.message : 'Unable to open Cashfree checkout.');
                }
            }

            if (window.Cashfree) {
                openCheckout();
                return;
            }

            var script = document.createElement('script');
            script.src = 'https://sdk.cashfree.com/js/v3/cashfree.js';
            script.onload = openCheckout;
            script.onerror = function () {
                alert('Unable to load Cashfree checkout. Please try again.');
            };
            document.head.appendChild(script);
        };
    </script>
</asp:Content>

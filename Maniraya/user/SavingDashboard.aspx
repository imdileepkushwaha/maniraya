<%@ Page Title="Saving Dashboard" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingDashboard.aspx.cs" Inherits="user_SavingDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=23" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Saving Dashboard</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">Saving Dashboard</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upSavingDashboard" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="profile-page dash-subpage dash-subpage--saving">
        <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-bank"></i></div>
            <div class="profile-hero-info">
                <h2>Saving Dashboard</h2>
                <p class="profile-hero-meta">Track your care number, monthly purchases, maturity plan, and level income in one place.</p>
            </div>
            <div class="profile-hero-actions">
                <a href="SavingProductPurchase.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-shopping-cart"></i> Buy Product</a>
                <a href="SAvingProductPurchaseReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list-alt"></i> Saving Report</a>
                <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Dashboard</a>
            </div>
        </div>

        <div class="dash-subpage-layout">
            <div class="dash-subpage-panel dash-subpage-panel--care">
                <div class="dash-subpage-panel-head">
                    <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-id-card"></i></span>
                    <div>
                        <h3>Care Card</h3>
                        <p>Your unique saving identity</p>
                    </div>
                </div>
                <div class="dash-subpage-panel-body">
                    <div class="dash-care-showcase">
                        <div class="dash-care-showcase-top">
                            <span class="dash-care-showcase-label">Unique Care Number</span>
                            <span class="dash-care-showcase-chip" aria-hidden="true"><i class="fa fa-credit-card"></i></span>
                        </div>
                        <div class="dash-care-showcase-number">
                            <asp:Label ID="lblcardno" runat="server" Text="0000 0000 0000" />
                        </div>
                        <div class="dash-care-showcase-select-wrap">
                            <label class="dash-care-showcase-select-label" for="<%= ddlCouponCode.ClientID %>">Select Coupon Code</label>
                            <asp:DropDownList ID="ddlCouponCode" runat="server" CssClass="dash-care-showcase-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCouponCode_SelectedIndexChanged" />
                        </div>
                        <div class="dash-care-showcase-footer">
                            <div>
                                <span class="dash-care-showcase-status-label">Account Status</span>
                                <asp:Label ID="lblsavingstatus" runat="server" Text="-" CssClass="dash-status-badge" />
                            </div>
                            <span class="dash-care-showcase-chip" aria-hidden="true"><i class="fa fa-bank"></i></span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="dash-subpage-stack">
                <div class="dash-subpage-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-blue" aria-hidden="true"><i class="fa fa-calendar"></i></span>
                        <div>
                            <h3>Monthly Purchase Summary</h3>
                            <p>Paid and pending installment overview</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="dash-metric-grid dash-metric-grid--3">
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-slate" aria-hidden="true"><i class="fa fa-list-ol"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Total Monthly Purchase</p>
                                    <p class="dash-metric-card-value"> <asp:Label ID="lbltotalemi" runat="server" Text="0.00" /></p>
                                </div>
                            </div>
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-green" aria-hidden="true"><i class="fa fa-check-circle"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Paid Monthly Purchase</p>
                                    <p class="dash-metric-card-value"> <asp:Label ID="lblpaidemi" runat="server" Text="0.00" /></p>
                                </div>
                            </div>
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-amber" aria-hidden="true"><i class="fa fa-clock-o"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Unpaid Monthly Purchase</p>
                                    <p class="dash-metric-card-value"> <asp:Label ID="lblpendingemi" runat="server" Text="0.00" /></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="dash-subpage-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-line-chart"></i></span>
                        <div>
                            <h3>Investment &amp; Maturity</h3>
                            <p>Plan timeline and expected returns</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="dash-metric-grid dash-metric-grid--3">
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-blue" aria-hidden="true"><i class="fa fa-calendar"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Investment Date</p>
                                    <p class="dash-metric-card-value is-date"><asp:Label ID="lblactivationdate" runat="server" Text="-" /></p>
                                </div>
                            </div>
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-emerald" aria-hidden="true"><i class="fa fa-inr"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Maturity Amount</p>
                                    <p class="dash-metric-card-value"><i class="fa fa-inr"></i> <asp:Label ID="lblmaturityamount" runat="server" Text="0.00" /></p>
                                </div>
                            </div>
                            <div class="dash-metric-card">
                                <span class="dash-metric-card-icon tone-gold" aria-hidden="true"><i class="fa fa-calendar-check-o"></i></span>
                                <div class="dash-metric-card-content">
                                    <p class="dash-metric-card-label">Maturity Date</p>
                                    <p class="dash-metric-card-value is-date"><asp:Label ID="lblmaturitydate" runat="server" Text="-" /></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="dash-subpage-panel">
                    <div class="dash-subpage-panel-body">
                        <div class="dash-income-status-row">
                            <div class="dash-highlight-card dash-highlight-card--compact">
                                <div class="dash-highlight-card-main">
                                    <span class="dash-highlight-card-icon" aria-hidden="true"><i class="fa fa-line-chart"></i></span>
                                    <div class="dash-highlight-card-text">
                                        <h4>Level Income</h4>
                                        <p>Your total level-wise earnings</p>
                                    </div>
                                    <div class="dash-highlight-card-amount"><i class="fa fa-inr"></i> <asp:Label ID="lbllevelincome" runat="server" Text="0" /></div>
                                </div>
                                <a href="LevelIncomeReport.aspx" class="dash-highlight-card-link"><i class="fa fa-external-link"></i> View Report</a>
                            </div>

                            <div class="dash-order-status-card">
                                <div class="dash-order-status-card-main">
                                    <span class="dash-order-status-card-icon" aria-hidden="true"><i class="fa fa-truck"></i></span>
                                    <div class="dash-order-status-card-text">
                                        <h4>Order Status</h4>
                                        <p>Delivery update for selected coupon</p>
                                        <span class="dash-order-status-orderid">Order: <asp:Label ID="lblorderid" runat="server" Text="-" /></span>
                                    </div>
                                    <asp:Label ID="lblorderdeliverystatus" runat="server" Text="-" CssClass="dash-order-status-badge" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

<%@ Page Title="MPremium Dashboard" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="PremiumDashboard.aspx.cs" Inherits="user_PremiumDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=22" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>MPremium Dashboard</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">MPremium Dashboard</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="profile-page dash-subpage dash-subpage--premium">
        <div class="profile-hero dash-subpage-hero dash-subpage-hero--premium">
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-diamond"></i></div>
            <div class="profile-hero-info">
                <h2>MPremium Dashboard</h2>
                <p class="profile-hero-meta">Monitor binary income, direct income, cashback wallet, and product wallet from one premium overview.</p>
            </div>
            <div class="profile-hero-actions">
                <a href="JoiningPackage.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-cube"></i> View Packages</a>
                <a href="PurchaseReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list-alt"></i> Purchase Report</a>
                <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Dashboard</a>
            </div>
        </div>

        <div class="dash-subpage-stack">
            <div class="dash-subpage-panel">
                <div class="dash-subpage-panel-head">
                    <span class="dash-subpage-panel-icon tone-gold" aria-hidden="true"><i class="fa fa-pie-chart"></i></span>
                    <div>
                        <h3>Income &amp; Wallet Overview</h3>
                        <p>Your premium earnings and wallet balances</p>
                    </div>
                </div>
                <div class="dash-subpage-panel-body">
                    <div class="dash-premium-grid">
                        <div class="dash-premium-metric tone-gold">
                            <span class="dash-premium-metric-icon" aria-hidden="true"><i class="fa fa-sitemap"></i></span>
                            <p class="dash-premium-metric-label">Binary Income</p>
                            <p class="dash-premium-metric-value"><i class="fa fa-inr"></i> <asp:Label ID="lblBinaryIncome" runat="server" Text="0" /></p>
                        </div>
                        <div class="dash-premium-metric tone-blue">
                            <span class="dash-premium-metric-icon" aria-hidden="true"><i class="fa fa-user-plus"></i></span>
                            <p class="dash-premium-metric-label">Direct Income</p>
                            <p class="dash-premium-metric-value"><i class="fa fa-inr"></i> <asp:Label ID="lblDirectIncome" runat="server" Text="0" /></p>
                        </div>
                        <div class="dash-premium-metric tone-green">
                            <span class="dash-premium-metric-icon" aria-hidden="true"><i class="fa fa-money"></i></span>
                            <p class="dash-premium-metric-label">Cashback Wallet</p>
                            <p class="dash-premium-metric-value"><i class="fa fa-inr"></i> <asp:Label ID="lblCashbackWallet" runat="server" Text="0" /></p>
                        </div>
                        <div class="dash-premium-metric tone-purple">
                            <span class="dash-premium-metric-icon" aria-hidden="true"><i class="fa fa-shopping-bag"></i></span>
                            <p class="dash-premium-metric-label">Product Wallet</p>
                            <p class="dash-premium-metric-value"><i class="fa fa-inr"></i> <asp:Label ID="lblProductWallet" runat="server" Text="0" /></p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="dash-subpage-panel">
                <div class="dash-subpage-panel-head">
                    <span class="dash-subpage-panel-icon tone-purple" aria-hidden="true"><i class="fa fa-bolt"></i></span>
                    <div>
                        <h3>Quick Actions</h3>
                        <p>Jump to packages and purchase history</p>
                    </div>
                </div>
                <div class="dash-subpage-panel-body">
                    <div class="dash-quick-links">
                        <a href="JoiningPackage.aspx" class="dash-quick-link">
                            <span class="dash-quick-link-icon" aria-hidden="true"><i class="fa fa-cube"></i></span>
                            <span class="dash-quick-link-text">
                                <strong>View Packages</strong>
                                <span>Explore and upgrade your MPremium plans</span>
                            </span>
                        </a>
                        <a href="PurchaseReport.aspx" class="dash-quick-link">
                            <span class="dash-quick-link-icon tone-green" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                            <span class="dash-quick-link-text">
                                <strong>Purchase Report</strong>
                                <span>Review your complete purchase history</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

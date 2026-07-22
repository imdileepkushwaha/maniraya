<%@ Page Title="User Wallet" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="UserWallet.aspx.cs" Inherits="UserWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=6" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>User Wallet</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home</a></li>
            <li><a href="#">My Wallet</a></li>
            <li class="active">User Wallet</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page wallet-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-credit-card"></i></div>
                    <div class="profile-hero-info">
                        <h2>User Wallet</h2>
                        <p class="profile-hero-meta">Overview of your wallet credits, debits, and available balance.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="TransactionReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-exchange-alt"></i> Transactions</a>
                        <a href="account_Ledger.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-book"></i> Account Ledger</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-2">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-money-bill-alt"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Main Wallet Balance Details</h3>
                                <p class="box-subtitle">Credited, debited, and current balance summary</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="row team-stat-grid wallet-stat-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-income">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-down"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Credited</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblCredited" runat="server" Text="0"></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-debit">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-up"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Debited</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblDebited" runat="server" Text="0"></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12">
                                <div class="team-stat-card team-stat-balance">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-rupee-sign"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Balance</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblCurrentWallet" runat="server" Text="0"></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-3">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-shopping-cart"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Shopping Wallet Balance Details</h3>
                                <p class="box-subtitle">Credited, debited, and current balance summary</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="row team-stat-grid wallet-stat-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-income">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-down"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Credited</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblCredited2" runat="server" Text="0"></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-debit">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-up"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Debited</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblDebited2" runat="server" Text="0"></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12">
                                <div class="team-stat-card team-stat-balance">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-rupee-sign"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Balance</p>
                                        <p class="team-stat-value">
                                            <i class="fa fa-rupee-sign"></i>
                                            <asp:Label ID="LblCurrentWallet2" runat="server" Text="0"></asp:Label></p>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

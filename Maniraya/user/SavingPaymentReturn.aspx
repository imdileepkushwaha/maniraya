<%@ Page Title="Saving Payment Status" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingPaymentReturn.aspx.cs" Inherits="user_SavingPaymentReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <style>
        .cf-return-card {
            max-width: 640px;
            margin: 0 auto;
            background: #fff;
            border: 1px solid #e8ecf1;
            border-radius: 18px;
            box-shadow: 0 12px 32px rgba(15, 23, 42, 0.06);
            overflow: hidden;
        }
        .cf-return-hero {
            padding: 28px 24px 20px;
            text-align: center;
        }
        .cf-return-hero.is-paid { background: linear-gradient(180deg, #ecfdf5 0%, #fff 70%); }
        .cf-return-hero.is-failed { background: linear-gradient(180deg, #fef2f2 0%, #fff 70%); }
        .cf-return-hero.is-pending { background: linear-gradient(180deg, #fffbeb 0%, #fff 70%); }
        .cf-return-icon {
            width: 68px;
            height: 68px;
            margin: 0 auto 14px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 28px;
        }
        .is-paid .cf-return-icon { background: #d1fae5; color: #047857; }
        .is-failed .cf-return-icon { background: #fee2e2; color: #b91c1c; }
        .is-pending .cf-return-icon { background: #fef3c7; color: #b45309; }
        .cf-return-hero h2 { margin: 0 0 8px; font-size: 1.5rem; color: #0f172a; }
        .cf-return-hero p { margin: 0; color: #64748b; line-height: 1.55; }
        .cf-return-meta {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 12px;
            padding: 0 24px 22px;
        }
        .cf-return-meta div {
            padding: 12px 14px;
            border-radius: 12px;
            background: #f8fafc;
            border: 1px solid #e8ecf1;
        }
        .cf-return-meta span {
            display: block;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.04em;
            text-transform: uppercase;
            color: #64748b;
            margin-bottom: 4px;
        }
        .cf-return-meta strong { color: #0f172a; font-size: 0.95rem; word-break: break-all; }
        .cf-return-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            justify-content: center;
            padding: 0 24px 24px;
        }
        @media (max-width: 575px) {
            .cf-return-meta { grid-template-columns: 1fr; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Payment Status</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="SavingProductPurchase.aspx">Saving Purchase</a></li>
            <li class="active">Payment Status</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="profile-page">
        <div class="cf-return-card">
            <div id="divReturnHero" runat="server" class="cf-return-hero is-pending">
                <div class="cf-return-icon"><i id="iReturnIcon" runat="server" class="fa fa-clock"></i></div>
                <h2><asp:Literal ID="litReturnTitle" runat="server" Text="Checking payment" /></h2>
                <p><asp:Literal ID="litReturnMessage" runat="server" Text="Please wait while we confirm your payment." /></p>
            </div>
            <div class="cf-return-meta">
                <div>
                    <span>Order ID</span>
                    <strong><asp:Literal ID="litOrderId" runat="server" Text="-" /></strong>
                </div>
                <div>
                    <span>Amount</span>
                    <strong><asp:Literal ID="litAmount" runat="server" Text="-" /></strong>
                </div>
                <div>
                    <span>Status</span>
                    <strong><asp:Literal ID="litStatus" runat="server" Text="-" /></strong>
                </div>
                <div>
                    <span>Payment ID</span>
                    <strong><asp:Literal ID="litPaymentId" runat="server" Text="-" /></strong>
                </div>
            </div>
            <div class="cf-return-actions">
                <a href="SavingProductOrderHistory.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-list"></i> Order History</a>
                <a href="SavingProductPurchase.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-undo"></i> Try Again</a>
                <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
            </div>
        </div>
    </div>
</asp:Content>

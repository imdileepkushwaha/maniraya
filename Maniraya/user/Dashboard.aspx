<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="user_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <meta property="og:title" content="Affiliate Link" />

    <meta property="og:url" content="http://arsenpay.in/user/Dashboard.aspx" />

    <style>
        .img-thumbnail {
            padding: 0;
        }

        .danger-table > tbody > tr > th {
            background: #f5d7d4;
            border: 1px solid #f5d7d4;
        }

        .danger-table > tbody > tr > td {
            border: 1px solid #e6e3e3;
        }

        .sucess-table > tbody > tr > th {
            background: #bce9bb;
            border: 1px solid #bce9bb;
        }

        .sucess-table > tbody > tr > td {
            border: 1px solid #e6e3e3;
        }

        .warning-table > tbody > tr > th {
            background: #f8ffbb;
            border: 1px solid #f8ffbb;
        }

        .warning-table > tbody > tr > td {
            border: 1px solid #e6e3e3;
        }


        .box.box-primary{
            padding: 0!important;
            background: transparent!important;
            border: none!important;
            box-shadow: none!important;
            border-radius: 0!important;
        }

        body.main-body.app .box.box-primary > .box-body{
            padding: 0!important;
            background: transparent!important;
            border: none!important;
            box-shadow: none!important;
            border-radius: 0!important;
        }
        body.main-body.app .box.box-primary, body.main-body.app.dark-mode .box.box-primary{
            padding: 0!important;
            background: transparent!important;
            border: none!important;
            box-shadow: none!important;
            border-radius: 0!important;
        }
    </style>



    <!--(Starts)Pasted from Recharge.aspx-->


    <script type="text/javascript">
        function theFunction(liElem, aElem) {

            document.getElementById("limobile").className = "";
            $('#tab1').removeClass('active');
            document.getElementById("lidth").className = "";
            $('#tab2').removeClass('active');
            document.getElementById("lilandline").className = "";
            $('#tab3').removeClass('active');
            document.getElementById("lielectricity").className = "";
            $('#tab4').removeClass('active');
            // document.getElementById("liSettings").className = "";
            // $('#settings').removeClass('active');
            document.getElementById("ligas").className = "";
            $('#tab5').removeClass('active');
            // alert(liElem);
            document.getElementById(liElem).className = "active";
            document.getElementById(aElem).className += " active";
        }




    </script>


    <style type="text/css">
        .nav-tabs {
            border-bottom: 2px solid #456f28;
            background: #456f28;
        }

            .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
                border-width: 0;
            }

            .nav-tabs > li > a {
                border: none;
                color: #ffffff;
                background: #456f28;
                padding: 10px 20px;
            }

                .nav-tabs > li.active > a, .nav-tabs > li > a:hover {
                    border: none;
                    color: #5a4080 !important;
                    background: #fff;
                }

                .nav-tabs > li > a::after {
                    content: "";
                    background: #5a4080;
                    height: 2px;
                    position: absolute;
                    width: 100%;
                    left: 0px;
                    bottom: -1px;
                    transition: all 250ms ease 0s;
                    transform: scale(0);
                }

            .nav-tabs > li.active > a::after, .nav-tabs > li:hover > a::after {
                transform: scale(1);
            }

        .tab-nav > li > a::after {
            background: #5a4080 none repeat scroll 0% 0%;
            color: #fff;
        }

        .tab-pane {
            padding: 15px 0;
        }

        .tab-content {
            padding: 20px;
        }

        .nav-tabs > li {
            width: auto;
            text-align: center;
        }

        .card {
            background: #FFF none repeat scroll 0% 0%;
            box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
            margin-bottom: 10px;
            margin-top: 0;
        }

        .form-horizontal .form-control {
            height: 44px;
        }

        @media all and (max-width:724px) {
            .nav-tabs > li > a > span {
                display: none;
            }

            .nav-tabs > li > a {
                padding: 5px 5px;
            }
        }

        .input-group {
            margin-bottom: 30px;
        }

        .list-inline > li {
            display: inline-block;
            width: 47%;
            padding: 6px 30px;
        }

        .dashboardbox {
            border: 1px solid #b9d4ec;
            border-bottom: none;
            padding: 17px 0;
            background-color: #f2f2f2;
            background-image: -webkit-gradient(linear, left top, left bottom, from(white), to(#f2f2f2));
            background-image: -webkit-linear-gradient(top, white, #f2f2f2);
            background-image: -moz-linear-gradient(top, white, #f2f2f2);
            background-image: -ms-linear-gradient(top, white, #f2f2f2);
            background-image: -o-linear-gradient(top, white, #f2f2f2);
            background-image: linear-gradient(top, white, #f2f2f2);
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            display: block;
            text-align: center;
            cursor: pointer;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -ms-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            transition: all 0.3s ease;
        }
    </style>

     
    <!--(Ends)-->
    <link href="../dist/css/user-profile.css" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=48" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <div Style="display: none">
        <asp:TextBox ID="txtflag" Style="display: none" runat="server"></asp:TextBox>
        <span id="LblNo" runat="server"></span>
        <asp:Label ID="lblwalletbalance123" runat="server" Text="Label" CssClass="label-success"></asp:Label>
        <asp:Label ID="lblUtilityBalance" runat="server" Text="Label" CssClass="label-success"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="row dash-legacy-welcome">
        <div class="ibox-title pull-left">
            <h1 class="pull-left" style="font-size:16px; padding-left:15px">Welcome</h1>
            <h5 class="pull-right">
                <asp:Label ID="lblusername" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lbluserid" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblmobile" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblrank" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>
    <div >
        <div >
            <div class="box box-primary dash-modern">
                <div class="box-header with-border">
                    <h3 class="box-title" style="width: 100%">
                        <marquee direction="left" onmouseover="stop();" onmouseout="start();"><asp:Literal ID="ltnews" runat="server"></asp:Literal></marquee>
                    </h3>
                </div>
                <div class="box-body">

                    <div class="dash-page-head">
                        <h1 class="dash-page-title">Dashboard</h1>
                        <div class="dash-page-actions">
                            <div class="dash-date-pill">
                                <i class="fa fa-calendar"></i>
                                <span id="dashDateRange"></span>
                            </div>
                            <button type="button" class="dash-icon-btn" onclick="location.reload();" title="Refresh">
                                <i class="fa fa-sync-alt"></i>
                            </button>
                            <a href="UserProfile.aspx" class="dash-icon-btn" title="Settings">
                                <i class="fa fa-sliders"></i>
                            </a>
                        </div>
                    </div>

                    <div class="dash-welcome-banner">
                        <div class="dash-welcome-text">
                            <h2>Welcome Back, <asp:Label ID="lblWelcomeName" runat="server" Text="Member" /></h2>
                            <p><asp:Literal ID="ltWelcomeNews" runat="server" Text="Stay updated with your latest network activity." /></p>
                            <span class="dash-welcome-meta">Member ID: <strong><asp:Label ID="lblWelcomeId" runat="server" Text="-" /></strong> &nbsp;|&nbsp; Rank: <strong><asp:Label ID="lblWelcomeRank" runat="server" Text="-" /></strong></span>
                        </div>
                        <div class="dash-welcome-actions">
                            <a href="DownlineReport.aspx" class="dash-btn dash-btn-primary">My Team</a>
                            <a href="UserWallet.aspx" class="dash-btn dash-btn-outline">My Wallet</a>
                        </div>
                    </div>

                    <div class="row dash-feature-row">
                        <div class="col-md-6">
                            <div class="dash-feature-card tone-saving">
                                <div class="dash-feature-head">
                                    <div class="dash-feature-title">
                                        <span class="dash-feature-icon" aria-hidden="true"><i class="fa fa-university"></i></span>
                                        <div>
                                            <h3>Saving Dashboard</h3>
                                            <p>Your savings products &amp; balance overview</p>
                                        </div>
                                    </div>
                                    <a href="SavingDashboard.aspx" class="dash-btn dash-feature-toggle">
                                        View <i class="fa fa-arrow-right"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="dash-feature-card tone-premium">
                                <div class="dash-feature-head">
                                    <div class="dash-feature-title">
                                        <span class="dash-feature-icon" aria-hidden="true"><i class="fa fa-gem"></i></span>
                                        <div>
                                            <h3>MPremium Dashboard</h3>
                                            <p>Your premium plans &amp; package overview</p>
                                        </div>
                                    </div>
                                    <a href="PremiumDashboard.aspx" class="dash-btn dash-feature-toggle">
                                        View <i class="fa fa-arrow-right"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnlCouponCards" runat="server" Visible="false" CssClass="dash-coupon-section">
                        <div class="dash-section-head dash-section-head--member">
                            <div class="dash-section-head-main">
                                <span class="dash-section-head-icon" aria-hidden="true"><i class="fa fa-ticket-alt"></i></span>
                                <div class="dash-section-head-copy">
                                    <h3 class="dash-section-title">My Coupons</h3>
                                    <p class="dash-section-sub">Coupon-wise paid &amp; unpaid installment summary</p>
                                </div>
                            </div>
                            <span class="dash-section-head-tag">Saving</span>
                        </div>
                        <div class="row dash-coupon-grid">
                            <asp:Repeater ID="rptCouponCards" runat="server">
                                <ItemTemplate>
                                    <div class="col-sm-12 col-md-6 col-xl-4">
                                        <div class="dash-coupon-card">
                                            <div class="dash-coupon-card-top">
                                                <span class="dash-coupon-card-icon" aria-hidden="true"><i class="fa fa-barcode"></i></span>
                                                <div class="dash-coupon-card-code-wrap">
                                                    <span class="dash-coupon-card-label">Coupon Code</span>
                                                    <strong class="dash-coupon-card-code"><%# Eval("CouponCode") %></strong>
                                                </div>
                                            </div>
                                            <div class="dash-coupon-card-stats">
                                                <div class="dash-coupon-stat is-paid">
                                                    <span class="dash-coupon-stat-label">Paid Installment</span>
                                                    <strong class="dash-coupon-stat-value"><%# Eval("PaidCount") %></strong>
                                                </div>
                                                <div class="dash-coupon-stat is-unpaid">
                                                    <span class="dash-coupon-stat-label">Unpaid Installment</span>
                                                    <strong class="dash-coupon-stat-value"><%# Eval("UnpaidCount") %></strong>
                                                </div>
                                                <div class="dash-coupon-stat is-current">
                                                    <span class="dash-coupon-stat-label">This Month Pending</span>
                                                    <strong class="dash-coupon-stat-value"><%# Eval("CurrentMonthPending") %></strong>
                                                </div>
                                            </div>
                                            <a href='<%# GetCouponReportUrl(Eval("CouponCode")) %>' class="dash-coupon-card-link">
                                                More Info <i class="fa fa-arrow-right" aria-hidden="true"></i>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>

                    <div class="dash-incentive-referral-stack">
                        <div class="dash-incentive-card dash-incentive-card--full" id="dashIncentiveCard">
                            <div class="dash-incentive-logo-bar">
                                <!-- <img src="img/logo.png" alt="Maniraya" class="dash-incentive-logo" /> -->
                                <span class="dash-incentive-brand">MPremium</span>
                            </div>

                            <div class="dash-incentive-layout">
                                <div class="dash-incentive-profile-side">
                                    <div class="dash-incentive-profile">
                                        <div class="dash-incentive-details">
                                            <p class="dash-incentive-period"><asp:Label ID="lblIncentivePeriod" runat="server" Text="Your 30 days incentive" /></p>
                                            <div class="dash-incentive-detail-row">
                                                <span class="dash-incentive-label">Distributor</span>
                                                <strong><asp:Label ID="lblIncentiveName" runat="server" Text="-" /></strong>
                                            </div>
                                            <div class="dash-incentive-detail-row">
                                                <span class="dash-incentive-label">State</span>
                                                <strong><asp:Label ID="lblIncentiveState" runat="server" Text="-" /></strong>
                                            </div>
                                            <div class="dash-incentive-detail-row">
                                                <span class="dash-incentive-label">District</span>
                                                <strong><asp:Label ID="lblIncentiveDistrict" runat="server" Text="-" /></strong>
                                            </div>
                                            <div class="dash-incentive-detail-row">
                                                <span class="dash-incentive-label">PAN</span>
                                                <strong><asp:Label ID="lblIncentivePan" runat="server" Text="-" /></strong>
                                            </div>
                                        </div>
                                        <div class="dash-incentive-photo-wrap">
                                            <div class="dash-incentive-photo-frame">
                                                <asp:Image ID="imgIncentivePhoto" runat="server" CssClass="dash-incentive-photo" ImageUrl="img/default.png" AlternateText="Profile photo" />
                                            </div>
                                            <div class="dash-incentive-updated" aria-live="polite">
                                                <span class="dash-incentive-updated-icon" aria-hidden="true"><i class="fa fa-clock"></i></span>
                                                <span class="dash-incentive-updated-label">Last Updated</span>
                                                <strong class="dash-incentive-updated-time"><asp:Label ID="lblIncentiveUpdated" runat="server" Text="" /></strong>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dash-incentive-actions">
                                        <button type="button" class="dash-incentive-action-btn is-print" onclick="printIncentiveCard();" title="Print incentive summary">
                                            <i class="fa fa-print" aria-hidden="true"></i>
                                            <span>Print</span>
                                        </button>
                                        <button type="button" class="dash-incentive-action-btn is-share" onclick="shareIncentiveCard();" title="Share incentive summary">
                                            <i class="fa fa-share-alt" aria-hidden="true"></i>
                                            <span>Share</span>
                                        </button>
                                    </div>
                                </div>

                                <div class="dash-incentive-main-side">
                                    <div class="dash-incentive-tabs" role="tablist" aria-label="Incentive period">
                                        <asp:LinkButton ID="lnkIncentive1Day" runat="server" CssClass="dash-incentive-tab is-1day" CommandArgument="1" OnClick="IncentiveTab_Click">
                                            <span class="dash-incentive-tab-num">24</span><span class="dash-incentive-tab-text">Hrs.</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkIncentive10Day" runat="server" CssClass="dash-incentive-tab is-10day" CommandArgument="10" OnClick="IncentiveTab_Click">
                                            <span class="dash-incentive-tab-num">10</span><span class="dash-incentive-tab-text">Days</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkIncentive30Day" runat="server" CssClass="dash-incentive-tab is-30day is-active" CommandArgument="30" OnClick="IncentiveTab_Click">
                                            <span class="dash-incentive-tab-num">30</span><span class="dash-incentive-tab-text">Days</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkIncentiveTillDate" runat="server" CssClass="dash-incentive-tab is-tilldate" CommandArgument="0" OnClick="IncentiveTab_Click">
                                            <span class="dash-incentive-tab-num"><i class="fa fa-infinity"></i></span><span class="dash-incentive-tab-text">Till Date</span>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="dash-incentive-income">
                                        <div class="dash-incentive-income-head">
                                            <span>Income Type</span>
                                            <span>Amount</span>
                                        </div>
                                        <div class="dash-incentive-income-row">
                                            <span>Saving Direct Income</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblSavingDirectIncome" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-row">
                                            <span>Level Income</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblLevelIncomeCard" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-row">
                                            <span>MPremium Direct Income</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblPremiumDirectIncome" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-row">
                                            <span>Team Bonus</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblMatchingIncomeCard" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-row">
                                            <span>Self Business Bonus</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblCashBackIncome" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-row is-wallet">
                                            <span>Product Wallet Balance</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblProductWalletBalance" runat="server" Text="0.00" /></strong>
                                        </div>
                                        <div class="dash-incentive-income-total">
                                            <span>Total Incentive</span>
                                            <strong><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblIncentiveTotal" runat="server" Text="0.00" /></strong>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="dvlink" runat="server" visible="True" class="dash-referral-section">
                            <div class="dash-referral-head">
                                <div class="dash-referral-head-main">
                                    <span class="dash-referral-head-icon" aria-hidden="true"><i class="fa fa-share-alt"></i></span>
                                    <div>
                                        <h3 class="dash-referral-title">Referral Links</h3>
                                        <p class="dash-referral-sub">Share your personal link and grow your left &amp; right team</p>
                                    </div>
                                </div>
                                <span class="dash-referral-id-pill">
                                    <i class="fa fa-id-badge"></i>
                                    Your ID: <strong><asp:Label ID="lblReferralUserId" runat="server" Text="-" /></strong>
                                </span>
                            </div>

                            <div class="dash-referral-grid">
                                <div class="dash-referral-card is-left">
                                    <div class="dash-referral-card-top">
                                        <span class="dash-referral-card-badge">Join Team</span>
                                        <span class="dash-referral-card-icon" aria-hidden="true"><i class="fa fa-arrow-left"></i></span>
                                    </div>
                                    <p class="dash-referral-card-text">New members joining from this link will be placed on your <strong>left</strong> side.</p>
                                    <div class="dash-referral-field">
                                        <asp:TextBox ID="TxtLeftLinkLink" runat="server" CssClass="form-control dash-referral-input" />
                                        <button type="button" class="dash-referral-copy-btn" onclick="copyReferralLink('<%=TxtLeftLinkLink.ClientID%>', this);" title="Copy link">
                                            <i class="fa fa-copy"></i>
                                            <span>Copy</span>
                                        </button>
                                    </div>
                                    <div class="dash-referral-actions">
                                        <button type="button" class="dash-referral-share-btn is-whatsapp" onclick="shareReferralWhatsApp('<%=TxtLeftLinkLink.ClientID%>');">
                                            <i class="fa fa-whatsapp"></i> Share on WhatsApp
                                        </button>
                                    </div>
                                </div>

                                <div class="dash-referral-card is-right" style="display:none">
                                    <div class="dash-referral-card-top">
                                        <span class="dash-referral-card-badge">Right Team</span>
                                        <span class="dash-referral-card-icon" aria-hidden="true"><i class="fa fa-arrow-right"></i></span>
                                    </div>
                                    <p class="dash-referral-card-text">New members joining from this link will be placed on your <strong>right</strong> side.</p>
                                    <div class="dash-referral-field">
                                        <asp:TextBox ID="TxtRightLink" runat="server" CssClass="form-control dash-referral-input" />
                                        <button type="button" class="dash-referral-copy-btn" onclick="copyReferralLink('<%=TxtRightLink.ClientID%>', this);" title="Copy link">
                                            <i class="fa fa-copy"></i>
                                            <span>Copy</span>
                                        </button>
                                    </div>
                                    <div class="dash-referral-actions">
                                        <button type="button" class="dash-referral-share-btn is-whatsapp" onclick="shareReferralWhatsApp('<%=TxtRightLink.ClientID%>');">
                                            <i class="fa fa-whatsapp"></i> Share on WhatsApp
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="dash-referral-tip">
                                <i class="fa fa-lightbulb-o" aria-hidden="true"></i>
                                <span>Tip: Share the correct link for the team side you want to build. Members register using your ID automatically.</span>
                            </div>
                        </div>
                    </div>

                    <div class="row dash-stats-grid">
                       
                        <div class="col-sm-6 col-xl-3" style="display:none">
                            <div class="card dash-income-card dash-income-compact tone-gold">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-user-plus"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Direct Members</p>
                                    <h3 class="dash-income-value is-number"><asp:Label ID="lblStatDirect" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--up">Direct referrals</span>
                                        <a href="TreeView.aspx" class="dash-income-meta-link">View tree</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-xl-3" style="display:none">
                            <div class="card dash-income-card dash-income-compact tone-teal">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-wallet"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Wallet Balance</p>
                                    <h3 class="dash-income-value"><asp:Label ID="lblStatWallet" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--muted">Available balance</span>
                                        <a href="UserWallet.aspx" class="dash-income-meta-link">View wallet</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="dash-tools-panel" style="display:none">
                        <asp:Panel ID="pnlnotification" runat="server" CssClass="dash-tools-alert-wrap">
                            <div class="dash-tools-alert" role="alert">
                                <span class="dash-tools-alert-icon" aria-hidden="true"><i class="fa fa-exclamation-triangle"></i></span>
                                <div class="dash-tools-alert-body">
                                    <strong>Action required</strong>
                                    <p>Please update your bank details to receive payouts on time.</p>
                                </div>
                                <a href="UserEdit.aspx" class="dash-tools-alert-action">Update now</a>
                            </div>
                        </asp:Panel>

                        <div class="dash-tools-grid">
                            <div class="dash-tools-news-card">
                                <div class="dash-tools-news-head">
                                    <span class="dash-tools-news-icon" aria-hidden="true"><i class="fa fa-bullhorn"></i></span>
                                    <div class="dash-tools-news-meta">
                                        <div class="dash-tools-news-meta-top">
                                            <span class="dash-tools-news-title">Latest Updates</span>
                                            <span class="dash-tools-live-badge"><span class="dash-tools-live-dot"></span> Live</span>
                                        </div>
                                        <span class="dash-tools-news-sub">Company announcements and important notices</span>
                                    </div>
                                </div>
                                <div class="dash-news-bar">
                                    <div class="dash-news-bar-viewport">
                                        <marquee class="dash-news-marquee" direction="left" scrollamount="4" onmouseover="this.stop();" onmouseout="this.start();">
                                            <asp:Literal ID="ltnewsTicker" runat="server"></asp:Literal>
                                        </marquee>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Button ID="Button1" runat="server" Style="display:none;" />
                        <asp:Button ID="Button2" runat="server" Style="display:none;" />
                    </div>

                    <div class="dash-section-head dash-section-head--income" style="display:none">
                        <div class="dash-section-head-main">
                            <span class="dash-section-head-icon" aria-hidden="true"><i class="fa fa-chart-line"></i></span>
                            <div class="dash-section-head-copy">
                                <h3 class="dash-section-title">Income Overview</h3>
                                <p class="dash-section-sub">Track all your earnings in one place</p>
                            </div>
                        </div>
                        <span class="dash-section-head-tag">Earnings</span>
                    </div>
                    <div class="row dash-income-grid" style="display:none">

                              <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-red">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-money-bill-alt"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Saving Income</p>
								<h3 class="dash-income-value"><asp:Label ID="lblselfincome" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag">Total</span>
									<a href="#" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>

                                      
                              
                                        <div class="col-sm-12 col-lg-6 col-xl-4" STyle="display:none">
						<div class="card"  style="background: linear-gradient(110deg, #FFA500 60%,#FFC55C 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-indian-rupee-sign text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Matching Income</p><br>
										<div class="" style="display:none">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><i class="fa fa-rupee-sign"></i> &nbsp <asp:Label ID="lblDirddectincome" runat="server" Text="00.00" Visible="false"></asp:Label></h3>
										</div>
                                        <div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><i class="fa fa-rupee-sign"></i> &nbsp <asp:Label ID="lblMatching11" runat="server" Text="00.00" Visible="false"></asp:Label>
												<asp:Label ID="lblMatchinggg" runat="server" Text="00.00" ></asp:Label>
											</h3>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0 text-white">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-white" aria-hidden="true"></i><a href="Dailypayoutdetail.aspx">More Info</a>
								</p>
							</div>
						</div>
					</div>

  <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-handshake"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Matching Income</p>
								<h3 class="dash-income-value" style="display:none"><asp:Label ID="lblDIrectorIncomeaa" runat="server" Text="Label"></asp:Label></h3>
								<h3 class="dash-income-value"><asp:Label ID="lblMatching" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag">Total</span>
									<a href="dailypayoutdetail.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>

                               <div class="col-sm-12 col-lg-6 col-xl-4" >
						<div class="card dash-income-card dash-income-compact tone-pink">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-gift"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Direct Income</p>
								<h3 class="dash-income-value" style="display:none"><asp:Label ID="lbldailyincome" runat="server" Text="0.00"></asp:Label></h3>
								<h3 class="dash-income-value"><asp:Label ID="lblDirectincome" runat="server" Text="0.00"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag">Total</span>
									<a href="DirectIncomeReport.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>
                              <div class="col-sm-12 col-lg-6 col-xl-4" >
						<div class="card dash-income-card dash-income-compact tone-orange">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-graduation-cap"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Cashback Wallet</p>
								<h3 class="dash-income-value"><asp:Label ID="lblgoldirector" runat="server" Text="0"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag">Total</span>
									<a href="BonusIncomeReport.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>

                              	<div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-purple">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-book"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Purchase Wallet</p>
								<h3 class="dash-income-value"><asp:Label ID="lblleadership" runat="server" Text="0.00"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag">Total</span>
									<a href="BonusIncomeReport.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>

                              	<div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-green">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-wallet"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Net Income</p>
								<h3 class="dash-income-value"><asp:Label ID="LblCurrentWallet" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--up">Balance</span>
									<a href="TransactionReport.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>
                               <div class="col-sm-12 col-lg-6 col-xl-4"  style="display:none" >
						<div class="card" style="background: linear-gradient(110deg, #FFA500 60%,#FFC55C 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-indian-rupee-sign text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Diamond Director Income</p><br>
										<div class="">
											<h3 class="font-weight-semibold tex
                                                t-left mb-0 text-success" ><asp:Label ID="Label6" runat="server" Text="0.00" ></asp:Label>
                                              
											</h3>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a>
								</p>
							</div>
						</div>
					</div>

                               <div class="col-sm-12 col-lg-6 col-xl-4"  style="display:none">
						<div class="card" style="background: linear-gradient(110deg, #FFA500 60%,#FFC55C 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-indian-rupee-sign text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Crown  Director Income</p><br>
										<div class="">
											<h3 class="font-weight-semibold tex
                                                t-left mb-0 text-success" ><asp:Label ID="Label7" runat="server" Text="0.00" ></asp:Label>
                                              
											</h3>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a>
								</p>
							</div>
						</div>
					</div>
                      <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa-solid fa-users text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Rank</p><br>
										<div class="">
											<h2 class="font-weight-semibold text-left mb-0 text-success"><asp:Label ID="sdsdff" runat="server" Text="Label"></asp:Label></h2></h2>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a>
								</p>
							</div>
						</div>
					</div>

                 	
					

                            
                             
                         <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-indian-rupee-sign text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Repurchase Income</p><br>
										<div class="">
                                          
											<h3 class="font-weight-semibold text-left mb-0 text-success" ><asp:Label ID="LblREpurchaseIncome" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a>
								</p>
							</div>
						</div>
					</div>

                        </div>

                    <div class="dash-section-head dash-section-head--member dash-section-head--spaced">
                        <div class="dash-section-head-main">
                            <span class="dash-section-head-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
                            <div class="dash-section-head-copy">
                                <h3 class="dash-section-title">Member Overview</h3>
                                <p class="dash-section-sub">Rank, team &amp; volume snapshot</p>
                            </div>
                        </div>
                        <span class="dash-section-head-tag">Team</span>
                    </div>
                    <div class="row dash-metrics-grid">

                             <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card" style="background: linear-gradient(110deg, #000075 60%,#0000D1 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">

										<i class="fa fa-indian-rupee-sign text-white icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">CREDITED</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><asp:Label ID="LblCredited" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div>
								<%--<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> More Info
								</p>--%>
							</div>
						</div>
					</div>

                    <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card" style="background: linear-gradient(110deg, #964B00 60%,#C46200 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-indian-rupee-sign text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">DEBIT</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><asp:Label ID="LblDebited" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div>
								<%--<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-danger" aria-hidden="true"></i>  More Info
								</p>--%>
							</div>
						</div>
					</div>
			
                                <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-gold">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="ti-crown"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Rank</p>
								<h3 class="dash-income-value is-text"><asp:Label ID="lblrank1" runat="server" Text="0.00"></asp:Label><asp:Label ID="lblrankreward" runat="server" Text="0.00" Visible="false"></asp:Label></h3>
								<div class="dash-income-meta">
									<a href="RankReport.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>

                        <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-orange">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="ti-crown"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Category</p>
								<h3 class="dash-income-value is-text"><asp:Label ID="lblrank2" runat="server" Text="Category Name"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Member tier</span>
								</div>
							</div>
						</div>
					</div>

                        		<div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-green">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-check"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Status</p>
								<h3 class="dash-income-value is-text is-sm"><asp:Label ID="lblstatus" runat="server" Text="Label"></asp:Label><span class="dash-income-value-sep">|</span><asp:Label ID="Lblactivatedate2" runat="server" Text="01/07/2024"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--up">Account</span>
								</div>
							</div>
						</div>
					</div>

                     

                            <div class="col-sm-12 col-lg-6 col-xl-4" style="Display:none">
						<div class="card" style="background: linear-gradient(110deg,#075264 60%,#9F92FF 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-users text-white icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">AutoPool Team</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><asp:Label ID="LblPooldownline" runat="server" Text="0"></asp:Label></h3>
										</div>
									</div>
								</div><br>
                                <p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-white" aria-hidden="true"></i><a href="PoolBinaryReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
			
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-rupee-sign text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Current Package</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="LblCurrentpackage" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<br />
								</p>
							</div>
						</div>
					</div>

                        	<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">

										<i class="fa-solid fa-users text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Today Performance Pair</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="lblPerformance" runat="server" Text="Label" ></asp:Label></h3>
										</div>
									</div>
								</div><br /><br /><br />
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i><a href="Dailypayoutdetail.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
                       

                    
						<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">

										<i class="fa-solid fa-users text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Total Team</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LblssDownline" runat="server" Text="Label" ></asp:Label></h3>
										</div>
									</div>
								</div><br />
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i><a href="DownlineReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
					<div class="col-sm-12 col-lg-6 col-xl-4"  style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa-solid fa-users text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Total Active Team</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="LblActssiveDownline" runat="server" Text="Label" ></asp:Label></h3>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> <a href="DownlineReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
				</div>

                      <div class="row dash-metrics-grid">
					<div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-indigo">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Total Direct</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblDirect" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<a href="UserDirectAssociates.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>
                          <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-purple">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-check-circle"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Active Direct</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblActiveDirect" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<a href="UserDirectAssociates.aspx" class="dash-income-meta-link">View report</a>
								</div>
							</div>
						</div>
					</div>
                           <div class="col-sm-12 col-lg-6 col-xl-4">
                            <div class="card dash-income-card dash-income-compact tone-indigo">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Total Team</p>
                                    <h3 class="dash-income-value is-number"><asp:Label ID="LblDownline" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--muted">Network members</span>
                                        <a href="DownlineReport.aspx" class="dash-income-meta-link">View report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-lg-6 col-xl-4">
                            <div class="card dash-income-card dash-income-compact tone-green">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-briefcase"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Active Team</p>
                                    <h3 class="dash-income-value is-number"><asp:Label ID="LblActiveDownline" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--up">Active members</span>
                                        <a href="DownlineReport.aspx" class="dash-income-meta-link">View report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-sm-12 col-lg-6 col-xl-4">
                            <asp:Panel ID="pnlDirectRank" runat="server" Visible="true" CssClass="card dash-income-card dash-income-compact tone-gold">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-trophy"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">My Direct Rank</p>
                                    <h3 class="dash-income-value is-text"><asp:Label ID="lblDirectRankName" runat="server" Text="Member" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--muted">Active: <asp:Label ID="lblDirectRankActiveCount" runat="server" Text="0" /></span>
                                        <span class="dash-income-tag dash-income-tag--up"><asp:Label ID="lblDirectRankNext" runat="server" Text="-" /></span>
                                        <a href="DirectRankReport.aspx" class="dash-income-meta-link">Rank progress</a>
                                    </div>
                                </div>
                            </asp:Panel>
					</div>

                        <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Total Left Team</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblTotalLeft" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<!-- <span class="dash-income-tag dash-income-tag--down">Inactive <asp:Label ID="LblInactiveleft" runat="server" Text="Label"></asp:Label></span> -->
									<asp:Label ID="Lblactiveleft" runat="server" Text="Label" Visible="false"></asp:Label>
									<asp:Button ID="Button3" runat="server" Text="Refresh" CssClass="dash-metric-refresh" OnClick="Button3_Click" />
								</div>
							</div>
						</div>
					</div>

                          <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Total Right Team</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblTotalright" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<!-- <span class="dash-income-tag dash-income-tag--down">Inactive <asp:Label ID="LblInActiveRight" runat="server" Text="Label"></asp:Label></span> -->
									<asp:Label ID="LblActiveRight" runat="server" Text="Label" Visible="false"></asp:Label>
								</div>
							</div>
						</div>
					</div>

                                     <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-certificate"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Left SV (Bonanza 26 Sept)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblleftBonanzasv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Bonanza volume</span>
								</div>
							</div>
						</div>
					</div>

                            <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-certificate"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Right SV (Bonanza 26 Sept)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblRightBonanzasv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Bonanza volume</span>
								</div>
							</div>
						</div>
					</div>

                            <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card" style="background: linear-gradient(110deg,#075264 60%,#9F92FF 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
									  <i class="fa fa-certificate text-white"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left"> LEFT SV </p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-white"> <asp:Label ID="LblRleftbv" runat="server" Text="Label"></asp:Label></h4>
										</div>
									</div>
								</div>
								
                                 <hr class="hr-white">
                                <p class="mb-0 text-white">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-danger" aria-hidden="true"></i>  Current Left SV
								  <asp:Label ID="lblcurrentleftbv" runat="server" Text="Label"></asp:Label></p>
							</div>
						</div>
					</div>


                                       <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card" style="background: linear-gradient(110deg,#075264 60%,#9F92FF 60%);">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
                                        <i class="fa fa-certificate text-white"></i>
									
									</div>
									<div class="float-left">
										<p class="mb-0 text-left"> Right SV </p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-white"> <asp:Label ID="LblRrightbv" runat="server" Text="Label"></asp:Label> </h4>
										</div>
									</div>
								</div>
								
                                 <hr class="hr-white">
                                <p class="mb-0 text-white">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-danger" aria-hidden="true"></i>  Current Right SV
								  <asp:Label ID="lblcurrrentightbv" runat="server" Text="Label"></asp:Label></p>
							</div>
						</div>
					</div>

                      
                      
                         
                                          <div class="col-sm-12 col-lg-6 col-xl-4 " style="display:none">
                                        <div class="card mb-0 gradient-10 gradient-10-shadow border1px-white card-summary gradient-10-hover">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-bubbles"></i>
                                                    </div>
                                                    <div class="text-right">
                                                         <h2 class="card-summary__price"> <asp:Label ID="Lblleftbv" runat="server" Text="Label"></asp:Label></h2>
                                                        <p class="card-summary__title"> Total Left SV </p>
                                                    </div>
                                                </div>
                                             <br />
                                               	<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i>   Carry PV
								<asp:Label ID="Lblleftcarrypv" runat="server" Text="Label"></asp:Label></p>
                                            </div>
                                          
							
                                        </div>
                                    </div>
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-rupee-sign text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Total Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="lblTotalincome" runat="server" Text="Label" ></asp:Label> <i class="fa fa-rupee-sign"></i> </h3>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> <a href="UserDirectAssociates.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
                               <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-users text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Left Directs</p><br>
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LblLeftDirect" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div><br><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a> 
								</p>
							</div>
						</div>
					</div>
					<div class="col-sm-12 col-lg-6 col-xl-4"  style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">

										<i class="fa fa-users text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Right Directs</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LblRightDirect" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div><br /><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-down-drop-circle mr-1 text-secondary" aria-hidden="true"></i><a href="#">More Info</a>
								</p>
							</div>
						</div>
					</div>
                          	<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa-solid fa-wallet text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Cash Wallet</p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-success">   <asp:Label ID="Totalbalance" runat="server" Text="Label"></asp:Label> <i class="fa fa-rupee-sign"></i> </h4>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<br />
								</p>
							</div>
						</div>
					</div>
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-rupee-sign text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Cash Back Income</p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LbllevelROiIncome" runat="server" Text="0" ></asp:Label> <i class="fa fa-rupee-sign"></i> </h4>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> <a href="levelincomereport.aspx">More Info</a>
								</p>
							</div>
						</div>
					</div>
                          </div>
                        <div class="row dash-metrics-grid" style="display:none">

                             <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-certificate"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Left SV (First Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblleftjoiningsv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Current Left SV</span>
									<span class="dash-income-meta-text"><asp:Label ID="lblleftjoiningcarrysv" runat="server" Text="Label"></asp:Label></span>
								</div>
							</div>
						</div>
					</div>

                            <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-certificate"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Right SV (First Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblrightjoiningsv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Current Right SV</span>
									<span class="dash-income-meta-text"><asp:Label ID="lblrightjoiningcarrysv" runat="server" Text="Label"></asp:Label></span>
								</div>
							</div>
						</div>
					</div>

                                       <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-blue">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-certificate"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Self SV (First Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lbltotalselfjoiningsv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Personal volume</span>
								</div>
							</div>
						</div>
					</div>

                         </div>
                      

                              <div class="row dash-metrics-grid">

                        <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-user"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Myself BV</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblMyselfBV" runat="server" Text="0"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Self repurchase volume</span>
								</div>
							</div>
						</div>
					</div>

                        <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-indigo">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Team Repurchase BV</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblTeamRepurchaseBV" runat="server" Text="0"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Team repurchase volume</span>
								</div>
							</div>
						</div>
					</div>

                        <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-green">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-rupee-sign"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Repurchase Income</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblRepurchaseIncomeBV" runat="server" Text="0"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Repurchase income</span>
								</div>
							</div>
						</div>
					</div>

                             <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-sync-alt"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Left SV (Re-Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblleftrepurchasesv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Current Left SV</span>
									<span class="dash-income-meta-text"><asp:Label ID="lblleftrepurchasecarrysv" runat="server" Text="Label"></asp:Label></span>
								</div>
							</div>
						</div>
					</div>

                            <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-sync-alt"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Right SV (Re-Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lblRightrepurchasesv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Current Right SV</span>
									<span class="dash-income-meta-text"><asp:Label ID="lblRightrepurchasecarrysv" runat="server" Text="Label"></asp:Label></span>
								</div>
							</div>
						</div>
					</div>

                                       <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-sync-alt"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Self SV (Re-Purchase)</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="lbltotalselfRepurchasesv" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--muted">Personal volume</span>
								</div>
							</div>
						</div>
					</div>

                         </div>

                                            <div class="row">

                  




                

                         </div>
                    <div class="row" style="display:none">
                    
					
					
					



                        </div>
                    
                    <div class="row">

                   
         

                                        <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
                                        <div class="card mb-0 gradient-2 gradient-2-shadow border1px-white card-summary gradient-2-hover">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-cloud-download"></i>
                                                    </div>
                                                    <div class="text-right">

                                                         <h2 class="card-summary__price"> <asp:Label ID="Lblrightbv" runat="server" Text="Label"></asp:Label> </h2>
                                                        <p class="card-summary__title"> RIGHT PV </p>
                                                    </div>
                                                </div>
                                              
                                                 <br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i>  Left Carry PV
								<asp:Label ID="Lblrightcarrypv" runat="server" Text="Label"></asp:Label></p>
                                            </div>
                                        </div>
                                    </div>
                             
                  


         

                        </div>
                    
                          <div class="row">
                      
                                <div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
                                        <div class="card mb-0 gradient-2 gradient-2-shadow border1px-white card-summary gradient-2-hover">
                                            <div class="card-body" style="background: linear-gradient(110deg,#A36A00 60%,#D18700 60%);">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-cloud-download"></i>
                                                    </div>
                                                    <div class="text-right">

                                                         <h2 class="card-summary__price"> <asp:Label ID="LblRetailProfit" runat="server" Text="Label"></asp:Label> </h2>
                                                        <p class="card-summary__title">Self BV </p>
                                                    </div>
                                                </div>
                                                <hr class="hr-white">
                                               
                                            </div>
                                        </div>
                                    </div>

                    </div>
                    
                 <div class="row" style="display:none">
                             <div class="col-sm-12 col-lg-6 col-xl-4">
                                        <div class="card mb-0 gradient-10 gradient-10-shadow border1px-white card-summary gradient-10-hover">
                                            <div class="card-body" style="background: linear-gradient(110deg,#A36A00 60%,#D18700 60%);">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-bubbles"></i>
                                                    </div>
                                                    <div class="text-right">
                                                         <h2 class="card-summary__price"></h2>
                                                        <p class="card-summary__title"> CURRENT LEFT BV </p>
                                                    </div>
                                                </div>
                                                <hr class="hr-white">
                                               
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-sm-12 col-lg-6 col-xl-4">
                                        <div class="card mb-0 gradient-2 gradient-2-shadow border1px-white card-summary gradient-2-hover">
                                            <div class="card-body" style="background: linear-gradient(110deg,#A36A00 60%,#D18700 60%);">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-cloud-download"></i>
                                                    </div>
                                                    <div class="text-right">

                                                         <h2 class="card-summary__price">  </h2>
                                                        <p class="card-summary__title">CURRENT RIGHT BV </p>
                                                    </div>
                                                </div>
                                                <hr class="hr-white">
                                               
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-sm-12 col-lg-6 col-xl-4">
                                        <div class="card mb-0 gradient-2 gradient-2-shadow border1px-white card-summary gradient-2-hover">
                                            <div class="card-body" style="background: linear-gradient(110deg,#A36A00 60%,#D18700 60%);">
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <div class="card-summary__icon">
                                                        <i class="icon-cloud-download"></i>
                                                    </div>
                                                    <div class="text-right">

                                                         <h2 class="card-summary__price"> <asp:Label ID="lblcurrentselfbv" runat="server" Text="Label"></asp:Label> </h2>
                                                        <p class="card-summary__title">CURRENT Self BV </p>
                                                    </div>
                                                </div>
                                                <hr class="hr-white">
                                               
                                            </div>
                                        </div>
                                    </div>


                                  



                    </div>

                    <br />
                                      <div class="row">
				
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-rupee-sign text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Autopool Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <i class="fa fa-rupee-sign"></i> <asp:Label ID="LblPoolIncome" runat="server" Text="Label"></asp:Label>  </h3>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> <a href="AutoPoolIncomeReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa fa-rupee-sign text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Level Income</p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="lbl878" runat="server" Text="0" ></asp:Label> <i class="fa fa-rupee-sign"></i> </h4>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-white" aria-hidden="true"></i> <a href="LevelIncomeReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>

             </div>

                      <div class="row">
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">

										<i class="fa fa-rupee-sign text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Group Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LBlGroupIncome" runat="server" Text="Label"></asp:Label> <i class="fa fa-rupee-sign"></i> </h3>
										</div>
									</div>
								</div><br />
								<p class="text-muted mb-0">
									<i class="mdi mdi-arrow-up-drop-circle mr-1 text-success" aria-hidden="true"></i> <a href="LuckyDrawClosingReport.aspx">More Info</a> 
								</p>
							</div>
						</div>
					</div>
					<div class="col-sm-12 col-lg-6 col-xl-4" style="display:none">
						<div class="card">
							<div class="card-body">
								<div class="clearfix">
									<div class="float-right">
										<i class="fa-solid fa-calendar text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Payment Date</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="lblpaydate" runat="server" Text="Label"></asp:Label></h3>
										</div>
									</div>
								</div><br>
								<p class="text-muted mb-0">
									  <br>
								</p>
							</div>
						</div>
					</div>
				

             </div>

                  <!--  <div class="row">
                        <div class="col-lg-4 col-xs-12" style="display:none;">
                            <div class="small-box bg-aqua" style="height: 130px; background-color: #28e8cf !important;">
                                <div class="col-md-6" style="background-color: #28e8cf !important">
                                    <div class="inner">
                                        <p class="text">Main Wallet</p>
                                        <h3 class="number count-to">
                                           </h3>
                                    </div>
                                </div>
                                <div class="col-md-6" style="background-color: #28e8cf !important">
                                    <div class="inner">
                                        <p class="text">Payout Wallet</p>
                                        <h3 class="number count-to">
                                          </h3>
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                            </div>

                        </div>

						
						<div class="col-lg-4 col-xs-12" >
							
							 <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Status &nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;
Profit Share Budget</p>
                                    <h3 class="number count-to">
                                        </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                               
                            </div>
							
						</div>
                          	<div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Current Package</p>
                                    <h3 class="number count-to">
                                      
                                              </asp:Label>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                              
                            </div>
                        </div>

                             <div class="col-lg-4 col-xs-12" style="display:none" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Current Group</p>
                                    <h3 class="number count-to">
                                      
                                          <asp:Label ID="LblGroup" runat="server" Text="Label"></asp:Label>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                              
                            </div>
                        </div>
						   <div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Activation Date</p>
                                    <h3 class="number count-to">
                                      
                                           
                                    </h3>
                                </div>
                                <div class="icon">
                                       <i class="fa fa-calendar"></i>
                                </div>
                              
                            </div>
                        </div> -->
                        

                          <div class="col-lg-4 col-xs-12" style="display:none">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Main Wallet</p>
                                    <h3 class="number count-to">
                                        <asp:Label ID="lblwalletBalance" runat="server" Text="Label"></asp:Label></h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="UserWallet.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                         <div class="col-lg-4 col-xs-12" style="display:none">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Payout Wallet</p>
                                    <h3 class="number count-to">
                                         <asp:Label ID="lblshoppingWallet" runat="server" Text="Label"></asp:Label></h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="UserWallet.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
							 
						</div>
							
							 
                        </div>
					
			<!--	<div class="row">

                        <div class="col-lg-4 col-xs-12" >	 
							 
                             <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Total Team</p>
                                    <h3 class="number count-to">
                                      
                                         
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="DownlineReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
							
							
						</div>

                        <div class="col-lg-4 col-xs-12" >
                             <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Total Active Team</p>
                                    <h3 class="number count-to">
                                      
                                         
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="DownlineReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
							
						</div>
					<div class="col-lg-4 col-xs-12" >
						
                             <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Active Direct</p>
                                    <h3 class="number count-to">
                                      
                                         
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="UserDirectAssociates.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
						</div>
							
						</div>	
					
						 <div class="col-lg-4 col-xs-12" >
					
							
			
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Total Direct</p>
                                    <h3 class="number count-to">
                                        <asp:Label ID="LblsalaryPoint" runat="server" Text="Label" Visible="false"></asp:Label>
                                        
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="UserDirectAssociates.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
						</div> 


                    <div class="col-lg-4 col-xs-12" >
					
							
			
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Total Income</p>
                                    <h3 class="number count-to">
                                      
                                         
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="UserDirectAssociates.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
						</div> 
						
						
					</div>
					<div class="row">
                         <div class="col-lg-4 col-xs-12">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Level ROI Income</p>
                                    <h3 class="number count-to">
                                        <asp:Label ID="Lblsalary" runat="server" Text="0" Visible="false"></asp:Label>
                                           <font style="color:#fff">$</font>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="GiftBalanceReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
							 
							 <div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">ROI Income</p>
                                  
                                      
									 <h3 class="number count-to">
										 <asp:Label ID="LblBinaryPoint" runat="server" Text="0" Visible="false"></asp:Label>
                                           <font style="color:#fff">$</font>
									</h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="GiftBalanceReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>

                        <div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Autopool Income</p>
                                    <h3 class="number count-to">
                                       <%-- <asp:Label ID="LblBinaryIncome" runat="server" Text="Label" Visible="false"></asp:Label>--%>
                                         <asp:Label ID="asfr" runat="server" Text="0" Visible="false" ></asp:Label>
                                            <font style="color:#fff">$</font>
                                    </h3>
                                    
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="AutoPoolIncomeReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>

                        <div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <p class="text">Level Income</p>
                                    <h3 class="number count-to">
                                        <asp:Label ID="LblRechargewallet" runat="server" Text="Label" Visible="false"></asp:Label>
                                         <font style="color:#fff">$</font>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="LevelIncomeReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
						<div class="col-lg-4 col-xs-12" style="display:none;" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Level Achieved</p>
                                    <h3 class="number count-to">
                                      
                                         <asp:Label ID="LblLevelNo" runat="server" Text="Label" ></asp:Label>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
						<div class="col-lg-4 col-xs-12" >
 <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Group Income</p>
                                    <h3 class="number count-to">
                                      
                                         <font style="color:#fff">$</font>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="LuckyDrawClosingReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
							
						</div>
						
						<div class="col-lg-4 col-xs-12" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Payment Date</p>
                                    <h3 class="number count-to">
                                      
                                                
                                    </h3><br>
                                </div>
                                <div class="icon">
                                   <i class="fa fa-calendar"></i>
                                </div>
                              
                            </div>
                        </div>
                      
                        <div class="col-lg-4 col-xs-12" style="display:none" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Group Income (commission)</p>
                                    <h3 class="number count-to">
                                      
                                                <asp:Label ID="lblincome" runat="server" Text="Label"></asp:Label> <font style="color:#fff">%</font>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="LuckyDrawClosingReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                     
                      

                      

                   

                        <div class="col-lg-4 col-xs-12" style="display:none" >
                            <div class="small-box bg-primary" >
                                <div class="inner">
                                    <p class="text">Boost Profit Share Status</p>
                                    <h3 class="number count-to">
                                      
                                         <asp:Label ID="LblBoostPFS" runat="server" Text="" ></asp:Label>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-users"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>

					</div> -->
					
					
					
					
					<div class="row">
							 
						
						
						
						
						
						
					</div>
					
					<div class="row">
						
						
						
					</div>
                        <div class="col-lg-4 col-xs-12" style="display: none;">
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <p class="text">Change Password</p>
                                    <h3 class="number count-to">
                                        <span id="Span4" style="color: transparent">0.0</span></h3>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-key"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
					
				</div>
                       
                          <div class="col-lg-4 col-xs-12" style="display:none;">
                            <div class="small-box bg-purple">
                                <div class="inner" style="min-height: 104px;">
                                    <p class="text">Total Business</p>
                                     <h3 class="number count-to" style="font-size:16px;">
                                        <asp:Label ID="labl1323" runat="server" Text="Label"></asp:Label> <span style="color:white;">|</span> 
                                         <asp:Label ID="lblBVvalue" runat="server" Text="Label"></asp:Label>
                                     </h3>                                   
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
					
					

                    </div>
                </div>


              

                <!--Recharge Panel (Starts) pasted from Recharge.aspx-->


                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdateProgress ID="updateProgress" runat="server">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate></ContentTemplate>
                </asp:UpdatePanel>



                <!--Recharge Panel (Ends)-->
                <div class="row" style="display:none">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Quick link
                                    <span id="spanprime" runat="server" visible="false" class="blinking spanprime" style="float: right; font-weight: bold; font-size: 16px;">Become a Prime Member Now&nbsp;<a href="#" id="a_prime" onclick="return primeclick();" style="color: yellow;">click here</a></span>

                            </div>
                            <div class="panel-body">
                                <div class="row">
                                     <div class="col-md-6 col-lg-3 col-sm-6">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="useradd.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-user fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-success" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">User add </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-3 col-sm-6" style="display: none">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="Recharge.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-mobile fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-success" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Recharge & Bill </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-3 col-sm-6" style="display: none">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="rechargereport.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-calendar fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-danger" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Recharge Detail </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-3 col-sm-6" style="display: none">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="PurchaseItem.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-cart-plus fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-warning" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Purchase Product </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-3 col-sm-6">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="WithdrawlRequstAdd.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-rupee-sign fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-primary" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Withdrawl Request</div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-3 col-sm-6">
                                        <div class="panel-body no-padding dashboardbox">
                                            <a href="DepositRequstAdd.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-rupee-sign fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-info" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Deposit Request </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>

                                    <div class="col-md-6 col-lg-3 col-sm-6">
                                        <div class="panel panel-default panel-white">
                                            <a href="UserWallet.aspx" style="text-decoration: none;">
                                                <div class="panel-body no-padding dashboardbox">
                                                    <div class="partition-azure padding-20 text-center core-icon">
                                                        <i class="fa fa-rupee-sign fa-3x icon-big"></i>
                                                    </div>
                                                    <div class="core-content">
                                                    </div>
                                                </div>

                                                <div class="panel-footer clearfix label-primary" style="padding: 5px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;">
                                                    <div class="subtitle" style="text-align: center; color: #fff">Wallet Status </div>
                                                </div>
                                            </a>
                                        </div>

                                    </div>




                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row" style="display: none">
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <p class="text">Current PV</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblCurrentPV" runat="server" Text="Label"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-green">
                            <div class="inner">
                                <p class="text">Used PV</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblUsedPV" runat="server" Text="Label"></asp:Label></h3>

                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-red">
                            <div class="inner">
                                <p class="text">Total PV</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblTotalPV" runat="server" Text="Label"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>








                </div>


                <div class="row" style="display: none;">
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <p class="text">Current Recharge wallet</p>
                                <h3 class="number count-to">
                                    <%-- <asp:Label ID="LblRechargewallet" runat="server" Text="Label"></asp:Label>--%></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <!-- small box -->
                        <div class="small-box bg-red">
                            <div class="inner">
                                <p class="text">Current Utility Wallet</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblUtilityWallet" runat="server" Text="Label"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>


                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <p class="text">Current Balance</p>
                                <h3 class="number count-to">
                                  </h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>






                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <!-- small box -->
                        <div class="small-box bg-lime">
                            <div class="inner">
                                <p class="text">Monthly Business</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="Label3" runat="server" Text="0.00"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>







                </div>

                <div class="row" style="display: none;">
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <p class="text">Today's Business</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblTodayBuissness" runat="server" Text="Label"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-green">
                            <div class="inner">
                                <p class="text">Recharge Wallet Purchase</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblTodayWalletPurchase" runat="server" Text="Label"></asp:Label></h3>

                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <div class="small-box bg-red">
                            <div class="inner">
                                <p class="text">Utility Wallet Purchase</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="LblUtilitywalletPurchase" runat="server" Text="Label"></asp:Label></h3>
                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>



                    <%--<div class="col-lg-3 col-xs-6">--%>
                    <div class="col-lg-3 col-xs-12">
                        <!-- small box -->
                        <div class="small-box bg-blue">
                            <div class="inner">
                                <p class="text">Today's Commission</p>
                                <h3 class="number count-to">
                                    <asp:Label ID="Label4" runat="server" Text="0.00"></asp:Label></h3>

                            </div>
                            <div class="icon">
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>



                </div>


                <div class="row" style="display: none;">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Status Report</div>
                            <div class="panel-body">
                                <label>Awards & Rewards Current Qualification Status</label>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.N.">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AWARD">
                                                <ItemTemplate>
                                                    <asp:Label ID="labawardname" runat="server" Text='<%#Eval("awardname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="START DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="END DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARGET LEFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARGET RIGHT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CURRENT LEFTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentLeftBv" runat="server" Text='<%#Eval("CurrentLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CURRENT RIGHTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentRightBv" runat="server" Text='<%#Eval("CurrentRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REQUIRED LEFTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequiredLeftBv" runat="server" Text='<%#Eval("RequiredLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REQUIRED RIGHTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequiredRightBv" runat="server" Text='<%#Eval("RequiredRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <label>Dream Vacation Achievers Status Report</label>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.N.">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VACATION">
                                                <ItemTemplate>
                                                    <asp:Label ID="labawardname" runat="server" Text='<%#Eval("vacationname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="START DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                                    <asp:Label ID="lblfromdate1" runat="server" Text='<%#Eval("Fromdate") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="END DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                                    <asp:Label ID="lbltodate1" runat="server" Text='<%#Eval("Todate") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARGET LEFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARGET RIGHT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CURRENT LEFTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentLeftBv" runat="server" Text='<%#Eval("CurrentLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CURRENT RIGHTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentRightBv" runat="server" Text='<%#Eval("CurrentRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REQUIRED LEFTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequiredLeftBv" runat="server" Text='<%#Eval("RequiredLeft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REQUIRED RIGHTBV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequiredRightBv" runat="server" Text='<%#Eval("RequiredRight") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>

                <div class="row" style="display: none;">
                    <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                        <div class="userbox">
                            <div class="box-icon">
                                <asp:Image ID="ImgMyPhoto" runat="server" class="img-responsive img-circle" />
                                <%-- <img src="img/pic.png" class="img-responsive img-circle"/>--%>
                            </div>
                            <div class="info">
                                <h4 class="text-center">User Details</h4>
                                <h6 class="text-center"><strong>Address :</strong>
                                    <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label></h6>
                                <ul class="list-inline">
                                    <li><strong>Joining Date :</strong>
                                        <asp:Label ID="lbljoiningdate" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Activate date :</strong>
                                        <asp:Label ID="Lblactivatedate" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Sponser ID :</strong>
                                        <asp:Label ID="LblSponserId" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Sponser Name :</strong>
                                        <asp:Label ID="LblSponserName" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Parent ID :</strong>
                                        <asp:Label ID="LblParentId" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Parent Name :</strong>
                                        <asp:Label ID="LblParentName" runat="server" Text=""></asp:Label></li>
                                    <li><strong>Mobile :</strong>
                                     
                                    <li><strong>Email :</strong>
                                        <asp:Label ID="lblemail" runat="server" Text=""></asp:Label></li>
                                </ul>

                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4" style="display: none;">
                        <div class="userbox">
                            <div class="info">
                                <h4 class="text-center">Bank Details</h4>
                                <p><strong>A/c Holder Name :</strong>
                                    <asp:Label ID="lblaccountholdername" runat="server" Text=""></asp:Label></p>
                                <p><strong>A/c No :</strong>
                                    <asp:Label ID="lblaccountno" runat="server" Text=""></asp:Label></p>
                                <p><strong>Bank :</strong>
                                    <asp:Label ID="lblbank" runat="server" Text=""></asp:Label></p>
                                <p><strong>IFSC Code :</strong>
                                    <asp:Label ID="lblifsc" runat="server" Text=""></asp:Label></p>
                                <p><strong>Pan No :</strong>
                                    <asp:Label ID="lblpan" runat="server" Text=""></asp:Label></p>

                            </div>
                        </div>
                    </div>

                </div>


                <div class="row" style="display: none;">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Award List</div>
                            <div class="panel-body">
                                <div class="">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-hover table-bordered dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdBank_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Level">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("ulevel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("target") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Acheived">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("bv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Acheived Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status1" runat="server" Text='<%#Eval("ADate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PaymentStatus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PaymentStatus" runat="server" Text='<%#Eval("PaymentStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PaymentDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PaymentDate" runat="server" Text='<%#Eval("pdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView4" runat="server" CssClass="table table-hover dataTable danger-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Weekly Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GridView5" runat="server" CssClass="table dataTable sucess-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Fortnight Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView6" runat="server" CssClass="table table-hover dataTable warning-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Total Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row" style="display: none">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Performance</div>
                            <asp:Button ID="Button4" runat="server" Text="Refresh" OnClick="Button4_Click" />
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdPerformance" runat="server" CssClass="table table-hover table-bordered dataTable"
                                                Width="100%" AutoGenerateColumns="False" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("active") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deactive">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("deactive") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 60px; padding-bottom: 60px;">

                                        <%--<div class="col-lg-3 col-xs-6">--%>
                                        <div class="col-lg-3 col-xs-12">
                                            <div class="serviceBox text-center">
                                                <a data-toggle="modal" data-target="#modal-today">
                                                    <div class="service-icon">
                                                        <div class="service-icon">
                                                            <asp:Label ID="lblTodayPerformance" CssClass="heading" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <h3 class="title">Today Performance</h3>
                                                </a>
                                            </div>
                                            <!-- small box -->
                                        </div>

                                        <%--<div class="col-lg-3 col-xs-6">--%>
                                        <div class="col-lg-3 col-xs-12">
                                            <div class="serviceBox pink text-center">
                                                <a data-toggle="modal" data-target="#modal-week">
                                                    <div class="service-icon">
                                                        <div class="service-icon">
                                                            <asp:Label ID="lblCurrentWeek" CssClass="heading" runat="server" Text="0" Style="color: #d41271;"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <h3 class="title">Week Performance</h3>
                                                </a>
                                            </div>

                                        </div>

                                        <%--<div class="col-lg-3 col-xs-6">--%>
                                        <div class="col-lg-3 col-xs-12">
                                            <div class="serviceBox yellow text-center">
                                                <a data-toggle="modal" data-target="#modal-month">
                                                    <div class="service-icon">
                                                        <div class="service-icon">
                                                            <asp:Label ID="lblCurrentMonth" CssClass="heading" runat="server" Text="0" Style="color: #fba21a;"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <h3 class="title">Month Performance</h3>
                                                </a>
                                            </div>

                                        </div>

                                        <%--<div class="col-lg-3 col-xs-6">--%>
                                        <div class="col-lg-3 col-xs-12 text-center">
                                            <div class="serviceBox blue text-center">
                                                <a data-toggle="modal" data-target="#modal-total">
                                                    <div class="service-icon">
                                                        <div class="service-icon">
                                                            <asp:Label ID="lblTotal" CssClass="heading" runat="server" Text="0" Style="color: #05b4b7;"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <h3 class="title">Total</h3>
                                                </a>
                                            </div>

                                        </div>

                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="display: none;">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">User Performance</div>
                            <div class="panel-body">
                                <div class="">
                                    <div class="col-md-6">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GridViewToday" runat="server" CssClass="table table-hover table-bordered dataTable" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Today Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrvVwWeek" runat="server" CssClass="table table-hover dataTable danger-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Weekly Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GrdVwMonth" runat="server" CssClass="table dataTable sucess-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Fortnight Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdVwTotal" runat="server" CssClass="table table-hover dataTable warning-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Total Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlTopDirectRanking" runat="server" Visible="true" CssClass="dash-prize-section dash-ranking-section">
                    <div class="dash-prize-head">
                        <div class="dash-prize-head-icon"><i class="fa fa-trophy"></i></div>
                        <div class="dash-prize-head-text">
                            <h3>Top Direct Ranking</h3>
                            <p>Overall leaderboard by SavingStatus active directs.</p>
                        </div>
                        <a href="TopDirectRanking.aspx" class="dash-btn dash-btn-outline dash-ranking-view-all">View All</a>
                    </div>
                    <asp:Panel ID="pnlTopDirectMyRank" runat="server" Visible="false" CssClass="dash-ranking-my-rank">
                        <i class="fa fa-user"></i>
                        <asp:Literal ID="litTopDirectMyRank" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlTopDirectGrid" runat="server" Visible="false" CssClass="dash-ranking-table-wrap">
                        <asp:GridView ID="grdTopDirectRanking" runat="server"
                            CssClass="dash-ranking-table" Width="100%"
                            AutoGenerateColumns="False" GridLines="None"
                            OnRowDataBound="grdTopDirectRanking_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRank" runat="server" CssClass="dash-rank-badge" Text='<%# Eval("RankNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User ID">
                                    <ItemTemplate><strong><%# Eval("userid") %></strong></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Direct Rank">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDirectRankTitle" runat="server" CssClass="dash-direct-rank-pill" Text='<%# Eval("DirectRank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Directs">
                                    <ItemTemplate>
                                        <span class="dash-direct-count"><%# Eval("DirectCount") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlTopDirectEmpty" runat="server" Visible="false" CssClass="dash-prize-empty">
                        <i class="fa fa-users"></i>
                        <p>No ranking data available yet. Active direct referrals will appear here.</p>
                    </asp:Panel>
                </asp:Panel>

                <asp:Panel ID="pnlPrizes" runat="server" Visible="true" CssClass="dash-prize-section dash-prize-section--winners">
                    <div class="dash-prize-head">
                        <div class="dash-prize-head-icon"><i class="fa fa-trophy"></i></div>
                        <div class="dash-prize-head-text">
                            <h3>Prize Winners</h3>
                            <p><asp:Label ID="lblPrizeSubtitle" runat="server" Text="Last month prize winners." /></p>
                        </div>
                        <span class="dash-prize-count"><asp:Label ID="lblPrizeCount" runat="server" Text="0" /></span>
                    </div>
                    <asp:Panel ID="pnlPrizeGrid" runat="server" Visible="false" CssClass="dash-winners-grid">
                        <asp:Repeater ID="rptPrizes" runat="server">
                            <ItemTemplate>
                                <div class="dash-winner-card">
                                    <span class="dash-winner-rank">#<%# Container.ItemIndex + 1 %></span>
                                    <div class="dash-winner-card-top">
                                        <span class="dash-winner-avatar"><%# GetInitial(Eval("UserName"), Eval("UserId")) %></span>
                                        <div class="dash-winner-id-wrap">
                                            <span class="dash-winner-name"><%# Server.HtmlEncode(GetWinnerName(Eval("UserName"), Eval("UserId"))) %></span>
                                            <span class="dash-winner-id"><i class="fa fa-id-badge"></i> <%# Server.HtmlEncode(Convert.ToString(Eval("UserId"))) %></span>
                                        </div>
                                    </div>
                                    <div class="dash-winner-prize">
                                        <span class="dash-winner-prize-ico"><i class="fa fa-gift"></i></span>
                                        <span class="dash-winner-prize-name"><%# Server.HtmlEncode(Convert.ToString(Eval("PrizeName"))) %></span>
                                    </div>
                                    <div class="dash-winner-foot">
                                        <span class="dash-winner-month"><i class="fa fa-calendar"></i> <%# GetPrizeMonth(Eval("PrizeMonth")) %></span>
                                        <span class="dash-winner-badge"><i class="fa fa-trophy"></i> Winner</span>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <asp:Panel ID="pnlPrizeEmpty" runat="server" Visible="false" CssClass="dash-prize-empty">
                        <i class="fa fa-gift"></i>
                        <p><asp:Label ID="lblPrizeEmptyText" runat="server" Text="No prize winners for last month yet." /></p>
                    </asp:Panel>
                </asp:Panel>

            </div>

        </div>
    </div>
    </div>

    
    <!-- /.box-body -->
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript" language="javascript">
        (function () {
            var el = document.getElementById("dashDateRange");
            if (!el) return;

            var end = new Date();
            var start = new Date();
            start.setDate(end.getDate() - 29);

            function fmt(d) {
                var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                return d.getDate() + " " + months[d.getMonth()] + " " + String(d.getFullYear()).slice(-2);
            }

            el.textContent = fmt(start) + " - " + fmt(end);
        })();

        function copyReferralLink(inputId, btn) {
            var input = document.getElementById(inputId);
            if (!input || !input.value) {
                return;
            }

            var text = input.value;

            function showCopied() {
                if (!btn) {
                    showReferralToast('Link copied to clipboard');
                    return;
                }
                var label = btn.querySelector('span');
                var icon = btn.querySelector('i');
                var oldLabel = label ? label.textContent : '';
                var oldIconClass = icon ? icon.className : '';
                btn.classList.add('is-copied');
                if (label) {
                    label.textContent = 'Copied';
                }
                if (icon) {
                    icon.className = 'fa fa-check';
                }
                showReferralToast('Referral link copied');
                setTimeout(function () {
                    btn.classList.remove('is-copied');
                    if (label) {
                        label.textContent = oldLabel || 'Copy';
                    }
                    if (icon) {
                        icon.className = oldIconClass || 'fa fa-copy';
                    }
                }, 1800);
            }

            if (navigator.clipboard && navigator.clipboard.writeText) {
                navigator.clipboard.writeText(text).then(showCopied).catch(function () {
                    input.select();
                    document.execCommand('copy');
                    showCopied();
                });
                return;
            }

            input.select();
            document.execCommand('copy');
            showCopied();
        }

        function shareReferralWhatsApp(inputId) {
            var input = document.getElementById(inputId);
            if (!input || !input.value) {
                return;
            }
            var message = 'Join Maniraya using my referral link: ' + input.value;
            window.open('https://wa.me/?text=' + encodeURIComponent(message), '_blank');
        }

        function getIncentiveLabelText(clientId) {
            var el = document.getElementById(clientId);
            if (!el) {
                return '';
            }
            return (el.innerText || el.textContent || '').replace(/\s+/g, ' ').trim();
        }

        function setExportText(id, value) {
            var el = document.getElementById(id);
            if (el) {
                el.textContent = value || '-';
            }
        }

        function setExportAmount(id, value) {
            var el = document.getElementById(id);
            if (el) {
                el.textContent = '₹ ' + (value || '0.00');
            }
        }

        function syncIncentiveExportSheet() {
            setExportText('exportIncentivePeriod', getIncentiveLabelText('<%= lblIncentivePeriod.ClientID %>'));
            setExportText('exportIncentiveName', getIncentiveLabelText('<%= lblIncentiveName.ClientID %>'));
            setExportText('exportIncentiveState', getIncentiveLabelText('<%= lblIncentiveState.ClientID %>'));
            setExportText('exportIncentiveDistrict', getIncentiveLabelText('<%= lblIncentiveDistrict.ClientID %>'));
            setExportText('exportIncentivePan', getIncentiveLabelText('<%= lblIncentivePan.ClientID %>'));
            setExportText('exportIncentiveUserId', getIncentiveLabelText('<%= lblReferralUserId.ClientID %>'));
            setExportAmount('exportSavingDirect', getIncentiveLabelText('<%= lblSavingDirectIncome.ClientID %>'));
            setExportAmount('exportLevelIncome', getIncentiveLabelText('<%= lblLevelIncomeCard.ClientID %>'));
            setExportAmount('exportPremiumDirect', getIncentiveLabelText('<%= lblPremiumDirectIncome.ClientID %>'));
            setExportAmount('exportTeamBonus', getIncentiveLabelText('<%= lblMatchingIncomeCard.ClientID %>'));
            setExportAmount('exportSelfBonus', getIncentiveLabelText('<%= lblCashBackIncome.ClientID %>'));
            setExportAmount('exportWallet', getIncentiveLabelText('<%= lblProductWalletBalance.ClientID %>'));
            setExportAmount('exportTotal', getIncentiveLabelText('<%= lblIncentiveTotal.ClientID %>'));
            setExportText('exportIncentiveUpdated', getIncentiveLabelText('<%= lblIncentiveUpdated.ClientID %>'));

            var photo = document.getElementById('<%= imgIncentivePhoto.ClientID %>');
            var exportPhoto = document.getElementById('exportIncentivePhoto');
            if (photo && exportPhoto) {
                exportPhoto.src = photo.src;
            }
        }

        function buildIncentiveShareText() {
            syncIncentiveExportSheet();
            var lines = [
                'MPremium Incentive Summary',
                getIncentiveLabelText('<%= lblIncentivePeriod.ClientID %>'),
                '',
                'Distributor: ' + getIncentiveLabelText('<%= lblIncentiveName.ClientID %>'),
                'User ID: ' + getIncentiveLabelText('<%= lblReferralUserId.ClientID %>'),
                'State: ' + getIncentiveLabelText('<%= lblIncentiveState.ClientID %>'),
                'District: ' + getIncentiveLabelText('<%= lblIncentiveDistrict.ClientID %>'),
                'PAN: ' + getIncentiveLabelText('<%= lblIncentivePan.ClientID %>'),
                '',
                'Saving Direct Income: ₹ ' + getIncentiveLabelText('<%= lblSavingDirectIncome.ClientID %>'),
                'Level Income: ₹ ' + getIncentiveLabelText('<%= lblLevelIncomeCard.ClientID %>'),
                'MPremium Direct Income: ₹ ' + getIncentiveLabelText('<%= lblPremiumDirectIncome.ClientID %>'),
                'Team Bonus: ₹ ' + getIncentiveLabelText('<%= lblMatchingIncomeCard.ClientID %>'),
                'Self Business Bonus: ₹ ' + getIncentiveLabelText('<%= lblCashBackIncome.ClientID %>'),
                'Product Wallet Balance: ₹ ' + getIncentiveLabelText('<%= lblProductWalletBalance.ClientID %>'),
                'Total Incentive: ₹ ' + getIncentiveLabelText('<%= lblIncentiveTotal.ClientID %>'),
                '',
                'Last Updated: ' + getIncentiveLabelText('<%= lblIncentiveUpdated.ClientID %>'),
                'https://mpremium.in/'
            ];

            return lines.join('\n');
        }

        function openIncentiveShareModal() {
            syncIncentiveExportSheet();
            var modal = document.getElementById('dashIncentiveShareModal');
            var preview = document.getElementById('dashIncentiveSharePreview');
            var exportCard = document.getElementById('dashIncentiveExportCard');
            if (!modal || !preview || !exportCard) {
                return;
            }

            preview.innerHTML = '';
            preview.appendChild(exportCard.cloneNode(true));

            modal.classList.add('is-open');
            modal.setAttribute('aria-hidden', 'false');
            document.body.classList.add('dash-incentive-share-open');
        }

        function closeIncentiveShareModal() {
            var modal = document.getElementById('dashIncentiveShareModal');
            if (!modal) {
                return;
            }
            modal.classList.remove('is-open');
            modal.setAttribute('aria-hidden', 'true');
            document.body.classList.remove('dash-incentive-share-open');
        }

        function loadHtml2Canvas(callback) {
            if (window.html2canvas) {
                callback();
                return;
            }

            var script = document.createElement('script');
            script.src = 'https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js';
            script.onload = callback;
            script.onerror = function () {
                showReferralToast('Unable to load image share tool');
            };
            document.head.appendChild(script);
        }

        function getSharePreviewCard() {
            var preview = document.getElementById('dashIncentiveSharePreview');
            if (!preview) {
                return document.getElementById('dashIncentiveExportCard');
            }
            return preview.querySelector('.dash-incentive-export-card') || document.getElementById('dashIncentiveExportCard');
        }

        function shareIncentiveAsImage() {
            var card = getSharePreviewCard();
            if (!card) {
                return;
            }

            loadHtml2Canvas(function () {
                window.html2canvas(card, {
                    backgroundColor: '#ffffff',
                    scale: 2,
                    useCORS: true,
                    allowTaint: true
                }).then(function (canvas) {
                    canvas.toBlob(function (blob) {
                        if (!blob) {
                            showReferralToast('Unable to create image');
                            return;
                        }

                        var fileName = 'mpremium-incentive-' + Date.now() + '.png';
                        if (navigator.share && navigator.canShare && navigator.canShare({ files: [new File([blob], fileName, { type: 'image/png' })] })) {
                            var file = new File([blob], fileName, { type: 'image/png' });
                            navigator.share({
                                title: 'MPremium Incentive Summary',
                                files: [file]
                            }).catch(function () {
                                downloadIncentiveImage(canvas, fileName);
                            });
                            return;
                        }

                        downloadIncentiveImage(canvas, fileName);
                    }, 'image/png');
                }).catch(function () {
                    showReferralToast('Unable to create share image');
                });
            });
        }

        function downloadIncentiveImage(canvas, fileName) {
            var link = document.createElement('a');
            link.download = fileName;
            link.href = canvas.toDataURL('image/png');
            link.click();
            showReferralToast('Incentive image downloaded');
        }

        function shareIncentiveWhatsApp() {
            var text = buildIncentiveShareText();
            window.open('https://wa.me/?text=' + encodeURIComponent(text), '_blank');
        }

        function copyIncentiveSummary() {
            var text = buildIncentiveShareText();
            if (navigator.clipboard && navigator.clipboard.writeText) {
                navigator.clipboard.writeText(text).then(function () {
                    showReferralToast('Incentive summary copied');
                }).catch(function () {
                    shareIncentiveWhatsApp();
                });
                return;
            }
            shareIncentiveWhatsApp();
        }

        function printIncentiveCard() {
            syncIncentiveExportSheet();

            var card = document.getElementById('dashIncentiveExportCard');
            if (!card) {
                return;
            }

            var iframe = document.getElementById('dashIncentivePrintFrame');
            if (!iframe) {
                iframe = document.createElement('iframe');
                iframe.id = 'dashIncentivePrintFrame';
                iframe.setAttribute('title', 'Print incentive summary');
                iframe.style.cssText = 'position:fixed;width:0;height:0;border:0;right:0;bottom:0;';
                document.body.appendChild(iframe);
            }

            var printDoc = iframe.contentWindow || iframe.contentDocument;
            if (printDoc.document) {
                printDoc = printDoc.document;
            }

            var html = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>MPremium Incentive Summary</title>';
            html += '<link rel="stylesheet" href="assets/css/dashboard-modern.css?v=40" />';
            html += '<style>';
            html += 'body{margin:0;padding:24px;background:#fff;-webkit-print-color-adjust:exact;print-color-adjust:exact;}';
            html += '.dash-incentive-export-card{max-width:520px;margin:0 auto;box-shadow:none !important;}';
            html += '@media print{body{padding:0;} .dash-incentive-export-card{border-radius:0;max-width:100%;}}';
            html += '</style></head><body>';
            html += card.outerHTML;
            html += '</body></html>';

            printDoc.open();
            printDoc.write(html);
            printDoc.close();

            var printWindow = iframe.contentWindow;
            var doPrint = function () {
                try {
                    printWindow.focus();
                    printWindow.print();
                } catch (e) {
                    document.body.classList.add('is-printing-incentive');
                    window.print();
                    setTimeout(function () {
                        document.body.classList.remove('is-printing-incentive');
                    }, 500);
                }
            };

            if (printWindow.document.readyState === 'complete') {
                setTimeout(doPrint, 150);
            } else {
                iframe.onload = function () {
                    setTimeout(doPrint, 150);
                };
            }
        }

        function shareIncentiveCard() {
            openIncentiveShareModal();
        }

        function showReferralToast(message) {
            var toast = document.getElementById('dashReferralToast');
            if (!toast) {
                return;
            }
            toast.textContent = message;
            toast.classList.add('is-visible');
            clearTimeout(window._dashReferralToastTimer);
            window._dashReferralToastTimer = setTimeout(function () {
                toast.classList.remove('is-visible');
            }, 2400);
        }

        function CopyToClipboard() {
            copyReferralLink('<%=TxtLeftLinkLink.ClientID%>', null);
        }

        function CopyToClipboard2() {
            copyReferralLink('<%=TxtRightLink.ClientID%>', null);
        }

        function primeclick() {

            if (confirm("Are you sure want to become a prime member ?")) {
                $.ajax({
                    url: "Dashboard.aspx/BecomePrimeMember",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{}",
                    success: function (r) {
                        if (r.d == 1) {
                            alert('Congrats! Your request has been send');
                            $(".spanprime").hide();
                            location.href = "Dashboard.aspx";
                        }
                        else if (r.d == 2) {

                            alert('error! you are already prime member');
                        }
                        else if (r.d == 3) {

                            alert('error! your previous request is already pending');
                        }
                        else {
                            return false;
                        }
                    },
                    error: function (r) { }
                });

            }
            else {
                return false;
            }
        }

    </script>



    <!--(Starts)Pasted from Recgarge.aspx-->






    <!--(Ends)-->


    <!--(Starts) For User Performance-->

    <div class="modal fade" id="modal-today">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Today Performance</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <center>
                    <table style="width:100%; text-align:center" class="table table-bordered table-hover table-responsive">
                        <tr>
                            <th style="text-align:center">Active</th>
                            <th style="text-align:center">Deactive</th>
                            <th style="text-align:center">Total</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTodayActive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTodayDeactive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTodayTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </center>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade" id="modal-week">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Current Week Performance</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <center>
                    <table style="width:100%; text-align:center" class="table table-bordered table-hover table-responsive">
                        <tr>
                            <th style="text-align:center">Active</th>
                            <th style="text-align:center">Deactive</th>
                            <th style="text-align:center">Total</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblWeekActive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWeekDeactive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWeekTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </center>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade" id="modal-month">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Current Month Performance</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <center>
                    <table style="width:100%; text-align:center" class="table table-bordered table-hover table-responsive">
                        <tr>
                            <th style="text-align:center">Active</th>
                            <th style="text-align:center">Deactive</th>
                            <th style="text-align:center">Total</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMonthActive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMonthDeactive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMonthTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </center>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade" id="modal-total">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Total Performance</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <center>
                    <table style="width:100%; text-align:center" class="table table-bordered table-hover table-responsive">
                        <tr>
                            <th style="text-align:center">Active</th>
                            <th style="text-align:center">Deactive</th>
                            <th style="text-align:center">Total</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalActive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalDeactive" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </center>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--(Ends) For User Performance-->

    <div id="dashIncentiveExport" class="dash-incentive-export" aria-hidden="true">
        <div class="dash-incentive-export-card" id="dashIncentiveExportCard">
            <div class="dash-incentive-export-topbar">
                <div class="dash-incentive-export-brand-wrap">
                    <span class="dash-incentive-export-brand">MPremium</span>
                    <span class="dash-incentive-export-website">https://mpremium.in/</span>
                </div>
                <span class="dash-incentive-export-period" id="exportIncentivePeriod"></span>
            </div>

            <div class="dash-incentive-export-profile">
                <div class="dash-incentive-export-details">
                    <div class="dash-incentive-export-detail">
                        <span>Distributor</span>
                        <strong id="exportIncentiveName"></strong>
                    </div>
                    <div class="dash-incentive-export-detail">
                        <span>State</span>
                        <strong id="exportIncentiveState"></strong>
                    </div>
                    <div class="dash-incentive-export-detail">
                        <span>District</span>
                        <strong id="exportIncentiveDistrict"></strong>
                    </div>
                    <div class="dash-incentive-export-detail">
                        <span>PAN</span>
                        <strong id="exportIncentivePan"></strong>
                    </div>
                    <div class="dash-incentive-export-detail">
                        <span>User ID</span>
                        <strong id="exportIncentiveUserId"></strong>
                    </div>
                </div>
                <div class="dash-incentive-export-photo-wrap">
                    <img id="exportIncentivePhoto" class="dash-incentive-export-photo" src="img/default.png" alt="Profile photo" />
                </div>
            </div>

            <div class="dash-incentive-export-income">
                <div class="dash-incentive-export-income-title">Income Summary</div>
                <div class="dash-incentive-export-income-head">
                    <span>Income Type</span>
                    <span>Amount</span>
                </div>
                <div class="dash-incentive-export-income-row"><span>Saving Direct Income</span><strong id="exportSavingDirect"></strong></div>
                <div class="dash-incentive-export-income-row"><span>Level Income</span><strong id="exportLevelIncome"></strong></div>
                <div class="dash-incentive-export-income-row"><span>MPremium Direct Income</span><strong id="exportPremiumDirect"></strong></div>
                <div class="dash-incentive-export-income-row"><span>Team Bonus</span><strong id="exportTeamBonus"></strong></div>
                <div class="dash-incentive-export-income-row"><span>Self Business Bonus</span><strong id="exportSelfBonus"></strong></div>
                <div class="dash-incentive-export-income-row is-wallet"><span>Product Wallet Balance</span><strong id="exportWallet"></strong></div>
                <div class="dash-incentive-export-income-total"><span>Total Incentive</span><strong id="exportTotal"></strong></div>
            </div>

            <div class="dash-incentive-export-footer">
                <span><i class="fa fa-clock"></i> Last Updated: <strong id="exportIncentiveUpdated"></strong></span>
            </div>
        </div>
    </div>

    <div id="dashIncentiveShareModal" class="dash-incentive-share-modal" aria-hidden="true">
        <div class="dash-incentive-share-backdrop" onclick="closeIncentiveShareModal();"></div>
        <div class="dash-incentive-share-dialog" role="dialog" aria-labelledby="dashIncentiveShareTitle">
            <div class="dash-incentive-share-dialog-head">
                <h4 id="dashIncentiveShareTitle">Share Incentive Summary</h4>
                <button type="button" class="dash-incentive-share-close" onclick="closeIncentiveShareModal();" aria-label="Close">&times;</button>
            </div>
            <div id="dashIncentiveSharePreview" class="dash-incentive-share-preview"></div>
            <div class="dash-incentive-share-actions">
                <button type="button" class="dash-incentive-share-action is-image" onclick="shareIncentiveAsImage();">
                    <i class="fa fa-picture-o"></i> Share Image
                </button>
                <button type="button" class="dash-incentive-share-action is-whatsapp" onclick="shareIncentiveWhatsApp();">
                    <i class="fa fa-whatsapp"></i> WhatsApp
                </button>
                <button type="button" class="dash-incentive-share-action is-copy" onclick="copyIncentiveSummary();">
                    <i class="fa fa-copy"></i> Copy Text
                </button>
                <button type="button" class="dash-incentive-share-action is-print" onclick="printIncentiveCard(); closeIncentiveShareModal();">
                    <i class="fa fa-print"></i> Print
                </button>
            </div>
        </div>
    </div>

    <div id="dashReferralToast" class="dash-referral-toast" role="status" aria-live="polite"></div>

</asp:Content>


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
    <link href="assets/css/dashboard-modern.css?v=13" rel="stylesheet" />
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
                                <i class="fa fa-refresh"></i>
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
                            <div class="dash-feature-card tone-saving" id="savingDashCard">
                                <div class="dash-feature-head">
                                    <div class="dash-feature-title">
                                        <span class="dash-feature-icon" aria-hidden="true"><i class="fa fa-bank"></i></span>
                                        <div>
                                            <h3>Saving Dashboard</h3>
                                            <p>Your savings products &amp; balance overview</p>
                                        </div>
                                    </div>
                                    <button type="button" class="dash-btn dash-feature-toggle" onclick="toggleDashFeature(this, 'savingDashCard');">
                                        <span class="dash-feature-toggle-text">View</span> <i class="fa fa-chevron-down"></i>
                                    </button>
                                </div>
                                <div class="dash-feature-body">
                                    <div class="dash-care-card">
                                        <div class="dash-care-card-top">
                                            <span class="dash-care-card-title">Unique Care Number</span>
                                            <span class="dash-care-card-chip" aria-hidden="true"><i class="fa fa-id-card"></i></span>
                                        </div>
                                        <div class="dash-care-card-number">
                                            <asp:Label ID="lblCareNumber" runat="server" Text="0000 0000 0000" /></div>
                                        <div class="dash-care-card-bottom">
                                            <div class="dash-care-card-holder">
                                                <span class="dash-care-card-label">Card Holder</span>
                                                <span class="dash-care-card-name">
                                                    <asp:Label ID="lblCareName" runat="server" Text="Member Name" /></span>
                                            </div>
                                            <span class="dash-care-card-logo" aria-hidden="true"><i class="fa fa-bank"></i></span>
                                        </div>
                                    </div>
                                    <div class="dash-feature-links">
                                        <a href="SavingProductPurchase.aspx" class="dash-feature-link"><i class="fa fa-shopping-cart"></i> Buy Saving Product</a>
                                        <a href="SAvingProductPurchaseReport.aspx" class="dash-feature-link"><i class="fa fa-list-alt"></i> Saving Report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="dash-feature-card tone-premium" id="premiumDashCard">
                                <div class="dash-feature-head">
                                    <div class="dash-feature-title">
                                        <span class="dash-feature-icon" aria-hidden="true"><i class="fa fa-diamond"></i></span>
                                        <div>
                                            <h3>MPremium Dashboard</h3>
                                            <p>Your premium plans &amp; package overview</p>
                                        </div>
                                    </div>
                                    <button type="button" class="dash-btn dash-feature-toggle" onclick="toggleDashFeature(this, 'premiumDashCard');">
                                        <span class="dash-feature-toggle-text">View</span> <i class="fa fa-chevron-down"></i>
                                    </button>
                                </div>
                                <div class="dash-feature-body">
                                    <div class="dash-feature-stats">
                                        <div class="dash-feature-stat">
                                            <p class="dash-feature-stat-label">Premium Status</p>
                                            <p class="dash-feature-stat-value"><asp:Label ID="lblPremiumStatus" runat="server" Text="-" /></p>
                                        </div>
                                        <div class="dash-feature-stat">
                                            <p class="dash-feature-stat-label">Active Package</p>
                                            <p class="dash-feature-stat-value"><asp:Label ID="lblPremiumPackage" runat="server" Text="-" /></p>
                                        </div>
                                    </div>
                                    <div class="dash-feature-links">
                                        <a href="JoiningPackage.aspx" class="dash-feature-link"><i class="fa fa-cube"></i> View Packages</a>
                                        <a href="PurchaseReport.aspx" class="dash-feature-link"><i class="fa fa-list-alt"></i> Purchase Report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row dash-stats-grid" style="display:none">
                        <div class="col-sm-6 col-xl-3">
                            <div class="card dash-income-card dash-income-compact tone-indigo">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Total Team</p>
                                    <h3 class="dash-income-value is-number"><asp:Label ID="lblStatTeam" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--muted">Network members</span>
                                        <a href="DownlineReport.aspx" class="dash-income-meta-link">View report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-xl-3" style="display:none">
                            <div class="card dash-income-card dash-income-compact tone-green">
                                <span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-briefcase"></i></span>
                                <div class="card-body">
                                    <p class="dash-income-label">Active Team</p>
                                    <h3 class="dash-income-value is-number"><asp:Label ID="lblStatActiveTeam" runat="server" Text="0" /></h3>
                                    <div class="dash-income-meta">
                                        <span class="dash-income-tag dash-income-tag--up">Active members</span>
                                        <a href="DownlineReport.aspx" class="dash-income-meta-link">View report</a>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                            <div id="dvlink" runat="server" visible="True" class="dash-tools-affiliate-card">
                                <div class="dash-tools-card-head">
                                    <span class="dash-tools-card-icon" aria-hidden="true"><i class="fa fa-link"></i></span>
                                    <div class="dash-tools-card-meta">
                                        <asp:Label ID="Label1" runat="server" Text="Affiliate Link (LEFT)" CssClass="dash-tools-card-title" />
                                        <span class="dash-tools-card-sub">Share this link to invite new members to your team</span>
                                    </div>
                                </div>
                                <div class="dash-tools-copy-group">
                                    <asp:TextBox ID="TxtLeftLinkLink" runat="server" CssClass="form-control dash-tools-input" />
                                    <asp:Button ID="Button1" runat="server" Text="Copy link" CssClass="btn dash-tools-copy-btn" OnClientClick="CopyToClipboard(); return false;" />
                                </div>
                            </div>

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

                        <div class="row" style="display:none;">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Affiliate Link (RIGHT)"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="form-group">
                                    <asp:TextBox ID="TxtRightLink" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button2" runat="server" Text="Copy" CssClass="btn btn-primary" OnClientClick="CopyToClipboard2()" />
                            </div>
                        </div>
                    </div>

                    <div class="dash-section-head">
                        <h3 class="dash-section-title">Income Overview</h3>
                        <p class="dash-section-sub">Track all your earnings in one place</p>
                    </div>
                    <div class="row dash-income-grid">

                              <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-red">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-money"></i></span>
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
											<h3 class="font-weight-semibold text-left mb-0 text-white"><i class="fa fa-inr"></i> &nbsp <asp:Label ID="lblDirddectincome" runat="server" Text="00.00" Visible="false"></asp:Label></h3>
										</div>
                                        <div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-white"><i class="fa fa-inr"></i> &nbsp <asp:Label ID="lblMatching11" runat="server" Text="00.00" Visible="false"></asp:Label>
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

                    <div class="dash-section-head dash-section-head--spaced">
                        <h3 class="dash-section-title">Member Overview</h3>
                        <p class="dash-section-sub">Rank, team &amp; volume snapshot</p>
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
										<i class="fa fa-inr text-secondary icon-size" style="color:green"></i>
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
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LblDownline" runat="server" Text="Label" ></asp:Label></h3>
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
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="LblActiveDownline" runat="server" Text="Label" ></asp:Label></h3>
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
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Total Left Team</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblTotalLeft" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--down">Inactive <asp:Label ID="LblInactiveleft" runat="server" Text="Label"></asp:Label></span>
									<asp:Label ID="Lblactiveleft" runat="server" Text="Label" Visible="false"></asp:Label>
									<asp:Button ID="Button3" runat="server" Text="Refresh" CssClass="dash-metric-refresh" OnClick="Button3_Click" />
								</div>
							</div>
						</div>
					</div>

                          <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-users"></i></span>
							<div class="card-body">
								<p class="dash-income-label">Total Right Team</p>
								<h3 class="dash-income-value is-number"><asp:Label ID="LblTotalright" runat="server" Text="Label"></asp:Label></h3>
								<div class="dash-income-meta">
									<span class="dash-income-tag dash-income-tag--down">Inactive <asp:Label ID="LblInActiveRight" runat="server" Text="Label"></asp:Label></span>
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
										<i class="fa fa-inr text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Total Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success">  <asp:Label ID="lblTotalincome" runat="server" Text="Label" ></asp:Label> <i class="fa fa-inr"></i> </h3>
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
											<h4 class="font-weight-semibold text-left mb-0 text-success">   <asp:Label ID="Totalbalance" runat="server" Text="Label"></asp:Label> <i class="fa fa-inr"></i> </h4>
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
										<i class="fa fa-inr text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Cash Back Income</p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LbllevelROiIncome" runat="server" Text="0" ></asp:Label> <i class="fa fa-inr"></i> </h4>
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
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-refresh"></i></span>
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

                            <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-refresh"></i></span>
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

                                       <div class="col-sm-12 col-lg-6 col-xl-4">
						<div class="card dash-income-card dash-income-compact tone-teal">
							<span class="dash-income-badge-icon" aria-hidden="true"><i class="fa fa-refresh"></i></span>
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
										<i class="fa fa-inr text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Autopool Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <i class="fa fa-inr"></i> <asp:Label ID="LblPoolIncome" runat="server" Text="Label"></asp:Label>  </h3>
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
										<i class="fa fa-inr text-secondary icon-size"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Level Income</p><br />
										<div class="">
											<h4 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="lbl878" runat="server" Text="0" ></asp:Label> <i class="fa fa-inr"></i> </h4>
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

										<i class="fa fa-inr text-secondary icon-size" style="color:green"></i>
									</div>
									<div class="float-left">
										<p class="mb-0 text-left">Group Income</p><br />
										<div class="">
											<h3 class="font-weight-semibold text-left mb-0 text-success"> <asp:Label ID="LBlGroupIncome" runat="server" Text="Label"></asp:Label> <i class="fa fa-inr"></i> </h3>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                    <i class="fa fa-inr"></i>
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
                                                        <i class="fa fa-inr fa-3x icon-big"></i>
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
                                                        <i class="fa fa-inr fa-3x icon-big"></i>
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
                                                        <i class="fa fa-inr fa-3x icon-big"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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
                                <i class="fa fa-inr"></i>
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



            </div>

        </div>
    </div>
    </div>
    <style>
        .serviceBox .service-icon {
            display: inline-block;
            width: 80px;
            height: 80px;
            border-radius: 50%;
            margin-bottom: 19px;
            position: relative;
        }

            .serviceBox .service-icon .heading {
                display: inline-block;
                width: 100%;
                height: 100%;
                border-radius: 50%;
                line-height: 80px;
                background: #fff;
                box-shadow: -5px 5px 5px rgba(0,0,0,0.5);
                font-size: 35px;
                color: #0fb513;
                position: absolute;
                top: 0;
                left: 0;
                text-align: center;
            }

            .serviceBox .service-icon:before {
                content: "";
                background: #0fb513;
                border-radius: 50%;
                position: absolute;
                top: -10px;
                left: -10px;
                bottom: -10px;
                right: -10px;
            }

            .serviceBox .service-icon:after {
                content: "";
                width: 4px;
                height: 0;
                background: #0fb513;
                margin: 0 auto;
                position: absolute;
                bottom: -55px;
                left: 0;
                right: 0;
                transition: all 0.3s ease 0s;
            }

        .serviceBox .title {
            font-size: 12px;
            font-weight: 600;
            letter-spacing: 1px;
            color: #000;
            text-transform: uppercase;
            margin: 0 0 10px 0;
            position: relative;
        }

        .serviceBox.pink .service-icon:before, .serviceBox.pink .service-icon:after {
            background: #d41271;
        }

        .serviceBox.pink .service-icon:before, .serviceBox.pink .service-icon:after {
            background: #d41271;
        }

        .serviceBox.yellow .service-icon:before, .serviceBox.yellow .service-icon:after {
            background: #fba21a;
        }

        .serviceBox.yellow .service-icon:before, .serviceBox.yellow .service-icon:after {
            background: #fba21a;
        }

        .serviceBox.blue .service-icon:before, .serviceBox.blue .service-icon:after {
            background: #05b4b7;
        }

        .serviceBox.blue .service-icon:before, .serviceBox.blue .service-icon:after {
            background: #05b4b7;
        }
    </style>
    <!-- /.box-body -->
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript" language="javascript">
        function toggleDashFeature(btn, cardId) {
            var card = document.getElementById(cardId);
            if (!card) return;
            var isOpen = card.classList.toggle("is-open");
            var label = btn.querySelector(".dash-feature-toggle-text");
            if (label) {
                label.textContent = isOpen ? "Hide" : "View";
            }
        }

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

        function CopyToClipboard() {


            /* Get the text field */
            var copyText = document.getElementById('<%=TxtLeftLinkLink.ClientID%>');

            /* Select the text field */
            copyText.select();

            /* Copy the text inside the text field */
            document.execCommand("Copy");

            /* Alert the copied text */
            alert("Copied the text: " + copyText.value);
        }
        function CopyToClipboard2() {


            /* Get the text field */
            var copyText1 = document.getElementById('<%=TxtRightLink.ClientID%>');

            /* Select the text field */
            copyText1.select();

            /* Copy the text inside the text field */
            document.execCommand("Copy");

            /* Alert the copied text */
            alert("Copied the text: " + copyText1.value);
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

</asp:Content>


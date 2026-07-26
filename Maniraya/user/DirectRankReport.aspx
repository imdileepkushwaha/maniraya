<%@ Page Title="My Direct Rank" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DirectRankReport.aspx.cs" Inherits="user_DirectRankReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <style>
        .dash-rank-page {
            --rank-gold: #e5a906;
            --rank-ink: #0f172a;
            --rank-muted: #64748b;
        }

        /* —— Rank hero (overrides default saving hero on this page only) —— */
        .dash-rank-page .dash-rank-hero.profile-hero,
        .dash-rank-page .dash-rank-hero.dash-subpage-hero,
        .dash-rank-page .dash-rank-hero.dash-subpage-hero--saving {
            display: grid;
            grid-template-columns: 1.15fr 0.85fr;
            gap: 0;
            align-items: stretch;
            padding: 0;
            overflow: hidden;
            border-radius: 22px;
            border: 1px solid rgba(229, 169, 6, 0.28);
            background:
                radial-gradient(ellipse 80% 70% at 100% 0%, rgba(251, 191, 36, 0.35) 0%, transparent 55%),
                radial-gradient(ellipse 60% 50% at 0% 100%, rgba(15, 23, 42, 0.08) 0%, transparent 50%),
                linear-gradient(125deg, #1a1408 0%, #2a2112 38%, #3d2e12 72%, #1f1810 100%);
            box-shadow: 0 22px 48px rgba(26, 20, 8, 0.28);
            position: relative;
        }
        .dash-rank-page .dash-rank-hero::before {
            content: "";
            position: absolute;
            inset: 0;
            background-image:
                linear-gradient(rgba(255, 255, 255, 0.03) 1px, transparent 1px),
                linear-gradient(90deg, rgba(255, 255, 255, 0.03) 1px, transparent 1px);
            background-size: 28px 28px;
            pointer-events: none;
            opacity: 0.45;
        }
        .dash-rank-page .dash-rank-hero .profile-hero-avatar {
            display: none;
        }
        .dash-rank-hero-main {
            position: relative;
            z-index: 1;
            padding: 28px 28px 26px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            gap: 18px;
            min-width: 0;
        }
        .dash-rank-hero-kicker {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            width: fit-content;
            padding: 6px 12px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 800;
            letter-spacing: 0.1em;
            text-transform: uppercase;
            color: #fde68a;
            background: rgba(253, 230, 138, 0.12);
            border: 1px solid rgba(253, 230, 138, 0.28);
        }
        .dash-rank-hero-kicker i { color: #fbbf24; }
        .dash-rank-page .dash-rank-hero .profile-hero-info {
            flex: none;
            min-width: 0;
        }
        .dash-rank-page .dash-rank-hero .profile-hero-info h2 {
            margin: 0 0 8px;
            font-size: clamp(1.55rem, 2.4vw, 2.05rem);
            font-weight: 800;
            letter-spacing: -0.02em;
            color: #fffbeb;
            line-height: 1.15;
        }
        .dash-rank-page .dash-rank-hero .profile-hero-meta {
            margin: 0;
            max-width: 36rem;
            font-size: 0.95rem;
            line-height: 1.55;
            color: rgba(254, 243, 199, 0.78);
        }
        .dash-rank-hero-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-top: 4px;
        }
        .dash-rank-page .dash-rank-hero .profile-hero-actions {
            display: none;
        }
        .dash-rank-page .dash-rank-hero .dash-rank-hero-actions .profile-btn {
            border-radius: 12px;
            padding: 10px 16px;
            font-weight: 700;
        }
        .dash-rank-page .dash-rank-hero .dash-rank-hero-actions .profile-btn-outline {
            background: rgba(255, 255, 255, 0.06);
            color: #fef3c7 !important;
            border: 1px solid rgba(253, 230, 138, 0.35);
        }
        .dash-rank-page .dash-rank-hero .dash-rank-hero-actions .profile-btn-outline:hover {
            background: rgba(253, 230, 138, 0.14);
            color: #fffbeb !important;
        }
        .dash-rank-page .dash-rank-hero .dash-rank-hero-actions .profile-btn-primary {
            background: linear-gradient(135deg, #fbbf24 0%, #e5a906 55%, #c98f05 100%);
            color: #1a1408 !important;
            border: none;
            box-shadow: 0 8px 20px rgba(229, 169, 6, 0.35);
        }

        .dash-rank-hero-side {
            position: relative;
            z-index: 1;
            padding: 22px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            gap: 14px;
            background:
                linear-gradient(160deg, rgba(255, 251, 235, 0.97) 0%, rgba(254, 243, 199, 0.92) 55%, rgba(253, 230, 138, 0.88) 100%);
            border-left: 1px solid rgba(229, 169, 6, 0.22);
        }
        .dash-rank-hero-side::before {
            content: "";
            position: absolute;
            top: -40px;
            right: -30px;
            width: 160px;
            height: 160px;
            border-radius: 50%;
            background: radial-gradient(circle, rgba(245, 158, 11, 0.35) 0%, transparent 70%);
            pointer-events: none;
        }

        .dash-rank-showcase {
            display: flex;
            flex-direction: column;
            gap: 14px;
            margin: 0;
            position: relative;
            z-index: 1;
        }
        .dash-rank-medal {
            position: relative;
            width: 100%;
            min-height: 148px;
            border-radius: 20px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            background: linear-gradient(160deg, #fff 0%, #fef3c7 100%);
            box-shadow: 0 14px 32px rgba(120, 53, 15, 0.14);
            border: 1px solid rgba(255, 255, 255, 0.9);
            overflow: hidden;
            padding: 18px 14px;
        }
        .dash-rank-medal::before {
            content: "";
            position: absolute;
            inset: 8px;
            border-radius: 14px;
            border: 1px dashed rgba(146, 64, 14, 0.22);
            pointer-events: none;
        }
        .dash-rank-medal i {
            font-size: 36px;
            color: #92400e;
            margin-bottom: 6px;
            z-index: 1;
        }
        .dash-rank-medal-label {
            position: relative;
            z-index: 1;
            font-size: 10px;
            font-weight: 800;
            letter-spacing: 0.14em;
            text-transform: uppercase;
            color: #92400e;
            opacity: 0.8;
        }
        .dash-rank-medal-name {
            position: relative;
            z-index: 1;
            margin-top: 2px;
            font-size: 1.45rem;
            font-weight: 800;
            color: #78350f;
            line-height: 1.15;
            text-align: center;
        }
        .dash-rank-medal.is-member {
            background: linear-gradient(160deg, #fff 0%, #e2e8f0 100%);
            box-shadow: 0 12px 28px rgba(100, 116, 139, 0.14);
        }
        .dash-rank-medal.is-member i,
        .dash-rank-medal.is-member .dash-rank-medal-label,
        .dash-rank-medal.is-member .dash-rank-medal-name { color: #475569; }

        .dash-rank-medal.is-distributor {
            background: linear-gradient(160deg, #fff 0%, #bfdbfe 100%);
            box-shadow: 0 14px 32px rgba(37, 99, 235, 0.16);
        }
        .dash-rank-medal.is-distributor i,
        .dash-rank-medal.is-distributor .dash-rank-medal-label,
        .dash-rank-medal.is-distributor .dash-rank-medal-name { color: #1e40af; }

        .dash-rank-medal.is-bronze {
            background: linear-gradient(160deg, #fff 0%, #fdba74 100%);
            box-shadow: 0 14px 32px rgba(194, 65, 12, 0.16);
        }
        .dash-rank-medal.is-bronze i,
        .dash-rank-medal.is-bronze .dash-rank-medal-label,
        .dash-rank-medal.is-bronze .dash-rank-medal-name { color: #9a3412; }

        .dash-rank-medal.is-silver {
            background: linear-gradient(160deg, #fff 0%, #cbd5e1 100%);
            box-shadow: 0 14px 32px rgba(71, 85, 105, 0.14);
        }
        .dash-rank-medal.is-silver i,
        .dash-rank-medal.is-silver .dash-rank-medal-label,
        .dash-rank-medal.is-silver .dash-rank-medal-name { color: #334155; }

        .dash-rank-medal.is-gold {
            background: linear-gradient(160deg, #fffbeb 0%, #fbbf24 55%, #f59e0b 100%);
        }
        .dash-rank-medal.is-diamond {
            background: linear-gradient(160deg, #fff 0%, #67e8f9 55%, #22d3ee 100%);
            box-shadow: 0 14px 32px rgba(8, 145, 178, 0.18);
        }
        .dash-rank-medal.is-diamond i,
        .dash-rank-medal.is-diamond .dash-rank-medal-label,
        .dash-rank-medal.is-diamond .dash-rank-medal-name { color: #0e7490; }

        .dash-rank-metrics {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr));
            gap: 8px;
        }
        .dash-rank-metric {
            flex: none;
            min-width: 0;
            background: rgba(255, 255, 255, 0.78);
            border: 1px solid rgba(120, 53, 15, 0.1);
            border-radius: 14px;
            padding: 12px 10px;
            text-align: center;
            box-shadow: 0 6px 14px rgba(120, 53, 15, 0.06);
        }
        .dash-rank-metric span {
            display: block;
            font-size: 10px;
            font-weight: 800;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #92400e;
            opacity: 0.75;
        }
        .dash-rank-metric strong {
            display: block;
            margin-top: 4px;
            font-size: 1.1rem;
            font-weight: 800;
            color: #78350f;
            line-height: 1.2;
            word-break: break-word;
        }

        @media (max-width: 900px) {
            .dash-rank-page .dash-rank-hero.profile-hero,
            .dash-rank-page .dash-rank-hero.dash-subpage-hero,
            .dash-rank-page .dash-rank-hero.dash-subpage-hero--saving {
                grid-template-columns: 1fr;
            }
            .dash-rank-hero-side {
                border-left: none;
                border-top: 1px solid rgba(229, 169, 6, 0.22);
            }
            .dash-rank-metrics {
                grid-template-columns: repeat(3, minmax(0, 1fr));
            }
        }
        @media (max-width: 520px) {
            .dash-rank-hero-main { padding: 22px 18px; }
            .dash-rank-hero-side { padding: 16px; }
            .dash-rank-metrics { grid-template-columns: 1fr; }
            .dash-rank-metric { text-align: left; padding: 12px 14px; }
        }

        .dash-rank-progress-card {
            display: grid;
            grid-template-columns: 1fr auto;
            gap: 20px;
            align-items: center;
        }
        @media (max-width: 640px) {
            .dash-rank-progress-card { grid-template-columns: 1fr; }
            .dash-rank-remain-pill { justify-self: start; }
        }
        .dash-rank-progress-title {
            margin: 0 0 6px;
            font-size: 15px;
            font-weight: 700;
            color: #0f172a;
        }
        .dash-rank-progress-sub {
            margin: 0 0 14px;
            font-size: 13px;
            color: #64748b;
        }
        .dash-rank-progress-bar {
            height: 14px;
            border-radius: 999px;
            background: #e2e8f0;
            overflow: hidden;
            box-shadow: inset 0 1px 2px rgba(15, 23, 42, 0.06);
        }
        .dash-rank-progress-fill {
            height: 100%;
            border-radius: 999px;
            background: linear-gradient(90deg, #f59e0b 0%, #e5a906 50%, #fbbf24 100%);
            box-shadow: 0 0 12px rgba(229, 169, 6, 0.45);
            transition: width 0.45s ease;
            min-width: 0;
        }
        .dash-rank-progress-foot {
            display: flex;
            flex-wrap: wrap;
            gap: 10px 16px;
            margin-top: 12px;
            font-size: 13px;
            color: #475569;
        }
        .dash-rank-progress-foot strong { color: #0f172a; }
        .dash-rank-remain-pill {
            min-width: 120px;
            padding: 16px 18px;
            border-radius: 18px;
            background: linear-gradient(145deg, #fffbeb, #fef3c7);
            border: 1px solid #fde68a;
            text-align: center;
        }
        .dash-rank-remain-pill span {
            display: block;
            font-size: 11px;
            font-weight: 800;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: #92400e;
        }
        .dash-rank-remain-pill strong {
            display: block;
            margin-top: 4px;
            font-size: 2rem;
            font-weight: 800;
            color: #78350f;
            line-height: 1;
        }
        .dash-rank-remain-pill em {
            display: block;
            margin-top: 6px;
            font-style: normal;
            font-size: 12px;
            color: #a16207;
        }

        .dash-rank-ladder-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
            gap: 14px;
        }
        .dash-rank-step {
            position: relative;
            border-radius: 18px;
            padding: 18px 16px 16px;
            background: #fff;
            border: 1px solid #e2e8f0;
            box-shadow: 0 8px 20px rgba(15, 23, 42, 0.04);
            transition: transform 0.15s ease, box-shadow 0.2s ease;
        }
        .dash-rank-step:hover {
            transform: translateY(-2px);
            box-shadow: 0 14px 28px rgba(15, 23, 42, 0.08);
        }
        .dash-rank-step.is-current {
            border-color: #fbbf24;
            background: linear-gradient(180deg, #fffbeb 0%, #fff 70%);
            box-shadow: 0 14px 32px rgba(245, 158, 11, 0.16);
        }
        .dash-rank-step.is-achieved {
            border-color: #bbf7d0;
            background: linear-gradient(180deg, #f0fdf4 0%, #fff 75%);
        }
        .dash-rank-step.is-locked {
            opacity: 0.72;
            background: #f8fafc;
        }
        .dash-rank-step-top {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            gap: 10px;
            margin-bottom: 12px;
        }
        .dash-rank-step-icon {
            width: 44px;
            height: 44px;
            border-radius: 14px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 18px;
            color: #fff;
            background: linear-gradient(135deg, #94a3b8, #64748b);
        }
        .dash-rank-step-icon.tone-member { background: linear-gradient(135deg, #94a3b8, #64748b); }
        .dash-rank-step-icon.tone-distributor { background: linear-gradient(135deg, #60a5fa, #2563eb); }
        .dash-rank-step-icon.tone-bronze { background: linear-gradient(135deg, #fb923c, #c2410c); }
        .dash-rank-step-icon.tone-silver { background: linear-gradient(135deg, #cbd5e1, #475569); }
        .dash-rank-step-icon.tone-gold { background: linear-gradient(135deg, #fbbf24, #d97706); }
        .dash-rank-step-icon.tone-diamond { background: linear-gradient(135deg, #22d3ee, #0891b2); }

        .dash-rank-step h4 {
            margin: 0 0 4px;
            font-size: 1.05rem;
            font-weight: 800;
            color: #0f172a;
        }
        .dash-rank-step p {
            margin: 0;
            font-size: 12px;
            color: #64748b;
            line-height: 1.45;
        }
        .dash-rank-step-foot {
            margin-top: 14px;
            padding-top: 12px;
            border-top: 1px dashed #e2e8f0;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 8px;
            font-size: 12px;
            color: #64748b;
        }
        .dash-rank-step-foot strong { color: #0f172a; }

        .dash-rank-badge {
            display: inline-flex;
            align-items: center;
            padding: 4px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 800;
            border: 1px solid transparent;
            white-space: nowrap;
        }
        .dash-rank-badge.is-current {
            background: #fff7ed;
            color: #c2410c;
            border-color: #fed7aa;
        }
        .dash-rank-badge.is-achieved {
            background: #ecfdf5;
            color: #047857;
            border-color: #a7f3d0;
        }
        .dash-rank-badge.is-locked {
            background: #f1f5f9;
            color: #94a3b8;
            border-color: #e2e8f0;
        }

        .dash-rank-rules-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(160px, 1fr));
            gap: 10px;
        }
        .dash-rank-rule {
            border-radius: 14px;
            padding: 14px 12px;
            border: 1px solid #e2e8f0;
            background: #fff;
        }
        .dash-rank-rule strong {
            display: block;
            font-size: 14px;
            font-weight: 800;
            color: #0f172a;
            margin-bottom: 4px;
        }
        .dash-rank-rule span {
            font-size: 12px;
            color: #64748b;
        }
        .dash-rank-rule.tone-distributor { border-color: #bfdbfe; background: #eff6ff; }
        .dash-rank-rule.tone-bronze { border-color: #fed7aa; background: #fff7ed; }
        .dash-rank-rule.tone-silver { border-color: #cbd5e1; background: #f8fafc; }
        .dash-rank-rule.tone-gold { border-color: #fde68a; background: #fffbeb; }
        .dash-rank-rule.tone-diamond { border-color: #a5f3fc; background: #ecfeff; }

        .dash-rank-note {
            margin: 14px 0 0;
            font-size: 12px;
            color: #64748b;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>My Direct Rank</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">My Team</a></li>
            <li class="active">My Direct Rank</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page dash-rank-page">
        <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving dash-rank-hero">
            <div class="dash-rank-hero-main">
                <div class="profile-hero-info">
                    <span class="dash-rank-hero-kicker"><i class="fa fa-trophy"></i> Direct Active Rank</span>
                    <h2>My Direct Rank</h2>
                    <p class="profile-hero-meta">Live rank from your Active Directs. Climb the ladder and unlock the next badge — no closing wait.</p>
                </div>
                <div class="dash-rank-hero-actions">
                    <a href="UserDirectAssociates.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-users"></i> My Direct</a>
                    <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-tachometer-alt"></i> Dashboard</a>
                </div>
            </div>
            <div class="dash-rank-hero-side">
                <div class="dash-rank-showcase">
                    <div id="divRankMedal" runat="server" class="dash-rank-medal is-member">
                        <i id="icoRankMedal" runat="server" class="fa fa-user"></i>
                        <span class="dash-rank-medal-label">Current Rank</span>
                        <div class="dash-rank-medal-name"><asp:Label ID="lblCurrentRank" runat="server" Text="Member" /></div>
                    </div>
                    <div class="dash-rank-metrics">
                        <div class="dash-rank-metric">
                            <span>Active Directs</span>
                            <strong><asp:Label ID="lblActiveDirects" runat="server" Text="0" /></strong>
                        </div>
                        <div class="dash-rank-metric">
                            <span>Next Target</span>
                            <strong><asp:Label ID="lblNextRank" runat="server" Text="-" /></strong>
                        </div>
                        <div class="dash-rank-metric">
                            <span>Still Need</span>
                            <strong><asp:Label ID="lblRemainingHero" runat="server" Text="0" /></strong>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-trophy"></i></div>
            <div class="profile-hero-actions" aria-hidden="true"></div>
        </div>

        <div class="dash-subpage-panel dash-saving-report-panel">
            <div class="dash-subpage-panel-head">
                <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-chart-line"></i></span>
                <div>
                    <h3>Next Rank Progress</h3>
                    <p>Track how close you are to the next badge</p>
                </div>
            </div>
            <div class="dash-subpage-panel-body">
                <div class="dash-rank-progress-card">
                    <div>
                        <p class="dash-rank-progress-title"><asp:Label ID="lblProgressSummary" runat="server" Text="" /></p>
                        <p class="dash-rank-progress-sub">Progress updates instantly when a direct becomes Active (SavingStatus = 1).</p>
                        <div class="dash-rank-progress-bar">
                            <div id="divProgressFill" runat="server" class="dash-rank-progress-fill" style="width:0%"></div>
                        </div>
                        <div class="dash-rank-progress-foot">
                            <span>Progress: <strong><asp:Label ID="lblProgressPct" runat="server" Text="0" />%</strong></span>
                            <span>Remaining: <strong><asp:Label ID="lblRemaining" runat="server" Text="0" /></strong></span>
                        </div>
                    </div>
                    <div class="dash-rank-remain-pill">
                        <span>To Go</span>
                        <strong><asp:Label ID="lblRemainingBig" runat="server" Text="0" /></strong>
                        <em><asp:Label ID="lblRemainHint" runat="server" Text="Active Directs" /></em>
                    </div>
                </div>
            </div>
        </div>

        <div class="dash-subpage-panel dash-saving-report-panel">
            <div class="dash-subpage-panel-head">
                <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-layer-group"></i></span>
                <div>
                    <h3>Rank Ladder</h3>
                    <p>Every milestone from Member to Diamond</p>
                </div>
            </div>
            <div class="dash-subpage-panel-body">
                <div class="dash-rank-ladder-grid">
                    <asp:Repeater ID="rptLadder" runat="server" OnItemDataBound="rptLadder_ItemDataBound">
                        <ItemTemplate>
                            <div runat="server" id="divStep" class="dash-rank-step">
                                <div class="dash-rank-step-top">
                                    <span runat="server" id="spIcon" class="dash-rank-step-icon"><i class="fa fa-star"></i></span>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                </div>
                                <h4><%# Eval("RankName") %></h4>
                                <p><%# Eval("RequiredLabel") %></p>
                                <div class="dash-rank-step-foot">
                                    <span>Still need</span>
                                    <strong><%# Convert.ToInt32(Eval("Remaining")) > 0 ? Eval("Remaining") + " more" : "Done" %></strong>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <div class="dash-subpage-panel dash-saving-report-panel">
            <div class="dash-subpage-panel-head">
                <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-info-circle"></i></span>
                <div>
                    <h3>How Rank Works</h3>
                    <p>Active Direct = your direct with Saving Status Active</p>
                </div>
            </div>
            <div class="dash-subpage-panel-body">
                <div class="dash-rank-rules-grid">
                    <div class="dash-rank-rule tone-distributor">
                        <strong>Distributor</strong>
                        <span>1 – 9 Active Directs</span>
                    </div>
                    <div class="dash-rank-rule tone-bronze">
                        <strong>Bronze</strong>
                        <span>10 – 24 Active Directs</span>
                    </div>
                    <div class="dash-rank-rule tone-silver">
                        <strong>Silver</strong>
                        <span>25 – 49 Active Directs</span>
                    </div>
                    <div class="dash-rank-rule tone-gold">
                        <strong>Gold</strong>
                        <span>50 – 99 Active Directs</span>
                    </div>
                    <div class="dash-rank-rule tone-diamond">
                        <strong>Diamond</strong>
                        <span>100+ Active Directs</span>
                    </div>
                </div>
                <p class="dash-rank-note">Rank is calculated live — no closing wait. Existing income Rank report remains separate.</p>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="Top Direct Ranking" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TopDirectRanking.aspx.cs" Inherits="user_TopDirectRanking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <style>
        .dash-saving-filter-row {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            align-items: flex-end;
            margin-bottom: 16px;
        }
        .dash-saving-filter-row .form-group {
            margin: 0;
            min-width: 160px;
            flex: 1 1 160px;
        }
        .dash-saving-filter-row label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 700;
            color: #64748b;
        }
        .dash-rank-badge {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 36px;
            height: 28px;
            padding: 0 10px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 800;
            background: #f1f5f9;
            color: #334155;
            border: 1px solid #e2e8f0;
        }
        .dash-rank-badge.is-gold {
            background: #fffbeb;
            color: #b45309;
            border-color: #fde68a;
        }
        .dash-rank-badge.is-silver {
            background: #f8fafc;
            color: #475569;
            border-color: #cbd5e1;
        }
        .dash-rank-badge.is-bronze {
            background: #fff7ed;
            color: #c2410c;
            border-color: #fed7aa;
        }
        .dash-direct-count {
            display: inline-block;
            font-weight: 800;
            color: #0f766e;
            font-size: 15px;
        }
        .dash-rank-self-row td {
            background: #ecfdf5 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Top Direct Ranking</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">My Team</a></li>
            <li class="active">Top Direct Ranking</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-trophy"></i></div>
                    <div class="profile-hero-info">
                        <h2>Top Direct Ranking</h2>
                        <p class="profile-hero-meta">Overall leaderboard of members with the highest total direct referrals.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserDirectAssociates.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-users"></i> My Direct</a>
                        <a href="DownlineReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-level-down-alt"></i> My Downline</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head dash-income-report-results-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-list-ol"></i></span>
                        <div class="dash-income-report-results-copy">
                            <h3>Overall Ranking</h3>
                            <asp:Label ID="lblResultSummary" runat="server" CssClass="dash-income-report-summary" Text="Loading ranking..." />
                        </div>
                        <div class="dash-income-report-filter">
                            <label for="<%= ddTopCount.ClientID %>">Show Top</label>
                            <asp:DropDownList ID="ddTopCount" runat="server" CssClass="form-control soh-records-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddTopCount_SelectedIndexChanged">
                                <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <asp:Panel ID="pnlMyRank" runat="server" Visible="false" CssClass="profile-alert" style="margin-bottom:16px;">
                            <i class="fa fa-user"></i>
                            <asp:Literal ID="litMyRank" runat="server" />
                        </asp:Panel>

                        <p class="dash-saving-report-intro">Ranking is based on total <strong>Active</strong> direct members (<code>ActiveStatus = 1</code>) sponsored under each Active user (overall / all-time).</p>

                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None" EmptyDataText=""
                                OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRank" runat="server" CssClass="dash-rank-badge"
                                                Text='<%# Eval("RankNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <strong><%# Eval("userid") %></strong>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <span class="dash-income-member-name"><%# Eval("username") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Directs">
                                        <ItemTemplate>
                                            <span class="dash-direct-count"><%# Eval("DirectCount") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-trophy"></i>
                                        <h4>No ranking data found</h4>
                                        <p>Direct referral ranking will appear here once members start sponsoring others.</p>
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
</asp:Content>

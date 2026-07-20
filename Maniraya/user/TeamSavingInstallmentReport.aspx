<%@ Page Title="Team Installment Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TeamSavingInstallmentReport.aspx.cs" Inherits="user_TeamSavingInstallmentReport" %>

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
        .dash-income-level-badge {
            display: inline-block;
            padding: 3px 10px;
            border-radius: 999px;
            background: #eff6ff;
            color: #1d4ed8;
            border: 1px solid #bfdbfe;
            font-size: 12px;
            font-weight: 700;
        }
        .dash-saving-report-table .dash-saving-action-btn.is-view {
            min-width: 148px;
            white-space: nowrap;
            color: #fff !important;
            background: linear-gradient(135deg, #2563eb 0%, #1d4ed8 100%) !important;
            border: none;
            box-shadow: 0 4px 14px rgba(37, 99, 235, 0.24);
        }
        .dash-saving-report-table .dash-saving-action-btn.is-view:hover,
        .dash-saving-report-table .dash-saving-action-btn.is-view:focus {
            color: #fff !important;
            text-decoration: none;
            transform: translateY(-1px);
            box-shadow: 0 8px 18px rgba(37, 99, 235, 0.28);
        }
        .dash-saving-report-table .dash-saving-action-btn.is-view i {
            color: #fff !important;
        }
        .soh-pager-bar .saving-pager-btn.is-active {
            color: #fff;
            background: #ef4444;
            border-color: #ef4444;
        }
        .soh-pager-bar .saving-pager-btn.is-ellipsis {
            cursor: default;
            color: #64748b;
            background: #fff;
            border-color: #e2e8f0;
        }
        .soh-pager-bar .saving-pager-btn.is-disabled {
            color: #cbd5e1;
            background: #fff;
            border-color: #e2e8f0;
            opacity: 1;
        }
        .dash-team-saving-status {
            display: inline-block;
            padding: 3px 10px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 700;
            border: 1px solid transparent;
        }
        .dash-team-saving-status.is-active {
            background: #ecfdf5;
            color: #047857;
            border-color: #a7f3d0;
        }
        .dash-team-saving-status.is-inactive {
            background: #fef2f2;
            color: #b91c1c;
            border-color: #fecaca;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Team Installment Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Team Installment Report</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-sitemap"></i></div>
                    <div class="profile-hero-info">
                        <h2>Team Installment Report</h2>
                        <p class="profile-hero-meta">View your sponsor-wise team up to 10 levels and check each member's paid / unpaid installment status (view only).</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SavingProductInstallmentList.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-calendar"></i> My Installments</a>
                        <a href="DownlineReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-users"></i> My Team</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-filter"></i></span>
                        <div>
                            <h3>Search Team</h3>
                            <p>Filter by level or member details within your 10-level sponsor network</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="dash-saving-filter-row">
                            <div class="form-group">
                                <label for="<%= ddLevel.ClientID %>">Level</label>
                                <asp:DropDownList ID="ddLevel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddLevel_SelectedIndexChanged">
                                    <asp:ListItem Value="">All Levels (1-10)</asp:ListItem>
                                    <asp:ListItem Value="1">Level 1</asp:ListItem>
                                    <asp:ListItem Value="2">Level 2</asp:ListItem>
                                    <asp:ListItem Value="3">Level 3</asp:ListItem>
                                    <asp:ListItem Value="4">Level 4</asp:ListItem>
                                    <asp:ListItem Value="5">Level 5</asp:ListItem>
                                    <asp:ListItem Value="6">Level 6</asp:ListItem>
                                    <asp:ListItem Value="7">Level 7</asp:ListItem>
                                    <asp:ListItem Value="8">Level 8</asp:ListItem>
                                    <asp:ListItem Value="9">Level 9</asp:ListItem>
                                    <asp:ListItem Value="10">Level 10</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="<%= ddSavingStatus.ClientID %>">Saving Status</label>
                                <asp:DropDownList ID="ddSavingStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddSavingStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="">All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtUserId.ClientID %>">User ID</label>
                                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Search user id" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtUserName.ClientID %>">User Name</label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Search name" />
                            </div>
                            <div class="form-group" style="flex: 0 0 auto;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="profile-btn profile-btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="profile-btn profile-btn-outline" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head dash-income-report-results-head">
                        <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-users"></i></span>
                        <div class="dash-income-report-results-copy">
                            <h3>Team Members</h3>
                            <asp:Label ID="lblResultSummary" runat="server" CssClass="dash-income-report-summary" Text="Loading your sponsor-wise team..." />
                        </div>
                        <div class="dash-income-report-filter">
                            <label for="<%= ddlRecordFilter.ClientID %>">Show</label>
                            <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control soh-records-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Status is based on <strong>SavingStatus</strong>. <strong>Installment Detail</strong> is available only for Active members. Payment is not allowed from this report.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None" EmptyDataText=""
                                OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# GetSerialNumber(Container.DataItemIndex) %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level">
                                        <ItemTemplate>
                                            <span class="dash-income-level-badge">L<%# Eval("userlevel") %></span>
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
                                    <asp:TemplateField HeaderText="Mobile Number">
                                        <ItemTemplate><%# Eval("mobile") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID">
                                        <ItemTemplate><%# Eval("email") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSavingStatus" runat="server" CssClass="dash-team-saving-status"
                                                Text='<%# Eval("StatusDisplay") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:PlaceHolder ID="phInstallmentDetail" runat="server"
                                                Visible='<%# Convert.ToString(Eval("savingstatus")) == "1" %>'>
                                                <a href='TeamMemberInstallmentView.aspx?uid=<%# Eval("userid") %>' class="dash-saving-action-btn is-view">
                                                    <i class="fa fa-eye"></i> Installment Detail
                                                </a>
                                            </asp:PlaceHolder>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-users"></i>
                                        <h4>No team members found</h4>
                                        <p>Your sponsor-wise team (up to 10 levels) will appear here once members join under you.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <asp:Panel ID="pnlPager" runat="server" CssClass="soh-pager-bar"></asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

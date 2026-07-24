<%@ Page Title="Team Pending Installment" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TeamPendingInstallmentReport.aspx.cs" Inherits="user_TeamPendingInstallmentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <link href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style>
        .dash-saving-filter-row {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            align-items: flex-end;
            margin-bottom: 12px;
        }
        .dash-saving-filter-row .form-group {
            margin: 0;
            min-width: 150px;
            flex: 1 1 150px;
        }
        .dash-saving-filter-row label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 700;
            color: #64748b;
        }
        .dash-saving-filter-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            align-items: center;
            padding-top: 4px;
            margin-top: 4px;
            border-top: 1px solid #e2e8f0;
        }
        .dash-filter-btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            min-width: 110px;
            padding: 10px 18px;
            border-radius: 10px;
            font-size: 0.86rem;
            font-weight: 700;
            border: 1px solid transparent;
            cursor: pointer;
            white-space: nowrap;
        }
        .dash-filter-btn-primary {
            color: #1a1d21;
            background: linear-gradient(135deg, #e5a906 0%, #c98f05 100%);
            box-shadow: 0 6px 18px rgba(229, 169, 6, 0.28);
        }
        .dash-filter-btn-clear {
            color: #334155;
            background: #fff;
            border-color: #cbd5e1;
        }
        .dash-filter-btn-clear:hover {
            background: #f8fafc;
            border-color: #94a3b8;
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
        .dash-pending-status {
            display: inline-block;
            padding: 3px 10px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 700;
            border: 1px solid transparent;
        }
        .dash-pending-status.is-pending {
            background: #fff7ed;
            color: #c2410c;
            border-color: #fed7aa;
        }
        .dash-pending-status.is-processing {
            background: #eff6ff;
            color: #1d4ed8;
            border-color: #bfdbfe;
        }
        .dash-pending-status.is-approved {
            background: #ecfdf5;
            color: #047857;
            border-color: #a7f3d0;
        }
        .dash-pending-status.is-rejected {
            background: #fef2f2;
            color: #b91c1c;
            border-color: #fecaca;
        }
        .soh-pager-bar .saving-pager-btn.is-active {
            color: #fff;
            background: #ef4444;
            border-color: #ef4444;
        }
        .soh-pager-bar .saving-pager-btn.is-ellipsis,
        .soh-pager-bar .saving-pager-btn.is-disabled {
            cursor: default;
            color: #cbd5e1;
            background: #fff;
            border-color: #e2e8f0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Team Pending Installment</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Team Pending Installment</li>
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
                        <h2>Team Pending Installment</h2>
                        <p class="profile-hero-meta">Only your own sponsor team (up to 10 levels). Outside-team members are never shown.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="TeamSavingInstallmentReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-users"></i> Team Report</a>
                        <a href="SavingProductInstallmentList.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-calendar"></i> My Installments</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-filter"></i></span>
                        <div>
                            <h3>Search Filters</h3>
                            <p>Filter by level, installment date, user and status</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="dash-saving-filter-row">
                            <div class="form-group">
                                <label for="<%= ddLevel.ClientID %>">Level</label>
                                <asp:DropDownList ID="ddLevel" runat="server" CssClass="form-control">
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
                                <label for="<%= txtFromDate.ClientID %>">From Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form_date" placeholder="dd/mm/yyyy" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtToDate.ClientID %>">To Date</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form_date" placeholder="dd/mm/yyyy" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtUserId.ClientID %>">User ID</label>
                                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Exact team user id" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtInstNo.ClientID %>">Installment No</label>
                                <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" placeholder="e.g. 2" />
                            </div>
                            <div class="form-group">
                                <label for="<%= ddStatus.ClientID %>">Status</label>
                                <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
                                    <asp:ListItem Value="">All Status</asp:ListItem>
                                    <asp:ListItem Value="Processing">Processing</asp:ListItem>
                                    <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                    <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-saving-filter-actions">
                            <asp:Button ID="btnSearch" runat="server" CssClass="dash-filter-btn dash-filter-btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="dash-filter-btn dash-filter-btn-clear" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head dash-income-report-results-head">
                        <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-list-ol"></i></span>
                        <div class="dash-income-report-results-copy">
                            <h3>Pending Installments</h3>
                            <asp:Label ID="lblResultSummary" runat="server" CssClass="dash-income-report-summary" Text="Loading team pending installments..." />
                        </div>
                        <div class="dash-income-report-filter">
                            <label for="<%= ddlRecordFilter.ClientID %>">Show</label>
                            <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control soh-records-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem Selected="True">25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Sirf aapki <strong>apni sponsor team</strong> (Level 1–10) ke installment. Aapka khud ka record aur team ke bahar koi user yahan nahi dikhega.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None" EmptyDataText=""
                                AllowPaging="true" PageSize="25"
                                OnPageIndexChanging="GridView1_PageIndexChanging"
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
                                        <ItemTemplate><strong><%# Eval("userid") %></strong></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate><span class="dash-income-member-name"><%# Eval("username") %></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate><%# Eval("mobile") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Installment Date">
                                        <ItemTemplate><%# Eval("installmentdate", "{0:dd/MM/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inst No">
                                        <ItemTemplate><%# Eval("instno") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate><span class="dash-direct-count"><%# Eval("amount", "{0:N2}") %></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-calendar-check"></i>
                                        <h4>No installment records found</h4>
                                        <p>Pending installments of your team members will appear here for the selected filters.</p>
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
    <script type="text/javascript">
        // contentScript renders before jQuery in MasterPage — never call $ at top-level here.
        Sys.Application.add_load(function () {
            if (typeof jQuery === 'undefined') { return; }

            function bindTeamPendingDates() {
                jQuery('.form_date').each(function () {
                    var $input = jQuery(this);
                    if ($input.data('teamPendingDp')) { return; }
                    $input.data('teamPendingDp', true);
                    $input.datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        todayHighlight: true
                    }).on('changeDate', function () {
                        jQuery(this).datepicker('hide');
                    });
                });
            }

            function loadBootstrapDatepicker(done) {
                // bootstrap-datepicker exposes .dates; jquery-ui does not
                if (jQuery.fn.datepicker && jQuery.fn.datepicker.dates) {
                    done();
                    return;
                }
                var existing = document.getElementById('teamPendingBootstrapDp');
                if (existing) {
                    existing.addEventListener('load', done);
                    return;
                }
                var s = document.createElement('script');
                s.id = 'teamPendingBootstrapDp';
                s.src = '../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js';
                s.onload = done;
                document.body.appendChild(s);
            }

            loadBootstrapDatepicker(bindTeamPendingDates);
        });
    </script>
</asp:Content>

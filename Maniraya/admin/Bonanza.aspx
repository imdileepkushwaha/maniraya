<%@ Page Title="Bonanza" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="Bonanza.aspx.cs" Inherits="admin_Bonanza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=74" />
    <style>
        .bonanza-rules {
            margin: 0 0 16px;
            padding: 14px 16px;
            border-radius: 12px;
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.12) 0%, rgba(229, 169, 6, 0.04) 100%);
            border: 1px solid rgba(229, 169, 6, 0.28);
            color: #78350f;
            font-size: 13px;
            line-height: 1.55;
        }

        .bonanza-rules strong {
            color: #92400e;
        }

        .bonanza-badge {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 4px 10px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 700;
            line-height: 1.2;
            white-space: nowrap;
        }

        .bonanza-badge.is-yes {
            background: #ecfdf5;
            color: #047857;
            border: 1px solid #a7f3d0;
        }

        .bonanza-badge.is-no {
            background: #fef2f2;
            color: #b91c1c;
            border: 1px solid #fecaca;
        }

        .admin-report-page .admin-table-pager-bar {
            display: flex !important;
            align-items: center;
            justify-content: center;
            flex-wrap: wrap;
            gap: 6px;
            padding: 14px 16px;
            border-top: 1px solid #e8edf3;
            background: #f8fafc;
        }

        .admin-report-page .admin-pager-info {
            width: 100%;
            margin-bottom: 6px;
            text-align: center;
            font-size: 12px;
            font-weight: 600;
            color: #64748b;
        }

        .admin-report-page .admin-pager-btn,
        .admin-report-page a.admin-pager-btn {
            display: inline-flex !important;
            align-items: center;
            justify-content: center;
            min-width: 38px;
            height: 36px;
            padding: 0 12px;
            margin: 0;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            line-height: 1;
            color: #475569 !important;
            text-decoration: none !important;
            background: #fff !important;
            border: 1px solid #e2e8f0 !important;
            cursor: pointer;
        }

        .admin-report-page .admin-pager-btn.is-active {
            color: #fff !important;
            background: #ef4444 !important;
            border-color: #ef4444 !important;
        }

        .admin-report-page .admin-pager-btn.is-disabled,
        .admin-report-page .admin-pager-btn.is-ellipsis {
            color: #cbd5e1 !important;
            cursor: default;
            pointer-events: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Bonanza</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Prize</a></li>
            <li class="active">Bonanza</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-sitemap"></i> Bonanza Check</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <div class="bonanza-rules">
                                    <strong>Rules:</strong>
                                    Active user ke niche selected <strong>Qualifying Legs</strong> chahiye.
                                    Har leg ke niche minimum <strong>10 active</strong> hone chahiye — kam hone par woh leg count nahi hogi.
                                    Overall team active <strong>Team Active Required</strong> se kam nahi hona chahiye.
                                    Optional <strong>Team Active Required (Max)</strong> set ho to usse zyada team active qualify nahi hoga.
                                    <strong>MP000001</strong> (admin) poori calculation se exclude hai.
                                    Active = <strong>SavingStatus = 1</strong>.
                                </div>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Filters</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtUserId.ClientID %>">User ID / Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Search user id or name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddlStatus.ClientID %>">Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="All" Value="" Selected="True" />
                                                    <asp:ListItem Text="Qualified Only" Value="Qualified" />
                                                    <asp:ListItem Text="Not Qualified" Value="NotQualified" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtMinDirects.ClientID %>">Min Active Directs (pre-filter)</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtMinDirects" runat="server" CssClass="form-control" Text="10" placeholder="e.g. 10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtQualifyingLegs.ClientID %>">Qualifying Legs Required</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-check-circle"></i></span>
                                                    <asp:TextBox ID="txtQualifyingLegs" runat="server" CssClass="form-control" Text="10" placeholder="e.g. 10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtTeamActive.ClientID %>">Team Active Required (Min)</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                                    <asp:TextBox ID="txtTeamActive" runat="server" CssClass="form-control" Text="500" placeholder="e.g. 500 / 1000" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtMaxTeamActive.ClientID %>">Team Active Required (Max)</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-filter"></i></span>
                                                    <asp:TextBox ID="txtMaxTeamActive" runat="server" CssClass="form-control" Text="" placeholder="Optional e.g. 2000" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-trophy"></i> Bonanza Status</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <asp:ImageButton ID="imgExcel" runat="server" CssClass="admin-export-excel-btn"
                                        ImageUrl="../user/img/excel123.png" Height="25px" Width="25px"
                                        OnClick="imgExcel_Click" ToolTip="Export to Excel" />
                                    <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem Selected="True">25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="admin-record-filter-suffix">records</span>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:Panel ID="pnlLoadError" runat="server" Visible="false" CssClass="alert alert-warning admin-report-alert">
                                    <i class="fa fa-exclamation-triangle"></i>
                                    <asp:Literal ID="litLoadError" runat="server" />
                                </asp:Panel>

                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-list"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Run search to check Bonanza qualification." />
                                    </span>
                                </div>

                                <div class="admin-table-paged-shell">
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="GridView1" runat="server"
                                            CssClass="table table-bordered table-hover dataTable"
                                            Width="100%" AutoGenerateColumns="False" GridLines="None"
                                            AllowPaging="true" PageSize="25"
                                            EmptyDataText="No users found for selected filters."
                                            OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnRowCommand="GridView1_RowCommand">
                                            <PagerSettings Visible="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="userid" HeaderText="User ID" />
                                                <asp:BoundField DataField="username" HeaderText="Name" />
                                                <asp:BoundField DataField="mobile" HeaderText="Mobile" />
                                                <asp:BoundField DataField="activedirects" HeaderText="Active Directs" />
                                                <asp:BoundField DataField="qualifyinglegs" HeaderText="Qualifying Legs" />
                                                <asp:BoundField DataField="teamactive" HeaderText="Team Active" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <span class='bonanza-badge <%# Convert.ToBoolean(Eval("isqualified")) ? "is-yes" : "is-no" %>'>
                                                            <i class='fa <%# Convert.ToBoolean(Eval("isqualified")) ? "fa-check" : "fa-times" %>'></i>
                                                            <%# Eval("statuslabel") %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Legs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkLegs" runat="server" CssClass="admin-action-btn is-view"
                                                            CommandName="viewlegs" CommandArgument='<%# Eval("userid") %>'
                                                            OnClick="lnkLegs_Click" CausesValidation="false">
                                                            <i class="fa fa-sitemap"></i> View
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar"></asp:Panel>
                                </div>

                                <asp:Panel ID="pnlLegs" runat="server" Visible="false" style="margin-top:20px;">
                                    <div class="admin-table-toolbar">
                                        <span class="admin-table-caption">
                                            <i class="fa fa-sitemap"></i>
                                            Direct legs for
                                            <asp:Literal ID="litLegsUser" runat="server" />
                                            (legs with under-active &lt; 10 are not counted)
                                        </span>
                                    </div>
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="gvLegs" runat="server"
                                            CssClass="table table-bordered table-hover dataTable"
                                            Width="100%" AutoGenerateColumns="False" GridLines="None"
                                            EmptyDataText="No active direct legs found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="leguserid" HeaderText="Leg User ID" />
                                                <asp:BoundField DataField="legusername" HeaderText="Name" />
                                                <asp:BoundField DataField="underactive" HeaderText="Active Under Leg" />
                                                <asp:TemplateField HeaderText="Counted">
                                                    <ItemTemplate>
                                                        <span class='bonanza-badge <%# Convert.ToBoolean(Eval("iscounted")) ? "is-yes" : "is-no" %>'>
                                                            <%# Convert.ToBoolean(Eval("iscounted")) ? "Yes" : "No" %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

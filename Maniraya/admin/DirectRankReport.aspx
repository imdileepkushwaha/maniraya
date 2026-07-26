<%@ Page Title="Direct Rank Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="DirectRankReport.aspx.cs" Inherits="admin_DirectRankReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=69" />
    <style>
        .admin-report-page .admin-table-pager-bar a.admin-pager-btn {
            cursor: pointer;
        }
        .admin-report-page .admin-table-pager-bar .admin-pager-btn.is-ellipsis {
            border: none;
            background: transparent;
            min-width: 24px;
            color: #94a3b8;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Direct Rank Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">My Network</a></li>
            <li class="active">Direct Rank Report</li>
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
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">User Direct Rank based on Active Directs (SavingStatus = 1). Shows current rank and how many more active directs are needed for next rank.</p>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Filters</h5>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtUserId.ClientID %>">User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" placeholder="Enter user id" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtUserName.ClientID %>">Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                    <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" placeholder="Enter name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddRank.ClientID %>">Current Rank</label>
                                                <asp:DropDownList ID="ddRank" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="">All Ranks</asp:ListItem>
                                                    <asp:ListItem Value="Member">Member</asp:ListItem>
                                                    <asp:ListItem Value="Distributor">Distributor</asp:ListItem>
                                                    <asp:ListItem Value="Bronze">Bronze</asp:ListItem>
                                                    <asp:ListItem Value="Silver">Silver</asp:ListItem>
                                                    <asp:ListItem Value="Gold">Gold</asp:ListItem>
                                                    <asp:ListItem Value="Diamond">Diamond</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtMinActive.ClientID %>">Min Active Directs</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-users"></i></span>
                                                    <asp:TextBox ID="txtMinActive" CssClass="form-control" runat="server" placeholder="e.g. 10" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                                <asp:Button ID="btnExcel" CssClass="btn btn-success" runat="server" Text="Export Excel" OnClick="btnExcel_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-trophy"></i> Direct Rank List</h3>
                                <div class="box-tools admin-record-filter-tools">
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
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-trophy"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Loading Direct Rank list..." />
                                    </span>
                                </div>
                                <div class="admin-table-paged-shell">
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                            AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="25"
                                            EmptyDataText="No users found for selected filters."
                                            OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <PagerSettings Visible="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="userid" HeaderText="User ID" />
                                                <asp:BoundField DataField="username" HeaderText="Name" />
                                                <asp:BoundField DataField="mobile" HeaderText="Mobile" />
                                                <asp:BoundField DataField="activedirects" HeaderText="Active Directs" />
                                                <asp:BoundField DataField="currentrank" HeaderText="Current Rank" />
                                                <asp:BoundField DataField="nextrank" HeaderText="Next Rank" />
                                                <asp:BoundField DataField="remaining" HeaderText="Need for Next" />
                                                <asp:BoundField DataField="requirement" HeaderText="Requirement" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar"></asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

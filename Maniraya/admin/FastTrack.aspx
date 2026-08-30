<%@ Page Title="Thailand Trip Bonanza" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FastTrack.aspx.cs" Inherits="admin_FastTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=74" />
    <style>
        .fasttrack-banner {
            display: block;
            width: 100%;
            max-height: 360px;
            object-fit: cover;
            border-radius: 12px;
            margin-bottom: 16px;
            border: 1px solid #fde68a;
        }

        .fasttrack-rules {
            margin: 0 0 16px;
            padding: 14px 16px;
            border-radius: 12px;
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.14) 0%, rgba(14, 116, 144, 0.06) 100%);
            border: 1px solid rgba(229, 169, 6, 0.32);
            color: #78350f;
            font-size: 13px;
            line-height: 1.6;
        }

        .fasttrack-rules strong {
            color: #92400e;
        }

        .fasttrack-badge {
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

        .fasttrack-badge.is-yes {
            background: #ecfdf5;
            color: #047857;
            border: 1px solid #a7f3d0;
        }

        .fasttrack-badge.is-no {
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
        <h1>Thailand Trip Bonanza</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Prize</a></li>
            <li class="active">Thailand Trip Bonanza</li>
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
                                <h3 class="box-title"><i class="fa fa-plane"></i> Thailand Trip Bonanza</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <!-- <img src="assets/images/fast-track-thailand.png" alt="Fast Track Thailand Trip" class="fasttrack-banner" /> -->
                                <div class="fasttrack-rules">
                                    <strong>Applicable to Savings Plans only.</strong>
                                    Qualify in the offer window <strong>25 Aug 2026 – 30 Oct 2026</strong> when <strong>both</strong> are met:<br />
                                    1) Self ke <strong>10 directs</strong> (₹18,000 / Bulk18 approved)<br />
                                    2) Un <strong>10 directs mein se har ek ke neeche alag 10 directs</strong> hone chahiye<br />
                                    Counted direct = approved ₹18,000 / Bulk18 saving purchase in the window. Admin user <strong>MP000001</strong> is excluded.
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
                                <h3 class="box-title"><i class="fa fa-trophy"></i> Thailand Trip Bonanza Status</h3>
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
                                        <asp:Label ID="lblSummary" runat="server" Text="Run search to check Thailand Trip Bonanza qualification." />
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
                                                <asp:BoundField DataField="selfdirects" HeaderText="Self Directs (10)" />
                                                <asp:BoundField DataField="completelegs" HeaderText="Legs with 10 (10)" />
                                                <asp:BoundField DataField="selfpending" HeaderText="Need Directs" />
                                                <asp:BoundField DataField="legspending" HeaderText="Need Legs" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <span class='fasttrack-badge <%# Convert.ToBoolean(Eval("isqualified")) ? "is-yes" : "is-no" %>'>
                                                            <i class='fa <%# Convert.ToBoolean(Eval("isqualified")) ? "fa-check" : "fa-times" %>'></i>
                                                            <%# Eval("statuslabel") %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSales" runat="server" CssClass="admin-action-btn is-view"
                                                            CommandName="viewsales" CommandArgument='<%# Eval("userid") %>'
                                                            CausesValidation="false">
                                                            <i class="fa fa-list"></i> View
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar"></asp:Panel>
                                </div>

                                <asp:Panel ID="pnlSales" runat="server" Visible="false" style="margin-top:20px;">
                                    <div class="admin-table-toolbar">
                                        <span class="admin-table-caption">
                                            <i class="fa fa-users"></i>
                                            Qualifying sales for
                                            <asp:Literal ID="litSalesUser" runat="server" />
                                            (offer window 25 Aug 2026 – 30 Oct 2026)
                                        </span>
                                    </div>
                                    <h4 style="font-size:14px;font-weight:700;margin:12px 0 8px;">Self Directs (each needs 10 directs to count as a complete leg)</h4>
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="gvLevel1" runat="server"
                                            CssClass="table table-bordered table-hover dataTable"
                                            Width="100%" AutoGenerateColumns="False" GridLines="None"
                                            EmptyDataText="No qualifying directs found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="buyerid" HeaderText="Direct User ID" />
                                                <asp:BoundField DataField="buyername" HeaderText="Name" />
                                                <asp:BoundField DataField="underdirects" HeaderText="Their Directs" />
                                                <asp:BoundField DataField="underpending" HeaderText="Need for 10" />
                                                <asp:TemplateField HeaderText="Counted (10/10)">
                                                    <ItemTemplate>
                                                        <span class='fasttrack-badge <%# Convert.ToBoolean(Eval("iscounted")) ? "is-yes" : "is-no" %>'>
                                                            <%# Convert.ToBoolean(Eval("iscounted")) ? "Yes" : "No" %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="saledate" HeaderText="Sale Date" DataFormatString="{0:dd/MM/yyyy}" />
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

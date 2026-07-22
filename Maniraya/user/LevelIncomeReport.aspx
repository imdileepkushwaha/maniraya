<%@ Page Title="Level Income Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="LevelIncomeReport.aspx.cs" Inherits="LevelIncomeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=29" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Level Income Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">My Income</a></li>
            <li class="active">Level Income</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-income-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--premium">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-chart-line"></i></div>
                    <div class="profile-hero-info">
                        <h2>Level Income Report</h2>
                        <p class="profile-hero-meta">View level-wise commission earned from your network with expected payout dates and status.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Dashboard</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-income-report-search-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-purple" aria-hidden="true"><i class="fa fa-filter"></i></span>
                        <div>
                            <h3>Search Criteria</h3>
                            <p>Filter level income by date range and member ID</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="row profile-form-grid dash-income-report-search-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtfromdate.ClientID %>"><i class="fa fa-calendar"></i> From Date</label>
                                    <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txttodate.ClientID %>"><i class="fa fa-calendar-check"></i> To Date</label>
                                    <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtuserid.ClientID %>"><i class="fa fa-id-badge"></i> User ID</label>
                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hdnUserId" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="dash-income-report-actions">
                            <asp:Button ID="btnSubmit" CssClass="profile-btn profile-btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="profile-btn profile-btn-outline" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head dash-income-report-results-head">
                        <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-table"></i></span>
                        <div class="dash-income-report-results-copy">
                            <h3>Income Details</h3>
                            <asp:Label ID="lblResultSummary" runat="server" CssClass="dash-income-report-summary" Text="Search to view your level income records." />
                        </div>
                        <asp:Panel ID="pnlSummary" runat="server" Visible="false" CssClass="dash-income-report-stats">
                            <div class="dash-income-report-stat">
                                <span class="dash-income-report-stat-label">Records</span>
                                <strong><asp:Literal ID="litRecordCount" runat="server" Text="0" /></strong>
                            </div>
                            <div class="dash-income-report-stat is-payable">
                                <span class="dash-income-report-stat-label">Total Amount</span>
                                <strong><i class="fa fa-rupee-sign"></i> <asp:Literal ID="litTotalPayable" runat="server" Text="0.00" /></strong>
                            </div>
                        </asp:Panel>
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
                        <asp:Panel ID="pnlLoadError" runat="server" Visible="false" CssClass="profile-alert" style="margin-bottom:16px;">
                            <i class="fa fa-exclamation-circle"></i>
                            <span>Unable to load level income data right now. Please try again later.</span>
                        </asp:Panel>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table dash-income-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="true"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# GetSerialNumber(Container.DataItemIndex) %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member">
                                        <ItemTemplate>
                                            <span class="dash-income-member-name"><%# Eval("username") %></span>
                                            <span class="dash-income-member-id"><%# Eval("UserId") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From User">
                                        <ItemTemplate>
                                            <span class="dash-saving-txn"><%# Eval("JuniorUserId") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level">
                                        <ItemTemplate>
                                            <span class="dash-income-level-badge">L<%# Eval("LevelNo") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" CssClass="dash-income-payable" Text='<%# Eval("Amount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction ID">
                                        <ItemTemplate>
                                            <span class="dash-saving-txn"><%# Eval("transactionid") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><%# Eval("EntryDate") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-inbox"></i>
                                        <h4>No records found</h4>
                                        <p>Try adjusting the date range or search again.</p>
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
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initLevelIncomeDatepicker() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        Sys.Application.add_load(function () {
            initLevelIncomeDatepicker();
        });
    </script>
</asp:Content>

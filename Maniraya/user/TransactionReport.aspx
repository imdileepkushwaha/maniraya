<%@ Page Title="Transaction Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TransactionReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=6" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Transaction Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Reports</a></li>
            <li class="active">Transaction Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page transaction-report-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-exchange"></i></div>
                    <div class="profile-hero-info">
                        <h2>Transaction Report</h2>
                        <p class="profile-hero-meta">Track wallet credits, debits, and transaction history for your account.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="account_Ledger.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-book"></i> Account Ledger</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-1">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-filter"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Search Criteria</h3>
                                <p class="box-subtitle">Filter transactions by date range</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row team-filter-grid transaction-filter-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtfromdate.ClientID %>"><i class="fa fa-calendar"></i> From Date</label>
                                    <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txttodate.ClientID %>"><i class="fa fa-calendar-check-o"></i> To Date</label>
                                    <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtuserid.ClientID %>"><i class="fa fa-id-badge"></i> User ID</label>
                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer transaction-search-footer">
                        <div class="transaction-search-actions">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary transaction-btn-search" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default transaction-btn-cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                        <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" ToolTip="Download Excel" CssClass="team-excel-btn" OnClick="imgExcel_Click" AlternateText="Download Excel" />
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-6">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Transaction Details</h3>
                                <p class="box-subtitle">Summary and list of wallet movements</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="row team-stat-grid transaction-stat-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-income">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-down"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Total Income</p>
                                        <asp:TextBox ID="LblCredited" CssClass="form-control team-stat-input" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="team-stat-card team-stat-debit">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-up"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Total Deduct</p>
                                        <asp:TextBox ID="LblDebited" CssClass="form-control team-stat-input" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12">
                                <div class="team-stat-card team-stat-balance">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-inr"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Balance</p>
                                        <asp:TextBox ID="LblCurrentWallet" CssClass="form-control team-stat-input" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="team-table-toolbar">
                            <span class="team-table-caption"><i class="fa fa-table"></i> Transaction List</span>
                            <div class="form-group team-toolbar-filter">
                                <label for="<%= ddlRecordFilter.ClientID %>">Show records</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control team-records-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group team-table-group">
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                    AutoGenerateColumns="False" EmptyDataText="No transactions found for the selected criteria." OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("mentiondate", "{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%# Eval("transactionid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcredit" runat="server" CssClass="txn-amount txn-amount-credit" Text='<%# Eval("cramount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldebit" runat="server" CssClass="txn-amount txn-amount-debit" Text='<%# Eval("dramount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltransactiontype" runat="server" Text='<%# Eval("transactiontype") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initTransactionDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/MM/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        $(function () {
            initTransactionDatepickers();
        });

        Sys.Application.add_load(function () {
            initTransactionDatepickers();
        });
    </script>
</asp:Content>

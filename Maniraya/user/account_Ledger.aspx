<%@ Page Title="Account Ledger" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="account_Ledger.aspx.cs" Inherits="User_account_Ledger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/radio/style.css" rel="stylesheet" />
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=8" rel="stylesheet" />
    <style>
        .ledger-wallet-toggle {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }

        .ledger-wallet-toggle .ledger-wallet-opt {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 9px 16px;
            border: 1px solid #dfe3ea;
            border-radius: 10px;
            background: #fff;
            font-size: 0.86rem;
            font-weight: 600;
            color: #475569;
        }

        .ledger-wallet-toggle .ledger-wallet-opt input {
            margin: 0;
        }

        .ledger-wallet-toggle .ledger-wallet-opt label {
            display: inline !important;
            margin: 0 !important;
            font-weight: 600;
            color: inherit !important;
            cursor: pointer;
        }

        .ledger-page .box-footer {
            display: flex;
            align-items: center;
            gap: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Account Ledger</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home</a></li>
            <li><a href="#">Report</a></li>
            <li class="active">Account Ledger</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page ledger-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-book"></i></div>
                    <div class="profile-hero-info">
                        <h2>Account Ledger</h2>
                        <p class="profile-hero-meta">Review every credit and debit movement across your wallets.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="TransactionReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-exchange-alt"></i> Transactions</a>
                        <a href="UserWallet.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-credit-card"></i> My Wallet</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-1">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-filter"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Search Criteria</h3>
                                <p class="box-subtitle">Filter the ledger by date range and wallet</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row team-filter-grid">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtFromDate.ClientID %>"><i class="fa fa-calendar"></i> From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtToDate.ClientID %>"><i class="fa fa-calendar-check"></i> To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label><i class="fa fa-wallet"></i> Wallet Type</label>
                                    <div class="ledger-wallet-toggle">
                                        <span class="ledger-wallet-opt">
                                            <asp:RadioButton ID="RDBtnRechargeWallet" runat="server" Text="Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnRechargeWallet_CheckedChanged" />
                                        </span>
                                        <span class="ledger-wallet-opt">
                                            <asp:RadioButton ID="RdBtnUtilityWallet" runat="server" Text="Cash Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnUtilityWallet_CheckedChanged" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" ToolTip="Download Excel" CssClass="team-excel-btn" Width="40px" OnClick="imgExcel_Click" AlternateText="Download Excel" />
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-6">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Ledger Details</h3>
                                <p class="box-subtitle">Detailed credit and debit history with running balance</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="team-table-toolbar">
                            <span class="team-table-caption"><i class="fa fa-table"></i> Ledger Entries</span>
                            <div class="form-group team-toolbar-filter">
                                <label for="<%= ddlRecordFilter.ClientID %>">Show records</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control team-records-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group team-table-group">
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="grdAccountsList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable team-table"
                                    Width="100%" GridLines="Horizontal" ShowFooter="true" EmptyDataText="No record(s) found" OnRowDataBound="grdAccountsList_RowDataBound">
                                    <FooterStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wallet Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWalletType" runat="server" Text='<%# Eval("walletType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("MentionDate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFranchiseeId" runat="server" Text='<%# Eval("userID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransactionId" runat="server" Text='<%# Eval("TransactionId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" CssClass="txn-amount txn-amount-debit" Text='<%# Eval("DrAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDebitTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit" runat="server" CssClass="txn-amount txn-amount-credit" Text='<%# Eval("CrAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCreditTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Old Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lbloldBalance" runat="server" Text='<%# Eval("oldBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentBalance" runat="server" Text='<%# Eval("CurrentBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrentBalanceTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
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
</asp:Content>

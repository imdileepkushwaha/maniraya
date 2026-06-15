<%@ Page Title="Account Ledger" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="account_Ledger.aspx.cs" Inherits="franchisee_account_Ledger" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/franchisee-account-ledger.css?v=1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <div class="content-header">
        <h1>Account Ledger</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Report</a></li>
            <li class="active">Account Ledger</li>
        </ol>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
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
            <div class="row fr-ledger-page">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-book"></i> Account Ledger Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="fr-ledger-summary">
                                <div class="fr-ledger-stat debit">
                                    <span class="fr-ledger-stat-icon"><i class="fa fa-arrow-down"></i></span>
                                    <div>
                                        <span class="fr-ledger-stat-label">Total Debit</span>
                                        <asp:Label ID="lblSummaryDebit" runat="server" CssClass="fr-ledger-stat-value" Text="0.00" />
                                    </div>
                                </div>
                                <div class="fr-ledger-stat credit">
                                    <span class="fr-ledger-stat-icon"><i class="fa fa-arrow-up"></i></span>
                                    <div>
                                        <span class="fr-ledger-stat-label">Total Credit</span>
                                        <asp:Label ID="lblSummaryCredit" runat="server" CssClass="fr-ledger-stat-value" Text="0.00" />
                                    </div>
                                </div>
                                <div class="fr-ledger-stat balance">
                                    <span class="fr-ledger-stat-icon"><i class="fa fa-inr"></i></span>
                                    <div>
                                        <span class="fr-ledger-stat-label">Latest Balance</span>
                                        <asp:Label ID="lblSummaryBalance" runat="server" CssClass="fr-ledger-stat-value" Text="0.00" />
                                    </div>
                                </div>
                            </div>

                            <div class="fr-ledger-toolbar">
                                <div class="fr-ledger-field">
                                    <label for="<%= txtFromDate.ClientID %>">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="dd-MMM-yyyy" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="fr-ledger-field">
                                    <label for="<%= txtToDate.ClientID %>">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="dd-MMM-yyyy" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="fr-ledger-actions">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:LinkButton ID="lnkExcel" runat="server" CssClass="fr-ledger-excel-btn" OnClick="imgExcel_Click" ToolTip="Download Excel">
                                        <asp:Image ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" />
                                        <span>Export</span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="fr-ledger-table-bar">
                                <p class="fr-ledger-record-label"><i class="fa fa-list"></i> Transaction History</p>
                                <div>
                                    <label class="fr-ledger-record-label" style="display:inline;margin-right:8px;">Show</label>
                                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control fr-ledger-record-select" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="fr-ledger-table-wrap table-responsive">
                                <asp:GridView ID="grdAccountsList" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-hover fr-ledger-grid"
                                    Width="100%" GridLines="None" ShowFooter="true"
                                    OnRowDataBound="grdAccountsList_RowDataBound">
                                    <EmptyDataTemplate>
                                        <div class="fr-ledger-empty">
                                            <i class="fa fa-inbox"></i>
                                            No transactions found for the selected period.
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("MentionDate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Franchisee Id">
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
                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DrAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDebitTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle CssClass="text-right" />
                                            <FooterStyle CssClass="text-right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CrAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCreditTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle CssClass="text-right" />
                                            <FooterStyle CssClass="text-right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Old Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lbloldBalance" runat="server" Text='<%# Eval("oldBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentBalance" runat="server" Text='<%# Eval("CurrentBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrentBalanceTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle CssClass="text-right" />
                                            <FooterStyle CssClass="text-right" />
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
            <asp:PostBackTrigger ControlID="lnkExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

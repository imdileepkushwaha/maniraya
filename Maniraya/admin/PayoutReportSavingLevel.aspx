<%@ Page Title="Saving Level Payout Report" Language="C#" MasterPageFile="adminmaster.master" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="true" CodeFile="PayoutReportSavingLevel.aspx.cs" Inherits="PayoutReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .saving-payout-page .saving-payout-hero {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            gap: 16px;
            flex-wrap: wrap;
            margin-bottom: 18px;
            padding: 16px 18px;
            border: 1px solid rgba(229, 169, 6, 0.22);
            border-radius: 14px;
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.1) 0%, rgba(255, 255, 255, 0.96) 58%);
        }

        .saving-payout-page .saving-payout-hero-copy {
            flex: 1;
            min-width: 240px;
        }

        .saving-payout-page .saving-payout-hero-title {
            display: flex;
            align-items: center;
            gap: 10px;
            margin: 0 0 8px;
            font-size: 1rem;
            font-weight: 700;
            color: #0f172a;
        }

        .saving-payout-page .saving-payout-hero-title i {
            width: 34px;
            height: 34px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            border-radius: 10px;
            color: #b45309;
            background: rgba(229, 169, 6, 0.16);
        }

        .saving-payout-page .saving-payout-hero-text {
            margin: 0;
            font-size: 13px;
            line-height: 1.55;
            color: #64748b;
        }

        .saving-payout-page .saving-payout-stats {
            display: flex;
            align-items: stretch;
            gap: 12px;
            flex-wrap: wrap;
        }

        .saving-payout-page .saving-payout-stat {
            min-width: 150px;
            padding: 12px 14px;
            border-radius: 12px;
            border: 1px solid #e2e8f0;
            background: #fff;
            box-shadow: 0 8px 20px rgba(15, 23, 42, 0.04);
        }

        .saving-payout-page .saving-payout-stat-label {
            display: block;
            margin-bottom: 4px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #94a3b8;
        }

        .saving-payout-page .saving-payout-stat-value {
            display: block;
            font-size: 1.35rem;
            font-weight: 800;
            line-height: 1.2;
            color: #0f172a;
        }

        .saving-payout-page .saving-payout-stat.is-amount .saving-payout-stat-value {
            color: #15803d;
        }

        .saving-payout-page .saving-payout-toolbar {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            flex-wrap: wrap;
            margin-bottom: 14px;
            padding: 10px 14px;
            border: 1px dashed #cbd5e1;
            border-radius: 12px;
            background: #f8fafc;
        }

        .saving-payout-page .saving-payout-toolbar-note {
            margin: 0;
            font-size: 13px;
            color: #64748b;
        }

        .saving-payout-page .saving-payout-toolbar-note strong {
            color: #0f172a;
        }

        .saving-payout-page .saving-payout-table .table > thead > tr > th {
            white-space: nowrap;
            vertical-align: middle;
        }

        .saving-payout-page .saving-payout-table .col-select {
            width: 52px;
            text-align: center;
        }

        .saving-payout-page .saving-payout-table .col-amount {
            min-width: 110px;
            text-align: right;
        }

        .saving-payout-page .saving-payout-user-name {
            display: block;
            font-size: 14px;
            font-weight: 700;
            color: #0f172a;
            line-height: 1.35;
        }

        .saving-payout-page .saving-payout-user-id {
            display: block;
            margin-top: 2px;
            font-size: 12px;
            font-weight: 600;
            color: #64748b;
        }

        .saving-payout-page .saving-payout-amount {
            display: inline-flex;
            align-items: center;
            justify-content: flex-end;
            min-width: 88px;
            padding: 6px 10px;
            border-radius: 999px;
            font-size: 13px;
            font-weight: 800;
            color: #166534;
            background: #dcfce7;
            border: 1px solid #bbf7d0;
        }

        .saving-payout-page .saving-payout-bank-meta {
            display: block;
            font-size: 12px;
            line-height: 1.45;
            color: #475569;
        }

        .saving-payout-page .saving-payout-bank-meta strong {
            color: #0f172a;
        }

        .saving-payout-page .saving-payout-empty {
            padding: 28px 18px;
            text-align: center;
            color: #64748b;
        }

        .saving-payout-page .saving-payout-empty i {
            display: block;
            margin-bottom: 10px;
            font-size: 2rem;
            color: #cbd5e1;
        }

        @media (max-width: 767px) {
            .saving-payout-page .saving-payout-stats {
                width: 100%;
            }

            .saving-payout-page .saving-payout-stat {
                flex: 1 1 140px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Level Payout Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
            <li class="active">Saving Level Payout Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="Loading" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page saving-payout-page">
                <div class="saving-payout-hero">
                    <div class="saving-payout-hero-copy">
                        <h4 class="saving-payout-hero-title">
                            <i class="fa fa-university" aria-hidden="true"></i>
                            Release Saving Level Income Payout
                        </h4>
                        <p class="saving-payout-hero-text">
                            Search users with payable saving level income balance above Rs. 50, select rows, and release payout to their registered bank account.
                        </p>
                    </div>
                    <asp:Panel ID="pnlSummary" runat="server" Visible="false" CssClass="saving-payout-stats">
                        <div class="saving-payout-stat">
                            <span class="saving-payout-stat-label">Eligible Users</span>
                            <span class="saving-payout-stat-value"><asp:Literal ID="litRecordCount" runat="server" Text="0" /></span>
                        </div>
                        <div class="saving-payout-stat is-amount">
                            <span class="saving-payout-stat-label">Total Payable</span>
                            <span class="saving-payout-stat-value"><asp:Literal ID="litTotalAmount" runat="server" Text="0.00" /></span>
                        </div>
                    </asp:Panel>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Find Payout Users</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtuserid.ClientID %>">User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Leave blank for all users" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search Payouts" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-money"></i> Payout List</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <asp:ImageButton ID="imgExcel" runat="server" CssClass="admin-export-excel-btn" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick="imgExcel_Click" ToolTip="Export to Excel" />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="saving-payout-toolbar">
                                    <p class="saving-payout-toolbar-note">
                                        <strong>Tip:</strong> Use the header checkbox to select all, then click <strong>Pay All Selected</strong> to release payout.
                                    </p>
                                    <asp:Button ID="btnPayAll" runat="server" CssClass="btn btn-success" Text="Pay All Selected" OnClick="btnPayAll_Click"
                                        OnClientClick="return confirm('Release payout for all selected users?');" />
                                </div>

                                <div class="admin-table-wrap table-responsive saving-payout-table">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No payout records found with balance above Rs. 50."
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="col-select" HeaderStyle-CssClass="col-select">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBox1" OnCheckedChanged="chckchanged" AutoPostBack="true" runat="server" ToolTip="Select all" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="60px">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <span class="saving-payout-user-name"><%# Eval("username") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID" ItemStyle-CssClass="col-userid-excel-text">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserIdDisplay" runat="server" CssClass="saving-payout-user-id" Text='<%# Eval("userid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable Amount" ItemStyle-CssClass="col-amount" HeaderStyle-CssClass="col-amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" CssClass="saving-payout-amount" Text='<%# Eval("amount") %>' />
                                                    <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Details">
                                                <ItemTemplate>
                                                    <span class="saving-payout-bank-meta"><strong><%# Eval("bankname") %></strong></span>
                                                    <span class="saving-payout-bank-meta"><%# Eval("branchname") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IFSC Code" ItemStyle-CssClass="col-ifsc-excel-text">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIFSCCode" runat="server" CssClass="saving-payout-bank-meta" Text='<%# Eval("ifsccode") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account" ItemStyle-CssClass="col-account-excel-text">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountNo" runat="server" CssClass="saving-payout-bank-meta" Text='<%# Eval("AccountNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAN">
                                                <ItemTemplate><%# Eval("PanNumber") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile" ItemStyle-CssClass="col-mobile-excel-text">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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

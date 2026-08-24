<%@ Page Title="Team Member Installments" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TeamMemberInstallmentView.aspx.cs" Inherits="user_TeamMemberInstallmentView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <style>
        .dash-saving-status.is-unpaid {
            background: #fff7ed;
            color: #c2410c;
            border: 1px solid #fed7aa;
        }
        .dash-saving-status.is-paid {
            background: #ecfdf5;
            color: #047857;
            border: 1px solid #a7f3d0;
        }
        .dash-saving-status.is-processing {
            background: #eff6ff;
            color: #1d4ed8;
            border: 1px solid #bfdbfe;
        }
        .dash-saving-filter-row {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            align-items: flex-end;
            margin-bottom: 16px;
        }
        .dash-saving-filter-row .form-group {
            margin: 0;
            min-width: 180px;
            flex: 1 1 180px;
        }
        .dash-saving-filter-row label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 700;
            color: #64748b;
        }
        .dash-team-member-chip {
            display: inline-block;
            margin-top: 8px;
            padding: 4px 12px;
            border-radius: 999px;
            background: rgba(255,255,255,0.18);
            font-size: 13px;
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Team Member Installments</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="TeamSavingInstallmentReport.aspx">Team Installment Report</a></li>
            <li class="active">Installment Detail</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-eye"></i></div>
                    <div class="profile-hero-info">
                        <h2>Installment Detail</h2>
                        <p class="profile-hero-meta">View-only paid and unpaid installment list for your team member. Payment is not allowed here.</p>
                        <asp:Label ID="lblMemberInfo" runat="server" CssClass="dash-team-member-chip" />
                    </div>
                    <div class="profile-hero-actions">
                        <a href="TeamSavingInstallmentReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back to Team</a>
                    </div>
                </div>

                <asp:Panel ID="pnlAccessDenied" runat="server" Visible="false" CssClass="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-body">
                        <div class="dash-saving-report-empty">
                            <i class="fa fa-lock"></i>
                            <h4>Access denied</h4>
                            <p>This user is not an Active saving member in your 10-level sponsor team, or the user id is invalid.</p>
                            <a href="TeamSavingInstallmentReport.aspx" class="profile-btn profile-btn-primary" style="margin-top:12px;display:inline-block;">Back to Team Report</a>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlContent" runat="server" Visible="false">
                    <div class="dash-subpage-panel dash-saving-report-panel">
                        <div class="dash-subpage-panel-head">
                            <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-money-bill-alt"></i></span>
                            <div>
                                <h3>All Installments</h3>
                                <p>Unpaid, processing and paid EMI records (view only)</p>
                            </div>
                        </div>
                        <div class="dash-subpage-panel-body">
                            <div class="dash-saving-filter-row">
                                <div class="form-group">
                                    <label for="<%= ddStatus.ClientID %>">Status</label>
                                    <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="Unpaid">Unpaid</asp:ListItem>
                                        <asp:ListItem Value="Processing">Processing</asp:ListItem>
                                        <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="<%= ddCouponCode.ClientID %>">Coupon Code</label>
                                    <asp:DropDownList ID="ddCouponCode" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddCouponCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtProduct.ClientID %>">Product</label>
                                    <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" placeholder="Search product" />
                                </div>
                                <div class="form-group" style="flex: 0 0 auto;">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="profile-btn profile-btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnReset" runat="server" CssClass="profile-btn profile-btn-outline" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                                </div>
                            </div>

                            <p class="dash-saving-report-intro">Status guide: <strong>Unpaid</strong> = payment due, <strong>Processing</strong> = submitted for approval, <strong>Paid</strong> = approved. This is view-only — you cannot pay for team members.</p>

                            <div class="dash-saving-report-table-wrap">
                                <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                    AutoGenerateColumns="False" GridLines="None"
                                    OnRowDataBound="GridView1_RowDataBound"
                                    EmptyDataText="">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Coupon Code">
                                            <ItemTemplate>
                                                <strong><%# Eval("couponcode") %></strong>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installment Date">
                                            <ItemTemplate>
                                                <span class="dash-saving-date"><%# Eval("installmentdate", "{0:dd MMM yyyy}") %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inst No">
                                            <ItemTemplate><%# Eval("instno") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i> <%# SavingProductHelper.FormatMoney(Eval("amount")) %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <span class="dash-saving-product"><%# Eval("productname") %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" CssClass="dash-saving-status" Text='<%# Eval("StatusDisplay") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="dash-saving-report-empty">
                                            <i class="fa fa-calendar-times-o"></i>
                                            <h4>No installments found</h4>
                                            <p>This team member has no saving installment records for the selected filters.</p>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

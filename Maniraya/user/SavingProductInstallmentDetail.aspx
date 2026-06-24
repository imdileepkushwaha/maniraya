<%@ Page Title="My Savings Installment" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingProductInstallmentDetail.aspx.cs" Inherits="user_SavingProductInstallmentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>My Savings Installment</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="SAvingProductPurchaseReport.aspx">My Savings</a></li>
            <li class="active">Installments</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-calendar-check-o"></i></div>
                    <div class="profile-hero-info">
                        <h2>Installment Details</h2>
                        <p class="profile-hero-meta">Track monthly installment dates, payment status, and transaction references for your saving plan.</p>
                        <asp:Label ID="lblCouponCode" runat="server" CssClass="dash-saving-coupon-chip" Visible="false" />
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SAvingProductPurchaseReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back to Savings</a>
                        <a href="SavingDashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-bank"></i> Dashboard</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-money"></i></span>
                        <div>
                            <h3>Monthly Installments</h3>
                            <p>Payment schedule and approval status for this coupon</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Pending installments can be paid from the action column when payment is enabled.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdGetHelp_RowDataBound" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Installment Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><asp:Label ID="lblcreatingdate" runat="server" Text='<%# Eval("entrydate", "{0:dd MMM yyyy}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><asp:Label ID="lblreleasedate" runat="server" Text='<%# Eval("approvedate", "{0:dd MMM yyyy, hh:mm tt}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-inr"></i> <asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product">
                                        <ItemTemplate>
                                            <span class="dash-saving-product"><asp:Label ID="lblproductname" runat="server" Text='<%# Eval("productname") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>' CssClass="dash-saving-status"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <span class="dash-saving-txn"><asp:Label ID="lbltransactionid" runat="server" Text='<%# Eval("OnlineTransactionId") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <a href="#" class="dash-saving-action-btn is-pay">
                                                <i class="fa fa-credit-card"></i> Pay
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-calendar-times-o"></i>
                                        <h4>No installments found</h4>
                                        <p>Installment records for this coupon will appear here once they are generated.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

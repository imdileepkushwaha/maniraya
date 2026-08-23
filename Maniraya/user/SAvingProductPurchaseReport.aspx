<%@ Page Title="My Savings" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SAvingProductPurchaseReport.aspx.cs" Inherits="user_SavingProductPurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>My Savings</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="SavingDashboard.aspx">Saving</a></li>
            <li class="active">My Savings</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
                <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-list-alt"></i></div>
                    <div class="profile-hero-info">
                        <h2>My Savings</h2>
                        <p class="profile-hero-meta">View your saving product requests, approval status, and installment history in one place.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="SavingProductPurchase.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-shopping-cart"></i> Buy Product</a>
                        <a href="SavingDashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-university"></i> Saving Dashboard</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-blue" aria-hidden="true"><i class="fa fa-table"></i></span>
                        <div>
                            <h3>Purchase History</h3>
                            <p>All saving product requests linked to your account</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <p class="dash-saving-report-intro">Click <strong>Installments</strong> on any approved request to view monthly payment details.</p>
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdGetHelp_RowDataBound" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coupon Code">
                                        <ItemTemplate>
                                            <strong><%# Eval("couponcode") %></strong>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><asp:Label ID="lblcreatingdate" runat="server" Text='<%# Eval("entrydate", "{0:dd MMM yyyy, hh:mm tt}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><asp:Label ID="lblreleasedate" runat="server" Text='<%# Eval("approvedate", "{0:dd MMM yyyy, hh:mm tt}") %>'></asp:Label></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i> <asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount") %>'></asp:Label></span>
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
                                            <a href='SavingProductInstallmentDetail.aspx?oid=<%# Eval("couponcode") %>' class="dash-saving-action-btn is-view">
                                                <i class="fa fa-calendar-check"></i> Installments
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-inbox"></i>
                                        <h4>No saving requests yet</h4>
                                        <p>Your saving product purchase history will appear here once you place a request.</p>
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

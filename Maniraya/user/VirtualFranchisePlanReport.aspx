<%@ Page Title="My Virtual Franchise Plans" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="VirtualFranchisePlanReport.aspx.cs" Inherits="user_VirtualFranchisePlanReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>My Virtual Franchise Plans</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Virtual Franchise</a></li>
            <li class="active">My Plans</li>
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
                        <h2>My Virtual Franchise Plans</h2>
                        <p class="profile-hero-meta">View plan requests, approval status, and rejection remarks.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="VirtualFranchisePlanPurchase.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-gem"></i> Buy Plan</a>
                        <a href="VirtualFranchiseROIReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-calendar"></i> ROI Report</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-arrow-left"></i> Back</a>
                    </div>
                </div>

                <div class="dash-subpage-panel dash-saving-report-panel">
                    <div class="dash-subpage-panel-head">
                        <span class="dash-subpage-panel-icon tone-blue" aria-hidden="true"><i class="fa fa-table"></i></span>
                        <div>
                            <h3>Plan Requests</h3>
                            <p>All virtual franchise plan requests linked to your account</p>
                        </div>
                    </div>
                    <div class="dash-subpage-panel-body">
                        <div class="dash-saving-report-table-wrap">
                            <asp:GridView ID="GridView1" runat="server" CssClass="dash-saving-report-table" Width="100%"
                                AutoGenerateColumns="False" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="dash-saving-sno"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan">
                                        <ItemTemplate><strong><%# Eval("planname") %></strong></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <span class="dash-saving-amount"><i class="fa fa-rupee-sign"></i> <%# Eval("planamount") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly ROI">
                                        <ItemTemplate><%# Eval("monthlyroi") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Cashback">
                                        <ItemTemplate><%# Eval("totalcashback") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate><%# Eval("paymentmethod") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate><span class="dash-saving-txn"><%# Eval("onlinetransactionid") %></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><%# Eval("entrydate", "{0:dd MMM yyyy, hh:mm tt}") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <span class="dash-saving-date"><%# Eval("approvedate", "{0:dd MMM yyyy, hh:mm tt}") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>' CssClass="dash-saving-status"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="dash-saving-report-empty">
                                        <i class="fa fa-inbox"></i>
                                        <h4>No plan requests yet</h4>
                                        <p>Your virtual franchise plan requests will appear here after you submit one.</p>
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

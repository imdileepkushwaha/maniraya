<%@ Page Title="Shopping Point / Coupon" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SavingBulkRewardReport.aspx.cs" Inherits="user_SavingBulkRewardReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=8" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Shopping Point / Coupon</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Shopping Point / Coupon</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page transaction-report-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-gift"></i></div>
                    <div class="profile-hero-info">
                        <h2>Shopping Point / Coupon</h2>
                        <p class="profile-hero-meta">2000 coupon can be redeemed now. 20000 shopping point can be used for purchase after 18 months.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-1">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-filter"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Search Criteria</h3>
                                <p class="box-subtitle">Filter rewards by entry date</p>
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
                                    <label for="<%= txttodate.ClientID %>"><i class="fa fa-calendar-check"></i> To Date</label>
                                    <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label><i class="fa fa-id-badge"></i> User ID</label>
                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer transaction-search-footer">
                        <div class="transaction-search-actions">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary transaction-btn-search" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default transaction-btn-cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-6">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Reward Details</h3>
                                <p class="box-subtitle">Coupon and shopping point are shown in separate rows</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="team-table-toolbar">
                            <span class="team-table-caption"><i class="fa fa-table"></i> Reward List</span>
                            <div class="form-group team-toolbar-filter">
                                <label for="<%= ddlRecordFilter.ClientID %>">Show records</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control team-records-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem Selected="True">25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group team-table-group">
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                    AutoGenerateColumns="False" EmptyDataText="No rewards found."
                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                    <PagerStyle CssClass="team-grid-pager" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#"><ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField DataField="RewardType" HeaderText="Reward Type" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N2}" />
                                        <asp:TemplateField HeaderText="Coupon Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcouponcode" runat="server" Text='<%# Eval("DisplayCouponCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="RedeemAfterDate" HeaderText="Redeem After" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("RewardStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRedeem" runat="server" CssClass="btn btn-success btn-sm"
                                                    CommandName="RedeemCoupon" CommandArgument='<%# Eval("Id") %>'>
                                                    Redeem on Purchase
                                                </asp:LinkButton>
                                                <asp:HyperLink ID="lnkPurchase" runat="server" CssClass="btn btn-primary btn-sm"
                                                    NavigateUrl="FranchiseeSearchNew.aspx" Text="Purchase" Visible="false"></asp:HyperLink>
                                                <asp:Label ID="lblLocked" runat="server" CssClass="text-muted" Visible="false" Text="Locked"></asp:Label>
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
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initRewardDatepickers() {
            $('.form_date').datepicker({ format: 'dd/MM/yyyy', autoclose: true }).on('changeDate', function () { $(this).datepicker('hide'); });
        }
        $(function () { initRewardDatepickers(); });
        Sys.Application.add_load(initRewardDatepickers);
    </script>
</asp:Content>

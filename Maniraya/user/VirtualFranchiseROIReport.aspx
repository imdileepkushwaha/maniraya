<%@ Page Title="Virtual Franchise ROI Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="VirtualFranchiseROIReport.aspx.cs" Inherits="user_VirtualFranchiseROIReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=8" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Virtual Franchise ROI Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Virtual Franchise</a></li>
            <li class="active">ROI Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page transaction-report-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-calendar-check"></i></div>
                    <div class="profile-hero-info">
                        <h2>Virtual Franchise ROI Report</h2>
                        <p class="profile-hero-meta">40-month monthly cashback schedule for your approved plans.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="VirtualFranchisePlanReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list"></i> My Plans</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-1">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-filter"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Search Criteria</h3>
                                <p class="box-subtitle">Filter ROI by date range</p>
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
                                <h3 class="box-title">ROI Details</h3>
                                <p class="box-subtitle">Monthly cashback schedule</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="team-table-toolbar">
                            <span class="team-table-caption"><i class="fa fa-table"></i> ROI List</span>
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
                                    AutoGenerateColumns="False" EmptyDataText="No ROI records found."
                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <PagerStyle CssClass="team-grid-pager" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#"><ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField DataField="planname" HeaderText="Plan" />
                                        <asp:BoundField DataField="monthno" HeaderText="Month" />
                                        <asp:BoundField DataField="roidate" HeaderText="ROI Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="roiamount" HeaderText="ROI Amount" DataFormatString="{0:N2}" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="paiddate" HeaderText="Paid Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="transactionid" HeaderText="Transaction ID" />
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
        function initVfRoiDatepickers() {
            $('.form_date').datepicker({ format: 'dd/MM/yyyy', autoclose: true }).on('changeDate', function () { $(this).datepicker('hide'); });
        }
        $(function () { initVfRoiDatepickers(); });
        Sys.Application.add_load(initVfRoiDatepickers);
    </script>
</asp:Content>

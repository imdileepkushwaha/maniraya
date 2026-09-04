<%@ Page Title="Bulk Reward Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingBulkRewardReport.aspx.cs" Inherits="admin_SavingBulkRewardReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=66" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Bulk Reward Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Bulk Reward Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Each approval creates two rows: 2000 coupon (redeemable now with coupon code) and 20000 shopping point (purchase after 18 months).</p>
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-calendar"></i> Date Range</h5>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtfromdate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control form_date" ID="txtfromdate" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txttodate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control form_date" ID="txttodate" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-sliders"></i> Filters</h5>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtuserid.ClientID %>">User Id</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Enter user id" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtcoupon.ClientID %>">Coupon Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                    <asp:TextBox ID="txtcoupon" CssClass="form-control" runat="server" placeholder="Enter coupon code" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= ddRewardType.ClientID %>">Reward Type</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-gift"></i></span>
                                                    <asp:DropDownList ID="ddRewardType" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="">All</asp:ListItem>
                                                        <asp:ListItem Value="Coupon">Coupon</asp:ListItem>
                                                        <asp:ListItem Value="Shopping Point">Shopping Point</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= ddstatus.ClientID %>">Status</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-info-circle"></i></span>
                                                    <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="">All</asp:ListItem>
                                                        <asp:ListItem Value="Available">Available</asp:ListItem>
                                                        <asp:ListItem Value="Redeemed">Redeemed</asp:ListItem>
                                                        <asp:ListItem Value="Locked">Locked</asp:ListItem>
                                                        <asp:ListItem Value="Redeemable">Redeemable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-gift"></i> Bulk Reward List</h3>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-table"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Use Search to load rewards." />
                                    </span>
                                </div>
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No bulk rewards found."
                                        AllowPaging="true" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserId" HeaderText="User Id" />
                                            <asp:BoundField DataField="username" HeaderText="Name" />
                                            <asp:BoundField DataField="RewardType" HeaderText="Reward Type" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="DisplayCouponCode" HeaderText="Coupon Code" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="RedeemAfterDate" HeaderText="Redeem After" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("RewardStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApproveBy" HeaderText="Approve By" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
        function initSavingReportDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }
        $(function () { initSavingReportDatepickers(); });
        Sys.Application.add_load(function () { initSavingReportDatepickers(); });
    </script>
</asp:Content>

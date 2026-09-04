<%@ Page Title="Repurchase Level Income Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="RepurchaseLevelIncome.aspx.cs" Inherits="admin_RepurchaseLevelIncome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Repurchase Level Income Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
            <li class="active">Repurchase Level Income Report</li>
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
                                <p class="admin-report-intro">Self cashback and compressed repurchase level income from ROIDailyLevelIncomeTB.</p>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Filter Income</h5>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtfromdate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-o"></i></span>
                                                    <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txttodate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtuserid.ClientID %>">User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Enter user ID" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddlIncomeType.ClientID %>">Income Type</label>
                                                <asp:DropDownList ID="ddlIncomeType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Self" Text="Self Income"></asp:ListItem>
                                                    <asp:ListItem Value="Level" Text="Level Income"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-table"></i> Income Details</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <asp:Label ID="lblSummary" runat="server" CssClass="text-muted" style="margin-right:12px;" Text="Records 0 | Self Income 0.00 | Level 0.00 | Total 0.00"></asp:Label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="admin-export-excel-btn" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick="ExportToExcel" ToolTip="Export to Excel" />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No repurchase income records found."
                                        AllowPaging="true" PageSize="25" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <PagerStyle HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="Userid" HeaderText="User ID" />
                                            <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                            <asp:BoundField DataField="IncomeType" HeaderText="Type" />
                                            <asp:BoundField DataField="LevelNo" HeaderText="Level" />
                                            <asp:BoundField DataField="Fromuserid" HeaderText="From User" />
                                            <asp:BoundField DataField="FromUserName" HeaderText="From Name" />
                                            <asp:BoundField DataField="OrderNO" HeaderText="Order No" />
                                            <asp:BoundField DataField="BV" HeaderText="BV" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="IncomePer" HeaderText="%" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Income" HeaderText="Income" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="PaybleAmount" HeaderText="Payable" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Status1" HeaderText="Status" />
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
            <asp:PostBackTrigger ControlID="ImageButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function bindRepurchaseIncomeDatePickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }
        Sys.Application.add_load(bindRepurchaseIncomeDatePickers);
    </script>
</asp:Content>

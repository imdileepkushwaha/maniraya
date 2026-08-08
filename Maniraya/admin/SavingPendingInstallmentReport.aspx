<%@ Page Title="Pending Installment Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingPendingInstallmentReport.aspx.cs" Inherits="admin_SavingPendingInstallmentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=77" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Pending Installment Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Pending Installment Report</li>
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
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn admin-send-reminder-btn"
                                        onclick="showAdminModal('sendReminderModal'); return false;"
                                        title="Open reminder popup">
                                        <i class="fa fa-bell"></i>
                                        <span>Send Reminder</span>
                                    </button>
                                </div>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Filter by Installment Date or Approve Date. For Pending, use Installment Date — Approve Date is blank until approved.</p>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Filters</h5>
                                    <div class="row">
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddDateType.ClientID %>">Date Filter By</label>
                                                <asp:DropDownList ID="ddDateType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="InstallmentDate" Selected="True">Installment Date</asp:ListItem>
                                                    <asp:ListItem Value="ApproveDate">Approve Date</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtFromDate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-o"></i></span>
                                                    <asp:TextBox ID="txtFromDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtToDate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox ID="txtToDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtUserId.ClientID %>">User Id</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" placeholder="Enter user id" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtInstNo.ClientID %>">Installment No</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox ID="txtInstNo" CssClass="form-control" runat="server" placeholder="e.g. 2" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddStatus.ClientID %>">Status</label>
                                                <asp:DropDownList ID="ddStatus" CssClass="form-control" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
                                                    <asp:ListItem Value="">All Status</asp:ListItem>
                                                    <asp:ListItem Value="Processing">Processing</asp:ListItem>
                                                    <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                                    <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-table"></i> Installment List</h3>
                                <span class="admin-box-header-summary">
                                    <asp:Label ID="lblSummary" runat="server" Text="Use Search to load records." />
                                </span>
                                <div class="box-tools admin-record-filter-tools">
                                    <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem Selected="True">25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="admin-record-filter-suffix">per page</span>
                                    <asp:ImageButton ID="btnExcel" runat="server" CssClass="admin-export-excel-btn"
                                        ImageUrl="../user/img/excel123.png" Height="25px" Width="25px"
                                        OnClick="btnExcel_Click" ToolTip="Export to Excel" />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-list"></i>
                                        <asp:Label ID="lblToolbarSummary" runat="server" Text="Pending installment records" />
                                    </span>
                                </div>
                                <div class="admin-table-paged-shell">
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No installment records found for selected filters."
                                        AllowPaging="true">
                                        <PagerSettings Visible="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# (GridView1.PageIndex * GridView1.PageSize) + Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate><%# Eval("userid") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate><%# Eval("mobile") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Id">
                                                <ItemTemplate><%# Eval("email") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Installment Date">
                                                <ItemTemplate><%# Eval("installmentdate", "{0:dd/MM/yyyy}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve Date">
                                                <ItemTemplate><%# Eval("approvedate", "{0:dd/MM/yyyy hh:mm tt}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inst No">
                                                <ItemTemplate><%# Eval("instno") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <span class="admin-amount-text"><%# Eval("amount", "{0:N2}") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate><%# Eval("status") %></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar"></asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="sendReminderModal" class="modal fade admin-modal-scrollable" tabindex="-1" role="dialog" aria-labelledby="sendReminderModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg admin-reminder-modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="sendReminderModalTitle"><i class="fa fa-bell"></i> Send Reminder</h4>
                            </div>
                            <div class="modal-body">
                                <p class="admin-report-intro" style="margin-top:0;">Search by Installment No and Status, then select rows to send SMS reminder (name only is personalized).</p>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtReminderInstNo.ClientID %>">Installment No</label>
                                            <asp:TextBox ID="txtReminderInstNo" runat="server" CssClass="form-control" placeholder="e.g. 2" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddReminderStatus.ClientID %>">Status</label>
                                            <asp:DropDownList ID="ddReminderStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <div>
                                                <asp:Button ID="btnReminderSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnReminderSearch_Click" CausesValidation="false" />
                                                <asp:Button ID="btnReminderReset" runat="server" CssClass="btn btn-default" Text="Reset" OnClick="btnReminderReset_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lblReminderSummary" runat="server" CssClass="admin-box-header-summary" style="display:block;margin:0 0 10px 0;" Text="" />
                                <div class="admin-table-wrap table-responsive" style="max-height:360px;">
                                    <asp:GridView ID="gvReminder" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No records found. Search by Installment No / Status."
                                        DataKeyNames="id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkReminderAll" runat="server" onclick="toggleReminderSelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkReminder" runat="server" />
                                                    <asp:HiddenField ID="hdnReminderUserId" runat="server" Value='<%# Eval("userid") %>' />
                                                    <asp:HiddenField ID="hdnReminderMobile" runat="server" Value='<%# Eval("mobile") %>' />
                                                    <asp:HiddenField ID="hdnReminderName" runat="server" Value='<%# Eval("username") %>' />
                                                    <asp:HiddenField ID="hdnReminderInstNo" runat="server" Value='<%# Eval("instno") %>' />
                                                    <asp:HiddenField ID="hdnReminderAmount" runat="server" Value='<%# Eval("amount") %>' />
                                                    <asp:HiddenField ID="hdnReminderDate" runat="server" Value='<%# Eval("installmentdate", "{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate><%# Eval("userid") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate><%# Eval("mobile") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inst No">
                                                <ItemTemplate><%# Eval("instno") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Installment Date">
                                                <ItemTemplate><%# Eval("installmentdate", "{0:dd/MM/yyyy}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate><%# Eval("amount", "{0:N2}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate><%# Eval("status") %></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer admin-modal-footer">
                                <asp:Label ID="lblReminderSendStatus" runat="server" CssClass="pull-left text-muted" style="margin-top:7px;" />
                                <asp:Button ID="btnSendReminderWhatsApp" runat="server" CssClass="btn admin-send-reminder-btn"
                                    Text="Send Reminder" OnClick="btnSendReminderWhatsApp_Click" CausesValidation="false"
                                    OnClientClick="return confirm('Send SMS reminder to selected users?');" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initPendingInstallmentDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        function toggleReminderSelectAll(source) {
            var checked = source.checked;
            $('#sendReminderModal input[id*="chkReminder"]').each(function () {
                if (this === source) {
                    return;
                }
                this.checked = checked;
            });
        }

        $(function () {
            initPendingInstallmentDatepickers();
        });

        Sys.Application.add_load(function () {
            initPendingInstallmentDatepickers();
        });
    </script>
</asp:Content>

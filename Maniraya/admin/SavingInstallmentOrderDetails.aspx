<%@ Page Title="Saving Installment Orders" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingInstallmentOrderDetails.aspx.cs" Inherits="admin_SavingInstallmentOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=69" />
    <style>
        .saving-order-address-cell {
            display: flex;
            align-items: flex-start;
            gap: 10px;
            justify-content: space-between;
        }

        .saving-order-address-text {
            flex: 1;
            min-width: 0;
            font-size: 12px;
            line-height: 1.5;
            color: #334155;
        }

        .saving-order-icon-btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 34px;
            height: 34px;
            border-radius: 8px;
            border: 1px solid #cbd5e1;
            background: #fff;
            color: #0f172a;
            text-decoration: none !important;
            transition: all 0.2s ease;
            flex-shrink: 0;
        }

        .saving-order-icon-btn:hover,
        .saving-order-icon-btn:focus {
            background: #eff6ff;
            border-color: #93c5fd;
            color: #1d4ed8;
        }

        .saving-order-icon-btn.is-status {
            width: auto;
            min-width: 34px;
            padding: 0 10px;
            gap: 6px;
            font-size: 12px;
            font-weight: 600;
        }

        .saving-address-print-sheet {
            padding: 8px 4px;
        }

        .saving-address-print-brand {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            margin-bottom: 18px;
            padding-bottom: 12px;
            border-bottom: 2px solid #0f172a;
        }

        .saving-address-print-brand h4 {
            margin: 0;
            font-size: 1.15rem;
            font-weight: 700;
            color: #0f172a;
        }

        .saving-address-print-meta {
            font-size: 12px;
            color: #64748b;
            text-align: right;
        }

        .saving-address-print-box {
            border: 2px dashed #94a3b8;
            border-radius: 12px;
            padding: 18px 20px;
            background: #f8fafc;
        }

        .saving-address-print-label {
            margin: 0 0 8px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: #64748b;
        }

        .saving-address-print-name {
            margin: 0 0 6px;
            font-size: 1.2rem;
            font-weight: 700;
            color: #0f172a;
        }

        .saving-address-print-lines {
            margin: 0;
            font-size: 1rem;
            line-height: 1.7;
            color: #1e293b;
            white-space: pre-line;
        }

        .saving-address-print-footer {
            margin-top: 16px;
            font-size: 12px;
            color: #64748b;
        }

        .delivery-badge-confirmed { background: #dbeafe; color: #1d4ed8; }
        .delivery-badge-shipped { background: #e0e7ff; color: #4338ca; }
        .delivery-badge-out { background: #fef3c7; color: #b45309; }
        .delivery-badge-delivered { background: #dcfce7; color: #15803d; }

        .admin-delivery-badge {
            display: inline-flex;
            align-items: center;
            padding: 4px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.02em;
            text-transform: uppercase;
        }

        .saving-order-products {
            display: flex;
            flex-direction: column;
            gap: 6px;
            min-width: 180px;
        }

        .saving-order-product-item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 10px;
            padding: 6px 10px;
            border-radius: 8px;
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            font-size: 12px;
            color: #334155;
        }

        .saving-order-product-item strong {
            color: #0f766e;
            white-space: nowrap;
        }

        .saving-order-product-item.is-unassigned,
        .saving-order-modal-products li.is-unassigned {
            background: #fff7ed;
            border-color: #fdba74;
            color: #9a3412;
        }

        .saving-order-product-item.is-unassigned strong,
        .saving-order-modal-products li.is-unassigned strong {
            color: #c2410c;
        }

        .saving-order-product-count {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            margin-top: 2px;
            font-size: 11px;
            font-weight: 700;
            color: #64748b;
            text-transform: uppercase;
            letter-spacing: 0.04em;
        }

        .saving-order-modal-products {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        .saving-order-modal-products li {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            padding: 8px 10px;
            border-radius: 8px;
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            margin-bottom: 8px;
            font-size: 13px;
        }

        #statusUpdateModal .modal-body .form-group > label {
            display: block;
            font-size: 16px;
            font-weight: 700;
            color: #0f172a;
            margin-bottom: 8px;
        }

        #statusUpdateModal .saving-consignment-group {
            margin-top: 4px;
            display: none;
        }

        #statusUpdateModal .saving-consignment-group.is-open {
            display: block;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Installment Orders</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Purchase Management</a></li>
            <li class="active">Saving Installment Orders</li>
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
            <asp:HiddenField ID="hfOrderId" runat="server" />
            <asp:HiddenField ID="hfInstallmentId" runat="server" />

            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Orders</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Approved saving installment orders with shipping address and delivery status tracking.</p>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtOrderId.ClientID %>">Order Id</label>
                                            <asp:TextBox ID="txtOrderId" CssClass="form-control" runat="server" placeholder="Search order id" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtUserId.ClientID %>">User Id / Name</label>
                                            <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" placeholder="User id or name" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= ddDeliveryStatus.ClientID %>">Delivery Status</label>
                                            <asp:DropDownList ID="ddDeliveryStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="">All Status</asp:ListItem>
                                                <asp:ListItem>Confirmed</asp:ListItem>
                                                <asp:ListItem>Shipped</asp:ListItem>
                                                <asp:ListItem>Out for Delivery</asp:ListItem>
                                                <asp:ListItem>Delivered</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtFromDate.ClientID %>">Request From Date</label>
                                            <asp:TextBox ID="txtFromDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtToDate.ClientID %>">Request To Date</label>
                                            <asp:TextBox ID="txtToDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
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
                                <h3 class="box-title"><i class="fa fa-truck"></i> Confirmed Installment Orders</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="admin-record-filter-suffix">per page</span>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption"><i class="fa fa-table"></i> Order dispatch list</span>
                                </div>
                                <div class="admin-table-paged-shell">
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                            AutoGenerateColumns="False" EmptyDataText="No confirmed installment orders found."
                                            AllowPaging="true"
                                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="id,orderid">
                                            <PagerSettings Visible="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Id">
                                                <ItemTemplate>
                                                    <strong><%# Eval("orderid") %></strong>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate><%# Eval("userid") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Products">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litProducts" runat="server" Mode="PassThrough" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <span class="admin-amount-text"><%# Eval("amount", "{0:N2}") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved On">
                                                <ItemTemplate><%# Eval("approvedate", "{0:dd/MM/yyyy hh:mm tt}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Address">
                                                <ItemTemplate>
                                                    <div class="saving-order-address-cell">
                                                        <span class="saving-order-address-text"><%# Eval("AddressSummary") %></span>
                                                        <asp:LinkButton ID="btnPrintAddress" runat="server" CssClass="saving-order-icon-btn"
                                                            CommandName="printaddress" CommandArgument='<%# Eval("orderid") %>' ToolTip="Print address">
                                                            <i class="fa fa-print"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryStatus" runat="server" Text='<%# Eval("DeliveryStatus") %>' CssClass="admin-delivery-badge" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnUpdateStatus" runat="server" CssClass="saving-order-icon-btn is-status"
                                                        CommandName="updatestatus" CommandArgument='<%# Eval("id") %>' ToolTip="Update delivery status for this product">
                                                        <i class="fa fa-pencil"></i> Status
                                                    </asp:LinkButton>
                                                </ItemTemplate>
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

                <div id="addressPrintModal" class="modal fade admin-image-preview-modal" tabindex="-1" role="dialog" aria-labelledby="addressPrintModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="addressPrintModalTitle"><i class="fa fa-print"></i> Shipping Address</h4>
                            </div>
                            <div class="modal-body">
                                <div id="addressPrintContent" class="saving-address-print-sheet">
                                    <asp:Literal ID="litPrintAddress" runat="server" Mode="PassThrough" />
                                </div>
                            </div>
                            <div class="modal-footer admin-modal-footer">
                                <button type="button" class="btn btn-primary" onclick="printSavingAddress(); return false;">
                                    <i class="fa fa-print"></i> Print Address
                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="statusUpdateModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="statusUpdateModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="statusUpdateModalTitle"><i class="fa fa-truck"></i> Update Delivery Status</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Order Id</label>
                                    <asp:TextBox ID="txtModalOrderId" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="form-group">
                                    <label>Customer Name</label>
                                    <asp:TextBox ID="txtModalUserName" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="form-group">
                                    <label>Product</label>
                                    <asp:Literal ID="litModalProducts" runat="server" Mode="PassThrough" />
                                </div>
                                <div class="form-group">
                                    <label for="<%= ddModalDeliveryStatus.ClientID %>">Delivery Status</label>
                                    <asp:DropDownList ID="ddModalDeliveryStatus" CssClass="form-control" runat="server" onchange="toggleConsignmentField();">
                                        <asp:ListItem>Confirmed</asp:ListItem>
                                        <asp:ListItem>Shipped</asp:ListItem>
                                        <asp:ListItem>Out for Delivery</asp:ListItem>
                                        <asp:ListItem>Delivered</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Panel ID="pnlConsignment" runat="server" CssClass="form-group saving-consignment-group">
                                    <label for="<%= txtConsignmentNumber.ClientID %>">Consignment Number</label>
                                    <asp:TextBox ID="txtConsignmentNumber" runat="server" CssClass="form-control" MaxLength="100" placeholder="Enter consignment / AWB number" />
                                </asp:Panel>
                            </div>
                            <div class="modal-footer admin-modal-footer">
                                <asp:Button ID="btnSaveDeliveryStatus" runat="server" CssClass="btn btn-primary" Text="Update Status" OnClick="btnSaveDeliveryStatus_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                window.toggleConsignmentField = function () {
                    var status = document.getElementById('<%= ddModalDeliveryStatus.ClientID %>');
                    var panel = document.getElementById('<%= pnlConsignment.ClientID %>');
                    if (!status || !panel) {
                        return;
                    }

                    var value = status.value || '';
                    if (status.selectedIndex >= 0 && status.options[status.selectedIndex]) {
                        value = status.options[status.selectedIndex].text || value;
                    }
                    value = (value || '').toLowerCase().replace(/\s+/g, ' ').trim();

                    var show = value === 'out for delivery' || value === 'out of delivery';
                    panel.className = show ? 'form-group saving-consignment-group is-open' : 'form-group saving-consignment-group';
                    panel.style.display = show ? 'block' : 'none';
                };
                window.toggleConsignmentField();
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initSavingOrderDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        $(function () {
            initSavingOrderDatepickers();
        });

        Sys.Application.add_load(function () {
            initSavingOrderDatepickers();
            initConsignmentToggle();
        });

        function initConsignmentToggle() {
            $(document).off('change.savingConsignment').on(
                'change.savingConsignment',
                '#<%= ddModalDeliveryStatus.ClientID %>',
                toggleConsignmentField
            );
            toggleConsignmentField();
        }

        function isOutForDeliveryStatus(value) {
            var status = (value || '').toLowerCase().replace(/\s+/g, ' ').trim();
            return status === 'out for delivery' || status === 'out of delivery';
        }

        function toggleConsignmentField() {
            var status = document.getElementById('<%= ddModalDeliveryStatus.ClientID %>');
            var panel = document.getElementById('<%= pnlConsignment.ClientID %>');
            if (!status || !panel) {
                return;
            }

            var value = status.value || '';
            if (status.selectedIndex >= 0 && status.options[status.selectedIndex]) {
                value = status.options[status.selectedIndex].text || value;
            }

            if (isOutForDeliveryStatus(value)) {
                panel.className = 'form-group saving-consignment-group is-open';
                panel.style.display = 'block';
            } else {
                panel.className = 'form-group saving-consignment-group';
                panel.style.display = 'none';
            }
        }

        function printSavingAddress() {
            var content = document.getElementById('addressPrintContent');
            if (!content) {
                return;
            }

            var printWindow = window.open('', '_blank', 'width=820,height=700');
            if (!printWindow) {
                alert('Please allow popups to print the address.');
                return;
            }

            printWindow.document.write('<!DOCTYPE html><html><head><title>Shipping Address</title>');
            printWindow.document.write('<style>');
            printWindow.document.write('body{font-family:Segoe UI,Arial,sans-serif;margin:24px;color:#0f172a;}');
            printWindow.document.write('.saving-address-print-brand{display:flex;justify-content:space-between;align-items:center;margin-bottom:18px;padding-bottom:12px;border-bottom:2px solid #0f172a;}');
            printWindow.document.write('.saving-address-print-brand h4{margin:0;font-size:20px;}');
            printWindow.document.write('.saving-address-print-meta{font-size:12px;color:#64748b;text-align:right;}');
            printWindow.document.write('.saving-address-print-box{border:2px dashed #94a3b8;border-radius:12px;padding:18px 20px;background:#f8fafc;}');
            printWindow.document.write('.saving-address-print-label{margin:0 0 8px;font-size:11px;font-weight:700;letter-spacing:.08em;text-transform:uppercase;color:#64748b;}');
            printWindow.document.write('.saving-address-print-name{margin:0 0 6px;font-size:20px;font-weight:700;}');
            printWindow.document.write('.saving-address-print-lines{margin:0;font-size:16px;line-height:1.7;white-space:pre-line;}');
            printWindow.document.write('.saving-address-print-footer{margin-top:16px;font-size:12px;color:#64748b;}');
            printWindow.document.write('</style></head><body>');
            printWindow.document.write(content.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
</asp:Content>

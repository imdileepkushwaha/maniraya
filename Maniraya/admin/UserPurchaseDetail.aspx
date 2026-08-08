<%@ Page Title="Franchisee Sales" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="UserPurchaseDetail.aspx.cs" Inherits="UserPurchaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=73" />
    <style>
        .purchase-detail-page .admin-filter-grid {
            display: grid;
            grid-template-columns: repeat(4, minmax(0, 1fr));
            gap: 14px 16px;
        }

        .purchase-detail-page .admin-filter-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            align-items: center;
        }

        .purchase-detail-page .table-responsive {
            margin: 0;
            border: 1px solid #e8edf3;
            border-radius: 10px;
            overflow: auto;
        }

        .purchase-detail-page .admin-table-paged-shell {
            border: 1px solid #e8edf3;
            border-radius: 12px;
            background: #fff;
            overflow: hidden;
        }

        .purchase-detail-page .admin-table-paged-shell > .table-responsive,
        .purchase-detail-page .admin-table-paged-shell > .admin-table-wrap {
            border: none;
            border-radius: 0;
            margin: 0;
        }

        .purchase-detail-page .admin-table-toolbar {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            margin-bottom: 14px;
            padding: 12px 14px;
            border-radius: 10px;
            background: #f8fafc;
            border: 1px solid #e8edf3;
        }

        .purchase-detail-page .admin-table-caption {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            font-size: 13px;
            font-weight: 700;
            color: #334155;
        }

        .purchase-detail-page .admin-table-caption i {
            color: #e52d27;
        }

        .purchase-detail-page .admin-table-pager-bar {
            display: flex !important;
            align-items: center;
            justify-content: center;
            flex-wrap: wrap;
            gap: 6px;
            padding: 12px 16px;
            border-top: 1px solid #e8edf3;
            background: linear-gradient(180deg, #fafbfc 0%, #f8fafc 100%);
        }

        .purchase-detail-page .admin-pager-info {
            width: 100%;
            margin: 0 0 6px;
            font-size: 12px;
            font-weight: 600;
            color: #64748b;
            text-align: center;
        }

        .purchase-detail-page .admin-pager-btn,
        .purchase-detail-page a.admin-pager-btn {
            display: inline-flex !important;
            align-items: center;
            justify-content: center;
            min-width: 36px;
            height: 36px;
            padding: 0 12px;
            margin: 0;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            line-height: 1;
            color: #64748b !important;
            text-decoration: none !important;
            background: #fff !important;
            border: 1px solid #e2e8f0 !important;
            box-sizing: border-box;
            white-space: nowrap;
        }

        .purchase-detail-page a.admin-pager-btn:hover,
        .purchase-detail-page a.admin-pager-btn:focus {
            color: #e52d27 !important;
            border-color: #f5c2bf !important;
            background: #fff5f5 !important;
        }

        .purchase-detail-page .admin-pager-btn.is-active {
            color: #fff !important;
            background: linear-gradient(135deg, #e52d27 0%, #c41e17 100%) !important;
            border-color: transparent !important;
            box-shadow: 0 2px 8px rgba(229, 45, 39, 0.25);
        }

        .purchase-detail-page .admin-pager-btn.is-disabled,
        .purchase-detail-page .admin-pager-btn.is-ellipsis {
            opacity: 0.5;
            cursor: default;
            pointer-events: none;
        }

        .purchase-detail-page .admin-pager-btn.is-ellipsis {
            opacity: 1;
            border-color: transparent !important;
            background: transparent !important;
            min-width: 24px;
        }

        .purchase-detail-page .purchase-expand-btn {
            width: 28px;
            height: 28px;
            cursor: pointer;
            border-radius: 6px;
            border: 1px solid #dbe3ee;
            background: #fff;
            padding: 2px;
            object-fit: contain;
        }

        .purchase-detail-page tr.purchase-child-row > td {
            background: #f8fafc;
            padding: 0 !important;
            border-top: 0 !important;
        }

        .purchase-detail-page .purchase-child-wrap {
            padding: 14px 16px 16px 48px;
        }

        .purchase-detail-page .purchase-child-wrap .table {
            margin-bottom: 0;
            background: #fff;
        }

        .purchase-detail-page .purchase-child-title {
            margin: 0 0 10px;
            font-size: 13px;
            font-weight: 700;
            color: #334155;
        }

        .purchase-detail-page .purchase-address-block {
            font-size: 12px;
            line-height: 1.55;
            color: #475569;
        }

        .purchase-detail-page .purchase-address-block strong {
            color: #1e293b;
            font-weight: 600;
        }

        .purchase-detail-page .btn-invoice-link {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            white-space: nowrap;
        }

        @media (max-width: 991px) {
            .purchase-detail-page .admin-filter-grid {
                grid-template-columns: repeat(2, minmax(0, 1fr));
            }
        }

        @media (max-width: 575px) {
            .purchase-detail-page .admin-filter-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Franchisee Sales</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Franchisee</a></li>
            <li class="active">Franchisee Sales</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <div class="purchase-detail-page admin-report-page">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Search Criteria</h3>
                            </div>
                            <div class="box-body admin-product-form">
                                <p class="admin-section-hint">Filter franchisee sales by date range, franchisee ID, and approval status.</p>
                                <div class="admin-filter-grid">
                                    <div class="form-group">
                                        <label for="<%= txtfromdate.ClientID %>">From Date</label>
                                        <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%= txttodate.ClientID %>">To Date</label>
                                        <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%= TxtFranchiseeId.ClientID %>">Franchisee ID</label>
                                        <asp:TextBox ID="TxtFranchiseeId" runat="server" CssClass="form-control" placeholder="Enter franchisee ID" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%= ddstatus.ClientID %>">Status</label>
                                        <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">All Status</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-product-footer">
                                <div class="admin-filter-actions">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Purchase Details</h3>
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
                                    <span class="admin-record-filter-suffix">records</span>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-table"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Use Search to load franchisee sales." />
                                    </span>
                                </div>
                                <div class="admin-table-paged-shell">
                                <div class="table-responsive admin-table-wrap">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No franchisee sales found for selected filters."
                                        AllowPaging="true" PageSize="10"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="PurchaseID">
                                        <PagerSettings Visible="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="40px">
                                                <ItemTemplate>
                                                    <img alt="Expand" class="purchase-expand-btn js-purchase-expand" src="img/PLUS.jpg" />
                                                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                        <div class="purchase-child-wrap">
                                                            <p class="purchase-child-title"><i class="fa fa-list"></i> Order Items</p>
                                                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="120px" DataField="ProductID" HeaderText="Product Code" />
                                                                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="70px">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40" Width="40" CssClass="img-thumbnail" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                                    <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                    <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                                                    <asp:BoundField DataField="PurchaseAmount" HeaderText="Purchase Amt" />
                                                                    <asp:TemplateField HeaderText="CGST">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblCGST" runat="server" Text='<%# Eval("CGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SGST">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblSGST" runat="server" Text='<%# Eval("SGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IGST">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblIGST" runat="server" Text='<%# Eval("IGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate><%# (GridView1.PageIndex * GridView1.PageSize) + Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid123" runat="server" Text='<%# Eval("PurchaseID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Franchisee / User">
                                                <ItemTemplate>
                                                    <asp:Label ID="Labeluserid" runat="server" Text='<%# Eval("userid") %>' Visible="false" />
                                                    <strong><%# Eval("FranchiseeName") %></strong>
                                                    <br />
                                                    <small class="text-muted">User: <asp:Label ID="lbluseridUsername" runat="server" Text='<%# Eval("Username") %>' /></small>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipping Address">
                                                <ItemTemplate>
                                                    <div class="purchase-address-block">
                                                        <div><strong>Address:</strong> <asp:Label ID="lblshippingaddress" runat="server" Text='<%# Eval("shippingaddress") %>' /></div>
                                                        <div><strong>City:</strong> <asp:Label ID="lblcity" runat="server" Text='<%# Eval("CityName") %>' /></div>
                                                        <div><strong>State:</strong> <asp:Label ID="lblstate" runat="server" Text='<%# Eval("StateName") %>' /></div>
                                                        <div><strong>Area:</strong> <asp:Label ID="lblarea" runat="server" Text='<%# Eval("ShippingAreaName") %>' /></div>
                                                        <div><strong>Pincode:</strong> <asp:Label ID="lblpincode" runat="server" Text='<%# Eval("shippingpincode") %>' /></div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluseridEmailId" runat="server" Text='<%# Eval("PurchaseAmount") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluseridContactNo" runat="server" Text='<%# Eval("CGST") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluseridaddress" runat="server" Text='<%# Eval("SGST") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("IGST") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid2" runat="server" Text='<%# Eval("TotalAmount") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Purchasedate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server"  CssClass="btn btn-default btn-xs btn-invoice-link"
                                                        NavigateUrl='<%# string.Format("../user/JoiningInvoice.aspx?OrderNo={0}", HttpUtility.UrlEncode(Eval("PurchaseID").ToString())) %>'
                                                        Text="Print" Target="_blank" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar" Visible="false"></asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initPurchaseDetailPage() {
            if (typeof $ === 'undefined') {
                return;
            }

            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });

            $(document).off('click.purchaseExpand').on('click.purchaseExpand', '.js-purchase-expand[src*="PLUS"]', function () {
                var $btn = $(this);
                var $row = $btn.closest('tr');
                var childHtml = $btn.next().html();

                if ($row.next().hasClass('purchase-child-row')) {
                    return;
                }

                $row.after('<tr class="purchase-child-row"><td colspan="12">' + childHtml + '</td></tr>');
                $btn.attr('src', 'img/Continue1.png');
            });

            $(document).off('click.purchaseCollapse').on('click.purchaseCollapse', '.js-purchase-expand[src*="Continue1"]', function () {
                var $btn = $(this);
                $btn.closest('tr').next('.purchase-child-row').remove();
                $btn.attr('src', 'img/PLUS.jpg');
            });
        }

        if (window.Sys && Sys.Application) {
            Sys.Application.add_load(initPurchaseDetailPage);
        } else {
            document.addEventListener('DOMContentLoaded', initPurchaseDetailPage);
        }
    </script>
</asp:Content>

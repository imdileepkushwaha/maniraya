<%@ Page Title="Coupon Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CouponReport.aspx.cs" Inherits="admin_CouponReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=74" />
    <style>
        .coupon-report-box-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            flex-wrap: wrap;
        }

        .coupon-report-header-tools {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-left: auto;
        }

        .coupon-report-count {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            margin: 0;
            padding: 7px 12px;
            border-radius: 999px;
            background: linear-gradient(135deg, rgba(229, 169, 6, 0.14) 0%, rgba(229, 169, 6, 0.06) 100%);
            border: 1px solid rgba(229, 169, 6, 0.28);
            color: #92400e;
            font-size: 13px;
            line-height: 1.2;
            white-space: nowrap;
        }

        .coupon-report-count i {
            font-size: 14px;
            color: #b45309;
        }

        .coupon-report-count-label {
            font-size: 13px;
            font-weight: 600;
            color: #78350f;
        }

        .coupon-report-count-value {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 28px;
            height: 28px;
            padding: 0 8px;
            border-radius: 999px;
            background: #fff;
            border: 1px solid rgba(229, 169, 6, 0.35);
            font-size: 14px;
            font-weight: 800;
            color: #0f172a;
            line-height: 1;
        }

        .coupon-report-header-tools .btn {
            font-size: 13px;
            font-weight: 600;
            padding: 7px 14px;
            line-height: 1.2;
        }

        .coupon-code {
            font-weight: 700;
            color: #0f172a;
            letter-spacing: 0.04em;
        }

        .coupon-print-toolbar {
            margin-top: 18px;
            padding-top: 16px;
            border-top: 1px dashed rgba(148, 163, 184, 0.45);
        }

        .coupon-print-hint {
            margin: 0;
            font-size: 0.88rem;
            color: #64748b;
        }

        .coupon-print-area {
            display: none;
        }

        .coupon-print-grid {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 14px;
        }

        .coupon-ticket {
            position: relative;
            border: 2px dashed #cbd5e1;
            border-radius: 14px;
            padding: 14px 14px 12px;
            background: linear-gradient(180deg, #fffdf5 0%, #ffffff 100%);
            box-shadow: 0 8px 20px rgba(15, 23, 42, 0.06);
            break-inside: avoid;
            page-break-inside: avoid;
        }

        .coupon-ticket::before,
        .coupon-ticket::after {
            content: "";
            position: absolute;
            top: 50%;
            width: 14px;
            height: 14px;
            margin-top: -7px;
            border-radius: 50%;
            background: #f8fafc;
            border: 2px dashed #cbd5e1;
        }

        .coupon-ticket::before { left: -9px; }
        .coupon-ticket::after { right: -9px; }

        .coupon-ticket-head {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 10px;
            margin-bottom: 10px;
            padding-bottom: 8px;
            border-bottom: 1px dashed rgba(229, 169, 6, 0.45);
        }

        .coupon-ticket-brand {
            font-size: 0.72rem;
            font-weight: 800;
            letter-spacing: 0.08em;
            text-transform: uppercase;
            color: #b45309;
        }

        .coupon-ticket-no {
            font-size: 0.72rem;
            font-weight: 700;
            color: #64748b;
        }

        .coupon-ticket-code {
            margin: 0;
            text-align: center;
            font-size: 1.6rem;
            font-weight: 800;
            letter-spacing: 0.1em;
            color: #0f172a;
            line-height: 1.3;
        }

        .coupon-ticket-copy {
            display: block;
            margin-top: 6px;
            font-size: 0.72rem;
            font-weight: 600;
            color: #64748b;
        }

        .coupon-ticket-meta {
            display: grid;
            gap: 6px;
            margin-top: 10px;
        }

        .coupon-ticket-row {
            display: flex;
            justify-content: space-between;
            gap: 10px;
            font-size: 0.82rem;
            line-height: 1.4;
        }

        .coupon-ticket-row span {
            color: #64748b;
            white-space: nowrap;
        }

        .coupon-ticket-row strong {
            color: #0f172a;
            text-align: right;
            word-break: break-word;
        }

        .coupon-ticket-foot {
            margin-top: 10px;
            padding-top: 8px;
            border-top: 1px dashed rgba(148, 163, 184, 0.45);
            text-align: center;
            font-size: 0.72rem;
            font-weight: 700;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #94a3b8;
        }

        .admin-report-page .admin-table-pager-bar {
            display: flex !important;
            align-items: center;
            justify-content: center;
            flex-wrap: wrap;
            gap: 6px;
            padding: 14px 16px;
            border-top: 1px solid #e8edf3;
            background: #f8fafc;
        }

        .admin-report-page .admin-pager-info {
            width: 100%;
            margin-bottom: 6px;
            text-align: center;
            font-size: 12px;
            font-weight: 600;
            color: #64748b;
        }

        .admin-report-page .admin-pager-btn,
        .admin-report-page a.admin-pager-btn {
            display: inline-flex !important;
            align-items: center;
            justify-content: center;
            min-width: 38px;
            height: 36px;
            padding: 0 12px;
            margin: 0;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            line-height: 1;
            color: #475569 !important;
            text-decoration: none !important;
            background: #fff !important;
            border: 1px solid #e2e8f0 !important;
            box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
            cursor: pointer;
        }

        .admin-report-page a.admin-pager-btn:hover,
        .admin-report-page a.admin-pager-btn:focus {
            color: #0f172a !important;
            border-color: #cbd5e1 !important;
            background: #f8fafc !important;
        }

        .admin-report-page .admin-pager-btn.is-active {
            color: #fff !important;
            background: #ef4444 !important;
            border-color: #ef4444 !important;
            box-shadow: 0 2px 8px rgba(239, 68, 68, 0.28);
        }

        .admin-report-page .admin-pager-btn.is-disabled {
            color: #cbd5e1 !important;
            background: #fff !important;
            border-color: #e2e8f0 !important;
            opacity: 1 !important;
            cursor: default;
            pointer-events: none;
        }

        .admin-report-page .admin-pager-btn.is-ellipsis {
            color: #64748b !important;
            cursor: default;
            pointer-events: none;
        }

        @media (max-width: 767px) {
            .coupon-print-grid {
                grid-template-columns: 1fr;
            }

            .coupon-report-header-tools {
                width: 100%;
                justify-content: flex-start;
                margin-left: 0;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Coupon Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Coupon Report</li>
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
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Coupons</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Filter approved coupons by Approve Month and Draw Type: <strong>Super Draw</strong> = installment approvals day 1–10 (InstNo ≠ 1); <strong>Mega Draw</strong> = installment approvals full month.</p>
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-calendar"></i> Approve Month &amp; Draw</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= ddlApproveMonth.ClientID %>">Approve Month</label>
                                                <asp:DropDownList ID="ddlApproveMonth" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlApproveMonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= ddlDrawType.ClientID %>">Draw Type</label>
                                                <asp:DropDownList ID="ddlDrawType" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDrawType_SelectedIndexChanged">
                                                    <asp:ListItem Text="-- Select Draw Type --" Value="" Selected="True" />
                                                    <asp:ListItem Text="Mega Draw" Value="Mega" />
                                                    <asp:ListItem Text="Super Draw" Value="Super" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12" style="display:none;" aria-hidden="true">
                                            <div class="form-group">
                                                <label for="<%= txtFromDate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form_date" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12" style="display:none;" aria-hidden="true">
                                            <div class="form-group">
                                                <label for="<%= txtToDate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form_date" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-sliders"></i> Filters</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtCouponCode.ClientID %>">Coupon Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-ticket"></i></span>
                                                    <asp:TextBox ID="txtCouponCode" runat="server" CssClass="form-control" placeholder="Search coupon code" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtUserId.ClientID %>">User Id / Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="User id or name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtMobile.ClientID %>">Mobile No</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-phone"></i></span>
                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile number" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border coupon-report-box-header">
                                <h3 class="box-title"><i class="fa fa-ticket"></i> Approved Coupons</h3>
                                <div class="box-tools coupon-report-header-tools">
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
                                    <p class="coupon-report-count">
                                        <i class="fa fa-print" aria-hidden="true"></i>
                                        <span class="coupon-report-count-label">Printable Tickets</span>
                                        <strong class="coupon-report-count-value"><asp:Literal ID="litCouponCount" runat="server" Text="0" /></strong>
                                    </p>
                                    <asp:Button ID="btnPrintAll" runat="server" CssClass="btn btn-primary"
                                        Text="Print All Coupons" OnClientClick="printAllCoupons(); return false;" Visible="false" />
                                </div>
                            </div>
                            <div class="box-body">
                                <p class="admin-report-intro">Approved installments only from <strong>SavingAccountInstallmentDetail</strong>. Super Draw: day 1–10 (InstNo ≠ 1). Mega Draw: full month. Print tickets for the draw.</p>

                                <asp:Panel ID="pnlLoadError" runat="server" Visible="false" CssClass="alert alert-warning admin-report-alert">
                                    <i class="fa fa-exclamation-triangle"></i>
                                    <asp:Literal ID="litLoadError" runat="server" />
                                </asp:Panel>

                                <div class="admin-table-paged-shell">
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server"
                                        CssClass="table table-bordered table-hover dataTable coupon-report-table"
                                        Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="true"
                                        EmptyDataText="No approved coupons found.">
                                        <PagerSettings Visible="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# GetSerialNumber(Container.DataItemIndex) %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coupon Code">
                                                <ItemTemplate><span class="coupon-code"><%# Eval("couponcode") %></span></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID">
                                                <ItemTemplate><%# Eval("userid") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No">
                                                <ItemTemplate><%# Eval("mobile") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate><%# Eval("productname") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate><%# Eval("quantity") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate><%# Eval("status") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve Date">
                                                <ItemTemplate><%# Eval("approvedate", "{0:dd MMM yyyy}") %></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Panel ID="pnlPager" runat="server" CssClass="admin-table-pager-bar"></asp:Panel>
                                </div>

                                <asp:Panel ID="pnlPrintArea" runat="server" CssClass="coupon-print-area" Visible="false">
                                    <div id="couponPrintArea" class="coupon-print-grid">
                                        <asp:Repeater ID="rptPrintCoupons" runat="server">
                                            <ItemTemplate>
                                                <div class="coupon-ticket">
                                                    <div class="coupon-ticket-head">
                                                        <span class="coupon-ticket-brand">Mpremium Lucky Draw</span>
                                                        <span class="coupon-ticket-no">#<%# Eval("TicketNo") %></span>
                                                    </div>
                                                    <p class="coupon-ticket-code">
                                                        <%# Eval("couponcode") %>
                                                        <asp:Label runat="server" CssClass="coupon-ticket-copy" Text='<%# Eval("copyLabel") %>'
                                                            Visible='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("copyLabel"))) %>' />
                                                    </p>
                                                    <div class="coupon-ticket-meta">
                                                        <div class="coupon-ticket-row"><span>User Name</span><strong><%# Eval("username") %></strong></div>
                                                        <div class="coupon-ticket-row"><span>User ID</span><strong><%# Eval("userid") %></strong></div>
                                                        <div class="coupon-ticket-row"><span>Mobile No</span><strong><%# Eval("mobile") %></strong></div>
                                                        <div class="coupon-ticket-row"><span>Approve Date</span><strong><%# Eval("approvedate") %></strong></div>
                                                    </div>
                                                    <div class="coupon-ticket-foot">Cut here · Lucky Draw Coupon</div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </asp:Panel>
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
        function initCouponReportDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        $(function () {
            initCouponReportDatepickers();
        });

        Sys.Application.add_load(function () {
            initCouponReportDatepickers();
        });

        function printAllCoupons() {
            var printArea = document.getElementById('couponPrintArea');
            if (!printArea || !printArea.children.length) {
                alert('No approved coupons available to print.');
                return;
            }

            var printStyles = [
                'body { margin: 0; padding: 12px; background: #fff; font-family: Arial, sans-serif; }',
                '.coupon-print-grid { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); gap: 12px; }',
                '.coupon-ticket { position: relative; border: 2px dashed #94a3b8; border-radius: 14px; padding: 16px 14px 12px; background: #fff; break-inside: avoid; page-break-inside: avoid; }',
                '.coupon-ticket-head { display: flex; align-items: center; justify-content: space-between; margin-bottom: 12px; padding-bottom: 8px; border-bottom: 1px dashed #e5a906; }',
                '.coupon-ticket-brand { font-size: 11px; font-weight: 800; letter-spacing: 0.08em; text-transform: uppercase; color: #b45309; }',
                '.coupon-ticket-no { font-size: 11px; font-weight: 700; color: #64748b; }',
                '.coupon-ticket-code { margin: 0 0 8px; text-align: center; font-size: 24px; font-weight: 800; letter-spacing: 0.1em; color: #0f172a; }',
                '.coupon-ticket-copy { display: block; margin-top: 6px; font-size: 11px; font-weight: 600; color: #64748b; }',
                '.coupon-ticket-meta { display: grid; gap: 6px; margin-top: 8px; }',
                '.coupon-ticket-row { display: flex; justify-content: space-between; gap: 10px; font-size: 12px; line-height: 1.4; }',
                '.coupon-ticket-row span { color: #64748b; }',
                '.coupon-ticket-row strong { color: #0f172a; text-align: right; word-break: break-word; }',
                '.coupon-ticket-foot { margin-top: 12px; padding-top: 8px; border-top: 1px dashed #cbd5e1; text-align: center; font-size: 10px; font-weight: 700; letter-spacing: 0.06em; text-transform: uppercase; color: #94a3b8; }',
                '@page { margin: 10mm; }'
            ].join('');

            var printWindow = window.open('', '_blank', 'width=900,height=700');
            if (!printWindow) {
                alert('Please allow popups to print coupons.');
                return;
            }

            printWindow.document.open();
            printWindow.document.write('<!DOCTYPE html><html><head><title>Coupon Print</title><style>' + printStyles + '</style></head><body>');
            printWindow.document.write('<div class="coupon-print-grid">' + printArea.innerHTML + '</div>');
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();

            printWindow.onload = function () {
                printWindow.print();
                printWindow.close();
            };

            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 400);
        }
    </script>
</asp:Content>

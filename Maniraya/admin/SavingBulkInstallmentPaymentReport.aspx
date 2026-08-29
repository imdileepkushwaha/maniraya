<%@ Page Title="Bulk EMI Payment Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingBulkInstallmentPaymentReport.aspx.cs" Inherits="admin_SavingBulkInstallmentPaymentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=66" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Bulk EMI Payment Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Bulk EMI Payment Report</li>
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
                                <p class="admin-report-intro">Approve or reject 17-EMI bulk payment requests. On approve, Inst 2–18 get approve dates one month apart from the user pay date. Rejection reason is shown to the user.</p>
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
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txtuserid.ClientID %>">User Id</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Enter user id" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= ddstatus.ClientID %>">Status</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-info-circle"></i></span>
                                                    <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Processing</asp:ListItem>
                                                        <asp:ListItem>Approved</asp:ListItem>
                                                        <asp:ListItem>Rejected</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="<%= txttransactionid.ClientID %>">Transaction Id / UTR No.</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-exchange"></i></span>
                                                    <asp:TextBox ID="txttransactionid" CssClass="form-control" runat="server" placeholder="Enter transaction id or UTR" />
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
                                <h3 class="box-title"><i class="fa fa-list-alt"></i> Bulk EMI Requests</h3>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-table"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Use Search to load requests." />
                                    </span>
                                </div>
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No bulk EMI payment requests found."
                                        OnRowDataBound="grdGetHelp_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                                    <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%# Eval("ImageName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coupon Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcouponcode" runat="server" Text='<%# Eval("CouponCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate><%# Eval("username") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate><%# Eval("productname") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMI Count">
                                                <ItemTemplate><%# Eval("InstCount") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" CssClass="admin-amount-text" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Date">
                                                <ItemTemplate><%# Eval("RequestDate", "{0:dd/MM/yyyy hh:mm tt}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UTR / Transaction Id">
                                                <ItemTemplate><%# Eval("OnlineTransactionId") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Screenshot">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkph" runat="server" CssClass="admin-product-thumb-link" CommandName="photolarge" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                        <img src='../ProductImage/<%# Eval("ImageName") %>' class="admin-product-thumb" alt="Payment screenshot" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Rejection reason (required to reject)"></asp:TextBox>
                                                    <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="admin-action-group">
                                                        <asp:LinkButton ID="btnApprove" CssClass="admin-action-btn is-approve" OnClick="btnApprove_click" runat="server">
                                                            <i class="fa fa-check"></i> Approve
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnReject" CssClass="admin-action-btn is-reject" OnClick="btnReject_click" runat="server">
                                                            <i class="fa fa-times"></i> Reject
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="DivPhotolarge" class="modal fade admin-image-preview-modal" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title"><i class="fa fa-picture-o"></i> Payment Screenshot</h4>
                            </div>
                            <div class="modal-body">
                                <div class="admin-image-preview-wrap">
                                    <asp:Image ID="ImageLarge" runat="server" CssClass="admin-image-preview-img" />
                                </div>
                            </div>
                            <div class="modal-footer admin-modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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

<%@ Page Title="Saving Product Installment Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingInstallmentReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=66" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Product Installment Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Installment Request</a></li>
            <li class="active">Saving Product Installment Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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
                                <p class="admin-report-intro">Filter by date, user ID, transaction ID / UTR, or status (Processing / Approved / Rejected). Default list shows latest Processing records.</p>
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
                                <asp:Button ID="btnPayAll" CssClass="btn btn-info" runat="server" Text="Approve All" OnClick="btnPayAll_Click" />
                              
                                </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-list-alt"></i> Installment Details</h3>
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
                                    <span class="admin-record-filter-suffix">records</span>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-table"></i>
                                        <asp:Label ID="lblSummary" runat="server" Text="Use Search to load installment requests." />
                                    </span>
                                </div>
                                <div class="admin-table-paged-shell">
                                    <div class="admin-table-wrap table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                            AutoGenerateColumns="False" EmptyDataText="No saving product installment requests found."
                                            AllowPaging="true" PageSize="25"
                                            OnRowDataBound="grdGetHelp_RowDataBound" OnRowCommand="GridView1_RowCommand"
                                            OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <PagerSettings Visible="false" />
                                            <Columns>
                                            <asp:TemplateField >
                                                 <HeaderTemplate>  
                                                    <%--<asp:CheckBox="CheckBox1" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />--%>
                                                    <asp:CheckBox ID="CheckBox1" OnCheckedChanged="chckchanged" AutoPostBack="true" runat="server" />
                                                </HeaderTemplate>  
                                                <ItemTemplate>  
                                                    <%--<asp:CheckBox="CheckBox2" runat="server" />--%>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    <%--<%#Container.DataItemIndex+1 %>--%>
                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                                    <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%#Eval("imagename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Order Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblorderid" runat="server" Text='<%#Eval("orderid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblurrorderid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Installment Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcreatingdate" runat="server" Text='<%#Eval("installmentdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreleasedate" runat="server" Text='<%#Eval("approvedate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaprodmount" runat="server" CssClass="admin-amount-text" Text='<%#Eval("amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%#Eval("productname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transaction Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("OnlineTransactionId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Image">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkph" runat="server" CssClass="admin-product-thumb-link" CommandName="photolarge" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                        <asp:Image ID="Image1" Visible="false" runat="server" ImageUrl='../ProductImage/<%#Eval("imagename") %>' Height="40px" Width="40px" />
                                                        <img src='../ProductImage/<%#Eval("imagename") %>' class="admin-product-thumb" alt="Product image" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblremark" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="admin-action-group">
                                                        <asp:LinkButton ID="btnApprove" CssClass="admin-action-btn is-approve" CommandName="approve" OnClick="btnApprove_click" runat="server">
                                                            <i class="fa fa-check"></i> Approve
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnReject" CssClass="admin-action-btn is-reject" CommandName="reject" OnClick="btnReject_click" runat="server">
                                                            <i class="fa fa-times"></i> Reject
                                                        </asp:LinkButton>
                                                    </div>
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

                <div id="DivPhotolarge" class="modal fade admin-image-preview-modal" tabindex="-1" role="dialog" aria-labelledby="savingProductImagePreviewTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="savingProductImagePreviewTitle"><i class="fa fa-picture-o"></i> Product Image Preview</h4>
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

        $(function () {
            initSavingReportDatepickers();
        });

        Sys.Application.add_load(function () {
            initSavingReportDatepickers();
        });
    </script>
</asp:Content>

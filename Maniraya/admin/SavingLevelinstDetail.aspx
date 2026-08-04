<%@ Page Title="Saving Level Installment Income" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingLevelinstDetail.aspx.cs" Inherits="admin_SavingLevelinstDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Level Installment Income</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
            <li class="active">Saving Level Installment Income</li>
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
                                <p class="admin-report-intro">Shows saving level income for installments only where <strong>Inst No &gt; 1</strong> (first installment excluded).</p>
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
                                                <label for="<%= txtInstNo.ClientID %>">Inst No (optional)</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox ID="txtInstNo" CssClass="form-control" runat="server" placeholder="e.g. 2" />
                                                </div>
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
                                <h3 class="box-title"><i class="fa fa-table"></i> Income Details (Inst No &gt; 1)</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="admin-export-excel-btn" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick="ExportToExcel" ToolTip="Export to Excel" />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No installment income records found (Inst No &gt; 1)." OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID">
                                                <ItemTemplate><asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate><asp:Label ID="lbluseridname" runat="server" Text='<%# Eval("username") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Junior User ID">
                                                <ItemTemplate><asp:Label ID="lbljunior" runat="server" Text='<%# Eval("JuniorUserId") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transaction ID">
                                                <ItemTemplate><asp:Label ID="lbltxn" runat="server" Text='<%# Eval("transactionid") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Level No">
                                                <ItemTemplate><asp:Label ID="lbllevel" runat="server" Text='<%# Eval("levelno") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inst No">
                                                <ItemTemplate><asp:Label ID="lblinstno" runat="server" Text='<%# Eval("InstNo") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate><asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate><asp:Label ID="lbldate" runat="server" Text='<%# Eval("mentiondate", "{0:dd/MM/yyyy}") %>' /></ItemTemplate>
                                            </asp:TemplateField>
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
        function bindSavingLevelInstDatePickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        Sys.Application.add_load(bindSavingLevelInstDatePickers);
    </script>
</asp:Content>

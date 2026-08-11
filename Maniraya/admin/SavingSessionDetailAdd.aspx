<%@ Page Title="Add Saving Session" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingSessionDetailAdd.aspx.cs" Inherits="admin_SavingSessionDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=77" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add Saving Session</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Add Saving Session</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-calendar"></i> New Session</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Add a saving session date range. New session becomes Active (Status=1); previous sessions are set Inactive.</p>
                                <div class="admin-form-section admin-form-section-last">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtFromDate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-o"></i></span>
                                                    <asp:TextBox ID="txtFromDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtToDate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox ID="txtToDate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Save Session"
                                    OnClick="btnSubmit_Click" OnClientClick="return validateSession();" />
                                <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset"
                                    OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-7">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-list"></i> Session List</h3>
                            </div>
                            <div class="box-body table-responsive">
                                <asp:GridView ID="gvSession" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-hover dataTable" Width="100%"
                                    EmptyDataText="No session found.">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Id" />
                                        <asp:BoundField DataField="fromdate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="todate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%# FormatStatus(Eval("status")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="mentionby" HeaderText="Entry By" />
                                        <asp:BoundField DataField="mentiondate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
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
        function initSessionDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        function validateSession() {
            var fromEl = document.getElementById('<%= txtFromDate.ClientID %>');
            var toEl = document.getElementById('<%= txtToDate.ClientID %>');
            if (!fromEl || !fromEl.value) {
                alert('Please enter From Date.');
                if (fromEl) fromEl.focus();
                return false;
            }
            if (!toEl || !toEl.value) {
                alert('Please enter To Date.');
                if (toEl) toEl.focus();
                return false;
            }
            return confirm('Save this saving session? Previous active sessions will become inactive.');
        }

        $(function () {
            initSessionDatepickers();
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initSessionDatepickers);
            }
        });
    </script>
</asp:Content>

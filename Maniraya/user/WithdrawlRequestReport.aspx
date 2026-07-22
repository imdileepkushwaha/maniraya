<%@ Page Title="Withdrawl Request Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="WithdrawlRequestReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=8" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Withdrawl Request Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">Withdrawl</a></li>
            <li class="active">Withdrawl Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page withdrawl-report-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-share"></i></div>
                    <div class="profile-hero-info">
                        <h2>Withdrawl Request Report</h2>
                        <p class="profile-hero-meta">Track the status of your withdrawal requests and payouts.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="WithdrawlRequstAdd.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-plus-circle"></i> New Request</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-1">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-filter"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Search Criteria</h3>
                                <p class="box-subtitle">Filter requests by date range and status</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row team-filter-grid">
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtfromdate.ClientID %>"><i class="fa fa-calendar"></i> From Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control form_date" ID="txtfromdate" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txttodate.ClientID %>"><i class="fa fa-calendar-check"></i> To Date</label>
                                    <asp:TextBox runat="server" CssClass="form-control form_date" ID="txttodate" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12">
                                <div class="form-group">
                                    <label for="<%= ddstatus.ClientID %>"><i class="fa fa-info-circle"></i> Status</label>
                                    <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Select Status</asp:ListItem>
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Approved</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false" />
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-6">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-list-alt"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Withdrawl Details</h3>
                                <p class="box-subtitle">List of your withdrawal requests</p>
                            </div>
                        </div>
                    </div>
                    <div class="box-body team-box-body">
                        <!-- <div class="team-table-toolbar">
                            <span class="team-table-caption"><i class="fa fa-table"></i> Request List</span>
                        </div> -->
                        <div class="form-group team-table-group">
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                    AutoGenerateColumns="False" EmptyDataText="No withdrawal requests found." OnRowDataBound="grdGetHelp_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Request">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreatingdate" runat="server" Text='<%#Eval("MentionDate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreleasedate" runat="server" Text='<%#Eval("approvedate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" CssClass="txn-amount" Text='<%#Eval("amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmode" runat="server" Text='<%#Eval("paymentmode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("OnlineTransactionId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
        function initWithdrawlDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        $(function () {
            initWithdrawlDatepickers();
        });

        Sys.Application.add_load(function () {
            initWithdrawlDatepickers();
        });
    </script>
</asp:Content>

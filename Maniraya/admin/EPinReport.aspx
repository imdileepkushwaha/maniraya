<%@ Page Title="E-Pin Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="EPinReport.aspx.cs" Inherits="admin_EPinReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>E-Pin Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">E-Pin Management</a></li>
            <li class="active">E-Pin Report</li>
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
                                <p class="admin-report-intro">Search generated E-Pins by user, status, and date range.</p>
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> User &amp; Status</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtgenerateuserid.ClientID %>">Generate User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtgenerateuserid"
                                                        placeholder="Enter generate user ID"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtuseduserid.ClientID %>">Used User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtuseduserid"
                                                        placeholder="Enter used user ID"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddstatus.ClientID %>">E-Pin Status</label>
                                                <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select E-Pin Status</asp:ListItem>
                                                    <asp:ListItem>Active</asp:ListItem>
                                                    <asp:ListItem>Used</asp:ListItem>
                                                    <asp:ListItem>Cancelled</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-calendar"></i> Date Range</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtfromdate.ClientID %>">From Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-o"></i></span>
                                                    <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"
                                                        placeholder="Select from date"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txttodate.ClientID %>">To Date</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                    <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server"
                                                        placeholder="Select to date"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Clear" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-list"></i> E-Pin Details</h3>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-toolbar">
                                    <span class="admin-table-caption">
                                        <i class="fa fa-check-circle"></i>
                                        Total Active:
                                        <asp:Label ID="LblCount" runat="server" Text="0"></asp:Label>
                                    </span>
                                </div>
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server"
                                        CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" GridLines="None"
                                        EmptyDataText="No E-Pin records found for selected filters.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="E-Pin No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblepinno" runat="server" Text='<%# Eval("EPinNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Generate User Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGenerateUserId" runat="server" Text='<%# Eval("GenerateUserId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblplanname" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Used User Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsedUserId" runat="server" Text='<%# Eval("UsedUserId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="E-Pin Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEPinStatus" runat="server" Text='<%# Eval("EPinStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMentionDate" runat="server" Text='<%# Eval("MentionDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
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
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initEpinReportDatepickers() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy'
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        $(document).ready(function () {
            initEpinReportDatepickers();
        });

        if (typeof Sys !== "undefined" && Sys.Application) {
            Sys.Application.add_load(initEpinReportDatepickers);
        }
    </script>
</asp:Content>

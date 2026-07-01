<%@ Page Title="Saving Level Payout Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PayoutReportSavingLevel.aspx.cs" Inherits="PayoutReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Saving Level Payout Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
            <li class="active">Saving Level Payout Report</li>
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
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">Search saving level income payout balances by user ID. Select rows and use Pay All to release payout.</p>
                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Filter Payout</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtuserid.ClientID %>">User ID</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                    <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Enter user ID" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnPayAll" CssClass="btn btn-info" runat="server" Text="Pay All Selected" OnClick="btnPayAll_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-money"></i> Payout Details</h3>
                                <div class="box-tools admin-record-filter-tools">
                                    <asp:ImageButton ID="imgExcel" runat="server" CssClass="admin-export-excel-btn" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick="imgExcel_Click" ToolTip="Export to Excel" />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="admin-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                        AutoGenerateColumns="False" EmptyDataText="No payout records found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBox1" OnCheckedChanged="chckchanged" AutoPostBack="true" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate><asp:Label ID="lblPaytmVendorCode" runat="server" Text='<%# Eval("username") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID">
                                                <ItemTemplate><asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate><asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account No">
                                                <ItemTemplate><asp:Label ID="lblBENE_ACC_NO" runat="server" Text='<%# Eval("AccountNo") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account Type">
                                                <ItemTemplate><asp:Label ID="lblaccounttype" runat="server" Text='<%# Eval("Accounttype") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Name">
                                                <ItemTemplate><asp:Label ID="lblbankname" runat="server" Text='<%# Eval("bankname") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch Name">
                                                <ItemTemplate><asp:Label ID="lblbranchname" runat="server" Text='<%# Eval("branchname") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IFSC Code">
                                                <ItemTemplate><asp:Label ID="lblBENE_IFSC" runat="server" Text='<%# Eval("ifsccode") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pan Number">
                                                <ItemTemplate><asp:Label ID="lblBENE_panIFSC" runat="server" Text='<%# Eval("PanNumber") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate><asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>' /></ItemTemplate>
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
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

<%@ Page Title="Deduction Charge" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="deductioncharge.aspx.cs" Inherits="deductioncharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Deduction Charge</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Deduction Charge</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Deduction Charge Settings</h3>
                        </div>

                        <div class="box-body admin-deduction-form">
                            <asp:HiddenField ID="hfId" runat="server" />
                            <p class="admin-deduction-intro">Configure admin charges, TDS rates, wallet settings and deposit limits for the platform.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-percent"></i> Tax &amp; Admin Charges</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtAdminCharge.ClientID %>">Admin Charge</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-money"></i></span>
                                                <asp:TextBox ID="TxtAdminCharge" runat="server" CssClass="form-control" placeholder="Enter admin charge" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtTdswithpam.ClientID %>">TDS With PAN</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-card"></i></span>
                                                <asp:TextBox ID="TxtTdswithpam" runat="server" CssClass="form-control" placeholder="TDS with PAN" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtTdswithoutpan.ClientID %>">TDS Without PAN</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-card-o"></i></span>
                                                <asp:TextBox ID="TxtTdswithoutpan" runat="server" CssClass="form-control" placeholder="TDS without PAN" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-wallet"></i> Wallet Settings</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtcashWallet.ClientID %>">Cash Wallet</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-credit-card"></i></span>
                                                <asp:TextBox ID="TxtcashWallet" runat="server" CssClass="form-control" placeholder="Cash wallet amount" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtcashWalletPercentage.ClientID %>">Cash Wallet Percentage</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-pie-chart"></i></span>
                                                <asp:TextBox ID="TxtcashWalletPercentage" runat="server" CssClass="form-control" placeholder="e.g. 10" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtCappingAmount.ClientID %>">Capping Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-level-up"></i></span>
                                                <asp:TextBox ID="TxtCappingAmount" runat="server" CssClass="form-control" placeholder="Maximum cap amount" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-bank"></i> Deposit Limits</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtMinAmt.ClientID %>">Min Deposit Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-arrow-down"></i></span>
                                                <asp:TextBox ID="TxtMinAmt" runat="server" CssClass="form-control" placeholder="Minimum deposit" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtMaxAmt.ClientID %>">Max Deposit Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-arrow-up"></i></span>
                                                <asp:TextBox ID="TxtMaxAmt" runat="server" CssClass="form-control" placeholder="Maximum deposit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-deduction-footer">
                            <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text="Update Settings" OnClick="btnUpdate_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

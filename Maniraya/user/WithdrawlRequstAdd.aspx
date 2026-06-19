<%@ Page Title="Withdrawl Request" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="WithdrawlRequstAdd.aspx.cs" Inherits="user_WithdrawlRequstAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/radio/style.css" rel="stylesheet" />
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Withdrawl Request</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Withdrawl</a></li>
            <li class="active">Withdrawl Fund</li>
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
            <div class="profile-page withdrawl-request-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-share"></i></div>
                    <div class="profile-hero-info">
                        <h2>Withdrawl Request</h2>
                        <p class="profile-hero-meta">Request a payout from your wallet. Applicable charges and TDS are calculated automatically.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="WithdrawlRequestReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list"></i> My Requests</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-5">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-money"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Withdrawl Details</h3>
                                <p class="box-subtitle">Enter the amount you wish to withdraw</p>
                            </div>
                        </div>
                    </div>

                    <div class="box-body profile-form-grid">
                        <div class="row" style="display:none">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RDBtnTRecharge" runat="server" Text="Recharge Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnTRecharge_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RdBtnUtility" runat="server" Text="Utility Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnUtility_CheckedChanged" />
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-user-circle"></i> Member Details</p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txtuserid.ClientID %>"><i class="fa fa-id-badge"></i> User Id</label>
                                    <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" placeholder="Enter user id" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txtusername.ClientID %>"><i class="fa fa-user"></i> User Name</label>
                                    <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-inr"></i> Amount</p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txtbalance.ClientID %>"><i class="fa fa-wallet"></i> Account Balance</label>
                                    <asp:TextBox ID="txtbalance" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txtamount.ClientID %>"><i class="fa fa-pencil"></i> Enter Amount</label>
                                    <asp:TextBox ID="txtamount" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" OnTextChanged="txtamount_TextChanged" AutoPostBack="true" placeholder="0.00" />
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-percent"></i> Charges &amp; Deductions</p>
                        <div class="row">
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtadminper.ClientID %>">Admin Percent</label>
                                    <asp:TextBox ID="txtadminper" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txtadmincharge.ClientID %>">Admin Charge</label>
                                    <asp:TextBox ID="txtadmincharge" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txttdsper.ClientID %>">TDS Percent</label>
                                    <asp:TextBox ID="txttdsper" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txttdscharge.ClientID %>">TDS Charge</label>
                                    <asp:TextBox ID="txttdscharge" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="withdrawl-net-amount">
                            <div class="withdrawl-net-amount-text">
                                <span class="withdrawl-net-amount-label">Net Payable Amount</span>
                                <span class="withdrawl-net-amount-hint">Amount credited after charges &amp; TDS</span>
                            </div>
                            <div class="withdrawl-net-amount-field">
                                <i class="fa fa-inr" aria-hidden="true"></i>
                                <asp:TextBox ID="txttotalamount" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="row" style="display:none">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Image</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Image</label>
                                    <asp:FileUpload ID="ImageUpload" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="topup-info-note">
                            <i class="fa fa-info-circle" aria-hidden="true"></i>
                            <span>The net payable amount is calculated automatically after deducting admin charges and TDS from the entered amount.</span>
                        </div>
                    </div>

                    <div class="box-footer profile-password-actions">
                        <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary profile-btn-primary-action" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-default profile-btn-secondary-action" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function validate() {
        }
    </script>
</asp:Content>

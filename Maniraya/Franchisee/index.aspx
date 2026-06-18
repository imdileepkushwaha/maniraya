<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title><%= clsUtility.ProjectName %> | Franchisee Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=5" />
    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../myassets/assets/css/maniraya-loader.css?v=1" />
    <link rel="stylesheet" href="assets/css/franchisee-login.css?v=3" />
    <script type="text/javascript">
        function validate2() {
            if (document.getElementById("<%=TxtOtp.ClientID%>").value === "") {
                alert('Enter OTP');
                document.getElementById("<%=TxtOtp.ClientID%>").focus();
                return false;
            }
        }
        function validate3() {
            if (document.getElementById("<%=txtuserid.ClientID%>").value === "") {
                alert('Enter User Id');
                document.getElementById("<%=txtuserid.ClientID%>").focus();
                return false;
            }
        }
    </script>
</head>
<body class="fr-login-page">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel runat="server" ID="uplMaster">
            <ContentTemplate>
                <div class="fr-login-shell">
                    <div class="fr-login-card">
                        <div class="fr-login-brand">
                            <div class="fr-login-logo-wrap">
                                <span class="brand-text-mpremium fr-login-brand-text">MPremium</span>
                            </div>
                            <h1><%= clsUtility.ProjectName %></h1>
                            <p>Franchisee Portal</p>
                            <span class="fr-login-badge"><i class="fa fa-building" aria-hidden="true"></i> Secure franchisee access</span>
                        </div>

                        <div class="fr-login-form-wrap">
                            <div class="fr-login-form-head">
                                <h2>Sign in</h2>
                                <p>Enter your credentials to access your franchisee dashboard</p>
                            </div>

                            <div class="fr-login-field">
                                <label for="<%= txtusername.ClientID %>"><i class="fa fa-user" aria-hidden="true"></i> Username</label>
                                <div class="fr-login-input-wrap">
                                    <asp:TextBox ID="txtusername" CssClass="form-control" runat="server" placeholder="Enter username"></asp:TextBox>
                                </div>
                            </div>

                            <div class="fr-login-field">
                                <label for="<%= txtpassword.ClientID %>"><i class="fa fa-lock" aria-hidden="true"></i> Password</label>
                                <div class="fr-login-input-wrap fr-login-input-wrap--password">
                                    <asp:TextBox ID="txtpassword" TextMode="Password" CssClass="form-control" placeholder="Enter password" runat="server"></asp:TextBox>
                                    <button type="button" class="fr-login-toggle" id="frPasswordToggle" aria-label="Show password">
                                        <i class="fa fa-eye" aria-hidden="true"></i>
                                    </button>
                                </div>
                            </div>

                            <div class="fr-login-actions">
                                <asp:Button runat="server" ID="btnLogin" Text="Sign In" CssClass="btn fr-login-submit" OnClick="btnLogin_Click" />
                                <button type="button" class="fr-login-forgot" onclick="showModal();">Forgot password?</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade fr-login-modal" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Forgot Password</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <p class="fr-modal-hint">Enter your user id and we will send an OTP to your registered mobile number.</p>
                                <label class="fr-modal-label" for="<%= txtuserid.ClientID %>">User Id</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtuserid" placeholder="Enter your user id"></asp:TextBox>
                            </div>
                            <asp:Panel ID="divsuccess" runat="server" Visible="false" CssClass="fr-modal-alert">
                                <strong>Success!</strong> <asp:Label ID="lblmessage" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-fr-secondary" data-dismiss="modal">Close</button>
                                <asp:Button ID="btnSend" runat="server" Text="Submit" OnClientClick="return validate3();" CssClass="btn btn-fr-primary" OnClick="btnSend_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="Divotp" class="modal fade fr-login-modal" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">OTP Confirmation</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <p class="fr-modal-hint">Enter the OTP sent to your registered mobile number to reset your password.</p>
                                <label class="fr-modal-label" for="<%= TxtOtp.ClientID %>">OTP</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TxtOtp" placeholder="Enter OTP"></asp:TextBox>
                                <asp:Label ID="LblMessages" runat="server" Text="Invalid OTP please enter valid OTP...!" CssClass="fr-modal-error" Visible="false"></asp:Label>
                            </div>
                            <asp:Panel ID="div2" runat="server" Visible="false" CssClass="fr-modal-alert">
                                <strong>Success!</strong> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-fr-secondary" data-dismiss="modal">Close</button>
                                <asp:Button ID="BtnResend" runat="server" Text="Resend OTP" CssClass="btn btn-fr-info" OnClick="BtnResend_Click" />
                                <asp:Button ID="BtnConfirm" runat="server" Text="Submit OTP" OnClientClick="return validate2();" CssClass="btn btn-fr-primary" OnClick="BtnConfirm_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="../myassets/assets/js/maniraya-loader.js?v=1"></script>
    <script type="text/javascript">
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function showModal12() {
            $('#Divotp').modal({ backdrop: 'static', keyboard: false });
        }
        function Closepopup1() {
            $('#Divotp').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }

        (function () {
            var toggle = document.getElementById('frPasswordToggle');
            var passwordInput = document.getElementById('<%= txtpassword.ClientID %>');
            if (toggle && passwordInput) {
                toggle.addEventListener('click', function () {
                    var isPassword = passwordInput.type === 'password';
                    passwordInput.type = isPassword ? 'text' : 'password';
                    toggle.innerHTML = isPassword
                        ? '<i class="fa fa-eye-slash" aria-hidden="true"></i>'
                        : '<i class="fa fa-eye" aria-hidden="true"></i>';
                });
            }
        })();
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title><%= clsUtility.ProjectName %> | Admin Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=5" />
    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/admin-login.css?v=1" />
</head>
<body class="admin-login-page">
    <form id="form1" runat="server">
        <div class="admin-login-shell">
            <div class="admin-login-card">
                <div class="admin-login-brand">
                    <div class="admin-login-logo-wrap">
                        <span class="brand-text-mpremium admin-login-brand-text">MPremium</span>
                    </div>
                    <h1><%= clsUtility.ProjectName %></h1>
                    <p>Admin Control Panel</p>
                    <span class="admin-login-badge"><i class="fa fa-shield" aria-hidden="true"></i> Secure administrator access</span>
                </div>

                <div class="admin-login-form-wrap">
                    <div class="admin-login-form-head">
                        <h2>Sign in</h2>
                        <p>Enter your credentials to access the admin dashboard</p>
                    </div>

                    <div class="admin-login-field">
                        <label for="<%= txtusername.ClientID %>"><i class="fa fa-user" aria-hidden="true"></i> Username</label>
                        <div class="admin-login-input-wrap">
                            <asp:TextBox ID="txtusername" CssClass="form-control" runat="server" placeholder="Enter admin username"></asp:TextBox>
                        </div>
                    </div>

                    <div class="admin-login-field">
                        <label for="<%= txtpassword.ClientID %>"><i class="fa fa-lock" aria-hidden="true"></i> Password</label>
                        <div class="admin-login-input-wrap admin-login-input-wrap--password">
                            <asp:TextBox ID="txtpassword" TextMode="Password" CssClass="form-control" placeholder="Enter password" runat="server"></asp:TextBox>
                            <button type="button" class="admin-login-toggle" id="adminPasswordToggle" aria-label="Show password">
                                <i class="fa fa-eye" aria-hidden="true"></i>
                            </button>
                        </div>
                    </div>

                    <asp:Button runat="server" ID="btnLogin" Text="Sign In" CssClass="btn admin-login-submit" OnClick="btnLogin_Click" />

                    <div class="admin-login-footer">
                        <a href="../user/index.aspx"><i class="fa fa-arrow-left"></i> Back to user login</a>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <script type="text/javascript">
        (function () {
            var toggle = document.getElementById('adminPasswordToggle');
            var passwordInput = document.getElementById('<%= txtpassword.ClientID %>');

            if (toggle && passwordInput) {
                toggle.addEventListener('click', function () {
                    var isPassword = passwordInput.type === 'password';
                    passwordInput.type = isPassword ? 'text' : 'password';
                    toggle.innerHTML = isPassword
                        ? '<i class="fa fa-eye-slash" aria-hidden="true"></i>'
                        : '<i class="fa fa-eye" aria-hidden="true"></i>';
                    toggle.setAttribute('aria-label', isPassword ? 'Hide password' : 'Show password');
                });
            }
        })();
    </script>
</body>
</html>

<%@ Page Title="Change Password" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="CHangePassword.aspx.cs" Inherits="admin_CHangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=4" rel="stylesheet" />
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=txtoldpassword.ClientID%>").value == "") {

                alert('Enter Old Password');
                document.getElementById("<%=txtoldpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                alert('Enter New Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                alert('Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value != document.getElementById("<%=txtconfirmpassword.ClientID%>").value) {

                alert('Password Not Match');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Change Password</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="UserProfile.aspx">My Profile</a></li>
            <li class="active">Change Password</li>
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
            <div class="profile-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-lock"></i></div>
                    <div class="profile-hero-info">
                        <h2>Change Password</h2>
                        <p class="profile-hero-meta">Keep your account secure with a strong password</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProfile.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-user"></i> View Profile</a>
                    </div>
                </div>
                <div class="profile-sections">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Update Password</h3>
                        </div>
                        <div class="box-body">
                            <div class="row profile-password-grid">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label><i class="fa fa-key"></i> Old Password</label>
                                        <div class="profile-password-field">
                                            <asp:TextBox ID="txtoldpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Enter old password"></asp:TextBox>
                                            <button type="button" class="profile-password-toggle" aria-label="Show password" title="Show password">
                                                <i class="fa fa-eye" aria-hidden="true"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label><i class="fa fa-lock"></i> New Password</label>
                                        <div class="profile-password-field">
                                            <asp:TextBox ID="txtuserpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Enter new password"></asp:TextBox>
                                            <button type="button" class="profile-password-toggle" aria-label="Show password" title="Show password">
                                                <i class="fa fa-eye" aria-hidden="true"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label><i class="fa fa-check-circle"></i> Confirm Password</label>
                                        <div class="profile-password-field">
                                            <asp:TextBox ID="txtconfirmpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Confirm new password"></asp:TextBox>
                                            <button type="button" class="profile-password-toggle" aria-label="Show password" title="Show password">
                                                <i class="fa fa-eye" aria-hidden="true"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="profile-password-actions">
                                <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div id="divOTP" runat="server" visible="false" class="profile-otp-panel">
                                <p class="profile-subsection-title"><i class="fa fa-mobile"></i> OTP Verification</p>
                                <div class="row">
                                    <div class="col-md-12" style="text-align:center; margin-bottom: 12px;">
                                        <span id="msg" style="font-size: 14px; color:#be123c;" runat="server"></span>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><i class="fa fa-shield"></i> Enter OTP</label>
                                            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-success btn-block" OnClick="btnVerify_Click" Text="Verify OTP" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnVerify" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        (function () {
            function initPasswordToggles() {
                var fields = document.querySelectorAll('.profile-password-field');
                var i;

                for (i = 0; i < fields.length; i++) {
                    (function (wrap) {
                        if (wrap.getAttribute('data-bound') === '1') {
                            return;
                        }

                        var input = wrap.querySelector('input');
                        var btn = wrap.querySelector('.profile-password-toggle');
                        var icon;

                        if (!input || !btn) {
                            return;
                        }

                        wrap.setAttribute('data-bound', '1');

                        btn.addEventListener('click', function () {
                            var show = input.type === 'password';
                            input.type = show ? 'text' : 'password';
                            icon = btn.querySelector('i');
                            icon.className = show ? 'fa fa-eye-slash' : 'fa fa-eye';
                            btn.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
                            btn.setAttribute('title', show ? 'Hide password' : 'Show password');
                            btn.classList.toggle('is-visible', show);
                        });
                    })(fields[i]);
                }
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initPasswordToggles);
            } else {
                initPasswordToggles();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initPasswordToggles);
            }
        })();
    </script>
</asp:Content>


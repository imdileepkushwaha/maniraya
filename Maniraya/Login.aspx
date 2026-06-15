<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
	    function Toggle() {
	        let temp = document.getElementById("<%=loginPassword.ClientID%>");
            if (temp.type === "password") {
                temp.type = "text";
            } else {
                temp.type = "password";
            }
        }

        function showLoginLoader() {
            var loader = document.getElementById("authLoginLoader");
            if (loader) {
                loader.hidden = false;
                loader.setAttribute("aria-hidden", "false");
            }
            document.body.classList.add("auth-login-loading");
        }

        function hideLoginLoader() {
            var loader = document.getElementById("authLoginLoader");
            if (loader) {
                loader.hidden = true;
                loader.setAttribute("aria-hidden", "true");
            }
            document.body.classList.remove("auth-login-loading");
        }

        function setupLoginFormLoader() {
            hideLoginLoader();
            if (typeof hideSiteLoader === "function") {
                hideSiteLoader();
            }

            var mainForm = document.getElementById("form1");
            if (!mainForm) return;

            mainForm.addEventListener("submit", function () {
                var userId = document.getElementById("<%=txtEmail.ClientID%>");
                var password = document.getElementById("<%=loginPassword.ClientID%>");
                var submitter = document.activeElement;

                if (!userId || !password) return;
                if (!userId.value.trim() || !password.value.trim()) return;
                var captcha = document.getElementById("<%=txtCaptcha.ClientID%>");
                if (!captcha || !captcha.value.trim()) return;
                if (submitter && submitter.id === "<%=btnLogin.ClientID%>") {
                    showLoginLoader();
                }
            });
        }

        document.addEventListener("DOMContentLoaded", setupLoginFormLoader);
        window.addEventListener("pageshow", hideLoginLoader);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="authLoginLoader" class="auth-login-loader" hidden aria-hidden="true" aria-live="polite">
        <div class="auth-login-loader-card">
            <div class="site-loader-cart" aria-hidden="true">
                <span class="site-loader-wheel"></span>
                <span class="site-loader-wheel"></span>
            </div>
            <p class="auth-login-loader-brand">Maniraya</p>
            <p class="auth-login-loader-text">Signing you in...</p>
            <div class="site-loader-progress" aria-hidden="true">
                <span class="site-loader-progress-fill"></span>
            </div>
        </div>
    </div>
            <main class="login-page-main login-page-enhanced">
            <section class="auth-section">
                <div class="container">
                    <div class="auth-wrap">
                    <article class="auth-promo-card">
                        <div class="auth-promo-pattern" aria-hidden="true"></div>
                        <div class="auth-promo-content">
                           
                            <span class="auth-kicker">Trusted by shoppers</span>
                            <h1>Welcome back</h1>
                            <p>
                                Sign in to track orders, manage your wallet, and unlock member-only deals on every purchase.
                            </p>
                            <ul class="auth-feature-list">
                                <li>
                                    <span class="auth-feature-icon" aria-hidden="true">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M12 3L4 7v6c0 4.4 3.4 8.5 8 9.5 4.6-1 8-5.1 8-9.5V7l-8-4Z" stroke="currentColor" stroke-width="1.8"/><path d="m9.5 12 1.8 1.8L15 10.1" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                    </span>
                                    <span class="auth-feature-copy">
                                        <strong>Secure login</strong>
                                        <small>256-bit encrypted access</small>
                                    </span>
                                </li>
                                <li>
                                    <span class="auth-feature-icon" aria-hidden="true">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/><path d="M9 11v6M15 11v6M8 7V5a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                    </span>
                                    <span class="auth-feature-copy">
                                        <strong>Fast checkout</strong>
                                        <small>Saved addresses &amp; details</small>
                                    </span>
                                </li>
                                <li>
                                    <span class="auth-feature-icon" aria-hidden="true">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M4 9.5h16M6.5 9.5 7 5h10l.5 4.5M7 13.5h.01M12 13.5h.01M17 13.5h.01M6 18.5h12a2 2 0 0 0 2-2v-5H4v5a2 2 0 0 0 2 2Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                    </span>
                                    <span class="auth-feature-copy">
                                        <strong>Member rewards</strong>
                                        <small>Coupons &amp; cashback offers</small>
                                    </span>
                                </li>
                            </ul>
                            <div class="auth-trust-badges">
                                <span>SSL Secured</span>
                                <span>24/7 Support</span>
                                <span>Fast Delivery</span>
                            </div>
                        </div>
                        <div class="auth-promo-stats">
                            <div><strong>10K+</strong><span>Products</span></div>
                            <div><strong>50K+</strong><span>Happy Users</span></div>
                            <div><strong>4.8★</strong><span>Rating</span></div>
                        </div>
                    </article>

                    <article class="auth-form-card">
                        <div class="auth-form-top">
                            <div class="auth-form-header">
                                <span class="auth-form-icon" aria-hidden="true">
                                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none"><path d="M12 12C14.21 12 16 10.21 16 8C16 5.79 14.21 4 12 4C9.79 4 8 5.79 8 8C8 10.21 9.79 12 12 12ZM12 14C9.33 14 4 15.34 4 18V20H20V18C20 15.34 14.67 14 12 14Z" fill="currentColor"/></svg>
                                </span>
                                <div>
                                    <h2>Sign In</h2>
                                    <p class="auth-form-subtitle">Enter your credentials to access your account.</p>
                                </div>
                            </div>
                        </div>
                        <div id="loginForm" class="auth-form" >
                            <div class="auth-field">
                                <label for="loginEmail">User ID</label>
                                <div class="auth-input-wrap">
                                    <span class="auth-input-icon" aria-hidden="true">
                                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 12C14.21 12 16 10.21 16 8C16 5.79 14.21 4 12 4C9.79 4 8 5.79 8 8C8 10.21 9.79 12 12 12ZM12 14C9.33 14 4 15.34 4 18V20H20V18C20 15.34 14.67 14 12 14Z" fill="currentColor"/></svg>
                                    </span>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control auth-input" placeholder="Enter your User ID" />
                                </div>
                            </div>

                            <div class="auth-field">
                                <label for="loginPassword">Password</label>
                                <div class="auth-input-wrap password-field">
                                    <span class="auth-input-icon" aria-hidden="true">
                                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M17 10V8C17 5.24 14.76 3 12 3C9.24 3 7 5.24 7 8V10H5V20H19V10H17ZM9 8C9 6.34 10.34 5 12 5C13.66 5 15 6.34 15 8V10H9V8Z" fill="currentColor"/></svg>
                                    </span>
                                    <asp:TextBox
    ID="loginPassword"
    runat="server"
    TextMode="Password"
    CssClass="form-control auth-input"
    placeholder="Enter your password" />
                                  <button type="button" onclick="Toggle()" id="togglePasswordBtn" class="password-toggle" aria-label="Show password" data-target="loginPassword">
      <span class="eye-icon eye-open" aria-hidden="true">
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round"></path>
              <circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"></circle>
          </svg>
      </span>
      <span class="eye-icon eye-close" aria-hidden="true">
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M3 4L21 20" stroke="currentColor" stroke-width="1.7" stroke-linecap="round"></path>
              <path d="M9.9 6.3C10.58 6.1 11.28 6 12 6C16.36 6 20.03 8.56 21.34 12.42C20.95 13.56 20.35 14.59 19.58 15.47M14.12 14.18C13.57 14.7 12.84 15 12 15C10.34 15 9 13.66 9 12C9 11.14 9.37 10.37 9.96 9.82M6.22 8.15C4.74 9.14 3.59 10.62 2.85 12.42C4.16 16.28 7.84 18.84 12.2 18.84C13.65 18.84 15.02 18.56 16.25 18.04" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round"></path>
          </svg>
      </span>
  </button>
                              

<!-- MESSAGE -->


                                </div>
                            </div>

                            <div class="auth-field">
                                <label for="<%= txtCaptcha.ClientID %>">Security Code</label>
                                <div class="auth-captcha-row">
                                    <div class="auth-captcha-display" aria-label="Captcha code">
                                        <asp:Label ID="lblCaptchaCode" runat="server" CssClass="auth-captcha-code" />
                                    </div>
                                    <asp:LinkButton ID="lnkRefreshCaptcha" runat="server" CssClass="auth-captcha-refresh" CausesValidation="false" OnClick="lnkRefreshCaptcha_Click" ToolTip="Refresh code">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                            <path d="M20 12a8 8 0 1 1-2.34-5.66" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                                            <path d="M20 4v6h-6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                                        </svg>
                                        <span>Refresh</span>
                                    </asp:LinkButton>
                                    <div class="auth-input-wrap auth-input-wrap--plain auth-captcha-input-wrap">
                                        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control auth-input auth-captcha-input" placeholder="Type code" autocomplete="off" MaxLength="6" />
                                    </div>
                                </div>
                            </div>

                            <div class="auth-meta-row">
                                <label class="remember-me">
                                    <asp:CheckBox
    ID="chkRemember"
    runat="server"
    Text=" Remember Me" />
                                   
                                   
                                </label>
                                <a href="#" id="forgotPasswordLink" class="forgot-password">Forgot password?</a>
                            </div>
                            
                            <asp:Label ID="lblLoginError" runat="server" CssClass="auth-feedback" Visible="false" />

                            <div class="auth-form-actions">
                                <asp:Button
    ID="btnLogin"
    runat="server"
    Text="Sign In"
    CssClass="auth-submit-btn"
    CausesValidation="false"
    OnClick="btnLogin_Click"
    />
                                <p class="auth-signup-text">
                                    Don't have an account?
                                    <a href="signup.aspx" class="auth-signup-link">Create one free</a>
                                </p>
                            </div>
                        </div>
                    </article>
                    </div>
                </div>
            </section>
        </main>

        <div id="forgotPopupBackdrop" class="forgot-popup-backdrop" hidden>
            <div class="forgot-popup" role="dialog" aria-modal="true" aria-labelledby="forgotPopupTitle">
                <div class="forgot-popup-accent" aria-hidden="true"></div>
                <div class="forgot-popup-body">
                    <button id="closeForgotPopup" class="forgot-popup-close-btn" type="button" aria-label="Close reset password popup">
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                            <path d="M18 6L6 18M6 6L18 18" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"/>
                        </svg>
                    </button>
                    <div class="forgot-popup-head">
                        <span class="forgot-popup-icon" aria-hidden="true">
                            <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
                                <path d="M17 10V8C17 5.24 14.76 3 12 3C9.24 3 7 5.24 7 8V10H5V20H19V10H17ZM9 8C9 6.34 10.34 5 12 5C13.66 5 15 6.34 15 8V10H9V8Z" fill="currentColor"/>
                            </svg>
                        </span>
                        <div class="forgot-popup-head-text">
                            <h3 id="forgotPopupTitle">Reset your password</h3>
                            <p>We'll email you a secure link to create a new password.</p>
                        </div>
                    </div>
                    <div id="forgotPasswordForm" class="forgot-popup-form" role="group" aria-label="Reset password form">
                        <div class="forgot-popup-field">
                            <label for="forgotEmailInput">Email address</label>
                            <div class="forgot-popup-input-wrap">
                                <span class="forgot-popup-input-icon" aria-hidden="true">
                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
                                        <path d="M4 6H20V18H4V6ZM4 8L12 13L20 8" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>
                                    </svg>
                                </span>
                                <input id="forgotEmailInput" type="email" placeholder="you@example.com" autocomplete="email" />
                            </div>
                        </div>
                        <div class="forgot-popup-actions">
                            <button type="button" id="cancelForgotPopup" class="forgot-popup-btn forgot-popup-btn-ghost">Cancel</button>
                            <button type="button" id="submitForgotPopup" class="forgot-popup-btn forgot-popup-btn-primary">
                                <span>Send reset link</span>
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                    <path d="M5 12H19M19 12L13 6M19 12L13 18" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                                </svg>
                            </button>
                        </div>
                    </div>
                    <p class="forgot-popup-footnote">
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                            <path d="M12 8V12M12 16H12.01M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                        </svg>
                        Check your spam folder if you don't see the email within a few minutes.
                    </p>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


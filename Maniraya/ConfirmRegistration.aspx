<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmRegistration.aspx.cs" Inherits="user_ConfirmRegistration" MasterPageFile="~/WebMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Registration Confirmed | <%= clsUtility.ProjectName %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <main class="reg-confirm-page">
        <div class="container">
            <div class="reg-confirm-shell">
                <article class="reg-confirm-card">
                    <div class="reg-confirm-top" aria-hidden="true">
                        <span class="reg-confirm-dot reg-confirm-dot-1"></span>
                        <span class="reg-confirm-dot reg-confirm-dot-2"></span>
                        <span class="reg-confirm-dot reg-confirm-dot-3"></span>
                        <span class="reg-confirm-dot reg-confirm-dot-4"></span>
                        <span class="reg-confirm-dot reg-confirm-dot-5"></span>
                        <div class="reg-confirm-icon-wrap">
                            <svg class="reg-confirm-icon" width="42" height="42" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                <path d="M6 12.5l3.8 3.8L18 7.5" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                            </svg>
                        </div>
                    </div>

                    <div class="reg-confirm-content">
                        <div class="reg-confirm-brand">
                            <span class="site-brand-text reg-confirm-logo">MPremium</span>
                        </div>

                        <p class="reg-confirm-status">Registration Successful</p>

                        <h1 class="reg-confirm-title">
                            Thank You,
                            <span class="reg-confirm-name"><asp:Label ID="lblName" runat="server"></asp:Label></span>!
                        </h1>

                        <p class="reg-confirm-message">
                            Welcome to <strong><%= clsUtility.ProjectName %></strong>.
                            Your account has been created successfully. Please save the details below —
                            you will need them to sign in.
                        </p>

                        <div class="reg-confirm-details">
                            <div class="reg-confirm-detail-row">
                                <div class="reg-confirm-detail-copy">
                                    <span class="reg-confirm-detail-label">User ID</span>
                                    <asp:Label ID="LblLoginId" runat="server" CssClass="reg-confirm-detail-value" ClientIDMode="Static"></asp:Label>
                                </div>
                                <button type="button" class="reg-confirm-action-btn" data-copy-target="LblLoginId" aria-label="Copy User ID">
                                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true"><rect x="9" y="9" width="11" height="11" rx="2" stroke="currentColor" stroke-width="1.8"/><path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1" stroke="currentColor" stroke-width="1.8"/></svg>
                                    <span>Copy</span>
                                </button>
                            </div>

                            <div class="reg-confirm-detail-divider" aria-hidden="true"></div>

                            <div class="reg-confirm-detail-row">
                                <div class="reg-confirm-detail-copy">
                                    <span class="reg-confirm-detail-label">Password</span>
                                    <asp:Label ID="LblPassword" runat="server" CssClass="reg-confirm-detail-value reg-confirm-detail-secret" ClientIDMode="Static"></asp:Label>
                                </div>
                                <div class="reg-confirm-detail-actions">
                                    <button type="button" class="reg-confirm-action-btn reg-confirm-toggle-btn" data-toggle-target="LblPassword" aria-label="Show password">
                                        <svg class="reg-eye-open" width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7"/><circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"/></svg>
                                        <svg class="reg-eye-close" width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M3 4L21 20" stroke="currentColor" stroke-width="1.7" stroke-linecap="round"/></svg>
                                    </button>
                                    <button type="button" class="reg-confirm-action-btn" data-copy-target="LblPassword" aria-label="Copy password">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true"><rect x="9" y="9" width="11" height="11" rx="2" stroke="currentColor" stroke-width="1.8"/><path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1" stroke="currentColor" stroke-width="1.8"/></svg>
                                        <span>Copy</span>
                                    </button>
                                </div>
                            </div>
                        </div>

                        <p class="reg-confirm-footnote">
                            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                <path d="M12 8V12M12 16H12.01M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/>
                            </svg>
                            Keep your login details safe. Do not share your password with anyone.
                        </p>

                        <div class="reg-confirm-buttons">
                            <a href="Login.aspx" class="reg-confirm-btn reg-confirm-btn-primary">Login Now</a>
                            <a href="index.aspx" class="reg-confirm-btn reg-confirm-btn-secondary">Back to Home</a>
                        </div>
                    </div>

                    <asp:Label ID="LblSponsorName" runat="server" style="display:none"></asp:Label>
                    <asp:Label ID="LblSponsorId" runat="server" style="display:none"></asp:Label>
                </article>
            </div>
        </div>
    </main>

    <script type="text/javascript">
        (function () {
            function getText(id) {
                var el = document.getElementById(id);
                return el ? (el.textContent || "").trim() : "";
            }

            function copyFeedback(btn) {
                var label = btn.querySelector("span");
                if (!label) return;
                var original = label.textContent;
                label.textContent = "Copied!";
                btn.classList.add("is-copied");
                window.setTimeout(function () {
                    label.textContent = original;
                    btn.classList.remove("is-copied");
                }, 1500);
            }

            document.addEventListener("click", function (e) {
                var copyBtn = e.target.closest("[data-copy-target]");
                if (copyBtn && copyBtn.hasAttribute("data-copy-target")) {
                    var value = getText(copyBtn.getAttribute("data-copy-target"));
                    if (!value) return;
                    if (navigator.clipboard && navigator.clipboard.writeText) {
                        navigator.clipboard.writeText(value).then(function () { copyFeedback(copyBtn); });
                    } else {
                        var temp = document.createElement("textarea");
                        temp.value = value;
                        document.body.appendChild(temp);
                        temp.select();
                        document.execCommand("copy");
                        document.body.removeChild(temp);
                        copyFeedback(copyBtn);
                    }
                    return;
                }

                var toggleBtn = e.target.closest(".reg-confirm-toggle-btn");
                if (toggleBtn) {
                    var target = document.getElementById(toggleBtn.getAttribute("data-toggle-target"));
                    if (!target) return;
                    var hidden = target.classList.contains("is-masked");
                    target.classList.toggle("is-masked", !hidden);
                    toggleBtn.classList.toggle("is-visible", hidden);
                }
            });

            document.addEventListener("DOMContentLoaded", function () {
                var pwd = document.getElementById("LblPassword");
                if (pwd) pwd.classList.add("is-masked");
            });
        })();
    </script>
</asp:Content>

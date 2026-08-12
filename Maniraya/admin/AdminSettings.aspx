<%@ Page Title="Admin Settings" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AdminSettings.aspx.cs" Inherits="admin_AdminSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .settings-wrap { max-width: 1180px; margin: 0 auto; }
        .settings-hero h1 {
            margin: 0 0 6px;
            font-size: 28px;
            font-weight: 700;
            color: #0f172a;
        }
        .settings-hero p {
            margin: 0 0 18px;
            color: #64748b;
            font-size: 14px;
        }
        .settings-add-card {
            background: #fff;
            border: 1px solid #e8ecf1;
            border-radius: 14px;
            padding: 16px 18px;
            margin-bottom: 18px;
            box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
        }
        .settings-add-card h3 {
            margin: 0 0 12px;
            font-size: 15px;
            font-weight: 700;
            color: #0f172a;
        }
        .settings-add-grid {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr)) auto;
            gap: 12px;
            align-items: end;
        }
        .settings-field label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 600;
            color: #475569;
        }
        .settings-field .form-control {
            height: 40px;
            border-radius: 10px;
            border-color: #dbe3ee;
        }
        .settings-btn-primary {
            height: 40px;
            border: 0;
            border-radius: 10px;
            padding: 0 18px;
            background: #2563eb;
            color: #fff;
            font-weight: 600;
        }
        .settings-btn-primary:hover { background: #1d4ed8; color: #fff; }
        .settings-btn-save {
            height: 36px;
            border: 0;
            border-radius: 10px;
            padding: 0 16px;
            background: #16a34a;
            color: #fff;
            font-weight: 600;
        }
        .settings-btn-save:hover { background: #15803d; color: #fff; }
        .settings-btn-danger {
            height: 36px;
            border: 0;
            border-radius: 10px;
            padding: 0 16px;
            background: #dc2626;
            color: #fff;
            font-weight: 600;
        }
        .settings-btn-danger:hover { background: #b91c1c; color: #fff; }
        .settings-btn-secondary {
            height: 36px;
            border: 1px solid #cbd5e1;
            border-radius: 10px;
            padding: 0 16px;
            background: #fff;
            color: #0f172a;
            font-weight: 600;
        }
        .settings-btn-secondary:hover { background: #f8fafc; color: #0f172a; }
        .settings-user-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
            align-items: end;
            justify-content: flex-end;
        }
        .settings-password-box {
            margin: 0 16px 14px;
            padding: 14px;
            border: 1px solid #e8ecf1;
            border-radius: 12px;
            background: #f8fafc;
            display: grid;
            grid-template-columns: minmax(0, 1fr) minmax(0, 1fr) auto;
            gap: 12px;
            align-items: end;
        }
        .settings-password-box h4 {
            grid-column: 1 / -1;
            margin: 0;
            font-size: 13px;
            font-weight: 700;
            color: #0f172a;
        }
        @media (max-width: 767px) {
            .settings-password-box { grid-template-columns: 1fr; }
        }
        .settings-layout {
            display: grid;
            grid-template-columns: 280px minmax(0, 1fr);
            gap: 16px;
            align-items: start;
        }
        .settings-users-card,
        .settings-menus-card {
            background: #fff;
            border: 1px solid #e8ecf1;
            border-radius: 14px;
            box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
            overflow: hidden;
        }
        .settings-users-card h3,
        .settings-menus-head h3 {
            margin: 0;
            font-size: 15px;
            font-weight: 700;
        }
        .settings-users-head,
        .settings-menus-head {
            padding: 14px 16px;
            border-bottom: 1px solid #eef2f7;
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 12px;
        }
        .settings-users-list { max-height: 620px; overflow: auto; }
        .settings-user-item {
            display: flex;
            width: 100%;
            text-align: left;
            border: 0;
            border-bottom: 1px solid #f1f5f9;
            background: #fff;
            padding: 12px 14px;
            gap: 10px;
            align-items: center;
            cursor: pointer;
        }
        .settings-user-item:hover { background: #f8fafc; }
        .settings-user-item.is-active {
            background: #ecfdf5;
            box-shadow: inset 3px 0 0 #10b981;
        }
        .settings-user-avatar {
            width: 36px;
            height: 36px;
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-weight: 700;
            color: #fff;
            background: #0ea5e9;
            flex-shrink: 0;
        }
        .settings-user-copy { min-width: 0; }
        .settings-user-copy strong {
            display: block;
            font-size: 13px;
            color: #0f172a;
        }
        .settings-user-copy span {
            display: block;
            font-size: 11px;
            color: #64748b;
        }
        .settings-user-status {
            margin-left: auto;
            font-size: 11px;
            font-weight: 700;
            color: #16a34a;
        }
        .settings-menus-body { padding: 16px; }
        .settings-menus-empty {
            padding: 40px 16px;
            text-align: center;
            color: #94a3b8;
        }
        .settings-menu-group {
            margin-bottom: 20px;
            border: 1px solid #e8ecf1;
            border-radius: 14px;
            background: #fff;
            overflow: hidden;
        }
        .settings-menu-group-title {
            display: flex;
            align-items: center;
            gap: 0;
            margin: 0;
            padding: 0;
            border-bottom: 1px solid #eef2f7;
            background: linear-gradient(180deg, #f8fafc 0%, #f1f5f9 100%);
        }
        .settings-menu-group-title .chk-main {
            display: flex;
            align-items: center;
            width: 100%;
            margin: 0;
            padding: 12px 14px;
            cursor: pointer;
            user-select: none;
        }
        .settings-menu-group-title .chk-main label {
            display: flex !important;
            align-items: center;
            gap: 12px;
            width: 100%;
            margin: 0 !important;
            padding: 0 !important;
            font-size: 12px !important;
            font-weight: 800 !important;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #334155 !important;
            cursor: pointer;
            line-height: 1.2;
        }
        .settings-menu-group-title .chk-main input[type="checkbox"] {
            -webkit-appearance: none;
            appearance: none;
            width: 20px;
            height: 20px;
            margin: 0;
            flex-shrink: 0;
            border: 2px solid #94a3b8;
            border-radius: 6px;
            background: #fff;
            position: relative;
            cursor: pointer;
            transition: border-color 0.15s ease, background 0.15s ease, box-shadow 0.15s ease;
        }
        .settings-menu-group-title .chk-main input[type="checkbox"]:hover {
            border-color: #2563eb;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.12);
        }
        .settings-menu-group-title .chk-main input[type="checkbox"]:checked {
            background: #2563eb;
            border-color: #2563eb;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.16);
        }
        .settings-menu-group-title .chk-main input[type="checkbox"]:checked::after {
            content: "";
            position: absolute;
            left: 5px;
            top: 1px;
            width: 5px;
            height: 10px;
            border: solid #fff;
            border-width: 0 2px 2px 0;
            transform: rotate(45deg);
        }
        .settings-menu-group-title .chk-main label::before {
            content: "";
            width: 28px;
            height: 28px;
            border-radius: 8px;
            background: #e2e8f0;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='14' height='14' viewBox='0 0 24 24' fill='none' stroke='%23475569' stroke-width='2.2' stroke-linecap='round' stroke-linejoin='round'%3E%3Cpath d='M3 7h18M3 12h18M3 17h18'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: center;
            flex-shrink: 0;
        }
        .settings-menu-group.is-checked .settings-menu-group-title {
            background: linear-gradient(180deg, #eff6ff 0%, #dbeafe 100%);
            border-bottom-color: #bfdbfe;
        }
        .settings-menu-group.is-checked .settings-menu-group-title .chk-main label {
            color: #1e40af !important;
        }
        .settings-menu-group.is-checked .settings-menu-group-title .chk-main label::before {
            background-color: #bfdbfe;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='14' height='14' viewBox='0 0 24 24' fill='none' stroke='%231d4ed8' stroke-width='2.2' stroke-linecap='round' stroke-linejoin='round'%3E%3Cpath d='M3 7h18M3 12h18M3 17h18'/%3E%3C/svg%3E");
        }
        .settings-menu-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
            gap: 10px;
            padding: 12px;
            background: #fbfdff;
        }
        .settings-menu-item {
            display: flex;
            gap: 10px;
            align-items: flex-start;
            border: 1px solid #e8ecf1;
            border-radius: 12px;
            padding: 12px;
            background: #fff;
            min-height: 74px;
            transition: border-color 0.15s ease, box-shadow 0.15s ease;
        }
        .settings-menu-item:hover {
            border-color: #bfdbfe;
            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.06);
        }
        .settings-menu-item input { margin-top: 3px; }
        .settings-menu-item strong {
            display: block;
            font-size: 13px;
            color: #0f172a;
            margin-bottom: 2px;
        }
        .settings-menu-item small {
            display: block;
            font-size: 11px;
            color: #94a3b8;
            line-height: 1.35;
        }
        .settings-role-pill {
            display: inline-block;
            margin-top: 4px;
            padding: 2px 8px;
            border-radius: 999px;
            background: #eff6ff;
            color: #1d4ed8;
            font-size: 11px;
            font-weight: 700;
        }
        @media (max-width: 991px) {
            .settings-add-grid { grid-template-columns: 1fr; }
            .settings-layout { grid-template-columns: 1fr; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Settings</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">Admin users &amp; menu access</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="settings-wrap">
                <div class="settings-hero">
                    <h1>Admin users &amp; menu access</h1>
                    <p>Create SubAdmin login, then select the user and tick menus/submenus to assign sidebar access.</p>
                </div>

                <div class="settings-add-card">
                    <h3>Add admin user</h3>
                    <div class="settings-add-grid">
                        <div class="settings-field">
                            <label for="<%= txtUsername.ClientID %>">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Login id" MaxLength="50" />
                        </div>
                        <div class="settings-field">
                            <label for="<%= txtPassword.ClientID %>">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" MaxLength="20" />
                        </div>
                        <div class="settings-field">
                            <label for="<%= txtRoleLabel.ClientID %>">Role label</label>
                            <asp:TextBox ID="txtRoleLabel" runat="server" CssClass="form-control" placeholder="e.g. Accounts, HR" MaxLength="50" />
                        </div>
                        <div>
                            <asp:Button ID="btnAddUser" runat="server" CssClass="settings-btn-primary" Text="Add user" OnClick="btnAddUser_Click" />
                        </div>
                    </div>
                </div>

                <div class="settings-layout">
                    <div class="settings-users-card">
                        <div class="settings-users-head">
                            <h3>Users</h3>
                        </div>
                        <div class="settings-users-list">
                            <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelectUser" runat="server" CssClass='<%# "settings-user-item" + (Convert.ToString(Eval("UserId")) == SelectedUserId ? " is-active" : "") %>'
                                        CommandName="select" CommandArgument='<%# Eval("UserId") %>'>
                                        <span class="settings-user-avatar" style='<%# "background:" + Eval("AvatarColor") %>'><%# Eval("Initial") %></span>
                                        <span class="settings-user-copy">
                                            <strong><%# Eval("UserId") %></strong>
                                            <span><%# Eval("RoleLabel") %></span>
                                        </span>
                                        <span class="settings-user-status"><%# Eval("StatusText") %></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="pnlNoUsers" runat="server" Visible="false" CssClass="settings-menus-empty">
                                No SubAdmin users yet. Add one above.
                            </asp:Panel>
                        </div>
                    </div>

                    <div class="settings-menus-card">
                        <asp:Panel ID="pnlMenuEmpty" runat="server" CssClass="settings-menus-empty">
                            Select a user from the left to assign menus.
                        </asp:Panel>
                        <asp:Panel ID="pnlMenuEditor" runat="server" Visible="false">
                            <div class="settings-menus-head">
                                <div>
                                    <h3>
                                        <asp:Literal ID="litSelectedUser" runat="server"></asp:Literal>
                                    </h3>
                                    <span class="settings-role-pill">
                                        Role:
                                        <asp:Literal ID="litSelectedRole" runat="server"></asp:Literal>
                                    </span>
                                    <div style="margin-top:6px;color:#64748b;font-size:12px;">Tick menus this admin can see in the sidebar.</div>
                                </div>
                                <div class="settings-user-actions">
                                    <asp:Button ID="btnSaveMenus" runat="server" CssClass="settings-btn-save" Text="Save access" OnClick="btnSaveMenus_Click" />
                                    <asp:Button ID="btnDeleteUser" runat="server" CssClass="settings-btn-danger" Text="Delete user"
                                        OnClick="btnDeleteUser_Click"
                                        OnClientClick="return confirm('Delete this SubAdmin user permanently?');" />
                                </div>
                            </div>
                            <div class="settings-password-box">
                                <h4>Change password</h4>
                                <div class="settings-field">
                                    <label for="<%= txtNewPassword.ClientID %>">New password</label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="20" placeholder="New password" />
                                </div>
                                <div class="settings-field">
                                    <label for="<%= txtConfirmPassword.ClientID %>">Confirm password</label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="20" placeholder="Confirm password" />
                                </div>
                                <div>
                                    <asp:Button ID="btnChangePassword" runat="server" CssClass="settings-btn-secondary" Text="Update password" OnClick="btnChangePassword_Click" />
                                </div>
                            </div>
                            <div class="settings-menus-body">
                                <asp:HiddenField ID="hdnSelectedUser" runat="server" />
                                <asp:Repeater ID="rptMainMenus" runat="server" OnItemDataBound="rptMainMenus_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="settings-menu-group" data-mainid='<%# Eval("MainMenuId") %>'>
                                            <div class="settings-menu-group-title">
                                                <asp:CheckBox ID="chkMain" runat="server" CssClass="chk-main"
                                                    Checked='<%# Convert.ToBoolean(Eval("Checked")) %>'
                                                    Text='<%# Eval("MainMenuName") %>' />
                                                <asp:HiddenField ID="hdnMainId" runat="server" Value='<%# Eval("MainMenuId") %>' />
                                            </div>
                                            <div class="settings-menu-grid">
                                                <asp:Repeater ID="rptSubMenus" runat="server">
                                                    <ItemTemplate>
                                                        <label class="settings-menu-item">
                                                            <asp:CheckBox ID="chkSub" runat="server" CssClass="chk-sub"
                                                                Checked='<%# Convert.ToBoolean(Eval("Checked")) %>' />
                                                            <asp:HiddenField ID="hdnSubId" runat="server" Value='<%# Eval("MenuId") %>' />
                                                            <asp:HiddenField ID="hdnSubMainId" runat="server" Value='<%# Eval("MainMenuId") %>' />
                                                            <span>
                                                                <strong><%# Eval("MenuName") %></strong>
                                                                <small><%# Eval("Url") %></small>
                                                            </span>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function syncMainFromSubs(group) {
            if (!group) return;
            var subs = group.querySelectorAll('.chk-sub input[type=checkbox]');
            var main = group.querySelector('.chk-main input[type=checkbox]');
            if (!main || !subs.length) return;
            var any = false;
            for (var i = 0; i < subs.length; i++) {
                if (subs[i].checked) { any = true; break; }
            }
            main.checked = any;
            syncMainGroupStyle(group);
        }
        function syncMainGroupStyle(group) {
            if (!group) return;
            var main = group.querySelector('.chk-main input[type=checkbox]');
            if (main && main.checked) group.classList.add('is-checked');
            else group.classList.remove('is-checked');
        }
        function bindSettingsChecks() {
            var groups = document.querySelectorAll('.settings-menu-group');
            for (var g = 0; g < groups.length; g++) {
                (function (group) {
                    var main = group.querySelector('.chk-main input[type=checkbox]');
                    var subs = group.querySelectorAll('.chk-sub input[type=checkbox]');
                    syncMainGroupStyle(group);
                    if (main && main.getAttribute('data-bound') !== '1') {
                        main.setAttribute('data-bound', '1');
                        main.addEventListener('change', function () {
                            for (var i = 0; i < subs.length; i++) subs[i].checked = main.checked;
                            syncMainGroupStyle(group);
                        });
                    }
                    for (var i = 0; i < subs.length; i++) {
                        if (subs[i].getAttribute('data-bound') === '1') continue;
                        subs[i].setAttribute('data-bound', '1');
                        subs[i].addEventListener('change', function () { syncMainFromSubs(group); });
                    }
                })(groups[g]);
            }
        }
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', bindSettingsChecks);
        } else {
            bindSettingsChecks();
        }
        if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindSettingsChecks);
        }
    </script>
</asp:Content>

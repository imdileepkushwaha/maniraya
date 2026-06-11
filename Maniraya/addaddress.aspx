<%@ Page Title="Add Address" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="addaddress.aspx.cs" Inherits="addaddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtZip.ClientID%>").value == "") {
                alert('Enter Pincode');
                document.getElementById("<%=txtZip.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPhone.ClientID%>").value == "") {
                alert('Enter Mobile');
                document.getElementById("<%=txtPhone.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtAddress1.ClientID%>").value == "") {
                alert('Enter Address Line 1');
                document.getElementById("<%=txtAddress1.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=DDlststate.ClientID%>").value == "0") {
                alert('Select State');
                document.getElementById("<%=DDlststate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=DDlastCity.ClientID%>").value == "" || document.getElementById("<%=DDlastCity.ClientID%>").value == "0") {
                alert('Select City Name');
                document.getElementById("<%=DDlastCity.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function SelectSingleRadio(rb) {
            var gv = document.getElementById("<%= gvAddress.ClientID %>");
            if (!gv) return;
            var radios = gv.getElementsByTagName("input");
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].type == "radio") {
                    radios[i].checked = false;
                }
            }
            rb.checked = true;
            updateAddressCardSelection();
        }

        function updateAddressCardSelection() {
            document.querySelectorAll(".addr-card").forEach(function (card) {
                var radio = card.querySelector("input[type='radio']");
                card.classList.toggle("is-selected", radio && radio.checked);
            });
        }

        function setAddressModalMode(mode) {
            var title = document.getElementById("addrModalTitle");
            var subtitle = document.getElementById("addrModalSubtitle");
            var saveBtn = document.getElementById("<%= btnSave.ClientID %>");
            if (!title) return;

            if (mode === "edit") {
                title.textContent = "Edit delivery address";
                if (subtitle) subtitle.textContent = "Update your delivery details below.";
                if (saveBtn) saveBtn.value = "Update address";
            } else {
                title.textContent = "Add delivery address";
                if (subtitle) subtitle.textContent = "Fill in your details for fast and accurate delivery.";
                if (saveBtn) saveBtn.value = "Save address";
            }
        }

        function showAddAddressModal() {
            var modal = document.getElementById("addAddressModal");
            if (modal) {
                modal.hidden = false;
                modal.setAttribute("aria-hidden", "false");
                document.body.classList.add("addr-modal-open");
            }
        }

        function closeAddAddressModal() {
            var modal = document.getElementById("addAddressModal");
            if (modal) {
                modal.hidden = true;
                modal.setAttribute("aria-hidden", "true");
                if (!isDeleteModalOpen()) {
                    document.body.classList.remove("addr-modal-open");
                }
            }
        }

        function isDeleteModalOpen() {
            var modal = document.getElementById("deleteAddressModal");
            return modal && !modal.hidden;
        }

        function showDeleteAddressModal(button) {
            var addressId = button.getAttribute("data-address-id");
            var label = button.getAttribute("data-address-label") || "this address";
            var hidden = document.getElementById("<%= hfDeleteAddressId.ClientID %>");
            var labelNode = document.getElementById("deleteAddressLabel");
            var modal = document.getElementById("deleteAddressModal");

            if (hidden) hidden.value = addressId || "";
            if (labelNode) labelNode.textContent = label;
            if (modal) {
                modal.hidden = false;
                modal.setAttribute("aria-hidden", "false");
                document.body.classList.add("addr-modal-open");
            }
        }

        function closeDeleteAddressModal() {
            var modal = document.getElementById("deleteAddressModal");
            var hidden = document.getElementById("<%= hfDeleteAddressId.ClientID %>");
            if (hidden) hidden.value = "";
            if (modal) {
                modal.hidden = true;
                modal.setAttribute("aria-hidden", "true");
            }
            var addModal = document.getElementById("addAddressModal");
            if (!addModal || addModal.hidden) {
                document.body.classList.remove("addr-modal-open");
            }
        }

        function setupAddressPageUi() {
            updateAddressCardSelection();

            var addModal = document.getElementById("addAddressModal");
            if (addModal && !addModal.dataset.bound) {
                addModal.dataset.bound = "1";
                addModal.addEventListener("click", function (event) {
                    if (event.target === addModal) {
                        closeAddAddressModal();
                    }
                });
            }

            var deleteModal = document.getElementById("deleteAddressModal");
            if (deleteModal && !deleteModal.dataset.bound) {
                deleteModal.dataset.bound = "1";
                deleteModal.addEventListener("click", function (event) {
                    if (event.target === deleteModal) {
                        closeDeleteAddressModal();
                    }
                });
            }
        }

        document.addEventListener("DOMContentLoaded", setupAddressPageUi);

        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setupAddressPageUi);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main class="addr-page-main">
                <section class="addr-section">
                    <div class="container">
                        <nav class="addr-steps" aria-label="Checkout progress">
                            <span class="addr-step is-done">Cart</span>
                            <span class="addr-step-line" aria-hidden="true"></span>
                            <span class="addr-step is-active">Address</span>
                            <span class="addr-step-line" aria-hidden="true"></span>
                            <span class="addr-step">Payment</span>
                        </nav>

                        <div class="addr-shell">
                            <header class="addr-page-head">
                                <div class="addr-page-head-copy">
                                    <span class="addr-eyebrow">Delivery</span>
                                    <h1>Saved Addresses</h1>
                                    <p>Select a delivery address or add a new one to continue checkout.</p>
                                </div>
                                <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="addr-btn-add" OnClick="lnkAddNew_Click" CausesValidation="false">
                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                        <path d="M12 5V19M5 12H19" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"/>
                                    </svg>
                                    <span>Add New Address</span>
                                </asp:LinkButton>
                            </header>

                            <div class="addr-list-panel">
                                <asp:GridView ID="gvAddress"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="addr-grid-table"
                                    DataKeyNames="AddressId"
                                    ShowHeader="false"
                                    GridLines="None"
                                    BorderStyle="None"
                                    OnRowDataBound="gvAddress_RowDataBound"
                                    OnRowCommand="gvAddress_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <article class='addr-card <%# Eval("Isdefault").ToString() == "YES" ? "is-default" : "" %>'>
                                                    <div class="addr-card-top">
                                                        <label class="addr-card-select">
                                                            <asp:RadioButton ID="rbSelect"
                                                                runat="server"
                                                                GroupName="DeliveryAddress"
                                                                AutoPostBack="true"
                                                                onclick="SelectSingleRadio(this);" />
                                                            <span class="addr-card-select-ui" aria-hidden="true"></span>
                                                            <span class="addr-card-select-text">Deliver here</span>
                                                        </label>
                                                        <span class='addr-type-badge addr-type-<%# Eval("Type").ToString().ToLower() %>'>
                                                            <%# Eval("Type") %>
                                                        </span>
                                                    </div>

                                                    <div class="addr-card-body">
                                                        <h3 class="addr-card-city"><%# Eval("Cityname") %></h3>
                                                        <p class="addr-card-line"><%# Eval("AddressFirst") %></p>
                                                        <p class="addr-card-line addr-card-line-muted">
                                                            <%# string.IsNullOrEmpty(Convert.ToString(Eval("Addresssecond"))) ? "" : Convert.ToString(Eval("Addresssecond")) + " · " %><%# Eval("AreaName") %>
                                                        </p>
                                                        <p class="addr-card-meta">
                                                            <span class="addr-card-pin"><%# Eval("Pincode") %></span>
                                                            <span class="addr-card-phone">
                                                                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                                    <path d="M6.6 10.8C8.2 14 10 15.8 13.2 17.4L15.4 15.2C15.7 14.9 16.1 14.8 16.5 15C17.6 15.4 18.8 15.6 20 15.6C20.6 15.6 21 16 21 16.6V20C21 20.6 20.6 21 20 21C10.6 21 3 13.4 3 4C3 3.4 3.4 3 4 3H7.4C8 3 8.4 3.4 8.4 4C8.4 5.2 8.6 6.4 9 7.5C9.1 7.9 9 8.3 8.7 8.6L6.6 10.8Z" fill="currentColor"/>
                                                                </svg>
                                                                <%# Eval("Mobile") %>
                                                            </span>
                                                        </p>
                                                    </div>

                                                    <div class="addr-card-foot">
                                                        <%# Eval("Isdefault").ToString() == "YES" ? "<span class=\"addr-default-pill\">Default</span>" : "<span class=\"addr-default-pill addr-default-pill-ghost\">&nbsp;</span>" %>
                                                        <div class="addr-card-actions">
                                                            <asp:LinkButton ID="btnEdit"
                                                                runat="server"
                                                                CommandName="EditAddress"
                                                                CommandArgument='<%# Eval("AddressId") %>'
                                                                CssClass="addr-action-btn addr-action-edit">
                                                                Edit
                                                            </asp:LinkButton>
                                                            <button type="button"
                                                                class="addr-action-btn addr-action-delete"
                                                                data-address-id='<%# Eval("AddressId") %>'
                                                                data-address-label='<%# Server.HtmlEncode(Convert.ToString(Eval("Cityname")) + ", " + Convert.ToString(Eval("Addressfirst"))) %>'
                                                                onclick="showDeleteAddressModal(this); return false;">
                                                                Delete
                                                            </button>
                                                        </div>
                                                        <asp:Label ID="Isdefault" runat="server" Text='<%# Eval("Isdefault") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblAddressId" runat="server" Text='<%# Eval("AddressId") %>' Visible="false"></asp:Label>
                                                    </div>
                                                </article>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="addr-empty-state">
                                            <span class="addr-empty-icon" aria-hidden="true">
                                                <svg width="32" height="32" viewBox="0 0 24 24" fill="none">
                                                    <path d="M12 21S4 15.5 4 9.5C4 6.5 6.5 4 9.5 4C11.1 4 12.6 4.7 13.6 6C14.6 4.7 16.1 4 17.7 4C20.7 4 23.2 6.5 23.2 9.5C23.2 15.5 12 21 12 21Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                                </svg>
                                            </span>
                                            <h3>No saved addresses yet</h3>
                                            <p>Add your first delivery address to proceed with checkout.</p>
                                            <asp:LinkButton ID="lnkAddEmpty" runat="server" CssClass="addr-btn-add addr-btn-add-inline" OnClick="lnkAddNew_Click" CausesValidation="false">
                                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                                    <path d="M12 5V19M5 12H19" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"/>
                                                </svg>
                                                Add Address
                                            </asp:LinkButton>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>

                            <footer class="addr-page-foot">
                                <a href="index.aspx" class="addr-back-link">← Continue shopping</a>
                                <asp:Button ID="btngotopayment" runat="server" CssClass="addr-checkout-btn" Text="Continue to Payment" OnClick="btngotopayment_Click" />
                            </footer>
                        </div>
                    </div>
                </section>
            </main>

            <div id="addAddressModal" class="addr-modal-backdrop" hidden aria-hidden="true">
                <div class="addr-modal" role="dialog" aria-modal="true" aria-labelledby="addrModalTitle">
                    <div class="addr-modal-accent" aria-hidden="true"></div>
                    <div class="addr-modal-body">
                        <button type="button" class="addr-modal-close" onclick="closeAddAddressModal()" aria-label="Close add address dialog">
                            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                <path d="M18 6L6 18M6 6L18 18" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"/>
                            </svg>
                        </button>

                        <div class="addr-modal-head">
                            <span class="addr-modal-icon" aria-hidden="true">
                                <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
                                    <path d="M12 21C16.4183 21 20 17.4183 20 13C20 8.5 12 3 12 3C12 3 4 8.5 4 13C4 17.4183 7.58172 21 12 21Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/>
                                    <circle cx="12" cy="13" r="3" stroke="currentColor" stroke-width="1.8"/>
                                </svg>
                            </span>
                            <div>
                                <h2 id="addrModalTitle">Add delivery address</h2>
                                <p id="addrModalSubtitle">Fill in your details for fast and accurate delivery.</p>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfEditAddressId" runat="server" />
                        <asp:HiddenField ID="hfDeleteAddressId" runat="server" />

                        <asp:Panel ID="pnlAddAddress" runat="server" CssClass="addr-modal-form auth-form">
                            <div class="addr-form-grid">
                                <div class="auth-field">
                                    <label for="<%= txtPhone.ClientID %>">Phone number</label>
                                    <div class="auth-input-wrap">
                                        <span class="auth-input-icon" aria-hidden="true">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M6.6 10.8C8.2 14 10 15.8 13.2 17.4L15.4 15.2C15.7 14.9 16.1 14.8 16.5 15C17.6 15.4 18.8 15.6 20 15.6C20.6 15.6 21 16 21 16.6V20C21 20.6 20.6 21 20 21C10.6 21 3 13.4 3 4C3 3.4 3.4 3 4 3H7.4C8 3 8.4 3.4 8.4 4C8.4 5.2 8.6 6.4 9 7.5C9.1 7.9 9 8.3 8.7 8.6L6.6 10.8Z" fill="currentColor"/></svg>
                                        </span>
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control auth-input" placeholder="+91 98765 43210" />
                                    </div>
                                </div>
                                <div class="auth-field">
                                    <label for="<%= ddlType.ClientID %>">Address type</label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control auth-input">
                                        <asp:ListItem Text="Home" Value="Home" />
                                        <asp:ListItem Text="Work" Value="Work" />
                                        <asp:ListItem Text="Other" Value="Other" />
                                    </asp:DropDownList>
                                </div>
                                <div class="auth-field addr-form-span-2">
                                    <label for="<%= txtAddress1.ClientID %>">Address line 1</label>
                                    <div class="auth-input-wrap">
                                        <span class="auth-input-icon" aria-hidden="true">
                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M4 10L12 4L20 10V20H4V10Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/></svg>
                                        </span>
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control auth-input" placeholder="House no., building, street" />
                                    </div>
                                </div>
                                <div class="auth-field addr-form-span-2">
                                    <label for="<%= txtAddress2.ClientID %>">Address line 2 <span class="addr-label-optional">(optional)</span></label>
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control auth-input" placeholder="Landmark, apartment, suite" />
                                </div>
                                <div class="auth-field">
                                    <label for="<%= DDlststate.ClientID %>">State</label>
                                    <asp:DropDownList ID="DDlststate" runat="server" CssClass="form-control auth-input" AutoPostBack="true" OnSelectedIndexChanged="ddstate_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="auth-field">
                                    <label for="<%= DDlastCity.ClientID %>">City</label>
                                    <asp:DropDownList ID="DDlastCity" runat="server" CssClass="form-control auth-input"></asp:DropDownList>
                                </div>
                                <div class="auth-field">
                                    <label for="<%= TxtAreaname.ClientID %>">Area name</label>
                                    <asp:TextBox ID="TxtAreaname" runat="server" CssClass="form-control auth-input" placeholder="Locality / area" />
                                </div>
                                <div class="auth-field">
                                    <label for="<%= txtZip.ClientID %>">PIN code</label>
                                    <asp:TextBox ID="txtZip" runat="server" CssClass="form-control auth-input" placeholder="400001" />
                                </div>
                            </div>

                            <label class="addr-default-check">
                                <asp:CheckBox ID="chkDefault" runat="server" />
                                <span>Set as default delivery address</span>
                            </label>

                            <div class="addr-modal-actions">
                                <button type="button" class="addr-modal-btn addr-modal-btn-ghost" onclick="closeAddAddressModal()">Cancel</button>
                                <asp:Button ID="btnSave" runat="server" Text="Save address" CssClass="addr-modal-btn addr-modal-btn-primary" OnClientClick="return validate();" OnClick="btnSave_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div id="deleteAddressModal" class="addr-modal-backdrop addr-confirm-backdrop" hidden aria-hidden="true">
                <div class="addr-confirm-modal" role="dialog" aria-modal="true" aria-labelledby="deleteAddressTitle">
                    <div class="addr-modal-accent addr-confirm-accent" aria-hidden="true"></div>
                    <div class="addr-confirm-body">
                        <span class="addr-confirm-icon" aria-hidden="true">
                            <svg width="26" height="26" viewBox="0 0 24 24" fill="none">
                                <path d="M12 9V14M12 18H12.01M10.29 3.86L1.82 18C1.64 18.3 1.54 18.64 1.54 19C1.54 20.1 2.44 21 3.54 21H20.46C21.56 21 22.46 20.1 22.46 19C22.46 18.64 22.36 18.3 22.18 18L13.71 3.86C13.32 3.18 12.59 2.75 11.79 2.75C11 2.75 10.27 3.18 9.88 3.86H10.29Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>
                            </svg>
                        </span>
                        <h2 id="deleteAddressTitle">Delete address?</h2>
                        <p class="addr-confirm-text">You are about to remove <strong id="deleteAddressLabel">this address</strong> from your saved list. This action cannot be undone.</p>
                        <div class="addr-confirm-actions">
                            <button type="button" class="addr-modal-btn addr-modal-btn-ghost" onclick="closeDeleteAddressModal()">Cancel</button>
                            <asp:Button ID="btnConfirmDelete" runat="server" Text="Yes, delete" CssClass="addr-modal-btn addr-confirm-delete-btn" OnClick="btnConfirmDelete_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="Package Plan Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PackagePlanMaster.aspx.cs" Inherits="admin_PackagePlanMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function syncProductName() {
            var ddl = document.getElementById("<%= ddlProduct.ClientID %>");
            var txt = document.getElementById("<%= txtProductName.ClientID %>");
            if (!ddl || !txt) return;
            if (ddl.selectedIndex > 0) {
                txt.value = ddl.options[ddl.selectedIndex].text;
            } else {
                txt.value = "";
            }
        }

        function validateAdd() {
            if (document.getElementById("<%= ddlPlan.ClientID %>").value === "0") {
                alert("Select Plan");
                document.getElementById("<%= ddlPlan.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%= ddlProduct.ClientID %>").value === "0") {
                alert("Select Product");
                document.getElementById("<%= ddlProduct.ClientID %>").focus();
                return false;
            }
            var qty = document.getElementById("<%= txtQuantity.ClientID %>").value.trim();
            if (qty === "" || isNaN(qty) || Number(qty) <= 0) {
                alert("Enter valid quantity");
                document.getElementById("<%= txtQuantity.ClientID %>").focus();
                return false;
            }
            return true;
        }

        function validateEdit() {
            if (document.getElementById("<%= ddlPlanEdit.ClientID %>").value === "0") {
                alert("Select Plan");
                return false;
            }
            if (document.getElementById("<%= ddlProductEdit.ClientID %>").value === "0") {
                alert("Select Product");
                return false;
            }
            var qty = document.getElementById("<%= txtQuantityEdit.ClientID %>").value.trim();
            if (qty === "" || isNaN(qty) || Number(qty) <= 0) {
                alert("Enter valid quantity");
                return false;
            }
            return true;
        }

        function syncProductNameEdit() {
            var ddl = document.getElementById("<%= ddlProductEdit.ClientID %>");
            var txt = document.getElementById("<%= txtProductNameEdit.ClientID %>");
            if (!ddl || !txt) return;
            if (ddl.selectedIndex > 0) {
                txt.value = ddl.options[ddl.selectedIndex].text;
            } else {
                txt.value = "";
            }
        }

        function openPackageEditModal() {
            if (typeof showAdminModal === "function") {
                showAdminModal("packageEditModal");
            } else if (window.jQuery) {
                jQuery("#packageEditModal").modal({ backdrop: "static", keyboard: false, show: true });
            }
        }

        function closePackageEditModal() {
            if (typeof closeAdminModal === "function") {
                closeAdminModal("packageEditModal");
            } else if (window.jQuery) {
                jQuery("#packageEditModal").modal("hide");
            }
        }

        Sys.Application.add_load(function () {
            syncProductName();
            syncProductNameEdit();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Package Plan Master</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Package Plan Master</li>
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
                            <h3 class="box-title"><i class="fa fa-cubes"></i> Create Plan Package</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-section-hint admin-plan-intro">Select a plan, add one or more products with quantity. Product name auto-fills when you pick from the dropdown.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-file-text-o"></i> Plan Selection</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddlPlan.ClientID %>">Plan</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-shopping-basket"></i> Add Products</h5>
                                <p class="admin-section-hint">Select product and quantity, click <strong>Add Product</strong> for each item, then <strong>Save Package</strong>.</p>
                                <div class="row admin-package-add-row">
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddlProduct.ClientID %>">Product</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cube"></i></span>
                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" onchange="syncProductName();"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtProductName.ClientID %>">Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-info-circle"></i></span>
                                                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Auto-filled from product"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtQuantity.ClientID %>">Quantity</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-sort-numeric-asc"></i></span>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" min="1" Text="1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group admin-package-add-btn-wrap">
                                            <label class="admin-package-add-btn-label">&nbsp;</label>
                                            <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-block admin-package-add-btn" runat="server" Text="Add Product" OnClientClick="return validateAdd();" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-package-pending-wrap">
                                    <h6 class="admin-package-pending-title"><i class="fa fa-list-ul"></i> Products to Add</h6>
                                    <asp:Panel ID="pnlPendingEmpty" runat="server" Visible="true" CssClass="admin-package-empty-msg">
                                        No products added yet. Select product and quantity, then click <strong>Add Product</strong>.
                                    </asp:Panel>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPending" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="gvPending_RowCommand" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                                <asp:BoundField DataField="quantity" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbRemove" runat="server" CommandName="remove" CommandArgument='<%# Eval("RowKey") %>' CssClass="admin-grid-delete-btn" ToolTip="Remove">
                                                            <i class="fa fa-times" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="admin-package-empty-msg">No products in add list.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnClear" CssClass="btn btn-default" runat="server" Text="Clear List" CausesValidation="false" OnClick="btnClear_Click" />
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save Package" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Plan Package List</h3>
                            
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSavedEmpty" runat="server" Visible="false" CssClass="admin-package-empty-msg">
                                Select a plan above to view its saved package products. Only products you saved via <strong>Save Package</strong> appear here.
                            </asp:Panel>

                            <asp:Panel ID="pnlPackageCard" runat="server" Visible="false" CssClass="admin-plan-package-card">
                                <div class="admin-plan-package-header">
                                    <div class="admin-plan-package-header-main">
                                        <span class="admin-plan-package-icon"><i class="fa fa-cubes"></i></span>
                                        <div>
                                            <span class="admin-plan-package-label">Plan Package</span>
                                            <asp:Label ID="lblPackagePlanName" runat="server" CssClass="admin-plan-package-name"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblPackageProductCount" runat="server" CssClass="admin-plan-package-count"></asp:Label>
                                </div>
                                <div class="admin-plan-package-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered admin-plan-package-table" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowHeader="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemStyle CssClass="admin-plan-package-sr" Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <span class="admin-plan-package-sr-num"><%# Container.DataItemIndex + 1 %></span>
                                                        <asp:Label ID="lblRowId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <span class="admin-plan-package-product">
                                                            <i class="fa fa-cube" aria-hidden="true"></i>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                        </span>
                                                        <asp:Label ID="lblProductId" runat="server" Visible="false" Text='<%# Eval("productid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="admin-plan-package-qty" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <span class="admin-plan-package-qty-badge"><asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" runat="server" CssClass="admin-grid-edit-btn" ToolTip="Edit">
                                                            <i class="fa fa-pencil" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="admin-package-empty-msg">No products in this package.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div id="packageEditModal" class="modal fade admin-category-edit-dialog" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><i class="fa fa-pencil"></i> Edit Package Item</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblEditId" runat="server" Visible="false"></asp:Label>
                            <div class="form-group">
                                <label for="<%= ddlPlanEdit.ClientID %>">Plan</label>
                                <asp:DropDownList ID="ddlPlanEdit" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="<%= ddlProductEdit.ClientID %>">Product</label>
                                <asp:DropDownList ID="ddlProductEdit" runat="server" CssClass="form-control" onchange="syncProductNameEdit();"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtProductNameEdit.ClientID %>">Product Name</label>
                                <asp:TextBox ID="txtProductNameEdit" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtQuantityEdit.ClientID %>">Quantity</label>
                                <asp:TextBox ID="txtQuantityEdit" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" CausesValidation="false" OnClientClick="return validateEdit();" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

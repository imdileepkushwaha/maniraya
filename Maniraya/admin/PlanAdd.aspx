<%@ Page Title="Add Plan" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PlanAdd.aspx.cs" Inherits="admin_PlanAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function getValue(id) {
            var el = document.getElementById(id);
            return el ? el.value.trim() : "";
        }

        function isValidDecimal(value) {
            if (value === "") return false;
            return !isNaN(value) && Number(value) >= 0;
        }

        function ensureAutoCreateDate(id) {
            var el = document.getElementById(id);
            if (!el || el.value) {
                return;
            }
            var today = new Date();
            var month = String(today.getMonth() + 1).padStart(2, "0");
            var day = String(today.getDate()).padStart(2, "0");
            el.value = today.getFullYear() + "-" + month + "-" + day;
        }

        function validatePlanForm(planNameId, planAmountId, businessVolumeId, createDateId, cappingAmountId) {
            if (getValue(planNameId) === "") {
                alert("Enter Plan Name");
                document.getElementById(planNameId).focus();
                return false;
            }
            if (!isValidDecimal(getValue(planAmountId))) {
                alert("Enter valid Plan Amount");
                document.getElementById(planAmountId).focus();
                return false;
            }
            if (!isValidDecimal(getValue(businessVolumeId))) {
                alert("Enter valid Business Volume");
                document.getElementById(businessVolumeId).focus();
                return false;
            }
            ensureAutoCreateDate(createDateId);
            if (!isValidDecimal(getValue(cappingAmountId))) {
                alert("Enter valid Capping Amount");
                document.getElementById(cappingAmountId).focus();
                return false;
            }
            return true;
        }

        function validate() {
            return validatePlanForm(
                "<%= txtPlanName.ClientID %>",
                "<%= txtPlanAmount.ClientID %>",
                "<%= txtBusinessVolume.ClientID %>",
                "<%= txtCreateDate.ClientID %>",
                "<%= txtCappingAmount.ClientID %>"
            );
        }

        function validate2() {
            return validatePlanForm(
                "<%= txtPlanNameEdit.ClientID %>",
                "<%= txtPlanAmountEdit.ClientID %>",
                "<%= txtBusinessVolumeEdit.ClientID %>",
                "<%= txtCreateDateEdit.ClientID %>",
                "<%= txtCappingAmountEdit.ClientID %>"
            );
        }

        function openPlanEditModal() {
            if (typeof showAdminModal === "function") {
                showAdminModal("planEditModal");
            } else if (window.jQuery) {
                jQuery("#planEditModal").modal({ backdrop: "static", keyboard: false, show: true });
            }
        }

        function closePlanEditModal() {
            if (typeof closeAdminModal === "function") {
                closeAdminModal("planEditModal");
            } else if (window.jQuery) {
                jQuery("#planEditModal").modal("hide");
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Plan Master</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add Plan</li>
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
                            <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Plan</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-section-hint admin-plan-intro">Create a new plan with amount, business volume, create date and capping amount.</p>
                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-file-text-o"></i> Plan Details</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtPlanName.ClientID %>">Plan Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                <asp:TextBox ID="txtPlanName" runat="server" CssClass="form-control" placeholder="Enter plan name" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtCreateDate.ClientID %>">Create Date</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtCreateDate" runat="server" CssClass="form-control is-readonly" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <p class="admin-field-hint">Automatically set to today's date</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-inr"></i> Amount &amp; Volume</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtPlanAmount.ClientID %>">Plan Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-money"></i></span>
                                                <asp:TextBox ID="txtPlanAmount" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="any" min="0"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtBusinessVolume.ClientID %>">Business Volume</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                                <asp:TextBox ID="txtBusinessVolume" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="any" min="0"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtCappingAmount.ClientID %>">Capping Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-level-up"></i></span>
                                                <asp:TextBox ID="txtCappingAmount" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="any" min="0"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnClear" CssClass="btn btn-default" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClear_Click" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Add Plan" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Plan List</h3>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:Label ID="lblPlanId" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Amount">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanAmount" runat="server" Text='<%# Eval("Planamount", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Business Volume">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBusinessVolume" runat="server" Text='<%# Eval("BuisnessVolume", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Date">
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDateDisplay") %>'></asp:Label>
                                                <asp:Label ID="lblCreateDateValue" runat="server" Visible="false" Text='<%# Eval("CreateDateValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Capping Amount">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCappingAmount" runat="server" Text='<%# Eval("cappingamount", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="72px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" runat="server" CssClass="admin-grid-edit-btn" ToolTip="Edit plan">
                                                    <i class="fa fa-pencil" aria-hidden="true"></i>
                                                    <span class="sr-only">Edit</span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="planEditModal" class="modal fade admin-category-edit-dialog" tabindex="-1" role="dialog" aria-labelledby="planEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="planEditModalTitle"><i class="fa fa-pencil"></i> Edit Plan</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblPlanIdEdit" Visible="false" runat="server"></asp:Label>
                            <div class="form-group">
                                <label for="<%= txtPlanNameEdit.ClientID %>">Plan Name</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                    <asp:TextBox ID="txtPlanNameEdit" runat="server" CssClass="form-control" placeholder="Enter plan name" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtPlanAmountEdit.ClientID %>">Plan Amount</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-money"></i></span>
                                            <asp:TextBox ID="txtPlanAmountEdit" runat="server" CssClass="form-control" TextMode="Number" step="any" min="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtBusinessVolumeEdit.ClientID %>">Business Volume</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                            <asp:TextBox ID="txtBusinessVolumeEdit" runat="server" CssClass="form-control" TextMode="Number" step="any" min="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtCreateDateEdit.ClientID %>">Create Date</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtCreateDateEdit" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtCappingAmountEdit.ClientID %>">Capping Amount</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-level-up"></i></span>
                                            <asp:TextBox ID="txtCappingAmountEdit" runat="server" CssClass="form-control" TextMode="Number" step="any" min="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Plan" UseSubmitBehavior="true" CausesValidation="false" CssClass="btn btn-primary" OnClientClick="return validate2();" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="Prize Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PrizeMaster.aspx.cs" Inherits="admin_PrizeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validatePrize() {
            var name = document.getElementById("<%=txtPrizeName.ClientID%>");
            if (name.value.trim() === "") {
                alert('Enter Prize Name');
                name.focus();
                return false;
            }
            return true;
        }

        function validatePrizeEdit() {
            var name = document.getElementById("<%=txtPrizeNameEdit.ClientID%>");
            if (name.value.trim() === "") {
                alert('Enter Prize Name');
                name.focus();
                return false;
            }
            return true;
        }

        function openPrizeEditModal() {
            if (typeof showAdminModal === "function") {
                showAdminModal("prizeEditModal");
            } else if (window.jQuery) {
                jQuery("#prizeEditModal").modal({ backdrop: "static", keyboard: false, show: true });
            }
        }

        function closePrizeEditModal() {
            if (typeof closeAdminModal === "function") {
                closeAdminModal("prizeEditModal");
            } else if (window.jQuery) {
                jQuery("#prizeEditModal").modal("hide");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Prize Master</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Prize</a></li>
            <li class="active">Prize Master</li>
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
                            <h3 class="box-title"><i class="fa fa-trophy"></i> Add Prize</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Create prizes that can later be assigned to members.</p>
                            <div class="admin-form-section admin-form-section-last">
                                <div class="row">
                                    <div class="col-md-5 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtPrizeName.ClientID %>">Prize Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-gift"></i></span>
                                                <asp:TextBox ID="txtPrizeName" CssClass="form-control" runat="server" placeholder="e.g. Bullet Bike, Gold Coin" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-7 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtPrizeDesc.ClientID %>">Description (optional)</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-align-left"></i></span>
                                                <asp:TextBox ID="txtPrizeDesc" CssClass="form-control" runat="server" placeholder="Short description" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnClear" CssClass="btn btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="return validatePrize();" runat="server" Text="Save Prize" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Prize List</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PrizeName" HeaderText="Prize Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <span class='<%# Convert.ToBoolean(Eval("Status")) ? "label label-success" : "label label-default" %>'>
                                                <%# Eval("StatusText") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="110px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="edt" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CssClass="admin-grid-edit-btn" ToolTip="Edit prize">
                                                <i class="icon fa fa-pencil-square-o" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkToggle" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkToggle_Click" CssClass="admin-grid-edit-btn" ToolTip="Toggle status">
                                                <i class='<%# Convert.ToBoolean(Eval("Status")) ? "icon fa fa-toggle-on" : "icon fa fa-toggle-off" %>' aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center" style="padding:18px;color:#888;">No prizes added yet.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div id="prizeEditModal" class="modal fade admin-category-edit-dialog" tabindex="-1" role="dialog" aria-labelledby="prizeEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="prizeEditModalTitle"><i class="fa fa-pencil-square-o"></i> Edit Prize</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblEditId" runat="server" Visible="false" Text="0"></asp:Label>
                            <div class="admin-form-section admin-form-section-last">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtPrizeNameEdit.ClientID %>">Prize Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-gift"></i></span>
                                                <asp:TextBox ID="txtPrizeNameEdit" runat="server" CssClass="form-control" placeholder="Enter prize name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtPrizeDescEdit.ClientID %>">Description</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-align-left"></i></span>
                                                <asp:TextBox ID="txtPrizeDescEdit" runat="server" CssClass="form-control" placeholder="Short description" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= chkEditStatus.ClientID %>">Status (Active)</label>
                                            <div>
                                                <asp:CheckBox ID="chkEditStatus" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Prize" OnClientClick="return validatePrizeEdit();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

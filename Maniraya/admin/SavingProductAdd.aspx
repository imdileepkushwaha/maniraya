<%@ Page Title="Add Saving Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductAdd.aspx.cs" Inherits="admin_SavingProductAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtproductname.ClientID%>").value.trim() === "") {
                alert('Enter Product Name');
                document.getElementById("<%=txtproductname.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtdp.ClientID%>").value.trim() === "") {
                alert('Enter DP');
                document.getElementById("<%=txtdp.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtmrp.ClientID%>").value.trim() === "") {
                alert('Enter MRP');
                document.getElementById("<%=txtmrp.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function validateEdit() {
            if (document.getElementById("<%=txtproductnameedit.ClientID%>").value.trim() === "") {
                alert('Enter Product Name');
                document.getElementById("<%=txtproductnameedit.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtdpedit.ClientID%>").value.trim() === "") {
                alert('Enter DP');
                document.getElementById("<%=txtdpedit.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtmrpedit.ClientID%>").value.trim() === "") {
                alert('Enter MRP');
                document.getElementById("<%=txtmrpedit.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function openSavingProductEditModal() {
            if (typeof showAdminModal === "function") {
                showAdminModal("savingProductEditModal");
            } else if (window.jQuery) {
                jQuery("#savingProductEditModal").modal({ backdrop: "static", keyboard: false, show: true });
            }
        }

        function closeSavingProductEditModal() {
            if (typeof closeAdminModal === "function") {
                closeAdminModal("savingProductEditModal");
            } else if (window.jQuery) {
                jQuery("#savingProductEditModal").modal("hide");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Saving Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Add Saving Product</li>
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
                            <h3 class="box-title">Add Saving Product</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Fill in product details and upload image.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i> Product Details</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtproductname.ClientID %>">Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                                <asp:TextBox ID="txtproductname" CssClass="form-control" runat="server" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtmrp.ClientID %>">MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtmrp" CssClass="form-control" runat="server" TextMode="Number" step="0.01" placeholder="Enter MRP" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtdp.ClientID %>">DP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtdp" CssClass="form-control" runat="server" TextMode="Number" step="0.01" placeholder="Enter DP" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Product Image</h5>
                                <p class="admin-section-hint">Upload a clear product photo. JPG or PNG recommended.</p>
                                <div class="admin-media-upload-grid is-compact">
                                    <div class="admin-category-image-compact is-primary" id="savingProductImageSlot">
                                        <div class="admin-category-image-compact-preview admin-product-image-preview-box">
                                            <div id="savingProductPlaceholder" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="savingProductPreview" class="admin-product-image-preview-img" alt="Saving product image preview" />
                                        </div>
                                        <div class="admin-category-image-compact-body">
                                            <div class="admin-category-image-compact-head">
                                                <span class="admin-category-image-compact-title">Product Photo</span>
                                                <span class="admin-product-image-slot-badge">Primary</span>
                                            </div>
                                            <div class="admin-product-image-dropzone" id="savingProductDropzone">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="admin-file-input-hidden" accept=".png,.jpg,.jpeg,.webp,image/png,image/jpeg,image/webp" />
                                                <label class="admin-product-image-dropzone-label" for="<%= FileUpload1.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Browse or drop image</span>
                                                </label>
                                                <span id="savingProductFileName" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                            <p class="admin-category-image-compact-hint">PNG, JPG or WebP · square image works best</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Clear" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="return validate();" runat="server" Text="Save Product" OnClick="btnSubmit_Click1" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Saving Product List</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="72px" />
                                        <ItemTemplate>
                                            <asp:Image ID="imgProduct" runat="server" CssClass="admin-grid-thumb admin-grid-thumb--category" AlternateText="Saving product" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                    <asp:BoundField DataField="MRP" HeaderText="MRP" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="DP" HeaderText="DP" />
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
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="edt" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CssClass="admin-grid-edit-btn" ToolTip="Edit product">
                                                <i class="icon fa fa-pencil-square-o" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkToggle" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="lnkToggle_Click" CssClass="admin-grid-edit-btn" ToolTip="Toggle status">
                                                <i class='<%# Convert.ToBoolean(Eval("Status")) ? "icon fa fa-toggle-on" : "icon fa fa-toggle-off" %>' aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div id="savingProductEditModal" class="modal fade admin-category-edit-dialog" tabindex="-1" role="dialog" aria-labelledby="savingProductEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="savingProductEditModalTitle"><i class="fa fa-pencil-square-o"></i> Edit Saving Product</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblproductid" runat="server" Visible="false" Text="0"></asp:Label>
                            <asp:HiddenField ID="hfEditImage" runat="server" />

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i> Product Details</h5>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtproductnameedit.ClientID %>">Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                                <asp:TextBox ID="txtproductnameedit" runat="server" CssClass="form-control" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtmrpedit.ClientID %>">MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtmrpedit" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="Enter MRP" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtdpedit.ClientID %>">DP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtdpedit" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="Enter DP" />
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

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Product Image</h5>
                                <div class="admin-media-upload-grid is-compact">
                                    <div class="admin-category-image-compact" id="savingProductEditSlot">
                                        <div class="admin-category-image-compact-preview admin-product-image-preview-box">
                                            <asp:Image ID="imgEditPreview" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Current product image" />
                                        </div>
                                        <div class="admin-category-image-compact-body">
                                            <div class="admin-category-image-compact-head">
                                                <span class="admin-category-image-compact-title">Update Image</span>
                                                <span class="admin-product-image-slot-badge is-optional">Optional</span>
                                            </div>
                                            <div class="admin-product-image-dropzone" id="savingProductEditDropzone">
                                                <asp:FileUpload ID="FileUploadEdit" runat="server" CssClass="admin-file-input-hidden" accept=".png,.jpg,.jpeg,.webp,image/png,image/jpeg,image/webp" />
                                                <label class="admin-product-image-dropzone-label" for="<%= FileUploadEdit.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Choose new image</span>
                                                </label>
                                                <span id="savingProductEditFileName" class="admin-product-image-filename" data-empty-text="Keep current image">Keep current image</span>
                                            </div>
                                            <p class="admin-category-image-compact-hint">Leave unchanged to keep current image</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Product" OnClientClick="return validateEdit();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

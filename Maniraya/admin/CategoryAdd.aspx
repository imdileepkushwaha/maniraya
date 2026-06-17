<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CategoryAdd.aspx.cs" Inherits="admin_CategoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var maxImageSize = 1048576;
        var allowedExt = ['png', 'jpg', 'jpeg', 'svg', 'webp'];

        var categoryAddSlot = {
            uploadId: "<%= fuCategoryImage.ClientID %>",
            previewId: "<%= imgAddPreview.ClientID %>",
            placeholderId: "categoryAddPlaceholder",
            dropzoneId: "categoryAddDropzone",
            filenameId: "categoryAddFileName",
            slotId: "categoryAddSlot"
        };

        var categoryEditSlot = {
            uploadId: "<%= fuCategoryImageEdit.ClientID %>",
            previewId: "<%= imgEditPreview.ClientID %>",
            placeholderId: "categoryEditPlaceholder",
            dropzoneId: "categoryEditDropzone",
            filenameId: "categoryEditFileName",
            slotId: "categoryEditSlot"
        };

        function isAllowedImage(file) {
            if (!file) return false;
            var ext = file.name.substring(file.name.lastIndexOf('.') + 1).toLowerCase();
            return allowedExt.indexOf(ext) !== -1;
        }

        function validateImageFile(file, required) {
            if (!file || !file.name) {
                return required ? "Please select a category image" : "";
            }
            if (!isAllowedImage(file)) {
                return "Only PNG, JPG, SVG and WebP images are allowed";
            }
            if (file.size > maxImageSize) {
                return "Image size must be 1 MB or less";
            }
            return "";
        }

        function validate() {
            if (document.getElementById("<%=txtcountryname.ClientID%>").value.trim() === "") {
                alert('Enter Category Name');
                document.getElementById("<%=txtcountryname.ClientID%>").focus();
                return false;
            }
            var upload = document.getElementById(categoryAddSlot.uploadId);
            var msg = validateImageFile(upload && upload.files ? upload.files[0] : null, true);
            if (msg) { alert(msg); return false; }
            return true;
        }

        function validate2() {
            var nameEl = document.getElementById("<%=txtcountrynameedit.ClientID%>");
            if (!nameEl || nameEl.value.trim() === "") {
                alert('Enter Category Name');
                if (nameEl) nameEl.focus();
                return false;
            }
            var upload = document.getElementById(categoryEditSlot.uploadId);
            var file = upload && upload.files ? upload.files[0] : null;
            if (file) {
                var msg = validateImageFile(file, false);
                if (msg) { alert(msg); return false; }
            }
            return true;
        }

        function openCategoryEditModal(imageUrl) {
            syncSlotPreview(categoryEditSlot, imageUrl, "Current image");
            var upload = document.getElementById(categoryEditSlot.uploadId);
            if (upload) upload.value = "";
            if (typeof showAdminModal === "function") {
                showAdminModal("myModal");
            } else if (typeof showModal === "function") {
                showModal();
            } else if (window.jQuery) {
                jQuery("#myModal").modal({ backdrop: "static", keyboard: false, show: true });
            }
        }

        function closeCategoryEditModal() {
            if (typeof closeAdminModal === "function") {
                closeAdminModal("myModal");
            } else if (typeof Closepopup === "function") {
                Closepopup();
            } else if (window.jQuery) {
                jQuery("#myModal").modal("hide");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add Category</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product management</a></li>
            <li class="active">Add Category</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="row" style="margin-bottom: 20px;">
        <div class="col-md-12">
            <div class="box box-solid" style="margin-bottom: 0;">
                <div class="box-body text-center">
                    <a href="CategoryAdd.aspx" class="btn btn-primary"><i class="fa fa-sitemap"></i> Add Category</a>
                    <a href="SubcategoryAdd.aspx" class="btn btn-default"><i class="fa fa-code-fork"></i> Add Subcategory</a>
                    <a href="SizeAdd.aspx" class="btn btn-default"><i class="fa fa-arrows-v"></i> Add Size</a>
                    <a href="ColorAdd.aspx" class="btn btn-default"><i class="fa fa-paint-brush"></i> Add Color</a>
                    <a href="ProductSizeColorMaster.aspx" class="btn btn-default"><i class="fa fa-cogs"></i> Subcategory Setting</a>
                    <a href="ProductAdd.aspx" class="btn btn-default"><i class="fa fa-cube"></i> Add Product</a>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Category</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-folder-open"></i> Category Details</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtcountryname.ClientID %>">Category Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                <asp:TextBox ID="txtcountryname" CssClass="form-control" runat="server" placeholder="Enter category name"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Category Image</h5>
                                <div class="admin-media-upload-grid is-compact">
                                    <div class="admin-category-image-compact is-primary" id="categoryAddSlot">
                                        <div class="admin-category-image-compact-preview admin-product-image-preview-box">
                                            <div id="categoryAddPlaceholder" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                            </div>
                                            <asp:Image ID="imgAddPreview" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Category preview" />
                                        </div>
                                        <div class="admin-category-image-compact-body">
                                            <div class="admin-category-image-compact-head">
                                                <span class="admin-category-image-compact-title">Category Icon / Image</span>
                                                <span class="admin-product-image-slot-badge">Required</span>
                                            </div>
                                            <div class="admin-product-image-dropzone" id="categoryAddDropzone">
                                                <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="admin-file-input-hidden" accept=".png,.jpg,.jpeg,.svg,.webp,image/png,image/jpeg,image/svg+xml,image/webp" />
                                                <label class="admin-product-image-dropzone-label" for="<%= fuCategoryImage.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Choose image</span>
                                                </label>
                                                <span id="categoryAddFileName" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                            <p class="admin-category-image-compact-hint">PNG, JPG, SVG or WebP · max 1 MB</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Clear" CausesValidation="false" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Add Category" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Category List</h3>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("Categoryid") %>'></asp:Label>
                                                <asp:Label ID="lblCategoryImg" runat="server" Visible="false" Text='<%# Eval("img") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="72px" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgCategory" runat="server" ImageUrl='<%# Eval("Image") %>' CssClass="admin-grid-thumb admin-grid-thumb--category" AlternateText="Category" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="72px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" runat="server" CssClass="admin-grid-edit-btn" ToolTip="Edit category">
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

            <div id="myModal" class="modal fade admin-category-edit-dialog" tabindex="-1" role="dialog" aria-labelledby="categoryEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="categoryEditModalTitle"><i class="fa fa-pencil"></i> Edit Category</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblcountryid" Visible="false" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfEditCategoryImage" runat="server" />

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-folder-open"></i> Category Details</h5>
                                <div class="form-group">
                                    <label for="<%= txtcountrynameedit.ClientID %>">Category Name</label>
                                    <div class="admin-input-group">
                                        <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrynameedit" placeholder="Enter category name"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Category Image</h5>
                                <div class="admin-media-upload-grid is-compact">
                                    <div class="admin-category-image-compact" id="categoryEditSlot">
                                        <div class="admin-category-image-compact-preview admin-product-image-preview-box">
                                            <div id="categoryEditPlaceholder" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                            </div>
                                            <asp:Image ID="imgEditPreview" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Current category image" />
                                        </div>
                                        <div class="admin-category-image-compact-body">
                                            <div class="admin-category-image-compact-head">
                                                <span class="admin-category-image-compact-title">Update Image</span>
                                                <span class="admin-product-image-slot-badge is-optional">Optional</span>
                                            </div>
                                            <div class="admin-product-image-dropzone" id="categoryEditDropzone">
                                                <asp:FileUpload ID="fuCategoryImageEdit" runat="server" CssClass="admin-file-input-hidden" accept=".png,.jpg,.jpeg,.svg,.webp,image/png,image/jpeg,image/svg+xml,image/webp" />
                                                <label class="admin-product-image-dropzone-label" for="<%= fuCategoryImageEdit.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Choose new image</span>
                                                </label>
                                                <span id="categoryEditFileName" class="admin-product-image-filename" data-empty-text="Keep current image">Keep current image</span>
                                            </div>
                                            <p class="admin-category-image-compact-hint">Leave unchanged to keep current · PNG, JPG, SVG, WebP · max 1 MB</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Category" UseSubmitBehavior="true" CausesValidation="false" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
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


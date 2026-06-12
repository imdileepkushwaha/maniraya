<%@ Page Title="Add Slider" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AddSlider.aspx.cs" Inherits="admin_AddSlider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validateSlider() {
            if (document.getElementById("<%=txt_title.ClientID%>").value.trim() === "") {
                alert('Enter title');
                document.getElementById("<%=txt_title.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_content.ClientID%>").value.trim() === "") {
                alert('Enter content');
                document.getElementById("<%=txt_content.ClientID%>").focus();
                return false;
            }
            var isEdit = document.getElementById("<%=btn_update.ClientID%>").style.display !== "none"
                && document.getElementById("<%=btn_update.ClientID%>").offsetParent !== null;
            var upload = document.getElementById("<%=FileUpload1.ClientID%>");
            if (!isEdit && (!upload || !upload.value)) {
                alert('Please select a photo');
                return false;
            }
            return true;
        }

        var sliderImageSlot = {
            uploadId: "<%= FileUpload1.ClientID %>",
            previewId: "<%= imgPreview.ClientID %>",
            placeholderId: "sliderImgPlaceholder1",
            dropzoneId: "sliderDropzone1",
            filenameId: "sliderFileName1",
            slotId: "sliderImageSlot1"
        };

        function updateSliderFileName(file) {
            var nameEl = document.getElementById(sliderImageSlot.filenameId);
            if (!nameEl) return;
            if (file) {
                nameEl.textContent = file.name;
                nameEl.classList.add("has-file");
            } else if (nameEl.getAttribute("data-current") === "1") {
                nameEl.textContent = "Current image";
                nameEl.classList.add("has-file");
            } else {
                nameEl.textContent = "No file selected";
                nameEl.classList.remove("has-file");
            }
        }

        function previewSliderImage(file) {
            var slot = sliderImageSlot;
            var img = document.getElementById(slot.previewId);
            var placeholder = document.getElementById(slot.placeholderId);
            var dropzone = document.getElementById(slot.dropzoneId);
            var card = document.getElementById(slot.slotId);
            if (!file || !file.type || file.type.indexOf("image/") !== 0) return;
            var reader = new FileReader();
            reader.onload = function (ev) {
                img.src = ev.target.result;
                img.style.display = "block";
                if (placeholder) placeholder.style.display = "none";
                if (dropzone) dropzone.classList.add("has-file");
                if (card) card.classList.add("has-file");
            };
            reader.readAsDataURL(file);
            updateSliderFileName(file);
        }

        function syncSliderImagePreview(imageUrl) {
            var slot = sliderImageSlot;
            var img = document.getElementById(slot.previewId);
            var placeholder = document.getElementById(slot.placeholderId);
            var dropzone = document.getElementById(slot.dropzoneId);
            var card = document.getElementById(slot.slotId);
            var nameEl = document.getElementById(slot.filenameId);
            if (!imageUrl || imageUrl.indexOf("images.png") !== -1) {
                if (img) { img.src = ""; img.style.display = "none"; }
                if (placeholder) placeholder.style.display = "flex";
                if (dropzone) dropzone.classList.remove("has-file");
                if (card) card.classList.remove("has-file");
                if (nameEl) { nameEl.removeAttribute("data-current"); updateSliderFileName(null); }
                return;
            }
            if (img) { img.src = imageUrl; img.style.display = "block"; }
            if (placeholder) placeholder.style.display = "none";
            if (dropzone) dropzone.classList.add("has-file");
            if (card) card.classList.add("has-file");
            if (nameEl) { nameEl.setAttribute("data-current", "1"); updateSliderFileName(null); }
        }

        function bindSliderImageUpload() {
            var upload = document.getElementById(sliderImageSlot.uploadId);
            var dropzone = document.getElementById(sliderImageSlot.dropzoneId);
            if (upload && !upload._sliderBound) {
                upload._sliderBound = true;
                upload.addEventListener("change", function (e) {
                    previewSliderImage(e.target.files[0]);
                });
            }
            if (dropzone && !dropzone._dragBound) {
                dropzone._dragBound = true;
                ["dragenter", "dragover"].forEach(function (evtName) {
                    dropzone.addEventListener(evtName, function (e) {
                        e.preventDefault();
                        dropzone.classList.add("is-dragover");
                    });
                });
                ["dragleave", "drop"].forEach(function (evtName) {
                    dropzone.addEventListener(evtName, function (e) {
                        e.preventDefault();
                        dropzone.classList.remove("is-dragover");
                    });
                });
                dropzone.addEventListener("drop", function (e) {
                    var file = e.dataTransfer && e.dataTransfer.files ? e.dataTransfer.files[0] : null;
                    if (!file || !upload) return;
                    try {
                        var dt = new DataTransfer();
                        dt.items.add(file);
                        upload.files = dt.files;
                    } catch (ex) { return; }
                    previewSliderImage(file);
                });
            }
        }

        Sys.Application.add_load(bindSliderImageUpload);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Slider</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Slider</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Add Slider</h3>
                </div>

                <div class="box-body admin-product-form">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />

                    <div class="admin-form-section">
                        <h5 class="admin-form-section-title"><i class="fa fa-sliders"></i> Slider Details</h5>
                        <div class="row">
                            <div class="col-md-6 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txt_title.ClientID %>">Title</label>
                                    <div class="admin-input-group">
                                        <span class="admin-input-icon"><i class="fa fa-header"></i></span>
                                        <asp:TextBox ID="txt_title" runat="server" CssClass="form-control" placeholder="Enter slider title" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6">
                                <div class="form-group">
                                    <label for="<%= txt_content.ClientID %>">Content</label>
                                    <div class="admin-input-group">
                                        <span class="admin-input-icon"><i class="fa fa-align-left"></i></span>
                                        <asp:TextBox ID="txt_content" runat="server" CssClass="form-control" placeholder="Enter slider content" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="admin-form-section admin-form-section-last">
                        <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Slider Photo</h5>
                        <p class="admin-section-hint">Upload JPG, PNG or GIF. Maximum file size 500 KB.</p>
                        <div class="admin-media-upload-grid">
                            <div class="admin-product-image-slot is-primary" id="sliderImageSlot1">
                                <div class="admin-product-image-slot-head">
                                    <p class="admin-product-image-slot-title">Slider Image</p>
                                    <span class="admin-product-image-slot-badge">Required</span>
                                </div>
                                <div class="admin-product-image-preview-box">
                                    <div id="sliderImgPlaceholder1" class="admin-product-image-placeholder">
                                        <i class="fa fa-image"></i>
                                        <span>No image</span>
                                    </div>
                                    <asp:Image ID="imgPreview" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Slider preview" />
                                </div>
                                <div class="admin-product-image-dropzone" id="sliderDropzone1">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                    <label class="admin-product-image-dropzone-label" for="<%= FileUpload1.ClientID %>">
                                        <i class="fa fa-cloud-upload"></i>
                                        <span>Browse or drop image</span>
                                    </label>
                                    <span id="sliderFileName1" class="admin-product-image-filename">No file selected</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box-footer admin-product-footer">
                    <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Delete" Visible="false" OnClick="btndelete_Click" OnClientClick="return confirm('Delete this slider?');" />
                    <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Clear" CausesValidation="false" OnClick="btnReset_Click" />
                    <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update Slider" Visible="false" OnClick="btn_update_Click" OnClientClick="return validateSlider();" />
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Add Slider" OnClick="btnSubmit_Click" OnClientClick="return validateSlider();" />
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Slider List</h3>
                </div>

                <div class="box-body">
                    <div class="form-group table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanging="grid_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Content">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontent" runat="server" Text='<%#Eval("Content") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Photo">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" ImageUrl='<%#Eval("Img_Url") %>' runat="server" CssClass="admin-grid-thumb" Height="60px" Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Link" ControlStyle-CssClass="admin-grid-action-link" HeaderText="Action" SelectText="Edit" ShowSelectButton="True">
                                    <ControlStyle CssClass="admin-grid-action-link" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="admin-grid-action-cell" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

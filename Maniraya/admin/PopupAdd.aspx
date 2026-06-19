<%@ Page Title="Add Popup" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PopupAdd.aspx.cs" Inherits="admin_PopupAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .popup-admin-row {
            margin-bottom: 1.25rem;
        }

        .popup-admin-card {
            height: 100%;
        }

        .popup-admin-card.is-editing {
            border-top: 3px solid #e5a906;
            box-shadow: 0 8px 24px rgba(229, 169, 6, 0.12);
        }

        .popup-admin-card-head {
            display: flex;
            align-items: center;
            gap: 0.65rem;
        }

        .popup-admin-card-icon {
            width: 2.2rem;
            height: 2.2rem;
            border-radius: 0.55rem;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #fff7df;
            color: #b8860b;
            font-size: 1rem;
        }

        .popup-admin-card-icon--image {
            background: #eef6ff;
            color: #2563eb;
        }

        .popup-grid-thumb {
            width: 88px;
            height: 56px;
            object-fit: cover;
            border-radius: 0.4rem;
            border: 1px solid #e5e7eb;
        }

        .popup-type-badge {
            display: inline-block;
            padding: 0.2rem 0.55rem;
            border-radius: 999px;
            font-size: 0.78rem;
            font-weight: 600;
        }

        .popup-type-badge--text {
            background: #fff7df;
            color: #9a6700;
        }

        .popup-type-badge--image {
            background: #eef6ff;
            color: #1d4ed8;
        }

        .popup-list-box {
            margin-top: 0.25rem;
        }
    </style>
    <script type="text/javascript">
        function showPopupDeleteConfirm(popupId) {
            var hidden = document.getElementById('<%= hfDeletePopupId.ClientID %>');
            if (hidden) {
                hidden.value = popupId;
            }

            if (typeof showAdminModal === 'function') {
                showAdminModal('popupDeleteModal');
            } else if (window.jQuery) {
                jQuery('#popupDeleteModal').modal({ backdrop: 'static', keyboard: false, show: true });
            }

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Website Popup</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Website Management</a></li>
            <li class="active">Add Popup</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:HiddenField ID="hfEditId" runat="server" />
    <asp:HiddenField ID="hfEditMode" runat="server" />
    <asp:HiddenField ID="hfCurrentImage" runat="server" />
    <asp:HiddenField ID="hfDeletePopupId" runat="server" />

    <div class="row popup-admin-row">
        <div class="col-md-6 col-sm-12">
            <asp:Panel ID="pnlTextSection" runat="server" CssClass="box box-primary popup-admin-card">
                <div class="box-header with-border">
                    <div class="popup-admin-card-head">
                        <span class="popup-admin-card-icon"><i class="fa fa-file-text-o"></i></span>
                        <h3 class="box-title">Text Popup</h3>
                    </div>
                </div>
                <div class="box-body admin-product-form">
                    <p class="admin-section-hint">Create a text announcement popup for the home page. Title is optional.</p>
                    <div class="form-group">
                        <label for="<%= txtTitle.ClientID %>">Popup Title</label>
                        <div class="admin-input-group">
                            <span class="admin-input-icon"><i class="fa fa-header"></i></span>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="e.g. Special Offer"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtPopupContent.ClientID %>">Popup Message</label>
                        <asp:TextBox ID="txtPopupContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8" placeholder="Enter announcement message"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= chkTextStatus.ClientID %>">Status (Active)</label>
                        <asp:CheckBox ID="chkTextStatus" runat="server" Checked="true" />
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnCancelText" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnCancelText_Click" />
                    <asp:Button ID="btnSubmitText" runat="server" Text="Add Text Popup" CssClass="btn btn-primary" OnClick="btnSubmitText_Click" />
                </div>
            </asp:Panel>
        </div>

        <div class="col-md-6 col-sm-12">
            <asp:Panel ID="pnlImageSection" runat="server" CssClass="box box-primary popup-admin-card">
                <div class="box-header with-border">
                    <div class="popup-admin-card-head">
                        <span class="popup-admin-card-icon popup-admin-card-icon--image"><i class="fa fa-picture-o"></i></span>
                        <h3 class="box-title">Image Popup</h3>
                    </div>
                </div>
                <div class="box-body admin-product-form">
                    <p class="admin-section-hint">Upload a banner or offer image popup. JPG, PNG or GIF supported.</p>
                    <div class="form-group">
                        <label for="<%= txtImageTitle.ClientID %>">Popup Title</label>
                        <div class="admin-input-group">
                            <span class="admin-input-icon"><i class="fa fa-header"></i></span>
                            <asp:TextBox ID="txtImageTitle" runat="server" CssClass="form-control" placeholder="Optional title"></asp:TextBox>
                        </div>
                    </div>

                    <div class="admin-form-section admin-form-section-last">
                        <h5 class="admin-form-section-title"><i class="fa fa-cloud-upload"></i> Popup Image</h5>
                        <div class="admin-media-upload-grid">
                            <div class="admin-product-image-slot is-primary" id="popupImageSlot1">
                                <div class="admin-product-image-slot-head">
                                    <p class="admin-product-image-slot-title">Popup Banner</p>
                                    <span class="admin-product-image-slot-badge">Required</span>
                                </div>
                                <div class="admin-product-image-preview-box">
                                    <div id="popupImgPlaceholder1" class="admin-product-image-placeholder">
                                        <i class="fa fa-image"></i>
                                        <span>No image selected</span>
                                    </div>
                                    <asp:Image ID="imgPreview" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Popup preview" />
                                </div>
                                <div class="admin-product-image-dropzone" id="popupDropzone1">
                                    <asp:FileUpload ID="fuPopupImage" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                    <label class="admin-product-image-dropzone-label" for="<%= fuPopupImage.ClientID %>">
                                        <i class="fa fa-cloud-upload"></i>
                                        <span>Browse or drop image here</span>
                                    </label>
                                    <span id="popupFileName1" class="admin-product-image-filename">No file selected</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group" style="margin-top:1rem;">
                        <label for="<%= chkImageStatus.ClientID %>">Status (Active)</label>
                        <asp:CheckBox ID="chkImageStatus" runat="server" Checked="true" />
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnCancelImage" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnCancelImage_Click" />
                    <asp:Button ID="btnSubmitImage" runat="server" Text="Add Image Popup" CssClass="btn btn-primary" OnClick="btnSubmitImage_Click" />
                </div>
            </asp:Panel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary popup-list-box">
                <div class="box-header with-border">
                    <h3 class="box-title">Popup List</h3>
                </div>
                <div class="box-body table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Literal ID="litPopupType" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="imgGridPopup" runat="server"
                                        ImageUrl='<%# Eval("PopupImage") %>'
                                        Visible='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("PopupImage"))) %>'
                                        CssClass="popup-grid-thumb"
                                        AlternateText="Popup image" />
                                    <asp:Literal ID="litNoImage" runat="server"
                                        Text="-"
                                        Visible='<%# string.IsNullOrWhiteSpace(Convert.ToString(Eval("PopupImage"))) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message">
                                <ItemTemplate>
                                    <span style="word-break: break-word;"><%# Eval("PopupContent") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkToggle" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName='<%# Convert.ToBoolean(Eval("Status")) ? "deactivate" : "activate" %>' OnClick="lnkToggle_Click" CssClass="admin-grid-edit-btn" ToolTip='<%# Convert.ToBoolean(Eval("Status")) ? "Deactivate" : "Activate" %>'><i class='<%# Convert.ToBoolean(Eval("Status")) ? "icon fa fa-toggle-on" : "icon fa fa-toggle-off" %>' aria-hidden="true"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="admin-grid-delete-btn" OnClientClick='<%# "showPopupDeleteConfirm(\"" + Eval("Id") + "\"); return false;" %>' ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <div id="popupDeleteModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="popupDeleteModalTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="popupDeleteModalTitle">Delete Popup</h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this popup?</p>
                    <p class="text-muted" style="margin-bottom:0;">This action cannot be undone.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

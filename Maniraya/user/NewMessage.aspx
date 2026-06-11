<%@ Page Title="New Message" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="NewMessage.aspx.cs" Inherits="Associate_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Compose Mail</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Customer Care</a></li>
            <li class="active">Compose Mail</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="profile-page">
        <div class="profile-hero">
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-envelope"></i></div>
            <div class="profile-hero-info">
                <h2>Compose Mail</h2>
                <p class="profile-hero-meta">Send a message or support ticket to admin</p>
            </div>
            <div class="profile-hero-actions">
                <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
            </div>
        </div>
        <div class="profile-sections">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">New Message</h3>
                </div>
                <div class="box-body profile-form-grid">
                    <asp:TextBox ID="txttoid" CssClass="form-control" runat="server" Text="admin" Style="display:none;" OnTextChanged="txttoid_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:Label ID="lblUserName" runat="server" Text="" Visible="false"></asp:Label>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label><i class="fa fa-tag"></i> Subject</label>
                                <asp:TextBox ID="txtmessagetitle" CssClass="form-control" runat="server" placeholder="Enter message subject"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group profile-upload-field">
                                <label><i class="fa fa-paperclip"></i> Attachment <span class="text-muted">(optional)</span></label>
                                <div class="profile-upload-zone profile-upload-zone-compact profile-upload-zone-attach" id="messageAttachZone">
                                    <div class="profile-upload-zone-inner">
                                        <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-paperclip"></i></span>
                                        <p class="profile-upload-title">Drag &amp; drop file here</p>
                                        <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from device</span></p>
                                        <p class="profile-upload-meta">PDF, DOC, DOCX, XLS, XLSX, TXT, JPG, PNG allowed</p>
                                    </div>
                                    <asp:FileUpload ID="fupAttachment" runat="server" CssClass="profile-upload-input"
                                        accept=".bmp,.gif,.png,.jpg,.jpeg,.doc,.docx,.xls,.xlsx,.txt,.pdf" />
                                </div>
                                <div class="profile-upload-selection profile-upload-selection-attach" id="messageAttachSelection" hidden>
                                    <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                        <img id="messageAttachPreviewImg" src="" alt="Attachment preview" hidden />
                                        <span class="profile-upload-doc-icon" id="messageAttachDocIcon" aria-hidden="true"><i class="fa fa-file-text-o"></i></span>
                                    </div>
                                    <div class="profile-upload-selection-info">
                                        <span class="profile-upload-filechip" id="messageAttachFilechip"></span>
                                        <button type="button" class="profile-upload-clear" id="messageAttachClear">
                                            <i class="fa fa-times"></i> Remove
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label><i class="fa fa-comment"></i> Message</label>
                                <asp:TextBox ID="txtdescription" CssClass="form-control profile-compose-textarea" TextMode="MultiLine" Rows="8" runat="server" placeholder="Write your message here..."></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer profile-compose-actions">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btncancel" CssClass="btn btn-danger" Text="Cancel" runat="server" OnClick="btncancel_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        (function () {
            var previewUrl = null;
            var allowedExt = ['.bmp', '.gif', '.png', '.jpg', '.jpeg', '.doc', '.docx', '.xls', '.xlsx', '.txt', '.pdf'];

            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }

            function getExtension(name) {
                var dot = name.lastIndexOf('.');
                return dot >= 0 ? name.substring(dot).toLowerCase() : '';
            }

            function isAllowedFile(file) {
                if (!file || !file.name) return false;
                return allowedExt.indexOf(getExtension(file.name)) !== -1;
            }

            function getDocIconClass(ext) {
                if (ext === '.pdf') return 'fa-file-pdf-o';
                if (ext === '.xls' || ext === '.xlsx') return 'fa-file-excel-o';
                if (ext === '.doc' || ext === '.docx') return 'fa-file-word-o';
                if (ext === '.txt') return 'fa-file-text-o';
                if (ext === '.jpg' || ext === '.jpeg' || ext === '.png' || ext === '.gif' || ext === '.bmp') return 'fa-file-image-o';
                return 'fa-file-o';
            }

            function clearSelection(input, zone, selection, previewImg, docIcon, filechip) {
                input.value = '';
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                    previewUrl = null;
                }
                previewImg.removeAttribute('src');
                previewImg.hidden = true;
                docIcon.hidden = false;
                docIcon.innerHTML = '<i class="fa fa-file-text-o"></i>';
                filechip.textContent = '';
                selection.hidden = true;
                zone.classList.remove('is-hidden');
            }

            function showSelection(file, zone, selection, previewImg, docIcon, filechip) {
                if (!isAllowedFile(file)) {
                    alert('Please choose a valid file type (PDF, DOC, DOCX, XLS, XLSX, TXT, JPG, PNG).');
                    return false;
                }

                var ext = getExtension(file.name);
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                    previewUrl = null;
                }

                if (file.type && file.type.match(/^image\//i)) {
                    previewUrl = URL.createObjectURL(file);
                    previewImg.src = previewUrl;
                    previewImg.hidden = false;
                    docIcon.hidden = true;
                } else {
                    previewImg.removeAttribute('src');
                    previewImg.hidden = true;
                    docIcon.hidden = false;
                    docIcon.innerHTML = '<i class="fa ' + getDocIconClass(ext) + '"></i>';
                }

                filechip.textContent = file.name + ' · ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initMessageAttachZone() {
                var input = document.getElementById('<%= fupAttachment.ClientID %>');
                var zone = document.getElementById('messageAttachZone');
                var selection = document.getElementById('messageAttachSelection');
                var previewImg = document.getElementById('messageAttachPreviewImg');
                var docIcon = document.getElementById('messageAttachDocIcon');
                var filechip = document.getElementById('messageAttachFilechip');
                var clearBtn = document.getElementById('messageAttachClear');

                if (!input || !zone || !selection || zone.getAttribute('data-bound') === '1') {
                    return;
                }

                zone.setAttribute('data-bound', '1');

                input.addEventListener('change', function () {
                    if (!input.files || !input.files.length) {
                        clearSelection(input, zone, selection, previewImg, docIcon, filechip);
                        return;
                    }
                    if (!showSelection(input.files[0], zone, selection, previewImg, docIcon, filechip)) {
                        clearSelection(input, zone, selection, previewImg, docIcon, filechip);
                    }
                });

                clearBtn.addEventListener('click', function () {
                    clearSelection(input, zone, selection, previewImg, docIcon, filechip);
                });

                ['dragenter', 'dragover'].forEach(function (eventName) {
                    zone.addEventListener(eventName, function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.add('is-dragover');
                    });
                });

                ['dragleave', 'drop'].forEach(function (eventName) {
                    zone.addEventListener(eventName, function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.remove('is-dragover');
                    });
                });

                zone.addEventListener('drop', function (e) {
                    var files = e.dataTransfer && e.dataTransfer.files;
                    if (!files || !files.length) {
                        return;
                    }

                    input.files = files;
                    if (!showSelection(files[0], zone, selection, previewImg, docIcon, filechip)) {
                        clearSelection(input, zone, selection, previewImg, docIcon, filechip);
                    }
                });
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initMessageAttachZone);
            } else {
                initMessageAttachZone();
            }
        })();
    </script>
</asp:Content>

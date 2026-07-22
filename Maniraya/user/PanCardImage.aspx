<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="PanCardImage.aspx.cs" Inherits="user_PanCardImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=hdstatus.ClientID%>").value == "Active") {
                if (!confirm('You can upload your pan details once.Are you sure want to update?')) {
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>PAN Card Upload</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="UserProfile.aspx">KYC</a></li>
            <li class="active">PAN Card</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdstatus" runat="server" />
            <div class="profile-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-credit-card"></i></div>
                    <div class="profile-hero-info">
                        <h2>PAN Card Upload</h2>
                        <p class="profile-hero-meta">Submit your PAN card image and number for verification</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProfile.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-user"></i> View Profile</a>
                    </div>
                </div>
                <div class="profile-sections">
                    <div class="box box-primary profile-kyc-card">
                        <div class="box-header with-border">
                            <h3 class="box-title">PAN Card Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="profile-kyc-layout">
                                <div class="profile-kyc-form">
                                    <div class="row profile-form-grid">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-id-badge"></i> User ID</label>
                                                <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-user"></i> User Name</label>
                                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="divStatus" runat="server" visible="false">
                                            <div class="form-group">
                                                <label><i class="fa fa-check-square"></i> Approval Status</label>
                                                <div class="profile-kyc-status-wrap">
                                                    <asp:Label ID="lblApprovalStatus" runat="server" CssClass="profile-kyc-badge"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-hashtag"></i> PAN Card Number</label>
                                                <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control profile-pan-input" placeholder="e.g. ABCDE1234F" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group profile-upload-field">
                                                <label><i class="fa fa-file-image"></i> Upload PAN Card</label>
                                                <div class="profile-upload-zone" id="panUploadZone">
                                                    <div class="profile-upload-zone-inner">
                                                        <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                        <p class="profile-upload-title">Drag &amp; drop PAN card image here</p>
                                                        <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from device</span></p>
                                                        <p class="profile-upload-meta">JPG, PNG, WEBP � Clear photo of front side</p>
                                                    </div>
                                                    <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                                </div>
                                                <div class="profile-upload-selection" id="panUploadSelection" hidden>
                                                    <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                        <img id="panUploadPreview" src="" alt="Selected PAN preview" />
                                                    </div>
                                                    <div class="profile-upload-selection-info">
                                                        <span class="profile-upload-filechip" id="panUploadFilechip"></span>
                                                        <button type="button" class="profile-upload-clear" id="panUploadClear">
                                                            <i class="fa fa-times"></i> Remove
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="profile-doc-preview">
                                    <p class="profile-photo-preview-label">Uploaded PAN</p>
                                    <asp:ImageButton ID="ImageShow" runat="server" Width="200px" Height="130px" OnClick="ImageShow_Click" />
                                    <span class="profile-doc-preview-hint">Click to enlarge</span>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" id="div_update" runat="server" visible="false">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        <div class="box-footer" id="div_noupdate" runat="server" visible="false">
                            <div class="profile-alert"><i class="fa fa-exclamation-circle"></i> You cannot upload PAN details. Please contact admin.</div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPhotolarge" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Image ID="ImageLarge" runat="server" Width="100%" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript">
        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false });
        }
        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }

        (function () {
            var previewUrl = null;

            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }

            function clearSelection(input, zone, selection, preview, filechip) {
                input.value = '';
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                    previewUrl = null;
                }
                preview.removeAttribute('src');
                filechip.textContent = '';
                selection.hidden = true;
                zone.classList.remove('is-hidden');
            }

            function showSelection(file, zone, selection, preview, filechip) {
                if (!file || !file.type.match(/^image\//i)) {
                    alert('Please choose a valid image file (JPG, PNG, WEBP).');
                    return false;
                }

                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                }

                previewUrl = URL.createObjectURL(file);
                preview.src = previewUrl;
                filechip.textContent = file.name + ' � ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initPanUploadZone() {
                var input = document.getElementById('<%= ImageUpload.ClientID %>');
                var zone = document.getElementById('panUploadZone');
                var selection = document.getElementById('panUploadSelection');
                var preview = document.getElementById('panUploadPreview');
                var filechip = document.getElementById('panUploadFilechip');
                var clearBtn = document.getElementById('panUploadClear');

                if (!input || !zone || !selection || zone.getAttribute('data-bound') === '1') {
                    return;
                }

                zone.setAttribute('data-bound', '1');

                input.addEventListener('change', function () {
                    if (!input.files || !input.files.length) {
                        clearSelection(input, zone, selection, preview, filechip);
                        return;
                    }
                    if (!showSelection(input.files[0], zone, selection, preview, filechip)) {
                        clearSelection(input, zone, selection, preview, filechip);
                    }
                });

                clearBtn.addEventListener('click', function () {
                    clearSelection(input, zone, selection, preview, filechip);
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
                    if (!showSelection(files[0], zone, selection, preview, filechip)) {
                        clearSelection(input, zone, selection, preview, filechip);
                    }
                });
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initPanUploadZone);
            } else {
                initPanUploadZone();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initPanUploadZone);
            }
        })();
    </script>
</asp:Content>

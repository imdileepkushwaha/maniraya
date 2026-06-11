<%@ Page Title="Withdrawl Request" Language="C#" MasterPageFile="Masterpage.master" AutoEventWireup="true" CodeFile="PhotoUpload.aspx.cs" Inherits="PhotoUpload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=3" rel="stylesheet" />
     <script type="text/javascript">
         function validate() {

             if (!confirm('You can update your photo only once.Are you sure want to update?')) {
                 return false;
             }
        <%--    if (document.getElementById("<%=txtoldpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Old Password');
                document.getElementById("<%=txtoldpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter New Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }--%>

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Photo Upload</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="UserProfile.aspx">My Profile</a></li>
            <li class="active">Photo Upload</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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

            <div class="profile-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-camera"></i></div>
                    <div class="profile-hero-info">
                        <h2>Photo Upload</h2>
                        <p class="profile-hero-meta">Update your profile picture</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProfile.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-user"></i> View Profile</a>
                        <a href="UserEdit.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-pencil"></i> Edit Profile</a>
                    </div>
                </div>
                <div class="profile-sections">
                    <div class="box box-primary profile-photo-card">
                        <div class="box-header with-border">
                            <h3 class="box-title">Upload Photo</h3>
                        </div>
                        <div class="box-body">
                            <div class="profile-photo-layout">
                                <div class="profile-photo-form">
                                    <div class="row profile-form-grid">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-id-badge"></i> User ID</label>
                                                <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-user"></i> User Name</label>
                                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group profile-upload-field">
                                                <label><i class="fa fa-image"></i> Choose Image</label>
                                                <div class="profile-upload-zone" id="profileUploadZone">
                                                    <div class="profile-upload-zone-inner">
                                                        <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload"></i></span>
                                                        <p class="profile-upload-title">Drag &amp; drop your photo here</p>
                                                        <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from device</span></p>
                                                        <p class="profile-upload-meta">JPG, PNG, WEBP · Recommended under 2 MB</p>
                                                    </div>
                                                    <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                                </div>
                                                <div class="profile-upload-selection" id="profileUploadSelection" hidden>
                                                    <div class="profile-upload-selection-preview">
                                                        <img id="profileUploadPreview" src="" alt="Selected photo preview" />
                                                    </div>
                                                    <div class="profile-upload-selection-info">
                                                        <span class="profile-upload-filechip" id="profileUploadFilechip"></span>
                                                        <button type="button" class="profile-upload-clear" id="profileUploadClear">
                                                            <i class="fa fa-times"></i> Remove
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="profile-photo-preview">
                                    <p class="profile-photo-preview-label">Current Photo</p>
                                    <asp:ImageButton ID="ImageShow" runat="server" Width="140px" Height="140px" OnClick="ImageShow_Click" />
                                    <span class="text-muted" style="font-size:0.78rem;">Click to enlarge</span>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" id="div_update" runat="server" visible="false">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        <div class="box-footer" id="div_noupdate" runat="server" visible="false">
                            <div class="profile-alert"><i class="fa fa-exclamation-circle"></i> You cannot update photo. Please contact admin.</div>
                        </div>
                    </div>
                </div>
            </div>


                <div id="DivPhotolarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content" style="margin-top: 10%;">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImageLarge" runat="server" Width="570px" Height="400px" />
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>

            
        </ContentTemplate>
        <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">   
 
    <script type="text/javascript">


        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
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
                filechip.textContent = file.name + ' · ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initProfileUploadZone() {
                var input = document.getElementById('<%= ImageUpload.ClientID %>');
                var zone = document.getElementById('profileUploadZone');
                var selection = document.getElementById('profileUploadSelection');
                var preview = document.getElementById('profileUploadPreview');
                var filechip = document.getElementById('profileUploadFilechip');
                var clearBtn = document.getElementById('profileUploadClear');

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
                document.addEventListener('DOMContentLoaded', initProfileUploadZone);
            } else {
                initProfileUploadZone();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initProfileUploadZone);
            }
        })();
        </script>
</asp:Content>




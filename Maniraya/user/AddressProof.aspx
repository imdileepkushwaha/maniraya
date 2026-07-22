<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="AddressProof.aspx.cs" Inherits="user_AddressProof" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=hdstatus.ClientID%>").value == "Active") {
                if (!confirm('You can upload your address details once.Are you sure want to update?')) {
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Address Proof / Aadhar</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="UserProfile.aspx">KYC</a></li>
            <li class="active">Address Proof</li>
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
            <asp:HiddenField ID="hdstatus" runat="server" />
            <div class="profile-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-id-card"></i></div>
                    <div class="profile-hero-info">
                        <h2>Address Proof / Aadhar</h2>
                        <p class="profile-hero-meta">Upload Aadhar images and update your address details</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProfile.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-user"></i> View Profile</a>
                        <a href="PanCardImage.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-credit-card"></i> PAN Card</a>
                    </div>
                </div>
                <div class="profile-sections">
                    <div class="box box-primary profile-kyc-card">
                        <div class="box-header with-border">
                            <h3 class="box-title">Aadhar Documents</h3>
                        </div>
                        <div class="box-body">
                            <div class="profile-aadhar-layout">
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
                                                <label><i class="fa fa-hashtag"></i> Aadhar Number</label>
                                                <asp:TextBox ID="txtAdharnumber" runat="server" CssClass="form-control profile-aadhaar-input" placeholder="12-digit Aadhar number" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="profile-upload-grid">
                                        <div class="form-group profile-upload-field">
                                            <label><i class="fa fa-file-image"></i> Front Side</label>
                                            <div class="profile-upload-zone profile-upload-zone-compact" id="aadharFrontZone">
                                                <div class="profile-upload-zone-inner">
                                                    <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                    <p class="profile-upload-title">Upload front side</p>
                                                    <p class="profile-upload-hint">or <span class="profile-upload-browse">browse</span></p>
                                                </div>
                                                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                            </div>
                                            <div class="profile-upload-selection" id="aadharFrontSelection" hidden>
                                                <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                    <img id="aadharFrontPreview" src="" alt="Aadhar front preview" />
                                                </div>
                                                <div class="profile-upload-selection-info">
                                                    <span class="profile-upload-filechip" id="aadharFrontFilechip"></span>
                                                    <button type="button" class="profile-upload-clear" id="aadharFrontClear">
                                                        <i class="fa fa-times"></i> Remove
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group profile-upload-field">
                                            <label><i class="fa fa-file-image"></i> Back Side</label>
                                            <div class="profile-upload-zone profile-upload-zone-compact" id="aadharBackZone">
                                                <div class="profile-upload-zone-inner">
                                                    <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                    <p class="profile-upload-title">Upload back side</p>
                                                    <p class="profile-upload-hint">or <span class="profile-upload-browse">browse</span></p>
                                                </div>
                                                <asp:FileUpload ID="ImageUpload2" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                            </div>
                                            <div class="profile-upload-selection" id="aadharBackSelection" hidden>
                                                <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                    <img id="aadharBackPreview" src="" alt="Aadhar back preview" />
                                                </div>
                                                <div class="profile-upload-selection-info">
                                                    <span class="profile-upload-filechip" id="aadharBackFilechip"></span>
                                                    <button type="button" class="profile-upload-clear" id="aadharBackClear">
                                                        <i class="fa fa-times"></i> Remove
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="profile-aadhar-previews">
                                    <div class="profile-doc-preview">
                                        <p class="profile-photo-preview-label">Front Side</p>
                                        <asp:ImageButton ID="ImageShow" runat="server" Width="200px" Height="130px" OnClick="ImageShow_Click" />
                                        <span class="profile-doc-preview-hint">Click to enlarge</span>
                                    </div>
                                    <div class="profile-doc-preview">
                                        <p class="profile-photo-preview-label">Back Side</p>
                                        <asp:ImageButton ID="ImageShow2" runat="server" Width="200px" Height="130px" OnClick="ImageShow2_Click" />
                                        <span class="profile-doc-preview-hint">Click to enlarge</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="box box-primary profile-kyc-card">
                        <div class="box-header with-border">
                            <h3 class="box-title">Address Details</h3>
                        </div>
                        <div class="box-body profile-form-grid">
                            <p class="profile-subsection-title"><i class="fa fa-map-marker-alt"></i> Residential Address</p>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label><i class="fa fa-home"></i> Address</label>
                                        <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server" Rows="3" placeholder="Enter your full address"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><i class="fa fa-globe"></i> Country</label>
                                        <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select Country</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><i class="fa fa-map"></i> State</label>
                                        <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select State</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><i class="fa fa-building"></i> City</label>
                                        <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><i class="fa fa-map-pin"></i> Area / Other</label>
                                        <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" placeholder="Area or locality"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><i class="fa fa-envelope"></i> Pincode</label>
                                        <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="6-digit pincode" MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" id="div_update" runat="server" visible="false">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        <div class="box-footer" id="div_noupdate" runat="server" visible="false">
                            <div class="profile-alert"><i class="fa fa-exclamation-circle"></i> You cannot upload address details. Please contact admin.</div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
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
            var previewUrls = {};

            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }

            function clearSelection(config) {
                var input = document.getElementById(config.inputId);
                var zone = document.getElementById(config.zoneId);
                var selection = document.getElementById(config.selectionId);
                var preview = document.getElementById(config.previewId);
                var filechip = document.getElementById(config.filechipId);

                if (!input || !zone || !selection || !preview || !filechip) {
                    return;
                }

                input.value = '';
                if (previewUrls[config.key]) {
                    URL.revokeObjectURL(previewUrls[config.key]);
                    delete previewUrls[config.key];
                }
                preview.removeAttribute('src');
                filechip.textContent = '';
                selection.hidden = true;
                zone.classList.remove('is-hidden');
            }

            function showSelection(file, config) {
                var zone = document.getElementById(config.zoneId);
                var selection = document.getElementById(config.selectionId);
                var preview = document.getElementById(config.previewId);
                var filechip = document.getElementById(config.filechipId);

                if (!file || !file.type.match(/^image\//i)) {
                    alert('Please choose a valid image file (JPG, PNG, WEBP).');
                    return false;
                }

                if (previewUrls[config.key]) {
                    URL.revokeObjectURL(previewUrls[config.key]);
                }

                previewUrls[config.key] = URL.createObjectURL(file);
                preview.src = previewUrls[config.key];
                filechip.textContent = file.name + ' � ' + formatFileSize(file.size);
                selection.hidden = false;
                zone.classList.add('is-hidden');
                return true;
            }

            function initUploadZone(config) {
                var input = document.getElementById(config.inputId);
                var zone = document.getElementById(config.zoneId);
                var selection = document.getElementById(config.selectionId);
                var preview = document.getElementById(config.previewId);
                var filechip = document.getElementById(config.filechipId);
                var clearBtn = document.getElementById(config.clearId);

                if (!input || !zone || !selection || zone.getAttribute('data-bound') === '1') {
                    return;
                }

                zone.setAttribute('data-bound', '1');

                input.addEventListener('change', function () {
                    if (!input.files || !input.files.length) {
                        clearSelection(config);
                        return;
                    }
                    if (!showSelection(input.files[0], config)) {
                        clearSelection(config);
                    }
                });

                clearBtn.addEventListener('click', function () {
                    clearSelection(config);
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
                    if (!showSelection(files[0], config)) {
                        clearSelection(config);
                    }
                });
            }

            function initAadharUploadZones() {
                initUploadZone({
                    key: 'front',
                    inputId: '<%= ImageUpload.ClientID %>',
                    zoneId: 'aadharFrontZone',
                    selectionId: 'aadharFrontSelection',
                    previewId: 'aadharFrontPreview',
                    filechipId: 'aadharFrontFilechip',
                    clearId: 'aadharFrontClear'
                });

                initUploadZone({
                    key: 'back',
                    inputId: '<%= ImageUpload2.ClientID %>',
                    zoneId: 'aadharBackZone',
                    selectionId: 'aadharBackSelection',
                    previewId: 'aadharBackPreview',
                    filechipId: 'aadharBackFilechip',
                    clearId: 'aadharBackClear'
                });
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initAadharUploadZones);
            } else {
                initAadharUploadZones();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initAadharUploadZones);
            }
        })();
    </script>
</asp:Content>

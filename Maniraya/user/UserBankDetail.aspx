<%@ Page Title="Edit User Details" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="UserBankDetail.aspx.cs" Inherits="UserBankDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=hdstatus.ClientID%>").value == "1") {
                if (!confirm('You can update your bank details only once.Are you sure want to update?')) {
                    return false;
                }
            }


            if (document.getElementById("<%=txtsponserid.ClientID%>").value == "") {
                alert('Enter Sponser Id');
                document.getElementById("<%=txtsponserid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert('Enter Name');
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtaccountholdername.ClientID%>").value == "") {
                alert('Enter Account holder Name');
                document.getElementById("<%=txtaccountholdername.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtaccountno.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtaccountno.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtifsccode.ClientID%>").value == "") {
                alert('Enter IFSC CODE');
                document.getElementById("<%=txtifsccode.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtbranchname.ClientID%>").value == "") {
                alert('Enter Branch Name');
                document.getElementById("<%=txtbranchname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddbank.ClientID%>").value == "0") {
                alert('Select Bank Name');
                document.getElementById("<%=ddbank.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Bank Details</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home</a></li>
            <li><a href="#">Bank Details</a></li>
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
            <div class="row">
                <div class="col-md-12">
                    
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Bank Details</h3>
                        </div>
                        <div class="box-body">
                            <!-- Hidden fields required by the existing code-behind (bank update also saves profile details). -->
                            <asp:HiddenField ID="hdstatus" runat="server" />
                            <asp:TextBox ID="txtsponserid" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtsponsername" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtname" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtmobile" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtemail" runat="server" style="display:none;" />
                            <asp:DropDownList ID="ddgender" runat="server" style="display:none;">
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" style="display:none;" />
                            <asp:DropDownList ID="ddcountry" runat="server" style="display:none;" AutoPostBack="true" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged" />
                            <asp:DropDownList ID="ddstate" runat="server" style="display:none;" AutoPostBack="true" OnSelectedIndexChanged="ddstate_SelectedIndexChanged" />
                            <asp:DropDownList ID="ddcity" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtareaname" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtpincode" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtdateofbirth" runat="server" style="display:none;" CssClass="form_date" />
                            <asp:TextBox ID="txtnomineename" runat="server" style="display:none;" />
                            <asp:TextBox ID="txtnomineerelation" runat="server" style="display:none;" />

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>A/c Holder Name</label>
                                        <asp:TextBox ID="txtaccountholdername" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>A/c No</label>
                                        <asp:TextBox ID="txtaccountno" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>IFSC Code</label>
                                        <asp:TextBox ID="txtifsccode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>PAN number</label>
                                        <asp:TextBox ID="txtpan" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Bank</label>
                                        <asp:DropDownList ID="ddbank" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Branch</label>
                                        <asp:TextBox ID="txtbranchname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-body" style="border-top: 1px solid #e8edf3; padding-top: 20px;">
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group profile-upload-field">
                                        <label><i class="fa fa-file-image"></i> Passbook / Cancel Cheque</label>
                                        <div class="profile-upload-zone profile-upload-zone-compact" id="passbookZone">
                                            <div class="profile-upload-zone-inner">
                                                <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                <p class="profile-upload-title">Upload passbook image</p>
                                                <p class="profile-upload-hint">or <span class="profile-upload-browse">browse</span></p>
                                                <p class="profile-upload-hint">Allowed: JPG, PNG, PDF</p>
                                            </div>
                                            <asp:FileUpload ID="fuPassbook" runat="server" CssClass="profile-upload-input" />
                                        </div>
                                        <div class="profile-upload-selection" id="passbookSelection" hidden>
                                            <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                <img id="passbookPreview" src="" alt="Passbook preview" />
                                            </div>
                                            <div class="profile-upload-selection-info">
                                                <span class="profile-upload-filechip" id="passbookFilechip"></span>
                                                <button type="button" class="profile-upload-clear" id="passbookClear">
                                                    <i class="fa fa-times"></i> Remove
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><i class="fa fa-check-circle"></i> Approval Status</label>
                                                <div class="profile-kyc-status-wrap">
                                                    <asp:Label ID="lblPassbookApprovalStatus" runat="server" Text="-" CssClass="profile-kyc-badge"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group" style="margin-top: 22px;">
                                                <div id="div_passbook_update" runat="server" visible="false">
                                                    <asp:Button ID="btnPassbookSubmit" runat="server" Text="Upload Passbook"
                                                        CssClass="btn btn-success btn-block" OnClick="btnPassbookSubmit_Click"
                                                        CausesValidation="false" />
                                                </div>

                                                <div id="div_passbook_noupdate" runat="server" visible="false">
                                                    <div class="profile-alert"><i class="fa fa-exclamation-circle"></i> You cannot upload passbook. Please contact admin.</div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="profile-doc-preview" style="margin-top: 4px;">
                                        <p class="profile-photo-preview-label">Current Passbook</p>
                                        <div class="profile-upload-selection-preview profile-upload-selection-preview-doc" style="min-height: 180px;">
                                            <asp:Image ID="imgPassbook" runat="server" Visible="false"
                                                Style="max-width: 100%; max-height: 160px; object-fit: contain; border-radius: 10px;" />
                                            <asp:Label ID="lblPassbookPreviewText" runat="server" Text="No passbook uploaded yet." CssClass="text-muted" />
                                        </div>
                                        <span class="profile-doc-preview-hint">Latest uploaded file preview</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer">
                            <div class="box-footer" id="div_update" runat="server" visible="false">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                                </div>
                            <div class="box-footer" id="div_noupdate" runat="server" visible="false"><span style="float:right;font-size:20px;color:red;"><i>You cannot update bank details.Please contact admin.</i></span></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPassbookSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        $('.form_date').datepicker({
            format: 'dd/mm/yyyy',
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
    </script>
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
    </script>

    <script type="text/javascript">
        (function () {
            var previewUrl = null;

            function clearSelection(uploadInput, selection, zone, preview, filechip) {
                if (previewUrl) {
                    URL.revokeObjectURL(previewUrl);
                    previewUrl = null;
                }

                if (uploadInput) {
                    uploadInput.value = '';
                }

                if (preview) {
                    preview.removeAttribute('src');
                    preview.style.display = 'none';
                }

                if (filechip) {
                    filechip.textContent = '';
                }

                if (selection) {
                    selection.hidden = true;
                }

                if (zone) {
                    zone.classList.remove('is-hidden');
                }
            }

            function initPassbookUpload() {
                var uploadInput = document.getElementById("<%=fuPassbook.ClientID%>");
                var zone = document.getElementById("passbookZone");
                var selection = document.getElementById("passbookSelection");
                var preview = document.getElementById("passbookPreview");
                var filechip = document.getElementById("passbookFilechip");
                var clearBtn = document.getElementById("passbookClear");

                if (!uploadInput || !zone || !selection || !preview || !filechip || !clearBtn) return;
                if (uploadInput.getAttribute('data-passbook-bound') === '1') return;
                uploadInput.setAttribute('data-passbook-bound', '1');

                clearBtn.addEventListener('click', function () {
                    clearSelection(uploadInput, selection, zone, preview, filechip);
                });

                uploadInput.addEventListener('change', function () {
                    var file = uploadInput.files && uploadInput.files[0];
                    if (!file) {
                        clearSelection(uploadInput, selection, zone, preview, filechip);
                        return;
                    }

                    if (previewUrl) {
                        URL.revokeObjectURL(previewUrl);
                        previewUrl = null;
                    }

                    filechip.textContent = file.name;
                    selection.hidden = false;
                    zone.classList.add('is-hidden');

                    if (file.type && file.type.indexOf('image/') === 0) {
                        previewUrl = URL.createObjectURL(file);
                        preview.src = previewUrl;
                        preview.style.display = 'block';
                    } else {
                        preview.removeAttribute('src');
                        preview.style.display = 'none';
                    }
                });
            }

            Sys.Application.add_load(initPassbookUpload);
        })();
    </script>
</asp:Content>


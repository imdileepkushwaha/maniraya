<%@ Page Title="PAN Card Upload" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="PanCardImage.aspx.cs" Inherits="franchisee_PanCardImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/franchisee-kyc-upload.css?v=1" />
    <script type="text/javascript">
        function validate() {
            var fileInput = document.getElementById("<%=ImageUpload.ClientID%>");
            var panNo = document.getElementById("<%=txtPanNumber.ClientID%>");
            if (!fileInput || !fileInput.value) {
                alert('Please select PAN card image.');
                if (fileInput) { fileInput.focus(); }
                return false;
            }
            if (!panNo || !panNo.value) {
                alert('Please enter PAN number.');
                if (panNo) { panNo.focus(); }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <div class="content-header">
        <h1>PAN Card Upload</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">KYC</a></li>
            <li class="active">PAN Card</li>
        </ol>
    </div>
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
            <div class="row fr-kyc-page">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-id-card"></i> PAN Card Document</h3>
                        </div>
                        <div class="box-body">
                            <div class="fr-kyc-layout">
                                <div class="fr-kyc-preview-col">
                                    <div class="fr-kyc-preview-card">
                                        <p class="fr-kyc-preview-title">Uploaded PAN</p>
                                        <div class="fr-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageShow" runat="server" CssClass="fr-kyc-doc-thumb img-responsive" OnClick="ImageShow_Click" />
                                        </div>
                                        <p class="fr-kyc-zoom-hint"><i class="fa fa-search-plus"></i> Click to view full size</p>
                                    </div>
                                </div>
                                <div class="fr-kyc-form-col">
                                    <div class="fr-kyc-info-grid">
                                        <div class="fr-kyc-field">
                                            <label>User Id</label>
                                            <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="fr-kyc-field">
                                            <label>User Name</label>
                                            <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div id="divStatus" runat="server" visible="false" class="fr-kyc-status-row">
                                        <label class="fr-kyc-field" style="display:block;margin-bottom:8px;">Approval Status</label>
                                        <asp:Label ID="lblApprovalStatus" runat="server" CssClass="fr-kyc-status"></asp:Label>
                                    </div>
                                    <div class="fr-kyc-field" style="margin-bottom:16px;">
                                        <label>PAN Number</label>
                                        <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control" placeholder="e.g. ABCDE1234F" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="fr-kyc-upload-zone">
                                        <i class="fa fa-cloud-upload"></i>
                                        <p>Upload PAN card image</p>
                                        <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" />
                                        <small>Clear scan or photo of PAN card (JPG, PNG, PDF).</small>
                                    </div>
                                    <div class="fr-kyc-tips">
                                        <strong>Tip:</strong> Ensure name and PAN number are clearly visible. Blurry or cropped images may be rejected.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit PAN" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPhotolarge" class="modal fade fr-kyc-modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><i class="fa fa-id-card"></i> PAN Card Preview</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Image ID="ImageLarge" runat="server" CssClass="img-responsive" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    </script>
</asp:Content>

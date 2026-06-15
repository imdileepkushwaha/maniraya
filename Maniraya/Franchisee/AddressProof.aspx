<%@ Page Title="Address Proof / Aadhar" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="AddressProof.aspx.cs" Inherits="franchisee_AddressProof" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/franchisee-kyc-upload.css?v=1" />
    <script type="text/javascript">
        function validate() {
            var front = document.getElementById("<%=ImageUpload.ClientID%>");
            var back = document.getElementById("<%=ImageUpload2.ClientID%>");
            var aadhar = document.getElementById("<%=txtAdharnumber.ClientID%>");
            if (!front || !front.value) {
                alert('Please upload Aadhar front side.');
                if (front) { front.focus(); }
                return false;
            }
            if (!back || !back.value) {
                alert('Please upload Aadhar back side.');
                if (back) { back.focus(); }
                return false;
            }
            if (!aadhar || !aadhar.value) {
                alert('Please enter Aadhar number.');
                if (aadhar) { aadhar.focus(); }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <div class="content-header">
        <h1>Address Proof / Aadhar</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">KYC</a></li>
            <li class="active">Address Proof / Aadhar</li>
        </ol>
    </div>
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
            <div class="row fr-kyc-page">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-address-card-o"></i> Aadhar / Address Proof</h3>
                        </div>
                        <div class="box-body">
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
                                <label style="display:block;margin-bottom:8px;font-size:13px;font-weight:600;color:#64748b;">Approval Status</label>
                                <asp:Label ID="lblApprovalStatus" runat="server" CssClass="fr-kyc-status"></asp:Label>
                            </div>

                            <div class="fr-kyc-dual-preview">
                                <div class="fr-kyc-preview-card">
                                    <p class="fr-kyc-preview-title">Front Side</p>
                                    <div class="fr-kyc-preview-wrap">
                                        <asp:ImageButton ID="ImageShow" runat="server" CssClass="fr-kyc-doc-thumb img-responsive" OnClick="ImageShow_Click" />
                                    </div>
                                    <p class="fr-kyc-zoom-hint"><i class="fa fa-search-plus"></i> Click to enlarge</p>
                                </div>
                                <div class="fr-kyc-preview-card">
                                    <p class="fr-kyc-preview-title">Back Side</p>
                                    <div class="fr-kyc-preview-wrap">
                                        <asp:ImageButton ID="ImageShow2" runat="server" CssClass="fr-kyc-doc-thumb img-responsive" OnClick="ImageShow2_Click" />
                                    </div>
                                    <p class="fr-kyc-zoom-hint"><i class="fa fa-search-plus"></i> Click to enlarge</p>
                                </div>
                            </div>

                            <div class="fr-kyc-field" style="margin-bottom:16px;">
                                <label>Aadhar Number</label>
                                <asp:TextBox ID="txtAdharnumber" runat="server" CssClass="form-control" placeholder="12-digit Aadhar number" MaxLength="12"></asp:TextBox>
                            </div>

                            <div class="fr-kyc-upload-grid">
                                <div class="fr-kyc-upload-zone">
                                    <i class="fa fa-file-image-o"></i>
                                    <p>Aadhar Front</p>
                                    <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" />
                                    <small>Front side with photo</small>
                                </div>
                                <div class="fr-kyc-upload-zone">
                                    <i class="fa fa-file-image-o"></i>
                                    <p>Aadhar Back</p>
                                    <asp:FileUpload ID="ImageUpload2" runat="server" CssClass="form-control" />
                                    <small>Back side with address</small>
                                </div>
                            </div>

                            <div class="fr-kyc-tips">
                                <strong>Note:</strong> Upload both sides clearly. Aadhar number must match the document. Admin will verify before approval.
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit Documents" OnClick="btnSubmit_Click" />
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
                            <h4 class="modal-title"><i class="fa fa-address-card-o"></i> Document Preview</h4>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>

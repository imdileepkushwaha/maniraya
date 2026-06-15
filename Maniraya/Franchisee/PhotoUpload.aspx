<%@ Page Title="Photo Upload" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="PhotoUpload.aspx.cs" Inherits="franchisee_PhotoUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/franchisee-photo-upload.css?v=1" />
    <script type="text/javascript">
        function validate() {
            var fileInput = document.getElementById("<%=ImageUpload.ClientID%>");
            if (!fileInput || !fileInput.value) {
                alert('Please select a photo to upload.');
                if (fileInput) { fileInput.focus(); }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <div class="content-header">
        <h1>Photo Upload</h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="UserProfile.aspx">My Profile</a></li>
        <li class="active">Photo Upload</li>
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
            <div class="row fr-photo-page">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-camera"></i> Update Profile Photo</h3>
                        </div>
                        <div class="box-body">
                            <div class="fr-photo-layout">
                                <div class="fr-photo-preview-col">
                                    <div class="fr-photo-preview-card">
                                        <p class="fr-photo-preview-title">Current Photo</p>
                                        <div class="fr-photo-preview-wrap">
                                            <asp:ImageButton ID="ImageShow" runat="server" CssClass="fr-photo-thumb img-responsive" OnClick="ImageShow_Click" />
                                        </div>
                                        <p class="fr-photo-zoom-hint"><i class="fa fa-search-plus"></i> Click photo to view full size</p>
                                    </div>
                                </div>
                                <div class="fr-photo-form-col">
                                    <div class="fr-photo-info-grid">
                                        <div class="fr-photo-field">
                                            <label>User Id</label>
                                            <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                                        </div>
                                        <div class="fr-photo-field">
                                            <label>User Name</label>
                                            <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="fr-photo-upload-zone">
                                        <i class="fa fa-cloud-upload"></i>
                                        <p>Choose a new profile photo</p>
                                        <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" />
                                        <small>JPG, JPEG, or PNG recommended. Max clear face photo works best.</small>
                                    </div>
                                    <div class="fr-photo-tips">
                                        <strong>Note:</strong> Upload a recent passport-size photo with a plain background. After approval, it will appear on your dashboard and profile.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Upload Photo" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPhotolarge" class="modal fade fr-photo-modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><i class="fa fa-picture-o"></i> Profile Photo Preview</h4>
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
        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="UploadKYC.aspx.cs" Inherits="UploadKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .admin-kyc-upload-page .admin-kyc-doc-card {
            border: 1px solid #e2e8f0;
            border-radius: 12px;
            background: #fff;
            padding: 18px 18px 14px;
            margin-bottom: 16px;
            box-shadow: 0 2px 10px rgba(15, 23, 42, 0.04);
        }

        .admin-kyc-upload-page .admin-kyc-doc-title {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            margin: 0 0 14px;
            padding-bottom: 10px;
            border-bottom: 1px solid #eef2f7;
            font-size: 15px;
            font-weight: 700;
            color: #0f172a;
        }

        .admin-kyc-upload-page .admin-kyc-doc-title i {
            margin-right: 8px;
            color: #e52d27;
        }

        .admin-kyc-upload-page .admin-kyc-status {
            display: inline-block;
            min-width: 78px;
            text-align: center;
            padding: 4px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.03em;
            text-transform: uppercase;
        }

        .admin-kyc-upload-page .admin-kyc-status.Pending,
        .admin-kyc-upload-page .Pending {
            background: linear-gradient(135deg, #0284c7 0%, #0369a1 100%);
            color: #fff !important;
        }

        .admin-kyc-upload-page .admin-kyc-status.Approved,
        .admin-kyc-upload-page .Approved {
            background: linear-gradient(135deg, #15803d 0%, #166534 100%);
            color: #fff !important;
        }

        .admin-kyc-upload-page .admin-kyc-status.Rejected,
        .admin-kyc-upload-page .Rejected {
            background: linear-gradient(135deg, #dc2626 0%, #b91c1c 100%);
            color: #fff !important;
        }

        .admin-kyc-upload-page .admin-kyc-preview-wrap {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 120px;
            padding: 10px;
            border: 1px dashed #cbd5e1;
            border-radius: 12px;
            background: #f8fafc;
        }

        .admin-kyc-upload-page .admin-kyc-preview-wrap .img {
            width: 110px !important;
            height: 110px !important;
            object-fit: cover;
            border-radius: 10px;
            border: 1px solid #e2e8f0;
            background: #fff;
            cursor: zoom-in;
            box-shadow: 0 2px 8px rgba(15, 23, 42, 0.06);
        }

        .admin-kyc-upload-page .admin-kyc-actions-row {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 10px;
            margin-top: 8px;
        }

        .admin-kyc-upload-page .admin-kyc-file input[type="file"] {
            display: block;
            width: 100%;
            padding: 8px 10px;
            border: 1px solid #dfe3ea;
            border-radius: 10px;
            background: #fff;
        }

        .admin-kyc-upload-page .admin-kyc-hint {
            margin-top: 6px;
            font-size: 12px;
            color: #94a3b8;
        }
    </style>
    <script type="text/javascript">
        function validate() { return true; }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>KYC Upload</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="kycApprovalForUser.aspx">KYC</a></li>
            <li class="active">Upload KYC</li>
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
            <div class="row admin-kyc-upload-page">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Upload / Update KYC Documents</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Review and upload signup form, PAN, cancelled cheque/passbook, and Aadhaar for the selected user.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-user"></i> User Details</h5>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>User ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtchange" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>User Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Signup Form -->
                            <div class="admin-kyc-doc-card">
                                <div class="admin-kyc-doc-title">
                                    <span><i class="fa fa-file-text-o"></i> Signup Form</span>
                                    <div id="divStatus" runat="server" visible="false">
                                        <asp:Label ID="lblApprovalStatus" runat="server" CssClass="admin-kyc-status" Text="Pending"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group admin-kyc-file">
                                            <label>Upload Signup Form</label>
                                            <asp:FileUpload ID="ImageUpload" runat="server" CssClass="ImageUpload" accept="image/*" />
                                            <div class="admin-kyc-hint">JPG / PNG recommended. Click preview to enlarge.</div>
                                        </div>
                                        <div class="admin-kyc-actions-row" id="div_update" runat="server">
                                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit Signup Form" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-7">
                                        <div class="admin-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageShow" runat="server" Width="110px" Height="110px" CssClass="img IMageForm" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- PAN Card -->
                            <div class="admin-kyc-doc-card">
                                <div class="admin-kyc-doc-title">
                                    <span><i class="fa fa-credit-card"></i> PAN Card</span>
                                    <div id="divpanstatus" runat="server" visible="false">
                                        <asp:Label ID="lblpanstatus" runat="server" CssClass="admin-kyc-status" Text="Pending"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>PAN Number</label>
                                            <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control" placeholder="Enter PAN number"></asp:TextBox>
                                        </div>
                                        <div class="form-group admin-kyc-file">
                                            <label>Upload PAN Image</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ImageUploadPan" accept="image/*" />
                                        </div>
                                        <div class="admin-kyc-actions-row" id="div1" runat="server">
                                            <asp:Button ID="Button1" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit PAN" OnClick="btnSubmitPan_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-7">
                                        <div class="admin-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="110px" Height="110px" CssClass="img IMageFormPan" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Cancel Cheque / Passbook -->
                            <div class="admin-kyc-doc-card">
                                <div class="admin-kyc-doc-title">
                                    <span><i class="fa fa-university"></i> Cancel Cheque / Passbook</span>
                                    <div id="divchequestatus" runat="server" visible="false">
                                        <asp:Label ID="lblchequestatus" runat="server" CssClass="admin-kyc-status" Text="Pending"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group admin-kyc-file">
                                            <label>Upload Cancel Cheque / Passbook</label>
                                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="ImageUploadCheque" accept="image/*,application/pdf" />
                                            <div class="admin-kyc-hint">Supports JPG, PNG, PDF.</div>
                                        </div>
                                        <div class="admin-kyc-actions-row" id="div4" runat="server">
                                            <asp:Button ID="Button2" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit Passbook" OnClick="btnSubmitCheque_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-7">
                                        <div class="admin-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageButton2" runat="server" Width="110px" Height="110px" CssClass="img IMageFormCheque" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Aadhaar -->
                            <div class="admin-kyc-doc-card">
                                <div class="admin-kyc-doc-title">
                                    <span><i class="fa fa-id-card-o"></i> Address Proof / Aadhaar</span>
                                    <div id="divaadharstatus" runat="server" visible="false">
                                        <asp:Label ID="lblaadharstatus" runat="server" CssClass="admin-kyc-status" Text="Pending"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Aadhaar Number</label>
                                            <asp:TextBox ID="txtaadharno" runat="server" CssClass="form-control" placeholder="Enter Aadhaar number" MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group admin-kyc-file">
                                            <label>Front Side</label>
                                            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="ImageUploadAadhar" accept="image/*" />
                                        </div>
                                        <div class="admin-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageButton3" runat="server" Width="110px" Height="110px" CssClass="img IMageFormAadhar" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group admin-kyc-file">
                                            <label>Back Side</label>
                                            <asp:FileUpload ID="FileUpload4" runat="server" CssClass="ImageUploadAadharBack" accept="image/*" />
                                        </div>
                                        <div class="admin-kyc-preview-wrap">
                                            <asp:ImageButton ID="ImageButton4" runat="server" Width="110px" Height="110px" CssClass="img IMageFormAadharBack" />
                                        </div>
                                    </div>
                                </div>
                                <div class="admin-kyc-actions-row" id="div3" runat="server" style="margin-top: 14px;">
                                    <asp:Button ID="Button3" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit Aadhaar" OnClick="btnSubmitAadhar_Click" />
                                </div>
                            </div>

                            <div class="box-footer" style="padding-left: 0; padding-right: 0;">
                                <a href="kycApprovalForUser.aspx" class="btn btn-default"><i class="fa fa-arrow-left"></i> Back to KYC Approval</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPhotolarge" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body admin-kyc-preview-body">
                            <img id="img1" runat="server" class="img1 admin-kyc-preview-img" src="" alt="KYC preview" />
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
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button2" />
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function bindKycUploadPreview() {
            $(".ImageUpload").off("change.kyc").on("change.kyc", function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(".IMageForm").attr("src", e.target.result);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            });

            $(".ImageUploadPan").off("change.kyc").on("change.kyc", function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(".IMageFormPan").attr("src", e.target.result);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            });

            $(".ImageUploadCheque").off("change.kyc").on("change.kyc", function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(".IMageFormCheque").attr("src", e.target.result);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            });

            $(".ImageUploadAadhar").off("change.kyc").on("change.kyc", function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(".IMageFormAadhar").attr("src", e.target.result);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            });

            $(".ImageUploadAadharBack").off("change.kyc").on("change.kyc", function () {
                var input = this;
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $(".IMageFormAadharBack").attr("src", e.target.result);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            });

            $(".img").off("click.kyc").on("click.kyc", function () {
                $(".img1").attr("src", $(this).attr("src"));
                showModal1();
                return false;
            });
        }

        $(document).ready(function () {
            bindKycUploadPreview();
        });

        if (typeof Sys !== "undefined" && Sys.Application) {
            Sys.Application.add_load(bindKycUploadPreview);
        }

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

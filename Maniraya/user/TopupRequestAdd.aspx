
<%@ Page Title="Topup Request" Language="C#" MasterPageFile="Masterpage.master" AutoEventWireup="true" CodeFile="TopupRequestAdd.aspx.cs" Inherits="user_TopupRequestAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=11" rel="stylesheet" />
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=RDBTNAdmin.ClientID%>").checked == true) {
                if (document.getElementById("<%=ddbankaccountno.ClientID%>").value == "0") {
                    alert('Select Account');
                    document.getElementById("<%=ddbankaccountno.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=ddmode.ClientID%>").value == "Select") {
                    alert('Select Paymentmode');
                    document.getElementById("<%=ddmode.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=ddplan.ClientID%>").value == "0") {
                    alert('Select plan');
                    document.getElementById("<%=ddplan.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=TxtTransactionId.ClientID%>").value == "") {
                    alert('Enter TransactionID');
                    document.getElementById("<%=TxtTransactionId.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=RDBtnFranchisee.ClientID%>").checked == true) {
                if (document.getElementById("<%=TxtFranchiseeUserId.ClientID%>").value == "") {
                    alert('Enter franchisee ID');
                    document.getElementById("<%=TxtFranchiseeUserId.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtamount.ClientID%>").value == "") {
                alert('Enter Amount');
                document.getElementById("<%=txtamount.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Topup Request</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="TopupRequestAdd.aspx">Topup</a></li>
            <li class="active">New Request</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="Loading" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page topup-request-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-credit-card"></i></div>
                    <div class="profile-hero-info">
                        <h2>Submit Topup Request</h2>
                        <p class="profile-hero-meta">Select your plan, transfer the amount, and share payment details for quick activation.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="TopupDetail.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-list"></i> My Requests</a>
                        <a href="Dashboard.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-home"></i> Dashboard</a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-4">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-paper-plane"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Request Details</h3>
                                <p class="box-subtitle">Fill in the plan and payment information below</p>
                            </div>
                        </div>
                    </div>

                    <div class="box-body profile-form-grid">
                        <div class="row" style="display:none">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RDBtnTRecharge" runat="server" Text="Main Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnTRecharge_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RdBtnUtility" runat="server" Text="Cash Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnUtility_CheckedChanged" />
                                </div>
                            </div>
                        </div>

                        <div class="topup-request-to-section">
                            <p class="profile-subsection-title"><i class="fa fa-user-circle"></i> Request To</p>
                            <p class="topup-request-to-hint">Select who should receive and verify your topup payment.</p>
                            <div class="topup-choice-grid">
                                <div class="topup-choice-card topup-radio-admin" data-topup-choice="admin">
                                    <span class="topup-choice-icon" aria-hidden="true"><i class="fa fa-university"></i></span>
                                    <div class="topup-choice-body">
                                        <asp:RadioButton ID="RDBTNAdmin" runat="server" Text="Admin" GroupName="B" AutoPostBack="true" OnCheckedChanged="RDBTNAdmin_CheckedChanged" />
                                        <p class="topup-choice-desc">Pay to company bank account with QR code</p>
                                    </div>
                                    <span class="topup-choice-tick" aria-hidden="true"><i class="fa fa-check"></i></span>
                                </div>
                                <div class="topup-choice-card topup-radio-franchisee" data-topup-choice="franchisee">
                                    <span class="topup-choice-icon" aria-hidden="true"><i class="fa fa-building"></i></span>
                                    <div class="topup-choice-body">
                                        <asp:RadioButton ID="RDBtnFranchisee" runat="server" Text="Franchisee" GroupName="B" AutoPostBack="true" OnCheckedChanged="RDBtnFranchisee_CheckedChanged" />
                                        <p class="topup-choice-desc">Send request to your local franchisee partner</p>
                                    </div>
                                    <span class="topup-choice-tick" aria-hidden="true"><i class="fa fa-check"></i></span>
                                </div>
                            </div>
                        </div>

                        <asp:Panel runat="server" Visible="false" ID="Pnlfranchisee" CssClass="topup-panel-block">
                            <p class="profile-subsection-title"><i class="fa fa-building"></i> Franchisee Details</p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= TxtFranchiseeUserId.ClientID %>"><i class="fa fa-id-badge"></i> Franchisee ID</label>
                                        <asp:TextBox ID="TxtFranchiseeUserId" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFranchiseeUserId_TextChanged" placeholder="Enter franchisee ID" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= TxtFranchiseename.ClientID %>"><i class="fa fa-user"></i> Franchisee Name</label>
                                        <asp:TextBox ID="TxtFranchiseename" Enabled="false" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="row" style="display:none">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>User Id</label>
                                    <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>User Name</label>
                                    <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <p class="profile-subsection-title"><i class="fa fa-tags"></i> Plan &amp; Amount</p>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= ddplan.ClientID %>"><i class="fa fa-cube"></i> Select Plan</label>
                                    <asp:DropDownList ID="ddplan" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddplan_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Plan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="<%= txtamount.ClientID %>"><i class="fa fa-rupee-sign"></i> Amount</label>
                                    <asp:TextBox ID="txtamount" ReadOnly="true" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" placeholder="Select a plan" />
                                    <asp:Label ID="lblmssg" Visible="false" runat="server" CssClass="topup-amount-hint"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <asp:Panel runat="server" Visible="false" ID="Pnladmin" CssClass="topup-panel-block">
                            <p class="profile-subsection-title"><i class="fa fa-university"></i> Bank Account</p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= ddbankaccountno.ClientID %>"><i class="fa fa-university"></i> Select Account</label>
                                        <asp:DropDownList ID="ddbankaccountno" AutoPostBack="true" OnSelectedIndexChanged="ddbankaccountno_SelectedIndexChanged" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Account</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtdepositaccountno.ClientID %>"><i class="fa fa-hashtag"></i> Account Number</label>
                                        <asp:TextBox ID="txtdepositaccountno" Enabled="false" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtdepositbank.ClientID %>"><i class="fa fa-building-o"></i> Bank Name</label>
                                        <asp:TextBox ID="txtdepositbank" Enabled="false" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtifsccode.ClientID %>"><i class="fa fa-code"></i> IFSC Code</label>
                                        <asp:TextBox ID="txtifsccode" Enabled="false" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="row topup-bank-detail-row">
                                <div class="col-md-6">
                                    <div class="topup-qr-card">
                                        <p class="topup-qr-label"><i class="fa fa-qrcode"></i> Scan to Pay</p>
                                        <asp:Image ID="QR" runat="server" CssClass="topup-qr-image" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= txtaccountholdername.ClientID %>"><i class="fa fa-user"></i> Account Holder Name</label>
                                        <asp:TextBox ID="txtaccountholdername" Enabled="false" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>

                            <p class="profile-subsection-title"><i class="fa fa-check-square"></i> Payment Proof</p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= TxtTransactionId.ClientID %>"><i class="fa fa-exchange-alt"></i> Transaction ID</label>
                                        <asp:TextBox ID="TxtTransactionId" runat="server" CssClass="form-control" placeholder="Enter UTR / transaction reference" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="<%= TxtNarration.ClientID %>"><i class="fa fa-comment"></i> Narration</label>
                                        <asp:TextBox ID="TxtNarration" runat="server" CssClass="form-control" placeholder="Optional note" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group profile-upload-field topup-payment-upload">
                                        <label><i class="fa fa-camera"></i> Payment Screenshot</label>
                                        <div class="profile-upload-zone profile-upload-zone-attach profile-upload-zone-compact topup-payment-upload-zone" id="topupPaymentUploadZone">
                                            <div class="profile-upload-zone-inner">
                                                <span class="profile-upload-icon" aria-hidden="true"><i class="fa fa-cloud-upload-alt"></i></span>
                                                <p class="profile-upload-title">Drop payment screenshot here</p>
                                                <p class="profile-upload-hint">or <span class="profile-upload-browse">browse from gallery</span></p>
                                                <p class="profile-upload-meta">JPG, PNG, WEBP � UTR / receipt clearly visible</p>
                                            </div>
                                            <asp:FileUpload ID="ImageUpload" runat="server" CssClass="profile-upload-input" accept="image/jpeg,image/png,image/webp,image/gif" />
                                        </div>
                                        <div class="profile-upload-selection profile-upload-selection-attach" id="topupPaymentUploadSelection" hidden>
                                            <div class="profile-upload-selection-preview profile-upload-selection-preview-doc">
                                                <img id="topupPaymentUploadPreview" src="" alt="Payment screenshot preview" />
                                            </div>
                                            <div class="profile-upload-selection-info">
                                                <span class="profile-upload-filechip" id="topupPaymentUploadFilechip"></span>
                                                <button type="button" class="profile-upload-clear" id="topupPaymentUploadClear">
                                                    <i class="fa fa-times"></i> Remove
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6" style="display:none">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txt" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="col-md-6" style="display:none;">
                            <div class="form-group">
                                <asp:TextBox ID="txtbranchname" Enabled="false" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="row" style="display:none;">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Deposit Mode</label>
                                    <asp:DropDownList ID="ddmode" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="IMPS">THIRD PARTY TRANSFER</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="display:none;">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Account Balance</label>
                                    <asp:TextBox ID="txtbalance" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="topup-info-note">
                            <i class="fa fa-info-circle" aria-hidden="true"></i>
                            <span>After payment, submit the transaction ID and upload a screenshot so your request can be verified quickly.</span>
                        </div>
                    </div>

                    <div class="box-footer profile-password-actions">
                        <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary profile-btn-primary-action" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-default profile-btn-secondary-action" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
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
        function CopyToClipboard() {
            var copyText = document.getElementById('<%=txtaccountholdername.ClientID%>');
            copyText.select();
            document.execCommand("Copy");
            alert("Copied the text: " + copyText.value);
        }

        (function () {
            var previewUrl = null;

            function formatFileSize(bytes) {
                if (!bytes) return '';
                if (bytes < 1024) return bytes + ' B';
                if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
                return (bytes / 1048576).toFixed(1) + ' MB';
            }

            function clearPaymentUpload(input, zone, selection, preview, filechip) {
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

            function showPaymentUpload(file, zone, selection, preview, filechip) {
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

            function initTopupChoiceCards() {
                var cards = document.querySelectorAll('.topup-choice-card');
                cards.forEach(function (card) {
                    if (card.getAttribute('data-bound') === '1') {
                        return;
                    }
                    card.setAttribute('data-bound', '1');
                    card.addEventListener('click', function (e) {
                        if (e.target.tagName === 'INPUT' || e.target.tagName === 'LABEL') {
                            return;
                        }
                        var radio = card.querySelector('input[type="radio"]');
                        if (radio && !radio.checked) {
                            radio.click();
                        }
                    });
                });
            }

            function initTopupPaymentUpload() {
                var input = document.getElementById('<%= ImageUpload.ClientID %>');
                var zone = document.getElementById('topupPaymentUploadZone');
                var selection = document.getElementById('topupPaymentUploadSelection');
                var preview = document.getElementById('topupPaymentUploadPreview');
                var filechip = document.getElementById('topupPaymentUploadFilechip');
                var clearBtn = document.getElementById('topupPaymentUploadClear');

                if (!input || !zone || !selection || zone.getAttribute('data-bound') === '1') {
                    return;
                }

                zone.setAttribute('data-bound', '1');

                input.addEventListener('change', function () {
                    if (!input.files || !input.files.length) {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                        return;
                    }
                    if (!showPaymentUpload(input.files[0], zone, selection, preview, filechip)) {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    }
                });

                clearBtn.addEventListener('click', function () {
                    clearPaymentUpload(input, zone, selection, preview, filechip);
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
                    if (!showPaymentUpload(files[0], zone, selection, preview, filechip)) {
                        clearPaymentUpload(input, zone, selection, preview, filechip);
                    }
                });
            }

            function initTopupPageUi() {
                initTopupChoiceCards();
                initTopupPaymentUpload();
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', initTopupPageUi);
            } else {
                initTopupPageUi();
            }

            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initTopupPageUi);
            }
        })();
    </script>
</asp:Content>

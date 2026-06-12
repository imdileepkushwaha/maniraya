<%@ Page Title="Add Franchisee" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="franchiseeAdd.aspx.cs" Inherits="franchiseeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validateStep(step) {
            if (step === 1) {
                if (document.getElementById("<%=DDLstFranchiseeType.ClientID%>").value === "0") {
                    alert('Select Franchisee Type');
                    document.getElementById("<%=DDLstFranchiseeType.ClientID%>").focus();
                    return false;
                }
                return true;
            }
            if (step === 2) {
                if (document.getElementById("<%=txtname.ClientID%>").value.trim() === "") {
                    alert('Enter Name');
                    document.getElementById("<%=txtname.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtmobile.ClientID%>").value.trim() === "") {
                    alert('Enter Mobile');
                    document.getElementById("<%=txtmobile.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtemail.ClientID%>").value.trim() === "") {
                    alert('Enter Email');
                    document.getElementById("<%=txtemail.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtOutletName.ClientID%>").value.trim() === "") {
                    alert('Enter Outlet Name');
                    document.getElementById("<%=txtOutletName.ClientID%>").focus();
                    return false;
                }
                return true;
            }
            if (step === 3) {
                return true;
            }
            if (step === 4) {
                if (document.getElementById("<%=txtaddress.ClientID%>").value.trim() === "") {
                    alert('Enter Address');
                    document.getElementById("<%=txtaddress.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=ddcountry.ClientID%>").value === "0") {
                    alert('Select Country');
                    document.getElementById("<%=ddcountry.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=ddstate.ClientID%>").value === "0") {
                    alert('Select State');
                    document.getElementById("<%=ddstate.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=ddcity.ClientID%>").value === "0") {
                    alert('Select City');
                    document.getElementById("<%=ddcity.ClientID%>").focus();
                    return false;
                }
                return true;
            }
            if (step === 5) {
                if (document.getElementById("<%=txtuserpassword.ClientID%>").value === "") {
                    alert('Enter Password');
                    document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value === "") {
                    alert('Enter Confirm Password');
                    document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtuserpassword.ClientID%>").value !== document.getElementById("<%=txtconfirmpassword.ClientID%>").value) {
                    alert('Password does not match');
                    document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                    return false;
                }
                return true;
            }
            return true;
        }

        function validate() {
            for (var i = 1; i <= 5; i++) {
                if (!validateStep(i)) {
                    if (window.franchiseeWizard) {
                        franchiseeWizard.goToStep(i);
                    }
                    return false;
                }
            }
            return true;
        }

        var franchiseeWizard = {
            totalSteps: 5,
            stepFieldId: "<%= hfWizardStep.ClientID %>",

            init: function () {
                var saved = parseInt(document.getElementById(this.stepFieldId).value, 10);
                this.goToStep(isNaN(saved) || saved < 1 ? 1 : saved);
                this.bindButtons();
            },

            bindButtons: function () {
                var self = this;
                var prevBtn = document.getElementById("btnWizardPrev");
                var nextBtn = document.getElementById("btnWizardNext");
                if (prevBtn && !prevBtn._bound) {
                    prevBtn._bound = true;
                    prevBtn.addEventListener("click", function () {
                        self.goToStep(self.getStep() - 1);
                    });
                }
                if (nextBtn && !nextBtn._bound) {
                    nextBtn._bound = true;
                    nextBtn.addEventListener("click", function () {
                        if (!validateStep(self.getStep())) {
                            return;
                        }
                        self.goToStep(self.getStep() + 1);
                    });
                }

                var stepButtons = document.querySelectorAll(".admin-wizard-step");
                for (var i = 0; i < stepButtons.length; i++) {
                    if (stepButtons[i]._bound) continue;
                    stepButtons[i]._bound = true;
                    stepButtons[i].addEventListener("click", function () {
                        var target = parseInt(this.getAttribute("data-step"), 10);
                        var current = self.getStep();
                        if (target < current) {
                            self.goToStep(target);
                            return;
                        }
                        for (var s = current; s < target; s++) {
                            if (!validateStep(s)) {
                                self.goToStep(s);
                                return;
                            }
                        }
                        self.goToStep(target);
                    });
                }
            },

            getStep: function () {
                return parseInt(document.getElementById(this.stepFieldId).value, 10) || 1;
            },

            goToStep: function (step) {
                if (step < 1) step = 1;
                if (step > this.totalSteps) step = this.totalSteps;

                document.getElementById(this.stepFieldId).value = step;

                var panels = document.querySelectorAll(".admin-wizard-panel");
                for (var i = 0; i < panels.length; i++) {
                    panels[i].classList.toggle("is-active", parseInt(panels[i].getAttribute("data-step-panel"), 10) === step);
                }

                var steps = document.querySelectorAll(".admin-wizard-step");
                for (var j = 0; j < steps.length; j++) {
                    var stepNo = parseInt(steps[j].getAttribute("data-step"), 10);
                    steps[j].classList.toggle("is-active", stepNo === step);
                    steps[j].classList.toggle("is-complete", stepNo < step);
                }

                var prevBtn = document.getElementById("btnWizardPrev");
                var nextBtn = document.getElementById("btnWizardNext");
                var submitBtn = document.getElementById("<%=btnSubmit.ClientID%>");
                if (prevBtn) prevBtn.style.display = step === 1 ? "none" : "";
                if (nextBtn) nextBtn.style.display = step === this.totalSteps ? "none" : "";
                if (submitBtn) submitBtn.style.display = step === this.totalSteps ? "" : "none";
            }
        };

        function showModal1() {
            if (window.showAdminModal) {
                showAdminModal("DivPANlarge");
            }
        }

        function showModal2() {
            if (window.showAdminModal) {
                showAdminModal("DivGSTLarge");
            }
        }

        Sys.Application.add_load(function () {
            franchiseeWizard.init();
        });

        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var wizardPrm = Sys.WebForms.PageRequestManager.getInstance();
            if (wizardPrm && !wizardPrm._franchiseeWizardBound) {
                wizardPrm._franchiseeWizardBound = true;
                wizardPrm.add_endRequest(function () {
                    franchiseeWizard.init();
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Franchisee</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Franchisee</a></li>
            <li class="active">Add Franchisee</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Franchisee</h3>
                        </div>

                        <div class="box-body admin-product-form admin-franchisee-wizard-form">
                            <asp:HiddenField ID="hfWizardStep" runat="server" Value="1" />
                            <p class="admin-franchisee-wizard-intro">Complete all steps to register a new franchisee. Required fields are validated at each step.</p>

                            <div class="admin-wizard">
                                <div class="admin-wizard-steps" role="tablist" aria-label="Franchisee registration steps">
                                    <button type="button" class="admin-wizard-step is-active" data-step="1">
                                        <span class="admin-wizard-step-no">1</span>
                                        <span class="admin-wizard-step-label">Sponsor &amp; Type</span>
                                    </button>
                                    <button type="button" class="admin-wizard-step" data-step="2">
                                        <span class="admin-wizard-step-no">2</span>
                                        <span class="admin-wizard-step-label">Personal</span>
                                    </button>
                                    <button type="button" class="admin-wizard-step" data-step="3">
                                        <span class="admin-wizard-step-no">3</span>
                                        <span class="admin-wizard-step-label">Documents</span>
                                    </button>
                                    <button type="button" class="admin-wizard-step" data-step="4">
                                        <span class="admin-wizard-step-no">4</span>
                                        <span class="admin-wizard-step-label">Address</span>
                                    </button>
                                    <button type="button" class="admin-wizard-step" data-step="5">
                                        <span class="admin-wizard-step-no">5</span>
                                        <span class="admin-wizard-step-label">Password</span>
                                    </button>
                                </div>

                                <div class="admin-wizard-panels">
                                    <div class="admin-wizard-panel is-active" data-step-panel="1">
                                        <div class="admin-form-section admin-form-section-last">
                                            <h5 class="admin-form-section-title"><i class="fa fa-users"></i> Sponsor &amp; Franchisee Type</h5>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= DDLstFranchiseeType.ClientID %>">Franchisee Type</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                                            <asp:DropDownList ID="DDLstFranchiseeType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtSponsorId.ClientID %>">Sponsor ID</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                            <asp:TextBox ID="txtSponsorId" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtSponsorId_TextChanged" placeholder="Enter sponsor ID" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtSponsorName.ClientID %>">Sponsor Name</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                            <asp:TextBox ID="txtSponsorName" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto filled" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= TxtType.ClientID %>">Sponsor Type</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                            <asp:TextBox ID="TxtType" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto filled" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="admin-wizard-panel" data-step-panel="2">
                                        <div class="admin-form-section admin-form-section-last">
                                            <h5 class="admin-form-section-title"><i class="fa fa-address-card-o"></i> Personal Information</h5>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtname.ClientID %>">User Name</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-user-circle"></i></span>
                                                            <asp:TextBox ID="txtname" CssClass="form-control" runat="server" placeholder="Enter user name" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtmobile.ClientID %>">Mobile No</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-phone"></i></span>
                                                            <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="Enter mobile number" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtemail.ClientID %>">Email</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-envelope"></i></span>
                                                            <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" placeholder="Enter email address" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= ddgender.ClientID %>">Gender</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-venus-mars"></i></span>
                                                            <asp:DropDownList ID="ddgender" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtOutletName.ClientID %>">Outlet Name</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-building"></i></span>
                                                            <asp:TextBox ID="txtOutletName" runat="server" CssClass="form-control" placeholder="Enter outlet name" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div style="display:none;">
                                                <fieldset>
                                                    <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddlDay" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="admin-wizard-panel" data-step-panel="3">
                                        <div class="admin-form-section admin-form-section-last">
                                            <h5 class="admin-form-section-title"><i class="fa fa-file-image-o"></i> KYC Documents</h5>
                                            <p class="admin-section-hint">Upload PAN and GST documents. These are optional but recommended.</p>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="admin-doc-upload-card">
                                                        <h6 class="admin-doc-upload-title"><i class="fa fa-id-card"></i> PAN Details</h6>
                                                        <div class="form-group">
                                                            <label for="<%= txtPANNo.ClientID %>">PAN No</label>
                                                            <asp:TextBox ID="txtPANNo" runat="server" CssClass="form-control" placeholder="Enter PAN number" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="<%= filePAN.ClientID %>">PAN Upload</label>
                                                            <asp:FileUpload ID="filePAN" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <asp:Button ID="btnPANUPload" runat="server" Text="Upload PAN" OnClick="btnPANUPload_Click" CssClass="btn btn-default btn-sm" />
                                                        <div class="admin-doc-preview-wrap">
                                                            <span class="admin-doc-preview-label">Preview</span>
                                                            <asp:ImageButton ID="imgPAN" runat="server" CssClass="admin-doc-preview-thumb" OnClick="imgPAN_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="admin-doc-upload-card">
                                                        <h6 class="admin-doc-upload-title"><i class="fa fa-file-text-o"></i> GST Details</h6>
                                                        <div class="form-group">
                                                            <label for="<%= txtGSTNo.ClientID %>">GST No</label>
                                                            <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control" placeholder="Enter GST number" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="<%= fileGST.ClientID %>">GST Upload</label>
                                                            <asp:FileUpload ID="fileGST" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <asp:Button ID="btnGSTUpload" runat="server" Text="Upload GST" OnClick="btnGSTUpload_Click" CssClass="btn btn-default btn-sm" />
                                                        <div class="admin-doc-preview-wrap">
                                                            <span class="admin-doc-preview-label">Preview</span>
                                                            <asp:ImageButton ID="imgGST" runat="server" CssClass="admin-doc-preview-thumb" OnClick="imgGST_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="admin-wizard-panel" data-step-panel="4">
                                        <div class="admin-form-section admin-form-section-last">
                                            <h5 class="admin-form-section-title"><i class="fa fa-map-marker"></i> Communication &amp; Address</h5>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="<%= txtaddress.ClientID %>">Address</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-home"></i></span>
                                                            <asp:TextBox ID="txtaddress" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server" placeholder="Enter full address" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= ddcountry.ClientID %>">Country</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-globe"></i></span>
                                                            <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select Country</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= ddstate.ClientID %>">State</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-map"></i></span>
                                                            <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select State</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= ddcity.ClientID %>">City</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-building-o"></i></span>
                                                            <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select City</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <asp:Panel ID="otherPnl" runat="server" Visible="false">
                                                        <div class="form-group">
                                                            <label for="<%= TxtOtherCity.ClientID %>">Other City</label>
                                                            <div class="admin-input-group">
                                                                <span class="admin-input-icon"><i class="fa fa-pencil"></i></span>
                                                                <asp:TextBox ID="TxtOtherCity" CssClass="form-control" runat="server" placeholder="Enter other city" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtpincode.ClientID %>">Pincode</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-map-pin"></i></span>
                                                            <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="Enter pincode" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div style="display:none;">
                                                <asp:DropDownList ID="ddlsttehsil" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsttehsil_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Tehsil</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlstmarket" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlstmarket_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Market</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" Visible="false" Text="A"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="admin-wizard-panel" data-step-panel="5">
                                        <div class="admin-form-section admin-form-section-last">
                                            <h5 class="admin-form-section-title"><i class="fa fa-lock"></i> Login Credentials</h5>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtuserpassword.ClientID %>">Password</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-key"></i></span>
                                                            <asp:TextBox ID="txtuserpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Enter password" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="<%= txtconfirmpassword.ClientID %>">Confirm Password</label>
                                                        <div class="admin-input-group">
                                                            <span class="admin-input-icon"><i class="fa fa-check-circle"></i></span>
                                                            <asp:TextBox ID="txtconfirmpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Confirm password" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer admin-wizard-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" CausesValidation="false" />
                            <button type="button" id="btnWizardPrev" class="btn btn-default" style="display:none;">Previous</button>
                            <button type="button" id="btnWizardNext" class="btn btn-primary">Next</button>
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Add Franchisee" OnClick="btnSubmit_Click" Style="display:none;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPANlarge" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="panPreviewTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="panPreviewTitle">PAN Preview</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:Image ID="ImagePANLarge" runat="server" CssClass="admin-doc-preview-large" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivGSTLarge" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gstPreviewTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="gstPreviewTitle">GST Preview</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:Image ID="ImageGSTLarge" runat="server" CssClass="admin-doc-preview-large" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPANUPload" />
            <asp:PostBackTrigger ControlID="btnGSTUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

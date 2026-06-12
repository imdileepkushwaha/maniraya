<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="BankAccountAdd.aspx.cs" Inherits="admin_BankAccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtaccountholdername.ClientID%>").value == "") {
                alert('Enter Acc Holder Name');
                document.getElementById("<%=txtaccountholdername.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositaccountno.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtdepositaccountno.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositbank.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtdepositbank.ClientID%>").focus();
                return false;
            }
        }

        function validate2() {
            if (document.getElementById("<%=txtaccholdernameedit.ClientID%>").value == "") {
                alert('Enter Account Holder Name');
                document.getElementById("<%=txtaccholdernameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtaccountnoedit.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtaccountnoedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositbankedit.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtdepositbankedit.ClientID%>").focus();
                return false;
            }
        }

        function updateQrFileName(file) {
            var nameEl = document.getElementById("qrFileName");
            if (!nameEl) {
                return;
            }
            nameEl.textContent = file ? file.name : "No file selected";
            nameEl.classList.toggle("has-file", !!file);
        }

        function resetQrPreview() {
            var img = document.getElementById("<%=ImageShow.ClientID%>");
            var placeholder = document.getElementById("qrPreviewPlaceholder");
            var upload = document.getElementById("<%=ProductImageUpload.ClientID%>");
            var dropzone = document.getElementById("qrDropzone");
            if (img) {
                img.src = "";
                img.style.display = "none";
            }
            if (placeholder) {
                placeholder.style.display = "flex";
            }
            if (upload) {
                upload.value = "";
            }
            if (dropzone) {
                dropzone.classList.remove("is-dragover", "has-file");
            }
            updateQrFileName(null);
        }

        function previewQrFile(file) {
            var img = document.getElementById("<%=ImageShow.ClientID%>");
            var placeholder = document.getElementById("qrPreviewPlaceholder");
            var dropzone = document.getElementById("qrDropzone");
            if (!file || !file.type || file.type.indexOf("image/") !== 0) {
                resetQrPreview();
                return;
            }
            var reader = new FileReader();
            reader.onload = function (ev) {
                img.src = ev.target.result;
                img.style.display = "block";
                if (placeholder) {
                    placeholder.style.display = "none";
                }
                if (dropzone) {
                    dropzone.classList.add("has-file");
                }
            };
            reader.readAsDataURL(file);
            updateQrFileName(file);
        }

        function bindQrPreview() {
            var upload = document.getElementById("<%=ProductImageUpload.ClientID%>");
            var dropzone = document.getElementById("qrDropzone");
            if (!upload || upload._qrBound) {
                return;
            }
            upload._qrBound = true;
            upload.addEventListener("change", function (e) {
                previewQrFile(e.target.files[0]);
            });

            if (!dropzone || dropzone._dragBound) {
                return;
            }
            dropzone._dragBound = true;
            ["dragenter", "dragover"].forEach(function (evtName) {
                dropzone.addEventListener(evtName, function (e) {
                    e.preventDefault();
                    dropzone.classList.add("is-dragover");
                });
            });
            ["dragleave", "drop"].forEach(function (evtName) {
                dropzone.addEventListener(evtName, function (e) {
                    e.preventDefault();
                    dropzone.classList.remove("is-dragover");
                });
            });
            dropzone.addEventListener("drop", function (e) {
                var file = e.dataTransfer && e.dataTransfer.files ? e.dataTransfer.files[0] : null;
                if (!file) {
                    return;
                }
                try {
                    var dt = new DataTransfer();
                    dt.items.add(file);
                    upload.files = dt.files;
                } catch (ex) {
                    return;
                }
                previewQrFile(file);
            });
        }

        function openAddBankAccountModal() {
            document.getElementById("<%=txtdepositbank.ClientID%>").value = "";
            document.getElementById("<%=txtdepositaccountno.ClientID%>").value = "";
            document.getElementById("<%=txtifsccode.ClientID%>").value = "";
            document.getElementById("<%=txtaccountholdername.ClientID%>").value = "";
            resetQrPreview();
            showAdminModal('addBankAccountModal');
        }

        Sys.Application.add_load(bindQrPreview);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Bankaccount Add</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Bankaccount Add</li>
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
                            <h3 class="box-title">Details</h3>
                            <div class="box-tools">
                                <button type="button" class="btn btn-primary btn-sm admin-box-header-btn" onclick="openAddBankAccountModal();">
                                    <i class="fa fa-plus"></i> Add Bank Account
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Account Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("accountno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Holder Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("accountholdername") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbankname" runat="server" Text='<%#Eval("BankName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IFSC Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblimage" runat="server" Text='<%#Eval("IFSCCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QR Code">
                                            <ItemTemplate>
                                                <asp:Image ID="lblbranchname" runat="server" ImageUrl='<%# "../ProductImage/" + Eval("BranchName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton style="display:none;" ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDel" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="addBankAccountModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="bankAccountAddModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="bankAccountAddModalTitle">Add Bank Account</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body admin-modal-form admin-bank-account-form">
                                <p class="admin-modal-form-intro">Add payment bank account details and upload QR code for deposits.</p>

                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-university"></i> Bank Details</h5>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtdepositbank.ClientID %>">Bank Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-building"></i></span>
                                                    <asp:TextBox ID="txtdepositbank" runat="server" CssClass="form-control" placeholder="e.g. State Bank of India" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtifsccode.ClientID %>">IFSC Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                    <asp:TextBox ID="txtifsccode" runat="server" CssClass="form-control" placeholder="e.g. SBIN0001234" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-credit-card"></i> Account Details</h5>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtdepositaccountno.ClientID %>">Account Number</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox ID="txtdepositaccountno" runat="server" CssClass="form-control" placeholder="Enter account number" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtaccountholdername.ClientID %>">Account Holder Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                    <asp:TextBox ID="txtaccountholdername" runat="server" CssClass="form-control" placeholder="Name as per bank records" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-qrcode"></i> Payment QR Code</h5>
                                    <div class="admin-qr-upload-card">
                                        <div class="admin-qr-preview-box">
                                            <div id="qrPreviewPlaceholder" class="admin-qr-placeholder">
                                                <i class="fa fa-qrcode"></i>
                                                <span>QR preview</span>
                                            </div>
                                            <asp:Image ID="ImageShow" runat="server" CssClass="admin-qr-preview-img" />
                                        </div>
                                        <div class="admin-qr-upload-side">
                                            <p class="admin-qr-upload-title">Upload QR Image</p>
                                            <p class="admin-qr-upload-hint">Use a clear, high-quality payment QR so users can scan easily.</p>
                                            <div class="admin-qr-dropzone" id="qrDropzone">
                                                <asp:FileUpload ID="ProductImageUpload" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                                <label class="admin-qr-dropzone-label" for="<%= ProductImageUpload.ClientID %>">
                                                    <span class="admin-qr-dropzone-icon"><i class="fa fa-cloud-upload"></i></span>
                                                    <span class="admin-qr-dropzone-text"><strong>Browse file</strong> or drag image here</span>
                                                    <span class="admin-qr-dropzone-meta">PNG, JPG, WEBP</span>
                                                </label>
                                            </div>
                                            <span class="admin-qr-filename" id="qrFileName">No file selected</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="bankAccountEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="bankAccountEditModalTitle">Edit Bank Account</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblbankaccountid" runat="server" Visible="false" Text="0"></asp:Label>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="<%= txtaccountnoedit.ClientID %>">Account Number</label>
                                            <asp:TextBox ID="txtaccountnoedit" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="<%= txtaccholdernameedit.ClientID %>">Account Holder Name</label>
                                            <asp:TextBox ID="txtaccholdernameedit" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="<%= txtdepositbankedit.ClientID %>">Bank Name</label>
                                            <asp:TextBox ID="txtdepositbankedit" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>QR Code</label>
                                            <asp:Image ID="ImageButton1" runat="server" Width="50px" Height="50px" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="<%= FileUpload1.ClientID %>">Image Upload</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

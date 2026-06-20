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
            return true;
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
            return true;
        }

        function syncEditBankQrPreview(imageUrl) {
            if (window.AdminImageUpload) {
                AdminImageUpload.setUrlPreview("editQrUploadCard", imageUrl, "Current QR image");
            }
        }

        function resetAddBankQrUpload() {
            if (window.AdminImageUpload) {
                AdminImageUpload.reset(document.querySelector(".admin-qr-upload-card"));
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Bank Account</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add Bank Account</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row admin-utility-page">
                <div class="col-md-12 admin-utility-stack">
                    <div class="box box-primary admin-utility-add-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Bank Account</h3>
                        </div>
                        <div class="box-body admin-bank-account-form">
                            <p class="admin-modal-form-intro">Add payment bank account details and upload QR code for user deposits.</p>

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
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Save Bank Account" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary admin-utility-list-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Bank Account List</h3>
                        </div>
                        <div class="box-body">
                            <div class="admin-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Number">
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
                                                <asp:Image ID="lblbranchname" runat="server" CssClass="admin-utility-qr-thumb" ImageUrl='<%# "../ProductImage/" + Eval("BranchName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ToolTip="Edit account"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDel" CssClass="admin-grid-delete-btn" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" OnClientClick="return confirm('Are you sure you want to delete this bank account?');" ToolTip="Delete account"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade admin-utility-edit-modal" tabindex="-1" role="dialog" aria-labelledby="bankAccountEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="bankAccountEditModalTitle"><i class="fa fa-pencil-square-o"></i> Edit Bank Account</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body admin-modal-form admin-bank-account-form admin-bank-account-form--edit">
                                <p class="admin-modal-form-intro admin-modal-form-intro--visible">Update bank account details. Leave QR upload empty to keep the current image.</p>

                                <asp:Label ID="lblbankaccountid" runat="server" Visible="false" Text="0"></asp:Label>
                                <asp:HiddenField ID="hfEditQrImage" runat="server" />

                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-university"></i> Bank Details</h5>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtdepositbankedit.ClientID %>">Bank Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-building"></i></span>
                                                    <asp:TextBox ID="txtdepositbankedit" runat="server" CssClass="form-control" placeholder="e.g. State Bank of India" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtifsccodeedit.ClientID %>">IFSC Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                    <asp:TextBox ID="txtifsccodeedit" runat="server" CssClass="form-control" placeholder="e.g. SBIN0001234" />
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
                                                <label for="<%= txtaccountnoedit.ClientID %>">Account Number</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox ID="txtaccountnoedit" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" placeholder="Enter account number" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtaccholdernameedit.ClientID %>">Account Holder Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                    <asp:TextBox ID="txtaccholdernameedit" runat="server" CssClass="form-control" placeholder="Name as per bank records" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-qrcode"></i> Payment QR Code</h5>
                                    <div class="admin-qr-upload-card" id="editQrUploadCard">
                                        <div class="admin-qr-preview-box">
                                            <div id="editQrPreviewPlaceholder" class="admin-qr-placeholder">
                                                <i class="fa fa-qrcode"></i>
                                                <span>QR preview</span>
                                            </div>
                                            <asp:Image ID="ImageButton1" runat="server" CssClass="admin-qr-preview-img" AlternateText="Current QR code" />
                                        </div>
                                        <div class="admin-qr-upload-side">
                                            <p class="admin-qr-upload-title">Replace QR Image</p>
                                            <p class="admin-qr-upload-hint">Upload a new QR only if you want to change the existing payment code.</p>
                                            <div class="admin-qr-dropzone" id="editQrDropzone">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                                <label class="admin-qr-dropzone-label" for="<%= FileUpload1.ClientID %>">
                                                    <span class="admin-qr-dropzone-icon"><i class="fa fa-cloud-upload"></i></span>
                                                    <span class="admin-qr-dropzone-text"><strong>Browse file</strong> or drag image here</span>
                                                    <span class="admin-qr-dropzone-meta">PNG, JPG, WEBP</span>
                                                </label>
                                            </div>
                                            <span class="admin-qr-filename" id="editQrFileName" data-empty-text="Keep current QR image">Keep current QR image</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer admin-modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
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

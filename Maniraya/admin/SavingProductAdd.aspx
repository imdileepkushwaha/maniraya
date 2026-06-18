<%@ Page Title="Add Saving Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductAdd.aspx.cs" Inherits="admin_ProductAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
           
           if (document.getElementById("<%=txtproductname.ClientID%>").value == "") {
                alert('Enter Product Name');
                document.getElementById("<%=txtproductname.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtdp.ClientID%>").value == "") {
                alert('Enter DP');
                document.getElementById("<%=txtdp.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtmrp.ClientID%>").value == "") {
                alert('Enter MRP');
                document.getElementById("<%=txtmrp.ClientID%>").focus();
                return false;
            }
            return true;
        }

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add  Saving Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#"> Saving Product </a></li>
            <li class="active">Add Saving Product</li>
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
                            <h3 class="box-title">Add  Saving Product</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Fill in product details, upload images</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i>Product Details</h5>
                                <div class="row">
                                  
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                                <asp:TextBox ID="txtproductname" CssClass="form-control" runat="server" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtmrp.ClientID %>">MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtmrp" CssClass="form-control" runat="server" TextMode="Number" step="0.01" placeholder="Enter MRP" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtdp.ClientID %>">DP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtdp" CssClass="form-control" runat="server" TextMode="Number" step="0.01" placeholder="Enter DP" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Product Image</h5>
                                <p class="admin-section-hint">Upload a clear product photo. JPG or PNG recommended. You can browse or drag and drop the file.</p>
                                <div class="admin-media-upload-grid is-compact">
                                    <div class="admin-category-image-compact is-primary" id="savingProductImageSlot">
                                        <div class="admin-category-image-compact-preview admin-product-image-preview-box">
                                            <div id="savingProductPlaceholder" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="savingProductPreview" class="admin-product-image-preview-img" alt="Saving product image preview" />
                                        </div>
                                        <div class="admin-category-image-compact-body">
                                            <div class="admin-category-image-compact-head">
                                                <span class="admin-category-image-compact-title">Product Photo</span>
                                                <span class="admin-product-image-slot-badge">Primary</span>
                                            </div>
                                            <div class="admin-product-image-dropzone" id="savingProductDropzone">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="admin-file-input-hidden" accept=".png,.jpg,.jpeg,.webp,image/png,image/jpeg,image/webp" />
                                                <label class="admin-product-image-dropzone-label" for="<%= FileUpload1.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Browse or drop image</span>
                                                </label>
                                                <span id="savingProductFileName" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                            <p class="admin-category-image-compact-hint">PNG, JPG or WebP · square image works best</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="return validate();" runat="server" Text="Save Product" OnClick="btnSubmit_Click1" />
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
 
</asp:Content>

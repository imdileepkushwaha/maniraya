<%@ Page Title="Add Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ProductAdd.aspx.cs" Inherits="admin_ProductAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {
                alert('Select Category');
                document.getElementById("<%=ddcountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtstatename.ClientID%>").value == "") {
                alert('Enter Product Name');
                document.getElementById("<%=txtstatename.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtAmount.ClientID%>").value == "") {
                alert('Enter CP');
                document.getElementById("<%=TxtAmount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtDP.ClientID%>").value == "") {
                alert('Enter DP');
                document.getElementById("<%=TxtDP.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {
                alert('Enter Business Volume');
                document.getElementById("<%=TxtBV.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtMRP.ClientID%>").value == "") {
                alert('Enter MRP');
                document.getElementById("<%=TxtMRP.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function setVariantRowState(row, checked) {
            if (!row) {
                return;
            }
            if (checked) {
                row.classList.add("admin-variant-row-selected");
            } else {
                row.classList.remove("admin-variant-row-selected");
            }
        }

        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            setVariantRowState(row, objRef.checked);
        }

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        setVariantRowState(row, true);
                        inputList[i].checked = true;
                    } else {
                        setVariantRowState(row, false);
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product management</a></li>
            <li class="active">Add Product</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <div class="row" style="margin-bottom: 20px;">
        <div class="col-md-12">
            <div class="box box-solid" style="margin-bottom: 0;">
                <div class="box-body text-center">
                    <a href="CategoryAdd.aspx" class="btn btn-default"><i class="fa fa-sitemap"></i> Add Category</a>
                    <a href="SubcategoryAdd.aspx" class="btn btn-default"><i class="fa fa-code-fork"></i> Add Subcategory</a>
                    <a href="SizeAdd.aspx" class="btn btn-default"><i class="fa fa-arrows-v"></i> Add Size</a>
                    <a href="ColorAdd.aspx" class="btn btn-default"><i class="fa fa-paint-brush"></i> Add Color</a>
                    <a href="ProductSizeColorMaster.aspx" class="btn btn-default"><i class="fa fa-cogs"></i> Subcategory Setting</a>
                    <a href="ProductAdd.aspx" class="btn btn-primary"><i class="fa fa-cube"></i> Add Product</a>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Product</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Fill in product details, upload images, and select size/color variants configured for the chosen sub-category.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i> Category &amp; Product</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddcountry.ClientID %>">Category</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-folder-open"></i></span>
                                                <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddsubcategory.ClientID %>">Sub-Category</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                                <asp:DropDownList ID="ddsubcategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddsubcategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Sub-Category</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtstatename.ClientID %>">Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cube"></i></span>
                                                <asp:TextBox ID="txtstatename" CssClass="form-control" runat="server" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-inr"></i> Pricing &amp; Volume</h5>
                                <div class="row">
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtAmount.ClientID %>">CP (Cost Price)</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-money"></i></span>
                                                <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" TextMode="Number" placeholder="0.00" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtDP.ClientID %>">DP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                <asp:TextBox ID="TxtDP" CssClass="form-control" runat="server" TextMode="Number" placeholder="0.00" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtBV.ClientID %>">Business Volume</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                                <asp:TextBox ID="TxtBV" CssClass="form-control" runat="server" TextMode="Number" placeholder="0.00" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtMRP.ClientID %>">MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-shopping-cart"></i></span>
                                                <asp:TextBox ID="TxtMRP" CssClass="form-control" runat="server" TextMode="Number" placeholder="0.00" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-file-text-o"></i> Tax &amp; Compliance</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtGst.ClientID %>">GST (%)</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-percent"></i></span>
                                                <asp:TextBox ID="txtGst" CssClass="form-control" runat="server" TextMode="Number" placeholder="e.g. 18" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtHSN.ClientID %>">HSN Code</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                <asp:TextBox ID="txtHSN" CssClass="form-control" runat="server" placeholder="Enter HSN code" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtBatch.ClientID %>">Batch Number</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                <asp:TextBox ID="txtBatch" CssClass="form-control" runat="server" placeholder="Enter batch number" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Product Images</h5>
                                <p class="admin-section-hint">Upload up to 4 images. The first image is used as the primary product photo. JPG or PNG recommended.</p>
                                <div class="admin-product-images-grid">
                                    <div class="admin-product-image-slot is-primary" id="prodImageSlot1">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 1</p>
                                            <span class="admin-product-image-slot-badge">Primary</span>
                                        </div>
                                        <div class="admin-product-image-preview-box">
                                            <div id="prodImgPlaceholder1" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="prodImgPreview1" class="admin-product-image-preview-img" alt="Product image 1 preview" />
                                        </div>
                                        <div class="admin-product-image-dropzone" id="prodDropzone1">
                                            <asp:FileUpload ID="ProductImageUpload" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                            <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload.ClientID %>">
                                                <i class="fa fa-cloud-upload"></i>
                                                <span>Browse or drop</span>
                                            </label>
                                            <span id="prodFileName1" class="admin-product-image-filename">No file selected</span>
                                        </div>
                                    </div>

                                    <div class="admin-product-image-slot" id="prodImageSlot2">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 2</p>
                                        </div>
                                        <div class="admin-product-image-preview-box">
                                            <div id="prodImgPlaceholder2" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="prodImgPreview2" class="admin-product-image-preview-img" alt="Product image 2 preview" />
                                        </div>
                                        <div class="admin-product-image-dropzone" id="prodDropzone2">
                                            <asp:FileUpload ID="ProductImageUpload2" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                            <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload2.ClientID %>">
                                                <i class="fa fa-cloud-upload"></i>
                                                <span>Browse or drop</span>
                                            </label>
                                            <span id="prodFileName2" class="admin-product-image-filename">No file selected</span>
                                        </div>
                                    </div>

                                    <div class="admin-product-image-slot" id="prodImageSlot3">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 3</p>
                                        </div>
                                        <div class="admin-product-image-preview-box">
                                            <div id="prodImgPlaceholder3" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="prodImgPreview3" class="admin-product-image-preview-img" alt="Product image 3 preview" />
                                        </div>
                                        <div class="admin-product-image-dropzone" id="prodDropzone3">
                                            <asp:FileUpload ID="ProductImageUpload3" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                            <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload3.ClientID %>">
                                                <i class="fa fa-cloud-upload"></i>
                                                <span>Browse or drop</span>
                                            </label>
                                            <span id="prodFileName3" class="admin-product-image-filename">No file selected</span>
                                        </div>
                                    </div>

                                    <div class="admin-product-image-slot" id="prodImageSlot4">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 4</p>
                                        </div>
                                        <div class="admin-product-image-preview-box">
                                            <div id="prodImgPlaceholder4" class="admin-product-image-placeholder">
                                                <i class="fa fa-image"></i>
                                                <span>No image</span>
                                            </div>
                                            <img id="prodImgPreview4" class="admin-product-image-preview-img" alt="Product image 4 preview" />
                                        </div>
                                        <div class="admin-product-image-dropzone" id="prodDropzone4">
                                            <asp:FileUpload ID="ProductImageUpload4" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                            <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload4.ClientID %>">
                                                <i class="fa fa-cloud-upload"></i>
                                                <span>Browse or drop</span>
                                            </label>
                                            <span id="prodFileName4" class="admin-product-image-filename">No file selected</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-align-left"></i> Description</h5>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="<%= Txtshortdiscription.ClientID %>">Short Description</label>
                                            <div class="admin-input-group admin-textarea-group">
                                                <span class="admin-input-icon"><i class="fa fa-comment-o"></i></span>
                                                <asp:TextBox ID="Txtshortdiscription" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Brief summary shown in listings" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="<%= TxtDescription.ClientID %>">Full Description</label>
                                            <div class="admin-editor-wrap">
                                                <cc1:Editor ID="TxtDescription" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-th-large"></i> Size &amp; Color Variants</h5>
                                <p class="admin-section-hint">Select sub-category first. Variants come from Subcategory Setting. Check the rows you want to include with this product.</p>
                                <div class="admin-product-variant-wrap table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkStatus" runat="server" onclick="Check_Click(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.N.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("SubCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaccouColorName" runat="server" Text='<%#Eval("ColorName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaccSizeName" runat="server" Text='<%#Eval("SizeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
    <script src="../bower_components/ckeditor/ckeditor.js"></script>
    <script src="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <script>
        $(function () {
            $('.textarea').wysihtml5();
        });
    </script>
</asp:Content>

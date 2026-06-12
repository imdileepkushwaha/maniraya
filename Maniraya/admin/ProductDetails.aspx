<%@ Page Title="Product Detail" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="admin_ProductDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
    <script type="text/javascript">

       
           function validate2() {
               // alert('sd');
               if (document.getElementById("<%=txtstatenameedit.ClientID%>").value == "") {

                   alert('Enter State Name');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtstatenameedit.ClientID%>").focus();
                   return false;                  
               }
               if (document.getElementById("<%=TxtAmountEdit.ClientID%>").value == "") {

                   alert('Enter Amount');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtAmountEdit.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtDescription.ClientID%>").value == "") {

                   alert('Enter Description');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtDescription.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {

                   alert('Enter Buissness Volume');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtBV.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtMrp.ClientID%>").value == "") {

                   alert('Enter MRP');
                   document.getElementById("<%=TxtMrp.ClientID%>").focus();
                   return false;
               }
               return true;
           }

        var editProductImageSlots = [
            { uploadId: "<%= ProductImageUpload.ClientID %>", previewId: "<%= Image2.ClientID %>", placeholderId: "editImgPlaceholder1", dropzoneId: "editDropzone1", filenameId: "editFileName1", slotId: "editImageSlot1" },
            { uploadId: "<%= ProductImageUpload2.ClientID %>", previewId: "<%= Image3.ClientID %>", placeholderId: "editImgPlaceholder2", dropzoneId: "editDropzone2", filenameId: "editFileName2", slotId: "editImageSlot2" },
            { uploadId: "<%= ProductImageUpload3.ClientID %>", previewId: "<%= Image4.ClientID %>", placeholderId: "editImgPlaceholder3", dropzoneId: "editDropzone3", filenameId: "editFileName3", slotId: "editImageSlot3" }
        ];

        function updateEditFileName(filenameId, file, currentLabel) {
            var nameEl = document.getElementById(filenameId);
            if (!nameEl) {
                return;
            }
            if (file) {
                nameEl.textContent = file.name;
                nameEl.classList.add("has-file");
            } else if (currentLabel) {
                nameEl.textContent = currentLabel;
                nameEl.classList.add("has-file");
            } else {
                nameEl.textContent = "No file selected";
                nameEl.classList.remove("has-file");
            }
        }

        function resetEditProductImagePreview(slot) {
            var img = document.getElementById(slot.previewId);
            var placeholder = document.getElementById(slot.placeholderId);
            var upload = document.getElementById(slot.uploadId);
            var dropzone = document.getElementById(slot.dropzoneId);
            var card = document.getElementById(slot.slotId);
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
            if (card) {
                card.classList.remove("has-file");
            }
            updateEditFileName(slot.filenameId, null, null);
        }

        function isEditPlaceholderImage(url) {
            return !url || url === "../ProductImage/" || url.indexOf("images.png") !== -1;
        }

        function syncEditProductImageSlot(slot, imageUrl) {
            if (isEditPlaceholderImage(imageUrl)) {
                resetEditProductImagePreview(slot);
                return;
            }
            var img = document.getElementById(slot.previewId);
            var placeholder = document.getElementById(slot.placeholderId);
            var dropzone = document.getElementById(slot.dropzoneId);
            var card = document.getElementById(slot.slotId);
            var upload = document.getElementById(slot.uploadId);
            if (upload) {
                upload.value = "";
            }
            if (img) {
                img.src = imageUrl;
                img.style.display = "block";
            }
            if (placeholder) {
                placeholder.style.display = "none";
            }
            if (dropzone) {
                dropzone.classList.add("has-file");
            }
            if (card) {
                card.classList.add("has-file");
            }
            updateEditFileName(slot.filenameId, null, "Current image");
        }

        function syncEditProductImages(url1, url2, url3) {
            var urls = [url1, url2, url3];
            for (var i = 0; i < editProductImageSlots.length; i++) {
                syncEditProductImageSlot(editProductImageSlots[i], urls[i] || "");
            }
        }

        function previewEditProductImage(slot, file) {
            var img = document.getElementById(slot.previewId);
            var placeholder = document.getElementById(slot.placeholderId);
            var dropzone = document.getElementById(slot.dropzoneId);
            var card = document.getElementById(slot.slotId);
            if (!file || !file.type || file.type.indexOf("image/") !== 0) {
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
                if (card) {
                    card.classList.add("has-file");
                }
            };
            reader.readAsDataURL(file);
            updateEditFileName(slot.filenameId, file, null);
        }

        function bindEditProductImageSlot(slot) {
            var upload = document.getElementById(slot.uploadId);
            var dropzone = document.getElementById(slot.dropzoneId);
            if (!upload || upload._editImgBound) {
                return;
            }
            upload._editImgBound = true;
            upload.addEventListener("change", function (e) {
                previewEditProductImage(slot, e.target.files[0]);
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
                previewEditProductImage(slot, file);
            });
        }

        function bindEditProductImageSlots() {
            for (var i = 0; i < editProductImageSlots.length; i++) {
                bindEditProductImageSlot(editProductImageSlots[i]);
            }
        }

        function initEditProductImageUploads() {
            bindEditProductImageSlots();
        }

        Sys.Application.add_load(initEditProductImageUploads);

        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var editImgPrm = Sys.WebForms.PageRequestManager.getInstance();
            if (editImgPrm && !editImgPrm._editImgUploadBound) {
                editImgPrm._editImgUploadBound = true;
                editImgPrm.add_endRequest(function () {
                    for (var i = 0; i < editProductImageSlots.length; i++) {
                        var upload = document.getElementById(editProductImageSlots[i].uploadId);
                        if (upload) {
                            upload._editImgBound = false;
                        }
                        var dropzone = document.getElementById(editProductImageSlots[i].dropzoneId);
                        if (dropzone) {
                            dropzone._dragBound = false;
                        }
                    }
                    bindEditProductImageSlots();
                });
            }
        }
        
    </script>

    

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
       Product Detail     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Product Detail</li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                          
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Name</label>
                                     <asp:TextBox ID="TxtProductNameSearch" CssClass="form-control " runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Code</label>
                                         <asp:TextBox ID="TxtProductCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                                    
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                          <label>Status</label>
                                           <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Deactive</asp:ListItem>
                                         
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                            </div>

                             

                        </div>
                        <div class="box-footer">
                              
                              <asp:Button ID="BtnSearch"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />



                        </div>

                    </div>
                </div>
      <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Product List</h3>
              <div class="box-tools admin-record-filter-tools">
                  <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                  <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter" AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                      <asp:ListItem Selected="True">10</asp:ListItem>
                      <asp:ListItem>25</asp:ListItem>
                      <asp:ListItem>50</asp:ListItem>
                      <asp:ListItem>100</asp:ListItem>
                      <asp:ListItem>All</asp:ListItem>
                  </asp:DropDownList>
                  <span class="admin-record-filter-suffix">per page</span>
              </div>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  
                <div class="form-group table-responsive">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  >
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" />
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# (GridView1.PageIndex * GridView1.PageSize) + Container.DataItemIndex + 1 %>
                                    
                                          <asp:Label ID="LblDescription" runat="server" Visible="false" Text='<%#Eval("Description") %>'></asp:Label>
                                          <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%#Eval("Image") %>'></asp:Label>
                                          <asp:Label ID="LblImage2" runat="server" Visible="false" Text='<%#Eval("Image2") %>'></asp:Label>
                                                <asp:Label ID="LblImage3" runat="server" Visible="false" Text='<%#Eval("Image3") %>'></asp:Label>
                                         <asp:Label ID="LblStatuschk" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                                            <asp:Label ID="LblHSNcode" runat="server" Visible="false" Text='<%#Eval("HSNCODE") %>'></asp:Label>
                                          <asp:Label ID="LBLBatchno" runat="server" Visible="false" Text='<%#Eval("BATCHNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Category Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("Categoryname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product Code">
                               <ItemTemplate>
                                       <asp:Label ID="lblid" runat="server"  Text='<%#Eval("ProductId") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename" runat="server"  Text='<%#Eval("ProductName") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="CP">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename1" runat="server"  Text='<%#Eval("Amount") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="DP">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename2" runat="server"  Text='<%#Eval("DP") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatenameGST" runat="server"  Text='<%#Eval("GST") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Buissness Volume">
                               <ItemTemplate>
                                     <asp:Label ID="lblbv" runat="server"  Text='<%#Eval("BV") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="MRP">
                               <ItemTemplate>
                                     <asp:Label ID="lblMRP" runat="server"  Text='<%#Eval("MRP") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Product Image" >
                               <ItemTemplate>
                                   <asp:LinkButton ID="lnkph" runat="server" CommandName="photolarge"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px"  /></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                               <asp:Label ID="lblstatus" runat="server"  Text='<%#Eval("Status1") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Purchase Status">
                               <ItemTemplate>
                               <asp:Label ID="lblstatus1" runat="server"  Text='<%#Eval("PurchaseStatus1") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>--%>
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
              
                </div>             
             
                   
                       
            
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>
        <div id="myModal" class="modal fade admin-modal-scrollable" tabindex="-1" role="dialog" aria-labelledby="productEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg admin-product-edit-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="productEditModalTitle">Edit Product Details</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body admin-product-form">
                                <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>

                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-cube"></i> Product Information</h5>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtstatenameedit.ClientID %>">Product Name</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtstatenameedit" placeholder="Enter product name"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= DDLstStatusEdit.ClientID %>">Status</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-toggle-on"></i></span>
                                                    <asp:DropDownList ID="DDLstStatusEdit" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                                        <asp:ListItem Value="0">Deactive</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtAmountEdit.ClientID %>">CP</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxtAmountEdit" TextMode="Number" placeholder="CP"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TXTDP.ClientID %>">DP</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TXTDP" TextMode="Number" placeholder="DP"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtGst.ClientID %>">GST</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-percent"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtGst" TextMode="Number" placeholder="GST"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtBV.ClientID %>">Business Volume</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxtBV" TextMode="Number" placeholder="BV"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtMrp.ClientID %>">MRP</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxtMrp" TextMode="Number" placeholder="MRP"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtHsncode.ClientID %>">HSN Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxtHsncode" placeholder="HSN code"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= Txtbatchno.ClientID %>">Batch No</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="Txtbatchno" placeholder="Batch no"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-image"></i> Product Images</h5>
                                    <p class="admin-section-hint">Upload new images to replace existing ones. JPG or PNG recommended. Leave empty to keep current image.</p>
                                    <div class="admin-product-images-grid admin-product-images-grid--edit">
                                        <div class="admin-product-image-slot is-primary" id="editImageSlot1">
                                            <div class="admin-product-image-slot-head">
                                                <p class="admin-product-image-slot-title">Image 1</p>
                                                <span class="admin-product-image-slot-badge">Primary</span>
                                            </div>
                                            <div class="admin-product-image-preview-box">
                                                <div id="editImgPlaceholder1" class="admin-product-image-placeholder">
                                                    <i class="fa fa-image"></i>
                                                    <span>No image</span>
                                                </div>
                                                <asp:Image ID="Image2" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Product image 1" />
                                            </div>
                                            <div class="admin-product-image-dropzone" id="editDropzone1">
                                                <asp:FileUpload ID="ProductImageUpload" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                                <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Browse or drop</span>
                                                </label>
                                                <span id="editFileName1" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                        </div>

                                        <div class="admin-product-image-slot" id="editImageSlot2">
                                            <div class="admin-product-image-slot-head">
                                                <p class="admin-product-image-slot-title">Image 2</p>
                                            </div>
                                            <div class="admin-product-image-preview-box">
                                                <div id="editImgPlaceholder2" class="admin-product-image-placeholder">
                                                    <i class="fa fa-image"></i>
                                                    <span>No image</span>
                                                </div>
                                                <asp:Image ID="Image3" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Product image 2" />
                                            </div>
                                            <div class="admin-product-image-dropzone" id="editDropzone2">
                                                <asp:FileUpload ID="ProductImageUpload2" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                                <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload2.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Browse or drop</span>
                                                </label>
                                                <span id="editFileName2" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                        </div>

                                        <div class="admin-product-image-slot" id="editImageSlot3">
                                            <div class="admin-product-image-slot-head">
                                                <p class="admin-product-image-slot-title">Image 3</p>
                                            </div>
                                            <div class="admin-product-image-preview-box">
                                                <div id="editImgPlaceholder3" class="admin-product-image-placeholder">
                                                    <i class="fa fa-image"></i>
                                                    <span>No image</span>
                                                </div>
                                                <asp:Image ID="Image4" runat="server" CssClass="admin-product-image-preview-img" AlternateText="Product image 3" />
                                            </div>
                                            <div class="admin-product-image-dropzone" id="editDropzone3">
                                                <asp:FileUpload ID="ProductImageUpload3" runat="server" CssClass="admin-file-input-hidden" accept="image/*" />
                                                <label class="admin-product-image-dropzone-label" for="<%= ProductImageUpload3.ClientID %>">
                                                    <i class="fa fa-cloud-upload"></i>
                                                    <span>Browse or drop</span>
                                                </label>
                                                <span id="editFileName3" class="admin-product-image-filename">No file selected</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-align-left"></i> Description</h5>
                                    <div class="form-group admin-product-editor-wrap">
                                        <cc1:Editor ID="TxtDescription" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Product" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>


        <div id="DivPhotolarge" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="productImageModalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="productImageModalTitle">Product Images</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">

                                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                    <!-- Indicators -->
                                    <ol class="carousel-indicators">
                                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                        <li data-target="#myCarousel" data-slide-to="1"></li>
                                        <li data-target="#myCarousel" data-slide-to="2"></li>
                                    </ol>

                                    <!-- Wrapper for slides -->
                                    <div class="carousel-inner">
                                        <div class="item active">
                                               <asp:Image ID="ImageLarge" runat="server" Width="570px" Height="400px" />
                                           <%-- <img src="../ProductImage/636480744192755102Chrysanthemum.jpg" alt="image" runat="server">--%>
                                        </div>

                                        <div class="item">
                                             <asp:Image ID="ImageLarge2" runat="server" Width="570px" Height="400px" />
                                           
                                        </div>

                                        <div class="item">
                                              <asp:Image ID="ImageLarge3" runat="server" Width="570px" Height="400px" />
                                            
                                        </div>
                                    </div>

                                    <!-- Left and right controls -->
                                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                        <span class="glyphicon glyphicon-chevron-right"></span>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </div>

                                <%--<div class="form-group">

                                    <asp:Image ID="Image5" runat="server" Width="570px" Height="400px" />
                                </div>--%>
                            </div>
                            <div class="modal-footer">

                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
     </div>
      
      
        
      </ContentTemplate>
         <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnUpdate" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
   <script src="../bower_components/ckeditor/ckeditor.js"></script>
       <script src="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
   <script>
       $(function () {
           // Replace the <textarea id="editor1"> with a CKEditor
           // instance, using default configuration.
           //CKEDITOR.replace('editor1')
           //bootstrap WYSIHTML5 - text editor
           $('.textarea').wysihtml5()
       })
</script>
</asp:Content>


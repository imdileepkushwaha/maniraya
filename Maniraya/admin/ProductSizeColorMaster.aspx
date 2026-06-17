<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="ProductSizeColorMaster.aspx.cs" Inherits="admin_ProductSizeColorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
      function getSizePanel() {
          return document.getElementById("adminPscSizePanel");
      }

      function getSizeCheckboxes() {
          var panel = getSizePanel();
          if (!panel) {
              return [];
          }
          return panel.querySelectorAll("input[type='checkbox']");
      }

      function hasSizeSelected() {
          var boxes = getSizeCheckboxes();
          for (var i = 0; i < boxes.length; i++) {
              if (boxes[i].checked) {
                  return true;
              }
          }
          return false;
      }

      function selectAllSizes(selectAll) {
          var boxes = getSizeCheckboxes();
          for (var i = 0; i < boxes.length; i++) {
              boxes[i].checked = !!selectAll;
          }
          if (typeof window.updateSizeSelectionCount === "function") {
              window.updateSizeSelectionCount();
          }
      }

      function validate() {
          if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {
              alert('Select Category');
              document.getElementById("<%=ddcountry.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=ddsubcategory.ClientID%>").value == "0") {
              alert('Select SubCategory');
              document.getElementById("<%=ddsubcategory.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=ddlColor.ClientID%>").value == "0") {
              alert('Select Color');
              document.getElementById("<%=ddlColor.ClientID%>").focus();
              return false;
          }
          if (!hasSizeSelected()) {
              alert('Select at least one Size');
              return false;
          }
          return true;
      }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
     <h1 style="color:white;">Subcategory Setting
     </h1>
     <ol class="breadcrumb">
          <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home > </a></li>
         <li><a href="#">Subcategory Setting </a></li>
         <li class="active">Subcategory Setting</li>
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
                    <a href="ProductSizeColorMaster.aspx" class="btn btn-primary"><i class="fa fa-cogs"></i> Subcategory Setting</a>
                    <a href="ProductAdd.aspx" class="btn btn-default"><i class="fa fa-cube"></i> Add Product</a>
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
           <h3 class="box-title">Product Color Size Master</h3>
       </div>

       <div class="box-body admin-psc-form">
            <p class="admin-psc-intro">Map color and size combinations for a sub-category. Select one color and multiple sizes, then submit.</p>

            <div class="admin-form-section">
                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i> Category Details</h5>
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
                                <asp:DropDownList ID="ddsubcategory" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Select Sub-Category</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="form-group">
                            <label for="<%= ddlColor.ClientID %>">Color</label>
                            <div class="admin-input-group">
                                <span class="admin-input-icon"><i class="fa fa-tint"></i></span>
                                <asp:DropDownList ID="ddlColor" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="admin-form-section admin-form-section-last">
                <div class="admin-psc-size-header">
                    <div>
                        <h5 class="admin-form-section-title admin-psc-size-title"><i class="fa fa-arrows-alt"></i> Select Sizes</h5>
                        <p class="admin-psc-size-hint">Choose one or more sizes for the selected color.</p>
                    </div>
                    <div class="admin-psc-size-tools">
                        <span id="sizeSelectionCount" class="admin-psc-size-count">0 selected</span>
                        <button type="button" class="btn btn-default btn-xs admin-psc-size-btn" onclick="selectAllSizes(true);">Select All</button>
                        <button type="button" class="btn btn-default btn-xs admin-psc-size-btn" onclick="selectAllSizes(false);">Clear</button>
                    </div>
                </div>
                <div class="admin-size-select-panel" id="adminPscSizePanel">
                    <asp:CheckBoxList ID="cblSize" CssClass="admin-size-chip-list" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                    <asp:Label ID="lblNoSizes" runat="server" Visible="false" CssClass="admin-size-empty">
                        <i class="fa fa-info-circle"></i> No sizes found. Please add sizes from <strong>Size Master</strong> first.
                    </asp:Label>
                </div>
            </div>
       </div>
       <div class="box-footer admin-psc-footer">
          <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
          <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Save Mapping" OnClick="btnSubmit_Click" />
   </div>
                              </div>
                         </div>
                     <div class="col-md-12">

         <div class="box box-primary">
        <div class="box-header with-border">
          <h3 class="box-title">Details</h3>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
       
          <div class="box-body">
                <div class="form-group">
              <asp:GridView ID="gvData"
    runat="server"
    AutoGenerateColumns="False"
  OnRowCommand="GridView1_RowCommand"
    DataKeyNames="ID"
    
    CssClass="table table-bordered table-hover dataTable" Width="100%">

<Columns>

<asp:BoundField DataField="ID"
    HeaderText="ID"
    ReadOnly="True" />

                                                    <asp:TemplateField HeaderText="Color">
    <ItemTemplate>
          <asp:Label ID="lblid" runat="server"  Text='<%# Eval("ID") %>'></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color">
    <ItemTemplate>
          <asp:Label ID="lblSubCategoryID" runat="server"  Text='<%# Eval("SubCategoryID") %>'></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubCategoryName">
    <ItemTemplate>
          <asp:Label ID="lblSubCategoryname" runat="server"  Text='<%# Eval("SubCategoryName") %>'></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>
 
                                               
                                            <asp:TemplateField HeaderText="Color">
    <ItemTemplate>
          <asp:Label ID="lblColorName" runat="server"  Text='<%# Eval("ColorName") %>'></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
    <ItemTemplate>
          <asp:Label ID="lblsizename" runat="server"  Text='<%# Eval("SizeName") %>'></asp:Label>
        <asp:Label ID="lblcolorid" runat="server"  Text='<%# Eval("colorid") %>' Visible="false"></asp:Label>
        <asp:Label ID="lblsizeid" runat="server"  Text='<%# Eval("sizeid") %>' Visible="false"></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
    <ItemTemplate>
          <asp:Label ID="lblstausvalue" runat="server"  Text='<%# Eval("Status").ToString() == "1" ? "Unblock" : "Block" %>'></asp:Label>
          <asp:Label ID="lblstatus" runat="server"  Text='<%# Eval("Status") %>' Visible="false"></asp:Label>
          
    </ItemTemplate>
</asp:TemplateField>


      <asp:TemplateField HeaderText="Action">
    <ItemTemplate>

        <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
       
    </ItemTemplate>
   
</asp:TemplateField>
          <asp:TemplateField HeaderText="Status Update">
    <ItemTemplate>

        
         <asp:LinkButton ID="Lblupdate" CommandName="Status"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
    </ItemTemplate>
   
</asp:TemplateField>

</Columns>

</asp:GridView>
                    </div>
              </div>
             </div>
                         </div>
                                 <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="pscEditModalTitle" aria-hidden="true">
              <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="pscEditModalTitle">Edit Mapping</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="<%= lblsubcate.ClientID %>">Sub-Category</label>
                      <asp:Label ID="lblcountryid" Visible="false"  runat="server" Text=""></asp:Label>
                <asp:Label ID="lblsubcate" CssClass="form-control"  runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="<%= Ddlstcoloredit.ClientID %>">Color</label>
                            <asp:DropDownList ID="Ddlstcoloredit"  CssClass="form-control" runat="server">
                            </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= DDlstsizeedit.ClientID %>">Size</label>
                            <asp:DropDownList ID="DDlstsizeedit"  CssClass="form-control" runat="server">
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                   <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="return validate2();" CssClass="btn btn-primary" />
                      <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
                </div>
                         </ContentTemplate>
         </asp:UpdatePanel>

    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
     <script type="text/javascript">
         window.updateSizeSelectionCount = function () {
             var $panel = $("#adminPscSizePanel");
             var $countEl = $("#sizeSelectionCount");
             if (!$panel.length || !$countEl.length) {
                 return;
             }
             var count = $panel.find("input[type='checkbox']:checked").length;
             $countEl.text(count + " selected");
         };

         function initPscSizeCounter() {
             window.updateSizeSelectionCount();

             $(document)
                 .off("change.pscSize click.pscSize", "#adminPscSizePanel, #adminPscSizePanel input[type='checkbox'], #adminPscSizePanel label")
                 .on("change.pscSize", "#adminPscSizePanel input[type='checkbox']", window.updateSizeSelectionCount)
                 .on("click.pscSize", "#adminPscSizePanel", function () {
                     window.setTimeout(window.updateSizeSelectionCount, 0);
                 });
         }

         $(initPscSizeCounter);

         if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
             var pscPrm = Sys.WebForms.PageRequestManager.getInstance();
             if (!pscPrm._pscSizeCounterHooked) {
                 pscPrm._pscSizeCounterHooked = true;
                 pscPrm.add_endRequest(initPscSizeCounter);
             }
         }
     </script>
</asp:Content>


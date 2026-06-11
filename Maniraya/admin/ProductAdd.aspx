<%@ Page Title="Add Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ProductAdd.aspx.cs" Inherits="admin_ProductAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
    <script type="text/javascript">

        function validate() {

            if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

                   alert('Select Category');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddcountry.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=txtstatename.ClientID%>").value == "") {

                   alert('Enter Product Name');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtstatename.ClientID%>").focus();
                   return false;
               }
            if (document.getElementById("<%=TxtAmount.ClientID%>").value == "") {

                alert('Enter CP');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtAmount.ClientID%>").focus();
                   return false;
            }
            if (document.getElementById("<%=TxtDP.ClientID%>").value == "") {

                alert('Enter DP');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtDP.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {

                alert('Enter Buissness Volume');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtBV.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {

                alert('Enter MRP');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtMRP.ClientID%>").focus();
                return false;
            }
           }
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        ;
                        break;
                    }
                }
            }
            // headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
       Add Product     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Add Product</li>
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
              <h3 class="box-title">Add Product</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                    <div class="col-md-3">
                <div class="form-group">
                  <label >Select Category</label>
                    <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                    <asp:ListItem Value="0"> Select Category</asp:ListItem>
                                </asp:DropDownList>
                </div>             
               </div>
                           <div class="col-md-3">
 <div class="form-group">
   <label >Select SubCategory</label>
     <asp:DropDownList ID="ddsubcategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddsubcategory_SelectedIndexChanged">
                     <asp:ListItem Value="0"> Select SubCategory</asp:ListItem>
                 </asp:DropDownList>
 </div>             
</div>
                    <div class="col-md-3">
                <div class="form-group">
                  <label >Product Name</label>
                 <asp:TextBox ID="txtstatename" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                       <div class="col-md-3">
                <div class="form-group">
                  <label >Amount</label>
                   <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                      </div>
                    <div class="row">
                    <div class="col-md-4">
                <div class="form-group">
                  <label >DP</label>
                   <asp:TextBox ID="TxtDP" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-4">
                <div class="form-group">
               
                   <label >Buisness Volume</label>
                   <asp:TextBox ID="TxtBV" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                      
                </div>             
               </div>
                        <div class="col-md-4">
                <div class="form-group">
                  <label >MRP</label>
                   <asp:TextBox ID="TxtMRP" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                      </div>
                      <div class="row">
                    
                    <div class="col-md-4">
                <div class="form-group">
               
                <label >GST</label>
                   <asp:TextBox ID="txtGst" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                      
                </div>             
               </div>
                           <div class="col-md-4">
                <div class="form-group">
                  <label >HSN CODE</label>
                   <asp:TextBox ID="txtHSN" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                           <div class="col-md-4">
                <div class="form-group">
               
                <label >BATCH NUMBER</label>
                   <asp:TextBox ID="txtBatch" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                      
                </div>             
               </div>
                      </div>

               

                  <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Upload Image1</label>
                                       <asp:FileUpload ID="ProductImageUpload" runat="server" />

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Upload Image2</label>
                                        <asp:FileUpload ID="ProductImageUpload2" runat="server" />

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Upload Image3</label>
                                        <asp:FileUpload ID="ProductImageUpload3" runat="server" />

                                    </div>
                                </div>
                       <div class="col-md-3">
     <div class="form-group">
         <label>Upload Image4</label>
         <asp:FileUpload ID="ProductImageUpload4" runat="server" />

     </div>
 </div>
                            </div>
                      <div class="row">
     <div class="col-md-12">
         
 <div class="form-group">
   <label >Short Description</label>                  
    <asp:TextBox ID="Txtshortdiscription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
 </div>        
</div>
    
       </div>
                   <div class="row">
                    <div class="col-md-12">
                        
                <div class="form-group">
                  <label >Description</label>                  
                    <cc1:Editor ID="TxtDescription" runat="server" />
                </div>        
               </div>
                   
                      </div>

                   <div class="row" >
       <div class="col-md-3">
           </div>
       <div class="col-md-6">
    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" >
               <Columns>       
                      <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);"/>
                        </HeaderTemplate>
                       <ItemTemplate>
                          <asp:CheckBox ID="ChkStatus" runat="server" onclick = "Check_Click(this)"/>
                       </ItemTemplate>
                   </asp:TemplateField>                               
                    <asp:TemplateField HeaderText="S.N.">
                      
                       <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Subcategory">
                       <ItemTemplate>
                           <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("SubCategoryName") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color">
     <ItemTemplate>
         <asp:Label ID="lblaccouColorName" runat="server" Text='<%#Eval("ColorName") %>'></asp:Label>
     </ItemTemplate>
 </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
     <ItemTemplate>
         <asp:Label ID="lblaccSizeName" runat="server" Text='<%#Eval("SizeName") %>'></asp:Label>
     </ItemTemplate>
 </asp:TemplateField>
                                                                             
               </Columns>
           </asp:GridView>
           </div>
      <div class="col-md-3">
           </div>
</div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
               
                     <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" OnClientClick="return validate();" runat="server" Text="Submit" OnClick="btnSubmit_Click1" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
              </div>
         
          </div>
            </div>
     
     </div>
      
      

        
      </ContentTemplate>
        <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
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
     <script type="text/javascript">


         function showModal() {
             $('#myModal').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#myModal').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }
        </script>
</asp:Content>


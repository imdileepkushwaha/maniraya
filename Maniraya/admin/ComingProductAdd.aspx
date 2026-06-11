<%@ Page Title="Add Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ComingProductAdd.aspx.cs" Inherits="ComingProductAdd" %>

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

                alert('Enter Amount');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtAmount.ClientID%>").focus();
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
        
    </script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
       Add Coming Product     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Add Coming Product</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add Coming Product</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Select Category</label>
                    <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select Category</asp:ListItem>
                                </asp:DropDownList>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Product Name</label>
                 <asp:TextBox ID="txtstatename" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                      </div>
                    <div class="row">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Amount</label>
                   <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
               
                   <label >Buisness Volume</label>
                   <asp:TextBox ID="TxtBV" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                      
                </div>             
               </div>
                      </div>
                      <div class="row">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >MRP</label>
                   <asp:TextBox ID="TxtMRP" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
               
                
                      
                </div>             
               </div>
                      </div>


                  <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Upload Image1</label>
                                       <asp:FileUpload ID="ProductImageUpload" runat="server" />

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Upload Image2</label>
                                        <asp:FileUpload ID="ProductImageUpload2" runat="server" />

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Upload Image3</label>
                                        <asp:FileUpload ID="ProductImageUpload3" runat="server" />

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


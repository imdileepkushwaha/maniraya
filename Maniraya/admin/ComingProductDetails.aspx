<%@ Page Title="Product Detail" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ComingProductDetails.aspx.cs" Inherits="ComingProductDetails" %>
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
               if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {

                   alert('Enter MRP');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtMrp.ClientID%>").focus();
                   return false;
               }
           }
        
    </script>

    

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
       Coming Product Detail     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Coming Product Detail</li>
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
              <h3 class="box-title">Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  
                <div class="form-group">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    
                                          <asp:Label ID="LblDescription" runat="server" Visible="false" Text='<%#Eval("Description") %>'></asp:Label>
                                          <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%#Eval("Image") %>'></asp:Label>
                                          <asp:Label ID="LblImage2" runat="server" Visible="false" Text='<%#Eval("Image2") %>'></asp:Label>
                                                <asp:Label ID="LblImage3" runat="server" Visible="false" Text='<%#Eval("Image3") %>'></asp:Label>
                                         <asp:Label ID="LblStatuschk" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
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
                                      <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename1" runat="server"  Text='<%#Eval("Amount") %>'></asp:Label>
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

                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>|
                                            <asp:LinkButton ID="lbldel" CommandName="del"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
              
                </div>             
             
                   
                       
            
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>
        <div id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Edit Product Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    Product Name
                         <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>

                                    <asp:TextBox runat="server" class="form-control" ID="txtstatenameedit"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    Amount                      
                          <asp:TextBox runat="server" class="form-control" ID="TxtAmountEdit" TextMode="Number"></asp:TextBox>
                                </div>
                                  <div class="form-group">
                                   Buisness Volume                     
                          <asp:TextBox runat="server" class="form-control" ID="TxtBV" TextMode="Number"></asp:TextBox>
                                </div>
                                   <div class="form-group">
                                   MRP                     
                          <asp:TextBox runat="server" class="form-control" ID="TxtMrp" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    Chage Product Image1                   
                           <asp:FileUpload ID="ProductImageUpload" runat="server" />
                                </div>
                                <div class="form-group">
                                    Product Image                   
                          <asp:Image ID="Image2" runat="server" Width="50px" Height="50px" />
                                </div>

                                <div class="form-group">
                                    Chage Product Image2                   
                           <asp:FileUpload ID="ProductImageUpload2" runat="server" />
                                </div>
                                <div class="form-group">
                                    Product Image                   
                          <asp:Image ID="Image3" runat="server" Width="50px" Height="50px" />
                                </div>

                                <div class="form-group">
                                    Chage Product Image3   
                                <asp:FileUpload ID="ProductImageUpload3" runat="server" />
                                </div>
                                <div class="form-group">
                                    Product Image                   
                          <asp:Image ID="Image4" runat="server" Width="50px" Height="50px" />
                                </div>

                                <div class="form-group">
                                    Description                   
                               <cc1:Editor ID="TxtDescription" runat="server" />
                                </div>
                                <div class="form-group">
                                    Status                   
                                <asp:DropDownList ID="DDLstStatusEdit" CssClass="form-control" runat="server">

                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Deactive</asp:ListItem>

                                </asp:DropDownList>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>


        <div id="DivPhotolarge" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">

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

                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
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
     <script type="text/javascript">


         function showModal1() {
             $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#DivPhotolarge').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }
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


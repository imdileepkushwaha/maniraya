<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="ProductSizeColorMaster.aspx.cs" Inherits="admin_ProductSizeColorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
      function validate() {

      if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

             alert('Select Category');
             // alert("Enter Rank No"); 
             document.getElementById("<%=ddcountry.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddsubcategory.ClientID%>").value == "0") {

             alert('Select SubCategory');
             // alert("Enter Rank No"); 
             document.getElementById("<%=ddsubcategory.ClientID%>").focus();
             return false;
         }
          if (document.getElementById("<%=ddlColor.ClientID%>").value == "0") {

      alert('Select Color');
      // alert("Enter Rank No"); 
           document.getElementById("<%=ddlColor.ClientID%>").focus();
           return false;
       }
      
     
          if (document.getElementById("<%=ddlSize.ClientID%>").value == "0") {

      alert('Select Size');
      // alert("Enter Rank No"); 
          document.getElementById("<%=ddlSize.ClientID%>").focus();
          return false;
      }
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
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
               <div class="row">
                   <div class="col-md-12">
                          <div class="box box-primary">
       <div class="box-header with-border">
           <h3 class="box-title">Product Color Size Master</h3>
       </div>

       <div class="box-body">
            <div class="form-group">
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
   <label >Subcategory</label>
  <asp:DropDownList ID="ddsubcategory" CssClass="form-control" runat="server" >
                  <asp:ListItem Value="0"> Select SubCategory</asp:ListItem>
              </asp:DropDownList>
 </div>             
</div>
                     <div class="col-md-3">
 <div class="form-group">
   <label >Color</label>
 <asp:DropDownList ID="ddlColor" CssClass="form-control"
    runat="server"
    />
 </div>             
</div>
                     <div class="col-md-3">
 <div class="form-group">
   <label >Size</label>
  <asp:DropDownList ID="ddlSize" CssClass="form-control"
    runat="server"
    />
 </div>             
</div>

           </div>
           </div>
                                 <div class="box-footer">
    
          <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                     <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
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
          <asp:Label ID="lblsizename" runat="server"  Text='<%# Eval("Sizename") %>'></asp:Label>
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

        <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
       
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
                                 <div id="myModal" class="modal fade">
              <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Edit</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                     Name
                      <asp:Label ID="lblcountryid" Visible="false"  runat="server" Text=""></asp:Label>
                <asp:Label ID="lblsubcate" CssClass="form-control"  runat="server" Text=""></asp:Label>
                    </div>
                        <div class="form-group">
                       Color
 
                            <asp:DropDownList ID="Ddlstcoloredit"  CssClass="form-control" runat="server">
                                 
                            </asp:DropDownList>
</div>
                </div>
                                        <div class="form-group">
                       Size
 
                            <asp:DropDownList ID="DDlstsizeedit"  CssClass="form-control" runat="server">
                              
                            </asp:DropDownList>
</div>
                </div>
                <div class="modal-footer">
                   <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="return validate2();" CssClass="btn btn-primary" />
                      <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                </div>
            </div>
        </div>
                </div>
                       </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
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


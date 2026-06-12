<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CategoryAdd.aspx.cs" Inherits="admin_CategoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">

         function validate() {
             // alert('sd');
             if (document.getElementById("<%=txtcountryname.ClientID%>").value == "") {

                   alert('Enter Catogory Name');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtcountryname.ClientID%>").focus();
                   return false;
               }
           }
           function validate2() {
               if (document.getElementById("<%=txtcountrynameedit.ClientID%>").value == "") {

                   alert('Enter Catogory Name');
                   document.getElementById("<%=txtcountrynameedit.ClientID%>").focus();
                   return false;
               }
               return true;
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
      <section class="content-header">
      <h1>
       Add Category     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Add Category</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="row">
        <!-- left column -->
        <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add Category</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                <div class="form-group">
                  <label >Category Name</label>
                   <asp:TextBox ID="txtcountryname" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               
              </div>
              <!-- /.box-body -->

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
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Categoryid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Category Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("CategoryName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
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
                  <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="categoryEditModalTitle" aria-hidden="true">
                  <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="categoryEditModalTitle">Edit Category</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body admin-product-form">
                        <asp:Label ID="lblcountryid" Visible="false" runat="server" Text=""></asp:Label>
                        <div class="form-group">
                            <label for="<%= txtcountrynameedit.ClientID %>">Category Name</label>
                            <div class="admin-input-group">
                                <span class="admin-input-icon"><i class="fa fa-folder-open"></i></span>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrynameedit" placeholder="Enter category name"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                       <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                       <asp:Button ID="btnUpdate" runat="server" Text="Update Category" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                    </div>
                </div>
            </div>
                    </div>
            </div>
      
      

        
      </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>



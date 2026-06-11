<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AddGalleryImage.aspx.cs" Inherits="admin_AddGalleryImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Event Add      
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Event</a></li>
        <li class="active">Event Add </li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

             <div class="row">
            <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Event Add</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
              <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                 <asp:HiddenField ID="HiddenField1" runat="server" />
                                 <asp:HiddenField ID="HiddenField2" runat="server" />
                                 <label>Title :</label>
                                 <asp:TextBox ID="txt_title" runat="server" CssClass="form-control" />
                             </div>
                         </div>

                  <div class="col-md-12" style="display:none;">
                             <div class="form-group">
                                 <label>Content :</label>
                                 <asp:TextBox ID="txt_content" runat="server" CssClass="form-control" />
                             </div>
                         </div>

                  <div class="col-md-12">
                             <div class="form-group">
                                 <label>Brouse Photo :</label>
                                 <asp:FileUpload ID="FileUpload1" size="1" tabindex="7" Style="width: 200px;" runat="server" />
                             </div>
                         </div>
                         
                     </div>
                    
                     
              <!-- /.box-body -->

              <div class="box-footer">
                   <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                  <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btn_update_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btndelete_Click" />
               
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
                <div class="form-group table-responsive">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID" 
                            OnSelectedIndexChanging="grid_SelectedIndexChanging">
                                <Columns>
                                     <asp:CommandField ShowSelectButton="true" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:CommandField>

                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Photo"><ItemTemplate>
                            <asp:Image ID="Image1" ImageUrl='<%#Eval("Img_Url") %>' runat="server" Height="60px" Width="60px" />
                        
                        </ItemTemplate></asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                </div>             
               
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>

            </div>

      </div>
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

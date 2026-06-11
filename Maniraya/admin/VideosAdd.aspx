<%@ Page Title="Add Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="VideosAdd.aspx.cs" Inherits="VideosAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
       Add Videos     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Add Videos</li>
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
              <h3 class="box-title">Add Videos</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Title</label>
                    <asp:TextBox ID="txttitle" CssClass="form-control" runat="server">
                                </asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Description</label>
                      <asp:TextBox ID="txtdescripition" CssClass="form-control" runat="server">
                                </asp:TextBox>
                </div>             
               </div>
                      </div>
                 <div class="row">
                    <div class="col-md-12">
                <div class="form-group">
                  <label >Video Url</label>
                    <asp:TextBox ID="txtvideourl" CssClass="form-control" runat="server">
                                </asp:TextBox>
                </div>             
               </div>
                   
                      </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
               
                     <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click1" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click1" />
              </div>
         
          </div>
            </div>
     
        <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Video Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  
                <div class="form-group">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Title">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("title") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Description">
                               <ItemTemplate>
                                       <asp:Label ID="lblid" runat="server"  Text='<%#Eval("description").ToString().Length > 100 ? Eval("description").ToString().Substring(0,100) + "..." :Eval("description").ToString() %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Video" >
                               <ItemTemplate>
                                   <asp:LinkButton ID="lnkph" runat="server" CommandName="photolarge"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                   <iframe class="img-fluid mySlides" src='<%# Eval("VideoUrl") %>' frameborder="0" allowfullscreen="" id="fitvid119463" scrolling="no" style="width:100%;max-width:200px;height:100%;min-height:80px;overflow:hidden"></iframe>
                                       </asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                               <asp:Label ID="lblstatus" runat="server"  Text='<%#Eval("Tstatus") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" OnClick="lnkedit"   CommandArgument='<%# Eval("id") %>'  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton> | 
                                             <asp:LinkButton ID="lblDel" OnClick="lnkdel"   CommandArgument='<%# Eval("id") %>'  runat="server"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
              
                </div>             
             
                   
                       
            
              </div>
              <!-- /.box-body -->

           
         
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


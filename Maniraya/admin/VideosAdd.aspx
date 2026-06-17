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
                                            <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" OnClick="lnkedit" CausesValidation="false" CommandArgument='<%# Eval("id") %>' runat="server" ToolTip="Edit video"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbldel" CssClass="admin-grid-delete-btn" OnClick="lnkdel" CausesValidation="false" OnClientClick="return showDeleteModal(this);" data-video-title='<%# Eval("title") %>' CommandArgument='<%# Eval("id") %>' runat="server" ToolTip="Delete video"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
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
      
      

        

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
<!-- Edit Video Modal -->
<div id="myModal" class="modal fade admin-video-edit-dialog admin-modal-scrollable" tabindex="-1" role="dialog" aria-labelledby="videoEditModalTitle" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="videoEditModalTitle"><i class="fa fa-video-camera"></i> Edit Video</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body admin-product-form admin-video-form">
                <p class="admin-modal-form-intro admin-modal-form-intro--visible">Update the video title, description, and embed URL shown on the website.</p>

                <div class="admin-form-section">
                    <h5 class="admin-form-section-title"><i class="fa fa-file-text-o"></i> Video Details</h5>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="<%= txtEdittitle.ClientID %>">Title</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-header"></i></span>
                                    <asp:TextBox ID="txtEdittitle" CssClass="form-control" runat="server" placeholder="Enter video title"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="<%= txtEditdescripition.ClientID %>">Description</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-align-left"></i></span>
                                    <asp:TextBox ID="txtEditdescripition" CssClass="form-control" runat="server" placeholder="Short description"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="admin-form-section admin-form-section-last">
                    <h5 class="admin-form-section-title"><i class="fa fa-link"></i> Video URL</h5>
                    <div class="form-group" style="margin-bottom: 0;">
                        <label for="<%= txtEditvideourl.ClientID %>">Embed / YouTube URL</label>
                        <div class="admin-input-group">
                            <span class="admin-input-icon"><i class="fa fa-youtube-play"></i></span>
                            <asp:TextBox ID="txtEditvideourl" CssClass="form-control" runat="server" placeholder="https://www.youtube.com/embed/..."></asp:TextBox>
                        </div>
                        <p class="admin-field-hint">Paste the full embed link so the video preview plays correctly in the grid.</p>
                    </div>
                </div>
            </div>
            <div class="modal-footer admin-modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update Video" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</div>

<!-- Delete Confirm Modal -->
<div id="deleteModal" class="modal fade admin-delete-confirm-dialog" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm" role="document">
        <div class="modal-content">
            <div class="modal-body admin-delete-confirm-body">
                <div class="admin-delete-confirm-icon" aria-hidden="true">
                    <i class="fa fa-trash"></i>
                </div>
                <h4 class="admin-delete-confirm-title" id="deleteModalLabel">Delete Video?</h4>
                <p class="admin-delete-confirm-text">You are about to remove <strong id="deleteVideoTitle">this video</strong> from the list.</p>
                <p class="admin-delete-confirm-note">This action cannot be undone.</p>
            </div>
            <div class="modal-footer admin-delete-confirm-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                <button type="button" class="btn btn-danger admin-delete-confirm-btn" onclick="executeDelete()">Delete</button>
            </div>
        </div>
    </div>
</div>

<script src="../bower_components/ckeditor/ckeditor.js"></script>
<script src="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
<script>
    $(function () {
        $('.textarea').wysihtml5();
    });
</script>
<script type="text/javascript">
    var deletePostBackReference = '';

    function showDeleteModal(btnRef) {
        deletePostBackReference = btnRef.href;
        var title = btnRef.getAttribute('data-video-title') || '';
        var $title = document.getElementById('deleteVideoTitle');
        if ($title) {
            $title.textContent = title || 'this video';
        }
        if (typeof showAdminModal === 'function') {
            showAdminModal('deleteModal');
        } else {
            $('#deleteModal').modal({ backdrop: 'static', keyboard: false, show: true });
        }
        return false;
    }

    function executeDelete() {
        if (deletePostBackReference) {
            if (typeof closeAdminModal === 'function') {
                closeAdminModal('deleteModal');
            }
            eval(deletePostBackReference.replace('javascript:', ''));
        }
    }

    function showModal() {
        if (typeof showAdminModal === 'function') {
            showAdminModal('myModal');
        } else {
            $('#myModal').modal({ backdrop: 'static', keyboard: false, show: true });
        }
    }

    function Closepopup() {
        if (typeof closeAdminModal === 'function') {
            closeAdminModal('myModal');
        } else {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    }
</script>
</asp:Content>


<%@ Page Title="Add News" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="NewsAdd.aspx.cs" Inherits="admin_NewsAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">

         function validate() {
             // alert('sd');
             if (document.getElementById("<%=txtnews.ClientID%>").value == "") {

                alert('Enter News');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtnews.ClientID%>").focus();
                return false;
            }
        }
        function validate2() {
            // alert('sd');
            if (document.getElementById("<%=txtnewsedit.ClientID%>").value == "") {

                alert('Enter News');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtnewsedit.ClientID%>").focus();
                   return false;
               }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
       News Master   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#"> News</a></li>
        <li class="active"> News Master</li>
      
      </ol>
    </section>   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add News</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

                 <div class="box-body">
                     <div class="row">
                         <div class="col-md-2">News</div>
                            
                        
                         <div class="col-md-10">
                             <div class="form-group">
                                
                                  <asp:TextBox ID="txtnews" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     
                 </div>
              <!-- /.box-body -->

              <div class="box-footer">                
                    <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" runat="server" Text="Cancel" />  
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
                     <div class="row">
                       
                            
                        
                         <div class="col-md-12">
                             <div class="form-group">
                                
                                    <div class="table-responsive">

                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("newsid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="News">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnews" runat="server" Text='<%#Eval("newsdetail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                              <asp:LinkButton ID="lbDelete" CommandName="mydel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-remove" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                             </div>
                         </div>
                     </div>
                     
                 </div>
              <!-- /.box-body -->

            
         
          </div>
            </div>

                </div>



            

            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit News Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                News
                            <asp:Label ID="lblnewsid" Visible="false" runat="server" Text=""></asp:Label>

                                <asp:TextBox runat="server" class="form-control" ID="txtnewsedit" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   

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




<%@ Page Title="Withdrawl Request" Language="C#" MasterPageFile="Masterpage.master" AutoEventWireup="true" CodeFile="PhotoUpload.aspx.cs" Inherits="PhotoUpload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {

             if (!confirm('You can update your photo only once.Are you sure want to update?')) {
                 return false;
             }
        <%--    if (document.getElementById("<%=txtoldpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Old Password');
                document.getElementById("<%=txtoldpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter New Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }--%>

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1 style="color:white">
    Photo Upload  
      </h1>
      <ol class="breadcrumb">
      <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home > </a></li>
        <li><a href="#">My Profile > </a></li>
          <li class="active">Photo Upload</li>
      
      </ol>
    </section>   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row" style="color:white" >
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Photo Upload </h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                     <br />
                       <br />
                         <div class="row">
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Change Image :</label>
                                 <asp:FileUpload ID="ImageUpload" runat="server" />    
                             </div>
                         </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                  <div class="box-footer" id="div_update" runat="server" visible="false">
                               <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                      </div>
                                 <div class="box-footer" id="div_noupdate" runat="server" visible="false"><span style="float:right;font-size:20px;color:red;"><i>You cannot update photo.Please contact admin.</i></span></div>
                        </div>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <asp:ImageButton ID="ImageShow" runat="server" Width="100px" Height="100px" OnClick="ImageShow_Click" />
                             </div>
                         </div>
                     </div>
                        
                       </div>
                         <div class="box-footer">
                        
             

                               
                    
              </div>


                  
              </div>
              </div>
                  </div>


                <div id="DivPhotolarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content" style="margin-top: 10%;">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImageLarge" runat="server" Width="570px" Height="400px" />
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
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
</asp:Content>




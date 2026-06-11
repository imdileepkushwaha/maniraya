<%@ Page Title="" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="TransactionPass.aspx.cs" Inherits="user_TransactionPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
       Transaction Password
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">User</a></li>
        <li class="active">Transaction Password </li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>         
           <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="border-width:0px;padding: 10px;position:fixed;top:25%;left:40%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
          <asp:UpdatePanel runat="server" ID="uplMaster" >
        <ContentTemplate>
        <div class="row">
          <div class="col-md-12">
               <div class="box box-primary" >
            <div class="box-header with-border">
              <h3 class="box-title">Transaction Password</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Password :</label>
                                  <asp:TextBox ID="Txtapssword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                

                                   <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                
                             </div>
                         </div>
                     </div>


                   </div>
              </div>
            
          </div>
                 <div id="Divotp" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">OTP Confirmation</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                               OTP                           
                                <asp:TextBox runat="server" class="form-control" ID="TxtOtp"></asp:TextBox>
                                <asp:Label ID="LblMessages" runat="server" Text="Invalid OTP please enter valid OTP...!" CssClass="bg-red-active" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                             <asp:Button ID="BtnConfirm" runat="server" Text="Submit OTP" OnClientClick="return validate2();" CssClass="btn btn-success" OnClick="BtnConfirm_Click"  />
                            <asp:Button ID="BtnResend" runat="server" Text="Resend OTP" CssClass="btn btn-info" OnClick="BtnResend_Click" />
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
                        </div>
                        <div class="row" id="div2" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="alert alert-success"> <strong>Success!</strong> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>. </div>
                            </div>
                        </div>
                       
                    </div>
                </div>
            </div>
             </div>
            </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
     <script type="text/javascript">
       
         function showModal12() {
             $('#Divotp').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup1() {
             $('#Divotp').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();
         }
    </script>
</asp:Content>


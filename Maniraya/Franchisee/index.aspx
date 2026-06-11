<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8">
    <title>  <%= clsUtility.ProjectName %> | Log in</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css"/>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css"/>
  <!-- Ionicons -->
  <link rel="stylesheet" href="../bower_components/Ionicons/css/ionicons.min.css"/>
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/AdminLTE.min.css"/>
  <!-- iCheck -->
  <link rel="stylesheet" href="../plugins/iCheck/square/blue.css"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
      <script>
          function validate2() {
              if (document.getElementById("<%=TxtOtp.ClientID%>").value == "") {

             alert('Enter OTP');
             document.getElementById("<%=TxtOtp.ClientID%>").focus();
                return false;
            }
          }
          function validate3() {
              if (document.getElementById("<%=txtuserid.ClientID%>").value == "") {

                    alert('Enter User Id');
                    document.getElementById("<%=txtuserid.ClientID%>").focus();
             return false;
         }
     }
         </script>
</head>
<body  class="hold-transition login-page">
      <form id="form1" runat="server">
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
     <div class="login-box">
      <div class="login-logo">
           <a href="#"><b>  <%= clsUtility.ProjectName %> </b></a>
    <%--  <a href="#"><b><img src="../img/logo.png"></b></a>--%>
      </div><!-- /.login-logo -->
      <div class="login-box-body">
        <p class="login-box-msg">Franchisee Login</p>
      
          <div class="form-group has-feedback">      
             <asp:TextBox ID="txtusername" CssClass="form-control" runat="server" placeholder="Username"></asp:TextBox>
            <span class="glyphicon glyphicon-user form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
              <asp:TextBox ID="txtpassword" TextMode="Password" CssClass="form-control" placeholder="Password"  runat="server"></asp:TextBox>
          
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
          
            <div class="col-xs-4">
          <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-primary btn-block btn-flat" OnClick="btnLogin_Click"/>
            
            </div>
              <div class="col-xs-8">
                     <a onclick="showModal();" style="cursor:pointer;">Forgot password?</a>
                  </div><!-- /.col -->
          </div>
        
      </div><!-- /.login-box-body -->
    </div><!-- /.login-box -->
         
      <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Forgot Password</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                User Id                           
                                <asp:TextBox runat="server" class="form-control" ID="txtuserid"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSend" runat="server" Text="Submit" OnClientClick="return validate3();" CssClass="btn green" OnClick="btnSend_Click" />
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
                        </div>
                        <div class="row" id="divsuccess" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="alert alert-success"> <strong>Success!</strong> <asp:Label ID="lblmessage" runat="server" Text="Label"></asp:Label>. </div>
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
                </ContentTemplate>
    </asp:UpdatePanel>
           </form>
    <!-- jQuery 2.1.3 -->
   <script src="../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="../plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
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
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyRegistration.aspx.cs" Inherits="VerifyRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8">
       <link href="../css/radio/style.css" rel="stylesheet" />
    <title>RAXTAN MARKETING PVT LTD | Log in</title>
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
      <script type="text/javascript">

         
          
    </script>
</head>
<body  class="hold-transition login-page">
      <form id="form1" runat="server">
     <!-- /.login-box -->
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="border-width:0px;padding: 10px;position:fixed;top:25%;left:40%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <div class="row" style="margin-top:100px;">
        <div class="col-md-3">
            </div>
                <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Verify Registration</h3>
                        </div>
                        <div class="box-body">
                            <div class="row" id="divmob" runat="server" visible="false">
                                <div class="form-group">
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Enter OTP Sent On Mobile</label>
                                        <asp:TextBox ID="txtmobotp" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnverifymob" CssClass="btn btn-primary" runat="server" Text="Verify" OnClick="btnverifymob_Click"  />
                                        &nbsp;
                                        <asp:Button ID="btnresendmobotp" CssClass="btn btn-primary" runat="server" Text="Resend OTP" OnClick="btnresendmobotp_Click"  />
                                        &nbsp;
                                        <asp:Label ID="lblmobstatus" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divemail" runat="server" visible="false">
                                <div class="form-group">
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Enter OTP Sent On Email</label>
                                        <asp:TextBox ID="txtemailotp"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <br />
                                       <asp:Button ID="btnverifyemail"  CssClass="btn btn-primary" runat="server" Text="Verify" OnClick="btnverifyemail_Click"  />
                                        &nbsp;
                                        <asp:Button ID="btnresendemailotp" CssClass="btn btn-primary" runat="server" Text="Resend OTP" OnClick="btnresendemailotpClick"  />
                                        &nbsp;
                                        <asp:Label ID="lblemailstatus" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                       
                    </div>
                </div>
            </div>
     </div>
                 <div class="col-md-2">
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
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <meta charset="UTF-8">
    <title>Maniraya Marketing Pvt. Ltd. | Log in</title>
    <meta charset="UTF-8">
		<meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=0'>
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="Description" content="Xino - Laravel Admin & Dashboard Template">
		<meta name="Author" content="Spruko Technologies Private Limited">
		<meta name="Keywords" content="cryptocurrency, dashboard, admin, crypto, ico, bootstrap admin template, admin template, bootstrap dashboard template, crypto dashboard, cryptocurrency dashboard, ico dashboard, crypto admin, dashboard cryptocurrency, cryptocurrency trading dashboard, crypto dashboard template "/>

		<!-- Title -->
     

        <!-- Favicon -->
        <link rel="icon" href="img/logo.png" type="image/x-icon"/>

		<!-- Icons css -->
		<link href="assets/css/icons.html" rel="stylesheet">

		<!---Fontawesome css-->
		<link href="assets/plugins/fontawesome-free/css/all.min.css" rel="stylesheet">

		<!---Ionicons css-->
		<link href="assets/plugins/ionicons/css/ionicons.min.css" rel="stylesheet">

		<!---Typicons css-->
		<link href="assets/plugins/typicons.font/typicons.css" rel="stylesheet">

		<!---Feather css-->
		<link href="assets/plugins/feather/feather.css" rel="stylesheet">

		<!---Falg-icons css-->
        <link href="assets/plugins/flag-icon-css/css/flag-icon.min.css" rel="stylesheet">

		
		<!---Style css-->
		<link href="assets/css/style.css" rel="stylesheet">
		<link href="assets/css/style-dark.css" rel="stylesheet">

        <!-- skin css-->
        <link href="assets/css/skin-modes.css" rel="stylesheet">

		<!--- Animations css-->
		<link href="assets/css/animate.css" rel="stylesheet">

        <!-- Switcher css -->
		<link href="assets/switcher/css/switcher.css" rel="stylesheet">
		<link rel="stylesheet" href="assets/switcher/demo.css">
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
	
	
	<script>
	    // Change the type of input to password or text
	    function Toggle() {
	        let temp = document.getElementById("<%=txtpassword.ClientID%>");
             
            if (temp.type === "password") {
                temp.type = "text";
            }
            else {
                temp.type = "password";
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
           <div class="my-auto page page-h">
			<div class="main-signin-wrapper" >
				<div class="main-card-signin d-md-flex" style="background-color:#fff; color:#000;">
				
				<div class="p-5 wd-md-100p">
					<div class="main-signin-header">
						
                        <h2 class="text-center" style="color:#000">Maniraya Marketing Pvt. Ltd.</h2><br />
						<h4  style="color:#1fb159">Please sign in to continue</h4>
						
							<div class="form-group">
								<label>User ID</label>
                                 <asp:TextBox ID="txtusername" class="form-control" runat="server" placeholder="Username"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Password</label>  <asp:TextBox ID="txtpassword" TextMode="Password" CssClass="form-control" placeholder="Password"  runat="server"></asp:TextBox><br><input type="checkbox" onclick="Toggle()">
    <strong>Show Password</strong>
 
							</div>
                        
                         <asp:Button runat="server" ID="btnLogin" Text="Sign In" class="btn btn-main-primary btn-block" OnClick="btnLogin_Click"/>
                     
						
					</div>
					<div class="main-signin-footer mt-3 mg-t-5">
						<p>   <a onclick="showModal();" style="cursor:pointer;">Forgot password?</a></p>
						<p style="color:black">Don't have an account? <a href="https://maniraya.com/Register.aspx" style="color:#1fb159">Create an Account</a></p>
					</div>
				</div>
			</div>
			</div>
		</div>

            <div class="row" style="display:none">
               
                 <div> <div class="login-box" >
      <div class="login-logo">
            <%-- <a href="#"><b><%= clsUtility.ProjectName %></b></a>--%>

      </div><!-- /.login-logo -->
      <div class="login-box-body">
           <a href="#"><b><img src="img/spinelogo.png" style="width:80%"></b></a>
        <p class="login-box-msg">User Login</p>
      
          <div class="form-group has-feedback">      
            
            <span class="glyphicon glyphicon-user form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
             
          
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
          
            <div class="col-xs-4">
         
            
            </div>
              <div class="col-xs-8">
                  Forgot password?</a>
                  </div><!-- /.col -->
          </div>
        
      </div><!-- /.login-box-body -->
    </div></div>

            </div>
    <!-- /.login-box -->
         
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

    <script src="assets/plugins/jquery/jquery.min.js"></script>

		<!-- Bootstrap Bundle js -->
		<script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>

		<!-- Ionicons js -->
		<script src="assets/plugins/ionicons/ionicons.js"></script>

		<!-- Moment js -->
		<script src="assets/plugins/moment/moment.js"></script>

		<!-- eva-icons js -->
		<script src="assets/js/eva-icons.min.js"></script>

		<!-- Rating js-->
		<script src="assets/plugins/rating/jquery.rating-stars.js"></script>
		<script src="assets/plugins/rating/jquery.barrating.js"></script>

		
		<!-- Custom js -->
		<script src="assets/js/custom.js"></script>

        <!-- Switcher js -->
		<script src="assets/switcher/js/switcher.js"></script>
</body>
</html>
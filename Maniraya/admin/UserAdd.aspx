<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs" Inherits="admin_UserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtsponserid.ClientID%>").value == "") {

                alert('Enter Sponser Id');
                document.getElementById("<%=txtsponserid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {

                alert('Enter Name');
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtmobile.ClientID%>").value == "") {

                alert('Enter Mobile');
                document.getElementById("<%=txtmobile.ClientID%>").focus();
                return false;
            }
            if (validatephonenumber(document.getElementById("<%=txtmobile.ClientID%>").value) == false) {
                alert('Invalid Mobile No');
                document.getElementById("<%=txtmobile.ClientID%>").focus();
                return false;
            }
            //if (document.getElementById("<%=txtemail.ClientID%>").value == "") {

            // alert('Enter Email');
            //document.getElementById("<%=txtemail.ClientID%>").focus();
            //  return false;
            //}

            //if (validateemail(document.getElementById("<%=txtemail.ClientID%>").value) == false) {

            //alert('Invalid Email ID');
            //document.getElementById("<%=txtemail.ClientID%>").focus();
            //return false;
            //}
            if (document.getElementById("<%=ddgender.ClientID%>").value == "0") {

                alert('Select Gender');
                document.getElementById("<%=ddgender.ClientID%>").focus();
                return false;
            }

            // if (document.getElementById("<%=txtaddress.ClientID%>").value == "") {

            //     alert('Enter Address');
            //     document.getElementById("<%=txtaddress.ClientID%>").focus();
            //     return false;
            // }

            //if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

            //    alert('Select Country');
            //    document.getElementById("<%=ddcountry.ClientID%>").focus();
            //    return false;
           // }
          //  if (document.getElementById("<%=ddstate.ClientID%>").value == "0") {

         //       alert('Select State');
         //       document.getElementById("<%=ddstate.ClientID%>").focus();
         //       return false;
        //    }
        //    if (document.getElementById("<%=ddcity.ClientID%>").value == "0") {

         //       alert('Select City');
          //      document.getElementById("<%=ddcity.ClientID%>").focus();
          //     return false;
        //   }
        //   if (document.getElementById("<%=ddcity.ClientID%>").value == "") {

       //         alert('Select City');
       //         document.getElementById("<%=ddcity.ClientID%>").focus();
       //         return false;
       //     }
            //      if (document.getElementById("<%=txtareaname.ClientID%>").value == "") {

            //          alert('Enter Area');
            //           document.getElementById("<%=txtareaname.ClientID%>").focus();
            //          return false;
            //    }



            //    if (document.getElementById("<%=txtpincode.ClientID%>").value == "") {

            //        alert('Enter Pincode');
            //         document.getElementById("<%=txtpincode.ClientID%>").focus();
            //         return false;
            //     }
            if (document.getElementById("<%=txtUserpassword.ClientID%>").value == "") {

                alert('Enter Password');
                document.getElementById("<%=txtUserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                alert('Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtUserpassword.ClientID%>").value != document.getElementById("<%=txtconfirmpassword.ClientID%>").value) {

                alert('Password Not Match');
                document.getElementById("<%=txtUserpassword.ClientID%>").focus();
                return false;
            }
            return true;
        }


        function validatephonenumber(inputtxt) {
            var phoneno = /^([6-9]{1})([0-9]{9})$/;
            if (inputtxt.match(phoneno)) {
                return true;
            }
            else {
                return false;
            }
        }

        function validateemail(inputtxt) {
            var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (inputtxt.match(email)) {
                return true;
            }
            else {
                return false;
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add User</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">User</a></li>
            <li class="active">Add User</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div class="admin-loading-overlay">
                <div class="admin-loading-spinner">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading..." ToolTip="Loading..." />
                    <span>Please wait...</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add User</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Enter sponsor details and personal information to register a new user. Fields marked in each section are required for submission.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-user-plus"></i> Sponsor Details</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtsponserid.ClientID %>">Sponsor ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txtsponserid" AutoPostBack="true" OnTextChanged="txtsponserid_TextChanged" CssClass="form-control" runat="server" placeholder="Enter sponsor ID" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtsponsername.ClientID %>">Sponsor Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtsponsername" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto-filled from sponsor ID" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="display: none">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnFree" runat="server" Text="Free Registration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnFree_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnEpin" runat="server" Text="E-Pin Registration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnEpin_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>

                            <asp:Panel ID="pnlpin" runat="server">
                                <div class="admin-form-section admin-user-pin-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-ticket"></i> Pin Details</h5>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= DDLstPlan.ClientID %>">Select Plan</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-list-alt"></i></span>
                                                    <asp:DropDownList ID="DDLstPlan" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLstPlan_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= ddepin.ClientID %>">Select E-Pin</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-key"></i></span>
                                                    <asp:DropDownList ID="ddepin" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddepin_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtamount.ClientID %>">Amount</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                    <asp:TextBox ID="txtamount" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto-filled from E-Pin" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="row" style="display: none">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnLeft" runat="server" Text="Left" GroupName="B" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnRight" runat="server" Text="Right" GroupName="B" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-address-card-o"></i> Personal Information</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtname.ClientID %>">Full Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user-circle"></i></span>
                                                <asp:TextBox ID="txtname" CssClass="form-control" runat="server" placeholder="Enter full name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtmobile.ClientID %>">Mobile</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-mobile"></i></span>
                                                <asp:TextBox ID="txtmobile" MaxLength="10" placeholder="10 digit mobile number (no +91 or 0)" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtemail.ClientID %>">Email</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-envelope-o"></i></span>
                                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" placeholder="Enter email address" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddgender.ClientID %>">Gender</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-venus-mars"></i></span>
                                                <asp:DropDownList ID="ddgender" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="display: none;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Date of Birth</label>
                                        <div class="admin-dob-grid">
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                <asp:DropDownList ID="ddlYear" CssClass="form-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="admin-input-group">
                                                <asp:DropDownList ID="ddlMonth" CssClass="form-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="admin-input-group">
                                                <asp:DropDownList ID="ddlDay" CssClass="form-control" ToolTip="Day" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="display: none;">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="<%= txtaddress.ClientID %>">Address</label>
                                        <div class="admin-input-group admin-textarea-group">
                                            <span class="admin-input-icon"><i class="fa fa-map-marker"></i></span>
                                            <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Enter address" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="display: none;">
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddcountry.ClientID %>">Country</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-globe"></i></span>
                                            <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Country</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddstate.ClientID %>">State</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-map"></i></span>
                                            <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select State</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="display: none;">
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddcity.ClientID %>">City</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-building-o"></i></span>
                                            <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select City</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtareaname.ClientID %>">Area</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-location-arrow"></i></span>
                                            <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" placeholder="Enter area" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="otherPnl" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtOtherCity.ClientID %>">Other City</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-building"></i></span>
                                                <asp:TextBox ID="TxtOtherCity" CssClass="form-control" runat="server" placeholder="Enter city name" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="display: none;">
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtpincode.ClientID %>">Pincode</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-map-pin"></i></span>
                                            <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server" placeholder="Enter pincode" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-lock"></i> Password</h5>
                                <p class="admin-section-hint">Set a secure login password for the new user account.</p>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtUserpassword.ClientID %>">Password</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-key"></i></span>
                                                <asp:TextBox ID="txtUserpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Enter password" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtconfirmpassword.ClientID %>">Confirm Password</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-check-circle"></i></span>
                                                <asp:TextBox ID="txtconfirmpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Re-enter password" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div style="display: none;">
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-credit-card"></i> PAN Card Details</h5>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= panUpload.ClientID %>">PAN Card Upload</label>
                                                <asp:FileUpload ID="panUpload" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= txtPanNumber.ClientID %>">PAN Number</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-id-card"></i></span>
                                                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control" placeholder="Enter PAN number" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Register User" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div_FDetails" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="userVerifyModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="userVerifyModalTitle">Verify User</h4>
                            <button type="button" class="close" onclick="ClosesFranchiseepopup();" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="divmob" runat="server">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="<%= txtmobotp.ClientID %>">Enter OTP sent on mobile</label>
                                        <asp:TextBox ID="txtmobotp" CssClass="form-control" runat="server" placeholder="Enter mobile OTP" />
                                    </div>
                                </div>
                                <div class="col-md-8 col-sm-7">
                                    <asp:Button ID="btnresendmobotp" CssClass="btn btn-default btn-sm" runat="server" Text="Resend OTP" OnClick="btnresendmobotp_Click" />
                                    <asp:Label ID="lblmobstatus" runat="server" CssClass="admin-otp-status"></asp:Label>
                                </div>
                                <div class="col-md-4 col-sm-5 text-right">
                                    <asp:Button ID="btnverifymob" CssClass="btn btn-primary" runat="server" Text="Verify" OnClick="btnverifymob_Click" />
                                </div>
                            </div>
                            <div class="row" id="divemail" runat="server" style="margin-top: 16px;">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="<%= txtemailotp.ClientID %>">Enter OTP sent on email</label>
                                        <asp:TextBox ID="txtemailotp" CssClass="form-control" runat="server" placeholder="Enter email OTP" />
                                    </div>
                                </div>
                                <div class="col-md-8 col-sm-7">
                                    <asp:Button ID="btnresendemailotp" CssClass="btn btn-default btn-sm" runat="server" Text="Resend OTP" OnClick="btnresendemailotpClick" />
                                    <asp:Label ID="lblemailstatus" runat="server" CssClass="admin-otp-status"></asp:Label>
                                </div>
                                <div class="col-md-4 col-sm-5 text-right">
                                    <asp:Button ID="btnverifyemail" CssClass="btn btn-primary" runat="server" Text="Verify" OnClick="btnverifyemail_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <%--    <script type="text/javascript">
        function pageLoad(sender, args) {
            $('#' + '<%=txtdateofbirth.ClientID%>').datepicker({
                format: 'dd M yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
    </script>--%>


    <script type="text/javascript">


        function showFranchiseeModal() {
            $('#Div_FDetails').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosesFranchiseepopup() {
            $('#Div_FDetails').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>


</asp:Content>


<%@ Page Title="View Profile" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="assets/css/user-profile.css?v=2" rel="stylesheet" />

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

            if (document.getElementById("<%=txtemail.ClientID%>").value == "") {

                alert('Enter Email');

                document.getElementById("<%=txtemail.ClientID%>").focus();

                return false;

            }

            if (document.getElementById("<%=txtaddress.ClientID%>").value == "") {

                alert('Enter Address');

                document.getElementById("<%=txtaddress.ClientID%>").focus();

                return false;

            }

            if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

                alert('Select Country');

                document.getElementById("<%=ddcountry.ClientID%>").focus();

                return false;

            }

            if (document.getElementById("<%=ddstate.ClientID%>").value == "0") {

                alert('Select State');

                document.getElementById("<%=ddstate.ClientID%>").focus();

                return false;

            }

            if (document.getElementById("<%=ddcity.ClientID%>").value == "0") {

                alert('Select City');

                document.getElementById("<%=ddcity.ClientID%>").focus();

                return false;

            }

            if (document.getElementById("<%=txtareaname.ClientID%>").value == "") {

                alert('Enter Area');

                document.getElementById("<%=txtareaname.ClientID%>").focus();

                return false;

            }

        }



        function fnprint() {

            $("#btnprint").hide();

            var divElements = document.getElementById("div_print").innerHTML;

            var oldPage = document.body.innerHTML;

            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";

            window.print();

            document.body.innerHTML = oldPage;

            window.location = "UserProfile.aspx";

        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">

    <section class="content-header">

        <h1>View Profile</h1>

        <ol class="breadcrumb">

            <li><a href="Dashboard.aspx">Home</a></li>

            <li><a href="UserProfile.aspx">My Profile</a></li>

            <li class="active">View Profile</li>

        </ol>

    </section>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">

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

            <div class="profile-page" id="div_print">

                <div class="profile-hero">

                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-user"></i></div>

                    <div class="profile-hero-info">

                        <h2><%= Session["username"] %></h2>

                        <p class="profile-hero-meta">Member ID: <strong><%= Session["userid"] %></strong></p>

                    </div>

                    <div class="profile-hero-actions">

                        <a href="UserEdit.aspx" class="profile-btn profile-btn-primary"><i class="fa fa-pencil"></i> Edit Profile</a>

                        <a href="CHangePassword.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-lock"></i> Change Password</a>

                    </div>

                </div>



                <div class="profile-sections">

                    <div class="box box-primary">

                        <div class="box-header with-border">

                            <h3 class="box-title">Personal Details</h3>

                        </div>

                        <div class="box-body profile-form-grid">

                            <p class="profile-subsection-title"><i class="fa fa-id-card"></i> Sponsor &amp; Identity</p>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-hashtag"></i> Sponsor ID</label>

                                        <asp:TextBox ID="txtsponserid" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-user"></i> Sponsor Name</label>

                                        <asp:TextBox ID="txtsponsername" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-user-circle"></i> Full Name</label>

                                        <asp:TextBox ID="txtname" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-venus-mars"></i> Gender</label>

                                        <asp:DropDownList ID="ddgender" Enabled="false" CssClass="form-control" runat="server">

                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>

                                            <asp:ListItem Value="Male">Male</asp:ListItem>

                                            <asp:ListItem Value="Female">Female</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>



                            <p class="profile-subsection-title"><i class="fa fa-phone"></i> Contact</p>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-mobile"></i> Mobile</label>

                                        <asp:TextBox ID="txtmobile" Enabled="false" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-envelope"></i> Email</label>

                                        <asp:TextBox ID="txtemail" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>



                            <p class="profile-subsection-title"><i class="fa fa-map-marker"></i> Location</p>

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="form-group">

                                        <label><i class="fa fa-home"></i> Address</label>

                                        <asp:TextBox ID="txtaddress" Enabled="false" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-map"></i> State</label>

                                        <asp:DropDownList ID="ddstate" AutoPostBack="true" Enabled="false" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">

                                            <asp:ListItem Value="0">Select State</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-building"></i> City</label>

                                        <asp:DropDownList ID="ddcity" Enabled="false" CssClass="form-control" runat="server">

                                            <asp:ListItem Value="0">Select City</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>



                            <div class="row" style="display:none">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label>Select Country</label>

                                        <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">

                                            <asp:ListItem Value="0">Select Country</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>

                            <div class="row" style="display:none">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label>Area Name</label>

                                        <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label>Pincode</label>

                                        <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label>Date of Birth</label>

                                        <asp:TextBox ID="txtdateofbirth" CssClass="form-control form_date" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>



                    <div class="box box-primary">

                        <div class="box-header with-border">

                            <h3 class="box-title">Nominee Details</h3>

                        </div>

                        <div class="box-body profile-form-grid">

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-user"></i> Nominee Name</label>

                                        <asp:TextBox ID="txtnomineename" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-users"></i> Nominee Relation</label>

                                        <asp:TextBox ID="txtnomineerelation" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>



                    <div class="box box-primary">

                        <div class="box-header with-border">

                            <h3 class="box-title">Bank Details</h3>

                        </div>

                        <div class="box-body profile-form-grid">

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-user"></i> Account Holder Name</label>

                                        <asp:TextBox ID="txtaccountholdername" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-wallet"></i> Withdrawal Wallet Address</label>

                                        <asp:TextBox ID="txtaccountno" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-code"></i> IFSC Code</label>

                                        <asp:TextBox ID="txtifsccode" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-id-card"></i> PAN Number</label>

                                        <asp:TextBox ID="txtpan" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label><i class="fa fa-university"></i> Bank</label>

                                        <asp:DropDownList ID="ddbank" CssClass="form-control" Enabled="false" runat="server"></asp:DropDownList>

                                    </div>

                                </div>

                                <div class="col-md-6" style="display:none">

                                    <div class="form-group">

                                        <label>Branch</label>

                                        <asp:TextBox ID="txtbranchname" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>

                        <div class="box-footer">

                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false" />

                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" runat="server" Text="Cancel" Visible="false" />

                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">

    <script type="text/javascript">

        $('.form_date').datepicker({

            format: 'dd/mm/yyyy',

        }).on('changeDate', function (ev) {

            $(this).datepicker('hide');

        });

    </script>

    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">

        Sys.Application.add_load(LoadHandler);

        function LoadHandler() {

            $('.form_date').datepicker({

                format: 'dd/mm/yyyy',

            }).on('changeDate', function (ev) {

                $(this).datepicker('hide');

            });

        }

    </script>

</asp:Content>



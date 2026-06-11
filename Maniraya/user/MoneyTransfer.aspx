<%@ Page Title="Money Transfer" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="MoneyTransfer.aspx.cs" Inherits="user_MoneyTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validatelogin() {
            if (document.getElementById("<%=txtmobilelogin.ClientID%>").value == "") {
                alert('Enter Mobile No');
                document.getElementById("<%=txtmobilelogin.Text%>").focus();
                return false;
            }
        }
        function validatecreate() {
            if (document.getElementById("<%=txtmobilecreate.ClientID%>").value == "") {
                 alert('Enter Mobile No');
                 document.getElementById("<%=txtmobilecreate.Text%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtnamecreate.ClientID%>").value == "") {
                 alert('Enter Sender Name');
                 document.getElementById("<%=txtnamecreate.Text%>").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function validateBeneficiary() {
            if (document.getElementById("<%=txtbenaccountno.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtbenaccountno.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddbank.ClientID%>").value == "0") {
                alert('Select Bank');
                document.getElementById("<%=ddbank.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtifsccode.ClientID%>").value == "") {
                alert('Enter IFSC Code');
                document.getElementById("<%=txtifsccode.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtbeneficiaryname.ClientID%>").value == "") {
                alert('Enter Beneficiary Name');
                document.getElementById("<%=txtbeneficiaryname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtbenegiciarymobile.ClientID%>").value == "") {
                alert('Enter Beneficiary Mobile');
                document.getElementById("<%=txtbenegiciarymobile.ClientID%>").focus();
                return false;
            }
        }
        function validateMR() {
            if (document.getElementById("<%=txtamount.ClientID%>").value == "") {
                alert('Enter Amount');
                document.getElementById("<%=txtamount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtbeneficiaryaccountMR.ClientID%>").value == "") {
                alert('Enter Beneficiary Account No');
                document.getElementById("<%=txtbeneficiaryaccountMR.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtbeneficiarynameMR.ClientID%>").value == "") {
                alert('Enter Beneficiary Name');
                document.getElementById("<%=txtbeneficiarynameMR.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <style>
        .dropdown-menu > li > a {
            text-transform: capitalize;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Money Transfer   
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Money Transfer </a></li>
            <li class="active">Money Transfer </li>

        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divLogin" runat="server">
                <div class="row">
                    <div class="col-md-12">

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Sender Login</h3>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">Enter Mobile No</div>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmobilelogin" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnLogin" OnClick="btnLogin_Click" OnClientClick="return validatelogin();" CssClass="btn btn-primary" runat="server" Text="Login" />
                                </div>

                            </div>
                        </div>


                    </div>
                </div>
<p></p>

            </div>


            <div id="divCreate" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-12">

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Create Sender</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-danger">
                                            <strong>Error!</strong>
                                            <asp:Label ID="lblloginerror" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">Sender Mobile No</div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtmobilecreate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">First Name</div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtnamecreate" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                   
                                </div>
                                 <div class="row">
                                       <div class="col-md-2">Last Name</div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtLastname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btncreate" OnClick="btncreate_Click" OnClientClick="return validatecreate();" CssClass="btn btn-primary" runat="server" Text="Create" />
                                       
                                    </div>
                                      <div class="col-md-2">
                                           <asp:Button ID="btncancelcreate" OnClick="btncancelcreate_Click" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                                          </div>
                                     </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div id="divWelcome" runat="server" visible="false">

                 <div class="row">
                    <div class="col-md-12">

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Welcome</h3>
                            </div>
                        </div>
                        <div class="box-body">
                             <div class="row">
                                <div class="col-md-2"><asp:LinkButton ID="btnAddBeneficiary" OnClick="btnAddBeneficiary_Click" runat="server" CssClass="btn btn-primary"> Add Beneficiary</asp:LinkButton></div>

                                
                                 <div class="col-md-2"><asp:LinkButton ID="btnLogout" OnClick="btnLogout_Click" runat="server"  CssClass="btn btn-danger">Logout</asp:LinkButton></div>
                               

                            </div>
                            <p></p>
                            <div class="row">
                                
                                <div class="col-md-4">
                                    <label>KYC</label>
                                     <asp:Label ID="lblkyc" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                    </div>
                                <div class="col-md-4">
                                     <label>Sender Name</label>
                                       <asp:Label ID="lblsendername" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                </div>
                                  <div class="col-md-4">
                               <label>User Name</label>
                                      <asp:Label ID="lblsendername2" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                               
                             
                            </div>
                            </div>
                            <p></p>
                             <div class="row">
                                <div class="col-md-3">
                                      <label>Limit</label>
                                      <asp:Label ID="lbllimit" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                      <label>Remaining Balance</label>
                                       <asp:Label ID="lblused" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                </div>
                                    <div class="col-md-3">
                                      <label>User Balance $</label>
                                       <asp:Label ID="lbluserbalance" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                </div>
                                  <div class="col-md-3">
                                      <label>Currency</label>
                                      <asp:Label ID="lblcurrency" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                           <p></p>
                            
                               <div class="row">
                                                        <div class="col-md-12">
                                                            <h3>Beneficiary List   <span class="pull-right">
                                                                
                                                            </span></h3>

                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" OnRowCommand="GridView1_RowCommand" Width="100%" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="beneID" HeaderText="Beneficiary Id" />
                                                                        <asp:BoundField DataField="beneName" HeaderText="Beneficiary Name" />
                                                                        <asp:BoundField DataField="bankName" HeaderText="BankName" />
                                                                        <asp:BoundField DataField="accountNo" HeaderText="Account No" />
                                                                        <asp:BoundField DataField="ifsc" HeaderText="IFSC" />
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="LnkPay" runat="server" CommandName="LnkPay" CssClass="btn btn-success btn-round fa fa-inr" ToolTip="Money Transfer"></asp:LinkButton>
                                                                                <asp:LinkButton ID="LnkDelete" runat="server" CommandName="LnkDelete" CssClass="btn btn-danger btn-round fa fa-trash-o" OnClientClick="if ( !confirm('Are you sure you want to delete this user?')) return false;" ToolTip="Delete"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                        </div>


                    </div>
                </div>




                
            </div>

            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModalBeneficiary" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Add Beneficiary</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Sender Mobile No                           
                                <asp:TextBox runat="server" class="form-control" ID="txtsendermobileno" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Bank                           
                               <asp:DropDownList ID="ddbank" AutoPostBack="true" OnSelectedIndexChanged="ddbank_SelectedIndexChanged" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        Account No                         
                                <asp:TextBox runat="server" class="form-control" onkeypress="return isNumber(event)" ID="txtbenaccountno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="form-group">
                                        IFSC            
                                        <div class="input-group bottom15">
                                            <span class="input-group-btn" style="width: 25% !important;display:none;">
                                                <asp:TextBox runat="server" class="form-control" ID="txtifscsticky" ></asp:TextBox>
                                            </span>
                                            <asp:TextBox runat="server" class="form-control" ID="txtifsccode"></asp:TextBox>
                                        </div>
                                          <div class="input-group bottom15">
                                               <asp:Button ID="btnverify" runat="server" Text="Verify"  CssClass="btn green" OnClick="btnverify_Click" />
                                          </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        Beneficiary  Name            
                                <asp:TextBox runat="server" class="form-control" ID="txtbeneficiaryname"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="form-group">
                                        Beneficiary  Mobile                      
                                <asp:TextBox runat="server" class="form-control" MaxLength="10" ID="txtbenegiciarymobile" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div role="alert" class="alert alert-info">
                                        <strong>Note :- </strong>Always make sure your Account Number & IFSC Code is correct. 
Company will not be liable for any wrong transactions.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSubmitBeneficiary" runat="server" Text="Submit" OnClientClick="return validateBeneficiary();" CssClass="btn btn-primary" OnClick="btnSubmitBeneficiary_Click" />
                            <asp:Button ID="btncloseBen" runat="server" CssClass="btn btn-danger" OnClick="btncloseBen_Click" Text="Close" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModalOTP" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Enter OTP</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div role="alert" id="divoptsuccess" visible="false" runat="server" class="alert alert-success">
                                        <strong>Note :- </strong>
                                        <asp:Label ID="lblmsgotp" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div role="alert" id="divotperror" visible="false" runat="server" class="alert alert-danger">
                                        <strong>Error :- </strong>
                                        <asp:Label ID="lblmsgotperror" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        Enter OTP             
                               
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" class="form-control" ID="txtotp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnResend" OnClick="btnResend_Click" runat="server" CssClass="btn btn-info btn-xs"><i class="fa fa-refresh"></i> Resend</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnOTPSubmit" runat="server" Text="Submit" OnClientClick="return validatOTP();" CssClass="btn btn-primary" OnClick="btnOTPSubmit_Click" />
                            <asp:Button ID="btnCloseOTP" runat="server" CssClass="btn btn-danger" OnClick="btnCloseOTP_Click" Text="Close" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModalMoneyTransfer" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Money Transfer <small>(
                                <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>)</small> </h4>
                        </div>
                        <div class="modal-body">
                            <div role="alert" id="divErrorMR" visible="false" runat="server" class="alert alert-danger">
                                <strong>Error :- </strong>
                                <asp:Label ID="lblerrorMR" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Beneficiary Account No 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtbeneficiaryaccountMR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Beneficiary Name
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtbeneficiarynameMR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Amount $ 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" class="form-control" ID="txtamount"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Trasnfer Type 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddtransfertype" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="2">IMPS</asp:ListItem>
                                            <asp:ListItem Value="1">NEFT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSubmitMoneyTranfer" runat="server" Text="Submit" OnClientClick="return validateMR();" CssClass="btn btn-primary" OnClick="btnSubmitMoneyTranfer_Click" />
                            <asp:Button ID="btnCloseMoneyTransfer" runat="server" CssClass="btn btn-danger" OnClick="btnCloseMoneyTransfer_Click" Text="Close" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="myModalMRConfirmation" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Confirm Money Transfer </h4>
                        </div>
                        <div class="modal-body">
                            <div role="alert" id="divErrorMRConfirm" visible="false" runat="server" class="alert alert-danger">
                                <strong>Error :- </strong>
                                <asp:Label ID="lblerrorMRConfirm" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Sender Mobile No 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblsendermobileconfirm" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Beneficiary Account No 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblbeneficiaryaccnoConfirm" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Beneficiary Name
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblbeneficiarynameConfirm" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Trasnfer Type 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lbltransfertypeConfirm" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Amount $
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblamountConfirm" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Charges 
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblcharges" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Total Amount $
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lbltotalamount" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSubmitConfirmMR" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitConfirmMR_Click" />
                            <asp:Button ID="btnCloseConfirmMR" runat="server" CssClass="btn btn-danger" OnClick="btnCloseConfirmMR_Click" Text="Close" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function showModalBeneficiary() {
            $('#myModalBeneficiary').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosepopupBeneficiary() {
            $('#myModalBeneficiary').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function showModalOTP() {
            $('#myModalOTP').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosepopupOTP() {
            $('#myModalOTP').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function showModalMR() {
            $('#myModalMoneyTransfer').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosepopupMR() {
            $('#myModalMoneyTransfer').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }

        function showModalMRConfirm() {
            $('#myModalMoneyTransfer').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();

            $('#myModalMRConfirmation').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosepopupMRConfirm() {
            $('#myModalMRConfirmation').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }


    </script>
</asp:Content>


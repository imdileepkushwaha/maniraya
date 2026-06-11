<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="UserReportActive.aspx.cs" Inherits="UserReportActive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script type="text/javascript">
          function validate2() {
              if (document.getElementById("<%=txtnameedit.ClientID%>").value == "") {

                   alert('Enter Name');
                   document.getElementById("<%=txtnameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtmobileedit.ClientID%>").value == "") {

                   alert('Enter Mobile');
                   document.getElementById("<%=txtmobileedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtemailedit.ClientID%>").value == "") {

                   alert('Enter Email');
                   document.getElementById("<%=txtemailedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtaddressedit.ClientID%>").value == "") {

                   alert('Enter Address');
                   document.getElementById("<%=txtaddressedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddcountryedit.ClientID%>").value == "0") {

                   alert('Select Country');
                   document.getElementById("<%=ddcountryedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddstateedit.ClientID%>").value == "0") {

                   alert('Select State');
                   document.getElementById("<%=ddstateedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddcityedit.ClientID%>").value == "0") {

                   alert('Select City');
                   document.getElementById("<%=ddcityedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddareaedit.ClientID%>").value == "0") {

                   alert('Select Area');
                   document.getElementById("<%=ddareaedit.ClientID%>").focus();
                return false;
            }
        }
    </script>

    <style type="text/css">
        .Active, .Active:hover {
            background-color: #006b0d;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
        .Deactive, .Deactive:hover {
            background-color: #bf0a0a;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
 <section class="content-header">
      <h1>
       User Report     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Retailer</a></li>
        <li class="active">User Report</li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>User Id</label>
                                        <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name</label>
                                       <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Mobile No</label>
                                      <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>Email Id</label>
                                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>From date</label>
                                       <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>To date</label>
                                         <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                          <%--<label>Country</label>
                                         <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <label>Area Pin Code</label>
                                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                            </div>

                             <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>State</label>
                                         <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>City</label>
                                           <asp:DropDownList ID="ddcity"  AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged"   CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                          <label>Area</label>
                                         <asp:DropDownList ID="ddarea" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select Area</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                            </div>


                            

                             <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Package</label>
                                         <asp:DropDownList ID="ddlPackage" AutoPostBack="true" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select Package</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Spnsor Id</label>
                                           <asp:DropDownList ID="ddlSponsor" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select Sponsor</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Pan Number</label>
                                         <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                            </div>




                        </div>
                        <div class="box-footer">
                                <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                           



                        </div>

                    </div>
                </div>
                    <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                              <div style="float: right">
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSubmit_Click">
                                    <%--<asp:ListItem>5</asp:ListItem>--%>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                               
                            </div>
                        </div>

                        <div class="box-body">
                       
                               
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpassword" runat="server" Text='<%#Eval("password") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("packageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%#Eval("CityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("stateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pan Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPanCard" runat="server" Text='<%# Eval("PanNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pin Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPincode" runat="server" Text='<%# Eval("Pincode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sponsor Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSponsorId" runat="server" Text='<%# Eval("SponserId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sponsor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSponsorName" runat="server" Text='<%# Eval("sponserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("cityname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress1" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Recharge Wallet">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRechargeWallet" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utility Wallet">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUtilityWallet" runat="server" Text='<%#Eval("utilityBalance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reg. Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="E-Pin Generation Status">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEpinStatus" runat="server" CommandName="epin" CommandArgument='<%# Eval("userid") %>' 
                                                Text='<%# Eval("epinGenerationStatus").ToString() == "1" ? "Unblock" : "Block" %>' CssClass='<%# Eval("epinGenerationStatus").ToString() == "1" ? "Active" : "Deactive" %>' ToolTip='<%# "Click to " + (Eval("epinGenerationStatus").ToString() == "1" ? "Block" : "Unblock") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkActiveStatus" runat="server" CommandName="changeStatus" CommandArgument='<%#Eval("userid") %>' 
                                                Text='<%#Eval("activeStatus").ToString() == "1" ? "Active" : "Deactive" %>' CssClass='<%#Eval("activeStatus").ToString() == "1" ? "Active" : "Deactive" %>' ToolTip='<%# "Click to " + (Eval("activeStatus").ToString() == "1" ? "Deactive" : "Active") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="TopUp Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkActiveStatus1" runat="server" Text='<%#Eval("Status").ToString() == "1" ? "Topup" : "Free" %>' CssClass='<%#Eval("Status").ToString() == "1" ? "Active" : "Deactive" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BankDetails">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEditw" CommandArgument='<%# Eval("userid") %>' runat="server" Text="Edit" OnClick="btneditbank_click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                    </div>
                              
                             
                             
                            
                            

                        </div>
                        <div class="box-footer">
                        



                        </div>

                    </div>
                </div>
                    <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit User Details</h4>
                        </div>
                        <div class="modal-body">
                        <div class="row">
                             <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Name</label>
                                          <asp:TextBox ID="txtnameedit" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                 </div>
                             <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Mobile</label>
                                          <asp:TextBox ID="txtmobileedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                 </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Email</label>
                                        <asp:TextBox ID="txtemailedit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Gender</label>
                                        <asp:DropDownList ID="ddgenderedit" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                             <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Address</label>
                                           <asp:TextBox ID="txtaddressedit" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                 </div>
                           
                            </div>
                             <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select Country</label>
                                       <asp:DropDownList ID="ddcountryedit" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountryedit_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select State</label>
                                         <asp:DropDownList ID="ddstateedit" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstateedit_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                             <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select City</label>
                                        <asp:DropDownList ID="ddcityedit" AutoPostBack="true" OnSelectedIndexChanged="ddcityedit_SelectedIndexChanged"  CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Other</label>
                                      
  <asp:TextBox ID="ddareaedit"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                             <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Pincode</label>
                                       <asp:TextBox ID="txtpincodeedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Date of birth</label>
                                       <asp:TextBox ID="txtdateofbirthedit" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                                   
                                       
                                  
                               
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                        </div>
                    </div>
                </div>
           
                    </div>



            

            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">

    <script type="text/javascript">
        $('.form_date').datepicker({
            format: 'dd/M/yyyy',
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/M/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
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
   
  <script>
     
        </script>
   
</asp:Content>


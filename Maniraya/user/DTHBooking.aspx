<%@ Page Title="" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="DTHBooking.aspx.cs" Inherits="user_DTHBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=DDlserviceprovider.ClientID%>").value == "0") {

                alert('select service provider');
                document.getElementById("<%=DDlserviceprovider.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=DDlsettopbox.ClientID%>").value == "") {

                alert('select settop box');
                document.getElementById("<%=DDlsettopbox.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=DDlsettopbox.ClientID%>").value == "0") {

                 alert('select settop box');
                 document.getElementById("<%=DDlsettopbox.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=DDLpackage.ClientID%>").value == "") {

                 alert('select package');
                 document.getElementById("<%=DDLpackage.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=DDLpackage.ClientID%>").value == "0") {

                alert('select package');
                document.getElementById("<%=DDLpackage.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=Txtname.ClientID%>").value == "") {

                alert('Enter Name');
                document.getElementById("<%=Txtname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtMobile.ClientID%>").value == "") {

                alert('Enter Mobile No');
                document.getElementById("<%=TxtMobile.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=Txtaddress.ClientID%>").value == "") {

                alert('Enter address');
                document.getElementById("<%=Txtaddress.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPincode.ClientID%>").value == "") {

                alert('Enter pincode');
                document.getElementById("<%=TxtPincode.ClientID%>").focus();
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
           
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
        <h1>DTH Booking entry</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">DTH Booking</a></li>
            <li class="active">DTH Booking entry</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">DTH Subscrirtion</h3>
                        </div>
                        <div class="box-body">
                           <div class="row">
                               
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Service Provider :</label>
                                        <asp:DropDownList ID="DDlserviceprovider" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDlserviceprovider_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Settop Box</label>
                                       <asp:DropDownList ID="DDlsettopbox" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="DDlsettopbox_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                
                                <div class="col-md-6">
                                    <div class="form-group">
                                      <label>Package :</label>
                                        <asp:DropDownList ID="DDLpackage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLpackage_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Price</label>
                                        <asp:TextBox ID="TxtPrice" Enabled="false" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:LinkButton ID="LnkView" runat="server" OnClientClick="showModal()" Visible="false">View</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="div_row_retailer" runat="server" visible="false">
                                
                                <div class="col-md-6">
                                    <div class="form-group">
                                      <label>User Price</label>
                                        <asp:TextBox ID="txtrprice" Enabled="false" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Commission</label>
                                        <asp:TextBox ID="txtcomm" Enabled="false" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                              
                                <div class="col-md-6">
                                    <div class="form-group">
                                         <label>Name</label>
                                        <asp:TextBox ID="Txtname"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Mobile</label>
                                      <asp:TextBox ID="TxtMobile"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                                <div class="form-group">
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address :</label>
                                           <asp:TextBox ID="Txtaddress"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Pincode</label>
                                         <asp:TextBox ID="TxtPincode"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                                <div class="form-group">
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>State :</label>
                                            <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>City</label>
                                        <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server"  >
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>District :</label>
                                            <asp:TextBox ID="txtdistrict"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>LandMark :</label>
                                             <asp:TextBox ID="txtLandMark"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            </div>
                        <div class="box-footer">
                             <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        </div>
                    </div>
                 </div>
               <div id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">View Commission</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                   User Price                        
                                    <asp:TextBox runat="server" class="form-control" ID="TxtRetailerPrice"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                  Commission                 
                                    <asp:TextBox runat="server" class="form-control" ID="TxtCommssion"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">                           
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
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


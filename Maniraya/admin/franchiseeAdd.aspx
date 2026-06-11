<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="franchiseeAdd.aspx.cs" Inherits="franchiseeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
      
        function validate() {
          
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
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                alert('Enter Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                alert('Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
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
          //  if (document.getElementById("<%=ddlsttehsil.ClientID%>").value == "0") {

              //  alert('Select tehsil');
                //document.getElementById("<%=ddlsttehsil.ClientID%>").focus();
                //return false;
         //   }
            //if (document.getElementById("<%=ddlstmarket.ClientID%>").value == "0") {

               // alert('Select Market');
               // document.getElementById("<%=ddlstmarket.ClientID%>").focus();
               // return false;
          //  }
            
            if (document.getElementById("<%=txtareaname.ClientID%>").value == "") {

                alert('Enter Area');
                document.getElementById("<%=txtareaname.ClientID%>").focus();
                  return false;
              }
              if (document.getElementById("<%=txtuserpassword.ClientID%>").value != document.getElementById("<%=txtconfirmpassword.ClientID%>").value) {

                alert('Password Not Match');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
              }


            if (document.getElementById("<%=txtOutletName.ClientID%>").value == "") {

                alert('Enter Outlet Name');
                document.getElementById("<%=txtOutletName.ClientID%>").focus();
                return false;
            }



        }
    </script>
      <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
   <section class="content-header">
      <h1>
      Add franchisee     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">franchisee</a></li>
        <li class="active">Add franchisee</li>
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
              <h3 class="box-title">Add franchisee</h3>
            </div>

                 <div class="box-body">
                    
                    
                     
                  
                    
                      <h4><b>Personal Information</b></h4>
                     <hr />
                       <div class="row">
                            <div class="col-md-6">
                             <div class="form-group">
                                  <label>FranchiseeType :</label>
                                 <asp:DropDownList ID="DDLstFranchiseeType" runat="server" CssClass="form-control">
                                     
                                 </asp:DropDownList>
                             </div>
                           </div>
                             <div class="col-md-6">
                             <div class="form-group">
                                 <label>Sponsor Id :</label>
                         <asp:TextBox ID="txtSponsorId" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtSponsorId_TextChanged"></asp:TextBox>
                             </div>
                         </div>
                             </div>
                     <div class="row">
                       
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Sponsor Name :</label>
                                    <asp:TextBox ID="txtSponsorName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                             <div class="col-md-6">
                             <div class="form-group">
                                  <label>Sponsor Type :</label>
                                    <asp:TextBox ID="TxtType" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                         <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Mobile No :</label>
                                    <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                       <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Email :</label>
                          <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Gender :</label>
                                     <asp:DropDownList ID="ddgender" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                       <div class="row">
                         <div class="col-md-6" style="display:none;">
                             <div class="form-group">
                                   <label>Date of Birth : Year-Month-Date</label>
                                   <fieldset >
                                          
                                            <%--<asp:TextBox ID="txtdateofbirth" CssClass="form-control form_date" runat="server"></asp:TextBox>--%>
                                            <div class="col-md-4 dvRow">
                                                <asp:DropDownList ID="ddlYear" CssClass="form-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 dvRow">
                                                <asp:DropDownList ID="ddlMonth"  CssClass="form-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 dvRow">
                                                <asp:DropDownList ID="ddlDay"  CssClass="form-control" ToolTip="Day" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </fieldset>
                                 </div>
                             </div>
                            <div class="col-md-6">
                           
                         </div>
                           <div class="col-md-6">
                               <div class="form-group">
                                   <label>Outlet Name : </label>
                                   <asp:TextBox ID="txtOutletName" runat="server" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>


                           </div>

                     <div class="row">

                         <div class="col-md-6">
                             <label>PAN No. : </label>
                             <asp:TextBox ID="txtPANNo" runat="server" CssClass="form-control"></asp:TextBox>
                             <br />
                             <label>PAN Upload</label>
                             <asp:FileUpload ID="filePAN" runat="server" />
                             <asp:Button ID="btnPANUPload" runat="server" Text="Upload PAN" OnClick="btnPANUPload_Click" CssClass="btn btn-warning" />
                             <br /><br />
                         </div>

                         <div class="col-md-6">
                             <label>View PAN</label>
                             <br />
                             <asp:ImageButton ID="imgPAN" runat="server" Width="100px" Height="100px" OnClick="imgPAN_Click" />
                         </div>

                     </div>

                     <div class="row">

                         <div class="col-md-6">
                             <label>GST No. : </label>
                             <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control"></asp:TextBox>
                             <br />
                             <label>GST Upload</label>
                             <asp:FileUpload ID="fileGST" runat="server" />
                             <asp:Button ID="btnGSTUpload" runat="server" Text="Upload GST" OnClick="btnGSTUpload_Click" CssClass="btn btn-warning" />
                             <br /><br />
                         </div>

                         <div class="col-md-6">
                             <label>View GST</label>
                             <br />
                             <asp:ImageButton ID="imgGST" runat="server" Width="100px" Height="100px" OnClick="imgGST_Click" />
                         </div>

                     </div>
                      

                      <h4><b>Communication Information</b></h4>
                     <hr />
                      <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                 <label>Address :</label>
                           <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                        
                     </div>
                       <div class="row">
                            <div class="col-md-6">
                             <div class="form-group">
                                  <label>Country :</label>
                                      <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>State :</label>
                           <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                       
                     </div>
                       <div class="row">
                             <div class="col-md-6">
                             <div class="form-group">
                                  <label>City :</label>
                                     <asp:DropDownList ID="ddcity"  CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6" style="display:none">
                             <div class="form-group">
                                       <label>Tehsil :</label>
                                  <asp:DropDownList ID="ddlsttehsil"  CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsttehsil_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select Tehsil</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                       
                     </div>
                      <div class="row">
                             <div class="col-md-6" style="display:none">
                             <div class="form-group">
                                  <label>Market :</label>
                                     <asp:DropDownList ID="ddlstmarket"  CssClass="form-control" runat="server"  OnSelectedIndexChanged="ddlstmarket_SelectedIndexChanged" >
                                            <asp:ListItem Value="0"> Select Market</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                               <label></label>
                          <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" Visible="false" Text="A"></asp:TextBox>
                             </div>
                         </div>
                       
                     </div>
                         <asp:Panel ID="otherPnl" runat="server" Visible="false">
                              <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Other City :</label>
                                         <asp:TextBox ID="TxtOtherCity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                       
                                    </div>
                                </div>
                            </div>
                                </asp:Panel>
                          <div class="row">
                                <div class="col-md-6">
                             <div class="form-group">
                                  <label>Pincode :</label>
                                         <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                       
                        
                     </div>
                           <h4><b>Password Information</b></h4>
                     <hr />
                       <div class="row">
                                <div class="col-md-6">
                             <div class="form-group">
                                  <label>Password :</label>
                                       <asp:TextBox ID="txtuserpassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Confirm Password :</label>
                           <asp:TextBox ID="txtconfirmpassword" TextMode="Password"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                        
                     </div>
                 </div>
                    <div class="box-footer">
                         <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
             
                    
              </div>

                 </div>
         </div>
                </div>


            <div id="DivPANlarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImagePANLarge" runat="server" Width="570px" Height="400px" />
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>


            <div id="DivGSTLarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImageGSTLarge" runat="server" Width="570px" Height="400px" />
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
            <asp:PostBackTrigger ControlID="btnPANUPload" />
            <asp:PostBackTrigger ControlID="btnGSTUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">  
    <script type="text/javascript">
        $('.form_date').datepicker({
            format: 'dd/mmm/yyyy',
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/mmm/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
</script>


     <script type="text/javascript">


         function showModal1() {
             debugger;
             $('#DivPANlarge').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#DivPANlarge').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }
        </script>


     <script type="text/javascript">


         function showModal2() {
             debugger;
             $('#DivGSTLarge').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#DivGSTLarge').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }
        </script>
  
</asp:Content>


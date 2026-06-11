<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SetTopBoxMaster.aspx.cs" Inherits="Subscription_ServiceProviderMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function removeSpecialChars(id) {
            var stringToReplace = document.getElementById(id).value;
            stringToReplace = stringToReplace.replace(/[^\w\s]/gi, '');
            document.getElementById(id).value = stringToReplace;
            return true;
        }
        function IsNumeric(id) {
            try {
                var ch = $('#' + id).val();
                if (!$.isNumeric(ch)) {
                    $('#' + id).val($('#' + id).val().replace(/[^0-9\.]/g, ''));
                }
                $('#' + id).val(Number($('#' + id).val()));
            } catch (err) { }
            return true;
        }
        function validate() {

            if (document.getElementById("<%=DDlServiceProvider.ClientID%>").value == "0") {

                 alert('select service provider');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=DDlServiceProvider.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtSetTopbox.ClientID%>").value == "") {

                 alert('Enter settop box');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TxtSetTopbox.ClientID%>").focus();
                   return false;
               }
            
           }
           function validate2() {
               // alert('sd');
               if (document.getElementById("<%=ddlserviceprovideredit.ClientID%>").value == "0") {

                   alert('select service provider');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddlserviceprovideredit.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtSetTopboxedit.ClientID%>").value == "") {

                 alert('Enter settop box');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtSetTopboxedit.ClientID%>").focus();
                 return false;
             }
            
           }
    </script>
     
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
      Settop Box Master 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">DTH Susscription</a></li>
        <li class="active"> Settop Box Master</li>
      </ol>
    </section>    
  </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add Settop Box</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   <div class="row">
                   <div class="col-md-6">
                <div class="form-group">
                  <label >Service Provider name</label>
                    <asp:DropDownList ID="DDlServiceProvider" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>             
               </div>
                       <div class="col-md-6">
                <div class="form-group">
                  <label >Settop Box name</label>
                  <asp:TextBox ID="TxtSetTopbox" onchange="return removeSpecialChars(this.id);" onkeyup="return removeSpecialChars(this.id);" runat="server" CssClass="form-control"></asp:TextBox>
                </div>             
               </div>
                       </div>
                  <div class="row" style="display:none">
                   <div class="col-md-6">
                <div class="form-group">
                  <label >MRP</label>
                    <asp:TextBox ID="TxtMrp" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>             
               </div>
                       <div class="col-md-6">
                <div class="form-group">
                  <label >Description</label>
                    <asp:TextBox ID="TxtDescription"  runat="server" CssClass="form-control"  ></asp:TextBox>
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
                 <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  
                <div class="form-group table-responsive">
                 <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Settop Box">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("STBName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrition" Visible="False">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename" runat="server"  Text='<%#Eval("Remark") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="MRP" Visible="False">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename2" runat="server"  Text='<%#Eval("MRP") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Service Provoder">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename3" runat="server"  Text='<%#Eval("Operator") %>'></asp:Label>
                                   <asp:Label ID="Label1" runat="server"  Text='<%#Eval("Operatorid") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
              
                </div>             
             
                   
                       
            
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>
                 <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"> Edit SetTop Box</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                         Service Provider name
                         <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>
                           <asp:DropDownList ID="ddlserviceprovideredit" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                           
                        </div>
                         <div class="form-group">
                        SetTop Box
                      
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtSetTopboxedit" ></asp:TextBox>
                        </div>
                         <div class="form-group" style="display:none">
                        MRP
                        
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtMrpedit"  onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);"></asp:TextBox>
                        </div>
                         <div class="form-group" style="display:none">
                        Description
                      
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtDescriptionedit" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click"  />
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
  
   
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
     <script>
         $(document).ready(function () {
             $('[data-toggle="tooltip"]').tooltip();
             if ($('[id$="msgjson"]').html() != "") {
                 try {
                     MSG = JSON.parse($('[id$="msgjson"]').html());
                     if (MSG.resp == 0 || MSG.resp == -1) {
                         $('#msg').removeClass("alert-info").removeClass("alert-success").addClass("alert-danger");
                     }
                     if (MSG.resp == 1) {
                         $('#msg').removeClass("alert-danger").removeClass("alert-info").addClass("alert-success");
                     }
                     $('#msgText').html(MSG.msg);
                 } catch (err) {
                     console.log(err);
                 }
             }
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
   </asp:Content>

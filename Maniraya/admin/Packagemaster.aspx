<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="Packagemaster.aspx.cs" Inherits="admin_Packagemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="../js/jquery_1.7.1.js"></script>
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

            if (document.getElementById("<%=ddloperator.ClientID%>").value == "0") {

                alert('select service provider');
                // alert("Enter Rank No"); 
                document.getElementById("<%=ddloperator.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=ddlsettopbox.ClientID%>").value == "0") {

                alert('select settop box');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=ddlsettopbox.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtPackage.ClientID%>").value == "") {

                 alert('Enter package');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TxtPackage.ClientID%>").focus();
                   return false;
               }
            if (document.getElementById("<%=TxtValidity.ClientID%>").value == "") {

                alert('Enter validity');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtValidity.ClientID%>").focus();
                 return false;
            }
            if (document.getElementById("<%=TxtNoOfChanel.ClientID%>").value == "") {

                alert('Enter no of channels');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtNoOfChanel.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtMRP.ClientID%>").value == "") {

                alert('Enter mrp');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtMRP.ClientID%>").focus();
                return false;
            }
           
               
            if (document.getElementById("<%=TxtDescription.ClientID%>").value == "") {

                alert('Enter description');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtDescription.ClientID%>").focus();
                 return false;
             }
           }
           function validate2() {
               // alert('sd');
               if (document.getElementById("<%=ddlsettopboxedit.ClientID%>").value == "0") {

                   alert('select settop box');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddlsettopboxedit.ClientID%>").focus();
                 return false;
               }
               if (document.getElementById("<%=TxtPackageedit.ClientID%>").value == "") {

                   alert('Enter package');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtPackageedit.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtValidityedit.ClientID%>").value == "") {

                 alert('Enter validity');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtValidityedit.ClientID%>").focus();
                 return false;
             }
               if (document.getElementById("<%=TxtNoofchannelsEdit.ClientID%>").value == "") {

                   alert('Enter no of channel');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtNoofchannelsEdit.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtMrpedit.ClientID%>").value == "") {

                 alert('Enter mrp');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtMrpedit.ClientID%>").focus();
                return false;
             }
              
               
            if (document.getElementById("<%=TxtDescriptionedit.ClientID%>").value == "") {

                alert('Enter description');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtDescriptionedit.ClientID%>").focus();
                return false;
            }
           }
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
      Package Master 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">DTH Susscription</a></li>
        <li class="active"> Package Master </li>
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
              <h3 class="box-title">Add Package</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   <div class="row">

                       <div class="col-md-6">
                <div class="form-group">
                  <label >Service Provider</label>
                    <asp:DropDownList ID="ddloperator" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddloperator_SelectedIndexChanged"></asp:DropDownList>
                </div>             
               </div>
                   <div class="col-md-6">
                <div class="form-group">
                  <label >SetTop Box</label>
                    <asp:DropDownList ID="ddlsettopbox" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>             
               </div>
                       
                       </div>
                  <div class="row">
                      <div class="col-md-6">
                <div class="form-group">
                  <label >Package name</label>
                  <asp:TextBox ID="TxtPackage" onchange="return removeSpecialChars(this.id);" onkeyup="return removeSpecialChars(this.id);" runat="server" CssClass="form-control"></asp:TextBox>
                </div>             
               </div>
                   <div class="col-md-6">
                <div class="form-group">
                  <label >Validity in days</label>
                    <asp:TextBox ID="TxtValidity" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>             
               </div>
                       
                       </div>
                    <div class="row">
                        <div class="col-md-6">
                <div class="form-group">
                  <label >No of channels</label>
                    <asp:TextBox ID="TxtNoOfChanel"  runat="server" CssClass="form-control" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" ></asp:TextBox>
                </div>             
               </div>
                   <div class="col-md-6">
                <div class="form-group">
                  <label >MRP</label>
                    <asp:TextBox ID="TxtMRP" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" runat="server" CssClass="form-control txtmrp" ></asp:TextBox>
                </div>             
               </div>
                      
                       </div>

                 

                   <div class="row">
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
                                    <asp:TemplateField HeaderText="Package Name">
                               <ItemTemplate>
                                     <asp:Label ID="lbloperator" runat="server"  Text='<%#Eval("ServiceProvider") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Set Top Box">
                               <ItemTemplate>
                                     <asp:Label ID="lblsbox" runat="server"  Text='<%#Eval("SBoxName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Package Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblpackage" runat="server"  Text='<%#Eval("PackageName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="validity">
                               <ItemTemplate>
                                     <asp:Label ID="lblvalidity" runat="server"  Text='<%#Eval("ValidityInDays") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="channel No">
                               <ItemTemplate>
                                     <asp:Label ID="lblchannelno" runat="server"  Text='<%#Eval("ChannelsNo") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>                                    
                                      <asp:TemplateField HeaderText="MRP">
                               <ItemTemplate>
                                     <asp:Label ID="lblmrp" runat="server"  Text='<%#Eval("PMRP") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Settop Box">
                               <ItemTemplate>
                                     <asp:Label ID="lblsettopboxname" runat="server"  Text='<%#Eval("SBoxName") %>'></asp:Label>
                                   <asp:Label ID="lblstbid" runat="server"  Text='<%#Eval("STBID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lblremark" runat="server"  Text='<%#Eval("Remark") %>' Visible="false"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatus" runat="server"  Text='<%#Eval("ActiveStatus") %>'></asp:Label>
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
                        <h4 class="modal-title"> Edit SetTop Box
                        <span style="float:right">Status : <asp:DropDownList ID="ddlstatus" runat="server" style="font-size:12px;">
                            <asp:ListItem Text="Active"  Value="1"></asp:ListItem>
                            <asp:ListItem Text="DeActive" Value="0"></asp:ListItem></asp:DropDownList></span>
                            </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                         Settop Box
                         <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>
                           <asp:DropDownList ID="ddlsettopboxedit" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                           
                        </div>
                         <div class="form-group">
                        Package name
                      
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtPackageedit" ></asp:TextBox>
                        </div>
                         <div class="form-group">
                       validity
                        
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtValidityedit"  onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);"></asp:TextBox>
                        </div>
                          <div class="form-group">
                       No Of Channels
                        
                         
                           <asp:TextBox ID="TxtNoofchannelsEdit"  runat="server" CssClass="form-control" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" ></asp:TextBox>
                        </div>
                            <div class="form-group">
                       MRP
                        
                         
                           <asp:TextBox ID="TxtMrpedit"  runat="server" CssClass="form-control txtmrpedit" onchange="return IsNumeric(this.id);" onkeyup="return IsNumeric(this.id);" ></asp:TextBox>
                        </div>
                        
                        
                         <div class="form-group">
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


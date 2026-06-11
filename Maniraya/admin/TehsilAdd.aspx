<%@ Page Title="Add Market" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="TehsilAdd.aspx.cs" Inherits="TehsilAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function validate() {

             if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

                   alert('Select Country');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddcountry.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=ddstate.ClientID%>").value == "0") {

                   alert('Select State');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddstate.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=ddlstcity.ClientID%>").value == "0") {

                   alert('Select  City');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddlstcity.ClientID%>").focus();
                   return false;
               }
             if (document.getElementById("<%=TXtTehsil.ClientID%>").value == "") {

                 alert('Enter Tehsil');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TXtTehsil.ClientID%>").focus();
                   return false;
               }
           }
           function validate2() {
               // alert('sd');
               if (document.getElementById("<%=txtcitynameedit.ClientID%>").value == "") {

                   alert('Enter Tehsil');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtcitynameedit.ClientID%>").focus();
                   return false;
               }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Add Tehsil     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Add Tehsil</li>
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
              <h3 class="box-title">Add Tehsil</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

                 <div class="box-body">
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Select Country</label>
                                 <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                     <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Select State</label>
                                 <asp:DropDownList ID="ddstate" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem Value="0"> Select State</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>City</label>
                                 <asp:DropDownList ID="ddlstcity" CssClass="form-control" runat="server" >
                                     <asp:ListItem Value="0"> Select City</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Tehsil</label>
                                 <asp:TextBox ID="TXtTehsil" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                 </div>
              <!-- /.box-body -->

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
<div class="row">
                  
                 
                <div class="form-group table-responsive">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("TehsilId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Country Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("CountryName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename" runat="server"  Text='<%#Eval("statename") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblcityname" runat="server"  Text='<%#Eval("cityname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Tehsil Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblTehsilName" runat="server"  Text='<%#Eval("TehsilName") %>'></asp:Label>
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
   
                
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
               
                    
              </div>
         
          </div>
            </div>

     <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Tehsil Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                         Tehsil Name
                            <asp:Label ID="lblcityid" Visible="false" runat="server" Text=""></asp:Label>
                         
                            <asp:TextBox runat="server" class="form-control" ID="txtcitynameedit" ></asp:TextBox>
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


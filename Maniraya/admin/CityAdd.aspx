<%@ Page Title="Add City" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CityAdd.aspx.cs" Inherits="admin_CityAdd" %>

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
               if (document.getElementById("<%=txtcityname.ClientID%>").value == "") {

                   alert('Enter City Name');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtcityname.ClientID%>").focus();
                   return false;
               }
           }
           function validate2() {
               // alert('sd');
               if (document.getElementById("<%=txtcitynameedit.ClientID%>").value == "") {

                   alert('Enter City Name');
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
       Add City     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Add City</li>
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
              <h3 class="box-title">Add City</h3>
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
                                 <asp:DropDownList ID="ddstate" CssClass="form-control" runat="server">
                                     <asp:ListItem Value="0"> Select State</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>City</label>
                                 <asp:TextBox ID="txtcityname" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
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
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("cityid") %>'></asp:Label>
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
                        <h4 class="modal-title">Edit City Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                         City Name
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


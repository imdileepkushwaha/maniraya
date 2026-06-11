<%@ Page Title="Joining Package Activation" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="UserOwnActivation.aspx.cs" Inherits="UserOwnActivation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=DDLst.ClientID%>").value == "0") {
                alert('Select Paymentmode');
                document.getElementById("<%=DDLst.ClientID%>").focus();
                return false;
            }            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
     Joining Package Activation 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">My Wallet</a></li>
        <li class="active">Joining Package Activation </li>
      
      </ol>
    </section>   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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

            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Joining Package Activation</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Address :</label>
                                  <asp:TextBox ID="TxtAddress"  runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>MobileNo :</label>
                                  <asp:TextBox ID="TxtMobileNo" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>Registration date :</label>
                               <asp:TextBox ID="TxtCityName" Enabled="false" runat="server"  CssClass="form-control" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                   <label>Account Balance :</label>
                               <asp:TextBox ID="txtbalance" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                     <asp:TemplateField HeaderText="Packege Code">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("PlanName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("Planamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankname" runat="server" Text='<%#Eval("MonthlyAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                       <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>                              
                                    
                                </Columns>
                            </asp:GridView>
                             </div>
                         </div>
                        
                     </div>
                        
                        
                       </div>
                         <div class="box-footer">
                        
             

                              
                    
              </div>


                  
              </div>
              
              </div>
                  </div>


               <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Package Activation</h4>
                        </div>
                        <div class="modal-body">
                              <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Package Code :</label>
                                  <asp:TextBox ID="TxtPackageCode" AutoPostBack="true" runat="server" CssClass="form-control" Enabled="false" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Package Name :</label>
                                  <asp:TextBox ID="TxtPackegeName" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>MRP :</label>
                               <asp:TextBox ID="TxtMRP" Enabled="false" runat="server"  CssClass="form-control" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>Monthly Volume :</label>
                               <asp:TextBox ID="TXTBv" Enabled="false" runat="server"  CssClass="form-control" />    
                             </div>
                         </div>
                     </div>
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Mode Of Payment :</label>
                                 <asp:DropDownList ID="DDLst" runat="server" CssClass="form-control" >
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                         <asp:ListItem Value="1">Available wallet Balance</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>Payble Amount :</label>
                               <asp:TextBox ID="TxtAmount" Enabled="false" runat="server"  CssClass="form-control" />    
                             </div>
                         </div>
                     </div>
                              
                        </div>
                        <div class="modal-footer">
                             <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Submit" OnClientClick="return validate();" OnClick="btnSubmit_Click"  />
                                       <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            </div>

            
        </ContentTemplate>
        <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">   
 
  
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




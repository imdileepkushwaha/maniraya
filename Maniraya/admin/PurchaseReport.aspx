<%@ Page Title="Purchase Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PurchaseReport.aspx.cs" Inherits="admin_PurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
     Purchase Report 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
        <li class="active">Purchase Report   </li>
      
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
              <h3 class="box-title">Search Criteria</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>From date :</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>To date :</label>
                                  <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                              <div class="col-md-4">
                             <div class="form-group">
                                 <label>Franchisee ID :</label>
                                 <asp:DropDownList ID="DDLstFranchisee" CssClass="form-control"  runat="server">
                                                     <asp:ListItem Value="0"> Select Franchisee</asp:ListItem>
                                                </asp:DropDownList>
                             </div>
                         </div>
                              
                     </div>
                       <div class="row">
                            <div class="col-md-4">
                             <div class="form-group">
                                 <label>User ID :</label>
                                 <asp:TextBox ID="txtuserid"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                           <div class="col-md-4">
                             <div class="form-group">
                                 <label>Transaction ID :</label>
                                 <asp:TextBox ID="txtransactionId"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                       </div>
                         
                          
                       </div>
                         <div class="box-footer">
                        
             

                             

                              
                      <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
              </div>


                  
              </div>


              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatusd" runat="server" Text='<%#Eval("TransactionCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>       
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Purchase Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("PurchaseDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkph" runat="server" CommandName="photolarge"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px"  /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount/pcs">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransactiontype" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                       <asp:TemplateField HeaderText="Payment Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpstatus" runat="server" Text='<%#Eval("PStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>         
                                   <asp:TemplateField HeaderText="Product Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatus" runat="server" Text='<%#Eval("RStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                   <asp:TemplateField HeaderText="Franchisee">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_fname" runat="server" OnClick="lnk_frame_click" CommandArgument='<%# Eval("StateId").ToString()+"_"+Eval("CityId").ToString()+"_"+Eval("FranchiseeId").ToString() %>' ><%#Eval("FranchiseeName") %></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Payment Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatusd" runat="server" Text='<%#Eval("PaymentCode") %>'></asp:Label>
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




             <div id="Div_FDetails" class="modal fade">
                           <div class="modal-dialog" style="margin-top:60px;">
                               <div class="modal-content">
                                   <div class="modal-header">
                                       <div class="container-fluid">
                                           <h4 class="modal-title pull-left">Franchisee Details</h4>
                                       </div>
                                   </div>
                                   <div class="modal-body">
                                       <div class="container-fluid">
                                           <div class="product_item" style="width: 100%;">
                                              <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Name :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblfname" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Mobile No :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblmob" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Address :</div>
                                                  <div class="col-md-9"><asp:Label ID="lbladdress" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">State :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblstate" runat="server"></asp:Label></div>
                                                   <div class="col-md-3" style="font-weight:bold">City :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblcity" runat="server"></asp:Label></div>
                                              </div>
                                                <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Pincode :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblpincode" runat="server"></asp:Label></div>
                                              </div>
                                           </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
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

        function showFranchiseeModal() {
            $('#Div_FDetails').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosesFranchiseepopup() {
            $('#Div_FDetails').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }


     </script>
</asp:Content>


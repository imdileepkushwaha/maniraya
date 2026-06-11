<%@ Page Title="Purchase Report" Language="C#" MasterPageFile="~/franchisee/franchiseemaster.master" AutoEventWireup="true" CodeFile="FranchiseePvReport.aspx.cs" Inherits="FranchiseePvReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
     Franchisee PV Report
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Accounts</a></li>
        <li class="active">Franchisee PV Report </li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
         <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:5%;left:28%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
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
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GV1_DataBound" ShowFooter="true">
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatusd3" runat="server" Text='<%#Eval("TransactionCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>       
                                     <asp:TemplateField HeaderText="PV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("CreateDate","{0:dd/MM/yyyy}") %>'></asp:Label>
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
                                   <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk" runat="server" Text="View" CommandArgument='<%#Eval("TransactionCode") %>' OnClick="lnk_click"></asp:LinkButton>
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
                               <div class="modal-content" style="width:100%;min-width:750px;">
                                   <div class="modal-header">
                                       <div class="container-fluid">
                                           <h4 class="modal-title pull-left">Purchase Details</h4>
                                       </div>
                                   </div>
                                   <div class="modal-body">
                                       <div class="container-fluid">
                                            <div class="product_item" style="width: 100%;">
                                              <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Transaction Id :</div>
                                                  <div class="col-md-9"><asp:Label ID="lbltransactionid" runat="server"></asp:Label></div>
                                              </div>
                                              <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Franchisee Id :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblfranchiseeid" runat="server"></asp:Label></div>
                                                  <div class="col-md-3" style="font-weight:bold">UserId :</div>
                                                  <div class="col-md-3"><asp:Label ID="lbltuserid" runat="server"></asp:Label></div>
                                              </div>
                                                <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Purchase Date :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblpurchasedate" runat="server"></asp:Label></div>
                                                  <div class="col-md-3" style="font-weight:bold">Payment Status:</div>
                                                  <div class="col-md-3"><asp:Label ID="lblpaymentstatus" runat="server"></asp:Label></div>
                                              </div>
                                                <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Product Status :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblproductstatus" runat="server"></asp:Label></div>
                                                  <div class="col-md-3" style="font-weight:bold">Payment Code:</div>
                                                  <div class="col-md-3"><asp:Label ID="lblpaymentcode" runat="server"></asp:Label></div>
                                              </div>
                                                </div>
                                           <br />
                                           <div class="product_item" style="width: 100%;overflow-x:auto;max-width:1000px;">
                                             <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatusd1" runat="server" Text='<%#Eval("TransactionCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>       
                                    <asp:TemplateField HeaderText="User Id"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Purchase Id"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date"  Visible="false">
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
                                       <asp:TemplateField HeaderText="Payment Status"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpstatus" runat="server" Text='<%#Eval("PStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>         
                                   <asp:TemplateField HeaderText="Product Status"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatus" runat="server" Text='<%#Eval("RStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Payment Code"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprostatusd2" runat="server" Text='<%#Eval("PaymentCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                    
                                </Columns>
                            </asp:GridView>
                                           </div>
                                           <br />
                                            <div class="product_item" style="width: 100%;" id="div_succcess" runat="server">
                                                <div class="row">
                                                  <div class="col-md-12" style="font-weight:bold"><h3><asp:Label ID="lblorderstatus" runat="server"></asp:Label></h3></div>
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


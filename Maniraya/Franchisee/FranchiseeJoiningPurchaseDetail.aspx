<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="FranchiseeJoiningPurchaseDetail.aspx.cs" Inherits="FranchiseeJoiningPurchaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     
      <section class="content-header">
      <h1>
     Franchisee  Purchase Detail
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Purchase management</a></li>
        <li class="active"> Franchisee  Purchase Detail </li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                  <div class="row">
            <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Franchisee Customer Purchase Detail</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
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
                                   <asp:TextBox ID="TxtFranchiseeId" runat="server" CssClass="form-control" />
                               
                             </div>
                         </div>
                     </div>
                            <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                   <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                 </div>
                             </div>
                                </div>
                          
                     


                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID">
                               <Columns>
                                    <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="img/PLUS.jpg" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable" >
                        <Columns>

                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductID" HeaderText="Product Code" />
                               <asp:TemplateField HeaderText="Image">
                               <ItemTemplate>
                                   <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px"  />
                                 
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product Name" />
                              <asp:BoundField ItemStyle-Width="150px" DataField="MRP" HeaderText="MRP" />
                               <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantity" />                               
                                <asp:BoundField ItemStyle-Width="150px" DataField="BV" HeaderText="BV" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" /> 
                              <asp:BoundField ItemStyle-Width="150px" DataField="PurchaseAmount" HeaderText="PurchaseAmount" />
                               <asp:BoundField ItemStyle-Width="150px" DataField="CGST" HeaderText="CGST" /> 
                              <asp:BoundField ItemStyle-Width="150px" DataField="CGST" HeaderText="SGST" />                      
                              <asp:BoundField ItemStyle-Width="150px" DataField="CGST" HeaderText="IGST" />                                           
                                <asp:BoundField ItemStyle-Width="150px" DataField="TotalAmount" HeaderText="TotalAmount" />
                            
                             
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Franchisee ID">
                                        <ItemTemplate>
                                                <asp:Label ID="lblFranchiseeID" runat="server" Text='<%#Eval("FranchiseeID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Franchisee name">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lbluseridUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Purchase Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridEmailId" runat="server" Text='<%#Eval("PurchaseAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="CGST">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridContactNo" runat="server" Text='<%#Eval("CGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SGST">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridaddress" runat="server" Text='<%#Eval("SGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="IGST">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("IGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid2" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                           Discount <asp:Label ID="lblCash" runat="server" Text='<%#Eval("Cash") %>'></asp:Label><br />
                                            Payble Amount  <asp:Label ID="LblRestAmount" runat="server" Text='<%#Eval("restamount") %>'></asp:Label><br />
                                              Payment Mode  <asp:Label ID="LblPaymentmode" runat="server" Text='<%#Eval("Paymentmode") %>'></asp:Label><br />
                                                TransactionNo  <asp:Label ID="LblChequeno" runat="server" Text='<%#Eval("Chequeno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("Discount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                 <%--  <asp:TemplateField HeaderText="Payble Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaybleAmount" runat="server" Text='<%#Eval("PaybleAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Purchase date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Purchasedate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice" Visible="false">
                                        <ItemTemplate>
                                            
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Invoice" class="btn btn-outline-dark btn-text w-100" NavigateUrl='<%# string.Format("FranchiseeInvoice.aspx?OrderNo={0}",
                    HttpUtility.UrlEncode(Eval("PurchaseID").ToString())) %>' Target="_blank"></asp:HyperLink>                                   
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Action" Visible="false">
                                        <ItemTemplate>
                                                <asp:LinkButton ID="btnApprove" CommandName="approve" OnClick="btnApprove_click" runat="server"> Approve |</asp:LinkButton> 
                                             <asp:LinkButton ID="btnReject" CommandName="reject" OnClick="btnReject_click" runat="server">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                             </div>
                         </div>
                      
                            
                     </div>
                          </div>
                       </div>
                 </div>


               </div>



           

            
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=PLUS]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "img/Continue1.png");
    });
    $("[src*=Continue1]").live("click", function () {
        $(this).attr("src", "img/PLUS.jpg");
        $(this).closest("tr").next().remove();
    });

</script>
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
     </script>
</asp:Content>


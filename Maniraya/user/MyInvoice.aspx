<%@ Page Title="" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="MyInvoice.aspx.cs" Inherits="user_MyInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <link rel="stylesheet" href="invoicecss/invoicecss.css">--%>
  <script language="javascript">
      function printdiv(printpage) {
          var headstr = "<html><head><title></title></head><body>";
          var footstr = "</body>";
          var newstr = document.all.item(printpage).innerHTML;
          var oldstr = document.body.innerHTML;
          document.body.innerHTML = headstr + newstr + footstr;
          window.print();
          document.body.innerHTML = oldstr;
          return false;
      }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Invoice</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Invoice</a></li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Invoice</h3>
                   <div style="float: right">
                 
                     <%--  <asp:Button ID="Button1" runat="server" Text="Print" CssClass="btn btn-warning"  OnClientClick="printdiv('drrt');" />--%>
                       </div>
                        </div>
               
                <div class="box-body">
                     <div class="row">
                      <div class="col-md-12">
                   
                    <section class="invoice">

          
            
          
  <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-globe"></i> Team Maker India Pvt. Ltd.
            <small class="pull-right">Date: <asp:Label ID="Lbldate" runat="server" Text=""></asp:Label></small>
          </h2>
        </div>
        <!-- /.col -->
      </div>

  <div class="row invoice-info">
        <div class="col-sm-4 invoice-col">
          From
          <address>
            <strong>Team Maker India</strong><br />
           Sangam Vihar, Aligarh - 202001 (UP).<br />
            Email : info@teammakerindia.com
<br />
              Ph : +(91) 821 814 9076
          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4 invoice-col">
          To
          <address>
            <strong>M/S : <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label></strong><br>
            <asp:Label ID="LblAddress" runat="server" Text=""></asp:Label><br>
                 Email : <asp:Label ID="LblEmail" runat="server" Text=""></asp:Label><br>
               Mobile : <asp:Label ID="LblMobileNo" runat="server" Text=""></asp:Label><br>
         
          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4 invoice-col">
          <b>Invoice : #<asp:Label ID="LblInvoiceNo" runat="server" Text=""></asp:Label></b><br>
        
        
          <b>Purchase date : <asp:Label ID="LblPdate" runat="server" Text=""></asp:Label><br>
        
        </div>
        <!-- /.col -->
      </div>


            <div class="row">
        <div class="col-xs-12 table-responsive">
                 <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered"  Width="100%" OnRowDataBound="gvOrders_RowDataBound"  >
                        <Columns>
                                 <asp:TemplateField HeaderText="S.N.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                             <asp:TemplateField HeaderText="Perticular">
                                        <ItemTemplate>
                                           
                                             <asp:Label ID="LblProductName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                             (<asp:Label ID="LaProductID" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
    
                               <asp:BoundField  DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField  DataField="Price" HeaderText="Rate" />
                               <asp:TemplateField HeaderText="Calculate Amount">
                                        <ItemTemplate>
                                           
                                             <asp:Label ID="LblPurchaseAmount" runat="server" Text='<%#Eval("PurchaseAmount") %>'></asp:Label> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                          
                             <asp:TemplateField HeaderText="CGST">
                                        <ItemTemplate>
                                           
                                             <asp:Label ID="LblCGST" runat="server" Text='<%#Eval("CGST") %>'></asp:Label> (<asp:Label ID="lblCGSTPer" runat="server" Text='<%#Eval("CGSTper") %>'></asp:Label>%)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="SGST">
                                        <ItemTemplate>
                                            
                                             <asp:Label ID="LblSGST" runat="server" Text='<%#Eval("SGST") %>'></asp:Label> (<asp:Label ID="lblSGSTPer" runat="server" Text='<%#Eval("SGSTper") %>'></asp:Label>%)
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Purchase Amount">
                                        <ItemTemplate>
                                            
                                             <asp:Label ID="LblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                            
                            
                        </Columns>
                    </asp:GridView>
               
               </div>             
                          </div>

 <div class="row">
        <!-- accepted payments column -->
        <div class="col-xs-6">
          <p class="lead">Terms</p>
         

          <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
             1. Goods/Service once sold will not be taken back<br />
                2. This Is Electronic Generated Slip . No Stamp Or Signature Is Required<br />
              E. &amp; O.E.<br />

          </p>
             <p></p>
            <strong>Thanks </strong>
            <p>
                For  Team Maker India Pvt. Ltd.
            </p>
             
        </div>
        <!-- /.col -->
        <div class="col-xs-6">
         
          <div class="table-responsive" >
            <table class="table">
              
              <tr id="trCgst" runat="server" >
                <th>CGST : <asp:Label ID="LblCgstPer" runat="server" Visible="true" Text=""></asp:Label></th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblCGST" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr id="trSgst" runat="server"  >
                <th>SGST : <asp:Label ID="LblSgstPer" runat="server" Visible="true" Text=""></asp:Label></th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblSGSt" runat="server" Text=""></asp:Label></td>
              </tr>
                 <tr id="trIgst" runat="server" visible="false" style="display:none;">
                <th>IGST : <asp:Label ID="LblIgstPer" runat="server" Visible="false" Text=""></asp:Label></th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblIGST" runat="server" Text=""></asp:Label></td>
              </tr>
				 <tr >
                <th style="width:50%">Subtotal:</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblTotal" runat="server" Text=""></asp:Label></td>
              </tr>
                  <tr>
                <th>Shipping Charge :</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblShipping" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr>
                <th>Total :</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblGTotal" runat="server" Text=""></asp:Label></td>
              </tr>
            </table>
			   <p class="lead">Amount in words: <asp:Label ID="LblAmountinWorld" runat="server" Text=""> </asp:Label> Rupees Only</p>

          </div>
        </div>
        <!-- /.col -->
      </div>

 <div class="row no-print">
        <div class="col-xs-12" style="display:none;">
            <asp:LinkButton ID="LinkButtonSearchClient" runat="server" 
    NavigateUrl='<%# String.Format("InvoicePrint.aspx?PurchaseId={0}&UserId={1}", Eval("PurchaseID"),Eval("userid"))%>' 
    Text='Print'></asp:LinkButton>
          <a href="#" target="_blank" class="btn btn-default" id="rt" runat="server"><i class="fa fa-print"></i> Print</a>
        
        </div>
      </div>


                   </section>      
                  
                  
               
                               
              
           
        </div>
                      </div>
                          </div>
                 
                </div>
                 </div>
            </div>
      
  
   
   
      
  
   
   
      
  
   
   
      
  
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


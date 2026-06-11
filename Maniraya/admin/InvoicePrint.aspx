<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePrint.aspx.cs" Inherits="admin_InvoicePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>AdminLTE 2 | Invoice</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="../bower_components/Ionicons/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
</head>
<body onload="window.print();">
    <form id="form1" runat="server">
   <div class="wrapper">
    <section class="invoice">

          
            
          
  <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-globe"></i> Raxtan Marketing Pvt. Ltd.
            <small class="pull-right">Date: <asp:Label ID="Lbldate" runat="server" Text=""></asp:Label></small>
          </h2>
        </div>
        <!-- /.col -->
      </div>

  <div class="row invoice-info">
        <div class="col-sm-4 invoice-col">
          From
          <address>
            <strong>Raxtan Marketing Pvt. Ltd.</strong><br>
            House No. A-3, 2nd Floor, Street No.-7, Kiran Garden, New Delhi-110059<br>
            Email : raxtancare@gmail.com<br>
              Ph : +(91) 8750945497
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
                 <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"  Width="100%"  >
                        <Columns>
                                 <asp:TemplateField HeaderText="S.N.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>                       
                            <asp:BoundField  DataField="ProductName" HeaderText="Perticular" />
                               <asp:BoundField  DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField  DataField="Amount" HeaderText="Rate" />
                              <asp:BoundField  DataField="TotalAmount" HeaderText="Amount" />
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
                For Raxtan Marketing Pvt. Ltd.
            </p>
             
        </div>
        <!-- /.col -->
        <div class="col-xs-6">
          <p class="lead">Amount in words: <asp:Label ID="LblAmountinWorld" runat="server" Text=""> </asp:Label> Rupees Only</p>

          <div class="table-responsive">
            <table class="table">
              <tr>
                <th style="width:50%">Subtotal:</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblTotal" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr id="trCgst" runat="server" visible="false">
                <th>CGST : (<asp:Label ID="LblCgstPer" runat="server" Text=""></asp:Label> % )</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblCGST" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr id="trSgst" runat="server" visible="false">
                <th>SGST : (<asp:Label ID="LblSgstPer" runat="server" Text=""></asp:Label> % )</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblSGSt" runat="server" Text=""></asp:Label></td>
              </tr>
                 <tr id="trIgst" runat="server" visible="false">
                <th>IGST : (<asp:Label ID="LblIgstPer" runat="server" Text=""></asp:Label> % )</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblIGST" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr>
                <th>Total :</th>
                <td><i class="fa fa-inr"></i> <asp:Label ID="LblGTotal" runat="server" Text=""></asp:Label></td>
              </tr>
            </table>
          </div>
        </div>
        <!-- /.col -->
      </div>

 


                   </section>
    </div>
    </form>
</body>
</html>

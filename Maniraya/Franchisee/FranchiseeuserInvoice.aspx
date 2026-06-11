<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FranchiseeuserInvoice.aspx.cs" Inherits="FranchiseeuserInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel='stylesheet' href='https://netdna.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'>

<script src='https://netdna.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script>
    <meta charset="UTF-8"/>
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Invoice</title>
    <style>
    td, th {
      border: 1px solid black;
      padding: 8px;
    }
    
    th {
      font-weight: bold;
    }
    .center{
        text-align: center;
    }
    .color{
        background-color: grey;
    }
    </style>
</head>
<body style="width: auto">
    <form id="form1" runat="server">

         <div class="container">
  <div style="text-align: center; padding: 10px; font-size: 24px;">
        TAX INVOICE
    </div>
  
      
  
      <div style="display: flex; margin-top: 5px;">
    <table style="border-collapse: collapse; margin-right: 10px; flex: 4;">

          <tr>


              <td style="width:50%"><img src="logo.png" alt="" style="height: 15%; width: 15%; display: inline-block; float:left;"/></td>
               <td style="width:50%"><h4>MEHDI LINK PRIVATE LIMITED</h4>
                    <h6>HEAD OFFICE - Pranpur, District- katihar, Bihar, 854116, INDIA <br>
CONTACT – 7542868240 <br>EMAIL- support@mehdilink.com <br>WEBSITE – MEHDILINK.COM </h6>
                    <b style="text-align: left; margin-right: 50px;"> COMPANY GSTN - 09AAMCR1468G1Z1  </b><br />
       <b style="text-align: right;"> COMPANY CIN - U24290BR2022PTC059990 </b></td> 
            </tr>

          
      </table> 
   
  </div> 

              <div style="background-color:#4800ff; color:#fff; padding:5px">Bill To </div>

            
           <div style="margin-top: 5px;display: flex; ">

         
    <table style=" margin-right: 10px; flex: 5; border:0px solid #fff ">

          <tr>


              <td style="width:50%;vertical-align:top"">
                <asp:Label ID="LblDistributername" runat="server" Text="Label"></asp:Label><br />
               ADDRESS : <asp:Label ID="LblBillingAddress" runat="server" Text="Label"></asp:Label><br />
            POST  : <asp:Label ID="LblBillingPost" runat="server" Text="Label"></asp:Label><br />
             CITY :   <asp:Label ID="LblBillingCity" runat="server" Text="Label"></asp:Label><br />
               STATE : <asp:Label ID="LblBillingState" runat="server" Text="Label"></asp:Label><br />
           PIN CODE :     <asp:Label ID="LblBillingPincode" runat="server" Text="Label"></asp:Label><br />
             MOBILE :   <asp:Label ID="LblDistributerMobile" runat="server" Text="Label"></asp:Label>

              </td>
               <td style="width:50%; vertical-align:top; text-align:right"">  Date : <asp:Label ID="LblInvoicedate" runat="server" Text="Label"></asp:Label><br />
                  Time : <asp:Label ID="LblInvoiceTime" runat="server" Text="Label"></asp:Label><br />
         Invoice Number : <asp:Label ID="LblInvoiceNumber" runat="server" Text="Label"></asp:Label><br /></td> 
            </tr>

          
      </table> 

                      
                
       
       <table style="border-collapse: collapse; width: 50%; max-width: 500px; flex: 1; margin-right: 5px; display:none">
            <tr>
              <th class="color" colspan="3" >USER DETAILS</th>
            </tr>
            <tr>
              <td>NAME </td>
              <td></td>
            </tr>
            <tr>
              <td>USER ID</td>
              <td><asp:Label ID="LbDistributerlUserid" runat="server" Text="Label"></asp:Label></td>
            </tr>
           
           
              <tr>
                <td>REG.DATE</td>
                <td><asp:Label ID="LblDistributerRegdate" runat="server" Text="Label"></asp:Label></td>
              </tr>
              <tr>
                <td>EMAIL </td>
                <td><asp:Label ID="LblDistributerEmail" runat="server" Text="Label"></asp:Label></td>
              </tr>
             
          </table>
  </div> 
    <div style="display: flex;">
       
    
      </div>  
      <div style="display: flex; margin-top: 5px;">
    
   
   <%--     <table style="border-collapse: collapse; width: 50%; max-width: 500px; flex: 1;">
          <tr>
            <th class="color" colspan="3">SHIPPING DETAILS</th>
          </tr>
          <tr>
            <td>NAME </td>
            <td><asp:Label ID="LblShipppingName" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
            <td>ADDRESS</td>
            <td><asp:Label ID="LblShippingAddress" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
            <td>POST </td>
            <td><asp:Label ID="LblShippingPost" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
              <td>CITY </td>
              <td><asp:Label ID="LblShippingCity" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td>STATE</td>
              <td><asp:Label ID="LblShippingState" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td>PIN CODE</td>
              <td><asp:Label ID="LblShippingPincode" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td>MOBILE</td>
              <td><asp:Label ID="LblShippingMobile" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>--%>
      </div> 
<div style="margin-top: 10px;">
    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
          <Columns>
                                   <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="PRODUCT NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproduct" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblquantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="HCN/SAC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHSNCODE" runat="server" Text='<%#Eval("HSNCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DP/RATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDP" runat="server" Text='<%#Eval("DP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="RMAP" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="BV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBV" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOTAL TAXABLE">
                                        <ItemTemplate>
                                              <asp:Label ID="lblPurchaseAmount" runat="server" Text='<%#Eval("PurchaseAmount") %>'></asp:Label>
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="GST">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblGST" runat="server" Text='<%#Eval("GST") %>'></asp:Label>(
                                              <asp:Label ID="lblGSTper" runat="server" Text='<%#Eval("GSTper") %>'></asp:Label>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                              <asp:Label ID="lblTotalDP" runat="server" Text='<%#Eval("TotalDP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   </Columns>
    </asp:GridView>
    
</div> 

             <br /><br />
         <table style="border-collapse: collapse; width: 40%; flex: 1; float:right">
          
          <tr>
            <td class="center">Total Amount</td>
            <td class="center" style="width: 10px;">:</td>
            <td class="center"><asp:Label ID="LblTTamount" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
            <td class="center">Discount</td>
            <td class="center" style="width: 10px;">:</td>
            <td class="center"><asp:Label ID="LBlDiscount" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
            <td class="center">Payble Amount</td>
            <td class="center" style="width: 10px;">:</td>
            <td class="center"><asp:Label ID="LblRestAmount" runat="server" Text="Label"></asp:Label></td>
          </tr>


        </table>
<div style="display: flex; margin-top: 10px;">
    <table style="border-collapse: collapse; width: 60%; max-width: 500px; margin-right: 10px; flex: 1;">
        
          <tr>
              <td style="height: 50px;" ><b>AMOUNT IN WORDS  : </b> <asp:Label ID="LblPaybleamountwords" runat="server" Text="Label"></asp:Label> ONLY </td>
            </tr>
         <tr>
        <td style="height: 50px;"><b> TOTAL PAYBLE AMOUNT IN FIGURE (INR) :</b> ₹ <asp:Label ID="LblPaybleamount" runat="server" Text="Label"></asp:Label></td>
      </tr>
      </table> 
   
  </div> 
  <div style="display: flex; margin-top: 5px;">
    <table style="border-collapse: collapse; width: 50%; max-width: 1000px; margin-right: 10px; flex: 1;">
        <tr>
            <td class="center color" > <b style="font-size: 18px;">TERMS & CONDITIONS  </td>
          </tr>
          <tr>
              <td> 
                 <ul>
                    <li>SUBJECT TO BIHAR JURISDICTION</li>
                    <li>GST IS NOT REFUNDABLE</li>
                    <li>RETURN YOUR PRODUCT WITHIN 30 DAYS FROM THE DATE OF ACTIVATION (T&C APPLY)</li>
                </ul> </td>
            </tr>
      </table> 
    <table style="border-collapse: collapse; max-width: 500px; flex: 1;">
      <tr>
        <th class="color">AUTHORISED SIGNATORY</th>
      </tr>
      <tr>
        <td style="height: 100px;" class="center"> <br> <br>  </br></br> <b>Mehdi Link Private Limited</b></td>
      </tr>
    </table>
  </div> 
  <div style="text-align: center; padding: 20px; font-size: 20px; font-weight: bold;">
    THIS IS A COMPUTER GENERATED INVOICE
  </div>
             </div>
    </form>
</body>
</html>

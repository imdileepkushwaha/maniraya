<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="addtocart.aspx.cs" Inherits="addtocart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
     <link rel="stylesheet" href="myassets/assets/css/add-to-cart.css" />
    <style type="text/css">
       .cart-line-item {
  display: grid;
  grid-template-columns:
    60px
    1fr
    120px
    120px
    40px;
  align-items: center;
  gap: 16px;
}

.cart-remove-btn {
  width: 32px;
  height: 32px;

  border: none;
  background: #f3f3f3;
  color: #333;

  border-radius: 6px;
  cursor: pointer;

  font-size: 16px;

  transition: 0.3s;
}

.cart-remove-btn:hover {
  background: #ff4d4f;
  color: white;
}
    </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
     <asp:HiddenField ID="hfwalllet" runat="server" />
      <main class="cart-page-main">
    <section class="cart-page-hero">
      <div class="container">
        <p class="cart-page-kicker">Your Shopping Cart</p>
        <h1>Items added to cart</h1>
        <p>Review your products, apply coupon, and continue to secure checkout.</p>
        <div class="cart-hero-meta">
          <span>Free shipping above $200</span>
          <span>Easy 14-day return</span>
          <span>Secure checkout</span>
        </div>
      </div>
    </section>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">

<ContentTemplate>
    <section class="cart-page-content">
      <div class="container cart-page-grid">
        <div class="cart-items-card">
          <div class="cart-card-head">
            <h2>Cart Items</h2>
            <div class="cart-card-head-right">
              <span> <asp:Label ID="LblProductcount" runat="server" Text=""></asp:Label></span>
                 <asp:Button 
     runat="server"
     Text="Clear Cart" OnClick="Unnamed_Click" CssClass="ghost-btn" 
    />
              
            </div>
          </div>
            <asp:Repeater ID="rptCart" runat="server">
    <ItemTemplate>

        <article class="cart-line-item">

            <div class="cart-thumb thumb-blue">
                <asp:HiddenField
                    ID="hdnProductId"
                    runat="server"
                    Value='<%# Eval("ProductId") %>' />
                <asp:HiddenField
                    ID="hdnProductName"
                    runat="server"
                    Value='<%# Eval("Productname") %>' />
                <asp:HiddenField
                    ID="hdnSubProductId"
                    runat="server"
                    Value='<%# Eval("SubProductId") %>' />
                <asp:HiddenField
                    ID="hdnMRP"
                    runat="server"
                    Value='<%# Eval("MRP") %>' />
                <asp:HiddenField
                    ID="hdnAmount"
                    runat="server"
                    Value='<%# Eval("Amount") %>' />
                <asp:HiddenField
                    ID="hdnDP"
                    runat="server"
                    Value='<%# Eval("DP") %>' />
                <asp:HiddenField
                    ID="hdnTOTALDP"
                    runat="server"
                    Value='<%# Eval("TOTALDP") %>' />
                <asp:HiddenField
                    ID="hdnBV"
                    runat="server"
                    Value='<%# Eval("BV") %>' />
                <asp:HiddenField
                    ID="hdnTOTALBV"
                    runat="server"
                    Value='<%# Eval("TOTALBV") %>' />
                <asp:HiddenField
                    ID="hdnGST"
                    runat="server"
                    Value='<%# Eval("GST") %>' />
                <asp:HiddenField
                    ID="hdnQuantity"
                    runat="server"
                    Value='<%# Eval("Quantity") %>' />
                <asp:HiddenField
                    ID="hdnTOTALAMOUNT"
                    runat="server"
                    Value='<%# Eval("TOTALAMOUNT") %>' />
                <asp:HiddenField
                    ID="hdnpurchaseAmount"
                    runat="server"
                    Value='<%# Eval("purchaseamount") %>' />
                <asp:HiddenField
                    ID="hdnCGST"
                    runat="server"
                    Value='<%# Eval("CGST") %>' />
                <asp:HiddenField
                    ID="hdnSGST"
                    runat="server"
                    Value='<%# Eval("SGST") %>' />
                <asp:HiddenField
                    ID="hdnIGST"
                    runat="server"
                    Value='<%# Eval("IGST") %>' />
                <asp:HiddenField
                    ID="hdnFranchiseeId"
                    runat="server"
                    Value='<%# Eval("Franchiseeid") %>' />
                <%# Eval("ProductName").ToString().Substring(0,2) %>
            </div>

            <div class="cart-item-info">
                <h3><%# Eval("ProductName") %></h3>
                 <p>Color: <%# Eval("ColorName") %> | Size: <%# Eval("SizeName") %></p>
               
            </div>

            <div class="cart-qty-box" aria-label="Quantity">

                <asp:Button 
                    runat="server" BorderStyle="None"
                    Text="-" 
                    CommandArgument='<%# Eval("Id") %>'
                    OnCommand="DecreaseQty" aria-label="Decrease quantity"/>
                
             

                <span><%# Eval("Quantity") %></span>

              <asp:Button 
                    runat="server" BorderStyle="None"
                    Text="+" 
                    CommandArgument='<%# Eval("Id") %>'
                    OnCommand="IncreaseQty" />

                                                                        

            </div>
                          
            <div class="cart-line-price">
                <strong>$<%# Eval("Amount") %></strong>
                <span>$<%# Eval("MRP") %></span>
            </div>
              <asp:Button 
        runat="server"
        Text="✕" 
        CommandArgument='<%# Eval("Id") %>'
        OnCommand="RemoveQty" class="cart-remove-btn"/>
          
                  
                  
        </article>

    </ItemTemplate>
</asp:Repeater>
        

          <div class="cart-delivery-progress" aria-label="Free shipping progress">
            <div class="cart-progress-copy">
              <strong>Almost there!</strong>
              <span>Add $32 more for free express shipping.</span>
            </div>
            <div class="cart-progress-track">
              <span class="cart-progress-fill" style="width: 84%"></span>
            </div>
          </div>

          <div class="cart-bottom-actions">
            <a href="index.aspx" class="secondary-btn">Continue Shopping</a>
              <asp:Button 
    ID="btnUpdateCart"
    runat="server"
    Text="Update Cart"
    CssClass="ghost-btn"
   />
           
          </div>
        </div>

        <aside class="order-summary-card">
          <h2>Order Summary</h2>
          <p class="summary-subtitle">Apply coupon to save more on your order.</p>

          <div class="coupon-row" action="#">
              <asp:TextBox 
    ID="txtCoupon"
    runat="server"
    placeholder="Enter coupon code" aria-label="Coupon code"/>
           
           <asp:Button 
    ID="btnApplyCoupon"
    runat="server"
    Text="Apply"
    />
          </div>

          <div class="summary-list">
            <div>
              <span>Subtotal</span>
                <asp:Label ID="lblSubtotal" runat="server" Text="00"></asp:Label>
              
            </div>
            <div>
              <span>Discount</span>
             <asp:Label ID="lblDiscount" runat="server" Text="00"></asp:Label>
            </div>
            <div>
              <span>Shipping</span>
                 <asp:Label ID="lblShipping" runat="server" Text="00"></asp:Label>
           
            </div>
            <div>
              <span>Tax</span>
            <asp:Label ID="lblTax" runat="server" Text="00"></asp:Label>
            </div>
                <div>
    <span>Wallet Deduction</span>
  <asp:Label ID="Lblwalletdeduction" runat="server" Text="00"></asp:Label>
  </div>
          </div>

          <div class="summary-total">
            <span>Total Payable</span>
           <asp:Label 
    ID="lblTotal"
    runat="server"
    Text="00"></asp:Label>
          </div>
    <asp:LinkButton ID="LinkButton1" CssClass="checkout-btn-static" runat="server" PostBackUrl="~/addaddress.aspx">Proceed to Checkout</asp:LinkButton>
            <asp:Button 
    ID="btnCheckout"
    runat="server"
    Text="Proceed to Checkout"
    CssClass="checkout-btn-static" Visible="false" OnClick="btnCheckout_Click"
     />
        
          <div class="summary-payments" aria-label="Accepted payments">
            <img src="myassets/assets/images/payment/1.svg" alt="Payment method" />
            <img src="myassets/assets/images/payment/2.svg" alt="Payment method" />
            <img src="myassets/assets/images/payment/3.svg" alt="Payment method" />
            <img src="myassets/assets/images/payment/4.svg" alt="Payment method" />
          </div>
          <p class="secure-note">100% Secure payment with trusted gateways.</p>
          <ul class="summary-benefits">
            <li>No-cost EMI options available</li>
            <li>Trusted seller warranty support</li>
            <li>Safe delivery with order tracking</li>
          </ul>
        </aside>
      </div>
    </section>
    </ContentTemplate>
              </asp:UpdatePanel>
  </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


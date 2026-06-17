<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="Productdetail.aspx.cs" Inherits="Productdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
   <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
    $(document).ready(function () {

        $(".pd-color").click(function () {

            $(".pd-color").removeClass("is-active");

            $(this).addClass("is-active");

            $("#<%= hfColor.ClientID %>").val($(this).attr("title"));
            GetStock();

        });      
    });

    $(document).ready(function () {
        $(".pd-thumb").on("click", function () {
            $(".pd-thumb").removeClass("is-active");
            $(this).addClass("is-active");
            var thumbSrc = $(this).find("img").attr("src");
            if (thumbSrc) {
                $("#<%= Image5.ClientID %>").attr("src", thumbSrc);
            }
        });
    });

    $(document).ready(function () {

        $(".pd-size").click(function () {

            $(".pd-size").removeClass("is-active");

            $(this).addClass("is-active");

            $("#<%= hfSize.ClientID %>").val($(this).attr("title"));
            GetStock();

        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hfproductid" runat="server" />
     <asp:HiddenField ID="hfsubcategoryid" runat="server" />
     <asp:HiddenField ID="hffranchiseeid" runat="server" Value="F000001" />
    <asp:HiddenField ID="hfColor" runat="server" />
    <asp:HiddenField ID="hfSize" runat="server" />
     <asp:HiddenField ID="hfstock" runat="server" />
     <main class="product-detail-page-main">
   <section class="product-detail-top">
     <div class="container">
       <div class="product-detail-top-grid">
         <div class="pd-gallery">
           <div class="pd-thumbs">
             <button class="pd-thumb is-active" type="button" aria-label="Blue hoodie">
               <asp:Image ID="Image1" runat="server" alt="Blue hoodie thumb"/>              
             </button>
             <button class="pd-thumb" type="button" aria-label="Pink hoodie">
                  <asp:Image ID="Image2" runat="server" alt="Pink hoodie thumb"/>
             
             </button>
             <button class="pd-thumb" type="button" aria-label="Brown hoodie">
                  <asp:Image ID="Image3" runat="server" alt="Brown hoodie thumb"/>
             
             </button>
             <button class="pd-thumb" type="button" aria-label="Grey hoodie">
                  <asp:Image ID="Image4" runat="server" alt="Grey hoodie thumb"/>
            
             </button>
           </div>
           <div class="pd-main-image-wrap">
                <asp:Image ID="Image5" runat="server" alt="Full Sleeve Hoodie Jacket" CssClass="pd-main-image" />
            
           </div>
         </div>

         <div class="pd-summary">
           <div class="pd-summary-head">
             <p class="pd-breadcrumb-mini"><asp:Label ID="lblSubCategory" runat="server" /></p>
             <h1><asp:Label ID="lblProductName" runat="server"></asp:Label></h1>
             <div class="pd-stock-row">
               <span class="pd-in-stock"><asp:Label ID="lblstock" runat="server"></asp:Label></span>
               <div class="product-rating pd-rating">
                 <span class="stars-filled">★★★★</span><span class="stars-empty">☆</span>
                 <span class="rating-value">(93 Reviews)</span>
               </div>
             </div>
           </div>

           <div class="pd-price-card">
             <div class="pd-price-main">
               <span class="pd-price-label">Offer Price</span>
               <div class="pd-price-row">
                 <strong>₹<asp:Label ID="lblPrice" runat="server" /></strong>
                 <span class="product-old-price">₹<asp:Label ID="lblMRP" runat="server" /></span>
               </div>
             </div>
             <span class="pd-price-badge">Member Deal</span>
           </div>

           <p class="pd-short-desc">
             <asp:Label ID="lblDescription" runat="server" />
           </p>

           <div class="pd-options-block">
           <div class="pd-option-row">
             <span class="pd-option-label">Color</span>
             <div class="pd-color-list">
             <asp:Repeater ID="rptColors" runat="server">

<ItemTemplate>

<button
type="button"
class='pd-color <%# Container.ItemIndex == 0 ? "is-active" : "" %>' style='--c:<%# Eval("ColorCode") %>;'
title='<%# Eval("ColorId") %>'>
 
</button>
    
</ItemTemplate>

</asp:Repeater>
             </div>
           </div>

           <div class="pd-option-row">
             <span class="pd-option-label">Size</span>
             <div class="pd-size-list">
             <asp:Repeater ID="rptSizes" runat="server">

<ItemTemplate>

<button
type="button"
class='pd-size <%# Container.ItemIndex == 0 ? "is-active" : "" %>' title='<%# Eval("SizeID") %>'>

<%# Eval("SizeName") %>
     
</button>
  
</ItemTemplate>

</asp:Repeater>
             </div>
           </div>
           </div>

           <div class="pd-action-links">
             <a href="#">
               <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M12 21s-7-4.6-7-10a4 4 0 0 1 7-2.6A4 4 0 0 1 19 11c0 5.4-7 10-7 10Z" stroke="currentColor" stroke-width="1.8"/></svg>
               Wishlist
             </a>
             <a href="#">
               <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M8 7h12M8 12h12M8 17h12M4 7h.01M4 12h.01M4 17h.01" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
               Compare
             </a>
             <a href="#">
               <svg width="14" height="14" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M21 15a4 4 0 0 1-4 4H8l-5 3V7a4 4 0 0 1 4-4h10a4 4 0 0 1 4 4v8Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/></svg>
               Ask a question
             </a>
           </div>

           <div class="pd-cart-row">
             <asp:LinkButton ID="lnkaddtocart" class="add-btn pd-add-btn" runat="server" OnClick="lnkaddtocart_Click">
               <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/><path d="M9 11v6M15 11v6M8 7V5a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
               Add to Cart
             </asp:LinkButton>
             <a class="view-btn pd-continue-btn" href="index.aspx">Continue Shopping</a>
           </div>

           <ul class="pd-meta-list">
             <li><span class="pd-meta-key">BV</span><span class="pd-meta-val"><asp:Label ID="LblBV" runat="server" /></span></li>
             <li><span class="pd-meta-key">Category</span><span class="pd-meta-val"><asp:Label ID="Lblcategory" runat="server" /></span></li>
             <li><span class="pd-meta-key">Delivery</span><span class="pd-meta-val">2-5 Business Days</span></li>
           </ul>
         </div>

         <aside class="pd-side-info">
           <ul>
             <li>Shipping Worldwide</li>
             <li>Always Authentic</li>
             <li>Cash On Delivery Available</li>
           </ul>
           <div class="pd-warranty-box">
             <h4>Return & Warranty</h4>
             <p>14 Days Easy Return</p>
             <p>Warranty Not Available</p>
           </div>
           <div class="pd-chat-box">
             <p>Sold by Maniraya Store</p>
             <button type="button" class="view-btn">Chat Now</button>
           </div>
         </aside>
       </div>
     </div>
   </section>

   <section class="product-detail-body-section">
     <div class="container">
       <div class="product-detail-body-grid">
         <div class="pd-tabs-wrap">
           <div class="pd-tabs">
             <button type="button" class="pd-tab is-active">Description</button>
             <button type="button" class="pd-tab" style="display:none;">Additional info</button>
             <button type="button" class="pd-tab" style="display:none;">Vendor</button>
             <button type="button" class="pd-tab" style="display:none;">Reviews</button>
           </div>
           <article class="pd-tab-panel is-active">
             <h3>Description</h3>
             <asp:Literal ID="litFullDescription" runat="server"></asp:Literal>
           </article>
           <article class="pd-tab-panel" style="display:none;">
             <h3>Additional Information</h3>
             <ul>
               <li><strong>Material:</strong> 80% Cotton, 20% Polyester</li>
               <li><strong>Fit Type:</strong> Regular Fit</li>
               <li><strong>Sleeve:</strong> Full Sleeve</li>
               <li><strong>Neck:</strong> Hooded</li>
               <li><strong>Care:</strong> Machine wash cold, tumble dry low</li>
             </ul>
           </article>
           <article class="pd-tab-panel" style="display:none;">
             <h3>Vendor</h3>
             <p>
               Sold and fulfilled by <strong>Maniraya Store</strong>, known for quality fashion essentials and fast
               dispatch.
             </p>
             <ul>
               <li>Average dispatch time: 24 hours</li>
               <li>On-time shipping: 100%</li>
               <li>Response rate: 98%</li>
             </ul>
           </article>
           <article class="pd-tab-panel" style="display:none;">
             <h3>Reviews</h3>
             <p><strong>Overall rating:</strong> 4.5 / 5 (320 reviews)</p>
             <ul>
               <li>"Fabric quality is very good and comfortable for daily use."</li>
               <li>"Color and fitting are exactly as expected."</li>
               <li>"Value for money product with quick delivery."</li>
             </ul>
           </article>
         </div>

         <aside class="pd-side-rating-card" style="display:none;">
           <p><strong>4.5</strong> (320)</p>
           <p>Ship on Time: 100%</p>
           <p>Chat Response Rate: 98%</p>
           <a href="#">Go To Store</a>
         </aside>
       </div>
     </div>
   </section>
 </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScript" Runat="Server">
    <script>
        function GetStock() {
            let franchiseeid = $("#<%= hffranchiseeid.ClientID %>").val();
            let color = $("#<%= hfColor.ClientID %>").val();
            let size = $("#<%= hfSize.ClientID %>").val();
            let productId = $("#<%= hfproductid.ClientID %>").val();
            $.ajax({

                type: "POST",

                url: "Productdetail.aspx/GetStock",

                data: JSON.stringify({
                    franchiseeid: franchiseeid,
                    color: color,
                    size: size,
                    productId: productId
                }),

                contentType: "application/json; charset=utf-8",

                dataType: "json",

                success: function (response) {

                    let stock = response.d;

                    if (stock > 0) {

                        $("#<%= lblstock.ClientID %>")
                            .html("In Stock: " + stock)
                            .css("color", "green");
                        
                        $("#<%= hfstock.ClientID %>").val(stock);
                    }
                    else {

                        $("#<%= lblstock.ClientID %>")
                            .html("Out of Stock")
                            .css("color", "red");
                        $("#<%= hfstock.ClientID %>").val(stock);

                    }

                }

            });

        }
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs"
    Inherits="index" %>



    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
            .no-underline {
                text-decoration: none !important;
                color: inherit !important;
                display: block;
            }

            .no-underline:hover {
                text-decoration: none !important;
            }

            .category-card {
                cursor: pointer;
            }
        </style>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section class="hero">
            <div class="container">
                <div class="hero-shell">
                    <div class="hero-main">
                        <div id="selectedCarousel" class="selected-carousel" aria-label="Promotional carousel">
                            <div class="selected-carousel-track">
                                <article class="selected-slide slide-one is-active">
                                    <img class="selected-slide-image"
                                        src="https://images.unsplash.com/photo-1511556532299-8f662fc26c06?auto=format&fit=crop&w=1400&q=80"
                                        alt="Smartwatch and accessories" />
                                    <div class="selected-slide-content">
                                        <p class="selected-kicker">New Launch</p>
                                        <h2>Smart Shopping <span class="hero-highlight">Starts Here</span></h2>
                                        <p class="hero-slide-desc">Discover premium electronics, fashion and daily
                                            essentials — curated for you at unbeatable prices.</p>
                                        <div class="hero-slide-actions">
                                            <a href="#products" class="btn-primary">Shop Now</a>
                                            <a href="signup.aspx" class="btn-hero-outline">Join Free</a>
                                        </div>
                                    </div>
                                </article>
                                <article class="selected-slide slide-two">
                                    <img class="selected-slide-image"
                                        src="https://images.unsplash.com/photo-1523381210434-271e8be1f52b?auto=format&fit=crop&w=1400&q=80"
                                        alt="Fashion clothing showcase" />
                                    <div class="selected-slide-content">
                                        <p class="selected-kicker">Limited Offer</p>
                                        <h2>Flat <span class="hero-highlight">40% OFF</span> On Top Picks</h2>
                                        <p class="hero-slide-desc">Trending products with secure checkout, fast delivery
                                            and hassle-free returns.</p>
                                        <div class="hero-slide-actions">
                                            <a href="#products" class="btn-primary">View Deals</a>
                                            <a href="#products" class="btn-hero-outline">Browse All</a>
                                        </div>
                                    </div>
                                </article>
                                <article class="selected-slide slide-three">
                                    <img class="selected-slide-image"
                                        src="https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=1400&q=80"
                                        alt="Home essentials setup" />
                                    <div class="selected-slide-content">
                                        <p class="selected-kicker">Today Only</p>
                                        <h2>Upgrade Your <span class="hero-highlight">Home &amp; Lifestyle</span></h2>
                                        <p class="hero-slide-desc">Premium quality products for modern everyday living —
                                            handpicked by Maniraya.</p>
                                        <div class="hero-slide-actions">
                                            <a href="#products" class="btn-primary">Start Shopping</a>
                                            <a href="#about" class="btn-hero-outline">Learn More</a>
                                        </div>
                                    </div>
                                </article>
                            </div>
                            <button class="hero-carousel-btn hero-carousel-prev" type="button"
                                aria-label="Previous slide">
                                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                    <path d="M15 18L9 12L15 6" stroke="currentColor" stroke-width="2"
                                        stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                            </button>
                            <button class="hero-carousel-btn hero-carousel-next" type="button" aria-label="Next slide">
                                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                    <path d="M9 18L15 12L9 6" stroke="currentColor" stroke-width="2"
                                        stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                            </button>
                            <div class="selected-carousel-footer">
                                <span class="hero-slide-counter"><strong class="hero-slide-current">01</strong> /
                                    03</span>
                                <div class="selected-carousel-dots" role="tablist" aria-label="Carousel navigation">
                                    <button class="selected-dot is-active" type="button" aria-label="Go to slide 1"
                                        data-slide-to="0"></button>
                                    <button class="selected-dot" type="button" aria-label="Go to slide 2"
                                        data-slide-to="1"></button>
                                    <button class="selected-dot" type="button" aria-label="Go to slide 3"
                                        data-slide-to="2"></button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <aside class="hero-side" aria-label="Featured offers">
                        <article class="hero-promo-card hero-promo-primary">
                            <span class="hero-promo-badge">Promo Code</span>
                            <h3>Extra 20% Off</h3>
                            <p>Use code <strong>MPremium</strong> on your first order. Limited time only.</p>
                            <a href="#products" class="hero-promo-link">Claim Offer &rarr;</a>
                        </article>
                        <article class="hero-promo-card hero-promo-secondary">
                            <span class="hero-promo-badge">Member Rewards</span>
                            <h3>Earn While You Shop</h3>
                            <p>Join Maniraya today and unlock exclusive deals, wallet rewards &amp; more.</p>
                            <a href="signup.aspx" class="hero-promo-link">Sign Up Free &rarr;</a>
                        </article>
                    </aside>
                </div>
            </div>
        </section>
        <section class="features-section" aria-label="Store highlights">
            <div class="container">
                <div class="features-grid">
                    <article class="feature-card feature-card-gold">
                        <div class="feature-card-icon" aria-hidden="true">
                            <img src="myassets/assets/images/feature-icon_1.svg" alt="" />
                        </div>
                        <div class="feature-card-body">
                            <h3>Return &amp; Refund</h3>
                            <p>7-day hassle-free money back guarantee on eligible orders.</p>
                        </div>
                    </article>
                    <article class="feature-card feature-card-blue">
                        <div class="feature-card-icon" aria-hidden="true">
                            <img src="myassets/assets/images/feature-icon_3.svg" alt="" />
                        </div>
                        <div class="feature-card-body">
                            <h3>Quality Support</h3>
                            <p>Dedicated help team available online 24/7 for every query.</p>
                        </div>
                    </article>
                    <article class="feature-card feature-card-green">
                        <div class="feature-card-icon" aria-hidden="true">
                            <img src="myassets/assets/images/feature-icon_2.svg" alt="" />
                        </div>
                        <div class="feature-card-body">
                            <h3>Secure Payment</h3>
                            <p>Encrypted checkout with trusted and protected payment options.</p>
                        </div>
                    </article>
                    <article class="feature-card feature-card-navy">
                        <div class="feature-card-icon" aria-hidden="true">
                            <img src="myassets/assets/images/feature-icon_4.svg" alt="" />
                        </div>
                        <div class="feature-card-body">
                            <h3>Daily Offers</h3>
                            <p>Fresh deals and member-only discounts added every single day.</p>
                        </div>
                    </article>
                </div>
            </div>
        </section>

        <section class="top-categories">
            <div class="container">
                <div class="categories-section-head">
                    <div class="categories-head-text">
                        <span class="section-eyebrow">Shop by Category</span>
                        <h2>Top <span>Categories</span></h2>
                        <p>Explore curated collections across our most-loved shopping categories.</p>
                    </div>
                    <a href="categories.aspx" class="categories-view-all">View All</a>
                </div>
                <div class="category-grid">
                    <asp:Repeater ID="rptcategory" runat="server" OnItemCommand="rptCategories_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCategory" runat="server" CommandName="SelectCategory"
                                CommandArgument='<%# Eval("CategoryId") %>' CssClass="category-card">
                                <div class="category-card-media">
                                    <img src='<%# Eval("Image") %>' alt='<%# Eval("CategoryName") %>' loading="lazy" />
                                    <span class="category-card-shade" aria-hidden="true"></span>
                                </div>
                                <div class="category-card-body">
                                    <div class="category-card-info">
                                        <h3>
                                            <%# Eval("CategoryName") %>
                                        </h3>
                                        <span class="category-card-meta">Shop collection</span>
                                    </div>
                                    <span class="category-card-arrow" aria-hidden="true">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
                                            <path d="M5 12H19M19 12L13 6M19 12L13 18" stroke="currentColor"
                                                stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                        </svg>
                                    </span>
                                </div>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </section>



        <section id="products" class="products-section">
            <div class="container">
                <div class="products-section-head">
                    <div class="products-head-text">
                        <span class="section-eyebrow">Curated For You</span>
                        <h2>Featured <span>Products</span></h2>
                        <p>Handpicked bestsellers across every category — quality picks at great prices.</p>
                    </div>
                    <a href="products.aspx" class="products-view-cart">View All</a>
                </div>
                <asp:DropDownList ID="ddcountry" runat="server" Visible="false" AutoPostBack="true"
                    OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                    <asp:ListItem Value="0">All Categories</asp:ListItem>
                </asp:DropDownList>
                <div id="productGrid" class="product-grid">

                    <asp:Repeater ID="rptProducts" runat="server">

                        <ItemTemplate>

                            <article class="product-card">
                                <div class="product-card-media">
                                    <%# Convert.ToDecimal(Eval("MRP"))> Convert.ToDecimal(Eval("Amount")) ? "<span class=\"product-badge product-badge-sale\">-" +
                                            Math.Round((Convert.ToDecimal(Eval("MRP")) -
                                            Convert.ToDecimal(Eval("Amount"))) / Convert.ToDecimal(Eval("MRP")) * 100) +
                                            "%</span>" : "" %>
                                        <a class="product-image-link"
                                            href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                            <div class="product-image">
                                                <img src='<%# Eval("Image") %>' alt='<%# Eval("ProductName") %>'
                                                    loading="lazy" />
                                            </div>
                                            <span class="product-card-overlay" aria-hidden="true">
                                                <span class="product-card-overlay-btn">Quick View</span>
                                            </span>
                                        </a>
                                </div>
                                <div class="product-card-body">
                                    <div class="product-card-head">
                                        <span class="product-category-tag">
                                            <%# Eval("categoryName") %>
                                        </span>
                                    </div>
                                    <h3 class="product-title">
                                        <a
                                            href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                            <%# Eval("ProductName") %>
                                        </a>
                                    </h3>
                                    <div class="product-price-block">
                                        <div class="product-price-main">
                                            <span class="price">&#8377; <%# Eval("Amount") %></span>
                                            <span class="product-old-price">&#8377; <%# Eval("MRP") %></span>
                                        </div>
                                        <%# Convert.ToInt32(Eval("discount"))> 0 ? "<span class=\"product-discount\">Save " + Eval("discount") + "%</span>" : "" %>
                                    </div>
                                    <div class="product-card-actions">
                                        <a class="view-btn"
                                            href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                            <svg width="15" height="15" viewBox="0 0 24 24" fill="none"
                                                aria-hidden="true">
                                                <path
                                                    d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z"
                                                    stroke="currentColor" stroke-width="1.7" />
                                                <circle cx="12" cy="12" r="3" stroke="currentColor"
                                                    stroke-width="1.7" />
                                            </svg>
                                            <span>View</span>
                                        </a>
                                        <a class="add-btn"
                                            href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                            <svg width="15" height="15" viewBox="0 0 24 24" fill="none"
                                                aria-hidden="true">
                                                <path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor"
                                                    stroke-width="1.8" stroke-linejoin="round" />
                                                <path d="M9 11v6M15 11v6" stroke="currentColor" stroke-width="1.8"
                                                    stroke-linecap="round" />
                                            </svg>
                                            <span>Add to Cart</span>
                                        </a>
                                    </div>
                                </div>
                            </article>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>
            </div>
        </section>


        <section id="about" class="about-section">
            <div class="container">
                <div class="about-section-head">
                    <span class="section-eyebrow">Why Maniraya</span>
                    <h2>Your Trusted <span>Shopping Partner</span></h2>
                    <p>We are redefining online shopping with curated quality, secure payments, and service you can
                        count on every day.</p>
                </div>

                <div class="about-main">
                    <div class="about-visual">
                        <div class="about-visual-frame">
                            <img src="https://images.unsplash.com/photo-1607082348824-0a96f2a4b9da?auto=format&fit=crop&w=900&q=80"
                                alt="Happy customers shopping online" loading="lazy" />
                            <span class="about-visual-shade" aria-hidden="true"></span>
                        </div>
                        <div class="about-float-card about-float-rating">
                            <span class="about-float-icon" aria-hidden="true">&#9733;</span>
                            <div>
                                <strong>4.9 / 5</strong>
                                <span>Store Rating</span>
                            </div>
                        </div>
                        <div class="about-float-card about-float-shoppers">
                            <strong>10K+</strong>
                            <span>Happy Shoppers</span>
                        </div>
                    </div>

                    <div class="about-info">
                        <div class="about-info-card">
                            <div class="about-info-top">
                                <span class="about-info-label">Our Promise</span>
                                <h3>Built for the way you <span>shop today</span></h3>
                                <p class="about-info-lead">From discovery to doorstep delivery, Maniraya brings together
                                    premium products, wallet rewards, and a seamless checkout experience — all in one
                                    trusted platform.</p>
                            </div>

                            <div class="about-features-grid">
                                <article class="about-feature-card about-feature-secure">
                                    <span class="about-feature-step" aria-hidden="true">01</span>
                                    <span class="about-feature-icon" aria-hidden="true">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                            <path
                                                d="M12 2L3 7V11C3 16.55 6.84 21.74 12 23C17.16 21.74 21 16.55 21 11V7L12 2Z"
                                                stroke="currentColor" stroke-width="1.8" stroke-linejoin="round" />
                                        </svg>
                                    </span>
                                    <div class="about-feature-body">
                                        <h4>Secure Payments</h4>
                                        <p>Encrypted checkout with trusted and protected payment gateways.</p>
                                    </div>
                                </article>
                                <article class="about-feature-card about-feature-delivery">
                                    <span class="about-feature-step" aria-hidden="true">02</span>
                                    <span class="about-feature-icon" aria-hidden="true">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                            <path d="M3 7H21L19 21H5L3 7ZM3 7L2 3H1" stroke="currentColor"
                                                stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round" />
                                            <circle cx="9" cy="20" r="1" fill="currentColor" />
                                            <circle cx="17" cy="20" r="1" fill="currentColor" />
                                        </svg>
                                    </span>
                                    <div class="about-feature-body">
                                        <h4>Fast Delivery</h4>
                                        <p>Quick dispatch with same-day and next-day options on select items.</p>
                                    </div>
                                </article>
                                <article class="about-feature-card about-feature-support">
                                    <span class="about-feature-step" aria-hidden="true">03</span>
                                    <span class="about-feature-icon" aria-hidden="true">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                            <path
                                                d="M12 8V12L15 15M21 12C21 16.97 16.97 21 12 21C7.03 21 3 16.97 3 12C3 7.03 7.03 3 12 3C16.97 3 21 7.03 21 12Z"
                                                stroke="currentColor" stroke-width="1.8" stroke-linecap="round" />
                                        </svg>
                                    </span>
                                    <div class="about-feature-body">
                                        <h4>24/7 Support</h4>
                                        <p>Dedicated help team available round the clock via chat and email.</p>
                                    </div>
                                </article>
                                <article class="about-feature-card about-feature-returns">
                                    <span class="about-feature-step" aria-hidden="true">04</span>
                                    <span class="about-feature-icon" aria-hidden="true">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
                                            <path
                                                d="M4 12C4 7.58 7.58 4 12 4C16.42 4 20 7.58 20 12M7 12V13C7 14.1 7.9 15 9 15H10L12 17L14 15H15C16.1 15 17 14.1 17 13V12"
                                                stroke="currentColor" stroke-width="1.8" stroke-linecap="round"
                                                stroke-linejoin="round" />
                                        </svg>
                                    </span>
                                    <div class="about-feature-body">
                                        <h4>Easy Returns</h4>
                                        <p>Hassle-free 7-day return policy on eligible product orders.</p>
                                    </div>
                                </article>
                            </div>

                            <div class="about-trust-row" aria-label="Trust badges">
                                <span class="about-trust-pill">SSL Secured</span>
                                <span class="about-trust-pill">Verified Seller</span>
                                <span class="about-trust-pill">COD Available</span>
                            </div>

                            <div class="about-actions">
                                <a href="#products" class="btn-primary about-shop-btn">Start Shopping</a>
                                <a id="lnkAboutContact" runat="server" href="mailto:info@mpremium.in" class="btn-hero-outline about-outline-btn">Contact
                                    Us</a>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="about-metrics" aria-label="Company highlights">
                    <div class="about-metric">
                        <strong>10K+</strong>
                        <span>Happy Customers</span>
                    </div>
                    <div class="about-metric">
                        <strong>500+</strong>
                        <span>Quality Products</span>
                    </div>
                    <div class="about-metric">
                        <strong>50+</strong>
                        <span>Cities Served</span>
                    </div>
                    <div class="about-metric">
                        <strong>98%</strong>
                        <span>Satisfaction Rate</span>
                    </div>
                </div>
            </div>
        </section>
        <section class="coupon-newsletter" aria-label="Newsletter signup">
            <div class="container">
                <div class="coupon-banner">
                    <div class="coupon-banner-offer">
                        <span class="coupon-banner-eyebrow">Exclusive Offer</span>
                        <div class="coupon-banner-discount">
                            <span class="coupon-banner-percent">70%</span>
                            <span class="coupon-banner-off-label">OFF</span>
                        </div>
                        <p class="coupon-banner-note">On your first order</p>
                        <div class="coupon-banner-code">
                            <span>Use code</span>
                            <strong>MPremium</strong>
                        </div>
                    </div>
                    <div class="coupon-banner-signup">
                        <span class="section-eyebrow">Stay Updated</span>
                        <h2>Get Your Discount Coupon</h2>
                        <p>Subscribe to our newsletter and unlock member-only deals, early access sales, and special
                            offers.</p>
                        <div class="coupon-subscribe-form" role="group" aria-label="Newsletter email signup">
                            <div class="coupon-input-wrap">
                                <svg class="coupon-input-icon" width="18" height="18" viewBox="0 0 24 24" fill="none"
                                    aria-hidden="true">
                                    <path d="M4 6h16a1 1 0 0 1 1 1v10a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V7a1 1 0 0 1 1-1Z"
                                        stroke="currentColor" stroke-width="1.6" />
                                    <path d="m4 7 8 6 8-6" stroke="currentColor" stroke-width="1.6"
                                        stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                                <input type="email" id="couponEmailInput" placeholder="Enter your email address"
                                    autocomplete="email" aria-label="Email address" />
                            </div>
                            <button type="button" class="coupon-subscribe-btn">
                                Subscribe
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                    <path d="M5 12h14M13 6l6 6-6 6" stroke="currentColor" stroke-width="2"
                                        stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                            </button>
                        </div>
                        <p class="coupon-disclaimer">No spam. Unsubscribe anytime.</p>
                        <p class="coupon-feedback" aria-live="polite"></p>
                    </div>
                </div>
            </div>
        </section>

        <a id="lnkWhatsAppFloat" runat="server" href="https://wa.me/918884448586" class="whatsapp-float"
            target="_blank" rel="noopener noreferrer" aria-label="Chat with us on WhatsApp" title="Chat on WhatsApp">
            <span class="whatsapp-float-pulse" aria-hidden="true"></span>
            <svg class="whatsapp-float-icon" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
                <path
                    d="M17.472 14.382c-.297-.149-1.758-.867-2.03-.967-.273-.099-.471-.148-.67.15-.197.297-.767.966-.94 1.164-.173.199-.347.223-.644.075-.297-.15-1.255-.463-2.39-1.475-.883-.788-1.48-1.761-1.653-2.059-.173-.297-.018-.458.13-.606.134-.133.298-.347.446-.52.149-.174.198-.298.298-.497.099-.198.05-.371-.025-.52-.075-.149-.669-1.612-.916-2.207-.242-.579-.487-.5-.669-.51-.173-.008-.371-.01-.57-.01-.198 0-.52.074-.792.372-.272.297-1.04 1.016-1.04 2.479 0 1.462 1.065 2.875 1.213 3.074.149.198 2.096 3.2 5.077 4.487.709.306 1.262.489 1.694.625.712.227 1.36.195 1.871.118.571-.085 1.758-.719 2.006-1.413.248-.694.248-1.289.173-1.413-.074-.124-.272-.198-.57-.347m-5.421 7.403h-.004a9.87 9.87 0 0 1-5.031-1.378l-.361-.214-3.741.982.998-3.648-.235-.374a9.86 9.86 0 0 1-1.51-5.26c.001-5.45 4.436-9.884 9.888-9.884 2.64 0 5.122 1.03 6.988 2.898a9.825 9.825 0 0 1 2.893 6.994c-.003 5.45-4.435 9.884-9.885 9.884m8.413-18.297A11.815 11.815 0 0 0 12.05 0C5.495 0 .16 5.335.157 11.892c0 2.096.547 4.142 1.588 5.945L.057 24l6.305-1.654a11.882 11.882 0 0 0 5.683 1.448h.005c6.554 0 11.89-5.335 11.893-11.893a11.821 11.821 0 0 0-3.48-8.413z" />
            </svg>
        </a>

    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="contentScript" Runat="Server">
    </asp:Content>
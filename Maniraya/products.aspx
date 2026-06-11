<%@ Page Title="All Products" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main class="catalog-page-main">
                <section class="products-section catalog-page-section">
                    <div class="container">
                        <div class="catalog-page-head">
                            <a href="index.aspx" class="catalog-back-link">&larr; Back to home</a>
                            <div class="products-section-head">
                                <div class="products-head-text">
                                    <span class="section-eyebrow">Curated For You</span>
                                    <h1>All <span>Products</span></h1>
                                    <p>Explore our complete catalog of quality products at great prices.</p>
                                </div>
                                <a href="addtocart.aspx" class="products-view-cart">View Cart</a>
                            </div>
                        </div>
                        <div class="products-toolbar">
                            <asp:DropDownList ID="ddCategory" CssClass="product-category-filter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged">
                                <asp:ListItem Value="0">All Categories</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="product-grid product-grid-catalog">
                            <asp:Repeater ID="rptProducts" runat="server">
                                <ItemTemplate>
                                    <article class="product-card">
                                        <div class="product-card-media">
                                            <%# Convert.ToDecimal(Eval("MRP")) > Convert.ToDecimal(Eval("Amount")) ? "<span class=\"product-badge product-badge-sale\">-" + Math.Round((Convert.ToDecimal(Eval("MRP")) - Convert.ToDecimal(Eval("Amount"))) / Convert.ToDecimal(Eval("MRP")) * 100) + "%</span>" : "" %>
                                            <a class="product-image-link" href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                                <div class="product-image">
                                                    <img src='<%# Eval("Image") %>' alt='<%# Eval("ProductName") %>' loading="lazy" />
                                                </div>
                                                <span class="product-card-overlay" aria-hidden="true">
                                                    <span class="product-card-overlay-btn">Quick View</span>
                                                </span>
                                            </a>
                                        </div>
                                        <div class="product-card-body">
                                            <div class="product-card-head">
                                                <span class="product-category-tag"><%# Eval("categoryName") %></span>
                                            </div>
                                            <h3 class="product-title">
                                                <a href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'><%# Eval("ProductName") %></a>
                                            </h3>
                                            <div class="product-price-block">
                                                <div class="product-price-main">
                                                    <span class="price">&#8377; <%# Eval("Amount") %></span>
                                                    <span class="product-old-price">&#8377; <%# Eval("MRP") %></span>
                                                </div>
                                                <%# Convert.ToInt32(Eval("discount")) > 0 ? "<span class=\"product-discount\">Save " + Eval("discount") + "%</span>" : "" %>
                                            </div>
                                            <div class="product-card-actions">
                                                <a class="view-btn" href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                                    <svg width="15" height="15" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7"/><circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"/></svg>
                                                    <span>View</span>
                                                </a>
                                                <a class="add-btn" href='Productdetail.aspx?productid=<%# Eval("ProductID") %>&franchiseeid=<%# Eval("franchiseeid") %>'>
                                                    <svg width="15" height="15" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/><path d="M9 11v6M15 11v6" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                                                    <span>Add to Cart</span>
                                                </a>
                                            </div>
                                        </div>
                                    </article>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:Panel ID="pnlEmpty" runat="server" CssClass="catalog-empty-state" Visible="false">
                            <h3>No products found</h3>
                            <p>Try another category or check back soon for new arrivals.</p>
                            <a href="products.aspx" class="categories-view-all">View all products</a>
                        </asp:Panel>
                    </div>
                </section>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

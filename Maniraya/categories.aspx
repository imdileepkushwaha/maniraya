<%@ Page Title="All Categories" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="categories.aspx.cs" Inherits="categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <main class="catalog-page-main">
        <section class="top-categories catalog-page-section">
            <div class="container">
                <div class="catalog-page-head">
                    <a href="index.aspx" class="catalog-back-link">&larr; Back to home</a>
                    <div class="categories-section-head">
                        <div class="categories-head-text">
                            <span class="section-eyebrow">Shop by Category</span>
                            <h1>All <span>Categories</span></h1>
                            <p>Browse every collection and find products that match your style.</p>
                        </div>
                    </div>
                </div>
                <div class="category-grid category-grid-catalog">
                    <asp:Repeater ID="rptCategories" runat="server">
                        <ItemTemplate>
                            <a class="category-card" href='products.aspx?category=<%# Eval("CategoryId") %>'>
                                <div class="category-card-media">
                                    <img src='<%# Eval("Image") %>' alt='<%# Eval("CategoryName") %>' loading="lazy" />
                                    <span class="category-card-shade" aria-hidden="true"></span>
                                </div>
                                <div class="category-card-body">
                                    <div class="category-card-info">
                                        <h3><%# Eval("CategoryName") %></h3>
                                        <span class="category-card-meta">Shop collection</span>
                                    </div>
                                    <span class="category-card-arrow" aria-hidden="true">
                                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M5 12H19M19 12L13 6M19 12L13 18" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                    </span>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

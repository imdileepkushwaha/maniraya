<%@ Page Title="Image Gallery" Language="C#" MasterPageFile="WebMasterPage.master" AutoEventWireup="true" CodeFile="ImageGallery.aspx.cs" Inherits="ImageGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- Fancybox CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@fancyapps/ui@5.0/dist/fancybox/fancybox.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="container">
            <h1 class="page-title">Image Gallery</h1>
            <p>Explore our premium product collections.</p>
        </div>
    </div>

    <div class="gallery-grid">
        <asp:Repeater ID="rptGallery" runat="server">
            <ItemTemplate>
                <div class="gallery-item">
                    <a href='<%# ResolveUrl(Eval("Img_Url").ToString()) %>' data-fancybox="gallery" data-caption='<%# Eval("Title") %>'>
                        <img src='<%# ResolveUrl(Eval("Img_Url").ToString()) %>' alt='<%# Eval("Title") %>' loading="lazy" />
                        <div class="gallery-overlay">
                            <div class="gallery-caption"><%# Eval("Title") %></div>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Fancybox JS -->
    <script src="https://cdn.jsdelivr.net/npm/@fancyapps/ui@5.0/dist/fancybox/fancybox.umd.js"></script>
    <script>
        Fancybox.bind('[data-fancybox="gallery"]', {
            // Options will go here
            Toolbar: {
                display: {
                    left: ["infobar"],
                    middle: ["zoomIn", "zoomOut", "toggle1to1"],
                    right: ["slideshow", "thumbs", "close"],
                },
            },
        });
    </script>
</asp:Content>

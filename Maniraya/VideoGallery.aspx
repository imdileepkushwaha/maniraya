<%@ Page Title="Video Gallery" Language="C#" MasterPageFile="WebMasterPage.master" AutoEventWireup="true" CodeFile="VideoGallery.aspx.cs" Inherits="VideoGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="container">
            <h1 class="page-title">Video Gallery</h1>
            <p>Watch our products in action and learn more about our franchise opportunities.</p>
        </div>
    </div>

    <div class="video-grid">
        <asp:Repeater ID="rptVideoGallery" runat="server">
            <ItemTemplate>
                <div class="video-item">
                    <div class="video-wrapper">
                        <iframe src='<%# GetEmbedUrl(Eval("VideoUrl")) %>' title='<%# Eval("title") %>' allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                    </div>
                    <div class="video-caption"><%# Eval("title") %></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

<%@ Page Title="FAQ" Language="C#" MasterPageFile="WebMasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="container">
            <h1 class="page-title">Frequently Asked Questions</h1>
            <p>Find answers to common questions about our products and services.</p>
        </div>
    </div>

    <div class="container faq-container">
        <asp:Repeater ID="rptFAQ" runat="server">
            <ItemTemplate>
                <div class="faq-item">
                    <details>
                        <summary><%# Eval("Question") %></summary>
                        <p class="faq-answer"><%# Eval("Answer") %></p>
                    </details>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

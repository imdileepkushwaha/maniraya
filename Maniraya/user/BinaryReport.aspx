<%@ Page Title="Binary Report" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="BinaryReport.aspx.cs" Inherits="admin_BinaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/binary-tree.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Binary Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">My Team</a></li>
            <li class="active">Binary Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page binary-report-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-sitemap"></i></div>
                    <div class="profile-hero-info">
                        <h2>My Treeview</h2>
                        <p class="profile-hero-meta">Visualize your binary genealogy tree and explore team structure</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserDirectAssociates.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-users"></i> My Direct</a>
                        <a href="DownlineReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-level-down"></i> My Downline</a>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Genealogy Tree</h3>
                    </div>
                    <div class="box-body binary-report-body">
                        <div class="binary-report-toolbar">
                            <div class="form-group">
                                <label for="<%= txtuserid.ClientID %>">User ID</label>
                                <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" placeholder="Enter member ID"></asp:TextBox>
                            </div>
                            <div class="binary-report-actions">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <p class="binary-report-mobile-hint"><i class="fa fa-arrows-h" aria-hidden="true"></i> Swipe left or right inside the tree to view all members</p>
                        <div class="binary-report-tree-frame">
                            <iframe id="f1" runat="server" title="Binary genealogy tree"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

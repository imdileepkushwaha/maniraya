<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DownlineReport.aspx.cs" Inherits="admin_DownlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=5" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>Downline Report</h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home > </a></li>
            <li><a href="#">My Team > </a></li>
            <li class="active">My Downline</li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="profile-page team-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-level-down"></i></div>
                    <div class="profile-hero-info">
                        <h2>My Downline</h2>
                        <p class="profile-hero-meta">Explore your complete downline members on both left and right legs</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserDirectAssociates.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-users"></i> My Direct</a>
                        <a href="BinaryReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-sitemap"></i> My Treeview</a>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Downline Report</h3>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="downline-search-toolbar">
                            <div class="form-group">
                                <label for="<%= txtuserid.ClientID %>">User ID</label>
                                <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="downline-search-actions">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details</h3>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="row team-stat-grid">
                            <div class="col-md-6">
                                <div class="team-stat-card team-stat-left">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-left"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Left Team</p>
                                        <h3 class="team-stat-value"><asp:Label ID="LblTotalLeft" runat="server" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="team-stat-card team-stat-right">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-right"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Right Team</p>
                                        <h3 class="team-stat-value"><asp:Label ID="LblTotalright" runat="server" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="downline-team-split">
                            <div class="downline-team-panel is-left">
                                <div class="downline-team-panel-head">
                                    <h4><i class="fa fa-arrow-left" aria-hidden="true"></i> Left Team Members</h4>
                                </div>
                                <div class="team-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                        AutoGenerateColumns="False" OnRowDataBound="GridView_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parent ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsponserid" runat="server" Text='<%# Eval("ParentUserId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="downline-team-panel is-right">
                                <div class="downline-team-panel-head">
                                    <h4><i class="fa fa-arrow-right" aria-hidden="true"></i> Right Team Members</h4>
                                </div>
                                <div class="team-table-wrap table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                        AutoGenerateColumns="False" OnRowDataBound="GridView_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parent ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsponserid" runat="server" Text='<%# Eval("ParentUserId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsponsername" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>





         
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


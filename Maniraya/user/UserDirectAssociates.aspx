<%@ Page Title="" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="UserDirectAssociates.aspx.cs" Inherits="user_UserDirectAssociates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=5" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Direct User Details</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">My Team</a></li>
            <li class="active">My Direct</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="profile-page team-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-users"></i></div>
                    <div class="profile-hero-info">
                        <h2>My Direct</h2>
                        <p class="profile-hero-meta">View your personally sponsored members on left and right team</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="DownlineReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-level-down-alt"></i> My Downline</a>
                        <a href="BinaryReport.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-sitemap"></i> My Treeview</a>
                    </div>
                </div>
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Direct Associates</h3>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="row team-stat-grid">
                            <div class="col-md-6">
                                <div class="team-stat-card team-stat-left">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-left"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Left Team</p>
                                        <h3 class="team-stat-value"><asp:Label ID="LblLeftDirect" runat="server" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="team-stat-card team-stat-right">
                                    <span class="team-stat-icon" aria-hidden="true"><i class="fa fa-arrow-right"></i></span>
                                    <div class="team-stat-content">
                                        <p class="team-stat-label">Right Team</p>
                                        <h3 class="team-stat-value"><asp:Label ID="LblRightDirect" runat="server" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="team-toolbar">
                            <div class="team-toolbar-actions" style="display:flex;flex-wrap:wrap;gap:12px;align-items:flex-end;flex:1;">
                                <div class="form-group" style="margin:0;min-width:180px;flex:1 1 180px;">
                                    <label for="<%= txtSearch.ClientID %>">Search (User ID / Name / Mobile)</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Type to search..."></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin:0;min-width:140px;">
                                    <label for="<%= DDlstPosition.ClientID %>">Position</label>
                                    <asp:DropDownList ID="DDlstPosition" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Selected="True">Both</asp:ListItem>
                                        <asp:ListItem Value="1">Left</asp:ListItem>
                                        <asp:ListItem Value="2">Right</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin:0;">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                            <div class="form-group team-toolbar-filter">
                                <label for="<%= ddlRecordFilter.ClientID %>">Show records</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control team-records-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group team-table-group">
                            <span class="team-table-caption"><i class="fa fa-list-alt"></i> Direct Members List</span>
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="grdBank" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                    AutoGenerateColumns="false" EmptyDataText="No Data Found" OnRowDataBound="grdBank_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserId" HeaderText="User ID" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                        <asp:BoundField DataField="planname" HeaderText="Package" />
                                        <asp:BoundField DataField="StandingPosition" HeaderText="Standing Position" />
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                        <asp:BoundField DataField="mentiondate" HeaderText="D. O. J." />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

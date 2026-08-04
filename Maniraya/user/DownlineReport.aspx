<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DownlineReport.aspx.cs" Inherits="admin_DownlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=9" rel="stylesheet" />
    <style>
        .downline-result-summary {
            display: block;
            margin: 0 0 10px;
            font-size: 0.86rem;
            font-weight: 600;
            color: #64748b;
        }

        /* Single-panel layout + force horizontal scroll on mobile */
        .team-page .downline-team-split {
            grid-template-columns: 1fr;
        }
        .team-page .downline-team-panel {
            min-width: 0;
            max-width: 100%;
        }
        body.main-body.app .box.box-primary .downline-team-panel .team-table-wrap.table-responsive,
        .content-wrapper .box.box-primary .downline-team-panel .team-table-wrap.table-responsive,
        .team-page .downline-team-panel .team-table-wrap.table-responsive {
            width: 100%;
            max-width: 100%;
            max-height: min(520px, 60vh) !important;
            overflow-x: auto !important;
            overflow-y: auto !important;
            -webkit-overflow-scrolling: touch;
        }
        .team-page .downline-team-panel .team-table-wrap .team-table {
            width: 100%;
            min-width: 560px;
            margin-bottom: 0 !important;
        }
        .team-page .downline-team-panel .team-table-wrap .team-table > tbody > tr > td {
            white-space: nowrap;
        }

        .team-table .pagination-ys,
        .team-table td table {
            margin: 12px auto 4px;
        }
        .team-table td table td {
            border: none !important;
            padding: 2px !important;
            white-space: normal !important;
        }
        .team-table td table a,
        .team-table td table span {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 34px;
            height: 34px;
            padding: 0 10px;
            margin: 0 2px;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            color: #64748b;
            text-decoration: none;
            background: #fff;
            border: 1px solid #e2e8f0;
        }
        .team-table td table span {
            color: #fff;
            background: #0f172a;
            border-color: #0f172a;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>Downline Report</h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home > </a></li>
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
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-level-down-alt"></i></div>
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
                            <div class="form-group team-toolbar-filter">
                                <label for="<%= ddlRecordFilter.ClientID %>">Show records</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control team-records-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem Selected="True">25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
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
                                        <p class="team-stat-label">My Team</p>
                                        <h3 class="team-stat-value"><asp:Label ID="LblDownline" runat="server" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="downline-team-split">
                            <div class="downline-team-panel is-left">
                                <div class="downline-team-panel-head">
                                    <h4><i class="fa fa-users" aria-hidden="true"></i> Team Members</h4>
                                </div>
                                <asp:Label ID="lblResultSummary" runat="server" CssClass="downline-result-summary" Text=""></asp:Label>
                                <div class="team-table-wrap table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                        AutoGenerateColumns="False" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView_RowDataBound"
                                        EmptyDataText="No downline members found.">
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                                            PageButtonCount="5" Position="Bottom" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# GetSerialNumber(Container.DataItemIndex) %>
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
                                            <asp:TemplateField HeaderText="Sponsor ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsponserid" runat="server" Text='<%# Eval("sponserid") %>'></asp:Label>
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

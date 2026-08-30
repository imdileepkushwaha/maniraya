<%@ Page Title="Thailand Trip Bonanza" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="FastTrackReport.aspx.cs" Inherits="user_FastTrackReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=9" rel="stylesheet" />
    <link href="assets/css/dashboard-modern.css?v=25" rel="stylesheet" />
    <style>
        .ft-banner {
            display: block;
            width: 100%;
            max-height: 280px;
            object-fit: cover;
            border-radius: 16px;
            margin-bottom: 16px;
            border: 1px solid #fde68a;
        }
        .ft-status {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 8px 16px;
            border-radius: 999px;
            font-size: 14px;
            font-weight: 800;
            letter-spacing: 0.04em;
            text-transform: uppercase;
        }
        .ft-status.is-yes {
            background: #ecfdf5;
            color: #047857;
            border: 1px solid #a7f3d0;
        }
        .ft-status.is-no {
            background: #fef2f2;
            color: #b91c1c;
            border: 1px solid #fecaca;
        }
        .ft-cards {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 16px;
        }
        @media (max-width: 720px) {
            .ft-cards { grid-template-columns: 1fr; }
        }
        .ft-card {
            border-radius: 18px;
            padding: 18px 18px 16px;
            background: #fff;
            border: 1px solid #e2e8f0;
            box-shadow: 0 8px 20px rgba(15, 23, 42, 0.04);
        }
        .ft-card h4 {
            margin: 0 0 4px;
            font-size: 1.05rem;
            font-weight: 800;
            color: #0f172a;
        }
        .ft-card p {
            margin: 0 0 14px;
            font-size: 13px;
            color: #64748b;
        }
        .ft-metrics {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr));
            gap: 8px;
            margin-bottom: 14px;
        }
        .ft-metric {
            text-align: center;
            padding: 10px 6px;
            border-radius: 12px;
            background: #f8fafc;
            border: 1px solid #e2e8f0;
        }
        .ft-metric span {
            display: block;
            font-size: 10px;
            font-weight: 800;
            letter-spacing: 0.06em;
            text-transform: uppercase;
            color: #64748b;
        }
        .ft-metric strong {
            display: block;
            margin-top: 4px;
            font-size: 1.25rem;
            color: #0f172a;
        }

        .ft-metric strong span {
            display: block;
            margin-top: 4px;
            font-size: 1.25rem;
            color: #0f172a;
        }
        .ft-bar {
            height: 12px;
            border-radius: 999px;
            background: #e2e8f0;
            overflow: hidden;
        }
        .ft-bar-fill {
            height: 100%;
            border-radius: 999px;
            background: linear-gradient(90deg, #f59e0b 0%, #e5a906 50%, #fbbf24 100%);
            min-width: 0;
        }
        .ft-rules {
            margin: 0;
            font-size: 13px;
            color: #64748b;
            line-height: 1.6;
        }
        .ft-table-title {
            margin: 0 0 10px;
            font-size: 15px;
            font-weight: 800;
            color: #0f172a;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Thailand Trip Bonanza</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Reports</a></li>
            <li class="active">Thailand Trip Bonanza</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="profile-page dash-subpage dash-subpage--saving dash-saving-report-page">
        <div class="profile-hero dash-subpage-hero dash-subpage-hero--saving">
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-plane"></i></div>
            <div class="profile-hero-info">
                <h2>Thailand Trip Bonanza</h2>
                <p class="profile-hero-meta">Savings plan only. Window: 25 Aug 2026 – 30 Oct 2026. Need 10 self directs, and each of those 10 must have 10 directs of their own.</p>
            </div>
            <div class="profile-hero-actions">
                <asp:Label ID="lblStatus" runat="server" CssClass="ft-status is-no" Text="Not Achieved" />
            </div>
        </div>

        <!-- <img src="../admin/assets/images/fast-track-thailand.png" alt="Fast Track Thailand Trip" class="ft-banner" /> -->

        <div class="ft-cards">
            <div class="ft-card">
                <h4>My Directs</h4>
                <p>Target: 10 self directs (₹18,000 / Bulk18)</p>
                <div class="ft-metrics">
                    <div class="ft-metric">
                        <span>Done</span>
                        <strong><asp:Label ID="lblL1Done" runat="server" Text="0" /></strong>
                    </div>
                    <div class="ft-metric">
                        <span>Pending</span>
                        <strong><asp:Label ID="lblL1Pending" runat="server" Text="10" /></strong>
                    </div>
                    <div class="ft-metric">
                        <span>Target</span>
                        <strong>10</strong>
                    </div>
                </div>
                <div class="ft-bar">
                    <div id="divL1Fill" runat="server" class="ft-bar-fill" style="width:0%"></div>
                </div>
            </div>
            <div class="ft-card">
                <h4>Directs with 10 each</h4>
                <p>Target: 10 of your directs must each have 10 directs</p>
                <div class="ft-metrics">
                    <div class="ft-metric">
                        <span>Done</span>
                        <strong><asp:Label ID="lblL2Done" runat="server" Text="0" /></strong>
                    </div>
                    <div class="ft-metric">
                        <span>Pending</span>
                        <strong><asp:Label ID="lblL2Pending" runat="server" Text="100" /></strong>
                    </div>
                    <div class="ft-metric">
                        <span>Target</span>
                        <strong>100</strong>
                    </div>
                </div>
                <div class="ft-bar">
                    <div id="divL2Fill" runat="server" class="ft-bar-fill" style="width:0%"></div>
                </div>
            </div>
        </div>

        <div class="dash-subpage-panel dash-saving-report-panel" style="margin-top:16px;">
            <div class="dash-subpage-panel-head">
                <span class="dash-subpage-panel-icon tone-amber" aria-hidden="true"><i class="fa fa-info-circle"></i></span>
                <div>
                    <h3>Plan Rules</h3>
                    <p>Both targets must be completed in the offer window</p>
                </div>
            </div>
            <div class="dash-subpage-panel-body">
                <p class="ft-rules">
                    Counted direct = approved ₹18,000 / Bulk18 saving purchase between <strong>25 Aug 2026</strong> and <strong>30 Oct 2026</strong>.
                    You need <strong>10 self directs</strong>, and <strong>10 of those directs</strong> must each have <strong>10 directs</strong> of their own.
                    Status is <strong>Achieved</strong> only when both are complete. Dual occupancy, non-transferable.
                </p>
            </div>
        </div>

        <div class="dash-subpage-panel dash-saving-report-panel">
            <div class="dash-subpage-panel-head">
                <span class="dash-subpage-panel-icon tone-green" aria-hidden="true"><i class="fa fa-users"></i></span>
                <div>
                    <h3>My Directs</h3>
                    <p>Each direct needs 10 of their own directs to count as a complete leg</p>
                </div>
            </div>
            <div class="dash-subpage-panel-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvLevel1" runat="server" CssClass="table table-bordered table-hover"
                        Width="100%" AutoGenerateColumns="False" GridLines="None"
                        EmptyDataText="No qualifying directs yet.">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="buyerid" HeaderText="Direct User ID" />
                            <asp:BoundField DataField="buyername" HeaderText="Name" />
                            <asp:BoundField DataField="underdirects" HeaderText="Their Directs" />
                            <asp:BoundField DataField="underpending" HeaderText="Need for 10" />
                            <asp:TemplateField HeaderText="Counted (10/10)">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("iscounted")) ? "Yes" : "No" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="saledate" HeaderText="Sale Date" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

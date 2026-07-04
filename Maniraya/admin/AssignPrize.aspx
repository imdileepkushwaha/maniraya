<%@ Page Title="Assign Prize" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="AssignPrize.aspx.cs" Inherits="admin_AssignPrize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .prize-assign-hint {
            color: #8a95a1;
            font-size: 12px;
            margin-top: 4px;
        }

        /* ===== Find Member search panel ===== */
        .pa-search-panel {
            background: linear-gradient(135deg, #f0f9f3 0%, #eafaf0 100%);
            border: 1px solid #d3ebdc;
            border-radius: 14px;
            padding: 16px;
        }
        .pa-search-bar {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            align-items: stretch;
        }
        .pa-search-input {
            display: flex;
            align-items: center;
            flex: 1 1 260px;
            min-width: 200px;
            background: #fff;
            border: 1px solid #cfe3d6;
            border-radius: 10px;
            box-shadow: 0 1px 2px rgba(16, 60, 35, 0.04);
            overflow: hidden;
            transition: border-color 0.15s ease, box-shadow 0.15s ease;
        }
        .pa-search-input:focus-within {
            border-color: #2fa15c;
            box-shadow: 0 0 0 3px rgba(47, 161, 92, 0.15);
        }
        .pa-search-input > i {
            flex-shrink: 0;
            width: 42px;
            text-align: center;
            color: #1f7a45;
            font-size: 15px;
            pointer-events: none;
        }
        .pa-search-input .form-control {
            flex: 1 1 auto;
            min-width: 0;
            width: auto;
            height: 44px;
            margin: 0 !important;
            padding: 8px 14px 8px 4px !important;
            border: none !important;
            border-radius: 0 !important;
            background: transparent !important;
            box-shadow: none !important;
            font-size: 14px;
            line-height: 1.4;
            box-sizing: border-box;
        }
        .pa-search-input .form-control:focus {
            outline: none;
            border: none !important;
            box-shadow: none !important;
        }
        .pa-search-btn {
            height: 46px;
            padding: 0 24px;
            border: none;
            border-radius: 10px;
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            color: #fff !important;
            font-weight: 700;
            font-size: 14px;
            cursor: pointer;
            box-shadow: 0 8px 18px -8px rgba(31, 122, 69, 0.7);
            transition: transform 0.12s ease, box-shadow 0.12s ease;
        }
        .pa-search-btn:hover,
        .pa-search-btn:focus {
            transform: translateY(-1px);
            box-shadow: 0 10px 22px -8px rgba(31, 122, 69, 0.85);
            color: #fff !important;
        }
        .pa-search-note {
            margin: 10px 2px 0;
            font-size: 12.5px;
            color: #6b8577;
        }
        .pa-search-note i {
            color: #2fa15c;
        }

        /* ===== Member result card ===== */
        .pa-member-card {
            display: flex;
            align-items: center;
            gap: 16px;
            margin-top: 14px;
            padding: 15px 18px;
            border-radius: 14px;
            background: #fff;
            border: 1px solid #e0ece5;
            border-left: 5px solid #2fa15c;
            box-shadow: 0 12px 28px -20px rgba(16, 60, 35, 0.6);
            flex-wrap: wrap;
        }
        .pa-member-avatar {
            width: 52px;
            height: 52px;
            border-radius: 50%;
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            color: #fff;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 21px;
            flex-shrink: 0;
        }
        .pa-member-info {
            flex: 1;
            min-width: 180px;
        }
        .pa-member-name {
            font-size: 18px;
            font-weight: 700;
            color: #16341f;
            margin: 0 0 8px;
        }
        .pa-member-chips {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
        }
        .pa-chip {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            font-size: 12.5px;
            font-weight: 600;
            color: #40566b;
            background: #f2f6f9;
            border: 1px solid #e3ebf0;
            border-radius: 999px;
            padding: 4px 12px;
        }
        .pa-chip i {
            color: #1f7a45;
        }
        .pa-chip.is-coupon {
            color: #b45309;
            background: #fff7e6;
            border: 1px dashed #f0b429;
            letter-spacing: 0.4px;
        }
        .pa-chip.is-coupon i {
            color: #d97706;
        }
        .pa-member-badge {
            align-self: flex-start;
            display: inline-flex;
            align-items: center;
            gap: 5px;
            font-size: 11px;
            font-weight: 700;
            color: #1f7a45;
            background: rgba(47, 161, 92, 0.12);
            padding: 5px 11px;
            border-radius: 999px;
        }

        /* ===== Assigned Prizes filter bar ===== */
        .pa-filter-bar {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            align-items: center;
            padding: 14px 16px;
            margin-bottom: 16px;
            background: #f7fafc;
            border: 1px solid #e6edf2;
            border-radius: 12px;
        }
        .pa-filter-input {
            display: flex;
            align-items: center;
            flex: 1 1 280px;
            min-width: 200px;
            background: #fff;
            border: 1px solid #dbe3ea;
            border-radius: 10px;
            overflow: hidden;
            transition: border-color 0.15s ease, box-shadow 0.15s ease;
        }
        .pa-filter-input:focus-within {
            border-color: #2fa15c;
            box-shadow: 0 0 0 3px rgba(47, 161, 92, 0.12);
        }
        .pa-filter-input > i {
            flex-shrink: 0;
            width: 40px;
            text-align: center;
            color: #8a95a1;
            font-size: 14px;
            pointer-events: none;
        }
        .pa-filter-input .form-control {
            flex: 1 1 auto;
            min-width: 0;
            width: auto;
            height: 42px;
            margin: 0 !important;
            padding: 8px 14px 8px 4px !important;
            border: none !important;
            border-radius: 0 !important;
            background: transparent !important;
            box-shadow: none !important;
            font-size: 14px;
            line-height: 1.4;
            box-sizing: border-box;
        }
        .pa-filter-input .form-control:focus {
            outline: none;
            border: none !important;
            box-shadow: none !important;
        }
        .pa-filter-btn {
            height: 44px;
            padding: 0 24px;
            border: none;
            border-radius: 10px;
            background: linear-gradient(135deg, #1f7a45, #2fa15c);
            color: #fff !important;
            font-weight: 700;
            font-size: 14px;
            cursor: pointer;
            box-shadow: 0 8px 18px -10px rgba(31, 122, 69, 0.7);
            transition: transform 0.12s ease;
        }
        .pa-filter-btn:hover,
        .pa-filter-btn:focus {
            transform: translateY(-1px);
            color: #fff !important;
        }
        .pa-filter-reset {
            height: 44px;
            padding: 0 20px;
            background: #fff;
            border: 1px solid #dbe3ea;
            border-radius: 10px;
            color: #52616b !important;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
        }
        .pa-filter-reset:hover,
        .pa-filter-reset:focus {
            background: #eef2f5;
            color: #33404d !important;
        }

        /* ===== Excel export button ===== */
        .pa-export-btn {
            display: inline-flex;
            align-items: center;
            gap: 7px;
            height: 32px;
            padding: 0 14px;
            border: none;
            border-radius: 8px;
            background: linear-gradient(135deg, #1d6f3f, #2fa15c);
            color: #fff !important;
            font-weight: 700;
            font-size: 13px;
            cursor: pointer;
            text-decoration: none !important;
            box-shadow: 0 6px 14px -8px rgba(31, 122, 69, 0.7);
        }
        .pa-export-btn:hover,
        .pa-export-btn:focus {
            color: #fff !important;
            text-decoration: none !important;
            box-shadow: 0 8px 18px -8px rgba(31, 122, 69, 0.85);
        }
        .pa-export-btn i {
            font-size: 15px;
        }

        @media (max-width: 600px) {
            .pa-search-btn,
            .pa-filter-btn,
            .pa-filter-reset {
                flex: 1 1 100%;
                width: 100%;
            }
            .pa-member-badge {
                align-self: center;
            }
        }
    </style>
    <script type="text/javascript">
        function openAssignConfirm(count) {
            return confirm('Save ' + count + ' prize assignment(s)?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Assign Prize</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Prize</a></li>
            <li class="active">Assign Prize</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-trophy"></i> Assign Prize to Member</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Search a member by User ID, pick a prize and month, then add to the list. You can add multiple members before saving.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Find Member</h5>
                                <div class="pa-search-panel">
                                    <div class="pa-search-bar">
                                        <div class="pa-search-input">
                                            <i class="fa fa-user"></i>
                                            <asp:TextBox ID="txtUserId" CssClass="form-control" runat="server" placeholder="Enter member User ID" />
                                        </div>
                                        <asp:Button ID="btnSearch" CssClass="pa-search-btn" runat="server" Text="Search Member" OnClick="btnSearch_Click" />
                                    </div>
                                    <p class="pa-search-note"><i class="fa fa-info-circle"></i> Enter the member's User ID and click search to view their details and coupon code.</p>
                                    <asp:Panel ID="pnlUser" runat="server" Visible="false" CssClass="pa-member-card">
                                        <div class="pa-member-avatar"><i class="fa fa-user"></i></div>
                                        <div class="pa-member-info">
                                            <p class="pa-member-name"><asp:Label ID="lblUserName" runat="server" Text="" /></p>
                                            <div class="pa-member-chips">
                                                <span class="pa-chip"><i class="fa fa-id-badge"></i> ID: <asp:Label ID="lblUserIdShow" runat="server" Text="" /></span>
                                                <span class="pa-chip"><i class="fa fa-phone"></i> <asp:Label ID="lblMobile" runat="server" Text="" /></span>
                                                <span class="pa-chip is-coupon"><i class="fa fa-ticket"></i> <asp:Label ID="lblCoupon" runat="server" Text="" /></span>
                                            </div>
                                        </div>
                                        <span class="pa-member-badge"><i class="fa fa-check-circle"></i> Member Found</span>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-gift"></i> Prize Details</h5>
                                <div class="row">
                                    <div class="col-md-5 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= ddlPrize.ClientID %>">Select Prize</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-trophy"></i></span>
                                                <asp:DropDownList ID="ddlPrize" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtMonth.ClientID %>">Prize Month</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtMonth" CssClass="form-control" runat="server" TextMode="Month" />
                                            </div>
                                            <p class="prize-assign-hint">Select the month this prize is awarded for.</p>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-12">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <div>
                                                <asp:Button ID="btnAddToList" CssClass="btn btn-success btn-block" runat="server" Text="Add to List" OnClick="btnAddToList_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list-ul"></i> Members to Assign
                                <asp:Label ID="lblStagedCount" runat="server" CssClass="label label-info" Text="0" /></h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="gvStaged" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="gvStaged_RowCommand" DataKeyNames="RowKey">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserId" HeaderText="User ID" />
                                    <asp:BoundField DataField="UserName" HeaderText="Member Name" />
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                    <asp:BoundField DataField="PrizeName" HeaderText="Prize" />
                                    <asp:BoundField DataField="PrizeMonthDisplay" HeaderText="Month" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="80px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemove" runat="server" CommandName="removeRow" CommandArgument='<%# Eval("RowKey") %>' CssClass="admin-grid-edit-btn" ToolTip="Remove">
                                                <i class="icon fa fa-trash" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center" style="padding:18px;color:#888;">No members added yet. Search a member and click "Add to List".</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnClearList" CssClass="btn btn-default" runat="server" Text="Clear List" OnClick="btnClearList_Click" />
                            <asp:Button ID="btnSaveAll" CssClass="btn btn-primary" runat="server" Text="Save All Assignments" OnClick="btnSaveAll_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-history"></i> Assigned Prizes</h3>
                            <div class="box-tools pull-right">
                                <asp:LinkButton ID="btnExportAssignments" runat="server" CssClass="pa-export-btn" OnClick="btnExportAssignments_Click" ToolTip="Download Excel">
                                    <i class="fa fa-file-excel-o"></i> Excel
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="pa-filter-bar">
                                <div class="pa-filter-input">
                                    <i class="fa fa-search"></i>
                                    <asp:TextBox ID="txtFilterUser" CssClass="form-control" runat="server" placeholder="Search by User ID or Member Name" />
                                </div>
                                <asp:Button ID="btnFilter" CssClass="pa-filter-btn" runat="server" Text="Search" OnClick="btnFilter_Click" />
                                <asp:Button ID="btnFilterReset" CssClass="pa-filter-reset" runat="server" Text="Reset" OnClick="btnFilterReset_Click" />
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvAssignments" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="gvAssignments_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserId" HeaderText="User ID" />
                                        <asp:BoundField DataField="UserName" HeaderText="Member Name" />
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                        <asp:BoundField DataField="PrizeName" HeaderText="Prize" />
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate><%# GetMonthDisplay(Eval("PrizeMonth")) %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedOn" HeaderText="Assigned On" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle CssClass="admin-grid-action-cell" HorizontalAlign="Center" Width="80px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="deleteRow" CommandArgument='<%# Eval("Id") %>' CssClass="admin-grid-edit-btn" ToolTip="Delete" OnClientClick="return confirm('Delete this assignment?');">
                                                    <i class="icon fa fa-trash" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="text-center" style="padding:18px;color:#888;">No prize assignments found.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportAssignments" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

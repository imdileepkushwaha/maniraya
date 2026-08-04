<%@ Page Title="Franchisee Detail" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="FranchiseeSearchNew.aspx.cs" Inherits="FranchiseeSearchNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/box-modern.css?v=4" rel="stylesheet" />
    <link href="assets/css/repurchase-modern.css?v=6" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Franchisee Detail</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">My Repurchase</a></li>
            <li class="active">Franchisee Detail</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HDIsdistributer" runat="server" />
            <div class="profile-page repurchase-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-store"></i></div>
                    <div class="profile-hero-info">
                        <h2>Find Franchisee</h2>
                        <p class="profile-hero-meta">Search nearby franchisees and continue to repurchase products</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a id="lnksearch" class="profile-btn rp-search-trigger" href="javascript:showSearchModal();">
                            <i class="fa fa-search"></i> Search Franchisee
                        </a>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header with-border box-header-enhanced box-header-tone-0">
                        <div class="box-header-main">
                            <span class="box-header-icon" aria-hidden="true"><i class="fa fa-list"></i></span>
                            <div class="box-header-text">
                                <h3 class="box-title">Franchisee List</h3>
                                <p class="box-subtitle">Select a franchisee to purchase items</p>
                            </div>
                        </div>
                        <div class="box-tools">
                            <a class="rp-search-chip" href="javascript:showSearchModal();">
                                <i class="fa fa-sliders"></i> Filters
                            </a>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="repurchase-toolbar">
                            <p><i class="fa fa-info-circle"></i> Use Search to filter by state, city, tehsil, market or pincode. Click the cart icon to buy.</p>
                            <a class="rp-toolbar-search" href="javascript:showSearchModal();">
                                <i class="fa fa-search"></i> Open Search
                            </a>
                        </div>
                        <div class="repurchase-table-wrap table-responsive">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                EmptyDataText="No franchisee found. Try changing search filters.">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcity" runat="server" Text='<%# Eval("CityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateNamee" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pincode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPincode" runat="server" Text='<%# Eval("Pincode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit1" CommandName="View"
                                                CommandArgument='<%# Eval("StateId").ToString() + "_" + Eval("CityId").ToString() + "_" + Eval("id").ToString() %>'
                                                runat="server" CssClass="rp-action-btn" ToolTip="View details">
                                                <i class="fa fa-eye" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="rp-action-btn is-cart" ToolTip="Purchase"
                                                NavigateUrl='<%# string.Format("PurchaseItemRepurchase.aspx?FID={0}", HttpUtility.UrlEncode(Eval("userid").ToString() + "_" + "1" + "_" + "1")) %>'>
                                                <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                                            </asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="rp-action-btn is-cart" ToolTip="Purchase DP"
                                                NavigateUrl='<%# string.Format("PurchaseItemRepurchaseDP.aspx?FID={0}", HttpUtility.UrlEncode(Eval("userid").ToString() + "_" + "1" + "_" + "1")) %>'>
                                                <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="rp-pager">
                            <div class="dataTables_info" role="status" aria-live="polite">
                                <asp:Label ID="LblRecordCount" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="dataTables_paginate paging_simple_numbers">
                                <ul class="pagination">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <li class="paginate_button">
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="DivSearch" class="modal fade rp-modal rp-search-modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title"><i class="fa fa-search" aria-hidden="true"></i> Search Franchisee</h4>
                                <p class="rp-modal-sub">Filter by location to find the nearest franchisee</p>
                            </div>
                            <div class="modal-body">
                                <div class="rp-search-form">
                                    <div class="rp-search-grid">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                            <label for="<%= ddstate.ClientID %>"><i class="fa fa-map"></i> State</label>
                                            <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select State</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= ddcity.ClientID %>"><i class="fa fa-building"></i> City</label>
                                            <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select City</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= ddlsttehsil.ClientID %>"><i class="fa fa-map-marker"></i> Tehsil</label>
                                            <asp:DropDownList ID="ddlsttehsil" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="DDlstTehsil_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Tehsil</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= ddlstmarket.ClientID %>"><i class="fa fa-shopping-bag"></i> Market</label>
                                            <asp:DropDownList ID="ddlstmarket" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select Market</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group rp-search-pincode">
                                            <label for="<%= txtpincode.ClientID %>"><i class="fa fa-hashtag"></i> Pincode</label>
                                            <div class="rp-input-wrap">
                                                <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server" placeholder="e.g. 560065" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer rp-search-footer">
                                <button type="button" class="btn btn-default rp-modal-close" data-dismiss="modal">Close</button>
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary rp-search-submit" Text="Search Now" OnClick="BtnSearchFranchisee_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="Div_FDetails" class="modal fade rp-modal rp-franchisee-modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title"><i class="fa fa-store" aria-hidden="true"></i> Franchisee Details</h4>
                                <p class="rp-modal-sub">Contact and location information</p>
                            </div>
                            <div class="modal-body">
                                <div class="rp-detail-grid">
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-user"></i> Name</span>
                                        <span class="rp-field-value"><asp:Label ID="lblfname" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-phone"></i> Mobile No</span>
                                        <span class="rp-field-value"><asp:Label ID="lblmob" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-home"></i> Address</span>
                                        <span class="rp-field-value"><asp:Label ID="lbladdress" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-map"></i> State</span>
                                        <span class="rp-field-value"><asp:Label ID="lblstate" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-building"></i> City</span>
                                        <span class="rp-field-value"><asp:Label ID="lblcity" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="rp-detail-row">
                                        <span class="rp-field-label"><i class="fa fa-hashtag"></i> Pincode</span>
                                        <span class="rp-field-value"><asp:Label ID="lblpincode" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default rp-modal-close" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function showSearchModal() {
            $('#DivSearch').modal({ backdrop: 'static', keyboard: false });
        }
        function Closesearchpopup() {
            $('#DivSearch').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function showFranchiseeModal() {
            $('#Div_FDetails').modal({ backdrop: 'static', keyboard: false });
        }
        function ClosesFranchiseepopup() {
            $('#Div_FDetails').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>
</asp:Content>

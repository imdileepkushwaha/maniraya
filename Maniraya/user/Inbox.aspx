<%@ Page Title="Inbox" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Associate_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=10" rel="stylesheet" />
    <link href="assets/css/team-associates.css?v=3" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Inbox</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Customer Care</a></li>
            <li class="active">Inbox</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="profile-page team-page">
        <div class="profile-hero">
            <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-inbox"></i></div>
            <div class="profile-hero-info">
                <h2>Inbox</h2>
                <p class="profile-hero-meta">View messages and support replies</p>
            </div>
            <div class="profile-hero-actions">
                <a href="NewMessage.aspx" class="profile-btn profile-btn-outline"><i class="fa fa-pencil-alt"></i> Compose Mail</a>
            </div>
        </div>

        <asp:Panel ID="pnlempty" runat="server" Visible="false" CssClass="profile-sections">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="inbox-empty-state">
                        <i class="fa fa-inbox" aria-hidden="true"></i>
                        <h4>No messages yet</h4>
                        <p>Your inbox is empty. Compose a new mail to contact support.</p>
                        <a href="NewMessage.aspx" class="btn btn-primary">Compose Mail</a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnllist" runat="server" Visible="false" CssClass="profile-sections">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Messages</h3>
                </div>
                <div class="box-body team-box-body">
                    <div class="form-group team-table-group">
                        <div class="team-table-wrap table-responsive">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Message Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmessagetitle" runat="server" Text='<%# Eval("MessageTitle") %>'></asp:Label>
                                            <asp:Label ID="lblmessagedescription" Visible="false" runat="server" Text='<%# Eval("MessageDescription") %>'></asp:Label>
                                            <asp:Label ID="lblfromid" Visible="false" runat="server" Text='<%# Eval("FromId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("mentiondate", "{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attachment">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" ToolTip="Download Attachment" NavigateUrl='<%# Eval("Attachment", "~/ProductImage/{0}") %>' />
                                            <asp:Label Visible="false" ID="lblHyperLink" runat="server" Text='<%# Eval("Attachment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="inbox-action-cell">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnview" runat="server" CssClass="inbox-view-btn" OnClick="btnview_click" CausesValidation="false">
                                                <i class="fa fa-eye" aria-hidden="true"></i> View
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="inbox-pagination">
                        <asp:LinkButton ID="lbtnFirst" runat="server" CausesValidation="false" CssClass="inbox-page-btn" OnClick="lbtnFirst_Click">First</asp:LinkButton>
                        <asp:LinkButton ID="lbtnPrevious" runat="server" CausesValidation="false" CssClass="inbox-page-btn" OnClick="lbtnPrevious_Click">Prev</asp:LinkButton>
                        <asp:ListView ID="ListPaging" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="ListView2_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Paging" Text='<%# Eval("PageText") %>' CssClass="inbox-page-btn"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="inbox-page-btn" OnClick="lbtnNext_Click">Next</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CausesValidation="false" CssClass="inbox-page-btn" OnClick="lbtnLast_Click">Last</asp:LinkButton>
                    </div>
                    <asp:Label ID="lblPageInfo" runat="server" CssClass="inbox-page-info"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:Button ID="btnYes" runat="server" Text="Yes!" Style="display: none;" />
    <asp:Panel ID="pnlModal" runat="server" CssClass="inbox-message-modal" Style="display: none;">
        <div class="inbox-message-head">
            <div class="inbox-message-head-main">
                <span class="inbox-message-head-icon" aria-hidden="true"><i class="fa fa-envelopepen"></i></span>
                <div>
                    <h3>Message Detail</h3>
                    <p>Full message information</p>
                </div>
            </div>
            <button type="button" class="inbox-message-close" id="inboxModalCloseX" aria-label="Close">&times;</button>
        </div>
        <div class="inbox-message-body">
            <div class="inbox-message-grid">
                <div class="inbox-message-field">
                    <span class="inbox-message-label">From</span>
                    <asp:Label ID="lblmodfrom" runat="server" Text="-" CssClass="inbox-message-value"></asp:Label>
                </div>
                <div class="inbox-message-field">
                    <span class="inbox-message-label">Date</span>
                    <asp:Label ID="lblmoddate" runat="server" Text="-" CssClass="inbox-message-value"></asp:Label>
                </div>
            </div>
            <div class="inbox-message-field">
                <span class="inbox-message-label">Subject</span>
                <asp:Label ID="lblmodtitle" runat="server" Text="-" CssClass="inbox-message-value inbox-message-subject"></asp:Label>
            </div>
            <div class="inbox-message-field">
                <span class="inbox-message-label">Message</span>
                <asp:Label ID="lblmoddescription" runat="server" Text="-" CssClass="inbox-message-value inbox-message-body-text"></asp:Label>
            </div>
            <asp:Panel ID="pnlModAttachment" runat="server" Visible="false" CssClass="inbox-message-field">
                <span class="inbox-message-label">Attachment</span>
                <asp:HyperLink ID="hlModAttachment" runat="server" CssClass="inbox-message-attachment" Target="_blank"></asp:HyperLink>
            </asp:Panel>
        </div>
        <div class="inbox-message-footer">
            <asp:Button ID="btnClose" CssClass="btn btn-default inbox-modal-close-btn" runat="server" Text="Close" CausesValidation="false" />
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender TargetControlID="btnYes" ID="pnlModal_ModalPopupExtender"
        runat="server" Enabled="True" BackgroundCssClass="inbox-modal-background"
        PopupControlID="pnlModal" CancelControlID="btnClose" DropShadow="false">
    </cc1:ModalPopupExtender>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function initInboxModalClose() {
            var closeX = document.getElementById('inboxModalCloseX');
            var extenderId = '<%= pnlModal_ModalPopupExtender.ClientID %>';

            if (closeX && !closeX.getAttribute('data-bound')) {
                closeX.setAttribute('data-bound', '1');
                closeX.addEventListener('click', function () {
                    var ext = $find(extenderId);
                    if (ext) {
                        ext.hide();
                    }
                });
            }
        }

        if (window.Sys && Sys.Application) {
            Sys.Application.add_load(initInboxModalClose);
        } else if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', initInboxModalClose);
        } else {
            initInboxModalClose();
        }
    </script>
</asp:Content>

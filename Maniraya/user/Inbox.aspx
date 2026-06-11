<%@ Page Title="Inbox" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Associate_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.10.2.js"></script>
    <style>
        .info-box-content {
            padding: 5px 10px;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageheading" runat="Server">
    <section class="content-header">
        <h1>Inbox
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Customer Care</a></li>
            <li class="active">Inbox</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageData" runat="Server">
    <asp:Panel ID="pnllist" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Inbox</h3>
                    </div>
                    <div class="info-box-content row">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row" style="margin-right: 4px;">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Message Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmessagetitle" runat="server" Text='<%# Eval("MessageTitle") %>'></asp:Label>
                                                        <asp:Label ID="lblmessagedescription" Visible="false" runat="server" Text='<%# Eval("MessageDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("mentiondate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attachment">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="" ToolTip="Download Attachment" NavigateUrl='<%# Eval("Attachment", "~/ProductImage/{0}") %>'/>
                                                        <asp:Label Visible="false" ID="lblHyperLink" runat="server" Text='<%# Eval("Attachment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnview" CommandName="ledger" OnClick="btnview_click" runat="server">View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row" style="text-align: center;">
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnFirst" runat="server" CausesValidation="false"
                                                    OnClick="lbtnFirst_Click">First</asp:LinkButton>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnPrevious" runat="server" CausesValidation="false"
                                                    OnClick="lbtnPrevious_Click">Previous</asp:LinkButton>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:ListView ID="ListPaging" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="ListView2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>&nbsp;
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" OnClick="lbtnNext_Click">Next</asp:LinkButton>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnLast" runat="server" CausesValidation="false" OnClick="lbtnLast_Click">Last</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row" style="text-align: center;">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblPageInfo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnYes" runat="server" Text="Yes!" Style="display: none;" />
    <asp:Panel ID="pnlModal" Visible="false" runat="server" CssClass="box box-primary" Style="width: 500px; background-color: white;">
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="">
                    <div class="box-header" style="border-bottom: 2px solid gainsboro;">
                        <h3 class="box-title">Message Detail</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2" style="font-weight: bold;">From:</div>
                            <div class="col-md-10">
                                <asp:Label ID="lblfromid" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2" style="font-weight: bold;">Title:</div>
                            <div class="col-md-10">
                                <asp:Label ID="lbltitle" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2" style="font-weight: bold;">Detail:</div>
                            <div class="col-md-10">
                                <asp:Label ID="lbldescription" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2" style="font-weight: bold;">Date:</div>
                            <div class="col-md-10">
                                <asp:Label ID="lbldate" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btnClose" CssClass="btn btn-primary" runat="server" Text="Close" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender TargetControlID="btnYes" ID="pnlModal_ModalPopupExtender"
        runat="server" Enabled="True" BackgroundCssClass="modalBackground"
        PopupControlID="pnlModal" CancelControlID="btnClose" DropShadow="true">
    </asp:ModalPopupExtender>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>

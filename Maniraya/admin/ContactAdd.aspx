<%@ Page Title="Contact Settings" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ContactAdd.aspx.cs" Inherits="ContactAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Contact Settings</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Website Management</a></li>
            <li class="active">Contact Settings</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-5 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add / Update Contact</h3>
                        </div>
                        <div class="box-body">
                            <p class="admin-section-hint">Manage phone, email, address, and website shown on the public website, footer, contact page, and invoices.</p>
                            <div class="form-group">
                                <label for="<%= ddlContactType.ClientID %>">Contact Type</label>
                                <asp:DropDownList ID="ddlContactType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Phone">Phone</asp:ListItem>
                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                    <asp:ListItem Value="Address">Address</asp:ListItem>
                                    <asp:ListItem Value="Website">Website</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtTitle.ClientID %>">Title / Label</label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="e.g. Customer Support, Head Office"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtContactValue.ClientID %>">Value</label>
                                <asp:TextBox ID="txtContactValue" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Phone number, email, address, or website"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtDisplayOrder.ClientID %>">Display Order</label>
                                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control" Text="1" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= chkPrimary.ClientID %>">Primary (shown on header/footer)</label>
                                <asp:CheckBox ID="chkPrimary" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="<%= chkStatus.ClientID %>">Status (Active)</label>
                                <asp:CheckBox ID="chkStatus" runat="server" Checked="true" />
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-7 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Contact List</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ContactType" HeaderText="Type" />
                                    <asp:BoundField DataField="Title" HeaderText="Title" />
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <span style="word-break: break-word;"><%# Eval("ContactValue") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DisplayOrder" HeaderText="Order" />
                                    <asp:TemplateField HeaderText="Primary">
                                        <ItemTemplate>
                                            <%# Convert.ToBoolean(Eval("IsPrimary")) ? "Yes" : "No" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDelete_Click" CssClass="admin-grid-delete-btn" OnClientClick="return confirm('Are you sure to delete this contact?');" ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

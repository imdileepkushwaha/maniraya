<%@ Page Title="Franchisee Type Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FranchiseetypeMaster.aspx.cs" Inherits="FranchiseetypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Franchisee Type Master</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Franchisee Type</li>
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
                            <h3 class="box-title">Franchisee Type Settings</h3>
                        </div>

                        <div class="box-body admin-franchisee-type-form">
                            <p class="admin-franchisee-type-intro">Manage franchisee type names and profit values. Update the fields below and click save.</p>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-sitemap"></i> Franchisee Types</h5>
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable admin-inline-edit-grid" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" CssClass="admin-grid-index-cell" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Franchisee Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtAdminCharge" runat="server" Text='<%#Eval("type") %>' CssClass="form-control admin-grid-input" placeholder="Enter franchisee type" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtTdswithpam" runat="server" Text='<%#Eval("profit") %>' CssClass="form-control admin-grid-input" placeholder="Enter profit" TextMode="Number" />
                                                </ItemTemplate>
                                                <ItemStyle Width="220px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text="Update Franchisee Types" OnClick="btnUpdate_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

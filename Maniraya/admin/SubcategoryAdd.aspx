<%@ Page Title="Add Subcategory" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SubcategoryAdd.aspx.cs" Inherits="SubcategoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {
                alert('Select Category');
                document.getElementById("<%=ddcountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtstatename.ClientID%>").value == "") {
                alert('Enter Subcategory Name');
                document.getElementById("<%=txtstatename.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validate2() {
            if (document.getElementById("<%=ddcountryedit.ClientID%>").value == "0") {
                alert('Select Category');
                document.getElementById("<%=ddcountryedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtstatenameedit.ClientID%>").value == "") {
                alert('Enter Subcategory Name');
                document.getElementById("<%=txtstatenameedit.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Subcategory</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product management</a></li>
            <li class="active">Add Subcategory</li>
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
                            <h3 class="box-title">Add Subcategory</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <div class="admin-form-section admin-form-section-last">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddcountry.ClientID %>">Category</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-folder-open"></i></span>
                                                <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtstatename.ClientID %>">Subcategory Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                                <asp:TextBox ID="txtstatename" CssClass="form-control" runat="server" placeholder="Enter subcategory name" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Add Subcategory" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Subcategory List</h3>
                        </div>

                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("subcategoryid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%#Eval("categoryname") %>'></asp:Label>
                                                <asp:Label ID="lblcategoryid" runat="server" Visible="false" Text='<%#Eval("categoryid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subcategory Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatename" runat="server" Text='<%#Eval("subcategoryname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="subcategoryEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="subcategoryEditModalTitle">Edit Subcategory</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <label for="<%= ddcountryedit.ClientID %>">Category</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-folder-open"></i></span>
                                    <asp:DropDownList ID="ddcountryedit" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtstatenameedit.ClientID %>">Subcategory Name</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtstatenameedit" placeholder="Enter subcategory name"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Subcategory" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CountryAdd.aspx.cs" Inherits="admin_CountryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtcountryname.ClientID%>").value == "") {
                alert('Enter Country Name');
                document.getElementById("<%=txtcountryname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtcountrycode.ClientID%>").value == "") {
                alert('Enter Country Code');
                document.getElementById("<%=txtcountrycode.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validate2() {
            if (document.getElementById("<%=txtcountrynameedit.ClientID%>").value == "") {
                alert('Enter Country Name');
                document.getElementById("<%=txtcountrynameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtcountrycodeedit.ClientID%>").value == "") {
                alert('Enter Country Code');
                document.getElementById("<%=txtcountrycodeedit.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add Country</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add Country</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row admin-utility-page">
                <div class="col-md-5 col-lg-4">
                    <div class="box box-primary admin-utility-add-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Country</h3>
                        </div>
                        <div class="box-body admin-utility-form">
                            <p class="admin-section-hint">Add a new country with its display name and short code for use across state and city masters.</p>
                            <div class="form-group">
                                <label for="<%= txtcountryname.ClientID %>">Country Name</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-globe"></i></span>
                                    <asp:TextBox ID="txtcountryname" CssClass="form-control" runat="server" placeholder="e.g. India"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtcountrycode.ClientID %>">Country Code</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-flag"></i></span>
                                    <asp:TextBox ID="txtcountrycode" CssClass="form-control" runat="server" placeholder="e.g. IN"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Save Country" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-7 col-lg-8">
                    <div class="box box-primary admin-utility-list-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Country List</h3>
                        </div>
                        <div class="box-body">
                            <div class="admin-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Countryid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountrycode" runat="server" Text='<%#Eval("CountryCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ToolTip="Edit country"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade admin-utility-edit-modal" tabindex="-1" role="dialog" aria-labelledby="countryEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="countryEditModalTitle"><i class="fa fa-pencil-square-o"></i> Edit Country</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblcountryid" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                                <div class="form-group">
                                    <label for="<%= txtcountrynameedit.ClientID %>">Country Name</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrynameedit" placeholder="Country name"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtcountrycodeedit.ClientID %>">Country Code</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrycodeedit" placeholder="Country code"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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

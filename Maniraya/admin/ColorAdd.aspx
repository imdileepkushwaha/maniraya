<%@ Page Title="Add Color" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ColorAdd.aspx.cs" Inherits="ColorAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtcountryname.ClientID%>").value == "") {
                alert('Enter Color Name');
                document.getElementById("<%=txtcountryname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtcolorcode.ClientID%>").value == "") {
                alert('Enter Color Code');
                document.getElementById("<%=txtcolorcode.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validate2() {
            if (document.getElementById("<%=txtcountrynameedit.ClientID%>").value == "") {
                alert('Enter Color Name');
                document.getElementById("<%=txtcountrynameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=Txtcolorcodeedit.ClientID%>").value == "") {
                alert('Enter Color Code');
                document.getElementById("<%=Txtcolorcodeedit.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Color</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product management</a></li>
            <li class="active">Add Color</li>
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
                            <h3 class="box-title">Add Color</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <div class="admin-form-section admin-form-section-last">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtcountryname.ClientID %>">Color Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-paint-brush"></i></span>
                                                <asp:TextBox ID="txtcountryname" CssClass="form-control" runat="server" placeholder="Enter color name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtcolorcode.ClientID %>">Color Code</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                <asp:TextBox ID="txtcolorcode" CssClass="form-control" runat="server" placeholder="Enter color code" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Add Color" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Color List</h3>
                        </div>

                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%#Eval("Colorname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColorcode" runat="server" Text='<%#Eval("Colorcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsizename" runat="server" Text='<%# Eval("Status").ToString() == "1" ? "Unblock" : "Block" %>'></asp:Label>
                                                <asp:Label ID="lblsize" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="colorEditModalTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="colorEditModalTitle">Edit Color</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="lblcountryid" Visible="false" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <label for="<%= txtcountrynameedit.ClientID %>">Color Name</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-paint-brush"></i></span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrynameedit" placeholder="Enter color name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="<%= Txtcolorcodeedit.ClientID %>">Color Code</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="Txtcolorcodeedit" placeholder="Enter color code"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="<%= Ddlststatus.ClientID %>">Status</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-toggle-on"></i></span>
                                    <asp:DropDownList ID="Ddlststatus" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Block</asp:ListItem>
                                        <asp:ListItem Value="1">Unblock</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Color" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

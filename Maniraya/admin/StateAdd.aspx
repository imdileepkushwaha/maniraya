<%@ Page Title="Add State" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="StateAdd.aspx.cs" Inherits="admin_StateAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {
                alert('Select Country');
                document.getElementById("<%=ddcountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtstatename.ClientID%>").value == "") {
                alert('Enter State Name');
                document.getElementById("<%=txtstatename.ClientID%>").focus();
                return false;
            }
        }

        function validate2() {
            if (document.getElementById("<%=ddcountryedit.ClientID%>").value == "0") {
                alert('Select Country');
                document.getElementById("<%=ddcountryedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtstatenameedit.ClientID%>").value == "") {
                alert('Enter State Name');
                document.getElementById("<%=txtstatenameedit.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function openAddStateModal() {
            document.getElementById("<%=ddcountry.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=txtstatename.ClientID%>").value = "";
            showAdminModal('addStateModal');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add State</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add State</li>
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
                            <h3 class="box-title">Details</h3>
                            <div class="box-tools">
                                <button type="button" class="btn btn-primary btn-sm admin-box-header-btn" onclick="openAddStateModal();">
                                    <i class="fa fa-plus"></i> Add State
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("stateid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatename" runat="server" Text='<%#Eval("statename") %>'></asp:Label>
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

                <div id="addStateModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="stateAddModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="stateAddModalTitle">Add State</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="<%= ddcountry.ClientID %>">Select Country</label>
                                    <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtstatename.ClientID %>">State Name</label>
                                    <asp:TextBox ID="txtstatename" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="stateEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="stateEditModalTitle">Edit State</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>
                                <div class="form-group">
                                    <label for="<%= ddcountryedit.ClientID %>">Select Country</label>
                                    <asp:DropDownList ID="ddcountryedit" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtstatenameedit.ClientID %>">State Name</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtstatenameedit"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

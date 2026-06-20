<%@ Page Title="Add Bank" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="BankAdd.aspx.cs" Inherits="admin_BankAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtbankname.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtbankname.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validate2() {
            if (document.getElementById("<%=txtbanknameedit.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtbanknameedit.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Bank</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add Bank</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row admin-utility-page">
                <div class="col-md-5 col-lg-4">
                    <div class="box box-primary admin-utility-add-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Bank</h3>
                        </div>
                        <div class="box-body admin-utility-form">
                            <p class="admin-section-hint">Add bank names used in account setup, withdrawals, and payment configuration.</p>
                            <div class="form-group">
                                <label for="<%= txtbankname.ClientID %>">Bank Name</label>
                                <div class="admin-input-group">
                                    <span class="admin-input-icon"><i class="fa fa-university"></i></span>
                                    <asp:TextBox ID="txtbankname" CssClass="form-control" runat="server" placeholder="e.g. State Bank of India"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Save Bank" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-7 col-lg-8">
                    <div class="box box-primary admin-utility-list-card">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list"></i> Bank List</h3>
                        </div>
                        <div class="box-body">
                            <div class="admin-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Bankid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbankname" runat="server" Text='<%#Eval("BankName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ToolTip="Edit bank"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade admin-utility-edit-modal" tabindex="-1" role="dialog" aria-labelledby="bankEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="bankEditModalTitle"><i class="fa fa-pencil-square-o"></i> Edit Bank</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblbankid" Visible="false" runat="server" Text=""></asp:Label>
                                <div class="form-group">
                                    <label for="<%= txtbanknameedit.ClientID %>">Bank Name</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtbanknameedit" placeholder="Bank name"></asp:TextBox>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

<%@ Page Title="Add FAQ" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FAQAdd.aspx.cs" Inherits="FAQAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>FAQ Management</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Content</a></li>
            <li class="active">FAQ</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add/Update FAQ</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label for="<%= txtQuestion.ClientID %>">Question</label>
                                <asp:TextBox ID="txtQuestion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter question"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= txtAnswer.ClientID %>">Answer</label>
                                <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Enter answer"></asp:TextBox>
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

                <div class="col-md-6 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">FAQ List</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Question" HeaderText="Question" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDelete_Click" CssClass="admin-grid-delete-btn" OnClientClick="return confirm('Are you sure to delete this FAQ?');" ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
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

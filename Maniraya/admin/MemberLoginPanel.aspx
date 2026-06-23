<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="MemberLoginPanel.aspx.cs" Inherits="admin_MemberLoginPanel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=71" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">

    <section class="content-header">
      <h1>
       Direct Member Panel     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active"> Direct Member Panel     </li>
      </ol>
        <div class="container" style="display:none">
        <div class="row">
          <%--  <div class="col-md-6">
                <div class="form-control">
                    <label>User ID</label>
                   <asp:TextBox ID="txtsearch" CssClass="form-control form_date" runat="server" placeholder="Search By User ID"></asp:TextBox>
                </div>
            </div>--%>
            <%-- <div class="col-md-6">
                 <div class="form-control">
                      <label>User Name</label>
                       <asp:TextBox ID="TextBox1" CssClass="form-control form_date" runat="server" placeholder="Search By User ID"></asp:TextBox>
                 </div>
            </div>--%>
        </div>
        <div class="row">
<%--             <div class="col-md-6">
                <div class="form-control">
                     <asp:Button ID="btnfet" runat="server" OnClick="btnfet_Click" CssClass="btn btn-primary" Text="Search" />
                    </div>
                 </div>--%>
        </div>
            </div>


        
    </section>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">


      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page admin-member-login-page">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
                        </div>
                        <div class="box-body admin-search-form">
                            <div class="row">
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtsearch.ClientID %>">User ID</label>
                                        <asp:TextBox ID="txtsearch" CssClass="form-control" runat="server" placeholder="Search by user id"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtusername.ClientID %>">User Name</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtusername" placeholder="Search by user name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtmobile.ClientID %>">Mobile</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtmobile" placeholder="Search by mobile"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddlststatus.ClientID %>">Status</label>
                                        <asp:DropDownList ID="ddlststatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="-1">Select Status</asp:ListItem>
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Deactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-report-footer">
                            <asp:Button ID="btnfet" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnfet_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="fa fa-list-alt"></i> Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="admin-table-toolbar">
                                <span class="admin-table-caption"><i class="fa fa-users"></i> Direct Member List</span>
                            </div>
                            <div class="admin-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"
                                    AutoGenerateColumns="False" EmptyDataText="No members found."
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                    AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("userid") %>'></asp:Label>
                                                <asp:Label ID="LblUserImage" runat="server" Visible="false" Text='<%# Eval("UserImage") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserId" runat="server" CssClass="admin-member-id" Text='<%# Eval("UserId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member Login" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" CommandName="edt"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CssClass="admin-action-btn is-member-login"
                                                    ToolTip="Open member login panel">
                                                    <i class="fa fa-sign-in"></i> Login Panel
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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



<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="CategWiseLevelMaster.aspx.cs" Inherits="admin_CategWiseLevelMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Category Wise Level master     
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Level master</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Select Category</label>
                        <asp:DropDownList ID="ddcountry" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> Select Category</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                
            </div>

            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Level List</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="form-group">

                                <asp:LinkButton ID="btnUpdate" class="btn btn-round btn-success tooltips" OnClick="btnUpdate_Click" Style="margin: 6px 10px 0px 0px; color: white;" runat="server"><i class="fa fa-pencil" style="color:white;"></i> Update</asp:LinkButton>
                            </div>
                            <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Level">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllevel" runat="server" Text='<%#Eval("level") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comission %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcomission" CssClass="form-control" Text='<%#Eval("CommissionPercent") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                        <!-- /.box-body -->



                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


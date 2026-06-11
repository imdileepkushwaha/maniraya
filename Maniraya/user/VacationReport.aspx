<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="VacationReport.aspx.cs" Inherits="VacationReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Vacation Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Award & vacation</a></li>
            <li class="active">Vacation Report</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Detials</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Award ID">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Award Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labawardname" runat="server" Text='<%#Eval("vacationname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                                        <asp:Label ID="lblfromdate1" runat="server" Text='<%#Eval("Fromdate") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                                        <asp:Label ID="lbltodate1" runat="server" Text='<%#Eval("Todate") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Left Bv">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentLeftBv" runat="server" Text='<%#Eval("CurrentLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Right Bv">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentRightBv" runat="server" Text='<%#Eval("CurrentRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Required Left Bv">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredLeftBv" runat="server" Text='<%#Eval("RequiredLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Required Right Bv">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredRightBv" runat="server" Text='<%#Eval("RequiredRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
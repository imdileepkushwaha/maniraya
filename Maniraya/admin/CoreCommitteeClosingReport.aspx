<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="CoreCommitteeClosingReport.aspx.cs" Inherits="admin_CoreCommitteeClosingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
        <h1>Core Committee Closing Report
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Closing</a></li>
            <li class="active">Core Committee Closing Report </li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                   <%-- <div class="box box-primary">
                        <div class="box-header with-border">
                          <h3 class="box-title">Set Criteria</h3>
                        </div>
                          <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Turnover Amount :</label>
                                        <asp:TextBox ID="txtamount" CssClass="form-control" runat="server" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Percentage(%) :</label>
                                        <asp:TextBox ID="txtpercentage" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Pay" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel"  OnClick="btnCancel_Click" />
                        </div>
                    </div>--%>
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Core Committee Closing Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gvusers" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblleft" runat="server" Text='<%#Eval("leftuser") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblright" runat="server" Text='<%#Eval("rightuser") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="TDS(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltdsper" runat="server" Text='<%#Eval("tdsPer") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="TDS Charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltds" runat="server" Text='<%#Eval("tdsCharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="Admin(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladminchargeper" runat="server" Text='<%#Eval("adminPer") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Admin Charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladmincharge" runat="server" Text='<%#Eval("adminCharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                  <asp:TemplateField HeaderText="Payable Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpayableamount" runat="server" Text='<%#Eval("payableAmount") %>'></asp:Label>
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
                        </div>
                        <div class="box-footer">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


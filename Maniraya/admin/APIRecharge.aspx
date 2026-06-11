<%@ Page Title="Recharge API" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="APIRecharge.aspx.cs" Inherits="admin_APIRecharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
      Recharge API   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">API Management</a></li>
        <li class="active"> Recharge API </li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="row">
     

     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Recharge API List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row table-responsive">
  <div class="ibox-tools">
                        <asp:HiddenField ID="hdn_Id1" runat="server" />
                        <asp:LinkButton ID="btnAdd" class="btn btn-round btn-success tooltips" OnClick="btnAdd_Click" Style="margin: 6px 10px 0px 0px;" runat="server"><i class="fa fa-plus" style="color:white;"></i></asp:LinkButton>
                    </div>
                          <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" >
                               <Columns>
                                        <asp:TemplateField Visible="true" ItemStyle-Width="1%">
                                            <HeaderTemplate>ID</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%">
                                            <HeaderTemplate>Name</HeaderTemplate>
                                            <ItemTemplate><%#Eval("Name") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%">
                                            <HeaderTemplate>API Url</HeaderTemplate>
                                            <ItemTemplate><%#Eval("Url") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%">
                                            <HeaderTemplate>Status Check Url</HeaderTemplate>
                                            <ItemTemplate><%#Eval("StatusCheckUrl") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%">
                                            <HeaderTemplate>Balance Url</HeaderTemplate>
                                            <ItemTemplate><%#Eval("BalanceUrl") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%">
                                            <HeaderTemplate>Dispute Url</HeaderTemplate>
                                            <ItemTemplate><%#Eval("DefaultUrl") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="2%">
                                            <HeaderTemplate>Errors</HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="scrollbar" id="style-4">
                                                    <div class="force-overflow">
                                                        <%#Eval("Errors") %>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="2%">
                                            <HeaderTemplate>Activity</HeaderTemplate>
                                            <ItemTemplate><%#Eval("Status") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%">
                                            <HeaderTemplate>Action</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkUpdate" class="btn btn-round btn-warning tooltips fa fa-pencil" CommandName="EditUser" runat="server"></asp:LinkButton>
                                                <asp:LinkButton ID="LnkEnable" class="btn btn-round btn-danger tooltips bottom_mrg" CommandName="EnableUser" runat="server"></asp:LinkButton>
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


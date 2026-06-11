<%@ Page Title="" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="PoolReport.aspx.cs" Inherits="user_PoolReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
       Pool Report   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Report</a></li>
        <li class="active"> Pool Report   </li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                               
                                <div class="col-md-4">
                                    <div class="form-group">
                                       <label>User ID</label>
                                        <asp:TextBox ID="TxtUserId" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                     
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Pool No</label>
                                        <asp:DropDownList ID="DDlstpoolNo" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                             <asp:ListItem Value="2">2</asp:ListItem>
                                             <asp:ListItem Value="3">3</asp:ListItem>
                                             <asp:ListItem Value="4">4</asp:ListItem>
                                             <asp:ListItem Value="5">5</asp:ListItem>
                                        </asp:DropDownList>
                                    
                                    </div>
                                </div>
                            </div>
                            

                             

                        </div>
                        <div class="box-footer">
                               
                           
                             <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />


                        </div>

                    </div>
                </div>
                  <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>                           
                              <div style="float: right">
                     
                        </div>
                    
                        <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                            <asp:GridView ID="GrdPoolId" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Pool ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("poolid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
           <asp:TemplateField HeaderText="Entry Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntrytype" runat="server" Text='<%#Eval("Entrytype") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
        
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                   </Columns>
                                                </asp:GridView>
                          </div>
                                    </div>
                                  </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                       <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Pool ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("userpoolid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                      <asp:TemplateField HeaderText="Parent Pool ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("parentuserpoolid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                   
                                     <asp:TemplateField HeaderText="LevelNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevelNo" runat="server" Text='<%#Eval("userlevel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     
                                     
                                   
                                </Columns>
                            </asp:GridView>
                                            </div>
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
          <Triggers>
      
    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


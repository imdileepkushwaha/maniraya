<%@ Page Title="Tree View" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TreeView.aspx.cs" Inherits="admin_DownlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1 style="color:white">
      Tree View    
      </h1>
      <ol class="breadcrumb">
      <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home > </a></li>
            <li><a href="#">My Team > </a></li>
            <li class="active">My Treeview</li>
      </ol>
    </section>    
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" style="color:white">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Search Criteria</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row" style="color:white">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >User ID</label>
                  <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
                
                </div>             
               </div>
                      </div>
                   <div class="box-footer">
               
                 <asp:Button ID="btnSubmit"  CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
              </div>
                  </div>

                
                 </div>
         </div>
                </div>

             <div class="row" style="color:white">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Search Criteria</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                    <div class="col-md-12">
                <div class="form-group">
                  <label >Downline List</label>
                  <div class="table-responsive">

                                <asp:Panel ID="pnllist" runat="server" Visible="false">
                     <div class="widget  box-inverse">
            <h4 class="widgettitle">User List</h4>
            <div class="widgetcontent">
                 <div class="table-responsive">
                         `<asp:TreeView ShowLines="true" ID="Account_Chart" runat="server" ExpandDepth="0" ImageSet="Simple"  OnTreeNodePopulate="Account_Chart_TreeNodePopulate" BorderStyle="None">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="white" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="white" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="white" />
                                            <LeafNodeStyle ForeColor="white" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="white" HorizontalPadding="5px"
                                                NodeSpacing="0px" VerticalPadding="0px" />
                                        </asp:TreeView>

                        <asp:Literal ID="ltteam" runat="server"></asp:Literal>
                     </div>

             
            </div>          
                
            </div>
                            </asp:Panel>
                        </div>
                </div>             
               </div>
                   
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


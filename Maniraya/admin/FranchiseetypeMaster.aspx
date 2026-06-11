<%@ Page Title="Level Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FranchiseetypeMaster.aspx.cs" Inherits="FranchiseetypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Franchisee Type Master    
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Franchisee Type    </li>
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
              <h3 class="box-title">Deduction Charge</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="form-group">
                  
                 <asp:LinkButton ID="btnUpdate" class="btn btn-round btn-success tooltips" OnClick="btnUpdate_Click" style="margin:6px 10px 0px 0px;color:white;" runat="server"><i class="fa fa-pencil" style="color:white;"></i> Update</asp:LinkButton>
                </div>  
                <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Type">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtAdminCharge" runat="server"  Text='<%#Eval("type") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Profit">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtTdswithpam" runat="server"  Text='<%#Eval("profit") %>' CssClass="form-control"></asp:Textbox>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


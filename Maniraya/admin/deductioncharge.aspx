<%@ Page Title="Level Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="deductioncharge.aspx.cs" Inherits="deductioncharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Deduction Charge    
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Deduction Charge    </li>
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
                           <asp:TemplateField HeaderText="Admin Charge">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtAdminCharge" runat="server"  Text='<%#Eval("admincharge") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS With Pan">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtTdswithpam" runat="server"  Text='<%#Eval("tdswithpan") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Without Pan">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtTdswithoutpan" runat="server"  Text='<%#Eval("tdswithoutpan") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cash Wallet">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtcashWallet" runat="server"  Text='<%#Eval("CashWallet") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash Wallet Percentage">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtcashWalletPercentage" runat="server"  Text='<%#Eval("CashWalletPercent") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Capping Amount">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtCappingAmount" runat="server"  Text='<%#Eval("CappingAmount") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Deposit Amount">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtMinAmt" runat="server"  Text='<%#Eval("MinDepositAmount") %>' CssClass="form-control"></asp:Textbox>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Deposit Amount">
                               <ItemTemplate>
                                     <asp:Textbox ID="TxtMaxAmt" runat="server"  Text='<%#Eval("MaxDepositAmount") %>' CssClass="form-control"></asp:Textbox>
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


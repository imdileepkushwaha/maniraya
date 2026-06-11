<%@ Page Title="" Language="C#" MasterPageFile="~/Franchisee/franchiseemaster.master" AutoEventWireup="true" CodeFile="account_Ledger.aspx.cs" Inherits="Franchisee_account_Ledger" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">

    <section class="content-header">
      <h1>
        Account Ledger
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Report</a></li>
        <li class="active">Account Ledger </li>
      
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
                            <h3 class="box-title">Recharge Report</h3>
                        </div>

                        <div class="box-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-3">
                 
                   
                
                    <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" CssClass="pull-right" Width="40px" OnClick="imgExcel_Click" />
                  
                </div>
            </div>
              <div class="row">
                <div class="col-md-10 text-center">
                                  
              
                </div>
                       <div class="col-md-2">
                           <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged" Width="80px">
                       
                      
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                           </div>

            </div>
            <div class="row">
                <div class="col-md-12 table-responsive">
                    <asp:GridView ID="grdAccountsList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-responsive datatable" 
                        Width="100%" GridLines="Horizontal" ShowFooter="true" OnRowDataBound="grdAccountsList_RowDataBound">
                        <FooterStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("MentionDate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Franchisee Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblFranchiseeId" runat="server" Text='<%# Eval("userID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblTransactionId" runat="server" Text='<%# Eval("TransactionId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit">
                                <ItemTemplate>
                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DrAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDebitTotal" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit">
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CrAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCreditTotal" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="old Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbloldBalance" runat="server" Text='<%# Eval("oldBalance") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrentBalance" runat="server" Text='<%# Eval("CurrentBalance") %>'></asp:Label>
                                </ItemTemplate>    
                                   <FooterTemplate>
                                    <asp:Label ID="lblCurrentBalanceTotal" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>                           
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
                            </div>
                        </div>
                        </div>
                   </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


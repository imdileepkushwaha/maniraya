<%@ Page Title="DMR Commission" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="DMRCommissionAdd.aspx.cs" Inherits="admin_DMRCommissionAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
       DMR Commission   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Money Transfer</a></li>
        <li class="active">DMR Commission</li>
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
                            <h3 class="box-title">Search Criteria</h3>
                        </div>

                        <div class="box-body">
                              <div class="row">

                           
                                       <div class="col-md-12" style="text-align:center;">
                    <asp:Button ID="btnAddDMRCommission" runat="server" Text="Save DMR Commission" class="btn btn-primary" OnClick="btnAddDMRCommission_Click" />
                </div>
                               
                          
                               
                            </div>    
                                                     <br />
                              <div class="row">

                                <div class="col-md-12">
                                  <div class="table-responsive">
                        <asp:GridView ID="GridView1" class="table table-bordered table-hover dataTable" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>Min</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMinAmt"  runat="server" class="form-control groupOfTexbox" onkeyup="IsNumeric(this.id)" onchange="IsNumeric(this.id)" onfocus="IsNumeric(this.id)" Text='<%# Eval("MinAmt") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Max</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMaxAmt"  runat="server" class="form-control groupOfTexbox" onkeyup="IsNumeric(this.id)" onchange="IsNumeric(this.id)" onfocus="IsNumeric(this.id)" Text='<%# Eval("MaxAmt") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>CommType</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblComType" runat="server" Text='<% # Eval("ComType")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlComType" runat="server" Class="form-control">
                                            <asp:ListItem Value="S">Surcharge</asp:ListItem>
                                            <asp:ListItem Value="C">Commission</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>AmountType</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmtType" runat="server" Text='<% # Eval("AmtType")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlAmtType" runat="server" Class="form-control">
                                            <asp:ListItem Value="F">Flat</asp:ListItem>
                                            <asp:ListItem Value="V">Variable</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>OldRate</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAtRate" ReadOnly="true" runat="server" class="form-control groupOfTexbox" onkeyup="IsDecimal(this.id)" onchange="IsDecimal(this.id)" onfocus="IsDecimal(this.id)" Text='<%# Eval("AtRate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>NewRate</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNewRate" runat="server" class="form-control groupOfTexbox" onkeyup="IsDecimal(this.id)" onchange="IsDecimal(this.id)" onfocus="IsDecimal(this.id)" Text='<%# Eval("NewRate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<% # Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lnkSaveNew" CssClass="btn btn-round btn-info fa fa-plus-square-o" runat="server" OnClientClick="return validateMinMax();" OnClick="lnkSaveNew_Click" Visible="false"></asp:LinkButton>
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
            </div>
               </ContentTemplate>
    </asp:UpdatePanel>
        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


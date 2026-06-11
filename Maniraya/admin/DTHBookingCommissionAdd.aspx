<%@ Page Title="DMR Commission" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="DTHBookingCommissionAdd.aspx.cs" Inherits="admin_DMRCommissionAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
       DTH Booking Commission   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">DTH Subcription</a></li>
        <li class="active">DTH Booking Commission </li>
      </ol>
    </section>      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
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
                    <asp:Button ID="btnAddDMRCommission" runat="server" Text="Save Commission" class="btn btn-primary" OnClick="btnAddDMRCommission_Click" />
                </div>
                               
                          
                               
                            </div>    
                                                     <br />
                              <div class="row">

                                <div class="col-md-12">
                                  <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                <asp:TemplateField HeaderText="Sr">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Operator">
                                    <ItemTemplate>
                                          <asp:HiddenField ID="HDId" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="txt_OperatorId" runat="server" Value='<%# Bind("Operatorid") %>'></asp:HiddenField>
                                        <asp:Label ID="lbl_OperatorName" runat="server" Text='<%# Bind("Operatorname") %>'></asp:Label>
                                    </ItemTemplate>
                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Package">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="txt_PackageID" runat="server" Value='<%# Bind("PackageID") %>'></asp:HiddenField>
                                        <asp:Label ID="lbl_Packagename" runat="server" Text='<%# Bind("Packagename") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commission Type">                                  
                                    <ItemTemplate>
                                         <asp:Label ID="lbl_commtype" runat="server" Text='<%# Bind("ComType") %>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlcommissiontype" CssClass="form-control" runat="server">
                                            <asp:ListItem value="S">Surcharge</asp:ListItem>
                                              <asp:ListItem value="C">Commission</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount Type">                                  
                                    <ItemTemplate>
                                           <asp:Label ID="lbl_amttype" runat="server" Text='<%# Bind("AmtType") %>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlamounttype" CssClass="form-control" runat="server">
                                              <asp:ListItem value="F">Flat</asp:ListItem>
                                              <asp:ListItem value="V">Variable</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="At rate">                                  
                                    <ItemTemplate>
                                        <asp:TextBox ID="Txtrate" runat="server" CssClass="form-control" Text='<%# Bind("AtRate") %>' TextMode="Number"></asp:TextBox>
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


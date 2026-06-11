<%@ Page Title="" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="POOLCLOSINGREPORT.aspx.cs" Inherits="LuckyDrawClosingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
  <section class="content-header">
      <h1>
       Pool Closing Report   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Closing</a></li>
        <li class="active"> Pool Closing Report   </li>
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
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>From date</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>To date</label>
                                        <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="col-md-4">
                                    <div class="form-group">
                                       <label>User ID</label>
                                        <asp:TextBox ID="TxtUserId" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                     
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                      
                                    
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
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick = "ExportToExcel" /></div>
                        </div>
                    
                        <div class="box-body">
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
                               
                                    <asp:TemplateField HeaderText="From date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate") %>'></asp:Label>
                                             <asp:Label ID="lblId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("ToDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Pool ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("Poolno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Level No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("LevelNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                         <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                  
                                     <asp:TemplateField HeaderText="NetIncome">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommission" runat="server" Text='<%#Eval("Commission") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Admin %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdminPer" runat="server" Text='<%#Eval("AdminPer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="AdminCharge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdminCharge" runat="server" Text='<%#Eval("AdminCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                                                 
                                     <asp:TemplateField HeaderText="PaybleAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaybleAmount" runat="server" Text='<%#Eval("PaybleAmount") %>'></asp:Label>
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
      
        <asp:PostBackTrigger ControlID = "ImageButton1" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


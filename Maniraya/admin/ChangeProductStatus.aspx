<%@ Page Title="Level Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ChangeProductStatus.aspx.cs" Inherits="admin_ChangeProductStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Change Status     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active">Change Status</li>
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
                                        <label>Product Name</label>
                                     <asp:TextBox ID="TxtProductNameSearch" CssClass="form-control " runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Code</label>
                                      <asp:TextBox ID="TxtProductCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                          <label>Status</label>
                                          <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Deactive</asp:ListItem>
                                         
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                            </div>

                             

                        </div>
                        <div class="box-footer">
                              
                              <asp:Button ID="BtnSearch"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />



                        </div>

                    </div>
                </div>
          <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Change Status</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="form-group">
                  
                 <asp:LinkButton ID="btnUpdate" class="btn btn-round btn-success tooltips" OnClick="btnUpdate_Click" style="margin:6px 10px 0px 0px;color:white;" runat="server"><i class="fa fa-pencil" style="color:white;"></i> Update</asp:LinkButton>
                </div>  
                <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" >
                                <Columns>
                                 <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                          <asp:Label ID="LblDescription" runat="server" Visible="false" Text='<%#Eval("Description") %>'></asp:Label>
                                          <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%#Eval("Image") %>'></asp:Label>
                                         <asp:Label ID="LblStatuschk" runat="server" Visible="false" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblCountryname" runat="server"  Text='<%#Eval("Categoryname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product Code">
                               <ItemTemplate>
                                   <asp:Label ID="lblid" runat="server"  Text='<%#Eval("ProductId") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename" runat="server"  Text='<%#Eval("ProductName") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename1" runat="server"  Text='<%#Eval("Amount") %>'></asp:Label>
                               </ItemTemplate>

                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Product Image" >
                               <ItemTemplate>
                                  
                                  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px"  />
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                   <asp:CheckBox ID="ChkStatus" runat="server" />
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Purchase Status">
                               <ItemTemplate>
                               <asp:Label ID="lblstatus1" runat="server"  Text='<%#Eval("PurchaseStatus1") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>--%>
                                        
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


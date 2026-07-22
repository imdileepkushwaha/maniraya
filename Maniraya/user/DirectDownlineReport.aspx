<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DirectDownlineReport.aspx.cs" Inherits="admin_DirectReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .Paid {
            background-color: #006b0d;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
        .Unpaid {
            background-color: #bf0a0a;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1 style="color:white">
       Downline Report  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home > </a></li>
            <li><a href="#">My Team > </a></li>
            <li class="active">My Direct</li>
      
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
              <h3 class="box-title">Downline Report</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                   <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                  <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                
                             </div>
                         </div>
                     </div>


                   </div>
              </div>
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
                   <div class="box-body">
                          <div class="row">
                        <div class="col-md-6">
                             <div class="form-group">
                                   <label> Left Team : </label> <asp:Label ID="LblTotalLeft" runat="server" Text="Label"></asp:Label> 

                                 </div></div>

                            
                                  <div class="col-md-6">


                             <div class="form-group">
                                     <label> Right Team : </label><asp:Label ID="LblTotalright" runat="server" Text="Label"></asp:Label>

                                 </div></div>
  </div>
                             
                          


              

                     
                   <div class="row">
                        <div class="col-md-6">
                             <div class="form-group">
                                 <label> Left Team</label>
                                  <div class="table-responsive">
                                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%"  AutoGenerateColumns="false" EmptyDataText="No Data Found" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Parent ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponserid" runat="server" Text='<%#Eval("ParentUserId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponsername" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     
                                </Columns>
                            </asp:GridView>
                                      </div>
                             </div>
                        </div>
                          <div class="col-md-6">
                             <div class="form-group">
                                     <label> Right Team</label>
                                  <div class="table-responsive">
                                 <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  EmptyDataText="No Data Found" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parent ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponserid" runat="server" Text='<%#Eval("ParentUserId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponsername" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
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
             </div>





         
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


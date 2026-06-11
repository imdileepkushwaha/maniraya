<%@ Page Title="Level Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="AwardMaster.aspx.cs" Inherits="AwardMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Award master     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Award master</li>
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
              <h3 class="box-title">Level List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="form-group">
                  
                 <asp:Button ID="btnUpdate" class="btn btn-round btn-success tooltips" OnClick="btnUpdate_Click" style="margin:6px 10px 0px 0px;color:white;" runat="server" Text="Update"></asp:Button>
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
                           <asp:TemplateField HeaderText="Level">
                               <ItemTemplate>
                                     <asp:Label ID="lbllevel" runat="server"  Text='<%#Eval("ulevel") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate>
                                   <asp:TextBox ID="txtamount" CssClass="form-control" Text='<%#Eval("Amount") %>'  runat="server"></asp:TextBox>
                               </ItemTemplate>
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Target (BV)">
                               <ItemTemplate>
                                   <asp:TextBox ID="txttarget" CssClass="form-control" Text='<%#Eval("target") %>'  runat="server"></asp:TextBox>
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


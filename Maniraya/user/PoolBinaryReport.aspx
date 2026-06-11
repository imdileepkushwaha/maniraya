<%@ Page Title="Binary Report" Language="C#" MasterPageFile="Masterpage.master" AutoEventWireup="true" CodeFile="PoolBinaryReport.aspx.cs" Inherits="PoolBinaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1 style="color:white;">
        Pool Wise Binary Report  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home ></a></li>
            <li><a href="#">My Team ></a></li>
        <li class="active">Pool Wise Binary  </li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


              <div class="row" style="color:white;">
          <div class="col-md-12">
               <div class="box box-primary" >
            <div class="box-header with-border">
              <h3 class="box-title">Genealogy Report </h3>
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
              <h3 class="box-title">Genealogy Report</h3>
            </div>
                   <div class="box-body">
                                   <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                            <asp:GridView ID="GrdPoolId" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Pool ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupid" runat="server" Text='<%#Eval("poolid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
           <asp:TemplateField HeaderText="Entry Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntrytype" runat="server" Text='<%#Eval("Entrytype") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
        
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
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
               <div class="row" style="background-color:#ffffff">
                       
                             <div class="col-md-12">
                                 <iframe id="f1" runat="server" style="width:100%" height="500px"></iframe>
                             </div>
                        
                        
                     </div>



           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>



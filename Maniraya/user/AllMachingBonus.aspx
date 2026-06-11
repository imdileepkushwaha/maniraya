<%@ Page Title="Repurchase Maching Bonus" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="AllMachingBonus.aspx.cs" Inherits="AllMachingBonus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
     All Bonus  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">My Income</a></li>
        <li class="active"> All Bonus  </li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>User ID :</label>
                                 <asp:TextBox ID="txtuserid"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
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


              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                      <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                     
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                               
                                  
                                       <asp:TemplateField HeaderText="Group maching Bonus">
                                        <ItemTemplate>
                                            <asp:Label ID="LblLeftBv" runat="server" Text='<%#Eval("GroupmatchingBonus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Repurchase Bonus">
                                        <ItemTemplate>
                                            <asp:Label ID="LblRightBv" runat="server" Text='<%#Eval("RepurchaseBonus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="Lifestyle bonus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmathingBv" runat="server" Text='<%#Eval("LifeStyleBonus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                     <asp:TemplateField HeaderText="Travel Bonus">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCfLeftBv" runat="server" Text='<%#Eval("TravelBonus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="CarBonus">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCfRightBv" runat="server" Text='<%#Eval("carBonus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="TotalAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommissionPer" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TDS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommission" runat="server" Text='<%#Eval("Tds") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderText="Admin Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommission" runat="server" Text='<%#Eval("admincharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Final Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommission" runat="server" Text='<%#Eval("finalamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
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
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
     
</asp:Content>


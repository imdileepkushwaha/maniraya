<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="PurchaseInvoice.aspx.cs" Inherits="user_PurchaseInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="container-fluid pt-4">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Purchase Invoice</h5>
                            </div>
                            <div class="card-body">
                       
                          <div class="row">
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>From date :</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                 
                             </div>
                         </div>
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>To date :</label>
                                  <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                  
                             </div>
                         </div>
                               <div class="col-md-3" style="display:none;">
                             <div class="form-group">
                                 <label>User ID :</label>
                                 <asp:TextBox ID="txtuserid"  CssClass="form-control form_date" runat="server" ></asp:TextBox>
                             </div>
                         </div>
                                  <div class="col-md-3" style="display:none;">
                             <div class="form-group">
                                 <label>Status :</label>
                                 <asp:DropDownList ID="DDLSTStatus" CssClass="form-control form_date" runat="server">
                                  
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="2">Reject</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                         
                           <div class="row">
                         <div class="col-md-6">
                               <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                             </div>
                       </div>
                      
                        
                <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID">
                               <Columns>
                                    <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="img/PLUS.jpg" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable" >
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductID" HeaderText="Product Code" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product Name" />
                               <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantity" />
                                  <asp:BoundField ItemStyle-Width="150px" DataField="DP" HeaderText="DP" />
                                
                                <asp:BoundField ItemStyle-Width="150px" DataField="TotalDP" HeaderText="TotalDP" />
                                
                          
                              
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="User name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="EmailId">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridEmailId" runat="server" Text='<%#Eval("EmailId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="ContactNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridContactNo" runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("PurchaseID") %>' Visible="false"></asp:Label>
											   <asp:Label ID="LblOrderNo" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("PurchaseDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                   
                                  
                                      <asp:TemplateField HeaderText="Total DP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDP" runat="server" Text='<%#Eval("TotalDP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                             <asp:Label ID="LblInvoiceStatus" runat="server" Text='<%#Eval("InvoiceStatus") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblPstatus" runat="server" Text='<%#Eval("PStatus") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
								        <%--  <asp:TemplateField HeaderText="Status" Visible="false">
                                        <ItemTemplate>
											  <asp:Label ID="Lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
										     </ItemTemplate>
                                    </asp:TemplateField> --%>    
                                                 <asp:TemplateField HeaderText="Invoice">
                                        <ItemTemplate>
                                            
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Invoice" class="btn btn-outline-dark btn-text w-100" NavigateUrl='<%# string.Format("MehndilinkInvoice.aspx?OrderNo={0}",
                    HttpUtility.UrlEncode(Eval("PurchaseID").ToString())) %>' Target="_blank"></asp:HyperLink>
                                        
                                            
                                          
                                         
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
                 </section>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
	
	  <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'MM/DD/YYYY',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
       
    
</asp:Content>

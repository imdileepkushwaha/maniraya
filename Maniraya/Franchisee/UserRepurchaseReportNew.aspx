<%@ Page Title="" Language="C#" MasterPageFile="~/Franchisee/franchiseemaster.master" AutoEventWireup="true" CodeFile="UserRepurchaseReportNew.aspx.cs" Inherits="UserRepurchaseReportNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
     function Check_Click(objRef) {
             //Get the Row based on checkbox
             var row = objRef.parentNode.parentNode;
             if (objRef.checked) {
                 //If checked change color to Aqua
                 row.style.backgroundColor = "aqua";
             }
             else {
                 //If not checked change back to original color
                 if (row.rowIndex % 2 == 0) {
                     //Alternating Row Color
                     row.style.backgroundColor = "white";
                 }
                 else {
                     row.style.backgroundColor = "white";
                 }
             }
             //Get the reference of GridView
             var GridView = row.parentNode;
             //Get all input elements in Gridview
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < inputList.length; i++) {
                 //The First element is the Header Checkbox
                 var headerCheckBox = inputList[0];
                 //Based on all or none checkboxes
                 //are checked check/uncheck Header Checkbox
                 var checked = true;
                 if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                     if (!inputList[i].checked) {;
                         break;
                     }
                 }
             }
            // headerCheckBox.checked = checked;
         }
         function checkAll(objRef) {
             var GridView = objRef.parentNode.parentNode.parentNode;
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < inputList.length; i++) {
                 //Get the Cell To find out ColumnIndex
                 var row = inputList[i].parentNode.parentNode;
                 if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                     if (objRef.checked) {
                         //If the header checkbox is checked
                         //check all checkboxes
                         //and highlight all rows
                         row.style.backgroundColor = "aqua";
                         inputList[i].checked = true;
                     }
                     else {
                         //If the header checkbox is checked
                         //uncheck all checkboxes
                         //and change rowcolor back to original 
                         if (row.rowIndex % 2 == 0) {
                             //Alternating Row Color
                             row.style.backgroundColor = "white";
                         }
                         else {
                             row.style.backgroundColor = "white";
                         }
                         inputList[i].checked = false;
                     }
                 }
             }
         }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
      User Repurchase Report
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Repurchase</a></li>
        <li class="active">User Repurchase Report</li>
      </ol>
    </section>  
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
             <section class="content">
            <div class="container-fluid">
            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Search Criteria</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>From date :</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtfromdate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
                         </div>
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>To date :</label>
                                  <asp:TextBox ID="txttodate"  CssClass="form-control" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txttodate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
                         </div>
                               <div class="col-md-3">
                             <div class="form-group">
                                 <label>User ID :</label>
                                 <asp:TextBox ID="txtuserid"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                                  <div class="col-md-3">
                             <div class="form-group">
                                 <label>Status :</label>
                                 <asp:DropDownList ID="DDLSTStatus" CssClass="form-control" runat="server" Enabled="false">
                                     <asp:ListItem Value="0">Pending</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="2">Reject</asp:ListItem>
                                 </asp:DropDownList>
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
            </div>
                   <div class="box-body table-responsive">
                  <div class="row">
                       <div class="col-md-9">
                           </div>
                       <div class="col-md-3">
                       <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                           <asp:Button ID="BtnReject" runat="server" Text="Reject" CssClass="btn btn-danger" OnClick="BtnReject_Click" />
                           </div>
                  </div>
                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID">
                               <Columns>
                                     <asp:TemplateField HeaderText="">
                                             <HeaderTemplate>
                                                 <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);"/>
                                         </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:CheckBox ID="ChkStatus" runat="server" onclick = "Check_Click(this)"/>
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
                                             <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%#Eval("ImageIMAGE") %>'></asp:Label>
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
                                      <asp:TemplateField HeaderText="address">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridaddress" runat="server" Text='<%#Eval("address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
								    <asp:TemplateField HeaderText="shippingaddress">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridShippingaddress" runat="server" Text='<%#Eval("Shippingaddress") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Purchase Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
											 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                           
											   <asp:Label ID="LblOrderNo" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("PurchaseDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                   
                                  
                                    
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                             <asp:Label ID="LblInvoiceStatus" runat="server" Text='<%#Eval("InvoiceStatus") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblPstatus" runat="server" Text='<%#Eval("PStatus") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                      <asp:TemplateField HeaderText="Transactionid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionid" runat="server" Text='<%#Eval("transactionid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Bank Slip" >
                               <ItemTemplate>
                                     <asp:LinkButton ID="lnkph" runat="server" CommandName="photolarge"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageIMAGE") %>' Height="40px" Width="40px"  /></asp:LinkButton>
                               </ItemTemplate>

                                       
                           </asp:TemplateField>
								          <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
											  <asp:Label ID="Lblstatus" runat="server" ></asp:Label>
										     </ItemTemplate>
                                    </asp:TemplateField>     
                                                 <asp:TemplateField HeaderText="Product Detail">
                                        <ItemTemplate>
                                            
                                               <asp:LinkButton ID="btnSuccess" CssClass="btn btn-success" CommandName="moredetail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Success" runat="server"><i class="icon fa fa-eye" aria-hidden="true"></i></asp:LinkButton>
                                            
                                          
                                         
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
                  <div id="DivPhotolarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImageLarge" runat="server" Width="570px" Height="400px" />
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>


                <div id="DivProductDetail" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                   <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable" >
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductID" HeaderText="Product Code" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product Name" />
                               <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantity" />
                               
                                   <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                             
                                  <asp:BoundField ItemStyle-Width="150px" DataField="TotalAmount" HeaderText="Total Amount" />                      
                            <asp:BoundField ItemStyle-Width="150px" DataField="BV" HeaderText="Total BV" />                      
                               <asp:BoundField ItemStyle-Width="150px" DataField="Type" HeaderText="Type" />
                        </Columns>
                    </asp:GridView>       
                        
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
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
       
       <script type="text/javascript">
         		   function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();

        }

        function showModal2() {
            $('#DivProductDetail').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup2() {
            $('#DivProductDetail').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();

        }
     </script>
</asp:Content>


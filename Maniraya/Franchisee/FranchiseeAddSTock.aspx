<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="FranchiseeAddSTock.aspx.cs" Inherits="FranchiseeAddSTock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%-- <script type="text/javascript">
        function gettotal() {

            var Quantity = 0, Amount = 0, TotalAMount = 0;
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value != "") {
                Quantity = document.getElementById("<%=TxtQuantity.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtAmount.ClientID%>").value != "") {
                Amount = document.getElementById("<%=TxtAmount.ClientID%>").value;
              }
              var TotalAMount = Quantity * Amount;


              document.getElementById("<%=TxtTotalAmount.ClientID%>").innerText = TotalAMount;
        }
        function gettotal2() {

            var TotalPurchase = 0, CGST = 0, SGST = 0; IGST = 0; CalculateCGST = 0; CalculateSGST = 0; CalculateIGST = 0;; TotalC = 0;
            if (document.getElementById("<%=TxtTotalPrice.ClientID%>").value != "") {
                TotalPurchase = document.getElementById("<%=TxtTotalPrice.ClientID%>").value;
            }
          //  if (document.getElementById("<%=TxtCGST.ClientID%>").value != "") {
           //     CGST = document.getElementById("<%=TxtCGST.ClientID%>").value;
           // }
          //  if (document.getElementById("<%=TxtSGST.ClientID%>").value != "") {
          //      SGST = document.getElementById("<%=TxtSGST.ClientID%>").value;
          //  }
          //  if (document.getElementById("<%=TxtIGST.ClientID%>").value != "") {
          //      IGST = document.getElementById("<%=TxtIGST.ClientID%>").value;
          //  }
            
            var CalculateCGST = TotalPurchase * (CGST / 100);
            var CalculateSGST = TotalPurchase * (SGST / 100);
            var CalculateIGST = TotalPurchase * (IGST / 100);
            var TotalC = Number(TotalPurchase) + Number(CalculateCGST) + Number(CalculateSGST) + Number(CalculateIGST);

            
        //    document.getElementById("<%=TxtCGstAmount.ClientID%>").innerText = parseFloat(CalculateCGST, 2).toFixed(2);
           // document.getElementById("<%=TxtSGstAmount.ClientID%>").innerText = parseFloat(CalculateSGST, 2).toFixed(2);
          //  document.getElementById("<%=TxtIGstAmount.ClientID%>").innerText = parseFloat(CalculateIGST, 2).toFixed(2);

            document.getElementById("<%=TxtpaybleAmount.ClientID%>").innerText = parseFloat(TotalC, 2).toFixed(2);
            document.getElementById("<%= HDTotal.ClientID %>").value = parseFloat(TotalC, 2).toFixed(2)
          }
        function validate() {
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value == "") {

                alert('Enter Quantity');
                document.getElementById("<%=TxtQuantity.ClientID%>").focus();
                return false;
            }
        }
          </script>--%>
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=TxtFranchiseeId.ClientID%>").value == "") {
                alert('Enter Franchisee');
                document.getElementById("<%=TxtFranchiseeId.ClientID%>").focus();
                return false;
            }
         
            if (document.getElementById("<%=DDLstProduct.ClientID%>").value == "0") {
                alert('Select Product');
                document.getElementById("<%=DDLstProduct.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPurchaseStock.ClientID%>").value == "") {
                alert('Enter Purchase Quantity  ');
                document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=TxtPurchaseStock.ClientID%>").value == "0") {
                 alert('Enter Purchase Quantity  ');
                 document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                return false;
            }
           if (document.getElementById("<%=TxtPurchasePrice.ClientID%>").value == "") {
              alert('Enter Price');
              document.getElementById("<%=TxtPurchasePrice.ClientID%>").focus();
              return false;
          }
        }

         function validate2() {
             if (document.getElementById("<%=TxtAmount.ClientID%>").value == "") {
                alert('Select Amount');
                document.getElementById("<%=TxtAmount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtMrp.ClientID%>").value == "") {
                alert('Enter MRP');
                document.getElementById("<%=TxtMrp.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=TxtQuantity.ClientID%>").value == "0") {
                alert('Select Quantity');
                document.getElementById("<%=TxtQuantity.ClientID%>").focus();
                return false;
            }           
         }
         function validate3() {
             if (document.getElementById("<%=TxtFranchiseeId.ClientID%>").value == "") {
                 alert('Enter Franchiseeid');
                 document.getElementById("<%=TxtFranchiseeId.ClientID%>").focus();
                return false;
            }          
           //  if (document.getElementById("<%=TxtCGST.ClientID%>").value == "") {
            //     alert('Enter CGST');
            //     document.getElementById("<%=TxtCGST.ClientID%>").focus();
           //      return false;
           //  }
           //  if (document.getElementById("<%=TxtSGST.ClientID%>").value == "") {
           //      alert('Enter SGST');
            //     document.getElementById("<%=TxtSGST.ClientID%>").focus();
            //     return false;
           //  }
           //  if (document.getElementById("<%=TxtIGST.ClientID%>").value == "") {
           //      alert('Enter IGST');
           //      document.getElementById("<%=TxtIGST.ClientID%>").focus();
           //      return false;
           //  }
        }
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
        <section class="content-header">
      <h1>
    Franchisee Purchase
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active"> Franchisee Purchase  </li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="row">
            <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Franchisee Add Stock</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   
                                               <div class="row">
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>Franchiseeid :</label>
                                <asp:TextBox ID="TxtFranchiseeId" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFranchiseeId_TextChanged"/>
                             </div>
                         </div>
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>Franchisee Name :</label>
                               <asp:TextBox ID="TxtFRanchiseeName" runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
												   <div class="col-md-3" style="display:none;">
                             <div class="form-group">
                                 <label>Fund balance :</label>
                               <asp:Label ID="LblCurrentWallet" runat="server" CssClass="form-control"/>
                             </div>
                         </div>
                    <div class="col-md-3">
                             <div class="form-group">
                                 <label>Product :</label>
                               <asp:DropDownList ID="DDLstProduct" CssClass="form-control"  runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DDLstProduct_SelectedIndexChanged"></asp:DropDownList>
                                <asp:TextBox ID="TxtImage" runat="server" Enabled="false" CssClass="form-control" Visible="false" />
                                 
                             </div>
                         </div>
                                                    <div class="col-md-3">
          <div class="form-group">
              <label>Product :</label>
            <asp:DropDownList ID="DDLstSubProduct" CssClass="form-control"  runat="server"  ></asp:DropDownList>
            
              
          </div>
      </div>
                     </div>
                  <hr />
                    <div class="row">
                       
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>Available Limit :</label>
                             <asp:TextBox ID="TxtAvailableStock" runat="server" Enabled="false" CssClass="form-control" />
                                 <asp:HiddenField ID="HDGST" runat="server" />
                             </div>
                         </div>
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label>Purchase Quantity :</label>
                                    <asp:TextBox ID="TxtPurchaseStock" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                           <div class="col-md-3">
                             <div class="form-group">
    <label>
        <asp:Label ID="LblType" runat="server" Text="Label"></asp:Label></label>
                                     <asp:TextBox ID="TxtDP" runat="server" CssClass="form-control" Enabled="false"/>
                                 <asp:TextBox ID="TxtPurchasePrice" runat="server" CssClass="form-control" Enabled="false" />
                             </div>
                         </div>
                     </div>
                     <div class="row">
                      
                        <div class="col-md-3">
                             <div class="form-group">
                                 <label>MRP :</label>
                                     <asp:TextBox ID="TxtPurchaseMRP" runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
                           <div class="col-md-3">
                             <div class="form-group">
                                   <label>GST :</label>
                                   
                                <asp:TextBox ID="TxtGST" runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
                          <div class="col-md-3" style="display:none;">
                             <div class="form-group">
                                 <label>BV :</label>
                                     <asp:TextBox ID="TxtBV" runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
						   <div class="col-md-3" style="display:none;">
                             <div class="form-group">
                                 <label>SV :</label>
                                     <asp:TextBox ID="TxtSV" runat="server" CssClass="form-control" Enabled="false"/>
                             </div>
                         </div>
                     </div>
                                          <div class="row">
                <div class="col-md-12">
                      <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="add" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Remove All" OnClick="btnCancel_Click" />
               
                        </div>
                     </div>
  <asp:Panel ID="PnlDt" Visible="false" runat="server">
     <div class="row">
                               <div class="col-md-12">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Image">
                               <ItemTemplate>
                                   <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px"  />
                                  <asp:Label ID="LblProductImageG" runat="server"  Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Product Code">
                               <ItemTemplate>
                                  
                                    <asp:Label ID="LblProductCodeG" runat="server"  Text='<%#Eval("ProductId") %>'></asp:Label>
                                 
                               </ItemTemplate>
                           </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Product Code">
    <ItemTemplate>
       
         <asp:Label ID="LblSubProductCodeG" runat="server"  Text='<%#Eval("SubProductId") %>'></asp:Label>
      
    </ItemTemplate>
</asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                               <ItemTemplate>
                                    <asp:Label ID="LblProductNameG" runat="server"  Text='<%#Eval("ProductName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SubProduct Name">
    <ItemTemplate>
         <asp:Label ID="LblSubProductNameG" runat="server"  Text='<%#Eval("SubProductName") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="DP/Peices">
                               <ItemTemplate>
                                      <asp:Label ID="LblProductAmountG" runat="server" Text='<%#Eval("Amount") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="LblDP" runat="server"  Text='<%#Eval("DP") %>' ></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="MRP">
                               <ItemTemplate>
                                     <asp:Label ID="LBlMrp" runat="server" Text='<%#Eval("MRP") %>' ></asp:Label>
                                     <asp:Label ID="LblBv" runat="server"  Text='<%#Eval("BV") %>' Visible="false"></asp:Label>
                                      <asp:Label ID="LblSv" runat="server"  Text='<%#Eval("SV") %>' Visible="false"></asp:Label>
                                  
                                   
                                  
                                     <asp:Label ID="LblStock" runat="server"  Text='<%#Eval("STOCK") %>' Visible="false"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                     
                           <asp:TemplateField HeaderText="Quantity">
                               <ItemTemplate>
                                     <asp:Label ID="lblQuantity" runat="server"  Text='<%#Eval("Quantity") %>'></asp:Label>
                               </ItemTemplate>
                                    <FooterTemplate>
      <asp:Label ID="label1" runat="server"  text=""></asp:Label>
     </FooterTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Discount" Visible="false">
                               <ItemTemplate>
                                    <asp:Label ID="LblDiscountAmount" runat="server"  Text='<%#Eval("Discount") %>' ></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Purchase Amount">
                               <ItemTemplate>
                                    <asp:Label ID="LblPurchaseAmount" runat="server"  Text='<%#Eval("PurchaseAmount") %>' ></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
                                      <asp:TemplateField HeaderText="CGST" >
                               <ItemTemplate>
                                    <asp:Label ID="LblCGST" runat="server"  Text='<%#Eval("CGST") %>' ></asp:Label>
                                     <asp:Label ID="LblGSTPER" runat="server"  Text='<%#Eval("GSTPER") %>' Visible="false"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>    
                                       <asp:TemplateField HeaderText="SGST" >
                               <ItemTemplate>
                                    <asp:Label ID="LblSGST" runat="server"  Text='<%#Eval("SGST") %>' ></asp:Label>
                                   
                               </ItemTemplate>
                           </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="IGST" >
                               <ItemTemplate>
                                    <asp:Label ID="LblIGST" runat="server"  Text='<%#Eval("IGST") %>' ></asp:Label>
                                   
                               </ItemTemplate>
                                            <FooterTemplate>
                                                            <asp:Label ID="label1" runat="server" Text=""></asp:Label>
                                                        </FooterTemplate>
                           </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Calculate Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblTotalAmount" runat="server"  Text='<%#Eval("TotalAmount") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="LblTotalDP" runat="server"  Text='<%#Eval("TOTALDP") %>'  ></asp:Label>
                               </ItemTemplate>
                                         <FooterTemplate>
      <asp:Label ID="lblGrandTotal" runat="server"  text=""></asp:Label>
     </FooterTemplate>

                           </asp:TemplateField>                                 
                                          <asp:TemplateField HeaderText="Total BV" Visible="false">
                               <ItemTemplate>
                                     <asp:Label ID="LblTotalBV" runat="server"  Text='<%#Eval("TOTALBV") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
									   <asp:TemplateField HeaderText="Total SV" Visible="false">
                               <ItemTemplate>
                                     <asp:Label ID="LblTotalSV" runat="server"  Text='<%#Eval("TOTALSV") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                         <asp:LinkButton ID="lbDelete" CommandName="del"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-remove" aria-hidden="true"></i></asp:LinkButton>
                                            
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                           </div>
                              </div>
                    <hr />
                     <div class="row">
                          <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                            <div class="col-md-4">
                             <div class="form-group">
                               
                             </div>
                         </div>


                         </div>
                     <div class="row" style="display:none;">
                          <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                            <div class="col-md-2">
                             <div class="form-group">
                                 <label>CGST %:</label>
                                     <asp:TextBox ID="TxtCGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" onchange="gettotal2();"/>
                             </div>
                         </div>
                           <div class="col-md-2">
                             <div class="form-group">
                                 <label>CGST :</label>
                                     <asp:label ID="TxtCGstAmount" runat="server" CssClass="form-control"  />
                             </div>
                         </div>

                         </div>
                       <div class="row" style="display:none;">
                          <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                            <div class="col-md-2">
                             <div class="form-group">
                                 <label>SGST :</label>
                                     <asp:TextBox ID="TxtSGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" onchange="gettotal2();"/>
                             </div>
                         </div>
                             <div class="col-md-2">
                             <div class="form-group">
                                 <label>CGST :</label>
                                     <asp:label ID="TxtSGstAmount" runat="server" CssClass="form-control"  />
                             </div>
                         </div>

                         </div>
                      <div class="row" style="display:none;">
                          <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                            <div class="col-md-2">
                             <div class="form-group">
                                 <label>IGST :</label>
                                     <asp:TextBox ID="TxtIGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" onchange="gettotal2();"/>
                             </div>
                         </div>
                           <div class="col-md-2">
                             <div class="form-group">
                                 <label>IGST :</label>
                                     <asp:label ID="TxtIGstAmount" runat="server" CssClass="form-control"  />
                             </div>
                         </div>

                         </div>
                       <div class="row" style="display:none;">
                          <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 </div>
                              </div>
                            <div class="col-md-4">
                             <div class="form-group">
                                 <label>Payble Amount :</label>
                                     <asp:Label ID="TxtpaybleAmount" runat="server" CssClass="form-control" />
                                   <asp:HiddenField ID="HDTotal" runat="server" />
                                   <label>Total Purchase :</label>
                                   
                             </div>
                         </div>


                         </div>

      <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Purchase</label>
                                               <asp:TextBox ID="TxtTotalpurchase" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                               </div>
                                        </div>
                                            <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CGST</label>
                                               <asp:TextBox ID="TxtTotalCGST" CssClass="form-control" runat="server" ReadOnly="true" ></asp:TextBox>
                                               </div>
                                        </div>
                                            <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SGST</label>
                                               <asp:TextBox ID="TxtTotalSGST" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                               </div>
                                        </div>
                                              <div class="col-md-2">
                                        <div class="form-group">
                                            <label>IGST</label>
                                               <asp:TextBox ID="TxtTotalIGST" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                               </div>
                                        </div>
                                           <div class="col-md-3" style="display:none;">
                                        <div class="form-group">
                                            <label>Courier Charge</label>
                                               <asp:TextBox ID="TxtShipping" CssClass="form-control" runat="server" Enabled="false" Text="150.00" ></asp:TextBox>
                                               </div>
                                        </div>
                                              <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Amount</label>
                                              <asp:TextBox ID="TxtTotalPrice" runat="server" CssClass="form-control" ReadOnly="true"  Visible="false"/>
                                               <asp:TextBox ID="TxtTotalTotalDP" runat="server" CssClass="form-control" ReadOnly="true" />
                                               <asp:TextBox ID="TXTTTAmount" CssClass="form-control" runat="server" Visible="false" Enabled="false"  ></asp:TextBox>
                                               </div>
                                        </div>
           <div class="col-md-2" style="display:none;">
                                        <div class="form-group">
                                            <label>Total BV</label>
                                               <asp:TextBox ID="TxtTotalBV" CssClass="form-control" runat="server" ReadOnly="true" ></asp:TextBox>
                                               </div>
                                        </div>
          
                                          <div class="col-md-3" style="display:none;">
                                        <div class="form-group">
                                            <label>Wallet Type</label>
                                            <asp:DropDownList ID="DDLSTWallet" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Main Wallet</asp:ListItem>
                                                <asp:ListItem Value="2">Shopping Wallet</asp:ListItem>
                                            </asp:DropDownList>
                                               </div>
                                        </div>
                                          </div>
       <div class="row">
		    <div class="col-md-3" style="display:none;">
                                        <div class="form-group">
                                            <label>Discount</label>
                                               <asp:TextBox ID="TxtDiscount" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>
                                               </div>
                                        </div>
                                    <div class="col-md-3" style="display:none;" >
                                        <div class="form-group">
                                            <label>Discount Amount</label>
                                               <asp:TextBox ID="TxtCash" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtCash_TextChanged" Enabled="false" ></asp:TextBox>
                                               </div>
                                        </div>
		     <div class="col-md-3" style="display:none;">
                                        <div class="form-group">
                                            <label>Total SV</label>
                                               <asp:TextBox ID="TxtTotalSV" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>
                                               </div>
                                        </div>
             <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payble Amount</label>
                                               <asp:TextBox ID="TxtRestAmount" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtRestAmount_TextChanged" Enabled="false" ></asp:TextBox>
                                               </div>
                                        </div>
      <div class="col-md-3" style="Display:none;">
                                        <div class="form-group">
                                            <label>Payment Mode</label>
 <asp:DropDownList ID="DDlstPaymentMode" runat="server" CssClass="form-control">
       <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                <asp:ListItem Value="Draft">Draft</asp:ListItem>
       <asp:ListItem Value="UPI">UPI</asp:ListItem>
                                                <asp:ListItem Value="Paytm">Paytm</asp:ListItem>
                                            </asp:DropDownList>
                                               </div>
                                        </div>
            <div class="col-md-3" style="Display:none;">
                                        <div class="form-group">
                                            <label>Transaction No</label>
   <asp:TextBox ID="TxtTransactionNo" CssClass="form-control" runat="server" ></asp:TextBox>
                                               </div>
                                        </div>
      </div>
        <div class="row">
                <div class="col-md-12">
                      <asp:Button ID="BtnSubmitPurchase" OnClientClick="return validate3();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="BtnSubmitPurchase_Click"/>
                     
                    </div>
          </div>
                    </asp:Panel> 
    

          
          


         

             
         
            
                  <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit Product Details</h4>
                        </div>
                           <div class="modal-body">
                            <div class="row">
                          <div class="col-md-3">
                        <div class="form-group">       
                            <asp:Label ID="Label2" CssClass="form-control" runat="server" Visible="false"></asp:Label>                  
                         <label>Product Code :</label> <asp:TextBox ID="TxtProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>               
                        </div>
                              </div>
                                  <div class="col-md-3">
<div class="form-group">       
    <asp:Label ID="Label3" CssClass="form-control" runat="server" Visible="false"></asp:Label>                  
 <label>SubProduct Code :</label> <asp:TextBox ID="TxtSubProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>               
</div>
      </div>
                                  <div class="col-md-3">
                        <div class="form-group">                         
                        <label>Product Name :</label> <asp:TextBox ID="TxtProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>          
                        </div>
                              </div>
                                
                                          <div class="col-md-3">
<div class="form-group">                         
<label>SubProduct Name :</label> <asp:TextBox ID="TxtSubProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>          
</div>
      </div>
                              
                                </div>
                          <div class="row">
                                        <div class="col-md-3">
<div class="form-group">                         
<label>DP/Pieces :</label>
    <asp:TextBox ID="TxtDPedit" CssClass="form-control" runat="server" onchange="gettotal();"></asp:TextBox> <asp:TextBox ID="TxtAmount" Visible="false" CssClass="form-control" runat="server" onchange="gettotal();"></asp:TextBox>  
</div>
      </div>
                               <div class="col-md-3">
                        <div class="form-group">                         
                       <label>MRP :</label> <asp:TextBox ID="TxtMrp" CssClass="form-control" runat="server" TextMode="Number"  ></asp:TextBox>             
                        </div>
                              </div>
                          <div class="col-md-3">
                        <div class="form-group">                         
                          <label>Quantity :</label> <asp:TextBox ID="TxtQuantity" CssClass="form-control" runat="server" TextMode="Number"  onchange="gettotal();"></asp:TextBox>             
                        </div>
                              </div>
                                  <div class="col-md-3">
                        <div class="form-group">                         
                        <label></label> <asp:Label ID="TxtTotalAmount" CssClass="form-control" runat="server"  Visible="false"></asp:Label>     
                            
                        </div>
                              </div>
                                 
                                
                              
                                </div>
                        
                      
                                           
                      
                      
                      
                        
                    </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn green" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
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
    <script type="text/javascript">
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>
     <script type="text/javascript">
         $('.form_date').datepicker({
             format: 'dd/MM/yyyy',
         }).on('changeDate', function (ev) {
             $(this).datepicker('hide');
         });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/MM/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
</asp:Content>


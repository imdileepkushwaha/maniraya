<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FranchiseePurchaseMaster.aspx.cs" Inherits="FranchiseePurchaseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                alert('Enter Purchase Quantity');
                document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=TxtPurchaseStock.ClientID%>").value == "0") {
                 alert('Enter Purchase Quantity');
                 document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                return false;
            }
           if (document.getElementById("<%=TxtPurchasePrice.ClientID%>").value == "") {
              alert('Enter Price');
              document.getElementById("<%=TxtPurchasePrice.ClientID%>").focus();
              return false;
          }
          return true;
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
            return true;
         }
         function validate3() {
             if (document.getElementById("<%=TxtFranchiseeId.ClientID%>").value == "") {
                 alert('Enter Franchisee ID');
                 document.getElementById("<%=TxtFranchiseeId.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
        <section class="content-header">
      <h1>
    Franchisee Purchase Customer Price
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Product management</a></li>
        <li class="active"> Franchisee Purchase Customer Price </li>
      </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div class="admin-loading-overlay">
                <div class="admin-loading-spinner">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading..." ToolTip="Loading..." />
                    <span>Loading...</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Products to Purchase</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Select franchisee and product details, then add items to the purchase cart. Customer price will be applied for this purchase.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-user"></i> Franchisee Details</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtFranchiseeId.ClientID %>">Franchisee ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-card-o"></i></span>
                                                <asp:TextBox ID="TxtFranchiseeId" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFranchiseeId_TextChanged" placeholder="Enter franchisee ID" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtFRanchiseeName.ClientID %>">Franchisee Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user-circle-o"></i></span>
                                                <asp:TextBox ID="TxtFRanchiseeName" runat="server" CssClass="form-control" Enabled="false" placeholder="Franchisee name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= DDLstProduct.ClientID %>">Product</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cube"></i></span>
                                                <asp:DropDownList ID="DDLstProduct" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLstProduct_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <asp:TextBox ID="TxtImage" runat="server" Enabled="false" CssClass="form-control" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-shopping-cart"></i> Product &amp; Quantity</h5>
                                <div class="row">
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtAvailableStock.ClientID %>">Available Limit</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-database"></i></span>
                                                <asp:TextBox ID="TxtAvailableStock" runat="server" Enabled="false" CssClass="form-control" />
                                            </div>
                                            <asp:HiddenField ID="HDGST" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtPurchaseStock.ClientID %>">Purchase Quantity</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-sort-numeric-asc"></i></span>
                                                <asp:TextBox ID="TxtPurchaseStock" runat="server" CssClass="form-control" placeholder="Enter quantity" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtPurchasePrice.ClientID %>"><asp:Label ID="LblType" runat="server" Text="Customer Price"></asp:Label></label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtDP" runat="server" CssClass="form-control" Enabled="false" />
                                                <asp:TextBox ID="TxtPurchasePrice" runat="server" CssClass="form-control" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtPurchaseMRP.ClientID %>">MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                                <asp:TextBox ID="TxtPurchaseMRP" runat="server" CssClass="form-control" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtGST.ClientID %>">GST (%)</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-percent"></i></span>
                                                <asp:TextBox ID="TxtGST" runat="server" CssClass="form-control" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtBV.ClientID %>">BV</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                                <asp:TextBox ID="TxtBV" runat="server" CssClass="form-control" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Add to Cart" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Clear Cart" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="PnlDt" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Purchase Cart</h3>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px" CssClass="admin-grid-thumb" />
                                                    <asp:Label ID="LblProductImageG" runat="server" Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductCodeG" runat="server" Text='<%#Eval("ProductId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductNameG" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CP/Pieces">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProductAmountG" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                    <asp:Label ID="LblDP" runat="server" Text='<%#Eval("DP") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:Label ID="LBlMrp" runat="server" Text='<%#Eval("MRP") %>'></asp:Label>
                                                    <asp:Label ID="LblBv" runat="server" Text='<%#Eval("BV") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="LblStock" runat="server" Text='<%#Eval("STOCK") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="label1" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPurchaseAmount" runat="server" Text='<%#Eval("PurchaseAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCGST" runat="server" Text='<%#Eval("CGST") %>'></asp:Label>
                                                    <asp:Label ID="LblGSTPER" runat="server" Text='<%#Eval("GSTPER") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSGST" runat="server" Text='<%#Eval("SGST") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblIGST" runat="server" Text='<%#Eval("IGST") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="label1" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                    <asp:Label ID="LblTotalDP" runat="server" Text='<%#Eval("TOTALDP") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total BV">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTotalBV" runat="server" Text='<%#Eval("TOTALBV") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" CssClass="admin-grid-edit-btn" ToolTip="Edit item"><i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbDelete" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" CssClass="admin-grid-delete-btn" ToolTip="Remove item"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="admin-grid-action-cell" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Order Summary &amp; Payment</h3>
                            </div>
                            <div class="box-body admin-product-form">
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-calculator"></i> Order Totals</h5>
                                    <div class="admin-purchase-summary-grid">
                                        <div class="form-group">
                                            <label for="<%= TxtTotalpurchase.ClientID %>">Total Purchase</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtTotalpurchase" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= TxtTotalCGST.ClientID %>">CGST</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtTotalCGST" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= TxtTotalSGST.ClientID %>">SGST</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtTotalSGST" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= TxtTotalIGST.ClientID %>">IGST</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtTotalIGST" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= TxtTotalPrice.ClientID %>">Total Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="TxtTotalPrice" runat="server" CssClass="form-control" ReadOnly="true" />
                                                <asp:TextBox ID="TxtTotalTotalDP" runat="server" CssClass="form-control" ReadOnly="true" Visible="false"/>
                                                <asp:TextBox ID="TXTTTAmount" CssClass="form-control" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= TxtTotalBV.ClientID %>">Total BV</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-line-chart"></i></span>
                                                <asp:TextBox ID="TxtTotalBV" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-credit-card"></i> Payment Details</h5>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtRestAmount.ClientID %>">Payable Amount</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                    <asp:TextBox ID="TxtRestAmount" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtRestAmount_TextChanged" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= DDlstPaymentMode.ClientID %>">Payment Mode</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-money"></i></span>
                                                    <asp:DropDownList ID="DDlstPaymentMode" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                        <asp:ListItem Value="Draft">Draft</asp:ListItem>
                                                        <asp:ListItem Value="UPI">UPI</asp:ListItem>
                                                        <asp:ListItem Value="Paytm">Paytm</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <label for="<%= TxtTransactionNo.ClientID %>">Transaction No</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                    <asp:TextBox ID="TxtTransactionNo" CssClass="form-control" runat="server" placeholder="Enter transaction / cheque no." />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:TextBox ID="TxtShipping" CssClass="form-control" runat="server" Enabled="false" Text="150.00" Style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="TxtCash" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtCash_TextChanged" Enabled="false" Style="display:none;"></asp:TextBox>
                                <asp:DropDownList ID="DDLSTWallet" runat="server" CssClass="form-control" Style="display:none;">
                                    <asp:ListItem Value="1">Main Wallet</asp:ListItem>
                                    <asp:ListItem Value="2">Shopping Wallet</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TxtCGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" Style="display:none;" />
                                <asp:TextBox ID="TxtSGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" Style="display:none;" />
                                <asp:TextBox ID="TxtIGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" Style="display:none;" />
                                <asp:Label ID="TxtCGstAmount" runat="server" CssClass="form-control" Style="display:none;" />
                                <asp:Label ID="TxtSGstAmount" runat="server" CssClass="form-control" Style="display:none;" />
                                <asp:Label ID="TxtIGstAmount" runat="server" CssClass="form-control" Style="display:none;" />
                                <asp:Label ID="TxtpaybleAmount" runat="server" CssClass="form-control" Style="display:none;" />
                                <asp:HiddenField ID="HDTotal" runat="server" />
                            </div>
                            <div class="box-footer">
                                <asp:Button ID="BtnSubmitPurchase" OnClientClick="return validate3();" CssClass="btn btn-primary" runat="server" Text="Submit Purchase" OnClick="BtnSubmitPurchase_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div id="myModal" class="modal fade admin-modal-scrollable" tabindex="-1" role="dialog" aria-labelledby="purchaseEditModalTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg admin-purchase-edit-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="purchaseEditModalTitle">Edit Cart Item</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <asp:Label ID="Label2" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                            <div class="row">
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtProductCode.ClientID %>">Product Code</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                            <asp:TextBox ID="TxtProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtProductName.ClientID %>">Product Name</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-cube"></i></span>
                                            <asp:TextBox ID="TxtProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtDPedit.ClientID %>">CP/Pieces</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                            <asp:TextBox ID="TxtDPedit" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="TxtAmount" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtMrp.ClientID %>">MRP</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                            <asp:TextBox ID="TxtMrp" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtQuantity.ClientID %>">Quantity</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-sort-numeric-asc"></i></span>
                                            <asp:TextBox ID="TxtQuantity" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="TxtTotalAmount" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Item" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
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
            if (window.showAdminModal) {
                showAdminModal('myModal');
            } else {
                $('#myModal').modal({ backdrop: 'static', keyboard: false });
            }
        }
        function Closepopup() {
            if (window.closeAdminModal) {
                closeAdminModal('myModal');
            } else {
                $('#myModal').modal('hide');
                $('body').removeClass('modal-open');
                $('body').css('padding-right', '0');
                $('.modal-backdrop').remove();
            }
        }
    </script>
</asp:Content>

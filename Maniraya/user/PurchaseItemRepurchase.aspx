<%@ Page Title="Purchase Item" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseItemRepurchase.aspx.cs" Inherits="user_PurchaseItemRepurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/user-profile.css?v=8" rel="stylesheet" />
    <link href="assets/css/box-modern.css?v=4" rel="stylesheet" />
    <link href="assets/css/repurchase-modern.css?v=4" rel="stylesheet" />
    <style type="text/css">
        .product_item { display: block; background: #fff; border: 1px solid #e8ecf1; padding: 12px; border-radius: 12px; position: relative; overflow: hidden; }
        .product_sale { position: absolute; z-index: 2; right: -34px; transform: rotate(45deg); font-size: 12px; margin-top: 18px; }
        .product_sale p { margin: 0; color: #fff; background: #e86b5a; padding: 3px 34px; }
        .product_image { position: relative; overflow: hidden; margin-bottom: 10px; }
        .product_image img { max-width: 100%; height: auto; }
        .product_values { width: 100%; }
        .product_desc .ex3 { width: 100%; max-height: 110px; overflow: auto; color: #64748b; }
        .col-item { height: 100%; }
    </style>
    <script type="text/javascript">
        function gettotal() {
            var Quantity = 0, Amount = 0;
            var qtyEl = document.getElementById("<%=TxtQuantity.ClientID%>");
            var amtEl = document.getElementById("<%=TxtAmount.ClientID%>");
            var totalEl = document.getElementById("<%=TxtTotalAmount.ClientID%>");
            if (qtyEl && qtyEl.value != "") Quantity = qtyEl.value;
            if (amtEl && amtEl.value != "") Amount = amtEl.value;
            if (totalEl) totalEl.innerText = Quantity * Amount;
        }

        function validate() {
            var qty = document.getElementById("<%=TxtQuantity.ClientID%>");
            if (!qty || qty.value == "") {
                alert('Enter Quantity');
                if (qty) qty.focus();
                return false;
            }
            return true;
        }

        function validate2() {
            var mode = document.getElementById("<%=ddmode.ClientID%>");
            var txn = document.getElementById("<%=TxtTransactionId.ClientID%>");
            var uploadMsg = document.getElementById("<%=TextBox1.ClientID%>");
            var fileName = document.getElementById("<%=HDFilename.ClientID%>");
            var state = document.getElementById("<%=ddstate.ClientID%>");
            var city = document.getElementById("<%=ddcity.ClientID%>");

            if (state && state.value == "0") {
                alert('Select State');
                state.focus();
                return false;
            }
            if (city && city.value == "0") {
                alert('Select City');
                city.focus();
                return false;
            }
            if (!mode || mode.value == "Select") {
                alert('Select Payment Mode');
                if (mode) mode.focus();
                return false;
            }
            if (!txn || txn.value.replace(/^\s+|\s+$/g, '') == "") {
                alert('Enter Transaction Id');
                if (txn) txn.focus();
                return false;
            }
            if ((!uploadMsg || uploadMsg.value.replace(/^\s+|\s+$/g, '') == "") &&
                (!fileName || fileName.value.replace(/^\s+|\s+$/g, '') == "")) {
                alert('Upload Payment Slip');
                if (uploadMsg) uploadMsg.focus();
                return false;
            }
            return true;
        }

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GDoffer.ClientID %>');
            if (!gv) return;
            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
                if (radioBtn.length && radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <asp:HiddenField ID="HDPlantype" runat="server" />
        <asp:HiddenField ID="HDPlanId" runat="server" />
        <asp:HiddenField ID="HDIsdistributer" runat="server" />
        <asp:HiddenField ID="HdFranchiseeid" runat="server" />
        <h1>Purchase Item</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="FranchiseeSearchNew.aspx">My Repurchase</a></li>
            <li class="active">Purchase Item</li>
        </ol>
        <div style="display:none;">
            <asp:Label ID="Lblbalance" runat="server" Text="Balance"></asp:Label>
            <asp:Label ID="LblUtility" runat="server" Text="Balance"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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
            <div class="profile-page repurchase-page">
                <div class="profile-hero">
                    <div class="profile-hero-avatar" aria-hidden="true"><i class="fa fa-shopping-bag"></i></div>
                    <div class="profile-hero-info">
                        <h2>Purchase Item</h2>
                        <p class="profile-hero-meta">Add products to cart, then checkout with address and payment.</p>
                    </div>
                    <div class="profile-hero-actions">
                        <a href="UserProductCart.aspx" class="profile-btn profile-btn-primary">
                            <i class="fa fa-shopping-cart"></i> Cart (<asp:Literal ID="litCartCount" runat="server" Text="0" />)
                        </a>
                        <a id="lnksearch" runat="server" class="profile-btn profile-btn-outline" href="FranchiseeSearchNew.aspx">
                            <i class="fa fa-arrow-left"></i> Back
                        </a>
                    </div>
                </div>

                <asp:HiddenField ID="HdFiled" runat="server" />
                <asp:HiddenField ID="HDFilename" runat="server" />

                <asp:Panel ID="PurchasePanel" runat="server" Visible="false" CssClass="rp-cart-panel">
                    <div class="box box-primary">
                        <div class="box-header with-border box-header-enhanced box-header-tone-0">
                            <div class="box-header-main">
                                <span class="box-header-icon" aria-hidden="true"><i class="fa fa-shopping-cart"></i></span>
                                <div class="box-header-text">
                                    <h3 class="box-title">Cart Items</h3>
                                    <p class="box-subtitle">Review quantities, tax and totals before payment</p>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group repurchase-table-wrap table-responsive">

                                            <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px" />
                                                            <asp:Label ID="LblProductImageG" runat="server" Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Code">
                                                        <ItemTemplate>
                                                          <%--  <asp:Label ID="LblCatId" runat="server" Text='<%#Eval("CatID") %>'></asp:Label>--%>
                                                            <asp:Label ID="LblProductCodeG" runat="server" Text='<%#Eval("ProductId") %>'></asp:Label>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProductNameG" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                          
                                                           <asp:Label ID="LBlMrp" runat="server" Text='<%#Eval("MRP") %>' ></asp:Label>
                                                            <asp:Label ID="LblOfferProduct" runat="server" Text='<%#Eval("OFFERPRODUCTID") %>'  Visible="false"></asp:Label>
                                                          
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="SV">
                                                        <ItemTemplate>                                                        
                                                            <asp:Label ID="LblBv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DP/Peices" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDPAmountG" runat="server" Text='<%#Eval("DP") %>'></asp:Label>
                                                             
                                                              <asp:Label ID="LblStock" runat="server" Text='<%#Eval("STOCK") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Amount/Peices">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProductAmountG" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                             
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
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
                                                            <asp:Label ID="label1" runat="server" Text="Total : "></asp:Label>
                                                        </FooterTemplate>
                           </asp:TemplateField>  
                                                      <asp:TemplateField HeaderText="Calculate DP" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmountDP" runat="server" Text='<%#Eval("TotalDP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblGrandTotalDP" runat="server" Text=""></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculate Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total SV">
                               <ItemTemplate>
                                    <asp:Label ID="LblTotalBv" runat="server"  Text='<%#Eval("TOTALBV") %>' ></asp:Label>
                               </ItemTemplate>
															 <FooterTemplate>
                                                            <asp:Label ID="lblsvtotal" runat="server" Text=""></asp:Label>
                                                        </FooterTemplate>
                           </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i>EDIT</asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDelete" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-remove" aria-hidden="true"></i>DELETE</asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                                <div class="row rp-summary-grid">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total SV</label>
                                            <asp:TextBox ID="TxtTotalSV" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Purchase</label>
                                            <asp:TextBox ID="TxtTotalpurchase" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CGST</label>
                                            <asp:TextBox ID="TxtTotalCGST" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SGST</label>
                                            <asp:TextBox ID="TxtTotalSGST" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>IGST</label>
                                            <asp:TextBox ID="TxtTotalIGST" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Shipping Charge</label>
                                            <asp:TextBox ID="TxtShipping" CssClass="form-control" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Amount</label>
                                            <asp:TextBox ID="TXTTTAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="TXTTTDP" CssClass="form-control" runat="server" Enabled="false" Visible="false"></asp:TextBox>
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

                            <h4 class="box-title" style="margin:8px 0 14px;font-size:1rem;font-weight:700;">Shipping Address</h4>
                            <div class="rp-address-toggle">
                                <div class="form-group">
                                    <asp:RadioButton ID="RDBtnTRecharge" runat="server" Text="Profile Address" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnTRecharge_CheckedChanged" />
                                </div>
                                <div class="form-group">
                                    <asp:RadioButton ID="RdBtnUtility" runat="server" Text="Shipping Address" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnUtility_CheckedChanged" />
                                </div>
                            </div>
						 <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Address :</label>
                                        <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select State</label>
                                        <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                       <label>Select City :</label>
                                        <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                               <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                      <label>Other</label>
                                        <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server"></asp:TextBox>
                                    
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                          <label>Pincode :</label>
                                        <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
						               <div class="row" style="display:none;">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Select Deposit Account :</label>
                                   <asp:DropDownList ID="ddbankaccountno" AutoPostBack="true" OnSelectedIndexChanged="ddbankaccountno_SelectedIndexChanged"  CssClass="form-control"  runat="server">
                                            <asp:ListItem Value="0">Select Account No</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Deposit Account No :</label>
                                <asp:TextBox ID="txtdepositaccountno" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                          <div class="row" style="display:none;">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Deposit Bank :</label>
                                  <asp:TextBox ID="txtdepositbank" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>IFSC Code :</label>
                                 <asp:TextBox ID="txtifsccode" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row" style="display:none;">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Account Holder Name :</label>
                                 <asp:TextBox ID="txtaccountholdername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                        
                  
                            
                             <div class="col-md-6">
                             <div class="form-group">
                                 <label>QR Code :</label> <br>
                                <asp:Image ID="QR" runat="server" Width="200px" Height="200px"  />
                             </div>
                         </div>
</div>
                           <div class="row" >
                         <div class="col-md-6">
                             <div class="form-group">
                                    <label>Deposit Mode :</label>
                                   <asp:DropDownList ID="ddmode" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Select">Select </asp:ListItem>                                              
                                                <asp:ListItem Value="RTGS">RTGS</asp:ListItem>
                                                <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                                <asp:ListItem Value="IMPS">IMPS</asp:ListItem>
                                          <asp:ListItem Value="UPI">UPI</asp:ListItem>
                                       
                                            </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                   <label>TransactionId :</label>
                                 <asp:TextBox ID="TxtTransactionId"  runat="server"  CssClass="form-control" />
                             </div>
                         </div>
                                  
                     </div>
                        	 <div class="row">
                                   <div class="col-md-6">
                             <div class="form-group">
                                 <label>Upload Receipt Image</label>
     <asp:FileUpload ID="ImageUpload" runat="server" />  
                               <input id="BTNUpload" type="button" value="Upload" onclick="Uploadimageofsign(); return false;" style="display:none;"  />  
                                 <asp:Button ID="btnuploademo" CssClass="btn btn-danger" runat="server" Text="Upload" OnClientClick="return Uploadimageofsign();" OnClick="btnuploademo_Click" style="margin-top:8px;" />
                             </div>
							 </div>
							  <div class="col-md-3">
                                      <div class="form-group">
                                          <asp:Label ID="LblMsg" runat="server" ForeColor="#16a34a" Text=""></asp:Label>
                                       <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" ForeColor="#16a34a" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                         </div>
                         </div>
							  <div class="col-md-3">
                                      <div class="form-group">
                                         <div class="rp-upload-preview">
                                         <asp:Image ID="ImageButton1" runat="server" Width="100px" Height="100px" />
                                         </div>
                                         </div>
                         </div>
                     </div>
                        </div>
                            <div class="box-footer">
                                <asp:HiddenField ID="HDTotal" runat="server" />
                                <div class="rp-footer-actions">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClientClick="return validate2();" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Remove All" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                    </div>
                    </asp:Panel>
                    <div class="box box-primary">
                        <div class="box-header with-border box-header-enhanced box-header-tone-0">
                            <div class="box-header-main">
                                <span class="box-header-icon" aria-hidden="true"><i class="fa fa-boxes"></i></span>
                                <div class="box-header-text">
                                    <h3 class="box-title">Products</h3>
                                    <p class="box-subtitle">Browse franchisee products and add to cart</p>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row rp-product-grid">
                                <asp:Repeater ID="dlCustomers" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-4 col-sm-6">
                                            <div class="col-item rp-product-card">
                                                <div class="photo">
                                                    <div class="rp-product-media">
                                                        <img src='<%# Eval("Image") %>' class="img-responsive" alt="product" />
                                                    </div>
                                                    <asp:Label ID="lblim" runat="server" Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                                                    <%# HasDiscount(Eval("MRP"), Eval("Amount")) ? "<span class=\"rp-product-badge\">Save " + GetDiscountPercent(Eval("MRP"), Eval("Amount")) + "%</span>" : "" %>
                                                </div>
                                                <div class="info">
                                                    <div class="price">
                                                        <p class="rp-product-id">ID <asp:Label ID="lblid" runat="server" Text='<%#Eval("ProductId") %>'></asp:Label></p>
                                                        <h5 class="rp-product-title"><asp:Label ID="lblstatename" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label></h5>
                                                        <div class="rp-product-price-row">
                                                            <span class="rp-product-amount">&#8377;<asp:Label ID="lblstatename1" runat="server" Text='<%#Eval("Amount") %>'></asp:Label></span>
                                                            <span class="rp-product-mrp">MRP &#8377;<asp:Label ID="Lblmrp" runat="server" Text='<%#Eval("MRP") %>'></asp:Label></span>
                                                        </div>
                                                        <div class="rp-product-stats">
                                                            <span class="rp-stat"><i class="fa fa-star" aria-hidden="true"></i> BV <strong><asp:Label ID="LblBV" runat="server" Text='<%#Eval("BV") %>'></asp:Label></strong></span>
                                                        </div>
                                                        <asp:HiddenField ID="HDCategory" runat="server" Value='<%#Eval("CategoryID") %>' />
                                                        <asp:Label ID="LblDPDP" Visible="False" runat="server" Text='<%#Eval("DP") %>'></asp:Label>
                                                        <asp:HiddenField ID="HDBV" runat="server" Value='<%#Eval("BV") %>' />
                                                    </div>
                                                    <div class="rp-product-actions">
                                                        <asp:LinkButton ID="lnkph" runat="server" CssClass="btn btnBuyNow rp-btn-view" CommandName="photolarge" CommandArgument='<%# Eval("ProductId") %>'>
                                                            <i class="fa fa-eye"></i> View
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btnBuyNow rp-btn-cart" CommandName="BuyProduct" CommandArgument='<%# Eval("ProductId") %>'>
                                                            <i class="fa fa-shopping-cart"></i> Add to cart
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>











                        </div>
                        <div class="box-footer">
                            <div class="rp-pager">
                                <div class="rp-pager-info">
                                    <asp:Label ID="LblRecordCount" runat="server" Text=""></asp:Label>
                                </div>
                                <asp:Panel ID="pnlPager" runat="server" CssClass="rp-pager-nav" Visible="false">
                                    <ul class="pagination rp-pagination">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <li class='paginate_button <%# Convert.ToBoolean(Eval("IsActive")) ? "active" : "" %>'>
                                                    <asp:LinkButton ID="lnkPage" runat="server"
                                                        Text='<%# Eval("Text") %>'
                                                        CommandArgument='<%# Eval("Value") %>'
                                                        CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "page_disabled is-active" : "page_enabled" %>'
                                                        OnClick="Page_Changed"
                                                        OnClientClick='<%# Convert.ToBoolean(Eval("IsActive")) ? "return false;" : "" %>'
                                                        Enabled='<%# Convert.ToBoolean(Eval("Enabled")) %>'></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                 
                       <div id="myModal" class="modal fade rp-modal rp-product-modal">
                           <div class="modal-dialog modal-lg">
                               <div class="modal-content">
                                   <div class="modal-header">
                                       <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                       <h4 class="modal-title"><i class="fa fa-cube" aria-hidden="true"></i> Product Details</h4>
                                       <p class="rp-modal-sub">Product ID <asp:Label ID="LblProductCode" runat="server" Text=""></asp:Label></p>
                                   </div>
                                   <div class="modal-body">
                                       <asp:HiddenField ID="HdCatId" runat="server" />
                                       <asp:HiddenField ID="HdBuisnessVolume" runat="server" />

                                       <div class="rp-detail-layout">
                                           <div class="rp-detail-gallery">
                                               <span class="rp-detail-category">
                                                   <asp:Label ID="LblcategoryName123" runat="server" Text=""></asp:Label>
                                               </span>
                                               <div id="myCarousel" class="carousel slide rp-detail-carousel" data-ride="carousel">
                                                   <ol class="carousel-indicators">
                                                       <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                       <li data-target="#myCarousel" data-slide-to="1"></li>
                                                       <li data-target="#myCarousel" data-slide-to="2"></li>
                                                   </ol>
                                                   <div class="carousel-inner">
                                                       <div class="item active">
                                                           <asp:Image ID="Image2" runat="server" CssClass="rp-detail-image" />
                                                       </div>
                                                       <div class="item">
                                                           <asp:Image ID="Image3" runat="server" CssClass="rp-detail-image" />
                                                       </div>
                                                       <div class="item">
                                                           <asp:Image ID="Image4" runat="server" CssClass="rp-detail-image" />
                                                       </div>
                                                   </div>
                                                   <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                                       <span class="glyphicon glyphicon-chevron-left"></span>
                                                       <span class="sr-only">Previous</span>
                                                   </a>
                                                   <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                                       <span class="glyphicon glyphicon-chevron-right"></span>
                                                       <span class="sr-only">Next</span>
                                                   </a>
                                               </div>
                                           </div>

                                           <div class="rp-detail-info">
                                               <h3 class="rp-detail-title">
                                                   <asp:Label ID="LblProductName" runat="server" Text=""></asp:Label>
                                               </h3>

                                               <div class="rp-detail-price-block">
                                                   <div class="rp-detail-amount">
                                                       <span class="rp-detail-label">Amount</span>
                                                       <strong>&#8377;<asp:Label ID="LblAmount" runat="server" Text=""></asp:Label></strong>
                                                   </div>
                                                   <div class="rp-detail-mrp">
                                                       <span class="rp-detail-label">MRP</span>
                                                       <span>&#8377;<asp:Label ID="LblMRP" runat="server" Text=""></asp:Label></span>
                                                   </div>
                                               </div>

                                               <div class="rp-detail-chips">
                                                   <span class="rp-detail-chip">
                                                       <i class="fa fa-star" aria-hidden="true"></i>
                                                       BV <strong><asp:Label ID="LblBv" runat="server" Text=""></asp:Label></strong>
                                                   </span>
                                                   <span class="rp-detail-chip is-muted" style="display:none;">
                                                       DP <strong><asp:Label ID="LblDP" runat="server" Text=""></asp:Label></strong>
                                                   </span>
                                               </div>

                                               <div class="rp-detail-desc">
                                                   <h5>Description</h5>
                                                   <div class="rp-detail-desc-body">
                                                       <asp:Label ID="LblDescription" runat="server" Text=""></asp:Label>
                                                   </div>
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                                   <div class="modal-footer">
                                       <button type="button" class="btn btn-default rp-modal-close" data-dismiss="modal">Close</button>
                                   </div>
                               </div>
                           </div>
                       </div>



                    <div id="Div1" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Buy Product</h4>

                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="TxtImage" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="LblGST" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                                <label>Product Code :</label>
                                                <asp:TextBox ID="TxtProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Product Name :</label>
                                                <asp:TextBox ID="TxtProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                           <div class="form-group">
                                                <label>MRP :</label>
                                                <asp:TextBox ID="TxtMRP" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                
                                               </div>
                                        </div>


                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Amount :</label>
                                                <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" ReadOnly="true" ></asp:TextBox>
                                                      <asp:TextBox ID="TxtDP" CssClass="form-control" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                             <div class="form-group">
                                                <label>Quantity :</label>
                                                <asp:TextBox ID="TxtQuantity" CssClass="form-control" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="TxtQuantity_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>

                                          <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Total SV :</label>
                                                <asp:Label ID="Txtbv" CssClass="form-control" runat="server"  Visible="false"></asp:Label>
                                                <asp:Label ID="TxtTotalSV2" CssClass="form-control" runat="server" ></asp:Label>
                                                                        </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Total Amount :</label>
                                                <asp:Label ID="TxtTotalAmount" CssClass="form-control" runat="server" ></asp:Label>
                                                   <asp:Label ID="TxtTotalDP" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>


                                    </div>



                                     <div class="row">
                                        <div class="col-md-12">
                                            Offer Product
                                              <asp:GridView ID="GDoffer" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                                                <Columns>
                                                 
                                                                <asp:TemplateField HeaderText="Select">
                <ItemTemplate><asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" /></ItemTemplate>
            </asp:TemplateField>

                                                     
                                                      <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProductname" runat="server" Text='<%#Eval("productname") %>'></asp:Label>
                                                              <asp:Label ID="LblProductID" runat="server" Text='<%#Eval("OfferProductID") %>' Visible="false"></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Offer Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblOfferQuantity" runat="server" Text='<%#Eval("OfferQuantity") %>'></asp:Label>
                                                             
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Offer Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblOfferAmount" runat="server" Text='<%#Eval("OfferAmount") %>'></asp:Label>
                                                             
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>
                                            </div>
                                         </div>



                                </div>
                                <div class="modal-footer">

                                    <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="BtnAdd_Click" />
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function showModal1() {
            $('#Div1').modal({ backdrop: 'static', keyboard: false });
        }
        function Closepopup1() {
            $('#Div1').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }

        function Uploadimageofsign() {
            var fileUpload = document.getElementById('<%=ImageUpload.ClientID %>');
            if (!fileUpload || !fileUpload.files || fileUpload.files.length === 0) {
                alert('Please select a receipt image to upload');
                return false;
            }

            var file = fileUpload.files[0];
            var safeName = ($('#<%=HdFiled.ClientID%>').val() || '') + String(file.name || '').replace(/\s/g, '');
            $('#<%=HDFilename.ClientID%>').val(safeName);

            var data = new FormData();
            data.append(safeName, file);

            $.ajax({
                url: "UploadImage.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                async: false,
                success: function () {
                    var msg = document.getElementById("<%=TextBox1.ClientID%>");
                    if (msg) msg.value = "File Upload successfullly";
                },
                error: function () {
                    alert('Receipt upload failed. Please try again.');
                }
            });

            var msgBox = document.getElementById("<%=TextBox1.ClientID%>");
            if (msgBox && !msgBox.value) {
                msgBox.value = "File Upload successfullly";
            }
            return true;
        }
    </script>
</asp:Content>
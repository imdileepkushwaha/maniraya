<%@ Page Title="Add Saving Product Stock" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductStockAdd.aspx.cs" Inherits="admin_ProductAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
           
           if (document.getElementById("<%=ddproduct.ClientID%>").value == "0") {
                alert('Select Product ');
                document.getElementById("<%=ddproduct.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtdp.ClientID%>").value == "") {
                alert('Enter DP');
                document.getElementById("<%=txtdp.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtmrp.ClientID%>").value == "") {
                alert('Enter MRP');
                document.getElementById("<%=txtmrp.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=txtquantity.ClientID%>").value == "") {
                alert('Enter MRP');
                document.getElementById("<%=txtquantity.ClientID%>").focus();
                return false;
            }
            return true;
        }

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Saving Product Stock</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#"> Saving  Product</a></li>
            <li class="active">Add Saving Product Stock</li>
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
                            <h3 class="box-title">Add  Saving Product Stock</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Fill in  details</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-tags"></i>Stock Details</h5>
                                <div class="row">
                                  
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>Select Product Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                             <asp:DropDownList ID="ddproduct" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddproduct_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Product</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>MRP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtmrp" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>DP</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                <asp:TextBox ID="txtdp" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter product name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <div class="form-group">
                                            <label>Quantity</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-shopping-basket"></i></span>
                                               <asp:TextBox ID="txtquantity" CssClass="form-control" runat="server" placeholder="Enter Quantity" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                           

                           

                          
                           

                           
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="return validate();" runat="server" Text="Add Stock" OnClick="btnSubmit_Click1" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
 
</asp:Content>

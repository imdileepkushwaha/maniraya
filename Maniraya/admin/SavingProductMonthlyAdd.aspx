<%@ Page Title="Add Monthly Saving Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductMonthlyAdd.aspx.cs" Inherits="admin_SavingProductMonthlyAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=77" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add Monthly Saving Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Add Monthly Saving Product</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-plus-circle"></i> Add Product</h3>
                            </div>
                            <div class="box-body admin-search-form">
                                <p class="admin-report-intro">
                                    One product per month only. Entry Date is fixed to the 1st of the month (starts from 01/09/2026).
                                    No edit/delete from this page.
                                </p>
                                <div class="admin-form-section admin-form-section-last">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="<%= txtEntryDate.ClientID %>">Entry Date <span class="text-danger">*</span></label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtEntryDate" CssClass="form-control" runat="server" ReadOnly="true" />
                                                </div>
                                                <asp:HiddenField ID="hdnEntryDate" runat="server" />
                                                <small class="text-muted">Fixed to month start. After save, next month’s 1st date will show.</small>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="<%= txtProductName.ClientID %>">Product Name <span class="text-danger">*</span></label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server" placeholder="Enter product name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="<%= txtMrp.ClientID %>">MRP <span class="text-danger">*</span></label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                    <asp:TextBox ID="txtMrp" CssClass="form-control" runat="server" placeholder="0.00" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="<%= txtDp.ClientID %>">DP <span class="text-danger">*</span></label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                    <asp:TextBox ID="txtDp" CssClass="form-control" runat="server" placeholder="0.00" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="<%= txtGst.ClientID %>">GST(%) <span class="text-danger">*</span></label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-percent"></i></span>
                                                    <asp:TextBox ID="txtGst" CssClass="form-control" runat="server" placeholder="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="<%= txtHsnCode.ClientID %>">HSN Code</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-barcode"></i></span>
                                                    <asp:TextBox ID="txtHsnCode" CssClass="form-control" runat="server" placeholder="Enter HSN" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="<%= fuImage.ClientID %>">Product Image</label>
                                                <asp:FileUpload ID="fuImage" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer admin-report-footer">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Save Product"
                                    OnClick="btnSubmit_Click" OnClientClick="return validateMonthlyProduct();" />
                                <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset"
                                    OnClick="btnReset_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-7">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-list"></i> Saved Products</h3>
                            </div>
                            <div class="box-body table-responsive">
                                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-hover dataTable" Width="100%"
                                    EmptyDataText="No products found.">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Id" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                        <asp:BoundField DataField="MRP" HeaderText="MRP" DataFormatString="{0:0.##}" />
                                        <asp:BoundField DataField="DP" HeaderText="DP" DataFormatString="{0:0.##}" />
                                        <asp:BoundField DataField="GST" HeaderText="GST%" DataFormatString="{0:0.##}" />
                                        <asp:BoundField DataField="HSNCode" HeaderText="HSN" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                    </Columns>
                                </asp:GridView>
                            </div>
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

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function validateMonthlyProduct() {
            var entry = document.getElementById('<%= txtEntryDate.ClientID %>');
            var name = document.getElementById('<%= txtProductName.ClientID %>');
            var mrp = document.getElementById('<%= txtMrp.ClientID %>');
            var dp = document.getElementById('<%= txtDp.ClientID %>');
            var gst = document.getElementById('<%= txtGst.ClientID %>');

            if (!entry || !entry.value) {
                alert('Entry Date is required.');
                return false;
            }
            if (!name || !name.value.trim()) {
                alert('Enter Product Name.');
                if (name) name.focus();
                return false;
            }
            if (!mrp || !mrp.value.trim()) {
                alert('Enter MRP.');
                if (mrp) mrp.focus();
                return false;
            }
            if (!dp || !dp.value.trim()) {
                alert('Enter DP.');
                if (dp) dp.focus();
                return false;
            }
            if (!gst || !gst.value.trim()) {
                alert('Enter GST.');
                if (gst) gst.focus();
                return false;
            }
            return confirm('Save product for Entry Date ' + entry.value + '?');
        }
    </script>
</asp:Content>

<%@ Page Title="Assign Saving Product" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SavingProductAssign.aspx.cs" Inherits="admin_ProductAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=79" />
    <script type="text/javascript">
        function validateAssign() {
            var product = document.getElementById("<%= ddproduct.ClientID %>");
            var installment = document.getElementById("<%= ddinstallment.ClientID %>");
            if (!product || product.value === "0") {
                alert("Select Product");
                if (product) product.focus();
                return false;
            }
            if (!installment || installment.value === "0") {
                alert("Select Installment (1 to 18)");
                if (installment) installment.focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Assign Saving Product</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Saving Product</a></li>
            <li class="active">Assign Saving Product</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="admin-report-page">
                <div class="row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-link"></i> Assign to Installment</h3>
                            </div>
                            <div class="box-body admin-product-form">
                                <p class="admin-product-intro">
                                    Product choose karo — MRP aur DP auto aa jayenge. Phir installment 1 se 18 select karke assign karo.
                                    Naya user join kare to <strong>installment 1</strong> ka product milega.
                                </p>
                                <div class="admin-form-section">
                                    <h5 class="admin-form-section-title"><i class="fa fa-tags"></i> Product</h5>
                                    <div class="form-group">
                                        <label>Select Product</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-cart-plus"></i></span>
                                            <asp:DropDownList ID="ddproduct" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddproduct_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Product</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>MRP</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                    <asp:TextBox ID="txtmrp" CssClass="form-control" Enabled="false" runat="server" placeholder="Auto" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>DP</label>
                                                <div class="admin-input-group">
                                                    <span class="admin-input-icon"><i class="fa fa-rupee"></i></span>
                                                    <asp:TextBox ID="txtdp" CssClass="form-control" Enabled="false" runat="server" placeholder="Auto" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlInstallment" runat="server" Visible="false" CssClass="admin-form-section admin-form-section-last">
                                    <h5 class="admin-form-section-title"><i class="fa fa-list-ol"></i> Installment</h5>
                                    <div class="form-group">
                                        <label>Select Installment (1 to 18)</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-calendar"></i></span>
                                            <asp:DropDownList ID="ddinstallment" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddinstallment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Installment</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Currently assigned on this installment</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-shopping-basket"></i></span>
                                            <asp:TextBox ID="txtprevproduct" Enabled="false" CssClass="form-control" runat="server" placeholder="Not assigned yet" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="box-footer admin-product-footer">
                                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Reset" OnClick="btnCancel_Click" CausesValidation="false" />
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClientClick="return validateAssign();" runat="server" Text="Assign Product" OnClick="btnSubmit_Click1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-th-list"></i> 18 Installment Map</h3>
                            </div>
                            <div class="box-body table-responsive">
                                <asp:GridView ID="gvAssign" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-hover dataTable admin-assign-grid" Width="100%"
                                    EmptyDataText="No installment map found.">
                                    <Columns>
                                        <asp:BoundField DataField="InstallmentNo" HeaderText="Inst." />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                        <asp:BoundField DataField="MRP" HeaderText="MRP" DataFormatString="{0:0.##}" />
                                        <asp:BoundField DataField="DP" HeaderText="DP" DataFormatString="{0:0.##}" />
                                        <asp:BoundField DataField="EntryBy" HeaderText="Assigned By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Assigned On" DataFormatString="{0:dd/MM/yyyy}" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

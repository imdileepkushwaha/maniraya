<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseInvoice.aspx.cs" Inherits="user_PurchaseInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/css/team-associates.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Purchase Invoice</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx">Home</a></li>
            <li><a href="#">Purchase</a></li>
            <li class="active">Purchase Invoice</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="team-page">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Purchase Invoice</h3>
                    </div>
                    <div class="box-body team-box-body">
                        <div class="team-toolbar">
                            <div class="row team-filter-grid" style="flex:1 1 auto; margin:0; width:100%;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="<%= txtfromdate.ClientID %>"><i class="fa fa-calendar"></i> From Date</label>
                                        <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="<%= txttodate.ClientID %>"><i class="fa fa-calendar"></i> To Date</label>
                                        <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display:none;">
                                    <div class="form-group">
                                        <label>User ID</label>
                                        <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display:none;">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList ID="DDLSTStatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="team-toolbar-actions">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>

                        <div class="form-group team-table-group">
                            <span class="team-table-caption"><i class="fa fa-file-alt"></i> Invoice List</span>
                            <div class="team-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable team-table" Width="100%"
                                    AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <img alt="Expand" class="invoice-expand-toggle" src="../franchisee/img/PLUS.jpg" />
                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                    <div class="team-table-wrap">
                                                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable">
                                                            <Columns>
                                                                <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product Name" />
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("colorname") %>'></asp:Label>-
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("sizename") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                                                                <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantity" />
                                                                <asp:BoundField DataField="Purchaseamount" HeaderText="Purchase Amount" />
                                                                <asp:BoundField DataField="GST" HeaderText="GST" />
                                                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluseridUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmailId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluseridEmailId" runat="server" Text='<%#Eval("EmailId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ContactNo" Visible="false">
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
                                        <asp:TemplateField HeaderText="Purchase Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDP" runat="server" Text='<%#Eval("TotalDP") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblpurchaseamount" runat="server" Text='<%#Eval("purchaseamount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGST" runat="server" Text='<%#Eval("GST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                <asp:Label ID="LblInvoiceStatus" runat="server" Text='<%#Eval("InvoiceStatus") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="LblPstatus" runat="server" Text='<%#Eval("PStatus") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="Lblstatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice" Visible="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Invoice" CssClass="btn btn-outline-dark btn-text w-100"
                                                    NavigateUrl='<%# string.Format("JoiningInvoice.aspx?OrderNo={0}", HttpUtility.UrlEncode(Eval("PurchaseID").ToString())) %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
            bindInvoiceExpandToggle();
        }

        function bindInvoiceExpandToggle() {
            $('.invoice-expand-toggle').off('click').on('click', function () {
                var $toggle = $(this);
                var isOpen = $toggle.attr('data-open') === '1';

                if (isOpen) {
                    $toggle.attr('data-open', '0').attr('src', '../franchisee/img/PLUS.jpg');
                    $toggle.closest('tr').next('.invoice-child-row').remove();
                    return;
                }

                $toggle.attr('data-open', '1').attr('src', '../franchisee/img/Continue1.png');
                $toggle.closest('tr').after(
                    "<tr class='invoice-child-row'><td></td><td colspan='999'>" + $toggle.next().html() + "</td></tr>"
                );
            });
        }
    </script>
</asp:Content>

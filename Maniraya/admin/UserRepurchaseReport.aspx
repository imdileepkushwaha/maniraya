<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="UserRepurchaseReport.aspx.cs" Inherits="Franchisee_UserRepurchaseReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="assets/css/admin-layout.css?v=73" />
    <style type="text/css">
        .admin-product-thumb {
            width: 40px;
            height: 40px;
            object-fit: cover;
            border-radius: 6px;
            border: 1px solid #e5e7eb;
            cursor: pointer;
        }
        #DivPhotolarge .admin-image-preview-wrap {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 240px;
            max-height: 75vh;
            overflow: auto;
            background: #f8fafc;
            padding: 12px;
        }
        #DivPhotolarge .admin-image-preview-img {
            max-width: 100% !important;
            max-height: 70vh !important;
            width: auto !important;
            height: auto !important;
            object-fit: contain !important;
        }
    </style>
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
             <div class="admin-report-page">
             <section class="content">
            <div class="container-fluid">
            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title"><i class="fa fa-filter"></i> Search Criteria</h3>
            </div>
                   <div class="box-body">
                  
                          <div class="row">
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label for="<%= txtfromdate.ClientID %>">From date :</label>
                                 <div class="input-group date">
                                     <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                     <span class="input-group-addon form_date_btn" style="cursor:pointer;" title="Select date">
                                         <i class="fa fa-calendar"></i>
                                     </span>
                                 </div>
                             </div>
                         </div>
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label for="<%= txttodate.ClientID %>">To date :</label>
                                 <div class="input-group date">
                                     <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                                     <span class="input-group-addon form_date_btn" style="cursor:pointer;" title="Select date">
                                         <i class="fa fa-calendar"></i>
                                     </span>
                                 </div>
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
                                 <asp:DropDownList ID="DDLSTStatus" CssClass="form-control" runat="server">
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
              <h3 class="box-title"><i class="fa fa-list-alt"></i> Repurchase Details</h3>
            </div>
                   <div class="box-body">
                          <div class="admin-table-toolbar">
                              <span class="admin-table-caption"><i class="fa fa-table"></i> User Repurchase Requests</span>
                          </div>
                          <div class="admin-table-wrap table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID" EmptyDataText="No repurchase records found.">
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
                                   <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                                  <asp:BoundField ItemStyle-Width="150px" DataField="TotalAmount" HeaderText="Total Amount" />                      
                         
                            
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
                                             <asp:Label ID="LblImageHidden" runat="server" Visible="false" Text='<%#Eval("Image") %>'></asp:Label>
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
                                     <asp:TemplateField HeaderText="Purchase Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
											   <asp:Label ID="LblOrderNo" runat="server" Visible="false" Text='<%#Eval("OrderNo") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Product Image">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" class="admin-product-thumb-link"
                                                data-full="<%# Server.HtmlEncode(Convert.ToString(Eval("Image"))) %>"
                                                title="View image">
                                                <img src="<%# Server.HtmlEncode(Convert.ToString(Eval("Image"))) %>" class="admin-product-thumb" alt="Product image"
                                                    onerror="this.onerror=null;this.src='../ProductImage/images.png';" />
                                            </a>
                                            <asp:Label ID="LblImage" runat="server" Visible="false" Text='<%# Eval("Image") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
								          <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
											  <asp:Label ID="Lblstatus" runat="server" ></asp:Label>
										     </ItemTemplate>
                                    </asp:TemplateField>     
                                                 <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                               <asp:LinkButton ID="btnSuccess" CssClass="admin-action-btn is-approve" CommandName="mySuccess" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Approve" runat="server">
                                                   <i class="fa fa-check" aria-hidden="true"></i> Approve
                                               </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
								       <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                                        <ItemTemplate>
                                              <asp:LinkButton ID="btnFail" CssClass="admin-action-btn is-reject" CommandName="myFail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Reject" runat="server">
                                                  <i class="fa fa-times" aria-hidden="true"></i> Reject
                                              </asp:LinkButton>
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
                 </section>
             </div>
             </ContentTemplate>
    </asp:UpdatePanel>

    <div id="DivPhotolarge" class="modal fade admin-image-preview-modal" tabindex="-1" role="dialog" aria-labelledby="repurchaseImagePreviewTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="repurchaseImagePreviewTitle"><i class="fa fa-picture-o"></i> Image Preview</h4>
                </div>
                <div class="modal-body">
                    <div class="admin-image-preview-wrap">
                        <img id="imgRepurchasePreview" class="admin-image-preview-img" alt="Preview" src="../ProductImage/images.png" />
                    </div>
                </div>
                <div class="modal-footer admin-modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript">
        function previewRepurchaseImage(el) {
            if (!el) { return false; }
            var url = el.getAttribute('data-full') || '';
            if (!url) {
                var thumb = el.querySelector ? el.querySelector('img') : null;
                if (thumb) { url = thumb.getAttribute('src') || ''; }
            }
            if (!url) {
                url = '../ProductImage/images.png';
            }

            var img = document.getElementById('imgRepurchasePreview');
            if (img) {
                img.src = url;
                img.style.maxWidth = '100%';
                img.style.maxHeight = '70vh';
                img.style.width = 'auto';
                img.style.height = 'auto';
                img.style.objectFit = 'contain';
            }

            if (typeof showAdminModal === 'function') {
                showAdminModal('DivPhotolarge');
            } else if (window.jQuery) {
                $('#DivPhotolarge').modal('show');
            }
            return false;
        }

        function bindRepurchaseImagePreview() {
            if (!window.jQuery) { return; }
            $(document).off('click.repurchaseImg', '.admin-product-thumb-link').on('click.repurchaseImg', '.admin-product-thumb-link', function (e) {
                e.preventDefault();
                e.stopPropagation();
                previewRepurchaseImage(this);
            });
        }

        function bindRepurchaseExpanders() {
            if (!window.jQuery) { return; }
            $(document).off('click.repurchasePlus', '[src*=PLUS]').on('click.repurchasePlus', '[src*=PLUS]', function () {
                $(this).closest('tr').after("<tr><td></td><td colspan='999'>" + $(this).next().html() + "</td></tr>");
                $(this).attr('src', 'img/Continue1.png');
            });
            $(document).off('click.repurchaseMinus', '[src*=Continue1]').on('click.repurchaseMinus', '[src*=Continue1]', function () {
                $(this).attr('src', 'img/PLUS.jpg');
                $(this).closest('tr').next().remove();
            });
        }

        $(function () {
            bindRepurchaseImagePreview();
            bindRepurchaseExpanders();
        });
        if (typeof Sys !== 'undefined' && Sys.Application) {
            Sys.Application.add_load(function () {
                bindRepurchaseImagePreview();
                bindRepurchaseExpanders();
            });
        }
    </script>
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" />
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initRepurchaseDatePickers() {
            if (!window.jQuery || !jQuery.fn.datepicker) { return; }
            $('.form_date').each(function () {
                var $el = $(this);
                if ($el.data('datepicker')) {
                    $el.datepicker('remove');
                }
                $el.datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    todayHighlight: true,
                    orientation: 'bottom auto'
                }).on('changeDate', function () {
                    $(this).datepicker('hide');
                });
            });

            $(document).off('click.repurchaseCal', '.form_date_btn').on('click.repurchaseCal', '.form_date_btn', function (e) {
                e.preventDefault();
                $(this).closest('.input-group').find('.form_date').datepicker('show');
            });
        }

        $(function () { initRepurchaseDatePickers(); });
        if (typeof Sys !== 'undefined' && Sys.Application) {
            Sys.Application.add_load(initRepurchaseDatePickers);
        }
    </script>
</asp:Content>


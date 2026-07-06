<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="FranchiseeStockDetail.aspx.cs" Inherits="FranchiseeStockDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
        <section class="content-header">
      <h1>
     Franchisee Stock Detail
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Franchisee management</a></li>
        <li class="active"> Franchisee Stock Detail </li>
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
              <h3 class="box-title">Franchisee Stock</h3>
              <div class="box-tools admin-record-filter-tools">
                  <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                  <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter" AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                      <asp:ListItem Selected="True">10</asp:ListItem>
                      <asp:ListItem>25</asp:ListItem>
                      <asp:ListItem>50</asp:ListItem>
                      <asp:ListItem>100</asp:ListItem>
                      <asp:ListItem>All</asp:ListItem>
                  </asp:DropDownList>
                  <span class="admin-record-filter-suffix">per page</span>
              </div>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
            
                   <div class="row" style="display:none;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RDBtnTRecharge" runat="server" Text="Stock by Customer Price" GroupName="A"  />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnUtility" runat="server" Text="Stock by Distributer Price" GroupName="A"   />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                          <div class="row">
                         
                               <div class="col-md-4">
                             <div class="form-group">
                                 <label>Franchisee :</label>
                                <asp:DropDownList ID="DDLstFranchisee" CssClass="form-control"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="getproduct"></asp:DropDownList>
                             </div>
                         </div>
                               <div class="col-md-4">
                             <div class="form-group">
                                 <label>Product :</label>
                                <asp:DropDownList ID="DDLstProduct" CssClass="form-control"  runat="server"></asp:DropDownList>
                             </div>
                         </div>
                     </div>
                         
                          
                     
                          <div class="row">
                         
                               <div class="col-md-12">
                             <div class="form-group">
                           <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                         </div>

  </div>                  
              </div>
             

                             

                              
                   
           
                  
                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" ShowFooter="true" FooterStyle-BorderColor="Wheat" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" DataKeyNames="ProductID" OnRowDataBound="GridView1_RowDataBound">
                               <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" />
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Franchisee name">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lbluseridUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridEmailId" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="SubProduct Id">
    <ItemTemplate>
        <asp:Label ID="lblSubproductname" runat="server" Text='<%#Eval("SubProductID") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
 <asp:TemplateField HeaderText="SubProduct name">
    <ItemTemplate>
        <asp:Label ID="lblSubproductname" runat="server" Text='<%#Eval("Subproductname") %>'></asp:Label>
    </ItemTemplate>
       </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="BV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridContactNo" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="DP">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridContactNo" runat="server" Text='<%#Eval("DP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                      <asp:TemplateField HeaderText="Stock Recieve">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockNo" runat="server" Text='<%#Eval("Recieve") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbvleft" runat="server" Text='<%#Eval("Sales") %>'></asp:Label>
                                        </ItemTemplate>
                                         

                                        <FooterTemplate>


                                            <asp:Label ID="lbltotalbvleft" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldpleft" runat="server" Text='<%#Eval("stockleft") %>'></asp:Label>
                                        </ItemTemplate>
                                            <FooterTemplate>


                                            <asp:Label ID="lbltotaldpleft" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                      
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
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=PLUS]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "img/Continue1.png");
    });
    $("[src*=Continue1]").live("click", function () {
        $(this).attr("src", "img/PLUS.jpg");
        $(this).closest("tr").next().remove();
    });

</script>
       <script type="text/javascript">
           $('.form_date').datepicker({
               format: 'dd/mm/yyyy',
           }).on('changeDate', function (ev) {
               $(this).datepicker('hide');
           });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
</asp:Content>


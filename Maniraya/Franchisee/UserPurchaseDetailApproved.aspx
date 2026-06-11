<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="UserPurchaseDetailApproved.aspx.cs" Inherits="UserPurchaseDetailApproved" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      
        <section class="content-header">
      <h1>
   User Purchase
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Franchisee</a></li>
        <li class="active">Approved Purchase </li>
      
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
                 <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">


            
                  
                          <div class="row">
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>From date :</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                  <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>To date :</label>
                                  <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                                             <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                             </div>
                         </div>

                               <div class="col-md-4">
                             <div class="form-group">
                                 <label>Franchisee ID :</label>
                                   <asp:TextBox ID="TxtFranchiseeId" runat="server" CssClass="form-control" />
                               
                             </div>
                         </div>


                              <div class="col-md-4">
                             <div class="form-group">
                                 <label>Status :</label>
                                   <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                             
                                        </asp:DropDownList>
                               
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
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick = "ExportToExcel" /></div>
                    
                        <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PurchaseID" >
                               <Columns>
                                   
                                   
                                    <asp:TemplateField HeaderText="Purchase Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid123" runat="server" Text='<%#Eval("PurchaseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="User name">
                                        <ItemTemplate>
                                               <asp:Label ID="Labeluserid" runat="server" Text='<%#Eval("userid") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbluseridUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Shipping Address">
                                        <ItemTemplate>
                                              
                                       Address :      <asp:Label ID="lblshippingaddress" runat="server" Text='<%#Eval("shippingaddress") %>'></asp:Label>,
                                        City :     <asp:Label ID="lblcity" runat="server" Text='<%#Eval("CityName") %>'></asp:Label><br />
                                        State :     <asp:Label ID="lblstate" runat="server" Text='<%#Eval("StateName") %>'></asp:Label><br />
                                        Area  :   <asp:Label ID="lblarea" runat="server" Text='<%#Eval("ShippingAreaName") %>'></asp:Label><br />
                                        Pincode :      <asp:Label ID="lblpincode" runat="server" Text='<%#Eval("shippingpincode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                           
                               <%--      
                                   <asp:TemplateField HeaderText="Purchase Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridEmailId" runat="server" Text='<%#Eval("PurchaseAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                      <asp:TemplateField HeaderText="CGST" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridContactNo" runat="server" Text='<%#Eval("CGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SGST">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluseridaddress" runat="server" Text='<%#Eval("SGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="IGST" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("IGST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                               <%--    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid2" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Purchase date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Purchasedate") %>'></asp:Label>
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


 </div>


           

            
             </ContentTemplate>
          <Triggers>
      
        <asp:PostBackTrigger ControlID = "ImageButton1" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
     <%--  <script type="text/javascript">
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
     </script>--%>
</asp:Content>


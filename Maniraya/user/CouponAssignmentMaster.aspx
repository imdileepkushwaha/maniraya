<%@ Page Title="Add City" Language="C#" MasterPageFile="~/User/usermaster.master" AutoEventWireup="true" CodeFile="CouponAssignmentMaster.aspx.cs" Inherits="CouponAssignmentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Coupon Assigned     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">User</a></li>
        <li class="active">Coupon Assigned</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <div class="row">
     
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Assigned Coupon Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
<div class="row">
                  
                 
                <div class="form-group table-responsive">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coupon Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblname" runat="server"  Text='<%#Eval("CouponName") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblamount" runat="server"  Text='<%#Eval("Amount") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Valid From">
                               <ItemTemplate>
                                     <asp:Label ID="lblvfrom" runat="server"  Text='<%#Eval("FromDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valid Till">
                               <ItemTemplate>
                                     <asp:Label ID="bltdate" runat="server"  Text='<%#Eval("ToDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Minimum Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblminamount" runat="server"  Text='<%#Eval("minshopamount") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                               <ItemTemplate>
                                     <asp:Label ID="lblcode" runat="server"  Text='<%#Eval("code") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblusername" runat="server"  Text='<%#Eval("username") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatus" runat="server"  Text='<%#Eval("Status") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Purchase Id">
                               <ItemTemplate>
                                     <asp:Label ID="lblusername1" runat="server"  Text='<%#Eval("PurchaseId") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                </div>             
               </div>
   
                
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
             
                    
              </div>
         
          </div>
            </div>

    
     </div>
      
    
      </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
      <script type="text/javascript">
          Sys.Application.add_load(LoadHandler);
          function LoadHandler() {
              $('.form_date').datepicker({
                  format: 'dd/M/yyyy',
              }).on('changeDate', function (ev) {
                  $(this).datepicker('hide');
              });
          }
     </script>
   
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
</asp:Content>


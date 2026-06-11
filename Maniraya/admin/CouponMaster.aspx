<%@ Page Title="Add City" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CouponMaster.aspx.cs" Inherits="CouponMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Coupon Master     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Coupon Master</li>
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
              <h3 class="box-title">Add Coupon</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

                 <div class="box-body">
                     <div class="row">
                          <div class="col-md-4">
                             <div class="form-group">
                                 <label>Coupon Name</label>
                                  <asp:TextBox ID="txtcname" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Valid From</label>
                                  <asp:TextBox ID="txtfdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Valid Till</label>
                                 <asp:TextBox ID="txttdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Coupon Amount</label>
                                 <asp:TextBox ID="txtamount" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Minimun Purchase Amount</label>
                                 <asp:TextBox ID="txtminamount" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                 </div>
              <!-- /.box-body -->

              <div class="box-footer">
                 <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                    
              </div>
         
          </div>
            </div>

     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Coupon Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
<div class="row">
                  
                 
                <div class="form-group table-responsive">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Coupon Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblcname" runat="server"  Text='<%#Eval("Code") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatus" runat="server"  Text='<%#Eval("Astatus") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="lnkedit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>|
                                            <asp:LinkButton ID="lbledit" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="lnkdel"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
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

     <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Coupon Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:HiddenField ID="hdId" runat="server"></asp:HiddenField>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                Coupon Name: <asp:TextBox ID="txtcnameedit"  CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                Valid From: <asp:TextBox ID="txtvalidfrom"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                Valid Till: <asp:TextBox ID="txtvalidtill" CssClass="form-control form_date"  runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                Coupon Amount: <asp:TextBox ID="txtcouponamount" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                Minimum Purchase Amount: <asp:TextBox ID="txtminshopamount"  CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                Coupon Status:
                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Deactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="btn btn-primary" OnClick="btnUpdate_Click"  />
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
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


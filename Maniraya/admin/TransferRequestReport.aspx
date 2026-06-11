<%@ Page Title="Withdrawl Request Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="TransferRequestReport.aspx.cs" Inherits="TransferRequestReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
       Transfer To Wallet 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#"> Admin Request </a></li>
        <li class="active"> Transfer To Wallet </li>
      </ol>
    </section>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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
                            <h3 class="box-title">Search Criteria</h3>
                        </div>

                        <div class="box-body">
                          
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>From date</label>
                                 <asp:TextBox runat="server" CssClass="form-control form_date" ID="txtfromdate"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>To date</label>
                                      <asp:TextBox runat="server" CssClass="form-control form_date" ID="txttodate"></asp:TextBox>
                                    </div>
                                </div>
                               
                            </div>
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>User Id</label>
                                <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            
                              
                            </div>
                             

                        </div>
                        <div class="box-footer">
                              
                              
  
   <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                        </div>

                    </div>
                </div>
                  <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                        </div>

                        <div class="box-body">
                          
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group table-responsive">
                                     <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  OnRowDataBound="grdGetHelp_RowDataBound" OnRowCommand="GridView1_RowCommand" >
                                    <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date of Request">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcreatingdate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                              <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                  
                                    <asp:TemplateField HeaderText="BV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                           <asp:TemplateField HeaderText="Admincharge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdmincharge" runat="server" Text='<%#Eval("Admincharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                          <asp:TemplateField HeaderText="TDS Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTDSCharge" runat="server" Text='<%#Eval("TDSCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                           <asp:TemplateField HeaderText="Payble Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaybleAmount" runat="server" Text='<%#Eval("PaybleAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                           
                                    <asp:TemplateField HeaderText="TransactionID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionID" runat="server" Text='<%#Eval("TransactionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                        
                                   
                                </Columns>
                            </asp:GridView>
                                    </div>
                                </div>
                               
                             
                            </div>

                             

                        </div>
                        <div class="box-footer">
                              
                             



                        </div>

                    </div>
                </div>
                <div id="DivPhotolarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                                          
                          <asp:Image ID="ImageLarge" runat="server" Width="570px" Height="400px" />
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
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
     <script type="text/javascript">


         function showModal1() {
             $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#DivPhotolarge').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }
        </script>
</asp:Content>


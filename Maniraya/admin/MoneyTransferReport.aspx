<%@ Page Title="Money Transfer Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="MoneyTransferReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script type="text/javascript">
          function validate2() {
              // alert('sd');
              if (document.getElementById("<%=TxtLiveId.ClientID%>").value == "") {

                  alert('Enter Live ID');
                  // alert("Enter Rank No"); 
                  document.getElementById("<%=TxtLiveId.ClientID%>").focus();
                  return false;
              }
              if (document.getElementById("<%=TxtVEndorId.ClientID%>").value == "") {

                  alert('Enter Vendor ID');
                  // alert("Enter Rank No"); 
                  document.getElementById("<%=TxtVEndorId.ClientID%>").focus();
                  return false;
              }
          }
          </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
   <section class="content-header">
      <h1>
       Money Transfer Report   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#"> Money Transfer</a></li>
        <li class="active">  Money Transfer Report   </li>
      </ol>
    </section>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
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
                                        <label>User ID</label>
                                <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Account No</label>
                                     <asp:TextBox ID="txtaccountno"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                               
                            </div>
                            </div>
                        <div class="box-footer">
                               <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                        </div>
                      </div>
                  </div>
            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                        </div>

                        <div class="box-body">
                          
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group table-responsive">
                                     <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sender Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsendermobile" runat="server" Text='<%#Eval("SenderMobileNo") %>'></asp:Label>
                                              <asp:Label ID="lbluserId" runat="server" Text='<%#Eval("User_id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Account No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("AmountTransfer_To") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransaction" runat="server" Text='<%#Eval("transaction_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrequestedamount" runat="server" Text='<%#Eval("requestedamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("creatd_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            
                                              <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                   
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
            </div>

              <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Update Status</h4>
                    </div>
                    <div class="modal-body">
                       
                         <div class="form-group">
                       <label>Status</label>
                            <asp:Label ID="lblrefrenceid" Visible="false" runat="server" Text=""></asp:Label>
                              <asp:Label ID="lblUserid" Visible="false" runat="server" Text=""></asp:Label>
                           <asp:DropDownList ID="ddlststatus" runat="server" class="form-control">
                               <asp:ListItem Value="2">SUCCESS</asp:ListItem>
                                <asp:ListItem Value="3">FAILED</asp:ListItem>
                           </asp:DropDownList>      
                          
                        </div>
                         <div class="form-group">
                           <label>Live ID</label>
                        
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtLiveId" ></asp:TextBox>
                        </div>
                         <div class="form-group">
                       
                             <label>Vendor ID</label>
                         
                            <asp:TextBox runat="server" class="form-control" ID="TxtVEndorId" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click"  />
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
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


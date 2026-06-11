<%@ Page Title="Recharge Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="RechargeReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate2() {
            // alert('sd');
            if (document.getElementById("<%=txtliveid.ClientID%>").value == "") {

                  alert('enter Live id');
                  // alert("Enter Rank No"); 
                  document.getElementById("<%=txtliveid.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
       Recharge Report   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Recharge</a></li>
        <li class="active">Recharge Report </li>
      </ol>
    </section>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:10%;left:20%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

             <div class="row">
                    <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Recharge Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                      <div class="col-md-3">
                                        <label>From Date</label>
                                        <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                        </div>
                                    <div class="col-md-3">
                                        <label>To Date</label>
                                            <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                 <div class="col-md-3">
                                        <label>Service Type</label>
                                        <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceType_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Mobile Prepaid</asp:ListItem>
                                            <asp:ListItem Value="6">Mobile Postpaid</asp:ListItem>
                                            <asp:ListItem Value="3">Landline</asp:ListItem>
                                            <asp:ListItem Value="2">DTH</asp:ListItem>
                                            <asp:ListItem Value="5">Electricity</asp:ListItem>
                                            <asp:ListItem Value="9">Gas</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                       <div class="col-md-3">
                                        <label>Operator Type</label>
                                        <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>


                                 
                                     
                                   
                                </div>
                              <div class="row">
                                    <div class="col-md-3">
                                        <label>Name</label>
                                         <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                     <div class="col-md-3">
                                        <label>User Id</label>
                                          <asp:TextBox ID="txtusermobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3">
                                        <label>Recharge No</label>
                                          <asp:TextBox ID="txtrechargemobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                       <div class="col-md-3">
                                        <label>Transaction Id</label>
                                          <asp:TextBox ID="txttransactionid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                      
                                   
                                </div>
                                 
                               <div class="row">
                                   
                                   
                                     <div class="col-md-3">
                                        <label>Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="refund">Refund</asp:ListItem>
                                            <asp:ListItem Value="Success">Success</asp:ListItem>
                                            <asp:ListItem Value="Failed">Failed</asp:ListItem>
                                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                 
                                     
                                   
                                </div>
                            </div>
                        <div class="box-footer">
                               <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                         <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" ToolTip="Download Excel" CssClass="pull-right" Width="40px" OnClick="imgExcel_Click" />
                        </div>
                        </div>
                          <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Recharge Report</h3>
                        </div>

                        <div class="box-body">
                             <div class="row">
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true" 
                                    OnSelectedIndexChanged="btnSubmit_Click" Width="80px">
                                    <%--<asp:ListItem>5</asp:ListItem>--%>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                              <div class="row">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" OnRowDataBound="GridView1_RowDataBound"  AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                       <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("entrydate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                     <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("referenceid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("usermobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Recharge No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("RechargeMobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Operator">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount1" runat="server" Text='<%#Eval("Operator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="ServiceType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount2" runat="server" Text='<%#Eval("serviceType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount3" runat="server" Text='<%#Eval("rechargeamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Live Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount4" runat="server" Text='<%#Eval("liveid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Message">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmessage" runat="server" Text='<%#Eval("message") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalamount" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                      
                                    <asp:TemplateField HeaderText="Api name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblapiname" runat="server" Text='<%#Eval("apiname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                   
                                   <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                              <span onclick="return confirm_click();">
                                            <asp:LinkButton ID="btnSuccess" CssClass="btn btn-success" CommandName="mySuccess" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Success" runat="server"><i class="icon fa fa-check-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDispute" CssClass="btn purple" CommandName="myDispute" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Dispute" runat="server"><i class="icon fa fa-legal" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnFail" CssClass="btn btn-danger" CommandName="myFail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Fail" runat="server"><i class="icon fa fa-window-close-o" aria-hidden="true"></i></asp:LinkButton>
                                      </span>
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
          <Triggers>
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
     <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Update Status</h4>
                        </div>
                        <div class="modal-body">
                          
                            <div class="form-group">
                                Live Id
                                <asp:Label ID="LblRefrtenceId" runat="server" Text="" Visible="false"></asp:Label>
                           <asp:TextBox ID="txtliveid" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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


<%@ Page Title="Recharge Report" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="RechargeReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
            Recharge Report
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#"> Recharge</a></li>
        <li class="active">Recharge Report</li>
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
                                    <%--<div class="col-md-4">
                                        <label>Name</label>
                                         <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>--%>
                                    <%--<div class="col-md-4">
                                        <label>User Mobile</label>
                                          <asp:TextBox ID="txtusermobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <%--<label>Recharge User Id</label>--%>
                                        <label>Recharge Mobile No.</label>
                                          <asp:TextBox ID="txtrechargemobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                      <div class="col-md-4">
                                        <label>From Date</label>
                                        <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                        </div>
                                    <div class="col-md-4">
                                        <label>To Date</label>
                                            <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                   
                                </div>

                                 <div class="row">

                                    <div class="col-md-4">
                                        <%--<label>Transaction Id</label>--%>
                                        <label>Transaction No.</label>
                                          <asp:TextBox ID="txttransactionid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-md-4">
                                        <label>Service Type</label>
                                        <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control" 
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlServiceType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Mobile Prepaid</asp:ListItem>
                                            <asp:ListItem Value="6">Mobile Postpaid</asp:ListItem>
                                            <asp:ListItem Value="3">Landline</asp:ListItem>
                                            <asp:ListItem Value="2">DTH</asp:ListItem>
                                            <asp:ListItem Value="5">Electricity</asp:ListItem>
                                            <asp:ListItem Value="9">Gas</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <label>Operator Type</label>
                                        <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" 
                                            AutoPostBack="true" OnSelectedIndexChanged="btnSubmit_Click"></asp:DropDownList>
                                    </div>
                                   
                                </div>

                                <div class="row">

                                    <div class="col-md-4">
                                        <label>Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" 
                                            AutoPostBack="true" OnSelectedIndexChanged="btnSubmit_Click">
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
                              <div class="row table-responsive">
                                  <div class="col-md-12">
                                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" OnRowDataBound="GridView1_RowDataBound"  AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date & Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("entrydate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("referenceid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("usermobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   
                                    <asp:TemplateField HeaderText="Recharge No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("RechargeMobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operator Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOperatorName" runat="server" Text='<%#Eval("Operator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceType" runat="server" Text='<%#Eval("serviceType") %>'></asp:Label>
                                               <asp:Label ID="lbldispute" runat="server" Text='<%#Eval("disputestatus") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="server" Text='<%#Eval("rechargeamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                
                                    
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmessage" runat="server" Text='<%#Eval("message") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalamount" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Live ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLiveID" runat="server" Text='<%#Eval("LiveID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <span onclick="return confirm_click();">
                                            <asp:LinkButton ID="btnDispute"  CommandName="myDispute" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Dispute" runat="server"><i class="icon fa fa-legal" aria-hidden="true"></i> Dispute</asp:LinkButton>
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
    
</asp:Content>


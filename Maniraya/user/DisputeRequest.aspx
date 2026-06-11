<%@ Page Title="Dispute Request" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="DisputeRequest.aspx.cs" Inherits="admin_DisputeRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
      Dispute Request 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Request</a></li>
        <li class="active">Dispute Request</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
              <h3 class="box-title">Dispute Request</h3>
            </div>
         
              <div class="box-body">
                     <div class="row">
                                   
                                   
                         </div>
                   <div class="row">
                        <div class="col-md-4">
                                        <div class="form-group">
                                        <label>Transaction Id</label>
                                        <asp:TextBox ID="txttransactionid" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                    </div>
                                    <div class="col-md-4">
                                          <div class="form-group">
                                          <label>From Date</label>
                                            <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                               </div>
                                    </div>
                                    <div class="col-md-4">
                                          <div class="form-group">
                                          <label>To Date</label>
                                             <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                               </div>
                                    </div>
                                 
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                          <div class="form-group">
                                          <label>User ID</label>
                                            <asp:TextBox ID="txtuserno" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                    </div>
                                    <div class="col-md-4">
                                       <div class="form-group">
                                              <label>Recharge No</label>
                                          <asp:TextBox ID="txtrechargeno" CssClass="form-control" runat="server"></asp:TextBox>
                                           </div>
                                    </div>
                                     <div class="col-md-4">
                                       <div class="form-group">
                                             <label>Status</label>
                                      <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            <asp:ListItem>Pending</asp:ListItem>
                                            <asp:ListItem>Approved</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                           </div>
                              </div>
                                   
                                   </div>
                 
                                
                               
                  </div>
                 <div class="box-footer">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                 </div>
                 </div>
            <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Dispute Request</h3>
            </div>
         
              <div class="box-body">
                    <div class="row">
                                   <div class="col-md-4">
                                       </div>
                                 <div class="col-md-4">
                                       </div>
                                 <div class="col-md-4">
                                        <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged" Width="80px">
                                       
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                                             <asp:ListItem>500</asp:ListItem>
                                             <asp:ListItem>All</asp:ListItem>      
                    </asp:DropDownList>
                                       </div>
                            </div>
                     <div class="row table-responsive">
                         <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" Visible="false" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                            <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("referenceid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserno" runat="server" Text='<%#Eval("usermobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recharge No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("rechargemobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Recharge Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus1234" runat="server" Text='<%#Eval("Rechargedate","{0:dd/MM/yyyy hh:mm tt}") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="approve Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblapprovedate1234" runat="server" Text='<%#Eval("approvedate","{0:dd/MM/yyyy hh:mm tt}") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recharge Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("rechargeamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispute Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("entrydate") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="LiveId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus12" runat="server" Text='<%#Eval("LiveId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="txn.Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus2222" runat="server" Text='<%#Eval("rechargestatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="dispute Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit Status</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                Select Status
                            <asp:Label ID="lblidedit" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:DropDownList ID="ddstatusedit" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Select Status</asp:ListItem>
                                    <asp:ListItem>Approved</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                Live Id
                           <asp:TextBox ID="txtliveid" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                Remark
                           <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn green" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">


        function validate2() {
            // alert('sd');
            if (document.getElementById("<%=ddstatusedit.ClientID%>").value == "0") {

                   alert('Select Status');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddstatusedit.ClientID%>").focus();
                   return false;
               }
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


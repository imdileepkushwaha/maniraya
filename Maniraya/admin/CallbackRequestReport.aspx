<%@ Page Title="Callback Request Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CallbackRequestReport.aspx.cs" Inherits="admin_UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
      Callback Request Report
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Request</a></li>
        <li class="active">Callback Request Report</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
               <div class="row">
     

     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Callback Request Report</h3>
            </div>
         
              <div class="box-body">
                  
                          <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">

                                       
                                        <label>From Date</label>
                                         <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                             </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group">
                                         <label>To Date</label>
                                           <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server"></asp:TextBox>
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
                                           <label>Status</label>
                                       <asp:DropDownList ID="ddcallstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            <asp:ListItem>Pending</asp:ListItem>
                                            <asp:ListItem>Picked</asp:ListItem>
                                            <asp:ListItem>Not Picked</asp:ListItem>
                                        </asp:DropDownList>
                                              </div>
                                    </div>
                                   
                                </div>
                                <div class="row">

                                     <div class="col-md-6">
                                           <div class="form-group">
                                              <label>Result Status</label>
                                           <asp:DropDownList ID="ddresultstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            <asp:ListItem>Opened</asp:ListItem>
                                            <asp:ListItem>Closed</asp:ListItem>
                                        </asp:DropDownList>
                                               </div>
                                     </div>
                                    <div class="col-md-6">
                                      
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
              <h3 class="box-title">Callback Request Report</h3>
            </div>
         
              <div class="box-body">
                   <div class="row table-responsive">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Call Pick Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("callstatus2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Call Result Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcallresultstatus" runat="server" Text='<%#Eval("CallResultStatus2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Call Picked By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpickedby" runat="server" Text='<%#Eval("pickedby") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
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
                            <h4 class="modal-title">Edit Callback Request Status</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                User Id
                            <asp:Label ID="lblcallbackrequestid" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtuseridedit"></asp:TextBox>
                            </div>
                              <div class="form-group">
                                Mobile
                                <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtmobile"></asp:TextBox>
                            </div>
                              <div class="form-group">
                                Call Pick Status
                               <asp:RadioButtonList ID="rbstatus" RepeatDirection="Horizontal" style="width:100%;" runat="server">
                                   <asp:ListItem>Picked</asp:ListItem>
                                   <asp:ListItem>Not Picked</asp:ListItem>
                               </asp:RadioButtonList>
                            </div>
                             <div class="form-group">
                                Call Result Status
                               <asp:RadioButtonList ID="rbresultstatus" RepeatDirection="Horizontal" style="width:100%;" runat="server">
                                   <asp:ListItem>Opened</asp:ListItem>
                                   <asp:ListItem>Closed</asp:ListItem>
                               </asp:RadioButtonList>
                            </div>
                             <div class="form-group">
                                Call Picked By
                              <asp:DropDownList ID="ddpickedby"  CssClass="form-control"  runat="server">
                                  <asp:ListItem Value="0">Select Value</asp:ListItem>
                                  <asp:ListItem>Raja M</asp:ListItem>
                                  <asp:ListItem>Saraswathi V</asp:ListItem>
                                  <asp:ListItem>Kumaran</asp:ListItem>
                                  <asp:ListItem>Muthulakshmi</asp:ListItem>                                  
                              </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                Remark
                                <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtremark"></asp:TextBox>
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
            if (document.getElementById("<%=txtuseridedit.ClientID%>").value == "") {
                   toastr.warning('Warning', 'Enter User Id');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtuseridedit.ClientID%>").focus();
                   return false;
            }
           
            if (document.getElementById("<%=txtremark.ClientID%>").value == "") {
                toastr.warning('Warning', 'Enter User Id');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtremark.ClientID%>").focus();
                   return false;
            }
            if (document.getElementById("<%=ddpickedby.ClientID%>").value == "") {
                toastr.warning('Warning', 'Select Picked By');
                // alert("Enter Rank No"); 
                document.getElementById("<%=ddpickedby.ClientID%>").focus();
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


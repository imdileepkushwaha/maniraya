<%@ Page Title="Deposit Request Report" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="JoiningRequestReport.aspx.cs" Inherits="JoiningRequestReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
      <h1>
       Joining Product Request Report
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Joining Product Request Report </a></li>
        <li class="active">Joining Product Request Report  </li>
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
                                        <label>User Id</label>
                                <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Status</label>
                                          <asp:DropDownList ID="ddstatus"  CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">Pending</asp:ListItem>
                             <asp:ListItem Value="1">Approved</asp:ListItem>
                            
                                   
                        </asp:DropDownList>
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
                                            <asp:Label ID="lblcreatingdate" runat="server" Text='<%#Eval("entrydate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request From">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("Fromuserid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        

                                         <%--<asp:TemplateField HeaderText="Main Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmainamount" runat="server" Text='<%#Eval("Mainamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                
                                  
                                   <%-- <asp:TemplateField HeaderText="Sponser Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponserid" runat="server" Text='<%#Eval("sponserid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sponser Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsponsername" runat="server" Text='<%#Eval("sponsername") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   
                                          <asp:TemplateField HeaderText="Plan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("planname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Plan Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlanamount" runat="server" Text='<%#Eval("planamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Product Pin">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoofEpin" runat="server" Text='<%#Eval("EmPinNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                      
                                   
                                        
                                   
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status1") %>'></asp:Label>
                                              <asp:Label ID="Labelblstatus" runat="server" Text='<%#Eval("status") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                     
                                       
                                         <asp:TemplateField HeaderText="Approve By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproveBy" runat="server" Text='<%#Eval("ApproveBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreleasedate" runat="server" Text='<%#Eval("approvedate","{0:dd/MM/yyyy hh:mm tt}") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product Detail">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lbEdit" CommandName="photolarge"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                         
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Action" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnApprove" CommandName="approve" OnClick="btnApprove_click" runat="server"> Approve</asp:LinkButton> 
                                          
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
                                          
                          
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable" >
                        <Columns>
                            <asp:BoundField  DataField="ProductID" HeaderText="Product Code" />
                            <asp:BoundField  DataField="ProductName" HeaderText="Product Name" />
                               <asp:BoundField  DataField="Quantity" HeaderText="Quantity" />
                                
                             
                                   <asp:BoundField  DataField="Amount" HeaderText="Amount" />
                            
                            
                                <asp:BoundField  DataField="TotalAmount" HeaderText="TotalAmount" />
                                
                          
                              
                        </Columns>
                    </asp:GridView>
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


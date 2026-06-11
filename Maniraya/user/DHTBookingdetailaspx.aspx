<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="DHTBookingdetailaspx.aspx.cs" Inherits="user_DHTBookingdetailaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
     DTH Booking Report  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">DTH Booking</a></li>
        <li class="active">DTH Booking Report   </li>
      
      </ol>
    </section>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>From date :</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>To date :</label>
                                  <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                             </div>
                         </div>
                               <div class="col-md-4">
                             <div class="form-group">
                                 <label>User ID :</label>
                                 <asp:TextBox ID="txtuserid"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                             <div class="row">
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Service Provider :</label>
                                 <asp:DropDownList ID="DDlserviceprovider" runat="server" CssClass="form-control"></asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <label>Status :</label>
                                   <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                       <asp:ListItem Value="SUCCESS">SUCCESS</asp:ListItem>
                                          <asp:ListItem Value="FAILED">FAILED</asp:ListItem>
                                          <asp:ListItem Value="PENDING">PENDING</asp:ListItem>
                                   </asp:DropDownList>
                             </div>
                         </div>
                             <div class="col-md-4">
                             <div class="form-group">
                                 <label>Customer Mobile :</label>
                                 <asp:TextBox ID="TxtCustomerMobile"  CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                          
                       </div>
                         <div class="box-footer">
                        
             

                             

                              
                      <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click"  />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click"  />


                             <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" ToolTip="Download Excel" CssClass="pull-right" Width="40px"  />

              </div>


                  
              </div>


              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
                   <div class="box-body">
                  <div class="row">
                      <div class="col-md-4">
                          </div>
                        <div class="col-md-4">
                          </div>
                         <div class="col-md-4">
                   
                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true" 
                         Width="80px" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                        <asp:ListItem>All</asp:ListItem>                      
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                    </asp:DropDownList>
                  
                </div>
                  </div>



                          <div class="row">
                         <div class="col-md-12">
                             <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                               <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Entrydate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransactionid" runat="server" Text='<%#Eval("transactionid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CustomerMobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerMobile" runat="server" Text='<%#Eval("CustomerMobile") %>'></asp:Label>
                                               <asp:Label ID="LblCustomername" runat="server" Text='<%#Eval("CustomerName") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblAddress" runat="server" Text='<%#Eval("Address") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblPincode" runat="server" Text='<%#Eval("PinCode") %>' Visible="false"></asp:Label>
                                             <asp:Label ID="LblPackageName" runat="server" Text='<%#Eval("PackageName") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblSBoxName" runat="server" Text='<%#Eval("SBoxName") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LblOperator" runat="server" Text='<%#Eval("Operator") %>' Visible="false"></asp:Label>
                                             <asp:Label ID="LblState" runat="server" Text='<%#Eval("State") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LblCity" runat="server" Text='<%#Eval("City") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LblDistrict" runat="server" Text='<%#Eval("District") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LblLandmark" runat="server" Text='<%#Eval("Landmark") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PackRate">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPackRate" runat="server" Text='<%#Eval("PackRate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="LiveID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbLiveID" runat="server" Text='<%#Eval("LiveID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                              <asp:Label ID="lbldispute" runat="server" Text='<%#Eval("dispute") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                              <span onclick="return confirm_click();">
                                            <asp:LinkButton ID="btnSuccess" Visible="false" CssClass="btn btn-success" CommandName="mySuccess" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Success" runat="server"><i class="icon fa fa-check-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDispute" CssClass="btn purple" CommandName="myDispute" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Dispute" runat="server"><i class="icon fa fa-legal" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnFail" CssClass="btn btn-danger" Visible="false" CommandName="myFail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Fail" runat="server"><i class="icon fa fa-window-close-o" aria-hidden="true"></i></asp:LinkButton>
                                      </span>
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
                        <h4 class="modal-title">Subscription Package Detail</h4>
                    </div>
                  <div class="modal-body">
                                            <div class="form-group">
                                                 <label>Service Provider</label>
                                                                                <asp:Label ID="LblOperator" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>    
                        <div class="form-group">
                                                <label>SetopBox</label>
                                                                              <asp:Label ID="LblSboxname" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>    
                           <div class="form-group">
                                               <label>Package</label>
                                                                               <asp:Label ID="LblPackage" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>       
                                <div class="form-group">
                                             <label>Customer Name</label>
                                                                            <asp:Label ID="LblCustomername" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>   
                      <div class="form-group">
                                           <label>State</label>
                                                                           <asp:Label ID="lblState" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>    
                      <div class="form-group">
                                           <label>City</label>
                                                                           <asp:Label ID="lblCity" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>   
                            <div class="form-group">
                                           <label>Customer Address</label>
                                                                           <asp:Label ID="LblAddress" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>  
                        <div class="form-group">
                                         <label>Pincode</label>
                                                                                <asp:Label ID="LblPinconw" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>  
                      <div class="form-group">
                                             <label>District</label>
                                                                            <asp:Label ID="lbldistrict" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>  
                       <div class="form-group">
                                             <label>Landmark</label>
                                                                            <asp:Label ID="lbllandmark" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>                         
                                                                
                                                               
                                                                </div>
                    <div class="modal-footer">
                    
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">

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
              format: 'dd/MM/yyyy',
          }).on('changeDate', function (ev) {
              $(this).datepicker('hide');
          });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/MM/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
</asp:Content>


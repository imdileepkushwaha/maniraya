<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="BankAccountAdd.aspx.cs" Inherits="admin_BankAccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=txtaccountholdername.ClientID%>").value == "") {
                alert('Enter Acc Holder Name');
                document.getElementById("<%=txtaccountholdername.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositaccountno.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtdepositaccountno.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositbank.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtdepositbank.ClientID%>").focus();
                return false;
            }
          
        }

        function validate2() {
            if (document.getElementById("<%=txtaccholdernameedit.ClientID%>").value == "") {
                alert('Enter Account Holder Name');
                document.getElementById("<%=txtaccholdernameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtaccountnoedit.ClientID%>").value == "") {
                alert('Enter Account No');
                document.getElementById("<%=txtaccountnoedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtdepositbankedit.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=txtdepositbankedit.ClientID%>").focus();
                return false;
            }
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Bankaccount Add      
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Bankaccount Add </li>
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
              <h3 class="box-title">Bankaccount Add</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
              <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Bank Name :</label>
                                 <asp:TextBox ID="txtdepositbank" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Bank Account Number  :</label>
                               <asp:TextBox ID="txtdepositaccountno" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                    <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>IFSC Code :</label>
                                <asp:TextBox ID="txtifsccode" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                           <div class="col-md-6">
                             <div class="form-group">
                                 <label>Account Holder Name  :</label>
                               <asp:TextBox ID="txtaccountholdername" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                     <div class="row">
                           <div class="col-md-6">
                                  <h5>QR Code</h5>
                             <div class="form-group">
                                 <asp:Image ID="ImageShow" runat="server" Width="100px" Height="100px"  />
                              
                           
                             </div>
                         </div>
                          <div class="col-md-6">
                              <h5>QR Code</h5>
                             <div class="form-group">
                                   <asp:FileUpload ID="ProductImageUpload" runat="server" />
                           
                             </div>
                         </div>
                       
                     </div>
              <!-- /.box-body -->

              <div class="box-footer">
                   <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
               
              </div>
         
          </div>
            </div>
                 <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                <div class="form-group table-responsive">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                     <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText=" Account Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("accountno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Holder Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("accountholdername") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankname" runat="server" Text='<%#Eval("BankName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="IFSC Code">
                                        <ItemTemplate>
                                              <asp:Label ID="lblimage" runat="server" Text='<%#Eval("IFSCCode") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QR Code">
                                        <ItemTemplate>
                                           
                                            <asp:Image ID="lblbranchname" runat="server" ImageUrl='<%# "../ProductImage/" + Eval("BranchName") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton style="display:none;" ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton> 
                                            <asp:LinkButton ID="lnkDel" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </div>             
               
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>

                  <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit Bank Account Details</h4>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Name :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="lblbankaccountid" runat="server" Visible="false" Text="0"></asp:Label>
                                        <asp:TextBox ID="txtaccountnoedit" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Address :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtaccholdernameedit" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Remark :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtdepositbankedit" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">IFSC Code :</label>
                                    </div>
                                    <div class="col-sm-3">
                                     
                                           <asp:Image ID="ImageButton1" runat="server" Width="50px" Height="50px"  />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Image Upload :</label>
                                    </div>
                                    <div class="col-sm-3">
                                         <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn green" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            </div>

          
        </ContentTemplate>
          <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
                <asp:PostBackTrigger ControlID = "btnUpdate" />
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
</asp:Content>


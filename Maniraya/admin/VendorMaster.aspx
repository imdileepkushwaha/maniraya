<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=TxtCompanyname.ClientID%>").value == "") {
                alert('Enter Company Name');
                document.getElementById("<%=TxtCompanyname.ClientID%>").focus();
                return false;
            }
           // if (document.getElementById("<%=TxtOwnername.ClientID%>").value == "") {
           //     alert('Enter Owner No');
          //      document.getElementById("<%=TxtOwnername.ClientID%>").focus();
          //      return false;
         //   }
            if (document.getElementById("<%=TxtContactNo.ClientID%>").value == "") {
                alert('Enter Contact No');
                document.getElementById("<%=TxtContactNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtAddress.ClientID%>").value == "") {
                alert('Enter Address ');
                document.getElementById("<%=TxtAddress.ClientID%>").focus();
                return false;
            }
           // if (document.getElementById("<%=TxtGStNo.ClientID%>").value == "") {
          //      alert('Enter GST No');
          //      document.getElementById("<%=TxtGStNo.ClientID%>").focus();
          //      return false;
         //   }
        }

        function validate2() {
            if (document.getElementById("<%=TxtCompanyNameEdit.ClientID%>").value == "") {
                alert('Enter Company Name');
                document.getElementById("<%=TxtCompanyNameEdit.ClientID%>").focus();
                return false;
            }
          //  if (document.getElementById("<%=TxtOwnernameEdit.ClientID%>").value == "") {
            //    alert('Enter Owner No');
            //    document.getElementById("<%=TxtOwnernameEdit.ClientID%>").focus();
            //    return false;
           // }
            if (document.getElementById("<%=TxtConatctNoEdit.ClientID%>").value == "") {
                alert('Enter Contact No');
                document.getElementById("<%=TxtConatctNoEdit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtAddressEdit.ClientID%>").value == "") {
                alert('Enter Address ');
                document.getElementById("<%=TxtAddressEdit.ClientID%>").focus();
                return false;
            }
           // if (document.getElementById("<%=TxtGstNoEdit.ClientID%>").value == "") {
            //    alert('Enter GST No');
           //     document.getElementById("<%=TxtGstNoEdit.ClientID%>").focus();
           //     return false;
           // }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Vendor Master      
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Vendor Master </li>
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
              <h3 class="box-title">Vendor Master</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
              <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Company Name :</label>
                                 <asp:TextBox ID="TxtCompanyname" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Owner Name :</label>
                               <asp:TextBox ID="TxtOwnername" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                    <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>ContactNo :</label>
                                <asp:TextBox ID="TxtContactNo" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Address :</label>
                             <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>GstNo :</label>
                                   <asp:TextBox ID="TxtGStNo" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                
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
                <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                     <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("OwnerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankname" runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblifsccode" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST NO ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbranchname" runat="server" Text='<%#Eval("GstNo") %>'></asp:Label>
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
              <!-- /.box-body -->

           
         
          </div>
            </div>

                  <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit Vendor Details</h4>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Company Name :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="LblVendorId" runat="server" Visible="false" Text="0"></asp:Label>
                                        <asp:TextBox ID="TxtCompanyNameEdit" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Owner Name :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TxtOwnernameEdit" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">ContactNo :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TxtConatctNoEdit" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Address :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TxtAddressEdit" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">GST No :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="TxtGstNoEdit" runat="server" CssClass="form-control" />
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


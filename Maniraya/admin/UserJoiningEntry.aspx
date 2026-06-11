<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="UserJoiningEntry.aspx.cs" Inherits="UserJoiningEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function gettotal() {

            var Quantity = 0, Amount = 0, TotalAMount = 0;
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value != "") {
                Quantity = document.getElementById("<%=TxtQuantity.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtAmount.ClientID%>").value != "") {
                Amount = document.getElementById("<%=TxtAmount.ClientID%>").value;
            }
            var TotalAMount = Quantity * Amount;


            document.getElementById("<%=TxtTotalAmount.ClientID%>").innerText = TotalAMount;
          }
          function gettotal2() {

              var TotalPurchase = 0, CGST = 0, SGST = 0; IGST = 0; CalculateCGST = 0; CalculateSGST = 0; CalculateIGST = 0;; TotalC = 0;
              if (document.getElementById("<%=TxtTotalPrice.ClientID%>").value != "") {
                TotalPurchase = document.getElementById("<%=TxtTotalPrice.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtCGST.ClientID%>").value != "") {
                CGST = document.getElementById("<%=TxtCGST.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtSGST.ClientID%>").value != "") {
                SGST = document.getElementById("<%=TxtSGST.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtIGST.ClientID%>").value != "") {
                IGST = document.getElementById("<%=TxtIGST.ClientID%>").value;
            }

            var CalculateCGST = TotalPurchase * (CGST / 100);
            var CalculateSGST = TotalPurchase * (SGST / 100);
            var CalculateIGST = TotalPurchase * (IGST / 100);
            var TotalC = Number(TotalPurchase) + Number(CalculateCGST) + Number(CalculateSGST) + Number(CalculateIGST);


            document.getElementById("<%=TxtCGstAmount.ClientID%>").innerText = parseFloat(CalculateCGST, 2).toFixed(2);
            document.getElementById("<%=TxtSGstAmount.ClientID%>").innerText = parseFloat(CalculateSGST, 2).toFixed(2);
            document.getElementById("<%=TxtIGstAmount.ClientID%>").innerText = parseFloat(CalculateIGST, 2).toFixed(2);

            document.getElementById("<%=TxtpaybleAmount.ClientID%>").innerText = parseFloat(TotalC, 2).toFixed(2);
            document.getElementById("<%= HDTotal.ClientID %>").value = parseFloat(TotalC, 2).toFixed(2)
        }
        function validate() {
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value == "") {

                alert('Enter Quantity');
                document.getElementById("<%=TxtQuantity.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtsponserid.ClientID%>").value == "") {
                 alert('Select User');
                 document.getElementById("<%=txtsponserid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPurchaseDate.ClientID%>").value == "") {
                 alert('Enter Purchase date');
                 document.getElementById("<%=TxtPurchaseDate.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=DDLstProduct.ClientID%>").value == "0") {
                 alert('Select Product');
                 document.getElementById("<%=DDLstProduct.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPurchaseStock.ClientID%>").value == "") {
                 alert('Enter Purchase Quantity  ');
                 document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPurchaseStock.ClientID%>").value == "0") {
                 alert('Enter Purchase Quantity  ');
                 document.getElementById("<%=TxtPurchaseStock.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtPurchasePrice.ClientID%>").value == "") {
                 alert('Enter Purchase Price');
                 document.getElementById("<%=TxtPurchasePrice.ClientID%>").focus();
              return false;
          }
      }

      function validate2() {
          if (document.getElementById("<%=TxtAmount.ClientID%>").value == "") {
                 alert('Select Amount');
                 document.getElementById("<%=TxtAmount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtMrp.ClientID%>").value == "") {
                 alert('Enter MRP');
                 document.getElementById("<%=TxtMrp.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value == "0") {
                 alert('Select Quantity');
                 document.getElementById("<%=TxtQuantity.ClientID%>").focus();
                return false;
            }
        }
        function validate3() {
            if (document.getElementById("<%=txtsponserid.ClientID%>").value == "") {
                 alert('Select User');
                 document.getElementById("<%=txtsponserid.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtPurchaseDate.ClientID%>").value == "") {
                 alert('Enter Purchase date');
                 document.getElementById("<%=TxtPurchaseDate.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtCGST.ClientID%>").value == "") {
                 alert('Enter CGST');
                 document.getElementById("<%=TxtCGST.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtSGST.ClientID%>").value == "") {
                 alert('Enter SGST');
                 document.getElementById("<%=TxtSGST.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtIGST.ClientID%>").value == "") {
                 alert('Enter IGST');
                 document.getElementById("<%=TxtIGST.ClientID%>").focus();
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Joining Inviove Generate   
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Invoice</a></li>
            <li class="active">Joining Inviove Generate   </li>
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
                            <h3 class="box-title">Joining Inviove Generate </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>UserId :</label>
                                        <asp:TextBox ID="txtsponserid" AutoPostBack="true" OnTextChanged="txtsponserid_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>User name :</label>
                                        <asp:TextBox ID="txtsponsername" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Purchase date :</label>
                                        <asp:TextBox ID="TxtPurchaseDate" runat="server" CssClass="form-control form_date" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("PlanName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plan Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("PlanAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("regdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>


                            </div>
                            <hr />
                            <div class="row" style="display:none">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product :</label>
                                        <asp:DropDownList ID="DDLstProduct" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLstProduct_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:TextBox ID="TxtImage" runat="server" Enabled="false" CssClass="form-control" Visible="false" />

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>AvailableStock :</label>
                                        <asp:TextBox ID="TxtAvailableStock" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Purchase Quantity :</label>
                                        <asp:TextBox ID="TxtPurchaseStock" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="display:none">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Purchase Price :</label>
                                        <asp:TextBox ID="TxtPurchasePrice" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Purchase MRP :</label>
                                        <asp:TextBox ID="TxtPurchaseMRP" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->

                            <div class="box-footer" style="display:none">
                                <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="add" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Remove All" OnClick="btnCancel_Click" />

                            </div>

                        </div>
                    </div>
                    <asp:Panel ID="PnlDt"  runat="server" Visible="false">
                        <div class="col-md-12">

                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Detail</h3>
                                </div>
                                <!-- /.box-header -->
                                <!-- form start -->

                                <div class="box-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("../Productimage/"+Eval("Image"))%>' Height="40px" Width="40px" />
                                                            <asp:Label ID="LblProductImageG" runat="server" Text='<%# string.Format("../Productimage/"+Eval("Image"))%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Code">
                                                        <ItemTemplate>

                                                            <asp:Label ID="LblProductCodeG" runat="server" Text='<%#Eval("ProductId") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProductNameG" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount/Peices">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProductAmountG" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblBv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="label1" runat="server" Text="Total : "></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculate Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDelete" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-remove" aria-hidden="true"></i></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Total Purchase :</label>
                                                    <asp:TextBox ID="TxtTotalPrice" runat="server" CssClass="form-control" ReadOnly="true" />
                                                </div>
                                            </div>


                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>GST %:</label>
                                                    <asp:TextBox ID="TxtCGST" runat="server" CssClass="form-control" TextMode="Number" step="0.00" onchange="gettotal2();" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>GST :</label>
                                                    <asp:Label ID="TxtCGstAmount" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" style="display:none">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>SGST %:</label>
                                                    <asp:TextBox ID="TxtSGST" runat="server" CssClass="form-control" TextMode="Number" Text="0" step="0.00" onchange="gettotal2();" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>CGST :</label>
                                                    <asp:Label ID="TxtSGstAmount" runat="server" Text="0" CssClass="form-control" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" style="display:none">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>IGST %:</label>
                                                    <asp:TextBox ID="TxtIGST" runat="server" Text="0" CssClass="form-control" TextMode="Number" step="0.00" onchange="gettotal2();" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>IGST :</label>
                                                    <asp:Label ID="TxtIGstAmount" runat="server" Text="0" CssClass="form-control" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Payble Amount :</label>
                                                    <asp:Label ID="TxtpaybleAmount" runat="server" CssClass="form-control" />
                                                    <asp:HiddenField ID="HDTotal" runat="server" />
                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                </div>
                                <!-- /.box-body -->

                                <div class="box-footer">
                                    <asp:Button ID="BtnSubmitPurchase" OnClientClick="return validate3();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="BtnSubmitPurchase_Click" />


                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <div id="myModal" class="modal fade">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Edit Product Details</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                                <label>Product Code :</label>
                                                <asp:TextBox ID="TxtProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Product Name :</label>
                                                <asp:TextBox ID="TxtProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Amount/Pieces :</label>
                                                <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" onchange="gettotal();"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>MRP :</label>
                                                <asp:TextBox ID="TxtMrp" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Quantity :</label>
                                                <asp:TextBox ID="TxtQuantity" CssClass="form-control" runat="server" TextMode="Number" onchange="gettotal();"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Total Amount :</label>
                                                <asp:Label ID="TxtTotalAmount" CssClass="form-control" runat="server"></asp:Label>

                                            </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
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


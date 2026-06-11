<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="JoiningPlanAdd.aspx.cs" Inherits="JoiningPlanAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=TxtPlanname.ClientID%>").value == "") {
                alert('Enter Plan Name');
                document.getElementById("<%=TxtPlanname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPlanAmount.ClientID%>").value == "") {
                alert('Enter Plan Amount');
                document.getElementById("<%=TxtPlanAmount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtBV.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=TxtBV.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=TxtMonthlyAmount.ClientID%>").value == "") {
                 alert('Enter Monthly Amount');
                 document.getElementById("<%=TxtMonthlyAmount.ClientID%>").focus();
                return false;
             }
             if (document.getElementById("<%=TxtMonthCount.ClientID%>").value == "") {
                 alert('Enter No of Month');
                 document.getElementById("<%=TxtMonthCount.ClientID%>").focus();
                return false;
            }
        }

        function validate2() {
            if (document.getElementById("<%=TxtPlanNameEdit.ClientID%>").value == "") {
                alert('Enter Plan Name');
                document.getElementById("<%=TxtPlanNameEdit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtPlanAmountEdit.ClientID%>").value == "") {
                alert('Enter Plan Amount');
                document.getElementById("<%=TxtPlanAmountEdit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtBuisnessVolume.ClientID%>").value == "") {
                alert('Enter Bank Name');
                document.getElementById("<%=TxtBuisnessVolume.ClientID%>").focus();
                return false;
            }
          
        }
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }       
      
         function Check_Click(objRef) {
             //Get the Row based on checkbox
             var row = objRef.parentNode.parentNode;
             if (objRef.checked) {
                 //If checked change color to Aqua
                 row.style.backgroundColor = "aqua";
             }
             else {
                 //If not checked change back to original color
                 if (row.rowIndex % 2 == 0) {
                     //Alternating Row Color
                     row.style.backgroundColor = "white";
                 }
                 else {
                     row.style.backgroundColor = "white";
                 }
             }
             //Get the reference of GridView
             var GridView = row.parentNode;
             //Get all input elements in Gridview
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < inputList.length; i++) {
                 //The First element is the Header Checkbox
                 var headerCheckBox = inputList[0];
                 //Based on all or none checkboxes
                 //are checked check/uncheck Header Checkbox
                 var checked = true;
                 if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                     if (!inputList[i].checked) {;
                         break;
                     }
                 }
             }
            // headerCheckBox.checked = checked;
         }
         function checkAll(objRef) {
             var GridView = objRef.parentNode.parentNode.parentNode;
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < inputList.length; i++) {
                 //Get the Cell To find out ColumnIndex
                 var row = inputList[i].parentNode.parentNode;
                 if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                     if (objRef.checked) {
                         //If the header checkbox is checked
                         //check all checkboxes
                         //and highlight all rows
                         row.style.backgroundColor = "aqua";
                         inputList[i].checked = true;
                     }
                     else {
                         //If the header checkbox is checked
                         //uncheck all checkboxes
                         //and change rowcolor back to original 
                         if (row.rowIndex % 2 == 0) {
                             //Alternating Row Color
                             row.style.backgroundColor = "white";
                         }
                         else {
                             row.style.backgroundColor = "white";
                         }
                         inputList[i].checked = false;
                     }
                 }
             }
         }
    </script>
    
     <style>
        label {
        float: left;
  clear: none;
  display: block;
  padding: 2px 1em 0 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Joining Plan Add      
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Joining Plan Add  </li>
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
              <h3 class="box-title">Joining Plan Add </h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
              <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Plan Name :</label>
                                 <asp:TextBox ID="TxtPlanname"  runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Plan Amount :</label>
                               <asp:TextBox ID="TxtPlanAmount" runat="server" onkeypress="return isNumber(event)" CssClass="form-control" />
                                   
                             </div>
                         </div>
                 
                     </div>
                   <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Business Volume :</label>
                                 <asp:TextBox ID="TxtBV" runat="server"   TextMode="Number" Text="0" CssClass="form-control" />
                               
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                <%-- <label>Month Count :</label>--%>
                                    <label>capping Amount :</label>
                               <asp:TextBox ID="TxtMonthCount" runat="server" Visible="false" onkeypress="return isNumber(event)" CssClass="form-control" />
                               <asp:TextBox ID="TxtMonthlyAmount"  runat="server" onkeypress="return isNumber(event)" CssClass="form-control" />
                             </div>
                         </div>
                 
                     </div>
                    <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                               
                                 <asp:CheckBox ID="ChkMoneyTransfer" CssClass="form-control" runat="server" Text="Money Transfer" Visible="false" /> 
                             </div>
                         </div>
                        </div>
                  <div class="row" style="display:none">
                        <div class="col-md-3">
                            </div>
                        <div class="col-md-6">
                     <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>       
                                       <asp:TemplateField HeaderText="">
                                             <HeaderTemplate>
                                                 <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);"/>
                                         </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:CheckBox ID="ChkStatus" runat="server" onclick = "Check_Click(this)"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>                               
                                     <asp:TemplateField HeaderText="S.N.">
                                       
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("TYpeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                              
                                </Columns>
                            </asp:GridView>
                            </div>
                       <div class="col-md-3">
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
                                    <asp:TemplateField HeaderText="Plan Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("PlanName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("Planamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Business Volume">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountno2" runat="server" Text='<%#Eval("BuisnessVolume") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Capping Amt" >
                                        <ItemTemplate>
                                            <asp:Label ID="LblMonthlyAmount" runat="server" Text='<%#Eval("MonthlyAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                       <asp:TemplateField HeaderText="No of Month" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCountMonth" runat="server" Text='<%#Eval("CountMonth") %>'></asp:Label>
                                             <asp:Label ID="LblOperatorMermission" runat="server" Text='<%#Eval("operatorpermission") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                      <asp:TemplateField HeaderText="Money Transfer" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblMoneyTransfer" runat="server" Text='<%#Eval("MoneyTransfer1") %>'></asp:Label>
                                           
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
                            <h4 class="modal-title">Edit Plan Details</h4>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                                    
                                    <div class="col-sm-4">
                                        <label>Plan Name :</label>
                                        <asp:Label ID="lblbankaccountid" runat="server" Visible="false" Text="0"></asp:Label>
                                        <asp:TextBox ID="TxtPlanNameEdit"  runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                         <label>Plan Amount :</label>
                                         <asp:TextBox ID="TxtPlanAmountEdit" TextMode="Number" runat="server" CssClass="form-control" />
                                          <asp:TextBox ID="TxtBuisnessVolume" runat="server" TextMode="Number" Text="0" Visible="false" CssClass="form-control" />
                                    </div>
                                  <div class="col-sm-4">
                                        
                                         <asp:CheckBox ID="ChkMoneyTransferEdit" CssClass="form-control" runat="server" Text="Money Transfer" Visible="false"/> 
                                       </div>
                                </div>
                               <div class="row">
                                    
                                    <div class="col-sm-4">
                                        <label>Business Volume :</label>
                                      
                                         <asp:TextBox ID="TxtBvEdit"  runat="server" CssClass="form-control" />
                                     
                                    </div>
                                    <div class="col-sm-4" >
                                         <label>Capping Amount :</label>
                                       <asp:TextBox ID="TxtMonthlyCountEdit"  runat="server" onkeypress="return isNumber(event)" CssClass="form-control" Visible="false"/>
                                           <asp:TextBox ID="TxtMonthlyAmountEdit"   runat="server" onkeypress="return isNumber(event)" CssClass="form-control" />
                                     
                                    </div>
                                </div>
                            <br />
                              <div class="row" style="display:none">
                                    <div class="col-sm-3">
                                     
                                </div>
                                   <div class="col-sm-6">
                               <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>       
                                       <asp:TemplateField HeaderText="">
                                             <HeaderTemplate>
                                                 <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);"/>
                                         </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:CheckBox ID="ChkStatus" runat="server" onclick = "Check_Click(this)"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>                               
                                     <asp:TemplateField HeaderText="S.N.">
                                       
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("TYpeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccountholdername" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                              
                                </Columns>
                            </asp:GridView>
                                       </div>
                                    <div class="col-sm-3">
                                     
                                </div>
                                  </div>
                              
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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


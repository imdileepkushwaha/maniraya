<%@ Page Title="Withdrawl Request" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="CashRequstAdd.aspx.cs" Inherits="CashRequstAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
        <%--    if (document.getElementById("<%=txtoldpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Old Password');
                document.getElementById("<%=txtoldpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter New Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }--%>

        }
    </script>
      <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
     Transfer To wallet 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Request</a></li>
        <li class="active"> Transfer To wallet </li>
      
      </ol>
    </section>   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">  Transfer To wallet </h3>
            </div>
                   <div class="box-body">
                   
                      
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>PV Balance :</label>
                               <asp:TextBox ID="txtbalance" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Used PV :</label>
                                   <asp:TextBox ID="txtamount"    runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                    
                        
                       </div>
                         <div class="box-footer">
                        
             

                               <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    
              </div>


                  
              </div>
              </div>
                  </div>


                <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">  Transfer To wallet Report </h3>
            </div>
                   <div class="box-body">
                   
                      
                          <div class="row">
                         <div class="col-md-12">
                            <div class="form-group table-responsive">
                                     <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  >
                                    <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date of Request">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcreatingdate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                              <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                  
                                    <asp:TemplateField HeaderText="BV">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                           <asp:TemplateField HeaderText="Admincharge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdmincharge" runat="server" Text='<%#Eval("Admincharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                          <asp:TemplateField HeaderText="TDS Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTDSCharge" runat="server" Text='<%#Eval("TDSCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                           <asp:TemplateField HeaderText="Payble Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaybleAmount" runat="server" Text='<%#Eval("PaybleAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                           
                                    <asp:TemplateField HeaderText="TransactionID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionID" runat="server" Text='<%#Eval("TransactionID") %>'></asp:Label>
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
                  </div>

            
        </ContentTemplate>
        <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">   
 
    <script type="text/javascript">
        function validate() {
        <%--    if (document.getElementById("<%=txtoldpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Old Password');
                document.getElementById("<%=txtoldpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter New Password');
                document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                toastr.warning('Warning', 'Enter Confirm Password');
                document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }--%>
          
        }
    </script>
</asp:Content>




<%@ Page Title="Withdrawl Request" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="WithdrawlRequstAdd.aspx.cs" Inherits="user_WithdrawlRequstAdd" %>


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
      <h1 style="color:white;">
     Withdrawl Request  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home > </a></li>
            <li><a href="#">Withdrawl > </a></li>
        <li class="active">Withdrawl Fund </li>
      
      </ol>
    </section>   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row" style="color:white;">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Withdrawl Request</h3>
            </div>
                   <div class="box-body">
                    <%--<h4> Request Type</h4>
                        <hr />--%>
                        <div class="row" style="display:none">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RDBtnTRecharge" runat="server" Text="Recharge Wallet" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnTRecharge_CheckedChanged"   />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnUtility" runat="server" Text="Utility Wallet" GroupName="A"  AutoPostBack="true" OnCheckedChanged="RdBtnUtility_CheckedChanged" />
                                    </div>
                                </div>
                           </div>
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
                                 <label>Account Balance :</label>
                               <asp:TextBox ID="txtbalance" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Enter Amount :</label>
                                   <asp:TextBox ID="txtamount"    runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" OnTextChanged="txtamount_TextChanged" AutoPostBack="true" />
                             </div>
                         </div>
                     </div>
                       <div class="row">

                            <div class="col-md-6">
                             <div class="form-group">
                                  <label>Admin Percent :</label>
                                   <asp:TextBox ID="txtadminper"   Enabled="false"   runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />

                                                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Admincharge :</label>
                                   <asp:TextBox ID="txtadmincharge"    Enabled="false"  runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />

                                                             </div>
                         </div>
                       
                     </div>

                        <div class="row">
                        
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>TDS Percent :</label>
                               <asp:TextBox ID="txttdsper" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    

                           </div>
                         </div>
                              <div class="col-md-6">
                             <div class="form-group">
                                  <label>TDS Charge :</label>
                               <asp:TextBox ID="txttdscharge" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    

                           </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                                                    <label>Net Amount :</label>
                               <asp:TextBox ID="txttotalamount" Enabled="false" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />    

                                 
                                   
                             </div>
                         </div>
                         <div class="col-md-6" style="display:none">
                             <div class="form-group">
                                <label>Image :</label>

                                 <asp:FileUpload ID="FileUpload1" runat="server" />  
                             </div>
                         </div>
                     </div>
                        
                         <div class="row"  style="display:none">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Image :</label>
                                 <asp:FileUpload ID="ImageUpload" runat="server" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                
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




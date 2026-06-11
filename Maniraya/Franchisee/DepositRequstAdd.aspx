<%@ Page Title="Deposit Request" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="DepositRequstAdd.aspx.cs" Inherits="user_DepositRequstAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">


         function validate() {
                         
            
        if (document.getElementById("<%=txtamount.ClientID%>").value == "") {

                alert('Enter Amount');
                document.getElementById("<%=txtamount.ClientID%>").focus();
                return false;
            }       
           
             if (document.getElementById("<%=ddbankaccountno.ClientID%>").value == "0") {
                 alert('Select Bank Account');
                 document.getElementById("<%=ddbankaccountno.ClientID%>").focus();
                     return false;
                 }

                 if (document.getElementById("<%=ddmode.ClientID%>").value == "Select") {

                 alert('Select Paymentmode');
                 document.getElementById("<%=ddmode.ClientID%>").focus();
                     return false;
                 }
                 if (document.getElementById("<%=TxtTransactionId.ClientID%>").value == "") {

                 alert('Enter TransactionID');
                 document.getElementById("<%=TxtTransactionId.ClientID%>").focus();
                     return false;
                 }

        }
    </script>
     <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
     Deposit Request  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Deposit</a></li>
        <li class="active">Deposit Request </li>
      
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

            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Deposit Request</h3>
            </div>
                   <div class="box-body">
                      
                      
                      
                     
                    
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Select Deposit Account :</label>
                                   <asp:DropDownList ID="ddbankaccountno" AutoPostBack="true" OnSelectedIndexChanged="ddbankaccountno_SelectedIndexChanged"  CssClass="form-control"  runat="server">
                                            <asp:ListItem Value="0">Select Account No</asp:ListItem>
                                        </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Deposit Account No :</label>
                                <asp:TextBox ID="txtdepositaccountno" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Deposit Bank :</label>
                                  <asp:TextBox ID="txtdepositbank" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>IFSC Code :</label>
                                 <asp:TextBox ID="txtifsccode" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Account Holder Name :</label>
                                 <asp:TextBox ID="txtaccountholdername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Branch Name :</label>
                                <asp:TextBox ID="txtbranchname" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                           <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                    <label>Deposit Mode :</label>
                                   <asp:DropDownList ID="ddmode" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Select">Select </asp:ListItem>
                                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                <asp:ListItem Value="RTGS">RTGS</asp:ListItem>
                                                <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                                <asp:ListItem Value="IMPS">IMPS</asp:ListItem>
                                          <asp:ListItem Value="IMPS">THIRD PARTY TRANSFER</asp:ListItem>
                                         <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                            </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                   <label>TransactionId/ChequeNo :</label>
                                 <asp:TextBox ID="TxtTransactionId"  runat="server"  CssClass="form-control" />
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
                                   <asp:TextBox ID="txtamount"    runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                            
                                   <label>Image :</label>
                                 <asp:FileUpload ID="ImageUpload" runat="server" />    
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                   <label>Narration :</label>
                                  <asp:TextBox ID="TxtNarration"  runat="server"  CssClass="form-control" />
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
 
  
</asp:Content>




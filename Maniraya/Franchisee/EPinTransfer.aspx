<%@ Page Title="Transfer E-Pin" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="EPinTransfer.aspx.cs" Inherits="admin_EPinAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function validate() {

             if (document.getElementById("<%=txtuserid.ClientID%>").value == "") {

                   alert('Enter User Id');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtuserid.Text%>").focus();
                   return false;
               }
               if (document.getElementById("<%=txtusername.ClientID%>").value == "") {

                   alert('Enter User Name');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=txtusername.ClientID%>").focus();
                   return false;
               }
             if (document.getElementById("<%=ddplan.ClientID%>").value == "0") {

                 alert('select amount');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=ddplan.ClientID%>").focus();
                   return false;
             }
             if (document.getElementById("<%=txtavailablepins.ClientID%>").value == "") {

                 alert('select amount');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=txtavailablepins.ClientID%>").focus();
                 return false;
             }
           }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
      Transfer E-Pin     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">E-Pin management</a></li>
        <li class="active"> Transfer E-Pin</li>
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
              <h3 class="box-title">Transfer E-Pin</h3>
            </div>

                 <div class="box-body">
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged"  />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User name</label>
                               <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Select Amount :</label>
                               <asp:DropDownList ID="ddplan" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddplan_SelectedIndexChanged"  >
                                     <asp:ListItem Value="0">Select Amount</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Available E-Pin :</label>
                               <asp:TextBox ID="txtavailablepins" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" Enabled="False" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>No Of E-Pin :</label>
                            
                                  <asp:TextBox ID="txtnoofepin" runat="server" onkeypress="return isNumberKey(event);" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Transfer User Id :</label>
                          <asp:TextBox ID="txttransferuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txttransferuserid_TextChanged" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Transfer User Name :</label>
                                     <asp:TextBox ID="txttransferusername" runat="server"  CssClass="form-control" />   
                             </div>
                         </div>
                     </div>
                 </div>
                    <div class="box-footer">
                        
               <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click1" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                    
              </div>

                 </div>
         </div>
                </div>
      
      

    
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
      
</asp:Content>


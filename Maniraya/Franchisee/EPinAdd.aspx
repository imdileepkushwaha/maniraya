<%@ Page Title="Generate E-Pin" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="EPinAdd.aspx.cs" Inherits="admin_EPinAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

                alert('Select plan');
                // alert("Enter Rank No"); 
                document.getElementById("<%=ddplan.ClientID%>").focus();
                   return false;
            }
            if (document.getElementById("<%=txtamount.ClientID%>").value == "") {

                alert('Select plan');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtamount.ClientID%>").focus();
                return false;
            }
           }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
      Generate E-Pin     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">E-Pin management</a></li>
        <li class="active"> Generate E-Pin</li>
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
              <h3 class="box-title">Generate E-Pin</h3>
            </div>

                 <div class="box-body">
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Enter User ID</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" OnTextChanged="txtuserid_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User name</label>
                                 <asp:TextBox ID="txtusername" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Plan</label>
                                  <asp:DropDownList ID="ddplan" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddplan_SelectedIndexChanged" >
                                     <asp:ListItem Value="0">Select Plan</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>E-Pin Amount</label>
                                   <asp:TextBox ID="txtamount" onkeypress="return isNumber(event)" Text="" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>No of Pin</label>
                                <asp:TextBox ID="txtnoofepin" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtnoofepin_TextChanged"></asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FIL1" runat="server" TargetControlID="txtnoofepin" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Total Amount</label>
                                   <asp:TextBox ID="TxtTotalAmount" onkeypress="return isNumber(event)"  Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                    
                 </div>
                    <div class="box-footer" id="divFooter" runat="server">
                          <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
              
                    
              </div>

                 </div>
         </div>
                </div>
      
      

    
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
       
</asp:Content>


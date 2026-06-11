<%@ Page Title="Transfer E-Pin" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="WalletTransfer.aspx.cs" Inherits="WalletTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txttransferuserid.ClientID%>").value == "") {
                  alert('Enter User Id');
                  // alert("Enter Rank No"); 
                  document.getElementById("<%=txttransferuserid.Text%>").focus();
                   return false;
               }
               if (document.getElementById("<%=txttransferusername.ClientID%>").value == "") {
                  alert('Enter User Name');
                  // alert("Enter Rank No"); 
                  document.getElementById("<%=txttransferusername.ClientID%>").focus();
                   return false;
               }
            if (document.getElementById("<%=TXtAmount.ClientID%>").value == "") {
                alert('Enter Amount');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TXtAmount.ClientID%>").focus();
                  return false;
              }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Wallet Transfer
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My Wallet</a></li>
            <li class="active">Wallet Transfer</li>
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
                            <h3 class="box-title">Wallet Transfer</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>User Id :</label>
                                        <asp:TextBox ID="txtuserid" Enabled="false" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtuserid_TextChanged" />
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
                                        <label>Your Balance :</label>
                                        <asp:TextBox ID="txtbalance" Enabled="false"  runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Transfer Amount :</label>
                                        <asp:TextBox ID="TXtAmount"  runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Transfer User ID :</label>
                                        <asp:TextBox ID="txttransferuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txttransferuserid_TextChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Transfer User Name :</label>
                                        <asp:TextBox ID="txttransferusername" runat="server" Enabled="false" CssClass="form-control" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


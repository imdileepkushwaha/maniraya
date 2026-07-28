<%@ Page Title="Transfer E-Pin" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="EPinTransfer.aspx.cs" Inherits="admin_EPinAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtuserid.ClientID%>").value == "") {
                alert('Enter User Id');
                document.getElementById("<%=txtuserid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtusername.ClientID%>").value == "") {
                alert('Enter User Name');
                document.getElementById("<%=txtusername.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddplan.ClientID%>").value == "0") {
                alert('Select amount');
                document.getElementById("<%=ddplan.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtavailablepins.ClientID%>").value == "") {
                alert('Available E-Pin not found');
                document.getElementById("<%=txtavailablepins.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtnoofepin.ClientID%>").value == "") {
                alert('Enter No Of E-Pin');
                document.getElementById("<%=txtnoofepin.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txttransferuserid.ClientID%>").value == "") {
                alert('Enter Transfer User Id');
                document.getElementById("<%=txttransferuserid.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Transfer E-Pin</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">E-Pin Management</a></li>
            <li class="active">Transfer E-Pin</li>
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
                            <h3 class="box-title"><i class="fa fa-exchange"></i> Transfer E-Pin</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Transfer available E-Pins from one member wallet to another member.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-user"></i> From Member</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtuserid.ClientID %>">User ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    OnTextChanged="txtuserid_TextChanged" placeholder="Enter sender user ID" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtusername.ClientID %>">User Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control"
                                                    placeholder="Auto-filled from user ID" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-ticket"></i> Pin Selection</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddplan.ClientID %>">Select Amount</label>
                                            <asp:DropDownList ID="ddplan" AutoPostBack="true" CssClass="form-control" runat="server"
                                                OnSelectedIndexChanged="ddplan_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Amount</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtavailablepins.ClientID %>">Available E-Pin</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-database"></i></span>
                                                <asp:TextBox ID="txtavailablepins" runat="server" onkeypress="return isNumberKey(event);"
                                                    CssClass="form-control" Enabled="False" placeholder="Available pins" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtnoofepin.ClientID %>">No Of E-Pin</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                <asp:TextBox ID="txtnoofepin" runat="server" onkeypress="return isNumberKey(event);"
                                                    CssClass="form-control" placeholder="Enter pins to transfer" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-share"></i> Transfer To</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txttransferuserid.ClientID %>">Transfer User ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txttransferuserid" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    OnTextChanged="txttransferuserid_TextChanged" placeholder="Enter receiver user ID" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txttransferusername.ClientID %>">Transfer User Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txttransferusername" runat="server" CssClass="form-control"
                                                    placeholder="Auto-filled from user ID" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-report-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server"
                                Text="Transfer E-Pin" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click1" CssClass="btn btn-default" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

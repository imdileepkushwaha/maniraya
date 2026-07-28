<%@ Page Title="Generate E-Pin" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="EPinAdd.aspx.cs" Inherits="admin_EPinAdd" %>

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
                alert('Select plan');
                document.getElementById("<%=ddplan.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtamount.ClientID%>").value == "") {
                alert('Enter E-Pin amount');
                document.getElementById("<%=txtamount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtnoofepin.ClientID%>").value == "") {
                alert('Enter No of Pin');
                document.getElementById("<%=txtnoofepin.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Generate E-Pin</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">E-Pin Management</a></li>
            <li class="active">Generate E-Pin</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                            <h3 class="box-title"><i class="fa fa-key"></i> Generate E-Pin</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Generate E-Pins for a member against a selected plan and amount.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-user"></i> Member Details</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtuserid.ClientID %>">User ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txtuserid" AutoPostBack="true" OnTextChanged="txtuserid_TextChanged"
                                                    CssClass="form-control" runat="server" placeholder="Enter user ID"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtusername.ClientID %>">User Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtusername" Enabled="false" CssClass="form-control" runat="server"
                                                    placeholder="Auto-filled from user ID"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-ticket"></i> E-Pin Details</h5>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddplan.ClientID %>">Plan</label>
                                            <asp:DropDownList ID="ddplan" AutoPostBack="true" CssClass="form-control" runat="server"
                                                OnSelectedIndexChanged="ddplan_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Plan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtamount.ClientID %>">E-Pin Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-inr"></i></span>
                                                <asp:TextBox ID="txtamount" onkeypress="return isNumber(event)" Text="" AutoPostBack="true"
                                                    OnTextChanged="txtamount_TextChanged" CssClass="form-control" runat="server"
                                                    placeholder="Enter amount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtnoofepin.ClientID %>">No of Pin</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-hashtag"></i></span>
                                                <asp:TextBox ID="txtnoofepin" onkeypress="return isNumber(event)" CssClass="form-control"
                                                    runat="server" AutoPostBack="true" OnTextChanged="txtnoofepin_TextChanged"
                                                    placeholder="Enter number of pins"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= TxtTotalAmount.ClientID %>">Total Amount</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calculator"></i></span>
                                                <asp:TextBox ID="TxtTotalAmount" onkeypress="return isNumber(event)" Enabled="false"
                                                    CssClass="form-control" runat="server" placeholder="Calculated total"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer admin-report-footer">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server"
                                Text="Generate E-Pin" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

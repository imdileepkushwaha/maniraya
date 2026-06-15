<%@ Page Title="Franchisee Report" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="FranchiseeReport.aspx.cs" Inherits="FranchiseeReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate2() {
            if (document.getElementById("<%=txtnameedit.ClientID%>").value.trim() === "") {
                alert('Enter Name');
                document.getElementById("<%=txtnameedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtmobileedit.ClientID%>").value.trim() === "") {
                alert('Enter Mobile');
                document.getElementById("<%=txtmobileedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtemailedit.ClientID%>").value.trim() === "") {
                alert('Enter Email');
                document.getElementById("<%=txtemailedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtaddressedit.ClientID%>").value.trim() === "") {
                alert('Enter Address');
                document.getElementById("<%=txtaddressedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddcountryedit.ClientID%>").value === "0") {
                alert('Select Country');
                document.getElementById("<%=ddcountryedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddstateedit.ClientID%>").value === "0") {
                alert('Select State');
                document.getElementById("<%=ddstateedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddcityedit.ClientID%>").value === "0") {
                alert('Select City');
                document.getElementById("<%=ddcityedit.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validate3() {
            if (document.getElementById("<%=txtSponsorId.ClientID%>").value.trim() === "") {
                alert('Enter Sponsor ID');
                document.getElementById("<%=txtSponsorId.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtSponsorName.ClientID%>").value.trim() === "") {
                alert('Enter valid Sponsor ID');
                document.getElementById("<%=txtSponsorName.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function showModalsponser() {
            if (window.showAdminModal) {
                showAdminModal("DivSponser");
            }
        }

        function Closepopupsponser() {
            if (window.closeAdminModal) {
                closeAdminModal("DivSponser");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Franchisee Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Franchisee</a></li>
            <li class="active">Franchisee Report</li>
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
                            <h3 class="box-title">Search Criteria</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Search franchisees by contact details, location, tehsil, or registration date range.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-search"></i> Franchisee Search</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtname.ClientID %>">Name</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox ID="txtname" CssClass="form-control" runat="server" placeholder="Enter franchisee name" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtmobile.ClientID %>">Mobile No</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-mobile"></i></span>
                                                <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="Enter mobile number" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtemail.ClientID %>">Email</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-envelope-o"></i></span>
                                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" placeholder="Enter email address" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-calendar"></i> Date &amp; Location</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtfromdate.ClientID %>">From Date</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calendar-o"></i></span>
                                                <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server" placeholder="Select from date" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txttodate.ClientID %>">To Date</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-calendar-check-o"></i></span>
                                                <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server" placeholder="Select to date" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddcountry.ClientID %>">Country</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-globe"></i></span>
                                                <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Country</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddstate.ClientID %>">State</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-map"></i></span>
                                                <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select State</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddcity.ClientID %>">City</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-building-o"></i></span>
                                                <asp:DropDownList ID="ddcity" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select City</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddlsttehsil.ClientID %>">Tehsil</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-map-signs"></i></span>
                                                <asp:DropDownList ID="ddlsttehsil" CssClass="form-control" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select Tehsil</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search Franchisees" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Franchisee List</h3>
                            <div class="box-tools admin-record-filter-tools">
                                <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter" AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem Selected="True">25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                                <span class="admin-record-filter-suffix">records</span>
                                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="admin-export-excel-btn" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick="ExportToExcel" ToolTip="Export to Excel" />
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Franchisee Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusertype" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Password">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpswd" runat="server" Text='<%#Eval("password") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("cityname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdress1" runat="server" Text='<%#Eval("balanceamount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("mentiondate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-Pin Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEpinStatus" runat="server" CommandName="epin" CommandArgument='<%# Eval("userid") %>'
                                                    Text='<%# Eval("epinGenerationStatus").ToString() == "1" ? "Unblock" : "Block" %>'
                                                    CssClass='<%# Eval("epinGenerationStatus").ToString() == "1" ? "Active" : "Deactive" %>'
                                                    ToolTip='<%# "Click to " + (Eval("epinGenerationStatus").ToString() == "1" ? "Block" : "Unblock") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkActiveStatus" runat="server" CommandName="changeStatus" CommandArgument='<%#Eval("userid") %>'
                                                    Text='<%#Eval("activeStatus").ToString() == "1" ? "Active" : "Deactive" %>'
                                                    CssClass='<%#Eval("activeStatus").ToString() == "1" ? "Active" : "Deactive" %>'
                                                    ToolTip='<%# "Click to " + (Eval("activeStatus").ToString() == "1" ? "Deactive" : "Active") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ToolTip="Edit franchisee"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="admin-grid-action-cell" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sponsor">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbSEdit" CommandName="sedt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ToolTip="Edit sponsor"><i class="icon fa fa-users" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="admin-grid-action-cell" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="myModal" class="modal fade admin-modal-scrollable" tabindex="-1" role="dialog" aria-labelledby="franchiseeEditModalTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg admin-franchisee-edit-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="franchiseeEditModalTitle">Edit Franchisee Details</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <div class="row">
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtnameedit.ClientID %>">Name</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="txtnameedit" CssClass="form-control" runat="server" placeholder="Enter name" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtmobileedit.ClientID %>">Mobile</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-mobile"></i></span>
                                            <asp:TextBox ID="txtmobileedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="Enter mobile" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtemailedit.ClientID %>">Email</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-envelope-o"></i></span>
                                            <asp:TextBox ID="txtemailedit" CssClass="form-control" runat="server" placeholder="Enter email" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddgenderedit.ClientID %>">Gender</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-venus-mars"></i></span>
                                            <asp:DropDownList ID="ddgenderedit" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="<%= txtaddressedit.ClientID %>">Address</label>
                                        <div class="admin-input-group admin-textarea-group">
                                            <span class="admin-input-icon"><i class="fa fa-map-marker"></i></span>
                                            <asp:TextBox ID="txtaddressedit" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server" placeholder="Enter address" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddcountryedit.ClientID %>">Country</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-globe"></i></span>
                                            <asp:DropDownList ID="ddcountryedit" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountryedit_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select Country</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddstateedit.ClientID %>">State</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-map"></i></span>
                                            <asp:DropDownList ID="ddstateedit" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstateedit_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select State</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddcityedit.ClientID %>">City</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-building-o"></i></span>
                                            <asp:DropDownList ID="ddcityedit" AutoPostBack="true" OnSelectedIndexChanged="ddcityedit_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select City</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddltehsiledit.ClientID %>">Tehsil</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-map-signs"></i></span>
                                            <asp:DropDownList ID="ddltehsiledit" AutoPostBack="true" OnSelectedIndexChanged="ddltehsiledit_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select Tehsil</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= ddlmarketedit.ClientID %>">Market</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-shopping-bag"></i></span>
                                            <asp:DropDownList ID="ddlmarketedit" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select Market</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtpincodeedit.ClientID %>">Pincode</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-map-pin"></i></span>
                                            <asp:TextBox ID="txtpincodeedit" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" placeholder="Enter pincode" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtdateofbirthedit.ClientID %>">Date of Birth</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-birthday-cake"></i></span>
                                            <asp:TextBox ID="txtdateofbirthedit" CssClass="form-control form_date" runat="server" placeholder="Select date of birth" />
                                        </div>
                                    </div>
                                </div>
                                <asp:TextBox ID="ddareaedit" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Franchisee" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivSponser" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="sponsorEditModalTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="sponsorEditModalTitle">Edit Sponsor Details</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-product-form">
                            <div class="row">
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtFranchiseeid.ClientID %>">Franchisee ID</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                            <asp:TextBox ID="TxtFranchiseeid" CssClass="form-control" runat="server" Enabled="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtFranchiseename.ClientID %>">Franchisee Name</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="TxtFranchiseename" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= DDLstFranchiseeType.ClientID %>">Franchisee Type</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-sitemap"></i></span>
                                            <asp:DropDownList ID="DDLstFranchiseeType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtSponsorId.ClientID %>">Sponsor ID</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-link"></i></span>
                                            <asp:TextBox ID="txtSponsorId" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtSponsorId_TextChanged" placeholder="Enter sponsor ID" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= txtSponsorName.ClientID %>">Sponsor Name</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-user-circle"></i></span>
                                            <asp:TextBox ID="txtSponsorName" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto filled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <div class="form-group">
                                        <label for="<%= TxtType.ClientID %>">Sponsor Type</label>
                                        <div class="admin-input-group">
                                            <span class="admin-input-icon"><i class="fa fa-tag"></i></span>
                                            <asp:TextBox ID="TxtType" Enabled="false" CssClass="form-control" runat="server" placeholder="Auto filled" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button ID="Btnsponserupdate" runat="server" Text="Update Sponsor" OnClientClick="return validate3();" CssClass="btn btn-primary" OnClick="Btnsponserupdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function initFranchiseeDatepicker() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy'
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        Sys.Application.add_load(initFranchiseeDatepicker);
    </script>
</asp:Content>

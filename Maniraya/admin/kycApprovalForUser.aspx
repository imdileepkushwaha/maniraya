<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="kycApprovalForUser.aspx.cs" Inherits="admin_kycApprovalForUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>KYC Approval</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">User</a></li>
            <li class="active">Approve KYC</li>
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
                            <h3 class="box-title">Search Criteria</h3>
                        </div>

                        <div class="box-body admin-product-form">
                            <p class="admin-product-intro">Search users and review submitted KYC documents — sign-up form, PAN, cancelled cheque/passbook, and Aadhaar.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-search"></i> User Search</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtname.ClientID %>">User ID</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-id-badge"></i></span>
                                                <asp:TextBox ID="txtname" CssClass="form-control" runat="server" placeholder="Enter user ID" />
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
                                            <label for="<%= txtemail.ClientID %>">Email ID</label>
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
                                            <label for="<%= ddarea.ClientID %>">Area</label>
                                            <div class="admin-input-group">
                                                <span class="admin-input-icon"><i class="fa fa-location-arrow"></i></span>
                                                <asp:DropDownList ID="ddarea" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select Area</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer admin-product-footer">
                            <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search KYC Records" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">KYC Documents</h3>
                            <div class="box-tools admin-record-filter-tools">
                                <label for="<%= ddlRecordFilter.ClientID %>" class="admin-record-filter-label">Show</label>
                                <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control admin-record-filter" AutoPostBack="true" OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                                <span class="admin-record-filter-suffix">per page</span>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="form-group table-responsive admin-kyc-grid-wrap">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable admin-kyc-grid" Width="100%"
                                    AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False">
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" />
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
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sign Up Form">
                                            <ItemTemplate>
                                                <div class="admin-kyc-image-cell">
                                                    <asp:ImageButton ID="imgSignUpForm" runat="server" AlternateText="Sign up form" CommandName="openSignUpImg" CssClass="admin-kyc-thumb" ImageUrl='<%# "../ProductImage/" + Eval("SignUpFormImage") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sign Up Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSignUpStatus" runat="server" Text='<%# Eval("SignUpImgStatuss") %>' CssClass='<%# Eval("SignUpImgStatuss") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action for Sign Up">
                                            <ItemTemplate>
                                                <div class="admin-kyc-actions">
                                                    <asp:LinkButton ID="lnkApproveSignUp" runat="server" OnClientClick="return confirm('Sure to Approve Sign Up Form?');" Text="Approve" CommandName="approve_signup" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-approve"
                                                        Visible='<%# Eval("SignUpFormImage").ToString() != "" ? Eval("SignUpImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkRejectSignUp" runat="server" OnClientClick="return confirm('Sure to Reject Sign Up Form?');" Text="Reject" CommandName="reject_signup" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-reject"
                                                        Visible='<%# Eval("SignUpFormImage").ToString() != "" ? Eval("SignUpImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PAN Card">
                                            <ItemTemplate>
                                                <div class="admin-kyc-image-cell">
                                                    <asp:ImageButton ID="imgPANCard" runat="server" AlternateText="PAN card" CommandName="openPANImg" CssClass="admin-kyc-thumb" ImageUrl='<%# "../ProductImage/" + Eval("PanImage") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN Card Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPanStatus" runat="server" Text='<%# Eval("PanImgStatuss") %>' CssClass='<%# Eval("PanImgStatuss") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action for PAN">
                                            <ItemTemplate>
                                                <div class="admin-kyc-actions">
                                                    <asp:LinkButton ID="lnkApprovePan" runat="server" OnClientClick="return confirm('Sure to Approve PAN Card?');" Text="Approve" CommandName="approve_pan" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-approve"
                                                        Visible='<%# Eval("PanImage").ToString() != "" ? Eval("PanImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkRejectPan" runat="server" Text="Reject" OnClientClick="return confirm('Sure to Reject PAN Card?');" CommandName="reject_pan" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-reject"
                                                        Visible='<%# Eval("PanImage").ToString() != "" ? Eval("PanImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cancel Cheque/Passbook">
                                            <ItemTemplate>
                                                <div class="admin-kyc-image-cell">
                                                    <asp:ImageButton ID="imgCheque" runat="server" AlternateText="Cancel cheque" CommandName="openChequeImg" CssClass="admin-kyc-thumb" ImageUrl='<%# "../ProductImage/" + Eval("CancelCheque") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Cheque/Passbook Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheque" runat="server" Text='<%# Eval("ChequeImgStatuss") %>' CssClass='<%# Eval("ChequeImgStatuss") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action for Cheque">
                                            <ItemTemplate>
                                                <div class="admin-kyc-actions">
                                                    <asp:LinkButton ID="lnkApproveCheque" runat="server" OnClientClick="return confirm('Sure to Approve Cancel Cheque/Passbook?');" Text="Approve" CommandName="approve_cheque" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-approve"
                                                        Visible='<%# Eval("CancelCheque").ToString() != "" ? Eval("ChequeImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkRejectCheque" runat="server" OnClientClick="return confirm('Sure to Reject Cancel Cheque/Passbook?');" Text="Reject" CommandName="reject_cheque" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-reject"
                                                        Visible='<%# Eval("CancelCheque").ToString() != "" ? Eval("ChequeImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Aadhaar Card">
                                            <ItemTemplate>
                                                <div class="admin-kyc-image-cell admin-kyc-image-cell-dual">
                                                    <asp:ImageButton ID="imgAadhaar" runat="server" AlternateText="Aadhaar front" CommandName="openAadhaarImg" CssClass="admin-kyc-thumb"
                                                        ImageUrl='<%# "../ProductImage/" + Eval("AadharImage") %>' />
                                                    <asp:ImageButton ID="imgAadhaarBack" runat="server" AlternateText="Aadhaar back" CommandName="openAadhaarImgBack" CssClass="admin-kyc-thumb"
                                                        ImageUrl='<%# "../ProductImage/" + Eval("AadharImageBack") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aadhaar Card Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAadhaarStatus" runat="server" Text='<%# Eval("AadharImgStatuss") %>' CssClass='<%# Eval("AadharImgStatuss") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action for Aadhaar Card">
                                            <ItemTemplate>
                                                <div class="admin-kyc-actions">
                                                    <asp:LinkButton ID="lnkApproveAadhaar" runat="server" OnClientClick="return confirm('Sure to Approve Aadhaar Card?');" Text="Approve" CommandName="approve_aadhaar" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-approve"
                                                        Visible='<%# Eval("AadharImage").ToString() != "" ? Eval("AadharImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkRejectAadhaar" runat="server" OnClientClick="return confirm('Sure to Reject Aadhaar Card?');" Text="Reject" CommandName="reject_aadhaar" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-reject"
                                                        Visible='<%# Eval("AadharImage").ToString() != "" ? Eval("AadharImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST" Visible="false">
                                            <ItemTemplate>
                                                <div class="admin-kyc-image-cell">
                                                    <asp:ImageButton ID="imgGSTCard" runat="server" AlternateText="GST document" CommandName="opengstImg" CssClass="admin-kyc-thumb" ImageUrl='<%# "../ProductImage/" + Eval("gstImage") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGstStatus" runat="server" Text='<%# Eval("IsGstApplicable") %>' CssClass='<%# Eval("IsGstApplicable") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action for GST" Visible="false">
                                            <ItemTemplate>
                                                <div class="admin-kyc-actions">
                                                    <asp:LinkButton ID="lnkApproveGST" runat="server" OnClientClick="return confirm('Sure to Approve GST?');" Text="Approve" CommandName="approve_GST" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-approve"
                                                        Visible='<%# Eval("gstImage").ToString() != "" ? Eval("IsGstApplicable").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkRejectGST" runat="server" Text="Reject" OnClientClick="return confirm('Sure to Reject GST?');" CommandName="reject_GST" CommandArgument='<%#Eval("userid") %>' CssClass="admin-kyc-btn admin-kyc-btn-reject"
                                                        Visible='<%# Eval("gstImage").ToString() != "" ? Eval("IsGstApplicable").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" CommandArgument='<%#Eval("userid") %>' OnClick="lnkedit_click" CssClass="admin-kyc-btn admin-kyc-btn-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="DivPhotolarge" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="kycPreviewModalTitle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="kycPreviewModalTitle">Document Preview</h4>
                            <button type="button" class="close" onclick="Closepopup();" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body admin-kyc-preview-body">
                            <asp:Image ID="ImageLarge" runat="server" CssClass="admin-kyc-preview-img" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" onclick="Closepopup();">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        function bindKycDatePickers() {
            $('.form_date').datepicker({
                format: 'dd/M/yyyy'
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        }

        Sys.Application.add_load(bindKycDatePickers);

        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false });
        }

        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>
</asp:Content>

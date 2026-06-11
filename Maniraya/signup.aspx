<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function signupTogglePassword(fieldId, btn, label) {
            var input = document.getElementById(fieldId);
            if (!input) return;
            var show = input.type === "password";
            input.type = show ? "text" : "password";
            if (!btn) return;
            btn.classList.toggle("is-password-visible", show);
            btn.setAttribute("aria-label", show ? ("Hide " + label) : ("Show " + label));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="signup-page-shell">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">


            <contenttemplate>

                <main class="signup-page-main">
                    <section class="auth-section">
                        <div class="container">
                            <div class="auth-wrap auth-wrap-signup">
                            <article class="auth-form-card auth-form-card-full">
                                <div class="auth-form-top">
                                    <div class="auth-form-header">
                                        <span class="auth-form-icon" aria-hidden="true">
                                            <svg width="22" height="22" viewBox="0 0 24 24" fill="none"><path d="M15 12C17.21 12 19 10.21 19 8C19 5.79 17.21 4 15 4C12.79 4 11 5.79 11 8C11 10.21 12.79 12 15 12ZM6 20V18C6 15.79 9.58 14 15 14C20.42 14 24 15.79 24 18V20H6Z" fill="currentColor" transform="scale(0.85) translate(2,2)"/></svg>
                                        </span>
                                        <div>
                                            <h2>Create Your Account</h2>
                                            <p class="auth-form-subtitle">Complete the form below to join Maniraya and start shopping.</p>
                                        </div>
                                    </div>
                                </div>
                                <div id="loginForm" class="auth-form signup-form-pattern">

                                    <div class="signup-progress" aria-label="Registration steps">
                                        <div class="signup-progress-item is-active">
                                            <span class="signup-progress-num">1</span>
                                            <span class="signup-progress-label">Sponsor</span>
                                        </div>
                                        <div class="signup-progress-line" aria-hidden="true"></div>
                                        <div class="signup-progress-item">
                                            <span class="signup-progress-num">2</span>
                                            <span class="signup-progress-label">Personal</span>
                                        </div>
                                        <div class="signup-progress-line" aria-hidden="true"></div>
                                        <div class="signup-progress-item">
                                            <span class="signup-progress-num">3</span>
                                            <span class="signup-progress-label">Security</span>
                                        </div>
                                    </div>

                                    <section class="content signup-form-body">
                                        <div class="signup-step-card">
                                            <div class="signup-step-head">
                                                <span class="signup-step-icon" aria-hidden="true">
                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="currentColor"><path d="M16 11C17.66 11 19 9.66 19 8C19 6.34 17.66 5 16 5C14.34 5 13 6.34 13 8C13 9.66 14.34 11 16 11ZM8 13C5.67 13 2 14.17 2 16.5V19H14V16.5C14 14.17 10.33 13 8 13ZM15 12.08C17.84 12.56 20 14.58 20 17V19H22V17C22 14.09 18.67 12.42 15 12.08Z"/></svg>
                                                </span>
                                                <div>
                                                    <h3>Sponsor Information</h3>
                                                    <p>Enter your sponsor details to get started.</p>
                                                </div>
                                            </div>
                                            <div class="signup-step-grid">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Sponsor ID</label>
                                                    <div class="signup-input-wrap">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M10 6H5C3.9 6 3 6.9 3 8V19C3 20.1 3.9 21 5 21H19C20.1 21 21 20.1 21 19V8C21 6.9 20.1 6 19 6H14M10 6V4C10 2.9 10.9 2 12 2C13.1 2 14 2.9 14 4V6M10 6H14" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtsponserid" AutoPostBack="true" OnTextChanged="txtsponserid_TextChanged" CssClass="form-control signup-control signup-control-icon" runat="server" placeholder="Enter sponsor ID"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Sponsor Name</label>
                                                    <div class="signup-input-wrap">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 12C14.21 12 16 10.21 16 8C16 5.79 14.21 4 12 4C9.79 4 8 5.79 8 8C8 10.21 9.79 12 12 12ZM12 14C9.33 14 4 15.34 4 18V20H20V18C20 15.34 14.67 14 12 14Z" fill="currentColor"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtsponsername" Enabled="false" CssClass="form-control signup-control signup-control-icon is-readonly" runat="server" placeholder="Auto-filled sponsor name"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="display: none">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                    <asp:TextBox ID="txtparentname" Enabled="false" CssClass="form-control" runat="server" placeholder="Parental Name"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="display: none">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                    <asp:TextBox ID="txtparentid" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtparentid_TextChanged" placeholder="Parental ID"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <!--Body-->
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-key text-primary"></i></div>
                                                        <asp:TextBox ID="txtepin" CssClass="form-control" runat="server" placeholder="E-Pin" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-inr text-primary"></i></div>
                                                        <asp:TextBox ID="txtamount" Enabled="false" CssClass="form-control" runat="server" placeholder="Amount"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-row" style="display: none;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:RadioButton ID="RdBtnFree" runat="server" Text="Free Regitration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnFree_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:RadioButton ID="RdBtnEpin" runat="server" Text="E-Pin Regitration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnEpin_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlpin" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Select Plan :</label>
                                                        <asp:DropDownList ID="DDLstPlan" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLstPlan_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Select E-Pin :</label>
                                                        <asp:DropDownList ID="ddepin" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddepin_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                        </asp:Panel>
                                        <div class="row" style="display: none">
                                            <div class="col-md-1">
                                                <div class="form-group custom-radio">
                                                    <asp:RadioButton ID="RdBtnLeft" runat="server" Text="Left" GroupName="B" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group custom-radio">
                                                    <asp:RadioButton ID="RdBtnRight" runat="server" Text="Right" GroupName="B" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="text-start">Select Position :</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                        <asp:DropDownList ID="ddposition" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0">Select Position</asp:ListItem>
                                                            <asp:ListItem Value="Left">Left</asp:ListItem>
                                                            <asp:ListItem Value="Right">Right</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="signup-step-card">
                                            <div class="signup-step-head">
                                                <span class="signup-step-icon" aria-hidden="true">
                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="currentColor"><path d="M12 12C14.21 12 16 10.21 16 8C16 5.79 14.21 4 12 4C9.79 4 8 5.79 8 8C8 10.21 9.79 12 12 12ZM12 14C9.33 14 4 15.34 4 18V20H20V18C20 15.34 14.67 14 12 14Z"/></svg>
                                                </span>
                                                <div>
                                                    <h3>Personal Details</h3>
                                                    <p>Tell us a bit about yourself.</p>
                                                </div>
                                            </div>
                                            <div class="signup-step-grid">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Full Name</label>
                                                    <div class="signup-name-group">
                                                        <asp:DropDownList ID="ddpp" CssClass="form-control signup-control signup-prefix" runat="server">
                                                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                            <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="signup-name-input-wrap">
                                                            <span class="signup-input-icon" aria-hidden="true">
                                                                <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 12C14.21 12 16 10.21 16 8C16 5.79 14.21 4 12 4C9.79 4 8 5.79 8 8C8 10.21 9.79 12 12 12ZM12 14C9.33 14 4 15.34 4 18V20H20V18C20 15.34 14.67 14 12 14Z" fill="currentColor"/></svg>
                                                            </span>
                                                            <asp:TextBox ID="txtname" CssClass="form-control signup-control signup-control-icon" runat="server" placeholder="Enter your full name"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Gender</label>
                                                    <div class="signup-input-wrap">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 3V9M12 3L9 6M12 3L15 6M7 10H17C18.1 10 19 10.9 19 12V20C19 21.1 18.1 22 17 22H7C5.9 22 5 21.1 5 20V12C5 10.9 5.9 10 7 10Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control signup-control signup-control-icon" runat="server">
                                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Email Address</label>
                                                    <div class="signup-input-wrap">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M4 6H20V18H4V6ZM4 8L12 13L20 8" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtemail" CssClass="form-control signup-control signup-control-icon" runat="server" placeholder="you@example.com"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Mobile Number</label>
                                                    <div class="signup-input-wrap">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M6.6 3H8.4L9.2 7.8L7.6 8.8C8.5 11.1 9.9 13.5 12.2 15.8L13.2 14.2L18 15V16.8C18 17.5 17.5 18 16.8 18.1C10.1 18.8 5.2 13.9 5.9 7.2C6 6.5 6.5 6 6.6 3Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control signup-control signup-control-icon" runat="server" maxlength="10" placeholder="10-digit mobile number"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="signup-field">
                                                    <label class="signup-label">Date of Birth</label>
                                                    <div class="signup-dob-grid row g-2">
                                                        <div class="col-md-4 dvRow">
                                                            <div class="signup-input-wrap">
                                                                <span class="signup-input-icon" aria-hidden="true">
                                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M7 3V6M17 3V6M4 9H20M6 6H18C19.1 6 20 6.9 20 8V20C20 21.1 19.1 22 18 22H6C4.9 22 4 21.1 4 20V8C4 6.9 4.9 6 6 6Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                                </span>
                                                                <asp:DropDownList ID="ddlYear" CssClass="form-control signup-control signup-control-icon signup-dob-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 dvRow">
                                                            <div class="signup-input-wrap">
                                                                <span class="signup-input-icon" aria-hidden="true">
                                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M8 2V5M16 2V5M4 8H20M6 5H18C19.1 5 20 5.9 20 7V19C20 20.1 19.1 21 18 21H6C4.9 21 4 20.1 4 19V7C4 5.9 4.9 5 6 5Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/><path d="M8 12H8.01M12 12H12.01M16 12H16.01" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"/></svg>
                                                                </span>
                                                                <asp:DropDownList ID="ddlMonth" CssClass="form-control signup-control signup-control-icon signup-dob-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 dvRow">
                                                            <div class="signup-input-wrap">
                                                                <span class="signup-input-icon" aria-hidden="true">
                                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M7 3V6M17 3V6M5 6H19C20.1 6 21 6.9 21 8V19C21 20.1 20.1 21 19 21H5C3.9 21 3 20.1 3 19V8C3 6.9 3.9 6 5 6Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/><circle cx="12" cy="15" r="2" fill="currentColor"/></svg>
                                                                </span>
                                                                <asp:DropDownList ID="ddlDay" CssClass="form-control signup-control signup-control-icon signup-dob-control" ToolTip="Day" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none;">
                                                    <label>Date Of Birth(dd/MM/yyyy)</label>


                                                    <asp:TextBox ID="txtdob" CssClass="form-control form_date" runat="server" Placeholder="dd/MM/yyyy"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Address :</label>
                                                    <asp:TextBox ID="TextBox3" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Select Country :</label>
                                                    <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select Country</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Select State :</label>
                                                    <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select State</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Select City :</label>
                                                    <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select City</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Pincode :</label>
                                                    <asp:TextBox ID="TextBox2" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" Placeholder="Pincode"></asp:TextBox>

                                                </div>
                                            </div>



                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Nominee Name :</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                        <asp:TextBox ID="txtnomineename" placeholder="Nominee Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6" style="display: none">
                                                <div class="form-group">
                                                    <label class="text-start">Select Relation :</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                        <asp:DropDownList ID="ddrelation" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0">Select Relation</asp:ListItem>
                                                            <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                                            <asp:ListItem Value="Wife">Wife</asp:ListItem>
                                                            <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                                            <asp:ListItem Value="Father">Father</asp:ListItem>
                                                            <asp:ListItem Value="Son">Son</asp:ListItem>
                                                            <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                            <asp:ListItem Value="Brother">Brother</asp:ListItem>
                                                            <asp:ListItem Value="Cousin">Cousin</asp:ListItem>
                                                            <asp:ListItem Value="Uncle">Uncle</asp:ListItem>
                                                            <asp:ListItem Value="Aunt">Aunt</asp:ListItem>
                                                            <asp:ListItem Value="Brother-In-Law">Brother-In-Law</asp:ListItem>
                                                            <asp:ListItem Value="Mother-In-Law">Mother-In-Law</asp:ListItem>
                                                            <asp:ListItem Value="Sister-In-Law">Sister-In-Law</asp:ListItem>
                                                            <asp:ListItem Value="Father-In-Law">Father-In-Law</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Adhar Number :</label>
                                                    <asp:TextBox ID="txtaadhar" CssClass="form-control" runat="server" placeholder="Aadhar Number" MaxLength="12"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>



                                        <div class="row">


                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                        <asp:TextBox ID="txtheight" CssClass="form-control" runat="server" placeholder="Name"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="Name"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Select Gender:</label>
                                                    <asp:DropDownList ID="ddgender" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Pan Number:</label>
                                                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control" placeholder="Pan Card Number"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6" style="display: none;">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                    <asp:TextBox ID="txtaccountholdername" placeholder="Account Holder Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br>
                                            <div class="col-md-6" style="display: none;">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                                    <asp:TextBox ID="txtaccountno" placeholder="Account Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="display: none;">
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-light"><i class="fa fa-tag prefix text-primary"></i></div>
                                                    <asp:TextBox ID="txtifsccode" Placeholder="IFSC Code" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddbank" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Bank</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="display: none;">
                                        </div>


                                        <div class="row" style="display: none">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="row" style="display: none">



                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" Placeholder="Pincode"></asp:TextBox>
                                                    <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" Placeholder="Area" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">


                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Mobile Number:</label>
                                                    <asp:TextBox ID="txt123mobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" maxlength="10" placeholder="Mobile No"></asp:TextBox>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Nominee Name:</label>
                                                    <asp:TextBox ID="txt123nomineename" placeholder="Nominee Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label>Nominee Date of Birth : Year-Month-Date</label>


                                                    <fieldset class="row">
                                                        <div class="col-md-4 dvRow">
                                                            <asp:DropDownList ID="ddlYear2" CssClass="form-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged2">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4 dvRow">
                                                            <asp:DropDownList ID="ddlMonth2" CssClass="form-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged2">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4 dvRow">
                                                            <asp:DropDownList ID="ddlDay2" CssClass="form-control" ToolTip="Day" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="form-group" style="display: none;">
                                                    <label>Nominee Date Of Birth(dd/MM/yyyy)</label>


                                                    <asp:TextBox ID="txtdob2" CssClass="form-control form_date" runat="server" Placeholder="dd/MM/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="display: none;">
                                                <div class="form-group">
                                                    <label class="text-end">Nominee Relation:</label>
                                                    <asp:DropDownList ID="dd132relation" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Relation</asp:ListItem>
                                                        <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                                        <asp:ListItem Value="Wife">Wife</asp:ListItem>
                                                        <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                                        <asp:ListItem Value="Father">Father</asp:ListItem>
                                                        <asp:ListItem Value="Son">Son</asp:ListItem>
                                                        <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                        <asp:ListItem Value="Brother">Brother</asp:ListItem>
                                                        <asp:ListItem Value="Sister">Sister</asp:ListItem>
                                                        <asp:ListItem Value="Father-In-Law">Father-In-Law</asp:ListItem>
                                                        <asp:ListItem Value="Mother-In-Law">Mother-In-Law</asp:ListItem>
                                                        <asp:ListItem Value="Other">Other</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                            </div>
                                        </div>

                                        <div class="signup-step-card">
                                            <div class="signup-step-head">
                                                <span class="signup-step-icon is-security" aria-hidden="true">
                                                    <svg width="18" height="18" viewBox="0 0 24 24" fill="currentColor"><path d="M17 10V8C17 5.24 14.76 3 12 3C9.24 3 7 5.24 7 8V10H5V20H19V10H17ZM9 8C9 6.34 10.34 5 12 5C13.66 5 15 6.34 15 8V10H9V8Z"/></svg>
                                                </span>
                                                <div>
                                                    <h3>Account Security</h3>
                                                    <p>Create a strong password to protect your account.</p>
                                                </div>
                                            </div>
                                            <div class="signup-step-grid">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Password</label>
                                                    <div class="signup-input-wrap password-field">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M17 10V8C17 5.24 14.76 3 12 3C9.24 3 7 5.24 7 8V10H5V20H19V10H17ZM9 8C9 6.34 10.34 5 12 5C13.66 5 15 6.34 15 8V10H9V8Z" fill="currentColor"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtuserpassword" TextMode="Password" CssClass="form-control signup-control signup-control-icon" runat="server" placeholder="Create password"></asp:TextBox>
                                                        <button type="button" class="password-toggle" aria-label="Show password" onclick="signupTogglePassword('<%= txtuserpassword.ClientID %>', this, 'password')">
                                                            <span class="eye-icon eye-open" aria-hidden="true">
                                                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round"/><circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"/></svg>
                                                            </span>
                                                            <span class="eye-icon eye-close" aria-hidden="true">
                                                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M3 4L21 20" stroke="currentColor" stroke-width="1.7" stroke-linecap="round"/><path d="M9.9 6.3C10.58 6.1 11.28 6 12 6C16.36 6 20.03 8.56 21.34 12.42C20.95 13.56 20.35 14.59 19.58 15.47M14.12 14.18C13.57 14.7 12.84 15 12 15C10.34 15 9 13.66 9 12C9 11.14 9.37 10.37 9.96 9.82M6.22 8.15C4.74 9.14 3.59 10.62 2.85 12.42C4.16 16.28 7.84 18.84 12.2 18.84C13.65 18.84 15.02 18.56 16.25 18.04" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                            </span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="signup-field">
                                                    <label class="signup-label">Confirm Password</label>
                                                    <div class="signup-input-wrap password-field">
                                                        <span class="signup-input-icon" aria-hidden="true">
                                                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 15V17M8 11V8C8 5.79 9.79 4 12 4C14.21 4 16 5.79 16 8V11M6 11H18V20H6V11Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                        </span>
                                                        <asp:TextBox ID="txtconfirmpassword" TextMode="Password" CssClass="form-control signup-control signup-control-icon" runat="server" placeholder="Re-enter password"></asp:TextBox>
                                                        <button type="button" class="password-toggle" aria-label="Show confirm password" onclick="signupTogglePassword('<%= txtconfirmpassword.ClientID %>', this, 'confirm password')">
                                                            <span class="eye-icon eye-open" aria-hidden="true">
                                                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round"/><circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"/></svg>
                                                            </span>
                                                            <span class="eye-icon eye-close" aria-hidden="true">
                                                                <svg width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M3 4L21 20" stroke="currentColor" stroke-width="1.7" stroke-linecap="round"/><path d="M9.9 6.3C10.58 6.1 11.28 6 12 6C16.36 6 20.03 8.56 21.34 12.42C20.95 13.56 20.35 14.59 19.58 15.47M14.12 14.18C13.57 14.7 12.84 15 12 15C10.34 15 9 13.66 9 12C9 11.14 9.37 10.37 9.96 9.82M6.22 8.15C4.74 9.14 3.59 10.62 2.85 12.42C4.16 16.28 7.84 18.84 12.2 18.84C13.65 18.84 15.02 18.56 16.25 18.04" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round"/></svg>
                                                            </span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-row" style="display: none;">
                                            <div class="form-group col-md-6">
                                            </div>
                                            <div class="form-group col-md-6">
                                                <div class="col-md-4 dvRow">
                                                </div>
                                                <div class="col-md-4 dvRow">
                                                </div>
                                                <div class="col-md-4 dvRow">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-12" style="margin-bottom: 0 !important;">
                                            </div>
                                        </div>

                                        <div class="signup-terms-box">
                                            <asp:CheckBox ID="CheckBox1" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" runat="server" />
                                            <span>I agree to the <a href="Terms_Conditions.html" target="_blank">Terms &amp; Conditions</a></span>
                                        </div>
                                            </div>
                                        </div>

                                        <div class="auth-form-actions signup-form-actions">
                                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="auth-submit-btn signup-submit-btn" runat="server" Text="Create Account" OnClick="btnSubmit_Click" Enabled="false" />
                                            <p class="auth-signup-text">
                                                Already have an account?
                                                <a href="Login.aspx" class="auth-signup-link">Sign In</a>
                                            </p>
                                        </div>
                                    </section>
                                </div>
                            </article>
                            </div>
                        </div>
                    </section>
                </main>




            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


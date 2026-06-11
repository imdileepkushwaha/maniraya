<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="UserAutoPoolIncomeReport.aspx.cs" Inherits="admin_UserAutoPoolIncomeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .Approved {
            background-color: #127113;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Pending {
            background-color: #1596ab;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Rejected {
            background-color: #c91d1d;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>User ID</label>
                                       <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>Mobile No</label>
                                      <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>Email Id</label>
                                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>From date</label>
                                       <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group" style="display:none;">
                                        <label>To date</label>
                                         <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                          <label>Country</label>
                                         <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                            </div>

                             <div class="row">

                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>State</label>
                                         <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <div class="form-group">
                                        <label>City</label>
                                           <asp:DropDownList ID="ddcity"  AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged"   CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group" style="display:none;">
                                          <label>Area</label>
                                         <asp:DropDownList ID="ddarea" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select Area</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                            </div>

                        </div>
                        <div class="box-footer">
                                <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                           



                        </div>

                    </div>
                </div>
                    <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                        </div>

                        <div class="box-body">
                       
                               
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" 
                                            OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Achievement Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("achievedate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reward">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("awardamount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("awardname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <%--  <asp:TemplateField HeaderText="User ID">
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
                                            <asp:ImageButton ID="imgSignUpForm" runat="server" AlternateText="No Image" CommandName="openSignUpImg" Height="100px" Width="100px" ImageUrl='<%# "../ProductImage/" + Eval("SignUpFormImage") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign Up Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSignUpStatus" runat="server" Text='<%# Eval("SignUpImgStatuss") %>' CssClass='<%# Eval("SignUpImgStatuss") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action for Sign Up">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApproveSignUp" runat="server" OnClientClick="return confirm('Sure to Approve Sign Up Form?');" Text="Approve |" CommandName="approve_signup" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("SignUpFormImage").ToString() != "" ? Eval("SignUpImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkRejectSignUp" runat="server" OnClientClick="return confirm('Sure to Reject Sign Up Form?');" Text="Reject" CommandName="reject_signup" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("SignUpFormImage").ToString() != "" ? Eval("SignUpImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PAN Card">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgPANCard" runat="server" AlternateText="No Image" CommandName="openPANImg" Height="100px" Width="100px" ImageUrl='<%# "../ProductImage/" + Eval("PanImage") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAN Card Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPanStatus" runat="server" Text='<%# Eval("PanImgStatuss") %>' CssClass='<%# Eval("PanImgStatuss") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action for PAN">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApprovePan" runat="server" OnClientClick="return confirm('Sure to Approve PAN Card?');" Text="Approve |" CommandName="approve_pan" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("PanImage").ToString() != "" ? Eval("PanImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkRejectPan" runat="server" Text="Reject" OnClientClick="return confirm('Sure to Reject PAN Card?');" CommandName="reject_pan" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("PanImage").ToString() != "" ? Eval("PanImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cancel Cheque/Passbook">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgCheque" runat="server" AlternateText="No Image" CommandName="openChequeImg" Height="100px" Width="100px" ImageUrl='<%# "../ProductImage/" + Eval("CancelCheque") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancel Cheque/Passbook Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCheque" runat="server" Text='<%# Eval("ChequeImgStatuss") %>' CssClass='<%# Eval("ChequeImgStatuss") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action for Cheque">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApproveCheque" runat="server" OnClientClick="return confirm('Sure to Approve Cancel Cheque/Passbook?');" Text="Approve |" CommandName="approve_cheque" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("CancelCheque").ToString() != "" ? Eval("ChequeImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkRejectCheque" runat="server" OnClientClick="return confirm('Sure to Reject Cancel Cheque/Passbook?');" Text="Reject" CommandName="reject_cheque" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("CancelCheque").ToString() != "" ? Eval("ChequeImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Aadhaar Card">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgAadhaar" runat="server" AlternateText="No Image" CommandName="openAadhaarImg" Height="100px" Width="100px" 
                                                ImageUrl='<%# "../ProductImage/" + Eval("AadharImage") %>' />
                                            <asp:ImageButton ID="imgAadhaarBack" runat="server" AlternateText="No Image" CommandName="openAadhaarImgBack" Height="100px" Width="100px" 
                                                ImageUrl='<%# "../ProductImage/" + Eval("AadharImageBack") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aadhaar Card Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAadhaarStatus" runat="server" Text='<%# Eval("AadharImgStatuss") %>' CssClass='<%# Eval("AadharImgStatuss") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action for Aadhaar Card">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApproveAadhaar" runat="server" OnClientClick="return confirm('Sure to Approve Aadhaar Card?');" Text="Approve |" CommandName="approve_aadhaar" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("AadharImage").ToString() != "" ? Eval("AadharImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkRejectAadhaar" runat="server" OnClientClick="return confirm('Sure to Reject Aadhaar Card?');" Text="Reject" CommandName="reject_aadhaar" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("AadharImage").ToString() != "" ? Eval("AadharImgStatuss").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="GST" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgGSTCard" runat="server" AlternateText="No Image" CommandName="opengstImg" Height="100px" Width="100px" ImageUrl='<%# "../ProductImage/" + Eval("gstImage") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST Status" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGstStatus" runat="server" Text='<%# Eval("IsGstApplicable") %>' CssClass='<%# Eval("IsGstApplicable") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action for GST" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApproveGST" runat="server" OnClientClick="return confirm('Sure to Approve GST?');" Text="Approve |" CommandName="approve_GST" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("gstImage").ToString() != "" ? Eval("IsGstApplicable").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkRejectGST" runat="server" Text="Reject" OnClientClick="return confirm('Sure to Reject GST?');" CommandName="reject_GST" CommandArgument='<%#Eval("userid") %>' 
                                                Visible='<%# Eval("gstImage").ToString() != "" ? Eval("IsGstApplicable").ToString() == "Pending" ? true : false : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkedit" runat="server"  Text="Edit"  CommandArgument='<%#Eval("userid") %>' OnClick="lnkedit_click"></asp:LinkButton>
                                               </ItemTemplate>
                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                                    </div>
                              
                             
                             
                            
                            

                        </div>
                        <div class="box-footer">
                        



                        </div>

                    </div>
                </div>

            </div>



            <div id="DivPhotolarge" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                  
                        <div class="modal-body">
                       
                            <div class="form-group">
                                          
                              <asp:Image ID="ImageLarge" runat="server" Width="100%" Height="100%" />
                            </div>
                        
                        </div>
                        <div class="modal-footer">
                       
                              <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">

    <script type="text/javascript">

        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();

        }
    </script>


</asp:Content>


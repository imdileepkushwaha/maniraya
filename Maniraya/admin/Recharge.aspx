<%@ Page Title="Recharge" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="Recharge.aspx.cs" Inherits="admin_Recharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script type="text/javascript">
        function theFunction(liElem, aElem) {

            document.getElementById("limobile").className = "";
            $('#tab1').removeClass('active');
            document.getElementById("lidth").className = "";
            $('#tab2').removeClass('active');
            document.getElementById("lilandline").className = "";
            $('#tab3').removeClass('active');
            document.getElementById("lielectricity").className = "";
            $('#tab4').removeClass('active');
            // document.getElementById("liSettings").className = "";
            // $('#settings').removeClass('active');
            document.getElementById("ligas").className = "";
            $('#tab5').removeClass('active');
            // alert(liElem);
            document.getElementById(liElem).className = "active";
            document.getElementById(aElem).className += " active";
        }
    </script>
  

    <style type="text/css">
        .nav-tabs {
            border-bottom: 2px solid #DDD;
        }

            .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
                border-width: 0;
            }

            .nav-tabs > li > a {
                border: none;
                color: #ffffff;
                background: #5a4080;
            }

                .nav-tabs > li.active > a, .nav-tabs > li > a:hover {
                    border: none;
                    color: #5a4080 !important;
                    background: #fff;
                }

                .nav-tabs > li > a::after {
                    content: "";
                    background: #5a4080;
                    height: 2px;
                    position: absolute;
                    width: 100%;
                    left: 0px;
                    bottom: -1px;
                    transition: all 250ms ease 0s;
                    transform: scale(0);
                }

            .nav-tabs > li.active > a::after, .nav-tabs > li:hover > a::after {
                transform: scale(1);
            }

        .tab-nav > li > a::after {
            background: #5a4080 none repeat scroll 0% 0%;
            color: #fff;
        }

        .tab-pane {
            padding: 15px 0;
        }

        .tab-content {
            padding: 20px;
        }

        .nav-tabs > li {
            width: 20%;
            text-align: center;
        }

        .card {
            background: #FFF none repeat scroll 0% 0%;
            box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
            margin-bottom: 30px;
            margin-top: 30px;
        }

        .form-horizontal .form-control {
            height: 44px;
        }

        @media all and (max-width:724px) {
            .nav-tabs > li > a > span {
                display: none;
            }

            .nav-tabs > li > a {
                padding: 5px 5px;
            }
        }

        .input-group {
            margin-bottom: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
   <section class="content-header">
      <h1>
       Recharge    
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Recharge</a></li>
        <li class="active">Recharge</li>
      </ol>
    </section>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate></ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
                    <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Recharge</h3>
                        </div>

                        <div class="box-body">
                              <div class="row">
                                  <div class="form-group">
                                
            <h5>Recharge (Wallet Balance:
                <asp:Label ID="lblwalletbalance" runat="server" Text="[]"></asp:Label>)</h5>
            <asp:TextBox ID="txtflag" Style="display: none" runat="server"></asp:TextBox>
            <span id="LblNo" runat="server"></span>
      
                                      </div>
                                  </div>
                                <div class="row">
                                    <div id="DivBanner" runat="server" class="row full-width-search" style="">

                    <asp:UpdatePanel ID="rpdatePanelecharge" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<input id="TxtiffrC" type="text"/>--%>
                    <asp:TextBox ID="TxtiffrC" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtIreffOpcode" runat="server" Style="display: none"></asp:TextBox>


                    <asp:TextBox ID="txtOp" Visible="false" runat="server"></asp:TextBox>

                    <div class="col-md-offset-2 col-md-8">
                        <div class="card">
                            <ul class="nav nav-tabs" role="tablist">
                                <li id="limobile" role="presentation" class="active"><a href="#tab1" aria-controls="home" role="tab" data-toggle="tab"><i class="fa fa-mobile"></i>&nbsp; <span>Mobile</span></a></li>
                                <li id="lidth" role="presentation"><a href="#tab2" aria-controls="profile" role="tab" data-toggle="tab"><i class="fa fa-television"></i>&nbsp; <span>DTH</span></a></li>
                                <li id="lilandline" role="presentation"><a href="#tab3" aria-controls="messages" role="tab" data-toggle="tab"><i class="fa fa-phone"></i>&nbsp; <span>Landline</span></a></li>
                                <li id="lielectricity" role="presentation"><a href="#tab4" aria-controls="settings" role="tab" data-toggle="tab"><i class="fa fa-plug"></i>&nbsp; <span>Electricity</span></a></li>
                                <li id="ligas" role="presentation"><a href="#tab5" aria-controls="settings" role="tab" data-toggle="tab"><i class="fa fa-fire"></i>&nbsp; <span>Gas</span></a></li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content" id="Div1">
                                <div role="tabpanel" class="tab-pane active" id="tab1">
                                    <div class="form-horizontal">
                                        <div class="input-group">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rdpre" runat="server" onclick="ckeckpostpiad(this.id)" GroupName="Prepaid" /><label for="radio1"><span><span></span></span>Prepaid</label>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="RadioButton2" GroupName="Prepaid" onclick="ckeckpostpiad(this.id)" runat="server" /><label for="radio2"><span><span></span></span>Postpaid</label>
                                            </label>
                                        </div>

                                        <div class="input-group">
                                            <asp:TextBox ID="TxtMobileNo" onkeyup="getOpCircle(this.value)" class="form-control" placeholder="Enter Mobile Number" MaxLength="10" runat="server" onkeypress="return isNumber(event)" autocomplete="off"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-mobile fa-fw" style="font-size: 26px; width: 17px;"></i></span>
                                        </div>

                                        <div class="input-group">
                                            <asp:DropDownList class="form-control" Style="color: black; display: none" onchange="IReff(this.options[this.selectedIndex].text)" ID="DdlOpertorPostPaid" runat="server"></asp:DropDownList>
                                            <asp:DropDownList class="form-control" Style="color: black" onchange="IReff(this.options[this.selectedIndex].text)" ID="DdlOpertor" runat="server"></asp:DropDownList>
                                            <span class="input-group-addon"><a style="text-decoration: none !important;" href="#" id="Viwelink" class="big-link" data-toggle="modal" data-target="#myModal">View Plan
                                            </a></span>
                                        </div>

                                        <div class="input-group">
                                            <asp:TextBox ID="TxtAmount" onkeypress="return isNumber(event)" class="form-control" MaxLength="10" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-inr fa-fw"></i></span>
                                        </div>

                                        <div class="input-group">
                                            <a href="#" class="btn btn-primary" onclick="fill()" data-toggle="modal" data-target="#Model1">Continue Recharge</a>
                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab2">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:DropDownList ID="DdlDTHOpertor" class="form-control" onChange="DtHLabel(this);" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="TxtCardNo" autocomplete="off" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="TxtDTHAmount" onkeypress="return isNumber(event)" class="form-control" MaxLength="10" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fill2('ctl00_contentData_TxtCardNo','ctl00_contentData_TxtDTHAmount','ctl00_contentData_DdlDTHOpertor','2')" data-toggle="modal" data-target="#Model1">Recharge DTH</a>

                                                <div id="txtDthMsg" style="text-align: center; color: black; font-size: 12px; padding-right: 20px;"></div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab3">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:DropDownList class="form-control" ID="dthLandline" Style="color: black" onchange="getOpOption(this,'TxtStd','txtLandLine1','','')" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-3" style="padding-left: 0;">
                                                    <asp:TextBox ID="TxtStd" onkeypress="return isNumber(event)" runat="server" placeholder="STD Code" class="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                                </div>
                                                <div class="col-md-9" style="padding-right: 0;">
                                                    <asp:TextBox ID="TxtLandLine" onkeypress="return isNumber(event)" class="form-control" placeholder="Enter Line Number" MaxLength="8" runat="server" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="Div2" style="display: none">
                                                <asp:TextBox ID="txtLandLine1" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="TxtElAmt" onkeypress="return isNumber(event)" Style="color: black" class="form-control" MaxLength="6" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fillopenEl('TxtStd','TxtLandLine','TxtElAmt','dthLandline','3')" data-toggle="modal" data-target="#Model1">Pay Now</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab4">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:DropDownList ID="DdlELECTRICITY" class="form-control" onChange="getOpOption2(this,'TxtElecttrcityOption1','TxtElecttrcityOption2','TxtElecttrcityOption3','TxtElecttrcityOption4');" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="txtCustomerNo" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="Electtrcityoption1" style="display: none">
                                                <asp:TextBox ID="TxtElecttrcityOption1" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="Electtrcityoption2" style="display: none">
                                                <asp:TextBox ID="TxtElecttrcityOption2" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="Electtrcityoption3" style="display: none">
                                                <asp:TextBox ID="TxtElecttrcityOption3" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="Electtrcityoption4" style="display: none">
                                                <asp:TextBox ID="TxtElecttrcityOption4" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="txtElecttrcityAmt" onkeypress="return isNumber(event)" class="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fillopenEl('','txtCustomerNo','txtElecttrcityAmt','DdlELECTRICITY','4')" data-toggle="modal" data-target="#Model1">Pay Now
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab5">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:DropDownList ID="Ddlgus" class="form-control" onChange="getOpGas(this,'TxtGusOption1','TxtGusOption2','TxtGusOption3','TxtGusOption4');" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="txtGusNo" onkeypress="return isNumber(event)" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="DivGus1" style="display: none">
                                                <asp:TextBox ID="TxtGusOption1" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="DivGus2" style="display: none">
                                                <asp:TextBox ID="TxtGusOption2" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="DivGus3" style="display: none">
                                                <asp:TextBox ID="TxtGusOption3" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group" id="DivGus4" style="display: none">
                                                <asp:TextBox ID="TxtGusOption4" Style="color: black;" class="form-control" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="txtGusAmt" onkeypress="return isNumber(event)" class="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fillopenEl('','txtGusNo','txtGusAmt','Ddlgus','5')" data-toggle="modal" data-target="#Model1">Pay Now
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                                    </div>
                            </div>
                        </div>
                        </div>
        </div>

    <div class="ibox float-e-margins">
        
        <div class="ibox-content collapse in">
            <div class="widgets-container">
                

            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <div class="modal fade" id="Model1" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header ">
                    <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                </div>
                <div class="modal-body" style="padding: 0;">
                    <div class="col-md-10 col-sm-10 search-col-padding">
                        <div class="col-md-4  search-col-padding">
                            <span style="font-weight: bold;">Recharge No -</span>
                        </div>
                        <div class="col-md-6 col-sm-6 search-col-padding">
                            <input readonly="true" style="border: none;" id="LblNo1" />
                        </div>
                        <div class="col-md-4 col-sm-4 search-col-padding">
                            <span style="font-weight: bold;">Operator -</span>
                        </div>
                        <div class="col-md-6 col-sm-6 search-col-padding">
                            <input readonly="true" style="border: none;" id="LblOp" />
                        </div>
                        <div class="col-md-4 col-sm-4 search-col-padding">
                            <span style="font-weight: bold;">Amount -</span>
                        </div>
                        <div class="col-md-6 col-sm-6 search-col-padding">
                            <input readonly="true" style="border: none;" id="LblAmt" />
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-2 search-col-padding">
                        <img id="OImage" style="float: right;" />
                    </div>
                    <div class="col-md-12 col-sm-12 search-col-padding" style="text-align: center;">
                      <div class="progress progress-striped active">
                    <div style="width: 100%" aria-valuemax="100" aria-valuemin="0" aria-valuenow="100" role="progressbar" class="progress-bar progress-bar-success" id="loading"> <span class="sr-only"> Loading </span> </div>
                  </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12 col-sm-12 search-col-padding">
                        <asp:Button ID="ButRecharge" UseSubmitBehavior="false" OnClick="ButRecharge_Click" OnClientClick="ImageVisbletrue(LblNo1,LblOp,LblAmt,ctl00_contentpageData_DdlOpertor)" ValidationGroup="re" class="big-link btn btn-primary" runat="server" Style="margin: 0px" Text="Go Recharge" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                    <%--  <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="SingleParagraph" ValidationGroup="re" Style="font-size: 14px; font-weight: 600; color: red;" runat="server" />
                                                            <div style="display: none">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="none" runat="server" ValidationGroup="re" ControlToValidate="TxtMobileNo" ValidationExpression="^[0-9]*$" ErrorMessage="Recharge No only 10 Digit !"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="none" ValidationGroup="re" ControlToValidate="TxtMobileNo" runat="server" ErrorMessage="Fill Recharge No !"></asp:RequiredFieldValidator><br />

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="none" runat="server" ValidationGroup="re" ControlToValidate="TxtAmount" ValidationExpression="^[0-9]*$" ErrorMessage="Amount only Numeric !"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="none" ValidationGroup="re" ControlToValidate="TxtAmount" runat="server" ErrorMessage="Fill Recharge Amount !"></asp:RequiredFieldValidator><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="none" ValidationGroup="re" ControlToValidate="DdlOpertor" InitialValue="0" runat="server" ErrorMessage="Select Operator !"></asp:RequiredFieldValidator>
                                                            </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H2">Plan</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <iframe src="" style="width: 100%; height: 400px"></iframe>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="RechargeHistory" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1">Basic Modal</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView runat="server" ID="GridViewLast10"
                        Style="color: black; font-size: 12px; width: 100%;" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>Rechage No</HeaderTemplate>
                                <ItemTemplate><%# Eval("AmountTransferTo") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Amount</HeaderTemplate>
                                <ItemTemplate><%# Eval("RequestedAmount") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Status</HeaderTemplate>
                                <ItemTemplate><%# CheckType(Eval("Type").ToString()) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Oprator Name</HeaderTemplate>
                                <ItemTemplate><%# Eval("OpratorName") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Date & Time</HeaderTemplate>
                                <ItemTemplate><%# Eval("EntryDate") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#444444" Font-Bold="True" ForeColor="#E7E7FF" />
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
       <%-- <script src="assets/js/vendor/jquery.min.js"></script>--%>
       <script src="Recharge.js"></script>
</asp:Content>


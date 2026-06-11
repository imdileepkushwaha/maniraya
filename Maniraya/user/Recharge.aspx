<%@ Page Title="Recharge" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="Recharge.aspx.cs" Inherits="admin_Recharge" %>

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
            <li><a href="#"> Recharge</a></li>
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
                        <div class="ibox-title">
            <h5>Wallet Balance:
                <asp:Label ID="lblwalletbalance" runat="server" Text="Label"></asp:Label>   <asp:Label ID="Lblcashwallet" runat="server" Text="Label" Visible="false"></asp:Label></h5>
                             
            <asp:TextBox ID="txtflag" Style="display: none" runat="server"></asp:TextBox>
            <span id="LblNo" runat="server"></span>
        </div>
                            </div>
                        <div class="row">

                            <div class="col-md-7">
                            <div id="DivBanner" runat="server" class="row full-width-search" style="">

                    <asp:UpdatePanel ID="rpdatePanelecharge" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<input id="TxtiffrC" type="text"/>--%>
                    <asp:TextBox ID="TxtiffrC" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="TxtCircel" runat="server" Style="display: none"></asp:TextBox>


                    <asp:TextBox ID="txtOp" Visible="false" runat="server"></asp:TextBox>

                    <%--<div class="col-md-offset-2 col-md-8">--%>
                                <div class="col-md-12">
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
                                                <asp:RadioButton ID="rdpre" runat="server" Checked="true" onclick="ckeckpostpiad(this.id)" GroupName="Prepaid" /><label for="radio1"><span><span></span></span>Prepaid</label>
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
                                            <asp:DropDownList class="form-control" Style="color: black"  ID="DdlOpertor" onchange="IReff(this.options[this.selectedIndex].text)" runat="server"></asp:DropDownList>
                                            <span class="input-group-addon"><a style="text-decoration: none !important;" href="#" id="Viwelink" class="big-link" data-toggle="modal"  data-target="#myModal">View Plan
                                            </a></span>
                                            
                                        </div>

                                        <div class="input-group">
                                            <asp:TextBox ID="TxtAmount" onkeypress="return isNumber(event)" class="form-control" MaxLength="10" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-inr fa-fw"></i></span>
                                        </div>

                                        <div class="input-group">
                                            <a href="#" class="btn btn-primary" onclick="fill()" data-toggle="modal" data-target="#Model1">Continue Recharge</a>
                                              <span class="input-group-addon" id="spanroffer" style="display:none;" ><a style="text-decoration: none !important;" href="#" id="A1" class="big-link" data-toggle="modal" data-target="#Divroffer" >R-offer
                                            </a></span>
                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab2">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label>Select Operator</label>
                                                <asp:DropDownList ID="DdlDTHOpertor" class="form-control" onChange="DtHLabel(this);" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                 <label id="typename">CustomerID</label>
                                                <asp:TextBox ID="TxtCardNo" autocomplete="off" class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                 <label>Amount</label>
                                                <asp:TextBox ID="TxtDTHAmount"  onkeypress="return isNumber(event)" class="form-control" MaxLength="10" placeholder="Amount" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fill2('ctl00_contentpageData_TxtCardNo','ctl00_contentpageData_TxtDTHAmount','ctl00_contentpageData_DdlDTHOpertor','2')" data-toggle="modal" data-target="#Model1">Recharge DTH</a>

                                                <div id="txtDthMsg" style="text-align: center; color: black; font-size: 12px; padding-right: 20px;"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="tab3">
                                    <div class="container-fluid">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:DropDownList class="form-control" ID="dthLandline" Style="color: black" onchange="getOpOption(this,'ctl00_contentpageData_txtoptional1','ctl00_contentpageData_txtoptional2','ctl00_contentpageData_txtoptional3','ctl00_contentpageData_TxtStd')" runat="server"></asp:DropDownList>
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
                                              <div class="form-group" id="divoptional1" style="display:none;">
                                                <asp:TextBox ID="txtoptional1"  Style="color: black" class="form-control"  placeholder="" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>
                                               <div class="form-group" id="divoptional2"  style="display:none;">
                                                <asp:TextBox ID="txtoptional2"  Style="color: black" class="form-control"  placeholder="" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>
                                               <div class="form-group" id="divoptional3"  style="display:none;">
                                                <asp:TextBox ID="txtoptional3"  Style="color: black" class="form-control"  placeholder="" runat="server" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fillopenEl('ctl00_contentpageData_TxtStd','ctl00_contentpageData_TxtLandLine','ctl00_contentpageData_TxtElAmt','ctl00_contentpageData_dthLandline','3')" data-toggle="modal" data-target="#Model1">Pay Now</a>
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
                                                  <span class="input-group-addon"  style="display:none;"><a style="text-decoration: none !important;" href="#" onclick="getOpBill()" >View Bill</a></span>
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
                                          
                                                 <div class="box box-danger box-solid" id="divmsge" style="display:none;">
            <div class="box-header with-border">
              <h3 class="box-title">Detail</h3>

              <div class="box-tools pull-right">
            
              
              </div>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
            <span id="Span_Message123" style="text-align: center; color: black; font-size: 12px; padding-right: 20px;"></span><br />
             <span id="Span_partial123" style="text-align: center; color: black; font-size: 12px; padding-right: 20px;"></span>
            </div>
            <!-- /.box-body -->
          </div>
                                                 <div class="box box-danger box-solid" id="divloading1" style="display:none;">
            <div class="box-header">
              <h3 class="box-title">Detail</h3>
            </div>
            <div class="box-body">
              Please wait
            </div>
            <!-- /.box-body -->
            <!-- Loading (remove the following to stop the loading)-->
            <div class="overlay">
              <i class="fa fa-refresh fa-spin"></i>
            </div>
            <!-- end loading -->
          </div>
                                                 </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtElecttrcityAmt" onkeypress="return isNumber(event)" class="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <a href="#" class="btn btn-primary" onclick="fillopenEl('','ctl00_contentpageData_txtCustomerNo','ctl00_contentpageData_txtElecttrcityAmt','ctl00_contentpageData_DdlELECTRICITY','4')" data-toggle="modal" data-target="#Model1">Pay Now
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


                            <div class="col-md-5 col-xs-12">

                           <!--Last 10 transactions report (Starts)-->

                       

                                <div class="col-md-12">
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
     <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
                </asp:Timer>
                                    <div class="card" style="border-radius:5px;">
                                        <div style="background-color:#5A4080; color:#fff; text-align:center; padding:5px;">
                                            Last 10 Transactions
                                        </div>

                                        <!-- Tab panes -->
                                        <%--<div class="tab-content">--%>
                                        <div class="box-body table-responsive no-padding">

                                            <%--<asp:GridView ID="grdTransReport" runat="server" CssClass="table table-bordered table-responsive table-hover datatable" Width="100%" AutoGenerateColumns="false" 
                                                OnRowDataBound="grdTransReport_RowDataBound">--%>
                                            <asp:GridView ID="grdTransReport" runat="server" CssClass="table table-hover table-bordered table-responsive datatable" Width="100%" AutoGenerateColumns="false" 
                                                OnRowDataBound="grdTransReport_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Trans. Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransId" runat="server" Text='<%# Eval("ReferenceId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRechargeNo" runat="server" Text='<%# Eval("RechargeMobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("RechargeAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Live Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLiveId" runat="server" Text='<%# Eval("LiveID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
            </ContentTemplate>
                                          </asp:UpdatePanel>
                                </div>
                         



                           <!--Last 10 transactions report (Ends)-->

                            </div>
                            
                            </div>

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
                        <div class="col-md-8 col-sm-8 search-col-padding">
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
                        <%--<asp:Button ID="ButRecharge" UseSubmitBehavior="false" OnClick="ButRecharge_Click"  ValidationGroup="re" class="big-link btn btn-primary" runat="server" Style="margin: 0px" Text="Go Recharge" />--%>
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
     <div class="modal fade" id="Divroffer" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H4">R-offer</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <iframe src="" style="width: 100%; height: 400px" id="iframeroffer"></iframe>

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


    <div class="modal fade" id="Divbill" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H3">Bill Detail</h4>
                </div>
                <div class="modal-body">
                   <div class="form-control">
                       message
                       <asp:TextBox ID="TxtMessage" runat="server"></asp:TextBox>
                   </div>                    
                      <div class="form-control">
                       BillNo
                       <asp:TextBox ID="TxtBillNo" runat="server"></asp:TextBox>
                   </div>
                     <div class="form-control">
                       Amount
                       <asp:TextBox ID="TxtAmountBill" runat="server"></asp:TextBox>
                   </div>
                       <div class="form-control">
                       Bill Date
                       <asp:TextBox ID="TxtBilldate" runat="server"></asp:TextBox>
                   </div>
                       <div class="form-control">
                       Due Date
                       <asp:TextBox ID="TxtDuedate" runat="server"></asp:TextBox>
                   </div>
                       <div class="form-control">
                       Partial pay
                       <asp:TextBox ID="Txtpartialpay" runat="server"></asp:TextBox>
                   </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script src="Recharge.js"></script>
     <script type="text/javascript">
         function showModal() {
             $('#Divbill').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#Divbill').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();
         }

       

    </script>
   
</asp:Content>


<%@ Page Title="Message Setting" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="MessageSetting.aspx.cs" Inherits="admin_MessageSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
          text-align: center;
    border-radius: 0;
    border: 0;
  
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
    Message Setting
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Settings</a></li>
        <li class="active"> Message Setting</li>
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
        <ContentTemplate>
               <div class="row">
     

     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Message Setting</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row">
                         
                       
                          <div class="card">

                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#home4" data-toggle="tab">Registration</a></li>
                                        <li style="display:none;"><a href="#profile4" data-toggle="tab">Fund Transfer</a></li>
                                        <li style="display:none;"><a href="#messages4" data-toggle="tab">Fund Receive</a></li>
                                        <li><a href="#settings4" data-toggle="tab">Fund Debit</a></li>
                                        <li><a href="#login4" data-toggle="tab">Fund Credit</a></li>
                                        <li><a href="#button4" data-toggle="tab">Accepted</a></li>
                                        <li><a href="#tab4" data-toggle="tab">Success</a></li>
                                        <li><a href="#examp4" data-toggle="tab">Failed</a></li>
                                        <li><a href="#Div1" data-toggle="tab">Refund</a></li>
                                    </ul>

                                    <!-- Tab panes -->
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="home4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_userid" runat="server" class="btn btn-soft btn-sm btn-block btn-round" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_Registration, '{UserID}'); return false;" Text="{UserID}" />
                                                    <asp:Button ID="btn_pwd" runat="server" class="btn btn-soft btn-sm btn-block btn-round" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_Registration, '{Password}');return false;" Text="{Password}" />
                                                    <asp:Button ID="btn_PinPwd" runat="server" class="btn btn-soft btn-sm btn-block btn-round" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_Registration, '{PinPassword}');return false;" Text="{PinPassword}" />
                                                    <input id="Button1" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_Registration, '{Website}')" value="{Website}" />
                                                    <input id="Button2" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_Registration, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_Registration" Rows="4" runat="server" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="profile4" style="display:none;">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_fromMobile" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{FromMobileNo}');return false;" />
                                                    <asp:Button ID="btn_toMobile" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{ToMobileNo}');return false;" />
                                                    <asp:Button ID="btn_fromUser" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{FromUserName}');return false;" />
                                                    <asp:Button ID="btn_toUser" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{ToUserName}');return false;" />
                                                    <asp:Button ID="btn_amount" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_bal_amount" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{BalanceAmount}');return false;" />
                                                    <input id="Button3" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{Website}')" value="{Website}" />
                                                    <input id="Button5" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferTo, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_FundTransferTo" runat="server" Rows="7" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="messages4" style="display:none;">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_fromMobile1" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{FromMobileNo}');return false;" />
                                                    <asp:Button ID="btn_toMobile1" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{ToMobileNo}');return false;" />
                                                    <asp:Button ID="btn_fromUser1" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{FromUserName}');return false;" />
                                                    <asp:Button ID="btn_toUser1" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{ToUserName}');return false;" />
                                                    <asp:Button ID="btn_amount1" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{Amount}');return false;" />
                                                    <asp:Button ID="Button51" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{BalanceAmount}');return false;" />
                                                    <input id="Button6" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{Website}')" value="{Website}" />
                                                    <input id="Button7" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundTransferFrom, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_FundTransferFrom" Rows="7" runat="server" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="settings4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_fromMobile7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{FromMobileNo}');return false;" />
                                                    <asp:Button ID="btn_toMobile7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{ToMobileNo}');return false;" />
                                                    <asp:Button ID="btn_fromUser7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{FromUserName}');return false;" />
                                                    <asp:Button ID="btn_toUser7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{ToUserName}');return false;" />
                                                    <asp:Button ID="btn_amount7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_bal7" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{BalanceAmount}');return false;" />
                                                    <input id="Button8" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{Website}')" value="{Website}" />
                                                    <input id="Button9" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundDebit, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_FundDebit" Rows="7" runat="server" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="login4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_fromMobile8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{FromMobileNo}');return false;" />
                                                    <asp:Button ID="btn_toMobile8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToMobileNo}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{ToMobileNo}');return false;" />
                                                    <asp:Button ID="btn_fromUser8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{FromUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{FromUserName}');return false;" />
                                                    <asp:Button ID="btn_toUser8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{ToUserName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{ToUserName}');return false;" />
                                                    <asp:Button ID="btn_amount8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_bal8" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{BalanceAmount}');return false;" />
                                                    <input id="Button10" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{Website}')" value="{Website}" />
                                                    <input id="Button11" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_FundCredit, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_FundCredit" Rows="7" runat="server" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="button4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_MobileA" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Mobile}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{Mobile}');return false;" />
                                                    <asp:Button ID="btn_AmtA" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_OpNameA" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{OperatorName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{OperatorName}');return false;" />
                                                    <asp:Button ID="btn_TransIDA" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{TransactionID}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{TransactionID}');return false;" />
                                                    <asp:Button ID="btn_BalAmtA" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{BalanceAmount}');return false;" />
                                                    <input id="Button12" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{Website}')" value="{Website}" />
                                                    <input id="Button13" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechAccept, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_RechAccept" runat="server" Rows="8" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                    <label>
                                                        <input id="chk_Acc" type="checkbox" class="icheckbox_flat-purple" runat="server" /><span style="font-size: medium; color: #0097a7">Enable</span></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_MobileS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Mobile}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{Mobile}');return false;" />
                                                    <asp:Button ID="btn_AmtS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_OpNameS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{OperatorName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{OperatorName}');return false;" />
                                                    <asp:Button ID="btn_OpIdS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{LiveID}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{LiveID}');return false;" />
                                                    <asp:Button ID="btn_TransIDS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{TransactionID}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{TransactionID}');return false;" />
                                                    <asp:Button ID="btn_BalAmtS" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{BalanceAmount}');return false;" />
                                                    <input id="Button14" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{Website}')" value="{Website}" />
                                                    <input id="Button15" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechSuccess, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_RechSuccess" runat="server" Rows="8" class="form-control form-soft form-round" TextMode="MultiLine"></asp:TextBox>
                                                    <label>
                                                        <input id="chk_succ" type="checkbox" class="icheckbox_flat-purple" runat="server" /><span style="font-size: medium; color: #0097a7">Enable</span></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="examp4">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btn_MobileF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Mobile}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Mobile}');return false;" />
                                                    <asp:Button ID="btn_AmtF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Amount}');return false;" />
                                                    <asp:Button ID="btn_OpNameF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{OperatorName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{OperatorName}');return false;" />
                                                    <asp:Button ID="btn_ReasonF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Reason}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Reason}');return false;" />
                                                    <asp:Button ID="btn_TransIDF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{TransactionID}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{TransactionID}');return false;" />
                                                    <asp:Button ID="btn_BalAmtF" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{BalanceAmount}');return false;" />
                                                    <input id="Button16" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Website}')" value="{Website}" />
                                                    <input id="Button17" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_RechFailed" runat="server" class="form-control form-soft form-round" Rows="8" TextMode="MultiLine"></asp:TextBox>
                                                    <label>
                                                        <input id="chk_fail" type="checkbox" class="icheckbox_flat-purple" runat="server" /><span style="font-size: medium; color: #0097a7">Enable</span></label>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="tab-pane" id="Div1">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="Button19" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Mobile}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Mobile}');return false;" />
                                                    <asp:Button ID="Button20" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Amount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Amount}');return false;" />
                                                    <asp:Button ID="Button22" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{OperatorName}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{OperatorName}');return false;" />
                                                    <asp:Button ID="Button23" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Reason}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Reason}');return false;" />
                                                    <asp:Button ID="Button24" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{TransactionID}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{TransactionID}');return false;" />
                                                    <asp:Button ID="Button25" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{BalanceAmount}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{BalanceAmount}');return false;" />
                                                    <input id="Button26" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{Website}')" value="{Website}" />
                                                    <input id="Button27" type="button" class="btn btn-soft btn-sm btn-block btn-round" onclick="insertAtCursor(ContentPlaceHolder1_txt_RechFailed, '{CompanyName}')" value="{CompanyName}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txtRefund" runat="server" class="form-control form-soft form-round" Rows="8" TextMode="MultiLine"></asp:TextBox>
                                                    <label>
                                                        <input id="Chk_Refund" type="checkbox" class="icheckbox_flat-purple" runat="server" /><span style="font-size: medium; color: #0097a7">Enable</span></label>

                                                </div>
                                            </div>

                                        </div>
                                        
                                    </div>
                             
                                </div>

                            </div>
                  <div class="row">

                        <div class="card">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#Operator-Up" data-toggle="tab">Operator Up Message</a></li>
                                        <li><a href="#Operator-down" data-toggle="tab">Operator Down Message</a></li>
                                    </ul>
                                    <!-- Tab panes -->
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="Operator-Up">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="Button18" runat="server" class="btn btn-soft btn-sm btn-block btn-round" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_OperatorUP, this.value); return false;" Text="{Operator}" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_OperatorUP" Rows="4" runat="server" class="form-control form-soft form-round" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="Operator-down">
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="Button21" runat="server" class="btn btn-soft btn-sm btn-block btn-round" Text="{Operator}" OnClientClick="insertAtCursor(ContentPlaceHolder1_txt_OperatorDown, this.value);return false;" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="txt_OperatorDown" runat="server" Rows="7" class="form-control form-soft form-round" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                     
                              </div>

                  <div class="row">
                                            <div class="col-sm-12">
                                                <div class="spacer">
                                                    <asp:Button ID="btn_Submit" runat="server" Text=" Save " Width="100px" class="btn btn-primary btn-round" OnClick="btn_Submit_Click" />
                                                </div>
                                            </div>
                                        </div>
                    </div>
                  </div>
                 </div>
         </div>
                   </div>



            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Message Setting</h5>
                    <div class="ibox-tools">
                    </div>
                </div>
                <div class="ibox-content collapse in">
                    <div class="widgets-container">
                        <div class="form-horizontal">
                          
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript">
        function AppendText(sender) {
            var txt_Registration = document.getElementById('<%=txt_Registration.ClientID%>');
             txt_Registration.value = txt_Registration.value + sender.value;
             txt_Registration.focus();
             return false;
         }
         function AppendText_FundTo(sender) {
             var txt_FundTransferTo = document.getElementById('<%=txt_FundTransferTo.ClientID%>');
            txt_FundTransferTo.value = txt_FundTransferTo.value + sender.value;
            txt_FundTransferTo.focus();
            return false;
        }
        function insertAtCursor(myField, myValue) {
            //IE support
            if (document.selection) {
                myField.focus();
                sel = document.selection.createRange();
                sel.text = myValue;
            }
                //MOZILLA and others
            else if (myField.selectionStart || myField.selectionStart == '0') {
                var startPos = myField.selectionStart;
                var endPos = myField.selectionEnd;
                myField.value = myField.value.substring(0, startPos)
                    + myValue
                    + myField.value.substring(endPos, myField.value.length);
            } else {
                myField.value += myValue;
            }
        }
        function AppendText_FundFrom(sender) {
            var txt_FundTransferFrom = document.getElementById('<%=txt_FundTransferFrom.ClientID%>');
            txt_FundTransferFrom.value = txt_FundTransferFrom.value + sender.value;
            txt_FundTransferFrom.focus();
            return false;
        }
        function AppendText_Debit(sender) {
            var txt_FundDebit = document.getElementById('<%=txt_FundDebit.ClientID%>');
        txt_FundDebit.value = txt_FundDebit.value + sender.value;
        txt_FundDebit.focus();
        return false;
    }
    function AppendText_Credit(sender) {
        var txt_FundCredit = document.getElementById('<%=txt_FundCredit.ClientID%>');
        txt_FundCredit.value = txt_FundCredit.value + sender.value;
        txt_FundCredit.focus();
        return false;
    }
    function AppendText_RechAcc(sender) {
        var txt_RechAccept = document.getElementById('<%=txt_RechAccept.ClientID%>');
        txt_RechAccept.value = txt_RechAccept.value + sender.value;
        txt_RechAccept.focus();
        return false;
    }
    function AppendText_RechSucc(sender) {
        var txt_RechSuccess = document.getElementById('<%=txt_RechSuccess.ClientID%>');
        txt_RechSuccess.value = txt_RechSuccess.value + sender.value;
        txt_RechSuccess.focus();
        return false;
    }
    function AppendText_RechFail(sender) {
        var txt_RechFailed = document.getElementById('<%=txt_RechFailed.ClientID%>');
        txt_RechFailed.value = txt_RechFailed.value + sender.value;
        txt_RechFailed.focus();
        return false;
    }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#static').modal('show');
        }
    </script>
</asp:Content>


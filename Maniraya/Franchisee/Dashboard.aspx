<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="assets/css/franchisee-dashboard.css?v=1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
   <div class="content-header">
    <h1>Dashboard</h1>
    <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Dashboard</li>
    </ol>
   </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">

    
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Dashboard</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <asp:Panel ID="pnlnotification" runat="server">

                                <div class="alert alert-danger"><strong>Error!</strong> Please Update your bank details  <a href="UserEdit.aspx">click here</a> to update. </div>

                            </asp:Panel>

                        </div>
                    </div>
                     <div class="row" style="display:none;">
                       
                        <div class="col-md-12">
  <div class="form-group">
                            <marquee direction = "left" onmouseover="stop();" onmouseout="start();"> <asp:Literal ID="ltnews" runat="server" ></asp:Literal></marquee>
       </div>
                        </div>
                    </div>
                    <div class="row" style="display:none;">
                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:Label ID="Label1" runat="server" Text="Affiliate Link (LEFT)"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">

                                <asp:TextBox ID="TxtLeftLinkLink" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-3">


                            <asp:Button ID="Button1" runat="server" Text="Copy" CssClass="btn btn-primary" OnClientClick="CopyToClipboard()" />

                        </div>
                    </div>
                    <div class="row" style="display:none;">
                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:Label ID="Label2" runat="server" Text="Affiliate Link (RIGHT)"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">

                                <asp:TextBox ID="TxtRightLink" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-3">

                            <asp:Button ID="Button2" runat="server" Text="Copy" CssClass="btn btn-primary" OnClientClick="CopyToClipboard2()" />

                        </div>
                    </div>
                    <div class="row">
                                <%-- <div class="col-lg-3 col-xs-6">--%>
                         <div class="col-lg-4 col-xs-12">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="LblCredited" runat="server" Text="Label"></asp:Label></h3>

              <p>Credited</p>
            </div>
            <div class="icon">
              <i class="fa fa-plus-square-o"></i>
            </div>
            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
                                   <%--<div class="col-lg-3 col-xs-6">--%>
                        <div class="col-lg-4 col-xs-12">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3><asp:Label ID="LblDebited" runat="server" Text="Label"></asp:Label></h3>

              <p>Debited</p>
            </div>
            <div class="icon">
              <i class="fa fa-minus-square-o"></i>
            </div>
            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
                               
                              
                                 <%--<div class="col-lg-3 col-xs-6">--%>
                        <div class="col-lg-4 col-xs-12">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="LblCurrentWallet" runat="server" Text="Label"></asp:Label></h3>

              <p>Balance</p>
            </div>
            <div class="icon">
              <i class="fa fa-inr"></i>
            </div>
            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
                            </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                            </div>
                        </div>

                    </div>
                    <div class="row" style="display:none;">
                        <div class="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Status Report</div>
                                <div class="panel-body">
                                    <label>Awards & Rewards Current Qualification Status</label>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.N.">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AWARD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labawardname" runat="server" Text='<%#Eval("awardname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="START DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="END DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARGET LEFT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARGET RIGHT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CURRENT LEFTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentLeftBv" runat="server" Text='<%#Eval("CurrentLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CURRENT RIGHTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentRightBv" runat="server" Text='<%#Eval("CurrentRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUIRED LEFTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredLeftBv" runat="server" Text='<%#Eval("RequiredLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUIRED RIGHTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredRightBv" runat="server" Text='<%#Eval("RequiredRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <label>Dream Vacation Achievers Status Report</label>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.N.">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VACATION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labawardname" runat="server" Text='<%#Eval("vacationname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="START DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                                        <asp:Label ID="lblfromdate1" runat="server" Text='<%#Eval("Fromdate") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="END DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                                        <asp:Label ID="lbltodate1" runat="server" Text='<%#Eval("Todate") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARGET LEFT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TARGET RIGHT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CURRENT LEFTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentLeftBv" runat="server" Text='<%#Eval("CurrentLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CURRENT RIGHTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentRightBv" runat="server" Text='<%#Eval("CurrentRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUIRED LEFTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredLeftBv" runat="server" Text='<%#Eval("RequiredLeft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUIRED RIGHTBV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequiredRightBv" runat="server" Text='<%#Eval("RequiredRight") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary fr-user-detail-box">
                                <div class="box-header with-border">
                                    <h3 class="box-title"><i class="fa fa-id-card-o"></i> User Details</h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-4 col-sm-12 fr-user-photo-col">
                                            <div class="fr-detail-card fr-detail-card-photo">
                                                <div class="fr-detail-card-head">
                                                    <i class="fa fa-camera"></i> My Photo
                                                </div>
                                                <div class="fr-detail-card-body">
                                                    <asp:Image ID="ImgMyPhoto" runat="server" CssClass="fr-user-avatar img-responsive" />
                                                    <p class="fr-photo-hint">Your profile picture</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-5 col-md-4 col-sm-12" style="margin-bottom:18px;">
                                            <div class="fr-detail-card">
                                                <div class="fr-detail-card-head">
                                                    <i class="fa fa-user"></i> My Profile
                                                </div>
                                                <div class="fr-detail-card-body">
                                                    <ul class="fr-info-list">
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-calendar"></i> Joining Date</span>
                                                            <span class="fr-info-value"><asp:Label ID="lbljoiningdate" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-mobile"></i> Mobile</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblmobile" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-envelope"></i> Email</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblemail" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-map-marker"></i> Address</span>
                                                            <span class="fr-info-value"><asp:Label ID="lbladdress" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12" style="margin-bottom:18px;">
                                            <div class="fr-detail-card">
                                                <div class="fr-detail-card-head">
                                                    <i class="fa fa-university"></i> Bank Details
                                                </div>
                                                <div class="fr-detail-card-body">
                                                    <ul class="fr-info-list">
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-user"></i> A/c Holder</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblaccountholdername" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-credit-card"></i> A/c No</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblaccountno" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-building"></i> Bank</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblbank" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-barcode"></i> IFSC Code</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblifsc" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                        <li>
                                                            <span class="fr-info-label"><i class="fa fa-id-card"></i> Pan No</span>
                                                            <span class="fr-info-value"><asp:Label ID="lblpan" runat="server" Text=""></asp:Label></span>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row" style="display:none;">
                        <div class="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">User Detail</div>
                                <div class="panel-body">
                                    <div class="">
                                    <div class="col-md-6">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GridViewToday" runat="server" CssClass="table table-hover table-bordered dataTable" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Today Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrvVwWeek" runat="server" CssClass="table table-hover dataTable danger-table" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Weekly Performance">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repurchase BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BV">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                         <div class="col-md-6">
                            <div class="table-responsive">

                                <asp:GridView ID="GrdVwMonth" runat="server" CssClass="table dataTable sucess-table" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fortnight Performance">
                                            <ItemTemplate>

                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Repurchase BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Count">
                                            <ItemTemplate>
                                                <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="table-responsive">
                                <asp:GridView ID="GrdVwTotal" runat="server" CssClass="table table-hover dataTable warning-table" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Total Performance">
                                            <ItemTemplate>

                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljoiningbv" runat="server" Text='<%#Eval("joiningBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Repurchase BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrepurchasebv" runat="server" Text='<%#Eval("RepurchaseBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total BV">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotalbv" runat="server" Text='<%#Eval("TotalBv") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Count">
                                            <ItemTemplate>
                                                <asp:Label ID="count" runat="server" Text='<%#Eval("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    
                   
                </div>
            </div>
        

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <script type="text/javascript" language="javascript">
        function CopyToClipboard() {


            /* Get the text field */
            var copyText = document.getElementById('<%=TxtLeftLinkLink.ClientID%>');

            /* Select the text field */
            copyText.select();

            /* Copy the text inside the text field */
            document.execCommand("Copy");

            /* Alert the copied text */
            alert("Copied the text: " + copyText.value);
        }
        function CopyToClipboard2() {


            /* Get the text field */
            var copyText1 = document.getElementById('<%=TxtRightLink.ClientID%>');

            /* Select the text field */
            copyText1.select();

            /* Copy the text inside the text field */
            document.execCommand("Copy");

            /* Alert the copied text */
            alert("Copied the text: " + copyText1.value);
        }
    </script>
</asp:Content>

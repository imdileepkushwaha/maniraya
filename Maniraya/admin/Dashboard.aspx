<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
         Dashboard        
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i>Dashboard</a></li>      
      </ol>
    </section>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <div >
       <div class="row">
            <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="LblUserCount" runat="server" Text="  "></asp:Label>
               </h3>

              <p>Users </p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="UserReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="Lbltotalteamactive" runat="server" Text="  "></asp:Label>
               </h3>

              <p>Total Active User </p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="UserReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="Lbltodayteamactive" runat="server" Text="  "></asp:Label>
               </h3>

              <p>Today Active Users </p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="UserReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="Lbltotakbusiness" runat="server" Text="  "></asp:Label>
               </h3>

              <p>Total Business </p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="SavingProductPurchaseReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           
                <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="lbltotalbonus" runat="server" Text=" "></asp:Label></h3>
              <p>Total Bonus</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

             <div class="col-lg-3 col-xs-6" style="display:none;">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="LblProductCount" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Purchase</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="PurchaseReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Lbltotakbusinesstoday" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Today Business</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="SavingProductPurchaseReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Lblwithdrawal" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Total Withdrawal</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Lblwithdrawaltoday" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Today Withdrawal</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
                  <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="lblpendingwithdraw" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Pending Withdrawal</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Lbldeposit" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Total Deposit</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="DepositRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="Lbldeposittoday" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Today Deposit</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="DepositRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
           <div class="col-lg-3 col-xs-6" >
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="lbltotalpayout" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Total Payout</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

                <div class="col-lg-3 col-xs-6" >
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="lbltotalpayouttoday" runat="server" Text="  "></asp:Label>
                </h3>

              <p>Today Payout</p>
            </div>
            <div class="icon">
              <i class="fa fa-table"></i>
            </div>
            <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

        <!-- Saving Product KPIs -->
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="LblTotalFirstPurchase" runat="server" Text="0"></asp:Label></h3>
              <p>Total First Purchase</p>
            </div>
            <div class="icon">
              <i class="fa fa-shopping-cart"></i>
            </div>
            <a href="SavingProductPurchaseReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="LblMonthFirstPurchase" runat="server" Text="0"></asp:Label></h3>
              <p>This Month First Purchase</p>
            </div>
            <div class="icon">
              <i class="fa fa-calendar"></i>
            </div>
            <a href="SavingProductPurchaseReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-maroon">
            <div class="inner">
              <h3><asp:Label ID="LblTotalInstallmentPaid" runat="server" Text="0"></asp:Label></h3>
              <p>Total Installment Paid</p>
            </div>
            <div class="icon">
              <i class="fa fa-money"></i>
            </div>
            <a href="SavingInstallmentReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-maroon">
            <div class="inner">
              <h3><asp:Label ID="LblMonthInstallmentPaid" runat="server" Text="0"></asp:Label></h3>
              <p>This Month Installment Paid</p>
            </div>
            <div class="icon">
              <i class="fa fa-calendar-check-o"></i>
            </div>
            <a href="SavingInstallmentReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

             <div class="col-lg-3 col-xs-6" style="display:none;">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="LblPurchaseAmount" runat="server" Text="" ></asp:Label>
               </h3>

              <p> Franchisee</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="FranchiseeReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
                    
           </div>
           <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3><asp:Label ID="LblActiveEpin" runat="server" Text=""></asp:Label>
               </h3>

              <p>Product </p>
            </div>
            <div class="icon">
              <i class="fa fa-edit"></i>
            </div>
            <a href="ProductDetails.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
                 <div class="col-lg-3 col-xs-6" >
       
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="lable1" runat="server" Text="" ></asp:Label>
               </h3>

              <p> Award & Reward</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="UsersRewardReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
          </div>
                    
           </div>
           </div>
 
    <div class="row">
         <div class="col-md-6" style="display:none">
                 
         <asp:Literal ID="ltScripts" runat="server" ></asp:Literal>  
        <div id="chart_div" style="height:500px;" > 
            </div>     
   

        </div>
        <div class="col-md-4">
                   <div class="row">
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <div class="info-box-content">
              <div class="admin-ibox-head">
                <div class="admin-ibox-head-main">
                  <span class="admin-ibox-icon bg-aqua"><i class="fa fa-mail-reply"></i></span>
                  <span class="info-box-text">Deposit Request</span>
                </div>
                <span class="admin-ibox-chip">Fund In</span>
              </div>
              <div class="admin-ibox-stats">
                <div class="admin-ibox-stat">
                  <span class="admin-ibox-stat-label">Total</span>
                  <span class="admin-ibox-stat-value"><asp:Label ID="LblDepositlTotal" runat="server" Text="0"></asp:Label></span>
                </div>
                <div class="admin-ibox-stat is-pending">
                  <span class="admin-ibox-stat-label">Pending</span>
                  <span class="admin-ibox-stat-value"><asp:Label ID="LblDepositPending" runat="server" Text="0"></asp:Label></span>
                </div>
              </div>
              <a href="DepositRequestReport.aspx" class="admin-ibox-action">View details <i class="fa fa-arrow-right"></i></a>
            </div>
          
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <div class="info-box-content">
              <div class="admin-ibox-head">
                <div class="admin-ibox-head-main">
                  <span class="admin-ibox-icon bg-red"><i class="fa fa-share"></i></span>
                  <span class="info-box-text">Withdrawl Request</span>
                </div>
                <span class="admin-ibox-chip">Fund Out</span>
              </div>
              <div class="admin-ibox-stats">
                <div class="admin-ibox-stat">
                  <span class="admin-ibox-stat-label">Total</span>
                  <span class="admin-ibox-stat-value"><asp:Label ID="LblWithdrawlTotal" runat="server" Text="0"></asp:Label></span>
                </div>
                <div class="admin-ibox-stat is-pending">
                  <span class="admin-ibox-stat-label">Pending</span>
                  <span class="admin-ibox-stat-value"><asp:Label ID="LblWithdrawlPending" runat="server" Text="0"></asp:Label></span>
                </div>
              </div>
              <a href="WithdrawlRequestReport.aspx" class="admin-ibox-action">View details <i class="fa fa-arrow-right"></i></a>
            </div>
          
          </div>
         
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <div class="info-box-content">
              <div class="admin-ibox-head">
                <div class="admin-ibox-head-main">
                  <span class="admin-ibox-icon bg-green"><i class="fa fa-envelope-o"></i></span>
                  <span class="info-box-text">News</span>
                </div>
                <span class="admin-ibox-chip">Updates</span>
              </div>
              <div class="admin-ibox-hero">
                <span class="admin-ibox-hero-value"><asp:Label ID="LblNewsCount" runat="server" Text="0"></asp:Label></span>
                <span class="admin-ibox-hero-caption">Published news items</span>
              </div>
              <a href="NewsAdd.aspx" class="admin-ibox-action">Manage news <i class="fa fa-arrow-right"></i></a>
            </div>
         
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <div class="info-box-content">
              <div class="admin-ibox-head">
                <div class="admin-ibox-head-main">
                  <span class="admin-ibox-icon bg-yellow"><i class="fa fa-circle-o"></i></span>
                  <span class="info-box-text">Purchase Pending</span>
                </div>
                <span class="admin-ibox-chip">Orders</span>
              </div>
              <div class="admin-ibox-hero">
                <span class="admin-ibox-hero-value"><asp:Label ID="LblPurchaseProductCount" runat="server" Text="0"></asp:Label></span>
                <span class="admin-ibox-hero-caption">Awaiting approval</span>
              </div>
              <a href="#" class="admin-ibox-action">View pending <i class="fa fa-arrow-right"></i></a>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
      </div>

        </div>
          <div class="col-md-8">
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>  
              <div class="admin-chart-card">
                  <div class="admin-chart-card-header">
                      <div class="admin-chart-card-title-wrap">
                          <h2 class="admin-chart-card-title">Registration Analytics</h2>
                          <div class="admin-chart-card-subtitle">
                              <strong id="lblTotalJoinsThisWeek">--</strong>
                              <span>Total Joins (This Week)</span>
                          </div>
                      </div>
                      <div class="admin-chart-card-actions">
                          <div class="admin-chart-pill-selector">
                              <button type="button" class="admin-chart-pill-btn active">Weekly</button>
                              <button type="button" class="admin-chart-pill-btn">Monthly</button>
                              <button type="button" class="admin-chart-pill-btn">Yearly</button>
                          </div>
                          <div class="admin-chart-legends">
                              <span class="admin-chart-legend-item"><span class="legend-dot bg-red-dot"></span> Joins</span>
                              <span class="admin-chart-legend-item"><span class="legend-dot bg-gray-dot"></span> Trend</span>
                          </div>
                      </div>
                  </div>
                  <div class="admin-chart-card-body">
                      <div id="Div1" class="admin-chart-element"></div>
                  </div>
              </div>
          </div>
      
        <!-- /.col (LEFT) -->
      
        </div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
   
    </asp:Content>
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
            <a href="UserReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <a href="TransactionReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
            <span class="info-box-icon bg-aqua"><i class="fa fa-mail-reply"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Deposit Request</span>
              Total : <asp:Label ID="LblDepositlTotal" runat="server" Text="" Font-Bold="true"></asp:Label>    <br />      
              Pending : <asp:Label ID="LblDepositPending" runat="server" Text="" Font-Bold="true"></asp:Label> <br /> 
                 <a href="DepositRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
          
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-share"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Withdrawl Request</span>
                 Total : <asp:Label ID="LblWithdrawlTotal" runat="server" Text="" Font-Bold="true"></asp:Label>      <br />         
              Pending : <asp:Label ID="LblWithdrawlPending" runat="server" Text="" Font-Bold="true"></asp:Label>  <br />   
                 <a href="WithdrawlRequestReport.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
          
          </div>
         
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-green"><i class="fa fa-envelope-o"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">News</span>
              <span class="info-box-number"><asp:Label ID="LblNewsCount" runat="server" Text=""></asp:Label></span>
                   <a href="NewsAdd.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
         
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-yellow"><i class="fa fa-circle-o"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Purchase Pending</span>
              <span class="info-box-number"><asp:Label ID="LblPurchaseProductCount" runat="server" Text=""></asp:Label></span>
                 <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
        <div id="Div1" style="height:500px;" >     
     </div>  

        </div>
      
        <!-- /.col (LEFT) -->
      
        </div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
   
    </asp:Content>
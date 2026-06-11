<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Incomestatement.aspx.cs" Inherits="user_Incomestatement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
   
        .incomedetails, .income-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .incomedetails td, .income-table td, .income-table th {
           border: 1px solid #000;
            padding: 2px;
            text-align: left;
            font-size: 14px;
        }
        .incomedetails td {
            font-weight: bold;
        }
        
             
   </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
 

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <div class="row">
          <div class="col-md-12">
                 <div class="box-body">
      <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />
                                </div>
        




                            </div>
                     </div>
              </div>
           </div>
    <div class="container">

        <div class="header" style="text-align:center;">
            <h1>MANIRAYA MARKETING PRIVATE LIMITED</h1>
            <p>ADDRESS: #33 1st floor MANIRAYA marketing pvt ltd 9th A cross HIG A sector yelahanka new town Bangalore Karnataka 560064</p>
            <p>Ph: 8884448586, E-Mail: growmaniraya@gmail.com</p>
        </div>

        <table class="incomedetails">
            <tr >
                <td>User ID:</td>
                <td>
                    <asp:Label ID="LblUserid" runat="server" Text="Label"></asp:Label></td>
                <td>Pay To:</td>
                <td><asp:Label ID="Lblusername" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Name:</td>
                <td><asp:Label ID="Lblusernametwo" runat="server" Text="Label"></asp:Label></td>
                <td>Bank Name:</td>
                <td><asp:Label ID="Lblbankname" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Address:</td>
                <td ><asp:Label ID="Lbladdress" runat="server" Text="Label"></asp:Label><br><asp:Label ID="Lblarea" runat="server" Text="Label"></asp:Label><asp:Label ID="LblCity" runat="server" Text="Label"></asp:Label><asp:Label ID="LblPincode" runat="server" Text="Label"></asp:Label></td>
                <td>Account No:</td>
                <td><asp:Label ID="Lblaccountno" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>State:</td>
                <td><asp:Label ID="LblState" runat="server" Text="Label"></asp:Label></td>
                <td>IFSC Code:</td>
                <td><asp:Label ID="Lblifsccode" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Mobile No:</td>
                <td><asp:Label ID="LblMobile" runat="server" Text="Label"></asp:Label></td>
                <td>PAN No:</td>
                <td><asp:Label ID="LblPanno" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>

             <h2 class="text-center">MONTHLY COMBINED INCOME STATEMENT</h2>
        <p style="text-align: center;">Business <asp:Label ID="LblFromdate" runat="server" Text="Label"></asp:Label></p>
        <div class="row">
   
           
              <div class="col-md-12" >
        <table class="income-table" >
            <tr>
                <th colspan="3" style="text-align:center;"> INCOME SUMMERY</th>
            </tr>
            <tr>
                <td>Matching Income (Daily Income)</td>
                <td>:</td>
                <td class="right-align">  <asp:Label ID="Lblbinaryincome" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Direct Income (Daily Income)</td>
                     <td>:</td>
                <td class="right-align"><asp:Label ID="LblDirectincome" runat="server" Text="Label"></asp:Label></td>
            </tr>
			 <tr>
                <td>Booster Income (Daily Income)</td>
                     <td>:</td>
                <td class="right-align"><asp:Label ID="LblBoosterincome" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Level Income (Weekly Inocme)</td>
                     <td>:</td>
                <td class="right-align"><asp:Label ID="LblLevelINcome" runat="server" Text="Label"></asp:Label></td>
            </tr>
           
            <tr>
                <td>Self Income (Monthly)</td>
                     <td>:</td>
                <td class="right-align"><asp:Label ID="LblSelfincome" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Repurchase Income (Monthly)</td>
                     <td>:</td>
                <td class="right-align"><asp:Label ID="LblREpurchase" runat="server" Text="Label"></asp:Label></td>
            </tr>
			
			  <tr class="total-row">
                <td>Total Daily Income</td>
                <td>:</td>
                <td class="right-align"><asp:Label ID="LblTotaldaily" runat="server" Text="Label"></asp:Label></td>
            </tr>
           
            <tr class="total-row">
                <td>Total Weekly Income</td>
                <td>:</td>
                <td class="right-align"><asp:Label ID="LblTotalweekly" runat="server" Text="Label"></asp:Label></td>
            </tr>
			
			
			 <tr class="total-row">
                <td>Total Monthly Income</td>
                <td>:</td>
                <td class="right-align"><asp:Label ID="LblTotalMonthly" runat="server" Text="Label"></asp:Label></td>
            </tr>
			
				 <tr class="total-row">
                <td>Total Daily Income + Total Weekly Income + Total Monthly Income </td>
                <td>:</td>
                <td class="right-align"><asp:Label ID="LblTotalincome" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
            </div>
            
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>

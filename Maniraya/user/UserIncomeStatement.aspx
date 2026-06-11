<%@ Page Title="" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="UserIncomeStatement.aspx.cs" Inherits="user_UserIncomeStatement" %>

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
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search"  />
                                </div>
        




                            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


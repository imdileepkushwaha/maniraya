<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="GSTReport.aspx.cs" Inherits="GSTReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">

    <section class="content-header">
        <h1>GST Report
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Report</a></li>
            <li class="active">GST Report </li>

        </ol>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">GST Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                   <div class="col-md-2" style="display:none;">
                                    <asp:TextBox ID="txtuserid" runat="server" placeholder="UserId" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/img/excel-img.png" ToolTip="Download Excel" CssClass="pull-right" Width="40px" OnClick="imgExcel_Click" />

                                </div>




                            </div>
                            <div class="row">
                                <div class="col-md-10 text-center">
                                    <br />
                                    <asp:RadioButton ID="RDBtnRechargeWallet" runat="server" Text="User" GroupName="A" AutoPostBack="true" OnCheckedChanged="RDBtnRechargeWallet_CheckedChanged" />
                                    <asp:RadioButton ID="RdBtnUtilityWallet" runat="server" Text="Franchisee" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnUtilityWallet_CheckedChanged" />
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged" Width="80px">


                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-12 table-responsive">
                                    <asp:GridView ID="grdAccountsList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-responsive datatable"
                                        Width="100%" GridLines="Horizontal" ShowFooter="true" OnRowDataBound="grdAccountsList_RowDataBound">
                                        <FooterStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderNoe" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchasedate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Purchasedate","{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserID" runat="server" Text='<%# Eval("userID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProductName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                             <asp:TemplateField HeaderText="Amount/Pcs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PurchaseAmount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurchaseAmount" runat="server" Text='<%# Eval("PurchaseAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTPER" runat="server" Text='<%# Eval("GSTPER") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbCGST" runat="server" Text='<%# Eval("CGST") %>'></asp:Label>
                                                </ItemTemplate>
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblTotalCGST" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalSGST" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="IGST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalIGST" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalTotalAmount" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                          
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


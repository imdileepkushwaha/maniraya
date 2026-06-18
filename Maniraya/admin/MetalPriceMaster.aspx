<%@ Page Title="Metal Price Master" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="MetalPriceMaster.aspx.cs" Inherits="MetalPriceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .metal-price-card {
            border: 1px solid #e8edf3;
            border-radius: 12px;
            padding: 18px;
            margin-bottom: 16px;
            background: linear-gradient(180deg, #fff 0%, #fafbfd 100%);
        }
        .metal-price-card h4 {
            margin: 0 0 4px;
            font-weight: 700;
            color: #1e293b;
        }
        .metal-price-card .metal-unit {
            font-size: 12px;
            color: #64748b;
            margin-bottom: 12px;
            display: block;
        }
        .metal-price-card.gold { border-left: 4px solid #e5a906; }
        .metal-price-card.silver { border-left: 4px solid #94a3b8; }
        .metal-price-card.diamond { border-left: 4px solid #38bdf8; }
        .metal-price-input-wrap {
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .metal-price-input-wrap .currency {
            font-weight: 700;
            color: #334155;
            font-size: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Commodities Price</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product Management</a></li>
            <li class="active">Metal Price Master</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-5 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Update Metal Prices</h3>
                        </div>
                        <div class="box-body">
                            <p class="admin-section-hint">Set current rates for Gold, Silver, and Diamond. These values are saved in the database and can be used across the application.</p>

                            <div class="metal-price-card gold">
                                <h4><i class="fa fa-circle" style="color:#e5a906"></i> Gold Price</h4>
                                <span class="metal-unit">Rate per gram (₹)</span>
                                <div class="metal-price-input-wrap">
                                    <span class="currency">₹</span>
                                    <asp:TextBox ID="txtGoldPrice" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="0.01"></asp:TextBox>
                                </div>
                            </div>

                            <div class="metal-price-card silver">
                                <h4><i class="fa fa-circle" style="color:#94a3b8"></i> Silver Price</h4>
                                <span class="metal-unit">Rate per gram (₹)</span>
                                <div class="metal-price-input-wrap">
                                    <span class="currency">₹</span>
                                    <asp:TextBox ID="txtSilverPrice" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="0.01"></asp:TextBox>
                                </div>
                            </div>

                            <div class="metal-price-card diamond">
                                <h4><i class="fa fa-circle" style="color:#38bdf8"></i> Diamond Price</h4>
                                <span class="metal-unit">Rate per carat (₹)</span>
                                <div class="metal-price-input-wrap">
                                    <span class="currency">₹</span>
                                    <asp:TextBox ID="txtDiamondPrice" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="0.01"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save Prices" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-7 col-sm-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Saved Metal Prices</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="MetalType" HeaderText="Metal" />
                                    <asp:TemplateField HeaderText="Price (₹)">
                                        <ItemTemplate>
                                            <%# string.Format("{0:N2}", Eval("Price")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PriceUnit" HeaderText="Unit" />
                                    <asp:TemplateField HeaderText="Last Updated">
                                        <ItemTemplate>
                                            <%# Eval("UpdatedOn", "{0:dd-MMM-yyyy hh:mm tt}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

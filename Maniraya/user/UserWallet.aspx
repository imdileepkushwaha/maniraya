<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="UserWallet.aspx.cs" Inherits="UserWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>User Wallet</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My wallet</a></li>
            <li class="active">User Wallet</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Main Wallet Balance Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-green">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblCredited" runat="server" Text="Label"></asp:Label></h3>

                                            <p>Credited</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-plus-square-o"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-red">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblDebited" runat="server" Text="Label"></asp:Label></h3>

                                            <p>Debited</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-minus-square-o"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-aqua">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblCurrentWallet" runat="server" Text="Label"></asp:Label></h3>

                                            <p>Balance</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-inr"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <div class="box-header with-border" style="display:none">
                            <h3 class="box-title">Shopping Wallet Balance Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-green">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblCredited2" runat="server" Text="Label"></asp:Label></h3>
                                            <p>Credited</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-plus-square-o"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-red">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblDebited2" runat="server" Text="Label"></asp:Label></h3>
                                            <p>Debited</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-minus-square-o"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-xs-6">
                                    <!-- small box -->
                                    <div class="small-box bg-aqua">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="LblCurrentWallet2" runat="server" Text="Label"></asp:Label></h3>

                                            <p>Balance</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-inr"></i>
                                        </div>
                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>

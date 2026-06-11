<%@ Page Title="Purchase Item" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseItem.aspx.cs" Inherits="admin_PurchaseItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }
    </script>
    <script type="text/javascript">
        function gettotal() {

            var Quantity = 0, Amount = 0, TotalAMount = 0;
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value != "") {
                Quantity = document.getElementById("<%=TxtQuantity.ClientID%>").value;
            }
            if (document.getElementById("<%=TxtAmount.ClientID%>").value != "") {
                Amount = document.getElementById("<%=TxtAmount.ClientID%>").value;
            }
            var TotalAMount = Quantity * Amount;


            document.getElementById("<%=TxtTotalAmount.ClientID%>").innerText = TotalAMount;
        }

        function validate() {
            if (document.getElementById("<%=TxtQuantity.ClientID%>").value == "") {

                  alert('Enter Quantity');
                  document.getElementById("<%=TxtQuantity.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <style type="text/css">
        @import url(http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css);

        .col-item {
            border: 1px solid #E1E1E1;
            border-radius: 5px;
            background: #FFF;
            margin-bottom: 20px;
        }

            .col-item .photo img {
                margin: 0 auto;
                width: 100%;
            }

            .col-item .info {
                padding: 10px;
                border-radius: 0 0 5px 5px;
                margin-top: 1px;
            }

            .col-item:hover .info {
                background-color: #F5F5DC;
            }

            .col-item .price {
                /*width: 50%;*/
                float: left;
                margin-top: 5px;
            }

            .col-item .price-text-color {
                font-size: 18px;
                margin-top: 3px;
            }

            .col-item .price h5 {
                line-height: 22px;
                margin: 0;
                font-size: 15px;
                font-weight: bold;
                color: #777;
            }

        .price-text-color {
            color: #219FD1;
        }

        .col-item .info .rating {
            color: #777;
            font-size: 20px;
            vertical-align: middle;
        }

        .col-item .rating {
            /*width: 50%;*/
            float: left;
            font-size: 17px;
            text-align: right;
            line-height: 52px;
            margin-bottom: 10px;
            height: 52px;
        }

        .col-item .separator {
            border-top: 1px solid #E1E1E1;
        }

        .clear-left {
            clear: left;
        }

        .col-item .separator p {
            line-height: 20px;
            margin-bottom: 0;
            margin-top: 10px;
            /*text-align: center;
        width: 100%;*/
        }

            .col-item .separator p i {
                margin-right: 5px;
            }

        .col-item .btn-add {
            width: 50%;
            float: left;
        }

        .col-item .btn-add {
            border-right: 1px solid #E1E1E1;
        }

        .col-item .btn-details {
            width: 50%;
            float: left;
            padding-left: 10px;
        }

        .controls {
            margin-top: 20px;
        }

        [data-slide="prev"] {
            margin-right: 10px;
        }

        .product_grid a {
            text-decoration: none;
        }

        .product_item {
            display: inline-block;
            background: #fff;
            border: 1px solid #ccc;
            padding: 10px;
            position: relative;
            overflow: hidden;
        }

        .product_sale {
            position: absolute;
            z-index: 99;
            right: -37px;
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            transform: rotate(45deg);
            font-size: 13px;
            margin-top: 23px;
        }

        .product_image {
            position: relative;
            overflow: hidden;
        }

            .product_image img {
            }

        .product_title {
            float: left;
            width: 100%;
            text-transform: uppercase;
        }

            .product_title h5 {
                margin: auto;
                font-size: 2.1em;
                font-weight: 500;
                line-height: 1;
                padding-bottom: 7px;
            }

        .product_price a {
            color: #ea2e49;
            padding-left: 6px;
        }

        .price_old {
            color: #ea2e49;
            text-decoration: line-through;
        }

        .product_price span {
            font-size: 1.1em;
            line-height: 1;
            padding-left: 2px;
        }

        .product_desc p {
            margin: 0;
            line-height: 1.3;
            padding: 7px 5px;
        }

        .product_rating {
            float: right;
            width: 100px;
            height: 20px;
            overflow: hidden;
            background: url(https://bit.ly/1B4PjyM) top left no-repeat;
            background-position: 0 76%;
        }

        .product_buttons {
            -webkit-font-smoothing: antialiased;
            -moz-font-smoothing: antialiased;
            font-smoothing: antialiased;
        }

            .product_buttons .product_heart:hover {
                color: #DF0404;
                background: rgba(255, 255, 255, 0.5);
            }

            .product_buttons .product_compare:hover {
                color: rgb(18, 150, 18);
                background: rgba(255, 255, 255, 0.5);
            }

            .product_buttons .add_to_cart:hover {
                color: #4DC8D3;
                ;
                background: rgba(255, 255, 255, 0.5);
            }

        /* Custom, iPhone Retina */
        @media only screen and (min-width : 320px) {
            .product_sale p {
                margin: 0px;
                color: #fff;
                background: #ff0000;
                padding: 3px 34px;
                box-shadow: 0 2px 8px 0 rgba(0, 0, 0, 0.4);
            }

            .product_values {
                float: left;
                width: calc(100% - 100px);
                padding: 0 10px;
            }

            .product_rating {
                margin-right: 10px;
            }

            .product_image {
                height: 150px;
                float: left;
                width: 100px;
            }

                .product_image .product_buttons {
                    display: none;
                }

            .product_desc {
                overflow: hidden;
                float: left;
                line-height: 1;
            }

            .product_values .product_buttons {
                position: relative;
                text-align: left;
                float: left;
                margin-top: 7px;
            }

                .product_values .product_buttons button {
                    color: #252525;
                    background: rgba(255, 255, 255, 1);
                    font-size: 1em;
                    border-radius: 50%;
                    width: 40px;
                    height: 40px;
                    border: 1px solid #000;
                }
        }

        /* Extra Small Devices, Phones */
        @media only screen and (min-width : 480px) {
            .product_image {
                height: 250px;
                width: 175px;
            }

            .product_values {
                width: calc(100% - 175px);
            }
        }

        @media only screen and (min-width: 678px) {
            .product_item {
                width: 49.5%;
            }

            .product_image {
                height: 150px;
                width: 100px;
            }

            .product_values {
                width: calc(100% - 100px);
            }
        }

        /* Small Devices, Tablets */
        @media only screen and (min-width : 768px) {
        }

        /* Medium Devices, Desktops */
        @media only screen and (min-width : 992px) {
            .product_image {
                height: 199px;
                width: 175px;
            }

            .product_values {
                width: calc(100% - 175px);
            }

            .product_desc {
                max-height: 200px;
            }
        }

        /* Large Devices, Wide Screens */
        @media only screen and (min-width : 1200px) {
            .product_item {
                width: 33%;
            }

            .product_desc {
                max-height: 131px;
            }
        }



        /*==========  Non-Mobile First Method  ==========*/

        /* Large Devices, Wide Screens */
        @media only screen and (max-width : 1200px) {
        }

        /* Medium Devices, Desktops */
        @media only screen and (max-width : 992px) {
            .product_desc {
                max-height: 67px;
            }
        }

        /* Small Devices, Tablets */
        @media only screen and (max-width : 768px) {
        }

        /* Extra Small Devices, Phones */
        @media only screen and (max-width : 480px) {
            .product_title h5 {
                font-weight: bold;
            }
        }

        /* Custom, iPhone Retina */
        @media only screen and (max-width : 320px) {
            .product_sale {
                display: none;
            }

            .product_image img {
                position: relative;
            }

            .product_price span {
                float: left;
                width: 100%;
            }

            .product_desc {
                display: none;
            }

            .product_buttons {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Purchase Item   
        </h1>
        <div class="row">
            <div class="col-md-5">
                <ol class="breadcrumb">
                    <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">My Repurchase</a></li>
                    <li class="active">Purchase Item</li>

                </ol>
            </div>
            <div class="col-md-7">
                <div class="box box-primary">
                    <div class="box-header with-border">

                        <div style="float: right; color: white; display:none">
                            Your Main Balance  :   <i class="fa fa-inr"></i>
                            <asp:Label ID="Lblbalance" runat="server" Text="Balance"></asp:Label>
                        </div>
                        <div style="float: right; color: white;">
                            Your Shopping Balance  :   <i class="fa fa-inr"></i>
                            <asp:Label ID="LblUtility" runat="server" Text="Balance"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">



                <div class="col-md-12">
                    <asp:Panel ID="PurchasePanel" runat="server" Visible="false">

                        <div class="box-body">

                            <div class="col-md-6">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Item </h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" style="color: #ea2e49">
                            </div>

                            <div class="row">
                                <div class="col-md-12" style="color: #ea2e49">
                                    <div class="form-group table-responsive">

                                        <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px" />
                                                        <asp:Label ID="LblProductImageG" runat="server" Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCatId" runat="server" Text='<%#Eval("CatID") %>'></asp:Label>
                                                        <asp:Label ID="LblProductCodeG" runat="server" Text='<%#Eval("ProductId") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblProductNameG" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount/Peices">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblProductAmountG" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product BV">
                                                    <ItemTemplate>


                                                        <asp:Label ID="LblBv" runat="server" Text='<%#Eval("BV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="label1" runat="server" Text="Total : "></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Calculate Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDelete" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-remove" aria-hidden="true"></i></asp:LinkButton>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total Purchase</label>
                                        <asp:TextBox ID="TxtTotalpurchase" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Courier Charge</label>
                                        <asp:TextBox ID="TxtShipping" CssClass="form-control" runat="server" Enabled="false" Text="150.00"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total Amount</label>
                                        <asp:TextBox ID="TXTTTAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Wallet Type</label>
                                        <asp:DropDownList ID="DDLSTWallet" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">Main Wallet</asp:ListItem>
                                            <asp:ListItem Value="2">Shopping Wallet</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="box-footer">

                            <asp:HiddenField ID="HDTotal" runat="server" />

                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Remove All" OnClick="btnCancel_Click" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="box box-primary">

                    <div class="box-body">


                        <div class="row">
                            <asp:Repeater ID="dlCustomers" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-4">
                                        <div class="col-item">
                                            <div class="photo">
                                                <img src='<%# Eval("Image") %>' class="img-responsive" alt="a" style="width: 72%; padding-top: 20px; height: 228px;" />
                                                <asp:Label ID="lblim" runat="server" Text='<%#Eval("Image") %>' Visible="false"></asp:Label>
                                            </div>
                                            <div class="info">
                                                <div class="row">
                                                    <div class="price col-md-6">
                                                        <h5>
                                                            <asp:Label ID="lblstatename" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label></h5>
                                                        <h6 class="price-text-color"><i class="fa fa-inr"></i>
                                                            <asp:Label ID="lblstatename1" runat="server" Text='<%#Eval("Amount") %>' ForeColor="#ff0000"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="rating hidden-sm col-md-6">
                                                        <asp:HiddenField ID="HDBV" runat="server" Value='<%#Eval("BV") %>' />
                                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("ProductId") %>' ForeColor="#009933"></asp:Label><asp:HiddenField ID="HDCategory" runat="server" Value='<%#Eval("CategoryID") %>' />

                                                    </div>
                                                </div>
                                                <div class="separator clear-center">
                                                    <p class="btn-details pull-center">
                                                        <asp:LinkButton ID="lnkph" runat="server" CssClass="btn btn-xs btn-primary" CommandName="photolarge" CommandArgument='<%# Eval("ProductId") %>'>
                                          <i class="fa fa-eye"></i> View
                                                        </asp:LinkButton>
                                                    </p>

                                                    <%-- <p class="btn-details pull-right text-right text-danger">
                                                            <i class="fa fa-list"></i>
                                                            <label style="font-size: 16px; font-weight: bold;">
                                                                Stock</label>
                                                            <asp:Label ID="LblStckAvail" runat="server" Text='<%#Eval("Stock") %>' style="font-size: 16px; font-weight: bold;"></asp:Label>
                                                        </p>--%>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12  text-center">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="BuyProduct" CommandArgument='<%# Eval("ProductId") %>'>
                    <img src='../ProductImage/addtocard.png' class="img-responsive" style="display: -webkit-inline-box;"/>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                        </div>


                                        <%-- <asp:Image ID="Image1" runat="server" ImageUrl= Height="200px" Width="200px"  /><br />--%>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>











                    </div>
                    <div class="box-footer">



                        <div class="row">
                            <div class="col-sm-5">
                                <div class="dataTables_info" id="example2_info" role="status" aria-live="polite">
                                    <asp:Label ID="LblRecordCount" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-7">
                                <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                    <ul class="pagination">

                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>


                                                <li class="paginate_button ">

                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>

                                                </li>

                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>

                            </div>
                        </div>


                    </div>

                </div>
                <div id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="container-fluid">
                                    <h4 class="modal-title pull-left">Full Details</h4>
                                    <span class="pull-right"><b>Product Id :
                                               <asp:Label ID="LblProductCode" runat="server" Text=""></asp:Label></b></span>
                                </div>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">

                                    <asp:HiddenField ID="HdCatId" runat="server" />
                                    <asp:HiddenField ID="HdBuisnessVolume" runat="server" />
                                    <div class="product_item" style="width: 100%; color: #777">
                                        <div class="product_sale">
                                            <p>
                                                <asp:Label ID="LblcategoryName123" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="product_image">
                                            <div class="pro-img">
                                                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                                    <!-- Indicators -->
                                                    <ol class="carousel-indicators">
                                                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                        <li data-target="#myCarousel" data-slide-to="1"></li>
                                                        <li data-target="#myCarousel" data-slide-to="2"></li>
                                                    </ol>

                                                    <!-- Wrapper for slides -->
                                                    <div class="carousel-inner">
                                                        <div class="item active">
                                                            <asp:Image ID="Image2" runat="server" Width="570px" Height="200px" />
                                                            <%--<asp:Image ID="Image2" runat="server" Width="100%" />--%>
                                                            <%-- <img src="../ProductImage/636480744192755102Chrysanthemum.jpg" alt="image" runat="server">--%>
                                                        </div>

                                                        <div class="item">
                                                            <asp:Image ID="Image3" runat="server" Width="570px" Height="200px" />

                                                        </div>

                                                        <div class="item">
                                                            <asp:Image ID="Image4" runat="server" Width="570px" Height="200px" />

                                                        </div>
                                                    </div>

                                                    <!-- Left and right controls -->
                                                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                                        <span class="sr-only">Previous</span>
                                                    </a>
                                                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                                        <span class="glyphicon glyphicon-chevron-right"></span>
                                                        <span class="sr-only">Next</span>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="product_values">
                                            <div class="product_title">

                                                <h4>
                                                    <asp:Label ID="LblProductName" runat="server" Text=""></asp:Label></h4>
                                            </div>
                                            <div>
                                                <h5>MRP : <i class="fa fa-inr"></i>
                                                    <asp:Label ID="LblMRP" runat="server" Text=""></asp:Label></h5>
                                            </div>
                                            <div>
                                                <h5>B.V : <i class="fa fa-circle-o"></i>
                                                    <asp:Label ID="LblBv" runat="server" Text=""></asp:Label></h5>
                                            </div>
                                            <%--  <div>
                                                       <h5>Available Stock : <i class="fa fa-chrome"></i>
                                                           <asp:Label ID="LblStock" runat="server" Text=""></asp:Label></h5>
                                                   </div>--%>

                                            <div class="product_price">
                                                <a href="#">Amount After Discount
          <i class="fa fa-inr"></i>
                                                    <span class="price_new">
                                                        <asp:Label ID="LblAmount" runat="server" Text=""></asp:Label></span></a>

                                            </div>
                                            <div class="product_desc">
                                                <p class="truncate">
                                                    <asp:Label ID="LblDescription" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">

                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>



                <div id="Div1" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Buy Product</h4>

                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="TxtImage" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                            <label>Product Code :</label>
                                            <asp:TextBox ID="TxtProductCode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Product Name :</label>
                                            <asp:TextBox ID="TxtProductName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Amount/Pieces :</label>
                                            <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Quantity :</label>
                                            <asp:TextBox ID="TxtQuantity" CssClass="form-control" runat="server" TextMode="Number" onchange="gettotal();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Total Amount :</label>
                                            <asp:Label ID="TxtTotalAmount" CssClass="form-control" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                        </div>
                                    </div>


                                </div>







                            </div>
                            <div class="modal-footer">

                                <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="BtnAdd_Click" OnClientClick="validate();" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

            <script src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
            <script src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
            <script type="text/javascript">
                function showModal1() {
                    $('#Div1').modal({ backdrop: 'static', keyboard: false })
                }
                function Closepopup1() {
                    $('#Div1').modal('hide');
                    $('body').removeClass('modal-open');
                    $('body').css('padding-right', '0');
                    $('.modal-backdrop').remove();

                }

            </script>


            <script type="text/javascript">


                function showModal() {
                    $('#myModal').modal({ backdrop: 'static', keyboard: false })
                }
                function Closepopup() {
                    $('#myModal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('body').css('padding-right', '0');
                    $('.modal-backdrop').remove();

                }
            </script>

            <script>
                $(function () {
                    $('#example1').DataTable()
                    $('#example2').DataTable({
                        'paging': true,
                        'lengthChange': false,
                        'searching': false,
                        'ordering': true,
                        'info': true,
                        'autoWidth': false
                    })
                })
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>



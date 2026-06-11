<%@ Page Title="Purchase Item" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="FranchiseeSearch.aspx.cs" Inherits="FranchiseeSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
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
        <h1>Franchisee Detail
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My Repurchase</a></li>
            <li class="active">Franchisee Detail</li>

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
                            <h3 class="box-title">Details</h3>
                            <div style="float: right; color:black;">
                                <a id="lnksearch" class="btn btn-xs btn-primary" href="javascript:showSearchModal();">
                                          Search
                                                            </a>
                            </div>
                        </div>
                        <div class="box-body">


                            <div class="row">
                                <asp:Repeater ID="dlCustomers" runat="server" >
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <center>
                                            <div class="col-item">
                                                <div class="photo">
                                                  <asp:ImageButton ID="imgbtn" runat="server" ImageUrl='<%# Eval("Image") %>' CssClass="img-responsive" style="width: 72%; height: 150px;" OnClick="imgclick" CommandArgument='<%#Eval("StateId").ToString()+"_"+Eval("CityId").ToString()+"_"+Eval("id").ToString() %>' />
                                                </div>
                                                <div class="info">
                                                    <div class="row">
                                                        <div class="price col-md-12">
                                                            <h5>
                                                                <asp:Label ID="lblstatename" runat="server" Text='<%#Eval("Username") %>'></asp:Label></h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12  text-center">
                                                            <a href='PurchaseItem.aspx?ID=<%#Eval("StateId").ToString()+"_"+Eval("CityId").ToString()+"_"+Eval("id").ToString() %>'><input type="button" class="btn btn-blue" value="Proceed"/>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                
                                            </div>
                                                </center>

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
                    </>
                     <div id="DivSearch" class="modal fade">
                        <div class="modal-dialog" style="top:20%;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Search</h4>

                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                                <label>State :</label>
                                                 <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select State</asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City :</label>
                                                     <asp:DropDownList ID="ddcity"  CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddcity_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Select City</asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                              
                                                  <label>Tehsil :</label>
                                                 <asp:DropDownList ID="ddlsttehsil" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="DDlstTehsil_SelectedIndexChanged"  >
                                            <asp:ListItem Value="0"> Select Tehsil</asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>


                                    </div>
                                      <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                               <label>Market :</label>
                                                     <asp:DropDownList ID="ddlstmarket"  CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0"> Select Market</asp:ListItem>
                                        </asp:DropDownList>
                                              
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                               <label>Pincode :</label>
                                                <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                              
                                            </div>
                                        </div>


                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="BtnSearchFranchisee_Click" />
                                    <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>          
                                </div>
                            </div>
                        </div>
                    </div>

                       <div id="Div_FDetails" class="modal fade">
                           <div class="modal-dialog" style="margin-top:60px;">
                               <div class="modal-content">
                                   <div class="modal-header">
                                       <div class="container-fluid">
                                           <h4 class="modal-title pull-left">Franchisee Details</h4>
                                       </div>
                                   </div>
                                   <div class="modal-body">
                                       <div class="container-fluid">
                                           <div class="product_item" style="width: 100%;">
                                              <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Name :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblfname" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Mobile No :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblmob" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Address :</div>
                                                  <div class="col-md-9"><asp:Label ID="lbladdress" runat="server"></asp:Label></div>
                                              </div>
                                               <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">State :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblstate" runat="server"></asp:Label></div>
                                                   <div class="col-md-3" style="font-weight:bold">City :</div>
                                                  <div class="col-md-3"><asp:Label ID="lblcity" runat="server"></asp:Label></div>
                                              </div>
                                                <div class="row">
                                                  <div class="col-md-3" style="font-weight:bold">Pincode :</div>
                                                  <div class="col-md-9"><asp:Label ID="lblpincode" runat="server"></asp:Label></div>
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

                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
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

      <script type="text/javascript">


          function showSearchModal() {
              $('#DivSearch').modal({ backdrop: 'static', keyboard: false })
          }
          function Closesearchpopup() {
              $('#DivSearch').modal('hide');
              $('body').removeClass('modal-open');
              $('body').css('padding-right', '0');
              $('.modal-backdrop').remove();
          }
    </script>

    <script type="text/javascript">


        function showFranchiseeModal() {
            $('#Div_FDetails').modal({ backdrop: 'static', keyboard: false })
        }
        function ClosesFranchiseepopup() {
            $('#Div_FDetails').modal('hide');
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

</asp:Content>



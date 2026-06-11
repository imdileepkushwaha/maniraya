<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="businessSummary.aspx.cs" Inherits="admin_businessSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">

        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan='999'>" + $(this).next().html() + "</td></tr>");
            $(this).attr("src", "img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "img/plus.png");
            $(this).closest("tr").next().remove();
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
      Business Summary
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Accounts</a></li>
        <li class="active">Business Summary</li>
      </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                          <h3 class="box-title">Business Summary</h3>
                        </div>

                        <div class="box-body">
                            
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">

                                        <label>User Id</label>
                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                   <div class="col-md-4">
                                    <div class="form-group">

                                        <label>From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form_date"></asp:TextBox>

                                    </div>
                                </div>
                                   <div class="col-md-4">
                                    <div class="form-group">

                                        <label>To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form_date"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                                                 

                        </div>

                        <div class="box-footer">

                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="btnReset_Click" />
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="box box-primary">

                        <div class="box-header with-border">
                          <h3 class="box-title">Details</h3>
                        </div>

                        <div class="box-body">
                            <div class="row table-responsive">

                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" 
                                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img alt="" style="cursor:pointer;" src="img/plus.png" />
                                                <asp:Panel ID="pnl1" runat="server" style="display:none;">

                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                                                        CssClass="table table-bordered table-hover datatable">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="User Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserMobile1" runat="server" Text='<%# Eval("UserMobile") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Operator">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOperator1" runat="server" Text='<%# Eval("Operator") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Sum Of Recharge Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSumOfRechargeAmount1" runat="server" Text='<%# Eval("sumOfRechargeAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Sum Of Debit Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSumOfDebitAmount1" runat="server" Text='<%# Eval("sumOfDebitAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Recharge API">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRechargeAPI1" runat="server" Text='<%# Eval("rechargeAPI") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("UserMobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sum Of Recharge Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSumOfRechargeAmount" runat="server" Text='<%# Eval("sumOfRechargeAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sum Of Debit Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSumOfDebitAmount" runat="server" Text='<%# Eval("sumOfDebitAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">

    <script type="text/javascript">
        $('.form_date').datepicker({
            format: 'dd/mm/yyyy',
        }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
        });
    </script>
    <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/mm/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>

</asp:Content>


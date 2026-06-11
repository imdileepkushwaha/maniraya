<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="RoyalityincomeReport.aspx.cs" Inherits="RoyalityincomeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Royality Income Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My income</a></li>
            <li class="active">>Royality Income Report</li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                          <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                      <label>Select Date</label>
                                        <asp:DropDownList ID="DDlstFromdate" runat="server" CssClass="form-control"></asp:DropDownList>
                                     
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                      
                                    
                                    </div>
                                </div>
                            </div>
                            

                             

                        </div>
                        <div class="box-footer">
                               
                           
                             <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel"  />


                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Detials</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Data Found" OnRowDataBound="grdBank_RowDataBound">   
                                                                                  <Columns>

 <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="img/plus.png" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover dataTable" >
                        <Columns>
                              <asp:TemplateField HeaderText="Inst.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInstno" runat="server" Text='<%#Eval("Instno") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Inst. date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInstdate" runat="server" Text='<%#Eval("Instdate") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Admin Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladmincharge" runat="server" Text='<%#Eval("admincharge") %>'></asp:Label><br />(
                                              <asp:Label ID="Lbladminper" runat="server" Text='<%#Eval("adminper") %>'></asp:Label>%)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="TDS Charge">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltdscharge" runat="server" Text='<%#Eval("tdscharge") %>'></asp:Label><br />(
                                              <asp:Label ID="Lbltds" runat="server" Text='<%#Eval("tds") %>'></asp:Label>%)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Payble Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaybleamount" runat="server" Text='<%#Eval("paybleamount") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status1") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>


                                                                                       <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                              <asp:TemplateField HeaderText="From date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("ToDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                                                                            <asp:TemplateField HeaderText="Userid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserid" runat="server" Text='<%#Eval("Userid") %>'></asp:Label>
                                               <asp:Label ID="lblDailyid" runat="server" Text='<%#Eval("Dailyid") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>           
                                                                                          <asp:TemplateField HeaderText="Pair">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmathingBv" runat="server" Text='<%#Eval("pair") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                                                                          <asp:TemplateField HeaderText="Income">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmathingIncome" runat="server" Text='<%#Eval("income") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                        
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "img/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "img/plus.png");
        $(this).closest("tr").next().remove();
    });

</script>
</asp:Content>
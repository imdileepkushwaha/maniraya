<%@ Page Title="" Language="C#" MasterPageFile="~/user/MasterPage.master" AutoEventWireup="true" CodeFile="DailyLevelReport.aspx.cs" Inherits="DailyLevelReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1 style="color:white;">Daily Level Income Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home > </a></li>
            <li><a href="#">My Income > </a></li>
            <li class="active">Daily Level Income</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" style="color:white;">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                   <div class="col-md-4">
                                       </div>
                                 <div class="col-md-4">
                                       </div>
                                 <div class="col-md-4">
                                        <asp:DropDownList ID="ddlRecordFilter" runat="server" CssClass="form-control pull-right margin-left-10" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlRecordFilter_SelectedIndexChanged" Width="80px">
                                       
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                                             <asp:ListItem>500</asp:ListItem>
                                             <asp:ListItem>All</asp:ListItem>      
                    </asp:DropDownList>
                                       </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="grdBank" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Data Found">                                            <Columns>
                                                <%--<asp:BoundField DataField="UserId" HeaderText="User ID" />
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" />                                              
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" />                                            
                                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                                <asp:BoundField DataField="RegDate" HeaderText="D. O. J." />
                                             <asp:BoundField DataField="ParentUserID" HeaderText="Parent ID" />
                                             <asp:BoundField DataField="parentname" HeaderText="Parent Name" />
                                             
                                                <asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                                
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                                <asp:BoundField DataField="fromuserid" HeaderText="From User ID" />
                                                <asp:BoundField DataField="Entrydate" HeaderText="Entry Date" />
                                            <asp:BoundField DataField="LevelNo" HeaderText="Level Number" />
                                              <asp:BoundField DataField="income" HeaderText="Amount" />
                                                <asp:BoundField DataField="adminper" HeaderText="adminper" />         
                                                <asp:BoundField DataField="admincharge" HeaderText="admincharge" />     
                                               <asp:BoundField DataField="paybleamount" HeaderText="paybleamount" />        
                                             <asp:BoundField DataField="Status1" HeaderText="Status" />                                             

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
</asp:Content>
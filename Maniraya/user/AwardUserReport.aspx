<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="AwardUserReport.aspx.cs" Inherits="AwardUserReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>My Reward</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i>Home</a></li>
            <li><a href="#">My income</a></li>
            <li class="active">>My Reward</li>
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
                            <h3 class="box-title">Detials</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row table-responsive">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="grdBank" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Data Found" OnRowDataBound="grdBank_RowDataBound">   
                                                                                  <Columns>
                                                                                       <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                                <asp:BoundField DataField="LeftId" HeaderText="Left User Required" />
                                                <asp:BoundField DataField="RightId" HeaderText="Right User Required" />
                                                <asp:BoundField DataField="leftpoint" HeaderText="Left user Achieved" /> 
                                                 <asp:BoundField DataField="rightpoint" HeaderText="Right user Achieved" />                                                                                           
                                                <asp:BoundField DataField="Leftbalance" HeaderText="Left user balance" />  
                                                   <asp:BoundField DataField="Rightbalance" HeaderText="Right user balance" />                                                                                      
                                                <asp:BoundField DataField="AwardName" HeaderText="Level" />
                                                                                       <asp:BoundField DataField="Amount" HeaderText="Bonus Income" />
                                                                                       <asp:BoundField DataField="Award" HeaderText="Reward" />
                                                                                       <asp:BoundField DataField="EDUCATIONINCOME" HeaderText="Self Education" />
                                                                                         <asp:BoundField DataField="CHILDEDUCATIONINCOME" HeaderText="Child Education" />
                                                <asp:BoundField DataField="achieved" HeaderText="Achieved" />
                                                <asp:BoundField DataField="Achievedate" HeaderText="Date of Achieved" />
                                          
                                        
                                  
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
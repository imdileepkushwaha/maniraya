<%@ Page Title="Define Recharge Commission" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="RechargeCommission.aspx.cs" Inherits="admin_DownlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
       Recharge Commission    
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Recharge</a></li>
        <li class="active">Recharge Commission</li>
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
                            <h3 class="box-title">Define recharge commission</h3>
                        </div>

                        <div class="box-body">
                              <div class="row">
                            <div class="col-md-3">
                                <label class="control-label"></label>
                            </div>
                            <div class="col-md-9">
                                <asp:Button ID="bntn_Save" runat="server" Text="Update" CssClass="btn btn-success"
                                    OnClick="bntn_Save_Click" />
                                <asp:Label ID="LblMsg" runat="server" ForeColor="Red" Text="Text F or P  Only while inserting commission type,"></asp:Label>
                                <asp:Label ID="LblMsg1" runat="server" ForeColor="Red" Text="Text S or C  Only on Surcharge"></asp:Label>
                            </div>
                        </div>
                               <div class="row table-responsive">
                                   <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                <asp:TemplateField HeaderText="Sr">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operator">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="txt_OperatorId" runat="server" Value='<%# Bind("OperatorID") %>'></asp:HiddenField>
                                        <asp:Label ID="lbl_OperatorName" runat="server" Text='<%# Bind("Operator") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="1">                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype1" runat="server" Text='<%# Bind("type1") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype1" runat="server" Text='<%# Bind("changetype1") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno1" runat="server" Text="1" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_1" runat="server" Text='<%# Bind("1") %>' Width="100PX" Style="text-align: right" onkeyup="CheckValue(this)"></asp:TextBox>
                                        <asp:RadioButtonList ID="rbtype1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="2">
                                    <ItemTemplate>
                                         <asp:Label ID="lbltype2" runat="server" Text='<%# Bind("type2") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype2" runat="server" Text='<%# Bind("changetype2") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno2" runat="server" Text="2" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_2" runat="server" Text='<%# Bind("2") %>' Width="100PX" Style="text-align: right" onkeyup="CheckValue(this)"></asp:TextBox>
                                        <asp:RadioButtonList ID="rbtype2" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype2" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="3">
                                    <ItemTemplate>
                                         <asp:Label ID="lbltype3" runat="server" Text='<%# Bind("type3") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype3" runat="server" Text='<%# Bind("changetype3") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno3" runat="server" Text="3" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_3" runat="server" Text='<%# Bind("3") %>' Width="100PX" Style="text-align: right" onkeyup="CheckValue(this)"></asp:TextBox>
                                     <asp:RadioButtonList ID="rbtype3" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype3" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="4">
                                    <ItemTemplate>
                                         <asp:Label ID="lbltype4" runat="server" Text='<%# Bind("type4") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype4" runat="server" Text='<%# Bind("changetype4") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno4" runat="server" Text="4" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_4" runat="server" Text='<%# Bind("4") %>' Width="100PX" Style="text-align: right" onkeyup="CheckValue(this)"></asp:TextBox>
                                    <asp:RadioButtonList ID="rbtype4" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype4" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                         </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="5">
                                    <ItemTemplate>
                                         <asp:Label ID="lbltype5" runat="server" Text='<%# Bind("type5") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype5" runat="server" Text='<%# Bind("changetype5") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno5" runat="server" Text="5" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_5" runat="server" Text='<%# Bind("5") %>' Width="100PX" Style="text-align: right" onkeyup="CheckValue(this)"></asp:TextBox>
                                     <asp:RadioButtonList ID="rbtype5" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype5" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6">
                                    <ItemTemplate>
                                         <asp:Label ID="lbltype6" runat="server" Text='<%# Bind("type6") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblchangetype6" runat="server" Text='<%# Bind("changetype6") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblrankno6" runat="server" Text="6" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_6" runat="server" Text='<%# Bind("6") %>' Width="100PX" Style="text-align: right"   onkeyup="CheckValue(this)"></asp:TextBox>
                                     <asp:RadioButtonList ID="rbtype6" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C">&nbsp;Com&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="S">&nbsp;Sur&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RadioButtonList ID="rbchangetype6" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="F">&nbsp;Flat&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="P">&nbsp;Per&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
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
</asp:Content>


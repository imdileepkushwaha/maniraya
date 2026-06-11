<%@ Page Title="Add IP" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="IPAdd.aspx.cs" Inherits="admin_IPAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
      ADD IP   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Settings</a></li>
        <li class="active">ADD IP </li>
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
              <h3 class="box-title">IP/Long Code List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row">
                         <div class="ibox-tools">                        
                           <a   href="#" ></a>
                        <asp:LinkButton ID="btnAdd" class="btn btn-round btn-success tooltips" OnClick="btnAdd_Click" style="margin:6px 10px 0px 0px;" runat="server"><i class="fa fa-plus" style="color:white;"></i></asp:LinkButton>
                    </div>
                        </div>
                   <div class="row table-responsive">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False">
                               <Columns>
                                    <asp:TemplateField Visible="true">
                                        <HeaderTemplate>ID</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>IP</HeaderTemplate>
                                        <ItemTemplate><%#Eval("IP") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%">
                                        <HeaderTemplate>UserID</HeaderTemplate>
                                        <ItemTemplate><%#chknull(Eval("UserId").ToString())%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>IP Type</HeaderTemplate>
                                        <ItemTemplate><%#Eval("IPType1") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Activity</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Status1") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Action</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkUpdate" class="btn btn-round btn-warning tooltips fa fa-pencil" CommandName="EditUser" runat="server"></asp:LinkButton>
                                                  <asp:LinkButton ID="LnkEnable" class="btn btn-round btn-info tooltips fa fa-ban" CommandName="EnableUser" runat="server"></asp:LinkButton>
                                             <asp:LinkButton ID="LnkDelete" class="btn btn-round btn-danger tooltips fa fa-trash-o" CommandName="delete1" runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField>
                                                   <HeaderTemplate>Enable/Disable</HeaderTemplate>
                                                   <ItemTemplate>                                                      
                                                       <asp:LinkButton ID="LnkEnable" class="btn btn-xs btn-red tooltips"  CommandName="EnableUser" runat="server"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                                   </ItemTemplate>
                                               </asp:TemplateField>  --%>
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
        <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always" >
        <ContentTemplate>
            <div id="myModalAdd" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Add IP/Long Code</h4>
                        </div>
                        <div class="modal-body">
                           <div class="form-horizontal">
                                <div class="row">
                                        <label class="col-lg-2 control-label">IP Type</label>
                                        <div class="col-lg-6">
                                            <asp:Label ID="LblUserFlag" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddl_Type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_Type_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select IP Type</asp:ListItem>
                                                <asp:ListItem Value="1">Long Code IP</asp:ListItem>
                                                <asp:ListItem Value="2">API IP</asp:ListItem>
                                                  <asp:ListItem Value="3">Call Back IP</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator10" class="error" runat="server" InitialValue="0" ValidationGroup="WhiteLabel" ControlToValidate="ddl_Type" ErrorMessage="Select Type"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="display:none;">
                                        <label class="col-lg-2 control-label">User ID</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txt_UserId" runat="server"  ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" class="error" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_UserId" ErrorMessage="Fill UserID !"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-lg-2 control-label">IP</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txt_IP" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator21" class="error" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_IP" ErrorMessage="Fill IP !"></asp:RequiredFieldValidator>


                                        </div>
                                    </div>                           
                            </div>
                        </div>
                        <div class="modal-footer">
                           <asp:Button ID="ButSubmit" runat="server" OnClick="ButSubmit_Click"  ValidationGroup="WhiteLabel" Text="Submit" class="btn btn-primary start" />
                            <asp:Button ID="btnClose" class="btn red" runat="server" OnClick="btnClose_Click" Text="Close" />
                                     <asp:Label ID="LblEditId" Text="0" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <script type="text/javascript">


        function showModal() {
            $('#myModalAdd').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#myModalAdd').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();


        }
    </script>
</asp:Content>


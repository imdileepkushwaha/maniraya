<%@ Page Title="Email Setting" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="EmailSetting.aspx.cs" Inherits="admin_EmailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
    Email Setting
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Settings</a></li>
        <li class="active">Email Setting</li>
      </ol>
    </section>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
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
              <h3 class="box-title">System Setting</h3>
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
                  <div class="row">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False">
                                 <Columns>
                                                  <asp:TemplateField Visible="false">
                                                   <HeaderTemplate>ID</HeaderTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="LblId" runat="server" Text='<%#Eval("ID") %>'></asp:Label></ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField>
                                                   <HeaderTemplate>User ID</HeaderTemplate>
                                                   <ItemTemplate><%#Checknull(Eval("MobileNo").ToString())%></ItemTemplate>
                                               </asp:TemplateField>                                                  
                                                  <asp:TemplateField>
                                                   <HeaderTemplate>Email From</HeaderTemplate>
                                                   <ItemTemplate><%#Eval("FromEmail") %></ItemTemplate>
                                               </asp:TemplateField>                                               
                                                <asp:TemplateField>
                                                   <HeaderTemplate>Port No.</HeaderTemplate>
                                                   <ItemTemplate><%#Eval("Port") %></ItemTemplate>
                                               </asp:TemplateField>
                                                <asp:TemplateField>
                                                   <HeaderTemplate>Host Name</HeaderTemplate>
                                                   <ItemTemplate><%#Eval("HostName") %></ItemTemplate>
                                               </asp:TemplateField>
                                                 <asp:TemplateField>
                                                   <HeaderTemplate>Email User Id</HeaderTemplate>
                                                   <ItemTemplate><%#Eval("EmailUserId") %></ItemTemplate>
                                               </asp:TemplateField>
                                                 <asp:TemplateField>
                                                   <HeaderTemplate>Password</HeaderTemplate>
                                                   <ItemTemplate><%#Eval("Password") %></ItemTemplate>
                                               </asp:TemplateField>                                                
                                               <asp:TemplateField>
                                                   <HeaderTemplate>Action</HeaderTemplate>
                                                   <ItemTemplate>
                                                       <asp:LinkButton ID="LnkUpdate" class="btn btn-round btn-warning tooltips" CommandName="EditUser" runat="server"><i class="fa fa-edit"></i></asp:LinkButton>
                                                       <asp:LinkButton ID="LnkEnable" class="btn btn-round btn-danger tooltips"  CommandName="Delete1" runat="server"><i class="fa fa-times-circle"></i></asp:LinkButton>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
        <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always" >
        <ContentTemplate>
            <div id="myModalAdd" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Email Setting</h4>
                        </div>
                        <div class="modal-body">
                           <div class="form-horizontal">
                                 <div class="form-group">
                                <label class="col-lg-2 control-label">Email User ID</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="Txt_EmailUserId" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator10" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="Txt_EmailUserId" ErrorMessage="Fill UserID"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Password</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txt_Password" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator21" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_Password" ErrorMessage="Fill Password !"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Email From</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txt_EmailFrom" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator1" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_EmailFrom" ErrorMessage="Fill Email From !"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="WhiteLabel" Display="Dynamic" ControlToValidate="txt_EmailFrom" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Fill Email ID"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Host Name</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txt_HostName" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator2" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_HostName" ErrorMessage="Fill Host Name !"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Port No.</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txt_Port" runat="server" onkeypress="return isNumberKey(event)" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator3" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="txt_Port" ErrorMessage="Fill Port No. !"></asp:RequiredFieldValidator>
                                </div>
                            </div>                             
                            </div>
                        </div>
                        <div class="modal-footer">
                              <asp:Button ID="ButSubmit" runat="server" OnClick="ButSubmit_Click" ValidationGroup="WhiteLabel" Text="Submit" class="btn btn-primary btn-round" />
                                         
                                     <asp:Label ID="LblEditId" Text="0" Visible="false" runat="server"></asp:Label>
                            <button type="button" class="btn red" data-dismiss="modal">Close</button>
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


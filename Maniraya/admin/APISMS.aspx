<%@ Page Title="Add SMS API" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="APISMS.aspx.cs" Inherits="admin_APISMS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#static').modal('show');
        }
        function openModal1() {
            try {
                var hdn_Id1 = document.getElementById('<%=hdn_Id1.ClientID %>');
                $('#staticconfirm' + hdn_Id1.value).modal('show');
            }
            catch (err) {
                alert(err);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
      <section class="content-header">
      <h1>
      Add SMS API   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">API Management</a></li>
        <li class="active">Add SMS API</li>
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
              <h3 class="box-title">SMS API List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                         <div class="ibox-tools">
                         <asp:HiddenField ID="hdn_Id1" runat="server" />
                        <asp:LinkButton ID="btnAdd" class="btn btn-round btn-success tooltips" OnClick="btnAdd_Click" Style="margin: 6px 10px 0px 0px;" runat="server"><i class="fa fa-plus" style="color:white;"></i></asp:LinkButton>
                    </div>
                       <div class="form-group table-responsive">
                           <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField Visible="true" ItemStyle-Width="1%">
                                        <HeaderTemplate>ID</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <%--   <asp:TemplateField ItemStyle-Width="5%">
                                        <HeaderTemplate>Member ID</HeaderTemplate>
                                        <ItemTemplate><%#Checknull(Eval("MobileNo").ToString())%></ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <HeaderTemplate>Name</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Name") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="15%">
                                        <HeaderTemplate>Url</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Url") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%" ItemStyle-BackColor="Gray">
                                        <HeaderTemplate>Activity</HeaderTemplate>
                                        <ItemTemplate ><%#Eval("status1") %></ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%">
                                        <HeaderTemplate>Default API</HeaderTemplate>
                                        <ItemTemplate><%#Eval("DefaultType1") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <HeaderTemplate>Action</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkUpdate" class="btn btn-round btn-warning tooltips fa fa-pencil bottom_mrg" title="Edit API" CommandName="EditUser" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="LnkEnable" class="btn btn-round btn-danger tooltips bottom_mrg" CommandName="EnableUser" runat="server"></asp:LinkButton>
                                            <div class="modal fade" id='staticconfirm<%#Eval("Id") %>' tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header modal-success">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                            <h4 class="modal-title" id="H1">Question Message</h4>
                                                        </div>
                                                        <div class="modal-body" style="text-align: center;">
                                                            <img src="~/lib/img/loading/question-mark.png" width="90" id="Img1" runat="server" />
                                                            <p runat="server" id="P1" style="font-size: medium; font-weight: bold">
                                                                Active API Already Exists for this User, You want to Replace this as Active API ???????....!"
                                                            </p>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="LinkButton1" class="btn btn-primary" CommandName="SubOk" runat="server">Yes</asp:LinkButton>
                                                            <button type="button" class="btn btn-dark" data-dismiss="modal">No</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                </Columns>
                            </asp:GridView>
                           </div>
                      </div>
                  </div>
                 </div>
         </div>
                    </div>



        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
    <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always">
        <ContentTemplate>
            <div id="myModalAdd" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">SMS API</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <asp:Label ID="lblapiId" runat="server" Text="0" Visible="false"></asp:Label>
                                <div class="row" id="DefaultAPI" runat="server">
                                    <label class="col-lg-2 control-label">Default API</label>
                                    <div class="col-lg-6">
                                        <asp:CheckBox ID="chk_Default" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-lg-2 control-label">Name</label>
                                    <div class="col-lg-6">
                                        <asp:Label ID="LblUserFlag" runat="server" Visible="false"></asp:Label>
                                        <asp:TextBox ID="TxtName" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator10" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtName" ErrorMessage="Fill Name"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-lg-2 control-label">URL</label>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="TxtHomePageUrl" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox><a href="#static1" style="height: 40px; color: brown; font-size: small; font-weight: 700" id="A1" data-toggle="modal">Help</a>
                                        <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator1" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtHomePageUrl" ErrorMessage="Fill Url !"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="ButSubmit" runat="server" OnClick="ButSubmit_Click1" ValidationGroup="WhiteLabel" Text="Submit" class="btn btn-primary btn-round" />
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

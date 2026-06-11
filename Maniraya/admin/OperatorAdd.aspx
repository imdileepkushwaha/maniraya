<%@ Page Title="Add Operator" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="OperatorAdd.aspx.cs" Inherits="admin_OperatorAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
      <section class="content-header">
      <h1>
      Operator List   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Operator Management</a></li>
        <li class="active">Operator List</li>
      </ol>
    </section>   
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server">
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
              <h3 class="box-title">Operator List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
<div class="row">
                    <div class="ibox-tools">                        
                           <a   href="#" ></a>
                        <asp:LinkButton ID="btnAdd" class="btn btn-round btn-success tooltips" OnClick="btnAdd_Click" style="margin:6px 10px 0px 0px;" runat="server"><i class="fa fa-plus" style="color:white;"></i></asp:LinkButton>
                    </div>
                 
                <div class="form-group table-responsive">
                  <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>ID</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Operator</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Operator") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>OP&nbsp;ID</HeaderTemplate>
                                        <ItemTemplate><%# Eval("OPID") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Type</HeaderTemplate>
                                        <ItemTemplate><%#Eval("TypeName") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Length</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Length") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Minimum</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Minimum") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Maximum</HeaderTemplate>
                                        <ItemTemplate><%#Eval("Maximum") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Display&nbsp;Name</HeaderTemplate>
                                        <ItemTemplate><%#Eval("DisplayName") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>Starts&nbsp;With</HeaderTemplate>
                                        <ItemTemplate><%#Eval("StartsWith") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>Disabled&nbsp;Reasion</HeaderTemplate>
                                        <ItemTemplate><%#Eval("DisabledReasion") %></ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField >
                                        <HeaderTemplate>Status</HeaderTemplate>
                                           <ItemTemplate><%# Eval("showstatus").ToString() == "1" ? "Active" : "Deactive" %></ItemTemplate>
                                      
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>Update</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkUpdate" class="btn btn-round btn-warning tooltips" CommandName="EditUser" runat="server"><i class="fa fa-edit"></i></asp:LinkButton>
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
        <asp:UpdatePanel runat="server" ID="uplMaster" UpdateMode="Always" >
        <ContentTemplate>
            <div id="myModalAdd" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Add New Operator</h4>
                        </div>
                        <div class="modal-body">
                           <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Operator
                                            </label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="TxtOperator" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtOperator" ErrorMessage="Fill Operato"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                OP Id
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAddOPID" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddOPID" ErrorMessage="Fill OP Id !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Type
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DdlAddType" ValidationGroup="AddOperator" class="form-control" runat="server">
                                                    <asp:ListItem Value="0">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="1">Type1</asp:ListItem>
                                                </asp:DropDownList>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" InitialValue="0" runat="server" ValidationGroup="AddOperator" ControlToValidate="DdlAddType" ErrorMessage="Select Type !"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Length
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAddLength" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddLength" ErrorMessage="Fill Length !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Minimum
                                            </label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="TxtAddMinimum" runat="server" onkeypress="return isNumberKey(event)" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddMinimum" ErrorMessage="Fill Minimum value !"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Maximum
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAddMaximum" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddMaximum" ErrorMessage="Fill Maximum !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Display Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAddDisplayName" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>

                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddDisplayName" ErrorMessage="Fill Display Name !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Display Note
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAddDisplayNote" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAddDisplayNote" ErrorMessage="Fill Display Note !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Account Display
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtAccountDisplay" TextMode="MultiLine" Rows="3" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>

                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator15" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtAccountDisplay" ErrorMessage="Fill Account Display !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Location Display
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TxtLocationDisplay" TextMode="MultiLine" Rows="3" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator16" runat="server" ValidationGroup="AddOperator" ControlToValidate="TxtLocationDisplay" ErrorMessage="Fill Location Display !"></asp:RequiredFieldValidator></span>
                                            </div>
                                        </div>

                                    </div>
 
                                    <div class="col-lg-6" style="display:none;">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                               Number Starts with
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txt_Startswith" TextMode="MultiLine" Rows="3" runat="server" ToolTip="[,] Saperated starting number" class="form-control"></asp:TextBox>
                                             
                                            </div>
                                        </div>
                                        </div>
                                    <div class="col-lg-6" style="display:none;">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Reason-Disabled
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txt_ReasonDisabled" TextMode="MultiLine" Rows="3" runat="server" ValidationGroup="AddOperator" class="form-control"></asp:TextBox>
                                                <span>
                                                 
                                            </div>
                                        </div>

                                    </div>
                                     <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" for="form-field-1">
                                                Status
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DDLstatus" runat="server" class="form-control">
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                      <asp:ListItem Value="0">Deactive</asp:ListItem>
                                                </asp:DropDownList>
                                                <span>
                                                 
                                            </div>
                                        </div>

                                    </div>
 
                                  
                                </div>                               
                            </div>
                        </div>
                        <div class="modal-footer">
                           <asp:Button ID="ButSubmit" runat="server" OnClick="ButSubmit_Click" Width="150px" ValidationGroup="AddOperator" Text="Submit" class="btn btn-primary" />
                                   
                                     <asp:Label ID="LblEditId" Text="0" Visible="false" runat="server"></asp:Label>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
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


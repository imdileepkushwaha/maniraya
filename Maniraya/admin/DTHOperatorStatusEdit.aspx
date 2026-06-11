<%@ Page Title="Edit Operator API Code" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="DTHOperatorStatusEdit.aspx.cs" Inherits="admin_DTHOperatorStatusEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
      <section class="content-header">
      <h1>
      Edit Operator Status
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Recharge</a></li>
        <li class="active">Edit Operator Status</li>
      </ol>
    </section>      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <%-- <asp:UpdateProgress id="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="row">
     

     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Edit Operator Status</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlstatus" runat="server" class="form-control">
                                        <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="DeActive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbl_msg" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btn_Save" runat="server" Text="Update" CssClass="btn btn-success btn-round" OnClientClick="save()" OnClick="btn_Save_Click" />
                                </div>
                            </div>
                        </div>
                    <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="5%">
                                       <ItemTemplate>
                                           <asp:CheckBox ID="chk" runat="server" CssClass='<%# Eval("id") %>' />
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.no">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdn_OpId" runat="server" Value='<%# Bind("Id") %>' />
                                        </ItemTemplate>
                                        <ControlStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Operator" HeaderText="Operator" />
                                    <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" />
                            </asp:GridView>
                                </div>
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
    <script type="text/javascript">
        function openModal() {
            $('#static').modal('show');
        }

        function ToUpper(ctrl) {

            //var t = ctrl.value;

            //ctrl.value = t.toUpperCase();

        }

        function ToLower(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toLowerCase();

        }
        //function RestrictSpace() {
        //    if (event.keyCode == 32) {
        //        event.returnValue = false;
        //        return false;
        //    }
        //}
        function alphanumeric_only(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            else if (event) keycode = event.keyCode;
            else if (e) keycode = e.which;

            else return true;
            if ((keycode >= 47 && keycode <= 57) || (keycode >= 65 && keycode <= 90) || (keycode >= 95 && keycode <= 122)) {

                return true;
            }

            else {
                //alert("Please do not use special characters")
                return false;
            }

            return true;
        }
        function searchName(strName, tblId) {
            try {
                var strData = strName.toLowerCase().split(" ");
                var tblData = document.getElementById(tblId);
                var rowData;
                for (var i = 1; i < tblData.rows.length - 1; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            } catch (err) {
                //console.log(err);
            }
        }

    </script>
</asp:Content>


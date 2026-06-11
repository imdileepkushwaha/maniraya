<%@ Page Title="Edit Operator API Code" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="OperatorAPICodeEdit.aspx.cs" Inherits="admin_OperatorAPICodeEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
      <section class="content-header">
      <h1>
      Edit Operator API Code   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Operator Management</a></li>
        <li class="active">Edit Operator API Code</li>
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
              <h3 class="box-title">Edit Operator API Code</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <input id="TxtSearch" placeholder="Search" type="text" onkeyup="searchName(this.value,'<%=GridView1.ClientID %>')" class="form-control" list="ContentPlaceHolder1_Searchdata" />
                                    <datalist id="Searchdata" runat="server">
                                    </datalist>
                                    <asp:Label ID="lbl_msg" runat="server" Text="" ForeColor="Maroon"></asp:Label>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="btn btn-success btn-round" OnClientClick="save()" OnClick="btn_Save_Click" />
                                </div>
                            </div>
                        </div>
                    <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="5%" HeaderText="S.no">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdn_OpId" runat="server" Value='<%# Bind("Id") %>' />
                                        </ItemTemplate>
                                        <ControlStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Operator" HeaderText="Operator" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:DataList ID="DataList1" BorderColor="White" runat="server" RepeatColumns="20" RepeatDirection="Horizontal" Style="border-bottom: none; border-collapse: separate" ForeColor="#333333" BorderStyle="None">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbl_API" runat="server" Text='<%# Bind("Name") %>' Width="90px" Style="border: none"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DataList ID="DataList2" runat="server" RepeatColumns="20" BorderStyle="None" RepeatDirection="Horizontal" CellPadding="4" ForeColor="#333333" OnItemDataBound="DataList2_ItemDataBound">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:HiddenField ID="hdn_ApiId" runat="server" Value='<%# Bind("ApiId") %>' />
                                                        <asp:TextBox ID="txt_code" runat="server" Text="0" Width="80px" Style="margin-right: 5px;" CssClass="form-control" onkeypress="return alphanumeric_only(this);" onkeyup="ToUpper(this)"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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


<%@ Page Title="API Comission" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="APIComission.aspx.cs" Inherits="admin_APIComission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
    API Comission
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">API Management</a></li>
        <li class="active">API Comission</li>
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
              <h3 class="box-title">Recharge API List</h3>
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
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" Width="100%" AutoGenerateColumns="False">
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
                                                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="20" RepeatDirection="Horizontal">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:Label ID="lbl_API" runat="server" Text='<%# Bind("Name") %>' Width="90px"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DataList ID="DataList2" runat="server" RepeatColumns="20" Style="border: none; border-collapse: separate" BorderStyle="None" RepeatDirection="Horizontal" CellPadding="4" ForeColor="#333333" OnItemDataBound="DataList2_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div>
                                                               <%-- onkeypress="return onlyDecimalNumbers(this, event, true, false)"--%>
                                                                <asp:HiddenField ID="hdn_ApiId" runat="server" Value='<%# Bind("ApiId") %>' />
                                                                <asp:TextBox ID="txt_comm" runat="server" Text="0" ValidationGroup="abc" onkeypress="return onlyDecimalNumbers(this, event, true, true)"  Width="87px" style="margin-right:3px;" CssClass="form-control" ></asp:TextBox>
                                                                <%--<asp:RegularExpressionValidator ControlToValidate="txt_comm" ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Enter Number only" ValidationExpression="^[+-]?[0-9]{1,9}(?:\.[0-9]{1,2})?$" ValidationGroup="abc"></asp:RegularExpressionValidator>--%>
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



         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
     <script type="text/javascript">
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }

    </script>
    <script type="text/javascript">

        var numbersOnly = /^\d+$/;
        var decimalOnly = /^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/;
        var uppercaseOnly = /^[A-Z]+$/;
        var lowercaseOnly = /^[a-z]+$/;
        var stringOnly = /^[A-Za-z0-9]+$/;

        function testInputData(myfield, restrictionType) {

            var myData = document.getElementById(myfield).value;
            if (myData !== '') {
                if (restrictionType.test(myData)) {
                    alert('It is GOOD!');
                } else {
                    alert('Your data input is invalid!');
                }
            } else {
                alert('Please enter data!');
            }
            return;

        }
        function onlyDecimalNumbers(obj, e, allowDecimal, allowNegative) {
            var key;
            var isCtrl = false;
            var keychar;
            var reg;

            if (window.event) {
                key = e.keyCode;
                isCtrl = window.event.ctrlKey
            }
            else if (e.which) {
                key = e.which;
                isCtrl = e.ctrlKey;
            }
            if (isNaN(key)) return true;
            keychar = String.fromCharCode(key);
            // check for backspace or delete, or if Ctrl was pressed
            if (key == 8 || isCtrl) {
                return true;
            }
            reg = /\d/;
            var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
            var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;
            return isFirstN || isFirstD || reg.test(keychar);
            // isFirstN || isFirstD ||
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


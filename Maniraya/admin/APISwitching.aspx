<%@ Page Title="API Switching" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="APISwitching.aspx.cs" Inherits="admin_APISwitching" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
      API Switching  
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">API Management</a></li>
        <li class="active">API Switching </li>
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
              <h3 class="box-title">Recharge API</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                 <div class="row">
                       <asp:HiddenField ID="hdn_Id1" runat="server" />
                               <div class="col-md-4">
                                <div class="form-group">
                                    <input id="TxtSearch" type="text" placeholder="Search" onkeyup="searchName(this.value,'<%=GridView1.ClientID %>')" class="form-control" list="ContentPlaceHolder1_Searchdata" />
                                    <datalist id="Searchdata" runat="server">
                                    </datalist>
                                    <asp:Label ID="lbl_msg" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3">
                                          <asp:CheckBox ID="chk_SMS" runat="server" Checked="true" Text="SMS" />
                                    </div>

                                    <div class="col-md-9">
                                        <asp:CheckBox ID="chk_Mail" runat="server" Checked="true" Text="E-MAIL" />
                                    </div>
                                  
                                     
                                </div>

                                
                            </div>

                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="btn btn-success btn-round" OnClick="btn_Save_Click" />
                                </div>
                            </div>
                            <div class="col-md-3">
                        
                            </div>

                        </div>

                  <div class="row">
                                        <div class="col-md-12">
                                <div class="form-group text-center">
                                   <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">A</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton2" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">B</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton3" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">C</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton4" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">D</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton5"  runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">E</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton6"  runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">F</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton7" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">G</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton8" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">H</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">I</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton10" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">J</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton11" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">K</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton12" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">L</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">M</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton14" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">N</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton15" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">O</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton16" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">P</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton17" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">Q</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton18" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">R</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton19" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">S</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton20" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">T</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton21" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">U</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton22" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">V</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton23" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">W</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton24" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">X</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton25"  runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">Y</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton26" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">Z</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButton27" runat="server" OnClick="Filter_Click" class="btn btn-sm btn-primary btn-round">All</asp:LinkButton>
                                </div>

                                

                                
                            </div>
                              </div>

                   <div class="row">
     

     <div class="col-md-12 table-responsive">
         <asp:GridView ID="GridView1" runat="server"  CssClass="table table-bordered table-hover dataTable" Width="100%"
                                            AutoGenerateColumns="False" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdn_OpId" runat="server" Value='<%# Eval("Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Operator">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_OPName" runat="server" Text='<%# Eval("Operator") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="API With Priority">
                                                    <ItemTemplate>
                                                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="6" Style="border: none; border-collapse: separate" RepeatDirection="Horizontal" ForeColor="#333333" OnItemDataBound="DataList1_ItemDataBound" Width="100%">
                                                            <ItemTemplate>
                                                                <div class="tbl">
                                                                    <div class="col-md-12" style="margin-top: 0.6em;">
                                                                        <asp:HiddenField ID="hdn_APIId" runat="server" Value='<%# Bind("Id") %>' />
                                                                        <asp:Label ID="lbl_APIName" Style="font-size: 14px; padding-left: 15px; font-weight: 400;" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="tbl">
                                                                    <div class="col-md-12">
                                                                        <div class="col-sm-1">
                                                                            <asp:CheckBox ID="chk" runat="server" class="icheckbox_flat" />

                                                                        </div>
                                                                        <div class="col-sm-5" style="padding-left: 4px;">
                                                                            <asp:TextBox runat="server" Text="1" ID="txt_Priority" class="form-control" Height="30px" onkeypress="return isNumber(event)" Width="50px" AutoPostBack="False"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Back API">
                                                    <ItemStyle Width="110px" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_IsDown" runat="server" class="icheckbox_flat" Checked='<%# Eval("IsDown") %>' Text="Down" ToolTip="Is Operator Down" />
                                                        <asp:HiddenField ID="hdn_IsDown" runat="server" Value='<%# Eval("IsDown") %>' />
                                                        <asp:HiddenField ID="hdn_BackUpAPI" runat="server" Value='<%# Eval("BackupAPI") %>' />
                                                        <asp:DropDownList ID="ddl_BackUpAPI" runat="server" ></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle />
                                            <HeaderStyle />
                                            <PagerStyle />
                                            <RowStyle />
                                            <SelectedRowStyle />
                                            <SortedAscendingCellStyle />
                                            <SortedAscendingHeaderStyle />
                                            <SortedDescendingCellStyle />
                                            <SortedDescendingHeaderStyle />
                                        </asp:GridView>

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


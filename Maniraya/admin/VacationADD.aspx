<%@ Page Title="Vacation Add" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="VacationADD.aspx.cs" Inherits="VacationADD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             if (document.getElementById("<%=Txtawradname.ClientID%>").value == "") {
                alert('Enter vacation Name');
                document.getElementById("<%=Txtawradname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtFRomdate.ClientID%>").value == "") {
                alert('Enter from date');
                document.getElementById("<%=TxtFRomdate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtToDate.ClientID%>").value == "") {
                alert('Enter to date');
                document.getElementById("<%=TxtToDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtTargetLaeft.ClientID%>").value == "") {
                alert('Enter target left ');
                document.getElementById("<%=TxtTargetLaeft.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=TxtTargetRight.ClientID%>").value == "") {
                alert('Enter target right');
                document.getElementById("<%=TxtTargetRight.ClientID%>").focus();
                return false;
            }
        }

        function validate2() {
            if (document.getElementById("<%=awardname.ClientID%>").value == "") {
                alert('Enter vacation Name');
                document.getElementById("<%=awardname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtfromdateedit.ClientID%>").value == "") {
                alert('Enter from date');
                document.getElementById("<%=txtfromdateedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txttodateedit.ClientID%>").value == "") {
                alert('Enter to date');
                document.getElementById("<%=txttodateedit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtargerleftdit.ClientID%>").value == "") {
                alert('Enter target left ');
                document.getElementById("<%=txtargerleftdit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txttargetrightedit.ClientID%>").value == "") {
                alert('Enter target right');
                document.getElementById("<%=txttargetrightedit.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Vacation Add      
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Award & Reward</a></li>
        <li class="active">Vacation Add </li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="row">
            <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Vacation Add</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                   <div class="row">
                         <div class="col-md-12">
                             <div class="form-group">
                                 <label>Vacation Name :</label>
                                  <asp:TextBox ID="Txtawradname"  runat="server" CssClass="form-control" />
                             </div>
                         </div>
                        
                     </div>
              <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>From date:</label>
                               <asp:TextBox ID="TxtFRomdate" runat="server" CssClass="form-control form_date" />
                               
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>To Date :</label>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="form-control form_date" />
                             </div>
                         </div>
                     </div>
                    <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>Target Left :</label>
                             <asp:TextBox ID="TxtTargetLaeft" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                  <label>Target Right :</label>
                                   <asp:TextBox ID="TxtTargetRight" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                   
              <!-- /.box-body -->

              <div class="box-footer">
                   <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
               
              </div>
         
          </div>
            </div>
                 <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                     <asp:TemplateField HeaderText="Award ID">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblid" runat="server"  Text='<%#Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vacation Name">
                                        <ItemTemplate>
                                            <asp:Label ID="labawardname" runat="server" Text='<%#Eval("Vacationname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("Todate1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltargetleft" runat="server" Text='<%#Eval("TargetLeft") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltargetright" runat="server" Text='<%#Eval("TargetRight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </div>             
               
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>

                  <div id="myModal" class="modal fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Edit Award Details</h4>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Vacation name :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="lblawardid" runat="server" Visible="false" Text="0"></asp:Label>
                                        <asp:TextBox ID="awardname" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">From Date :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtfromdateedit" runat="server" CssClass="form-control form_date"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">To Date :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txttodateedit" runat="server" CssClass="form-control form_date" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Target Left :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtargerleftdit" runat="server" CssClass="form-control" TextMode="Number" step="0.00" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="exampleInputEmail1">Target Right :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txttargetrightedit" runat="server" CssClass="form-control" TextMode="Number" step="0.00"/>
                                    </div>
                                </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
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
        function showModal() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();
        }
    </script>
     <script type="text/javascript">
         $('.form_date').datepicker({
             format: 'dd/M/yyyy',
         }).on('changeDate', function (ev) {
             $(this).datepicker('hide');
         });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/M/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
</asp:Content>


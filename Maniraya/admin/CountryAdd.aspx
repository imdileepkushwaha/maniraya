<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CountryAdd.aspx.cs" Inherits="admin_CountryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function validate() {
            // alert('sd');
            if (document.getElementById("<%=txtcountryname.ClientID%>").value == "") {

                alert('Enter Country Name');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtcountryname.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtcountrycode.ClientID%>").value == "") {

                alert('Enter Country Code');
                // alert("Enter Rank No"); 
                document.getElementById("<%=txtcountrycode.ClientID%>").focus();
                return false;
            }
        }

           function validate2() {
               if (document.getElementById("<%=txtcountrynameedit.ClientID%>").value == "") {
                   alert('Enter Country Name');
                   document.getElementById("<%=txtcountrynameedit.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=txtcountrycodeedit.ClientID%>").value == "") {
                   alert('Enter Country Code');
                   document.getElementById("<%=txtcountrycodeedit.ClientID%>").focus();
                   return false;
               }
           }

        function openAddCountryModal() {
            document.getElementById("<%=txtcountryname.ClientID%>").value = "";
            document.getElementById("<%=txtcountrycode.ClientID%>").value = "";
            showAdminModal('addCountryModal');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Add Country     
      </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Utility management</a></li>
            <li class="active">Add Country</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                            <div class="box-tools">
                                <button type="button" class="btn btn-primary btn-sm admin-box-header-btn" onclick="openAddCountryModal();">
                                    <i class="fa fa-plus"></i> Add Country
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("Countryid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryname" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountrycode" runat="server" Text='<%#Eval("CountryCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbEdit" CssClass="admin-grid-edit-btn" CommandName="edt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                        <!-- /.box-body -->



                    </div>
                </div>
                <div id="addCountryModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="countryAddModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="countryAddModalTitle">Add Country</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="<%= txtcountryname.ClientID %>">Country Name</label>
                                    <asp:TextBox ID="txtcountryname" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtcountrycode.ClientID %>">Country Code</label>
                                    <asp:TextBox ID="txtcountrycode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="countryEditModalTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="countryEditModalTitle">Edit Country</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblcountryid" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                                <div class="form-group">
                                    <label for="<%= txtcountrynameedit.ClientID %>">Country Name</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrynameedit"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="<%= txtcountrycodeedit.ClientID %>">Country Code</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrycodeedit"></asp:TextBox>
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">




</asp:Content>


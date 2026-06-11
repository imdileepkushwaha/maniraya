<%@ Page Title="" Language="C#" MasterPageFile="franchiseemaster.master" AutoEventWireup="true" CodeFile="EPinReport.aspx.cs" Inherits="admin_EPinReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
   <section class="content-header">
      <h1>
      Generate E-Pin     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">E-Pin management</a></li>
        <li class="active">E-Pin Report</li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Generate User ID</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtgenerateuserid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Used User ID</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtuseduserid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Select Status</label>
                                        <asp:DropDownList ID="ddstatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select E-Pin Status</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Used</asp:ListItem>
                                            <asp:ListItem>Cancelled</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>From date</label>
                                        <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>To date</label>
                                        <asp:TextBox ID="txttodate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        </div>
                                    </div>
                            </div>

                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />



                        </div>

                    </div>
                </div>
         

            <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>
                        </div>

                        <div class="box-body">
                       
                               
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" >
                                 <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                              
                           <asp:TemplateField HeaderText="E-Pin No">
                               <ItemTemplate>
                                     <asp:Label ID="lblepinno" runat="server"  Text='<%#Eval("EPinNo") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Generate User Id ">
                                                                                    <ItemTemplate>
                                     <asp:Label ID="lblGenerateUserId" runat="server"  Text='<%#Eval("GenerateUserId") %>'></asp:Label>
                               </ItemTemplate>


                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Amount ">
                               <ItemTemplate>
                                     <asp:Label ID="lblplanname" runat="server"  Text='<%#Eval("Amount") %>'></asp:Label>

                               </ItemTemplate>


                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Used User Id ">
                               <ItemTemplate>
                                     <asp:Label ID="lblUsedUserId" runat="server"  Text='<%#Eval("UsedUserId") %>'></asp:Label>

                               </ItemTemplate>


                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="E-Pin Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblEPinStatus" runat="server"  Text='<%#Eval("EPinStatus") %>'></asp:Label>

                               </ItemTemplate>


                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Date">
                               <ItemTemplate>
                                     <asp:Label ID="lblMentionDate" runat="server"  Text='<%#Eval("MentionDate","{0:dd/MM/yyyy}") %>'></asp:Label>

                               </ItemTemplate>


                           </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                                    </div>
                              
                             
                             
                            
                            

                        </div>
                        <div class="box-footer">
                        



                        </div>

                    </div>
                </div>

               </div>

          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
     <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
 <script type="text/javascript">
     $('.form_date').datepicker({
         format: 'dd/mm/yyyy',
     }).on('changeDate', function (ev) {
         $(this).datepicker('hide');
     });
     </script>
</asp:Content>


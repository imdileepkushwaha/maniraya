<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="CreateLinkForJoining.aspx.cs" Inherits="CreateLinkForJoining" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Set Placement Affiliate Link </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My Network</a></li>
            <li class="active">Set Placement Affiliate Link</li>
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
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>User Id :</label>
                                        <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" Enabled="false"/>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>User Name :</label>
                                        <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control"  />
                                    </div>
                                </div>
                               
                            </div>
                           
                            <br />
                               <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                   <asp:RadioButton ID="RdBtnLeft" runat="server" Text="Left Stainding Position" GroupName="B" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RdBtnRight" runat="server" Text="Right Stainding Position" GroupName="B" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                           <div class="box-footer">
                        
             

                               <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                      
                    
              </div>

                    </div>

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Link For Joining"></asp:Label>
                                       
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="TxtLink" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                       
                                      <asp:Button ID="Button2" runat="server" Text="Copy" CssClass="btn btn-primary" OnClientClick="CopyToClipboard2()"/>
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
     <script type="text/javascript" language="javascript">
        
          function CopyToClipboard2() {


              /* Get the text field */
              var copyText1 = document.getElementById('<%=TxtLink.ClientID%>');

              /* Select the text field */
              copyText1.select();

              /* Copy the text inside the text field */
              document.execCommand("Copy");

              /* Alert the copied text */
              alert("Copied the text: " + copyText1.value);
          }
    </script>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AdminDownload.aspx.cs" Inherits="admin_AdminDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <link href="../css/radio/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
    <section class="content-header">
        <h1>Admin Download Option</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Tools</a></li>
            <li class="active">Admin Download Option</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Upload Option</h3>
                </div>
                <!-- /.box-header -->
                <!-- form start -->
                <div class="box-body">
                       <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RDBTNAdmin" runat="server" Text="Retailer" GroupName="B"  />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButton ID="RDBtnFranchisee" runat="server" Text="Franchisee" GroupName="B"/>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>TO ID</label>
                                <asp:TextBox ID="txtToID" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtToID_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>TO Name</label>
                                  <asp:TextBox ID="Txtname" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                              <label>Choose File</label>
                                <asp:FileUpload ID="fupContent" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                            
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


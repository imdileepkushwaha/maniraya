<%@ Page Title="New Message" Language="C#" MasterPageFile="usermaster.master" AutoEventWireup="true" CodeFile="NewMessage.aspx.cs" Inherits="Associate_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        hr {
            margin-top: 3px;
            margin-bottom: 3px;
            border: 0;
        }
        .label {
            
            font-size: 100% !important;
            font-weight: 600 !important;
            line-height: 2 !important;
        }
    </style>
    <script src="js/jquery-1.10.2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageheading" runat="Server">
    <section class="content-header">
      <h1>
            Compose Mail
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Customer Care</a></li>
        <li class="active">Compose Mail</li>
      </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageData" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Compose Mail</h3>
                </div>
                <div class="box-content row">
                    <div class="col-lg-12 col-md-12">
                        <hr />
                        <div class="row" style="display:none;">
                            <div class="col-md-offset-1 col-md-1">
                                <label for="exampleInputEmail1">To</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txttoid" CssClass="form-control" runat="server" OnTextChanged="txttoid_TextChanged" AutoPostBack="true" Text="admin" ></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-offset-1 col-md-1">
                                <label for="exampleInputEmail1">Subject</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtmessagetitle" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-offset-1 col-md-1">
                                <label for="exampleInputEmail1">Attachment</label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="fupAttachment" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-offset-1 col-md-1">
                                <label for="exampleInputEmail1">Message</label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtdescription" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-sm-offset-2 col-sm-12">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btncancel" CssClass="btn btn-default" Text="Cancel" runat="server" OnClick="btncancel_Click" />
                            </div>
                        </div>
                        <hr />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>


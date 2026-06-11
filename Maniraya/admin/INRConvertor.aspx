<%@ Page Title="Tree View" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="INRConvertor.aspx.cs" Inherits="admin_DownlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
     <section class="content-header">
      <h1>
      INR TO Dollar 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Dashboard</a></li>
        <li class="active">INR TO Dollar</li>
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
                      
              <div class="box-body">
                    <div class="row">
                    <div class="col-md-4">
                <div class="form-group">
                  <label >INR</label>
                  <asp:TextBox ID="txt_inr" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                        <div class="col-md-4">
                <div class="form-group">
                  <label >DOLLAR</label>
                  <asp:TextBox ID="txt_dollar" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-4">
                <div class="form-group">
                    <br />
                 <asp:Button ID="btnSubmit"  CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnSubmit_Click" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


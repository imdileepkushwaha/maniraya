<%@ Page Title="System Setting" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="SystemSetting.aspx.cs" Inherits="admin_SystemSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
   <section class="content-header">
      <h1>
     System Setting
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Settings</a></li>
        <li class="active">System Setting </li>
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
              <h3 class="box-title">System Setting</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                               <label >Interval Between Recharge (In Minutes)</label>
                                <asp:TextBox ID="txt_InterRech" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                              <label>Interval Between Transfer (In Minutes)</label>
                               <asp:TextBox ID="txt_InterTrans" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>                                    
                             </div>
                        </div>
                      <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                            <label >Min Fund Transfer</label>
                                <asp:TextBox ID="txt_MinFundTransfer" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox> 
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                            <label >Max Fund Transfer</label>
                               <asp:TextBox ID="txt_MaxFundTransfer" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                             <label >BackUp Days</label>
                               <asp:TextBox ID="Txt_BackDate" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>                                     
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                            <label >DMR Verification charge</label>
                                <asp:TextBox ID="txtDMR" onkeypress="return isNumber(event)" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>                                                                         
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-3">
                             </div>
                          <div class="col-md-6">
                           <label >Push Notification</label>
                              <div class="input-group">
                              <asp:TextBox ID="txtDDlPushNotification" CssClass="form-control" runat="server"></asp:TextBox>  
                                       <div class="input-group-addon">
                                                            <asp:Button ID="btn_key" runat="server"  Text="New Key" OnClick="btn_key_Click" />
                                                            </div>                    
                                                                                                         
                             </div>
                               <span>
                                        <asp:CheckBox ID="chkDDlPushNotification"  Text="Without Push Notification" runat="server" /></span>   
                              </div>
                          <div class="col-md-3">
                            
                              </div>
                        </div>
                  </div>
                 <div class="box-footer">
                      <asp:Button ID="ButSubmit" runat="server" OnClick="ButSubmit_Click" ValidationGroup="WhiteLabel" Text="Submit" class="btn btn-primary btn-round" />
                                                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" class="btn btn-danger btn-round" />
                 </div>
                 </div>
         </div>
                 </div>



            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
</asp:Content>


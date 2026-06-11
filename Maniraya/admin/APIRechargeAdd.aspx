<%@ Page Title="Recharge API" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="APIRechargeAdd.aspx.cs" Inherits="admin_APIRechargeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
      Recharge API   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">API Management</a></li>
        <li class="active"> Recharge API </li>
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
              <h3 class="box-title">Recharge API</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="row">
                         <div class="col-md-8">
                              <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                              <label >Name <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                                <asp:Label ID="LblUserFlag" runat="server" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TxtName" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator10" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtName" ErrorMessage="Fill Name"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>

                              <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                            <label >API URL <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                               <asp:TextBox ID="TxtHomePageUrl" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator1" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtHomePageUrl" ErrorMessage="Fill Url !"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>
                               <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                           <label >Operator Type </label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                             <asp:TextBox ID="txtoperatortype" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control" placeholder="topupcode.DTHcode.landlinecode.electricode.postpaidcode.specialcode" ></asp:TextBox>

                                          </div>
                                      </div>
                                  </div>
                                 <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                        <label >Status Check URL</label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                           <asp:TextBox ID="txt_statusUrl" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>

                                          </div>
                                      </div>
                                  </div>
                              <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                        <label>Balance URL</label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                             <asp:TextBox ID="txt_balUrl" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>

                                          </div>
                                      </div>
                                  </div>
                                 <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                       <label >Dispute URL</label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                             <asp:TextBox ID="txt_Default" Rows="3" TextMode="MultiLine" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>

                                          </div>
                                      </div>
                                  </div>
                                <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                      <label >Errors</label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                              <asp:TextBox ID="txt_Error" runat="server" ValidationGroup="WhiteLabel" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                          </div>
                                      </div>
                                  </div>
                                  <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                  <label >Status Name <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                              <asp:TextBox ID="TxtStatusName" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator2" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtStatusName" ErrorMessage="Fill Status Name"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>
                               <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                <label >Status Value <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                             <asp:TextBox ID="TxtStatusValue" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator3" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtStatusValue" ErrorMessage="Fill Status Value"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>
                              <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                 <label >Live ID <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                              <asp:TextBox ID="TxtOpId" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator4" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtOpId" ErrorMessage="Fill OperatorId"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>
                                 <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                                 <label >Vendor Code <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                              <asp:TextBox ID="TxtVenId" runat="server" ValidationGroup="WhiteLabel" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" class="error" ID="RequiredFieldValidator5" runat="server" ValidationGroup="WhiteLabel" ControlToValidate="TxtVenId" ErrorMessage="Fill Vender Id"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>
                               <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                               <label >Response Type <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                            <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">json</asp:ListItem>
                                                <asp:ListItem Value="2">xml</asp:ListItem>
                                                <asp:ListItem Value="3">string</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" class="error" ControlToValidate="ddlType" ErrorMessage="Select Response Type" InitialValue="0" ValidationGroup="WhiteLabel"></asp:RequiredFieldValidator>

                                          </div>
                                      </div>
                                  </div>

                                <div class="row">
                                  <div class="col-md-3">
                                      <div class="form-group">

                               <label >Activity <span style="color: red;">*</span></label>

                                          </div>

                                      </div>
                                     <div class="col-md-9">
                                          <div class="form-group">

                                            <asp:DropDownList ID="DDLstatus" runat="server" class="form-control">
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                      <asp:ListItem Value="0">Deactive</asp:ListItem>
                                                </asp:DropDownList>
                                        

                                          </div>
                                      </div>
                                  </div>
                           

                             </div>
                             <div class="col-md-4" style="text-align: left; font-size: medium; font-weight: bold">
                                <span style="color:maroon;font-weight:400;text-decoration:underline;">Replacement Values</span><br />
                                 <span style="color:blue;font-weight:100;font-size:12px">Copy And Paste it where you want</span> <br />
                                <br />
                                {VenderID} for VenderID.
                                <br />
                                <br />
                                {TransactionID} for TransactionID.
                                <br />
                                <br />                               
                                {uid} for TransID
                                <br />
                                <br />
                                {mobile} for MobileNo.
                                <br />
                                <br />
                                {operator} for Operator.
                                <br />
                                <br />
                                {amount} for Amount.
                                <br />
                                <br />
                                {operatortype} for OperatorType like topup or special.
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


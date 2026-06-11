<%@ Page Title="" Language="C#" MasterPageFile="~/user/usermaster.master" AutoEventWireup="true" CodeFile="requestToChangeNumber.aspx.cs" Inherits="user_requestToChangeNumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">

    <section class="content-header">
      <h1>
        Request Admin to Change Number
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">My Profile</a></li>
        <li class="active">Request Admin to Change Number </li>
      
      </ol>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">
                  Request Admin to Change Number</h3>                
                  <span style="color:red">
                    (For changing the mobile number, the charge of 
                    <asp:Label ID="lblMobileChangeCharge" runat="server" Font-Bold="true"></asp:Label> will be deducted from your recharge wallet).
                </span>
            </div>
                   <div class="box-body">
                          <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" />
                             </div>
                         </div>
                     </div>
                         <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Previous Mobile No. :</label>
                                 <asp:TextBox ID="txtPrevMobileNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>  
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                <label>New Mobile No. :</label>
                                 <asp:TextBox ID="txtNewMobileNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>  
                             </div>
                         </div>
                     </div>

                       <div class="row">
                           <div class="col-md-12 text-center">
                               <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-info" />
                               <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" />
                           </div>
                       </div>
                        
                       </div>


                  
              </div>
              </div>
                  </div>




            
        </ContentTemplate>
        <Triggers>
      
        <%--<asp:PostBackTrigger ControlID = "btnSubmit" />--%>
    </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


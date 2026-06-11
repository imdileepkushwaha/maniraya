<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="mobileChangeRequests.aspx.cs" Inherits="admin_mobileChangeRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .Approved {
            background-color: #127113;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Pending {
            background-color: #1596ab;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Rejected {
            background-color: #c91d1d;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
      Mobile No. Change Requests
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Admin Request</a></li>
        <li class="active">Mobile No. Change Requests</li>
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
              <h3 class="box-title">Mobile No. Change Requests</h3>
            </div>
                 </div>

         
             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Mobile No. Change Requests</h3>
            </div>
         
              <div class="box-body table-responsive">
                   <div class="row">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover datatable" 
                      Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <%--<asp:HiddenField ID="hdnRequest_Id" runat="server" Value='<%# Eval("request_Id") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("userType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser_Id" runat="server" Text='<%# Eval("user_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Previous No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblpreviousNo" runat="server" Text='<%# Eval("previousNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="New Mobile No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblnewMobileNo" runat="server" Text='<%# Eval("newMobileNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Request Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("requestStatus") %>' CssClass='<%# Eval("requestStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Charge Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblchargeAmount" runat="server" Text='<%# Eval("chargeAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mentioned By">
                                <ItemTemplate>
                                    <asp:Label ID="lblcreatedBy" runat="server" Text='<%# Eval("createdBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mentioned Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblcreatedDate" runat="server" Text='<%# Eval("createdDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApprove" runat="server" Visible='<%# Eval("requestStatus").ToString() == "Pending" ? true : false %>' Text="Approve |" 
                                        OnClientClick="return confirm('Sure to Approve?');" CommandName="approve" CommandArgument='<%# Eval("request_Id") %>'></asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkReject" runat="server" Visible='<%# Eval("requestStatus").ToString() == "Pending" ? true : false %>' 
                                        OnClientClick="return confirm('Sure to Reject?');" Text="Reject" CommandName="reject" CommandArgument='<%# Eval("request_Id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
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


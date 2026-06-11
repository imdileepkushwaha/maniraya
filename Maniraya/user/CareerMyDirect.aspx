<%@ Page Title="" Language="C#" MasterPageFile="masterpage.master" AutoEventWireup="true" CodeFile="CareerMyDirect.aspx.cs" Inherits="CareerMyDirect" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdateProgress ID="updateProgress" runat="server">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
               <div class="container-fluid pt-4">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>MY DIRECT</h5>
                            </div>
                            <div class="card-body">
                                  <div class="row">
                               
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label>User ID</label>
                                        <asp:TextBox ID="TxtUserId" runat="server" CssClass="form-control"></asp:TextBox>
                                     
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Start Up :</label>
                                  <asp:DropDownList ID="DDlstpoolNo" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0">No Topup</asp:ListItem>
                                            <asp:ListItem Value="9">Topup</asp:ListItem>
                                          
                                            
                                        </asp:DropDownList>
                                    
                                    </div>
                                </div>
                                         <div class="col-md-2">
                                    <div class="form-group">
                                       <label>Position</label>
                                      <asp:DropDownList ID="DDlstPosition" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0">Both</asp:ListItem>
                                            <asp:ListItem Value="1">Left</asp:ListItem>
                                             <asp:ListItem Value="2">Right</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                 
                                     
                                    </div>
                                </div>
                                       <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From date</label>
                                 <asp:TextBox ID="txtfromdate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                                           <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtfromdate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To date</label>
                                        <asp:TextBox ID="txttodate"  CssClass="form-control form_date" runat="server"></asp:TextBox>
                                          <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txttodate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                    </div>
                                </div>
                               
                            </div>
                                  <div class="row">
                                       <div class="col-md-6">
                                    <div class="form-group">
                                       <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                     
                                    </div>
                                </div>
                                      </div>
                                   <div class="table-responsive latest-order-table">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                       <asp:TemplateField HeaderText="Upline ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUplineId" runat="server" Text='<%#Eval("UplineId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Upline Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUplineName" runat="server" Text='<%#Eval("UplineName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                   
                                     <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                            
                                    <asp:TemplateField HeaderText="Reg. Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegdate" runat="server" Text='<%#Eval("Regdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Active Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivadate" runat="server" Text='<%#Eval("Activedate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                                         
                                     
                                         <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("Statenew") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%#Eval("Citynew") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Leadership Rank">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkLeadershipRank" CommandName="Leadershipedt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text='<%#Eval("LeadershipRank") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkRank" CommandName="rankedt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text='<%#Eval("Rank") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                       <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("userStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                   
                                </Columns>
                            </asp:GridView>
                                       </div>
                                </div>
                            </div>
                        </div>
                    </div>
              <div class="row" style="display:none;">
                  <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                          
                            

                             

                        </div>
                        <div class="box-footer">
                               
                           
                           


                        </div>

                    </div>
                </div>
                  <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>                           
                          
                        </div>
                    
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                       
                                            </div>
                                    </div>
                                </div>
                               
                               
                            </div>
                            

                             

                        </div>
                        <div class="box-footer">
                               
                           


                        </div>

                    </div>
                </div>
                  </div>



            <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Rank Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                          <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  >
                                <Columns>
                             
                           <asp:TemplateField HeaderText="Rank">
                               <ItemTemplate>
                                     <asp:Label ID="lblRank" runat="server"  Text='<%#Eval("Rankname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblStatus" runat="server"  Text='<%#Eval("Status") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Date">
                               <ItemTemplate>
                                     <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("Entrydate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                 
                                   
                            </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                     
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>

                        <div id="myModalleader" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Leadership Rank Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                          <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False"  >
                                <Columns>
                             
                           <asp:TemplateField HeaderText="Rank">
                               <ItemTemplate>
                                     <asp:Label ID="lblRank" runat="server"  Text='<%#Eval("Rankname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                                     <asp:Label ID="lblStatus" runat="server"  Text='<%#Eval("Status") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Date">
                               <ItemTemplate>
                                     <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("Entrydate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                 
                                   
                            </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                     
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>
     </div>
      
    
        </ContentTemplate>
          <Triggers>
      
        
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
      <script type="text/javascript">


         function showModal() {
              $('#myModal').modal({ backdrop: 'static', keyboard: false })
          }
          function Closepopup() {
              $('#myModal').modal('hide');
              $('body').removeClass('modal-open');
              $('body').css('padding-right', '0');
              $('.modal-backdrop').remove();

          }
          function showModal1() {
              $('#myModalleader').modal({ backdrop: 'static', keyboard: false })
          }
          function Closepopup1() {
              $('#myModalleader').modal('hide');
              $('body').removeClass('modal-open');
              $('body').css('padding-right', '0');
              $('.modal-backdrop').remove();

          }
        </script>
</asp:Content>


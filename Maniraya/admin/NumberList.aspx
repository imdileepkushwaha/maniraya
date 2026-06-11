<%@ Page Title="Operator Series" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="NumberList.aspx.cs" Inherits="NumberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        function validate() {

            if (document.getElementById("<%=DdlOpertor.ClientID%>").value == "0") {

                alert('Select Opertor');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=DdlOpertor.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=ddlstcircle.ClientID%>").value == "0") {

                   alert('Select Circle');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddlstcircle.ClientID%>").focus();
                   return false;
               }
            if (document.getElementById("<%=TxtNumber.ClientID%>").value == "") {

                alert('enter number');
                // alert("Enter Rank No"); 
                document.getElementById("<%=TxtNumber.ClientID%>").focus();
                   return false;
               }
           }
           function validate2() {
            
            if (document.getElementById("<%=ddlstcircledit.ClientID%>").value == "0") {

                   alert('Select Circle');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=ddlstcircledit.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtNumberListEdit.ClientID%>").value == "") {

                   alert('enter number');
                   // alert("Enter Rank No"); 
                   document.getElementById("<%=TxtNumberListEdit.ClientID%>").focus();
                return false;
            }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    
   <section class="content-header">
      <h1>
      Operator Series
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Operator Series</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:15%;left:25%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Operator Series</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Select Operator</label>
                    <asp:DropDownList ID="DdlOpertor" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select Operator</asp:ListItem>
                                </asp:DropDownList>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
                  <label >Circle</label>
                 <asp:DropDownList ID="ddlstcircle" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select Circle</asp:ListItem>
                                </asp:DropDownList>
                </div>             
               </div>
                     <div class="col-md-6">
                <div class="form-group">
                  <label >Number</label>
                    <asp:TextBox ID="TxtNumber" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                </div>             
               </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
               
                     <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
              </div>
         
          </div>
            </div>
      <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  
                <div class="form-group">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="Operator">
                               <ItemTemplate>
                                     <asp:Label ID="lblOperator" runat="server"  Text='<%#Eval("Operator") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number">
                               <ItemTemplate>
                                     <asp:Label ID="lblNumber" runat="server"  Text='<%#Eval("Number") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Circle">
                               <ItemTemplate>
                                     <asp:Label ID="lblCircle" runat="server"  Text='<%#Eval("Circle") %>'></asp:Label>
                                     <asp:Label ID="Label1" runat="server"  Text='<%#Eval("IReffCircle") %>' Visible="false"></asp:Label>
                                   <asp:Label ID="Label2" runat="server"  Text='<%#Eval("IReffOp") %>' Visible="false"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbEdit" CommandName="edt"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
              
                </div>             
             
                   
                       
            
              </div>
              <!-- /.box-body -->

           
         
          </div>
            </div>
     <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Series Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                        Number
                         <asp:Label ID="lblstateid" Visible="false" runat="server" Text=""></asp:Label>
                         
                           <asp:TextBox ID="TxtNumberListEdit" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div class="form-group">
                        OPerator
                      
                         
                          <asp:DropDownList ID="ddloperatoredit" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select Operator</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                          <div class="form-group">
                        Circle
                      
                         
                          <asp:DropDownList ID="ddlstcircledit" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select Circle</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  OnClientClick="return validate2();" CssClass="btn btn-primary" OnClick="btnUpdate_Click"  />
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>
     </div>
      
      

        
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
   
       
   
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
        </script>
</asp:Content>


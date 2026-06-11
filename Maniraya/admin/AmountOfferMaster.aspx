<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AmountOfferMaster.aspx.cs" Inherits="AmountOfferMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

   <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function validate() {

            
               if (document.getElementById("<%=DDLstOfferProduct.ClientID%>").value == "0") {

                 alert('Select Offer Product');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=DDLstOfferProduct.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=TxtAmount.ClientID%>").value == "") {

                 alert('Enter Quantity');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TxtAmount.ClientID%>").focus();
                   return false;
               }
             if (document.getElementById("<%=TxtOfferQuantity.ClientID%>").value == "") {

                 alert('Enter Offer Quantity');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TxtOfferQuantity.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=TxtOfferAmount.ClientID%>").value == "") {

                 alert('Enter Offer Amount');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=TxtOfferAmount.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txtFromDate.ClientID%>").value == "") {

                 alert('Enter From date');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=txtFromDate.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtToDate.ClientID%>").value == "") {

                 alert('Enter To date');
                 // alert("Enter Rank No"); 
                 document.getElementById("<%=txtToDate.ClientID%>").focus();
                 return false;
             }
           }
          
    </script>
           <style type="text/css">
        .Active, .Active:hover {
            background-color: #006b0d;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
        .Deactive, .Deactive:hover {
            background-color: #bf0a0a;
            color: #fff;
            padding: 4px;
            border-radius: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
      <h1>
       Offer Amount Master   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Utility management</a></li>
        <li class="active">Offer Amount Master   </li>
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
              <h3 class="box-title">Offer Quantity</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

                 <div class="box-body">
                     <div class="row">
                         
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Quatity</label>
                                    <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Offer Product</label>
                                 <asp:DropDownList ID="DDLstOfferProduct" CssClass="form-control"  runat="server" ></asp:DropDownList>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Offer Quatity</label>
                                    <asp:TextBox ID="TxtOfferQuantity" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>Amount</label>
                                  <asp:TextBox ID="TxtOfferAmount" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 
                             </div>
                         </div>
                     </div>
                      <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>From date</label>
                                  <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                               <label>To date</label>
                               <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                             </div>
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
<div class="row">
                  
                 
                <div class="form-group table-responsive">
                  <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"  >
                                <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                          
                                         <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblQuantity" runat="server"  Text='<%#Eval("AMount") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offer Product Name">
                               <ItemTemplate>
                                     <asp:Label ID="lblstatename" runat="server"  Text='<%#Eval("OfferProductname") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offer Quantity">
                               <ItemTemplate>
                                     <asp:Label ID="lblOfferQuantity" runat="server"  Text='<%#Eval("OfferQuantity") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Offer Amount">
                               <ItemTemplate>
                                     <asp:Label ID="lblOfferAmount" runat="server"  Text='<%#Eval("OfferAmount") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Fromdate">
                               <ItemTemplate>
                                     <asp:Label ID="lblFromdate" runat="server"  Text='<%#Eval("Fromdate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Todate">
                               <ItemTemplate>
                                     <asp:Label ID="lblTodate" runat="server"  Text='<%#Eval("Todate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                                  
                                           <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkActiveStatus" runat="server" CommandName="changeStatus" CommandArgument='<%#Eval("ID") %>' 
                                                Text='<%#Eval("Status").ToString() == "1" ? "Active" : "Deactive" %>' CssClass='<%#Eval("Status").ToString() == "1" ? "Active" : "Deactive" %>' ToolTip='<%# "Click to " + (Eval("Status").ToString() == "1" ? "Deactive" : "Active") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           
                                </Columns>
                            </asp:GridView>
                </div>             
               </div>
   
                
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
             
                    
              </div>
         
          </div>
            </div>

     
     </div>
      
    
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
     
</asp:Content>


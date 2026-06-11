<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="PaidJoiningClosingMonthly.aspx.cs" Inherits="JoiningClosingReportMonthly"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" language="javascript">
         functionCheckall(Checkbox)
         {
             var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i
                    < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        } </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
  <section class="content-header">
      <h1>
      Paid Monthly Payout     
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Closing</a></li>
        <li class="active">Paid Monthly Payout</li>
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
                            <h3 class="box-title">Search Crteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Select Date</label>
                                        <asp:DropDownList ID="DDlstFromdate" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       <label>User ID</label>
                                        <asp:TextBox ID="TxtUserId" runat="server" CssClass="form-control"></asp:TextBox>
                                     
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                      
                                    
                                    </div>
                                </div>
                            </div>
                            

                             

                        </div>
                        <div class="box-footer">
                               
                           
                             <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="BtnMessage" CssClass="btn btn-primary" runat="server" Text="Message" OnClick="BtnMessage_Click" />
                             <div class="pull-right">
                             <asp:Button ID="btnpay"  CssClass="btn btn-primary" runat="server" Text="Paid to Bank" OnClick="btnpay_Click" Visible="false" />
                            </div>
                        </div>

                    </div>
                </div>
                  <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Details</h3>                           
                              <div style="float: right">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../user/img/excel123.png" Height="25px" Width="25px" OnClick = "ExportToExcel" /></div>
                        </div>
                    
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                       <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover dataTable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                      <asp:TemplateField HeaderText="#">
                                          <HeaderTemplate>  
                                                    <asp:CheckBox ID="CheckBox1" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />

                                          </HeaderTemplate> 
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                    <asp:TemplateField HeaderText="From date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdate" runat="server" Text='<%#Eval("Fromdate") %>'></asp:Label>
                                             <asp:Label ID="lblId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                               <asp:Label ID="LabMobile" runat="server" Text='<%#Eval("MObile") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("ToDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="UserID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("userid") %>'></asp:Label>||
                                              <asp:Label ID="lblusername" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                      
                                     <asp:TemplateField HeaderText="Bank Account">
                                        <ItemTemplate>
                                            A/C:<asp:Label ID="lblaccountno" runat="server" Text='<%#Eval("accountno") %>'></asp:Label><br />
                                            IFSC:   <asp:Label ID="Labelifsccode" runat="server" Text='<%#Eval("ifsccode") %>'></asp:Label>
                                             A/C Holder:   <asp:Label ID="LabelacHolder" runat="server" Text='<%#Eval("AccountHolderName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Other Detail">
                                                    <ItemTemplate>
                                                        <label class="myLabel">PhonePay - </label>
                                                        <asp:Label ID="lblphonepay" runat="server" Text='<%#Eval("PhonePay") %>'></asp:Label>
                                                        <label class="myLabel">BHIM ID - </label>
                                                        <asp:Label ID="lblbhim" runat="server" Text='<%#Eval("BhimNo") %>'></asp:Label>
                                                        <label class="myLabel">UPI ID - </label>
                                                        <asp:Label ID="lblupi" runat="server" Text='<%#Eval("UPINo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                   <%--  <asp:TemplateField HeaderText="Binary Income" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBindaryincome" runat="server" Text='<%#Eval("BinaryIncome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    --%>     
                                         <asp:TemplateField HeaderText="Self Purchase Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirect" runat="server" Text='<%#Eval("SelfPurchaseIncomee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                          <asp:TemplateField HeaderText="Matching Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbinaryincome" runat="server" Text='<%#Eval("matchingincomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Leadership Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadership" runat="server" Text='<%#Eval("leadershipincomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Director Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirector" runat="server" Text='<%#Eval("directorincomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Gold Director Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGold" runat="server" Text='<%#Eval("golddirectorincomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Crown Director Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrown" runat="server" Text='<%#Eval("crowndirectorIncomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                     
                                      <asp:TemplateField HeaderText="Platinum Director Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlatinum" runat="server" Text='<%#Eval("platinumdirectorIncomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Diamond Director Incomee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAward" runat="server" Text='<%#Eval("diamonddirectorIncomee") %>'></asp:Label>
                                        </ItemTemplate>
                                               </asp:TemplateField>
                                   
                                    
                                      <%--   <asp:TemplateField HeaderText="Award Income">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAward" runat="server" Text='<%#Eval("Levelincome") %>'></asp:Label>
                                        </ItemTemplate>  
                                    </asp:TemplateField>--%>
                              
                                      
                                    <asp:TemplateField HeaderText="TotalIncome">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmathingBv" runat="server" Text='<%#Eval("TotalIncome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                     <asp:TemplateField HeaderText="AdminCharge">
                                        <ItemTemplate>
                               
                                              <asp:Label ID="lblAdminCharge" runat="server" Text='<%#Eval("AdminCharge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                        
                                     <asp:TemplateField HeaderText="TDS Charge">
                                        <ItemTemplate>
                                   
                                               <asp:Label ID="lblTDSCharge" runat="server" Text='<%#Eval("TDS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                   
                                     <asp:TemplateField HeaderText="Net Pay">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaybleAmount" runat="server" Text='<%#Eval("PaybleAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     
                                      <asp:TemplateField HeaderText="Generate Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgeneratedate" runat="server" Text='<%#Eval("GenerateDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="TransactionID" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtTransaction" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                       <asp:TemplateField HeaderText="TransactionID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransaction" runat="server" Text='<%#Eval("TransactionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                  
                                    
                                   
                                </Columns>
                            </asp:GridView>
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



           
        </ContentTemplate>
          <Triggers>
      
        <asp:PostBackTrigger ControlID = "ImageButton1" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
</asp:Content>


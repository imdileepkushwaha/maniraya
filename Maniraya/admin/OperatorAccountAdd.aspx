<%@ Page Title="Add Operator Account" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="OperatorAccountAdd.aspx.cs" Inherits="admin_OperatorAccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
      Add Operator Account   
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Operator Management</a></li>
        <li class="active">Add Operator Account</li>
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
              <h3 class="box-title">Operator List</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
			<div class="col-md-12">
							
								<div class="panel panel-white">
									
                                        <div class="col-md-3">
                                   
                                         <asp:DropDownList ID="DDlOpertor" OnSelectedIndexChanged="DDlOpertor_SelectedIndexChanged"  AutoPostBack="true" class="form-control" runat="server">
                                         
                                         </asp:DropDownList>
                                        
                                     
                                 </div>
                                 <%--<div class="col-md-3">
                                     <div class="form-group">
                                         <input id="TxtSearch" type="text" placeholder="Search Operator" onkeyup="searchName(this.value,'')" class="form-control" list="ContentPlaceHolder1_Searchdata"/>
                                                    <datalist id="Searchdata" runat="server">
                                         </datalist>
                                     </div>
                                 </div>--%>
                               
                                 <div class="col-md-1">
                                    
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" class="btn btn-danger">Update</asp:LinkButton>
                                    
                                 </div>
                                     <div class="col-md-2">
                                   
                                         <asp:Label ID="lBLmESSAGE" runat="server" ></asp:Label>
                                    
                                 </div>
                                <div class="row">
                                 
                                           <div class="col-md-12">
                                               <div class="col-md-12">
                                                   <asp:CheckBox ID="CheckBox1"  runat="server" Text="Option 1"/>
                                                 <%--  Option 1--%>
                                               </div>
                                               
                                                <div class="col-md-3">
                                   
                                         <asp:TextBox ID="Txtre1" placeholder="ReplaceValue" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                <div class="col-md-9">
                                    
                                         <asp:TextBox ID="TxtField1" CssClass="form-control"  placeholder="Field name" runat="server"></asp:TextBox>
                                        
                                                    </div>

                                            </div>
                                              <div class="col-md-12">
                                               <div class="col-md-12">
                                                   <asp:CheckBox ID="CheckBox2"  runat="server" Text="Option 2"/>
                                                <%--   Option 2--%>
                                               </div>
                                               
                                                <div class="col-md-3">
                                    
                                         <asp:TextBox ID="Txtre2" placeholder="ReplaceValue" CssClass="form-control" runat="server"></asp:TextBox>
                                        
                                                    </div>
                                                <div class="col-md-9">
                                    
                                         <asp:TextBox ID="TxtField2" CssClass="form-control" placeholder="Field name" runat="server"></asp:TextBox>
                                        
                                                    </div>

                                            </div>
                                    <div class="col-md-12">
                                               <div class="col-md-12">
                                                   <asp:CheckBox ID="CheckBox3"  runat="server" Text="Option 3"/>
                                                 <%--  Option 3--%>
                                               </div>
                                               
                                                <div class="col-md-3">
                                    
                                         <asp:TextBox ID="Txtre3" placeholder="ReplaceValue" CssClass="form-control" runat="server"></asp:TextBox>
                                         
                                                    </div>
                                                <div class="col-md-9">
                                    
                                         <asp:TextBox ID="TxtField3" CssClass="form-control" placeholder="Field name" runat="server"></asp:TextBox>
                                         </div>
                                                    

                                            </div>
                                    <div class="col-md-12">
                                               <div class="col-md-12">
                                                   <asp:CheckBox ID="CheckBox4"  runat="server" Text="Option 4"/>
                                                  <%-- Option 4--%>
                                               </div>
                                               
                                                <div class="col-md-3">
                                   
                                         <asp:TextBox ID="Txtre4" placeholder="ReplaceValue" CssClass="form-control" runat="server"></asp:TextBox>
                                        
                                                    </div>
                                                <div class="col-md-9">
                                    
                                         <asp:TextBox ID="TxtField4" CssClass="form-control" placeholder="Field name" runat="server"></asp:TextBox>
                                      
                                                    </div>

                                            </div>
                                            </div>
									
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
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
  
</asp:Content>


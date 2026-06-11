<%@ Page Title="Generate Closing" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="CalculateSelfPurchaseClosing.aspx.cs" Inherits="CalculateSelfPurchaseClosing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" runat="Server">
     <section class="content-header">
      <h1>
      Generate SelfPurchase Closing 
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Closing</a></li>
        <li class="active">Generate Joining Closing</li>
      </ol>
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress id="updateProgress" runat="server">
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
              <h3 class="box-title">Generate SelfPurchase Closing</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
               <div class="row">
                         <div class="col-md-4">
                  <label >From Date</label>
                 <asp:TextBox ID="TxtFromdate" CssClass="form-control"  Enabled="false"  runat="server"></asp:TextBox>
                </div>     
                     <div class="col-md-4">
                  <label >To Date</label>
                 <asp:TextBox ID="TxtTodate" CssClass="form-control form_date" runat="server"></asp:TextBox>
                </div>  
                     <div class="col-md-4">
                 
                       
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
              

                
            </div>

          
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" runat="Server">
   
  

    <script type="text/javascript">
       $('.form_date').datepicker({
           format: 'dd/M/yyyy',
       }).on('changeDate', function (ev) {
           $(this).datepicker('hide');
       });
     </script>
       <script src="../bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(LoadHandler);
        function LoadHandler() {
            $('.form_date').datepicker({
                format: 'dd/M/yyyy',
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
     </script>
</asp:Content>




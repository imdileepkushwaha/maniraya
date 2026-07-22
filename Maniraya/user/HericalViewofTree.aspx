<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="HericalViewofTree.aspx.cs" Inherits="user_HericalViewofTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
       <section class="content-header">
      <h1>
       Tree View Sponserwise
      </h1>
      <ol class="breadcrumb text-white">
     <li><a href="Dashboard.aspx"><i class="fa fa-tachometer-alt"></i> Home</a></li>
            <li><a href="#">User</a></li>
        <li class="active">Tree View Sponserwise  </li>
      
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
                            <h3 class="box-title"></h3>
                        </div>
                      <div class="box-body">
                    <div class="row">
                    <div class="col-md-6">
                <div class="form-group">
                  <label >User ID</label>
                  <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox>
                </div>             
               </div>
                    <div class="col-md-6">
                <div class="form-group">
                
                </div>             
               </div>
                      </div>
                   <div class="box-footer">
               
                 <asp:Button ID="btnSubmit"  CssClass="btn btn-success" runat="server" Text="Search" OnClientClick="drawChart();" Visible="false" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" Visible="false" />
              </div>
                  </div>
                        </div>
                    </div>
                </div>
            <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title"></h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                  <div class="row">
                    <div class="col-md-12">
                <div class="form-group">
                  <label >Downline List</label>
                  <div class="table-responsive">
                      <div id="chart">
                                </div>
                        </div>
                </div>             
               </div>
                   
                      </div>
                  </div>

                  
                 
                 </div>
         </div>
                </div>
           <div id="MyPopup" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                <h4 class="modal-title">
                    User Detail
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    
                   
                    <table style="width: 100%;">
                        <tr>
                            <td>User Id:<asp:Label ID="LblUserID" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                            <td>User Name:<asp:Label ID="LblUserName" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                           
                        </tr>
                        
                        <tr>
                            <td>SponserId:<asp:Label ID="LblSponserId" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                            <td>Sponser Name:<asp:Label ID="LblSponserName" runat="server" Text="Label" ForeColor="Red"></asp:Label> </td>
                          
                        </tr>
                        
                          <tr>
                                <td>Reg Date :<asp:Label ID="LblRegdate" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
 <td>Activate Date :<asp:Label ID="Lblactivationdate" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                        
                          
                        </tr>
                           <tr>
                              <td>Status: <asp:Label ID="LblStatus" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>                          
                              <td></td>
                          
                        </tr>

                         <tr >
                              <td>Today Reg Left:<asp:Label ID="LblTodayREgLeft" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                            <td>Today Reg Right :<asp:Label ID="LblTodayREgRight" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                         <tr>
                              <td>Today Act Left:<asp:Label ID="LblTodayActLeft" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                              <td>Today Act Right:<asp:Label ID="LblTodayActRight" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                         <tr>
                              <td>Total Reg Left:<asp:Label ID="LblTotalRegLeft" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                                <td>Total Reg Right:<asp:Label ID="LblTotalRegRight" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                           
                          
                        </tr>
                         
                           <tr>
                              <td>Total Act Left:  <asp:Label ID="LblTotalACtLeft" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                         <td>Total Act Right: <asp:Label ID="LblTotalActRight" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                        <tr>
                              <td>Left SV:<asp:Label ID="LblLbv" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                              <td>Right SV :<asp:Label ID="LblRBv" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                        
                         
                       
                        
                         
                        
                    </table>





                </div>
               

                            
                     
              
                             
             
             
                        
          
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">
                    Close</button>
            </div>
        </div>
    </div>
</div>
            </ContentTemplate>
                            </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["orgchart"] });
  
    function drawChart() {

        var associateid = $('#<%=txtuserid.ClientID%>').val();
        $.ajax({
            type: "POST",
            url: "HericalViewofTree.aspx/GetChartData",
            data: "{UserId:'" + associateid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Entity');
                data.addColumn('string', 'ParentEntity');
                data.addColumn('string', 'ToolTip');
                for (var i = 0; i < r.d.length; i++) {
                    var employeeId = r.d[i][0].toString();
                    var employeeName = r.d[i][1];
                    var designation = r.d[i][2];
                    var reportingManager = r.d[i][3] != null ? r.d[i][3].toString() : '';
                    data.addRows([[{
                        v: employeeId,                     
                        f: employeeName + '<div><a href=HericalViewofTree.aspx?SuperId=' + employeeId + '><img src = "img/' + designation + '.png" style="width:50px;height:50px;" /></a><p>(<span>' + employeeId + '</span>)</p>(<span>' + designation + '</span>)</div>'
                    }, reportingManager, designation]]);
                }
                var chart = new google.visualization.OrgChart($("#chart")[0]);
                chart.draw(data, { allowHtml: true });
            },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
        });
    }
    google.setOnLoadCallback(drawChart);
</script>

     <script type="text/javascript">


         function showModal() {
             $('#MyPopup').modal({ backdrop: 'static', keyboard: false })
         }
         function Closepopup() {
             $('#MyPopup').modal('hide');
             $('body').removeClass('modal-open');
             $('body').css('padding-right', '0');
             $('.modal-backdrop').remove();

         }

         //<p><a href=javascript:; onclick="getpopup(' + employeeId + ');">View Detail</a></p>
         function getpopup(empid) {
             var usrid = empid;
             $.ajax({
                 type: "POST",
                 url: "HericalViewofTree.aspx/Getpopup",
                 data: "{UserId:'" + usrid + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (r) {
                     showModal();
                 },
                 failure: function (r) {
                     alert(r.d);
                 },
                 error: function (r) {
                     alert(r.d);
                 }
             });
             return false;
         }
        </script>
</asp:Content>


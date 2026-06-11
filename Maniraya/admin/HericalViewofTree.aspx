<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="HericalViewofTree.aspx.cs" Inherits="admin_HericalViewofTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
      <section class="content-header">
        <h1>Tree view</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">My Network</a></li>
            <li class="active">Tree view</li>
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
                            <h3 class="box-title">Search Criteria</h3>
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
               
                 <asp:Button ID="btnSubmit"  CssClass="btn btn-success" runat="server" Text="Search" OnClientClick="drawChart();" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel" />
              </div>
                  </div>
                        </div>
                    </div>
                </div>
            <div class="row">
     <div class="col-md-12">

             <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Search Criteria</h3>
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
            </ContentTemplate>
                            </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["orgchart"] });
  
    function drawChart() {
        debugger;
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
                        f: employeeName + '<div><p>(<span>' + employeeId + '</span>)</p>(<span>' + designation + '</span>)</div><img src = "img/' + designation + '.png" style="width:80px;height:80px;" />'
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
</asp:Content>


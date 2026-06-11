<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="AdminMenuPermission.aspx.cs" Inherits="admin_AdminMenuPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   
        <link href="https://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">
        <link rel="stylesheet" type="text/css" href="https://www.jqueryscript.net/demo/jQuery-Plugin-To-Create-Checkbox-Tree-View-highchecktree/css/highCheckTree.css"/>
        <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel='stylesheet' type='text/css'>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
     <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">User Permission </h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>User Id :</label>
                                        <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control txtuser" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group"><br />
                                        <input type="button" class="btn btn-primary" onclick="bindmenu();" value="Search"/>&nbsp;&nbsp;
                                        <input type="button" id="btnupdate" class="btn btn-danger" onclick="updatemenu();" value="Update" style="display:none" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
    <div id="tree-container">
       </div>
                           
                        </div>
                    </div>
                </div>
            </div>

    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
      <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
        <script type="text/javascript" src="https://www.jqueryscript.net/demo/jQuery-Plugin-To-Create-Checkbox-Tree-View-highchecktree/js/highchecktree.js"></script>
     <script>
        

         function bindmenu() {

             var user = $(".txtuser").val();
             if (user == "") {

                 alert('Enter UserId');
                 $("#btnupdate").hide();
                 return;
             }

             $.ajax({
                 url: "AdminMenuPermission.aspx/GetUserMenu",
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 data: "{'user': '" + user + "' }",
                 success: function (r) {
                   
                     

                     if (r.d != null) {

                         $("#btnupdate").show();
                         var mockData = [];


                         for (var i = 0; i < r.d.length; i++) {

                             var m = {
                                 id: r.d[i].MainMenuId.toString(),
                                 label: r.d[i].MainMenuName.toString(),
                                 checked: r.d[i].Checked
                             };


                             var s = [];
                             for (var j = 0; j < r.d[i].MenuId.length; j++) {

                                 s.push({
                                     item: {
                                         id: r.d[i].MenuId[j].Menu.toString(),
                                         label: r.d[i].MenuId[j].MenuName.toString(),
                                         checked: r.d[i].MenuId[j].Checked
                                     }
                                 });
                             }


                             var item = { item: m, children: s };
                             mockData.push(item);
                         }

                       
                         $('#tree-container').highCheckTree({
                             data: mockData
                         });
                         

                     }
                     else {

                         $("#btnupdate").hide();
                         alert('No reord found');
                         return;

                     }
                },
                error: function (r) { }
            });

         }


         function updatemenu() {

             var user = $(".txtuser").val();
             if (user == "") {

                 $("#btnupdate").hide();
                 alert('Enter UserId');
                 return;
             }

             var main = [];
             var sub = [];


             $(".checktree").children("li").each(function () {

                 var div = $(this).find(".checkbox");
                 var chk = $(div).attr("class");

                 if (chk.indexOf("half_checked") != -1 || chk.indexOf("checked") != -1) {
                     var m = $(this).attr("rel");
                     main.push(parseInt($(this).attr("rel")));

                     $(this).children("ul").children("li").each(function () {

                         var divsub = $(this).find(".checkbox");
                         var chksub = $(divsub).attr("class");

                       
                         if (chksub.indexOf("half_checked") != -1 || chksub.indexOf("checked") != -1) {
                             sub.push({
                                 M : parseInt(m) ,
                                 S: parseInt($(this).attr("rel"))
                             });
                         }


                     });

                 }

             });

             console.log(main);
             console.log(sub);

             var DT = {
                 user: user,
                 MainMenu: main,
                 SubMenu: sub
             };


             console.log(DT);

             console.log(JSON.stringify(DT));
            
             $.ajax({
                 url: "AdminMenuPermission.aspx/UpdateUserMenu",
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 data: '{ "Data": '+JSON.stringify(DT)+' }',
                 success: function (r) {

                     if (r.d == 1) {
                         alert('Menu Permission Updated Successfully');
                     }
                     else {
                         alert('Menu Permission Update Failed');
                     }

                 },
                 error: function (r) { }
             });


         }


        </script> 

</asp:Content>


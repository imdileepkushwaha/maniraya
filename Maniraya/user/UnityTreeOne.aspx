<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnityTreeOne.aspx.cs" Inherits="UnityTreeOne" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <script src="../MyJs/jquery-1.8.2.js"></script>
    <script src="../MyJs/jquery.tooltip.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' media="screen" />
 
    <script type="text/javascript">

        function InitializeToolTip() {

            $(".gridViewToolTip").tooltip({

                track: true,

                delay: 0,

                showURL: false,

                fade: 100,

                bodyHandler: function () {

                    return $($(this).next().html());

                },

                showURL: false

            });

        }

    </script>

    <script type="text/javascript">

        $(function () {

            InitializeToolTip();

        })

    </script>
     <style type="text/css">

        #tooltip    {

            position: absolute;

            z-index: 3000;

            border: 1px solid #111;

            background-color: #C2E0FF;

            padding: 5px;

            opacity: 0.85;    }

        #tooltip h3, #tooltip div

        {  margin: 0;  }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
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
                                <td>Reg Date :<asp:Label ID="LblDOB" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
 <td>Date of Birth :<asp:Label ID="Lbldateofbirth" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                        
                          
                        </tr>
                           <tr>
                              <td>Status: <asp:Label ID="LblStatus" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>                          
                              <td>Activate Date :<asp:Label ID="LblMobileno" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
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
                              <td>Left PT:<asp:Label ID="LblLbv" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                              <td>Right PT :<asp:Label ID="LblRBv" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                        
                         
                         <tr>
                              <td>Left Purchase:<asp:Label ID="LblLeftsale" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                              <td>Right Purchase :<asp:Label ID="LblRightSale" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                         <tr>
                              
                              <td>Own Purchase :<asp:Label ID="LblOwnpurchase" runat="server" Text="Label" ForeColor="Red"></asp:Label></td>
                          
                        </tr>
                          <tr style="display:none;">
                              <td>Rank</td>
                            <td>    <asp:Label ID="LblRank" runat="server" Text="Label"></asp:Label></td>
                          
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
        <script type="text/javascript">
            function showModal() {
                $('#MyPopup').modal({ backdrop: 'static', keyboard: false })
            }
            //function ShowPopup(title, body) {
            //    $("#MyPopup .modal-title").html(title);
            //    $("#MyPopup .modal-body").html(body);
            //    $("#MyPopup").modal("show");
            //}
</script>
        <table style="width: 100%; color:#fff;">
            <tr>
                <td style="text-align: center;" colspan="8">
                    <%--<img src="img/0.png" style="height:70px;" />--%>
                    <asp:Literal ID="ltuser1" runat="server"></asp:Literal>
                        <div id="Div1" style="display: none;">

                        
                    </div>
                    <br />
                   <asp:Label ID="lblusername1" runat="server" Text="" ForeColor="Black"></asp:Label><br />
                    <asp:LinkButton ID="lbluserid1" runat="server" Text="" ForeColor="Red" OnClick="lbluserid1_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <img src="img/band1.gif" />
                </td>
            </tr>
            <tr>

                <td style="text-align: center;" colspan="4">
                    <asp:Literal ID="ltuser2" runat="server"></asp:Literal>
                              <div id="Div3" style="display: none;">
                        

                    </div>
                    <br />
                
                    <asp:Label ID="lblusername2" runat="server" Text="" ForeColor="Black"></asp:Label> <br />
                       <asp:LinkButton ID="lbluserid2" runat="server" Text="" ForeColor="Black" OnClick="lbluserid2_Click"></asp:LinkButton>

                     


      
                </td>
                <td style="text-align: center;" colspan="4">
                    <asp:Literal ID="ltuser3" runat="server"></asp:Literal>
                          <div id="Div2" style="display: none;">
                        
                    </div>
                    <br />
                 
                    <asp:Label ID="lblusername3" runat="server" Text="" ForeColor="Black"></asp:Label><br />
                       <asp:LinkButton ID="lbluserid3" runat="server" Text="" ForeColor="Black" OnClick="lbluserid3_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>

                <td style="text-align: center;" colspan="4">
                    <img src="img/band2.gif"  />
                </td>
                <td style="text-align: center;" colspan="4">
                    <img src="img/band2.gif" />
                </td>
            </tr>

            <tr>

                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser4" runat="server"></asp:Literal>
                       <div id="Div4" style="display: none;">

                        

                    </div>
                    <br />
                 
                    <asp:Label ID="lblusername4" runat="server" Text="" ForeColor="Black"></asp:Label><br />
                       <asp:LinkButton ID="lbluserid4" runat="server" Text="" ForeColor="Black" OnClick="lbluserid4_Click"></asp:LinkButton>

                </td>
                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser5" runat="server"></asp:Literal>
                       <div id="Div5" style="display: none;">

                          

                    </div>
                    <br />
                  
                    <asp:Label ID="lblusername5" runat="server" Text="" ForeColor="Black" ></asp:Label><br />
                      <asp:LinkButton ID="lbluserid5" runat="server" Text="" ForeColor="Black" OnClick="lbluserid5_Click"></asp:LinkButton>

                </td>
                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser6" runat="server"></asp:Literal>
                       <div id="Div6" style="display: none;">

                           

                    </div>
                    <br />
                    
                    <asp:Label ID="lblusername6" runat="server" Text="" ForeColor="Black" ></asp:Label><br />
                    <asp:LinkButton ID="lbluserid6" runat="server" Text="" ForeColor="Black" OnClick="lbluserid6_Click"></asp:LinkButton>


                </td>
                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser7" runat="server"></asp:Literal>
                       <div id="Div7" style="display: none;">

                            

                    </div>
                    <br />
                
                    <asp:Label ID="lblusername7" runat="server" Text="" ForeColor="Black" ></asp:Label><br />
                        <asp:LinkButton ID="lbluserid7" runat="server" Text="" ForeColor="Black" OnClick="lbluserid7_Click"></asp:LinkButton>

                </td>
            </tr>
              <tr>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
            </tr>
            
        </table>
    </form>
</body>
</html>

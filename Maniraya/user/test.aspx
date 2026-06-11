<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="User_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <script src="../MyJs/jquery-1.8.2.js"></script>
    <script src="../MyJs/jquery.tooltip.min.js" type="text/javascript"></script>
    
 
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
          
        </div>

        <table style="width: 100%; color:#fff;">
            <tr>
                <td style="text-align: center;" colspan="4">
                    <%--<img src="img/0.png" style="height:70px;" />--%>
                    <asp:Literal ID="ltuser1" runat="server"></asp:Literal>
                        <div id="Div1" style="display: none;">

                        <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>
                    </div>
                    <br />
                    <asp:Label ID="lbluserid1" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="4">
                    <img src="img/band1.gif" />
                </td>
            </tr>
            <tr>

                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser2" runat="server"></asp:Literal>
                              <div id="Div3" style="display: none;">
                        <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID25" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName25" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName1" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus1" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv1" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount1" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase1" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount1" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining1" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase1" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount1" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>

                    </div>
                    <br />
                    <asp:Label ID="lbluserid2" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername2" runat="server" Text=""></asp:Label>

                    


      
                </td>
                <td style="text-align: center;" colspan="2">
                    <asp:Literal ID="ltuser3" runat="server"></asp:Literal>
                          <div id="Div2" style="display: none;">
                        <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID26" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName26" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName2" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus2" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv2" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount2" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase2" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount2" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining2" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase2" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount2" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>
                    </div>
                    <br />
                    <asp:Label ID="lbluserid3" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername3" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>

                <td style="text-align: center;" colspan="2">
                    <img src="img/band2.gif"  />
                </td>
                <td style="text-align: center;" colspan="2">
                    <img src="img/band2.gif" />
                </td>
            </tr>

            <tr>

                <td style="text-align: center;" colspan="1">
                    <asp:Literal ID="ltuser4" runat="server"></asp:Literal>
                       <div id="Div4" style="display: none;">

                        <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID27" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName27" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName3" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus3" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv3" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount3" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase3" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount3" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining3" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase3" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount3" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>

                    </div>
                    <br />
                    <asp:Label ID="lbluserid4" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername4" runat="server" Text=""></asp:Label>

                </td>
                <td style="text-align: center;" colspan="1">
                    <asp:Literal ID="ltuser5" runat="server"></asp:Literal>
                       <div id="Div5" style="display: none;">

                          <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID28" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName28" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName4" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus4" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv4" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount4" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase4" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount4" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining4" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase4" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount4" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>

                    </div>
                    <br />
                    <asp:Label ID="lbluserid5" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername5" runat="server" Text=""></asp:Label>

                </td>
                <td style="text-align: center;" colspan="1">
                    <asp:Literal ID="ltuser6" runat="server"></asp:Literal>
                       <div id="Div6" style="display: none;">

                           <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID29" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName29" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName5" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus5" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv5" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount5" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase5" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount5" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining5" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase5" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount5" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>

                    </div>
                    <br />
                    <asp:Label ID="lbluserid6" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername6" runat="server" Text=""></asp:Label>


                </td>
                <td style="text-align: center;" colspan="1">
                    <asp:Literal ID="ltuser7" runat="server"></asp:Literal>
                       <div id="Div7" style="display: none;">

                            <table style="width:100%;">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <b>User ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblUserID30" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>User Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblUserName30" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Sponser ID :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblSponserId6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Sponser Name :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblSponserName6" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Date of Joining :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblDOB6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Status :</b>&nbsp;
                                </td>
                                   <td>
                                    <asp:Label ID="LblStatus6" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>

                                 <tr>
                                <td style="white-space: nowrap;">
                                    <b>Mobile No :</b>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblMobileno6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   
                            </tr>

                          <tr >
                               
                                   <td>
                              ---------
                                </td> 
                                <td>
                              ------------
                                </td> 
                                <td>
                              -----------
                                </td> 
                                <td>
                              ----------
                                </td> 

                            </tr>
                             <tr >
                                <td style="white-space: nowrap;">
                                    <b>Performance </b>&nbsp;
                                </td>
                                <td>
                                   <b>Joining BV </b>&nbsp;
                                </td> 
                                   <td style="white-space: nowrap;">
                                    <b>Repurchase BV</b>&nbsp;
                                </td>
                                   <td>
                                   <b>Count</b>&nbsp;
                                </td> 
                            </tr>
                             
                             <tr>
                                <td style="white-space: nowrap;">
                                    <b>Self :</b>&nbsp;
                                </td>
                                <td>
                                   <asp:Label ID="LblSelfJoiningBv6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                   <asp:Label ID="LblSelfRepurchaseBv6" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                 <asp:Label ID="LblSelfCount6" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                              <tr>
                                <td style="white-space: nowrap;">
                                    <b>Left Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblLeftTeamjoining6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblLeftTeamRepurchase6" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblLeftteamCount6" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                               <tr>
                                <td style="white-space: nowrap;">
                                    <b>Right Team :</b>&nbsp;
                                </td>
                                <td>
                                  <asp:Label ID="LblRightTeamjoining6" runat="server" Text="Label"></asp:Label>
                                </td> 
                                   <td style="white-space: nowrap;">
                                     <asp:Label ID="LblRightTeamRepurchase6" runat="server" Text="Label"></asp:Label>
                                </td>
                                   <td>
                                <asp:Label ID="LblRightteamCount6" runat="server" Text="Label"></asp:Label>
                                </td> 
                            </tr>
                            
                             
                        </table>

                    </div>
                    <br />
                    <asp:Label ID="lbluserid7" runat="server" Text=""></asp:Label>-<asp:Label ID="lblusername7" runat="server" Text=""></asp:Label>

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

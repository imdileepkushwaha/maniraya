<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnityTreeOne.aspx.cs" Inherits="UnityTreeOne" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=5" />
    <title>Binary Tree</title>
    <link href="new_assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/css/binary-tree.css?v=3" rel="stylesheet" />
    <script src="../MyJs/jquery-1.8.2.js"></script>
    <script src="../MyJs/jquery.tooltip.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" media="screen" />
    <script type="text/javascript">
        function InitializeToolTip() {
            $(".gridViewToolTip").tooltip({
                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function () {
                    return $($(this).next().html());
                }
            });
        }
        $(function () {
            InitializeToolTip();
        });
    </script>
</head>
<body class="binary-tree-body">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div id="MyPopup" class="modal fade binary-user-modal" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header binary-user-modal-head">
                        <div>
                            <h4 class="modal-title">User Detail</h4>
                            <p class="binary-user-modal-sub">Member overview and team statistics</p>
                        </div>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="binary-user-section">
                            <h5 class="binary-user-section-title"><i class="fa fa-user"></i> Basic Information</h5>
                            <div class="binary-user-grid">
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">User ID</span>
                                    <asp:Label ID="LblUserID" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">User Name</span>
                                    <asp:Label ID="LblUserName" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Sponsor ID</span>
                                    <asp:Label ID="LblSponserId" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Sponsor Name</span>
                                    <asp:Label ID="LblSponserName" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Reg Date</span>
                                    <asp:Label ID="LblDOB" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Date of Birth</span>
                                    <asp:Label ID="Lbldateofbirth" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Status</span>
                                    <asp:Label ID="LblStatus" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Activate Date</span>
                                    <asp:Label ID="LblMobileno" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="binary-user-section">
                            <h5 class="binary-user-section-title"><i class="fa fa-calendar"></i> Today Statistics</h5>
                            <div class="binary-user-grid">
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Today Reg Left</span>
                                    <asp:Label ID="LblTodayREgLeft" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Today Reg Right</span>
                                    <asp:Label ID="LblTodayREgRight" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Today Act Left</span>
                                    <asp:Label ID="LblTodayActLeft" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Today Act Right</span>
                                    <asp:Label ID="LblTodayActRight" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="binary-user-section">
                            <h5 class="binary-user-section-title"><i class="fa fa-users"></i> Total Team Statistics</h5>
                            <div class="binary-user-grid">
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Total Reg Left</span>
                                    <asp:Label ID="LblTotalRegLeft" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Total Reg Right</span>
                                    <asp:Label ID="LblTotalRegRight" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Total Act Left</span>
                                    <asp:Label ID="LblTotalACtLeft" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Total Act Right</span>
                                    <asp:Label ID="LblTotalActRight" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="binary-user-section">
                            <h5 class="binary-user-section-title"><i class="fa fa-chart-line"></i> BV &amp; Purchase</h5>
                            <div class="binary-user-grid">
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Left PT</span>
                                    <asp:Label ID="LblLbv" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Right PT</span>
                                    <asp:Label ID="LblRBv" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Left Purchase</span>
                                    <asp:Label ID="LblLeftsale" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Right Purchase</span>
                                    <asp:Label ID="LblRightSale" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field">
                                    <span class="binary-user-field-label">Own Purchase</span>
                                    <asp:Label ID="LblOwnpurchase" runat="server" Text="0" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                                <div class="binary-user-field" style="display:none;">
                                    <span class="binary-user-field-label">Rank</span>
                                    <asp:Label ID="LblRank" runat="server" Text="-" CssClass="binary-user-field-value"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function showModal() {
               INR('#MyPopup').modal({ backdrop: 'static', keyboard: false });
            }
        </script>

        <div class="binary-tree-page">
            <div class="binary-tree-legend">
                <span class="binary-tree-legend-item"><span class="binary-tree-legend-dot is-golden"></span> Saving + Active</span>
                <span class="binary-tree-legend-item"><span class="binary-tree-legend-dot is-saving"></span> Saving Active</span>
                <span class="binary-tree-legend-item"><span class="binary-tree-legend-dot is-member"></span> Member Active</span>
                <span class="binary-tree-legend-item"><span class="binary-tree-legend-dot is-inactive"></span> Inactive</span>
                <span class="binary-tree-legend-item"><span class="binary-tree-legend-dot is-empty"></span> Available</span>
            </div>

            <div class="binary-tree-scroll-wrap">
            <table class="binary-tree-table">
                <tr>
                    <td class="binary-tree-cell" colspan="8">
                        <div class="binary-tree-node-card is-root">
                            <asp:Literal ID="ltuser1" runat="server"></asp:Literal>
                            <div id="Div1" style="display: none;"></div>
                            <asp:Label ID="lblusername1" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid1" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid1_Click"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="binary-tree-cell binary-tree-connector" colspan="8">
                        <img src="img/band1.gif" alt="" />
                    </td>
                </tr>
                <tr>
                    <td class="binary-tree-cell" colspan="4">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser2" runat="server"></asp:Literal>
                            <div id="Div3" style="display: none;"></div>
                            <asp:Label ID="lblusername2" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid2" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid2_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td class="binary-tree-cell" colspan="4">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser3" runat="server"></asp:Literal>
                            <div id="Div2" style="display: none;"></div>
                            <asp:Label ID="lblusername3" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid3" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid3_Click"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="binary-tree-cell binary-tree-connector" colspan="4">
                        <img src="img/band2.gif" alt="" />
                    </td>
                    <td class="binary-tree-cell binary-tree-connector" colspan="4">
                        <img src="img/band2.gif" alt="" />
                    </td>
                </tr>
                <tr>
                    <td class="binary-tree-cell" colspan="2">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser4" runat="server"></asp:Literal>
                            <div id="Div4" style="display: none;"></div>
                            <asp:Label ID="lblusername4" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid4" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid4_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td class="binary-tree-cell" colspan="2">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser5" runat="server"></asp:Literal>
                            <div id="Div5" style="display: none;"></div>
                            <asp:Label ID="lblusername5" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid5" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid5_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td class="binary-tree-cell" colspan="2">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser6" runat="server"></asp:Literal>
                            <div id="Div6" style="display: none;"></div>
                            <asp:Label ID="lblusername6" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid6" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid6_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td class="binary-tree-cell" colspan="2">
                        <div class="binary-tree-node-card">
                            <asp:Literal ID="ltuser7" runat="server"></asp:Literal>
                            <div id="Div7" style="display: none;"></div>
                            <asp:Label ID="lblusername7" runat="server" Text="" CssClass="binary-tree-node-name"></asp:Label>
                            <asp:LinkButton ID="lbluserid7" runat="server" Text="" CssClass="binary-tree-node-id" OnClick="lbluserid7_Click"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>

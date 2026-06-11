using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class User_test : System.Web.UI.Page
{
    clsUser objUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
       loadbinary(Request.QueryString["SuperId"].ToString());
    }

    DataTable getleftdata(string str_id)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = objUser.getLeftDataPlanWise2(objUser);
        return dt;
    }   
    DataTable getTeamBv(string str_id,string position,string type)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = objUser.getTeamSumBV(objUser, position, type);
        return dt;
    }
    DataTable getSelfBv(string str_id)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = objUser.getselfSumBV(objUser);
        return dt;
    }   
    DataTable getRightdata(string str_id)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = objUser.getRightDataPlanWise2(objUser);
        return dt;
    }
    public string  fetchimage(string Gender,string Status)
    {
        string imagepath = "";

        if (Status.ToUpper() == "ACTIVE")
        {
            imagepath = "img/green.png";
        }
        if (Status.ToUpper() == "DEACTIVE")
        {
            imagepath = "img/red.png";
        }
        if (Gender.ToUpper() == "MALE")
        {
            if (Status.ToUpper() == "ACTIVE")
            {
                imagepath = "img/green.png";
            }
            if (Status.ToUpper() == "DEACTIVE")
            {
                imagepath = "img/red.png";
            }
        }
        if (Gender.ToUpper() == "FEMALE")
        {
            if (Status.ToUpper() == "ACTIVE")
            {
                imagepath = "img/greenfemale.png";
            }
            if (Status.ToUpper() == "DEACTIVE")
            {
                imagepath = "img/redfemale.png";
            }
        }

        if (Status.ToUpper() == "ACTIVE")
            {
                imagepath = "img/green.png";
            }
            if (Status.ToUpper() == "DEACTIVE")
            {
                imagepath = "img/red.png";
            }
        if (imagepath == "")
        {
            imagepath = "img/red.png";
        }

        return imagepath;
    }
    void loadbinary(string str_superid)
    {
        objUser.UserId = str_superid;

        DataTable dt = new DataTable();
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            //divdata.Visible = true;
            lbluserid1.Text = dt.Rows[0]["Userid"].ToString();
            lblusername1.Text = dt.Rows[0]["username"].ToString();          
            ltuser1.Text = @"<a href='test.aspx?SuperId=" + dt.Rows[0]["Userid"].ToString() + @"'    class='gridViewToolTip' ><img src="""+fetchimage(dt.Rows[0]["gender"].ToString(),dt.Rows[0]["Status"].ToString())+@""" style=""height:70px;"" /></a>";
            LblUserID.Text = dt.Rows[0]["Userid"].ToString();
            LblUserName.Text = dt.Rows[0]["username"].ToString();
            LblSponserId.Text = dt.Rows[0]["SponserId"].ToString();
            LblSponserName.Text = dt.Rows[0]["SponserName"].ToString();
            LblDOB.Text = dt.Rows[0]["DOJ"].ToString();
            LblStatus.Text = dt.Rows[0]["Status"].ToString();
            LblMobileno.Text = dt.Rows[0]["Mobile"].ToString();

            DataTable dtleft = getleftdata(dt.Rows[0]["Userid"].ToString());
            DataTable DtLeftJoining = getTeamBv(dt.Rows[0]["Userid"].ToString(), "1", "J");
            DataTable DtLeftRepurchase = getTeamBv(dt.Rows[0]["Userid"].ToString(), "1", "R");
            DataTable DtRightJoining = getTeamBv(dt.Rows[0]["Userid"].ToString(), "2", "J");
            DataTable DtRightRepurchase = getTeamBv(dt.Rows[0]["Userid"].ToString(), "2", "R");
            DataTable DtSElfBv = getSelfBv(dt.Rows[0]["Userid"].ToString());
            
            if (dtleft.Rows.Count > 0)
            {
                LblLeftteamCount.Text = dtleft.Rows[0]["totaluser"].ToString();
            }
            else
            {
                LblLeftteamCount.Text = "0";
            }

            DataTable dtright = getRightdata(dt.Rows[0]["Userid"].ToString());
            if (dtright.Rows.Count > 0)
            {
                LblRightteamCount.Text = dtright.Rows[0]["totaluser"].ToString();
            }
            else
            {
                LblRightteamCount.Text = "0";
            }
            int dcmatch1 = 0;//Convert.ToInt32(LblLeftteamCount.Text) + Convert.ToInt32(LblRightteamCount.Text);
            LblSelfCount.Text = dcmatch1.ToString();
            LblLeftTeamjoining.Text = DtLeftJoining.Rows[0][0].ToString();
            LblLeftTeamRepurchase.Text = DtLeftRepurchase.Rows[0][0].ToString();
            LblRightTeamjoining.Text = DtRightJoining.Rows[0][0].ToString();
            LblRightTeamRepurchase.Text = DtRightRepurchase.Rows[0][0].ToString();
            LblSelfJoiningBv.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv.Rows[0]["RightJoiningBV"].ToString()));
            LblSelfRepurchaseBv.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv.Rows[0]["RightRepurchaseBV"].ToString()));
          //  lbltotalid1.Text = dcmatch1.ToString();
            //================ Second Child============
            DataTable dt2 = new DataTable();
            dt2 = loadchild(lbluserid1.Text, "1");
            if (dt2.Rows.Count > 0)
            {
                lbluserid2.Text = dt2.Rows[0]["Userid"].ToString();
                lblusername2.Text = dt2.Rows[0]["username"].ToString();
                ltuser2.Text = @"<a href='test.aspx?SuperId=" + dt2.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt2.Rows[0]["gender"].ToString(), dt2.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";
                LblUserID25.Text = dt2.Rows[0]["Userid"].ToString();
                LblUserName25.Text = dt2.Rows[0]["username"].ToString();
                LblSponserId1.Text = dt2.Rows[0]["SponserId"].ToString();
                LblSponserName1.Text = dt2.Rows[0]["SponserName"].ToString();
                LblDOB1.Text = dt2.Rows[0]["DOJ"].ToString();
                LblStatus1.Text = dt2.Rows[0]["Status"].ToString();
            //    LblMobileno1.Text = dt2.Rows[0]["Mobile"].ToString();

                DataTable dtleft2 = getleftdata(dt2.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining1 = getTeamBv(dt2.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase1 = getTeamBv(dt2.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining1 = getTeamBv(dt2.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase1 = getTeamBv(dt2.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv1 = getSelfBv(dt2.Rows[0]["Userid"].ToString());
                if (dtleft2.Rows.Count > 0)
                {
                    LblLeftteamCount1.Text = dtleft2.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblLeftteamCount1.Text = "0";
                }

                DataTable dtright2 = getRightdata(dt2.Rows[0]["Userid"].ToString());
                if (dtright2.Rows.Count > 0)
                {
                    LblRightteamCount1.Text = dtright2.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblRightteamCount1.Text = "0";
                }
                int dcmatch2 = 0;
                //dcmatch2 = Convert.ToInt32(LblLeftteamCount1.Text) + Convert.ToInt32(LblRightteamCount1.Text);
                LblSelfCount1.Text = dcmatch2.ToString();
                LblLeftTeamjoining1.Text = DtLeftJoining1.Rows[0][0].ToString();
                LblLeftTeamRepurchase1.Text = DtLeftRepurchase1.Rows[0][0].ToString();
                LblRightTeamjoining1.Text = DtRightJoining1.Rows[0][0].ToString();
                LblRightTeamRepurchase1.Text = DtRightRepurchase1.Rows[0][0].ToString();
                LblSelfJoiningBv1.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv1.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv1.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv1.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv1.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv1.Rows[0]["RightRepurchaseBV"].ToString()));
            }
            else
            {
				 string h=string.Format("<a href='https://mehdilink.in/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid1.Text,"1","img/default1.png","height:100px;");
                 ltuser2.Text = h;// @"<a href=""https://mehdilink.in/Regsiter.aspx?UserId="+Session["userid"].ToString()+"""><img src=""img/default1.png"" style=""height:70px;""/></a>";
                //ltuser2.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }
            //================ Second Child============


            //================ Third Child============

            DataTable dt3 = new DataTable();
            dt3 = loadchild(lbluserid1.Text, "2");
            if (dt3.Rows.Count > 0)
            {
                lbluserid3.Text = dt3.Rows[0]["Userid"].ToString();
                lblusername3.Text = dt3.Rows[0]["username"].ToString();
                ltuser3.Text = @"<a href='test.aspx?SuperId=" + dt3.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt3.Rows[0]["gender"].ToString(), dt3.Rows[0]["Status"].ToString()) + @""" style=""height:100px;"" /></a>";
                LblUserID26.Text = dt3.Rows[0]["Userid"].ToString();
                LblUserName26.Text = dt3.Rows[0]["username"].ToString();
                LblSponserId2.Text = dt3.Rows[0]["SponserId"].ToString();
                LblSponserName2.Text = dt3.Rows[0]["SponserName"].ToString();
                LblDOB2.Text = dt3.Rows[0]["DOJ"].ToString();
                LblStatus2.Text = dt3.Rows[0]["Status"].ToString();
               // LblMobileno2.Text = dt3.Rows[0]["Mobile"].ToString();

                DataTable dtleft3 = getleftdata(dt3.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining2 = getTeamBv(dt3.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase2 = getTeamBv(dt3.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining2 = getTeamBv(dt3.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase2 = getTeamBv(dt3.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv2 = getSelfBv(dt3.Rows[0]["Userid"].ToString());
                if (dtleft3.Rows.Count > 0)
                {

                    LblLeftteamCount2.Text = dtleft3.Rows[0]["totaluser"].ToString();


                }
                else
                {
                    LblLeftteamCount2.Text = "0";
                }

                DataTable dtright3 = getRightdata(dt3.Rows[0]["Userid"].ToString());
                if (dtright3.Rows.Count > 0)
                {
                    LblRightteamCount2.Text = dtright3.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblRightteamCount2.Text = "0";
                }
                int dcmatch3 = 0;
                //dcmatch3 = Convert.ToInt32(LblLeftteamCount2.Text) + Convert.ToInt32(LblRightteamCount2.Text);
                LblSelfCount2.Text = dcmatch3.ToString();
                LblLeftTeamjoining2.Text = DtLeftJoining2.Rows[0][0].ToString();
                LblLeftTeamRepurchase2.Text = DtLeftRepurchase2.Rows[0][0].ToString();
                LblRightTeamjoining2.Text = DtRightJoining2.Rows[0][0].ToString();
                LblRightTeamRepurchase2.Text = DtRightRepurchase2.Rows[0][0].ToString();
                LblSelfJoiningBv2.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv2.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv2.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv2.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv2.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv2.Rows[0]["RightRepurchaseBV"].ToString()));

            }
            else
            {
				 string h=string.Format("<a href='https://loveragone.com/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid1.Text,"2","img/default1.png","height:100px;");
                ltuser3.Text =h;// @"<a href=""https://loveragone.com/Regsiter.aspx?UserId="+Session["userid"].ToString()+"""><img src=""img/default1.png"" style=""height:70px;""/></a>";
               // ltuser3.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }


            //================ Third Child============

            //================ Fourth Child============

            DataTable dt4 = new DataTable();
            dt4 = loadchild(lbluserid2.Text, "1");
            if (dt4.Rows.Count > 0)
            {
                lbluserid4.Text = dt4.Rows[0]["Userid"].ToString();
                lblusername4.Text = dt4.Rows[0]["username"].ToString();
                ltuser4.Text = @"<a href='test.aspx?SuperId=" + dt4.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip'  ><img src=""" + fetchimage(dt4.Rows[0]["gender"].ToString(), dt4.Rows[0]["Status"].ToString()) + @""" style=""height:100px;"" /></a>";
                LblUserID27.Text = dt4.Rows[0]["Userid"].ToString();
                LblUserName27.Text = dt4.Rows[0]["username"].ToString();
                LblSponserId3.Text = dt4.Rows[0]["SponserId"].ToString();
                LblSponserName3.Text = dt4.Rows[0]["SponserName"].ToString();
                LblDOB3.Text = dt4.Rows[0]["DOJ"].ToString();
                LblStatus3.Text = dt4.Rows[0]["Status"].ToString();
              //  LblMobileno3.Text = dt4.Rows[0]["Mobile"].ToString();

                DataTable dtleft4 = getleftdata(dt4.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining3 = getTeamBv(dt4.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase3 = getTeamBv(dt4.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining3 = getTeamBv(dt4.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase3 = getTeamBv(dt4.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv3 = getSelfBv(dt4.Rows[0]["Userid"].ToString());
                if (dtleft4.Rows.Count > 0)
                {
                    LblLeftteamCount3.Text = dtleft4.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblLeftteamCount3.Text = "0";
                }

                DataTable dtright4 = getRightdata(dt4.Rows[0]["Userid"].ToString());
                if (dtright4.Rows.Count > 0)
                {
                    LblRightteamCount3.Text = dtright4.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblRightteamCount3.Text = "0";
                }
                int dcmatch4 = 0;
                //dcmatch4 = Convert.ToInt32(LblLeftteamCount3.Text) + Convert.ToInt32(LblRightteamCount3.Text);
                LblSelfCount3.Text = dcmatch4.ToString();
                LblLeftTeamjoining3.Text = DtLeftJoining3.Rows[0][0].ToString();
                LblLeftTeamRepurchase3.Text = DtLeftRepurchase3.Rows[0][0].ToString();
                LblRightTeamjoining3.Text = DtRightJoining3.Rows[0][0].ToString();
                LblRightTeamRepurchase3.Text = DtRightRepurchase3.Rows[0][0].ToString();
                LblSelfJoiningBv3.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv3.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv3.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv3.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv3.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv3.Rows[0]["RightRepurchaseBV"].ToString()));
            }
            else
            {
				if (lbluserid2.Text != "")
                {
                    string h = string.Format("<a href='https://loveragone.com/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid2.Text, "1", "img/default1.png", "height:100px;");
                    ltuser4.Text = h;
                }
                else
                {
                    ltuser4.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
                }
               // ltuser4.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }


            //================ Fourth Child============


            //================ Fifth Child============

            DataTable dt5 = new DataTable();
            dt5 = loadchild(lbluserid2.Text, "2");
            if (dt5.Rows.Count > 0)
            {
                lbluserid5.Text = dt5.Rows[0]["Userid"].ToString();
                lblusername5.Text = dt5.Rows[0]["username"].ToString();
                ltuser5.Text = @"<a href='test.aspx?SuperId=" + dt5.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip'  ><img src=""" + fetchimage(dt5.Rows[0]["gender"].ToString(), dt5.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";
                LblUserID28.Text = dt5.Rows[0]["Userid"].ToString();
                LblUserName28.Text = dt5.Rows[0]["username"].ToString();
                LblSponserId4.Text = dt5.Rows[0]["SponserId"].ToString();
                LblSponserName4.Text = dt5.Rows[0]["SponserName"].ToString();
                LblDOB4.Text = dt5.Rows[0]["DOJ"].ToString();
                LblStatus4.Text = dt5.Rows[0]["Status"].ToString();
              //  LblMobileno4.Text = dt5.Rows[0]["Mobile"].ToString();

                DataTable dtleft5 = getleftdata(dt5.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining4 = getTeamBv(dt5.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase4 = getTeamBv(dt5.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining4 = getTeamBv(dt5.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase4 = getTeamBv(dt5.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv4 = getSelfBv(dt5.Rows[0]["Userid"].ToString());
                if (dtleft5.Rows.Count > 0)
                {
                    LblLeftteamCount4.Text = dtleft5.Rows[0]["totaluser"].ToString();

                }
                else
                {
                    LblLeftteamCount4.Text = "0";

                }

                DataTable dtright5 = getRightdata(dt5.Rows[0]["Userid"].ToString());
                if (dtright5.Rows.Count > 0)
                {
                    LblRightteamCount4.Text = dtright5.Rows[0]["totaluser"].ToString();

                }
                else
                {
                    LblRightteamCount4.Text = "0";

                }
                int dcmatch5 = 0;
               // dcmatch5 = Convert.ToInt32(LblLeftteamCount4.Text) + Convert.ToInt32(LblRightteamCount4.Text);
                LblSelfCount4.Text = dcmatch5.ToString();
                LblLeftTeamjoining4.Text = DtLeftJoining4.Rows[0][0].ToString();
                LblLeftTeamRepurchase4.Text = DtLeftRepurchase4.Rows[0][0].ToString();
                LblRightTeamjoining4.Text = DtRightJoining4.Rows[0][0].ToString();
                LblRightTeamRepurchase4.Text = DtRightRepurchase4.Rows[0][0].ToString();
                LblSelfJoiningBv4.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv4.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv4.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv4.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv4.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv4.Rows[0]["RightRepurchaseBV"].ToString()));
            }
            else
            {
				if (lbluserid2.Text != "")
                {
                    string h = string.Format("<a href='https://loveragone.com/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid2.Text, "2", "img/default1.png", "height:100px;");
                    ltuser5.Text = h;
                }
                else
                {
                    ltuser5.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
                }
               // ltuser5.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }


            //================ Fifth Child============


            //================ Sixth Child============

            DataTable dt6 = new DataTable();
            dt6 = loadchild(lbluserid3.Text, "1");
            if (dt6.Rows.Count > 0)
            {
                lbluserid6.Text = dt6.Rows[0]["Userid"].ToString();
                lblusername6.Text = dt6.Rows[0]["username"].ToString();
                ltuser6.Text = @"<a href='test.aspx?SuperId=" + dt6.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt6.Rows[0]["gender"].ToString(), dt6.Rows[0]["Status"].ToString()) + @""" style=""height:100px;"" /></a>";

                LblUserID29.Text = dt6.Rows[0]["Userid"].ToString();
                LblUserName29.Text = dt6.Rows[0]["username"].ToString();
                LblSponserId5.Text = dt6.Rows[0]["SponserId"].ToString();
                LblSponserName5.Text = dt6.Rows[0]["SponserName"].ToString();
                LblDOB5.Text = dt6.Rows[0]["DOJ"].ToString();
                LblStatus5.Text = dt6.Rows[0]["Status"].ToString();
              //  LblMobileno5.Text = dt6.Rows[0]["Mobile"].ToString();

                DataTable dtleft6 = getleftdata(dt6.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining5 = getTeamBv(dt6.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase5 = getTeamBv(dt6.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining5 = getTeamBv(dt6.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase5 = getTeamBv(dt6.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv5 = getSelfBv(dt6.Rows[0]["Userid"].ToString());
                if (dtleft6.Rows.Count > 0)
                {
                    LblLeftteamCount5.Text = dtleft6.Rows[0]["totaluser"].ToString();

                }
                else
                {
                    LblLeftteamCount5.Text = "0";

                }

                DataTable dtright6 = getRightdata(dt6.Rows[0]["Userid"].ToString());
                if (dtright6.Rows.Count > 0)
                {
                    LblRightteamCount5.Text = dtright6.Rows[0]["totaluser"].ToString();

                }
                else
                {
                    LblRightteamCount5.Text = "0";

                }
                int dcmatch6 = 0;
                //dcmatch6 = Convert.ToInt32(LblLeftteamCount5.Text) + Convert.ToInt32(LblRightteamCount5.Text);
                LblSelfCount5.Text = dcmatch6.ToString();
                LblLeftTeamjoining5.Text = DtLeftJoining5.Rows[0][0].ToString();
                LblLeftTeamRepurchase5.Text = DtLeftRepurchase5.Rows[0][0].ToString();
                LblRightTeamjoining5.Text = DtRightJoining5.Rows[0][0].ToString();
                LblRightTeamRepurchase5.Text = DtRightRepurchase5.Rows[0][0].ToString();
                LblSelfJoiningBv5.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv5.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv5.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv5.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv5.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv5.Rows[0]["RightRepurchaseBV"].ToString()));
            }
            else
            {
				if (lbluserid3.Text != "")
                {
                    string h = string.Format("<a href='https://loveragone.com/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid3.Text, "1", "img/default1.png", "height:100px;");
                    ltuser6.Text = h;
                }
                else
                {
                    ltuser6.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
                }
               // ltuser6.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }


            //================ Sixth Child============


            //================ Seventh Child============

            DataTable dt7 = new DataTable();
            dt7 = loadchild(lbluserid3.Text, "2");
            if (dt7.Rows.Count > 0)
            {
                lbluserid7.Text = dt7.Rows[0]["Userid"].ToString();
                lblusername7.Text = dt7.Rows[0]["username"].ToString();
                ltuser7.Text = @"<a href='test.aspx?SuperId=" + dt7.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt7.Rows[0]["gender"].ToString(), dt7.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                LblUserID30.Text = dt7.Rows[0]["Userid"].ToString();
                LblUserName30.Text = dt7.Rows[0]["username"].ToString();
                LblSponserId6.Text = dt7.Rows[0]["SponserId"].ToString();
                LblSponserName6.Text = dt7.Rows[0]["SponserName"].ToString();
                LblDOB6.Text = dt7.Rows[0]["DOJ"].ToString();
                LblStatus6.Text = dt7.Rows[0]["Status"].ToString();
               // LblMobileno6.Text = dt7.Rows[0]["Mobile"].ToString();

                DataTable dtleft7 = getleftdata(dt7.Rows[0]["Userid"].ToString());
                DataTable DtLeftJoining6 = getTeamBv(dt7.Rows[0]["Userid"].ToString(), "1", "J");
                DataTable DtLeftRepurchase6 = getTeamBv(dt7.Rows[0]["Userid"].ToString(), "1", "R");
                DataTable DtRightJoining6 = getTeamBv(dt7.Rows[0]["Userid"].ToString(), "2", "J");
                DataTable DtRightRepurchase6 = getTeamBv(dt7.Rows[0]["Userid"].ToString(), "2", "R");
                DataTable DtSElfBv6 = getSelfBv(dt7.Rows[0]["Userid"].ToString());
                if (dtleft7.Rows.Count > 0)
                {
                    LblLeftteamCount6.Text = dtleft7.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblLeftteamCount6.Text = "0";
                }

                DataTable dtright7 = getRightdata(dt7.Rows[0]["Userid"].ToString());
                if (dtright7.Rows.Count > 0)
                {
                    LblRightteamCount6.Text = dtright7.Rows[0]["totaluser"].ToString();
                }
                else
                {
                    LblRightteamCount6.Text = "0";
                }
                int dcmatch7 = 0;
                //dcmatch7 = Convert.ToInt32(LblLeftteamCount6.Text) + Convert.ToInt32(LblRightteamCount6.Text);
                LblSelfCount6.Text = dcmatch7.ToString();
                LblLeftTeamjoining6.Text = DtLeftJoining6.Rows[0][0].ToString();
                LblLeftTeamRepurchase6.Text = DtLeftRepurchase6.Rows[0][0].ToString();
                LblRightTeamjoining6.Text = DtRightJoining6.Rows[0][0].ToString();
                LblRightTeamRepurchase6.Text = DtRightRepurchase6.Rows[0][0].ToString();
                LblSelfJoiningBv6.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv6.Rows[0]["LeftJoiningBV"].ToString()) + Convert.ToDecimal(DtSElfBv6.Rows[0]["RightJoiningBV"].ToString()));
                LblSelfRepurchaseBv6.Text = Convert.ToString(Convert.ToDecimal(DtSElfBv6.Rows[0]["LeftRepurchaseBV"].ToString()) + Convert.ToDecimal(DtSElfBv6.Rows[0]["RightRepurchaseBV"].ToString()));
            }
            else
            {
				if (lbluserid3.Text != "")
                {
                    string h = string.Format("<a href='https://loveragone.com/register.aspx?UserId={0}&PUserId={1}&EPinNo={2}' target='_blank'><img src={3} style={4}/></a></a>", Session["userid"].ToString(), lbluserid3.Text, "2", "img/default1.png", "height:70px;");
                    ltuser7.Text = h;
                }
                else
                {
                    ltuser7.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
                }
               // ltuser7.Text = @"<img src=""img/default1.png"" style=""height:70px;"" />";
            }


            //================ Seventh Child============

        }
        else
        {
            //divdata.Visible = false;
            Message.Show("Invalid User Id...!!!");
        }

    }

    DataTable loadchild(string str_parentid, string str_position)
    {
        objUser.ParentUserId = str_parentid;
        objUser.StandingPosition = str_position;
        DataTable dt = new DataTable();
        dt = objUser.getUserChild(objUser);
        return dt;
    }
}
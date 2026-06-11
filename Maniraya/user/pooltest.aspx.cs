using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class pooltest : System.Web.UI.Page
{
    clsUser objUser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
       loadbinary(Request.QueryString["SuperId"].ToString());
    }

    DataTable getfulldata(string str_id)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = getfulldownline(objUser.UserId);
        return dt;
    }


    DataTable getRightdata(string str_id)
    {
        DataTable dt = new DataTable();
        objUser.UserId = str_id;
        dt = objUser.getRightDataPlanWise2(objUser);
        return dt;
    }
    public string fetchimage(string Gender, string Status)
    {
        string imagepath = "";
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
        if (imagepath == "")
        {
            imagepath = "img/Active.png";
        }
        return imagepath;
    }
    public DataTable getUserName(string Poolid)
    {
        string str_query = "SELECT  P.userpoolid userid ,U.userid username,P.parentuserpoolid SponserId,'MALE' gender ,'ACTIVE' Status  FROM  PoolOneTb P join Userpoolonetb U ON P.Userpoolid=U.poolid WHERE P.Userpoolid = '" + Poolid + "' ";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }   
    public DataTable getfulldownline(string UserId)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "PoolDownline";
            SqlParameter[] parameter = {              
                    new SqlParameter("@Poolid",UserId), 
                   
                 
                  
                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
    void loadbinary(string str_superid)
    {
        objUser.UserId = str_superid;

        DataTable dt = new DataTable();
        dt = getUserName(objUser.UserId);
        if (dt.Rows.Count > 0)
        {
            //divdata.Visible = true;
            lbluserid1.Text = dt.Rows[0]["Userid"].ToString();
            lblusername1.Text = dt.Rows[0]["username"].ToString();
            ltuser1.Text = @"<a href='pooltest.aspx?SuperId=" + dt.Rows[0]["Userid"].ToString() + @"'    class='gridViewToolTip' ><img src=""" + fetchimage(dt.Rows[0]["gender"].ToString(), dt.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

            DataTable dtleft = getfulldata(dt.Rows[0]["Userid"].ToString());
            if (dtleft.Rows.Count > 0)
            {
                lblleftid1.Text = dtleft.Rows[0]["Leftcount"].ToString();
                lblrightid1.Text = dtleft.Rows[0]["RightCount"].ToString();
            }
            else
            {
                lblleftid1.Text = "0";
                lblrightid1.Text = "0";
            }
         
            int dcmatch1 = 0;
            if (Convert.ToInt32(lblleftid1.Text) > Convert.ToInt32(lblrightid1.Text))
            {
                dcmatch1 = Convert.ToInt32(lblrightid1.Text);
            }
            else if (Convert.ToInt32(lblleftid1.Text) < Convert.ToInt32(lblrightid1.Text))
            {
                dcmatch1 = Convert.ToInt32(lblleftid1.Text);
            }
            else
            {
                dcmatch1 = Convert.ToInt32(lblleftid1.Text) - 1;
                if (dcmatch1 < 0)
                {
                    dcmatch1 = 0;
                }
            }
            lbltotalid1.Text = dcmatch1.ToString();
            //================ Second Child============
            DataTable dt2 = new DataTable();
            dt2 = loadchild(lbluserid1.Text,"1");
            if (dt2.Rows.Count > 0)
            {
                lbluserid2.Text = dt2.Rows[0]["Userid"].ToString();
                lblusername2.Text = dt2.Rows[0]["username"].ToString();
                ltuser2.Text = @"<a href='pooltest.aspx?SuperId=" + dt2.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt2.Rows[0]["gender"].ToString(), dt2.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                DataTable dtleft2 = getfulldata(dt2.Rows[0]["Userid"].ToString());
                if (dtleft2.Rows.Count > 0)
                {
                    lblleftid2.Text = dtleft2.Rows[0]["Leftcount"].ToString();
                    lblrightid2.Text = dtleft2.Rows[0]["RightCount"].ToString();
                }
                else
                {
                    lblleftid2.Text = "0";
                    lblrightid2.Text = "0";
                }

               
                int dcmatch2 = 0;
                if (Convert.ToInt32(lblleftid2.Text) > Convert.ToInt32(lblrightid2.Text))
                {
                    dcmatch2 = Convert.ToInt32(lblrightid2.Text);
                }
                else if (Convert.ToInt32(lblleftid2.Text) < Convert.ToInt32(lblrightid2.Text))
                {
                    dcmatch2 = Convert.ToInt32(lblleftid2.Text);
                }
                else
                {
                    dcmatch2 = Convert.ToInt32(lblrightid2.Text)-1;
                    if (dcmatch2 < 0)
                    {
                        dcmatch2 = 0;
                    }
                }

                lbltotalid2.Text = dcmatch2.ToString();
            }
            else
            {
                ltuser2.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
            }
            //================ Second Child============


            //================ Third Child============

            DataTable dt3 = new DataTable();
            dt3 = loadchild(lbluserid1.Text, "2");
            if (dt3.Rows.Count > 0)
            {
                lbluserid3.Text = dt3.Rows[0]["Userid"].ToString();
                lblusername3.Text = dt3.Rows[0]["username"].ToString();
                ltuser3.Text = @"<a href='pooltest.aspx?SuperId=" + dt3.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt3.Rows[0]["gender"].ToString(), dt3.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                DataTable dtleft3 = getfulldata(dt3.Rows[0]["Userid"].ToString());
                if (dtleft3.Rows.Count > 0)
                {

                    lblleftid3.Text = dtleft3.Rows[0]["Leftcount"].ToString();
                    lblrightid3.Text = dtleft3.Rows[0]["RightCount"].ToString();


                }
                else
                {
                    lblleftid3.Text = "0";
                    lblrightid3.Text = "0";
                }

              
                int dcmatch3 = 0;
                if (Convert.ToInt32(lblleftid3.Text) > Convert.ToInt32(lblrightid3.Text))
                {
                    dcmatch3 = Convert.ToInt32(lblrightid3.Text);
                }
                else if (Convert.ToInt32(lblleftid3.Text) < Convert.ToInt32(lblrightid3.Text))
                {
                    dcmatch3 = Convert.ToInt32(lblleftid3.Text);
                }
                else
                {
                    dcmatch3 = Convert.ToInt32(lblleftid3.Text) - 1;
                    if (dcmatch3 < 0)
                    {
                        dcmatch3 = 0;
                    }
                }
                lbltotalid3.Text = dcmatch3.ToString();

            }
            else
            {
                ltuser3.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
            }


            //================ Third Child============

            //================ Fourth Child============

            DataTable dt4 = new DataTable();
            dt4 = loadchild(lbluserid2.Text,"1");
            if (dt4.Rows.Count > 0)
            {
                lbluserid4.Text = dt4.Rows[0]["Userid"].ToString();
                lblusername4.Text = dt4.Rows[0]["username"].ToString();
                ltuser4.Text = @"<a href='pooltest.aspx?SuperId=" + dt4.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip'  ><img src=""" + fetchimage(dt4.Rows[0]["gender"].ToString(), dt4.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                DataTable dtleft4 = getfulldata(dt4.Rows[0]["Userid"].ToString());
                if (dtleft4.Rows.Count > 0)
                {
                    lblleftid4.Text = dtleft4.Rows[0]["Leftcount"].ToString();
                    lblrightid4.Text = dtleft4.Rows[0]["RightCount"].ToString();
                }
                else
                {
                    lblleftid4.Text = "0";
                    lblrightid4.Text = "0";
                }

              
                int dcmatch4 = 0;
                if (Convert.ToInt32(lblleftid4.Text) > Convert.ToInt32(lblrightid4.Text))
                {
                    dcmatch4 = Convert.ToInt32(lblrightid4.Text) ;
                }
                else if (Convert.ToInt32(lblleftid4.Text) < Convert.ToInt32(lblrightid4.Text))
                {
                    dcmatch4 = Convert.ToInt32(lblleftid4.Text);
                }
                else
                {
                    dcmatch4 = Convert.ToInt32(lblleftid4.Text) - 1;
                    if (dcmatch4 < 0)
                    {
                        dcmatch4 = 0;
                    }
                }
                lbltotalid4.Text = dcmatch4.ToString();
            }
            else
            {
                ltuser4.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
            }


            //================ Fourth Child============


            //================ Fifth Child============

            DataTable dt5 = new DataTable();
            dt5 = loadchild(lbluserid2.Text, "2");
            if (dt5.Rows.Count > 0)
            {
                lbluserid5.Text = dt5.Rows[0]["Userid"].ToString();
                lblusername5.Text = dt5.Rows[0]["username"].ToString();
                ltuser5.Text = @"<a href='pooltest.aspx?SuperId=" + dt5.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip'  ><img src=""" + fetchimage(dt5.Rows[0]["gender"].ToString(), dt5.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                DataTable dtleft5 = getfulldata(dt5.Rows[0]["Userid"].ToString());
                if (dtleft5.Rows.Count > 0)
                {
                    lblleftid5.Text = dtleft5.Rows[0]["Leftcount"].ToString();
                    lblrightid5.Text = dtleft5.Rows[0]["RightCount"].ToString();

                }
                else
                {
                    lblleftid5.Text = "0";
                    lblrightid5.Text = "0";

                }

             
                int dcmatch5 = 0;
                if (Convert.ToInt32(lblleftid5.Text) > Convert.ToInt32(lblrightid5.Text))
                {
                    dcmatch5 = Convert.ToInt32(lblrightid5.Text);
                }
                else if (Convert.ToInt32(lblleftid5.Text) < Convert.ToInt32(lblrightid5.Text))
                {
                    dcmatch5 = Convert.ToInt32(lblleftid5.Text);
                }
                else
                {
                    dcmatch5 = Convert.ToInt32(lblleftid5.Text) - 1;
                    if (dcmatch5 < 0)
                    {
                        dcmatch5 = 0;
                    }
                }
                lbltotalid5.Text = dcmatch5.ToString();
            }
            else
            {
                ltuser5.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
            }


            //================ Fifth Child============


            //================ Sixth Child============

            DataTable dt6 = new DataTable();
            dt6 = loadchild(lbluserid3.Text, "1");
            if (dt6.Rows.Count > 0)
            {
                lbluserid6.Text = dt6.Rows[0]["Userid"].ToString();
                lblusername6.Text = dt6.Rows[0]["username"].ToString();
                ltuser6.Text = @"<a href='pooltest.aspx?SuperId=" + dt6.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt6.Rows[0]["gender"].ToString(), dt6.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";


                DataTable dtleft6 = getfulldata(dt6.Rows[0]["Userid"].ToString());
                if (dtleft6.Rows.Count > 0)
                {
                    lblleftid6.Text = dtleft6.Rows[0]["Leftcount"].ToString();
                    lblrightid6.Text = dtleft6.Rows[0]["RightCount"].ToString();

                }
                else
                {
                    lblleftid6.Text = "0";
                    lblrightid6.Text = "0";

                }

               
                int dcmatch6 = 0;
                if (Convert.ToInt32(lblleftid6.Text) > Convert.ToInt32(lblrightid6.Text))
                {
                    dcmatch6 = Convert.ToInt32(lblrightid6.Text);
                }
                else if (Convert.ToInt32(lblleftid6.Text) < Convert.ToInt32(lblrightid6.Text))
                {
                    dcmatch6 = Convert.ToInt32(lblleftid6.Text);
                }
                else
                {
                    dcmatch6 = Convert.ToInt32(lblleftid6.Text) - 1;
                    if (dcmatch6 < 0)
                    {
                        dcmatch6 = 0;
                    }
                }
                lbltotalid6.Text = dcmatch6.ToString();
            }
            else
            {
                ltuser6.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
            }


            //================ Sixth Child============


            //================ Seventh Child============

            DataTable dt7 = new DataTable();
            dt7 = loadchild(lbluserid3.Text, "2");
            if (dt7.Rows.Count > 0)
            {
                lbluserid7.Text = dt7.Rows[0]["Userid"].ToString();
                lblusername7.Text = dt7.Rows[0]["username"].ToString();
                ltuser7.Text = @"<a href='pooltest.aspx?SuperId=" + dt7.Rows[0]["Userid"].ToString() + @"'  class='gridViewToolTip' ><img src=""" + fetchimage(dt7.Rows[0]["gender"].ToString(), dt7.Rows[0]["Status"].ToString()) + @""" style=""height:70px;"" /></a>";

                DataTable dtleft7 = getfulldata(dt7.Rows[0]["Userid"].ToString());
                if (dtleft7.Rows.Count > 0)
                {
                    lblleftid7.Text = dtleft7.Rows[0]["Leftcount"].ToString();
                    lblrightid7.Text = dtleft7.Rows[0]["RightCount"].ToString();
                }
                else
                {
                    lblleftid7.Text = "0";
                    lblrightid7.Text = "0";
                }

              
             
                int dcmatch7 = 0;
                if (Convert.ToInt32(lblleftid7.Text) > Convert.ToInt32(lblrightid7.Text))
                {
                    dcmatch7 = Convert.ToInt32(lblrightid7.Text) ;
                }
                else if (Convert.ToInt32(lblleftid7.Text) < Convert.ToInt32(lblrightid7.Text))
                {
                    dcmatch7 = Convert.ToInt32(lblleftid7.Text);
                }
                else
                {
                    dcmatch7 = Convert.ToInt32(lblleftid7.Text) - 1;
                    if (dcmatch7 < 0)
                    {
                        dcmatch7 = 0;
                    }
                }
                lbltotalid7.Text = dcmatch7.ToString();
            }
            else
            {
                ltuser7.Text = @"<img src=""img/default.png"" style=""height:70px;"" />";
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
        dt = getchildfulldownline(str_parentid, str_position);
        return dt;
    }
    public DataTable getchildfulldownline(string UserId,string position)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "PoolChildid";
            SqlParameter[] parameter = {              
                    new SqlParameter("@Poolid",UserId), 
                      new SqlParameter("@Standingposition",position), 
                   
                 
                  
                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
   
}
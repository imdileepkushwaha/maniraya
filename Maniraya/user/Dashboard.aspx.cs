using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using DataTier;
using BusinessLogicTier;
using System.Web.UI.HtmlControls;
using System.Web.Services;

public partial class user_Dashboard : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsNews objnews = new clsNews();
    clsaward objAward = new clsaward();
    ClsVacation objvac = new ClsVacation();
    clsClosing objCL = new clsClosing();
    Data ObjData = new Data();
    public string LoginId = "";
    public string WhiteLabelId = "";
    clsRecharge objrecharge = new clsRecharge();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                TxtLeftLinkLink.Attributes.Add("readonly", "readonly");
                TxtRightLink.Attributes.Add("readonly", "readonly");
                loadnotification();
                laoddata();
                string url = clsUtility.ProjectWebsite;
                string userId = Session["userid"].ToString();

                TxtLeftLinkLink.Text = url + "signup.aspx?UserId=" + userId + "&standingposition=1";
                TxtRightLink.Text = url + "signup.aspx?UserId=" + userId + "&standingposition=2";
                //loadaward();
                //loadvacation();
                loadnews();
                Getsalary();
                // loadTodayPerformance();
                // loadweeklyPerformance();
                // loadmonthlyPerformance();
                // loadtotalPerformance();
                loadTotalSV();
                loadPerformance();
                loadTotalpayout();
                loadBuissness();
                UpdateBal(Session["userid"].ToString());
                //HtmlMeta tag = new HtmlMeta();
                //tag.Name = "title";
                //tag.Content = "Affiliate Link";
                //Page.Header.Controls.Add(tag);

                HtmlMeta tag1 = new HtmlMeta();
                tag1.Attributes.Add("property", "og:description");
                tag1.Content = TxtLeftLinkLink.Text;
                Page.Header.Controls.Add(tag1);
                GetPrimeStatus();
                filldashboard();
                loadwallet();
                loadPV();
                loadawardlist();
                GetAllIncome();
                LoadIncentiveCard();
                LoadPrizes();
                LoadTopDirectRanking();
                LoadDirectRank();


            }


            //(Starts)Pasted from Recharge.aspx



            //(Ends)
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    void LoadDirectRank()
    {
        try
        {
            string userId = Convert.ToString(Session["userid"]);
            DirectRankHelper.RankProgress progress = DirectRankHelper.GetProgressForUser(userId);

            if (lblWelcomeRank != null)
            {
                lblWelcomeRank.Text = progress.CurrentRank;
            }

            if (LblActiveDirect != null)
            {
                LblActiveDirect.Text = progress.ActiveDirectCount.ToString();
            }

            if (lblDirectRankName != null)
            {
                lblDirectRankName.Text = progress.CurrentRank;
            }
            if (lblDirectRankActiveCount != null)
            {
                lblDirectRankActiveCount.Text = progress.ActiveDirectCount.ToString();
            }
            if (lblDirectRankNext != null)
            {
                lblDirectRankNext.Text = progress.IsMaxRank
                    ? "Highest rank achieved"
                    : progress.NextRank + " — " + progress.RemainingForNext + " more needed";
            }
            if (pnlDirectRank != null)
            {
                pnlDirectRank.Visible = true;
            }
        }
        catch
        {
            if (lblWelcomeRank != null)
            {
                lblWelcomeRank.Text = "Member";
            }
        }
    }

    void LoadPrizes()
    {
        try
        {
            DataTable dt = PrizeHelper.GetAllWinners(200);
            pnlPrizes.Visible = true;
            if (dt != null && dt.Rows.Count > 0)
            {
                rptPrizes.DataSource = dt;
                rptPrizes.DataBind();
                lblPrizeCount.Text = dt.Rows.Count.ToString();
                pnlPrizeGrid.Visible = true;
                pnlPrizeEmpty.Visible = false;
            }
            else
            {
                lblPrizeCount.Text = "0";
                pnlPrizeGrid.Visible = false;
                pnlPrizeEmpty.Visible = true;
            }
        }
        catch
        {
            pnlPrizes.Visible = true;
            pnlPrizeGrid.Visible = false;
            pnlPrizeEmpty.Visible = true;
        }
    }

    void LoadTopDirectRanking()
    {
        try
        {
            pnlTopDirectRanking.Visible = true;
            DataTable dt = GetTopDirectRanking(10);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdTopDirectRanking.DataSource = dt;
                grdTopDirectRanking.DataBind();
                pnlTopDirectGrid.Visible = true;
                pnlTopDirectEmpty.Visible = false;
            }
            else
            {
                pnlTopDirectGrid.Visible = false;
                pnlTopDirectEmpty.Visible = true;
            }

            BindTopDirectMyRank();
        }
        catch
        {
            pnlTopDirectRanking.Visible = true;
            pnlTopDirectGrid.Visible = false;
            pnlTopDirectEmpty.Visible = true;
            pnlTopDirectMyRank.Visible = false;
        }
    }

    DataTable GetTopDirectRanking(int topCount)
    {
        string sql = @"
;WITH Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        ISNULL(s.UserName, '') AS username,
        COUNT(d.UserId) AS DirectCount
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(d.SavingStatus, 0) = 1
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId)), s.UserName
)
SELECT TOP (" + topCount + @")
    ROW_NUMBER() OVER (ORDER BY DirectCount DESC, userid ASC) AS RankNo,
    userid,
    username,
    DirectCount
FROM Ranked
ORDER BY DirectCount DESC, userid ASC";

        DataTable dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(sql);
        }
        catch
        {
            dt = new DataTable();
        }
        finally
        {
            ObjData.EndConnection();
        }

        return AppendDirectRankColumn(dt ?? new DataTable());
    }

    DataTable AppendDirectRankColumn(DataTable dt)
    {
        if (dt == null)
        {
            return new DataTable();
        }

        if (!dt.Columns.Contains("DirectRank"))
        {
            dt.Columns.Add("DirectRank", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            int directCount = 0;
            if (row["DirectCount"] != null && row["DirectCount"] != DBNull.Value)
            {
                int.TryParse(Convert.ToString(row["DirectCount"]), out directCount);
            }
            row["DirectRank"] = DirectRankHelper.GetRank(directCount);
        }

        return dt;
    }

    void BindTopDirectMyRank()
    {
        pnlTopDirectMyRank.Visible = false;
        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return;
        }

        string sql = @"
;WITH Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        COUNT(d.UserId) AS DirectCount,
        ROW_NUMBER() OVER (ORDER BY COUNT(d.UserId) DESC, LTRIM(RTRIM(s.UserId)) ASC) AS RankNo
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(d.SavingStatus, 0) = 1
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId))
)
SELECT RankNo, DirectCount
FROM Ranked
WHERE userid = '" + SqlEscape(currentUserId) + "'";

        try
        {
            ObjData.StartConnection();
            try
            {
                DataTable dt = ObjData.RunDataTable(sql);
                pnlTopDirectMyRank.Visible = true;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int myDirectCount = 0;
                    int.TryParse(Convert.ToString(dt.Rows[0]["DirectCount"]), out myDirectCount);
                    string myDirectRank = DirectRankHelper.GetRank(myDirectCount);
                    litTopDirectMyRank.Text = string.Format(
                        "Your overall rank: <strong>#{0}</strong> · Direct Rank: <strong>{1}</strong> · <strong>{2}</strong> active direct(s).",
                        Convert.ToString(dt.Rows[0]["RankNo"]),
                        myDirectRank,
                        Convert.ToString(dt.Rows[0]["DirectCount"]));
                }
                else
                {
                    litTopDirectMyRank.Text = "You are not in the ranking yet (0 active directs). Direct Rank: <strong>Member</strong>.";
                }
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            pnlTopDirectMyRank.Visible = false;
        }
    }

    protected void grdTopDirectRanking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblRank = (Label)e.Row.FindControl("lblRank");
        if (lblRank != null)
        {
            int rankNo;
            if (int.TryParse(lblRank.Text, out rankNo))
            {
                if (rankNo == 1)
                {
                    lblRank.CssClass = "dash-rank-badge is-gold";
                }
                else if (rankNo == 2)
                {
                    lblRank.CssClass = "dash-rank-badge is-silver";
                }
                else if (rankNo == 3)
                {
                    lblRank.CssClass = "dash-rank-badge is-bronze";
                }
            }
        }

        Label lblDirectRankTitle = (Label)e.Row.FindControl("lblDirectRankTitle");
        if (lblDirectRankTitle != null)
        {
            string rankName = (lblDirectRankTitle.Text ?? string.Empty).Trim();
            string css = "dash-direct-rank-pill";
            if (string.Equals(rankName, DirectRankHelper.RankDiamond, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-diamond";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankGold, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-gold";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankSilver, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-silver";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankBronze, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-bronze";
            }
            else if (string.Equals(rankName, DirectRankHelper.RankDistributor, StringComparison.OrdinalIgnoreCase))
            {
                css += " is-distributor";
            }
            else
            {
                css += " is-member";
            }
            lblDirectRankTitle.CssClass = css;
        }

        string currentUserId = Convert.ToString(Session["userid"]).Trim();
        DataRowView row = e.Row.DataItem as DataRowView;
        if (row != null && string.Equals(Convert.ToString(row["userid"]).Trim(), currentUserId, StringComparison.OrdinalIgnoreCase))
        {
            e.Row.CssClass = (e.Row.CssClass + " dash-rank-self-row").Trim();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    public string GetPrizeMonth(object value)
    {
        return PrizeHelper.FormatPrizeMonth(value);
    }

    public string GetWinnerName(object name, object userId)
    {
        string n = Convert.ToString(name).Trim();
        if (!string.IsNullOrEmpty(n))
        {
            return n;
        }

        return Convert.ToString(userId).Trim();
    }

    public string GetInitial(object name, object userId)
    {
        string source = GetWinnerName(name, userId);
        if (string.IsNullOrEmpty(source))
        {
            return "?";
        }

        return source.Substring(0, 1).ToUpper();
    }



    private void GetAllIncome()
    {
        DataSet ds = new DataSet();
        objuser.UserId = Session["userId"].ToString();
        ds = objuser.GetDirect(objuser);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // LblBinaryIncome.Text = ds.Tables[0].Rows[0][0].ToString();
            //LblRank.Text = ds.Tables[1].Rows[0][0].ToString();
            //  lblselfincome.Text = ds.Tables[2].Rows[0][0].ToString();
            //LblLevelIncome.Text = ds.Tables[3].Rows[0][0].ToString();

            // lblDirectincome.Text = ds.Tables[1].Rows[0][0].ToString();
            // lbldailyincome.Text = ds.Tables[4].Rows[0][0].ToString();
            //lbROIIncome.Text = ds.Tables[3].Rows[0][0].ToString();
            //LblReward.Text = ds.Tables[4].Rows[0][0].ToString();
            //LblRechargePoint.Text = ds.Tables[6].Rows[0][0].ToString();

            //if (ds.Tables[5].Rows.Count>0 && Convert.ToInt32(ds.Tables[5].Rows[0][0]) == 2100)
            //{
            //    Shopping.Visible = false;  
            //    Recharge.Visible = true; 
            //}
            //else if (ds.Tables[5].Rows.Count > 0 && Convert.ToInt32(ds.Tables[5].Rows[0][0]) == 1600)
            //{
            //    Recharge.Visible = false;
            //    Shopping.Visible = true;
            //}
            //else
            //{
            //    Recharge.Visible = false;
            //    Shopping.Visible = true;
            //}
        }
    }



    void loadwallet()
    {

        objaccount.UserId = Session["userId"].ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getUserWalletBalanceReport(objaccount);
        if (dt.Rows.Count > 0)
        {
            LblCredited.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet.Text = dt.Rows[0]["bal"].ToString();
        }
    }



    void loadTotalSV()
    {
        SetLabelDefault(lblleftjoiningsv);
        SetLabelDefault(lblrightjoiningsv);
        SetLabelDefault(lblleftjoiningcarrysv);
        SetLabelDefault(lblrightjoiningcarrysv);
        SetLabelDefault(lbltotalselfjoiningsv);
        SetLabelDefault(lblleftrepurchasesv);
        SetLabelDefault(lblRightrepurchasesv);
        SetLabelDefault(lblleftrepurchasecarrysv);
        SetLabelDefault(lblRightrepurchasecarrysv);
        SetLabelDefault(lbltotalselfRepurchasesv);
        SetLabelDefault(lblleftBonanzasv);
        SetLabelDefault(lblRightBonanzasv);

        try
        {
            DataTable dt = objuser.getTotalSV(Session["userid"].ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            DataRow row = dt.Rows[0];
            lblleftjoiningsv.Text = GetColumnValue(row, dt, "TotalLeftJoiningSV", "LeftJoiningSV", "LeftJoiningBV", "TotalLeftBV");
            lblrightjoiningsv.Text = GetColumnValue(row, dt, "TotalRightjoiningSV", "TotalRightJoiningSV", "RightJoiningSV", "RightJoiningBV", "TotalRightBV");
            lblleftjoiningcarrysv.Text = GetColumnValue(row, dt, "TotalLeftJoiningCarrySV", "LeftJoiningCarrySV", "CfLeftBv");
            lblrightjoiningcarrysv.Text = GetColumnValue(row, dt, "TotalRightjoiningCarrySV", "TotalRightJoiningCarrySV", "RightJoiningCarrySV", "CfRightBv");
            lbltotalselfjoiningsv.Text = GetColumnValue(row, dt, "TotalSelfjoiningSV", "TotalSelfJoiningSV", "SelfJoiningSV", "SelfBv");
            lblleftrepurchasesv.Text = GetColumnValue(row, dt, "TotalLeftRepurchaseSV", "LeftRepurchaseSV", "TotalLeftRepurchaseBv");
            lblRightrepurchasesv.Text = GetColumnValue(row, dt, "TotalRightRepurchaseSV", "RightRepurchaseSV", "TotalRightRepurchaseBv");
            lblleftrepurchasecarrysv.Text = GetColumnValue(row, dt, "TotalLeftRepurchasecarrySV", "LeftRepurchaseCarrySV");
            lblRightrepurchasecarrysv.Text = GetColumnValue(row, dt, "TotalRightRepurchasecarrySV", "TotalRightRepurchaseCarrySV", "RightRepurchaseCarrySV");
            lbltotalselfRepurchasesv.Text = GetColumnValue(row, dt, "TotalSelfRepurchaseSV", "SelfRepurchaseSV");
            lblleftBonanzasv.Text = GetColumnValue(row, dt, "BonanzaLeftBV", "LeftBonanzaBV");
            lblRightBonanzasv.Text = GetColumnValue(row, dt, "BonanzaRightBV", "RightBonanzaBV");
        }
        catch
        {
            // Keep default values when SV data is unavailable or schema differs.
        }
    }

    void SetLabelDefault(Label label)
    {
        if (label != null)
        {
            label.Text = "0";
        }
    }

    string GetColumnValue(DataRow row, DataTable dt, params string[] columnNames)
    {
        if (row == null || dt == null || columnNames == null)
        {
            return "0";
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                continue;
            }

            if (dt.Columns.Contains(columnName))
            {
                return Convert.ToString(row[columnName]);
            }

            foreach (DataColumn column in dt.Columns)
            {
                if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return Convert.ToString(row[column]);
                }
            }
        }

        return "0";
    }

    void loadTotalpayout()
    {
        DataTable dt = new DataTable();
        dt = objuser.getTotalincome(Session["userid"].ToString());
        //LblBinaryIncome.Text = dt.Rows[0]["Binaryincome"].ToString();
        Lblleftbv.Text = dt.Rows[0]["LeftBv"].ToString();
        lblgoldirector.Text = dt.Rows[0]["GoldDIrector1"].ToString();
        lblleadership.Text = dt.Rows[0]["leadershipincome1"].ToString();
      //  lblDIrectorIncome.Text = dt.Rows[0]["directorincome1"].ToString();
        lblselfincome.Text = dt.Rows[0]["selfincome"].ToString();
        Lblrightbv.Text = dt.Rows[0]["RightBv"].ToString();
        lblMatching.Text = dt.Rows[0]["Binaryincome"].ToString();
        // LblLevelIncome.Text = dt.Rows[0]["DailyLevelIncome"].ToString();
        lbldailyincome.Text = dt.Rows[0]["DailyLevel"].ToString();
        lblcurrentleftbv.Text = dt.Rows[0]["currentleftbv"].ToString();
        lblcurrrentightbv.Text = dt.Rows[0]["currentrightbv"].ToString();
        lblcurrentselfbv.Text = dt.Rows[0]["currentselfbv"].ToString();
        LblRleftbv.Text = dt.Rows[0]["RLeftBV"].ToString();
        LblRrightbv.Text = dt.Rows[0]["RRightBv"].ToString();
        LblRetailProfit.Text = dt.Rows[0]["RetailProfit"].ToString();
        //lblroyalty.Text= dt.Rows[0]["royality"].ToString();
        lblrank.Text = dt.Rows[0]["Rank"].ToString();
        lblPerformance.Text = dt.Rows[0]["TodayPerformance"].ToString();
        Lblleftcarrypv.Text = dt.Rows[0]["leftCarryPV"].ToString();
        Lblrightcarrypv.Text = dt.Rows[0]["RightCarryPV"].ToString();
        LblREpurchaseIncome.Text = dt.Rows[0]["Repurchaseincome"].ToString();
        lblDirectincome.Text = dt.Rows[0]["sponcering"].ToString();
        lblrankreward.Text = dt.Rows[0]["sponcering"].ToString();
        lblrank1.Text = dt.Rows[0]["Rank"].ToString();
        lblrank2.Text = dt.Rows[0]["categoryname"].ToString();
        //lblrfrl.Text = dt.Rows[0]["sponcering"].ToString();
        // LblTds.Text = dt.Rows[0]["TDS"].ToString();
    }

    void filldashboard()
    {
        objuser.UserId = Session["userid"].ToString();
       // DataTable LeftDt = objuser.getUserDownlineLeft(objuser);
    //    DataTable RightDt = objuser.getUserDownlineRight(objuser);
      //  LblTotalLeft.Text = LeftDt.Rows.Count.ToString();
      //  LblTotalright.Text = RightDt.Rows.Count.ToString();
      //  DataRow[] Sactiveusers = LeftDt.Select("Status='active'");
      //  DataRow[] Sdeactiveusers = RightDt.Select("Status='active'");
      //  DataRow[] SLdeactiveusers = LeftDt.Select("Status='deactive'");
      //  DataRow[] SRdeactiveusers = RightDt.Select("Status='deactive'");
      //  Lblactiveleft.Text = Sactiveusers.Length.ToString();
      //  LblActiveRight.Text = Sdeactiveusers.Length.ToString();
      //  LblInactiveleft.Text = SLdeactiveusers.Length.ToString();
      //  LblInActiveRight.Text = SRdeactiveusers.Length.ToString();
       // DataTable LeftDirectt = objuser.getUserleftDirect(objuser);
       // DataTable RightDirectt = objuser.getUserrightDirect(objuser);
       // LblLeftDirect.Text = LeftDirectt.Rows[0][0].ToString();
       // LblRightDirect.Text = RightDirectt.Rows[0][0].ToString();
        string Fromdate = string.Empty;
        string Todatedate = string.Empty;

        DataTable Dt = objCL.getdailyClosingReport(Fromdate, Todatedate, Session["UserId"].ToString());
        //lblleftbv.Text = Dt.Rows[0]["leftbv"].ToString();
        //lblrightbv.Text = Dt.Rows[0]["rightbv"].ToString();


        //  DataSet Ds = objuser.getTotalamount(objuser);
        //  LblBinaryIncome.Text = Ds.Tables[0].Rows[0][0].ToString();
        // LblDirectIncome.Text = Ds.Tables[1].Rows[0][0].ToString();
        //  LblSponserIncome.Text = Ds.Tables[2].Rows[0][0].ToString();
        // LblRoinIncome.Text = Ds.Tables[3].Rows[0][0].ToString();
        //lblTotalincome.Text = Convert.ToString(Convert.ToDecimal(LblBinaryIncome.Text)

    }

    public void Getsalary()
    {
        //objuser.UserId = Session["Userid"].ToString();
        //DataSet Dt = objuser.getUserdashboardnew(objuser);
        //if (Dt.Tables[0].Rows.Count > 0)
        //{
        //    Lblsalary.Text = Dt.Tables[1].Rows[0][0].ToString();
        //    LblBinaryIncome.Text = Dt.Tables[0].Rows[0][0].ToString();
        //    LblBinaryPoint.Text = Dt.Tables[2].Rows[0][0].ToString() + " Left : " + Dt.Tables[3].Rows[0][0].ToString() + " Right";
        //    LblsalaryPoint.Text = Dt.Tables[4].Rows[0][0].ToString() + " Left : " + Dt.Tables[4].Rows[0][1].ToString() + " Right";
        //    lblLeftBV.Text = Math.Round(Convert.ToDecimal(Dt.Tables[5].Rows[0][0].ToString()), 0).ToString() + " Left : " + Math.Round(Convert.ToDecimal(Dt.Tables[5].Rows[0][1].ToString()), 0).ToString() + " Right";
        //    lblBVvalue.Text = Math.Round(Convert.ToDecimal(Dt.Tables[5].Rows[0][0].ToString()) * 850, 0).ToString() + " : " + Math.Round(Convert.ToDecimal(Dt.Tables[5].Rows[0][1].ToString()) * 850, 0).ToString(); 
        //}
        //DataTable Dt = objuser.getUserDownlineDirect(Session["Userid"].ToString());
        //if (Dt.Rows.Count > 0)
        //{
        //    LblDirect.Text = Dt.Rows[0]["Direct"].ToString();
        //    LblActiveDirect.Text = Dt.Rows[0]["ActiveDirect"].ToString();
        //    LblDownline.Text = Dt.Rows[0]["Downline"].ToString();
        //    LblActiveDownline.Text = Dt.Rows[0]["ActiveDownline"].ToString();

        //    LblDirectIncome.Text = Dt.Rows[0]["DirectIncome"].ToString();
        //    Lbllevelincome.Text = Dt.Rows[0]["LevelIncome"].ToString();
        //    Lblsalary.Text = Dt.Rows[0]["ROIIncome"].ToString();
        //    LblBinaryPoint.Text = Dt.Rows[0]["BoosterIncome"].ToString();
        //    LblLevelNo.Text = Dt.Rows[0]["LevelNo"].ToString();
        //    LblBoostPFS.Text = Dt.Rows[0]["BoostStatus"].ToString();
        //}
        DataTable Dt = objuser.getUserDasboardproc(Session["Userid"].ToString());
        if (Dt.Rows.Count > 0)
        {
            LblDirect.Text = Dt.Rows[0]["TotalDirect"].ToString();
            LblActiveDirect.Text = Dt.Rows[0]["ActiveDirect"].ToString();
            LblDownline.Text = Dt.Rows[0]["TotalTeam"].ToString();
            LblActiveDownline.Text = Dt.Rows[0]["TotalActiveTeam"].ToString();

            LblPoolIncome.Text = Dt.Rows[0]["AutoPoolIncome"].ToString();
            //Lbllevelincome.Text = Dt.Rows[0]["LevelIncome"].ToString();
            LblCurrentpackage.Text = Dt.Rows[0]["Planname"].ToString();
            LblGroup.Text = Dt.Rows[0]["CurrentGroup"].ToString();
            Lblactivatedate2.Text = Dt.Rows[0]["Activatedate"].ToString();
            LBlGroupIncome.Text = Dt.Rows[0]["GroupIncome"].ToString();

            // LblROiIncome.Text = Dt.Rows[0]["ROIIncome"].ToString();
            LbllevelROiIncome.Text = Dt.Rows[0]["LevelRoiIncome"].ToString();
            lblpaydate.Text = Dt.Rows[0]["LuckyDate"].ToString();
            lblincome.Text = Dt.Rows[0]["CommissionPer"].ToString();
            //lblTotalincome.Text = Dt.Rows[0]["TotalIncome"].ToString();



            // LblGroup.Text = Dt.Rows[0]["CurrentGroup"].ToString();
            //  Lblactivatedate2.Text = Dt.Rows[0]["Activatedate"].ToString();
            //   LBlGroupIncome.Text = Dt.Rows[0]["GroupIncome"].ToString();
        }
    }
    public void GetPrimeStatus()
    {
        string status = objaccount.getPrimeMemberStatus(Session["userId"].ToString());
        if (status == "1")
        {
            spanprime.Visible = true;
        }
        else
        {
            spanprime.Visible = false;
        }
    }

    [WebMethod]
    public static int BecomePrimeMember()
    {
        int c = 0;

        clsAccount objaccount = new clsAccount();
        objaccount.UserId = HttpContext.Current.Session["userId"].ToString();
        objaccount.plananame = "P";
        objaccount.Amount = 1000;
        string res = objaccount.UserCashrequest(objaccount);
        if (res == "t")
        {
            c = 1;

        }
        else if (res == "f")
        {

            c = 2;
        }
        else if (res == "n")
        {

            c = 3;
        }
        return c;
    }



    void loadnews()
    {
        DataTable dt = new DataTable();
        dt = objnews.getRecentNews();
        ltnews.Text += "<span style='color:red;'> ";
        foreach (DataRow r in dt.Rows)
        {
            ltnews.Text += r["newsdetail"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        ltnews.Text += "</span>";
    }

    void TotalDownline()
    {

        objuser.UserId = Session["userId"].ToString();
        DataTable dt = new DataTable();
        dt = objuser.getUserDownlinePool(objuser);
        if (dt.Rows.Count > 0)
        {
            LblPooldownline.Text = dt.Rows[0]["Downline"].ToString();


        }
    }

    void loadnotification()
    {
        objuser.UserId = Session["userid"].ToString();
        DataTable dt = objuser.getUserDetail(objuser);
        if (dt == null || dt.Rows.Count == 0)
        {
            pnlnotification.Visible = false;
            return;
        }

        DataRow row = dt.Rows[0];
        if (Convert.ToString(row["AccountHolderName"]) == ""
            || Convert.ToString(row["AccountNo"]) == ""
            || Convert.ToString(row["IFSCCode"]) == ""
            || Convert.ToString(row["BankName"]) == ""
            || Convert.ToString(row["BankName"]) == "0"
            || Convert.ToString(row["BranchName"]) == ""
            || Convert.ToString(row["PanNumber"]) == "")
        {
            pnlnotification.Visible = true;
        }
        else
        {
            pnlnotification.Visible = false;
        }
    }
    void laoddata()
    {
        objuser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = getUserDetail(objuser);
        if (dt.Rows.Count > 0)
        {
            lbluserid.Text = dt.Rows[0]["userid"].ToString();
            lblWelcomeId.Text = dt.Rows[0]["userid"].ToString();
            lblReferralUserId.Text = dt.Rows[0]["userid"].ToString();
            lblusername.Text = dt.Rows[0]["username"].ToString();
            lblWelcomeName.Text = dt.Rows[0]["username"].ToString();
            LblSponserId.Text = dt.Rows[0]["sponserId"].ToString();
            LblParentId.Text = dt.Rows[0]["parentuserid"].ToString();
            ImgMyPhoto.ImageUrl = "../ProductImage/" + dt.Rows[0]["PhotoImage"].ToString();
            if (imgIncentivePhoto != null)
            {
                string photo = Convert.ToString(dt.Rows[0]["PhotoImage"]);
                imgIncentivePhoto.ImageUrl = string.IsNullOrWhiteSpace(photo) ? "img/default.png" : "../ProductImage/" + photo;
            }
            lbljoiningdate.Text = dt.Rows[0]["parentuserid"].ToString();
            LblParentName.Text = dt.Rows[0]["parentname"].ToString();
            LblSponserName.Text = dt.Rows[0]["sponsername"].ToString();
            lbljoiningdate.Text = dt.Rows[0]["regdate"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblmobile.Text = dt.Rows[0]["mobile"].ToString();
            lblemail.Text = dt.Rows[0]["email"].ToString();
            lblaccountholdername.Text = dt.Rows[0]["accountholdername"].ToString();
            lblaccountno.Text = dt.Rows[0]["accountno"].ToString();
            lblbank.Text = dt.Rows[0]["branchname"].ToString();
            lblifsc.Text = dt.Rows[0]["ifsccode"].ToString();
            lblpan.Text = dt.Rows[0]["pannumber"].ToString();
            Lblactivatedate.Text = dt.Rows[0]["activationdate"].ToString();
            lblstatus.Text = dt.Rows[0]["Savingstatus"].ToString();
            if (dt.Rows[0]["Savingstatus"].ToString() == "1")
            {
                lblstatus.Text = "Active ";
            }
            else
            {
                lblstatus.Text = "Deactive";
            }





        }

    }

    public DataTable getUserDetail(clsUser objUser)
    {
        string str_query = "SELECT ud.*,cm.stateid,sm.countryid,sm.statename,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,(select UserName from userdetail where UserId=ud.parentuserid) as parentname,convert(char,ud.activatedate,103) as activationdate,(select planamount from UserTopupTb where userid=ud.userid and type='A') planamount FROM userdetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + objUser.UserId + "' ";
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
    void loadaward()
    {
        DataTable dt = new DataTable();
        dt = objAward.getawardDetailfromdashboard();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    void loadvacation()
    {
        DataTable dt = new DataTable();
        dt = objvac.getvacationDetailfromdashboard();
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    void loadawardlist()
    {
        clsClosing objc = new clsClosing();
        DataTable dt = new DataTable();
        dt = objc.getAwardList(Session["userid"].ToString());
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label Id = e.Row.FindControl("lblid") as Label;
            Label lbltargetleft = e.Row.FindControl("lbltargetleft") as Label;
            Label lbltargetright = e.Row.FindControl("lbltargetright") as Label;
            Label lblCurrentLeftBv = e.Row.FindControl("lblCurrentLeftBv") as Label;
            Label lblCurrentRightBv = e.Row.FindControl("lblCurrentRightBv") as Label;
            Label lblrequiredLeftBv = e.Row.FindControl("lblrequiredLeftBv") as Label;
            Label lblrequiredRightBv = e.Row.FindControl("lblrequiredRightBv") as Label;
            Label lblstatus = e.Row.FindControl("lblstatus") as Label;
            string UserId = Session["userid"].ToString();
            DataTable Dt = objuser.getawardindashboar(UserId, Id.Text);
            if (Dt.Rows.Count > 0)
            {
                lblCurrentLeftBv.Text = Dt.Rows[0]["leftbv"].ToString();
                lblCurrentRightBv.Text = Dt.Rows[0]["RightBv"].ToString();
                Decimal I = Convert.ToDecimal(lbltargetleft.Text) - Convert.ToDecimal(lblCurrentLeftBv.Text);
                Decimal K = Convert.ToDecimal(lbltargetright.Text) - Convert.ToDecimal(lblCurrentRightBv.Text);
                if (I > 0)
                {
                    lblrequiredLeftBv.Text = I.ToString();
                }
                else
                {
                    lblrequiredLeftBv.Text = "0";
                }
                if (K > 0)
                {
                    lblrequiredRightBv.Text = K.ToString();
                }
                else
                {
                    lblrequiredRightBv.Text = "0";
                }
                if (I <= 0 && K <= 0)
                {
                    lblstatus.Text = "Achieved";
                }
                else
                {
                    lblstatus.Text = "Due";
                }
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Id = e.Row.FindControl("lblid") as Label;
            Label lbltargetleft = e.Row.FindControl("lbltargetleft") as Label;
            Label lbltargetright = e.Row.FindControl("lbltargetright") as Label;
            Label lblCurrentLeftBv = e.Row.FindControl("lblCurrentLeftBv") as Label;
            Label lblCurrentRightBv = e.Row.FindControl("lblCurrentRightBv") as Label;
            Label lblrequiredLeftBv = e.Row.FindControl("lblrequiredLeftBv") as Label;
            Label lblrequiredRightBv = e.Row.FindControl("lblrequiredRightBv") as Label;
            Label lblstatus = e.Row.FindControl("lblstatus") as Label;
            string UserId = Session["userid"].ToString();
            DataTable Dt = objuser.getvacationindashboar(UserId, Id.Text);
            if (Dt.Rows.Count > 0)
            {
                lblCurrentLeftBv.Text = Dt.Rows[0]["leftbv"].ToString();
                lblCurrentRightBv.Text = Dt.Rows[0]["RightBv"].ToString();
                Decimal I = Convert.ToDecimal(lbltargetleft.Text) - Convert.ToDecimal(lblCurrentLeftBv.Text);
                Decimal K = Convert.ToDecimal(lbltargetright.Text) - Convert.ToDecimal(lblCurrentRightBv.Text);
                if (I > 0)
                {
                    lblrequiredLeftBv.Text = I.ToString();
                }
                else
                {
                    lblrequiredLeftBv.Text = "0";
                }
                if (K > 0)
                {
                    lblrequiredRightBv.Text = K.ToString();
                }
                else
                {
                    lblrequiredRightBv.Text = "0";
                }
                if (I <= 0 && K <= 0)
                {
                    lblstatus.Text = "Achieved";
                }
                else
                {
                    lblstatus.Text = "Due";
                }
            }
        }
    }
    void loadTodayPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getTodayPerformance(Session["userid"].ToString());
        GridViewToday.DataSource = dt;
        GridViewToday.DataBind();
    }
    void loadweeklyPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getweeklyPerformance(Session["userid"].ToString());
        GrvVwWeek.DataSource = dt;
        GrvVwWeek.DataBind();
    }
    void loadmonthlyPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getmonthlyPerformance(Session["userid"].ToString());
        GrdVwMonth.DataSource = dt;
        GrdVwMonth.DataBind();
    }
    void loadtotalPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getTotalyPerformance(Session["userid"].ToString());
        GrdVwTotal.DataSource = dt;
        GrdVwTotal.DataBind();
    }
    void loadPV()
    {
        DataTable dt = new DataTable();
        dt = objuser.getPV(Session["userid"].ToString());
        if (dt.Rows.Count > 0)
        {
            LblTotalPV.Text = dt.Rows[0][0].ToString();
            LblUsedPV.Text = dt.Rows[0][1].ToString();
            LblCurrentPV.Text = dt.Rows[0][2].ToString();
        }
    }





    void loadPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getTotalyPerformancenew(Session["userid"].ToString());

        if (dt.Rows.Count > 0)
        {
            lblTodayActive.Text = dt.Rows[0]["active"].ToString();
            lblTodayDeactive.Text = dt.Rows[0]["deactive"].ToString();
            lblTodayTotal.Text = dt.Rows[0]["Total"].ToString();
            lblTodayPerformance.Text = dt.Rows[0]["Total"].ToString();

            lblWeekActive.Text = dt.Rows[1]["active"].ToString();
            lblWeekDeactive.Text = dt.Rows[1]["deactive"].ToString();
            lblWeekTotal.Text = dt.Rows[1]["Total"].ToString();
            lblCurrentWeek.Text = dt.Rows[1]["Total"].ToString();

            lblMonthActive.Text = dt.Rows[2]["active"].ToString();
            lblMonthDeactive.Text = dt.Rows[2]["deactive"].ToString();
            lblMonthTotal.Text = dt.Rows[2]["Total"].ToString();
            lblCurrentMonth.Text = dt.Rows[2]["Total"].ToString();

            lblTotalActive.Text = dt.Rows[3]["active"].ToString();
            lblTotalDeactive.Text = dt.Rows[3]["deactive"].ToString();
            lblTotalTotal.Text = dt.Rows[3]["Total"].ToString();
            lblTotal.Text = dt.Rows[3]["Total"].ToString();


        }


        //    GrdPerformance.DataSource = dt;
        //GrdPerformance.DataBind();
    }
    void loadBuissness()
    {
        DataTable dt = new DataTable();
        dt = objuser.getbuissnessPerformancenew(Session["userid"].ToString());
        LblTodayBuissness.Text = dt.Rows[0][0].ToString();
        LblTodayWalletPurchase.Text = dt.Rows[0][1].ToString();
        LblUtilitywalletPurchase.Text = dt.Rows[0][2].ToString();

    }






    //(Starts)Pasted from Recharge.aspx

    public void UpdateBal(string UserId)
    {
        DataTable dt = new DataTable();
        objuser.UserId = UserId;
        dt = objuser.getUserDetail(objuser);
        if (dt.Rows.Count > 0)
        {
            lblwalletbalance123.Text = dt.Rows[0]["balanceamount"].ToString();
            lblUtilityBalance.Text = dt.Rows[0]["UtilityBalance"].ToString();
            lblwalletBalance.Text = dt.Rows[0]["balanceamount"].ToString();
            lblshoppingWallet.Text = dt.Rows[0]["UtilityBalance"].ToString();
        }
    }
    private string GetSocialImage(DataTable dt)
    {
        string pageid = "1";
        string returnPath = "";
        //string searchExpression = "PageId = '" + pageid + "'";
        //DataRow[] foundRows = dt.Select(searchExpression);
        //if (foundRows.Length > 0)
        //{
        //    dt = foundRows.CopyToDataTable();

        //    returnPath = "<li><a  target='_blank' href='" + dt.Rows[0]["Text5"].ToString() + "'><i class='fa fa-facebook'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text6"].ToString() + "'><i class='fa fa-twitter'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text8"].ToString() + "'><i class='fa fa-linkedin'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text7"].ToString() + "'><i class='fa fa-google-plus'></i></a></li>";
        //}
        return returnPath;
    }
    /*********************  Start Recharge Functions ****************/
    public void WebsiteSetting(string host)
    {
        //WhiteLabelMaster obal = new WhiteLabelMaster();
        //System.Data.DataTable dt = obal.GetSellerWebsiteDetail(host);
        //string searchExpression = "PageId = 1";
        //DataRow[] foundRows = dt.Select(searchExpression);
        //dt = foundRows.CopyToDataTable();
        //if (dt.Rows.Count > 0)
        //{

        //    lblFooter.InnerText = dt.Rows[0]["FooterTitle"].ToString();

        //    WhiteLabelId = dt.Rows[0]["UserId"].ToString();

        //    LblNo.InnerHtml = dt.Rows[0]["CustomerCare"].ToString();
        //    DivCustome.InnerHtml = dt.Rows[0]["CareEmail"].ToString();

        //}
        //Ul4.InnerHtml = GetSocialImage(dt);
    }
    protected void rdpre_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void DdlDTHOpertor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }






    public static bool IsNumeric(object Expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }
    public string CheckType(string Status)
    {
        if (Status == "0")
            return "Failed";
        else if (Status == "1")
            return "Request Accepted";
        else if (Status == "2")
            return "Success";
        else if (Status == "3")
            return "Request Accepted";
        else
            return "";

    }



    protected void grdTransReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            if (lblstatus.Text == "Request Sent")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-info";
            }
            else
                if (lblstatus.Text == "Pending")
                {
                    lblstatus.CssClass = "label label-info";
                }
                else
                    if (lblstatus.Text == "Success")
                    {
                        lblstatus.CssClass = "label label-success";
                    }
                    else
                        if (lblstatus.Text == "Dispute")
                        {
                            lblstatus.CssClass = "label badge-info";
                        }
                        else
                            if (lblstatus.Text == "Failed")
                            {
                                lblstatus.CssClass = "label label-danger";
                            }
        }
    }

    protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = (Label)e.Row.FindControl("Status");
            if (lb.Text.ToUpper() == "ACHEIVED")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }

        }
    }

    //(Ends)

    int IncentiveDays
    {
        get { return ViewState["IncentiveDays"] != null ? (int)ViewState["IncentiveDays"] : 30; }
        set { ViewState["IncentiveDays"] = value; }
    }

    protected void IncentiveTab_Click(object sender, EventArgs e)
    {
        LinkButton tab = sender as LinkButton;
        int days;
        if (tab != null && int.TryParse(tab.CommandArgument, out days))
        {
            IncentiveDays = days;
        }

        LoadIncentiveCard();
    }

    void LoadIncentiveCard()
    {
        if (Session["userid"] == null)
        {
            return;
        }

        string userId = Session["userid"].ToString();
        LoadIncentiveProfile(userId);
        LoadIncentiveAmounts(userId, IncentiveDays);
        UpdateIncentiveTabState();
    }

    void LoadIncentiveProfile(string userId)
    {
        objuser.UserId = userId;
        DataTable dt = getUserDetail(objuser);
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        DataRow row = dt.Rows[0];
        lblIncentiveName.Text = Convert.ToString(row["username"]);
        lblIncentiveState.Text = GetRowText(row, "statename", "StateName");
        lblIncentiveDistrict.Text = GetRowText(row, "AreaName", "areaname", "address");
        lblIncentivePan.Text = MaskPan(GetRowText(row, "pannumber", "PanNumber"));

        int days = IncentiveDays;
        if (days == 0)
        {
            lblIncentivePeriod.Text = "Your incentive till date";
        }
        else if (days == 1)
        {
            lblIncentivePeriod.Text = "Your 1 day incentive";
        }
        else
        {
            lblIncentivePeriod.Text = "Your " + days + " days incentive";
        }
        lblIncentiveUpdated.Text = DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
    }

    void LoadIncentiveAmounts(string userId, int days)
    {
        decimal savingDirect = GetSavingLevelIncome(userId, true, days);
        decimal levelIncome = GetSavingLevelIncome(userId, false, days);
        decimal premiumDirect = GetTransactionIncome(userId, "Direct Income", days);
        decimal matchingIncome = GetTransactionIncome(userId, "Binary Income", days);
        decimal cashBack = GetCashBackIncome(userId, days);
        decimal productWallet = GetProductWalletBalance(userId);
        decimal total = savingDirect + levelIncome + premiumDirect + matchingIncome + cashBack;

        lblSavingDirectIncome.Text = savingDirect.ToString("N2");
        lblLevelIncomeCard.Text = levelIncome.ToString("N2");
        lblPremiumDirectIncome.Text = premiumDirect.ToString("N2");
        lblMatchingIncomeCard.Text = matchingIncome.ToString("N2");
        lblCashBackIncome.Text = cashBack.ToString("N2");
        lblProductWalletBalance.Text = productWallet.ToString("N2");
        lblIncentiveTotal.Text = total.ToString("N2");
    }

    void UpdateIncentiveTabState()
    {
        SetTabState(lnkIncentive1Day, IncentiveDays == 1);
        SetTabState(lnkIncentive10Day, IncentiveDays == 10);
        SetTabState(lnkIncentive30Day, IncentiveDays == 30);
        SetTabState(lnkIncentiveTillDate, IncentiveDays == 0);
    }

    static void SetTabState(LinkButton tab, bool isActive)
    {
        if (tab == null)
        {
            return;
        }

        string css = tab.CssClass ?? string.Empty;
        css = css.Replace(" is-active", string.Empty);
        if (isActive)
        {
            css += " is-active";
        }

        tab.CssClass = css.Trim();
    }

    decimal GetSavingLevelIncome(string userId, bool directOnly, int days)
    {
        string levelClause = directOnly ? "sd.LevelNo = 1" : "sd.LevelNo > 1";
        string sql = @"SELECT ISNULL(SUM(sd.Amount), 0)
            FROM SavingLevelIncomeDetail sd WITH (NOLOCK)
            WHERE sd.UserId = '" + SqlEscape(userId) + @"' AND " + levelClause;

        if (days > 0)
        {
            sql += " AND CONVERT(date, sd.MentionDate) >= CONVERT(date, DATEADD(day, -" + days + ", GETDATE()))";
        }

        return ExecuteScalarDecimal(sql);
    }

    decimal GetTransactionIncome(string userId, string transactionType, int days)
    {
        string sql = @"SELECT ISNULL(SUM(t.CrAmount), 0)
            FROM TransactionDetail t WITH (NOLOCK)
            WHERE t.UserId = '" + SqlEscape(userId) + @"'
            AND t.TransactionType = '" + SqlEscape(transactionType) + "'";

        if (days > 0)
        {
            sql += " AND CONVERT(date, t.MentionDate) >= CONVERT(date, DATEADD(day, -" + days + ", GETDATE()))";
        }

        return ExecuteScalarDecimal(sql);
    }

    decimal GetCashBackIncome(string userId, int days)
    {
        string sql = @"SELECT ISNULL(SUM(t.CrAmount), 0)
            FROM TransactionDetail t WITH (NOLOCK)
            WHERE t.UserId = '" + SqlEscape(userId) + @"'
            AND (
                t.TransactionType = 'Self Business Bonus'
                OR t.TransactionType = 'Cashback Income'
                OR t.TransactionType LIKE '%Self Business Bonus%'
            )";

        if (days > 0)
        {
            sql += " AND CONVERT(date, t.MentionDate) >= CONVERT(date, DATEADD(day, -" + days + ", GETDATE()))";
        }

        decimal txnTotal = ExecuteScalarDecimal(sql);
        if (txnTotal > 0m)
        {
            return txnTotal;
        }

        if (days != 0)
        {
            return 0m;
        }

        try
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userId) };
            DataSet ds = DBHelper.ExecuteQuery("sp_Totalincome", parameter);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("GoldDIrector1"))
            {
                decimal fallback;
                if (decimal.TryParse(Convert.ToString(ds.Tables[0].Rows[0]["GoldDIrector1"]), out fallback))
                {
                    return fallback;
                }
            }
        }
        catch
        {
        }

        return 0m;
    }

    decimal GetProductWalletBalance(string userId)
    {
        objuser.UserId = userId;
        DataTable dt = objuser.getUserDetail(objuser);
        if (dt != null && dt.Rows.Count > 0)
        {
            decimal utilityBalance;
            if (decimal.TryParse(Convert.ToString(dt.Rows[0]["UtilityBalance"]), out utilityBalance))
            {
                return utilityBalance;
            }

            decimal leadership;
            if (decimal.TryParse(Convert.ToString(dt.Rows[0]["leadershipincome1"]), out leadership))
            {
                return leadership;
            }
        }

        try
        {
            SqlParameter[] parameter = { new SqlParameter("@UserId", userId) };
            DataSet ds = DBHelper.ExecuteQuery("sp_Totalincome", parameter);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("leadershipincome1"))
            {
                decimal wallet;
                if (decimal.TryParse(Convert.ToString(ds.Tables[0].Rows[0]["leadershipincome1"]), out wallet))
                {
                    return wallet;
                }
            }
        }
        catch
        {
        }

        return 0m;
    }

    decimal ExecuteScalarDecimal(string sql)
    {
        ObjData.StartConnection();
        try
        {
            DataTable dt = ObjData.RunDataTable(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                decimal amount;
                if (decimal.TryParse(Convert.ToString(dt.Rows[0][0]), out amount))
                {
                    return amount;
                }
            }
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }

        return 0m;
    }

    static string GetRowText(DataRow row, params string[] columnNames)
    {
        if (row == null || columnNames == null)
        {
            return "-";
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                continue;
            }

            if (row.Table.Columns.Contains(columnName))
            {
                string value = Convert.ToString(row[columnName]).Trim();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            foreach (DataColumn column in row.Table.Columns)
            {
                if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    string value = Convert.ToString(row[column]).Trim();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return value;
                    }
                }
            }
        }

        return "-";
    }

    static string MaskPan(string pan)
    {
        if (string.IsNullOrWhiteSpace(pan) || pan == "-")
        {
            return "-";
        }

        pan = pan.Trim().ToUpperInvariant();
        if (pan.Length <= 4)
        {
            return pan;
        }

        if (pan.Length <= 7)
        {
            return pan.Substring(0, 2) + "xxx" + pan.Substring(pan.Length - 2);
        }

        return pan.Substring(0, 4) + "xxx" + pan.Substring(pan.Length - 3);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        filldashboard();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        loadPerformance();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class Dashboard : System.Web.UI.Page
{
    clsfranchisee objuserf = new clsfranchisee();
    clsUser objuser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsNews objnews = new clsNews();
    clsaward objAward = new clsaward();
    ClsVacation objvac = new ClsVacation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            if (!IsPostBack)
            {
                TxtLeftLinkLink.Attributes.Add("readonly", "readonly");
                TxtRightLink.Attributes.Add("readonly", "readonly");
                loadnotification();
                laoddata();
               // TxtLeftLinkLink.Text = "http://raxtan.com/user/Registration.aspx/?UserId=" + Session["userid"].ToString() + "&Standingposition=" + 1;
               // TxtRightLink.Text = "http://raxtan.com/user/Registration.aspx/?UserId=" + Session["userid"].ToString() + "&Standingposition=" + 2;
               // loadaward();
               // loadvacation();
                loadnews();
              //  loadTodayPerformance();
               // loadweeklyPerformance();
              //  loadmonthlyPerformance();
              //  loadtotalPerformance();
                loadwallet();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loadwallet()
    {

        objaccount.UserId = Session["fuserid"].ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getUserWalletBalanceReport(objaccount);
        if (dt.Rows.Count > 0)
        {
            LblCredited.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet.Text = dt.Rows[0]["bal"].ToString();
        }
    }
    void loadnews()
    {
        DataTable dt = new DataTable();
        dt = objnews.getRecentNews();
        ltnews.Text += "<span style='color:red;'>* ";
        foreach (DataRow r in dt.Rows)
        {
            ltnews.Text += r["newsdetail"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        ltnews.Text += "</span>";
    }
    void loadnotification()
    {
        objuserf.UserId = Session["fuserid"].ToString();
        DataTable dt = new DataTable();

        dt = objuserf.getUserDetail(objuserf);
        if (dt.Rows[0]["AccountHolderName"].ToString() == "" || dt.Rows[0]["AccountNo"].ToString() == "" || dt.Rows[0]["IFSCCode"].ToString() == "" || dt.Rows[0]["BankName"].ToString() == "" || dt.Rows[0]["BankName"].ToString() == "0" || dt.Rows[0]["BranchName"].ToString() == "" || dt.Rows[0]["PanNumber"].ToString() == "")
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
        objuserf.UserId = Session["fuserid"].ToString();
        DataTable dt = new DataTable();
        dt = objuserf.getUserDetail(objuserf);
        if (dt.Rows.Count > 0)
        {
            //lbluserid.Text = dt.Rows[0]["userid"].ToString();
            //lblusername.Text = dt.Rows[0]["username"].ToString();
           // LblSponserId.Text = dt.Rows[0]["sponserId"].ToString();
           // LblParentId.Text = dt.Rows[0]["parentuserid"].ToString();
            if (dt.Rows[0]["PhotoImage"].ToString() != "")
            {
                ImgMyPhoto.ImageUrl = "../ProductImage/" + dt.Rows[0]["PhotoImage"].ToString();
            }
            else
            {
                ImgMyPhoto.ImageUrl = "img/default.png";
            }
            lbljoiningdate.Text = dt.Rows[0]["parentuserid"].ToString();
          //  LblParentName.Text = dt.Rows[0]["parentname"].ToString();
          //  LblSponserName.Text = dt.Rows[0]["sponsername"].ToString();
            lbljoiningdate.Text = dt.Rows[0]["regdate"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblmobile.Text = dt.Rows[0]["mobile"].ToString();
            lblemail.Text = dt.Rows[0]["email"].ToString();
            lblaccountholdername.Text = dt.Rows[0]["accountholdername"].ToString();
            lblaccountno.Text = dt.Rows[0]["accountno"].ToString();
            lblbank.Text = dt.Rows[0]["branchname"].ToString();
            lblifsc.Text = dt.Rows[0]["ifsccode"].ToString();
            lblpan.Text = dt.Rows[0]["pannumber"].ToString();

        }

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
            string UserId = Session["fuserid"].ToString();
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
            string UserId = Session["fuserid"].ToString();
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
        dt = objuser.getTodayPerformance(Session["fuserid"].ToString());
        GridViewToday.DataSource = dt;
        GridViewToday.DataBind();
    }
    void loadweeklyPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getweeklyPerformance(Session["fuserid"].ToString());
        GrvVwWeek.DataSource = dt;
        GrvVwWeek.DataBind();
    }
    void loadmonthlyPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getmonthlyPerformance(Session["fuserid"].ToString());
        GrdVwMonth.DataSource = dt;
        GrdVwMonth.DataBind();
    }
    void loadtotalPerformance()
    {
        DataTable dt = new DataTable();
        dt = objuser.getTotalyPerformance(Session["fuserid"].ToString());
        GrdVwTotal.DataSource = dt;
        GrdVwTotal.DataBind();
    }
}
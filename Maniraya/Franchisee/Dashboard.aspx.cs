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
        if (Session["fuserid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                if (TxtLeftLinkLink != null)
                {
                    TxtLeftLinkLink.Attributes.Add("readonly", "readonly");
                }
                if (TxtRightLink != null)
                {
                    TxtRightLink.Attributes.Add("readonly", "readonly");
                }
                loadnotification();
                laoddata();
                loadnews();
                loadwallet();
            }
            catch (Exception)
            {
                // Keep dashboard visible even if a data call fails.
                pnlnotification.Visible = false;
            }
        }
    }

    void loadwallet()
    {
        objaccount.UserId = Session["fuserid"].ToString();
        DataTable dt = objaccount.getUserWalletBalanceReport(objaccount);
        if (dt != null && dt.Rows.Count > 0)
        {
            LblCredited.Text = dt.Rows[0]["sumCr"].ToString();
            LblDebited.Text = dt.Rows[0]["sumdr"].ToString();
            LblCurrentWallet.Text = dt.Rows[0]["bal"].ToString();
        }
    }

    void loadnews()
    {
        DataTable dt = objnews.getRecentNews();
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        ltnews.Text += "<span style='color:red;'>* ";
        foreach (DataRow r in dt.Rows)
        {
            ltnews.Text += r["newsdetail"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        ltnews.Text += "</span>";
    }

    void loadnotification()
    {
        pnlnotification.Visible = false;
        objuserf.UserId = Session["fuserid"].ToString();
        DataTable dt = objuserf.getUserDetail(objuserf);
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        DataRow row = dt.Rows[0];
        string accountHolder = GetRowValue(row, "AccountHolderName", "accountholdername");
        string accountNo = GetRowValue(row, "AccountNo", "accountno");
        string ifsc = GetRowValue(row, "IFSCCode", "ifsccode");
        string bankName = GetRowValue(row, "BankName", "bankname");
        string branchName = GetRowValue(row, "BranchName", "branchname");
        string panNumber = GetRowValue(row, "PanNumber", "pannumber");

        if (string.IsNullOrWhiteSpace(accountHolder)
            || string.IsNullOrWhiteSpace(accountNo)
            || string.IsNullOrWhiteSpace(ifsc)
            || string.IsNullOrWhiteSpace(bankName)
            || bankName == "0"
            || string.IsNullOrWhiteSpace(branchName)
            || string.IsNullOrWhiteSpace(panNumber))
        {
            pnlnotification.Visible = true;
        }
    }

    void laoddata()
    {
        objuserf.UserId = Session["fuserid"].ToString();
        DataTable dt = objuserf.getUserDetail(objuserf);
        if (dt == null || dt.Rows.Count == 0)
        {
            ImgMyPhoto.ImageUrl = "img/default.png";
            return;
        }

        DataRow row = dt.Rows[0];
        string photo = GetRowValue(row, "PhotoImage", "photoimage");
        if (!string.IsNullOrWhiteSpace(photo) && photo.IndexOf("default.png", StringComparison.OrdinalIgnoreCase) < 0)
        {
            ImgMyPhoto.ImageUrl = photo.StartsWith("../", StringComparison.Ordinal) ? photo : "../ProductImage/" + photo;
        }
        else
        {
            ImgMyPhoto.ImageUrl = "img/default.png";
        }

        lbljoiningdate.Text = GetRowValue(row, "regdate");
        lbladdress.Text = GetRowValue(row, "address");
        lblmobile.Text = GetRowValue(row, "mobile");
        lblemail.Text = GetRowValue(row, "email");
        lblaccountholdername.Text = GetRowValue(row, "AccountHolderName", "accountholdername");
        lblaccountno.Text = GetRowValue(row, "AccountNo", "accountno");
        lblbank.Text = GetRowValue(row, "BranchName", "branchname", "BankName", "bankname");
        lblifsc.Text = GetRowValue(row, "IFSCCode", "ifsccode");
        lblpan.Text = GetRowValue(row, "PanNumber", "pannumber");
    }

    static string GetRowValue(DataRow row, params string[] columnNames)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName) || !row.Table.Columns.Contains(columnName))
            {
                continue;
            }

            object value = row[columnName];
            if (value != null && value != DBNull.Value)
            {
                return Convert.ToString(value);
            }
        }

        return string.Empty;
    }

    void loadaward()
    {
        DataTable dt = objAward.getawardDetailfromdashboard();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    void loadvacation()
    {
        DataTable dt = objvac.getvacationDetailfromdashboard();
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
        DataTable dt = objuser.getTodayPerformance(Session["fuserid"].ToString());
        GridViewToday.DataSource = dt;
        GridViewToday.DataBind();
    }
    void loadweeklyPerformance()
    {
        DataTable dt = objuser.getweeklyPerformance(Session["fuserid"].ToString());
        GrvVwWeek.DataSource = dt;
        GrvVwWeek.DataBind();
    }
    void loadmonthlyPerformance()
    {
        DataTable dt = objuser.getmonthlyPerformance(Session["fuserid"].ToString());
        GrdVwMonth.DataSource = dt;
        GrdVwMonth.DataBind();
    }
    void loadtotalPerformance()
    {
        DataTable dt = objuser.getTotalyPerformance(Session["fuserid"].ToString());
        GrdVwTotal.DataSource = dt;
        GrdVwTotal.DataBind();
    }
}

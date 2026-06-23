using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

public partial class user_PremiumDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadPremiumData(Session["userid"].ToString());
        }
    }

    void LoadPremiumData(string userId)
    {
        DataTable dt = GetTotalIncomeData(userId);
        if (dt == null || dt.Rows.Count == 0 || IsErrorResult(dt))
        {
            return;
        }

        if (dt.Columns.Contains("Binaryincome"))
        {
            lblBinaryIncome.Text = dt.Rows[0]["Binaryincome"].ToString();
        }

        if (dt.Columns.Contains("sponcering"))
        {
            lblDirectIncome.Text = dt.Rows[0]["sponcering"].ToString();
        }

        if (dt.Columns.Contains("GoldDIrector1"))
        {
            lblCashbackWallet.Text = dt.Rows[0]["GoldDIrector1"].ToString();
        }

        if (dt.Columns.Contains("leadershipincome1"))
        {
            lblProductWallet.Text = dt.Rows[0]["leadershipincome1"].ToString();
        }
    }

    DataTable GetTotalIncomeData(string userId)
    {
        try
        {
            SqlParameter[] parameter = {
                new SqlParameter("@UserId", userId)
            };
            DataSet ds = DBHelper.ExecuteQuery("sp_Totalincome", parameter);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
        }
        catch
        {
        }

        return null;
    }

    static bool IsErrorResult(DataTable dt)
    {
        return dt != null
            && dt.Columns.Contains("Code")
            && dt.Columns.Contains("Remark")
            && dt.Rows.Count == 1
            && Convert.ToString(dt.Rows[0]["Code"]) == "0";
    }
}

using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

public partial class admin_SavingSessionDetailAdd : Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime fromDate;
        DateTime toDate;

        if (!TryParseUiDate(txtFromDate.Text, out fromDate))
        {
            ShowAlert("Invalid From Date. Use dd/mm/yyyy.");
            return;
        }

        if (!TryParseUiDate(txtToDate.Text, out toDate))
        {
            ShowAlert("Invalid To Date. Use dd/mm/yyyy.");
            return;
        }

        if (toDate.Date < fromDate.Date)
        {
            ShowAlert("To Date cannot be earlier than From Date.");
            return;
        }

        string mentionBy = Convert.ToString(Session["useradmin"]);
        string res = InsertSession(fromDate, toDate, mentionBy);

        if (res == "t")
        {
            ShowAlert("Saving session added successfully.");
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            BindGrid();
        }
        else if (res == "f")
        {
            ShowAlert("Session with this From Date already exists.");
        }
        else
        {
            ShowAlert("Unable to save session. Please try again.");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        BindGrid();
    }

    string InsertSession(DateTime fromDate, DateTime toDate, string mentionBy)
    {
        string res = "0";
        SqlConnection cn = ObjData.StartConnectionInTransaction();
        SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@FromDate", fromDate.Date),
                new SqlParameter("@ToDate", toDate.Date),
                new SqlParameter("@MentionBy", mentionBy ?? string.Empty)
            };

            res = Convert.ToString(ObjData.RunInsUpDelQueryTransProcScalar("sp_add_SavingSessionDetail", tr, parameter));
            tr.Commit();
        }
        catch (Exception)
        {
            try { tr.Rollback(); } catch { }
            res = "0";
        }
        finally
        {
            ObjData.EndConnection();
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return (res ?? string.Empty).Trim().ToLowerInvariant();
    }

    void BindGrid()
    {
        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(
                    "SELECT id, fromdate, todate, status, mentionby, mentiondate " +
                    "FROM SavingSessionDetail ORDER BY id DESC");
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        gvSession.DataSource = dt;
        gvSession.DataBind();
    }

    static bool TryParseUiDate(string input, out DateTime date)
    {
        date = DateTime.MinValue;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        string value = input.Trim();
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd" };
        return DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
            || DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
    }

    protected string FormatStatus(object status)
    {
        string value = Convert.ToString(status);
        bool active = value == "1"
            || string.Equals(value, "True", StringComparison.OrdinalIgnoreCase);

        return active
            ? "<span class=\"label label-success\">Active</span>"
            : "<span class=\"label label-default\">Inactive</span>";
    }

    void ShowAlert(string message)
    {
        string safe = (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
            "alert('" + safe + "');", true);
    }

}

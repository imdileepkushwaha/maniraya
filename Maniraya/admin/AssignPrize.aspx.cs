using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AssignPrize : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindPrizeDropDown();
            BindStagedGrid();
            BindAssignments(null);
        }
    }

    // ---------- staging table (kept in ViewState) ----------

    DataTable StagedTable
    {
        get
        {
            DataTable dt = ViewState["StagedPrizes"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("RowKey", typeof(string));
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("UserName", typeof(string));
                dt.Columns.Add("Mobile", typeof(string));
                dt.Columns.Add("PrizeId", typeof(int));
                dt.Columns.Add("PrizeName", typeof(string));
                dt.Columns.Add("PrizeMonth", typeof(string));
                dt.Columns.Add("PrizeMonthDisplay", typeof(string));
                ViewState["StagedPrizes"] = dt;
            }

            return dt;
        }
        set { ViewState["StagedPrizes"] = value; }
    }

    void BindPrizeDropDown()
    {
        ddlPrize.Items.Clear();
        ddlPrize.Items.Add(new ListItem("-- Select Prize --", "0"));

        DataTable dt = PrizeHelper.GetActivePrizes();
        foreach (DataRow row in dt.Rows)
        {
            ddlPrize.Items.Add(new ListItem(Convert.ToString(row["PrizeName"]), Convert.ToString(row["Id"])));
        }
    }

    void BindStagedGrid()
    {
        gvStaged.DataSource = StagedTable;
        gvStaged.DataBind();
        lblStagedCount.Text = StagedTable.Rows.Count.ToString();
    }

    void BindAssignments(string filter)
    {
        gvAssignments.DataSource = PrizeHelper.GetAllAssignments(filter, null);
        gvAssignments.DataBind();
    }

    public string GetMonthDisplay(object value)
    {
        return PrizeHelper.FormatPrizeMonth(value);
    }

    // ---------- member search ----------

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string userId = txtUserId.Text.Trim();
        if (string.IsNullOrWhiteSpace(userId))
        {
            ShowAlert("Enter a User ID to search.");
            pnlUser.Visible = false;
            return;
        }

        DataTable dt = PrizeHelper.GetUserByUserId(userId);
        if (dt == null || dt.Rows.Count == 0)
        {
            pnlUser.Visible = false;
            ClearFoundUser();
            ShowAlert("No member found for User ID: " + userId);
            return;
        }

        DataRow row = dt.Rows[0];
        string foundId = Convert.ToString(row["userid"]);
        string userName = Convert.ToString(row["username"]);
        string mobile = Convert.ToString(row["mobile"]);
        string coupon = row.Table.Columns.Contains("couponcodes") ? Convert.ToString(row["couponcodes"]) : string.Empty;

        ViewState["FoundUserId"] = foundId;
        ViewState["FoundUserName"] = userName;
        ViewState["FoundMobile"] = mobile;

        lblUserName.Text = string.IsNullOrWhiteSpace(userName) ? "(no name)" : userName;
        lblUserIdShow.Text = foundId;
        lblMobile.Text = string.IsNullOrWhiteSpace(mobile) ? "-" : mobile;
        lblCoupon.Text = string.IsNullOrWhiteSpace(coupon) ? "-" : coupon;
        pnlUser.Visible = true;
    }

    // ---------- staging actions ----------

    protected void btnAddToList_Click(object sender, EventArgs e)
    {
        string foundId = Convert.ToString(ViewState["FoundUserId"]);
        if (string.IsNullOrWhiteSpace(foundId))
        {
            ShowAlert("Search and select a member first.");
            return;
        }

        int prizeId;
        if (!int.TryParse(ddlPrize.SelectedValue, out prizeId) || prizeId <= 0)
        {
            ShowAlert("Please select a prize.");
            return;
        }

        string month = txtMonth.Text.Trim();
        if (string.IsNullOrWhiteSpace(month))
        {
            ShowAlert("Please select the prize month.");
            return;
        }

        DataTable dt = StagedTable;

        foreach (DataRow existing in dt.Rows)
        {
            if (string.Equals(Convert.ToString(existing["UserId"]), foundId, StringComparison.OrdinalIgnoreCase)
                && Convert.ToInt32(existing["PrizeId"]) == prizeId
                && string.Equals(Convert.ToString(existing["PrizeMonth"]), month, StringComparison.OrdinalIgnoreCase))
            {
                ShowAlert("This member already has this prize for the selected month in the list.");
                return;
            }
        }

        DataRow newRow = dt.NewRow();
        newRow["RowKey"] = Guid.NewGuid().ToString("N");
        newRow["UserId"] = foundId;
        newRow["UserName"] = Convert.ToString(ViewState["FoundUserName"]);
        newRow["Mobile"] = Convert.ToString(ViewState["FoundMobile"]);
        newRow["PrizeId"] = prizeId;
        newRow["PrizeName"] = ddlPrize.SelectedItem.Text;
        newRow["PrizeMonth"] = month;
        newRow["PrizeMonthDisplay"] = PrizeHelper.FormatPrizeMonth(month);
        dt.Rows.Add(newRow);
        StagedTable = dt;

        BindStagedGrid();
        ResetMemberInputs();
    }

    protected void gvStaged_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "removeRow", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string rowKey = Convert.ToString(e.CommandArgument);
        DataTable dt = StagedTable;
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            if (string.Equals(Convert.ToString(dt.Rows[i]["RowKey"]), rowKey, StringComparison.Ordinal))
            {
                dt.Rows.RemoveAt(i);
                break;
            }
        }

        StagedTable = dt;
        BindStagedGrid();
    }

    protected void btnClearList_Click(object sender, EventArgs e)
    {
        DataTable dt = StagedTable;
        dt.Rows.Clear();
        StagedTable = dt;
        BindStagedGrid();
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        DataTable dt = StagedTable;
        if (dt.Rows.Count == 0)
        {
            ShowAlert("Add at least one member to the list before saving.");
            return;
        }

        string createdBy = Session["useradmin"] != null ? Session["useradmin"].ToString() : string.Empty;
        int saved = 0;
        int skipped = 0;
        int failed = 0;

        foreach (DataRow row in dt.Rows)
        {
            string res = PrizeHelper.AssignPrize(
                Convert.ToInt32(row["PrizeId"]),
                Convert.ToString(row["UserId"]),
                Convert.ToString(row["PrizeMonth"]),
                createdBy);

            if (res == "ok")
            {
                saved++;
            }
            else if (res == "duplicate")
            {
                skipped++;
            }
            else
            {
                failed++;
            }
        }

        dt.Rows.Clear();
        StagedTable = dt;
        BindStagedGrid();
        BindAssignments(null);

        string message = saved + " assignment(s) saved successfully.";
        if (skipped > 0)
        {
            message += " " + skipped + " skipped (already assigned).";
        }
        if (failed > 0)
        {
            message += " " + failed + " failed.";
        }

        ShowAlert(message);
    }

    // ---------- existing assignments ----------

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindAssignments(txtFilterUser.Text.Trim());
    }

    protected void btnFilterReset_Click(object sender, EventArgs e)
    {
        txtFilterUser.Text = string.Empty;
        BindAssignments(null);
    }

    protected void btnExportAssignments_Click(object sender, EventArgs e)
    {
        DataTable dt = PrizeHelper.GetAllAssignments(txtFilterUser.Text.Trim(), null);

        StringBuilder sb = new StringBuilder();
        sb.Append("<table border='1' cellspacing='0' cellpadding='4'>");
        sb.Append("<tr style='background:#1f7a45;color:#ffffff;font-weight:bold;'>");
        sb.Append("<th>#</th><th>User ID</th><th>Member Name</th><th>Mobile</th>")
          .Append("<th>Prize</th><th>Month</th><th>Assigned On</th>");
        sb.Append("</tr>");

        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>").Append(i++).Append("</td>");
            sb.Append("<td style=\"mso-number-format:'\\@';\">").Append(HtmlEnc(row["UserId"])).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(row["UserName"])).Append("</td>");
            sb.Append("<td style=\"mso-number-format:'\\@';\">").Append(HtmlEnc(row["Mobile"])).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(row["PrizeName"])).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(PrizeHelper.FormatPrizeMonth(row["PrizeMonth"]))).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(FormatDate(row["CreatedOn"]))).Append("</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        string fileName = "AssignedPrizes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.Output.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    static string FormatDate(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "-";
        }

        DateTime parsed;
        if (DateTime.TryParse(Convert.ToString(value), out parsed))
        {
            return parsed.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        }

        return "-";
    }

    static string HtmlEnc(object value)
    {
        return System.Web.HttpUtility.HtmlEncode(Convert.ToString(value));
    }

    protected void gvAssignments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "deleteRow", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        int id;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out id))
        {
            return;
        }

        if (PrizeHelper.DeleteAssignment(id))
        {
            ShowAlert("Assignment deleted.");
        }
        else
        {
            ShowAlert("Unable to delete assignment.");
        }

        BindAssignments(txtFilterUser.Text.Trim());
    }

    // ---------- helpers ----------

    void ClearFoundUser()
    {
        ViewState["FoundUserId"] = null;
        ViewState["FoundUserName"] = null;
        ViewState["FoundMobile"] = null;
        lblUserName.Text = string.Empty;
        lblUserIdShow.Text = string.Empty;
        lblMobile.Text = string.Empty;
        lblCoupon.Text = string.Empty;
    }

    void ResetMemberInputs()
    {
        ddlPrize.SelectedIndex = 0;
        txtMonth.Text = string.Empty;
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

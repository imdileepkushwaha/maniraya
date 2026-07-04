using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PrizeMaster : System.Web.UI.Page
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
            BindPrizeGrid();
        }
    }

    void BindPrizeGrid()
    {
        GridView1.DataSource = PrizeHelper.GetAllPrizes();
        GridView1.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAddForm();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string createdBy = Session["useradmin"] != null ? Session["useradmin"].ToString() : string.Empty;
        string res = PrizeHelper.AddPrize(txtPrizeName.Text.Trim(), txtPrizeDesc.Text.Trim(), createdBy);

        switch (res)
        {
            case "ok":
                ShowAlert("Prize added successfully.");
                ClearAddForm();
                BindPrizeGrid();
                break;
            case "exists":
                ShowAlert("This prize already exists.");
                break;
            case "empty":
                ShowAlert("Enter Prize Name.");
                break;
            default:
                ShowAlert("Unable to add prize. Please try again.");
                break;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        DataRowView row = e.Row.DataItem as DataRowView;
        if (row == null)
        {
            return;
        }

        LinkButton lnkToggle = e.Row.FindControl("lnkToggle") as LinkButton;
        if (lnkToggle != null)
        {
            bool isActive = Convert.ToBoolean(row["Status"]);
            lnkToggle.CommandName = isActive ? "deactivate" : "activate";
            lnkToggle.ToolTip = isActive ? "Deactivate prize" : "Activate prize";
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "edt", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        int index = Convert.ToInt32(e.CommandArgument);
        string prizeId = ((Label)GridView1.Rows[index].FindControl("lblid")).Text;

        int parsedId;
        if (!int.TryParse(prizeId, out parsedId))
        {
            ShowAlert("Unable to load prize for edit.");
            return;
        }

        DataTable dt = PrizeHelper.GetPrizeById(parsedId);
        if (dt == null || dt.Rows.Count == 0)
        {
            ShowAlert("Prize not found.");
            return;
        }

        DataRow row = dt.Rows[0];
        lblEditId.Text = prizeId;
        txtPrizeNameEdit.Text = Convert.ToString(row["PrizeName"]);
        txtPrizeDescEdit.Text = row.Table.Columns.Contains("Description") ? Convert.ToString(row["Description"]) : string.Empty;
        chkEditStatus.Checked = !row.Table.Columns.Contains("Status") || Convert.ToBoolean(row["Status"]);

        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            UpdatePanel1.GetType(),
            "openPrizeEditModal",
            "openPrizeEditModal();",
            true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int prizeId;
        if (!int.TryParse(lblEditId.Text, out prizeId))
        {
            ShowAlert("Invalid prize selected.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPrizeNameEdit.Text))
        {
            ShowAlert("Enter Prize Name.");
            return;
        }

        if (PrizeHelper.UpdatePrize(prizeId, txtPrizeNameEdit.Text.Trim(), txtPrizeDescEdit.Text.Trim(), chkEditStatus.Checked))
        {
            ShowAlert("Prize updated successfully.");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "closePrizeEditModal", "closePrizeEditModal();", true);
            BindPrizeGrid();
            return;
        }

        ShowAlert("Unable to update prize.");
    }

    protected void lnkToggle_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        if (lnk == null)
        {
            return;
        }

        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            ShowAlert("Unable to update prize status.");
            return;
        }

        bool activate = string.Equals(lnk.CommandName, "activate", StringComparison.OrdinalIgnoreCase);
        if (PrizeHelper.SetPrizeStatus(id, activate))
        {
            ShowAlert(activate ? "Prize activated successfully." : "Prize deactivated successfully.");
            BindPrizeGrid();
            return;
        }

        ShowAlert("Unable to update prize status.");
    }

    void ClearAddForm()
    {
        txtPrizeName.Text = string.Empty;
        txtPrizeDesc.Text = string.Empty;
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactAdd : Page
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
            SiteContactHelper.EnsureTableAndSeedDefaults();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        GridView1.DataSource = SiteContactHelper.GetAllContacts();
        GridView1.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtContactValue.Text))
        {
            ShowAlert("Please enter contact value.");
            return;
        }

        int displayOrder = 1;
        int.TryParse(txtDisplayOrder.Text, out displayOrder);

        int? editId = null;
        if (ViewState["EditId"] != null)
        {
            int parsedId;
            if (int.TryParse(ViewState["EditId"].ToString(), out parsedId))
            {
                editId = parsedId;
            }
        }

        bool saved = SiteContactHelper.SaveContact(
            editId,
            ddlContactType.SelectedValue,
            txtTitle.Text,
            txtContactValue.Text,
            displayOrder,
            chkPrimary.Checked,
            chkStatus.Checked);

        if (!saved)
        {
            ShowAlert("Unable to save contact. Please try again.");
            return;
        }

        ShowAlert(editId.HasValue ? "Contact updated successfully." : "Contact added successfully.");
        ClearForm();
        BindGrid();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        DataTable dt = SiteContactHelper.GetContactById(id);
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        DataRow row = dt.Rows[0];
        ddlContactType.SelectedValue = Convert.ToString(row["ContactType"]);
        txtTitle.Text = Convert.ToString(row["Title"]);
        txtContactValue.Text = Convert.ToString(row["ContactValue"]);
        txtDisplayOrder.Text = Convert.ToString(row["DisplayOrder"]);
        chkPrimary.Checked = Convert.ToBoolean(row["IsPrimary"]);
        chkStatus.Checked = Convert.ToBoolean(row["Status"]);
        ViewState["EditId"] = id.ToString();
        btnSubmit.Text = "Update";
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        if (SiteContactHelper.DeleteContact(id))
        {
            ShowAlert("Contact deleted successfully.");
            ClearForm();
            BindGrid();
            return;
        }

        ShowAlert("Unable to delete contact.");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        ddlContactType.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
        txtContactValue.Text = string.Empty;
        txtDisplayOrder.Text = "1";
        chkPrimary.Checked = false;
        chkStatus.Checked = true;
        ViewState["EditId"] = null;
        btnSubmit.Text = "Submit";
    }

    private void ShowAlert(string message)
    {
        string safeMessage = (message ?? string.Empty).Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(this, GetType(), "contactAlert", "alert('" + safeMessage + "');", true);
    }
}

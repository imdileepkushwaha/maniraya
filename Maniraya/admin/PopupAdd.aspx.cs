using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PopupAdd : Page
{
    const string ModeText = "text";
    const string ModeImage = "image";

    static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            SitePopupHelper.EnsureTableAndSeedDefaults();
            BindGrid();
            UpdateSectionStyles();
        }
    }

    private void BindGrid()
    {
        GridView1.DataSource = SitePopupHelper.GetAllPopups();
        GridView1.DataBind();
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

        Image img = e.Row.FindControl("imgGridPopup") as Image;
        if (img != null && !string.IsNullOrWhiteSpace(img.ImageUrl))
        {
            img.ImageUrl = ResolveUrl(img.ImageUrl);
        }

        Literal litPopupType = e.Row.FindControl("litPopupType") as Literal;
        if (litPopupType != null)
        {
            litPopupType.Text = BuildPopupTypeBadge(row.Row);
        }
    }

    static string BuildPopupTypeBadge(DataRow row)
    {
        bool hasContent = !string.IsNullOrWhiteSpace(Convert.ToString(row["PopupContent"]));
        bool hasImage = !string.IsNullOrWhiteSpace(SitePopupHelper.GetPopupImagePath(row));

        if (hasImage && !hasContent)
        {
            return "<span class=\"popup-type-badge popup-type-badge--image\">Image</span>";
        }

        return "<span class=\"popup-type-badge popup-type-badge--text\">Text</span>";
    }

    protected void btnSubmitText_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPopupContent.Text))
        {
            ShowAlert("Please enter popup message.");
            return;
        }

        int? editId = GetEditIdForMode(ModeText);

        bool saved = SitePopupHelper.SavePopup(
            editId,
            txtTitle.Text,
            txtPopupContent.Text,
            string.Empty,
            chkTextStatus.Checked);

        if (!saved)
        {
            ShowAlert("Unable to save text popup. Please try again.");
            return;
        }

        ShowAlert(editId.HasValue ? "Text popup updated successfully." : "Text popup added successfully.");
        ClearTextForm();
        BindGrid();
    }

    protected void btnSubmitImage_Click(object sender, EventArgs e)
    {
        bool hasUploadedImage = fuPopupImage.HasFile;
        bool hasExistingImage = !string.IsNullOrWhiteSpace(hfCurrentImage.Value);

        if (!hasUploadedImage && !hasExistingImage)
        {
            ShowAlert("Please upload a popup image.");
            return;
        }

        int? editId = GetEditIdForMode(ModeImage);
        string imagePath = hfCurrentImage.Value;

        if (hasUploadedImage)
        {
            string savedPath;
            if (!TrySavePopupImage(editId, out savedPath))
            {
                return;
            }

            imagePath = savedPath;
        }

        bool saved = SitePopupHelper.SavePopup(
            editId,
            txtImageTitle.Text,
            string.Empty,
            imagePath,
            chkImageStatus.Checked);

        if (!saved)
        {
            ShowAlert("Unable to save image popup. Please try again.");
            return;
        }

        ShowAlert(editId.HasValue ? "Image popup updated successfully." : "Image popup added successfully.");
        ClearImageForm();
        BindGrid();
    }

    int? GetEditIdForMode(string mode)
    {
        if (!string.Equals(hfEditMode.Value, mode, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        int parsedId;
        if (int.TryParse(hfEditId.Value, out parsedId) && parsedId > 0)
        {
            return parsedId;
        }

        return null;
    }

    bool TrySavePopupImage(int? editId, out string relativePath)
    {
        relativePath = string.Empty;

        string extension = Path.GetExtension(fuPopupImage.FileName);
        if (!IsAllowedImageExtension(extension))
        {
            ShowAlert("Please upload JPG, JPEG, PNG or GIF image only.");
            return false;
        }

        string folderPhysicalPath = Server.MapPath(SitePopupHelper.PopupImageFolder);
        if (!Directory.Exists(folderPhysicalPath))
        {
            Directory.CreateDirectory(folderPhysicalPath);
        }

        string fileName = "popup_" + (editId.HasValue ? editId.Value.ToString() : DateTime.Now.Ticks.ToString()) + extension.ToLowerInvariant();
        string physicalPath = Path.Combine(folderPhysicalPath, fileName);
        fuPopupImage.SaveAs(physicalPath);

        relativePath = SitePopupHelper.PopupImageFolder + fileName;
        return true;
    }

    static bool IsAllowedImageExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            return false;
        }

        string normalized = extension.ToLowerInvariant();
        foreach (string allowed in AllowedImageExtensions)
        {
            if (normalized == allowed)
            {
                return true;
            }
        }

        return false;
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        DataTable dt = SitePopupHelper.GetPopupById(id);
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        ClearForm();
        DataRow row = dt.Rows[0];
        string content = Convert.ToString(row["PopupContent"]);
        string imagePath = SitePopupHelper.GetPopupImagePath(row);
        bool hasContent = !string.IsNullOrWhiteSpace(content);
        bool hasImage = !string.IsNullOrWhiteSpace(imagePath);

        if (hasImage && !hasContent)
        {
            LoadImageForm(id, row, imagePath);
            return;
        }

        LoadTextForm(id, row);
    }

    void LoadTextForm(int id, DataRow row)
    {
        hfEditId.Value = id.ToString();
        hfEditMode.Value = ModeText;
        txtTitle.Text = Convert.ToString(row["Title"]);
        txtPopupContent.Text = Convert.ToString(row["PopupContent"]);
        chkTextStatus.Checked = Convert.ToBoolean(row["Status"]);
        btnSubmitText.Text = "Update Text Popup";
        UpdateSectionStyles();
    }

    void LoadImageForm(int id, DataRow row, string imagePath)
    {
        hfEditId.Value = id.ToString();
        hfEditMode.Value = ModeImage;
        txtImageTitle.Text = Convert.ToString(row["Title"]);
        chkImageStatus.Checked = Convert.ToBoolean(row["Status"]);
        hfCurrentImage.Value = imagePath;
        btnSubmitImage.Text = "Update Image Popup";
        ShowImagePreview(imagePath);
        UpdateSectionStyles();
    }

    protected void lnkToggle_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        bool activate = string.Equals(lnk.CommandName, "activate", StringComparison.OrdinalIgnoreCase);
        if (SitePopupHelper.SetPopupStatus(id, activate))
        {
            ShowAlert(activate ? "Popup activated successfully." : "Popup deactivated successfully.");
            ClearForm();
            BindGrid();
            return;
        }

        ShowAlert("Unable to update popup status.");
    }

    protected void btnConfirmDelete_Click(object sender, EventArgs e)
    {
        int id;
        if (!int.TryParse(hfDeletePopupId.Value, out id))
        {
            ShowAlert("Unable to delete popup.");
            return;
        }

        if (SitePopupHelper.DeletePopup(id))
        {
            hfDeletePopupId.Value = string.Empty;
            CloseDeleteModal();
            ShowAlert("Popup deleted successfully.");
            ClearForm();
            BindGrid();
            return;
        }

        ShowAlert("Unable to delete popup.");
    }

    protected void btnCancelText_Click(object sender, EventArgs e)
    {
        ClearTextForm();
    }

    protected void btnCancelImage_Click(object sender, EventArgs e)
    {
        ClearImageForm();
    }

    void CloseDeleteModal()
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "closePopupDeleteModal",
            "if (typeof closeAdminModal === 'function') { closeAdminModal('popupDeleteModal'); }",
            true);
    }

    private void ClearForm()
    {
        ClearTextForm();
        ClearImageForm();
    }

    void ClearTextForm()
    {
        txtTitle.Text = string.Empty;
        txtPopupContent.Text = string.Empty;
        chkTextStatus.Checked = true;
        btnSubmitText.Text = "Add Text Popup";

        if (string.Equals(hfEditMode.Value, ModeText, StringComparison.OrdinalIgnoreCase))
        {
            hfEditId.Value = string.Empty;
            hfEditMode.Value = string.Empty;
        }

        UpdateSectionStyles();
    }

    void ClearImageForm()
    {
        txtImageTitle.Text = string.Empty;
        hfCurrentImage.Value = string.Empty;
        chkImageStatus.Checked = true;
        btnSubmitImage.Text = "Add Image Popup";
        ResetImagePreview();

        if (string.Equals(hfEditMode.Value, ModeImage, StringComparison.OrdinalIgnoreCase))
        {
            hfEditId.Value = string.Empty;
            hfEditMode.Value = string.Empty;
        }

        UpdateSectionStyles();
    }

    void UpdateSectionStyles()
    {
        pnlTextSection.CssClass = "box box-primary popup-admin-card"
            + (string.Equals(hfEditMode.Value, ModeText, StringComparison.OrdinalIgnoreCase) ? " is-editing" : string.Empty);
        pnlImageSection.CssClass = "box box-primary popup-admin-card"
            + (string.Equals(hfEditMode.Value, ModeImage, StringComparison.OrdinalIgnoreCase) ? " is-editing" : string.Empty);
    }

    void ShowImagePreview(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            ResetImagePreview();
            return;
        }

        string resolvedUrl = ResolveUrl(imagePath);
        string script = string.Format(
            "if (window.AdminImageUpload) {{ AdminImageUpload.setUrlPreview('popupImageSlot1', '{0}', 'Current image'); }}",
            JsEncode(resolvedUrl));
        ScriptManager.RegisterStartupScript(this, GetType(), "popupImagePreview", script, true);
    }

    void ResetImagePreview()
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "popupImageReset",
            "if (window.AdminImageUpload) { AdminImageUpload.reset('popupImageSlot1'); }",
            true);
    }

    static string JsEncode(string value)
    {
        return (value ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
    }

    private void ShowAlert(string message)
    {
        string safeMessage = (message ?? string.Empty).Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(this, GetType(), "popupAlert", "alert('" + safeMessage + "');", true);
    }
}

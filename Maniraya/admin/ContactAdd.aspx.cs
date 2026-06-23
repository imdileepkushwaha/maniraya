using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactAdd : Page
{
    const string SignUploadFolder = "~/InvoiceSign/";

    public string ActiveTabHref
    {
        get
        {
            string tab = ViewState["ActiveTab"] as string;
            if (string.IsNullOrWhiteSpace(tab))
            {
                return "#tabContact";
            }

            if (tab == "gst")
            {
                return "#tabGst";
            }

            if (tab == "sign")
            {
                return "#tabSign";
            }

            return "#tabContact";
        }
    }

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
            BindGstGrid();
            BindSignGrid();
            ClearGstForm();
            ClearSignForm();
            SetActiveTab("contact");
        }
    }

    void SetActiveTab(string tabKey)
    {
        ViewState["ActiveTab"] = tabKey ?? "contact";
    }

    private void BindGrid()
    {
        GridView1.DataSource = SiteContactHelper.GetContactsExcludingTypes(
            SiteContactHelper.TypeGst,
            SiteContactHelper.TypeSign);
        GridView1.DataBind();
    }

    private void BindGstGrid()
    {
        gvGst.DataSource = SiteContactHelper.GetContactsByType(SiteContactHelper.TypeGst);
        gvGst.DataBind();
    }

    private void BindSignGrid()
    {
        gvSign.DataSource = SiteContactHelper.GetContactsByType(SiteContactHelper.TypeSign);
        gvSign.DataBind();
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

        int? editId = GetEditId("EditId");

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
        SetActiveTab("contact");
    }

    protected void btnGstSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtGstNumber.Text))
        {
            ShowAlert("Please enter GST number.");
            return;
        }

        int displayOrder = 1;
        int.TryParse(txtGstDisplayOrder.Text, out displayOrder);

        int? editId = GetEditId("GstEditId");
        string title = string.IsNullOrWhiteSpace(txtGstTitle.Text) ? "Company GSTIN" : txtGstTitle.Text.Trim();

        bool saved = SiteContactHelper.SaveContact(
            editId,
            SiteContactHelper.TypeGst,
            title,
            txtGstNumber.Text.Trim().ToUpperInvariant(),
            displayOrder,
            chkGstPrimary.Checked,
            chkGstStatus.Checked);

        if (!saved)
        {
            ShowAlert("Unable to save GST number. Please try again.");
            return;
        }

        ShowAlert(editId.HasValue ? "GST number updated successfully." : "GST number added successfully.");
        ClearGstForm();
        BindGstGrid();
        SetActiveTab("gst");
    }

    protected void btnSignSubmit_Click(object sender, EventArgs e)
    {
        int? editId = GetEditId("SignEditId");
        string imageName = SaveUploadedSign(fuSignImage, hfSignImage.Value);
        if (imageName == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(imageName))
        {
            ShowAlert("Please upload a signature image.");
            return;
        }

        int displayOrder = 1;
        int.TryParse(txtSignDisplayOrder.Text, out displayOrder);
        string title = string.IsNullOrWhiteSpace(txtSignTitle.Text) ? "Authorised Signatory" : txtSignTitle.Text.Trim();

        bool saved = SiteContactHelper.SaveContact(
            editId,
            SiteContactHelper.TypeSign,
            title,
            imageName,
            displayOrder,
            chkSignPrimary.Checked,
            chkSignStatus.Checked);

        if (!saved)
        {
            ShowAlert("Unable to save signature. Please try again.");
            return;
        }

        ShowAlert(editId.HasValue ? "Signature updated successfully." : "Signature added successfully.");
        ClearSignForm();
        BindSignGrid();
        SetActiveTab("sign");
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
        SetActiveTab("contact");
    }

    protected void lnkGstEdit_Click(object sender, EventArgs e)
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

        LoadGstForm(dt.Rows[0], id);
        SetActiveTab("gst");
    }

    protected void lnkSignEdit_Click(object sender, EventArgs e)
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

        LoadSignForm(dt.Rows[0], id);
        SetActiveTab("sign");
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
            SetActiveTab("contact");
            return;
        }

        ShowAlert("Unable to delete contact.");
    }

    protected void lnkGstDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        if (SiteContactHelper.DeleteContact(id))
        {
            ShowAlert("GST number deleted successfully.");
            ClearGstForm();
            BindGstGrid();
            SetActiveTab("gst");
            return;
        }

        ShowAlert("Unable to delete GST number.");
    }

    protected void lnkSignDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int id;
        if (!int.TryParse(lnk.CommandArgument, out id))
        {
            return;
        }

        DataTable dt = SiteContactHelper.GetContactById(id);
        if (SiteContactHelper.DeleteContact(id))
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DeleteSignImageFile(Convert.ToString(dt.Rows[0]["ContactValue"]));
            }

            ShowAlert("Signature deleted successfully.");
            ClearSignForm();
            BindSignGrid();
            SetActiveTab("sign");
            return;
        }

        ShowAlert("Unable to delete signature.");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
        SetActiveTab("contact");
    }

    protected void btnGstCancel_Click(object sender, EventArgs e)
    {
        ClearGstForm();
        SetActiveTab("gst");
    }

    protected void btnSignCancel_Click(object sender, EventArgs e)
    {
        ClearSignForm();
        SetActiveTab("sign");
    }

    void LoadGstForm(DataRow row, int id)
    {
        txtGstTitle.Text = Convert.ToString(row["Title"]);
        txtGstNumber.Text = Convert.ToString(row["ContactValue"]);
        txtGstDisplayOrder.Text = Convert.ToString(row["DisplayOrder"]);
        chkGstPrimary.Checked = Convert.ToBoolean(row["IsPrimary"]);
        chkGstStatus.Checked = Convert.ToBoolean(row["Status"]);
        ViewState["GstEditId"] = id.ToString();
        btnGstSubmit.Text = "Update GST";
    }

    void LoadSignForm(DataRow row, int id)
    {
        string imageName = Convert.ToString(row["ContactValue"]);
        txtSignTitle.Text = Convert.ToString(row["Title"]);
        hfSignImage.Value = imageName;
        txtSignDisplayOrder.Text = Convert.ToString(row["DisplayOrder"]);
        chkSignPrimary.Checked = Convert.ToBoolean(row["IsPrimary"]);
        chkSignStatus.Checked = Convert.ToBoolean(row["Status"]);
        ViewState["SignEditId"] = id.ToString();
        btnSignSubmit.Text = "Update Signature";
        ShowSignPreview(imageName);
    }

    string SaveUploadedSign(FileUpload upload, string currentImageName)
    {
        if (upload == null || !upload.HasFile)
        {
            return currentImageName ?? string.Empty;
        }

        string extension = Path.GetExtension(upload.FileName).ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            ShowAlert("Please upload JPG, PNG, WEBP or GIF image only.");
            return null;
        }

        string fileName = "sign_" + Guid.NewGuid().ToString("N").Substring(0, 10) + extension;
        string folder = Server.MapPath(SignUploadFolder);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        upload.SaveAs(Path.Combine(folder, fileName));

        if (!string.IsNullOrWhiteSpace(currentImageName)
            && !string.Equals(currentImageName, fileName, StringComparison.OrdinalIgnoreCase))
        {
            DeleteSignImageFile(currentImageName);
        }

        hfSignImage.Value = fileName;
        ShowSignPreview(fileName);
        return fileName;
    }

    void ShowSignPreview(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            imgSignPreview.Visible = false;
            imgSignPreview.ImageUrl = string.Empty;
            return;
        }

        imgSignPreview.ImageUrl = ResolveUrl(SignUploadFolder + imageName);
        imgSignPreview.Visible = true;
    }

    void DeleteSignImageFile(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            return;
        }

        string path = Server.MapPath(SignUploadFolder + imageName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
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

    private void ClearGstForm()
    {
        txtGstTitle.Text = "Company GSTIN";
        txtGstNumber.Text = string.Empty;
        txtGstDisplayOrder.Text = "1";
        chkGstPrimary.Checked = true;
        chkGstStatus.Checked = true;
        ViewState["GstEditId"] = null;
        btnGstSubmit.Text = "Save GST";
    }

    private void ClearSignForm()
    {
        txtSignTitle.Text = "Authorised Signatory";
        hfSignImage.Value = string.Empty;
        txtSignDisplayOrder.Text = "1";
        chkSignPrimary.Checked = true;
        chkSignStatus.Checked = true;
        ViewState["SignEditId"] = null;
        btnSignSubmit.Text = "Save Signature";
        ShowSignPreview(string.Empty);
    }

    int? GetEditId(string viewStateKey)
    {
        if (ViewState[viewStateKey] == null)
        {
            return null;
        }

        int parsedId;
        if (int.TryParse(ViewState[viewStateKey].ToString(), out parsedId))
        {
            return parsedId;
        }

        return null;
    }

    private void ShowAlert(string message)
    {
        string safeMessage = (message ?? string.Empty).Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(this, GetType(), "contactAlert", "alert('" + safeMessage + "');", true);
    }
}

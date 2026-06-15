using BusinessLogicTier;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_CategoryAdd : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    private const int MaxImageSizeBytes = 1048576; // 1 MB

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Enctype = "multipart/form-data";

        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loaddata();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    void loaddata()
    {
        DataTable dt = objState.getCategory();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtcountryname.Text = string.Empty;
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "clearAddPreview", "syncSlotPreview(categoryAddSlot, '', null);", true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtcountrynameedit.Text))
        {
            ShowAlert("Enter Category Name");
            return;
        }

        string oldImageFileName = hfEditCategoryImage.Value;
        string newImageFileName = string.Empty;
        string validationMessage = ValidateAndSaveCategoryImage(fuCategoryImageEdit, false, out newImageFileName);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            ShowAlert(validationMessage);
            return;
        }

        objState.CategoryName = txtcountrynameedit.Text.Trim();
        objState.CategoryId = lblcountryid.Text;
        if (!string.IsNullOrEmpty(newImageFileName))
        {
            objState.CategoryImage = newImageFileName;
        }

        string res = objState.Update_Category(objState);
        if (res == "t")
        {
            if (!string.IsNullOrEmpty(newImageFileName) && !string.IsNullOrEmpty(oldImageFileName) && !string.Equals(oldImageFileName, newImageFileName, StringComparison.OrdinalIgnoreCase))
            {
                DeleteSavedImage(oldImageFileName);
            }

            ShowAlert("Category Updated Successfully");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "closeModal", "closeCategoryEditModal();", true);
            loaddata();
        }
        else
        {
            if (!string.IsNullOrEmpty(newImageFileName))
            {
                DeleteSavedImage(newImageFileName);
            }
            ShowAlert("Unable to update category");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtcountryname.Text))
        {
            ShowAlert("Enter Category Name");
            return;
        }

        string imageFileName;
        string validationMessage = ValidateAndSaveCategoryImage(fuCategoryImage, true, out imageFileName);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            ShowAlert(validationMessage);
            return;
        }

        objState.CategoryName = txtcountryname.Text.Trim();
        objState.MentionBy = Session["useradmin"].ToString();
        objState.CategoryImage = imageFileName;
        string res = objState.Insert_Category(objState);
        if (res == "t")
        {
            ShowAlert("Category Added Successfully");
            txtcountryname.Text = string.Empty;
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "clearAddPreview", "syncSlotPreview(categoryAddSlot, '', null);", true);
            loaddata();
        }
        else
        {
            if (!string.IsNullOrEmpty(imageFileName))
            {
                DeleteSavedImage(imageFileName);
            }
            ShowAlert("Unable to save category. Please try again.");
        }
    }

    private string ValidateAndSaveCategoryImage(FileUpload fileUpload, bool required, out string imageFileName)
    {
        imageFileName = string.Empty;

        if (!fileUpload.HasFile)
        {
            return required ? "Please select a category image" : string.Empty;
        }

        string extension = Path.GetExtension(fileUpload.FileName).ToLowerInvariant();
        if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".svg" && extension != ".webp")
        {
            return "Only PNG, JPG, SVG and WebP images are allowed";
        }

        if (fileUpload.PostedFile.ContentLength > MaxImageSizeBytes)
        {
            return "Image size must be 1 MB or less";
        }

        string uploadFolder = Server.MapPath("~/img/");
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        imageFileName = DateTime.Now.Ticks + "_" + Path.GetFileName(fileUpload.FileName);
        fileUpload.SaveAs(Path.Combine(uploadFolder, imageFileName));
        return string.Empty;
    }

    private void DeleteSavedImage(string imageFileName)
    {
        if (string.IsNullOrWhiteSpace(imageFileName))
        {
            return;
        }

        try
        {
            string imagePath = Server.MapPath("~/img/" + imageFileName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        catch
        {
        }
    }

    private void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblCountryname = (Label)GridView1.Rows[index].FindControl("lblCountryname");
            Label lblCategoryImg = (Label)GridView1.Rows[index].FindControl("lblCategoryImg");
            Image imgCategory = (Image)GridView1.Rows[index].FindControl("imgCategory");

            lblcountryid.Text = lblid.Text;
            txtcountrynameedit.Text = lblCountryname.Text;
            hfEditCategoryImage.Value = lblCategoryImg != null ? lblCategoryImg.Text : string.Empty;

            string imageUrl = imgCategory != null ? imgCategory.ImageUrl : string.Empty;
            string script = string.Format("openCategoryEditModal('{0}');", imageUrl.Replace("'", "\\'"));
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openEditCategory", script, true);
        }
    }
}

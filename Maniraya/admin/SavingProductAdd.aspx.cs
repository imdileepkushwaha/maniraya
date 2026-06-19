using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SavingProductAdd : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objState = new clsProduct();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Enctype = "multipart/form-data";

        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        BindProductGrid();
    }

    void BindProductGrid()
    {
        SavingProductHelper.EnsureStatusColumn();
        GridView1.DataSource = SavingProductHelper.GetAllProducts();
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

        Image imgProduct = e.Row.FindControl("imgProduct") as Image;
        if (imgProduct != null)
        {
            imgProduct.ImageUrl = SavingProductHelper.GetImageUrl(Convert.ToString(row["ImageName"]));
        }

        LinkButton lnkToggle = e.Row.FindControl("lnkToggle") as LinkButton;
        if (lnkToggle != null)
        {
            bool isActive = Convert.ToBoolean(row["Status"]);
            lnkToggle.CommandName = isActive ? "deactivate" : "activate";
            lnkToggle.ToolTip = isActive ? "Deactivate product" : "Activate product";
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "edt", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        int index = Convert.ToInt32(e.CommandArgument);
        string productId = ((Label)GridView1.Rows[index].FindControl("lblid")).Text;

        int parsedId;
        if (!int.TryParse(productId, out parsedId))
        {
            ShowAlert("Unable to load product for edit.");
            return;
        }

        DataTable dt = SavingProductHelper.GetProductById(parsedId);
        if (dt == null || dt.Rows.Count == 0)
        {
            ShowAlert("Product not found.");
            return;
        }

        DataRow row = dt.Rows[0];
        lblproductid.Text = productId;
        txtproductnameedit.Text = Convert.ToString(row["ProductName"]);
        txtmrpedit.Text = Convert.ToString(row["MRP"]);
        txtdpedit.Text = Convert.ToString(row["DP"]);
        chkEditStatus.Checked = !row.Table.Columns.Contains("Status") || Convert.ToBoolean(row["Status"]);

        string imageName = Convert.ToString(row["ImageName"]);
        hfEditImage.Value = imageName;
        imgEditPreview.ImageUrl = SavingProductHelper.GetImageUrl(imageName);
        imgEditPreview.Visible = !string.IsNullOrWhiteSpace(imageName);

        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            UpdatePanel1.GetType(),
            "openSavingProductEditModal",
            "openSavingProductEditModal();",
            true);
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
            ShowAlert("Unable to update product status.");
            return;
        }

        bool activate = string.Equals(lnk.CommandName, "activate", StringComparison.OrdinalIgnoreCase);
        if (SavingProductHelper.SetProductStatus(id, activate))
        {
            ShowAlert(activate ? "Product activated successfully." : "Product deactivated successfully.");
            BindProductGrid();
            return;
        }

        ShowAlert("Unable to update product status.");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAddForm();
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        string imageName = SaveUploadedImage(FileUpload1, "noimage.png");
        if (imageName == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(imageName))
        {
            imageName = "noimage.png";
        }

        objState.ProductName = txtproductname.Text.Trim();
        objState.MRP = Convert.ToDecimal(txtmrp.Text);
        objState.DP = txtdp.Text.Trim();
        objState.ProductImage = imageName;
        objState.MentionBy = Session["useradmin"].ToString();

        string res = Insert_Product(objState);
        if (res == "t")
        {
            ShowAlert("Saving Product Added Successfully");
            ClearAddForm();
            BindProductGrid();
        }
        else if (res == "f")
        {
            ShowAlert("Product Already Exists");
        }
        else
        {
            ShowAlert("Unknown error occurred");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int productId;
        if (!int.TryParse(lblproductid.Text, out productId))
        {
            ShowAlert("Invalid product selected.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtproductnameedit.Text))
        {
            ShowAlert("Enter Product Name");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtmrpedit.Text))
        {
            ShowAlert("Enter MRP");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtdpedit.Text))
        {
            ShowAlert("Enter DP");
            return;
        }

        string imageName = hfEditImage.Value;
        string uploadedImage = SaveUploadedImage(FileUploadEdit, hfEditImage.Value);
        if (uploadedImage == null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(uploadedImage))
        {
            imageName = uploadedImage;
        }

        decimal mrp;
        if (!decimal.TryParse(txtmrpedit.Text.Trim(), out mrp))
        {
            ShowAlert("Enter valid MRP");
            return;
        }

        if (SavingProductHelper.UpdateProduct(productId, txtproductnameedit.Text.Trim(), mrp, txtdpedit.Text.Trim(), imageName, chkEditStatus.Checked))
        {
            ShowAlert("Saving Product Updated Successfully");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "closeSavingProductEditModal", "closeSavingProductEditModal();", true);
            BindProductGrid();
            return;
        }

        ShowAlert("Unable to update saving product.");
    }

    string SaveUploadedImage(FileUpload upload, string currentImageName)
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

        string fileName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + Path.GetFileName(upload.FileName);
        string folder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        upload.SaveAs(Path.Combine(folder, fileName));
        return fileName;
    }

    void ClearAddForm()
    {
        txtproductname.Text = string.Empty;
        txtmrp.Text = string.Empty;
        txtdp.Text = string.Empty;
    }

    public string Insert_Product(clsProduct objProduct)
    {
        string res = string.Empty;
        SqlConnection cn;
        SqlTransaction tr = null;
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            string s2 = "sp_add_SavingProductMaster";
            SqlParameter[] parameter = {
                new SqlParameter("@ProductName", objProduct.ProductName),
                new SqlParameter("@MRP", objProduct.MRP),
                new SqlParameter("@DP", objProduct.DP),
                new SqlParameter("@ImageName", objProduct.ProductImage),
                new SqlParameter("@EntryBy", objProduct.MentionBy),
            };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }

        return res;
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

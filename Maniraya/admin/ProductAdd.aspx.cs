using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class admin_ProductAdd : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                ProductWeightHelper.EnsureWeightColumn();
                if (txtWeight != null)
                {
                    txtWeight.Attributes["step"] = "any";
                    txtWeight.Attributes["min"] = "0";
                }
                loadCategory();
               
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadCategory()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCategory();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CategoryName";
        ddcountry.DataValueField = "CategoryId";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Category", "0");
        ddcountry.Items.Insert(0, li);
    }

    void loadSubCategory()
    {
        ddsubcategory.Items.Clear();
        DataTable dt = new DataTable();        
        dt = objState.getSubcategoryBycategoryid(ddcountry.SelectedValue);
        ddsubcategory.DataSource = dt;
        ddsubcategory.DataTextField = "SubCategoryName";
        ddsubcategory.DataValueField = "SubCategoryId";
        ddsubcategory.DataBind();
        ListItem li = new ListItem("Select SubCategory", "0");
        ddsubcategory.Items.Insert(0, li);
    }
    string SaveUploadedImage(FileUpload upload)
    {
        if (upload == null || !upload.HasFile)
        {
            return string.Empty;
        }

        string extension = Path.GetExtension(upload.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = ".jpg";
        }

        extension = extension.ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            return null;
        }

        string folder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + extension;
        upload.SaveAs(Path.Combine(folder, fileName));
        return fileName;
    }

    bool TryAssignUploadedImage(FileUpload upload, string label, out string imageName)
    {
        imageName = SaveUploadedImage(upload);
        if (imageName == null)
        {
            Message.Show(label + ": Please upload JPG, PNG, WEBP or GIF image only.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(imageName))
        {
            Message.Show("Upload " + label + " first.");
            return false;
        }

        return true;
    }

    public string UploadImage()
    {
        return SaveUploadedImage(ProductImageUpload);
    }
    public string UploadImage2()
    {
        return SaveUploadedImage(ProductImageUpload2);
    }
    public string UploadImage3()
    {
        return SaveUploadedImage(ProductImageUpload3);
    }
    public string UploadImage4()
    {
        return SaveUploadedImage(ProductImageUpload4);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        

    }
    public string Insert_Product(clsProduct objState)
    {
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_add_Product";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@CategoryID",objState.CategoryId),
                 new SqlParameter("@SubCategoryID",objState.SubCategoryId),
                new SqlParameter("@ProductName",objState.ProductName), 
                 new SqlParameter("@Amount",objState.Amount), 
                  new SqlParameter("@Description",objState.Description), 
                new SqlParameter("@MentionBy",objState.MentionBy),
                 new SqlParameter("@ProductImage",objState.ProductImage ?? string.Empty),
                   new SqlParameter("@ProductImage2",objState.ProductImage2 ?? string.Empty),
                     new SqlParameter("@ProductImage3",objState.ProductImage3 ?? string.Empty),
                       new SqlParameter("@ProductImage4",objState.ProductImage4 ?? string.Empty),
                      new SqlParameter("@BV",objState.BV),
                        new SqlParameter("@GST",objState.GST),
                          new SqlParameter("@HSNCODE",objState.HSNCODE ?? string.Empty),
                            new SqlParameter("@BATCHNO",objState.BATCHNO ?? string.Empty),
                        new SqlParameter("@DP",objState.DP ?? string.Empty),
                       new SqlParameter("@MRP",objState.MRP),
                        new SqlParameter("@SubcategoryArray",objState.SubproductArray ?? string.Empty),
                        new SqlParameter("@AdditionalInfo", TxtDescription.Content ?? string.Empty)
                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
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
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        string imageName;
        if (!TryAssignUploadedImage(ProductImageUpload, "Image 1", out imageName))
        {
            return;
        }
        objState.ProductImage = imageName;

        if (!TryAssignUploadedImage(ProductImageUpload2, "Image 2", out imageName))
        {
            return;
        }
        objState.ProductImage2 = imageName;

        if (!TryAssignUploadedImage(ProductImageUpload3, "Image 3", out imageName))
        {
            return;
        }
        objState.ProductImage3 = imageName;

        if (!TryAssignUploadedImage(ProductImageUpload4, "Image 4", out imageName))
        {
            return;
        }
        objState.ProductImage4 = imageName;

        if(GridView2.Rows.Count==0)
        {
            Message.Show("Select Subcategory where size color exists");
            return;
        }
        objState.CategoryId = ddcountry.SelectedValue.ToString();
        objState.SubCategoryId = ddsubcategory.SelectedValue.ToString();
        objState.ProductName = txtstatename.Text.Trim();
        objState.HSNCODE = txtHSN.Text.Trim();
        objState.BATCHNO = txtBatch.Text.Trim();
        objState.Amount = Convert.ToDecimal(TxtAmount.Text);
        objState.MRP = Convert.ToDecimal(TxtMRP.Text);
        objState.GST = Convert.ToDecimal(txtGst.Text);
        objState.Description = Txtshortdiscription.Text.Trim();
        objState.BV = Convert.ToDecimal(TxtBV.Text);
        objState.DP = Convert.ToString(Convert.ToDecimal(TxtDP.Text));
        decimal weightGrams = ProductWeightHelper.ParseGrams(txtWeight.Text);
        objState.SubproductArray = getoperatorpermission();
        if (objState.SubproductArray == "")
        {
            Message.Show("select any size and color");
            return;
        }
        objState.MentionBy = Session["useradmin"].ToString();
        string res = Insert_Product(objState);
        if (res == "t")
        {
            ProductWeightHelper.SaveByLatestProduct(objState.ProductName, objState.CategoryId, objState.SubCategoryId, weightGrams);
            string popupScript = "alert('Product Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtstatename.Text = ""; ddcountry.SelectedValue = "0"; TxtAmount.Text = ""; TxtDescription.Content = String.Empty; TxtBV.Text = string.Empty;
            txtWeight.Text = "";

        }
        else if (res == "f")
        {
            string popupScript = "alert('Product Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('Unknown error occurred. Please verify all fields and try again.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }     
    }


    protected void ddsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        DataTable dt = new DataTable();
        dt = objState.getSubcategorySetting(ddsubcategory.SelectedValue);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubCategory();
    }
    private string getoperatorpermission()
    {
        int i = 0;
        string operatorpermission = "";
        foreach (GridViewRow r in GridView2.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            if (ChStatus.Checked == true)
            {
                if (i == 0)
                {
                    operatorpermission += lbllevel.Text;
                }
                else
                {
                    operatorpermission += "," + lbllevel.Text;
                }
            }
            else
            {

            }
            i++;
        }
        return operatorpermission;
    }
}
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
    public string UploadImage()
    {
        string Imagename = "";
        if (ProductImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload.PostedFile.FileName);
            Imagename = RandomNumber+ fileName;
            ProductImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);
            
        }
        return Imagename;
    }
    public string UploadImage2()
    {
        string Imagename = "";
        if (ProductImageUpload2.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload2.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ProductImageUpload2.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
    public string UploadImage3()
    {
        string Imagename = "";
        if (ProductImageUpload3.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload3.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ProductImageUpload3.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
    public string UploadImage4()
    {
        string Imagename = "";
        if (ProductImageUpload4.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload4.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ProductImageUpload4.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
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
                 new SqlParameter("@SubCategoryID",objState.CategoryId),
                new SqlParameter("@ProductName",objState.ProductName), 
                 new SqlParameter("@Amount",objState.Amount), 
                  new SqlParameter("@Description",objState.Description), 
                new SqlParameter("@MentionBy",objState.MentionBy),
                 new SqlParameter("@ProductImage",objState.ProductImage),
                   new SqlParameter("@ProductImage2",objState.ProductImage2),
                     new SqlParameter("@ProductImage3",objState.ProductImage3),
                       new SqlParameter("@ProductImage4",objState.ProductImage4),
                      new SqlParameter("@BV",objState.BV),
                        new SqlParameter("@GST",objState.GST),
                          new SqlParameter("@HSNCODE",objState.HSNCODE),
                            new SqlParameter("@BATCHNO",objState.BATCHNO),
                        new SqlParameter("@DP",objState.DP),
                       new SqlParameter("@MRP",objState.MRP),
                        new SqlParameter("@SubcategoryArray",objState.SubproductArray)
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
        objState.ProductImage = UploadImage();
        objState.ProductImage2 = UploadImage2();
        objState.ProductImage3 = UploadImage3();
        objState.ProductImage4 = UploadImage4();
        if(objState.ProductImage == "")
        {
            Message.Show("Upload Image first");
            return;
        }
        if (objState.ProductImage2 == "")
        {
            Message.Show("Upload Image Second");
            return;
        }
        if (objState.ProductImage3 == "")
        {
            Message.Show("Upload Image Third");
            return;
        }
        if (objState.ProductImage4 == "")
        {
            Message.Show("Upload Image Fourth");
            return;
        }
        if(GridView2.Rows.Count==0)
        {
            Message.Show("Select Subcategory where size color exixts");
            return;
        }
        objState.CategoryId = ddcountry.SelectedValue.ToString();
        objState.ProductName = txtstatename.Text;
        objState.HSNCODE = txtHSN.Text;
        objState.BATCHNO = txtBatch.Text;
        objState.Amount = Convert.ToDecimal(TxtAmount.Text);
        objState.MRP = Convert.ToDecimal(TxtMRP.Text);
        objState.GST = Convert.ToDecimal(txtGst.Text);
        objState.Description = Txtshortdiscription.Text;
        objState.BV = Convert.ToInt32(TxtBV.Text);
        objState.DP = Convert.ToString(Convert.ToDecimal(TxtDP.Text));
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
            string popupScript = "alert('Product Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtstatename.Text = ""; ddcountry.SelectedValue = "0"; TxtAmount.Text = ""; TxtDescription.Content = String.Empty; TxtBV.Text = string.Empty;

        }
        else
            if (res == "f")
            {
                string popupScript = "alert('Product Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
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
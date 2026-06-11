using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComingProductAdd : System.Web.UI.Page
{
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        

    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        objState.ProductImage = UploadImage();
        objState.ProductImage2 = UploadImage2();
        objState.ProductImage3 = UploadImage3();
        objState.CategoryId = ddcountry.SelectedValue.ToString();
        objState.ProductName = txtstatename.Text;
        objState.Amount = Convert.ToDecimal(TxtAmount.Text);
        objState.MRP = Convert.ToDecimal(TxtMRP.Text);
        objState.Description = TxtDescription.Content;
        objState.BV = Convert.ToInt32(TxtBV.Text);
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_ComingProduct(objState);
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
   
}
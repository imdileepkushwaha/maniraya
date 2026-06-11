using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComingProductDetails : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                
                loadProduct();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    
    void loadProduct()
    {
        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.ProductId = string.Empty;
        if (TxtProductNameSearch.Text != string.Empty)
        {
            objState.ProductName = TxtProductNameSearch.Text;
        }
        if (ddstatus.SelectedIndex != 0)
        {
            objState.Status = ddstatus.SelectedValue;
        }
       
        if (TxtProductCodeSearch.Text != string.Empty)
        {
            objState.ProductId = TxtProductCodeSearch.Text;
        }
        dt = objState.getComingProductAll(objState);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public string UploadImage()
    {
        string Imagename = "";
        if (ProductImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objState.ProductImage = UploadImage();
        objState.ProductImage2 = UploadImage2();
        objState.ProductImage3 = UploadImage3();
        objState.ProductName = txtstatenameedit.Text;
        objState.ProductId = lblstateid.Text;
        objState.Description = TxtDescription.Content;
        objState.Amount = Convert.ToDecimal(TxtAmountEdit.Text);
        objState.Status = DDLstStatusEdit.SelectedValue;
        objState.BV = Convert.ToInt32(TxtBV.Text);
        objState.MRP = Convert.ToDecimal(TxtMrp.Text);
        string res = objState.Update_ComingProduct(objState);
        if (res == "t")
        {
            string popupScript = "alert('Product Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadProduct();
        }
    }
   
  
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblstatename = (Label)GridView1.Rows[index].FindControl("lblstatename");
            Label lblAmount = (Label)GridView1.Rows[index].FindControl("lblstatename1");
            Label LblDescription = (Label)GridView1.Rows[index].FindControl("LblDescription");
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            Label LblImage2 = (Label)GridView1.Rows[index].FindControl("LblImage2");
            Label LblImage3 = (Label)GridView1.Rows[index].FindControl("LblImage3");
            Label LblStatus = (Label)GridView1.Rows[index].FindControl("LblStatuschk");
            Label LblBV = (Label)GridView1.Rows[index].FindControl("lblbv");
            Label Lblmrp = (Label)GridView1.Rows[index].FindControl("lblMRP");
            DDLstStatusEdit.SelectedValue = LblStatus.Text;
            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;
            TxtAmountEdit.Text = lblAmount.Text;
            TxtBV.Text = LblBV.Text;
            TxtDescription.Content = LblDescription.Text;
            TxtMrp.Text = Lblmrp.Text;
            if (LblImage.Text != "../ProductImage/" && LblImage.Text != "")
            {
                Image2.ImageUrl = LblImage.Text;
            }
            else
            {
                Image2.ImageUrl = "../ProductImage/images.png";
            }
            ////////////////////////////////////////////////////////////////////////
            if (LblImage2.Text != "../ProductImage/" && LblImage2.Text != "")
            {
                Image3.ImageUrl = LblImage2.Text;
            }
            else
            {
                Image3.ImageUrl = "../ProductImage/images.png";
            }
            ////////////////////////////////////////////////////////////////////////
            if (LblImage3.Text != "../ProductImage/" && LblImage3.Text != "")
            {
                Image4.ImageUrl = LblImage3.Text;
            }
            else
            {
                Image4.ImageUrl = "../ProductImage/images.png";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        if (e.CommandName == "photolarge")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            Label LblImage2 = (Label)GridView1.Rows[index].FindControl("LblImage2");
            Label LblImage3 = (Label)GridView1.Rows[index].FindControl("LblImage3");
            if (LblImage.Text != "../ProductImage/" && LblImage.Text != "")
            {
                ImageLarge.ImageUrl = LblImage.Text;
            }
            else
            {
                ImageLarge.ImageUrl = "../ProductImage/images.png";
            }
            if (LblImage2.Text != "../ProductImage/" && LblImage2.Text != "")
            {
                ImageLarge2.ImageUrl = LblImage2.Text;
            }
            else
            {
                ImageLarge2.ImageUrl = "../ProductImage/images.png";
            }
            if (LblImage3.Text != "../ProductImage/" && LblImage3.Text != "")
            {
                ImageLarge3.ImageUrl = LblImage3.Text;
            }
            else
            {
                ImageLarge3.ImageUrl = "../ProductImage/images.png";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }
        if (e.CommandName == "del")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            clsProduct obj = new clsProduct();
            string res = obj.DeleteComingProducts(Convert.ToInt16(lblid.Text));
            if (res == "t")
            {
                loadProduct();
                string popupScript = "alert('Product Deleted Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Product Delete Failed');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        loadProduct();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image image = e.Row.FindControl("Image1") as Image;
            if (!File.Exists(Server.MapPath(image.ImageUrl)))
            {
                image.ImageUrl = "../ProductImage/images.png";
            }
        }
    }
}
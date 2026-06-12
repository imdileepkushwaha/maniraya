using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;
using System.Data.SqlClient;

public partial class admin_ProductDetails : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objState = new clsProduct();

    private DataTable ProductData
    {
        get { return ViewState["ProductData"] as DataTable; }
        set { ViewState["ProductData"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadProduct(false);
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    void loadProduct(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

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

        ProductData = objState.getProductAll(objState);
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = ProductData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 10;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;

            if (dt.Rows.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 10;
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (ProductData != null)
        {
            BindGrid();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
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
        objState.BV = Convert.ToDecimal(TxtBV.Text);
        objState.MRP = Convert.ToDecimal(TxtMrp.Text);
        objState.HSNCODE = TxtHsncode.Text;
        objState.BATCHNO = Txtbatchno.Text;
        objState.CouponCode = Convert.ToString(Math.Round(Convert.ToDecimal(TXTDP.Text),0));
        objState.GST = Convert.ToDecimal(txtGst.Text);
        string res = Update_Product(objState);
        if (res == "t")
        {
            string popupScript = "alert('Product Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadProduct(false);
        }
    }

    public string Update_Product(clsProduct objState)
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

            s2 = "Select * from ProductMaster where ProductId='" + objState.ProductId + "' ";//and PurchaseStatus='0'";
            DataSet Ds = ObjData.RunSelectQueryTrans(s2, tr);
            if (Ds.Tables[0].Rows.Count == 1)
            {
                s2 = "update ProductMaster set ProductName='" + objState.ProductName + "',GST='"+objState.GST+"',Description='" + objState.Description + "',amount='" + objState.Amount + "',status='" + objState.Status + "',BV='" + objState.BV + "',MRP='" + objState.MRP + "',DP='" + objState.CouponCode + "',HSNCODE='" + objState.HSNCODE + "',BATCHNO='"+objState.BATCHNO+"'  where ProductId='" + objState.ProductId + "'";// and PurchaseStatus='0'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                if (objState.ProductImage != "")
                {
                    s2 = "update ProductMaster set ProductImage='" + objState.ProductImage + "' where ProductId='" + objState.ProductId + "'";// and PurchaseStatus='0'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }
                if (objState.ProductImage2 != "")
                {
                    s2 = "update ProductMaster set ProductImage2='" + objState.ProductImage2 + "' where ProductId='" + objState.ProductId + "'";// and PurchaseStatus='0'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }
                if (objState.ProductImage3 != "")
                {
                    s2 = "update ProductMaster set ProductImage3='" + objState.ProductImage3 + "' where ProductId='" + objState.ProductId + "'";// and PurchaseStatus='0'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
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

            Label lblstatenameGST = (Label)GridView1.Rows[index].FindControl("lblstatenameGST");
            Label lblstatename2 = (Label)GridView1.Rows[index].FindControl("lblstatename2");
            Label LblHSNcode = (Label)GridView1.Rows[index].FindControl("LblHSNcode");
            Label LBLBatchno = (Label)GridView1.Rows[index].FindControl("LBLBatchno");
            if (!string.IsNullOrEmpty(LblStatus.Text) && DDLstStatusEdit.Items.FindByValue(LblStatus.Text) != null)
            {
                DDLstStatusEdit.SelectedValue = LblStatus.Text;
            }
            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;
            TxtAmountEdit.Text = lblAmount.Text;
            TxtBV.Text = LblBV.Text;
            TxtDescription.Content = LblDescription.Text;
            TxtMrp.Text = Lblmrp.Text;
            TXTDP.Text = lblstatename2.Text;
            txtGst.Text = lblstatenameGST.Text;
            TxtHsncode.Text = LblHSNcode.Text;
            Txtbatchno.Text = LBLBatchno.Text;

            string img1 = GetEditImageUrl(LblImage.Text);
            string img2 = GetEditImageUrl(LblImage2.Text);
            string img3 = GetEditImageUrl(LblImage3.Text);

            Image2.ImageUrl = string.IsNullOrEmpty(img1) ? "../ProductImage/images.png" : img1;
            Image3.ImageUrl = string.IsNullOrEmpty(img2) ? "../ProductImage/images.png" : img2;
            Image4.ImageUrl = string.IsNullOrEmpty(img3) ? "../ProductImage/images.png" : img3;

            string popupScript = string.Format(
                "syncEditProductImages('{0}','{1}','{2}'); showModal();",
                JsEncode(img1),
                JsEncode(img2),
                JsEncode(img3));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", popupScript, true);
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
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        loadProduct(true);
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

    string GetEditImageUrl(string url)
    {
        if (string.IsNullOrEmpty(url) || url == "../ProductImage/" || url.IndexOf("images.png", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return string.Empty;
        }

        return url;
    }

    string JsEncode(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return value.Replace("\\", "\\\\").Replace("'", "\\'");
    }
}
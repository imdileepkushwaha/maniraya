using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Productdetail : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    private int PageSize = 12;
    DataTable PurchaseDt;
    Decimal TAmt = 0;

    static readonly object[][] StaticProducts = new object[][]
    {
        new object[] { 1001, "Smart Watch Series X3", "Electronics", 16999m, 21999m, "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1002, "Slim 3 Intel Core i5 Laptop", "Electronics", 58999m, 64999m, "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1003, "Wireless Earbuds Pro", "Electronics", 2499m, 3999m, "https://images.unsplash.com/photo-1590658268037-6bf12165a1df?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1004, "Classic Sneakers", "Fashion", 3299m, 4499m, "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1005, "Cotton Hoodie", "Fashion", 1899m, 2499m, "https://images.unsplash.com/photo-1578587018452-892bacefd3f2?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1006, "Leather Handbag", "Accessories", 4599m, 5999m, "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1007, "Skin Care Kit", "Beauty", 1299m, 1899m, "https://images.unsplash.com/photo-1556228720-195a672e8a03?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1008, "Hair Dryer Pro", "Beauty", 2199m, 2999m, "https://images.unsplash.com/photo-1522338140262-f46f5913618a?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1009, "Portable Laptop Table", "Home & Living", 3499m, 4299m, "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1010, "Kitchen Essentials Set", "Home & Living", 2799m, 3499m, "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1011, "Yoga Mat Premium", "Sports & Fitness", 999m, 1499m, "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?auto=format&fit=crop&w=600&q=80" },
        new object[] { 1012, "Organic Green Tea Pack", "Groceries", 449m, 599m, "https://images.unsplash.com/photo-1556678733-db22be894031?auto=format&fit=crop&w=600&q=80" }
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userid"] != null)
        //{
        if (!IsPostBack)
        {
            if (Session["returnUrl"] != null)
            {
                Session.Remove("returnUrl");

            }
            if (Request.QueryString["productid"] != null)
            {
                if (Request.QueryString["franchiseeid"] != null)
                {
                    hffranchiseeid.Value = Request.QueryString["franchiseeid"].ToString();
                }
                else if (string.IsNullOrWhiteSpace(hffranchiseeid.Value))
                {
                    hffranchiseeid.Value = "F000001";
                }

                string productId = Request.QueryString["productid"].ToString();
                getproductdetail(productId);
                loadColor(productId);
                loadsize(productId);
                getstockby(hffranchiseeid.Value, hfproductid.Value, hfColor.Value, hfSize.Value);
            }
            // loadProduct(1);
        }
        //}
        //else
        //{
        //    Response.Redirect("logout.aspx");
        //}
    }
    private void getproductdetail(string id)
    {
        objState.ProductId = id;
        DataTable Dt = objState.getProductByid(objState);
        if (Dt == null || Dt.Rows.Count == 0)
        {
            Dt = GetStaticProductDetail(id);
        }

        if (Dt == null || Dt.Rows.Count == 0)
        {
            lblProductName.Text = "Product not found";
            lblDescription.Text = "This product is unavailable right now.";
            return;
        }

        DataRow row = Dt.Rows[0];
        hfproductid.Value = row["ProductID"].ToString();
        lblSubCategory.Text = row["SubCategoryName"].ToString();
        lblProductName.Text = row["productname"].ToString();
        lblPrice.Text = row["Amount"].ToString();
        lblMRP.Text = row["MRP"].ToString();
        lblDescription.Text = row["Description"].ToString();
        LblBV.Text = row["BV"].ToString();
        Lblcategory.Text = row["CategoryName"].ToString();
        string productName = row["productname"].ToString();
        string mainImage = CatalogHelper.ResolveProductImageUrl(row["productImage"].ToString(), productName);
        Image1.ImageUrl = mainImage;
        Image2.ImageUrl = ResolveDetailImage(row["productImage2"].ToString(), mainImage, productName);
        Image3.ImageUrl = ResolveDetailImage(row["productImage3"].ToString(), mainImage, productName);
        Image4.ImageUrl = ResolveDetailImage(row["productImage4"].ToString(), mainImage, productName);
        Image5.ImageUrl = mainImage;
        Image5.CssClass = "pd-main-image";
    }

    string ResolveDetailImage(string imagePath, string fallbackImage, string productName)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return fallbackImage;
        }

        return CatalogHelper.ResolveProductImageUrl(imagePath, productName);
    }

    DataTable GetStaticProductDetail(string id)
    {
        int productId;
        if (!int.TryParse(id, out productId))
        {
            return null;
        }

        foreach (object[] item in StaticProducts)
        {
            if (Convert.ToInt32(item[0]) != productId)
            {
                continue;
            }

            string image = Convert.ToString(item[5]);
            string category = Convert.ToString(item[2]);
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(string));
            dt.Columns.Add("SubCategoryName", typeof(string));
            dt.Columns.Add("productname", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("MRP", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("BV", typeof(string));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("productImage", typeof(string));
            dt.Columns.Add("productImage2", typeof(string));
            dt.Columns.Add("productImage3", typeof(string));
            dt.Columns.Add("productImage4", typeof(string));

            DataRow row = dt.NewRow();
            row["ProductID"] = item[0].ToString();
            row["SubCategoryName"] = category;
            row["productname"] = item[1].ToString();
            row["Amount"] = item[3].ToString();
            row["MRP"] = item[4].ToString();
            row["Description"] = "Premium quality product from Maniraya with fast delivery and easy returns.";
            row["BV"] = "100";
            row["CategoryName"] = category;
            row["productImage"] = image;
            row["productImage2"] = image;
            row["productImage3"] = image;
            row["productImage4"] = image;
            dt.Rows.Add(row);
            return dt;
        }

        return null;
    }

    DataTable GetDefaultColors()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ColorId", typeof(string));
        dt.Columns.Add("ColorName", typeof(string));
        dt.Columns.Add("ColorCode", typeof(string));
        DataRow row = dt.NewRow();
        row["ColorId"] = "1";
        row["ColorName"] = "Default";
        row["ColorCode"] = "#e5a906";
        dt.Rows.Add(row);
        return dt;
    }

    DataTable GetDefaultSizes()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SizeID", typeof(string));
        dt.Columns.Add("SizeName", typeof(string));
        DataRow row = dt.NewRow();
        row["SizeID"] = "1";
        row["SizeName"] = "Standard";
        dt.Rows.Add(row);
        return dt;
    }

    void loadColor(string id)
    {
        objState.ProductId = id;
        DataTable Dt = objState.getColorByid(objState);
        if (Dt == null || Dt.Rows.Count == 0)
        {
            Dt = GetDefaultColors();
        }

        hfColor.Value = Dt.Rows[0]["ColorId"].ToString();
        rptColors.DataSource = Dt;
        rptColors.DataBind();
    }

    void loadsize(string id)
    {
        objState.ProductId = id;
        DataTable Dt = objState.getSizeyid(objState);
        if (Dt == null || Dt.Rows.Count == 0)
        {
            Dt = GetDefaultSizes();
        }

        hfSize.Value = Dt.Rows[0]["SizeID"].ToString();
        rptSizes.DataSource = Dt;
        rptSizes.DataBind();
    }

    private void getstockby(string franchiseeid, string productid, string color, string size)
    {
        clsProduct objproduct = new clsProduct();
        DataTable res = objproduct.getstockbysubproduct(franchiseeid, productid, color, size);
        int stock = 0;

        if (res != null && res.Rows.Count > 0)
        {
            int.TryParse(Convert.ToString(res.Rows[0][0]), out stock);
        }
        else if (!string.IsNullOrWhiteSpace(productid) && GetStaticProductDetail(productid) != null)
        {
            stock = 25;
        }

        lblstock.Text = stock > 0 ? "In Stock" : "out of Stock";
        hfstock.Value = stock.ToString();
    }

    protected void lnkaddtocart_Click(object sender, EventArgs e)
    {
        int stock;
        if (int.TryParse(hfstock.Value, out stock) && stock > 0)
        {
            if (Session["userid"] != null)
            {
                addtocart();
            }
            else
            {
                Session["returnUrl"] = "productdetail.aspx?productid=" + Request.QueryString["productid"].ToString()+ "&franchiseeid=" + hffranchiseeid.Value;
                Response.Redirect("login.aspx");

            }
        }
        else
        {

            Message.Show("Stock not available");
        }

    }
    private void addtocart()
    {
        objState.ProductId = hfproductid.Value;
        objState.colorId = hfColor.Value;
        objState.Sizeid = hfSize.Value;
        objState.UserId = Session["userid"].ToString();
        objState.FranchiseeID = hffranchiseeid.Value;
        string res = objState.addcartitem(objState);
        if (res == "S")
        {
            Response.Redirect("addtocart.aspx");
        }
    }
    [System.Web.Services.WebMethod]
    public static int GetStock(string franchiseeid, string color, string size, string productId)
    {
        clsProduct objproduct = new clsProduct();
        int stock = 0;
        DataTable res = objproduct.getstockbysubproduct(franchiseeid, productId, color, size);
        if (res != null && res.Rows.Count > 0)
        {
            int.TryParse(Convert.ToString(res.Rows[0][0]), out stock);
        }
        return stock;
    }

   
}
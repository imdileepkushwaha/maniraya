using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                ProductWeightHelper.EnsureWeightColumn();
                if (txtWeight != null)
                {
                    txtWeight.Attributes["step"] = "any";
                    txtWeight.Attributes["min"] = "0";
                }
                loadProduct(false);
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
        else if (ProductData != null)
        {
            BindGrid();
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
            pnlPager.Visible = false;
            pnlPager.Controls.Clear();
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
        BuildExternalPager();
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

    void BuildExternalPager()
    {
        pnlPager.Controls.Clear();

        DataTable dt = ProductData;
        if (!GridView1.AllowPaging || dt == null || dt.Rows.Count == 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
        int totalRecords = dt.Rows.Count;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = GridView1.PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"admin-pager-info\">Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("Prev", "nav_prev", currentPage - 1, currentPage > 0, false);
        AddPagerLink("First", "nav_first", 0, currentPage > 0, false);

        const int windowSize = 4;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            AddPagerEllipsis("ell_start");
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), "nav_page_" + i, i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            AddPagerEllipsis("ell_end");
        }

        AddPagerLink("Last", "nav_last", totalPages - 1, currentPage < totalPages - 1, false);
        AddPagerLink("Next", "nav_next", currentPage + 1, currentPage < totalPages - 1, false);
    }

    void AddPagerEllipsis(string controlId)
    {
        Literal ellipsis = new Literal();
        ellipsis.ID = controlId;
        ellipsis.Text = "<span class=\"admin-pager-btn is-ellipsis\">.....</span>";
        pnlPager.Controls.Add(ellipsis);
    }

    void AddPagerLink(string text, string controlId, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            Literal active = new Literal();
            active.ID = controlId + "_active";
            active.Text = "<span class=\"admin-pager-btn is-active\">" + text + "</span>";
            pnlPager.Controls.Add(active);
            return;
        }

        if (!enabled)
        {
            Literal disabled = new Literal();
            disabled.ID = controlId + "_disabled";
            disabled.Text = "<span class=\"admin-pager-btn is-disabled\">" + text + "</span>";
            pnlPager.Controls.Add(disabled);
            return;
        }

        LinkButton link = new LinkButton();
        link.ID = controlId;
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.CausesValidation = false;
        link.Click += ExternalPager_Click;
        pnlPager.Controls.Add(link);
    }

    protected void ExternalPager_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        int pageIndex;
        if (link != null && int.TryParse(link.CommandArgument, out pageIndex))
        {
            GridView1.PageIndex = pageIndex;
            BindGrid();
        }
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

    bool TrySaveOptionalUploadedImage(FileUpload upload, string label, out string imageName)
    {
        imageName = SaveUploadedImage(upload);
        if (imageName == null)
        {
            ShowAlert(label + ": Please upload JPG, PNG, WEBP or GIF image only.");
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        decimal amount;
        decimal bv;
        decimal mrp;
        decimal dp;
        decimal gst;

        if (!TryParseProductDecimal(TxtAmountEdit.Text, "CP", out amount))
        {
            return;
        }

        if (!TryParseProductDecimal(TxtBV.Text, "Business Volume", out bv))
        {
            return;
        }

        if (!TryParseProductDecimal(TxtMrp.Text, "MRP", out mrp))
        {
            return;
        }

        if (!TryParseProductDecimal(TXTDP.Text, "DP", out dp))
        {
            return;
        }

        if (!TryParseProductDecimal(txtGst.Text, "GST", out gst))
        {
            return;
        }

        string imageName;
        if (!TrySaveOptionalUploadedImage(ProductImageUpload, "Image 1", out imageName))
        {
            return;
        }
        objState.ProductImage = imageName;

        if (!TrySaveOptionalUploadedImage(ProductImageUpload2, "Image 2", out imageName))
        {
            return;
        }
        objState.ProductImage2 = imageName;

        if (!TrySaveOptionalUploadedImage(ProductImageUpload3, "Image 3", out imageName))
        {
            return;
        }
        objState.ProductImage3 = imageName;

        if (!TrySaveOptionalUploadedImage(ProductImageUpload4, "Image 4", out imageName))
        {
            return;
        }
        objState.ProductImage4 = imageName;

        objState.ProductName = txtstatenameedit.Text.Trim();
        objState.ProductId = lblstateid.Text.Trim();
        objState.Description = Txtshortdiscription.Text.Trim();
        objState.Amount = amount;
        objState.Status = DDLstStatusEdit.SelectedValue;
        objState.BV = bv;
        objState.MRP = mrp;
        objState.HSNCODE = TxtHsncode.Text.Trim();
        objState.BATCHNO = Txtbatchno.Text.Trim();
        objState.CouponCode = Convert.ToString(Math.Round(dp, 0));
        objState.GST = gst;
        decimal weightGrams = ProductWeightHelper.ParseGrams(txtWeight.Text);
        string res = Update_Product(objState);
        ProductWeightHelper.SaveByProductId(objState.ProductId, weightGrams);

        string fullDesc = (TxtDescription.Content ?? string.Empty).Replace("'", "''");
        string sqlUpdateAddInfo = "update ProductMaster set additionalinfo='" + fullDesc + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
        ObjData.StartConnection();
        try {
            ObjData.RunInsUpDelQuery(sqlUpdateAddInfo);
        } finally {
            ObjData.EndConnection();
        }

        if (res == "t")
        {
            string popupScript = "alert('Product Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loadProduct(false);
        }
        else
        {
            ShowAlert("Unable to update product. Please verify the details and try again.");
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
                s2 = "update ProductMaster set ProductName='" + SqlEscape(objState.ProductName)
                    + "',GST='" + objState.GST
                    + "',Description='" + SqlEscape(objState.Description)
                    + "',amount='" + objState.Amount
                    + "',status='" + SqlEscape(objState.Status)
                    + "',BV='" + objState.BV
                    + "',MRP='" + objState.MRP
                    + "',DP='" + SqlEscape(objState.CouponCode)
                    + "',HSNCODE='" + SqlEscape(objState.HSNCODE)
                    + "',BATCHNO='" + SqlEscape(objState.BATCHNO)
                    + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
                ObjData.RunInsUpDelQueryTrans(s2, tr);
                if (!string.IsNullOrWhiteSpace(objState.ProductImage))
                {
                    s2 = "update ProductMaster set ProductImage='" + SqlEscape(objState.ProductImage) + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }
                if (!string.IsNullOrWhiteSpace(objState.ProductImage2))
                {
                    s2 = "update ProductMaster set ProductImage2='" + SqlEscape(objState.ProductImage2) + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }
                if (!string.IsNullOrWhiteSpace(objState.ProductImage3))
                {
                    s2 = "update ProductMaster set ProductImage3='" + SqlEscape(objState.ProductImage3) + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }
                if (!string.IsNullOrWhiteSpace(objState.ProductImage4))
                {
                    s2 = "update ProductMaster set ProductImage4='" + SqlEscape(objState.ProductImage4) + "' where ProductId='" + SqlEscape(objState.ProductId) + "'";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                }

                res = "t";
                tr.Commit();
            }
            else
            {
                res = "0";
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
            Label LblAdditionalInfo = (Label)GridView1.Rows[index].FindControl("LblAdditionalInfo");
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            Label LblImage2 = (Label)GridView1.Rows[index].FindControl("LblImage2");
            Label LblImage3 = (Label)GridView1.Rows[index].FindControl("LblImage3");
            Label LblImage4 = (Label)GridView1.Rows[index].FindControl("LblImage4");
            Label LblStatus = (Label)GridView1.Rows[index].FindControl("LblStatuschk");
            Label LblBV = (Label)GridView1.Rows[index].FindControl("lblbv");
            Label Lblmrp = (Label)GridView1.Rows[index].FindControl("lblMRP");

            Label lblstatenameGST = (Label)GridView1.Rows[index].FindControl("lblstatenameGST");
            Label lblstatename2 = (Label)GridView1.Rows[index].FindControl("lblstatename2");
            Label LblHSNcode = (Label)GridView1.Rows[index].FindControl("LblHSNcode");
            Label LBLBatchno = (Label)GridView1.Rows[index].FindControl("LBLBatchno");
            Label LblWeight = (Label)GridView1.Rows[index].FindControl("LblWeight");
            if (!string.IsNullOrEmpty(LblStatus.Text) && DDLstStatusEdit.Items.FindByValue(LblStatus.Text) != null)
            {
                DDLstStatusEdit.SelectedValue = LblStatus.Text;
            }
            lblstateid.Text = lblid.Text;
            txtstatenameedit.Text = lblstatename.Text;
            TxtAmountEdit.Text = FormatDecimalInput(lblAmount.Text);
            TxtBV.Text = FormatDecimalInput(LblBV.Text);
            Txtshortdiscription.Text = LblDescription.Text;
            TxtDescription.Content = LblAdditionalInfo.Text;
            TxtMrp.Text = FormatDecimalInput(Lblmrp.Text);
            TXTDP.Text = FormatDecimalInput(lblstatename2.Text);
            txtGst.Text = FormatDecimalInput(lblstatenameGST.Text);
            TxtHsncode.Text = LblHSNcode.Text;
            Txtbatchno.Text = LBLBatchno.Text;
            txtWeight.Text = FormatDecimalInput(LblWeight != null ? LblWeight.Text : "0");

            string img1 = GetEditImageUrl(LblImage.Text);
            string img2 = GetEditImageUrl(LblImage2.Text);
            string img3 = GetEditImageUrl(LblImage3.Text);
            string img4 = GetEditImageUrl(LblImage4.Text);

            Image2.ImageUrl = string.IsNullOrEmpty(img1) ? "../ProductImage/images.png" : img1;
            Image3.ImageUrl = string.IsNullOrEmpty(img2) ? "../ProductImage/images.png" : img2;
            Image4.ImageUrl = string.IsNullOrEmpty(img3) ? "../ProductImage/images.png" : img3;
            Image5.ImageUrl = string.IsNullOrEmpty(img4) ? "../ProductImage/images.png" : img4;

            string popupScript = string.Format(
                "syncEditProductImages('{0}','{1}','{2}','{3}'); showModal();",
                JsEncode(img1),
                JsEncode(img2),
                JsEncode(img3),
                JsEncode(img4));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", popupScript, true);
        }
        if (e.CommandName == "photolarge")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            Label LblImage2 = (Label)GridView1.Rows[index].FindControl("LblImage2");
            Label LblImage3 = (Label)GridView1.Rows[index].FindControl("LblImage3");
            Label LblImage4 = (Label)GridView1.Rows[index].FindControl("LblImage4");
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
            if (LblImage4.Text != "../ProductImage/" && LblImage4.Text != "")
            {
                ImageLarge4.ImageUrl = LblImage4.Text;
            }
            else
            {
                ImageLarge4.ImageUrl = "../ProductImage/images.png";
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

    private static decimal ParseGridDecimal(string value)
    {
        decimal amount;
        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
        {
            return amount;
        }

        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out amount))
        {
            return amount;
        }

        return 0;
    }

    private static string FormatDecimalInput(string value)
    {
        return ParseGridDecimal(value).ToString(CultureInfo.InvariantCulture);
    }

    private bool TryParseProductDecimal(string text, string fieldName, out decimal value)
    {
        value = 0;
        string trimmed = (text ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(trimmed))
        {
            ShowAlert("Enter " + fieldName);
            return false;
        }

        if (decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
            || decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
        {
            return true;
        }

        ShowAlert("Enter valid " + fieldName);
        return false;
    }

    private void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}
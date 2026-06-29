using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;
using System.Data.SqlClient;
using DataTier;
using System.Text;

public partial class user_SavingProductPurchase : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsProduct objproduct = new clsProduct();
    clsState objState = new clsState();

    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadprevproduct();
                LoadShippingAddress();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT sd.productid, pd.productname, pd.ImageName, pd.MRP, pd.DP
            FROM SavingMonthlyProductDetail sd WITH (nolock)
            LEFT JOIN SavingProductMaster pd WITH (nolock) ON sd.productid = pd.id
            WHERE sd.Status = 1";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }

    void loadprevproduct()
    {
        DataTable dt = getPrevProduct();
        if (dt == null || dt.Rows.Count == 0)
        {
            imgProduct.ImageUrl = ResolveUrl("~/ProductImage/noimage.png");
            imgProduct.CssClass = "saving-product-showcase-img";
            litProductName.Text = "Saving Product";
            litMrp.Text = "0.00";
            litDp.Text = "0.00";
            litAmount.Text = "₹ 0.00";
            return;
        }

        DataRow row = dt.Rows[0];
        string productName = Convert.ToString(row["productname"]);
        string imageName = Convert.ToString(row["ImageName"]);
        decimal mrp = 0m;
        decimal dp = 0m;

        decimal.TryParse(Convert.ToString(row["MRP"]), out mrp);
        decimal.TryParse(Convert.ToString(row["DP"]), out dp);

        txtproductname.Text = productName;
        litProductName.Text = string.IsNullOrWhiteSpace(productName) ? "Saving Product" : productName;
        litMrp.Text = mrp.ToString("N2");
        litDp.Text = dp.ToString("N2");

        decimal amount = dp > 0 ? dp : mrp;
        txtamount.Text = amount.ToString("0.##");
        litAmount.Text = "₹ " + amount.ToString("N2");

        string imageUrl = string.IsNullOrWhiteSpace(imageName)
            ? ResolveUrl("~/ProductImage/noimage.png")
            : ResolveUrl("~/ProductImage/" + imageName);
        imgProduct.ImageUrl = imageUrl;
        imgProduct.CssClass = "saving-product-showcase-img";
    }

    public string UploadImage()
    {
        if (!ImageUpload.HasFile)
        {
            return string.Empty;
        }

        string extension = Path.GetExtension(ImageUpload.PostedFile.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = ".jpg";
        }

        extension = extension.ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp" && extension != ".gif")
        {
            return string.Empty;
        }

        string uploadFolder = Server.MapPath("~/ProductImage/");
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string imagename = DateTime.Now.Ticks + extension;
        ImageUpload.PostedFile.SaveAs(Path.Combine(uploadFolder, imagename));
        return imagename;
    }

    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
        LoadShippingAddress();
    }

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            objaccount.UserId = txtuserid.Text;
            DataTable dtrechrge = objaccount.getUserWalletBalanceReportrechargewallet(objaccount);
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }

    DataTable GetUserAddressDetail(string userId)
    {
        DataTable dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            SqlParameter[] parameter = {
                new SqlParameter("@UserId", userId)
            };
            dt = ObjData.RunDataTableProcedure("sp_getuseraddressdetail", parameter);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt;
    }

    void LoadShippingAddress()
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            ShowShippingEmpty(false);
            return;
        }

        DataRow row = dt.Rows[0];

        bool hasShipping = HasCompleteShippingAddress(row);
        bool hasProfile = HasCompleteProfileAddress(row);

        btnUseProfileShipping.Visible = !hasShipping && hasProfile;
        btnUseProfileFromEmpty.Visible = hasProfile;

        if (hasShipping)
        {
            BindShippingView(row, true);
            ShowShippingView();
            return;
        }

        if (hasProfile)
        {
            BindShippingView(row, false);
            ShowShippingView();
            return;
        }

        ShowShippingEmpty(hasProfile);
    }

    void BindShippingView(DataRow row, bool isShippingAddress)
    {
        lblShippingSource.Visible = true;
        if (isShippingAddress)
        {
            lblShippingSource.Text = "Saved Shipping";
            litShippingAddress.Text = "<p class=\"saving-shipping-address-text\">" +
                Server.HtmlEncode(FormatAddress(
                    GetValue(row, "Shippingaddress"),
                    GetValue(row, "ShippingAreaName"),
                    GetValue(row, "ShippingCity"),
                    GetValue(row, "ShippingState"),
                    GetValue(row, "ShippingPincode"))) + "</p>";
            return;
        }

        lblShippingSource.Text = "Profile Address";
        litShippingAddress.Text = "<p class=\"saving-shipping-address-text\">" +
            Server.HtmlEncode(FormatAddress(
                GetValue(row, "Address"),
                GetValue(row, "AreaName"),
                GetValue(row, "Cityname"),
                GetValue(row, "Statename"),
                GetValue(row, "Pincode"))) + "</p>" +
            "<p class=\"saving-shipping-empty-text\">This is your profile address. Confirm it for shipping or enter a different delivery address.</p>";
    }

    void ShowShippingView()
    {
        hfShippingMode.Value = "view";
        pnlShippingView.Visible = true;
        pnlShippingEmpty.Visible = false;
        pnlShippingEdit.Visible = false;
    }

    void ShowShippingEmpty(bool hasProfileAddress)
    {
        hfShippingMode.Value = "empty";
        pnlShippingView.Visible = false;
        pnlShippingEmpty.Visible = true;
        pnlShippingEdit.Visible = false;
        btnUseProfileFromEmpty.Visible = hasProfileAddress;
    }

    void ShowShippingEdit(DataRow row, bool useShippingFields)
    {
        hfShippingMode.Value = "edit";
        pnlShippingView.Visible = false;
        pnlShippingEmpty.Visible = false;
        pnlShippingEdit.Visible = true;

        LoadShipStates();

        if (row == null)
        {
            txtShipAddress.Text = string.Empty;
            txtShipArea.Text = string.Empty;
            txtShipPincode.Text = string.Empty;
            return;
        }

        if (useShippingFields && HasCompleteShippingAddress(row))
        {
            txtShipAddress.Text = GetValue(row, "Shippingaddress");
            txtShipArea.Text = GetValue(row, "ShippingAreaName");
            txtShipPincode.Text = GetValue(row, "ShippingPincode");
            SetDropDownValue(ddShipState, GetValue(row, "Shippingstateid"));
            LoadShipCities();
            SetDropDownValue(ddShipCity, GetValue(row, "ShippingCityId"));
            return;
        }

        txtShipAddress.Text = GetValue(row, "Address");
        txtShipArea.Text = GetValue(row, "AreaName");
        txtShipPincode.Text = GetValue(row, "Pincode");
        SetDropDownValue(ddShipState, GetValue(row, "stateid"));
        LoadShipCities();
        SetDropDownValue(ddShipCity, GetValue(row, "CityId"));
    }

    void LoadShipStates()
    {
        ddShipState.Items.Clear();
        objState.CountryId = "1";
        DataTable dt = objState.getState(objState);
        ddShipState.DataSource = dt;
        ddShipState.DataTextField = "StateName";
        ddShipState.DataValueField = "StateID";
        ddShipState.DataBind();
        ddShipState.Items.Insert(0, new ListItem("Select State", "0"));
    }

    void LoadShipCities()
    {
        ddShipCity.Items.Clear();
        if (ddShipState.SelectedValue == "0")
        {
            ddShipCity.Items.Insert(0, new ListItem("Select City", "0"));
            return;
        }

        objState.StateId = ddShipState.SelectedValue;
        DataTable dt = objState.getCity(objState);
        ddShipCity.DataSource = dt;
        ddShipCity.DataTextField = "CityName";
        ddShipCity.DataValueField = "CityID";
        ddShipCity.DataBind();
        ddShipCity.Items.Insert(0, new ListItem("Select City", "0"));
    }

    protected void ddShipState_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadShipCities();
    }

    protected void btnAddShipping_Click(object sender, EventArgs e)
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        DataRow row = dt != null && dt.Rows.Count > 0 ? dt.Rows[0] : null;
        ShowShippingEdit(row, false);
    }

    protected void btnChangeShipping_Click(object sender, EventArgs e)
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            ShowShippingEmpty(false);
            return;
        }

        DataRow row = dt.Rows[0];
        ShowShippingEdit(row, HasCompleteShippingAddress(row));
    }

    protected void btnCancelShipping_Click(object sender, EventArgs e)
    {
        LoadShippingAddress();
    }

    protected void btnUseProfileShipping_Click(object sender, EventArgs e)
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (dt == null || dt.Rows.Count == 0 || !HasCompleteProfileAddress(dt.Rows[0]))
        {
            Message.Show("Profile address is not available. Please add your address from Address Proof page.");
            return;
        }

        DataRow row = dt.Rows[0];
        string result = UpdateUserShipping(
            Session["userid"].ToString(),
            GetValue(row, "Address"),
            GetValue(row, "CityId"),
            GetValue(row, "AreaName"),
            GetValue(row, "Pincode"));

        if (result == "t")
        {
            Message.Show("Profile address saved as shipping address.");
            LoadShippingAddress();
        }
        else
        {
            Message.Show("Unable to save shipping address. Please try again.");
        }
    }

    protected void btnSaveShipping_Click(object sender, EventArgs e)
    {
        if (!ValidateShippingForm())
        {
            return;
        }

        string result = UpdateUserShipping(
            Session["userid"].ToString(),
            txtShipAddress.Text.Trim(),
            ddShipCity.SelectedValue,
            txtShipArea.Text.Trim(),
            txtShipPincode.Text.Trim());

        if (result == "t")
        {
            Message.Show("Shipping address saved successfully.");
            LoadShippingAddress();
        }
        else
        {
            Message.Show("Unable to save shipping address. Please try again.");
        }
    }

    bool ValidateShippingForm()
    {
        if (string.IsNullOrWhiteSpace(txtShipAddress.Text))
        {
            Message.Show("Enter shipping address.");
            return false;
        }

        if (ddShipState.SelectedValue == "0")
        {
            Message.Show("Select state.");
            return false;
        }

        if (ddShipCity.SelectedValue == "0")
        {
            Message.Show("Select city.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtShipPincode.Text))
        {
            Message.Show("Enter pincode.");
            return false;
        }

        return true;
    }

    bool EnsureShippingAddressSaved()
    {
        DataTable dt = GetUserAddressDetail(Session["userid"].ToString());
        if (dt != null && dt.Rows.Count > 0 && HasCompleteShippingAddress(dt.Rows[0]))
        {
            return true;
        }

        Message.Show("Please add or confirm your shipping address before submitting the purchase request.");
        if (dt != null && dt.Rows.Count > 0 && HasCompleteProfileAddress(dt.Rows[0]) && !HasCompleteShippingAddress(dt.Rows[0]))
        {
            ShowShippingView();
            BindShippingView(dt.Rows[0], false);
            btnUseProfileShipping.Visible = true;
        }
        else if (dt != null && dt.Rows.Count > 0)
        {
            ShowShippingEdit(dt.Rows[0], HasCompleteShippingAddress(dt.Rows[0]));
        }
        else
        {
            ShowShippingEmpty(false);
        }

        return false;
    }

    public string UpdateUserShipping(string userid, string address, string city, string area, string pincode)
    {
        string res = "";
        SqlConnection cn = null;
        SqlTransaction tr = null;

        try
        {
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            string sql = "UPDATE UserDetail SET Shippingaddress='" + SqlEscape(address)
                + "',ShippingCityId='" + SqlEscape(city)
                + "',ShippingAreaName='" + SqlEscape(area)
                + "',ShippingPincode='" + SqlEscape(pincode)
                + "' WHERE UserId='" + SqlEscape(userid) + "'";

            ObjData.RunInsUpDelQueryTrans(sql, tr);
            res = "t";
            tr.Commit();
        }
        catch
        {
            res = "0";
            if (tr != null)
            {
                tr.Rollback();
            }
        }
        finally
        {
            ObjData.EndConnection();
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return res;
    }

    static bool HasCompleteShippingAddress(DataRow row)
    {
        return row != null
            && !string.IsNullOrWhiteSpace(GetValue(row, "Shippingaddress"))
            && !string.IsNullOrWhiteSpace(GetValue(row, "ShippingCityId"))
            && GetValue(row, "ShippingCityId") != "0"
            && !string.IsNullOrWhiteSpace(GetValue(row, "ShippingPincode"));
    }

    static bool HasCompleteProfileAddress(DataRow row)
    {
        return row != null
            && !string.IsNullOrWhiteSpace(GetValue(row, "Address"))
            && !string.IsNullOrWhiteSpace(GetValue(row, "CityId"))
            && GetValue(row, "CityId") != "0"
            && !string.IsNullOrWhiteSpace(GetValue(row, "Pincode"));
    }

    static string GetValue(DataRow row, string columnName)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (DataColumn column in row.Table.Columns)
        {
            if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase)
                && row[column] != DBNull.Value)
            {
                return Convert.ToString(row[column]).Trim();
            }
        }

        return string.Empty;
    }

    static string FormatAddress(string address, string area, string city, string state, string pincode)
    {
        StringBuilder sb = new StringBuilder();
        AppendLineIfPresent(sb, address);
        AppendLineIfPresent(sb, area);

        string location = JoinParts(city, state);
        if (!string.IsNullOrWhiteSpace(pincode))
        {
            location = string.IsNullOrWhiteSpace(location)
                ? pincode
                : location + " - " + pincode;
        }

        AppendLineIfPresent(sb, location);
        return sb.ToString().Trim();
    }

    static void AppendLineIfPresent(StringBuilder sb, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            sb.Append(value.Trim());
        }
    }

    static string JoinParts(string first, string second)
    {
        if (string.IsNullOrWhiteSpace(first))
        {
            return second ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(second))
        {
            return first;
        }

        return first.Trim() + ", " + second.Trim();
    }

    static void SetDropDownValue(DropDownList ddl, string value)
    {
        if (ddl == null || string.IsNullOrWhiteSpace(value) || value == "0")
        {
            return;
        }

        ListItem item = ddl.Items.FindByValue(value);
        if (item != null)
        {
            ddl.ClearSelection();
            item.Selected = true;
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    public string Insert_ProductPurchase(clsProduct objState)
    {
        string str_orderid = Guid.NewGuid().ToString().Substring(0, 8);

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_add_SavingAccountDetail";
            int quantity = objState.Quantity > 0 ? objState.Quantity : 1;
            SqlParameter[] parameter = {
                new SqlParameter("@OrderId", str_orderid),
                new SqlParameter("@UserId", objState.UserId),
                new SqlParameter("@Amount", objState.Amount),
                new SqlParameter("@OnlineTransactionId", objState.TransactionCode),
                new SqlParameter("@ImageName", objState.ProductImage ?? string.Empty),
                new SqlParameter("@EntryBy", objState.MentionBy),
                new SqlParameter("@quantity", quantity),
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text == "")
        {
            Message.Show("Enter User Id...!!!");
            return;
        }

        if (txtusername.Text == "")
        {
            Message.Show("Enter User Name...!!!");
            return;
        }

        if (txtamount.Text == "")
        {
            Message.Show("Enter Amount...!!!");
            return;
        }

        if (!EnsureShippingAddressSaved())
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(txttransactionid.Text))
        {
            Message.Show("Enter UTR No / Transaction ID.");
            return;
        }

        if (!ImageUpload.HasFile)
        {
            Message.Show("Please upload payment screenshot.");
            return;
        }

        string uploadedImage = UploadImage();
        if (string.IsNullOrWhiteSpace(uploadedImage))
        {
            Message.Show("Invalid payment screenshot. Please upload JPG, PNG, WEBP or GIF image.");
            return;
        }

        objproduct.ProductImage = uploadedImage;
        objproduct.MentionBy = Session["userid"].ToString();
        objproduct.UserId = Session["userid"].ToString();
        objproduct.Amount = Convert.ToDecimal(txtamount.Text);
        objproduct.TransactionCode = txttransactionid.Text.Trim();
        objproduct.Quantity = 1;

        string res = Insert_ProductPurchase(objproduct);
        if (res == "t")
        {
            string popupScript = "alert('Saving Product Purchase Request Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txttransactionid.Text = "";
        }
  else
                    if (res == "f")
                    {
                         string popupScript = "alert('Another Request Is Pending');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txttransactionid.Text = "";
                    }

 else
                    if (res == "u")
                    {
                         string popupScript = "alert('This Transaction Id already used');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txttransactionid.Text = "";
                    }
        else
        {
            string popupScript = "alert('Unknown error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Session cart for user-panel franchisee repurchase (PurchaseItemRepurchase).
/// </summary>
public static class UserPanelCartHelper
{
    public const string CartSessionKey = "UserPanelCart";
    public const string MetaSessionKey = "UserPanelCartMeta";

    public class CartMeta
    {
        public string FranchiseeId { get; set; }
        public string PlanType { get; set; }
        public string PlanId { get; set; }
        public string FranchiseeState { get; set; }
        public string UserState { get; set; }
    }

    public class CartTotals
    {
        public decimal Subtotal { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Shipping { get; set; }
        public decimal Payable { get; set; }
        public int ItemCount { get; set; }
        public int QuantityCount { get; set; }
        public ProductWeightHelper.ShippingQuote Quote { get; set; }
    }

    public static DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProductId");
        dt.Columns.Add("ProductName");
        dt.Columns.Add("Image");
        dt.Columns.Add("Amount");
        dt.Columns.Add("MRP");
        dt.Columns.Add("BV");
        dt.Columns.Add("DP");
        dt.Columns.Add("STOCK");
        dt.Columns.Add("TOTALBV");
        dt.Columns.Add("TOTALDP");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("TotalAmount");
        dt.Columns.Add("CGST");
        dt.Columns.Add("SGST");
        dt.Columns.Add("IGST");
        dt.Columns.Add("PurchaseAmount");
        dt.Columns.Add("GSTPER");
        dt.Columns.Add("OFFERPRODUCTID");
        return dt;
    }

    public static DataTable GetCart(HttpSessionState session)
    {
        if (session == null)
        {
            return CreateTable();
        }

        DataTable dt = session[CartSessionKey] as DataTable;
        if (dt == null)
        {
            dt = CreateTable();
            session[CartSessionKey] = dt;
        }

        return dt;
    }

    public static CartMeta GetMeta(HttpSessionState session)
    {
        if (session == null)
        {
            return null;
        }

        return session[MetaSessionKey] as CartMeta;
    }

    public static void Save(HttpSessionState session, DataTable cart, CartMeta meta)
    {
        if (session == null)
        {
            return;
        }

        session[CartSessionKey] = cart;
        session[MetaSessionKey] = meta;
    }

    public static void Clear(HttpSessionState session)
    {
        if (session == null)
        {
            return;
        }

        session[CartSessionKey] = CreateTable();
        session[MetaSessionKey] = null;
    }

    public static int GetLineCount(HttpSessionState session)
    {
        DataTable cart = session == null ? null : session[CartSessionKey] as DataTable;
        return cart == null ? 0 : cart.Rows.Count;
    }

    public static string GetCatalogUrl(CartMeta meta)
    {
        if (meta == null || string.IsNullOrWhiteSpace(meta.FranchiseeId))
        {
            return "FranchiseeSearchNew.aspx";
        }

        return "PurchaseItemRepurchase.aspx?FID=" + HttpUtility.UrlEncode(meta.FranchiseeId + "_" + meta.PlanType + "_" + meta.PlanId);
    }

    public static bool AddProduct(HttpSessionState session, string userId, string productId, string franchiseeId, string planType, string planId, int quantity, out string message)
    {
        message = string.Empty;
        if (session == null)
        {
            message = "Session expired. Please login again.";
            return false;
        }

        if (quantity < 1)
        {
            quantity = 1;
        }

        clsProduct objProduct = new clsProduct();
        objProduct.ProductId = productId;
        DataTable productDt = objProduct.getProduct(objProduct);
        if (productDt == null || productDt.Rows.Count == 0)
        {
            message = "Product not found.";
            return false;
        }

        int stock = GetFranchiseeStock(objProduct, productId, franchiseeId);
        DataTable cart = GetCart(session);
        CartMeta meta = GetMeta(session);

        if (meta != null && !string.IsNullOrWhiteSpace(meta.FranchiseeId)
            && !string.Equals(meta.FranchiseeId, franchiseeId, StringComparison.OrdinalIgnoreCase)
            && cart.Rows.Count > 0)
        {
            cart = CreateTable();
            meta = null;
        }

        DataRow existing = FindRow(cart, productId);
        int already = existing == null ? 0 : ToInt(existing["Quantity"]);
        int newQty = already + quantity;
        if (stock > 0 && newQty > stock)
        {
            message = "You cannot purchase more than franchisee stock. Please contact the franchisee.";
            return false;
        }

        if (stock <= 0)
        {
            message = "This product is out of stock at the selected franchisee.";
            return false;
        }

        if (meta == null)
        {
            meta = LoadMeta(userId, franchiseeId, planType, planId);
        }
        else
        {
            meta.PlanType = planType;
            meta.PlanId = planId;
        }

        DataRow product = productDt.Rows[0];
        if (existing == null)
        {
            DataRow dr = cart.NewRow();
            dr["ProductId"] = Convert.ToString(product["ProductId"]);
            dr["ProductName"] = Convert.ToString(product["ProductName"]);
            dr["Image"] = NormalizeImage(Convert.ToString(product["Image"]));
            dr["Amount"] = Convert.ToString(product["Amount"]);
            dr["MRP"] = Convert.ToString(product["MRP"]);
            dr["BV"] = Convert.ToString(product["BV"]);
            dr["DP"] = Convert.ToString(product["DP"]);
            dr["STOCK"] = stock.ToString();
            dr["Quantity"] = newQty.ToString();
            dr["GSTPER"] = Convert.ToString(product["GST"]);
            dr["OFFERPRODUCTID"] = string.Empty;
            cart.Rows.Add(dr);
            RecalcRow(dr, IsSameState(meta));
        }
        else
        {
            existing["STOCK"] = stock.ToString();
            existing["Quantity"] = newQty.ToString();
            RecalcRow(existing, IsSameState(meta));
        }

        Save(session, cart, meta);
        return true;
    }

    public static bool UpdateQuantity(HttpSessionState session, string productId, int quantity, out string message)
    {
        message = string.Empty;
        DataTable cart = GetCart(session);
        CartMeta meta = GetMeta(session);
        DataRow row = FindRow(cart, productId);
        if (row == null)
        {
            message = "Item not found in cart.";
            return false;
        }

        if (quantity < 1)
        {
            quantity = 1;
        }

        int stock = ToInt(row["STOCK"]);
        if (meta != null)
        {
            clsProduct objProduct = new clsProduct();
            stock = GetFranchiseeStock(objProduct, productId, meta.FranchiseeId);
            row["STOCK"] = stock.ToString();
        }

        if (stock > 0 && quantity > stock)
        {
            message = "Quantity cannot exceed franchisee stock (" + stock + ").";
            return false;
        }

        row["Quantity"] = quantity.ToString();
        RecalcRow(row, IsSameState(meta));
        Save(session, cart, meta);
        return true;
    }

    public static void Remove(HttpSessionState session, string productId)
    {
        DataTable cart = GetCart(session);
        DataRow row = FindRow(cart, productId);
        if (row != null)
        {
            cart.Rows.Remove(row);
        }

        if (cart.Rows.Count == 0)
        {
            Save(session, cart, GetMeta(session));
        }
        else
        {
            Save(session, cart, GetMeta(session));
        }
    }

    public static decimal GetMrpTotal(DataTable cart)
    {
        decimal mrp = 0;
        if (cart == null)
        {
            return 0;
        }

        foreach (DataRow row in cart.Rows)
        {
            int qty = ToInt(row["Quantity"]);
            if (qty < 1)
            {
                qty = 1;
            }

            mrp += ToDecimal(row["MRP"]) * qty;
        }

        return mrp;
    }

    public static CartTotals GetTotals(HttpSessionState session)
    {
        DataTable cart = GetCart(session);
        CartTotals totals = new CartTotals();
        foreach (DataRow row in cart.Rows)
        {
            totals.ItemCount += 1;
            totals.QuantityCount += ToInt(row["Quantity"]);
            totals.Subtotal += ToDecimal(row["TotalAmount"]);
            totals.PurchaseAmount += ToDecimal(row["PurchaseAmount"]);
            totals.Cgst += ToDecimal(row["CGST"]);
            totals.Sgst += ToDecimal(row["SGST"]);
            totals.Igst += ToDecimal(row["IGST"]);
        }

        totals.Quote = ProductWeightHelper.QuoteFromCart(cart, totals.Subtotal);
        totals.Shipping = totals.Quote == null ? 0 : totals.Quote.ShippingAmount;
        totals.Payable = totals.Subtotal + totals.Shipping;
        return totals;
    }

    public static DataTable BuildPurchaseProductForSp(DataTable source, string franchiseeId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FranchiseeId", typeof(string));
        dt.Columns.Add("ProductId", typeof(int));
        dt.Columns.Add("SubProductId", typeof(int));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Image", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("MRP", typeof(decimal));
        dt.Columns.Add("BV", typeof(decimal));
        dt.Columns.Add("DP", typeof(decimal));
        dt.Columns.Add("STOCK", typeof(int));
        dt.Columns.Add("TOTALBV", typeof(decimal));
        dt.Columns.Add("TOTALDP", typeof(decimal));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("TotalAmount", typeof(decimal));
        dt.Columns.Add("CGST", typeof(decimal));
        dt.Columns.Add("SGST", typeof(decimal));
        dt.Columns.Add("IGST", typeof(decimal));
        dt.Columns.Add("PurchaseAmount", typeof(decimal));
        dt.Columns.Add("GSTPER", typeof(decimal));

        if (source == null)
        {
            return dt;
        }

        foreach (DataRow src in source.Rows)
        {
            DataRow dr = dt.NewRow();
            dr["FranchiseeId"] = franchiseeId ?? string.Empty;
            dr["ProductId"] = ToInt(src["ProductId"]);
            dr["SubProductId"] = source.Columns.Contains("SubProductId") ? ToInt(src["SubProductId"]) : 0;
            dr["ProductName"] = Convert.ToString(src["ProductName"]);
            dr["Image"] = Convert.ToString(src["Image"]);
            dr["Amount"] = ToDecimal(src["Amount"]);
            dr["MRP"] = ToDecimal(src["MRP"]);
            dr["BV"] = ToDecimal(src["BV"]);
            dr["DP"] = ToDecimal(src["DP"]);
            dr["STOCK"] = ToInt(src["STOCK"]);
            dr["TOTALBV"] = ToDecimal(src["TOTALBV"]);
            dr["TOTALDP"] = ToDecimal(src["TOTALDP"]);
            dr["Quantity"] = ToInt(src["Quantity"]);
            dr["TotalAmount"] = ToDecimal(src["TotalAmount"]);
            dr["CGST"] = ToDecimal(src["CGST"]);
            dr["SGST"] = ToDecimal(src["SGST"]);
            dr["IGST"] = ToDecimal(src["IGST"]);
            dr["PurchaseAmount"] = ToDecimal(src["PurchaseAmount"]);
            dr["GSTPER"] = ToDecimal(src["GSTPER"]);
            dt.Rows.Add(dr);
        }

        return dt;
    }

    public static string AddPurchase(string userId, decimal purchaseAmount, decimal cgst, decimal sgst, decimal igst, decimal payable, string franchiseeId, string planType, DataTable products, string bankId, string transactionId, string paymentMode, string imageName)
    {
        Data objData = new Data();
        SqlTransaction tr = null;
        string res = "0";
        try
        {
            SqlConnection cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            SqlParameter[] parameter = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@PurchaseAmount", purchaseAmount),
                new SqlParameter("@CGSTAmount", cgst),
                new SqlParameter("@SGSTAmount", sgst),
                new SqlParameter("@IGSTAmount", igst),
                new SqlParameter("@CGSTPer", "0"),
                new SqlParameter("@SGSTPer", "0"),
                new SqlParameter("@IGSTPer", "0"),
                new SqlParameter("@Paybleamount", payable),
                new SqlParameter("@FranchiseeId", franchiseeId),
                new SqlParameter("@Plantype", ToInt(planType)),
                new SqlParameter("@PurchaseProduct", SqlDbType.Structured)
                {
                    TypeName = "dbo.FranchiseePurchaseProductTrustedCart",
                    Value = products
                },
                new SqlParameter("@Cashamount", "0"),
                new SqlParameter("@RestAmount", "0"),
                new SqlParameter("@BankID", ToInt(bankId)),
                new SqlParameter("@Onlinetransactionid", transactionId ?? string.Empty),
                new SqlParameter("@PaymentMode", paymentMode ?? string.Empty),
                new SqlParameter("@Img", imageName ?? string.Empty),
                new SqlParameter("@Isdistributer", "0")
            };

            DataTable result = objData.RunDataTableProcedureTRans("sp_add_PurchaseRepurchase", tr, parameter);
            res = result != null && result.Rows.Count > 0 ? Convert.ToString(result.Rows[0][1]) : "0";
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            try
            {
                if (tr != null)
                {
                    tr.Rollback();
                }
            }
            catch
            {
            }

            if (ex != null && !string.IsNullOrWhiteSpace(ex.Message))
            {
                res = "Error: " + ex.Message;
            }
        }
        finally
        {
            objData.EndConnection();
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return res;
    }

    public static string UpdateUserShipping(string userId, string address, string city, string area, string pincode)
    {
        Data objData = new Data();
        SqlTransaction tr = null;
        string res = "0";
        try
        {
            SqlConnection cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            string sql = "update UserDetail set Shippingaddress='" + Escape(address) + "',ShippingCityId='" + Escape(city) + "', ShippingAreaName='" + Escape(area) + "',ShippingPincode='" + Escape(pincode) + "'  where UserId='" + Escape(userId) + "'";
            objData.RunInsUpDelQueryTrans(sql, tr);
            res = "t";
            tr.Commit();
        }
        catch
        {
            res = "0";
            try
            {
                if (tr != null)
                {
                    tr.Rollback();
                }
            }
            catch
            {
            }
        }
        finally
        {
            objData.EndConnection();
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return res;
    }

    public static bool HasDiscount(object mrpObj, object amountObj)
    {
        decimal mrp;
        decimal amount;
        if (!decimal.TryParse(Convert.ToString(mrpObj), out mrp) || !decimal.TryParse(Convert.ToString(amountObj), out amount))
        {
            return false;
        }

        return mrp > 0 && amount > 0 && mrp > amount;
    }

    public static string FormatMoney(object value)
    {
        return ToDecimal(value).ToString("0.00");
    }

    static CartMeta LoadMeta(string userId, string franchiseeId, string planType, string planId)
    {
        CartMeta meta = new CartMeta
        {
            FranchiseeId = franchiseeId,
            PlanType = planType,
            PlanId = planId,
            UserState = string.Empty,
            FranchiseeState = string.Empty
        };

        try
        {
            clsUser objUser = new clsUser();
            objUser.UserId = userId;
            DataTable userDt = objUser.getUserDetail(objUser);
            if (userDt != null && userDt.Rows.Count > 0)
            {
                meta.UserState = Convert.ToString(userDt.Rows[0]["Statename"]);
            }
        }
        catch
        {
        }

        try
        {
            clsfranchisee objF = new clsfranchisee();
            objF.UserId = franchiseeId;
            DataTable fDt = objF.getuserdetailviaprocedure(objF);
            if (fDt != null && fDt.Rows.Count > 0)
            {
                meta.FranchiseeState = Convert.ToString(fDt.Rows[0]["statenew"]);
            }
        }
        catch
        {
        }

        return meta;
    }

    static int GetFranchiseeStock(clsProduct objProduct, string productId, string franchiseeId)
    {
        try
        {
            DataTable stockDt = objProduct.getCheckStockfranchisee(productId, franchiseeId);
            if (stockDt == null || stockDt.Rows.Count == 0)
            {
                return 0;
            }

            return ToInt(stockDt.Rows[0]["Cr"]) - ToInt(stockDt.Rows[0]["Dr"]);
        }
        catch
        {
            return 0;
        }
    }

    static void RecalcRow(DataRow dr, bool sameState)
    {
        int qty = ToInt(dr["Quantity"]);
        decimal amount = ToDecimal(dr["Amount"]);
        decimal gst = ToDecimal(dr["GSTPER"]);
        decimal bv = ToDecimal(dr["BV"]);
        decimal dp = ToDecimal(dr["DP"]);
        decimal line = qty * amount;
        decimal purchase = gst <= -100
            ? line
            : Math.Round((line * 100m) / (100m + gst), 2);
        decimal tax = line - purchase;

        dr["TOTALBV"] = (qty * bv).ToString();
        dr["TOTALDP"] = (qty * dp).ToString();
        dr["TotalAmount"] = line.ToString();
        dr["PurchaseAmount"] = purchase.ToString();
        if (sameState)
        {
            dr["CGST"] = Math.Round(tax / 2m, 2).ToString();
            dr["SGST"] = Math.Round(tax / 2m, 2).ToString();
            dr["IGST"] = "0";
        }
        else
        {
            dr["CGST"] = "0";
            dr["SGST"] = "0";
            dr["IGST"] = Math.Round(tax, 2).ToString();
        }
    }

    static bool IsSameState(CartMeta meta)
    {
        if (meta == null)
        {
            return false;
        }

        string a = (meta.FranchiseeState ?? string.Empty).Trim();
        string b = (meta.UserState ?? string.Empty).Trim();
        return a != string.Empty && string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }

    static DataRow FindRow(DataTable cart, string productId)
    {
        if (cart == null || string.IsNullOrWhiteSpace(productId))
        {
            return null;
        }

        foreach (DataRow row in cart.Rows)
        {
            if (string.Equals(Convert.ToString(row["ProductId"]), productId, StringComparison.OrdinalIgnoreCase))
            {
                return row;
            }
        }

        return null;
    }

    static string NormalizeImage(string image)
    {
        if (string.IsNullOrWhiteSpace(image) || image == "../ProductImage/")
        {
            return "../ProductImage/images.png";
        }

        return image;
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    public static int ToInt(object value)
    {
        int result;
        return int.TryParse(Convert.ToString(value), out result) ? result : 0;
    }

    public static decimal ToDecimal(object value)
    {
        decimal result;
        return decimal.TryParse(Convert.ToString(value), out result) ? result : 0m;
    }
}

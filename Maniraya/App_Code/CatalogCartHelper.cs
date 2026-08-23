using BusinessLogicTier;
using System;
using System.Data;
using System.Web;

public static class CatalogCartHelper
{
    public class AddResult
    {
        public bool ok { get; set; }
        public bool login { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public string loginUrl { get; set; }
    }

    public static AddResult AddFromListing(string productId, string franchiseeId)
    {
        return AddFromListing(productId, franchiseeId, null);
    }

    public static AddResult AddFromListing(string productId, string franchiseeId, string returnUrl)
    {
        HttpContext context = HttpContext.Current;
        if (context == null || context.Session == null)
        {
            return Fail("Unable to add this product right now.");
        }

        productId = (productId ?? string.Empty).Trim();
        franchiseeId = ResolveFranchiseeId(franchiseeId);
        if (string.IsNullOrWhiteSpace(productId))
        {
            return Fail("Product not found.");
        }

        if (context.Session["userid"] == null)
        {
            context.Session["PendingCatalogAdd"] = productId + "|" + franchiseeId;
            context.Session["returnUrl"] = SafeReturnUrl(returnUrl, context);
            return new AddResult
            {
                ok = false,
                login = true,
                message = "Please login to add products to cart.",
                loginUrl = "Login.aspx"
            };
        }

        return AddForUser(Convert.ToString(context.Session["userid"]), productId, franchiseeId);
    }

    public static AddResult TryCompletePendingAdd()
    {
        HttpContext context = HttpContext.Current;
        if (context == null || context.Session == null || context.Session["userid"] == null)
        {
            return null;
        }

        string pending = Convert.ToString(context.Session["PendingCatalogAdd"]);
        context.Session.Remove("PendingCatalogAdd");
        if (string.IsNullOrWhiteSpace(pending))
        {
            return null;
        }

        string[] parts = pending.Split('|');
        string productId = parts.Length > 0 ? parts[0] : string.Empty;
        string franchiseeId = parts.Length > 1 ? parts[1] : "F000001";
        return AddForUser(Convert.ToString(context.Session["userid"]), productId, franchiseeId);
    }

    static AddResult AddForUser(string userId, string productId, string franchiseeId)
    {
        try
        {
            clsProduct objProduct = new clsProduct();
            string colorId;
            string sizeId;
            ResolveColorAndSize(objProduct, productId, out colorId, out sizeId);

            int stock = ResolveProductStock(objProduct, franchiseeId, productId, colorId, sizeId);
            if (stock <= 0)
            {
                return Fail("Stock not available");
            }

            objProduct.ProductId = productId;
            objProduct.colorId = colorId;
            objProduct.Sizeid = sizeId;
            objProduct.UserId = userId;
            objProduct.FranchiseeID = franchiseeId;
            string res = objProduct.addcartitem(objProduct);
            if (!IsSuccess(res))
            {
                return Fail("Unable to add this product to cart.");
            }

            return new AddResult
            {
                ok = true,
                message = "Product added to cart.",
                count = GetCartCount(userId)
            };
        }
        catch
        {
            return Fail("Unable to add this product to cart.");
        }
    }

    public static int GetCartCount(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return 0;
        }

        clsProduct objProduct = new clsProduct();
        objProduct.UserId = userId;
        DataTable dt = objProduct.getCartItems(objProduct);
        return dt == null ? 0 : dt.Rows.Count;
    }

    static void ResolveColorAndSize(clsProduct objProduct, string productId, out string colorId, out string sizeId)
    {
        objProduct.ProductId = productId;
        colorId = FirstValue(objProduct.getColorByid(objProduct), "ColorId", "ColorID", "colorid");
        sizeId = FirstValue(objProduct.getSizeyid(objProduct), "SizeID", "SizeId", "sizeid");
        if (string.IsNullOrWhiteSpace(colorId))
        {
            colorId = "1";
        }
        if (string.IsNullOrWhiteSpace(sizeId))
        {
            sizeId = "1";
        }
    }

    static int ResolveProductStock(clsProduct objProduct, string franchiseeId, string productId, string colorId, string sizeId)
    {
        int stock = ReadStockValue(objProduct.getstockbysubproduct(franchiseeId, productId, colorId, sizeId));
        if (stock > 0)
        {
            return stock;
        }

        return ReadStockValue(objProduct.getCheckStockfranchisee(productId, franchiseeId));
    }

    static string ResolveFranchiseeId(string franchiseeId)
    {
        string value = (franchiseeId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(value) || value == "0")
        {
            return "F000001";
        }

        return value;
    }

    static string SafeReturnUrl(string returnUrl, HttpContext context)
    {
        string value = (returnUrl ?? string.Empty).Trim();
        if (IsSafeReturnUrl(value))
        {
            return value;
        }

        if (context.Request.UrlReferrer != null)
        {
            string referrer = context.Request.UrlReferrer.PathAndQuery;
            if (IsSafeReturnUrl(referrer))
            {
                return referrer;
            }
        }

        return "index.aspx";
    }

    static bool IsSafeReturnUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (value.IndexOf("://", StringComparison.Ordinal) >= 0 || value.StartsWith("//", StringComparison.Ordinal))
        {
            return false;
        }

        return value.StartsWith("/", StringComparison.Ordinal)
            || value.IndexOf(".aspx", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    static int ReadStockValue(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return 0;
        }

        int stock;
        if (dt.Columns.Contains("stockleft") && int.TryParse(Convert.ToString(dt.Rows[0]["stockleft"]), out stock))
        {
            return stock;
        }

        if (dt.Columns.Contains("Cr") && dt.Columns.Contains("Dr"))
        {
            int cr;
            int dr;
            int.TryParse(Convert.ToString(dt.Rows[0]["Cr"]), out cr);
            int.TryParse(Convert.ToString(dt.Rows[0]["Dr"]), out dr);
            return cr - dr;
        }

        int.TryParse(Convert.ToString(dt.Rows[0][0]), out stock);
        return stock;
    }

    static string FirstValue(DataTable dt, params string[] names)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return string.Empty;
        }

        DataRow row = dt.Rows[0];
        foreach (string name in names)
        {
            if (dt.Columns.Contains(name) && row[name] != DBNull.Value)
            {
                string value = Convert.ToString(row[name]).Trim();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
        }

        return string.Empty;
    }

    static bool IsSuccess(string result)
    {
        string value = (result ?? string.Empty).Trim();
        return value.Equals("S", StringComparison.OrdinalIgnoreCase)
            || value.Equals("t", StringComparison.OrdinalIgnoreCase)
            || value == "1";
    }

    static AddResult Fail(string message)
    {
        return new AddResult
        {
            ok = false,
            message = message
        };
    }
}

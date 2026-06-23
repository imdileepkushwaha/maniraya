using DataTier;

using System;

using System.Data;

using System.Globalization;

using System.Linq;

using System.Web.UI;

using System.Web.UI.WebControls;



public partial class user_SavingProductInvoice : Page

{

    Data ObjData = new Data();



    protected void Page_Load(object sender, EventArgs e)

    {

        if (Session["userid"] == null)

        {

            Response.Redirect("logout.aspx");

            return;

        }



        SiteContactHelper.BindInvoiceCompanyInfo(litCompanyContact);
        SiteContactHelper.BindInvoiceGst(litCompanyGst);
        SiteContactHelper.BindInvoiceSign(imgInvoiceSign, "../");
        SiteContactHelper.BindSupportContactLine(litSupportContact);



        string orderId = Convert.ToString(Request.QueryString["orderId"]).Trim();

        if (string.IsNullOrWhiteSpace(orderId))

        {

            int recordId;

            if (int.TryParse(Convert.ToString(Request.QueryString["id"]), out recordId) && recordId > 0)

            {

                orderId = GetOrderIdByRecordId(recordId, Session["userid"].ToString());

            }

        }



        if (string.IsNullOrWhiteSpace(orderId))

        {

            Response.Redirect("SavingProductOrderHistory.aspx");

            return;

        }



        DataTable items = GetInvoiceItems(orderId, Session["userid"].ToString());

        if (items == null || items.Rows.Count == 0)

        {

            Response.Redirect("SavingProductOrderHistory.aspx");

            return;

        }



        BindInvoice(orderId, items);

    }



    string GetOrderIdByRecordId(int recordId, string userId)

    {

        string sql = "SELECT TOP 1 orderid FROM SavingAccountDetail WITH (NOLOCK) WHERE id = "

            + recordId + " AND UserId = '" + SqlEscape(userId) + "'";



        ObjData.StartConnection();

        try

        {

            DataTable dt = ObjData.RunDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["orderid"]).Trim();
            }

            return string.Empty;

        }

        finally

        {

            ObjData.EndConnection();

        }

    }



    DataTable GetInvoiceItems(string orderId, string userId)

    {

        string sql = @"

SELECT

    sd.id,

    sd.orderid,

    sd.userid,

    sd.amount,

    sd.status,

    sd.entrydate,

    sd.approvedate,

    ISNULL(NULLIF(LTRIM(RTRIM(sd.couponcode)), ''), '-') AS couponcode,

    ISNULL(NULLIF(LTRIM(RTRIM(pm.productname)), ''), 'Saving Product') AS productname,

    pm.MRP,

    pm.DP,

    ud.username,

    ud.mobile,

    CASE

        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.Shippingaddress

        ELSE ud.Address

    END AS ShipAddress,

    CASE

        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingAreaName

        ELSE ud.AreaName

    END AS ShipArea,

    CASE

        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN CS.CityName

        ELSE C.CityName

    END AS ShipCity,

    CASE

        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN SS.StateName

        ELSE S.StateName

    END AS ShipState,

    CASE

        WHEN NULLIF(LTRIM(RTRIM(ud.Shippingaddress)), '') IS NOT NULL THEN ud.ShippingPincode

        ELSE ud.Pincode

    END AS ShipPincode

FROM SavingAccountDetail sd WITH (NOLOCK)

LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id

LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sd.UserId

LEFT JOIN CityMaster CS WITH (NOLOCK) ON ud.ShippingCityId = CS.CityId

LEFT JOIN StateMaster SS WITH (NOLOCK) ON CS.StateId = SS.StateId

LEFT JOIN CityMaster C WITH (NOLOCK) ON ud.CityId = C.CityId

LEFT JOIN StateMaster S WITH (NOLOCK) ON C.StateId = S.StateId

WHERE sd.orderid = '" + SqlEscape(orderId) + @"'

  AND sd.UserId = '" + SqlEscape(userId) + @"'

  AND (sd.status = 'Approved'

       OR LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', 'approve', '1', 'active'))

ORDER BY sd.couponcode, sd.id";



        DataTable dt = null;

        ObjData.StartConnection();

        try

        {

            dt = ObjData.RunDataTable(sql);

        }

        finally

        {

            ObjData.EndConnection();

        }



        if (dt == null)

        {

            return new DataTable();

        }



        if (!dt.Columns.Contains("mrpdisplay"))

        {

            dt.Columns.Add("mrpdisplay", typeof(string));

        }



        if (!dt.Columns.Contains("amountdisplay"))

        {

            dt.Columns.Add("amountdisplay", typeof(string));

        }



        foreach (DataRow row in dt.Rows)

        {

            decimal amount = ParseDecimal(row["amount"]);

            decimal mrp = ParseDecimal(row["MRP"]);

            if (mrp <= 0)

            {

                mrp = amount;

            }



            row["mrpdisplay"] = mrp.ToString("0.00", CultureInfo.InvariantCulture);

            row["amountdisplay"] = amount.ToString("0.00", CultureInfo.InvariantCulture);

        }



        return dt;

    }



    void BindInvoice(string orderId, DataTable items)

    {

        DataRow header = items.Rows[0];

        DateTime invoiceDate = GetInvoiceDate(items);



        lblBillingName.Text = Convert.ToString(header["username"]);

        lblUserId.Text = Convert.ToString(header["userid"]);

        lblBillingAddress.Text = Convert.ToString(header["ShipAddress"]);

        lblBillingArea.Text = Convert.ToString(header["ShipArea"]);

        lblBillingCity.Text = Convert.ToString(header["ShipCity"]);

        lblBillingState.Text = Convert.ToString(header["ShipState"]);

        lblBillingPincode.Text = Convert.ToString(header["ShipPincode"]);

        lblBillingMobile.Text = Convert.ToString(header["mobile"]);

        lblInvoiceDate.Text = invoiceDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

        lblInvoiceTime.Text = invoiceDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);

        lblInvoiceNumber.Text = "SP" + orderId;

        lblOrderId.Text = orderId;



        gvItems.DataSource = items;

        gvItems.DataBind();



        decimal totalAmount = items.AsEnumerable().Sum(row => ParseDecimal(row["amount"]));

        lblPayableAmount.Text = totalAmount.ToString("0.00", CultureInfo.InvariantCulture);

        lblAmountWords.Text = ConvertWholeNumber(Math.Round(totalAmount, 0).ToString(CultureInfo.InvariantCulture)).ToUpperInvariant();

    }



    static DateTime GetInvoiceDate(DataTable items)

    {

        DateTime latestDate = DateTime.MinValue;

        foreach (DataRow row in items.Rows)

        {

            DateTime approvedDate = GetDateValue(row["approvedate"]);

            if (approvedDate.Year > 1900 && approvedDate > latestDate)

            {

                latestDate = approvedDate;

            }



            DateTime entryDate = GetDateValue(row["entrydate"]);

            if (entryDate.Year > 1900 && entryDate > latestDate)

            {

                latestDate = entryDate;

            }

        }



        return latestDate.Year > 1900 ? latestDate : DateTime.Now;

    }



    static DateTime GetDateValue(object value)

    {

        DateTime parsedDate;

        if (value == null || value == DBNull.Value)

        {

            return DateTime.MinValue;

        }



        if (DateTime.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsedDate))

        {

            return parsedDate;

        }



        return DateTime.MinValue;

    }



    static decimal ParseDecimal(object value)

    {

        decimal parsed;

        if (value == null || value == DBNull.Value)

        {

            return 0m;

        }



        if (decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out parsed))

        {

            return parsed;

        }



        return 0m;

    }



    static string SqlEscape(string value)

    {

        return (value ?? string.Empty).Replace("'", "''");

    }



    static string ConvertWholeNumber(string number)

    {

        try

        {

            double amount = Convert.ToDouble(number);

            if (amount <= 0)

            {

                return string.Empty;

            }



            int whole = (int)Math.Round(amount);

            return ConvertWholeNumberCore(whole).Trim();

        }

        catch

        {

            return string.Empty;

        }

    }



    static string ConvertWholeNumberCore(int number)

    {

        if (number == 0)

        {

            return "Zero";

        }



        if (number < 0)

        {

            return "Minus " + ConvertWholeNumberCore(Math.Abs(number));

        }



        string words = string.Empty;

        if ((number / 10000000) > 0)

        {

            words += ConvertWholeNumberCore(number / 10000000) + " Crore ";

            number %= 10000000;

        }



        if ((number / 100000) > 0)

        {

            words += ConvertWholeNumberCore(number / 100000) + " Lakh ";

            number %= 100000;

        }



        if ((number / 1000) > 0)

        {

            words += ConvertWholeNumberCore(number / 1000) + " Thousand ";

            number %= 1000;

        }



        if ((number / 100) > 0)

        {

            words += ConvertWholeNumberCore(number / 100) + " Hundred ";

            number %= 100;

        }



        if (number > 0)

        {

            if (words != string.Empty)

            {

                words += "and ";

            }



            string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",

                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };



            if (number < 20)

            {

                words += unitsMap[number];

            }

            else

            {

                words += tensMap[number / 10];

                if ((number % 10) > 0)

                {

                    words += " " + unitsMap[number % 10];

                }

            }

        }



        return words;

    }

}


using DataTier;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class SiteContactHelper
{
    public const string TypePhone = "Phone";
    public const string TypeEmail = "Email";
    public const string TypeAddress = "Address";
    public const string TypeWebsite = "Website";
    public const string TypeGst = "GST";
    public const string TypeSign = "Sign";
    public const string SignImageVirtualFolder = "~/InvoiceSign/";

    static readonly Tuple<string, string, string, bool>[] DefaultContacts = new[]
    {
        Tuple.Create(TypePhone, "Customer Support", "+91 888 444 8586", true),
        Tuple.Create(TypeEmail, "Primary Email", "info@mpremium.in", true),
        Tuple.Create(TypeEmail, "Support Email", "Customer@maniraya.com", false),
        Tuple.Create(
            TypeAddress,
            "Head Office",
            "#33 1st floor MANIRAYA marketing pvt ltd 9th A cross HIG A sector yelahanka new town Bangalore Karnataka 560064, INDIA",
            true),
        Tuple.Create(TypeWebsite, "Website", "maniraya.com", true),
        Tuple.Create(TypeGst, "Company GSTIN", "29AARCM8049H1ZQ", true)
    };

    public static void EnsureTableAndSeedDefaults()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string createSql = @"
                IF OBJECT_ID('tbl_SiteContact', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_SiteContact (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        ContactType NVARCHAR(20) NOT NULL,
                        Title NVARCHAR(200) NULL,
                        ContactValue NVARCHAR(MAX) NOT NULL,
                        DisplayOrder INT NOT NULL DEFAULT 0,
                        IsPrimary BIT NOT NULL DEFAULT 0,
                        Status BIT NOT NULL DEFAULT 1,
                        CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
                    )
                END";
                objData.RunInsUpDelQuery(createSql);
                EnsureGstContactExists(objData);

                DataTable countTable = objData.RunDataTable("SELECT COUNT(*) AS ItemCount FROM tbl_SiteContact");
                int count = 0;
                if (countTable != null && countTable.Rows.Count > 0)
                {
                    count = Convert.ToInt32(countTable.Rows[0]["ItemCount"]);
                }

                if (count > 0)
                {
                    return;
                }

                int order = 1;
                foreach (Tuple<string, string, string, bool> item in DefaultContacts)
                {
                    string type = (item.Item1 ?? string.Empty).Replace("'", "''");
                    string title = (item.Item2 ?? string.Empty).Replace("'", "''");
                    string value = (item.Item3 ?? string.Empty).Replace("'", "''");
                    string isPrimary = item.Item4 ? "1" : "0";
                    string insertSql = string.Format(
                        "INSERT INTO tbl_SiteContact (ContactType, Title, ContactValue, DisplayOrder, IsPrimary, Status) VALUES ('{0}', '{1}', '{2}', {3}, {4}, 1)",
                        type,
                        title,
                        value,
                        order,
                        isPrimary);
                    objData.RunInsUpDelQuery(insertSql);
                    order++;
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }
    }

    public static DataTable GetAllContacts()
    {
        EnsureTableAndSeedDefaults();
        return RunSelect("SELECT * FROM tbl_SiteContact ORDER BY ContactType, DisplayOrder, Id DESC");
    }

    public static DataTable GetContactsByType(string contactType)
    {
        EnsureTableAndSeedDefaults();
        string safeType = (contactType ?? string.Empty).Replace("'", "''");
        return RunSelect(
            "SELECT * FROM tbl_SiteContact WHERE ContactType = '" + safeType + "' ORDER BY IsPrimary DESC, DisplayOrder, Id DESC");
    }

    public static DataTable GetContactsExcludingType(string excludedType)
    {
        return GetContactsExcludingTypes(excludedType);
    }

    public static DataTable GetContactsExcludingTypes(params string[] excludedTypes)
    {
        EnsureTableAndSeedDefaults();
        if (excludedTypes == null || excludedTypes.Length == 0)
        {
            return GetAllContacts();
        }

        System.Collections.Generic.List<string> safeTypes = new System.Collections.Generic.List<string>();
        foreach (string excludedType in excludedTypes)
        {
            if (string.IsNullOrWhiteSpace(excludedType))
            {
                continue;
            }

            safeTypes.Add("'" + excludedType.Trim().Replace("'", "''") + "'");
        }

        if (safeTypes.Count == 0)
        {
            return GetAllContacts();
        }

        return RunSelect(
            "SELECT * FROM tbl_SiteContact WHERE ContactType NOT IN (" + string.Join(",", safeTypes)
            + ") ORDER BY ContactType, DisplayOrder, Id DESC");
    }

    public static DataTable GetActiveContacts(string contactType)
    {
        EnsureTableAndSeedDefaults();
        string safeType = (contactType ?? string.Empty).Replace("'", "''");
        return RunSelect(
            "SELECT * FROM tbl_SiteContact WHERE Status = 1 AND ContactType = '" + safeType + "' ORDER BY IsPrimary DESC, DisplayOrder, Id");
    }

    public static string GetPrimaryValue(string contactType)
    {
        DataTable dt = GetActiveContacts(contactType);
        if (dt != null && dt.Rows.Count > 0)
        {
            return Convert.ToString(dt.Rows[0]["ContactValue"]) ?? string.Empty;
        }

        return GetFallbackValue(contactType);
    }

    public static string GetPrimaryPhone()
    {
        return GetPrimaryValue(TypePhone);
    }

    public static string GetPrimaryEmail()
    {
        return GetPrimaryValue(TypeEmail);
    }

    public static string GetPrimaryAddress()
    {
        return GetPrimaryValue(TypeAddress);
    }

    public static string GetPrimaryWebsite()
    {
        return GetPrimaryValue(TypeWebsite);
    }

    public static string GetPrimaryGst()
    {
        return GetPrimaryValue(TypeGst);
    }

    public static string GetPrimarySignImageFileName()
    {
        return GetPrimaryValue(TypeSign);
    }

    public static string GetInvoiceSignImageUrl(string relativePrefix)
    {
        string fileName = GetPrimarySignImageFileName();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        string prefix = string.IsNullOrWhiteSpace(relativePrefix) ? "../" : relativePrefix;
        if (!prefix.EndsWith("/"))
        {
            prefix += "/";
        }

        return prefix + "InvoiceSign/" + fileName.Trim().Replace("\\", "/").TrimStart('/');
    }

    public static void BindInvoiceSign(System.Web.UI.WebControls.Image image, string relativePrefix)
    {
        if (image == null)
        {
            return;
        }

        string url = GetInvoiceSignImageUrl(relativePrefix);
        if (string.IsNullOrWhiteSpace(url))
        {
            image.Visible = false;
            image.ImageUrl = string.Empty;
            return;
        }

        image.ImageUrl = url;
        image.Visible = true;
        if (string.IsNullOrWhiteSpace(image.AlternateText))
        {
            image.AlternateText = "Authorised Signatory";
        }
    }

    public static string BuildInvoiceGstLine()
    {
        string gst = GetPrimaryGst();
        if (string.IsNullOrWhiteSpace(gst))
        {
            return string.Empty;
        }

        return "COMPANY GSTN - " + gst.Trim();
    }

    public static string BuildTelHref(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return "#";
        }

        StringBuilder digits = new StringBuilder();
        foreach (char ch in phone)
        {
            if (char.IsDigit(ch))
            {
                digits.Append(ch);
            }
            else if (ch == '+' && digits.Length == 0)
            {
                digits.Append(ch);
            }
        }

        return "tel:" + digits.ToString();
    }

    public static string BuildMailtoHref(string email)
    {
        return string.IsNullOrWhiteSpace(email) ? "#" : "mailto:" + email.Trim();
    }

    public static string BuildWhatsAppHref(string phone, string message)
    {
        string digits = NormalizeDigits(phone);
        if (string.IsNullOrEmpty(digits))
        {
            return "#";
        }

        string text = string.IsNullOrWhiteSpace(message)
            ? "Hi Maniraya, I need help"
            : message;
        return "https://wa.me/" + digits + "?text=" + Uri.EscapeDataString(text);
    }

    public static string BuildWebsiteHref(string website)
    {
        if (string.IsNullOrWhiteSpace(website))
        {
            return "#";
        }

        string value = website.Trim();
        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        return "https://" + value.TrimStart('/');
    }

    public static string BuildInvoiceCompanyHtml()
    {
        string address = GetPrimaryAddress();
        string phone = GetPrimaryPhone();
        string email = GetPrimaryEmail();
        string website = GetPrimaryWebsite();

        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(address))
        {
            sb.Append("HEAD OFFICE - ");
            sb.Append(HttpUtilityHtmlEncode(address));
            sb.Append("<br />");
        }

        if (!string.IsNullOrWhiteSpace(phone))
        {
            sb.Append("CONTACT – ");
            sb.Append(HttpUtilityHtmlEncode(phone));
            sb.Append("<br />");
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            sb.Append("EMAIL- ");
            sb.Append(HttpUtilityHtmlEncode(email));
            sb.Append("<br />");
        }

        if (!string.IsNullOrWhiteSpace(website))
        {
            sb.Append("WEBSITE – ");
            sb.Append(HttpUtilityHtmlEncode(website));
        }

        return sb.ToString();
    }

    public static string BuildSupportContactLine()
    {
        return string.Format(
            "Support: Contact us at {0} / {1} for inquiries.",
            GetPrimaryEmail(),
            GetPrimaryPhone());
    }

    public static void BindSupportContactLine(Literal literal)
    {
        if (literal == null)
        {
            return;
        }

        literal.Text = HttpUtilityHtmlEncode(BuildSupportContactLine());
    }

    public static string BuildIncomeStatementContactLine()
    {
        return string.Format(
            "Ph: {0}, E-Mail: {1}",
            GetPrimaryPhone(),
            GetPrimaryEmail());
    }

    public static void BindIncomeStatementContact(Literal literal)
    {
        if (literal == null)
        {
            return;
        }

        literal.Text = HttpUtilityHtmlEncode(BuildIncomeStatementContactLine());
    }

    public static void BindInvoiceCompanyInfo(Literal literal)
    {
        if (literal == null)
        {
            return;
        }

        literal.Text = BuildInvoiceCompanyHtml();
    }

    public static void BindInvoiceGst(Literal literal)
    {
        if (literal == null)
        {
            return;
        }

        literal.Text = HttpUtilityHtmlEncode(BuildInvoiceGstLine());
    }

    public static void BindInvoiceCompanyInfo(Control page, string controlId)
    {
        if (page == null || string.IsNullOrWhiteSpace(controlId))
        {
            return;
        }

        Control found = page.FindControl(controlId);
        Literal literal = found as Literal;
        if (literal != null)
        {
            BindInvoiceCompanyInfo(literal);
        }
    }

    public static void BindHyperLink(HyperLink link, string href, string text)
    {
        if (link == null)
        {
            return;
        }

        link.NavigateUrl = href ?? "#";
        link.Text = text ?? string.Empty;
    }

    public static void BindHyperLink(HyperLink link, string href)
    {
        if (link == null)
        {
            return;
        }

        link.NavigateUrl = href ?? "#";
    }

    public static bool SaveContact(
        int? id,
        string contactType,
        string title,
        string contactValue,
        int displayOrder,
        bool isPrimary,
        bool status)
    {
        if (string.IsNullOrWhiteSpace(contactType) || string.IsNullOrWhiteSpace(contactValue))
        {
            return false;
        }

        EnsureTableAndSeedDefaults();

        string safeType = contactType.Trim().Replace("'", "''");
        string safeTitle = (title ?? string.Empty).Trim().Replace("'", "''");
        string safeValue = contactValue.Trim().Replace("'", "''");
        string statusBit = status ? "1" : "0";
        string primaryBit = isPrimary ? "1" : "0";

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                if (isPrimary)
                {
                    objData.RunInsUpDelQuery(
                        "UPDATE tbl_SiteContact SET IsPrimary = 0 WHERE ContactType = '" + safeType + "'");
                }

                string sql;
                if (id.HasValue && id.Value > 0)
                {
                    sql = string.Format(
                        "UPDATE tbl_SiteContact SET ContactType='{0}', Title='{1}', ContactValue='{2}', DisplayOrder={3}, IsPrimary={4}, Status={5} WHERE Id={6}",
                        safeType,
                        safeTitle,
                        safeValue,
                        displayOrder,
                        primaryBit,
                        statusBit,
                        id.Value);
                }
                else
                {
                    sql = string.Format(
                        "INSERT INTO tbl_SiteContact (ContactType, Title, ContactValue, DisplayOrder, IsPrimary, Status) VALUES ('{0}', '{1}', '{2}', {3}, {4}, {5})",
                        safeType,
                        safeTitle,
                        safeValue,
                        displayOrder,
                        primaryBit,
                        statusBit);
                }

                objData.RunInsUpDelQuery(sql);
                return true;
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    public static bool DeleteContact(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery("DELETE FROM tbl_SiteContact WHERE Id=" + id);
                return true;
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    public static DataTable GetContactById(int id)
    {
        if (id <= 0)
        {
            return new DataTable();
        }

        return RunSelect("SELECT * FROM tbl_SiteContact WHERE Id=" + id);
    }

    static DataTable RunSelect(string sql)
    {
        Data objData = new Data();
        DataTable dt = new DataTable();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable result = objData.RunDataTable(sql);
                if (result != null)
                {
                    dt = result;
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }

        return dt;
    }

    static void EnsureGstContactExists(Data objData)
    {
        DataTable gstCountTable = objData.RunDataTable(
            "SELECT COUNT(*) AS ItemCount FROM tbl_SiteContact WHERE ContactType = '" + TypeGst.Replace("'", "''") + "'");
        int gstCount = 0;
        if (gstCountTable != null && gstCountTable.Rows.Count > 0)
        {
            gstCount = Convert.ToInt32(gstCountTable.Rows[0]["ItemCount"]);
        }

        if (gstCount > 0)
        {
            return;
        }

        foreach (Tuple<string, string, string, bool> item in DefaultContacts)
        {
            if (!string.Equals(item.Item1, TypeGst, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string type = (item.Item1 ?? string.Empty).Replace("'", "''");
            string title = (item.Item2 ?? string.Empty).Replace("'", "''");
            string value = (item.Item3 ?? string.Empty).Replace("'", "''");
            string isPrimary = item.Item4 ? "1" : "0";
            string insertSql = string.Format(
                "INSERT INTO tbl_SiteContact (ContactType, Title, ContactValue, DisplayOrder, IsPrimary, Status) VALUES ('{0}', '{1}', '{2}', 6, {3}, 1)",
                type,
                title,
                value,
                isPrimary);
            objData.RunInsUpDelQuery(insertSql);
            break;
        }
    }

    static string NormalizeDigits(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return string.Empty;
        }

        StringBuilder digits = new StringBuilder();
        foreach (char ch in phone)
        {
            if (char.IsDigit(ch))
            {
                digits.Append(ch);
            }
        }

        return digits.ToString();
    }

    static string GetFallbackValue(string contactType)
    {
        foreach (Tuple<string, string, string, bool> item in DefaultContacts)
        {
            if (string.Equals(item.Item1, contactType, StringComparison.OrdinalIgnoreCase) && item.Item4)
            {
                return item.Item3;
            }
        }

        return string.Empty;
    }

    static string HttpUtilityHtmlEncode(string value)
    {
        return System.Web.HttpUtility.HtmlEncode(value ?? string.Empty);
    }
}

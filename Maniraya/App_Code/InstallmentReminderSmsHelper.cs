using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Sends installment reminder SMS via shortmsgservice.com HTTP API.
/// </summary>
public static class InstallmentReminderSmsHelper
{
    public static bool IsEnabled
    {
        get
        {
            string value = GetSetting("InstallmentReminderSmsEnabled", "true");
            return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static string BuildReminderMessage(string userName)
    {
        string name = string.IsNullOrWhiteSpace(userName) ? "Member" : userName.Trim();
        // Only name is dynamic — body must match DLT template text.
        return "Dear " + name
            + "\nYour this month installment is pending kindly pay, If already paid then ignore. Thanks Mpremium Team.";
    }

    public static bool TrySendReminder(string mobile, string userName, out string statusMessage)
    {
        statusMessage = string.Empty;

        if (!IsEnabled)
        {
            statusMessage = "SMS reminder disabled.";
            return false;
        }

        string number = NormalizeMobile(mobile);
        if (string.IsNullOrWhiteSpace(number))
        {
            statusMessage = "Valid mobile number not found.";
            return false;
        }

        string message = BuildReminderMessage(userName);
        string apiUrl = BuildApiUrl(number, message);

        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string responseText = HttpGet(apiUrl, 60000);
            if (IsSuccessResponse(responseText))
            {
                statusMessage = "SMS sent.";
                return true;
            }

            if (string.IsNullOrWhiteSpace(responseText))
            {
                statusMessage = "Empty SMS gateway response.";
                return false;
            }

            statusMessage = "SMS gateway: " + Truncate(responseText, 160);
            return false;
        }
        catch (Exception ex)
        {
            statusMessage = "SMS send failed: " + Truncate(ex.Message, 160);
            return false;
        }
    }

    static string BuildApiUrl(string mobile, string message)
    {
        string baseUrl = GetSetting("InstallmentReminderSmsApiUrl",
            "http://sms.shortmsgservice.com/sms-panel/api/http/index.php");
        string username = GetSetting("InstallmentReminderSmsUsername", "mpremium");
        string apiKey = GetSetting("InstallmentReminderSmsApiKey", "E0893-22520");
        string sender = GetSetting("InstallmentReminderSmsSender", "REGISR");
        string route = GetSetting("InstallmentReminderSmsRoute", "TRANS");
        string templateId = GetSetting("InstallmentReminderSmsTemplateId", "1677100000000388862");

        // Existing site SMS calls append literal "sms" before route (gateway quirk / template suffix).
        string messageWithSuffix = (message ?? string.Empty).TrimEnd() + "sms";

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["username"] = username;
        query["apikey"] = apiKey;
        query["apirequest"] = "Text";
        query["sender"] = sender;
        query["mobile"] = mobile;
        query["message"] = messageWithSuffix;
        query["route"] = route;
        query["TemplateID"] = templateId;
        query["format"] = "JSON";

        string url = baseUrl.Trim();
        if (url.Contains("?"))
        {
            return url.TrimEnd('&') + "&" + query;
        }
        return url + "?" + query;
    }

    static string NormalizeMobile(string mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
        {
            return string.Empty;
        }

        StringBuilder digits = new StringBuilder();
        foreach (char ch in mobile.Trim())
        {
            if (char.IsDigit(ch))
            {
                digits.Append(ch);
            }
        }

        string value = digits.ToString();
        if (value.StartsWith("91") && value.Length > 10)
        {
            value = value.Substring(value.Length - 10);
        }

        if (value.Length != 10)
        {
            return string.Empty;
        }

        return value;
    }

    static bool IsSuccessResponse(string responseText)
    {
        if (string.IsNullOrWhiteSpace(responseText))
        {
            return false;
        }

        return responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("\"status\": \"success\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("\"Status\":\"Success\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("success", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    static string HttpGet(string url, int timeoutMs)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = timeoutMs;
        request.ReadWriteTimeout = timeoutMs;
        request.KeepAlive = false;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    static string GetSetting(string key, string defaultValue)
    {
        string value = ConfigurationManager.AppSettings[key];
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
    }

    static string Truncate(string value, int maxLen)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLen)
        {
            return value ?? string.Empty;
        }
        return value.Substring(0, maxLen);
    }
}

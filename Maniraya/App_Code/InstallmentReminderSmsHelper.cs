using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;

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

    /// <summary>
    /// DLT template body. Only the member name after "Dear " is dynamic.
    /// Trailing "sms" matches gateway/OTP URL style used on this site.
    /// </summary>
    public static string BuildReminderMessage(string userName)
    {
        string name = string.IsNullOrWhiteSpace(userName) ? "Member" : userName.Trim();
        return "Dear " + name
            + " Your this month installment is pending kindly pay, If already paid then ignore. Thanks Mpremuim Team.sms";
    }

    public static bool TrySendReminder(string mobile, string userName, out string statusMessage)
    {
        string apiUrl;
        return TrySendReminder(mobile, userName, out statusMessage, out apiUrl);
    }

    /// <summary>
    /// Builds full API URL into apiUrl (for debug), then hits the gateway.
    /// </summary>
    public static bool TrySendReminder(string mobile, string userName, out string statusMessage, out string apiUrl)
    {
        statusMessage = string.Empty;
        apiUrl = string.Empty;

        if (!IsEnabled)
        {
            statusMessage = "SMS reminder disabled.";
            WriteLog("SKIP disabled");
            return false;
        }

        string number = NormalizeMobile(mobile);
        if (string.IsNullOrWhiteSpace(number))
        {
            statusMessage = "Valid mobile not found (" + Truncate(mobile, 20) + ").";
            WriteLog("SKIP invalid mobile raw=" + Truncate(mobile, 40));
            return false;
        }

        string displayName = string.IsNullOrWhiteSpace(userName) ? "Member" : userName.Trim();
        string message = BuildReminderMessage(displayName);

        // Full API string for debug — inspect this before / after hit.
        string str = BuildApiUrl(number, message);
        apiUrl = str;
        WriteLog("API str=" + str);

        try
        {
            string responseText = HttpGet(str, 60000);
            WriteLog("RESP mobile=" + number + " " + Truncate(responseText, 400));

            if (IsSuccessResponse(responseText))
            {
                statusMessage = "SMS Sent Successfully";
                return true;
            }

            if (string.IsNullOrWhiteSpace(responseText))
            {
                statusMessage = "Empty SMS gateway response. API=" + Truncate(str, 250);
                return false;
            }

            statusMessage = Truncate(responseText, 180) + " | API=" + Truncate(str, 220);
            return false;
        }
        catch (WebException webEx)
        {
            string body = ReadWebExceptionBody(webEx);
            statusMessage = "SMS failed: " + Truncate(string.IsNullOrWhiteSpace(body) ? webEx.Message : body, 180)
                + " | API=" + Truncate(str, 220);
            WriteLog("WEBEX mobile=" + number + " " + statusMessage);
            return false;
        }
        catch (Exception ex)
        {
            statusMessage = "SMS failed: " + Truncate(ex.Message, 180) + " | API=" + Truncate(str, 220);
            WriteLog("EXCEPTION mobile=" + number + " " + ex.ToString());
            return false;
        }
    }

    /// <summary>
    /// Panel login: username=mpremium + apikey. Recipient: mobile= + Dear NAME in message.
    /// </summary>
    static string BuildApiUrl(string mobile, string message)
    {
        string baseUrl = GetSetting("InstallmentReminderSmsApiUrl",
            "http://sms.shortmsgservice.com/sms-panel/api/http/index.php");
        string username = GetSetting("InstallmentReminderSmsUsername", "mpremium");
        string apiKey = GetSetting("InstallmentReminderSmsApiKey", "E0893-22520");
        string sender = GetSetting("InstallmentReminderSmsSender", "REGISR");
        string route = GetSetting("InstallmentReminderSmsRoute", "TRANS");
        string templateId = GetSetting("InstallmentReminderSmsTemplateId", "1677100000000388862");

        // Same style as working OTP Sendotp URL on this site.
        StringBuilder url = new StringBuilder();
        url.Append(baseUrl.Trim());
        url.Append(baseUrl.Contains("?") ? "&" : "?");
        url.Append("username=").Append(username);
        url.Append("&apikey=").Append(apiKey);
        url.Append("&apirequest=Text");
        url.Append("&sender=").Append(sender);
        url.Append("&mobile=").Append(mobile);
        url.Append("&message=").Append(HttpUtility.UrlEncode(message ?? string.Empty));
        url.Append("&route=").Append(route);
        url.Append("&TemplateID=").Append(templateId);
        url.Append("&format=JSON");
        return url.ToString();
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
        if (value.Length > 10 && (value.StartsWith("91") || value.StartsWith("0")))
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

        if (responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("\"status\": \"success\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("SMS Sent Successfully", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return true;
        }

        if (responseText.IndexOf("\"status\":\"error\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("\"status\": \"error\"", StringComparison.OrdinalIgnoreCase) >= 0
            || responseText.IndexOf("fail", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return false;
        }

        return false;
    }

    static string HttpGet(string url, int timeoutMs)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = timeoutMs;
        request.ReadWriteTimeout = timeoutMs;
        request.KeepAlive = false;
        request.ProtocolVersion = HttpVersion.Version11;
        request.UserAgent = "ManirayaSmsClient/1.0";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    static string ReadWebExceptionBody(WebException webEx)
    {
        try
        {
            if (webEx.Response == null)
            {
                return webEx.Message;
            }

            using (Stream stream = webEx.Response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
            {
                string body = reader.ReadToEnd();
                return string.IsNullOrWhiteSpace(body) ? webEx.Message : body;
            }
        }
        catch
        {
            return webEx.Message;
        }
    }

    static void WriteLog(string line)
    {
        try
        {
            string folder = HostingEnvironment.MapPath("~/App_Data");
            if (string.IsNullOrWhiteSpace(folder))
            {
                return;
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, "installment-reminder-sms.log");
            File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + line + Environment.NewLine);
        }
        catch
        {
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

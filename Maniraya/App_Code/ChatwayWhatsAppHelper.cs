using DataTier;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;

public static class ChatwayWhatsAppHelper
{
    public enum InvoiceMessageType
    {
        FirstPurchase,
        Reinstallment
    }

    public static bool IsEnabled
    {
        get
        {
            string value = GetSetting("ChatwayEnabled", "true");
            return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static string InvoiceAccessKey
    {
        get { return GetSetting("ChatwayInvoiceAccessKey", string.Empty); }
    }

    public static bool TrySendInvoiceAfterApprove(string userId, string orderId, InvoiceMessageType messageType, out string statusMessage)
    {
        return TrySendInvoiceAfterApprove(userId, orderId, 0, messageType, out statusMessage);
    }

    public static bool TrySendInvoiceAfterApprove(string userId, string orderId, int installmentId, InvoiceMessageType messageType, out string statusMessage)
    {
        statusMessage = string.Empty;

        if (!IsEnabled)
        {
            statusMessage = "WhatsApp disabled.";
            WriteLog("SKIP enabled=false userId=" + userId + " orderId=" + orderId + " installmentId=" + installmentId);
            return false;
        }

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(orderId))
        {
            statusMessage = "UserId or OrderId missing.";
            WriteLog("SKIP missing ids userId=" + userId + " orderId=" + orderId);
            return false;
        }

        string mobile = GetUserMobile(userId);
        string number = NormalizeWhatsAppNumber(mobile);
        if (string.IsNullOrWhiteSpace(number))
        {
            statusMessage = "Valid mobile number not found.";
            WriteLog("SKIP invalid mobile userId=" + userId + " mobile=" + mobile);
            return false;
        }

        string message = GetMessageTemplate(messageType);

        // WhatsApp needs a REAL PDF body. SavingProductInvoice.aspx is HTML — naming it
        // Invoice.pdf caused "Format error. Failed to open file." on phones.
        // Use SavingProductInvoicePdf.ashx (application/pdf) + accessKey for Chatway fetch.
        string fileName = "Invoice.pdf";
        string fileUrl = BuildInvoiceFileUrl(orderId, userId, true, installmentId);
        WriteLog("USING invoice PDF url=" + fileUrl);

        if (PreferStaticPdfUrl())
        {
            string pdfError;
            string pdfFileName;
            string pdfUrl = SavingInvoicePdfHelper.SaveInvoicePdfAndGetPublicUrl(orderId, userId, installmentId, out pdfFileName, out pdfError);
            if (!string.IsNullOrWhiteSpace(pdfUrl))
            {
                fileName = string.IsNullOrWhiteSpace(pdfFileName) ? "Invoice.pdf" : pdfFileName;
                fileUrl = pdfUrl;
                WriteLog("PDF SAVED fileName=" + fileName + " publicUrl=" + fileUrl);
            }
            else
            {
                WriteLog("PDF SAVE FAIL " + pdfError + " keeping ashx url=" + fileUrl);
            }
        }

        WriteLog("START userId=" + userId + " orderId=" + orderId + " installmentId=" + installmentId + " number=" + number + " type=" + messageType);

        // Background only when explicitly enabled. Default is sync so Approve shows real Chatway result.
        if (IsBackgroundSendEnabled())
        {
            string numberCopy = number;
            string messageCopy = message;
            string fileUrlCopy = fileUrl;
            string fileNameCopy = fileName;
            string userIdCopy = userId;
            string orderIdCopy = orderId;
            int installmentIdCopy = installmentId;

            Action sendAction = delegate
            {
                try
                {
                    string ignored;
                    bool ok = TrySendFile(numberCopy, messageCopy, fileUrlCopy, fileNameCopy, out ignored);
                    WriteLog((ok ? "BG OK " : "BG FAIL ") + ignored + " userId=" + userIdCopy + " orderId=" + orderIdCopy + " installmentId=" + installmentIdCopy);
                }
                catch (Exception ex)
                {
                    WriteLog("BG EXCEPTION " + ex.Message + " userId=" + userIdCopy + " orderId=" + orderIdCopy + " installmentId=" + installmentIdCopy);
                }
            };

            try
            {
                HostingEnvironment.QueueBackgroundWorkItem(ct => sendAction());
            }
            catch
            {
                ThreadPool.QueueUserWorkItem(delegate { sendAction(); });
            }

            statusMessage = "WhatsApp invoice queued.";
            return true;
        }

        bool sent = TrySendFile(number, message, fileUrl, fileName, out statusMessage);
        WriteLog((sent ? "OK " : "FAIL ") + statusMessage + " userId=" + userId + " orderId=" + orderId + " installmentId=" + installmentId + " number=" + number);
        return sent;
    }

    public static bool QueueSendInvoiceAfterApprove(string userId, string orderId, InvoiceMessageType messageType, out string statusMessage)
    {
        return TrySendInvoiceAfterApprove(userId, orderId, 0, messageType, out statusMessage);
    }

    public static bool TrySendTextMessage(string mobile, string message, out string statusMessage)
    {
        statusMessage = string.Empty;

        if (!IsEnabled)
        {
            statusMessage = "WhatsApp disabled.";
            return false;
        }

        string number = NormalizeWhatsAppNumber(mobile);
        if (string.IsNullOrWhiteSpace(number))
        {
            statusMessage = "Valid mobile number not found.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            statusMessage = "Message is empty.";
            return false;
        }

        string username = GetSetting("ChatwayUsername", string.Empty);
        string token = GetSetting("ChatwayToken", string.Empty);
        string fileApi = GetSetting("ChatwayApiBaseUrl", "https://int.chatway.in/api/send-file");
        string apiBase = GetSetting("ChatwayTextApiBaseUrl", string.Empty);
        if (string.IsNullOrWhiteSpace(apiBase))
        {
            apiBase = fileApi.Replace("/send-file", "/send");
            if (string.Equals(apiBase, fileApi, StringComparison.OrdinalIgnoreCase))
            {
                apiBase = "https://int.chatway.in/api/send";
            }
        }

        int timeoutMs = GetTimeoutMilliseconds();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(token))
        {
            statusMessage = "Chatway credentials missing.";
            return false;
        }

        try
        {
            EnsureTls12();

            string apiNumber = number.Trim().TrimStart('+');
            string numberWithPlus = "+" + apiNumber;
            string responseText;
            bool ok = ExecuteSendTextRequest(username, token, apiBase, numberWithPlus, message, timeoutMs, out responseText);

            if (!ok && IsGenericChatwayError(responseText))
            {
                WriteLog("TEXT RETRY without + prefix number=" + apiNumber);
                ok = ExecuteSendTextRequest(username, token, apiBase, apiNumber, message, timeoutMs, out responseText);
            }

            WriteLog("TEXT RESPONSE " + Truncate(responseText, 500));

            if (ok
                || (!string.IsNullOrWhiteSpace(responseText)
                    && (responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
                        || responseText.IndexOf("accepted for delivery", StringComparison.OrdinalIgnoreCase) >= 0)))
            {
                statusMessage = "WhatsApp reminder sent.";
                return true;
            }

            if (string.IsNullOrWhiteSpace(responseText))
            {
                statusMessage = "Empty response from Chatway.";
                return false;
            }

            if (IsGenericChatwayError(responseText))
            {
                statusMessage = "Chatway could not deliver reminder.";
                return false;
            }

            statusMessage = "Chatway response: " + Truncate(responseText, 180);
            return false;
        }
        catch (WebException webEx)
        {
            string body = ReadWebExceptionBody(webEx);
            statusMessage = "WhatsApp send failed: " + Truncate(string.IsNullOrWhiteSpace(body) ? webEx.Message : body, 160);
            WriteLog("TEXT WEBEX " + statusMessage);
            return false;
        }
        catch (Exception ex)
        {
            statusMessage = "WhatsApp send failed: " + Truncate(ex.Message, 160);
            WriteLog("TEXT EXCEPTION " + ex.Message);
            return false;
        }
    }

    static bool ExecuteSendTextRequest(
        string username,
        string token,
        string apiBase,
        string number,
        string message,
        int timeoutMs,
        out string responseText)
    {
        responseText = string.Empty;

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["username"] = username.Trim();
        query["number"] = number.Trim();
        query["message"] = message ?? string.Empty;
        query["token"] = token.Trim();

        string requestUrl = apiBase.Trim();
        if (requestUrl.Contains("?"))
        {
            requestUrl = requestUrl.TrimEnd('&') + "&" + query;
        }
        else
        {
            requestUrl = requestUrl + "?" + query;
        }

        WriteLog("TEXT REQUEST number=" + number);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
        request.Method = "GET";
        request.Timeout = timeoutMs;
        request.ReadWriteTimeout = timeoutMs;
        request.KeepAlive = false;
        request.ProtocolVersion = HttpVersion.Version11;
        request.UserAgent = "ManirayaChatwayClient/1.0";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
        {
            responseText = reader.ReadToEnd();
        }

        return !string.IsNullOrWhiteSpace(responseText)
            && (responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
                || responseText.IndexOf("accepted for delivery", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public static bool TrySendFile(string number, string message, string fileUrl, string fileName, out string statusMessage)
    {
        statusMessage = string.Empty;

        string username = GetSetting("ChatwayUsername", string.Empty);
        string token = GetSetting("ChatwayToken", string.Empty);
        string apiBase = GetSetting("ChatwayApiBaseUrl", "https://int.chatway.in/api/send-file");
        int timeoutMs = GetTimeoutMilliseconds();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(token))
        {
            statusMessage = "Chatway credentials missing.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(fileUrl))
        {
            statusMessage = "Number or file URL missing.";
            return false;
        }

        try
        {
            EnsureTls12();

            // Working Chatway sample used +91... ; response echoed 91... without '+'.
            string apiNumber = number.Trim().TrimStart('+');
            string numberWithPlus = "+" + apiNumber;

            string responseText;
            bool ok = ExecuteSendFileRequest(username, token, apiBase, numberWithPlus, message, fileUrl, fileName, timeoutMs, out responseText);

            bool isPdfEndpoint = IsPdfFileUrl(fileUrl);

            // Retry without '+' (some accounts prefer plain 91...).
            if (!ok && IsGenericChatwayError(responseText))
            {
                WriteLog("RETRY without + prefix number=" + apiNumber);
                ok = ExecuteSendFileRequest(username, token, apiBase, apiNumber, message, fileUrl, fileName, timeoutMs, out responseText);
            }

            // Retry without accessKey only for legacy HTML invoice pages (not PDF endpoints).
            if (!ok && !isPdfEndpoint && IsGenericChatwayError(responseText))
            {
                string oid = orderIdFromFileUrl(fileUrl);
                if (string.IsNullOrWhiteSpace(oid))
                {
                    oid = ExtractOrderIdFromAnyUrl(fileUrl);
                }

                string simpleUrl = BuildInvoiceFileUrl(oid, null, false);
                WriteLog("RETRY without accessKey/userId file_url=" + simpleUrl);
                ok = ExecuteSendFileRequest(username, token, apiBase, numberWithPlus, message, simpleUrl, "Invoice.pdf", timeoutMs, out responseText);
            }

            WriteLog("RESPONSE " + Truncate(responseText, 500));

            if (string.IsNullOrWhiteSpace(responseText))
            {
                statusMessage = "Empty response from Chatway.";
                return false;
            }

            if (ok
                || responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
                || responseText.IndexOf("accepted for delivery", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                statusMessage = "WhatsApp invoice sent.";
                return true;
            }

            if (IsGenericChatwayError(responseText))
            {
                statusMessage = "Chatway could not deliver (Something went wrong). Check number on WhatsApp and invoice URL access.";
                return false;
            }

            statusMessage = "Chatway response: " + Truncate(responseText, 180);
            return false;
        }
        catch (WebException webEx)
        {
            string body = ReadWebExceptionBody(webEx);
            if (webEx.Status == WebExceptionStatus.Timeout)
            {
                statusMessage = "WhatsApp send timed out. Chatway did not respond in time.";
            }
            else if (!string.IsNullOrWhiteSpace(body))
            {
                statusMessage = "WhatsApp send failed: " + Truncate(body, 160);
            }
            else
            {
                statusMessage = "WhatsApp send failed: " + Truncate(webEx.Message, 160);
            }
            WriteLog("WEBEX status=" + webEx.Status + " msg=" + webEx.Message + " body=" + Truncate(body, 300));
            return false;
        }
        catch (Exception ex)
        {
            statusMessage = "WhatsApp send failed: " + Truncate(ex.Message, 160);
            WriteLog("EXCEPTION " + ex.Message);
            return false;
        }
    }

    static bool ExecuteSendFileRequest(
        string username,
        string token,
        string apiBase,
        string number,
        string message,
        string fileUrl,
        string fileName,
        int timeoutMs,
        out string responseText)
    {
        responseText = string.Empty;

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["username"] = username.Trim();
        query["number"] = number.Trim();
        query["message"] = message ?? string.Empty;
        query["token"] = token.Trim();
        query["file_url"] = fileUrl.Trim();
        query["file_name"] = string.IsNullOrWhiteSpace(fileName) ? "Invoice.pdf" : fileName.Trim();

        string requestUrl = apiBase.Trim();
        if (requestUrl.Contains("?"))
        {
            requestUrl = requestUrl.TrimEnd('&') + "&" + query;
        }
        else
        {
            requestUrl = requestUrl + "?" + query;
        }

        WriteLog("REQUEST number=" + number + " file_url=" + fileUrl + " file_name=" + fileName);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
        request.Method = "GET";
        request.Timeout = timeoutMs;
        request.ReadWriteTimeout = timeoutMs;
        request.KeepAlive = false;
        request.ProtocolVersion = HttpVersion.Version11;
        request.UserAgent = "ManirayaChatwayClient/1.0";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
        {
            responseText = reader.ReadToEnd();
        }

        return !string.IsNullOrWhiteSpace(responseText)
            && (responseText.IndexOf("\"status\":\"success\"", StringComparison.OrdinalIgnoreCase) >= 0
                || responseText.IndexOf("accepted for delivery", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    static bool IsGenericChatwayError(string responseText)
    {
        if (string.IsNullOrWhiteSpace(responseText))
        {
            return false;
        }

        return responseText.IndexOf("Something went wrong", StringComparison.OrdinalIgnoreCase) >= 0
            || (responseText.IndexOf("\"status\":\"error\"", StringComparison.OrdinalIgnoreCase) >= 0
                && responseText.IndexOf("success", StringComparison.OrdinalIgnoreCase) < 0);
    }

    static string orderIdFromFileUrl(string fileUrl)
    {
        return ExtractOrderIdFromAnyUrl(fileUrl);
    }

    static string ExtractOrderIdFromAnyUrl(string fileUrl)
    {
        try
        {
            Uri uri;
            if (!Uri.TryCreate(fileUrl, UriKind.Absolute, out uri))
            {
                return string.Empty;
            }

            string orderId = HttpUtility.ParseQueryString(uri.Query).Get("orderId");
            return orderId ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    static bool PreferStaticPdfUrl()
    {
        string value = GetSetting("ChatwayPreferStaticPdfUrl", "false");
        return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsPdfFileUrl(string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
        {
            return false;
        }

        return fileUrl.IndexOf("/InvoiceFiles/", StringComparison.OrdinalIgnoreCase) >= 0
            || fileUrl.IndexOf("SavingProductInvoicePdf.ashx", StringComparison.OrdinalIgnoreCase) >= 0
            || fileUrl.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
    }

    static bool IncludeAccessKeyInPrimaryUrl()
    {
        // Default true — PDF ashx requires accessKey for Chatway (no login session).
        string value = GetSetting("ChatwayIncludeAccessKeyInUrl", "true");
        return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
    }

    public static string GetMessageTemplate(InvoiceMessageType messageType)
    {
        if (messageType == InvoiceMessageType.Reinstallment)
        {
            return "Thank you for paying your Saving Product installment with Maniraya Enterprises. Kindly download this Invoice for future use.";
        }

        return "Thank you for purchasing from Maniraya Enterprises. Kindly download this Invoice for future use.";
    }

    public static string BuildInvoiceFileUrl(string orderId, string userId)
    {
        return BuildInvoiceFileUrl(orderId, userId, true, 0);
    }

    public static string BuildInvoiceFileUrl(string orderId, string userId, bool includeAccessKey)
    {
        return BuildInvoiceFileUrl(orderId, userId, includeAccessKey, 0);
    }

    public static string BuildInvoiceFileUrl(string orderId, string userId, bool includeAccessKey, int installmentId)
    {
        string baseUrl = GetSetting("ChatwayInvoiceBaseUrl", "https://mpremium.in/user/SavingProductInvoicePdf.ashx");
        var builder = new StringBuilder();
        builder.Append(baseUrl.Trim());
        builder.Append(baseUrl.Contains("?") ? "&" : "?");
        builder.Append("orderId=").Append(HttpUtility.UrlEncode((orderId ?? string.Empty).Trim()));

        // PDF ashx needs userId on live — without it the endpoint returns 500 and Chatway fails.
        bool includeUserId = true;
        string includeUserIdSetting = GetSetting("ChatwayIncludeUserIdInUrl", "true");
        if (string.Equals(includeUserIdSetting, "0", StringComparison.OrdinalIgnoreCase)
            || string.Equals(includeUserIdSetting, "false", StringComparison.OrdinalIgnoreCase)
            || string.Equals(includeUserIdSetting, "no", StringComparison.OrdinalIgnoreCase))
        {
            includeUserId = false;
        }

        if (includeUserId && !string.IsNullOrWhiteSpace(userId))
        {
            builder.Append("&userId=").Append(HttpUtility.UrlEncode(userId.Trim()));
        }

        if (installmentId > 0)
        {
            builder.Append("&installmentId=").Append(installmentId.ToString());
        }

        if (includeAccessKey)
        {
            string accessKey = InvoiceAccessKey;
            if (!string.IsNullOrWhiteSpace(accessKey))
            {
                builder.Append("&accessKey=").Append(HttpUtility.UrlEncode(accessKey.Trim()));
            }
        }

        return builder.ToString();
    }

    public static string NormalizeWhatsAppNumber(string mobile)
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
        if (value.StartsWith("00"))
        {
            value = value.Substring(2);
        }

        if (value.Length == 10)
        {
            value = "91" + value;
        }

        if (value.Length < 10 || value.Length > 15)
        {
            return string.Empty;
        }

        // Chatway success responses use 91xxxxxxxxxx (no '+').
        return value;
    }

    static int GetTimeoutMilliseconds()
    {
        int seconds;
        if (!int.TryParse(GetSetting("ChatwayTimeoutSeconds", "120"), out seconds) || seconds < 30)
        {
            seconds = 120;
        }

        if (seconds > 300)
        {
            seconds = 300;
        }

        return seconds * 1000;
    }

    static bool IsBackgroundSendEnabled()
    {
        string value = GetSetting("ChatwayBackgroundSend", "false");
        return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
    }

    static void EnsureTls12()
    {
        try
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12;

            const int Tls13 = 12288;
            try
            {
                ServicePointManager.SecurityProtocol |= (SecurityProtocolType)Tls13;
            }
            catch
            {
            }
        }
        catch
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }

    static string GetUserMobile(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return string.Empty;
        }

        string sql = @"
SELECT TOP 1 LTRIM(RTRIM(ISNULL(ud.Mobile, ''))) AS Mobile
FROM UserDetail ud WITH (NOLOCK)
WHERE LTRIM(RTRIM(ud.UserId)) = '" + SqlEscape(userId.Trim()) + "'";

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable dt = objData.RunDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["Mobile"]);
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

        return string.Empty;
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

            string path = Path.Combine(folder, "chatway-whatsapp.log");
            File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + (line ?? string.Empty) + Environment.NewLine);
        }
        catch
        {
        }
    }

    static string ReadWebExceptionBody(WebException webEx)
    {
        try
        {
            if (webEx == null || webEx.Response == null)
            {
                return string.Empty;
            }

            using (Stream stream = webEx.Response.GetResponseStream())
            {
                if (stream == null)
                {
                    return string.Empty;
                }

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch
        {
            return string.Empty;
        }
    }

    static string GetSetting(string key, string defaultValue)
    {
        try
        {
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
        }
        catch
        {
        }

        return defaultValue ?? string.Empty;
    }

    static string SanitizeFileName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Invoice";
        }

        char[] invalid = Path.GetInvalidFileNameChars();
        StringBuilder sb = new StringBuilder();
        foreach (char ch in value.Trim())
        {
            if (Array.IndexOf(invalid, ch) >= 0 || ch == ' ')
            {
                sb.Append('_');
            }
            else
            {
                sb.Append(ch);
            }
        }

        string cleaned = sb.ToString().Trim('_');
        return string.IsNullOrWhiteSpace(cleaned) ? "Invoice" : cleaned;
    }

    static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value ?? string.Empty;
        }

        return value.Substring(0, maxLength) + "...";
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

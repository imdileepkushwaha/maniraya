<%@ WebHandler Language="C#" Class="CashfreeWebhook" %>

using System;
using System.IO;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;

public class CashfreeWebhook : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.TrySkipIisCustomErrors = true;

        if (!string.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
        {
            WriteOk(context, "Cashfree webhook ready");
            return;
        }

        string rawBody;
        context.Request.InputStream.Position = 0;
        using (StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
        {
            rawBody = reader.ReadToEnd();
        }

        string timestamp = Convert.ToString(context.Request.Headers["x-webhook-timestamp"]);
        string signature = Convert.ToString(context.Request.Headers["x-webhook-signature"]);

        // Cashfree dashboard Test sends a POST and requires HTTP 200.
        if (IsTestPing(rawBody, timestamp, signature))
        {
            WriteOk(context, "test accepted");
            return;
        }

        try
        {
            if (!CashfreeHelper.VerifyWebhookSignature(timestamp, rawBody, signature))
            {
                context.Response.StatusCode = 401;
                context.Response.Write("{\"ok\":false,\"message\":\"invalid signature\"}");
                return;
            }

            string result = CashfreeHelper.HandleWebhook(rawBody);
            WriteOk(context, result ?? "ok");
        }
        catch (Exception)
        {
            WriteOk(context, "received");
        }
    }

    static bool IsTestPing(string rawBody, string timestamp, string signature)
    {
        if (string.IsNullOrWhiteSpace(rawBody))
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(signature) || string.IsNullOrWhiteSpace(timestamp))
        {
            return true;
        }

        try
        {
            JObject json = JObject.Parse(rawBody);
            string type = json["type"] != null ? Convert.ToString(json["type"]) : string.Empty;
            if (!string.IsNullOrWhiteSpace(type)
                && type.IndexOf("TEST", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            JObject data = json["data"] as JObject;
            JObject order = data != null ? data["order"] as JObject : json["order"] as JObject;
            string orderId = string.Empty;
            if (order != null && order["order_id"] != null)
            {
                orderId = Convert.ToString(order["order_id"]);
            }
            else if (json["order_id"] != null)
            {
                orderId = Convert.ToString(json["order_id"]);
            }

            return string.IsNullOrWhiteSpace(orderId);
        }
        catch
        {
            return true;
        }
    }

    static void WriteOk(HttpContext context, string message)
    {
        context.Response.StatusCode = 200;
        context.Response.Write("{\"ok\":true,\"message\":\"" + (message ?? "ok").Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}");
    }

    public bool IsReusable
    {
        get { return false; }
    }
}

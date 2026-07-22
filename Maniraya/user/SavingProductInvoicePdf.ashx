<%@ WebHandler Language="C#" Class="SavingProductInvoicePdf" %>

using System;
using System.Configuration;
using System.Globalization;
using System.Web;

public class SavingProductInvoicePdf : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string orderId = Convert.ToString(context.Request.QueryString["orderId"]).Trim();
            string userId = Convert.ToString(context.Request.QueryString["userId"]).Trim();
            string accessKey = Convert.ToString(context.Request.QueryString["accessKey"]).Trim();

            string configuredKey = Convert.ToString(ConfigurationManager.AppSettings["ChatwayInvoiceAccessKey"]).Trim();
            bool keyOk = !string.IsNullOrWhiteSpace(configuredKey)
                && string.Equals(accessKey, configuredKey, StringComparison.Ordinal);

            bool isLoggedIn = false;
            try
            {
                isLoggedIn = context.Session != null
                    && (context.Session["userid"] != null || context.Session["useradmin"] != null);
            }
            catch
            {
                isLoggedIn = false;
            }

            if (!keyOk && !isLoggedIn)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Unauthorized");
                return;
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("orderId required");
                return;
            }

            byte[] pdf = SavingInvoicePdfHelper.BuildInvoicePdf(orderId, userId);
            if (pdf == null || pdf.Length == 0)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Invoice not found");
                return;
            }

            string fileName = "Invoice_" + orderId.Replace(" ", "_") + ".pdf";
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("Content-Disposition", "inline; filename=\"" + fileName + "\"");
            context.Response.AddHeader("Content-Length", pdf.Length.ToString(CultureInfo.InvariantCulture));
            context.Response.BinaryWrite(pdf);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            context.Response.Write("Invoice PDF error: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}

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
            int installmentId = 0;
            int.TryParse(Convert.ToString(context.Request.QueryString["installmentId"]).Trim(), out installmentId);

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

            if (string.IsNullOrWhiteSpace(orderId) && installmentId <= 0)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("orderId or installmentId required");
                return;
            }

            byte[] pdf = SavingInvoicePdfHelper.BuildInvoicePdf(orderId, userId, installmentId);
            if (pdf == null || pdf.Length == 0)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Invoice not found");
                return;
            }

            // Verify PDF magic so WhatsApp never gets HTML/text labeled as PDF.
            if (pdf.Length < 5
                || pdf[0] != (byte)'%'
                || pdf[1] != (byte)'P'
                || pdf[2] != (byte)'D'
                || pdf[3] != (byte)'F')
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Generated content is not a valid PDF");
                return;
            }

            string safeOrder = (orderId ?? string.Empty).Replace(" ", "_").Replace("\"", "").Replace("'", "");
            if (string.IsNullOrWhiteSpace(safeOrder) && installmentId > 0)
            {
                safeOrder = "I" + installmentId.ToString(CultureInfo.InvariantCulture);
            }
            else if (installmentId > 0)
            {
                safeOrder = safeOrder + "_I" + installmentId.ToString(CultureInfo.InvariantCulture);
            }

            string fileName = "Invoice_" + safeOrder + ".pdf";
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("Content-Disposition", "inline; filename=\"" + fileName + "\"");
            context.Response.AddHeader("Content-Length", pdf.Length.ToString(CultureInfo.InvariantCulture));
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

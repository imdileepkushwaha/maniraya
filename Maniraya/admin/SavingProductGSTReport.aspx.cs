using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SavingProductGSTReport : System.Web.UI.Page
{
    readonly Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindReport();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        gvGst.PageIndex = 0;
        BindReport();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOrderId.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        gvGst.PageIndex = 0;
        BindReport();
    }

    protected void gvGst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGst.PageIndex = e.NewPageIndex;
        BindReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = BuildGstReport();

        decimal totalPrice = 0m, totalCgst = 0m, totalSgst = 0m, totalIgst = 0m;

        StringBuilder sb = new StringBuilder();
        sb.Append("<table border='1' cellspacing='0' cellpadding='4'>");
        sb.Append("<tr style='background:#1f7a45;color:#ffffff;font-weight:bold;'>");
        sb.Append("<th>#</th><th>Order ID</th><th>Date</th><th>Member Name</th><th>User ID</th>")
          .Append("<th>Product(s)</th><th>Price</th><th>CGST</th><th>SGST</th><th>IGST</th>");
        sb.Append("</tr>");

        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            decimal price = ParseDecimal(row["price"]);
            decimal cgst = ParseDecimal(row["cgst"]);
            decimal sgst = ParseDecimal(row["sgst"]);
            decimal igst = ParseDecimal(row["igst"]);

            totalPrice += price;
            totalCgst += cgst;
            totalSgst += sgst;
            totalIgst += igst;

            sb.Append("<tr>");
            sb.Append("<td>").Append(i++).Append("</td>");
            sb.Append("<td style=\"mso-number-format:'\\@';\">").Append(HtmlEnc(row["orderid"])).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(GetDateDisplay(row["orderdate"]))).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(row["username"])).Append("</td>");
            sb.Append("<td style=\"mso-number-format:'\\@';\">").Append(HtmlEnc(row["userid"])).Append("</td>");
            sb.Append("<td>").Append(HtmlEnc(row["productname"])).Append("</td>");
            sb.Append("<td>").Append(price.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
            sb.Append("<td>").Append(cgst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
            sb.Append("<td>").Append(sgst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
            sb.Append("<td>").Append(igst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
            sb.Append("</tr>");
        }

        sb.Append("<tr style='background:#eafaf0;font-weight:bold;'>");
        sb.Append("<td colspan='6' style='text-align:right;'>Total</td>");
        sb.Append("<td>").Append(totalPrice.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
        sb.Append("<td>").Append(totalCgst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
        sb.Append("<td>").Append(totalSgst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
        sb.Append("<td>").Append(totalIgst.ToString("N2", CultureInfo.InvariantCulture)).Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");

        string fileName = "SavingProductGSTReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "utf-8";
        Response.Output.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    static string HtmlEnc(object value)
    {
        return System.Web.HttpUtility.HtmlEncode(Convert.ToString(value));
    }

    public string GetDateDisplay(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "-";
        }

        DateTime parsed;
        if (DateTime.TryParse(Convert.ToString(value), out parsed))
        {
            return parsed.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
        }

        return "-";
    }

    void BindReport()
    {
        DataTable orders = BuildGstReport();
        gvGst.DataSource = orders;
        gvGst.DataBind();

        decimal totalPrice = 0m, totalCgst = 0m, totalSgst = 0m, totalIgst = 0m;
        foreach (DataRow row in orders.Rows)
        {
            totalPrice += ParseDecimal(row["price"]);
            totalCgst += ParseDecimal(row["cgst"]);
            totalSgst += ParseDecimal(row["sgst"]);
            totalIgst += ParseDecimal(row["igst"]);
        }

        lblTotalOrders.Text = orders.Rows.Count.ToString();
        lblTotalPrice.Text = totalPrice.ToString("N2", CultureInfo.InvariantCulture);
        lblTotalCgst.Text = totalCgst.ToString("N2", CultureInfo.InvariantCulture);
        lblTotalSgst.Text = totalSgst.ToString("N2", CultureInfo.InvariantCulture);
        lblTotalIgst.Text = totalIgst.ToString("N2", CultureInfo.InvariantCulture);
    }

    DataTable BuildGstReport()
    {
        DataTable result = new DataTable();
        result.Columns.Add("orderid", typeof(string));
        result.Columns.Add("orderdate", typeof(DateTime));
        result.Columns.Add("userid", typeof(string));
        result.Columns.Add("username", typeof(string));
        result.Columns.Add("productname", typeof(string));
        result.Columns.Add("price", typeof(decimal));
        result.Columns.Add("cgst", typeof(decimal));
        result.Columns.Add("sgst", typeof(decimal));
        result.Columns.Add("igst", typeof(decimal));

        DataTable source = GetOrderRows();
        if (source == null || source.Rows.Count == 0)
        {
            return result;
        }

        // Group by orderid preserving first-seen order (source is already ordered by id DESC).
        Dictionary<string, DataRow> map = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);
        Dictionary<string, List<string>> products = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (DataRow row in source.Rows)
        {
            string orderId = Convert.ToString(row["orderid"]).Trim();
            if (string.IsNullOrEmpty(orderId))
            {
                orderId = "-";
            }

            decimal amount = ParseDecimal(row["amount"]);
            decimal cgstPer = ParseDecimal(row["CGSTPER"]);
            decimal sgstPer = ParseDecimal(row["SGSTPER"]);
            decimal igstPer = ParseDecimal(row["IGSTPER"]);
            decimal totalRate = cgstPer + sgstPer + igstPer;

            decimal taxable = amount;
            if (totalRate > 0m)
            {
                taxable = RoundMoney((amount * 100m) / (100m + totalRate));
            }

            decimal cgst = RoundMoney(taxable * cgstPer / 100m);
            decimal sgst = RoundMoney(taxable * sgstPer / 100m);
            decimal igst = RoundMoney(taxable * igstPer / 100m);

            string productName = Convert.ToString(row["productname"]).Trim();

            DataRow target;
            if (!map.TryGetValue(orderId, out target))
            {
                target = result.NewRow();
                target["orderid"] = orderId;

                DateTime orderDate;
                if (DateTime.TryParse(Convert.ToString(row["approvedate"]), out orderDate))
                {
                    target["orderdate"] = orderDate;
                }
                else
                {
                    target["orderdate"] = DBNull.Value;
                }

                target["userid"] = Convert.ToString(row["userid"]).Trim();
                target["username"] = Convert.ToString(row["username"]).Trim();
                target["price"] = 0m;
                target["cgst"] = 0m;
                target["sgst"] = 0m;
                target["igst"] = 0m;
                result.Rows.Add(target);
                map[orderId] = target;
                products[orderId] = new List<string>();
            }

            target["price"] = ParseDecimal(target["price"]) + amount;
            target["cgst"] = ParseDecimal(target["cgst"]) + cgst;
            target["sgst"] = ParseDecimal(target["sgst"]) + sgst;
            target["igst"] = ParseDecimal(target["igst"]) + igst;

            if (!string.IsNullOrEmpty(productName) && !products[orderId].Contains(productName))
            {
                products[orderId].Add(productName);
            }
        }

        foreach (DataRow target in result.Rows)
        {
            string orderId = Convert.ToString(target["orderid"]);
            List<string> names;
            if (products.TryGetValue(orderId, out names) && names.Count > 0)
            {
                target["productname"] = string.Join(", ", names);
            }
            else
            {
                target["productname"] = "Saving Product";
            }
        }

        return result;
    }

    DataTable GetOrderRows()
    {
        StringBuilder sql = new StringBuilder();
        sql.Append(@"
SELECT
    sd.id,
    sd.orderid,
    sd.userid,
    ISNULL(ud.username, '') AS username,
    ISNULL(NULLIF(LTRIM(RTRIM(pm.productname)), ''), 'Saving Product') AS productname,
    sd.approvedate,
    ISNULL(sd.amount, 0) AS amount,
    ISNULL(sd.CGST, 0) AS CGSTPER,
    ISNULL(sd.SGST, 0) AS SGSTPER,
    ISNULL(sd.IGST, 0) AS IGSTPER
FROM SavingAccountDetail sd WITH (NOLOCK)
LEFT JOIN SavingProductMaster pm WITH (NOLOCK) ON sd.productid = pm.id
LEFT JOIN UserDetail ud WITH (NOLOCK) ON ud.UserId = sd.UserId
WHERE (sd.status = 'Approved' OR sd.status = '1'
       OR LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', 'approve'))");

        if (!string.IsNullOrWhiteSpace(txtOrderId.Text))
        {
            sql.Append(" AND sd.orderid LIKE '%").Append(SqlEscape(txtOrderId.Text.Trim())).Append("%'");
        }

        if (!string.IsNullOrWhiteSpace(txtUserId.Text))
        {
            string userSearch = SqlEscape(txtUserId.Text.Trim());
            sql.Append(" AND (sd.userid LIKE '%").Append(userSearch)
               .Append("%' OR ud.username LIKE '%").Append(userSearch).Append("%')");
        }

        string fromDate = NormalizeDate(txtFromDate.Text);
        string toDate = NormalizeDate(txtToDate.Text);
        if (!string.IsNullOrEmpty(fromDate))
        {
            sql.Append(" AND CONVERT(date, sd.approvedate) >= '").Append(fromDate).Append("'");
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            sql.Append(" AND CONVERT(date, sd.approvedate) <= '").Append(toDate).Append("'");
        }

        sql.Append(" ORDER BY sd.id DESC");

        DataTable dt = null;
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql.ToString());
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    static string NormalizeDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        DateTime parsed;
        if (DateTime.TryParse(value.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed) ||
            DateTime.TryParse(value.Trim(), out parsed))
        {
            return parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        return string.Empty;
    }

    static decimal ParseDecimal(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }

        decimal parsed;
        return decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out parsed)
            ? parsed
            : 0m;
    }

    static decimal RoundMoney(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}

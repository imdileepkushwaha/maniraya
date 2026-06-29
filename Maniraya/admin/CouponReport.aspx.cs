using System;
using System.Data;
using System.Web.UI;
using DataTier;

public partial class admin_CouponReport : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadCouponReport();
        }
    }

    void LoadCouponReport()
    {
        DataTable source = GetApprovedCoupons();
        GridView1.DataSource = source;
        GridView1.DataBind();

        DataTable printTickets = BuildPrintTickets(source);
        rptPrintCoupons.DataSource = printTickets;
        rptPrintCoupons.DataBind();

        pnlPrintArea.Visible = printTickets.Rows.Count > 0;
        btnPrintAll.Visible = printTickets.Rows.Count > 0;
        litCouponCount.Text = printTickets.Rows.Count.ToString();
    }

    DataTable GetApprovedCoupons()
    {
        string str_query = @"SELECT sd.couponcode,
                sd.approvedate,
                sd.amount,
                1 AS quantity,
                sd.status,
                pm.productname,
                ud.username,
                ud.userid,
                ud.mobile
            FROM SavingAccountDetail sd WITH (NOLOCK)
            LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
            LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sd.userid
            WHERE " + GetApprovedStatusFilter("sd") + @"
                AND ISNULL(LTRIM(RTRIM(sd.couponcode)), '') <> ''
            ORDER BY sd.approvedate DESC, ud.username, sd.couponcode";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        return dt ?? new DataTable();
    }

    static string GetApprovedStatusFilter(string tableAlias)
    {
        return "(" + tableAlias + ".status = 'Approved'"
            + " OR " + tableAlias + ".status = '1'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + tableAlias + ".status, '')))) IN ('approved', 'approve'))";
    }

    static DataTable BuildPrintTickets(DataTable source)
    {
        DataTable tickets = CreateTicketTable();
        if (source == null || source.Rows.Count == 0)
        {
            return tickets;
        }

        int ticketNo = 1;
        foreach (DataRow row in source.Rows)
        {
            int copies = GetQuantity(row);
            for (int i = 0; i < copies; i++)
            {
                DataRow ticket = tickets.NewRow();
                ticket["TicketNo"] = ticketNo++;
                ticket["couponcode"] = GetValue(row, "couponcode");
                ticket["username"] = GetValue(row, "username");
                ticket["userid"] = GetValue(row, "userid");
                ticket["mobile"] = GetValue(row, "mobile");
                ticket["productname"] = GetValue(row, "productname");
                ticket["amount"] = GetValue(row, "amount");
                ticket["approvedate"] = FormatApproveDate(row);
                ticket["copyLabel"] = copies > 1 ? "Copy " + (i + 1) + " of " + copies : string.Empty;
                tickets.Rows.Add(ticket);
            }
        }

        return tickets;
    }

    static DataTable CreateTicketTable()
    {
        DataTable tickets = new DataTable();
        tickets.Columns.Add("TicketNo", typeof(int));
        tickets.Columns.Add("couponcode", typeof(string));
        tickets.Columns.Add("username", typeof(string));
        tickets.Columns.Add("userid", typeof(string));
        tickets.Columns.Add("mobile", typeof(string));
        tickets.Columns.Add("productname", typeof(string));
        tickets.Columns.Add("amount", typeof(string));
        tickets.Columns.Add("approvedate", typeof(string));
        tickets.Columns.Add("copyLabel", typeof(string));
        return tickets;
    }

    static int GetQuantity(DataRow row)
    {
        int quantity = 1;
        if (row != null && row.Table.Columns.Contains("quantity") && row["quantity"] != DBNull.Value)
        {
            int.TryParse(Convert.ToString(row["quantity"]), out quantity);
        }

        return quantity > 0 ? quantity : 1;
    }

    static string FormatApproveDate(DataRow row)
    {
        if (row == null || !row.Table.Columns.Contains("approvedate") || row["approvedate"] == DBNull.Value)
        {
            return "-";
        }

        DateTime approveDate;
        if (DateTime.TryParse(Convert.ToString(row["approvedate"]), out approveDate))
        {
            return approveDate.ToString("dd MMM yyyy");
        }

        return Convert.ToString(row["approvedate"]);
    }

    static string GetValue(DataRow row, string columnName)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (DataColumn column in row.Table.Columns)
        {
            if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase)
                && row[column] != DBNull.Value)
            {
                return Convert.ToString(row[column]).Trim();
            }
        }

        return string.Empty;
    }
}

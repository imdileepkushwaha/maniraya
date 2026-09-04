using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class user_SavingProductPurchaseReport : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            loadprevproduct();
        }
    }

    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT sd.*, pm.productname
            FROM SavingAccountDetail sd WITH (NOLOCK)
            LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
            WHERE userid = '" + SqlEscape(Session["userid"].ToString()) + "'";

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

    void loadprevproduct()
    {
        if (GridView1 != null)
        {
            GridView1.DataSource = getPrevProduct();
            GridView1.DataBind();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    public bool CanShowInstallments(object status, object couponCode)
    {
        string coupon = Convert.ToString(couponCode).Trim();
        return IsApprovedStatus(Convert.ToString(status)) && !string.IsNullOrWhiteSpace(coupon);
    }

    static bool IsApprovedStatus(string status)
    {
        string normalized = (status ?? string.Empty).Trim().ToLowerInvariant();
        return normalized == "1"
            || normalized == "approved"
            || normalized == "approve"
            || normalized == "active";
    }

    static void ApplyStatusBadge(Label lblstatus)
    {
        if (lblstatus == null)
            return;

        string status = (lblstatus.Text ?? string.Empty).Trim();
        string normalized = status.ToLowerInvariant();

        if (normalized == "0" || normalized == "pending")
        {
            lblstatus.Text = "Pending";
            lblstatus.CssClass = "dash-saving-status is-pending";
            return;
        }

        if (IsApprovedStatus(status))
        {
            lblstatus.Text = "Approved";
            lblstatus.CssClass = "dash-saving-status is-approved";
            return;
        }

        if (normalized == "2" || normalized == "rejected" || normalized == "cancelled" || normalized == "canceled")
        {
            lblstatus.Text = "Rejected";
            lblstatus.CssClass = "dash-saving-status is-rejected";
        }
    }

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        ApplyStatusBadge(lblstatus);

        HyperLink lnkInstallments = e.Row.FindControl("lnkInstallments") as HyperLink;
        if (lnkInstallments == null)
        {
            return;
        }

        string status = string.Empty;
        string coupon = string.Empty;
        DataRowView row = e.Row.DataItem as DataRowView;
        if (row != null)
        {
            if (row.Row.Table.Columns.Contains("status") && row["status"] != DBNull.Value)
            {
                status = Convert.ToString(row["status"]);
            }
            if (row.Row.Table.Columns.Contains("couponcode") && row["couponcode"] != DBNull.Value)
            {
                coupon = Convert.ToString(row["couponcode"]).Trim();
            }
        }

        lnkInstallments.Visible = CanShowInstallments(status, coupon);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

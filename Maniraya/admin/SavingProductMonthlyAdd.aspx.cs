using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI;

public partial class admin_SavingProductMonthlyAdd : Page
{
    // Monthly product add starts from 1 September 2026.
    static readonly DateTime MonthlyStartDate = new DateTime(2026, 9, 1);

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
            SetNextEntryDate();
            BindGrid();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime entryDate;
        if (!TryGetEntryDate(out entryDate))
        {
            ShowAlert("Entry Date is required and must be the 1st of the month.");
            SetNextEntryDate();
            return;
        }

        if (entryDate.Day != 1 || entryDate.Date < MonthlyStartDate.Date)
        {
            ShowAlert("Entry Date must be 1st of month and on/after 01/09/2026.");
            SetNextEntryDate();
            return;
        }

        if (MonthAlreadyUsed(entryDate))
        {
            ShowAlert("A product for this month already exists. Entry Date moved to next available month.");
            SetNextEntryDate();
            return;
        }

        string productName = (txtProductName.Text ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(productName))
        {
            ShowAlert("Enter Product Name.");
            return;
        }

        decimal mrp, dp, gst;
        if (!TryParseDecimal(txtMrp.Text, out mrp) || mrp < 0)
        {
            ShowAlert("Enter valid MRP.");
            return;
        }
        if (!TryParseDecimal(txtDp.Text, out dp) || dp < 0)
        {
            ShowAlert("Enter valid DP.");
            return;
        }
        if (!TryParseDecimal(txtGst.Text, out gst) || gst < 0)
        {
            ShowAlert("Enter valid GST.");
            return;
        }

        string imageName = "noimage.png";
        try
        {
            if (fuImage.HasFile)
            {
                string safeName = Path.GetFileName(fuImage.FileName);
                imageName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + safeName;
                string folder = Server.MapPath("~/ProductImage/");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                fuImage.SaveAs(Path.Combine(folder, imageName));
            }
        }
        catch (Exception ex)
        {
            ShowAlert("Image upload failed: " + ex.Message);
            return;
        }

        string res = InsertProduct(
            productName,
            mrp,
            dp,
            imageName,
            Convert.ToString(Session["useradmin"]),
            gst,
            (txtHsnCode.Text ?? string.Empty).Trim(),
            entryDate);

        if (res == "t")
        {
            ShowAlert("Saving product added successfully for " + entryDate.ToString("dd/MM/yyyy") + ".");
            ClearForm();
            SetNextEntryDate();
            BindGrid();
        }
        else if (res == "f")
        {
            ShowAlert("Product name already exists.");
        }
        else if (res == "m")
        {
            ShowAlert("A product for this month already exists.");
            SetNextEntryDate();
        }
        else if (res == "d")
        {
            ShowAlert("Entry Date must be the 1st of the month.");
            SetNextEntryDate();
        }
        else
        {
            ShowAlert("Unable to save product. Please try again.");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearForm();
        SetNextEntryDate();
        BindGrid();
    }

    string InsertProduct(
        string productName,
        decimal mrp,
        decimal dp,
        string imageName,
        string entryBy,
        decimal gst,
        string hsnCode,
        DateTime entryDate)
    {
        string res = "0";
        SqlConnection cn = ObjData.StartConnectionInTransaction();
        SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@ProductName", productName),
                new SqlParameter("@MRP", mrp),
                new SqlParameter("@DP", dp),
                new SqlParameter("@ImageName", imageName ?? "noimage.png"),
                new SqlParameter("@EntryBy", entryBy ?? string.Empty),
                new SqlParameter("@GST", gst),
                new SqlParameter("@HSNCode", hsnCode ?? string.Empty),
                new SqlParameter("@EntryDate", entryDate.Date)
            };

            res = Convert.ToString(ObjData.RunInsUpDelQueryTransProcScalar("sp_add_SavingProductMaster", tr, parameter));
            tr.Commit();
        }
        catch
        {
            try { tr.Rollback(); } catch { }
            res = "0";
        }
        finally
        {
            ObjData.EndConnection();
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return (res ?? string.Empty).Trim().ToLowerInvariant();
    }

    void SetNextEntryDate()
    {
        DateTime next = GetNextAvailableEntryDate();
        txtEntryDate.Text = next.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        hdnEntryDate.Value = next.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    DateTime GetNextAvailableEntryDate()
    {
        DateTime candidate = MonthlyStartDate.Date;

        try
        {
            ObjData.StartConnection();
            try
            {
                // Walk months from Sep-2026 until a free month is found.
                for (int i = 0; i < 240; i++)
                {
                    string sql =
                        "SELECT TOP 1 id FROM SavingProductMaster WITH (NOLOCK) " +
                        "WHERE YEAR(EntryDate) = " + candidate.Year +
                        " AND MONTH(EntryDate) = " + candidate.Month +
                        " AND DAY(EntryDate) = 1";

                    DataTable dt = ObjData.RunDataTable(sql);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return candidate;
                    }
                    candidate = candidate.AddMonths(1);
                }
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            return MonthlyStartDate.Date;
        }

        return candidate;
    }

    bool MonthAlreadyUsed(DateTime entryDate)
    {
        try
        {
            ObjData.StartConnection();
            try
            {
                string sql =
                    "SELECT TOP 1 id FROM SavingProductMaster WITH (NOLOCK) " +
                    "WHERE YEAR(EntryDate) = " + entryDate.Year +
                    " AND MONTH(EntryDate) = " + entryDate.Month +
                    " AND DAY(EntryDate) = 1";
                DataTable dt = ObjData.RunDataTable(sql);
                return dt != null && dt.Rows.Count > 0;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    void BindGrid()
    {
        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(
                    "SELECT id, ProductName, MRP, DP, GST, HSNCode, EntryDate, EntryBy " +
                    "FROM SavingProductMaster WITH (NOLOCK) " +
                    "ORDER BY EntryDate DESC, id DESC");
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

        gvProducts.DataSource = dt;
        gvProducts.DataBind();
    }

    void ClearForm()
    {
        txtProductName.Text = string.Empty;
        txtMrp.Text = string.Empty;
        txtDp.Text = string.Empty;
        txtGst.Text = string.Empty;
        txtHsnCode.Text = string.Empty;
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "resetMonthlyProductImage",
            "if (window.AdminImageUpload) { AdminImageUpload.reset(document.getElementById('monthlyProductImageSlot')); }",
            true);
    }

    bool TryGetEntryDate(out DateTime entryDate)
    {
        entryDate = DateTime.MinValue;
        string hidden = hdnEntryDate != null ? Convert.ToString(hdnEntryDate.Value) : string.Empty;
        if (!string.IsNullOrWhiteSpace(hidden)
            && DateTime.TryParseExact(hidden.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out entryDate))
        {
            return true;
        }

        string ui = txtEntryDate != null ? Convert.ToString(txtEntryDate.Text) : string.Empty;
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };
        return DateTime.TryParseExact((ui ?? string.Empty).Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out entryDate);
    }

    static bool TryParseDecimal(string input, out decimal value)
    {
        value = 0m;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        string text = input.Trim();
        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
            || decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
    }

    void ShowAlert(string message)
    {
        string safe = (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
            "alert('" + safe + "');", true);
    }
}

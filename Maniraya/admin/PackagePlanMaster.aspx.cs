using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PackagePlanMaster : System.Web.UI.Page
{
    private readonly clsPackagePlan objPackage = new clsPackagePlan();
    private const string PendingSessionKey = "PackagePlanPending";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindDropdowns();
            PendingProducts = CreatePendingTable();
            LoadGrid();
        }

        BindPendingGrid();
    }

    private DataTable PendingProducts
    {
        get
        {
            if (Session[PendingSessionKey] == null)
            {
                Session[PendingSessionKey] = CreatePendingTable();
            }

            return (DataTable)Session[PendingSessionKey];
        }
        set
        {
            Session[PendingSessionKey] = value;
        }
    }

    private static DataTable CreatePendingTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowKey");
        dt.Columns.Add("planid");
        dt.Columns.Add("PlanName");
        dt.Columns.Add("productid");
        dt.Columns.Add("ProductName");
        dt.Columns.Add("quantity", typeof(int));
        return dt;
    }

    private void BindDropdowns()
    {
        BindPlanDropdown(ddlPlan);
        BindPlanDropdown(ddlPlanEdit);
        BindProductDropdown(ddlProduct);
        BindProductDropdown(ddlProductEdit);
    }

    private void BindPlanDropdown(DropDownList ddl)
    {
        string selected = ddl.SelectedValue;
        DataTable dt = objPackage.GetPlansForDropdown();
        ddl.Items.Clear();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "PlanName";
            ddl.DataValueField = "Id";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, new ListItem("Select Plan", "0"));

        if (!string.IsNullOrEmpty(selected) && ddl.Items.FindByValue(selected) != null)
        {
            ddl.SelectedValue = selected;
        }
    }

    private void BindProductDropdown(DropDownList ddl)
    {
        string selected = ddl.SelectedValue;
        DataTable dt = objPackage.GetProductsForDropdown();
        ddl.Items.Clear();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "ProductName";
            ddl.DataValueField = "ProductId";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, new ListItem("Select Product", "0"));

        if (!string.IsNullOrEmpty(selected) && ddl.Items.FindByValue(selected) != null)
        {
            ddl.SelectedValue = selected;
        }
    }

    private void BindPendingGrid()
    {
        gvPending.DataSource = PendingProducts;
        gvPending.DataBind();
        pnlPendingEmpty.Visible = PendingProducts.Rows.Count == 0;
    }

    private void LoadGrid()
    {
        string planId = ddlPlan.SelectedValue;
        if (planId == "0" || string.IsNullOrEmpty(planId))
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            pnlSavedEmpty.Visible = true;
            pnlPackageCard.Visible = false;
            return;
        }

        DataTable dt = objPackage.GetPlanProductList(planId);
        if (dt == null)
        {
            dt = new DataTable();
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        bool hasRows = dt.Rows.Count > 0;
        pnlSavedEmpty.Visible = !hasRows;
        pnlPackageCard.Visible = hasRows;

        if (hasRows)
        {
            lblPackagePlanName.Text = ddlPlan.SelectedItem.Text;
            lblPackageProductCount.Text = dt.Rows.Count == 1
                ? "1 product in this package"
                : dt.Rows.Count + " products in this package";
        }
    }

    protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        PendingProducts = CreatePendingTable();
        BindPendingGrid();
        LoadGrid();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "syncProduct", "syncProductName();", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlPlan.SelectedValue == "0")
        {
            ShowAlert("Select plan first");
            return;
        }

        if (ddlProduct.SelectedValue == "0")
        {
            ShowAlert("Select product");
            return;
        }

        int quantity;
        if (!int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity <= 0)
        {
            ShowAlert("Enter valid quantity");
            return;
        }

        string planId = ddlPlan.SelectedValue;
        string productId = ddlProduct.SelectedValue;
        string planName = ddlPlan.SelectedItem.Text;
        string productName = ddlProduct.SelectedItem.Text;

        foreach (DataRow row in PendingProducts.Rows)
        {
            if (string.Equals(row["planid"].ToString(), planId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(row["productid"].ToString(), productId, StringComparison.OrdinalIgnoreCase))
            {
                ShowAlert("This product is already in the list. Remove it first or change quantity.");
                return;
            }
        }

        DataRow newRow = PendingProducts.NewRow();
        newRow["RowKey"] = Guid.NewGuid().ToString();
        newRow["planid"] = planId;
        newRow["PlanName"] = planName;
        newRow["productid"] = productId;
        newRow["ProductName"] = productName;
        newRow["quantity"] = quantity;
        PendingProducts.Rows.Add(newRow);
        Session[PendingSessionKey] = PendingProducts;

        ddlProduct.SelectedIndex = 0;
        txtProductName.Text = string.Empty;
        txtQuantity.Text = "1";
        BindPendingGrid();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "syncProduct", "syncProductName();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (PendingProducts.Rows.Count == 0)
        {
            ShowAlert("Add at least one product to the list before saving");
            return;
        }

        int saved = 0;
        int skipped = 0;
        int failed = 0;

        foreach (DataRow row in PendingProducts.Rows)
        {
            string planId = row["planid"].ToString();
            string productId = row["productid"].ToString();

            if (objPackage.PlanProductExists(planId, productId, string.Empty))
            {
                skipped++;
                continue;
            }

            objPackage.PlanId = planId;
            objPackage.ProductId = productId;
            objPackage.Quantity = Convert.ToInt32(row["quantity"]);

            if (objPackage.InsertPlanProduct(objPackage) == "t")
            {
                saved++;
            }
            else
            {
                failed++;
            }
        }

        PendingProducts = CreatePendingTable();
        Session[PendingSessionKey] = PendingProducts;
        BindPendingGrid();
        LoadGrid();

        if (failed == 0 && skipped == 0)
        {
            ShowAlert(saved + " product(s) saved to plan package successfully");
        }
        else if (failed == 0)
        {
            ShowAlert(saved + " product(s) saved. " + skipped + " already existed and were skipped.");
        }
        else
        {
            ShowAlert(saved + " saved, " + skipped + " skipped, " + failed + " failed.");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        PendingProducts = CreatePendingTable();
        Session[PendingSessionKey] = PendingProducts;
        ddlProduct.SelectedIndex = 0;
        txtProductName.Text = string.Empty;
        txtQuantity.Text = "1";
        BindPendingGrid();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "syncProduct", "syncProductName();", true);
    }

    protected void gvPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "remove")
        {
            return;
        }

        string rowKey = e.CommandArgument.ToString();
        for (int i = PendingProducts.Rows.Count - 1; i >= 0; i--)
        {
            if (PendingProducts.Rows[i]["RowKey"].ToString() == rowKey)
            {
                PendingProducts.Rows.RemoveAt(i);
                break;
            }
        }

        Session[PendingSessionKey] = PendingProducts;
        BindPendingGrid();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int quantity;
        if (!int.TryParse(txtQuantityEdit.Text.Trim(), out quantity) || quantity <= 0)
        {
            ShowAlert("Enter valid quantity");
            return;
        }

        if (objPackage.PlanProductExists(ddlPlanEdit.SelectedValue, ddlProductEdit.SelectedValue, lblEditId.Text))
        {
            ShowAlert("This product already exists for the selected plan");
            return;
        }

        objPackage.Id = lblEditId.Text;
        objPackage.PlanId = ddlPlanEdit.SelectedValue;
        objPackage.ProductId = ddlProductEdit.SelectedValue;
        objPackage.Quantity = quantity;

        if (objPackage.UpdatePlanProduct(objPackage) == "t")
        {
            ShowAlert("Package item updated successfully");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "closePkgModal", "closePackageEditModal();", true);
            LoadGrid();
        }
        else
        {
            ShowAlert("Unable to update package item");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "edt")
        {
            return;
        }

        int index = Convert.ToInt32(e.CommandArgument.ToString());

        Label lblProductId = (Label)GridView1.Rows[index].FindControl("lblProductId");
        Label lblProductName = (Label)GridView1.Rows[index].FindControl("lblProductName");
        Label lblQuantity = (Label)GridView1.Rows[index].FindControl("lblQuantity");
        Label lblRowIdEdit = (Label)GridView1.Rows[index].FindControl("lblRowId");

        lblEditId.Text = lblRowIdEdit.Text;
        BindPlanDropdown(ddlPlanEdit);
        BindProductDropdown(ddlProductEdit);

        if (ddlPlanEdit.Items.FindByValue(ddlPlan.SelectedValue) != null)
        {
            ddlPlanEdit.SelectedValue = ddlPlan.SelectedValue;
        }

        if (ddlProductEdit.Items.FindByValue(lblProductId.Text) != null)
        {
            ddlProductEdit.SelectedValue = lblProductId.Text;
        }

        txtProductNameEdit.Text = lblProductName.Text;
        txtQuantityEdit.Text = lblQuantity.Text;

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openPkgModal", "openPackageEditModal(); syncProductNameEdit();", true);
    }

    private void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

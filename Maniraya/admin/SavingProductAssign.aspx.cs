using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ProductAdd : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objState = new clsProduct();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        SavingProductHelper.EnsureInstallmentProductAssignTable();

        if (!IsPostBack)
        {
            loadproduct();
            loadinstallments();
            loadassignmentmap();
        }
    }

    void loadproduct()
    {
        ddproduct.Items.Clear();
        DataTable dt = SavingProductHelper.GetActiveProductsForAssign();
        ddproduct.DataSource = dt;
        ddproduct.DataTextField = "ProductName";
        ddproduct.DataValueField = "id";
        ddproduct.DataBind();
        ddproduct.Items.Insert(0, new ListItem("Select Product", "0"));
    }

    void loadinstallments()
    {
        ddinstallment.Items.Clear();
        ddinstallment.Items.Add(new ListItem("Select Installment", "0"));
        for (int i = 1; i <= 18; i++)
        {
            ddinstallment.Items.Add(new ListItem("Installment " + i, i.ToString()));
        }
    }

    void loadassignmentmap()
    {
        gvAssign.DataSource = SavingProductHelper.GetInstallmentAssignmentMap();
        gvAssign.DataBind();
    }

    void bindproductdetail()
    {
        txtmrp.Text = "";
        txtdp.Text = "";
        pnlInstallment.Visible = false;
        txtprevproduct.Text = "";

        if (ddproduct.SelectedValue == "0")
        {
            return;
        }

        DataTable dt = getProductDetail(ddproduct.SelectedValue);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtmrp.Text = GetCol(dt.Rows[0], "mrp", "MRP");
            txtdp.Text = GetCol(dt.Rows[0], "dp", "DP");
            pnlInstallment.Visible = true;
            bindcurrentassignment();
        }
    }

    void bindcurrentassignment()
    {
        txtprevproduct.Text = "Not assigned yet";
        int instNo;
        if (!int.TryParse(ddinstallment.SelectedValue, out instNo) || instNo < 1)
        {
            return;
        }

        DataTable map = SavingProductHelper.GetInstallmentAssignmentMap();
        if (map == null)
        {
            return;
        }

        foreach (DataRow row in map.Rows)
        {
            if (Convert.ToInt32(row["InstallmentNo"]) != instNo)
            {
                continue;
            }

            string name = Convert.ToString(row["ProductName"]);
            txtprevproduct.Text = string.IsNullOrWhiteSpace(name) ? "Not assigned yet" : name.Trim();
            break;
        }
    }

    public DataTable getProductDetail(string productid)
    {
        string str_query = "SELECT * FROM SavingProductMaster WITH (NOLOCK) WHERE id='" + productid.Replace("'", "''") + "'";
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
        ObjData.EndConnection();
        return dt;
    }

    public string Insert_Product(clsProduct objState)
    {
        string res = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            SqlParameter[] parameter = {
                new SqlParameter("@ProductId", objState.ProductId),
                new SqlParameter("@EntryBy", objState.MentionBy),
            };
            res = ObjData.RunInsUpDelQueryTransProcScalar("sp_add_SavingMonthlyProductDetail", tr, parameter);
            tr.Commit();
        }
        catch
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        int productId;
        int instNo;
        if (!int.TryParse(ddproduct.SelectedValue, out productId) || productId <= 0)
        {
            ShowAlert("Select Product");
            return;
        }

        if (!int.TryParse(ddinstallment.SelectedValue, out instNo) || instNo < 1 || instNo > 18)
        {
            ShowAlert("Select Installment (1 to 18)");
            return;
        }

        string adminId = Convert.ToString(Session["useradmin"]);
        bool ok = SavingProductHelper.AssignProductToInstallment(instNo, productId, adminId);
        if (!ok)
        {
            ShowAlert("Unable to assign product");
            return;
        }

        if (instNo == 1)
        {
            objState.ProductId = productId.ToString();
            objState.MentionBy = adminId;
            Insert_Product(objState);
        }

        loadassignmentmap();
        bindcurrentassignment();
        ShowAlert("Installment " + instNo + " pe product assign ho gaya. Naya user join kare to installment 1 ka product milega.");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddproduct.SelectedValue = "0";
        if (ddinstallment.Items.Count > 0)
        {
            ddinstallment.SelectedValue = "0";
        }
        txtmrp.Text = "";
        txtdp.Text = "";
        txtprevproduct.Text = "";
        pnlInstallment.Visible = false;
    }

    protected void ddproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindproductdetail();
    }

    protected void ddinstallment_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindcurrentassignment();
        pnlInstallment.Visible = ddproduct.SelectedValue != "0";
    }

    static string GetCol(DataRow row, params string[] names)
    {
        foreach (string name in names)
        {
            if (row.Table.Columns.Contains(name) && row[name] != DBNull.Value)
            {
                return Convert.ToString(row[name]).Trim();
            }
        }

        return string.Empty;
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + (message ?? string.Empty).Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}

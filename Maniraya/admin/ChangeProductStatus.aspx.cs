using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class admin_ChangeProductStatus : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();

    private DataTable ProductData
    {
        get { return ViewState["ProductData"] as DataTable; }
        set { ViewState["ProductData"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loadProduct(false);
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    void loadProduct(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.ProductId = string.Empty;
        if (TxtProductNameSearch.Text != string.Empty)
        {
            objState.ProductName = TxtProductNameSearch.Text;
        }
        if (ddstatus.SelectedIndex != 0)
        {
            objState.Status = ddstatus.SelectedValue;
        }

        if (TxtProductCodeSearch.Text != string.Empty)
        {
            objState.ProductId = TxtProductCodeSearch.Text;
        }

        ProductData = objState.getProductAll(objState);
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = ProductData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 10;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;

            if (dt.Rows.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 10;
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (ProductData != null)
        {
            BindGrid();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            objState.ProductId = lbllevel.Text;
            objState.Status = ChStatus.Checked ? "1" : "0";
            objState.Update_ProductStatus(objState);
        }

        string popupScript = "alert('Status updated successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        loadProduct(false);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Status = e.Row.FindControl("LblStatuschk") as Label;
            CheckBox ChkStatus = e.Row.FindControl("ChkStatus") as CheckBox;
            if (Status.Text == "1")
            {
                ChkStatus.Checked = true;
            }
            Image image = e.Row.FindControl("Image1") as Image;
            if (!File.Exists(Server.MapPath(image.ImageUrl)))
            {
                image.ImageUrl = "../ProductImage/images.png";
            }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        loadProduct(true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.IO;
using DataTier;



public partial class FranchiseeStockDetail : System.Web.UI.Page
{

    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();

    private DataTable StockData
    {
        get { return ViewState["StockData"] as DataTable; }
        set { ViewState["StockData"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadFranchisee();
                loadProduct();
                loaduser(false);
                RDBtnTRecharge.Checked = true;
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadFranchisee()
    {
        DDLstFranchisee.Items.Clear();
        DataTable dt = new DataTable();
        dt = objV.getFranchiseeList();
        DDLstFranchisee.DataSource = dt;
        DDLstFranchisee.DataTextField = "Username";
        DDLstFranchisee.DataValueField = "userid";
        DDLstFranchisee.DataBind();
        ListItem li = new ListItem("Select Franchisee", "0");
        DDLstFranchisee.Items.Insert(0, li);
    }

    void loadProduct()
    {
        if (DDLstFranchisee.SelectedIndex > 0)
        {
            DDLstProduct.Items.Clear();
            DataTable dt = new DataTable();
            dt = getProductForPurchaseFranchiseeWise(DDLstFranchisee.SelectedValue);
            DDLstProduct.DataSource = dt;
            DDLstProduct.DataTextField = "Productname";
            DDLstProduct.DataValueField = "Productid";
            DDLstProduct.DataBind();
            ListItem li = new ListItem("Select Product", "0");
            DDLstProduct.Items.Insert(0, li);
        }
        else
        {
            DDLstProduct.Items.Clear();
            DataTable dt = new DataTable();
            dt = objP.getProductForPurchase();
            DDLstProduct.DataSource = dt;
            DDLstProduct.DataTextField = "Productname";
            DDLstProduct.DataValueField = "Productid";
            DDLstProduct.DataBind();
            ListItem li = new ListItem("Select Product", "0");
            DDLstProduct.Items.Insert(0, li);
            
        }
    }
    public DataTable getProductForPurchaseFranchiseeWise(string id)
    {
        string str_query = " SELECT DISTINCT a.productid,b.ProductName FROM FranchiseeStockMaster a LEFT JOIN productmaster b ON a.ProductID=b.ProductId WHERE FranchiseeId='" + id + "' ";
        if (RDBtnTRecharge.Checked == true)
        {
            str_query += " and isnull(a.Isdistributer,0)='" + 0 + "'";

        }
        if (RdBtnUtility.Checked == true)
        {
            str_query += " and isnull(a.Isdistributer,0)='" + 1 + "'";

        }
        str_query += " order by a.productid";
        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void getproduct(object sender, EventArgs e)
    {
        loadProduct();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser(true);
    }

    void loaduser(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

        objV.VendorId = string.Empty;
        objV.ProductId = string.Empty;
        if (DDLstProduct.SelectedIndex > 0)
        {
            objV.ProductId = DDLstProduct.SelectedValue;
        }
        if (DDLstFranchisee.SelectedIndex > 0)
        {
            objV.VendorId = DDLstFranchisee.SelectedValue;
        }

        StockData = getFranchiseeStock(objV);
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = StockData;
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
        if (StockData != null)
        {
            BindGrid();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    public DataTable getFranchiseeStock(clsvendor objstate)
    {
        string str_query = "  SELECT  fd.userid,fd.Username,pm.ProductId,pm.ProductName,pm.BV AS BV,pm.DP AS DP,isnull(sum(fm.CRQuantity),0) Recieve,isnull(sum(fm.DrQuantity),0) Sales,(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) AS stockleft, (BV * (isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0))) AS BVLEFT, (DP *(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) ) AS DPLEFT FROM FranchiseeStockMaster fm JOIN ProductMaster pm ON fm.ProductID=pm.ProductId JOIN franchiseedetail fd ON fm.FranchiseeID=fd.userid where 1=1";
        if (objstate.VendorId != string.Empty)
        {
            str_query += " and fd.userid='" + objstate.VendorId + "'";
        }
        if (objstate.ProductId != string.Empty)
        {
            str_query += " and pm.ProductId='" + objstate.ProductId + "'";
        }
        if (RDBtnTRecharge.Checked == true)
        {
            str_query += " and isnull(fm.Isdistributer,0)='" + 0 + "'";

        }
        if (RdBtnUtility.Checked == true)
        {
            str_query += " and isnull(fm.Isdistributer,0)='" + 1 + "'";

        }
        str_query += " GROUP BY fd.userid,fd.Username,pm.ProductId,pm.ProductName,pm.BV,pm.DP  order BY pm.ProductId";
        DataTable dt = null;

        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer && StockData != null)
        {
            decimal totalBv = 0;
            decimal totalDp = 0;

            foreach (DataRow row in StockData.Rows)
            {
                if (row["BVLEFT"] != DBNull.Value)
                {
                    totalBv += Convert.ToDecimal(row["BVLEFT"]);
                }
                if (row["DPLEFT"] != DBNull.Value)
                {
                    totalDp += Convert.ToDecimal(row["DPLEFT"]);
                }
            }

            e.Row.Cells[5].Text = String.Format("{0:N0}", totalBv);
            e.Row.Cells[6].Text = String.Format("{0:N0}", totalDp);
        }
    }
}
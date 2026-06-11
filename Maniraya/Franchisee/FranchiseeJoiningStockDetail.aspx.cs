using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using DataTier;

public partial class FranchiseeJoiningStockDetail : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fuserid"] != null)
            {
               
               
                txtuserid.Text = Session["fuserid"].ToString();
                loadProduct();
                loaduser();
                txtuserid.Enabled = false;
                RDBtnTRecharge.Checked = true;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
   

    void loadProduct()
    {
        DDLstProduct.Items.Clear();
        DataTable dt = new DataTable();
        dt = getProductForPurchaseFranchiseeWise(txtuserid.Text);
        DDLstProduct.DataSource = dt;
        DDLstProduct.DataTextField = "Productname";
        DDLstProduct.DataValueField = "Productid";
        DDLstProduct.DataBind();
        ListItem li = new ListItem("Select Product", "0");
        DDLstProduct.Items.Insert(0, li);
    }
    public DataTable getProductForPurchaseFranchiseeWise(string id)
    {
        string str_query = " SELECT DISTINCT a.productid,b.ProductName FROM FranchiseeJoiningStockMaster a LEFT JOIN productmaster b ON a.ProductID=b.ProductId WHERE FranchiseeId='" + id + "' ";
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
        loaduser();
    }
    void loaduser()
    {
        objV.VendorId = string.Empty;
        objV.ProductId = string.Empty;
        if (DDLstProduct.SelectedIndex > 0)
        {
            objV.ProductId = DDLstProduct.SelectedValue;
        }
       
            objV.VendorId = txtuserid.Text;
      
        
        DataTable dt = new DataTable();
        dt = getFranchiseeStock(objV);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    public DataTable getFranchiseeStock(clsvendor objstate)
    {
        string str_query = "SELECT  fd.userid,fd.Username,pm.ProductId,pm.ProductName,pm.BV AS BV,pm.DP AS DP,isnull(sum(fm.CRQuantity),0) Recieve,isnull(sum(fm.DrQuantity),0) Sales,(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) AS stockleft,(BV * (isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0))) AS BVLEFT, (DP *(isnull(sum(fm.CRQuantity),0)-isnull(sum(fm.DrQuantity),0)) ) AS DPLEFT, (SELECT isnull(sum(CRQuantity),0) FROM FranchiseeJoiningStockMaster WHERE FranchiseeId='" + objstate.VendorId + "' AND ProductID=pm.ProductId AND isnull(HoldStock,0)=1) AS HOLDSTOCK,(isnull(sum(fm.CRQuantity),0)- (SELECT isnull(sum(CRQuantity),0) FROM FranchiseeJoiningStockMaster WHERE FranchiseeId='" + objstate.VendorId + "' AND ProductID=pm.ProductId AND isnull(HoldStock,0)=1)-isnull(sum(fm.DrQuantity),0)) AS balance  FROM FranchiseeJoiningStockMaster fm JOIN ProductMaster pm ON fm.ProductID=pm.ProductId JOIN franchiseedetail fd ON fm.FranchiseeID=fd.userid where 1=1";
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
    
}
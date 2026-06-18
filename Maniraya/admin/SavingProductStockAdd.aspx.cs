using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class admin_ProductAdd : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {

                loadproduct();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    public DataTable getProduct()
    {
        string str_query = @"SELECT * from  SavingProductMaster
                 ORDER BY ProductName";

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

    public DataTable getProductDetail(String productid)
    {
        string str_query = @"SELECT * from  SavingProductMaster with (nolock)
                 where id='"+productid+"'";

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


    void loadproduct()
    {
        ddproduct.Items.Clear();
        DataTable dt = new DataTable();
        dt = getProduct();
        ddproduct.DataSource = dt;
        ddproduct.DataTextField = "ProductName";
        ddproduct.DataValueField = "id";
        ddproduct.DataBind();
        ListItem li = new ListItem("Select Product", "0");
        ddproduct.Items.Insert(0, li);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        

    }
    public string Insert_Product(clsProduct objState)
    {

        string str_orderid = Guid.NewGuid().ToString().Substring(0, 8);

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_add_SavingProductStockDetail";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@FranchiseId","admin"),
                new SqlParameter("@ProductId",objState.ProductId),
                new SqlParameter("@StockIn",objState.Quantity),
                new SqlParameter("@OrderId",str_orderid),
                new SqlParameter("@EntryBy",objState.MentionBy),
               
                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
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
        
        objState.ProductId = ddproduct.SelectedValue.ToString();
        objState.Quantity = Convert.ToInt32( txtquantity.Text);
       
       
        objState.MentionBy = Session["useradmin"].ToString();
        string res = Insert_Product(objState);
        if (res == "t")
        {
            string popupScript = "alert('Saving Product Stock Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtquantity.Text = txtmrp.Text = txtdp.Text = "";
            ddproduct.SelectedValue = "0";

        }
       
            else
            {
                string popupScript = "alert('Unknown error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }     
    }





    protected void ddproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = getProductDetail(ddproduct.SelectedValue.ToString());
        if (dt.Rows.Count > 0)
        {
            txtmrp.Text = dt.Rows[0]["mrp"].ToString();
            txtdp.Text = dt.Rows[0]["dp"].ToString();
        }
        else
        {
            txtmrp.Text = "";
            txtdp.Text = "";

        }
    }
}
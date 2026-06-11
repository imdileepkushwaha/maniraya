using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_QuantityOfferMaster : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsProduct objP = new clsProduct();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadProduct();
                loadOfferProduct();
                loadcity();
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
        dt = objP.getProductForPurchase();
        DDLstProduct.DataSource = dt;
        DDLstProduct.DataTextField = "Productname";
        DDLstProduct.DataValueField = "Productid";
        DDLstProduct.DataBind();
        ListItem li = new ListItem("Select Product", "0");
        DDLstProduct.Items.Insert(0, li);
    }
    void loadOfferProduct()
    {
        DDLstOfferProduct.Items.Clear();
        DataTable dt = new DataTable();
        dt = objP.getProductForPurchase();
        DDLstOfferProduct.DataSource = dt;
        DDLstOfferProduct.DataTextField = "Productname";
        DDLstOfferProduct.DataValueField = "Productid";
        DDLstOfferProduct.DataBind();
        ListItem li = new ListItem("Select Product", "0");
        DDLstOfferProduct.Items.Insert(0, li);
    }
   
    void loadcity()
    {
        DataTable dt = new DataTable();
        dt = getoffer();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getoffer()
    {
        string str_query = "SELECT Q.ID,Q.ProductId,P.ProductName,Q.OfferProductID,O.ProductName OfferProductname,Q.OfferAmount,Q.OfferQuantity,Q.Fromdate,Q.Todate,Q.Status, CASE WHEN Q.Status=0 THEN 'INACTIVE' ELSE 'ACTIVE' END AS OFFERSTATUS,Q.Quantity FROM QuantityOffer Q INNER JOIN ProductMaster O ON Q.OfferProductID=O.ProductId INNER JOIN ProductMaster P ON Q.ProductID=P.ProductId";

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
    
    public string Insert_offer(string productid,string quantity,string offerproductid,string offerquantity,string amount,string fromdate,string todate)
    {
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_add_offerQuantity";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@ProductID",productid), 
                new SqlParameter("@OfferProductId",offerproductid), 
                new SqlParameter("@Quantity",quantity),
                 new SqlParameter("@OfferQuantity",offerquantity),
                  new SqlParameter("@Amount",amount),
                    new SqlParameter("@Fromdate",fromdate),
                   new SqlParameter("@Todate",todate),

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        string res = Insert_offer(DDLstProduct.SelectedValue,TxtQuatity.Text,DDLstOfferProduct.SelectedValue,TxtOfferQuantity.Text,TxtOfferAmount.Text,txtFromDate.Text,txtToDate.Text);
        if (res == "t")
        {
            string popupScript = "alert('record Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            TxtOfferAmount.Text = ""; TxtOfferQuantity.Text = ""; TxtQuatity.Text = ""; DDLstProduct.SelectedValue = "0"; DDLstOfferProduct.SelectedValue = "0";
            loadcity();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('City Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "changeStatus")
        {
            try
            {
               string id = e.CommandArgument.ToString();

               if (changeUserStatus(id) > 0)
                {
                    Response.Redirect("QuantityOfferMaster.aspx");
                  //  ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Status Changed Successfully...!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
            }
            finally
            {
                getoffer();
            }
        }
    }
    public int changeUserStatus(string id)
    {
        string sql = @"update QuantityOffer set status=(case status when 0 then 1 when 1 then 0 end) where id='" + id + "'";

        ObjData.StartConnection();
        try
        {
            return ObjData.RunInsUpDelQueryNew(sql);
        }
        catch
        {
            throw;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }
   
}
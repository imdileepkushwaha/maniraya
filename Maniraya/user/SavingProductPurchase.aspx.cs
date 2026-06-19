using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DataTier;

public partial class user_WithdrawlRequstAdd : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsProduct objproduct = new clsProduct();

    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadprevproduct();



            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT sd.productid, pd.productname, pd.ImageName, pd.MRP, pd.DP
            FROM SavingMonthlyProductDetail sd WITH (nolock)
            LEFT JOIN SavingProductMaster pd WITH (nolock) ON sd.productid = pd.id
            WHERE sd.Status = 1";

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

    void loadprevproduct()
    {
        DataTable dt = getPrevProduct();
        if (dt == null || dt.Rows.Count == 0)
        {
            imgProduct.ImageUrl = ResolveUrl("~/ProductImage/noimage.png");
            imgProduct.CssClass = "saving-product-showcase-img";
            litProductName.Text = "Saving Product";
            litMrp.Text = "0.00";
            litDp.Text = "0.00";
            litAmount.Text = "₹ 0.00";
            return;
        }

        DataRow row = dt.Rows[0];
        string productName = Convert.ToString(row["productname"]);
        string imageName = Convert.ToString(row["ImageName"]);
        decimal mrp = 0m;
        decimal dp = 0m;

        decimal.TryParse(Convert.ToString(row["MRP"]), out mrp);
        decimal.TryParse(Convert.ToString(row["DP"]), out dp);

        txtproductname.Text = productName;
        litProductName.Text = string.IsNullOrWhiteSpace(productName) ? "Saving Product" : productName;
        litMrp.Text = mrp.ToString("N2");
        litDp.Text = dp.ToString("N2");

        decimal amount = dp > 0 ? dp : mrp;
        txtamount.Text = amount.ToString("0.##");
        litAmount.Text = "₹ " + amount.ToString("N2");

        string imageUrl = string.IsNullOrWhiteSpace(imageName)
            ? ResolveUrl("~/ProductImage/noimage.png")
            : ResolveUrl("~/ProductImage/" + imageName);
        imgProduct.ImageUrl = imageUrl;
        imgProduct.CssClass = "saving-product-showcase-img";
    }
    public string UploadImage()
    {
        string Imagename = "";
        if (ImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
  
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            objaccount.UserId = txtuserid.Text;
            DataTable dtrechrge = objaccount.getUserWalletBalanceReportrechargewallet(objaccount);
           
            
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }

    public string Insert_ProductPurchase(clsProduct objState)
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
            s2 = "sp_add_SavingAccountDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@orderid",str_orderid),
                new SqlParameter("@UserId",objState.UserId),
                new SqlParameter("@Amount",objState.Amount),
                new SqlParameter("@OnlineTransactionId",objState.TransactionCode),
                new SqlParameter("@ImageName",objState.ProductImage),
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (txtamount.Text != "")
                {
                   
                        //  if (Convert.ToDecimal(txtamount.Text) >= 6.25M)
                        //  {
                        objproduct.ProductImage= UploadImage();
                        
                        objproduct.MentionBy = Session["userid"].ToString();
                    objproduct.UserId = Session["userid"].ToString();

                    objproduct.Amount = Convert.ToDecimal(txtamount.Text);
                    objproduct.TransactionCode = txttransactionid.Text;
                       
                        string res = Insert_ProductPurchase(objproduct);
                        if (res == "t")
                        {
                            string popupScript = "alert('Saving Product Purchase Request Added Successfully');";
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                            txttransactionid.Text = "";
                        }
                        else
                        {
                            string popupScript = "alert('Unknown error occurred');";
                            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        }


                    //  }
                    //  else
                    //  {
                    ///      Message.Show("Withdrwal Amount Must Be Greater Than 6.25$...!!!");
                    //  }

                }
                else
                {
                    Message.Show("Enter Amount...!!!");
                }
            }
            else
            {
                Message.Show("Enter User Name...!!!");
            }
        }
        else
        {
            Message.Show("Enter User Id...!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
   
  
   
}
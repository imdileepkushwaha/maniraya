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
            
               
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
   
 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        

    }
    public string Insert_Product(clsProduct objState)
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
            s2 = "sp_add_SavingProductMaster";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@ProductName",objState.ProductName),
                new SqlParameter("@MRP",objState.MRP),
                new SqlParameter("@DP",objState.DP),
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
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        string str_imagename = "noimage.png";
        if (FileUpload1.HasFile)
        {
            str_imagename = Guid.NewGuid().ToString().Substring(0, 8) + "_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("~/ProductImage/") + str_imagename);
        }

        objState.ProductName = txtproductname.Text;
        objState.MRP = Convert.ToDecimal( txtmrp.Text);
        objState.DP = txtdp.Text;
        objState.ProductImage = txtproductname.Text;
       
        objState.MentionBy = Session["useradmin"].ToString();
        string res = Insert_Product(objState);
        if (res == "t")
        {
            string popupScript = "alert('Saving Product Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtproductname.Text = txtmrp.Text = txtdp.Text = "";

        }
        else
            if (res == "f")
            {
                string popupScript = "alert('Product Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknown error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }     
    }


 
}
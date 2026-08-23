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
        decimal dp;
        if (!decimal.TryParse(Convert.ToString(objState.DP), System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out dp)
            && !decimal.TryParse(Convert.ToString(objState.DP), out dp))
        {
            return "0";
        }

        return SavingProductHelper.AddCatalogProduct(
            objState.ProductName,
            objState.MRP,
            dp,
            objState.ProductImage,
            objState.MentionBy,
            objState.GST,
            objState.HSNCODE);
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        decimal mrp;
        decimal gst;
        decimal dp;
        if (!decimal.TryParse(txtmrp.Text.Trim(), out mrp) || mrp < 0)
        {
            ShowAlert("Enter valid MRP.");
            return;
        }
        if (!decimal.TryParse(txtdp.Text.Trim(), out dp) || dp < 0)
        {
            ShowAlert("Enter valid DP.");
            return;
        }
        if (!decimal.TryParse(txtgst.Text.Trim(), out gst) || gst < 0)
        {
            ShowAlert("Enter valid GST.");
            return;
        }

        string str_imagename = "noimage.png";
        try
        {
            if (FileUpload1.HasFile)
            {
                string folder = Server.MapPath("~/ProductImage/");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string safeName = Path.GetFileName(FileUpload1.FileName);
                str_imagename = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + safeName;
                FileUpload1.SaveAs(Path.Combine(folder, str_imagename));
            }
        }
        catch (Exception ex)
        {
            ShowAlert("Image upload failed: " + ex.Message);
            return;
        }

        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtproductname.Text))
        {
            ShowAlert("Enter Product Name.");
            return;
        }

        objState.ProductName = txtproductname.Text.Trim();
        objState.MRP = mrp;
        objState.DP = dp.ToString(System.Globalization.CultureInfo.InvariantCulture);
        objState.HSNCODE = txthsncode.Text.Trim();
        objState.GST = gst;
        objState.ProductImage = str_imagename;
        objState.MentionBy = Session["useradmin"].ToString();

        string res = (Insert_Product(objState) ?? string.Empty).Trim().ToLowerInvariant();
        if (res == "t")
        {
            ShowAlert("Saving Product Added Successfully");
            txtgst.Text = txthsncode.Text = txtproductname.Text = txtmrp.Text = txtdp.Text = "";
        }
        else if (res == "f")
        {
            ShowAlert("Product Already Exists");
        }
        else if (res == "m")
        {
            ShowAlert("A product for this month already exists. Use Add Monthly Saving Product for month-wise products.");
        }
        else
        {
            ShowAlert("Unable to save product. Please try again.");
        }
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }


 
}
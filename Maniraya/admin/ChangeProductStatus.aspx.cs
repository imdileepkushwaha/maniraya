using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class admin_ChangeProductStatus : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loadProduct();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loadProduct()
    {
        DataTable dt = new DataTable();
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
        dt = objState.getProductAll(objState);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            objState.ProductId = lbllevel.Text;
            if (ChStatus.Checked == true)
            {
                objState.Status = "1";
            }
            else
            {
                objState.Status = "0";
            }
            objState.Update_ProductStatus(objState);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        loadProduct();
           
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Status = e.Row.FindControl("LblStatuschk") as Label;
            CheckBox ChkStatus = e.Row.FindControl("ChkStatus") as CheckBox;
            if (Status.Text=="1")
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
        loadProduct();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class UserJoiningEntry : System.Web.UI.Page
{
    clsvendor objbank = new clsvendor();
    clsProduct objP = new clsProduct();
    clsUser objUser = new clsUser();
    DataTable PurchaseDt;
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
            Response.Redirect("index.aspx");
        }
    }
    protected void txtsponserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();       
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtsponserid.Text;
        dt = objUser.selectUsernameJoiningpackage(objUser);
        if (dt.Rows.Count > 0)
        {
            txtsponsername.Text = dt.Rows[0]["username"].ToString();
            GridView2.DataSource = dt;
            GridView2.DataBind();

            bindtable2(dt);
          
        }
        else
        {
            txtsponsername.Text = "";
            txtsponserid.Text = "";
            string popupScript = "alert('Invalid User Id or Deactivate User or Invoice already generated');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
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
    public void CreateDatatable()
    {
        PurchaseDt = new DataTable();
        PurchaseDt.Columns.Add("ProductId");
        PurchaseDt.Columns.Add("ProductName");
        PurchaseDt.Columns.Add("Image");
        PurchaseDt.Columns.Add("Amount");
        PurchaseDt.Columns.Add("BV");      
        PurchaseDt.Columns.Add("Quantity");
        PurchaseDt.Columns.Add("TotalAmount");
    }

    public void bindtable2(DataTable dt)
    {
        //if (Convert.ToInt32(TxtAvailableStock.Text) < Convert.ToInt32(TxtPurchaseStock.Text))
        //{
        //    Message.Show("insufficient Stock");
        //    return;
        //}
        if (PurchaseDt == null)
        {
            CreateDatatable();
        }
        DataRow DR;
        foreach (GridViewRow Gr in GridView1.Rows)
        {
            DR = PurchaseDt.NewRow();
            DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
            DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
            DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
            DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
            DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
            DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
            DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
            PurchaseDt.Rows.Add(DR);

            if (((Label)Gr.FindControl("LblProductCodeG")).Text == DDLstProduct.SelectedValue)
            {
                string popupScript = "alert('this Product already add');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript3 = "Closepopup1();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                return;
            }
        }
        DR = PurchaseDt.NewRow();

        DR["ProductId"] = dt.Rows[0]["ProductId"].ToString() ;
        DR["ProductName"] = dt.Rows[0]["ProductName"].ToString();
        DR["Image"] = dt.Rows[0]["Image"].ToString();
        DR["Amount"] = dt.Rows[0]["Amount"].ToString();
        DR["BV"] = dt.Rows[0]["BV"].ToString();
        DR["Quantity"] = dt.Rows[0]["Quantity"].ToString();
        DR["TotalAmount"] = dt.Rows[0]["TotalAmount"].ToString(); //Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text);
        PurchaseDt.Rows.Add(DR);
        ViewState["PDT"] = PurchaseDt;
        PnlDt.Visible = true;
        GridView1.DataSource = PurchaseDt;
        GridView1.DataBind();
        ClearValue();  
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(TxtAvailableStock.Text) < Convert.ToInt32(TxtPurchaseStock.Text))
        {
            Message.Show("insufficient Stock");
            return;
        }
        if (PurchaseDt == null)
        {
            CreateDatatable();
        }
        DataRow DR;
        foreach (GridViewRow Gr in GridView1.Rows)
        {
            DR = PurchaseDt.NewRow();
            DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
            DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
            DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
            DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
            DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
            DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
            DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
            PurchaseDt.Rows.Add(DR);

            if (((Label)Gr.FindControl("LblProductCodeG")).Text == DDLstProduct.SelectedValue)
            {
                string popupScript = "alert('this Product already add');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript3 = "Closepopup1();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                return;
            }
        }
        DR = PurchaseDt.NewRow();

        DR["ProductId"] = DDLstProduct.SelectedValue;
        DR["ProductName"] = DDLstProduct.SelectedItem.Text;
        DR["Image"] = TxtImage.Text;
        DR["Amount"] = TxtPurchasePrice.Text;
        DR["BV"] = TxtPurchaseMRP.Text;
        DR["Quantity"] = TxtPurchaseStock.Text;
        DR["TotalAmount"] = Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text);
        PurchaseDt.Rows.Add(DR);
        ViewState["PDT"] = PurchaseDt;
        PnlDt.Visible = true;
        GridView1.DataSource = PurchaseDt;
        GridView1.DataBind();
        ClearValue();

    }
    private void ClearValue()
    {
        DDLstProduct.SelectedIndex = 0;
        TxtImage.Text = "";
        TxtPurchasePrice.Text = "";
        TxtPurchaseMRP.Text = "";
        TxtPurchaseStock.Text = "";
        TxtAvailableStock.Text = "0";
        TxtCGST.Text = "";
        TxtSGST.Text = "0";
        TxtpaybleAmount.Text = "";
        TxtIGST.Text = "0";
        TxtCGstAmount.Text = "";
        TxtSGstAmount.Text = "0";
        TxtIGstAmount.Text = "0";
    }
    private void cleardatarefresh()
    {
        TxtCGST.Text = "";
        TxtSGST.Text = "";
        TxtpaybleAmount.Text = "";
        TxtIGST.Text = "";
        TxtCGstAmount.Text = "";
        TxtSGstAmount.Text = "";
        TxtIGstAmount.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();       
        if (ViewState["PDT"] != null)
        {
            ViewState["PDT"] = null;
        }
        PurchaseDt = null;
        cleardatarefresh();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
      
            TxtProductCode.Text = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
            TxtProductName.Text = ((Label)GridView1.Rows[index].FindControl("LblProductNameG")).Text;
            TxtAmount.Text = ((Label)GridView1.Rows[index].FindControl("LblProductAmountG")).Text;
            TxtQuantity.Text = ((Label)GridView1.Rows[index].FindControl("lblQuantity")).Text;
            TxtTotalAmount.Text = ((Label)GridView1.Rows[index].FindControl("lblTotalAmount")).Text;
            TxtImage.Text = ((Label)GridView1.Rows[index].FindControl("LblProductImageG")).Text;
            TxtMrp.Text = ((Label)GridView1.Rows[index].FindControl("LblBv")).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

        }
        if (e.CommandName == "del")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {

                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string id = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
                DataTable table = ViewState["PDT"] as DataTable;
                table.Rows.RemoveAt(index);
                ViewState["PDT"] = table;
                GridView1.DataSource = table;
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    PnlDt.Visible = true;
                }
                ViewState["PDT"] = PurchaseDt;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (PurchaseDt == null)
        {
            CreateDatatable();
        }
        DataRow DR;
        foreach (GridViewRow Gr in GridView1.Rows)
        {
            if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
            {
                DR = PurchaseDt.NewRow();               
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = TxtAmount.Text;
                DR["BV"] = TxtMrp.Text;
                DR["Quantity"] = TxtQuantity.Text;
                DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
                PurchaseDt.Rows.Add(DR);
            }
            else
            {
                DR = PurchaseDt.NewRow();             
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                PurchaseDt.Rows.Add(DR);
            }
        }
        ViewState["PDT"] = PurchaseDt;
        GridView1.DataSource = PurchaseDt;
        GridView1.DataBind();
        ClearValue();
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
    }
    Decimal total = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmount")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblGrandTotal");
            TxtTotalPrice.Text= total.ToString();
            //HDTotal.Value = total.ToString();
        }
    }
    protected void DDLstProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Dt = objP.getProductForPurchaseselect(DDLstProduct.SelectedValue);
        DataTable StockDt = objP.getCheckStock(DDLstProduct.SelectedValue);
        if (Dt.Rows.Count > 0)
        {
            TxtImage.Text = Dt.Rows[0]["image"].ToString();
        }
        if (StockDt.Rows.Count > 0)
        {
            int Stock = Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString());
            TxtAvailableStock.Text = Stock.ToString();
        }
        else
        {
            TxtAvailableStock.Text = "0";
        }
    }
    protected void TxtCGST_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmitPurchase_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        Decimal t;
        if (HDTotal.Value== String.Empty )
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (!Decimal.TryParse(HDTotal.Value.ToString(), out t))
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        objbank.VendorId = txtsponserid.Text;
        objbank.PurchaseAmount = Convert.ToDecimal(TxtTotalPrice.Text);
        objbank.CGST = Convert.ToDecimal(TxtCGST.Text);
        objbank.SGST = Convert.ToDecimal(TxtSGST.Text);
        objbank.IGST = Convert.ToDecimal(TxtIGST.Text);
        objbank.CGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) *Convert.ToDecimal(TxtCGST.Text))/100,2);
        objbank.SGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtSGST.Text)) / 100, 2);
        objbank.IGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtIGST.Text)) / 100, 2);
        objbank.PaybleAmountAmount = Convert.ToDecimal(HDTotal.Value);
        objbank.Purchasedate = TxtPurchaseDate.Text;
        string i = objbank.UserJoiningPurchase(objbank, ViewState["PDT"] as DataTable);
        if (i == "t")
        {
            string popupScript = "alert('Purchase Successfull');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            GridView1.DataSource = null;
            GridView1.DataBind();
            PnlDt.Visible = false;
            if (ViewState["PDT"] != null)
            {
                ViewState["PDT"] = null;
            }
            PurchaseDt = null;
            ClearValue();
            txtsponserid.Text = "";
            txtsponsername.Text = "";

        }       
        else
        {
            string popupScript = "alert('unknown error');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
}
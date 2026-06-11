using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Data.SqlClient;
using DataTier;

public partial class FranchiseeTransferMaster : System.Web.UI.Page
{
    clsvendor objbank = new clsvendor();
    clsProduct objP = new clsProduct();
    clsfranchisee objF = new clsfranchisee();
    DataTable PurchaseDt;
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            if (!IsPostBack)
            {
               
                loadProduct();
                loadDonarFranchisee();
                LblType.Text = "Customer Price";
                TxtPurchasePrice.Visible = true;
                TxtDP.Visible = false;
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadDonarFranchisee()
    {
        if (Session["useradmin"] != null)
        {

            DataTable dt = new DataTable();
            objF.UserId = "F000001";
            dt = objF.getuserdetailviaprocedure(objF);
            if (dt.Rows.Count > 0)
            {

                ViewState["tstate"] = dt.Rows[0]["statenew"].ToString();
            }
            else
            {
                TxtFRanchiseeName.Text = "";
                TxtFranchiseeId.Text = "";

                Message.Show("Invalid User Id...!!!");
            }
        }
        else
        {
            DataTable dt = new DataTable();
            objF.UserId = Session["fuserid"].ToString();
            dt = objF.getuserdetailviaprocedure(objF);
            if (dt.Rows.Count > 0)
            {

                ViewState["tstate"] = dt.Rows[0]["statenew"].ToString();
            }
            else
            {
                TxtFRanchiseeName.Text = "";
                TxtFranchiseeId.Text = "";

                Message.Show("Invalid User Id...!!!");
            }
        }
    }
    void loadFranchisee()
    {
        DataTable dt = new DataTable();
        objF.UserId = TxtFranchiseeId.Text;
        dt = objF.getuserdetailviaprocedure(objF);
        if (dt.Rows.Count > 0)
        {
            TxtFRanchiseeName.Text = dt.Rows[0]["username"].ToString();
            ViewState["fstate"] = dt.Rows[0]["statenew"].ToString();
            ViewState["profit"] = dt.Rows[0]["profit"].ToString();
        }
        else
        {
            TxtFRanchiseeName.Text = "";
            TxtFranchiseeId.Text = "";

            Message.Show("Invalid User Id...!!!");
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
        PurchaseDt.Columns.Add("MRP");
        PurchaseDt.Columns.Add("BV");
        PurchaseDt.Columns.Add("DP");
        PurchaseDt.Columns.Add("STOCK");
        PurchaseDt.Columns.Add("TOTALBV");
        PurchaseDt.Columns.Add("TOTALDP");
        PurchaseDt.Columns.Add("Quantity");
        PurchaseDt.Columns.Add("TotalAmount");
        PurchaseDt.Columns.Add("CGST");
        PurchaseDt.Columns.Add("SGST");
        PurchaseDt.Columns.Add("IGST");
        PurchaseDt.Columns.Add("PurchaseAmount");
        PurchaseDt.Columns.Add("GSTPER");
    }    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
            if (PurchaseDt == null)
            {
                CreateDatatable();
            }
            int h = 0;
            if (!Int32.TryParse(TxtPurchaseStock.Text, out h))
            {
                string popupScript = "alert('Input only number in Sale Quantity !..');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript3 = "Closepopup1();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                return;
            }
            if (Convert.ToInt32(TxtAvailableStock.Text) < Convert.ToInt32(TxtPurchaseStock.Text))
            {
                string popupScript = "alert('You can Sale More than Stock');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript3 = "Closepopup1();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                return;
            }
            DataRow DR;
            foreach (GridViewRow Gr in GridView1.Rows)
            {
                DR = PurchaseDt.NewRow();             
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["DP"] = ((Label)Gr.FindControl("LblDP")).Text;
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBV")).Text;
                DR["TOTALDP"] = ((Label)Gr.FindControl("LblTotalDP")).Text;
                DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
                DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
                DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
                DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
                DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
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
            if (Convert.ToInt32(TxtAvailableStock.Text) >= Convert.ToInt32(TxtPurchaseStock.Text))
            {
                DR = PurchaseDt.NewRow();
                DR["ProductId"] = DDLstProduct.SelectedValue;
                DR["ProductName"] = DDLstProduct.SelectedItem.Text;
                DR["Image"] = TxtImage.Text;
                DR["Amount"] = TxtPurchasePrice.Text;
                DR["MRP"] = TxtPurchaseMRP.Text;
                DR["BV"] = TxtBV.Text;
                DR["DP"] = TxtDP.Text;
                DR["STOCK"] = TxtAvailableStock.Text;
                DR["TOTALBV"] = Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtBV.Text);
                DR["TOTALDP"] = Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtDP.Text);
                DR["Quantity"] = TxtPurchaseStock.Text;
                DR["TotalAmount"] = Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text);
                DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(HDGST.Value)), 2);
                if (ViewState["fstate"].ToString() == ViewState["tstate"].ToString())
                {
                    DR["CGST"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(HDGST.Value)))) / 2, 2);
                    DR["SGST"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(HDGST.Value)))) / 2, 2);
                    DR["IGST"] = "0";
                }
                else
                {
                    DR["CGST"] = "0";
                    DR["SGST"] = "0";
                    DR["IGST"] = Math.Round(Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(HDGST.Value))), 2);
                }
                DR["GSTPER"] = HDGST.Value;
                PurchaseDt.Rows.Add(DR);
                ViewState["PDT"] = PurchaseDt;
                PnlDt.Visible = true;
                GridView1.DataSource = PurchaseDt;
                GridView1.DataBind();
                ClearValue();
            }
            else
            {
                string popupScript = "alert('You can Sale More than Stock');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript3 = "Closepopup1();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                return;
            }
       
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
        TxtSGST.Text = "";
        TxtpaybleAmount.Text = "";
        TxtIGST.Text = "";
        TxtCGstAmount.Text = "";
        TxtSGstAmount.Text = "";
        TxtIGstAmount.Text = "";
        TxtDP.Text = "";
        TxtBV.Text = "";
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
            TxtDPEdit.Text = ((Label)GridView1.Rows[index].FindControl("LblDP")).Text;
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
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["DP"] = ((Label)Gr.FindControl("LblDP")).Text;
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["TOTALBV"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(((Label)Gr.FindControl("LblBv")).Text);
                DR["TOTALDP"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(((Label)Gr.FindControl("LblDP")).Text);
              
                DR["Quantity"] = TxtQuantity.Text;
                DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
                DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(((Label)Gr.FindControl("LblGSTPER")).Text)), 2);
                if (ViewState["fstate"].ToString() == ViewState["tstate"].ToString())
                {
                    DR["CGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(((Label)Gr.FindControl("LblGSTPER")).Text)))) / 2, 2);
                    DR["SGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(((Label)Gr.FindControl("LblGSTPER")).Text)))) / 2, 2);
                    DR["IGST"] = "0";
                }
                else
                {
                    DR["CGST"] = "0";
                    DR["SGST"] = "0";
                    DR["IGST"] = Math.Round(Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(((Label)Gr.FindControl("LblGSTPER")).Text))), 2);
                }
                DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;

                if (Convert.ToInt32(((Label)Gr.FindControl("LblStock")).Text) < Convert.ToInt32(TxtQuantity.Text))
                {
                    string popupScript = "alert('You can not add more than stock');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    string popupScript3 = "Closepopup1();";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                    return;
                }

                PurchaseDt.Rows.Add(DR);
            }
            else
            {
                DR = PurchaseDt.NewRow();
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["DP"] = ((Label)Gr.FindControl("LblDP")).Text;
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBV")).Text;
                DR["TOTALDP"] = ((Label)Gr.FindControl("LblTotalDP")).Text;
                DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
                DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
                DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
                DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
                DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
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
    Decimal tt = 0;
    Decimal totalPurchase = 0;
    Decimal totalGST = 0;
    Decimal totalCGST = 0;
    Decimal totalSGST = 0;
    Decimal totalIGST = 0;
    Decimal totalDP = 0;
    Decimal totalBV = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmount")).Text);
            totalDP += Convert.ToDecimal(((Label)e.Row.FindControl("LblTotalDP")).Text);
            totalBV += Convert.ToDecimal(((Label)e.Row.FindControl("LblTotalBV")).Text);
            totalPurchase += Convert.ToDecimal(((Label)e.Row.FindControl("LblPurchaseAmount")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);

            totalCGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);
            totalSGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalIGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          //  Label lblAmount = (Label)e.Row.FindControl("lblGrandTotal1");
            TxtTotalPrice.Text= total.ToString();
            HDTotal.Value = total.ToString();
            TxtTotalTotalDP.Text = totalDP.ToString();
            TxtTotalBV.Text = totalBV.ToString();
            TxtTotalpurchase.Text = totalPurchase.ToString();
            TxtpaybleAmount.Text = total.ToString();
            TxtTotalCGST.Text = totalCGST.ToString();
            TxtTotalSGST.Text = totalSGST.ToString();
            TxtTotalIGST.Text = totalIGST.ToString();
            TxtCash.Text = Convert.ToString((Convert.ToDecimal(TxtTotalBV.Text) * (Convert.ToDecimal(ViewState["profit"].ToString()))) / 100);
            TxtRestAmount.Text = Convert.ToString(Convert.ToDecimal(TxtTotalPrice.Text) - Convert.ToDecimal(TxtCash.Text));
        }
    }
    public DataTable getCheckStockfranchisee(string ID, String franchiseeId)
    {
        string str_query = "SELECT ProductID,isnull(Sum(CrQuantity),0) as Cr,isnull(Sum(DrQuantity),0) as Dr FROM FranchiseeStockMaster where ProductId='" + ID + "' and Franchiseeid='" + franchiseeId + "' and IsDistributer=0 GROUP BY ProductID ";
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
    protected void DDLstProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Dt = objP.getProductForPurchaseselectNew(DDLstProduct.SelectedValue);
        DataTable StockDt = getCheckStockfranchisee(DDLstProduct.SelectedValue, Session["fuserid"].ToString());
        if (Dt.Rows.Count > 0)
        {
            TxtImage.Text = Dt.Rows[0]["image"].ToString();
            TxtPurchasePrice.Text= Dt.Rows[0]["amount"].ToString();
            TxtPurchaseMRP.Text = Dt.Rows[0]["mrp"].ToString();
            TxtDP.Text = Dt.Rows[0]["DP"].ToString();
            TxtBV.Text = Dt.Rows[0]["BV"].ToString();
            HDGST.Value = Dt.Rows[0]["GST"].ToString();
            TxtGST.Text = Dt.Rows[0]["GST"].ToString();
        }
        if (StockDt.Rows.Count > 0 && (Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString()) > 0))
        {
            int Stock = Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString());
            TxtAvailableStock.Text = Stock.ToString();
            btnSubmit.Attributes.Remove("disabled");
        }
        else
        {
            TxtAvailableStock.Text = "0";
            btnSubmit.Attributes["disabled"] = "disabled";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('Insufficient Stock');", true);
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
        if (!Decimal.TryParse(TxtCash.Text.ToString(), out t))
        {
            string popupScript = "alert('Enter Cash Amount');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (!Decimal.TryParse(TxtRestAmount.Text.ToString(), out t))
        {
            string popupScript = "alert('Enter Rest Amount');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
      //  objbank.fr
        objbank.UserId = Session["fuserid"].ToString();
        objbank.MentionBy = Session["fuserid"].ToString();
        objbank.VendorId = TxtFranchiseeId.Text;
        objbank.PurchaseAmount = Convert.ToDecimal(TxtTotalpurchase.Text);
        objbank.CGST = 0; // Convert.ToDecimal(TxtCGST.Text);
        objbank.SGST = 0; //Convert.ToDecimal(TxtSGST.Text);
        objbank.IGST = 0; // Convert.ToDecimal(TxtIGST.Text);
        objbank.CGSTAmt = Convert.ToDecimal(TxtTotalCGST.Text); //Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) *Convert.ToDecimal(TxtCGST.Text))/100,2);
        objbank.SGSTAmt = Convert.ToDecimal(TxtTotalSGST.Text); //Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtSGST.Text)) / 100, 2);
        objbank.IGSTAmt = Convert.ToDecimal(TxtTotalIGST.Text); // Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtIGST.Text)) / 100, 2);
        objbank.PaybleAmountAmount = Convert.ToDecimal(HDTotal.Value);
        objbank.Purchasedate = DateTime.Now.ToString("dd/MMM/yyyy");
        objbank.CashAmount = Convert.ToDecimal(TxtCash.Text);
        objbank.RestAmount = Convert.ToDecimal(TxtRestAmount.Text);
        objbank.Purchasetype = DDlstPaymentMode.SelectedItem.Text;
        objbank.UpdateID = TxtTransactionNo.Text;
        DataTable Dt = FranchiseeTransferByFranchisee(objbank, ViewState["PDT"] as DataTable);
        if (Dt != null && Dt.Rows[0][1].ToString()=="1")
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

        }
        else if (Dt == null)
        {
            string popupScript = "alert('unknown error');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('"+Dt.Rows[0][0].ToString()+"');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    public DataTable FranchiseeTransferByFranchisee(clsvendor objP, DataTable Dt)
    {
        int i = 0;

        string res = "";
        string s2 = "";
        string ChkStock = "1";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        DataTable Dtresult = new DataTable();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {

            //foreach (DataRow Dr in Dt.Rows)
            //{
            //    string Q = "select isnull(Sum(CrQuantity),0) -isnull(Sum(DrQuantity),0) FROM StockMaster where ProductId='" + Dr["ProductId"].ToString() + "'";
            //    DataSet Ds = ObjData.RunSelectQueryTrans(Q, tr);
            //    if (Convert.ToInt32(Dr["Quantity"].ToString()) > Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString()))
            //    {
            //        ChkStock = "0";
            //    }
            //}
            if (ChkStock == "1")
            {
                s2 = "sp_Transfer_Franchisee_Purchase";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",objP.UserId),
                       new SqlParameter("@PurchaseAmount",objP.PurchaseAmount),
                          new SqlParameter("@CGSTAmount",objP.CGSTAmt),
                             new SqlParameter("@SGSTAmount",objP.SGSTAmt),
                                 new SqlParameter("@IGSTAmount",objP.IGSTAmt),
                                   new SqlParameter("@CGSTPer","0"),
                             new SqlParameter("@SGSTPer","0"),
                                 new SqlParameter("@IGSTPer","0"),
                                    new SqlParameter("@Paybleamount",objP.PaybleAmountAmount),
                    new SqlParameter("@FranchiseeId",objP.VendorId),
                       new SqlParameter("@PurchaseProduct",Dt),
                       new SqlParameter("@Cashamount",objP.CashAmount),
                       new SqlParameter("@RestAmount",objP.RestAmount),
                         new SqlParameter("@Paymentmode",objP.Purchasetype),
                       new SqlParameter("@ChequeNo",objP.UpdateID),
                         new SqlParameter("@Mentionby",objP.MentionBy),
                   new SqlParameter("@IsDistributer","0")

                };
                Dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = Dtresult.Rows[0][1].ToString();

                tr.Commit();
            }
            else
            {
                res = "3";
            }



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
        return Dtresult;

    }
    protected void TxtFranchiseeId_TextChanged(object sender, EventArgs e)
    {
        loadFranchisee();
    }
    protected void TxtCash_TextChanged(object sender, EventArgs e)
    {
        decimal f = 0;
        if (TxtTotalPrice.Text != string.Empty)
        {
            if (decimal.TryParse(TxtTotalTotalDP.Text, out f))
            {
                TxtRestAmount.Text = Convert.ToString(Convert.ToDecimal(TxtTotalTotalDP.Text) - Convert.ToDecimal(TxtCash.Text));
            }
        }
    }
    protected void TxtRestAmount_TextChanged(object sender, EventArgs e)
    {
        decimal f = 0;
        if (TxtTotalPrice.Text != string.Empty)
        {
            if (decimal.TryParse(TxtTotalTotalDP.Text, out f))
            {
                TxtCash.Text = Convert.ToString(Convert.ToDecimal(TxtTotalTotalDP.Text) - Convert.ToDecimal(TxtRestAmount.Text));
            }
        }
    }
    
}
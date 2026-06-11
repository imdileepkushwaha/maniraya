using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Text.RegularExpressions;

public partial class UserPurchaseMaster : System.Web.UI.Page
{
    clsvendor objbank = new clsvendor();
    clsProduct objP = new clsProduct();
    clsfranchisee objF = new clsfranchisee();
    clsAccount objaccount = new clsAccount();
    clsUser objU = new clsUser();
    DataTable PurchaseDt;
    public bool CGST = true;
    bool SGST = true;
    bool IGST = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] != null)
        {
            if (!IsPostBack)
            {
                TxtFranchiseeId.Enabled = false;
                TxtFranchiseeId.Text = Session["fuserid"].ToString();
                loadFranchisee();
                loadProduct();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
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
        }
        else
        {
            TxtFRanchiseeName.Text = "";
            TxtFranchiseeId.Text = "";

            Message.Show("Invalid Franchisee Id...!!!");
        }
    }

    void loaduser()
    {
        DataTable dt = new DataTable();
        objU.UserId = TxtuserId.Text;
        dt = objU.getUserName(objU);
        if (dt.Rows.Count > 0)
        {
            TxtuserName.Text = dt.Rows[0]["username"].ToString();
            ViewState["ustate"] = dt.Rows[0]["username"].ToString();
        }
        else
        {
            TxtuserName.Text = "";
            TxtuserId.Text = "";

            Message.Show("Invalid User Id...!!!");
        }
    }
    void userbalance()
    {
        objaccount.UserId = TxtuserId.Text;
        DataTable dtrechrge = objaccount.getAccountBalance(objaccount);
        if(dtrechrge !=null && dtrechrge.Rows.Count>0)
        {
            Txtshoppingbalance.Text = dtrechrge.Rows[0][0].ToString();
        }
        else
        {
            Txtshoppingbalance.Text = "0";
        }
        Txtshoppingbalance.Text = "0";

    }
    void loadProduct()
    {
        DDLstProduct.Items.Clear();
        DataTable dt = new DataTable();
        dt = objP.getProductForuserPurchase(Session["fuserid"].ToString());
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
            DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
            DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
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
            DR["BV"] = Txtbv.Text;
            DR["STOCK"] = TxtAvailableStock.Text;
            DR["GSTPER"] = TxtGST.Text;
            DR["TOTALBV"] = Convert.ToDecimal(Txtbv.Text) * Convert.ToDecimal(TxtPurchaseStock.Text);
            DR["Quantity"] = TxtPurchaseStock.Text;
            DR["TotalAmount"] = Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text);
            DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(TxtGST.Text)), 2);
            if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
            {
                DR["CGST"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(TxtGST.Text)))) / 2, 2);
                DR["SGST"] = Math.Round((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(TxtGST.Text)))) / 2, 2);
                DR["IGST"] = "0";
            }
            else
            {
                DR["CGST"] = "0";
                DR["SGST"] = "0";
                DR["IGST"] = Math.Round(Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) - ((Convert.ToDecimal(TxtPurchaseStock.Text) * Convert.ToDecimal(TxtPurchasePrice.Text) * 100) / (100 + Convert.ToDecimal(TxtGST.Text))), 2);
            }
            PurchaseDt.Rows.Add(DR);
            ViewState["PDT"] = PurchaseDt;
            PnlDt.Visible = true;
            GridView1.DataSource = PurchaseDt;
            GridView1.DataBind();
            ClearValue();
            string popupScript2 = "gettotal2();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
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
            TxtMrp.Text = ((Label)GridView1.Rows[index].FindControl("LBlMrp")).Text;
            TxtBvedit.Text = ((Label)GridView1.Rows[index].FindControl("LblBv")).Text;
            TxtBvedittotal.Text = ((Label)GridView1.Rows[index].FindControl("LblTotalBv")).Text;
            TxtGSTedit.Text = ((Label)GridView1.Rows[index].FindControl("LblGSTPER")).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModal();", true);

        }
        if (e.CommandName == "del")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {

                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string id = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
                if (PurchaseDt == null)
                {
                    CreateDatatable();
                }
                DataRow DR;
                foreach (GridViewRow Gr in GridView1.Rows)
                {
                    if (((Label)Gr.FindControl("LblProductCodeG")).Text != id)
                    {

                        DR = PurchaseDt.NewRow();
                        DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                        DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                        DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                        DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                        DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                        DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                        DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                        DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
                        DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                        DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                        DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;                      
                        DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
                        DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
                        DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
                        DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
                        PurchaseDt.Rows.Add(DR);
                    }
                }
                ViewState["PDT"] = PurchaseDt;
                GridView1.DataSource = PurchaseDt;
                GridView1.DataBind();
                ClearValue();
                //DataTable table = ViewState["PDT"] as DataTable;
                //table.Rows.RemoveAt(index);
                //ViewState["PDT"] = table;
                //GridView1.DataSource = table;
                //GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    PnlDt.Visible = false;
                }
                //ViewState["PDT"] = PurchaseDt;
            }
            string popupScript3 = "gettotal2();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
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
                TxtQuantity.Text = TxtQuantity.Text.Replace(",", "");
                TxtAmount.Text = TxtAmount.Text.Replace(",", "");
                TxtBvedit.Text = TxtBvedit.Text.Replace(",", "");
                TxtMrp.Text = TxtMrp.Text.Replace(",", "");
                TxtGSTedit.Text = TxtGSTedit.Text.Replace(",", "");
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["Amount"] = TxtAmount.Text;
                DR["MRP"] = TxtMrp.Text;
                DR["BV"] = TxtBvedit.Text;
                DR["GSTPER"] = TxtGSTedit.Text;
                DR["TOTALBV"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtBvedit.Text);
                DR["Quantity"] = TxtQuantity.Text;
                DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
                DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text)*100)/(100+Convert.ToDecimal(TxtGST.Text)),2);
                if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
                {
                    DR["CGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(TxtGSTedit.Text)))) / 2, 2);
                    DR["SGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(TxtGSTedit.Text)))) / 2, 2);
                    DR["IGST"] = "0";
                }
                else
                {
                    DR["CGST"] = "0";
                    DR["SGST"] = "0";
                    DR["IGST"] = Math.Round(Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(TxtGSTedit.Text))), 2);
                }
                if (Convert.ToInt32(((Label)Gr.FindControl("LblStock")).Text) < Convert.ToInt32(TxtQuantity.Text))
                {
                    string msg = "You can Purchase More than Stock " + ((Label)Gr.FindControl("LblStock")).Text;
                    string popupScript54 = "alert('" + msg + "');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript54, true);
                    string popupScript45 = "Closepopup();";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript45, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModal();", true);
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
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
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
        string popupScript3 = "gettotal2();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
    }
    Decimal total = 0;
    Decimal totalPurchase = 0;
    Decimal totalBV = 0;
    Decimal totalGST = 0;
    Decimal totalCGST = 0;
    Decimal totalSGST = 0;
    Decimal totalIGST = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmount")).Text);
            totalPurchase += Convert.ToDecimal(((Label)e.Row.FindControl("LblPurchaseAmount")).Text);
            totalBV += Convert.ToDecimal(((Label)e.Row.FindControl("LblTotalBv")).Text);
               totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);

            totalCGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);
            totalSGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalIGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
            if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
            {
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
                GridView1.Columns[10].Visible = false;
            }
            else
            {
                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
                GridView1.Columns[10].Visible = true;
            }

           

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblGrandTotal");
            TxttotalPurchase.Text = totalPurchase.ToString();
            TxttotalBV.Text = totalBV.ToString();
          
            TxtTotalGST.Text = totalGST.ToString();
            TxtTotalPrice.Text = total.ToString();
            HDTotal.Value = total.ToString();
            if (Convert.ToDecimal(Txtshoppingbalance.Text) > Convert.ToDecimal(HDTotal.Value))
            {
                Txtrest.Text = HDTotal.Value;
            }
            else
            {
                Txtrest.Text = Txtshoppingbalance.Text;
            }
            Txtcash.Text = Convert.ToString(Convert.ToDecimal(HDTotal.Value) - Convert.ToDecimal(Txtrest.Text));            
            TxtpaybleAmount.Text = total.ToString();
            HDCGST.Value = totalCGST.ToString();
            HDSGST.Value = totalSGST.ToString();
            HDIGST.Value = totalIGST.ToString();
        }
    }
    protected void DDLstProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Dt = objP.getProductForPurchaseselect(DDLstProduct.SelectedValue);
        DataTable StockDt = objP.getCheckStockfranchisee(DDLstProduct.SelectedValue, Session["fuserid"].ToString());
        if (Dt.Rows.Count > 0)
        {
            TxtImage.Text = Dt.Rows[0]["image"].ToString();
            TxtPurchasePrice.Text = Dt.Rows[0]["amount"].ToString();
            TxtPurchaseMRP.Text = Dt.Rows[0]["mrp"].ToString();
            Txtbv.Text = Dt.Rows[0]["bv"].ToString();
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
        if (HDTotal.Value == String.Empty)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (Txtcash.Text == String.Empty)
        {
            string popupScript = "alert('Enter Cash amount');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        Decimal FF = 0;
        if (!Decimal.TryParse(Txtcash.Text,out FF))
        {
            string popupScript = "alert('Enter Number only in Cash amount');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (!Decimal.TryParse(HDTotal.Value.ToString(), out t))
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        objbank.VendorId = TxtFranchiseeId.Text;
        objbank.PurchaseAmount = Convert.ToDecimal(TxttotalPurchase.Text);
        objbank.CGST = Convert.ToDecimal(TxtCGST.Text);
        objbank.SGST = Convert.ToDecimal(TxtSGST.Text);
        objbank.IGST = Convert.ToDecimal(TxtIGST.Text);
        // objbank.CGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtCGST.Text)) / 100, 2);
        // objbank.SGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtSGST.Text)) / 100, 2);
        //  objbank.IGSTAmt = Math.Round((Convert.ToDecimal(TxtTotalPrice.Text) * Convert.ToDecimal(TxtIGST.Text)) / 100, 2);
        objbank.CGSTAmt = Convert.ToDecimal(HDCGST.Value);
        objbank.SGSTAmt = Convert.ToDecimal(HDSGST.Value);
        objbank.IGSTAmt = Convert.ToDecimal(HDIGST.Value);

        objbank.PaybleAmountAmount = Convert.ToDecimal(HDTotal.Value);
        objbank.CashAmount = Convert.ToDecimal(Txtcash.Text);
        objbank.RestAmount = Convert.ToDecimal(Txtrest.Text);
        objbank.Purchasedate = DateTime.Now.ToString("dd/MMM/yyyy");
        objbank.UserId = TxtuserId.Text;
        DataTable Dt = objbank.AddPurchase123(objbank, ViewState["PDT"] as DataTable);
        if (Dt.Rows[0][1].ToString() == "1")
        {

            a_href.Attributes["href"] = "~/user/MehndilinkInvoice.aspx?OrderNo=" + Dt.Rows[0]["Purchaseid"].ToString();
                string popupScript = "alert('Purchase Successfull, OrderNo is " + Dt.Rows[0]["OrderNo"].ToString() + "');location.href='UserPurchaseMaster.aspx';";
                lblmssg.Text = "Purchase Successfull, OrderNo is " + Dt.Rows[0]["OrderNo"].ToString();
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "showSearchModal();", true);
           
           
          
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
        else
        {
            string popupScript = "alert('unknown error');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }

    protected void TxtFranchiseeId_TextChanged(object sender, EventArgs e)
    {
        loadFranchisee();
    }

    protected void TxtuserId_TextChanged(object sender, EventArgs e)
    {
        loaduser();
        userbalance();
    }

  
}
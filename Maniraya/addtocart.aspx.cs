using BusinessLogicTier;
using DataTier;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class addtocart : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    private int PageSize = 12;
    DataTable PurchaseDt;
    Decimal TAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {

            if (!IsPostBack)
            {
                UpdateBal();
                loadaddcartitem(Session["userid"].ToString());
              

                // loadProduct(1);
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    public void UpdateBal()
    {
        DataTable dt = new DataTable();
        if (Session["userid"] != null)
        {
            objuser.UserId = Session["userid"].ToString();
            dt = objuser.getUserDetail(objuser);
            if (dt.Rows.Count > 0)
            {

                hfwalllet.Value = Math.Round(Convert.ToDecimal(dt.Rows[0]["balanceamount"].ToString()), 2).ToString();
            }
        }


    }

    public void CreateDatatable()
    {

        PurchaseDt = new DataTable();
        PurchaseDt.Columns.Add("Franchiseeid");
        PurchaseDt.Columns.Add("ProductId");
        PurchaseDt.Columns.Add("SubProductId");
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
    void loadaddcartitem(string id)
    {
        rptCart.DataSource = null;
        rptCart.DataBind();
        objState.UserId = id;
        DataTable Dt = objState.getCartItems(objState);
        if (Dt.Rows.Count > 0)
        {

            rptCart.DataSource = Dt;
            rptCart.DataBind();
            LblProductcount.Text = Dt.Rows.Count.ToString() + " Products";
            LinkButton1.Visible = true;
            CalculateSummary(Dt);
        }
        else
        {
            LinkButton1.Visible = false;
        }


    }
    void CalculateSummary(DataTable dt)
    {
        decimal purchaseamounttotal = 0;
        decimal GSTtotal = 0;
        decimal Totaltotal = 0;
        decimal TotalMRP = 0;
        decimal WalletDeduction = 0;
        decimal shipping = 0;
        foreach (DataRow row in dt.Rows)
        {
            purchaseamounttotal += Convert.ToDecimal(row["PurchaseAmount"]);
            GSTtotal += Convert.ToDecimal(row["CGST"]) + Convert.ToDecimal(row["SGST"]) + Convert.ToDecimal(row["IGST"]);
            Totaltotal += Convert.ToDecimal(row["TotalAmount"]);
            TotalMRP += Convert.ToDecimal(row["MRP"]) * Convert.ToInt32(row["Quantity"]);
        }
        WalletDeduction = Math.Round(Totaltotal * 6 / 100,2);
        
        if(Convert.ToDecimal(hfwalllet.Value)>= WalletDeduction)
        {
            Lblwalletdeduction.Text = "₹ " + WalletDeduction.ToString();
        }
        else
        {
            Lblwalletdeduction.Text = "₹ 0";
        }
        decimal discount = TotalMRP - Totaltotal; //(Totaltotal/ TotalMRP*100);
        decimal tax = GSTtotal;
        decimal total = Totaltotal- WalletDeduction;
        if(Totaltotal<1000)
        {
            shipping =40;
            lblShipping.Text= "₹ " + shipping.ToString("0.00");
        }
        else
        {
            lblShipping.Text = "₹ 0";
        }
        total = total - shipping;
        lblSubtotal.Text = "₹ " + purchaseamounttotal.ToString("0.00");
        lblDiscount.Text = "₹ " + discount.ToString("0.00");
        lblTax.Text = "₹ " + tax.ToString("0.00");
        lblTotal.Text = "₹ " + total.ToString("0.00");
    }


    protected void IncreaseQty(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        objState.CategoryId = id.ToString();
        objState.BATCHNO = "P";       
        string res = objState.updatecartitem(objState);
        loadaddcartitem(Session["userid"].ToString());
    }
    protected void DecreaseQty(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        objState.CategoryId = id.ToString();
        objState.BATCHNO = "M";
        string res = objState.updatecartitem(objState);
        loadaddcartitem(Session["userid"].ToString());
    }
    protected void RemoveQty(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        objState.ProductId = id.ToString();
        string s = objState.DeleteCartItems(objState);
        loadaddcartitem(Session["userid"].ToString());
    }
    

    protected void Unnamed_Click(object sender, EventArgs e)
    {
       
        objState.UserId = Session["userid"].ToString();
        string s = objState.DeleteAllCartItems(objState);
        loadaddcartitem(Session["userid"].ToString());
        lblSubtotal.Text = "₹" ;
        lblDiscount.Text = "₹" ;
        lblTax.Text = "₹" ;
        lblTotal.Text = "₹" ;
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        //Decimal CGST = 0; Decimal SGST = 0;Decimal IGST = 0; Decimal Subtotal = 0; Decimal total = 0;
        //if (rptCart.Items.Count == 0)
        //{
        //    string popupScript = "alert('add to cart any Product');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    return;
        //}
        //if (PurchaseDt == null)
        //{
        //    CreateDatatable();
        //}
        //DataRow DR;
        //foreach (RepeaterItem item in rptCart.Items)
        //{
        //    DR = PurchaseDt.NewRow();

        //    DR["Franchiseeid"] = ((HiddenField)item.FindControl("hdnFranchiseeId")).Value;
        //    DR["ProductId"] = ((HiddenField)item.FindControl("hdnProductId")).Value;
        //    DR["SubProductId"] = ((HiddenField)item.FindControl("hdnSubProductId")).Value;
        //    DR["ProductName"] = ((HiddenField)item.FindControl("hdnProductName")).Value;
        //    DR["Image"] = "";
        //    DR["Amount"] = ((HiddenField)item.FindControl("hdnAmount")).Value;
        //    DR["MRP"] = ((HiddenField)item.FindControl("hdnMRP")).Value;
        //    DR["BV"] = ((HiddenField)item.FindControl("hdnBV")).Value;
        //    DR["DP"] = ((HiddenField)item.FindControl("hdnDP")).Value;
        //    DR["STOCK"] = "0";
        //    DR["TOTALBV"] = ((HiddenField)item.FindControl("hdnTOTALBV")).Value;
        //    DR["TOTALDP"] = ((HiddenField)item.FindControl("hdnTOTALDP")).Value;
        //    DR["Quantity"] = ((HiddenField)item.FindControl("hdnQuantity")).Value;
        //    DR["TotalAmount"] = ((HiddenField)item.FindControl("hdnTOTALAMOUNT")).Value;
        //    DR["PurchaseAmount"] = ((HiddenField)item.FindControl("hdnpurchaseAmount")).Value;
        //    DR["CGST"] = ((HiddenField)item.FindControl("hdnCGST")).Value;
        //    DR["SGST"] = ((HiddenField)item.FindControl("hdnSGST")).Value;
        //    DR["IGST"] = ((HiddenField)item.FindControl("hdnIGST")).Value;
        //    CGST += Convert.ToDecimal(((HiddenField)item.FindControl("hdnCGST")).Value);
        //    SGST += Convert.ToDecimal(((HiddenField)item.FindControl("hdnSGST")).Value);
        //    IGST += Convert.ToDecimal(((HiddenField)item.FindControl("hdnIGST")).Value);
        //    Subtotal+= Convert.ToDecimal(((HiddenField)item.FindControl("hdnpurchaseAmount")).Value);
        //    total += Convert.ToDecimal(((HiddenField)item.FindControl("hdnTOTALAMOUNT")).Value);

        //    DR["GSTPER"] = ((HiddenField)item.FindControl("hdnGST")).Value;

        //    PurchaseDt.Rows.Add(DR);

        //}

        //objState.PurchaseAmount = Convert.ToDecimal(Subtotal);
        //objState.CGST = Convert.ToDecimal(CGST);
        //objState.SGST = Convert.ToDecimal(SGST);
        //objState.IGST = Convert.ToDecimal(IGST);
        //objState.TotalAmount = Convert.ToDecimal(total);
        //objState.FranchiseeID = "F000001";
        //objState.UserId = Session["userid"].ToString();
        //objState.ProductId = "1";
        //Session["CartItem"] = PurchaseDt;
        //string i =objState.AddPurchaseOutside(objState, PurchaseDt, Convert.ToDecimal(0));
        //if (i == "1")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Purchase Successfull');window.location.href='index.aspx';", true);
        // //   string popupScript = "alert('Purchase Successfull');";
        // //   ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        // //   GridView1.DataSource = null;
        // //   GridView1.DataBind();
        // //   PurchasePanel.Visible = false;
        //    //if (ViewState["PDT"] != null)
        //    //{
        //    //    ViewState["PDT"] = null;
        //    //}
        //    //PurchaseDt = null;

        //}
        //else if (i == "2")
        //{
        //    string popupScript = "alert('you have insufficient balance');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "3")
        //{
        //    string popupScript = "alert('insufficient stock');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "4")
        //{
        //    string popupScript = "alert('Your already topup with this plan !');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "5")
        //{
        //    string popupScript = "alert('Already one request is pending please approve/Reject first !');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "6")
        //{
        //    string popupScript = "alert('you can purchase only 1000/2000/3000/4000 for freedom Plan !');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "7")
        //{
        //    string popupScript = "alert('you can purchase only 600/1200/1800/2400 for Unity Plan !');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "8")
        //{
        //    string popupScript = "alert('you can purchase only 2000 for Global Plan !');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else if (i == "9")
        //{
        //    string popupScript = "alert('First purchase minimum should be 26 Point!');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        //else
        //{
        //    string popupScript = "alert('unknown error');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        Response.Redirect("addaddress.aspx");
    }
}
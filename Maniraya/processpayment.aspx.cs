using BusinessLogicTier;
using DataTier;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class processpayment : System.Web.UI.Page
{
    DataTable PurchaseDt;
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["userid"] = "TW000001";
        if (Session["userid"] != null)
        {

            if (!IsPostBack)
            {

                loadaddcartitem(Session["userid"].ToString());
                loadaddress(Session["userid"].ToString());

                // loadProduct(1);
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    void loadaddcartitem(string id)
    {
       
        objState.UserId = id;
        DataTable Dt = objState.getCartItems(objState);
        if (Dt.Rows.Count > 0)
        {       
           

            CalculateSummary(Dt);
        }


    }
    void loadaddress(string id)
    {
       
        DataTable Dt = objuser.getUsercartaddress(id);
        if (Dt.Rows.Count > 0)
        {

            Lblusername.Text = Dt.Rows[0]["UserName"].ToString();
            Lbladdress.Text = Dt.Rows[0]["Addressfirst"].ToString()+" "+ Dt.Rows[0]["AddressSecond"].ToString();
            Lblcity.Text = Dt.Rows[0]["CityName"].ToString()+" ,";
            LblPincode.Text = Dt.Rows[0]["Pincode"].ToString();
            Lblmobile.Text = Dt.Rows[0]["mobile"].ToString();
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
    void CalculateSummary(DataTable dt)
    {
        decimal purchaseamounttotal = 0;
        decimal GSTtotal = 0;
        decimal Totaltotal = 0;
        decimal TotalMRP = 0;

        foreach (DataRow row in dt.Rows)
        {
            purchaseamounttotal += Convert.ToDecimal(row["PurchaseAmount"]);
            GSTtotal += Convert.ToDecimal(row["CGST"]) + Convert.ToDecimal(row["SGST"]) + Convert.ToDecimal(row["IGST"]);
            Totaltotal += Convert.ToDecimal(row["TotalAmount"]);
            TotalMRP += Convert.ToDecimal(row["MRP"]) * Convert.ToInt32(row["Quantity"]);
        }

        decimal discount = TotalMRP - Totaltotal; //(Totaltotal/ TotalMRP*100);
        decimal tax = GSTtotal;
        decimal total = Totaltotal;

        lblSubtotal.Text = "₹" + purchaseamounttotal.ToString("0.00");
        lblDiscount.Text = "₹" + discount.ToString("0.00");
        lblTax.Text = "₹" + tax.ToString("0.00");
        lblTotal.Text = "₹" + Totaltotal.ToString("0.00");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       

    }

    protected void Btnpayment_Click(object sender, EventArgs e)
    {
        Decimal CGST = 0; Decimal SGST = 0; Decimal IGST = 0; Decimal Subtotal = 0; Decimal total = 0;
        if (PurchaseDt == null)
        {
            CreateDatatable();
        }
        objState.UserId = Session["userid"].ToString();
        DataTable Dt = objState.getCartItems(objState);
        if (Dt.Rows.Count > 0)
        {
            DataRow DR;
            foreach (DataRow row in Dt.Rows)
            {
                DR = PurchaseDt.NewRow();

                DR["Franchiseeid"] = row["Franchiseeid"].ToString();
                DR["ProductId"] = row["Productid"].ToString();
                DR["SubProductId"] = row["SubProductID"].ToString();
                DR["ProductName"] = row["ProductName"].ToString();
                DR["Image"] = "";
                DR["Amount"] = row["Amount"].ToString();
                DR["MRP"] = row["MRP"].ToString();
                DR["BV"] = row["BV"].ToString();
                DR["DP"] = row["DP"].ToString();
                DR["STOCK"] = "0";
                DR["TOTALBV"] = row["TOTALBV"].ToString();
                DR["TOTALDP"] = row["TOTALDP"].ToString();
                DR["Quantity"] = row["Quantity"].ToString();
                DR["TotalAmount"] = row["TotalAmount"].ToString();
                DR["PurchaseAmount"] = row["PurchaseAmount"].ToString();
                DR["CGST"] = row["CGST"].ToString();
                DR["SGST"] = row["SGST"].ToString();
                DR["IGST"] = row["IGST"].ToString();
                CGST += Convert.ToDecimal(row["CGST"].ToString());
                SGST += Convert.ToDecimal(row["SGST"].ToString());
                IGST += Convert.ToDecimal(row["IGST"].ToString());
                Subtotal += Convert.ToDecimal(row["PurchaseAmount"].ToString());
                total += Convert.ToDecimal(row["TotalAmount"].ToString());

                DR["GSTPER"] = row["GST"].ToString();

                PurchaseDt.Rows.Add(DR);
            }

            objState.PurchaseAmount = Convert.ToDecimal(Subtotal);
            objState.CGST = Convert.ToDecimal(CGST);
            objState.SGST = Convert.ToDecimal(SGST);
            objState.IGST = Convert.ToDecimal(IGST);
            objState.TotalAmount = Convert.ToDecimal(total);
            objState.FranchiseeID = "F000001";
            objState.UserId = Session["userid"].ToString();
            objState.ProductId = "1";
            Session["CartItem"] = PurchaseDt;
            objState.TransactionCode = txtransactionid.Text;
            string i = objState.AddPurchaseOutside(objState, PurchaseDt, Convert.ToDecimal(0));
            if (i == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Purchase Successfull');window.location.href='index.aspx';", true);
                //   string popupScript = "alert('Purchase Successfull');";
                //   ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                //   GridView1.DataSource = null;
                //   GridView1.DataBind();
                //   PurchasePanel.Visible = false;
                //if (ViewState["PDT"] != null)
                //{
                //    ViewState["PDT"] = null;
                //}
                //PurchaseDt = null;

            }
            else if (i == "2")
            {
                string popupScript = "alert('you have insufficient balance');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "3")
            {
                string popupScript = "alert('insufficient stock');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "4")
            {
                string popupScript = "alert('Your already topup with this plan !');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "5")
            {
                string popupScript = "alert('Already one request is pending please approve/Reject first !');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "6")
            {
                string popupScript = "alert('you can purchase only 1000/2000/3000/4000 for freedom Plan !');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "7")
            {
                string popupScript = "alert('you can purchase only 600/1200/1800/2400 for Unity Plan !');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "8")
            {
                string popupScript = "alert('you can purchase only 2000 for Global Plan !');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (i == "9")
            {
                string popupScript = "alert('First purchase minimum should be 26 Point!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('unknown error');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }
}
